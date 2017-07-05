using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DocumentVersionFullDto : DocumentVersionBaseDto
    {
        public DocumentVersionFullDto()
            : this(null)
        {
        }
        public DocumentVersionFullDto(DOCU_DocumentVersion entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override DOCU_DocumentVersion ToEntity(DOCU_DocumentVersion entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}
