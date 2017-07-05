using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{

    public interface ITemplateGroupsGateway : IBaseGateway<TemplateGroupFullDto, TemplateGroupBaseDto, TemplateGroupsRequestDto>
    {
        ResultInfo<IList<TemplateQuestionFullDto>> GetQuestions(long id);
        ResultInfo<TemplateGroupFullDto> SetQuestions(long id, IList<TemplateQuestionFullDto> questions);
        ResultInfo<IList<TemplateDependencyFullDto>> GetDependencies(long id);
        ResultInfo<TemplateGroupFullDto> SetDependencies(long id, IList<TemplateDependencyFullDto> dependencies);
    }
}
