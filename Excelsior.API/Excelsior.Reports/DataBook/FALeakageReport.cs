namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for FALeakageReport.
    /// </summary>
    public partial class FALeakageReport : Telerik.Reporting.Report
    {
        public FALeakageReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetGroupInfo.ConnectionString = cs;
            //this.GetTrial.ConnectionString = cs;
            //this.GetCNVLeakageGradingList.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetGroupInfo.ObjectContext = dm;
            this.GetTrial.ObjectContext = dm;
            this.GetCNVLeakageGradingList.ObjectContext = dm;

        }
    }
}