using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AffiliationBaseDto
    {
        public AffiliationBaseDto()
            : this(null)
        {

        }
        public AffiliationBaseDto(CONTACT_Affiliation entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.AffiliationID;
                Name = entity.AffiliationName;
                IsActive = entity.IsActive;
                CountryId = entity.CountryID;

                if (!(sender is CountryBaseDto) && entity.CONTACTCountry != null)
                {
                    Country = new CountryFullDto(entity.CONTACTCountry, this);
                }
            }
        }
        public virtual CONTACT_Affiliation ToEntity(CONTACT_Affiliation entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CONTACT_Affiliation();
            } 
            entity.AffiliationID = Id.GetValueOrDefault();

            using (var fieldvalidation = new FieldValidation(fields)) {
                if (fieldvalidation["name"])
                    entity.AffiliationName = Name;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["countryid"])
                    entity.CountryID = CountryId;
            }
             
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? CountryId { get; set; }
        public CountryFullDto Country { get; set; }
    }
}
