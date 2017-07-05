using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class UserNotificationsRepository : EntityBaseRepository<CONTACT_UserNotification>, IUserNotificationsRepository
    {
        #region Constructor
        public UserNotificationsRepository(DataModel context) : base(context)
        {
        } 
        #endregion

        #region Functions
        public IQueryable<CONTACT_Notification> GetNotifications()
        {
            var notifications = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.CONTACT_Notifications;
            });
            return notifications;
        }
         
        #endregion
    }
}
