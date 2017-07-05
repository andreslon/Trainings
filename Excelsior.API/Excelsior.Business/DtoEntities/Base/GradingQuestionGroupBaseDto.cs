using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingQuestionGroupBaseDto
    {
        public GradingQuestionGroupBaseDto()
            : this(null)
        {
        }
        public GradingQuestionGroupBaseDto(GRD_QuestionGroup entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GQuestionGroupID;
                Name = entity.GQuestionGroupName;
                Index = entity.GQuestionGroupSeq;
            }
        }
        public virtual GRD_QuestionGroup ToEntity(GRD_QuestionGroup entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_QuestionGroup();
            }

            entity.GQuestionGroupID = Id.GetValueOrDefault();
            entity.GQuestionGroupName = Name;
            entity.GQuestionGroupSeq = Index; 

            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }
    }
}
