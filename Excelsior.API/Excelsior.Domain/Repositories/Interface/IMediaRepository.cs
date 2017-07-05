using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IMediaRepository : IEntityBaseRepository<PACS_RawDatum>
    {
        IQueryable<PACS_RawDatum> GetAll(long? seriesId, long? certUserId, long? certEquipmentId, string dataType, bool? isActive, IList<long> ids);
        string GetSegmentationStatus(long rawDataId);
    }
}
