using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface IVisitMatrixGateway
    {
        ResultInfo<IList<VisitMatrixSubjectFullDto>> GetSubjects(VisitMatrixSubjectsRequestDto request);

        ResultInfo<IList<VisitMatrixProcedureFullDto>> GetProcedures(VisitMatrixProceduresRequestDto request);

        ResultInfo<IList<TimePointFullDto>> GetPendingTimePoints(VisitMatrixProceduresRequestDto request);
    }
}