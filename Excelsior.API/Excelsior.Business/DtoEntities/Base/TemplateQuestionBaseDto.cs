using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateQuestionBaseDto
    {
        public TemplateQuestionBaseDto()
            : this(null)
        {

        }
        public TemplateQuestionBaseDto(CRF_TemplateQuestion entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFTemplateQuestionID;
                TemplateGroupId = entity.CRFTemplateGroupID;
                AnswerTypeId = entity.CRFAnswerTypeID;
                AnswerValidationId = entity.CRFAnswerValidationID;
                CDashVariable = entity.CDashVariable;
                IsActive = entity.IsActive;
                Description = entity.QuestionDes;
                IsLaterality = entity.IsLaterality;
                Index = entity.QuestionSeq;
                Text = entity.QuestionText;
                SDTMVariable = entity.SDTMVariable;

                if (!(sender is AnswerTypeBaseDto) && entity.CRFAnswerType != null)
                {
                    AnswerType = new AnswerTypeFullDto(entity.CRFAnswerType, this);
                }
                if (!(sender is AnswerValidationBaseDto) && entity.CRFAnswerValidation != null)
                {
                    AnswerValidation = new AnswerValidationFullDto(entity.CRFAnswerValidation, this);
                }
                if (!(sender is TemplateGroupBaseDto) && entity.CRFTemplateGroup != null)
                {
                    TemplateGroup = new TemplateGroupFullDto(entity.CRFTemplateGroup, this);
                }
            }
        }
        public virtual CRF_TemplateQuestion ToEntity(CRF_TemplateQuestion entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new CRF_TemplateQuestion();
            }

            entity.CRFTemplateQuestionID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["templategroupid"])
                    entity.CRFTemplateGroupID = TemplateGroupId;
                if (fieldvalidation["answertypeid"])
                    entity.CRFAnswerTypeID = AnswerTypeId;
                if (fieldvalidation["answervalidationid"])
                    entity.CRFAnswerValidationID = AnswerValidationId;
                if (fieldvalidation["text"])
                    entity.QuestionText = Text;
                if (fieldvalidation["cdashvariable"])
                    entity.CDashVariable = CDashVariable;
                if (fieldvalidation["sdtmvariable"])
                    entity.SDTMVariable = SDTMVariable;
                if (fieldvalidation["description"])
                    entity.QuestionDes = Description;
                if (fieldvalidation["islaterality"])
                    entity.IsLaterality = IsLaterality.GetValueOrDefault();
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["index"])
                    entity.QuestionSeq = Index.GetValueOrDefault(); 
            }
           

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateGroupId { get; set; }
        [Range(0, long.MaxValue)]
        public long? AnswerTypeId { get; set; }
        [Range(0, long.MaxValue)]
        public long? AnswerValidationId { get; set; }
        public string Text { get; set; }
        [StringLength(8)]
        public string CDashVariable { get; set; }
        [StringLength(8)]
        public string SDTMVariable { get; set; }
        public string Description { get; set; }
        public bool? IsLaterality { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }

        public AnswerTypeFullDto AnswerType { get; set; }
        public AnswerValidationFullDto AnswerValidation { get; set; }
        public TemplateGroupFullDto TemplateGroup { get; set; }
    }
}
