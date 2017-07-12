namespace Excelsior.Reports.Certificates
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for EquipmentCertificate.
    /// </summary>
    public partial class EquipmentCertificate : Telerik.Reporting.Report
    {
        public EquipmentCertificate()
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
            //this.GetDeviceCertificate.ConnectionString = cs;
            this.GetDeviceCertificate.ObjectContext = new DataModel(settings);
        }
    }
}