using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingQuestionBaseDto
    {
        public GradingQuestionBaseDto()
            : this(null)
        {
        }
        public GradingQuestionBaseDto(GRD_GradingQuestion entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GQuestionID;
                Text = entity.GQuestionString;
                Description = entity.GQuestionDes;
                AnswerMask = entity.AnswerMask;
                IsAnswerMeasurement = entity.IsAnswerMeasurement;
            }
        }
        public virtual GRD_GradingQuestion ToEntity(GRD_GradingQuestion entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_GradingQuestion();
            }
            entity.GQuestionID = Id.GetValueOrDefault();
            entity.GQuestionString = Text;
            entity.GQuestionDes = Description;
            entity.AnswerMask = AnswerMask;
            entity.IsAnswerMeasurement = IsAnswerMeasurement.GetValueOrDefault();
            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Text { get; set; }
        public string Description { get; set; }
        public bool? IsAnswerMeasurement { get; set; }
        [StringLength(64)]
        public string AnswerMask { get; set; }
    }
}