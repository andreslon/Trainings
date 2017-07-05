using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories.Interface
{ 
    public interface IStudyReportsRepository : IEntityBaseRepository<RPT_TrialReport>
    {
        IQueryable<RPT_TrialReport> GetAll(string userId, long? studyId);
    }
}
