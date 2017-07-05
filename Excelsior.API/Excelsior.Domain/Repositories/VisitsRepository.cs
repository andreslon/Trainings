using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class VisitsRepository : EntityBaseRepository<PACS_TimePoint>, IVisitsRepository
    {
        #region Constructor

        public VisitsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_TimePoint> GetAll(long? subjectId)
        {
            var entities = GetAll();

            if (subjectId.HasValue)
            {
                entities = entities.Where(x =>
                    x.SubjectID == subjectId);
            }

            return entities;
        }

        #endregion
    }
}
