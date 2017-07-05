using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_ProcessedDatum.PACS_ProcessedDatumMetadata))]
    public partial class PACS_ProcessedDatum
    {
        internal class PACS_ProcessedDatumMetadata
        {
            [Association("RawData_Association", "RawDataID", "RawDataID")]
            
            public PACS_RawDatum PACSRawDatum
            {
                get;
                set;
            }

            [Association("DicomFrame_Association", "DicomFrameID", "DicomFrameID")]
            
            public PACS_DicomFrame PACSDicomFrame
            {
                get;
                set;
            }

            [Association("User_Association", "UserID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }
        }
    }
}