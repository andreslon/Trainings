using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CERT_User.CERT_UserMetadata))]
    public partial class CERT_User
    {
        internal sealed class CERT_UserMetadata
        {
            [Association("CERT_UploadInfo_Association", "CertUserID", "CertUserID")]
            
            public CERT_UploadInfo CERT_UploadInfos
            {
                get;
                set;
            }

            [Association("CERT_ImgProcedureList_Association", "ImgProcedureID", "ImgProcedureID")]
            
            public CERT_ImgProcedureList CERTImgProcedureList
            {
                get;
                set;
            }

            [Association("CONTACTUserTrial_Association", "UserTrialID", "UserTrialID")]
            
            public CONTACT_UserTrial CONTACTUserTrial
            {
                get;
                set;
            }

            [Association("CertifiedBy_Association", "CertifiedByID", "UserID")]
            
            public CONTACT_User CertifiedBy
            {
                get;
                set;
            }

            [Association("Queries_Association", "CertUserID", "CertUserID")]
            
            public IList<QRY_Query> QRY_Queries
            {
                get;
                set;
            }
        }
    }
}