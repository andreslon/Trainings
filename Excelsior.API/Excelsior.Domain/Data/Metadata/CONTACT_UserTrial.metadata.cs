using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_UserTrial.CONTACT_UserTrialMetadata))]
    public partial class CONTACT_UserTrial
    {
        internal sealed class CONTACT_UserTrialMetadata
        {
            [Association("CONTACTUser_Association", "UserID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }

            [Association("PACSTrial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }
        }
    }
}