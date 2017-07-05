using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class ImpressionFullDto : ImpressionBaseDto
    {
        public ImpressionFullDto()
            : this(null)
        {

        }
        public ImpressionFullDto(GRD_Impression entity, object sender = null)
            : base(entity, sender)
        {

        }
        public override GRD_Impression ToEntity(GRD_Impression entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}
