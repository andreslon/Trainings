using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways.Interfaces
{
    public interface IUserNotificationsGateway
    {
        ResultInfo<IList<NotificationBaseDto>> GetNotifications();
        ResultInfo<IList<UserNotificationBaseDto>> GetAll(int userId);
        ResultInfo<bool> Update(int userId, List<UserNotificationFullDto> request);
    }
}
