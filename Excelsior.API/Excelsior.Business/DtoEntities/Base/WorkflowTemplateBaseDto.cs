using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class WorkflowTemplateBaseDto
    {
        public WorkflowTemplateBaseDto()
            : this(null)
        {
        }
        public WorkflowTemplateBaseDto(WF_Template entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.WFTemplateID;
                Name = entity.WFTemplateName;
                Type = entity.WFTemplateType;
                Note = entity.WFTemplateNote;
                IsLocked = entity.IsLocked;
                IsActive = entity.IsActive;
                StudyId = entity.TrialID;
            }
        }


        public virtual WF_Template ToEntity(WF_Template entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new WF_Template();
            }

            entity.WFTemplateID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.WFTemplateName = Name;
                if (fieldvalidation["type"])
                    entity.WFTemplateType = Type;
                if (fieldvalidation["note"])
                    entity.WFTemplateNote = Note;
                if (fieldvalidation["islocked"])
                    entity.IsLocked = IsLocked;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive;
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;

            }


            return entity;
        }


        public long? Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(256)]
        public string Note { get; set; }
        public bool IsLocked { get; set; }
        public bool IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }

    }
}
