namespace Excelsior.Reports.DataBook
{
    using Domain;
    using Infrastructure.Utilities;

    /// <summary>
    /// Summary description for OcularExamReportIndividual.
    /// </summary>
    public partial class OcularExamReportIndividual : Telerik.Reporting.Report
    {
        public OcularExamReportIndividual()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrialName.ObjectContext = dm;
            this.GetOcularExamSummaryByAnimalID.ObjectContext = dm;

        }
    }
}