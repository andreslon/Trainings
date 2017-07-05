using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class WorkflowStepBaseDto
    {
        public WorkflowStepBaseDto()
            : this(null)
        {
        }
        public WorkflowStepBaseDto(WF_StepList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.WFStepListID;
                Description = entity.WFStepListDes;
                Index = entity.SortingOrder;
            }
        }
        public virtual WF_StepList ToEntity(WF_StepList entity = null)
        {
            if (entity == null)
            {
                entity = new WF_StepList();
            }

            entity.WFStepListID = Id.GetValueOrDefault();
            entity.WFStepListDes = Description;
            entity.SortingOrder = Index;

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }
    }
}
