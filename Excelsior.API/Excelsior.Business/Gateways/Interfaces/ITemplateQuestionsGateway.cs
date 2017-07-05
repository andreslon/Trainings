using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface ITemplateQuestionsGateway : IBaseGateway<TemplateQuestionFullDto, TemplateQuestionBaseDto, TemplateQuestionsRequestDto>
    {
        ResultInfo<IList<TemplateAnswerFullDto>> GetAnswers(long id);
        ResultInfo<TemplateQuestionFullDto> SetAnswers(long id, IList<TemplateAnswerFullDto> answers);
        ResultInfo<IList<TemplateQuestionTagFullDto>> GetTags(long id);
        ResultInfo<TemplateQuestionFullDto> SetTags(long id, IList<TemplateQuestionTagFullDto> tags);
        ResultInfo<TemplateQuestionFullDto> SetValidation(long id, AnswerValidationFullDto validation);
        ResultInfo<TemplateQuestionFullDto> Clone(long id, CommonRequestDto request);
    }
}