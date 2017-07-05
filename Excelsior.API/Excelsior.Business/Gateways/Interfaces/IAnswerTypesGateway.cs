using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;

namespace Excelsior.Business.Gateways
{

    public interface IAnswerTypesGateway : IBaseGateway<AnswerTypeFullDto, AnswerTypeBaseDto, AnswerTypesRequestDto>
    {
    }
    
}
