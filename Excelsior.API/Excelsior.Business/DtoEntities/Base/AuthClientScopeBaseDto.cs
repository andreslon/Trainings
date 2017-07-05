using Excelsior.Domain;
using System;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuthClientScopeBaseDto
    {
        public AuthClientScopeBaseDto()
            : this(null)
        {

        }
        public AuthClientScopeBaseDto(AUTH_ClientScope entity, object sender = null)
        {
            if (entity != null)
            {
                ClientScopeId = entity.ClientScopeId;
                ScopeId = entity.ScopeId;
                ClientId = entity.ClientId;
                Name = entity.Name;
            }
        }
        public virtual AUTH_ClientScope ToEntity(AUTH_ClientScope entity = null)
        {
            if (entity == null)
            {
                entity = new AUTH_ClientScope();
            }


            entity.ClientScopeId = ClientScopeId;
            entity.ScopeId = ScopeId;
            entity.ClientId = ClientId;
            entity.Name = Name;

            return entity;
        }


        public Guid ClientScopeId { get; set; }
        public Guid ScopeId { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }

 
   

    }
}
