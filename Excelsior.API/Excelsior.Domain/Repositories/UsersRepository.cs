using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class UsersRepository : EntityBaseRepository<CONTACT_User>, IUsersRepository
    {
        #region Constructor

        public UsersRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<CONTACT_User> GetAll(bool? isActive, string search)
        {
            var users = GetAll();

            if (isActive.HasValue)
            {
                users = users.Where(x => x.IsActive == isActive);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                //users = users.Where(x => x.LastName.Contains(search)
                //    || x.FirstName.Contains(search)
                //    || x.LoweredUserName.Contains(search)
                //    || x.Email.Contains(search)
                //    || x.CONTACTAffiliation.AffiliationName.Contains(search)
                //    || (x.CONTACTAffiliation.CONTACTCountry == null ? false : x.CONTACTAffiliation.CONTACTCountry.CountryName.Contains(search)));

                users = from user in users
                        join affiliation in Context.CONTACT_Affiliations on user.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        where user.LastName.Contains(search)
                            || user.FirstName.Contains(search)
                            || user.LoweredUserName.Contains(search)
                            || user.Email.Contains(search)
                            || affiliation.AffiliationName.Contains(search)
                            || (country != null ? country.CountryName.Contains(search) : false)
                        select user;
            }

            return users;
        }

        public override void Delete(CONTACT_User entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CONTACT_User, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}