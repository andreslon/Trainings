namespace Excelsior.Reports.GradingSummary
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for GrdConfidence.
    /// </summary>
    public partial class GrdConfidence : Telerik.Reporting.Report
    {
        public GrdConfidence()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GrdConfidenceGetScore.ConnectionString = cs;
            //this.GetTrial.ConnectionString = cs;
            //this.GetProcedureList.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GrdConfidenceGetScore.ObjectContext = dm;
            this.GetTrial.ObjectContext = dm;
            this.GetProcedureList.ObjectContext = dm;
        }
    }
}