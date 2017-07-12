namespace Excelsior.Reports.Billing
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for SeriesCheckinCompleted.
    /// </summary>
    public partial class SeriesCheckinCompleted : Telerik.Reporting.Report
    {
        public SeriesCheckinCompleted()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
           
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetTrial.ObjectContext = cs;
            //this.GetSeriesCheckinCompleted.ConnectionString = cs;
            //this.GetSeriesCheckinSummary.ConnectionString = cs;
            //this.GetSeriesCreatedSummary.ConnectionString = cs;

            var settings = new Settings();
            this.GetTrial.ObjectContext = new DataModel(settings); 
            this.GetSeriesCheckinCompleted.ObjectContext = new DataModel(settings); ;
            this.GetSeriesCheckinSummary.ObjectContext = new DataModel(settings); ;
            this.GetSeriesCreatedSummary.ObjectContext = new DataModel(settings); ;
            var today = DateTime.UtcNow;
            var tt = (today.Day < 16) ? -1 : 0;
            this.ReportParameters["StartDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, 1);
            this.ReportParameters["EndDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, DateTime.DaysInMonth(today.AddMonths(tt).Year, today.AddMonths(tt).Month));
        }
    }
}