using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class MediaTypeFullDto : MediaTypeBaseDto
    {
        public MediaTypeFullDto()
            : this(null)
        {
        }
        public MediaTypeFullDto(PACS_DataType entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                 
            }
        }
        public override PACS_DataType ToEntity(PACS_DataType entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}
