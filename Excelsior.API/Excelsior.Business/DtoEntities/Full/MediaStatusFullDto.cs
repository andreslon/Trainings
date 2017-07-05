using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class MediaStatusFullDto : MediaStatusBaseDto
    {
        public MediaStatusFullDto()
            : this(null)
        {

        }
        public MediaStatusFullDto(PACS_RawDataStatus entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override PACS_RawDataStatus ToEntity(PACS_RawDataStatus entity = null)
        {
            entity = base.ToEntity(entity);
            return entity;
        }
    }
}