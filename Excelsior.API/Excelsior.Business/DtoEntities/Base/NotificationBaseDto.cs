using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class NotificationBaseDto
    {
        public NotificationBaseDto()
            : this(null)
        {

        }
        public NotificationBaseDto(CONTACT_Notification entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.NotificationID;
                Name = entity.NotificationName;
                Description = entity.NotificationDesc;
                Message = entity.NotificationMsg;
                IsPrivate = entity.IsPrivate;
            }
        }
        public virtual CONTACT_Notification ToEntity(CONTACT_Notification entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CONTACT_Notification();
            }

            entity.NotificationID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.NotificationName = Name;
                if (fieldvalidation["description"])
                    entity.NotificationDesc = Description;
                if (fieldvalidation["message"])
                    entity.NotificationMsg = Message;
                if (fieldvalidation["isprivate"])
                    entity.IsPrivate = IsPrivate;
            }


            return entity;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public bool IsPrivate { get; set; } 
    }
}
