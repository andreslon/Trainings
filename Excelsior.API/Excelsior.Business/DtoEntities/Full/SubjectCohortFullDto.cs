using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class SubjectCohortFullDto : SubjectCohortBaseDto
    {
        public SubjectCohortFullDto()
            : this(null)
        {
        }
        public SubjectCohortFullDto(PACS_SubjectCohort entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override PACS_SubjectCohort ToEntity(PACS_SubjectCohort entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}
