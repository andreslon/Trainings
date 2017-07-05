using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(DOCU_DocumentGroup.DOCU_DocumentGroupMetadata))]
    public partial class DOCU_DocumentGroup
    {
        internal class DOCU_DocumentGroupMetadata
        {
            [Association("Trial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }


            [Association("DOCU_AuthorizationTypes_Association", "DocuAuthorizationID", "DocuAuthorizationTypeID")]
            
            public DOCU_AuthorizationType DOCUAuthorizationType
            {
                get;
                set;
            }
        }
    }
}