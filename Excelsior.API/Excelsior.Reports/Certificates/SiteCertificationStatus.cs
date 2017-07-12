namespace Excelsior.Reports.Certificates
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for SiteCertificationStatus.
    /// </summary>
    public partial class SiteCertificationStatus : Telerik.Reporting.Report
    {
        public SiteCertificationStatus()
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
            //this.GetSites.ConnectionString = cs;
            //this.GetSiteCertificationStatus.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetSites.ObjectContext = dm;
            this.GetSiteCertificationStatus.ObjectContext = dm;
        }
    }
}