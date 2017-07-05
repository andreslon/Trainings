using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface ITemplatesGateway : IBaseGateway<TemplateFullDto, TemplateBaseDto, TemplatesRequestDto>
    {
        ResultInfo<IList<TemplateGroupFullDto>> GetGroups(long id);
        ResultInfo<TemplateFullDto> SetGroups(long id, IList<TemplateGroupFullDto> groups);
        ResultInfo<TemplateFullDto> Clone(long id, CommonRequestDto request);
    }
}