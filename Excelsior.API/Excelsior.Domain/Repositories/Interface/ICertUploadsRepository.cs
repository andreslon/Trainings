using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ICertUploadsRepository : IEntityBaseRepository<CERT_UploadInfo>
    {
        IQueryable<CERT_UploadInfo> GetAll(long? CertUserId, long? CertEquipmentId, bool? isCertified, bool? isActive);
    }
}