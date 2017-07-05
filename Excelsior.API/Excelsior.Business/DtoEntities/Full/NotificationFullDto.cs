using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Full
{
   public class NotificationFullDto: NotificationBaseDto
    {
        public NotificationFullDto()
            : this(null)
        {
            UserNotifications = new List<UserNotificationFullDto>();
            //NotificationRoles = new List<NotificationRoleFullDto>();
        }
        public NotificationFullDto(CONTACT_Notification entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is UserNotificationBaseDto) && entity.CONTACT_UserNotifications.Count > 0)
                {
                    UserNotifications = new List<UserNotificationFullDto>();
                    foreach (var item in entity.CONTACT_UserNotifications)
                    {
                        UserNotifications.Add(new UserNotificationFullDto(item, this));
                    }
                }
                //if (!(sender is NotificationRoleBaseDto) && entity.CONTACT_NotificationRoles.Count > 0)
                //{
                //    NotificationRoles = new List<NotificationRoleFullDto>();
                //    foreach (var item in entity.CONTACT_NotificationRoles)
                //    {
                //        NotificationRoles.Add(new NotificationRoleFullDto(item, this));
                //    }
                //}
            }
        }
        public override CONTACT_Notification ToEntity(CONTACT_Notification entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);
            if (UserNotifications.Count > 0)
            {
                entity.CONTACT_UserNotifications.Clear();
                foreach (var a in UserNotifications)
                {
                    var lde = a.ToEntity();
                    entity.CONTACT_UserNotifications.Add(lde);
                }
            }
            //if (NotificationRoles.Count > 0)
            //{
            //    entity.CONTACT_NotificationRoles.Clear();
            //    foreach (var a in NotificationRoles)
            //    {
            //        var lde = a.ToEntity();
            //        entity.CONTACT_NotificationRoles.Add(lde);
            //    }
            //}
            return entity;
        }


        public List<UserNotificationFullDto> UserNotifications { get; set; }
        //public List<NotificationRoleFullDto> NotificationRoles { get; set; }
    }
}
