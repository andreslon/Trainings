using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class ProcessedDataResponseDto
    {
        public long ProcessedDataID { get; set; }
        public long? RawDataID { get; set; }
        public string ProcessedDataLabel { get; set; }
        public string ProcessDataXMLString { get; set; }
        public long? UserID { get; set; }
        public long? DicomFrameID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
