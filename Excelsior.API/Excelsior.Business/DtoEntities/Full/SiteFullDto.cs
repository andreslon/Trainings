using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class SiteFullDto : SiteBaseDto
    {
        public SiteFullDto()
            : this(null)
        {

        }
        public SiteFullDto(PACS_Site entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override PACS_Site ToEntity(PACS_Site entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}