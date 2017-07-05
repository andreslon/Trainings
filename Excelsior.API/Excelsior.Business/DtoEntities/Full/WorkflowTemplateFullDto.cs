using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{

    public class WorkflowTemplateFullDto : WorkflowTemplateBaseDto
    {
        public WorkflowTemplateFullDto()
            : this(null)
        {
        }
        public WorkflowTemplateFullDto(WF_Template entity, object sender = null)
            : base(entity, sender)
        {
            Steps = new List<WorkflowTemplateStepFullDto>();

            if (entity != null)
            {
                if (!(sender is WorkflowTemplateStepBaseDto) && entity.WF_TempSteps.Count > 0)
                {
                    Steps = new List<WorkflowTemplateStepFullDto>();
                    foreach (var step in entity.WF_TempSteps.OrderBy(item => item.WFStepOrder))
                    {
                        Steps.Add(new WorkflowTemplateStepFullDto(step));
                    }
                }
            }
        }

        public override WF_Template ToEntity(WF_Template entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }

        public List<WorkflowTemplateStepFullDto> Steps { get; set; }
    }
}
