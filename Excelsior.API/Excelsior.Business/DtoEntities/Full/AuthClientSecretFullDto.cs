using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuthClientSecretFullDto : AuthClientSecretBaseDto
    {
        public AuthClientSecretFullDto()
            : this(null)
        {
        }
        public AuthClientSecretFullDto(AUTH_ClientSecret entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is AuthClientBaseDto) && entity.AUTHClient != null)
                {
                    AUTHClient = new AuthClientFullDto(entity.AUTHClient, this);
                }
            }
        }
        public override AUTH_ClientSecret ToEntity(AUTH_ClientSecret entity = null)
        {
            entity = base.ToEntity(entity);
            if (AUTHClient != null)
            {
                entity.AUTHClient = AUTHClient.ToEntity(entity.AUTHClient);
            }
            return entity;
        }

        public AuthClientFullDto AUTHClient { get; set; }
    }
}
 