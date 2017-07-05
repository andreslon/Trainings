using Excelsior.Business.DtoEntities;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface IBaseGateway<FullDto, BaseDto, RequestDto>
        where FullDto : class, new()
        where BaseDto : class, new()
        where RequestDto : class, new()
    {
        ResultInfo<IList<BaseDto>> GetAll(RequestDto request);
        ResultInfo<FullDto> GetSingle(long id);
        ResultInfo<FullDto> Add(FullDto request);
        ResultInfo<FullDto> Update(FullDto request, string fields = null, string password = null, string reason = null);
        ResultInfo<bool> Delete(long id);
    }
}
