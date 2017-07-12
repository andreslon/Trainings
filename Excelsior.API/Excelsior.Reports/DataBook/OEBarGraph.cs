namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for OEBarGraph.
    /// </summary>
    public partial class OEBarGraph : Telerik.Reporting.Report
    {
        public OEBarGraph()
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
            //this.GetOEGraphRE.ConnectionString = cs;
            //this.GetOEGraphLE.ConnectionString = cs;
            //this.GetSubjectGroupName.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrialName.ObjectContext = dm;
            this.GetGroupInfo.ObjectContext = dm;
            this.GetOEGraphRE.ObjectContext = dm;
            this.GetOEGraphLE.ObjectContext = dm;
            this.GetSubjectGroupName.ObjectContext = dm;
        }
    }
}