using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public interface IRolesRepository : IEntityBaseRepository<Aspnet_Role>
    {
        IQueryable<Aspnet_Role> GetAll(string search);
    }
}
