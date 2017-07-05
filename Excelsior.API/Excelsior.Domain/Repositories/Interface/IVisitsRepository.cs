using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IVisitsRepository : IEntityBaseRepository<PACS_TimePoint>
    {
        IQueryable<PACS_TimePoint> GetAll(long? subjectId);
    }
}
