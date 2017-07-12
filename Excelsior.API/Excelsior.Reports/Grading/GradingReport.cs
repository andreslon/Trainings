namespace Excelsior.Reports.Grading
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for GradingReport.
    /// </summary>
    public partial class GradingReport : Telerik.Reporting.Report
    {
        public GradingReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetGradingReportSeries.ConnectionString = cs;
            //this.GradingReportGetResults.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetGradingReportSeries.ObjectContext = dm;
            this.GradingReportGetResults.ObjectContext = dm;

        }
    }
}