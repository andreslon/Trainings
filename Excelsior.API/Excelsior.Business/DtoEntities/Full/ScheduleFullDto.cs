using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class ScheduleFullDto : ScheduleBaseDto
    {
        public ScheduleFullDto()
            : this(null)
        {
        }
        public ScheduleFullDto(PACS_TPProcList entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override PACS_TPProcList ToEntity(PACS_TPProcList entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}
