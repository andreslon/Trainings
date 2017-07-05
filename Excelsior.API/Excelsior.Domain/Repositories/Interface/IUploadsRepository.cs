using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IUploadsRepository : IEntityBaseRepository<UPLD_UploadInfo>
    {
        IQueryable<UPLD_UploadInfo> GetAll(long? seriesID, bool? isActive);
    }
}
