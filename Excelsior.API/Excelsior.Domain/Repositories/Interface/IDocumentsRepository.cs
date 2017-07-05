using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IDocumentsRepository : IEntityBaseRepository<DOCU_Document>
    {
        IQueryable<DOCU_Document> GetAll(long? studyId, string userId, bool? isActive, string search);
    }
}
