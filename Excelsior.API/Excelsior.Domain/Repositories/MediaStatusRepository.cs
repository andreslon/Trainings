using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class MediaStatusRepository : EntityBaseRepository<PACS_RawDataStatus>, IMediaStatusRepository
    {
        #region Constructor

        public MediaStatusRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<PACS_RawDataStatus> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.StatusName.Contains(search));
            }
            return query;
        }

        #endregion
    }
}
