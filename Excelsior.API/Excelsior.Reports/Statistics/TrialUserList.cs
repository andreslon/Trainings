namespace Excelsior.Reports.Statistics
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for TrialUserList.
    /// </summary>
    public partial class TrialUserList : Telerik.Reporting.Report
    {
        public TrialUserList()
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
            //this.GetSponsorUserList.ConnectionString = cs;
            //this.GetSiteUserList.ConnectionString = cs;
            //this.GetSiteEquipList.ConnectionString = cs;
            //this.GetRCUserList.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetSponsorUserList.ObjectContext = dm;
            this.GetSiteUserList.ObjectContext = dm;
            this.GetSiteEquipList.ObjectContext = dm;
            this.GetRCUserList.ObjectContext = dm;

        }
    }
}