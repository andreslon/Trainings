namespace Excelsior.Reports.GradingSummary
{
    partial class GrdConfidence
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrdConfidence));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector3 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule6 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector4 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule7 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector5 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.GetProcedureList = new Telerik.Reporting.OpenAccessDataSource();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.crosstab1 = new Telerik.Reporting.Crosstab();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.GrdConfidenceGetScore = new Telerik.Reporting.OpenAccessDataSource();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.GetTrial = new Telerik.Reporting.OpenAccessDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Segoe UI Semilight";
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "Normal.TableGroup";
            this.textBox1.Value = "=Fields.confidenceScore";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox2.Style.Font.Name = "Segoe UI Semilight";
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.StyleName = "Normal.GrandTotal";
            this.textBox2.Value = "Total";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox4.Style.Font.Name = "Segoe UI Semilight";
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.StyleName = "Normal.TableGroup";
            this.textBox4.Value = "=Fields.siteName";
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98333334922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.35000002384185791D));
            this.textBox3.Style.Font.Name = "Segoe UI Semilight";
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "Normal.TableGroup";
            this.textBox3.Value = "=Fields.siteID";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1083335876464844D), Telerik.Reporting.Drawing.Unit.Inch(0.35000002384185791D));
            this.textBox6.Style.Font.Name = "Segoe UI Semilight";
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.StyleName = "Normal.GrandTotal";
            this.textBox6.Value = "Total";
            // 
            // GetProcedureList
            // 
            this.GetProcedureList.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=excelsiorqa-db;Integrated Security=True";
            this.GetProcedureList.Name = "GetProcedureList";
            this.GetProcedureList.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetProcedureList.ObjectContextMember = "GetTechCertProcedureList";
            this.GetProcedureList.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "=Parameters.trialID.Value")});
            this.GetProcedureList.ProviderName = "System.Data.SqlClient";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.80000007152557373D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox11,
            this.textBox55,
            this.pictureBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox5
            // 
            this.textBox5.KeepTogether = false;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1916670799255371D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.1083335876464844D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Segoe UI Semilight";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox5.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "= Fields.trialName";
            // 
            // textBox11
            // 
            this.textBox11.KeepTogether = false;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0603922605514526D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox11.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Segoe UI Semilight";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.StyleName = "Title";
            this.textBox11.Value = "Study:";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox55.Style.Color = System.Drawing.Color.Gray;
            this.textBox55.Style.Font.Name = "Segoe UI";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.StyleName = "Title";
            this.textBox55.Value = "=Format(\"Reporting Date: {0:ddMMMyyyy}\", Now())";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0124424695968628D), Telerik.Reporting.Drawing.Unit.Inch(0.79992109537124634D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.pictureBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.5D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.crosstab1});
            this.detail.Name = "detail";
            // 
            // crosstab1
            // 
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.1250004768371582D)));
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.1250004768371582D)));
            this.crosstab1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.35000002384185791D)));
            this.crosstab1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.35000002384185791D)));
            this.crosstab1.Body.SetCellContent(0, 0, this.textBox9);
            this.crosstab1.Body.SetCellContent(0, 1, this.textBox10);
            this.crosstab1.Body.SetCellContent(1, 0, this.textBox13);
            this.crosstab1.Body.SetCellContent(1, 1, this.textBox14);
            tableGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.confidenceScore"));
            tableGroup1.Name = "confidenceScore1";
            tableGroup1.ReportItem = this.textBox1;
            tableGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.confidenceScore", Telerik.Reporting.SortDirection.Asc));
            tableGroup2.ReportItem = this.textBox2;
            this.crosstab1.ColumnGroups.Add(tableGroup1);
            this.crosstab1.ColumnGroups.Add(tableGroup2);
            this.crosstab1.Corner.SetCellContent(0, 0, this.textBox7);
            this.crosstab1.Corner.SetCellContent(0, 1, this.textBox8);
            this.crosstab1.DataSource = this.GrdConfidenceGetScore;
            this.crosstab1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox13,
            this.textBox14,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox6});
            this.crosstab1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9354959881165996E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.crosstab1.Name = "crosstab1";
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.siteName"));
            tableGroup4.Name = "siteName1";
            tableGroup4.ReportItem = this.textBox4;
            tableGroup4.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.siteName", Telerik.Reporting.SortDirection.Asc));
            tableGroup3.ChildGroups.Add(tableGroup4);
            tableGroup3.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.siteID"));
            tableGroup3.Name = "siteID1";
            tableGroup3.ReportItem = this.textBox3;
            tableGroup3.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.siteID", Telerik.Reporting.SortDirection.Asc));
            tableGroup5.ReportItem = this.textBox6;
            this.crosstab1.RowGroups.Add(tableGroup3);
            this.crosstab1.RowGroups.Add(tableGroup5);
            this.crosstab1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.3583340644836426D), Telerik.Reporting.Drawing.Unit.Inch(1.0500000715255737D));
            this.crosstab1.StyleName = "Normal.TableNormal";
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox9.Style.Font.Name = "Segoe UI Semilight";
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.StyleName = "Normal.TableBody";
            this.textBox9.Value = "=Count(Fields.confidenceScore)";
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox10.Style.Font.Name = "Segoe UI Semilight";
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.StyleName = "Normal.GrandTotal";
            this.textBox10.Value = "=Count(Fields.confidenceScore)";
            // 
            // textBox13
            // 
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox13.Style.Font.Name = "Segoe UI Semilight";
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.StyleName = "Normal.GrandTotal";
            this.textBox13.Value = "=Count(Fields.confidenceScore)";
            // 
            // textBox14
            // 
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox14.Style.Font.Name = "Segoe UI Semilight";
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.StyleName = "Normal.GrandTotal";
            this.textBox14.Value = "=Count(Fields.confidenceScore)";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.98333334922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Segoe UI Semilight";
            this.textBox7.StyleName = "Normal.TableHeader";
            this.textBox7.Value = "Site ID";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.125D), Telerik.Reporting.Drawing.Unit.Inch(0.34999999403953552D));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Segoe UI Semilight";
            this.textBox8.StyleName = "Normal.TableHeader";
            this.textBox8.Value = "Site Name";
            // 
            // GrdConfidenceGetScore
            // 
            this.GrdConfidenceGetScore.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=excelsiorqa-db;Integrated Security=True";
            this.GrdConfidenceGetScore.Name = "GrdConfidenceGetScore";
            this.GrdConfidenceGetScore.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GrdConfidenceGetScore.ObjectContextMember = "GrdConfidenceGetScore";
            this.GrdConfidenceGetScore.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "=Parameters.trialID.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("procedure", typeof(string), "=Parameters.ProcedureList.Value")});
            this.GrdConfidenceGetScore.ProviderName = "System.Data.SqlClient";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.39999997615814209D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // GetTrial
            // 
            this.GetTrial.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=excelsiorqa-db;Integrated Security=True";
            this.GetTrial.Name = "GetTrial";
            this.GetTrial.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetTrial.ObjectContextMember = "GetTrial";
            this.GetTrial.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "=Parameters.trialID.Value")});
            this.GetTrial.ProviderName = "System.Data.SqlClient";
            // 
            // GrdConfidence
            // 
            this.DataSource = this.GetTrial;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "GrdConfidence";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "trialID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.AvailableValues.DataSource = this.GetProcedureList;
            reportParameter2.AvailableValues.ValueMember = "= Fields.Item";
            reportParameter2.Name = "ProcedureList";
            reportParameter2.Text = "Procedures";
            reportParameter2.Value = "= Fields.Item";
            reportParameter2.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Normal.TableNormal")});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableGroup")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.GrandTotal")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Bold = true;
            styleRule4.Style.Font.Italic = false;
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule4.Style.Font.Strikeout = false;
            styleRule4.Style.Font.Underline = false;
            descendantSelector3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.SubTotal")});
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector3});
            styleRule5.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            descendantSelector4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableHeader")});
            styleRule6.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector4});
            styleRule6.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule6.Style.Font.Name = "Tahoma";
            styleRule6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            descendantSelector5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableBody")});
            styleRule7.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector5});
            styleRule7.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule7.Style.Font.Name = "Tahoma";
            styleRule7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5,
            styleRule6,
            styleRule7});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.Crosstab crosstab1;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.OpenAccessDataSource GrdConfidenceGetScore;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.OpenAccessDataSource GetTrial;
        private Telerik.Reporting.OpenAccessDataSource GetProcedureList;
    }
}