using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class EquipmentModelFullDto : EquipmentModelBaseDto
    {
        public EquipmentModelFullDto()
            : this(null)
        {
        }
        public EquipmentModelFullDto(CONTACT_EquipmentModel entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override CONTACT_EquipmentModel ToEntity(CONTACT_EquipmentModel entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}
