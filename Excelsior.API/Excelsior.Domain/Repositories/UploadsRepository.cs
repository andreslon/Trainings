using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class UploadsRepository : EntityBaseRepository<UPLD_UploadInfo>, IUploadsRepository
    {
        #region Constructor

        public UploadsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<UPLD_UploadInfo> GetAll(long? seriesID, bool? isActive)
        {
            var uploads = GetAll();
            if (seriesID.HasValue)
            {
                uploads = uploads.Where(item => item.SeriesID == seriesID);
            }
            if (isActive.HasValue)
            {
                uploads = uploads.Where(x => x.IsActive == isActive);
            }

            return uploads;
        }

        public override void Delete(UPLD_UploadInfo entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<UPLD_UploadInfo, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}