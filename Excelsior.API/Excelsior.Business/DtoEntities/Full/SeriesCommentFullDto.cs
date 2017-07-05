using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class SeriesCommentFullDto : SeriesCommentBaseDto
    {
        public SeriesCommentFullDto()
            : this(null)
        {
        }
        public SeriesCommentFullDto(PACS_SeriesComment entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override PACS_SeriesComment ToEntity(PACS_SeriesComment entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}