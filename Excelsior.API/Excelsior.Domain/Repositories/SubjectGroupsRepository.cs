using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class SubjectGroupsRepository : EntityBaseRepository<PACS_SubjectGroup>, ISubjectGroupsRepository
    {
        #region Constructor

        public SubjectGroupsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_SubjectGroup> GetAll(string search)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.GroupName.Contains(search)
                    || x.GroupDescription.Contains(search));
            }

            return query;
        }

        #endregion
    }
}
