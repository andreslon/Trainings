using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CountryBaseDto
    {
        public CountryBaseDto()
            : this(null)
        {

        }
        public CountryBaseDto(CONTACT_Country entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CountryID;
                Name = entity.CountryName;
            }
        }
        public virtual CONTACT_Country ToEntity(CONTACT_Country entity = null)
        {
            if (entity == null)
            {
                entity = new CONTACT_Country();
            }

            entity.CountryID = Id.GetValueOrDefault();
            entity.CountryName = Name;

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
