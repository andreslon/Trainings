using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{

    [MetadataTypeAttribute(typeof(PACS_DicomOPT.PACS_DicomOPTMetadata))]
    public partial class PACS_DicomOPT
    {
        internal class PACS_DicomOPTMetadata
        {
            [Association("DicomOPT_ScoutImage_Association", "RefRawDataID", "RawDataID")]
            
            public PACS_RawDatum ScoutImage
            {
                get;
                set;
            }
        }
    }
}