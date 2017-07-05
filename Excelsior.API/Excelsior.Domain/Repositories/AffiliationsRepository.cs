using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class AffiliationsRepository : EntityBaseRepository<CONTACT_Affiliation>, IAffiliationsRepository
    {
        #region Constructor

        public AffiliationsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        public IQueryable<CONTACT_Affiliation> GetAll(string userId, bool? isActive, string search)
        {
            var affiliations = GetAll();

            CONTACT_User u = null;
            if (!string.IsNullOrEmpty(userId))
            {
                u = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == userId);
                });
            }

            if (u != null)
            {
                var roles = new List<string> { };
                if ("[ophthalmic technician][site coordinator]".Contains(u.AspnetRole.LoweredRoleName))
                {
                    affiliations = affiliations.Where(x => x.AffiliationID == u.AffiliationID);
                }
            }

            if (isActive.HasValue)
            {
                affiliations = affiliations.Where(x => x.IsActive == isActive);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                //affiliations = affiliations.Where(x => x.AffiliationName.Contains(search)
                //    || (x.CONTACTCountry == null ? false : x.CONTACTCountry.CountryName.Contains(search)));

                affiliations = from affiliation in affiliations
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        where affiliation.AffiliationName.Contains(search)
                            || (country != null ? country.CountryName.Contains(search) : false)
                        select affiliation;
            }

            return affiliations;
        }

        public override void Delete(CONTACT_Affiliation entity)
        {
            entity.IsActive = false;               
        }

        public override void DeleteWhere(Expression<Func<CONTACT_Affiliation, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }
    }
}