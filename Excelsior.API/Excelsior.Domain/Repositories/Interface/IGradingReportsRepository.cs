using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public interface IReportsRepository : IEntityBaseRepository<GRD_Report>
    {
        IQueryable<GRD_Report> GetAll(long? seriesId, long? performedById, bool? isActive, bool? isPrimary, bool? isSigned);
    }
}
