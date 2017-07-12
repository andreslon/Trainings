namespace Excelsior.Reports.Performance
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for RCPerformance.
    /// </summary>
    public partial class RCPerformance : Telerik.Reporting.Report
    {
        public RCPerformance()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.RCPerformMetrics.ConnectionString = cs;
            //this.GetTrial.ConnectionString = cs;
            //this.GetGradingBacklog.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.RCPerformMetrics.ObjectContext = dm;
            this.GetTrial.ObjectContext = dm;
            this.GetGradingBacklog.ObjectContext = dm;
        }
    }
}