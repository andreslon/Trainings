
namespace Excelsior.Reports.Billing
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for SeriesCompleted.
    /// </summary>
    public partial class SeriesGradingCompleted : Telerik.Reporting.Report
    {
        public SeriesGradingCompleted()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //            var dc = new DataModel();
            ////            this.ReportParameters["Trial"].Value = dc.PACS_Trials.Single(t => t.TrialName == "MAJORCA");
            //            this.ReportParameters["Trial"].Value = "MAJORCA";

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetTrialName.ConnectionString = cs;
            //this.GetSeriesGradingSummary.ConnectionString = cs;
            //this.GetSeriesGradingCompleted.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrialName.ObjectContext = dm;
            this.GetSeriesGradingSummary.ObjectContext = dm;
            this.GetSeriesGradingCompleted.ObjectContext = dm;

            var today = DateTime.UtcNow;
            var tt = (today.Day < 16) ? -1 : 0;
            this.ReportParameters["StartDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, 1);
            this.ReportParameters["EndDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, DateTime.DaysInMonth(today.AddMonths(tt).Year, today.AddMonths(tt).Month));
        }
    }
}