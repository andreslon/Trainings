using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface ISchedulingGateway
    {
        ResultInfo<IList<SchedulingProcedureFullDto>> GetProcedures(SchedulingProceduresRequestDto request);        
    }
}