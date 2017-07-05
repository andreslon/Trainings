using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_Report.GRD_ReportMetadata))]
    public partial class GRD_Report
    {
        internal sealed class GRD_ReportMetadata
        {
            [Association("CONTACT_User_Association", "PerformedBy", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }

            [Association("PACS_Series_Association", "SeriesID", "SeriesID")]
            
            public PACS_Series PACSSeries
            {
                get;
                set;
            }

            [Association("GRD_ReportResults_Association", "GReportID", "GReportID")]
            
            public IList<GRD_ReportResult> GRD_ReportResults
            {
                get;
                set;
            }

            [Association("MEA_Measurements_Association", "GReportID", "GReportID")]
            
            public IList<MEA_Measurement> MEA_Measurements
            {
                get;
                set;
            }
        }
    }
}