using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAnswerTypesRepository : IEntityBaseRepository<CRF_AnswerType>
    {
        IQueryable<CRF_AnswerType> GetAll(string search);
    }
}
