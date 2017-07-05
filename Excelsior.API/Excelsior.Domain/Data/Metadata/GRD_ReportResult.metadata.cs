using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(GRD_ReportResult.GRD_ReportResultMetadata))]
    public partial class GRD_ReportResult
    {
        internal sealed class GRD_ReportResultMetadata
        {
            [Association("GRDReport_GRD_ReportResults_Association", "GReportID", "GReportID")]
            
            public GRD_Report GRDReport
            {
                get;
                set;
            }

            [Association("GRDReportResult_MEAMeasurement_Association", "GAnswerMeasurement", "MeasurementID")]
            
            public MEA_Measurement MEAMeasurement 
            {
                get;
                set;
            }            
        }
    }
}