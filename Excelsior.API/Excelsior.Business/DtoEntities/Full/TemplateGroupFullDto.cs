using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class TemplateGroupFullDto : TemplateGroupBaseDto
    {
        public TemplateGroupFullDto()
            : this(null)
        {
        }
        public TemplateGroupFullDto(CRF_TemplateGroup entity, object sender = null)
            : base(entity, sender)
        {
            Questions = new List<TemplateQuestionFullDto>();
            Dependencies = new List<TemplateDependencyFullDto>();

            if (entity != null)
            {
                if (!(sender is TemplateQuestionBaseDto) && entity.CRF_TemplateQuestions.Count > 0)
                {
                    Questions = new List<TemplateQuestionFullDto>();
                    foreach (var question in entity.CRF_TemplateQuestions.OrderBy(item => item.QuestionSeq))
                    {
                        Questions.Add(new TemplateQuestionFullDto(question));
                    }
                }
                if (!(sender is TemplateDependencyBaseDto) && entity.CRF_TemplateDependencies.Count > 0)
                {
                    Dependencies = new List<TemplateDependencyFullDto>();
                    foreach (var dependency in entity.CRF_TemplateDependencies)
                    {
                        Dependencies.Add(new TemplateDependencyFullDto(dependency));
                    }
                }
            }
        }
        public override CRF_TemplateGroup ToEntity(CRF_TemplateGroup entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
      
        public TemplateFullDto Template { get; set; }

        public List<TemplateQuestionFullDto> Questions { get; set; }
        public List<TemplateDependencyFullDto> Dependencies { get; set; }
    }
}