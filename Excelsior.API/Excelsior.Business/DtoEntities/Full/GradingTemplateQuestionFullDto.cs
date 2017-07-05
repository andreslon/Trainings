using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingTemplateQuestionFullDto : GradingTemplateQuestionBaseDto
    {
        public GradingTemplateQuestionFullDto()
            : this(null)
        {
            
        }
        public GradingTemplateQuestionFullDto(GRD_TempQuestion entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is GradingQuestionGroupBaseDto) && entity.GRDQuestionGroup != null)
                {
                    Group = new GradingQuestionGroupFullDto(entity.GRDQuestionGroup, this);
                }
                if (!(sender is GradingQuestionBaseDto) && entity.GRDGradingQuestion != null)
                {
                    Question = new GradingQuestionFullDto(entity.GRDGradingQuestion, this);
                }
            }
        }
        public override GRD_TempQuestion ToEntity(GRD_TempQuestion entity = null)
        {
            entity = base.ToEntity(entity);

            if (Group != null)
            {
                entity.GRDQuestionGroup = Group.ToEntity();
            }
            if (Question != null)
            {
                //TODO:
                //Question.GRDGradingQuestion = Question.ToEntity();
            }
            return entity;
        }

        public GradingQuestionGroupFullDto Group { get; set; }
        public GradingQuestionFullDto Question { get; set; }
    }
}
