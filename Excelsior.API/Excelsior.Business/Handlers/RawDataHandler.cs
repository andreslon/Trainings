using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System.Linq;

namespace Excelsior.Business.Logic
{
    public class RawDataHandler
    {
        public DataModel db { get; set; }
        public RawDataHandler(DataModel context)
        {
            db = context;
        }

        public IQueryable<PACS_RawDatum> GetRawDataBySeriesID(long seriesID)
        {
            var rawdata = db.PACS_RawData.Where(item => item.IsActive && item.SeriesID == seriesID);
            rawdata = rawdata.OfType<PACS_DicomOPT>();
            return rawdata;
        }

        public PACS_RawDatum GetRawDataByID(long rawDataID)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.PACS_RawData.FirstOrDefault(item => item.RawDataID == rawDataID);
            });
            return result;
        }

        public string GetSegmentationStatus(long rawDataID)
        {
            var status = string.Empty;
            var rawdata = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.PACS_RawData.First(item => item.RawDataID == rawDataID);
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
    }
}
