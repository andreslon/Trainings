using Excelsior.Domain;
using System;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthClientSecretBaseDto
    {
        public AuthClientSecretBaseDto()
            : this(null)
        {

        }
        public AuthClientSecretBaseDto(AUTH_ClientSecret entity, object sender = null)
        {
            if (entity != null)
            {
                ClientSecretId = entity.ClientSecretId;
                ClientSecret = entity.ClientSecret;
                ClientId = entity.ClientId; 
            }
        }
        public virtual AUTH_ClientSecret ToEntity(AUTH_ClientSecret entity = null)
        {
            if (entity == null)
            {
                entity = new AUTH_ClientSecret();
            }


            entity.ClientSecretId = ClientSecretId;
            entity.ClientSecret = ClientSecret;
            entity.ClientId = ClientId; 

            return entity;
        }


        public Guid ClientSecretId { get; set; }
        public string ClientSecret { get; set; }
        public Guid ClientId { get; set; }
        

    }
}
