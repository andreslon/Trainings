using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class SubjectFullDto : SubjectBaseDto
    {
        public SubjectFullDto()
            : this(null)
        {
          }
        public SubjectFullDto(PACS_Subject entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is SiteBaseDto) && entity.PACSSite != null)
                {
                    Site = new SiteFullDto(entity.PACSSite, this);
                }
            }
        }
        public override PACS_Subject ToEntity(PACS_Subject entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            
            return entity;
        }

        public SiteFullDto Site { get; set; }
    }
}
