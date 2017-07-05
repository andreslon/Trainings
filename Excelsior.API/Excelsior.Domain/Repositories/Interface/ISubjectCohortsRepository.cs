using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISubjectCohortsRepository : IEntityBaseRepository<PACS_SubjectCohort>
    {
        IQueryable<PACS_SubjectCohort> GetAll(string search);
    }
}
