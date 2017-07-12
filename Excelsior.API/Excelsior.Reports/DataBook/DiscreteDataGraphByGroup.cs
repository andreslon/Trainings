namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for DiscreteDataGraphByGroup.
    /// </summary>
    public partial class DiscreteDataGraphByGroup : Telerik.Reporting.Report
    {
        public DiscreteDataGraphByGroup()
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
            //this.GetDiscreteDataProcedureDes.ConnectionString = cs;
            //this.GetDiscreteDataByGroup.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetDiscreteDataProcedureDes.ObjectContext = dm;
            this.GetDiscreteDataByGroup.ObjectContext = dm;
        }
    }
}