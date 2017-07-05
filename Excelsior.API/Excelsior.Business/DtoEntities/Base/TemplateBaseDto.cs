using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateBaseDto
    {
        public TemplateBaseDto()
            : this(null)
        {

        }
        public TemplateBaseDto(CRF_Template entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFTemplateID;
                StudyId = entity.TrialID;
                Name = entity.TemplateName;
                Description = entity.TemplateDes;
                Abbrev = entity.TemplateAbbrev;
                Version = entity.TemplateVersion;
                AssocProtocol = entity.AssocProtocol;
                IsActive = entity.IsActive;
                IsLocked = entity.IsLocked;
            }
        }
        public virtual CRF_Template ToEntity(CRF_Template entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_Template();
            }

            entity.CRFTemplateID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["name"])
                    entity.TemplateName = Name;
                if (fieldvalidation["description"])
                    entity.TemplateDes = Description;
                if (fieldvalidation["abbrev"])
                    entity.TemplateAbbrev = Abbrev;
                if (fieldvalidation["version"])
                    entity.TemplateVersion = Version;
                if (fieldvalidation["assocprotocol"])
                    entity.AssocProtocol = AssocProtocol;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["islocked"])
                    entity.IsLocked = IsLocked.GetValueOrDefault();

            }


            return entity;
        }

        public long Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(8)]
        public string Abbrev { get; set; }
        [StringLength(50)]
        public string Version { get; set; }
        public string AssocProtocol { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
    }
}
