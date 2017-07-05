using System;
using System.Linq;

namespace Excelsior.Domain.Repositories.Interface
{
    public interface ICertEquipmentRepository : IEntityBaseRepository<CERT_Equipment>
    {
        IQueryable<CERT_Equipment> GetAll(CONTACT_User u, long? trialId, long? affiliationId, long? equipmentId, long? procedureId, bool? isActive, bool? isCertified, bool? hasPrevCert, string assignedTo, string search, string sort);
        IQueryable<CERT_Equipment> GetPrevCertifications(CERT_Equipment entity, CONTACT_User user, string search = null, string sort = null);
        int GetTotalUploads(long? certEquipmentId);
        int GetTotalQueriesPending(CERT_Equipment entity);
        int GetTotalQueriesFlagged(CERT_Equipment entity, CONTACT_User user);
    }
}