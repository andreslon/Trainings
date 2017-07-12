namespace Excelsior.Reports.GradingSummary
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for EligibilityDetailReport.
    /// </summary>
    public partial class EligibilityDetailReport : Telerik.Reporting.Report
    {
        public EligibilityDetailReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetTrial.ConnectionString = cs;
            //this.GetEligDetailReport.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetEligDetailReport.ObjectContext = dm;
        }
    }
}