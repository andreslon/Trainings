using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CERT_UploadInfo.CERT_UploadInfoMetadata))]
    public partial class CERT_UploadInfo
    {
        internal sealed class CERT_UploadInfoMetadata
        {
            [Association("QRY_Query_Association", "QueryID", "QueryID")]
            
            public QRY_Query QRYQuery
            {
                get;
                set;
            }

            [Association("CERT_Equipment_Association", "CertEquipmentID", "CertEquipmentID")]
            
            public CERT_Equipment CERTEquipment
            {
                get;
                set;
            }

            [Association("CERT_User_Association", "CertUserID", "CertUserID")]
            
            public CERT_User CERTUser
            {
                get;
                set;
            }

            [Association("CONTACT_Equipment_Association", "EquipmentID", "EquipmentID")]
            
            public CONTACT_Equipment CONTACTEquipment
            {
                get;
                set;
            }

            [Association("CONTACT_User_Association", "UserID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }
        }
    }
}