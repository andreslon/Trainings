using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAuthScopeRepository : IEntityBaseRepository<AUTH_Scope>
    { 
        int GetTotalCertifiedUsersByProcedure(long trialID, long userID, long procedureID);
        List<CERT_User> GetCertUsers(bool includeInactive, long? trialID = null);
    }
}
