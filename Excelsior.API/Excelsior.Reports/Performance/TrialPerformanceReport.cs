namespace Excelsior.Reports.Performance
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for TrialPerformanceReport.
    /// </summary>
    public partial class TrialPerformanceReport : Telerik.Reporting.Report
    {
        public TrialPerformanceReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetTrialName.ConnectionString = cs;
            //this.RCPerformMetrics.ConnectionString = cs;
            //this.SitePerformMetrics.ConnectionString = cs;
            //this.TrialRecruitmentMetrics.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrialName.ObjectContext = dm;
            this.RCPerformMetrics.ObjectContext = dm;
            this.SitePerformMetrics.ObjectContext = dm;
            this.TrialRecruitmentMetrics.ObjectContext = dm;
        }
    }
}