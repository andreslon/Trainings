using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateDependencyBaseDto
    {
        public TemplateDependencyBaseDto()
            : this(null)
        {

        }
        public TemplateDependencyBaseDto(CRF_TemplateDependency entity, object sender = null)
        {
            if (entity != null)
            {
                ActionEnable = entity.ActionEnable;
                Id = entity.CRFTemplateDependencyID;
                Expression = entity.Expression;
                TemplateGroupId = entity.CRFTemplateGroupID;
                TargetAnswerId = entity.TargetAnswerID;
                TargetQuestionId = entity.TargetQuestionID;
            }
        }
        public virtual CRF_TemplateDependency ToEntity(CRF_TemplateDependency entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_TemplateDependency();
            }

            entity.CRFTemplateDependencyID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["actionenable"])
                    entity.ActionEnable = ActionEnable.GetValueOrDefault();
                if (fieldvalidation["expression"])
                    entity.Expression = Expression;
                if (fieldvalidation["templategroupid"])
                    entity.CRFTemplateGroupID = TemplateGroupId;
                if (fieldvalidation["targetanswerid"])
                    entity.TargetAnswerID = TargetAnswerId;
                if (fieldvalidation["targetquestionid"])
                    entity.TargetQuestionID = TargetQuestionId;

            }

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateGroupId { get; set; }
        [Range(0, long.MaxValue)]
        public long? TargetQuestionId { get; set; }
        [Range(0, long.MaxValue)]
        public long? TargetAnswerId { get; set; }
        public string Expression { get; set; }
        public bool? ActionEnable { get; set; }
    }
}
