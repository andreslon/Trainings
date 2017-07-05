using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAttachmentsRepository : IEntityBaseRepository<PACS_SeriesAttachment>
    {
        IQueryable<PACS_SeriesAttachment> GetAll(long? seriesId, string userId, string laterality, bool? isActive, string search);
        long GetUserId(string userId);

    }
}
