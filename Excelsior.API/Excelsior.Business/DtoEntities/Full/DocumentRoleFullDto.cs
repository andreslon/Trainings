using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DocumentRoleFullDto : DocumentRoleBaseDto
    {
        public DocumentRoleFullDto()
            : this(null)
        {
        }
        public DocumentRoleFullDto(DOCU_DocumentRole entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override DOCU_DocumentRole ToEntity(DOCU_DocumentRole entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}
