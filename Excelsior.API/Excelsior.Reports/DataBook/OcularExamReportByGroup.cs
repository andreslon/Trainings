namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for OcularExamReportByGroup.
    /// </summary>
    public partial class OcularExamReportByGroup : Telerik.Reporting.Report
    {
        public OcularExamReportByGroup()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetTrialName.ConnectionString = cs;
            //this.GetGroupInfo.ConnectionString = cs;
            //this.GetSubjectGroupName.ConnectionString = cs;
            //this.GetOESummaryDiseaseOnlyByGroup.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrialName.ObjectContext = dm;
            this.GetGroupInfo.ObjectContext = dm;
            this.GetSubjectGroupName.ObjectContext = dm;
            this.GetOESummaryDiseaseOnlyByGroup.ObjectContext = dm;
        }
    }
}