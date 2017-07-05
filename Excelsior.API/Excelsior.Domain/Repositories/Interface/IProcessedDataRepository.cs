using System.Linq;
namespace Excelsior.Domain.Repositories
{
    public interface IProcessedDataRepository : IEntityBaseRepository<PACS_ProcessedDatum>
    {
        IQueryable<PACS_ProcessedDatum> GetAll(long id);
        long GetUserId(string id);
    }
}
