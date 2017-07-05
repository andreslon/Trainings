using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class UploadFullDto : UploadBaseDto
    {
        public UploadFullDto()
            : this(null)
        {
            
        }
        public UploadFullDto(UPLD_UploadInfo entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override UPLD_UploadInfo ToEntity(UPLD_UploadInfo entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}