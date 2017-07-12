namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for OcularExamReport.
    /// </summary>
    public partial class OcularExamReport : Telerik.Reporting.Report
    {
        public OcularExamReport()
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
            //this.GetGroupInfo.ConnectionString = cs;
            //this.GetOcularExamSummaryByTimePoint.ConnectionString = cs;
            //this.GetTimePointDescription.ConnectionString = cs;
            //this.GetOESummaryDiseaseOnlyByTimePoint.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetGroupInfo.ObjectContext = dm;
            this.GetOcularExamSummaryByTimePoint.ObjectContext = dm;
            this.GetTimePointDescription.ObjectContext = dm;
            this.GetOESummaryDiseaseOnlyByTimePoint.ObjectContext = dm;
        }
    }
}
