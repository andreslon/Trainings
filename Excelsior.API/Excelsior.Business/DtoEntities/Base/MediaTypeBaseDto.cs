using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class MediaTypeBaseDto
    {
        public MediaTypeBaseDto()
            : this(null)
        {
        }
        public MediaTypeBaseDto(PACS_DataType entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.DataTypeID;
                Name = entity.DataType;
            }
        }
        public virtual PACS_DataType ToEntity(PACS_DataType entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_DataType();
            }

            entity.DataTypeID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.DataType = Name; 
            }
            

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
