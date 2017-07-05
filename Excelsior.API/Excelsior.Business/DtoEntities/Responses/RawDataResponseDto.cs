using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class RawDataResponseDto
    {
        public long? RawDataID { get; set; }
        public long? SeriesID { get; set; }
        public long? DataTypeID { get; set; }
        public string ThumbImageLocation { get; set; }
        public string DCMInstanceUID { get; set; }
        public string DCMFileLocation { get; set; }
        public string Laterality { get; set; }
        public bool? IsActive { get; set; }
        public string LastError { get; set; }
        public long? StatusID { get; set; }
        public bool? HasError { get; set; }
        public DataTypeResponseDto DataType { get; set; }
        public RDStatusResponseDto Status { get; set; }
        public DicomOPResponseDto DicomOP { get; set; }
        public DicomOPTResponseDto DicomOPT { get; set; }
        public DicomWSIResponseDto DicomWSI { get; set; }
        public string SegmentationStatus { get; set; }
    }
}
