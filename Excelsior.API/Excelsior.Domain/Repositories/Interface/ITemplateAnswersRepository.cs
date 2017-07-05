using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ITemplateAnswersRepository : IEntityBaseRepository<CRF_TemplateAnswer>
    {
        IQueryable<CRF_TemplateAnswer> GetAll(long? questionId, string search);
    }
}
