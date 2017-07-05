using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CertEquipmentFullDto : CertEquipmentBaseDto
    {
        public CertEquipmentFullDto()
            : this(null)
        {
        }
        public CertEquipmentFullDto(CERT_Equipment entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is StudyBaseDto) && entity.PACSTrial != null)
                {
                    Study = new StudyFullDto(entity.PACSTrial, this);
                }
            }
        }
        public override CERT_Equipment ToEntity(CERT_Equipment entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }

        public StudyFullDto Study { get; set; }
    }
}
