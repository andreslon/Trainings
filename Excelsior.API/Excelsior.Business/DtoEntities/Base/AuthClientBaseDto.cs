using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthClientBaseDto
    {
        public AuthClientBaseDto()
            : this(null)
        {

        }
        public AuthClientBaseDto(AUTH_Client entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.ClientId;
                Name = entity.ClientName;
                RedirectUri = entity.RedirectUri;
                UserId = entity.UserId;
            }
        }
        public virtual AUTH_Client ToEntity(AUTH_Client entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new AUTH_Client();
            } 
            entity.ClientId = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.ClientName = Name;
                if (fieldvalidation["redirecturi"])
                    entity.RedirectUri = RedirectUri;
                if (fieldvalidation["userid"])
                    entity.UserId = UserId;
            } 
            return entity;
        }


        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RedirectUri { get; set; }
        public Guid UserId { get; set; }
    }
}
