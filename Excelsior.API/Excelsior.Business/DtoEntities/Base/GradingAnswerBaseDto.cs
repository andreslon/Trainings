using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingAnswerBaseDto
    {
        public GradingAnswerBaseDto()
            : this(null)
        {
        }
        public GradingAnswerBaseDto(GRD_GradingAnswer entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GAnswersID;
                Text = entity.GAnswerString;
                Index = entity.GAnswerSeq;
                StudyId = entity.TrialID;
            }
        }
        public virtual GRD_GradingAnswer ToEntity(GRD_GradingAnswer entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_GradingAnswer();
            }

            entity.GAnswersID = Id.GetValueOrDefault();
            entity.GAnswerString = Text;
            entity.GAnswerSeq = Index;
            entity.TrialID = StudyId; 
            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
    }
}
