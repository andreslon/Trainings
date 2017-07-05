using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class SeriesFullDto : SeriesBaseDto
    {
        public SeriesFullDto()
            : this(null)
        { }
        public SeriesFullDto(WF_Sequence entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                if (!(sender is SubjectBaseDto) && entity.PACSTimePoint != null && entity.PACSTimePoint.PACSSubject != null)
                {
                    Subject = new SubjectFullDto(entity.PACSTimePoint.PACSSubject, this);
                }
            }
        }
        public override WF_Sequence ToEntity(WF_Sequence entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);

            return entity;
        }

        public SubjectFullDto Subject { get; set; }
    }
}
