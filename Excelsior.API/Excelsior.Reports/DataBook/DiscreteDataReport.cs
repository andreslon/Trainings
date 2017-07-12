namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for DiscreteDataReport.
    /// </summary>
    public partial class DiscreteDataReport : Telerik.Reporting.Report
    {
        public DiscreteDataReport()
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
            //this.GetDiscreteDataSummary.ConnectionString = cs;
            //this.GetDiscreteDataProcedureDes.ConnectionString = cs;
            //this.GetGroupInfo.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrialName.ObjectContext = dm;
            this.GetDiscreteDataSummary.ObjectContext = dm;
            this.GetDiscreteDataProcedureDes.ObjectContext = dm;
            this.GetGroupInfo.ObjectContext = dm;

        }
    }
}