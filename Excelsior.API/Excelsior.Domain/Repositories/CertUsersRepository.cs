using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class CertUsersRepository : EntityBaseRepository<CERT_User>, ICertUsersRepository
    {
        #region Constructor
        public CertUsersRepository(DataModel context) : base(context)
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
                case "studyuser.studyname":
                    return string.Format("trial.TrialName {0}", dir);
                case "studyuser.studyalias":
                    return string.Format("trial.TrialAlias {0}", dir);
                case "studyuser.user.lastname":
                    return string.Format("user.LastName {0}", dir);
                case "studyuser.user.firstname":
                    return string.Format("user.FirstName {0}", dir);
                case "studyuser.user.affiliation.name":
                    return string.Format("affiliation.AffiliationName {0}", dir);
                case "studyuser.user.affiliation.country.name":
                    return string.Format("country.CountryName {0}", dir);
                case "procedure.name":
                    return string.Format("procedure.ImgProcedureName {0}", dir);
                default:
                    return "";
            }
        }

        public IQueryable<CERT_User> GetAll(CONTACT_User u, long? studyId, long? affiliationId, long? technicianId, long? procedureId, bool? isActive, bool? isCertified, bool? hasPrevCert, string assignedTo, string search, string sort)
        {
            if (!studyId.HasValue)
                throw new Exception("studyId is required");

            var entities = GetAll();
            var query = from certuser in entities
                        join procedure in Context.CERT_ImgProcedureLists on certuser.ImgProcedureID equals procedure.ImgProcedureID
                        join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID
                        join trial in Context.PACS_Trials on usertrial.TrialID equals trial.TrialID
                        join user in Context.CONTACT_Users on usertrial.UserID equals user.UserID
                        join affiliation in Context.CONTACT_Affiliations on user.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join certifiedby in Context.CONTACT_Users on certuser.CertifiedByID equals certifiedby.UserID into certifiers
                        from certifiedby in certifiers.DefaultIfEmpty()
                        join assignedto in Context.CONTACT_Users on certuser.AssignedToID equals assignedto.UserID into assignees
                        from assignedto in assignees.DefaultIfEmpty()
                        select new
                        {
                            certuser = certuser,
                            procedure = procedure,
                            usertrial = usertrial,
                            user = user,
                            affiliation = affiliation,
                            country = country,
                            certifiedby = certifiedby,
                            assignedto = assignedto,
                            trial = trial,
                            prevcert = 0
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            var fs = @"trial.TrialID == @trialId";
            andFilters.Add(fs);
            parameters.Add("@trialId", studyId);

            switch (u.AspnetRole.LoweredRoleName)
            {
                case "super user":
                case "data quality evaluator":
                    var rcs = Context.CONTACT_TrialReadingCenters.Where(x => x.TrialID == studyId && x.AffiliationID == u.AffiliationID);
                    var procedures = rcs.Select(x => x.ImgProcedureID.Value.ToString()).ToList();

                    var f = @"@rcprocedures.Contains(""["" + procedure.ImgProcedureID.ToString() + ""]"")";
                    andFilters.Add(f);
                    var sProcedures = string.Concat(procedures.Select(x => string.Format("[{0}]", x)));
                    parameters.Add("@rcprocedures", sProcedures);
                    break;
                case "ophthalmic technician":
                case "site coordinator":
                    affiliationId = u.AffiliationID;
                    break;
            }

            if (technicianId.HasValue)
            {
                var f = @"usertrial.UserID == @technicianId";
                andFilters.Add(f);
                parameters.Add("@technicianId", technicianId);
            }
            if (affiliationId.HasValue)
            {
                var f = @"user.AffiliationID == @affiliationId";
                andFilters.Add(f);
                parameters.Add("@affiliationId", affiliationId);
            }
            if (procedureId.HasValue)
            {
                var f = @"certuser.ImgProcedureID == @procedureId";
                andFilters.Add(f);
                parameters.Add("@procedureId", procedureId);
            }
            if (isActive.HasValue)
            {
                var f = @"certuser.IsActive == @isActive";
                andFilters.Add(f);
                parameters.Add("@isActive", isActive);
            }
            if (isCertified.HasValue)
            {
                var f = @"certuser.IsCertified == @isCertified";
                andFilters.Add(f);
                parameters.Add("@isCertified", isCertified);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                switch (assignedTo.ToLower())
                {
                    case "me":
                        var f1 = @"certuser.AssignedToID == @assignedToId";
                        andFilters.Add(f1);
                        parameters.Add("@assignedToId", u.UserID);
                        break;
                    case "any":
                        var f2 = @"certuser.AssignedToID != null";
                        andFilters.Add(f2);
                        break;
                    case "none":
                        var f3 = @"certuser.AssignedToID == null";
                        andFilters.Add(f3);
                        break;
                    default:
                        break;
                }
            }

            if (isCertified == false && u.AspnetRole.LoweredRoleName == "ophthalmic technician")
            {
                var f = @"usertrial.UserID == @techId";
                andFilters.Add(f);
                parameters.Add("@techId", u.UserID);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                if (!studyId.HasValue) {
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
                if (!technicianId.HasValue)
                {
                    var f = @"user.LastName.Contains(@search)
                        || user.FirstName.Contains(@search)
                        || user.LoweredUserName.Contains(@search)
                        || user.Email.Contains(@search)";
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
                var prevCerts = from certuser in entities.Where(x => x.IsActive && x.IsCertified)
                                join certifiedby in Context.CONTACT_Users on certuser.CertifiedByID equals certifiedby.UserID
                                select new
                                {
                                    certuser = certuser,
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


                var prevCertTotals = from prevcert in prevCerts.Where(filterPrev, parametersPrev).Select(x => x.certuser)
                                     group prevcert by new { procedureid = prevcert.ImgProcedureID, userid = prevcert.CONTACTUserTrial.UserID } into g
                                     select new { key = g.Key, total = g.Count() };
                //var pctl = prevCertTotals.ToList();

                var qpc = from o in query
                          join p in prevCertTotals on new { procedureid = o.certuser.ImgProcedureID, userid = o.usertrial.UserID } equals new { procedureid = p.key.procedureid, userid = p.key.userid } into prevcerts
                          from p in prevcerts.DefaultIfEmpty()
                          select new
                          {
                              certuser = o.certuser,
                              procedure = o.procedure,
                              usertrial = o.usertrial,
                              user = o.user,
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
                if (technicianId.HasValue)
                {
                    if (isCertified.HasValue && isCertified.Value)
                    {
                        query = query.OrderBy(x => x.certuser.DateofCertification);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.certuser.CertUserID);
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
                            .ThenBy(x => x.user.LastName)
                            .ThenBy(x => x.user.FirstName)
                            .ThenBy(x => x.procedure.ImgProcedureName)
                            .ThenBy(x => x.certuser.DateofCertification);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.trial.TrialName)
                            .ThenBy(x => x.affiliation.AffiliationName)
                            .ThenBy(x => x.user.LastName)
                            .ThenBy(x => x.user.FirstName)
                            .ThenBy(x => x.procedure.ImgProcedureName)
                            .ThenBy(x => x.certuser.CertUserID);
                        }
                    }
                    else
                    {
                        if (isCertified.HasValue && isCertified.Value)
                        {
                            query = query.OrderBy(x => x.certuser.DateofCertification);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.certuser.CertUserID);
                        }
                    }
                }
                result = query as IQueryable;
            }

            return result.Select("certuser").Cast<CERT_User>();
        }

        public IQueryable<CERT_User> GetPrevCertifications(CERT_User entity, CONTACT_User user, string search = null, string sort = null)
        {
            var query = from certuser in GetAll()
                        join procedure in Context.CERT_ImgProcedureLists on certuser.ImgProcedureID equals procedure.ImgProcedureID
                        join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID
                        join cuser in Context.CONTACT_Users on usertrial.UserID equals cuser.UserID
                        join affiliation in Context.CONTACT_Affiliations on cuser.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join certifiedby in Context.CONTACT_Users on certuser.CertifiedByID equals certifiedby.UserID
                        join cbaffiliation in Context.CONTACT_Affiliations on cuser.AffiliationID equals cbaffiliation.AffiliationID
                        join cbcountry in Context.CONTACT_Countries on cbaffiliation.CountryID equals cbcountry.CountryID into cbcountries
                        from cbcountry in cbcountries.DefaultIfEmpty()
                        join assignedto in Context.CONTACT_Users on certuser.AssignedToID equals assignedto.UserID into assignees
                        from assignedto in assignees.DefaultIfEmpty()
                        join trial in Context.PACS_Trials on usertrial.TrialID equals trial.TrialID
                        select new
                        {
                            certuser = certuser,
                            procedure = procedure,
                            usertrial = usertrial,
                            user = cuser,
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
                    var f = "certuser.ImgProcedureID == @procedureId && certuser.IsCertified == true && certuser.IsActive == true && usertrial.UserID == @userId";
                    andFilters.Add(f);
                    parameters.Add("@procedureId", entity.ImgProcedureID);
                    parameters.Add("@userId", entity.CONTACTUserTrial.UserID);
                    //Filter by affiliation for SU and DQE
                    switch (user.AspnetRole.LoweredRoleName)
                    {
                        case "super user":
                        case "data quality evaluator":
                            isAffiliationFiltered = true;
                            f = "certifiedby.AffiliationID == @affiliationId";
                            andFilters.Add(f);
                            parameters.Add("@affiliationId", user.AffiliationID);

                            //Get the procedures for the reading center
                            //var studyId = entity.CONTACTUserTrial.TrialID;
                            //var affiliationId = user.AffiliationID;

                            //Check reading center procedures??
                            //var rcs = Context.CONTACT_TrialReadingCenters.Where(x => x.TrialID == studyId && x.AffiliationID == affiliationId);
                            //var procedures = rcs.Select(x => x.ImgProcedureID).ToList();
                            
                            //f = "@rcprocedures.Contains(string.Format("[{0}]", certuser.ImgProcedureID))";
                            //andFilters.Add(f);
                            //var sProcedures = string.Join("[{0}]", procedures);
                            //parameters.Add("@rcprocedures", sProcedures);
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
                    return new List<CERT_User>().AsQueryable();
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
                result = query.OrderBy(x => x.certuser.DateofCertification);
            }

            return result.Select("certuser").Cast<CERT_User>();
        }

        public int GetTotalUploads(long? certUserId)
        {
            var result = 0;
            if (certUserId.HasValue)
            {
                result = Context.CERT_UploadInfos.Count(x => x.IsActive && x.CertUserID == certUserId);
            }
            return result;
        }

        public int GetTotalQueriesPending(CERT_User entity)
        {
            var result = 0;
            if (entity != null)
            {
                var affiliationId = entity.CONTACTUserTrial.CONTACTUser.AffiliationID;
                result = Context.QRY_Queries.Count(x => x.IsActive && !x.IsResolved && x.CertUserID == entity.CertUserID);
            }
            return result;
        }

        public int GetTotalQueriesFlagged(CERT_User entity, CONTACT_User user)
        {
            var result = 0;
            if (entity != null)
            {
                var affiliationId = entity.CONTACTUserTrial.CONTACTUser.AffiliationID;
                var query = Context.QRY_Queries.Where(x => x.IsActive && !x.IsResolved && x.CertUserID == entity.CertUserID);

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