using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class SubjectGroupFullDto : SubjectGroupBaseDto
    {
        public SubjectGroupFullDto()
            : this(null)
        {
        }
        public SubjectGroupFullDto(PACS_SubjectGroup entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override PACS_SubjectGroup ToEntity(PACS_SubjectGroup entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}
