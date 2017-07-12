namespace Excelsior.Reports.Certificates
{
    partial class SiteCertificationStatus
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteCertificationStatus));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector3 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.GetSites = new Telerik.Reporting.OpenAccessDataSource();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox55 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.crosstab1 = new Telerik.Reporting.Crosstab();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.GetSiteCertificationStatus = new Telerik.Reporting.OpenAccessDataSource();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.GetTrial = new Telerik.Reporting.OpenAccessDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox4.Style.Font.Name = "Segoe UI Light";
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.StyleName = "Normal.TableGroup";
            this.textBox4.Value = "=Fields.procedure";
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox3.Style.Font.Name = "Segoe UI Light";
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "Normal.TableGroup";
            this.textBox3.Value = "=Fields.techorequipment";
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1833333969116211D), Telerik.Reporting.Drawing.Unit.Inch(0.20000001788139343D));
            this.textBox6.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox6.Style.Font.Name = "Segoe UI Light";
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.StyleName = "Normal.TableGroup";
            this.textBox6.Value = "=Fields.siteName";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox5.Style.Font.Name = "Segoe UI Light";
            this.textBox5.StyleName = "Normal.TableGroup";
            this.textBox5.Value = "=Fields.siteRandomizedID";
            // 
            // GetSites
            // 
            this.GetSites.Name = "GetSites";
            this.GetSites.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetSites.ObjectContextMember = "GetSites";
            this.GetSites.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "=Parameters.trialID.Value")});
            this.GetSites.ProviderName = "System.Data.SqlClient";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.textBox1,
            this.textBox2,
            this.textBox55});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
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
            // textBox1
            // 
            this.textBox1.KeepTogether = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0603922605514526D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Segoe UI Semilight";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "Title";
            this.textBox1.Value = "Study:";
            // 
            // textBox2
            // 
            this.textBox2.KeepTogether = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2916667461395264D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.2395291328430176D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Segoe UI Semilight";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "= Fields.TrialName";
            // 
            // textBox55
            // 
            this.textBox55.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D));
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox55.Style.Color = System.Drawing.Color.Gray;
            this.textBox55.Style.Font.Name = "Segoe UI";
            this.textBox55.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox55.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox55.StyleName = "Title";
            this.textBox55.Value = "=Format(\"Reporting Date: {0:ddMMMyyyy}\", Now())";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.crosstab1});
            this.detail.Name = "detail";
            // 
            // crosstab1
            // 
            this.crosstab1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D)));
            this.crosstab1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D)));
            this.crosstab1.Body.SetCellContent(0, 0, this.textBox9);
            tableGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.procedure"));
            tableGroup2.Name = "procedure1";
            tableGroup2.ReportItem = this.textBox4;
            tableGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.procedure", Telerik.Reporting.SortDirection.Asc));
            tableGroup1.ChildGroups.Add(tableGroup2);
            tableGroup1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.techorequipment"));
            tableGroup1.Name = "techorequipment1";
            tableGroup1.ReportItem = this.textBox3;
            tableGroup1.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.techorequipment", Telerik.Reporting.SortDirection.Asc));
            this.crosstab1.ColumnGroups.Add(tableGroup1);
            this.crosstab1.Corner.SetCellContent(0, 0, this.textBox7, 2, 1);
            this.crosstab1.Corner.SetCellContent(0, 1, this.textBox8, 2, 1);
            this.crosstab1.DataSource = this.GetSiteCertificationStatus;
            this.crosstab1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox6});
            this.crosstab1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.crosstab1.Name = "crosstab1";
            tableGroup4.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.siteName"));
            tableGroup4.Name = "siteName1";
            tableGroup4.ReportItem = this.textBox6;
            tableGroup4.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.siteName", Telerik.Reporting.SortDirection.Asc));
            tableGroup3.ChildGroups.Add(tableGroup4);
            tableGroup3.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.siteRandomizedID"));
            tableGroup3.Name = "siteRandomizedID1";
            tableGroup3.ReportItem = this.textBox5;
            tableGroup3.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.siteRandomizedID", Telerik.Reporting.SortDirection.Asc));
            this.crosstab1.RowGroups.Add(tableGroup3);
            this.crosstab1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7833333015441895D), Telerik.Reporting.Drawing.Unit.Inch(0.59999996423721313D));
            this.crosstab1.StyleName = "Normal.TableNormal";
            // 
            // textBox9
            // 
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("= Fields.statusReport", Telerik.Reporting.FilterOperator.Equal, "No"));
            formattingRule1.Style.Color = System.Drawing.Color.Red;
            formattingRule1.Style.Font.Bold = true;
            formattingRule1.Style.Font.Name = "Segoe UI";
            formattingRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox9.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox9.Style.Font.Name = "Segoe UI Light";
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.StyleName = "Normal.TableBody";
            this.textBox9.Value = "= Fields.statusReport";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D), Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D));
            this.textBox7.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox7.Style.Font.Name = "Segoe UI Light";
            this.textBox7.StyleName = "Normal.TableHeader";
            this.textBox7.Value = "Site ID";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1833333969116211D), Telerik.Reporting.Drawing.Unit.Inch(0.39999997615814209D));
            this.textBox8.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox8.Style.Font.Name = "Segoe UI Light";
            this.textBox8.StyleName = "Normal.TableHeader";
            this.textBox8.Value = "Site Name";
            // 
            // GetSiteCertificationStatus
            // 
            this.GetSiteCertificationStatus.Name = "GetSiteCertificationStatus";
            this.GetSiteCertificationStatus.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetSiteCertificationStatus.ObjectContextMember = "GetSiteCertificationStatus";
            this.GetSiteCertificationStatus.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "=Parameters.trialID.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("siteID", typeof(System.Array), "=Parameters.SiteID.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("completedOnly", typeof(bool), "=Parameters.CertCompleted.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("queryPending", typeof(bool), "=Parameters.QueryPending.Value")});
            this.GetSiteCertificationStatus.ProviderName = "System.Data.SqlClient";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // GetTrial
            // 
            this.GetTrial.Name = "GetTrial";
            this.GetTrial.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.GetTrial.ObjectContextMember = "GetTrial";
            this.GetTrial.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("trialID", typeof(long), "=Parameters.trialID.Value")});
            this.GetTrial.ProviderName = "System.Data.SqlClient";
            // 
            // SiteCertificationStatus
            // 
            this.DataSource = this.GetTrial;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "SiteCertificationStatus";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "trialID";
            reportParameter1.Text = "trialID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.AllowNull = true;
            reportParameter2.AvailableValues.DataSource = this.GetSites;
            reportParameter2.AvailableValues.DisplayMember = "= Fields.RandomizedSiteID";
            reportParameter2.AvailableValues.ValueMember = "= Fields.SiteID";
            reportParameter2.MultiValue = true;
            reportParameter2.Name = "SiteID";
            reportParameter2.Text = "Sites";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Value = "= Fields.SiteID";
            reportParameter2.Visible = true;
            reportParameter3.Name = "CertCompleted";
            reportParameter3.Text = "Certification Completed Only?";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Boolean;
            reportParameter3.Value = "= False";
            reportParameter3.Visible = true;
            reportParameter4.Name = "QueryPending";
            reportParameter4.Text = "Query Pending?";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Boolean;
            reportParameter4.Value = "= False";
            reportParameter4.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
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
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableHeader")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableGroup")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableBody")});
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector3});
            styleRule5.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule5.Style.Font.Name = "Tahoma";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4000000953674316D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.OpenAccessDataSource GetTrial;
        private Telerik.Reporting.TextBox textBox55;
        private Telerik.Reporting.OpenAccessDataSource GetSites;
        private Telerik.Reporting.OpenAccessDataSource GetSiteCertificationStatus;
        private Telerik.Reporting.Crosstab crosstab1;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
    }
}