using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DicomOPFullDto : DicomOPBaseDto
    {
        public DicomOPFullDto()
            : this(null)
        {

        }
        public DicomOPFullDto(PACS_DicomOP entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override PACS_DicomOP ToEntity(PACS_DicomOP entity = null)
        { 
            entity = base.ToEntity(entity);
            return entity;
        }
    }
}