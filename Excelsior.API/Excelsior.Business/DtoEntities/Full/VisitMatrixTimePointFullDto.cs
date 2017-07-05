using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class VisitMatrixTimePointFullDto : VisitMatrixVisitBaseDto
    {
        public VisitMatrixTimePointFullDto()
            : this(null)
        { }
        public VisitMatrixTimePointFullDto(PACS_TimePointsList entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override PACS_TimePointsList ToEntity(PACS_TimePointsList entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}
