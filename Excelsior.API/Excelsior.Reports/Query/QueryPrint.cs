namespace Excelsior.Reports.Query
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for SeriesCompleted.
    /// </summary>
    public partial class QueryPrint : Telerik.Reporting.Report
    {
        public QueryPrint()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //            var dc = new DataModel();
            //            this.ReportParameters["Trial"].Value = dc.PACS_Trials.Single(t => t.TrialName == "MAJORCA");
            //            this.ReportParameters["Trial"].Value = "MAJORCA";

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetQuery.ConnectionString = cs;
            //this.GetQueryMessage.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetQuery.ObjectContext = dm;
            this.GetQueryMessage.ObjectContext = dm;
        }
    }
}