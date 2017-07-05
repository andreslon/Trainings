namespace Excelsior.Domain.Repositories
{
    public class MediaTypesRepository : EntityBaseRepository<PACS_DataType>, IMediaTypesRepository
    {
        #region Constructor

        public MediaTypesRepository(DataModel context) : base(context)
        {
        }

        #endregion
    }
}
