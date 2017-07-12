namespace Excelsior.Reports.Billing
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for BillingSeriesCheckIn.
    /// </summary>
    public partial class BillingSeriesCheckIn : Telerik.Reporting.Report
    {
        public BillingSeriesCheckIn()
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
            //this.GetSeriesCheckinCompleted.ConnectionString = cs;
            //this.GetSeriesCreated.ConnectionString = cs;

            var settings = new Settings();
            this.GetSeriesCheckinCompleted.ObjectContext = new DataModel(settings);

            var today = DateTime.UtcNow;
            var tt = (today.Day < 16) ? -1 : 0;
            this.ReportParameters["StartDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, 1);
            this.ReportParameters["EndDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, DateTime.DaysInMonth(today.AddMonths(tt).Year, today.AddMonths(tt).Month));
        }
    }
}