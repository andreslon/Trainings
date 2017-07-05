using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateAnswerFullDto: TemplateAnswerBaseDto
    {
        public TemplateAnswerFullDto()
            : this(null)
        {

        }
        public TemplateAnswerFullDto(CRF_TemplateAnswer entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is TemplateQuestionBaseDto) && entity.CRFTemplateQuestion != null)
                {
                    TemplateQuestion = new TemplateQuestionFullDto(entity.CRFTemplateQuestion, this);
                }
            }
        }
        public override CRF_TemplateAnswer ToEntity(CRF_TemplateAnswer entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
       
        public TemplateQuestionFullDto TemplateQuestion { get; set; }
    }
}
