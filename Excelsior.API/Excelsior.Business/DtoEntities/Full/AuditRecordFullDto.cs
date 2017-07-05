using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuditRecordFullDto : AuditRecordBaseDto
    {
        public AuditRecordFullDto()
            : this(null)
        {
        }
        public AuditRecordFullDto(AUDIT_Record entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override AUDIT_Record ToEntity(AUDIT_Record entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
} 
