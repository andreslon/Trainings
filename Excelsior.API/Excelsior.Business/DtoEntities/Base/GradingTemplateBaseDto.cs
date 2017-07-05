using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingTemplateBaseDto
    {
        public GradingTemplateBaseDto()
            : this(null)
        {
        }
        public GradingTemplateBaseDto(GRD_GradingTemplate entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GTemplateID;
                Name = entity.GTemplateName;
                Description = entity.GTemplateDes;

                IsActive = entity.IsActive;
                IsLocked = entity.IsLocked;
            }
        }
        public virtual GRD_GradingTemplate ToEntity(GRD_GradingTemplate entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new GRD_GradingTemplate();
            }

            entity.GTemplateID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.GTemplateName = Name;
                if (fieldvalidation["description"])
                    entity.GTemplateDes = Description;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["islocked"])
                    entity.IsLocked = IsLocked.GetValueOrDefault(); 
            } 

            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
    }
}
