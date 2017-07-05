using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class SubjectCohortsRepository : EntityBaseRepository<PACS_SubjectCohort>, ISubjectCohortsRepository
    {
        #region Constructor

        public SubjectCohortsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_SubjectCohort> GetAll(string search)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.CohortName.Contains(search));
            }

            return query;
        }

        #endregion
    }
}
