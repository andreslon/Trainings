namespace Excelsior.Reports.Performance
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for TrialRecruitmentProgress.
    /// </summary>
    public partial class TrialRecruitmentProgress : Telerik.Reporting.Report
    {
        public TrialRecruitmentProgress()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.TrialRecruitmentMetrics.ConnectionString = cs;

            var settings = new Settings();
            this.TrialRecruitmentMetrics.ObjectContext = new DataModel(settings);

        }
    }
}