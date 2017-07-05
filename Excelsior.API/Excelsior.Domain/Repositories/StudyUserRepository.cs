using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class StudyUserRepository : EntityBaseRepository<CONTACT_UserTrial>, IStudyUserRepository
    {
        #region Constructor
        public StudyUserRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions
        public IQueryable<CONTACT_UserTrial> GetAll(string userId, bool? isActive)
        {
            var query = GetAll();
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
                query = query.Where(x => x.UserID == u.UserID);
            }
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            } 
            return query;
        }
        #endregion
    }
}
