using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class TimePointsRepository : EntityBaseRepository<PACS_TimePointsList>, ITimePointsRepository
    {
        #region Constructor

        public TimePointsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_TimePointsList> GetAll(long? trialId, string search)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.TimePointsDescription.Contains(search));
            }

            if (trialId.HasValue)
            {
                query = query.Where(x => x.TrialID == trialId);
            }

            return query;
        }

        #endregion
    }
}
