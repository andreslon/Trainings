using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class QueriesRepository : EntityBaseRepository<QRY_Query>, IQueriesRepository
    {
        #region Constructor

        public QueriesRepository(DataModel context) : base(context)
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
                case "datecreated":
                    return string.Format("q.InitiateDate {0}", dir);
                case "createdby.username":
                    return string.Format("sender.LoweredUserName {0}", dir);
                case "createdby.firstname":
                    return string.Format("sender.FirstName {0}", dir);
                case "createdby.lastname":
                    return string.Format("sender.LastName {0}", dir);
                case "title":
                    return string.Format("q.Subject {0}", dir);
                default:
                    return "";
            }
        }

        public IQueryable<QRY_Query> GetAll(CONTACT_User user, long studyId, long? siteId, long? seriesId, long? certEquipmentId, long? certUserId, string queryType, string queryStatus, bool? isActive, string search, string sort)
        {
            var msgs = Context.QRY_Messages;

            var query = //from q in FindBy(x => x.TrialID == studyId)
                        from msg in msgs
                            //message sender
                        join msgsender in Context.CONTACT_Users on msg.UserID equals msgsender.UserID into msgsenders
                        from msgsender in msgsenders.DefaultIfEmpty()
                        join msgaffiliation in Context.CONTACT_Affiliations on msgsender.AffiliationID equals msgaffiliation.AffiliationID into msgaffiliations
                        from msgaffiliation in msgaffiliations.DefaultIfEmpty()
                        join msgcountry in Context.CONTACT_Countries on msgaffiliation.CountryID equals msgcountry.CountryID into msgcountries
                        from msgcountry in msgcountries.DefaultIfEmpty()
                            //query
                        join q in Context.QRY_Queries on msg.QueryID equals q.QueryID
                        join qstatus in Context.QRY_Status on q.StatusID equals qstatus.StatusID
                        //sender
                        join sender in Context.CONTACT_Users on q.SenderID equals sender.UserID into senders
                        from sender in senders.DefaultIfEmpty()
                        join saffiliation in Context.CONTACT_Affiliations on sender.AffiliationID equals saffiliation.AffiliationID into saffiliations
                        from saffiliation in saffiliations.DefaultIfEmpty()
                        join scountry in Context.CONTACT_Countries on saffiliation.CountryID equals scountry.CountryID into scountries
                        from scountry in scountries.DefaultIfEmpty()
                        //serie
                        join serie in Context.PACS_Series on q.SeriesID equals serie.SeriesID into series
                        from serie in series.DefaultIfEmpty()
                        join schedule in Context.PACS_TPProcLists on serie.TPProcListID equals schedule.TPProcID into schedules
                        from schedule in schedules.DefaultIfEmpty()
                        join procedure in Context.CERT_ImgProcedureLists on schedule.ImgProcedureID equals procedure.ImgProcedureID into procedures
                        from procedure in procedures.DefaultIfEmpty()
                        join timepoint in Context.PACS_TimePointsLists on schedule.TimePointsListID equals timepoint.TimePointsListID into timepoints
                        from timepoint in timepoints.DefaultIfEmpty()
                        join visit in Context.PACS_TimePoints on serie.TimePointsID equals visit.TimePointsID into visits
                        from visit in visits.DefaultIfEmpty()
                        join subject in Context.PACS_Subjects on visit.SubjectID equals subject.SubjectID into subjects
                        from subject in subjects.DefaultIfEmpty()
                        join site in Context.PACS_Sites on subject.SiteID equals site.SiteID into sites
                        from site in sites.DefaultIfEmpty()
                        join affiliation in Context.CONTACT_Affiliations on site.AffiliationID equals affiliation.AffiliationID into affiliations
                        from affiliation in affiliations.DefaultIfEmpty()
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join equipment in Context.CONTACT_Equipments on serie.EquipmentID equals equipment.EquipmentID into equipments
                        from equipment in equipments.DefaultIfEmpty()
                        join emodel in Context.CONTACT_EquipmentModels on equipment.EquipmentModelID equals emodel.EquipmentModelID into emodels
                        from emodel in emodels.DefaultIfEmpty()
                        join tech in Context.CONTACT_Users on serie.PhotographerID equals tech.UserID into techs
                        from tech in techs.DefaultIfEmpty()
                        join scohort in Context.PACS_SubjectCohorts on subject.SubjectCohortID equals scohort.SubjectCohortID into cohorts
                        from scohort in cohorts.DefaultIfEmpty()
                        join sgroup in Context.PACS_SubjectGroups on subject.SubjectGroupID equals sgroup.SubjectGroupID into groups
                        from sgroup in groups.DefaultIfEmpty()
                        //ceruser
                        join certuser in Context.CERT_Users on q.CertUserID equals certuser.CertUserID into certusers
                        from certuser in certusers.DefaultIfEmpty()
                        join cuprocedure in Context.CERT_ImgProcedureLists on certuser.ImgProcedureID equals cuprocedure.ImgProcedureID into cuprocedures
                        from cuprocedure in cuprocedures.DefaultIfEmpty()
                        join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID into usertrials
                        from usertrial in usertrials.DefaultIfEmpty()
                        join cuuser in Context.CONTACT_Users on usertrial.UserID equals cuuser.UserID into cuusers
                        from cuuser in cuusers.DefaultIfEmpty()
                        join cuaffiliation in Context.CONTACT_Affiliations on cuuser.AffiliationID equals cuaffiliation.AffiliationID into cuaffiliations
                        from cuaffiliation in cuaffiliations.DefaultIfEmpty()
                        join cucountry in Context.CONTACT_Countries on cuaffiliation.CountryID equals cucountry.CountryID into cucountries
                        from cucountry in cucountries.DefaultIfEmpty()
                        join cucertifiedby in Context.CONTACT_Users on certuser.CertifiedByID equals cucertifiedby.UserID into cucertifiers
                        from cucertifiedby in cucertifiers.DefaultIfEmpty()
                        //certequipment
                        join certequipment in Context.CERT_Equipments on q.CertEquipmentID equals certequipment.CertEquipmentID into certequipments
                        from certequipment in certequipments.DefaultIfEmpty()
                        join ceprocedure in Context.CERT_ImgProcedureLists on certequipment.ImgProcedureID equals ceprocedure.ImgProcedureID into ceprocedures
                        from ceprocedure in ceprocedures.DefaultIfEmpty()
                        join ceequipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals ceequipment.EquipmentID into ceequipments
                        from ceequipment in ceequipments.DefaultIfEmpty()
                        join ceemodel in Context.CONTACT_EquipmentModels on ceequipment.EquipmentModelID equals ceemodel.EquipmentModelID into ceemodels
                        from ceemodel in ceemodels.DefaultIfEmpty()
                        join ceaffiliation in Context.CONTACT_Affiliations on ceequipment.AffiliationID equals ceaffiliation.AffiliationID into ceaffiliations
                        from ceaffiliation in ceaffiliations.DefaultIfEmpty()
                        join cecountry in Context.CONTACT_Countries on ceaffiliation.CountryID equals cecountry.CountryID into cecountries
                        from cecountry in cecountries.DefaultIfEmpty()
                        join cecertifiedby in Context.CONTACT_Users on certequipment.CertifiedByID equals cecertifiedby.UserID into cecertifiers
                        from cecertifiedby in cecertifiers.DefaultIfEmpty()
                        //Trial
                        join trial in Context.PACS_Trials on q.TrialID equals trial.TrialID
                        select new
                        {
                            msg = msg,
                            msgsender = msgsender,
                            msgaffiliation = msgaffiliation,
                            msgcountry = msgcountry,
                            q = q,
                            qstatus = qstatus,
                            sender = sender,
                            saffiliation = saffiliation,
                            scountry = scountry,
                            serie = serie,
                            schedule = schedule,
                            procedure = procedure,
                            timepoint = timepoint,
                            visit = visit,
                            subject = subject,
                            site = site,
                            affiliation = affiliation,
                            country = country,
                            equipment = equipment,
                            emodel = emodel,
                            tech = tech,
                            scohort = scohort,
                            sgroup = sgroup,
                            certuser = certuser,
                            cuprocedure = cuprocedure,
                            usertrial = usertrial,
                            cuuser = cuuser,
                            cuaffiliation = cuaffiliation,
                            cucountry = cucountry,
                            cucertifiedby = cucertifiedby,
                            certequipment = certequipment,
                            ceprocedure = ceprocedure,
                            ceequipment = ceequipment,
                            ceemodel = ceemodel,
                            ceaffiliation = ceaffiliation,
                            cecountry = cecountry,
                            cecertifiedby = cecertifiedby,
                            trial = trial
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            var fm = @"q.TrialID == @studyId";
            andFilters.Add(fm);
            parameters.Add("@studyId", studyId);

            fm = @"msg.IsActive == true";
            andFilters.Add(fm);

            switch (user.AspnetRole.LoweredRoleName)
            {
                case "site coordinator":
                case "ophthalmic technician":
                    fm = @"((site != null ? site.AffiliationID == @uaffiliationId : false)
                        || (ceequipment != null ? ceequipment.AffiliationID == @uaffiliationId : false)
                        || (cuuser != null ? cuuser.AffiliationID == @uaffiliationId : false))";
                    break;
                default:
                    fm = "true";
                    break;
            }
            andFilters.Add(fm);
            parameters.Add("@uaffiliationId", user.AffiliationID);

            if (siteId.HasValue)
            {
                var site = Context.PACS_Sites.FirstOrDefault(x => x.SiteID == siteId);
                var affiliationId = site.AffiliationID;
                parameters.Add("@affiliationId", affiliationId);
                parameters.Add("@siteId", siteId);

                var orFilters = new List<string>();

                if (!seriesId.HasValue)
                {
                    var f = @"(subject != null ? subject.SiteID == @siteId : false)";
                    orFilters.Add(f);
                }
                if (!certUserId.HasValue)
                {
                    var f = @"(cuuser != null ? cuuser.AffiliationID == @affiliationId : false)";
                    orFilters.Add(f);
                }
                if (!certEquipmentId.HasValue)
                {
                    var f = @"(ceequipment != null ? ceequipment.AffiliationID == @affiliationId : false)";
                    orFilters.Add(f);
                }

                var fo = CombinePredicates(orFilters, "||");
                andFilters.Add(fo);
            }
            if (isActive.HasValue)
            {
                var f = @"q.IsActive == @isActive";
                andFilters.Add(f);
                parameters.Add("@isActive", isActive);
            }
            if (!string.IsNullOrEmpty(queryStatus))
            {
                string f;
                switch(queryStatus.ToLower())
                {
                    case "resolved":
                        f = @"q.IsResolved";
                        break;
                   case "all pending":
                        f = @"!q.IsResolved";
                        break;
                    case "my pending":
                        switch (user.AspnetRole.LoweredRoleName)
                        {
                            case "administrator":
                            case "project manager":
                            case "study manager":
                                f = @"qstatus.StatusName == ""Pending Resolution""";
                                break;
                            case "super user":
                            case "data quality evaluator":
                                f = @"qstatus.StatusName == ""Pending Resolution""";
                                break;
                            case "site coordinator":
                            case "ophthalmic technician":
                                f = @"qstatus.StatusName == ""Pending Response""";
                                break;
                            default:
                                f = "false";
                                break;
                        }
                        break;
                    default:
                        throw new Exception("Status filter not supported.");
                }
                andFilters.Add(f);
            }

            if (seriesId.HasValue)
            {
                var f = @"q.SeriesID == @seriesId";
                andFilters.Add(f);
                parameters.Add("@seriesId", seriesId);
            }
            else if (certUserId.HasValue)
            {
                var f = @"q.CertUserID == @certUserId";
                andFilters.Add(f);
                parameters.Add("@certUserId", certUserId);
            }
            else if (certEquipmentId.HasValue)
            {
                var f = @"q.CertEquipmentID == @certEquipmentId";
                andFilters.Add(f);
                parameters.Add("@certEquipmentId", certEquipmentId);
            }
            else if (!string.IsNullOrEmpty(queryType))
            {
                switch (queryType.ToLower())
                {
                    case "certification":
                        var f1 = @"q.CertEquipmentID != null || q.CertUserID != null";
                        andFilters.Add(f1);
                        break;
                    case "imaging":
                        var f2 = @"q.SeriesID != null";
                        andFilters.Add(f2);
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                var fi = @"q.Subject.Contains(@search)
                    || msg.MessageBody.Contains(@search)
                    || (msgsender != null ? (msgsender.LastName.Contains(@search)
                        || msgsender.FirstName.Contains(@search)
                        || msgsender.LoweredUserName.Contains(@search)
                        || msgsender.Email.Contains(@search)
                        || msgaffiliation.AffiliationName.Contains(@search)
                        || (msgcountry != null ? msgcountry.CountryName.Contains(@search) : false)) : false)
                    || sender.LastName.Contains(@search)
                    || sender.FirstName.Contains(@search)
                    || sender.LoweredUserName.Contains(@search)
                    || sender.Email.Contains(@search)
                    || saffiliation.AffiliationName.Contains(@search)
                    || (scountry != null ? scountry.CountryName.Contains(@search) : false)";
                orFilters.Add(fi);

                if (!seriesId.HasValue)
                {
                    var fii = @"(serie != null ? ((serie.SeriesGroupID != null ? serie.SeriesGroupID.Value.ToString().Contains(@search) : false)
                        || procedure.ImgProcedureName.Contains(@search)
                        || procedure.ImgProcedureDescription.Contains(@search)
                        || timepoint.TimePointsDescription.Contains(@search)
                        || subject.RandomizedSubjectID.Contains(@search)
                        || subject.AlternativeRandomizedSubjectID.Contains(@search)
                        || subject.NameCode.Contains(@search)
                        || site.RandomizedSiteID.Contains(@search)
                        || affiliation.AffiliationName.Contains(@search)
                        || (country != null ? country.CountryName.Contains(@search) : false)
                        || equipment.MainSerialNum.Contains(@search)
                        || equipment.SeconarySerialNum.Contains(@search)
                        || equipment.SoftwareVersion.Contains(@search)
                        || equipment.Notes.Contains(@search)
                        || emodel.EquipmentType.Contains(@search)
                        || emodel.ManufacturerModel.Contains(@search)
                        || emodel.ManufacturerName.Contains(@search)
                        || tech.LastName.Contains(@search)
                        || tech.FirstName.Contains(@search)
                        || tech.LoweredUserName.Contains(@search)
                        || tech.Email.Contains(@search)
                        || (scohort != null ? scohort.CohortName.Contains(@search) : false)
                        || (sgroup != null ? sgroup.GroupName.Contains(@search)
                            || sgroup.GroupDescription.Contains(@search) : false)) : false)";
                    orFilters.Add(fii);
                }
                if (!certUserId.HasValue)
                {
                    var fii = @"(certuser != null ? (ceprocedure.ImgProcedureName.Contains(@search)
                        || ceprocedure.ImgProcedureDescription.Contains(@search)
                        || cuuser.LastName.Contains(@search)
                        || cuuser.FirstName.Contains(@search)
                        || cuuser.LoweredUserName.Contains(@search)
                        || cuuser.Email.Contains(@search)
                        || cuaffiliation.AffiliationName.Contains(@search)
                        || (cucountry != null ? cucountry.CountryName.Contains(@search) : false)
                        || cucertifiedby.LastName.Contains(@search)
                        || cucertifiedby.FirstName.Contains(@search)
                        || cucertifiedby.LoweredUserName.Contains(@search)
                        || cucertifiedby.Email.Contains(@search)) : false)";
                    orFilters.Add(fii);
                }
                if (!certEquipmentId.HasValue)
                {
                    var fii = @"(certequipment != null ? (ceprocedure.ImgProcedureName.Contains(@search)
                        || ceprocedure.ImgProcedureDescription.Contains(@search)
                        || ceequipment.MainSerialNum.Contains(@search)
                        || ceequipment.SeconarySerialNum.Contains(@search)
                        || ceequipment.SoftwareVersion.Contains(@search)
                        || ceequipment.Notes.Contains(@search)
                        || ceemodel.EquipmentType.Contains(@search)
                        || ceemodel.ManufacturerModel.Contains(@search)
                        || ceemodel.ManufacturerName.Contains(@search)
                        || ceaffiliation.AffiliationName.Contains(@search)
                        || (cecountry != null ? cecountry.CountryName.Contains(@search) : false)
                        || cecertifiedby.LastName.Contains(@search)
                        || cecertifiedby.FirstName.Contains(@search)
                        || cecertifiedby.LoweredUserName.Contains(@search)
                        || cecertifiedby.Email.Contains(@search)) : false)";
                    orFilters.Add(fii);
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

            var qo = query.GroupBy("new(q, sender)", "it").Select("new(Key.q, Key.sender)");

            IQueryable result;
            if (!string.IsNullOrWhiteSpace(sort))
            {
                result = ApplySortExpression(sort, qo);
            }
            else
            {
                qo = qo.OrderBy("q.InitiateDate desc");
                result = qo;
            }
            //.GroupBy("new(Str1,Str2)", "new(Num2,Num1)");
            //.Select("new(Key.Str1, Key.Str2, Average(Num2) as SumNum, Max(Num1) as MaxNum)");
            return result.Select("q").Cast<QRY_Query>();
        }

        public override void Delete(QRY_Query entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<QRY_Query, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public int GetTotalMessages(long? queryId)
        {
            var result = 0;
            if (queryId.HasValue)
            {
                result = Context.QRY_Messages.Count(x => x.IsActive && x.QueryID == queryId);
            }
            return result;
        }

        public QRY_Message GetLastMessage(long? queryId)
        {
            QRY_Message result = null;
            if (queryId.HasValue)
            {
                result = Context.QRY_Messages.LastOrDefault(x => x.IsActive && x.QueryID == queryId);
            }
            return result;
        }

        #endregion
    }
}
