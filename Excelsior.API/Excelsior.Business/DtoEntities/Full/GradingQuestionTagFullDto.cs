using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingQuestionTagFullDto : GradingQuestionTagBaseDto
    {
        public GradingQuestionTagFullDto()
            : this(null)
        {
        }
        public GradingQuestionTagFullDto(GRD_QuestionTag entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override GRD_QuestionTag ToEntity(GRD_QuestionTag entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}
