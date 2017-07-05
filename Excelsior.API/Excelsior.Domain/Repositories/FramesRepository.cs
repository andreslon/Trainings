using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class FramesRepository : EntityBaseRepository<PACS_DicomFrame>, IFramesRepository
    {
        #region Constructor

        public FramesRepository(DataModel context) : base(context)
        {
        }



        #endregion

        #region Functions

        public IQueryable<PACS_DicomFrame> GetAll(long? rawDataId)
        {
            var query = GetAll();
            if (rawDataId.HasValue)
            {
                query = query.Where(x => x.RawDataID == rawDataId.Value);
            }
            return query;
        }
        #endregion
    }
}
