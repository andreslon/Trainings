using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(UPLD_UploadInfo.UPLD_UploadInfoMetadata))]
    public partial class UPLD_UploadInfo
    {
        internal class UPLD_UploadInfoMetadata
        {
            [Required(ErrorMessage = "Date is required.")]
            public DateTime PhotoDate
            {
                get;
                set;
            }

            [Required(ErrorMessage = "File is required.")]
            public string DataFileLocation
            {
                get;
                set;
            }

            [Association("Uploader_Association", "UploaderID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }

            [Association("PACS_Series_Association", "SeriesID", "SeriesID")]
            
            public PACS_Series PACSSeries
            {
                get;
                set;
            }
        }
    }
}