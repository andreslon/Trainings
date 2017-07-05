using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
   public class UserNotificationBaseDto
    {
        public UserNotificationBaseDto()
            : this(null)
        {

        }
        public UserNotificationBaseDto(CONTACT_UserNotification entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.UserNotificationID;
                UserId = entity.UserID;
                NotificationId = entity.NotificationID;
            }
        }
        public virtual CONTACT_UserNotification ToEntity(CONTACT_UserNotification entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CONTACT_UserNotification();
            }

            entity.UserNotificationID = Id;
            entity.UserID = UserId;
            entity.NotificationID = NotificationId;

            return entity;
        }

        public long Id { get; set; }
        public long? UserId { get; set; }
        [Required]
        public long? NotificationId { get; set; }
    }
}
