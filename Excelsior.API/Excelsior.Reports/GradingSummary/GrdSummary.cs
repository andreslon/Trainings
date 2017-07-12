namespace Excelsior.Reports.GradingSummary
{
    using Domain;
    using Infrastructure.Utilities;
    using System;

    /// <summary>
    /// Summary description for PreclinicalGradingSummary.
    /// </summary>
    public partial class GrdSummary : Telerik.Reporting.Report
    {
        public GrdSummary()
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
            //this.GetSubjectGroup.ConnectionString = cs;
            //this.GetSubjectCohort.ConnectionString = cs;
            //this.GetSubject.ConnectionString = cs;
            //this.GetGrdTemplate.ConnectionString = cs;
            //this.GetGrdQuestions.ConnectionString = cs;
            //this.GetGrdCategoryAnswerCount.ConnectionString = cs;
            //this.GetGrdNumericalAnswer.ConnectionString = cs;

            var settings = new Settings();
            var dm = new DataModel(settings);
            this.GetTrial.ObjectContext = dm;
            this.GetSubjectGroup.ObjectContext = dm;
            this.GetSubjectCohort.ObjectContext = dm;
            this.GetSubject.ObjectContext = dm;
            this.GetGrdTemplate.ObjectContext = dm;
            this.GetGrdQuestions.ObjectContext = dm;
            this.GetGrdCategoryAnswerCount.ObjectContext = dm;
            this.GetGrdNumericalAnswer.ObjectContext = dm;

        }

        private void crosstab1_NeedDataSource(object sender, EventArgs e)
        {
        }

        private void graph1_NeedDataSource(object sender, EventArgs e)
        {
        }

        private void crosstab1_ItemDataBound(object sender, EventArgs e)
        {
        }

        private void GrdSummary_ItemDataBound(object sender, EventArgs e)
        {
        }

        private void GrdSummary_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.Report rpt = (Telerik.Reporting.Processing.Report)sender;
            var obj = rpt.Parameters;

            var graphType = (string)obj["GraphType"].Value;
            if (graphType != null)
            {
                if (graphType.Equals("Table"))
                {
                    this.crosstab1.DataSource = this.GetGrdCategoryAnswerCount;
                    this.crosstab1.Visible = true;
                    this.graph1.Visible = false;
                }
                else if (graphType.Equals("Graph"))
                {
                    this.crosstab1.Visible = false;
                    this.graph1.DataSource = this.GetGrdNumericalAnswer;
                    this.graph1.Visible = true;
                }
            }

        }
    }
}