using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class WorkflowStepFullDto : WorkflowStepBaseDto
    {
        public WorkflowStepFullDto()
            : this(null)
        {

        }
        public WorkflowStepFullDto(WF_StepList entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override WF_StepList ToEntity(WF_StepList entity = null)
        {
            entity = base.ToEntity(entity);


            return entity;
        }
    }
}
