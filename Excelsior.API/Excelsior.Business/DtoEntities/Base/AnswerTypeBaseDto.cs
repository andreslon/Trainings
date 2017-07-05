using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AnswerTypeBaseDto
    {
        public AnswerTypeBaseDto()
            : this(null)
        {
        }
        public AnswerTypeBaseDto(CRF_AnswerType entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFAnswerTypeID;
                Name = entity.AnswerTypeName;
                IsActive = entity.IsActive;
            }
        }
        public virtual CRF_AnswerType ToEntity(CRF_AnswerType entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new CRF_AnswerType();
            }
            entity.CRFAnswerTypeID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            { 
                if (fieldvalidation["name"])
                    entity.AnswerTypeName = Name;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
            }
                   
            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}