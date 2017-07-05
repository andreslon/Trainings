using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISitesRepository : IEntityBaseRepository<PACS_Site>
    {
        IQueryable<PACS_Site> GetAll(CONTACT_User u, long? trialId, bool? isActive, string search, string sort);
        int GetTotalSubjects(PACS_Site entity);
        int GetTotalQueriesPending(PACS_Site entity);
        int GetTotalQueriesFlagged(PACS_Site entity, CONTACT_User user);
    }
}
