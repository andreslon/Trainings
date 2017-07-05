using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class AuthClientRepository : EntityBaseRepository<AUTH_Client>, IAuthClientRepository
    {
        #region Constructor
        public AuthClientRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions

        public IQueryable<AUTH_Client> FindClient(string clientId )
        {
            var clientIddG = Guid.Parse(clientId);
            var clients = FindBy(x => x.ClientId == clientIddG); 
            return clients.Select(x => x);

        } 
        #endregion
    }
}
