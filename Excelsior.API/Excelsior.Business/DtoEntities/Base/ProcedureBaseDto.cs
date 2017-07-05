using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class ProcedureBaseDto
    {
        public ProcedureBaseDto()
            : this(null)
        {
        }
        public ProcedureBaseDto(CERT_ImgProcedureList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.ImgProcedureID;
                Name = entity.ImgProcedureName;
                Description = entity.ImgProcedureDescription;
                MediaTypeId = entity.DataTypeID;
            }
        }
        public virtual CERT_ImgProcedureList ToEntity(CERT_ImgProcedureList entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CERT_ImgProcedureList();
            }

            entity.ImgProcedureID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.ImgProcedureName = Name;
                if (fieldvalidation["description"])
                    entity.ImgProcedureDescription = Description;
                if (fieldvalidation["mediatypeid"])
                    entity.DataTypeID = MediaTypeId;

            }
           

            return entity;
        }

        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaTypeId { get; set; }
    }
}
