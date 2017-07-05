using System;

namespace Excelsior.Domain.Repositories
{
    public interface IAuditRecordsRepository : IEntityBaseRepository<AUDIT_Record>
    {
        AUDIT_Record AddRecord(string actionName = null, long? userID = null);
        AUDIT_Record AddAuthRecord(Guid userId, string clientName, string actionName = null);
   
    }
}
