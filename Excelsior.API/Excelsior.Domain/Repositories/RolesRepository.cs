using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class RolesRepository : EntityBaseRepository<Aspnet_Role>, IRolesRepository
    {
        #region Constructor

        public RolesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<Aspnet_Role> GetAll(string search)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.RoleName.Contains(search));
            }

            return query;
        }

        #endregion
    }
}
