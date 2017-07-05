using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class CertEquipmentRepository : EntityBaseRepository<CERT_Equipment>, ICertEquipmentRepository
    {
        #region Constructor
        public CertEquipmentRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions
        protected override string ApplySortField(string item)
        {
            var split = item.Trim().Split(' ');
            var c = split.Count();
            var field = split[0];
            var dir = "asc";
            if (c > 1)
            {
                dir = split[1];
            }
            switch (field)
            {
                case "studyname":
                    return string.Format("trial.TrialName {0}", dir);
                case "studyalias":
                    return string.Format("trial.TrialAlias {0}", dir);
                case "equipment.mainserialnum":
                    return string.Format("equipment.MainSerialNum {0}", dir);
                case "equipment.equipmentmodel.manufacturername":
                    return string.Format("emodel.ManufacturerName {0}", dir);
                case "equipment.equipmentmodel.manufacturermodel":
                    return string.Format("emodel.ManufacturerModel {0}", dir);
                case "equipment.affiliation.name":
                    return string.Format("affiliation.AffiliationName {0}", dir);
                case "equipment.affiliation.country.name":
                    return string.Format("country.CountryName {0}", dir);
                case "procedure.name":
                    return string.Format("procedure.ImgProcedureName {0}", dir);
                default:
                    return "";
            }
        }

        public IQueryable<CERT_Equipment> GetAll(CONTACT_User u, long? studyId, long? affiliationId, long? equipmentId, long? procedureId, bool? isActive, bool? isCertified, bool? hasPrevCert, string assignedTo, string search, string sort)
        {
            var entities = GetAll();
            var query = from certequipment in entities
                        join trial in Context.PACS_Trials on certequipment.TrialID equals trial.TrialID
                        join procedure in Context.CERT_ImgProcedureLists on certequipment.ImgProcedureID equals procedure.ImgProcedureID
                        join equipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals equipment.EquipmentID
                        join emodel in Context.CONTACT_EquipmentModels on equipment.EquipmentModelID equals emodel.EquipmentModelID
                        join affiliation in Context.CONTACT_Affiliations on equipment.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join certifiedby in Context.CONTACT_Users on certequipment.CertifiedByID equals certifiedby.UserID into certifiers
                        from certifiedby in certifiers.DefaultIfEmpty()
                        join assignedto in Context.CONTACT_Users on certequipment.AssignedToID equals assignedto.UserID into assignees
                        from assignedto in assignees.DefaultIfEmpty()
                        select new
                        {
                            certequipment = certequipment,
                            procedure = procedure,
                            equipment = equipment,
                            emodel = emodel,
                            affiliation = affiliation,
                            country = country,
                            certifiedby = certifiedby,
                            assignedto = assignedto,
                            trial = trial,
                            prevcert = 0
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            if (studyId.HasValue)
            {
                var fs = @"trial.TrialID == @trialId";
                andFilters.Add(fs);
                parameters.Add("@trialId", studyId);
            }

            switch (u.AspnetRole.LoweredRoleName)
            {
                case "super user":
                case "data quality evaluator":
                    if (studyId.HasValue)
                    {
                        //Check reading center procedures??
                        var rcs = Context.CONTACT_TrialReadingCenters.Where(x => x.TrialID == studyId && x.AffiliationID == u.AffiliationID);
                        var procedures = rcs.Select(x => x.ImgProcedureID.Value.ToString()).ToList();

                        var f = @"@rcprocedures.Contains(""["" + procedure.ImgProcedureID.ToString() + ""]"")";
                        andFilters.Add(f);
                        var sProcedures = string.Concat(procedures.Select(x => string.Format("[{0}]", x)));
                        parameters.Add("@rcprocedures", sProcedures);
                    }
                    break;
                case "ophthalmic technician":
                case "site coordinator":
                    affiliationId = u.AffiliationID;
                    break;
            }


            if (equipmentId.HasValue)
            {
                var f = @"certequipment.EquipmentID == @equipmentId";
                andFilters.Add(f);
                parameters.Add("@equipmentId", equipmentId);
            }
            if (affiliationId.HasValue)
            {
                var f = @"equipment.AffiliationID == @affiliationId";
                andFilters.Add(f);
                parameters.Add("@affiliationId", affiliationId);
            }
            if (procedureId.HasValue)
            {
                var f = @"certequipment.ImgProcedureID == @procedureId";
                andFilters.Add(f);
                parameters.Add("@procedureId", procedureId);
            }
            if (isActive.HasValue)
            {
                var f = @"certequipment.IsActive == @isActive";
                andFilters.Add(f);
                parameters.Add("@isActive", isActive);
            }
            if (isCertified.HasValue)
            {
                var f = @"certequipment.IsCertified == @isCertified";
                andFilters.Add(f);
                parameters.Add("@isCertified", isCertified);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                switch (assignedTo.ToLower())
                {
                    case "me":
                        var f1 = @"certequipment.AssignedToID == @assignedToId";
                        andFilters.Add(f1);
                        parameters.Add("@assignedToId", u);
                        break;
                    case "any":
                        var f2 = @"certequipment.AssignedToID != null";
                        andFilters.Add(f2);
                        break;
                    case "none":
                        var f3 = @"certequipment.AssignedToID == null";
                        andFilters.Add(f3);
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                if (!studyId.HasValue)
                {
                    var f = @"trial.TrialAlias.Contains(@search)
                        || trial.TrialName.Contains(@search)";
                    orFilters.Add(f);
                }
                if (!procedureId.HasValue)
                {
                    var f = @"procedure.ImgProcedureName.Contains(@search) 
                        || procedure.ImgProcedureDescription.Contains(@search)";
                    orFilters.Add(f);
                }
                if (!equipmentId.HasValue)
                {
                    var f = @"equipment.MainSerialNum.Contains(@search)
                        || equipment.SeconarySerialNum.Contains(@search)
                        || equipment.SoftwareVersion.Contains(@search)
                        || equipment.Notes.Contains(@search)
                        || emodel.EquipmentType.Contains(@search)
                        || emodel.ManufacturerModel.Contains(@search)
                        || emodel.ManufacturerName.Contains(@search)";
                    orFilters.Add(f);

                    if (!affiliationId.HasValue)
                    {
                        var fi = @"affiliation.AffiliationName.Contains(@search)
                            || (country != null ? country.CountryName.Contains(@search) : false)";
                        orFilters.Add(fi);
                    }
                }
                if (string.IsNullOrWhiteSpace(assignedTo) || (!string.IsNullOrWhiteSpace(assignedTo) && assignedTo.ToLower() == "any"))
                {
                    var f = @"(assignedto != null ? assignedto.LastName.Contains(@search)
                        || assignedto.FirstName.Contains(@search)
                        || assignedto.LoweredUserName.Contains(@search)
                        || assignedto.Email.Contains(@search) : false)";
                    orFilters.Add(f);
                }
                if (!isCertified.HasValue || (isCertified.HasValue && isCertified.Value))
                {
                    var f = @"(certifiedby != null ? certifiedby.LastName.Contains(@search)
                        || certifiedby.FirstName.Contains(@search)
                        || certifiedby.LoweredUserName.Contains(@search)
                        || certifiedby.Email.Contains(@search) : false)";
                    orFilters.Add(f);
                }

                var filter = CombinePredicates(orFilters, "||");
                if (andFilters.Count > 0)
                {
                    var andFilter = CombinePredicates(andFilters, "&&");
                    filter = CombinePredicates(new string[] { andFilter, filter }, "&&");
                }
                query = query.Where(filter, parameters);
            }
            else
            {
                if (andFilters.Count > 0)
                {
                    var filter = CombinePredicates(andFilters, "&&");
                    query = query.Where(filter, parameters);
                }
            }

            if (hasPrevCert.HasValue)
            {
                var prevCerts = from certequipment in entities.Where(x => x.IsActive && x.IsCertified)
                                join certifiedby in Context.CONTACT_Users on certequipment.CertifiedByID equals certifiedby.UserID
                                select new
                                {
                                    certequipment = certequipment,
                                    certifiedby = certifiedby
                                };

                var filterPrev = "";
                switch (u.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                    case "manager":
                        filterPrev = "true";
                        break;
                    case "super user":
                    case "data quality evaluator":
                        filterPrev = "certifiedby.AffiliationID == @certAffiliationId";
                        break;
                    default:
                        filterPrev = "false";
                        break;
                }
                var parametersPrev = new Dictionary<string, object>();
                parametersPrev.Add("@certAffiliationId", u.AffiliationID);


                var prevCertTotals = from prevcert in prevCerts.Where(filterPrev, parametersPrev).Select(x => x.certequipment)
                                     group prevcert by new { procedureid = prevcert.ImgProcedureID, equipmentid = prevcert.EquipmentID } into g
                                     select new { key = g.Key, total = g.Count() };
                //var pctl = prevCertTotals.ToList();

                var qpc = from o in query
                          join p in prevCertTotals on new { procedureid = o.certequipment.ImgProcedureID, equipmentid = o.certequipment.EquipmentID } equals new { procedureid = p.key.procedureid, equipmentid = p.key.equipmentid } into prevcerts
                          from p in prevcerts.DefaultIfEmpty()
                          select new 
                          {
                              certequipment = o.certequipment,
                              procedure = o.procedure,
                              equipment = o.equipment,
                              emodel = o.emodel,
                              affiliation = o.affiliation,
                              country = o.country,
                              certifiedby = o.certifiedby,
                              assignedto = o.assignedto,
                              trial = o.trial,
                              prevcert = (p != null ? p.total : 0)
                          };

                string f;
                if (hasPrevCert.Value)
                {
                    f = @"prevcert > 0";
                }
                else
                {
                    f = @"prevcert <= 0";
                }

                query = qpc.Where(f);
            }

            IQueryable result;
            if (!string.IsNullOrWhiteSpace(sort))
            {
                result = ApplySortExpression(sort, query);
            }
            else
            {
                if (equipmentId.HasValue)
                {
                    if (isCertified.HasValue && isCertified.Value)
                    {
                        query = query.OrderBy(x => x.certequipment.DateofCertification);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.certequipment.CertEquipmentID);
                    }
                }
                else
                {
                    if (!studyId.HasValue)
                    {
                        if (isCertified.HasValue && isCertified.Value)
                        {
                            query = query.OrderBy(x => x.trial.TrialName)
                                .ThenBy(x => x.affiliation.AffiliationName)
                                .ThenBy(x => x.emodel.ManufacturerName)
                                .ThenBy(x => x.emodel.ManufacturerModel)
                                .ThenBy(x => x.equipment.MainSerialNum)
                                .ThenBy(x => x.procedure.ImgProcedureName)
                                .ThenBy(x => x.certequipment.DateofCertification);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.trial.TrialName)
                                .ThenBy(x => x.affiliation.AffiliationName)
                                .ThenBy(x => x.emodel.ManufacturerName)
                                .ThenBy(x => x.emodel.ManufacturerModel)
                                .ThenBy(x => x.equipment.MainSerialNum)
                                .ThenBy(x => x.procedure.ImgProcedureName)
                                .ThenBy(x => x.certequipment.CertEquipmentID);
                        }
                    }
                    else
                    {
                        if (isCertified.HasValue && isCertified.Value)
                        {
                            query = query.OrderBy(x => x.certequipment.DateofCertification);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.certequipment.CertEquipmentID);
                        }
                    }
                }
                result = query as IQueryable;
            }

            return result.Select("certequipment").Cast<CERT_Equipment>();
        }

        public IQueryable<CERT_Equipment> GetPrevCertifications(CERT_Equipment entity, CONTACT_User user, string search = null, string sort = null)
        {
            var query = from certequipment in GetAll()
                        join procedure in Context.CERT_ImgProcedureLists on certequipment.ImgProcedureID equals procedure.ImgProcedureID
                        join equipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals equipment.EquipmentID
                        join emodel in Context.CONTACT_EquipmentModels on equipment.EquipmentModelID equals emodel.EquipmentModelID
                        join affiliation in Context.CONTACT_Affiliations on equipment.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join certifiedby in Context.CONTACT_Users on certequipment.CertifiedByID equals certifiedby.UserID
                        join cbaffiliation in Context.CONTACT_Affiliations on certifiedby.AffiliationID equals cbaffiliation.AffiliationID
                        join cbcountry in Context.CONTACT_Countries on cbaffiliation.CountryID equals cbcountry.CountryID into cbcountries
                        from cbcountry in cbcountries.DefaultIfEmpty()
                        join assignedto in Context.CONTACT_Users on certequipment.AssignedToID equals assignedto.UserID into assignees
                        from assignedto in assignees.DefaultIfEmpty()
                        join trial in Context.PACS_Trials on certequipment.TrialID equals trial.TrialID
                        select new
                        {
                            certequipment = certequipment,
                            procedure = procedure,
                            equipment = equipment,
                            emodel = emodel,
                            affiliation = cbaffiliation,
                            country = cbcountry,
                            certifiedby = certifiedby,
                            assignedto = assignedto,
                            trial = trial
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            var isAffiliationFiltered = false;
            switch (user.AspnetRole.LoweredRoleName)
            {
                case "administrator":
                case "project manager":
                case "manager":
                case "super user":
                case "data quality evaluator":
                    var f = "certequipment.ImgProcedureID == @procedureId && certequipment.IsCertified == true && certequipment.IsActive == true && certequipment.EquipmentID == @equipmentId";
                    andFilters.Add(f);
                    parameters.Add("@procedureId", entity.ImgProcedureID);
                    parameters.Add("@equipmentId", entity.EquipmentID);
                    //Filter by affiliation for SU and DQE
                    switch (user.AspnetRole.LoweredRoleName)
                    {
                        case "super user":
                        case "data quality evaluator":
                            isAffiliationFiltered = true;
                            f = "certifiedby.AffiliationID == @affiliationId";
                            andFilters.Add(f);
                            parameters.Add("@affiliationId", user.AffiliationID);
                            break;
                        default:
                            break;
                    }
                    break;
                case "ophthalmic technician":
                case "site coordinator":
                case "grader":
                case "reviewer":
                case "cro sponsor":
                default:
                    return new List<CERT_Equipment>().AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                var f = @"trial.TrialAlias.Contains(@search)
                    || trial.TrialName.Contains(@search)";
                orFilters.Add(f);

                f = @"certifiedby.LastName.Contains(@search)
                        || certifiedby.FirstName.Contains(@search)
                        || certifiedby.LoweredUserName.Contains(@search)
                        || certifiedby.Email.Contains(@search)";
                orFilters.Add(f);

                if (!isAffiliationFiltered)
                {
                    f = @"affiliation.AffiliationName.Contains(@search)
                        || (country != null ? country.CountryName.Contains(@search) : false)";
                    orFilters.Add(f);
                }

                var filter = CombinePredicates(orFilters, "||");
                if (andFilters.Count > 0)
                {
                    var andFilter = CombinePredicates(andFilters, "&&");
                    filter = CombinePredicates(new string[] { andFilter, filter }, "&&");
                }
                query = query.Where(filter, parameters);
            }
            else
            {
                if (andFilters.Count > 0)
                {
                    var filter = CombinePredicates(andFilters, "&&");
                    query = query.Where(filter, parameters);
                }
            }

            IQueryable result;
            if (!string.IsNullOrWhiteSpace(sort))
            {
                result = ApplySortExpression(sort, query);
            }
            else
            {
                result = query.OrderBy(x => x.certequipment.DateofCertification);
            }

            return result.Select("certequipment").Cast<CERT_Equipment>();
        }

        public int GetTotalUploads(long? certEquipmentId)
        {
            var result = 0;
            if (certEquipmentId.HasValue)
            {
                result = Context.CERT_UploadInfos.Count(x => x.IsActive && x.CertEquipmentID == certEquipmentId);
            }
            return result;
        }

        public int GetTotalQueriesPending(CERT_Equipment entity)
        {
            var result = 0;
            if (entity != null)
            {
                var affiliationId = entity.CONTACTEquipment.AffiliationID;
                result = Context.QRY_Queries.Count(x => x.IsActive && !x.IsResolved && x.CertEquipmentID == entity.CertEquipmentID);
            }
            return result;
        }

        public int GetTotalQueriesFlagged(CERT_Equipment entity, CONTACT_User user)
        {
            var result = 0;
            if (entity != null)
            {
                var affiliationId = entity.CONTACTEquipment.AffiliationID;
                var query = Context.QRY_Queries.Where(x => x.IsActive && !x.IsResolved && x.CertEquipmentID == entity.CertEquipmentID);

                var uaffiliationId = user.AffiliationID;
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                    case "manager":
                        ////query = query.Where(x => x.QRYStatus.StatusName == "Pending Resolution");
                        ////break;
                    case "super user":
                    case "data quality evaluator":
                        query = query.Where(x => x.QRYStatus.StatusName == "Pending Resolution" && x.Sender.AffiliationID == uaffiliationId);
                        break;
                    case "site coordinator":
                    case "ophthalmic technician":
                        query = query.Where(x => x.QRYStatus.StatusName == "Pending Response");
                        break;
                    default:
                        query = query.Where(x => false);
                        break;
                }
                result = query.Count();
            }
            return result;
        }

        #endregion
    }
}