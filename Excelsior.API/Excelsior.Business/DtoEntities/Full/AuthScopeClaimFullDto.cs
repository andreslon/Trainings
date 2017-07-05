using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuthScopeClaimFullDto : AuthScopeClaimBaseDto
    {
        public AuthScopeClaimFullDto()
            : this(null)
        {
        }
        public AuthScopeClaimFullDto(AUTH_ScopeClaim entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is AuthScopeBaseDto) && entity.AUTHScope != null)
                {
                    AUTHScope = new AuthScopeFullDto(entity.AUTHScope, this);
                }
            } 
        }
        public override AUTH_ScopeClaim ToEntity(AUTH_ScopeClaim entity = null)
        {
            entity = base.ToEntity(entity);
            if (AUTHScope != null)
            {
                entity.AUTHScope = AUTHScope.ToEntity(entity.AUTHScope);
            }
            return entity;
        }

        public AuthScopeFullDto AUTHScope { get; set; }
    }
}
