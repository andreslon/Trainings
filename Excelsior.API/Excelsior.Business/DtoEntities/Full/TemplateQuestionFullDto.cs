using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateQuestionFullDto : TemplateQuestionBaseDto
    {
        public TemplateQuestionFullDto()
            : this(null)
        {

        }
        public TemplateQuestionFullDto(CRF_TemplateQuestion entity, object sender = null)
            : base(entity, sender)
        {
            Answers = new List<TemplateAnswerFullDto>();

            Tags = new List<TemplateQuestionTagFullDto>();

            if (entity != null)
            {
                if (!(sender is TemplateAnswerBaseDto) && entity.CRF_TemplateAnswers.Count > 0)
                {
                    Answers = new List<TemplateAnswerFullDto>();
                    foreach (var item in entity.CRF_TemplateAnswers.OrderBy(x => x.AnswerSeq))
                    {
                        Answers.Add(new TemplateAnswerFullDto(item, this));
                    }
                }

                if (!(sender is TemplateQuestionTagBaseDto) && entity.CRF_TemplateQuestionTags.Count > 0)
                {
                    Tags = new List<TemplateQuestionTagFullDto>();
                    foreach (var item in entity.CRF_TemplateQuestionTags.OrderBy(x => x.QuestionTagName))
                    {
                        Tags.Add(new TemplateQuestionTagFullDto(item, this));
                    }
                }
            }
        }
        public override CRF_TemplateQuestion ToEntity(CRF_TemplateQuestion entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }

        public List<TemplateAnswerFullDto> Answers { get; set; }
        public List<TemplateQuestionTagFullDto> Tags { get; set; }
    }
}
