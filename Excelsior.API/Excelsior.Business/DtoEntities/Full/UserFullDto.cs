using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Full
{
    public class UserFullDto : UserBaseDto
    {
        public UserFullDto()
            : this(null)
        {
        }
        public UserFullDto(CONTACT_User entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                City = entity.City;
                Address1 = entity.Address1;
                Address2 = entity.Address2;
                StateProvince = entity.StateProvince;
                ZipCode = entity.ZipCode;
                PhoneNumber = entity.PhoneNumber;
                CountryId = entity.CountryID;
                if (!(sender is CountryBaseDto) && entity.CONTACTCountry != null)
                {
                    Country = new CountryFullDto(entity.CONTACTCountry, this);
                }
            }
        }
        public override CONTACT_User ToEntity(CONTACT_User entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["city"])
                    entity.City = City;
                if (fieldvalidation["address1"])
                    entity.Address1 = Address1;
                if (fieldvalidation["address2"])
                    entity.Address2 = Address2;
                if (fieldvalidation["stateprovince"])
                    entity.StateProvince = StateProvince;
                if (fieldvalidation["zipcode"])
                    entity.ZipCode = ZipCode;
                if (fieldvalidation["phonenumber"])
                    entity.PhoneNumber = PhoneNumber;
                if (fieldvalidation["countryid"])
                    entity.CountryID = CountryId;
            }
            return entity;
        }

        public CountryFullDto Country { get; set; }
        [StringLength(200)]
        public string City { get; set; }
        [StringLength(200)]
        public string Address1 { get; set; }
        [StringLength(200)]
        public string Address2 { get; set; }
        [StringLength(200)]
        public string StateProvince { get; set; }
        [StringLength(200)]
        public string ZipCode { get; set; }
        [StringLength(200)]
        public string PhoneNumber { get; set; }
        [Range(0, long.MaxValue)]
        public long? CountryId { get; set; }
    }
}
