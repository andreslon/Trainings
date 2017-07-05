using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateQuestionTagBaseDto
    {
        public TemplateQuestionTagBaseDto()
            : this(null)
        {
        }
        public TemplateQuestionTagBaseDto(CRF_TemplateQuestionTag entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFTemplateQuestionTagID;
                Name = entity.QuestionTagName;
            }
        }
        public virtual CRF_TemplateQuestionTag ToEntity(CRF_TemplateQuestionTag entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_TemplateQuestionTag();
            }

            entity.CRFTemplateQuestionTagID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.QuestionTagName = Name; 
            }
           

            return entity;
        }


        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}