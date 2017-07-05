using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class WorkflowTemplateStepBaseDto
    {
        public WorkflowTemplateStepBaseDto()
            : this(null)
        {
        }
        public WorkflowTemplateStepBaseDto(WF_TempStep entity)
        {
            if (entity != null)
            {
                Id = entity.WFTempStepID;
                WorkflowStepId = entity.WFStepListID;
                Index = entity.WFStepOrder;
                TemplateId = entity.WFTemplateID;
                ShouldSkip = entity.ShouldSkip;

                if (entity.WFStepList != null)
                {
                    Description = entity.WFStepList.WFStepListDes;
                }
            }
        }

        public virtual WF_TempStep ToEntity(WF_TempStep entity = null)
        {
            if (entity == null)
            {
                entity = new WF_TempStep();
            }

            entity.WFTempStepID = Id.GetValueOrDefault();
            entity.WFStepListID = WorkflowStepId.GetValueOrDefault();
            entity.WFStepOrder = Index.GetValueOrDefault();
            entity.WFTemplateID = TemplateId.GetValueOrDefault();
            entity.ShouldSkip = ShouldSkip.GetValueOrDefault();

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateId { get; set; }
        [Range(0, long.MaxValue)]
        public long? WorkflowStepId { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }
        public bool? ShouldSkip { get; set; }
    }
}
