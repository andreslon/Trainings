using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class AttachmentsRepository : EntityBaseRepository<PACS_SeriesAttachment>, IAttachmentsRepository
    {
        #region Constructor

        public AttachmentsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public long GetUserId(string userId)
        {
            CONTACT_User u = null;
            if (!string.IsNullOrEmpty(userId))
            {
                u = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == userId.ToString());
                });
            }

            return u.UserID;
        }

        public IQueryable<PACS_SeriesAttachment> GetAll(long? seriesId, string userId, string laterality, bool? isActive, string search)
        {
            var query = GetAll();

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            //find the user

            var userIntId = GetUserId(userId);

            var f1 = @"UserID == @userIntId";
            andFilters.Add(f1);
            parameters.Add("@userIntId", userIntId);
 

            if (isActive.HasValue)
            {
                var f = @"IsActive == @isActive";
                andFilters.Add(f);
                parameters.Add("@isActive", isActive);
            }

            if (seriesId.HasValue)
            {
                var f = @"SeriesID == @seriesId";
                andFilters.Add(f);
                parameters.Add("@seriesId", seriesId);
            }
            if (!string.IsNullOrEmpty(laterality))
            {
                var f = @"Laterality == @laterality";
                andFilters.Add(f);
                parameters.Add("@laterality", laterality);
            }
            if (!string.IsNullOrEmpty(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                var fi = @"Laterality.Contains(@search)
                    || UserID.Contains(@search)
                    || SeriesID.Contains(@search)";
                orFilters.Add(fi);

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
            return query;
        }

        public override void Delete(PACS_SeriesAttachment entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<PACS_SeriesAttachment, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }
        #endregion
    }
}
