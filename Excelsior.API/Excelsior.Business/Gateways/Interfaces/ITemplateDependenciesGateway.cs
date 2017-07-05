using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface ITemplateDependenciesGateway : IBaseGateway<TemplateDependencyFullDto, TemplateDependencyBaseDto, TemplateDependenciesRequestDto>
    {
        ResultInfo<IList<TemplateDependencySourceFullDto>> GetSources(long id);
        ResultInfo<TemplateDependencyFullDto> SetSources(long id, IList<TemplateDependencySourceFullDto> sources);
    }
}