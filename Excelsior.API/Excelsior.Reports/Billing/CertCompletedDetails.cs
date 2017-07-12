namespace Excelsior.Reports.Billing
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for CertCompletedDetails.
    /// </summary>
    public partial class CertCompletedDetails : Telerik.Reporting.Report
    {
        public CertCompletedDetails()
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
            //this.GetCertUser.ConnectionString = cs;
            //this.GetProcedureList.ConnectionString = cs;
            //this.GetCertEquipment.ConnectionString = cs;
            //this.GetCertType.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetCertUser.ObjectContext = dm;
            this.GetProcedureList.ObjectContext = dm;
            this.GetCertEquipment.ObjectContext = dm;
            this.GetCertType.ObjectContext = dm;


            var today = DateTime.UtcNow;
            var tt = (today.Day < 16) ? -1 : 0;
            this.ReportParameters["StartDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, 1);
            this.ReportParameters["EndDate"].Value = new DateTime(today.AddMonths(tt).Year, today.AddMonths(tt).Month, DateTime.DaysInMonth(today.AddMonths(tt).Year, today.AddMonths(tt).Month));

        }

        private void CertCompletedDetails_ItemDataBinding(object sender, EventArgs e)
        {

        }
    }
}