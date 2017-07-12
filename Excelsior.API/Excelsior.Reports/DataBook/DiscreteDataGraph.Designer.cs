namespace Excelsior.Reports.DataBook
{
    partial class DiscreteDataGraph
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscreteDataGraph));
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphTitle graphTitle1 = new Telerik.Reporting.GraphTitle();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.CategoryScale categoryScale2 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox73 = new Telerik.Reporting.TextBox();
            this.textBox72 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.graph1 = new Telerik.Reporting.Graph();
            this.cartesianCoordinateSystem1 = new Telerik.Reporting.CartesianCoordinateSystem();
            this.graphAxis3 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.GetDiscreteDataByAnimalID = new Telerik.Reporting.OpenAccessDataSource();
            this.lineSeries1 = new Telerik.Reporting.LineSeries();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.GetTrial = new Telerik.Reporting.OpenAccessDataSource();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.table2 = new Telerik.Reporting.Table();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.GetDiscreteDataProcedureDes = new Telerik.Reporting.OpenAccessDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.pictureBox1,
            this.textBox73,
            this.textBox72});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox2.Style.Color = System.Drawing.Color.Gray;
            this.textBox2.Style.Font.Name = "Segoe UI Semilight";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.StyleName = "Title";
            this.textBox2.Value = "=Format(\"Report Date: {0:ddMMMyyyy}\", Today())";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.87503254413604736D), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox73
            // 
            this.textBox73.KeepTogether = false;
            this.textBox73.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2916667461395264D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox73.Name = "textBox73";
            this.textBox73.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.44996064901351929D));
            this.textBox73.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox73.Style.Font.Name = "Segoe UI Semilight";
            this.textBox73.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox73.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox73.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox73.Value = "= Fields.TrialName";
            // 
            // textBox72
            // 
            this.textBox72.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox72.Name = "textBox72";
            this.textBox72.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0603922605514526D), Telerik.Reporting.Drawing.Unit.Inch(0.4499606192111969D));
            this.textBox72.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox72.Style.Font.Bold = true;
            this.textBox72.Style.Font.Name = "Segoe UI Semilight";
            this.textBox72.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox72.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox72.StyleName = "Title";
            this.textBox72.Value = "Study:";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.5999999046325684D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1,
            this.table2});
            this.detail.Name = "detail";
            // 
            // graph1
            // 
            graphGroup1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.timePointSeq"));
            graphGroup1.Label = "= Fields.timePointDes";
            graphGroup1.Name = "timePointSeqGroup";
            graphGroup1.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.timePointSeq", Telerik.Reporting.SortDirection.Asc));
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.cartesianCoordinateSystem1);
            this.graph1.DataSource = this.GetDiscreteDataByAnimalID;
            this.graph1.Legend.IsInsidePlotArea = true;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Legend.Style.Visible = false;
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Inch(0.699999988079071D));
            this.graph1.Name = "graph1";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.graph1.Series.Add(this.lineSeries1);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.8000001907348633D), Telerik.Reporting.Drawing.Unit.Inch(2.89996075630188D));
            this.graph1.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.graph1.Style.Visible = true;
            graphTitle1.Style.LineColor = System.Drawing.Color.LightGray;
            graphTitle1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Inch(0D);
            graphTitle1.Text = "=Format(\"Animal ID: {0}\", Fields.animalID)";
            this.graph1.Titles.Add(graphTitle1);
            // 
            // cartesianCoordinateSystem1
            // 
            this.cartesianCoordinateSystem1.Name = "cartesianCoordinateSystem1";
            this.cartesianCoordinateSystem1.XAxis = this.graphAxis3;
            this.cartesianCoordinateSystem1.YAxis = this.graphAxis2;
            // 
            // graphAxis3
            // 
            this.graphAxis3.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis3.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis3.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis3.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis3.MinorGridLineStyle.Visible = false;
            this.graphAxis3.Name = "graphAxis3";
            this.graphAxis3.Scale = categoryScale1;
            // 
            // graphAxis2
            // 
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "graphAxis2";
            this.graphAxis2.Scale = numericalScale1;
            // 
            // GetDiscreteDataByAnimalID
            // 
            this.GetDiscreteDataByAnimalID.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=excelsiorqa-db;Integrated Security=True";
            this.GetDiscreteDataByAnimalID.Name = "GetDiscreteDataByAnimalID";
            this.GetDiscreteDataByAnimalID.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetDiscreteDataByAnimalID.ObjectContextMember = "GetDiscreteDataByAnimalID";
            this.GetDiscreteDataByAnimalID.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "= Parameters.TrialID.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("imgProcedureID", typeof(long), "= Parameters.ImgProcedureID.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("laterality", typeof(string), "= Parameters.Laterality.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("animalID", typeof(string), "= Parameters.AnimalID.Value")});
            this.GetDiscreteDataByAnimalID.ProviderName = "System.Data.SqlClient";
            // 
            // lineSeries1
            // 
            this.lineSeries1.CategoryGroup = graphGroup1;
            this.lineSeries1.CoordinateSystem = this.cartesianCoordinateSystem1;
            this.lineSeries1.DataPointLabel = "= Sum(Fields.measurement)";
            this.lineSeries1.DataPointLabelStyle.Visible = false;
            this.lineSeries1.DataPointStyle.Visible = true;
            this.lineSeries1.LegendItem.Value = "measurement";
            this.lineSeries1.LineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.lineSeries1.MarkerMaxSize = Telerik.Reporting.Drawing.Unit.Pixel(50D);
            this.lineSeries1.MarkerMinSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries1.MarkerSize = Telerik.Reporting.Drawing.Unit.Pixel(5D);
            this.lineSeries1.Name = "lineSeries1";
            graphGroup2.Name = "seriesGroup";
            this.lineSeries1.SeriesGroup = graphGroup2;
            this.lineSeries1.Size = null;
            this.lineSeries1.Y = "= Sum(Fields.measurement)";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // GetTrial
            // 
            this.GetTrial.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=excelsiorqa-db;Integrated Security=True";
            this.GetTrial.Name = "GetTrial";
            this.GetTrial.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetTrial.ObjectContextMember = "GetTrial";
            this.GetTrial.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "= Parameters.TrialID.Value")});
            this.GetTrial.ProviderName = "System.Data.SqlClient";
            // 
            // graphAxis1
            // 
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "GraphAxis1";
            this.graphAxis1.Scale = categoryScale2;
            // 
            // table2
            // 
            this.table2.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(6.3416194915771484D)));
            this.table2.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D)));
            this.table2.Body.SetCellContent(0, 0, this.textBox22);
            tableGroup1.Name = "tableGroup4";
            this.table2.ColumnGroups.Add(tableGroup1);
            this.table2.DataSource = this.GetDiscreteDataProcedureDes;
            this.table2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22});
            this.table2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000000149011612D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.table2.Name = "table2";
            tableGroup2.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup2.Name = "detailTableGroup1";
            this.table2.RowGroups.Add(tableGroup2);
            this.table2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.3416194915771484D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            // 
            // textBox22
            // 
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.3416194915771484D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox22.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Style.Font.Name = "Segoe UI Semilight";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "= Format(\"Procedure: {0}\", Fields.Item)";
            // 
            // GetDiscreteDataProcedureDes
            // 
            this.GetDiscreteDataProcedureDes.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=excelsiorqa-db;Integrated Security=True";
            this.GetDiscreteDataProcedureDes.Name = "GetDiscreteDataProcedureDes";
            this.GetDiscreteDataProcedureDes.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetDiscreteDataProcedureDes.ObjectContextMember = "GetDiscreteDataProcedureDescription";
            this.GetDiscreteDataProcedureDes.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("imgProcedureID", typeof(long), "= Parameters.ImgProcedureID.Value")});
            this.GetDiscreteDataProcedureDes.ProviderName = "System.Data.SqlClient";
            // 
            // DiscreteDataGraph
            // 
            this.DataSource = this.GetTrial;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "DiscreteDataGraph";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "TrialID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Name = "ImgProcedureID";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Name = "AnimalID";
            reportParameter4.Name = "Laterality";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.399960994720459D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox73;
        private Telerik.Reporting.TextBox textBox72;
        private Telerik.Reporting.OpenAccessDataSource GetTrial;
        private Telerik.Reporting.OpenAccessDataSource GetDiscreteDataByAnimalID;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.CartesianCoordinateSystem cartesianCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis3;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.LineSeries lineSeries1;
        private Telerik.Reporting.Table table2;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.OpenAccessDataSource GetDiscreteDataProcedureDes;
    }
}