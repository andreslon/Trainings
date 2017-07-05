using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IQueriesRepository : IEntityBaseRepository<QRY_Query>
    {
        IQueryable<QRY_Query> GetAll(CONTACT_User user, long studyId, long? siteId, long? seriesId, long? certEquipmentId, long? certUserId, string queryType, string queryStatus, bool? isActive, string search, string sort);
        int GetTotalMessages(long? queryId);
        QRY_Message GetLastMessage(long? queryId);
    }
}
