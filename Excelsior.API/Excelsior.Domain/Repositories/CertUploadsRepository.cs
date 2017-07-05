using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class CertUploadsRepository : EntityBaseRepository<CERT_UploadInfo>, ICertUploadsRepository
    {
        #region Constructor

        public CertUploadsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<CERT_UploadInfo> GetAll(long? CertUserId, long? CertEquipmentId, bool? isCertified, bool? isActive)
        {
            var uploads = GetAll();
            if (CertUserId.HasValue)
            {
                uploads = uploads.Where(item => item.CertUserID == CertUserId);
            }
            if (CertEquipmentId.HasValue)
            {
                uploads = uploads.Where(item => item.CertEquipmentID == CertEquipmentId);
            }
            if (isCertified.HasValue)
            {
                uploads = uploads.Where(x => x.IsCertified == isCertified);
            }
            if (isActive.HasValue)
            {
                uploads = uploads.Where(x => x.IsActive == isActive);
            }

            return uploads;
        }

        public override void Delete(CERT_UploadInfo entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CERT_UploadInfo, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}