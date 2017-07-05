using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(DOCU_DocumentVersionUser.DOCU_DocumentVersionUserMetadata))]
    public partial class DOCU_DocumentVersionUser
    {
        internal class DOCU_DocumentVersionUserMetadata
        {
            [Association("DocumentVersion_Association", "DocumentVersionID", "DocumentVersionID")]
            
            public DOCU_DocumentVersion DOCUDocumentVersion
            {
                get;
                set;
            }
        }
    }
}