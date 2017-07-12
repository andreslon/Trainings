namespace Excelsior.Reports.Performance
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for QueryReport.
    /// </summary>
    public partial class QueryReport : Telerik.Reporting.Report
    {
        public QueryReport()
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
            //this.GetQueryReport.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetQueryReport.ObjectContext = dm;

            var today = DateTime.UtcNow;
            var tt = (today.Day < 16) ? -1 : 0;
            this.ReportParameters["StartDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, 1);
        }
    }
}