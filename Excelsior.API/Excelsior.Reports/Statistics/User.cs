namespace Excelsior.Reports.Statistics
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for User.
    /// </summary>
    public partial class User : Telerik.Reporting.Report
    {
        public User()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //var cs = Security.Decrypt(ConfigurationManager.ConnectionStrings["EyeKorConnection"].ConnectionString);
            //this.GetUser.ConnectionString = cs;
            //this.GetUserTrials.ConnectionString = cs;
            //this.GetUserInOut.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetUser.ObjectContext = dm;
            this.GetUserTrials.ObjectContext = dm;
            this.GetUserInOut.ObjectContext = dm;

            this.ReportParameters["StartDate"].Value = DateTime.UtcNow.AddMonths(-1);
            this.ReportParameters["EndDate"].Value = DateTime.UtcNow;
        }
    }
}