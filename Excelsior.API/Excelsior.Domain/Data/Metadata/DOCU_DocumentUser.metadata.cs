using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(DOCU_DocumentUser.DOCU_DocumentUserMetadata))]
    public partial class DOCU_DocumentUser
    {
        internal class DOCU_DocumentUserMetadata
        {
            [Association("Document_Association", "DocumentID", "DocumentID")]
            
            public DOCU_Document DOCUDocument
            {
                get;
                set;
            }

            [Association("CONTACTUser_Association", "UserID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }
        }
    }
}