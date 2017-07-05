using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingQuestionTagBaseDto
    {
        public GradingQuestionTagBaseDto()
            : this(null)
        {
        }
        public GradingQuestionTagBaseDto(GRD_QuestionTag entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GQuestionTagID;
                Name = entity.GQuestionTagString;
            }
        }
        public virtual GRD_QuestionTag ToEntity(GRD_QuestionTag entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_QuestionTag();
            }

            entity.GQuestionTagID = Id.GetValueOrDefault();
            entity.GQuestionTagString = Name;

            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
