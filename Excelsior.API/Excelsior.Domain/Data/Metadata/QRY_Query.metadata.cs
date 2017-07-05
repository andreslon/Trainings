using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(QRY_Query.QRY_QueryMetadata))]
    public partial class QRY_Query
    {
        internal sealed class QRY_QueryMetadata
        {
            //[Required(ErrorMessage = "Sender is required.")]
            //public long? SenderID
            //{
            //    get;
            //    set;
            //}

            //[Required(ErrorMessage = "Recipient is required.")]
            //public long? ReceipientID
            //{
            //    get;
            //    set;
            //}

            [Required(ErrorMessage = "Subject is required.")]
            public string Subject
            {
                get;
                set;
            }

            [Association("Query_Trial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }

            [Association("Query_Sender_Association", "SenderID", "UserID")]
            
            public CONTACT_User Sender
            {
                get;
                set;
            }

            [Association("Query_Recipient_Association", "RecipientID", "UserID")]
            
            public CONTACT_User Recipient
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

            [Association("Query_Message_Association", "QueryID", "QueryID")]
            
            public IList<QRY_Message> QRY_Messages
            {
                get;
                set;
            }
        }
    }
}