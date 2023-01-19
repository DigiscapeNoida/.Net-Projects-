namespace PPM_TRACKING_SYSTEM
{
    
    partial class DataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataForm));
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.grpView = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbSerBook = new System.Windows.Forms.RadioButton();
            this.rdpSerAll = new System.Windows.Forms.RadioButton();
            this.rdpSerBookseries = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.optViewSignal = new System.Windows.Forms.RadioButton();
            this.optViewPPM = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grdvw = new System.Windows.Forms.DataGridView();
            this.Isbn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPMShorttitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPMDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPMOrdertype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignalCreation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignalId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanDueDate = new CalendarColumn();
            this.calendarColumn1 = new CalendarColumn();
            this.calendarColumn2 = new CalendarColumn();
            this.calendarColumn3 = new CalendarColumn();
            this.calendarColumn4 = new CalendarColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarColumn5 = new CalendarColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvw)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp
            // 
            this.dtp.Location = new System.Drawing.Point(0, 0);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(200, 20);
            this.dtp.TabIndex = 0;
            // 
            // grpView
            // 
            this.grpView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpView.Controls.Add(this.groupBox1);
            this.grpView.Controls.Add(this.groupBox4);
            this.grpView.Controls.Add(this.groupBox2);
            this.grpView.Controls.Add(this.label2);
            this.grpView.Controls.Add(this.label1);
            this.grpView.Controls.Add(this.groupBox3);
            this.grpView.Controls.Add(this.textBox2);
            this.grpView.Controls.Add(this.textBox1);
            this.grpView.Controls.Add(this.grdvw);
            this.grpView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpView.Location = new System.Drawing.Point(3, 4);
            this.grpView.Name = "grpView";
            this.grpView.Size = new System.Drawing.Size(1024, 534);
            this.grpView.TabIndex = 0;
            this.grpView.TabStop = false;
            this.grpView.Text = "PPM INFORMATION";
            this.grpView.TextChanged += new System.EventHandler(this.groupBox1_TextChanged);
            this.grpView.Enter += new System.EventHandler(this.grpView_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbSerBook);
            this.groupBox1.Controls.Add(this.rdpSerAll);
            this.groupBox1.Controls.Add(this.rdpSerBookseries);
            this.groupBox1.Location = new System.Drawing.Point(319, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 39);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Series ";
            // 
            // rdbSerBook
            // 
            this.rdbSerBook.AutoSize = true;
            this.rdbSerBook.Location = new System.Drawing.Point(79, 14);
            this.rdbSerBook.Name = "rdbSerBook";
            this.rdbSerBook.Size = new System.Drawing.Size(53, 19);
            this.rdbSerBook.TabIndex = 3;
            this.rdbSerBook.Text = "Book";
            this.rdbSerBook.UseVisualStyleBackColor = true;
            this.rdbSerBook.CheckedChanged += new System.EventHandler(this.rdbSerBook_CheckedChanged);
            // 
            // rdpSerAll
            // 
            this.rdpSerAll.AutoSize = true;
            this.rdpSerAll.Checked = true;
            this.rdpSerAll.Location = new System.Drawing.Point(155, 14);
            this.rdpSerAll.Name = "rdpSerAll";
            this.rdpSerAll.Size = new System.Drawing.Size(38, 19);
            this.rdpSerAll.TabIndex = 2;
            this.rdpSerAll.TabStop = true;
            this.rdpSerAll.Text = "All";
            this.rdpSerAll.UseVisualStyleBackColor = true;
            this.rdpSerAll.CheckedChanged += new System.EventHandler(this.rdpSerAll_CheckedChanged);
            // 
            // rdpSerBookseries
            // 
            this.rdpSerBookseries.AutoSize = true;
            this.rdpSerBookseries.Location = new System.Drawing.Point(12, 14);
            this.rdpSerBookseries.Name = "rdpSerBookseries";
            this.rdpSerBookseries.Size = new System.Drawing.Size(60, 19);
            this.rdpSerBookseries.TabIndex = 0;
            this.rdpSerBookseries.Text = "Series";
            this.rdpSerBookseries.UseVisualStyleBackColor = true;
            this.rdpSerBookseries.CheckedChanged += new System.EventHandler(this.rdpSerBookseries_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.optViewSignal);
            this.groupBox4.Controls.Add(this.optViewPPM);
            this.groupBox4.Location = new System.Drawing.Point(526, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(106, 82);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " View ";
            // 
            // optViewSignal
            // 
            this.optViewSignal.AutoSize = true;
            this.optViewSignal.Location = new System.Drawing.Point(14, 57);
            this.optViewSignal.Name = "optViewSignal";
            this.optViewSignal.Size = new System.Drawing.Size(60, 19);
            this.optViewSignal.TabIndex = 1;
            this.optViewSignal.TabStop = true;
            this.optViewSignal.Text = "Signal";
            this.optViewSignal.UseVisualStyleBackColor = true;
            // 
            // optViewPPM
            // 
            this.optViewPPM.AutoSize = true;
            this.optViewPPM.Location = new System.Drawing.Point(14, 20);
            this.optViewPPM.Name = "optViewPPM";
            this.optViewPPM.Size = new System.Drawing.Size(86, 19);
            this.optViewPPM.TabIndex = 0;
            this.optViewPPM.TabStop = true;
            this.optViewPPM.Text = "PPM Order";
            this.optViewPPM.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(640, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Click for Creating Signal / Uploading ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 26);
            this.button1.TabIndex = 7;
            this.button1.Text = "&Date Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(44, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 26);
            this.button5.TabIndex = 4;
            this.button5.Text = "Create &Signal";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(44, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 26);
            this.button4.TabIndex = 3;
            this.button4.Text = "&Archive Signal";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(146, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "&Upload";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Short title";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "ISBN";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(319, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 43);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " View PPM Status ";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(156, 18);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(38, 19);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "All";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(79, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(79, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Uploaded";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Pending";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(180, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // grdvw
            // 
            this.grdvw.AllowUserToAddRows = false;
            this.grdvw.AllowUserToDeleteRows = false;
            this.grdvw.AllowUserToOrderColumns = true;
            this.grdvw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdvw.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdvw.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdvw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdvw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Isbn,
            this.PPMShorttitle,
            this.PPMDate,
            this.PPMOrdertype,
            this.SignalCreation,
            this.SignalId,
            this.STATUS,
            this.ProdSite,
            this.PlanDueDate});
            this.grdvw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grdvw.GridColor = System.Drawing.Color.Gray;
            this.grdvw.Location = new System.Drawing.Point(6, 110);
            this.grdvw.Name = "grdvw";
            this.grdvw.Size = new System.Drawing.Size(1013, 420);
            this.grdvw.TabIndex = 0;
            this.grdvw.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvw_RowEnter);
            this.grdvw.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.celldblclick);
            this.grdvw.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grdvw_RowsAdded);
            this.grdvw.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvw_CellContentClick);
            this.grdvw.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvw_CellContentClick);
            this.grdvw.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvw_CellContentClick);
            this.grdvw.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grdvw_RowsAdded);
            this.grdvw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvw_CellContentClick);
            // 
            // Isbn
            // 
            this.Isbn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Isbn.DataPropertyName = "Isbn";
            this.Isbn.FillWeight = 60F;
            this.Isbn.HeaderText = "ISBN";
            this.Isbn.Name = "Isbn";
            this.Isbn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Isbn.Width = 60;
            // 
            // PPMShorttitle
            // 
            this.PPMShorttitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PPMShorttitle.DataPropertyName = "PPMShorttitle";
            this.PPMShorttitle.FillWeight = 150F;
            this.PPMShorttitle.HeaderText = "SHORT TITLE";
            this.PPMShorttitle.Name = "PPMShorttitle";
            this.PPMShorttitle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PPMShorttitle.Width = 109;
            // 
            // PPMDate
            // 
            this.PPMDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PPMDate.DataPropertyName = "PPMCreationdate";
            this.PPMDate.FillWeight = 80F;
            this.PPMDate.HeaderText = "PPM CREATION DATE";
            this.PPMDate.Name = "PPMDate";
            this.PPMDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PPMDate.Width = 156;
            // 
            // PPMOrdertype
            // 
            this.PPMOrdertype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PPMOrdertype.DataPropertyName = "PPMOrdertype";
            this.PPMOrdertype.FillWeight = 80F;
            this.PPMOrdertype.HeaderText = "ORDER TYPE";
            this.PPMOrdertype.Name = "PPMOrdertype";
            this.PPMOrdertype.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PPMOrdertype.Width = 109;
            // 
            // SignalCreation
            // 
            this.SignalCreation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SignalCreation.DataPropertyName = "SignalCreation";
            this.SignalCreation.FillWeight = 80F;
            this.SignalCreation.HeaderText = "SIGNAL CREATION DATE";
            this.SignalCreation.Name = "SignalCreation";
            this.SignalCreation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SignalCreation.Width = 172;
            // 
            // SignalId
            // 
            this.SignalId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SignalId.DataPropertyName = "SignalId";
            this.SignalId.FillWeight = 110F;
            this.SignalId.HeaderText = "SIGNAL ID";
            this.SignalId.Name = "SignalId";
            this.SignalId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SignalId.Width = 90;
            // 
            // STATUS
            // 
            this.STATUS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.STATUS.DataPropertyName = "UploadStatus";
            this.STATUS.FillWeight = 50F;
            this.STATUS.HeaderText = "UPLOAD STATUS";
            this.STATUS.Name = "STATUS";
            this.STATUS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.STATUS.Width = 130;
            // 
            // ProdSite
            // 
            this.ProdSite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProdSite.DataPropertyName = "ProdSite";
            this.ProdSite.FillWeight = 40F;
            this.ProdSite.HeaderText = "PROD SITE";
            this.ProdSite.Name = "ProdSite";
            this.ProdSite.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProdSite.Width = 96;
            // 
            // PlanDueDate
            // 
            this.PlanDueDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PlanDueDate.DataPropertyName = "PlanDueDate";
            this.PlanDueDate.FillWeight = 70F;
            this.PlanDueDate.HeaderText = "PLAN DATE";
            this.PlanDueDate.Name = "PlanDueDate";
            this.PlanDueDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PlanDueDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PlanDueDate.Width = 48;
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.calendarColumn1.DataPropertyName = "PlanDueDate";
            this.calendarColumn1.FillWeight = 70F;
            this.calendarColumn1.HeaderText = "PLAN DATE";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.calendarColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // calendarColumn2
            // 
            this.calendarColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.calendarColumn2.DataPropertyName = "PlanDueDate";
            this.calendarColumn2.FillWeight = 70F;
            this.calendarColumn2.HeaderText = "PLAN DATE";
            this.calendarColumn2.Name = "calendarColumn2";
            this.calendarColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.calendarColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // calendarColumn3
            // 
            this.calendarColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.calendarColumn3.DataPropertyName = "PlanDueDate";
            this.calendarColumn3.FillWeight = 70F;
            this.calendarColumn3.HeaderText = "PLAN DATE";
            this.calendarColumn3.Name = "calendarColumn3";
            this.calendarColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.calendarColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // calendarColumn4
            // 
            this.calendarColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.calendarColumn4.DataPropertyName = "PlanDueDate";
            this.calendarColumn4.FillWeight = 70F;
            this.calendarColumn4.HeaderText = "PLAN DATE";
            this.calendarColumn4.Name = "calendarColumn4";
            this.calendarColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.calendarColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Isbn";
            this.dataGridViewTextBoxColumn1.FillWeight = 60F;
            this.dataGridViewTextBoxColumn1.HeaderText = "ISBN";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PPMShorttitle";
            this.dataGridViewTextBoxColumn2.FillWeight = 150F;
            this.dataGridViewTextBoxColumn2.HeaderText = "SHORT TITLE";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "PPMCreationdate";
            this.dataGridViewTextBoxColumn3.FillWeight = 80F;
            this.dataGridViewTextBoxColumn3.HeaderText = "PPM CREATION DATE";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PPMOrdertype";
            this.dataGridViewTextBoxColumn4.FillWeight = 80F;
            this.dataGridViewTextBoxColumn4.HeaderText = "ORDER TYPE";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SignalCreation";
            this.dataGridViewTextBoxColumn5.FillWeight = 80F;
            this.dataGridViewTextBoxColumn5.HeaderText = "SIGNAL CREATION DATE";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "SignalId";
            this.dataGridViewTextBoxColumn6.FillWeight = 110F;
            this.dataGridViewTextBoxColumn6.HeaderText = "SIGNAL ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "UploadStatus";
            this.dataGridViewTextBoxColumn7.FillWeight = 50F;
            this.dataGridViewTextBoxColumn7.HeaderText = "UPLOAD STATUS";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "ProdSite";
            this.dataGridViewTextBoxColumn8.FillWeight = 40F;
            this.dataGridViewTextBoxColumn8.HeaderText = "PROD SITE";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // calendarColumn5
            // 
            this.calendarColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.calendarColumn5.DataPropertyName = "PlanDueDate";
            this.calendarColumn5.FillWeight = 70F;
            this.calendarColumn5.HeaderText = "PLAN DATE";
            this.calendarColumn5.Name = "calendarColumn5";
            this.calendarColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.calendarColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel1.Text = "Rows : ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel2.Text = "Thomson Digital";
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(1028, 562);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "DataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PPM -DATA INFORMATION";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DataForm_Load);
            this.grpView.ResumeLayout(false);
            this.grpView.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdvw)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpView;
        private System.Windows.Forms.DataGridView grdvw;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton optViewSignal;
        private System.Windows.Forms.RadioButton optViewPPM;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.Button button1;
        private CalendarColumn calendarColumn2;
        private CalendarColumn calendarColumn3;
        private CalendarColumn calendarColumn4;
        private CalendarColumn calendarColumn5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbSerBook;
        private System.Windows.Forms.RadioButton rdpSerAll;
        private System.Windows.Forms.RadioButton rdpSerBookseries;
        private System.Windows.Forms.DataGridViewTextBoxColumn Isbn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPMShorttitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPMDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPMOrdertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignalCreation;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignalId;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdSite;
        private CalendarColumn PlanDueDate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}