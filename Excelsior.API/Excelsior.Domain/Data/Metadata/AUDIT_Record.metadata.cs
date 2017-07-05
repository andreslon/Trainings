using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(AUDIT_Record.AUDIT_RecordMetadata))]
    public partial class AUDIT_Record
    {
        internal sealed class AUDIT_RecordMetadata
        {
            [Association("AUDIT_Action_Association", "AuditActionID", "AuditActionID")]
            public AUDIT_Action AUDITAction
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

            [Association("RelatedUser_Association", "RelatedUserID", "UserID")]
            public CONTACT_User RelatedUser
            {
                get;
                set;
            }

            [Association("PACS_Trial_Association", "TrialID", "TrialID")]
            public PACS_Trial PACSTrial
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

            [Association("PACS_Subject_Association", "SubjectID", "SubjectID")]
            public PACS_Subject PACSSubject
            {
                get;
                set;
            }

            [Association("WF_TempStep_Association", "WFTempStepID", "WFTempStepID")]
            public WF_TempStep WFTempStep
            {
                get;
                set;
            }

            [Association("CERT_UploadInfo_Association", "CertUploadInfoID", "CertUploadInfoID")]
            public CERT_UploadInfo CERTUploadInfo
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

            [Association("CERT_Equipment_Association", "CertEquipmentID", "CertEquipmentID")]
            public CERT_Equipment CERTEquipment
            {
                get;
                set;
            }
        }
    }
}