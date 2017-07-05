using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IUsersRepository : IEntityBaseRepository<CONTACT_User>
    {
        IQueryable<CONTACT_User> GetAll(bool? isActive, string search);
    }
}
