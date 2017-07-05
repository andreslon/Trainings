using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public interface ITimePointsRepository : IEntityBaseRepository<PACS_TimePointsList>
    {
        IQueryable<PACS_TimePointsList> GetAll(long? trialId, string search);
    }
}
