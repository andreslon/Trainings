using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TimePointFullDto : TimePointBaseDto
    {
        public TimePointFullDto()
            : this(null)
        {
         }
        public TimePointFullDto(PACS_TimePointsList entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
 

            }
        }
        public override PACS_TimePointsList ToEntity(PACS_TimePointsList entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            
            return entity;
        }
  }
}
