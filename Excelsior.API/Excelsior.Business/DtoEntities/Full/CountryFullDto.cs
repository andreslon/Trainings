using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CountryFullDto : CountryBaseDto
    {
        public CountryFullDto()
            : this(null)
        {

        }
        public CountryFullDto(CONTACT_Country entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override CONTACT_Country ToEntity(CONTACT_Country entity = null)
        {
            entity = base.ToEntity(entity);
            return entity;
        } 
    }
}