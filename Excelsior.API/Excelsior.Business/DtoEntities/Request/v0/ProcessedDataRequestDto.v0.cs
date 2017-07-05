using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Request.v0
{
    public class ProcessedDataRequestDto
    {
        public PACS_ProcessedDatum ToEntity(PACS_ProcessedDatum entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_ProcessedDatum();
            }

            entity.ProcessedDataID = ProcessedDataID;
            entity.RawDataID = RawDataID;
            entity.ProcessedDataLabel = ProcessedDataLabel;
            entity.ProcessDataXMLString = ProcessDataXMLString;
            entity.UserID = UserID;
            entity.DicomFrameID = DicomFrameID;

            return entity;
        }

        public long ProcessedDataID { get; set; }
        public long? RawDataID { get; set; }
        public string ProcessedDataLabel { get; set; }
        public string ProcessDataXMLString { get; set; }
        public long? UserID { get; set; }
        public long? DicomFrameID { get; set; }
    }
}
