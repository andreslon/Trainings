namespace Excelsior.Reports.Performance
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for SitePerformance.
    /// </summary>
    public partial class SitePerformance : Telerik.Reporting.Report
    {
        public SitePerformance()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.SitePerformMetrics.ConnectionString = cs;

            var settings = new Settings();
            this.SitePerformMetrics.ObjectContext = new DataModel(settings);
        }
    }
}