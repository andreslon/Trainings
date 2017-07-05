using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CrfDataResultFullDto : CrfDataResultBaseDto
    {
        public CrfDataResultFullDto()
            : this(null)
        {
        }
        public CrfDataResultFullDto(CRF_DataResult entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is CrfDataBaseDto) && entity.CRFData != null)
                {
                    CrfData = new CrfDataFullDto(entity.CRFData, this);
                }
            }
        }
        public override CRF_DataResult ToEntity(CRF_DataResult entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }

        public CrfDataFullDto CrfData { get; set; }
    }
}
