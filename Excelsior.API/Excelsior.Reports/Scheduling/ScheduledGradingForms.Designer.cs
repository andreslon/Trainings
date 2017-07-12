namespace Excelsior.Reports.Scheduling
{
    partial class ScheduledGradingForms
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduledGradingForms));
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup7 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup8 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup9 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup10 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule6 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule7 = new Telerik.Reporting.Drawing.StyleRule();
            this.getScheduledSeries = new Telerik.Reporting.OpenAccessDataSource();
            this.pageHeaderSection = new Telerik.Reporting.PageHeaderSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.detailSection = new Telerik.Reporting.DetailSection();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // getScheduledSeries
            // 
            this.getScheduledSeries.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=EyeKor;Integrated Security=True";
            this.getScheduledSeries.Name = "getScheduledSeries";
            this.getScheduledSeries.ObjectContext = typeof(Excelsior.Domain.DataModel);
            this.getScheduledSeries.ObjectContextMember = "WF_Sequences";
            this.getScheduledSeries.ProviderName = "System.Data.SqlClient";
            // 
            // pageHeaderSection
            // 
            this.pageHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.658333420753479D);
            this.pageHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox1,
            this.textBox18,
            this.pictureBox2});
            this.pageHeaderSection.Name = "pageHeaderSection";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.37716206908226013D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.16767692565918D), Telerik.Reporting.Drawing.Unit.Inch(0.28125002980232239D));
            this.textBox6.Style.Color = System.Drawing.Color.Gray;
            this.textBox6.Style.Font.Name = "Segoe UI";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.StyleName = "Title";
            this.textBox6.Value = "=Format(\"Reporting Date: {0:ddMMMyyyy}\", Now())";
            // 
            // textBox1
            // 
            this.textBox1.KeepTogether = false;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2771623134613037D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9905152320861816D), Telerik.Reporting.Drawing.Unit.Inch(0.37708339095115662D));
            this.textBox1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Segoe UI Semilight";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "= Fields.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialName";
            // 
            // textBox18
            // 
            this.textBox18.KeepTogether = false;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1770832538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.37708339095115662D));
            this.textBox18.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(75)))), ((int)(((byte)(116)))));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Name = "Segoe UI Semilight";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.StyleName = "Title";
            this.textBox18.Value = "Study:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.pictureBox2.MimeType = "image/png";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0142409801483154D), Telerik.Reporting.Drawing.Unit.Inch(0.658333420753479D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.pictureBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // detailSection
            // 
            this.detailSection.Height = Telerik.Reporting.Drawing.Unit.Inch(7.6416668891906738D);
            this.detailSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1});
            this.detailSection.KeepTogether = false;
            this.detailSection.Name = "detailSection";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.1847803592681885D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.8357837200164795D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.96628451347351074D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.280789852142334D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.22088444232940674D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20805087685585022D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.19763441383838654D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20805118978023529D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(5.0964689254760742D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox2);
            this.table1.Body.SetCellContent(0, 1, this.textBox4);
            this.table1.Body.SetCellContent(0, 2, this.textBox3);
            this.table1.Body.SetCellContent(0, 3, this.textBox5);
            this.table1.Body.SetCellContent(1, 0, this.textBox7);
            this.table1.Body.SetCellContent(1, 2, this.textBox8);
            this.table1.Body.SetCellContent(1, 3, this.textBox9);
            this.table1.Body.SetCellContent(2, 0, this.textBox10);
            this.table1.Body.SetCellContent(2, 2, this.textBox11);
            this.table1.Body.SetCellContent(2, 3, this.textBox12);
            this.table1.Body.SetCellContent(3, 0, this.textBox13);
            this.table1.Body.SetCellContent(3, 1, this.textBox14);
            this.table1.Body.SetCellContent(3, 2, this.textBox15);
            this.table1.Body.SetCellContent(3, 3, this.textBox16);
            this.table1.Body.SetCellContent(1, 1, this.textBox17);
            this.table1.Body.SetCellContent(2, 1, this.textBox19);
            this.table1.Body.SetCellContent(4, 0, this.pictureBox1, 1, 4);
            tableGroup1.Name = "tableGroup";
            tableGroup2.Name = "tableGroup1";
            tableGroup3.Name = "tableGroup2";
            tableGroup4.Name = "group";
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.ColumnGroups.Add(tableGroup4);
            this.table1.DataSource = this.getScheduledSeries;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox4,
            this.textBox3,
            this.textBox5,
            this.textBox7,
            this.textBox17,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox19,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.pictureBox1});
            this.table1.KeepTogether = true;
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.00015743573021609336D));
            this.table1.Name = "table1";
            tableGroup6.Name = "group1";
            tableGroup7.Name = "group2";
            tableGroup8.Name = "group3";
            tableGroup9.Name = "group4";
            tableGroup10.Name = "group5";
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.ChildGroups.Add(tableGroup7);
            tableGroup5.ChildGroups.Add(tableGroup8);
            tableGroup5.ChildGroups.Add(tableGroup9);
            tableGroup5.ChildGroups.Add(tableGroup10);
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup5.Name = "detailTableGroup";
            this.table1.RowGroups.Add(tableGroup5);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.2676386833190918D), Telerik.Reporting.Drawing.Unit.Inch(5.9310903549194336D));
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1847803592681885D), Telerik.Reporting.Drawing.Unit.Inch(0.22088445723056793D));
            this.textBox2.Value = "Subject ID:";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8357839584350586D), Telerik.Reporting.Drawing.Unit.Inch(0.22088445723056793D));
            this.textBox4.Value = "=Fields.PACSTimePoint.PASCSubject.RandomizedSubjectID";
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96628451347351074D), Telerik.Reporting.Drawing.Unit.Inch(0.22088445723056793D));
            this.textBox3.Value = "Group:";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2807900905609131D), Telerik.Reporting.Drawing.Unit.Inch(0.22088445723056793D));
            this.textBox5.StyleName = "";
            this.textBox5.Value = "= Fields.PACSTimePoint.PACSSubject.PACSSubjectGroup.GroupName";
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1847803592681885D), Telerik.Reporting.Drawing.Unit.Inch(0.2080509215593338D));
            this.textBox7.StyleName = "";
            this.textBox7.Value = "Alternative ID:";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96628451347351074D), Telerik.Reporting.Drawing.Unit.Inch(0.2080509215593338D));
            this.textBox8.StyleName = "";
            this.textBox8.Value = "Study Eye:";
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2807900905609131D), Telerik.Reporting.Drawing.Unit.Inch(0.2080509215593338D));
            this.textBox9.StyleName = "";
            this.textBox9.Value = "= Fields.PACSTimePoint.PACSSubject.Laterality";
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.184780478477478D), Telerik.Reporting.Drawing.Unit.Inch(0.19763439893722534D));
            this.textBox10.StyleName = "";
            this.textBox10.Value = "Name Code";
            // 
            // textBox11
            // 
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96628451347351074D), Telerik.Reporting.Drawing.Unit.Inch(0.19763439893722534D));
            this.textBox11.StyleName = "";
            this.textBox11.Value = "Study Date:";
            // 
            // textBox12
            // 
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2807900905609131D), Telerik.Reporting.Drawing.Unit.Inch(0.19763439893722534D));
            this.textBox12.StyleName = "";
            this.textBox12.Value = "=Format(\"{0:ddMMMyyyy}\", Fields.StudyDate)";
            // 
            // textBox13
            // 
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1847803592681885D), Telerik.Reporting.Drawing.Unit.Inch(0.20805126428604126D));
            this.textBox13.StyleName = "";
            this.textBox13.Value = "Time Point:";
            // 
            // textBox14
            // 
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8357839584350586D), Telerik.Reporting.Drawing.Unit.Inch(0.20805126428604126D));
            this.textBox14.StyleName = "";
            this.textBox14.Value = "=Fields.PACSTimePoint.PACSTimePointsList.TimePointsDescription";
            // 
            // textBox15
            // 
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.96628451347351074D), Telerik.Reporting.Drawing.Unit.Inch(0.20805126428604126D));
            this.textBox15.StyleName = "";
            this.textBox15.Value = "Procedure:";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2807900905609131D), Telerik.Reporting.Drawing.Unit.Inch(0.20805126428604126D));
            this.textBox16.StyleName = "";
            this.textBox16.Value = "= Fields.PACSTPProcList.CERTImgProcedureList.ImgProcedureName";
            // 
            // textBox17
            // 
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8357839584350586D), Telerik.Reporting.Drawing.Unit.Inch(0.2080509215593338D));
            this.textBox17.Value = "=Fields.PACSTimePoint.PASCSubject.AlternativeRandomizedSubjectID";
            // 
            // textBox19
            // 
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8357839584350586D), Telerik.Reporting.Drawing.Unit.Inch(0.19763439893722534D));
            this.textBox19.Value = "=Fields.PACSTimePoint.PASCSubject.NameCode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.26763916015625D), Telerik.Reporting.Drawing.Unit.Inch(5.0964722633361816D));
            this.pictureBox1.StyleName = "";
            // 
            // ScheduledGradingForms
            // 
            this.DataSource = this.getScheduledSeries;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection,
            this.detailSection});
            this.Name = "ProductCatalogReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule2.Style.Font.Name = "Segoe UI";
            styleRule2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(4D);
            styleRule2.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(4D);
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.TextBox), "Table.GroupHeader")});
            styleRule3.Style.Font.Bold = true;
            styleRule3.Style.Font.Name = "Cambria";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(20D);
            styleRule3.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.TextBox), "Table.SubGroupHeader")});
            styleRule4.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Bold = true;
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.TextBox), "Normal.TableHeader")});
            styleRule5.Style.Color = System.Drawing.Color.DimGray;
            styleRule5.Style.Font.Bold = false;
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule6.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.TextItemBase), "Normal.TableBody")});
            styleRule6.Style.Font.Name = "Segoe UI Semibold";
            styleRule6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            styleRule7.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.TextItemBase), "Table.GroupFooter")});
            styleRule7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5,
            styleRule6,
            styleRule7});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2677168846130371D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.OpenAccessDataSource getScheduledSeries;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection;
        private Telerik.Reporting.DetailSection detailSection;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.PictureBox pictureBox1;

    }
}