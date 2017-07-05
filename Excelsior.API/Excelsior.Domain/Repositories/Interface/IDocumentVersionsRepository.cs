using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IDocumentVersionsRepository : IEntityBaseRepository<DOCU_DocumentVersion>
    {
        IQueryable<DOCU_DocumentVersion> GetAll(long? studyId, long? documentId, bool? isActive);
    }
}
