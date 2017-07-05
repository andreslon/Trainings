using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IQueryMessagesRepository : IEntityBaseRepository<QRY_Message>
    {
        IQueryable<QRY_Message> GetAll(long queryId, bool? isActive, string search);
    }
}
