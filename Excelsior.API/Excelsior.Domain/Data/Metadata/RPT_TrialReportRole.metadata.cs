using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(RPT_TrialReportRole.RPT_TrialReportRoleMetadata))]
    public partial class RPT_TrialReportRole
    {
        internal sealed class RPT_TrialReportRoleMetadata
        {
            [Association("TrialReportRole_TrialReport_Association", "TrialReportID", "TrialReportID")]
            
            public RPT_TrialReport RPTTrialReport
            {
                get;
                set;
            }

            [Association("TrialReportRole_Role_Association", "RoleId", "RoleId")]
            
            public Aspnet_Role AspnetRole
            {
                get;
                set;
            }
        }
    }
}