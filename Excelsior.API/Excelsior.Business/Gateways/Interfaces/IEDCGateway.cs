using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;

namespace Excelsior.Business.Gateways
{
    public interface IEDCGateway
    {
        ResultInfo<GradingTemplateFullDto> GetGradingTemplateForProcedure(long procedureId, long timePointId, bool isHierarchical);

        ResultInfo<string> GetFrameLocation(long subjectId, long timePointListId, long procedureId);
    }
}