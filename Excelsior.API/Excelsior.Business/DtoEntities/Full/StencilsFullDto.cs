using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class StencilsFullDto : StencilsBaseDto
    {
        public StencilsFullDto()
            : this(null)
        {
        }
        public StencilsFullDto(MEA_Stencil entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override MEA_Stencil ToEntity(MEA_Stencil entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}
