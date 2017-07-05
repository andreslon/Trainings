using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface IWorkflowTemplatesGateway : IBaseGateway<WorkflowTemplateFullDto, WorkflowTemplateBaseDto, WorkflowTemplatesRequestDto>
    {
        ResultInfo<IList<WorkflowTemplateStepFullDto>> GetSteps(long id);
        ResultInfo<WorkflowTemplateFullDto> SetSteps(long id, IList<WorkflowTemplateStepFullDto> steps);
        ResultInfo<WorkflowTemplateFullDto> Clone(long id, CommonRequestDto request);
    }
}
