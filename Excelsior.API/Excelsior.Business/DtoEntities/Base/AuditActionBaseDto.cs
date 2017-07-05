using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuditActionBaseDto
    {
        public AuditActionBaseDto()
            : this(null)
        {
        }
        public AuditActionBaseDto(AUDIT_Action entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.AuditActionID;
                Name = entity.AuditActionName;
                Description = entity.AuditActionDes;
            }
        }
        public virtual AUDIT_Action ToEntity(AUDIT_Action entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new AUDIT_Action();
            }
            entity.AuditActionID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            { 
                if (fieldvalidation["name"])
                    entity.AuditActionName = Name;
                if (fieldvalidation["description"])
                    entity.AuditActionDes = Description;
            }
                   
            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}