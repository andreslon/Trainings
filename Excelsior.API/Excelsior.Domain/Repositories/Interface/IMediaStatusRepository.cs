using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IMediaStatusRepository
    {
        IQueryable<PACS_RawDataStatus> GetAll(string search);
    }
}
