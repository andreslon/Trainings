using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAspUserRepository : IEntityBaseRepository<Aspnet_User>
    {
        Aspnet_Application GetCurrentApplication();
    }
}
