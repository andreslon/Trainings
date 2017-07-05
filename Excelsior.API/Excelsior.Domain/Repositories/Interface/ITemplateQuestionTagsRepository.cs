using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ITemplateQuestionTagsRepository : IEntityBaseRepository<CRF_TemplateQuestionTag>
    {
        IQueryable<CRF_TemplateQuestionTag> GetAll(string trialQuestionTagNameS);
    }
}
