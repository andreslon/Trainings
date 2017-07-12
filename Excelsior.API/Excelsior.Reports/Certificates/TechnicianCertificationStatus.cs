namespace Excelsior.Reports.Certificates
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for CertificationCompleted.
    /// </summary>
    public partial class TechnicianCertificationStatus : Telerik.Reporting.Report
    {
        public TechnicianCertificationStatus()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetTechCertStatus.ConnectionString = cs;
            //this.GetCertificationStatusType.ConnectionString = cs;
            //this.GetProcedureList.ConnectionString = cs;
            //this.GetSiteID.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetTechCertStatus.ObjectContext = dm;
            this.GetCertificationStatusType.ObjectContext = dm;
            this.GetProcedureList.ObjectContext = dm;
            this.GetSiteID.ObjectContext = dm;

            //this.ReportParameters["StartDate"].Value = DateTime.UtcNow.AddMonths(-1);
            //this.ReportParameters["EndDate"].Value = DateTime.UtcNow;
        }
    }
}