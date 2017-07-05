using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{

    public class StudyReportFullDto : StudyReportBaseDto
    {
        public StudyReportFullDto()
            : this(null)
        {
        }
        public StudyReportFullDto(RPT_TrialReport entity, object sender = null)
            : base(entity, sender)
        {
        }
        public override RPT_TrialReport ToEntity(RPT_TrialReport entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            return entity;
        }
    }
}
