using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IDocumentRolesRepository : IEntityBaseRepository<DOCU_DocumentRole>
    {
        IQueryable<DOCU_DocumentRole> GetAll(long? documentId, string search);
    }
}
