using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IFramesRepository : IEntityBaseRepository<PACS_DicomFrame>
    {
        IQueryable<PACS_DicomFrame> GetAll(long? rawDataId);
    }
}
