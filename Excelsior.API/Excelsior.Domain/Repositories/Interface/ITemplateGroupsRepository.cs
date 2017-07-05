using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ITemplateGroupsRepository : IEntityBaseRepository<CRF_TemplateGroup>
    {
        IQueryable<CRF_TemplateGroup> GetAll(string search);
        IQueryable<CRF_TemplateQuestion> GetQuestions(long id);
        CRF_TemplateQuestion AddQuestion(CRF_TemplateGroup entity, CRF_TemplateQuestion question);
        IQueryable<CRF_TemplateDependency> GetDependencies(long id);
        CRF_TemplateDependency AddDependency(CRF_TemplateGroup entity, CRF_TemplateDependency dependency);
        CRF_TemplateDependencySource AddDependencySource(CRF_TemplateDependency entity, CRF_TemplateDependencySource source);
    }
}