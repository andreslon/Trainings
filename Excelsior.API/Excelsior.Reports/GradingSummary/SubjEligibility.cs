namespace Excelsior.Reports.GradingSummary
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for SubjEligibility.
    /// </summary>
    public partial class SubjEligibility : Telerik.Reporting.Report
    {
        public SubjEligibility()
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
            //this.GetSubjEligibility.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetSubjEligibility.ObjectContext = dm;
        }
    }
}