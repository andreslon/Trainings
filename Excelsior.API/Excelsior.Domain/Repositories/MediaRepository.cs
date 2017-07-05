using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class MediaRepository : EntityBaseRepository<PACS_RawDatum>, IMediaRepository
    {
        #region Constructor

        public MediaRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_RawDatum> GetAll(long? seriesId, long? certUserId, long? certEquipmentId, string dataType, bool? isActive, IList<long> ids)
        {
            var rawdata = GetAll();
            if (ids != null)
            {
                rawdata = rawdata.Where(item => ids.Contains(item.RawDataID));
            }
            if (seriesId.HasValue)
            {
                rawdata = rawdata.Where(item => item.SeriesID == seriesId);
            }
            if (certUserId.HasValue)
            {
                rawdata = rawdata.Where(item => item.CertUserID == certUserId);
            }
            if (certEquipmentId.HasValue)
            {
                rawdata = rawdata.Where(item => item.CertEquipmentID == certEquipmentId);
            }
            if (isActive.HasValue)
            {
                rawdata = rawdata.Where(x => x.IsActive == isActive);
            }
            if (!string.IsNullOrWhiteSpace(dataType))
            {
                rawdata = rawdata.Where(x => x.PACSDataType.DataType.ToLower() == dataType.ToLower());
            }

            return rawdata;
        }

        public string GetSegmentationStatus(long rawDataID)
        {
            var status = string.Empty;
            var rawdata = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.PACS_RawData.First(item => item.RawDataID == rawDataID);
            });

            var hasLayers = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return rawdata.PACS_ProcessedData.Any(pd => pd.ProcessedDataLabel.Contains("Layers"));
            });

            var hasAnalisysFile = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return rawdata.PACS_ProcessedData.Any(pd => pd.ProcessedDataLabel.Contains("Analysis File"));
            });


            if (hasLayers && hasAnalisysFile)
            {
                status = "full";
            }
            else
            {
                if (hasLayers)
                {
                    status = "complete";
                }
                if (hasAnalisysFile)
                {
                    status = "partial";
                }
            }

            return status;
        }

        public override void Delete(PACS_RawDatum entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<PACS_RawDatum, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}