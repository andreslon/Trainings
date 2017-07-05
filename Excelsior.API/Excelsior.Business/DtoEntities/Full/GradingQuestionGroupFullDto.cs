using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingQuestionGroupFullDto : GradingQuestionGroupBaseDto
    {
        public GradingQuestionGroupFullDto()
            : this(null)
        {
            Questions = new List<GradingTemplateQuestionFullDto>();
        }
        public GradingQuestionGroupFullDto(GRD_QuestionGroup entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override GRD_QuestionGroup ToEntity(GRD_QuestionGroup entity = null)
        {
            entity = base.ToEntity(entity);

            if (Questions.Count > 0)
            {
                entity.GRD_TempQuestions.Clear();
                foreach (var a in Questions)
                {
                    var lde = a.ToEntity();
                    entity.GRD_TempQuestions.Add(lde);
                }
            }

            return entity;
        }

        public List<GradingTemplateQuestionFullDto> Questions { get; set; }
    }
}
