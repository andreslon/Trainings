using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Full
{
    public class UserNotificationFullDto : UserNotificationBaseDto
    {
        public UserNotificationFullDto()
            : this(null)
        {
           
        }
        public UserNotificationFullDto(CONTACT_UserNotification entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is UserBaseDto) && entity.CONTACTUser != null )
                {
                    User = new UserFullDto(entity.CONTACTUser, this);
                }
                if (!(sender is NotificationBaseDto) && entity.CONTACTNotification != null)
                {
                    Notification = new NotificationFullDto(entity.CONTACTNotification, this);
                }
            }
        }
        public override CONTACT_UserNotification ToEntity(CONTACT_UserNotification entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);
             
            return entity;
        }

        public UserFullDto User { get; set; }
        public NotificationFullDto Notification { get; set; }


    }
}
