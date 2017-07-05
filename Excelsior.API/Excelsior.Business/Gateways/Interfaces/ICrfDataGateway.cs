using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface ICrfDataGateway : IBaseGateway<CrfDataFullDto, CrfDataBaseDto, CrfDataRequestDto>
    {
        ResultInfo<IList<CrfDataResultFullDto>> GetResults(long id);
        ResultInfo<CrfDataFullDto> SetResults(long id, IList<CrfDataResultFullDto> results);
    }
}
