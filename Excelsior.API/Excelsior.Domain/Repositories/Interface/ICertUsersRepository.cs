using System;
using System.Linq;

namespace Excelsior.Domain.Repositories.Interface
{

    public interface ICertUsersRepository : IEntityBaseRepository<CERT_User>
    {
        IQueryable<CERT_User> GetAll(CONTACT_User u, long? trialId, long? affiliationId, long? technicianId, long? procedureId, bool? isActive, bool? isCertified, bool? hasPrevCert, string assignedTo, string search, string sort);
        IQueryable<CERT_User> GetPrevCertifications(CERT_User entity, CONTACT_User user, string search = null, string sort = null);
        int GetTotalUploads(long? certUserId);
        int GetTotalQueriesPending(CERT_User entity);
        int GetTotalQueriesFlagged(CERT_User entity, CONTACT_User user);
    }
}
