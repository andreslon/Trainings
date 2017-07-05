using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IStencilsGateway
    {
        ResultInfo<IList<StencilsBaseDto>> GetAll(StencilsRequestDto request);
        ResultInfo<StencilsFullDto> GetSingle(long id);
        ResultInfo<StencilsFullDto> Add(StencilsFullDto request);
        ResultInfo<StencilsFullDto> Update(StencilsFullDto request);
        ResultInfo<bool> Delete(long id);
    }
}