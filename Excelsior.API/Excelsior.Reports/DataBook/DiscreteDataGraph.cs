namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;
    /// <summary>
    /// Summary description for DiscreteDataGraph.
    /// </summary>
    public partial class DiscreteDataGraph : Telerik.Reporting.Report
    {
        public DiscreteDataGraph()
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
            //this.GetDiscreteDataByAnimalID.ConnectionString = cs;
            //this.GetDiscreteDataProcedureDes.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetDiscreteDataByAnimalID.ObjectContext = dm;
            this.GetDiscreteDataProcedureDes.ObjectContext = dm;
        }
    }
}