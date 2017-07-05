using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISubjectsRepository : IEntityBaseRepository<PACS_Subject>
    {
        IQueryable<PACS_Subject> GetAll(string userId, long? trialId, long? siteId, long? affiliationId, long? groupId, long? cohortId, bool? isActive, bool? isRejected, string search, string sort);
    }
}
