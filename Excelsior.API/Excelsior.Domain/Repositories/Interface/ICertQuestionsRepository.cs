using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ICertQuestionsRepository : IEntityBaseRepository<CERT_QuestionList>
    {
        IQueryable<CERT_QuestionList> GetAll(long? procedureId, string search);
    }
}
