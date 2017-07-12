namespace Excelsior.Reports.Billing
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for CertificationCompleted.
    /// </summary>
    public partial class CertificationCompleted : Telerik.Reporting.Report
    {
        public CertificationCompleted()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetCertUserFull.ConnectionString = cs;
            //this.GetCertUserGrandfathered.ConnectionString = cs;
            //this.GetCertEquipmentFull.ConnectionString = cs;
            //this.GetCertEquipmentGrandfathered.ConnectionString = cs;
            //this.GetCertUserEquipCount.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetCertUserFull.ObjectContext = dm;
            this.GetCertUserGrandfathered.ObjectContext = dm;
            this.GetCertEquipmentFull.ObjectContext = dm;
            this.GetCertEquipmentGrandfathered.ObjectContext = dm;
            this.GetCertUserEquipCount.ObjectContext = dm;

            var today = DateTime.UtcNow;
            var tt = (today.Day < 16) ? -1 : 0;
            this.ReportParameters["StartDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, 1);
            this.ReportParameters["EndDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, DateTime.DaysInMonth(today.AddMonths(tt).Year, today.AddMonths(tt).Month));
        }
    }
}
