using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(RPT_TrialReport.RPT_TrialReportMetadata))]
    public partial class RPT_TrialReport
    {
        internal sealed class RPT_TrialReportMetadata
        {
            [Association("TrialReport_Trial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }

            [Association("TrialReport_Report_Association", "ReportID", "ReportID")]
            
            public RPT_Report RPTReport
            {
                get;
                set;
            }
        }
    }
}