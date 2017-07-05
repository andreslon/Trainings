using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System;
using System.Linq;

namespace Excelsior.Business.Handlers
{
    public class ProcessedDataHandler
    {
        public DataModel db { get; set; }
        public ProcessedDataHandler(DataModel context)
        {
            db = context;
        }

        public bool SetProcessedData(PACS_ProcessedDatum entity)
        {
            try
            {
                var now = DateTime.UtcNow;
                var existEntity = db.PACS_ProcessedData.FirstOrDefault(x => x.RawDataID == entity.RawDataID && x.ProcessedDataLabel == entity.ProcessedDataLabel);
                if (existEntity != null)
                {
                    existEntity.ProcessDataXMLString = entity.ProcessDataXMLString;
                    entity = existEntity;
                }
                else
                {
                    entity.DateCreated = now;
                    db.Add(entity);
                }

                entity.DateModified = now;

                DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    db.SaveChanges();
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
         
        public IQueryable<PACS_ProcessedDatum> GetProcessedData(long rawDataID)
        {
            return db.PACS_ProcessedData.Where(x => x.RawDataID == rawDataID);
        }
    }
}
