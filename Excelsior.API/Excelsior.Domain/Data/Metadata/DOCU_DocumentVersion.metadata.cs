using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(DOCU_DocumentVersion.DOCU_DocumentVersionMetadata))]
    public partial class DOCU_DocumentVersion
    {
        internal class DOCU_DocumentVersionMetadata
        {
            [Association("DocuDocument_Association", "DocumentID", "DocumentID")]
            
            public DOCU_Document DOCUDocument
            {
                get;
                set;
            }

            [Association("DocumentUsers_Association", "DocumentVersionID", "DocumentVersionID")]
            
            public IList<DOCU_DocumentVersionUser> DOCU_DocumentVersionUsers
            {
                get;
                set;
            }
        }
    }
}