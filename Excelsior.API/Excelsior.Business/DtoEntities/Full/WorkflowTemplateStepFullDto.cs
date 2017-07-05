using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class WorkflowTemplateStepFullDto : WorkflowTemplateStepBaseDto
    {
        public WorkflowTemplateStepFullDto()
            : this(null)
        {

        }

        public WorkflowTemplateStepFullDto(WF_TempStep entity)
           : base(entity)
        {
            if (entity != null)
            {

            }
        }
        public override WF_TempStep ToEntity(WF_TempStep entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }

    }
}
