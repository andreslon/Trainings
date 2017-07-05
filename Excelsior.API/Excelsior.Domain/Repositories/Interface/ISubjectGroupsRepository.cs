using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISubjectGroupsRepository : IEntityBaseRepository<PACS_SubjectGroup>
    {
        IQueryable<PACS_SubjectGroup> GetAll(string search);
    }
}
