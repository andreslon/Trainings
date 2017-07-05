using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateAnswerBaseDto
    {
        public TemplateAnswerBaseDto()
            : this(null)
        {

        }
        public TemplateAnswerBaseDto(CRF_TemplateAnswer entity, object sender = null)
        {
            if (entity != null)
            {
                Code = entity.AltAnswerString;
                Id = entity.CRFTemplateAnswerID;
                Index = entity.AnswerSeq;
                Value = entity.AnswerString;
                IsActive = entity.IsActive;
                TemplateQuestionId = entity.CRFTemplateQuestionID;
            }
        }
        public virtual CRF_TemplateAnswer ToEntity(CRF_TemplateAnswer entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_TemplateAnswer();
            }
            entity.CRFTemplateAnswerID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["code"])
                    entity.AltAnswerString = Code;
                if (fieldvalidation["index"])
                    entity.AnswerSeq = Index;
                if (fieldvalidation["value"])
                    entity.AnswerString = Value;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["templatequestionid"])
                    entity.CRFTemplateQuestionID = TemplateQuestionId;

            }


            return entity;
        }

        public long Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateQuestionId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        [Range(0, int.MaxValue)]
        public int Index { get; set; }
        public bool? IsActive { get; set; }
    }
}
