using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ITemplateQuestionsRepository : IEntityBaseRepository<CRF_TemplateQuestion>
    {
        IQueryable<CRF_TemplateQuestion> GetAll(long? templateId, bool? isActive, string search);
        IQueryable<CRF_TemplateAnswer> GetAnswers(long id);
        CRF_TemplateAnswer AddAnswer(CRF_TemplateQuestion entity, CRF_TemplateAnswer answer);
        IQueryable<CRF_TemplateQuestionTag> GetTags(long id);
        CRF_TemplateQuestionTag AddTag(CRF_TemplateQuestion entity, CRF_TemplateQuestionTag tag);
        CRF_AnswerValidation SetValidation(CRF_TemplateQuestion entity, CRF_AnswerValidation validation);
        CRF_TemplateQuestion Clone(CRF_TemplateQuestion entity, long? groupId);
    }
}