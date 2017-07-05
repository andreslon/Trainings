using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateDependencyFullDto : TemplateDependencyBaseDto
    {
        public TemplateDependencyFullDto()
            : this(null)
        {

        }
        public TemplateDependencyFullDto(CRF_TemplateDependency entity, object sender = null)
            : base(entity, sender)
        {
            Sources = new List<TemplateDependencySourceFullDto>();
            if (entity != null)
            {
                if(!(sender is TemplateDependencySourceBaseDto) && entity.CRF_TemplateDependencySources.Count > 0)
                {
                    Sources.AddRange(entity.CRF_TemplateDependencySources.Select(x => new TemplateDependencySourceFullDto(x, this)));
                }
            }
        }
        public override CRF_TemplateDependency ToEntity(CRF_TemplateDependency entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
      
        public List<TemplateDependencySourceFullDto> Sources { get; set; }
    }
}