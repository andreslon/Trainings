using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISchedulesRepository : IEntityBaseRepository<PACS_TPProcList>
    {
        IQueryable<PACS_TPProcList> GetAll(long? trialId, long? timePointId, long? procedureId, long? subjectId, string search);
    }
}
