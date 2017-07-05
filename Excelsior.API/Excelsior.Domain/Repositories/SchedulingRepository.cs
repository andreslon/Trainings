using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class SchedulingRepository : ISchedulingRepository
    {
        public DataModel Context { get; set; }

        public SchedulingRepository(DataModel context)
        {
            Context = context;
        }

        public IQueryable<CERT_ImgProcedureList> GetProcedures(long trialId, bool? scheduled, string search)
        {
            var query = Context.CERT_ImgProcedureLists.Where(x => x.PACS_Trials.Any(y => y.TrialID == trialId));

            if (scheduled.HasValue)
            {
                if (scheduled.Value)
                {
                    query = query.Where(x => x.PACS_TPProcLists.Any(y => y.PACSTimePointsList.TrialID == trialId));
                }
                else
                {
                    query = query.Where(x => !x.PACS_TPProcLists.Any(y => y.PACSTimePointsList.TrialID == trialId));
                }
            }

            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.ImgProcedureName.Contains(search) 
                    || x.ImgProcedureDescription.Contains(search) 
                    || x.PACSDataType.DataType.Contains(search));
            }

            return query;
        }

        public IQueryable<PACS_TimePointsList> GetTimePointsList(long trialId)
        {
            var query = Context.PACS_TimePointsLists.Where(x => x.TrialID == trialId).OrderBy(x => x.TimePointsSeq);

            return query;
        }
    }
}