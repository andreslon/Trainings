using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_DicomFrame.PACS_DicomFrameMetadata))]
    public partial class PACS_DicomFrame
    {
        internal class PACS_DicomFrameMetadata
        {
            [Association("RawData_Association", "RawDataID", "RawDataID")]
            
            public PACS_RawDatum PACSRawDatum
            {
                get;
                set;
            }
        }
    }
}