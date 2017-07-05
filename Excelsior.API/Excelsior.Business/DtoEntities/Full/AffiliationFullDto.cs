using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AffiliationFullDto : AffiliationBaseDto
    {
        public AffiliationFullDto()
            : this(null)
        {
        }
        public AffiliationFullDto(CONTACT_Affiliation entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override CONTACT_Affiliation ToEntity(CONTACT_Affiliation entity = null, string fields= null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        } 
    }
}
