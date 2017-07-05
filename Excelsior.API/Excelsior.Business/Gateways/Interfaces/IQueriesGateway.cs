using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;

namespace Excelsior.Business.Gateways
{
    public interface IQueriesGateway : IBaseGateway<QueryFullDto, QueryBaseDto, QueriesRequestDto>
    {
        ResultInfo<QueryFullDto> Resolve(long id, string password = null, string reason = null);
    }    
}
