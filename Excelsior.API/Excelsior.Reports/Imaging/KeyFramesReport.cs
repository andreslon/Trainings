namespace Excelsior.Reports.Imaging
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for KeyFramesReport.
    /// </summary>
    public partial class KeyFramesReport : Telerik.Reporting.Report
    {
        public KeyFramesReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetSeriesDetails.ConnectionString = cs;
            //this.GetKeyFrames.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetSeriesDetails.ObjectContext = dm;
            this.GetKeyFrames.ObjectContext = dm;
        }
    }
}