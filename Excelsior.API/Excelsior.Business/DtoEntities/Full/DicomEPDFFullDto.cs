using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DicomEPDFFullDto : DicomEPDFBaseDto
    {
        public DicomEPDFFullDto()
            : this(null)
        {

        }
        public DicomEPDFFullDto(PACS_DicomEPDF entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override PACS_DicomEPDF ToEntity(PACS_DicomEPDF entity = null)
        { 
            entity = base.ToEntity(entity);
            return entity;
        }
    }
}