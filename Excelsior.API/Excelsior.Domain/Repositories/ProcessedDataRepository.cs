using Excelsior.Domain.Helpers;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class ProcessedDataRepository : EntityBaseRepository<PACS_ProcessedDatum>, IProcessedDataRepository 
    {
        #region Constructor

        public ProcessedDataRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 
        
        public IQueryable<PACS_ProcessedDatum> GetAll(long id)
        {
            var query = GetAll();
            query = query.Where(x => x.RawDataID == id);
            return query;
        }

        public long GetUserId(string id)
        {
            CONTACT_User u = null;
            if (!string.IsNullOrEmpty(id))
            {
                u = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == id);
                });
            }
            return u.UserID; 
        }

        #endregion
    }
}
