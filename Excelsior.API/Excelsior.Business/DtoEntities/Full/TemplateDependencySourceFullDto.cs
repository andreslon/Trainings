using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateDependencySourceFullDto : TemplateDependencySourceBaseDto
    {
        public TemplateDependencySourceFullDto()
            : this(null)
        {

        }
        public TemplateDependencySourceFullDto(CRF_TemplateDependencySource entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is TemplateDependencyBaseDto) && entity.CRFTemplateDependency != null)
                {
                    Dependency = new TemplateDependencyFullDto(entity.CRFTemplateDependency, this);
                }
            }
        }
        public override CRF_TemplateDependencySource ToEntity(CRF_TemplateDependencySource entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
      
        public TemplateDependencyFullDto Dependency { get; set; }
    }
}