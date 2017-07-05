using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAuthUserRepository : IEntityBaseRepository<Aspnet_Membership>
    {
        //IQueryable<AUTH_Client> GetAll(DateTime? startDate, DateTime? endDate, string search);
        Aspnet_Membership GetByUserName(string userName);
        IQueryable<Aspnet_Membership> GetByEmail(string email);
        void ExecuteQuery(string commandText, Dictionary<string, object> parameters);
    }
}
