using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;
using System;

namespace Excelsior.Business.Gateways
{
    public interface IProcessedDataGateway 
    {
        ResultInfo<IList<ProcessedDataBaseDto>> GetAll(ProcessedDataRequestDto request);
        ResultInfo<ProcessedDataFullDto> Add(ProcessedDataFullDto request, string currentUserId);


    }
}