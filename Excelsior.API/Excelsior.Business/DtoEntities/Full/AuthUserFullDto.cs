using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuthUserFullDto : AuthUserBaseDto
    {
        public AuthUserFullDto()
            : this(null)
        { 
        }
        public AuthUserFullDto(Aspnet_Membership entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                
            }
        }
        public override Aspnet_Membership ToEntity(Aspnet_Membership entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields); 
            return entity; 
        }
         
    }
}
