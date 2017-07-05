using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{

    public class StudyUserFullDto : StudyUserBaseDto
    {
        public StudyUserFullDto()
            : this(null)
        {
        }
        public StudyUserFullDto(CONTACT_UserTrial entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is StudyBaseDto) && entity.PACSTrial != null)
                {
                    Study = new StudyFullDto(entity.PACSTrial);
                }
            }
        }
        public override CONTACT_UserTrial ToEntity(CONTACT_UserTrial entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
        public StudyFullDto Study { get; set; }
    }
}
