using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CertUserFullDto : CertUserBaseDto
    {
        public CertUserFullDto()
            : this(null)
        {
        }
        public CertUserFullDto(CERT_User entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override CERT_User ToEntity(CERT_User entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }

        //public List<CERT_UploadInfo> CERT_UploadInfos { get; set; }
        //public List<QRY_Query> QRY_Queries { get; set; }
        //public List<AUDIT_Record> AUDIT_Records { get; set; }
    }
}
