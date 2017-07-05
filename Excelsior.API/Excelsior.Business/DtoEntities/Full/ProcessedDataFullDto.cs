using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class ProcessedDataFullDto : ProcessedDataBaseDto
    {
        public ProcessedDataFullDto()
            : this(null)
        { 
        }
        public ProcessedDataFullDto(PACS_ProcessedDatum entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                
            }
        }
        public override PACS_ProcessedDatum ToEntity(PACS_ProcessedDatum entity = null)
        {
            entity = base.ToEntity(entity); 

            return entity;
        }
    }
}
