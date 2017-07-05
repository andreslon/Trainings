using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateGroupBaseDto
    {
        public TemplateGroupBaseDto()
             : this(null)
        {
        }
        public TemplateGroupBaseDto(CRF_TemplateGroup entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFTemplateGroupID;
                Name = entity.GroupName;
                Index = entity.GroupSeq;
                TemplateId = entity.CRFTemplateID;
            }
        }
        public virtual CRF_TemplateGroup ToEntity(CRF_TemplateGroup entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CRF_TemplateGroup();
            }

            entity.CRFTemplateGroupID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.GroupName = Name;
                if (fieldvalidation["index"])
                    entity.GroupSeq = Index;
                if (fieldvalidation["templateid"])
                    entity.CRFTemplateID = TemplateId;

            }


            return entity;
        }

        public long? Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public int Index { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateId { get; set; }
    }
}
