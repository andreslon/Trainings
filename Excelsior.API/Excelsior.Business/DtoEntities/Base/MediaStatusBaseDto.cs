using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class MediaStatusBaseDto
    {
        public MediaStatusBaseDto()
            : this(null)
        {

        }
        public MediaStatusBaseDto(PACS_RawDataStatus entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.StatusID;
                Name = entity.StatusName;
            }
        }
        public virtual PACS_RawDataStatus ToEntity(PACS_RawDataStatus entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_RawDataStatus();
            }

            entity.StatusID = Id.GetValueOrDefault();
            entity.StatusName = Name;

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(256)]
        [Required]
        public string Name { get; set; }

        
    }
}
