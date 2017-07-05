using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AttachmentFullDto : AttachmentBaseDto
    {
        public AttachmentFullDto()
            : this(null)
        {
        }
        public AttachmentFullDto(PACS_SeriesAttachment entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override PACS_SeriesAttachment ToEntity(PACS_SeriesAttachment entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}
