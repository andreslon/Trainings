using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories.Interface
{ 
    public interface IUserNotificationsRepository : IEntityBaseRepository<CONTACT_UserNotification>
    {
        IQueryable<CONTACT_Notification> GetNotifications();
    }
}
