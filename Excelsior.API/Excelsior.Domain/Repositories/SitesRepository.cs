using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class SitesRepository : EntityBaseRepository<PACS_Site>, ISitesRepository
    {
        #region Constructor

        public SitesRepository(DataModel context) : base(context)
        {
        }

        #endregion

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
                case "randomizedsiteid":
                    return string.Format("site.RandomizedSiteID {0}", dir);
                case "affiliation.name":
                    return string.Format("affiliation.AffiliationName {0}", dir);
                case "affiliation.country.name":
                    return string.Format("country.CountryName {0}", dir);
                case "principalinvestigator":
                    return string.Format("site.PrincipalInvestigator {0}", dir);
                default:
                    return "";
            }
        }

        public IQueryable<PACS_Site> GetAll(CONTACT_User u, long? trialId, bool? isActive, string search, string sort)
        {
            var sites = GetAll();

            var query = from site in sites
                        join affiliation in Context.CONTACT_Affiliations on site.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        select new
                        {
                            site = site,
                            affiliation = affiliation,
                            country = country
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            if (trialId.HasValue)
            {
                var trial = Context.PACS_Trials.FirstOrDefault(x => x.TrialID == trialId);
                if (trial != null)
                {
                    var f = @"site.TrialID == @trialId";
                    andFilters.Add(f);
                    parameters.Add("@trialId", trialId);

                    if(!trial.IsTestingPhase)
                    {
                        f = "!site.IsTestingSite";
                        andFilters.Add(f);
                    }
                }
            }

            if (isActive.HasValue)
            {
                var f = @"site.IsActive == @isActive";
                andFilters.Add(f);
                parameters.Add("@isActive", isActive);
            }

            //Filter by user role
            var roles = new List<string>();
            switch (u.AspnetRole.LoweredRoleName)
            {
                case "opthalmic technician":
                case "site coordinator":
                    var f = "affiliation.AffiliationID == @affiliationId";
                    andFilters.Add(f);
                    parameters.Add("@affiliationId", u.AffiliationID);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                var f = @"site.RandomizedSiteID.Contains(@search)
                    || site.PrincipalInvestigator.Contains(@search)
                    || affiliation.AffiliationName.Contains(@search)
                    || (country != null ? country.CountryName.Contains(@search) : false)";
                orFilters.Add(f);


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
                query = query.OrderBy(x => x.site.RandomizedSiteID);
                result = query as IQueryable;
            }

            return result.Select("site").Cast<PACS_Site>();
        }

        public override void Delete(PACS_Site entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<PACS_Site, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public int GetTotalSubjects(PACS_Site entity)
        {
            if (entity.PACSTrial.IsTestingPhase)
            {
                return DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.PACS_Subjects.Count(x => x.SiteID == entity.SiteID
                        && x.IsActive == true);
                });
            }
            else
            {
                return DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.PACS_Subjects.Count(x => x.SiteID == entity.SiteID
                        && x.IsActive == true && x.IsTestingSubject == false && x.PACSSite.IsTestingSite == false);
                });
            }
        }

        public int GetTotalQueriesPending(PACS_Site entity)
        {
            var result = 0;
            if (entity != null)
            {
                var query = from q in Context.QRY_Queries
                            join qstatus in Context.QRY_Status on q.StatusID equals qstatus.StatusID
                            //serie
                            join serie in Context.PACS_Series on q.SeriesID equals serie.SeriesID into series
                            from serie in series.DefaultIfEmpty()
                            join visit in Context.PACS_TimePoints on serie.TimePointsID equals visit.TimePointsID into visits
                            from visit in visits.DefaultIfEmpty()
                            join subject in Context.PACS_Subjects on visit.SubjectID equals subject.SubjectID into subjects
                            from subject in subjects.DefaultIfEmpty()
                                //ceruser
                            join certuser in Context.CERT_Users on q.CertUserID equals certuser.CertUserID into certusers
                            from certuser in certusers.DefaultIfEmpty()
                            join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID into usertrials
                            from usertrial in usertrials.DefaultIfEmpty()
                            join cuuser in Context.CONTACT_Users on usertrial.UserID equals cuuser.UserID into cuusers
                            from cuuser in cuusers.DefaultIfEmpty()
                                //certequipment
                            join certequipment in Context.CERT_Equipments on q.CertEquipmentID equals certequipment.CertEquipmentID into certequipments
                            from certequipment in certequipments.DefaultIfEmpty()
                            join ceequipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals ceequipment.EquipmentID into ceequipments
                            from ceequipment in ceequipments.DefaultIfEmpty()
                            select new
                            {
                                q = q,
                                qstatus = qstatus,
                                visit = visit,
                                subject = subject,
                                certuser = certuser,
                                usertrial = usertrial,
                                cuuser = cuuser,
                                certequipment = certequipment,
                                ceequipment = ceequipment,
                            };

                var andFilters = new List<string>();
                var orFilters = new List<string>();
                var parameters = new Dictionary<string, object>();

                var f = @"q.TrialID == @studyId";
                andFilters.Add(f);
                parameters.Add("@studyId", entity.TrialID);

                f = @"q.IsActive && !q.IsResolved";
                andFilters.Add(f);

                var fo = @"(subject != null ? subject.SiteID == @siteId : false)";
                orFilters.Add(fo);
                parameters.Add("@siteId", entity.SiteID);

                fo = @"(cuuser != null ? cuuser.AffiliationID = @affiliationId : false)";
                orFilters.Add(fo);
                fo = @"(ceequipment != null ? ceequipment.AffiliationID = @affiliationId : false)";
                orFilters.Add(fo);
                parameters.Add("@affiliationId", entity.AffiliationID);

                var filter = CombinePredicates(orFilters, "||");
                var andFilter = CombinePredicates(andFilters, "&&");
                filter = CombinePredicates(new string[] { andFilter, filter }, "&&");
                result = query.Where(filter, parameters).Count();
            }
            return result;
        }

        public int GetTotalQueriesFlagged(PACS_Site entity, CONTACT_User user)
        {
            var result = 0;
            if (entity != null)
            {
                var query = from q in Context.QRY_Queries
                            join qstatus in Context.QRY_Status on q.StatusID equals qstatus.StatusID
                            join sender in Context.CONTACT_Users on q.SenderID equals sender.UserID
                            //serie
                            join serie in Context.PACS_Series on q.SeriesID equals serie.SeriesID into series
                            from serie in series.DefaultIfEmpty()
                            join visit in Context.PACS_TimePoints on serie.TimePointsID equals visit.TimePointsID into visits
                            from visit in visits.DefaultIfEmpty()
                            join subject in Context.PACS_Subjects on visit.SubjectID equals subject.SubjectID into subjects
                            from subject in subjects.DefaultIfEmpty()
                                //ceruser
                            join certuser in Context.CERT_Users on q.CertUserID equals certuser.CertUserID into certusers
                            from certuser in certusers.DefaultIfEmpty()
                            join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID into usertrials
                            from usertrial in usertrials.DefaultIfEmpty()
                            join cuuser in Context.CONTACT_Users on usertrial.UserID equals cuuser.UserID into cuusers
                            from cuuser in cuusers.DefaultIfEmpty()
                                //certequipment
                            join certequipment in Context.CERT_Equipments on q.CertEquipmentID equals certequipment.CertEquipmentID into certequipments
                            from certequipment in certequipments.DefaultIfEmpty()
                            join ceequipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals ceequipment.EquipmentID into ceequipments
                            from ceequipment in ceequipments.DefaultIfEmpty()
                            select new
                            {
                                q = q,
                                qstatus = qstatus,
                                sender = sender,
                                visit = visit,
                                subject = subject,
                                certuser = certuser,
                                usertrial = usertrial,
                                cuuser = cuuser,
                                certequipment = certequipment,
                                ceequipment = ceequipment,
                            };

                var andFilters = new List<string>();
                var orFilters = new List<string>();
                var parameters = new Dictionary<string, object>();

                //FindBy(x => x.TrialID == studyId)

                var f = @"q.TrialID == @studyId";
                andFilters.Add(f);
                parameters.Add("@studyId", entity.TrialID);

                f = @"q.IsActive && !q.IsResolved";
                andFilters.Add(f);

                var fo = @"(subject != null ? subject.SiteID == @siteId : false)";
                orFilters.Add(fo);
                parameters.Add("@siteId", entity.SiteID);

                fo = @"(cuuser != null ? cuuser.AffiliationID = @affiliationId : false)";
                orFilters.Add(fo);
                fo = @"(ceequipment != null ? ceequipment.AffiliationID = @affiliationId : false)";
                orFilters.Add(fo);
                parameters.Add("@affiliationId", entity.AffiliationID);

                parameters.Add("@uaffiliationId", user.AffiliationID);
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                    case "study manager":
                    //f = @"qstatus.StatusName == ""Pending Resolution""";
                    //break;
                    case "super user":
                    case "data quality evaluator":
                        f = @"qstatus.StatusName == ""Pending Resolution"" && sender.AffiliationID == @uaffiliationId";
                        break;
                    case "site coordinator":
                    case "ophthalmic technician":
                        f = @"qstatus.StatusName == ""Pending Response""";
                        break;
                    default:
                        f = "false";
                        break;
                }
                andFilters.Add(f);

                var filter = CombinePredicates(orFilters, "||");
                var andFilter = CombinePredicates(andFilters, "&&");
                filter = CombinePredicates(new string[] { andFilter, filter }, "&&");
                query = query.Where(filter, parameters);
            }
            return result;
        }
    }
}