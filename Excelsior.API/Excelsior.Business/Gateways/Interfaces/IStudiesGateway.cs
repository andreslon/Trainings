using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface IStudiesGateway : IBaseGateway<StudyFullDto, StudyBaseDto, StudiesRequestDto>
    {
        ResultInfo<StudyFullDto> SetIsLocked(long id, bool isLocked, string password, string reason);
        ResultInfo<IList<ProcedureFullDto>> GetProcedures(long id);
        ResultInfo<IList<ProcedureFullDto>> SetProcedures(long id, IList<ProcedureFullDto> tags);
        ResultInfo<IList<ProcedureFullDto>> AddProcedures(long id, IList<ProcedureFullDto> tags);
        ResultInfo<IList<ProcedureFullDto>> RemoveProcedures(long id, IList<ProcedureFullDto> tags);
    }
}