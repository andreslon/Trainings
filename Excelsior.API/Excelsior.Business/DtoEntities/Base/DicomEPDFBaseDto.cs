using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class DicomEPDFBaseDto
    {
        public DicomEPDFBaseDto()
            : this(null)
        {

        }
        public DicomEPDFBaseDto(PACS_DicomEPDF  entity, object sender = null) 
        {
            if (entity != null)
            {
                
            }
        }
        public virtual PACS_DicomEPDF ToEntity(PACS_DicomEPDF entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_DicomEPDF();
            }
          
            return entity;
        }

         


    }
}
