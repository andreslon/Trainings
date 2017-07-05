using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateFullDto: TemplateBaseDto
    {
        public TemplateFullDto()
            : base(null)
        {
        }
        public TemplateFullDto(CRF_Template entity, object sender = null)
            : base(entity, sender)
        {
            Groups = new List<TemplateGroupFullDto>();

            if (entity != null)
            {
                if (!(sender is TemplateGroupBaseDto) && entity.CRF_TemplateGroups.Count > 0)
                {
                    Groups = new List<TemplateGroupFullDto>();
                    foreach (var group in entity.CRF_TemplateGroups.OrderBy(item => item.GroupSeq))
                    {
                        Groups.Add(new TemplateGroupFullDto(group));
                    }
                }
            }
        }
        public override CRF_Template ToEntity(CRF_Template entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }

        public StudyFullDto Study { get; set; }
        public List<TemplateGroupFullDto> Groups { get; set; }
    }
}
