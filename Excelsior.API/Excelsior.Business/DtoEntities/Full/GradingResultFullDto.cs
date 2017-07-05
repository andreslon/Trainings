using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingResultFullDto : GradingResultBaseDto
    {
        public GradingResultFullDto()
            : this(null)
        {
        }
        public GradingResultFullDto(GRD_ReportResult entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {

            }
        }
        public override GRD_ReportResult ToEntity(GRD_ReportResult entity = null, string fields = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }
    }
}
