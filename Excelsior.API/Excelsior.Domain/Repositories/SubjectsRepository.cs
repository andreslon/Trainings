using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class SubjectsRepository : EntityBaseRepository<PACS_Subject>, ISubjectsRepository
    {
        #region Constructor

        public SubjectsRepository(DataModel context) : base(context)
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
                case "randomizedid":
                   return string.Format("subject.RandomizedSubjectID {0}", dir);
                case "alternativerandomizedid":
                    return string.Format("subject.AlternativeRandomizedSubjectID {0}", dir);
                case "namecode":
                   return string.Format("subject.NameCode {0}", dir);
                case "gender":
                    return string.Format("subject.Gender {0}", dir);
                default:
                    return "";
            }
        }

        public IQueryable<PACS_Subject> GetAll(string userId, long? trialId, long? siteId, long? affiliationId, long? groupId, long? cohortId, bool? isActive, bool? isRejected, string search, string sort)
        {
            var subjects = GetAll();
            var query = from subject in subjects
                        join site in Context.PACS_Sites on subject.SiteID equals site.SiteID
                        join affiliation in Context.CONTACT_Affiliations on site.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join scohort in Context.PACS_SubjectCohorts on subject.SubjectCohortID equals scohort.SubjectCohortID into cohorts
                        from scohort in cohorts.DefaultIfEmpty()
                        join sgroup in Context.PACS_SubjectGroups on subject.SubjectGroupID equals sgroup.SubjectGroupID into groups
                        from sgroup in groups.DefaultIfEmpty()
                        select new
                        {
                            subject = subject,
                            site = site,
                            affiliation = affiliation,
                            country = country,
                            scohort = scohort,
                            sgroup = sgroup
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();
            
            if (trialId.HasValue)
            {
                var f = @"site.TrialID == @trialId";
                andFilters.Add(f);
                parameters.Add("@trialId", trialId);
            }

            if (siteId.HasValue)
            {
                var f = @"subject.SiteID == @siteId";
                andFilters.Add(f);
                parameters.Add("@siteId", siteId);
            }

            if (affiliationId.HasValue)
            {
                var f = @"site.AffiliationID == @affiliationId";
                andFilters.Add(f);
                parameters.Add("@affiliationId", affiliationId);
            }

            if (groupId.HasValue)
            {
                var f = @"subject.GroupID == @groupId";
                andFilters.Add(f);
                parameters.Add("@groupId", groupId);
            }

            if (cohortId.HasValue)
            {
                var f = @"subject.CohortID == @cohortId";
                andFilters.Add(f);
                parameters.Add("@cohortId", cohortId);
            }

            if (isActive.HasValue)
            {
                var f = @"subject.IsActive == @isActive";
                andFilters.Add(f);
                parameters.Add("@isActive", isActive);
            }

            if (isRejected.HasValue)
            {
                var f = @"subject.IsRejected == @isRejected";
                andFilters.Add(f);
                parameters.Add("@isRejected", isRejected);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                var fi = @"subject.RandomizedSubjectID.Contains(@search)
                    || subject.AlternativeRandomizedSubjectID.Contains(@search)
                    || subject.NameCode.Contains(@search)";
                orFilters.Add(fi);

                if (!siteId.HasValue)
                {
                    var fii = @"site.RandomizedSiteID.Contains(@search)
                    || affiliation.AffiliationName.Contains(@search)
                    || (country != null ? country.CountryName.Contains(@search) : false)";
                    orFilters.Add(fii);
                }

                if (!cohortId.HasValue)
                {
                    var fii = @"(scohort != null ? scohort.CohortName.Contains(@search) : false)";
                    orFilters.Add(fii);
                }
                if (!groupId.HasValue)
                {
                    var fii = @"(sgroup != null ? sgroup.GroupName.Contains(@search)
                    || sgroup.GroupDescription.Contains(@search) : false)";
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

            IQueryable result;
            if (!string.IsNullOrWhiteSpace(sort))
            {
                result = ApplySortExpression(sort, query);
            }
            else
            {
                query = query.OrderBy(x => x.subject.RandomizedSubjectID).ThenBy(x => x.subject.AlternativeRandomizedSubjectID);
                result = query as IQueryable;
            }

            return result.Select("subject").Cast<PACS_Subject>();
        }

        public override void Delete(PACS_Subject entity)
        {
            entity.IsActive = false;               
        }

        public override void DeleteWhere(Expression<Func<PACS_Subject, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }
    }
}