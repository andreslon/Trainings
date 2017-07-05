using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DicomWSIFullDto : DicomWSIBaseDto
    {
        public DicomWSIFullDto()
            : this(null)
        {

        }
        public DicomWSIFullDto(PACS_DicomWSI entity, object sender = null)
            : base(entity, sender) 
        {
        }
        public override PACS_DicomWSI ToEntity(PACS_DicomWSI entity = null)
        {
            entity = base.ToEntity(entity);
            return entity;
        }
    }
}