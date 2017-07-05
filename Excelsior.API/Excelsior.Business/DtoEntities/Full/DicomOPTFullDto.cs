using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DicomOPTFullDto : DicomOPTBaseDto
    {
        public DicomOPTFullDto()
            : this(null)
        {

        }
        public DicomOPTFullDto(PACS_DicomOPT entity, object sender = null)
            : base(entity, sender)
        {

        }
        public override PACS_DicomOPT ToEntity(PACS_DicomOPT entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
        
    }
}