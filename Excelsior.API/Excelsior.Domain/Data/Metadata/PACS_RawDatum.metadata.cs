using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [XmlInclude(typeof(PACS_DicomOP))]
    [XmlInclude(typeof(PACS_DicomOPT))]
    [XmlInclude(typeof(PACS_DicomEPDF))]
    [XmlInclude(typeof(PACS_DicomWSI))]
    [KnownType(typeof(PACS_DicomOP))]
    [KnownType(typeof(PACS_DicomOPT))]
    [KnownType(typeof(PACS_DicomEPDF))]
    [KnownType(typeof(PACS_DicomWSI))]
    [MetadataTypeAttribute(typeof(PACS_RawDatum.PACS_RawDatumMetadata))]
    public partial class PACS_RawDatum
    {
        internal class PACS_RawDatumMetadata
        {
            [Association("RawData_Status_Association", "StatusID", "StatusID")]
            
            public PACS_RawDataStatus PACSRawDataStatus
            {
                get;
                set;
            }

            [Association("RawData_DataType_Association", "DataTypeID", "DataTypeID")]
            
            public PACS_DataType PACSDataType
            {
                get;
                set;
            }

            [Association("RawData_Series_Association", "SeriesID", "SeriesID")]
            
            public PACS_Series PACSSeries
            {
                get;
                set;
            }
            
            //[Association("RawData_Frames_Association", "RawDataID", "RawDataID")]
            //
            //public IList<PACS_DicomFrame> PACS_DicomFrames
            //{
            //    get;
            //    set;
            //}
        }
    }
}