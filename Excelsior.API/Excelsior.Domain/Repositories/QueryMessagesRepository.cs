using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class QueryMessagesRepository : EntityBaseRepository<QRY_Message>, IQueryMessagesRepository
    {
        #region Constructor

        public QueryMessagesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<QRY_Message> GetAll(long queryId, bool? isActive, string search)
        {
            var result = FindBy(x => x.QueryID == queryId);

            if (isActive.HasValue)
            {
                result = result.Where(x => x.IsActive == isActive);
            }

            if (!string.IsNullOrEmpty(search))
            {
                result = from msg in result
                         join user in Context.CONTACT_Users on msg.UserID equals user.UserID into users
                         from user in users.DefaultIfEmpty()
                         join affiliation in Context.CONTACT_Affiliations on user.AffiliationID equals affiliation.AffiliationID into affiliations
                         from affiliation in affiliations.DefaultIfEmpty()
                         join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                         from country in countries.DefaultIfEmpty()
                         where msg.MessageBody.Contains(search)
                            || (user != null ? (user.LastName.Contains(search)
                            || user.FirstName.Contains(search)
                            || user.LoweredUserName.Contains(search)
                            || user.Email.Contains(search)
                            || affiliation.AffiliationName.Contains(search)
                            || (country != null ? country.CountryName.Contains(search) : false)) : false)
                         select msg;
            }

            return result.OrderByDescending(x => x.DateCreated);
        }

        public override void Delete(QRY_Message entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<QRY_Message, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}
