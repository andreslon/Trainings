using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CertUploadFullDto : CertUploadBaseDto
    {
        public CertUploadFullDto()
            : this(null)
        {
            
        }
        public CertUploadFullDto(CERT_UploadInfo entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override CERT_UploadInfo ToEntity(CERT_UploadInfo entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}