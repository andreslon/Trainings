namespace Excelsior.Reports.Certificates
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for TechnicianCertificate.
    /// </summary>
    public partial class TechnicianCertificate : Telerik.Reporting.Report
    {
        public TechnicianCertificate()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var settings = new Settings();
            //var cs = settings.GetConnectionString("EyeKorConnection");
            //this.GetTechnicianCertificate.ConnectionString = cs;
            this.GetTechnicianCertificate.ObjectContext = new DataModel(settings);
        }
    }
}     