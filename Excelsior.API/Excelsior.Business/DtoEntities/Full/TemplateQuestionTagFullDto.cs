using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateQuestionTagFullDto : TemplateQuestionTagBaseDto
    {
        public TemplateQuestionTagFullDto()
            : this(null)
        {
        }
        public TemplateQuestionTagFullDto(CRF_TemplateQuestionTag entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override CRF_TemplateQuestionTag ToEntity(CRF_TemplateQuestionTag entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}
