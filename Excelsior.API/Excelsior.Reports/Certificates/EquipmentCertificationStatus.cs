namespace Excelsior.Reports.Certificates
{
    using Domain;
    using Infrastructure.Utilities;
    using System;
    /// <summary>
    /// Summary description for CertificationCompleted.
    /// </summary>
    public partial class EquipmentCertificationStatus : Telerik.Reporting.Report
    {
        public EquipmentCertificationStatus()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetEquipCertStatus.ConnectionString = cs;
            //this.GetEquipCertType.ConnectionString = cs;
            //this.GetEquipProcedureList.ConnectionString = cs;
            //this.GetEquipSiteID.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetEquipCertStatus.ObjectContext = dm;
            this.GetEquipCertType.ObjectContext = dm;
            this.GetEquipProcedureList.ObjectContext = dm;
            this.GetEquipSiteID.ObjectContext = dm;

            //this.ReportParameters["StartDate"].Value = DateTime.UtcNow.AddMonths(-1);
            //this.ReportParameters["EndDate"].Value = DateTime.UtcNow;
        }
    }
}