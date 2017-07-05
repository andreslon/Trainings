using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{

    public class AuthClientScopeFullDto : AuthClientScopeBaseDto
    {
        public AuthClientScopeFullDto()
            : this(null)
        {
        }
        public AuthClientScopeFullDto(AUTH_ClientScope entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is AuthClientBaseDto) && entity.AUTHClient != null)
                {
                    AUTHClient = new AuthClientFullDto(entity.AUTHClient, this);
                }
                if (!(sender is AuthScopeBaseDto) && entity.AUTHScope != null)
                {
                    AUTHScope = new AuthScopeFullDto(entity.AUTHScope, this);
                }
            }
        }
        public override AUTH_ClientScope ToEntity(AUTH_ClientScope entity = null)
        {
            entity = base.ToEntity(entity);
            if (AUTHClient != null)
            {
                entity.AUTHClient = AUTHClient.ToEntity(entity.AUTHClient);
            }
            if (AUTHScope != null)
            {
                entity.AUTHScope = AUTHScope.ToEntity(entity.AUTHScope);
            }
            return entity;
        }

        public AuthClientFullDto AUTHClient { get; set; } 
        public AuthScopeFullDto AUTHScope { get; set; }

    }
}
