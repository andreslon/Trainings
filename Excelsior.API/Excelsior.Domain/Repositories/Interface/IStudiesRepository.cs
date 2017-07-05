using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IStudiesRepository : IEntityBaseRepository<PACS_Trial>
    {
        IQueryable<PACS_Trial> GetAll(string userId, bool? isActive, bool? isLocked, string search);
        int GetTotalSubjects(PACS_Trial trial);
        int GetTotalQueriesPending(PACS_Trial entity);
        int GetTotalQueriesFlagged(PACS_Trial entity, CONTACT_User user);
        IQueryable<CERT_ImgProcedureList> GetProcedures(long id);
        CERT_ImgProcedureList AddProcedure(PACS_Trial entity, CERT_ImgProcedureList procedure);
    }
}