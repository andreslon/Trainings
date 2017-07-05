using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AuditActionFullDto : AuditActionBaseDto
    {
        public AuditActionFullDto()
            : this(null)
        {
        }
        public AuditActionFullDto(AUDIT_Action entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
            }
        }
        public override AUDIT_Action ToEntity(AUDIT_Action entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
} 
