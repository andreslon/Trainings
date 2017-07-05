using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class EquipmentFullDto : EquipmentBaseDto
    {
        public EquipmentFullDto()
            : this(null)
        {
        }
        public EquipmentFullDto(CONTACT_Equipment entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                
            }
        }
        public override CONTACT_Equipment ToEntity(CONTACT_Equipment entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}
