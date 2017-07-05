using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class DocumentFullDto : DocumentBaseDto
    {
        public DocumentFullDto()
            : this(null)
        {
        }
        public DocumentFullDto(DOCU_Document entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override DOCU_Document ToEntity(DOCU_Document entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }
    }
}
