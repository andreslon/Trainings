using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(DOCU_Document.DOCU_DocumentMetadata))]
    public partial class DOCU_Document
    {
        internal class DOCU_DocumentMetadata
        {
            [Association("DocumentGroup_Association", "DocumentGroupID", "DocumentGroupID")]
            
            public DOCU_DocumentGroup DOCUDocumentGroup
            {
                get;
                set;
            }

            [Association("DocumentVersions_Association", "DocumentID", "DocumentID")]
            
            public IList<DOCU_DocumentVersion> DOCU_DocumentVersions
            {
                get;
                set;
            }

            [Required]
            public DOCU_Document DocumentName
            {
                get;
                set;
            }

            [Required]
            public DOCU_Document ApprovalDate
            {
                get;
                set;
            }

            //[Required]
            public DOCU_Document ReviewGoodUntilMonths
            {
                get;
                set;
            }

            //[Required]
            public DOCU_Document ReviewGracePeriodDays
            {
                get;
                set;
            }

            //[Required]
            public DOCU_Document InternalAuditGoodUntilMonths
            {
                get;
                set;
            }

            //[Required]
            public DOCU_Document InternalAuditPeriodDays
            {
                get;
                set;
            }
        }
    }
}