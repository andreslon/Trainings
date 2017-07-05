using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(RPT_Report.RPT_ReportMetadata))]
    public partial class RPT_Report
    {
        internal sealed class RPT_ReportMetadata
        {
            [Association("Report_Category_Association", "ReportCategoryID", "ReportCategoryID")]
            
            public RPT_ReportCategory RPTReportCategory
            {
                get;
                set;
            }
        }
    }
}