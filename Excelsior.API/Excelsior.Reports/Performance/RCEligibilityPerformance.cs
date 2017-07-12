namespace Excelsior.Reports.Performance
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for RCEligibilityPerformance.
    /// </summary>
    public partial class RCEligibilityPerformance : Telerik.Reporting.Report
    {
        public RCEligibilityPerformance()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.RCEligibilityPerformMetrics.ConnectionString = cs;

            var settings = new Settings();
            this.RCEligibilityPerformMetrics.ObjectContext = new DataModel(settings);
        }
    }
}
