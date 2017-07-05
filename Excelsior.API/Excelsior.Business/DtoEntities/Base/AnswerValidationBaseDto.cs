using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AnswerValidationBaseDto
    {
        public AnswerValidationBaseDto()
            : this(null)
        {
        }
        public AnswerValidationBaseDto(CRF_AnswerValidation entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFAnswerValidationID;
                StudyId = entity.TrialID;
                Name = entity.Name;
                IsActive = entity.IsActive;
            }
        }
        public virtual CRF_AnswerValidation ToEntity(CRF_AnswerValidation entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_AnswerValidation();
            }
            entity.CRFAnswerValidationID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["name"])
                    entity.Name = Name;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
            }
                    

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}