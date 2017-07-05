using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(AUDIT_CRUDAudit.AUDIT_CRUDAuditMetadata))]
    public partial class AUDIT_CRUDAudit
    {
        internal sealed class AUDIT_CRUDAuditMetadata
        {
            //[Association("CONTACT_User_Association", "UserID", "UserID")]
            //public CONTACT_User CONTACTUser
            //{
            //    get;
            //    set;
            //}
        }
    }
}