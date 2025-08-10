namespace BETask.Views
{
    partial class CustomerAssetForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpAgreementCalculator = new System.Windows.Forms.GroupBox();
            this.txtAgreement = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTaxRate = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dgAsset = new System.Windows.Forms.DataGridView();
            this.clmAssetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateGiven = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmReturnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPerMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAssetDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUpdate = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmCloseItem = new System.Windows.Forms.DataGridViewLinkColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.chkSelectedOnly = new System.Windows.Forms.CheckBox();
            this.linkTaxCalculator = new System.Windows.Forms.LinkLabel();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.btnAgreement = new System.Windows.Forms.Button();
            this.linkLabelSynchronize = new System.Windows.Forms.LinkLabel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnUpdateDates = new System.Windows.Forms.Button();
            this.syncCustomer = new System.Windows.Forms.LinkLabel();
            this.cmbLoadType = new System.Windows.Forms.ComboBox();
            this.linkClose = new System.Windows.Forms.LinkLabel();
            this.lblAgreement = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtPerMonth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpAgreementTo = new System.Windows.Forms.DateTimePicker();
            this.dtpAgreementFrom = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbReturnType = new System.Windows.Forms.ComboBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAssetAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.txtOtherAssetDetails = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpGivenDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlMain.SuspendLayout();
            this.grpAgreementCalculator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAsset)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpAgreementCalculator);
            this.pnlMain.Controls.Add(this.dgAsset);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1080, 641);
            this.pnlMain.TabIndex = 0;
            // 
            // grpAgreementCalculator
            // 
            this.grpAgreementCalculator.BackColor = System.Drawing.SystemColors.Info;
            this.grpAgreementCalculator.Controls.Add(this.txtAgreement);
            this.grpAgreementCalculator.Controls.Add(this.label13);
            this.grpAgreementCalculator.Controls.Add(this.txtTaxRate);
            this.grpAgreementCalculator.Controls.Add(this.label14);
            this.grpAgreementCalculator.Controls.Add(this.txtTaxAmount);
            this.grpAgreementCalculator.Controls.Add(this.label15);
            this.grpAgreementCalculator.Controls.Add(this.txtRate);
            this.grpAgreementCalculator.Controls.Add(this.label16);
            this.grpAgreementCalculator.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.grpAgreementCalculator.Location = new System.Drawing.Point(262, 376);
            this.grpAgreementCalculator.Name = "grpAgreementCalculator";
            this.grpAgreementCalculator.Size = new System.Drawing.Size(444, 101);
            this.grpAgreementCalculator.TabIndex = 52;
            this.grpAgreementCalculator.TabStop = false;
            this.grpAgreementCalculator.Text = "Calculate Rate";
            this.grpAgreementCalculator.Visible = false;
            // 
            // txtAgreement
            // 
            this.txtAgreement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAgreement.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtAgreement.Location = new System.Drawing.Point(326, 50);
            this.txtAgreement.Margin = new System.Windows.Forms.Padding(4);
            this.txtAgreement.Name = "txtAgreement";
            this.txtAgreement.ReadOnly = true;
            this.txtAgreement.Size = new System.Drawing.Size(83, 25);
            this.txtAgreement.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label13.Location = new System.Drawing.Point(322, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 19);
            this.label13.TabIndex = 7;
            this.label13.Text = "Agreement";
            // 
            // txtTaxRate
            // 
            this.txtTaxRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtTaxRate.Location = new System.Drawing.Point(110, 50);
            this.txtTaxRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxRate.Name = "txtTaxRate";
            this.txtTaxRate.Size = new System.Drawing.Size(50, 25);
            this.txtTaxRate.TabIndex = 6;
            this.txtTaxRate.Text = "5";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label14.Location = new System.Drawing.Point(106, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 19);
            this.label14.TabIndex = 5;
            this.label14.Text = "TaxRate";
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxAmount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtTaxAmount.Location = new System.Drawing.Point(235, 50);
            this.txtTaxAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.ReadOnly = true;
            this.txtTaxAmount.Size = new System.Drawing.Size(83, 25);
            this.txtTaxAmount.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label15.Location = new System.Drawing.Point(231, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 19);
            this.label15.TabIndex = 2;
            this.label15.Text = "Tax Amount";
            // 
            // txtRate
            // 
            this.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtRate.Location = new System.Drawing.Point(19, 50);
            this.txtRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(83, 25);
            this.txtRate.TabIndex = 4;
            this.txtRate.TextChanged += new System.EventHandler(this.txtRate_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label16.Location = new System.Drawing.Point(15, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 19);
            this.label16.TabIndex = 2;
            this.label16.Text = "Rate WithTax";
            // 
            // dgAsset
            // 
            this.dgAsset.AllowUserToAddRows = false;
            this.dgAsset.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgAsset.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgAsset.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAsset.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAsset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmAssetId,
            this.clmAssetName,
            this.clmQty,
            this.clmRate,
            this.clmBarcode,
            this.clmDateGiven,
            this.clmReturnType,
            this.clmPerMonth,
            this.clmAssetDetails,
            this.clmUpdate,
            this.clmCloseItem});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAsset.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAsset.Location = new System.Drawing.Point(0, 296);
            this.dgAsset.Name = "dgAsset";
            this.dgAsset.RowHeadersVisible = false;
            this.dgAsset.RowHeadersWidth = 51;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgAsset.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgAsset.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgAsset.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgAsset.RowTemplate.Height = 35;
            this.dgAsset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAsset.Size = new System.Drawing.Size(1080, 278);
            this.dgAsset.TabIndex = 3;
            this.dgAsset.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAsset_CellClick);
            // 
            // clmAssetId
            // 
            this.clmAssetId.HeaderText = "AssetId";
            this.clmAssetId.MinimumWidth = 6;
            this.clmAssetId.Name = "clmAssetId";
            this.clmAssetId.ReadOnly = true;
            this.clmAssetId.Visible = false;
            this.clmAssetId.Width = 50;
            // 
            // clmAssetName
            // 
            this.clmAssetName.HeaderText = "Asset Name";
            this.clmAssetName.MinimumWidth = 6;
            this.clmAssetName.Name = "clmAssetName";
            this.clmAssetName.ReadOnly = true;
            this.clmAssetName.Width = 200;
            // 
            // clmQty
            // 
            this.clmQty.HeaderText = "Qty";
            this.clmQty.MinimumWidth = 6;
            this.clmQty.Name = "clmQty";
            this.clmQty.ReadOnly = true;
            this.clmQty.Width = 50;
            // 
            // clmRate
            // 
            this.clmRate.HeaderText = "Rate";
            this.clmRate.MinimumWidth = 6;
            this.clmRate.Name = "clmRate";
            this.clmRate.ReadOnly = true;
            this.clmRate.Width = 50;
            // 
            // clmBarcode
            // 
            this.clmBarcode.HeaderText = "Serial";
            this.clmBarcode.MinimumWidth = 6;
            this.clmBarcode.Name = "clmBarcode";
            // 
            // clmDateGiven
            // 
            this.clmDateGiven.HeaderText = "Date";
            this.clmDateGiven.MinimumWidth = 6;
            this.clmDateGiven.Name = "clmDateGiven";
            this.clmDateGiven.ReadOnly = true;
            this.clmDateGiven.Width = 80;
            // 
            // clmReturnType
            // 
            this.clmReturnType.HeaderText = "Mode";
            this.clmReturnType.MinimumWidth = 6;
            this.clmReturnType.Name = "clmReturnType";
            this.clmReturnType.ReadOnly = true;
            this.clmReturnType.Width = 60;
            // 
            // clmPerMonth
            // 
            this.clmPerMonth.HeaderText = "PerM";
            this.clmPerMonth.Name = "clmPerMonth";
            this.clmPerMonth.Width = 50;
            // 
            // clmAssetDetails
            // 
            this.clmAssetDetails.HeaderText = "AssetDetails";
            this.clmAssetDetails.Name = "clmAssetDetails";
            this.clmAssetDetails.Visible = false;
            // 
            // clmUpdate
            // 
            this.clmUpdate.HeaderText = "Update";
            this.clmUpdate.Name = "clmUpdate";
            this.clmUpdate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmUpdate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clmCloseItem
            // 
            this.clmCloseItem.HeaderText = "Close";
            this.clmCloseItem.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.clmCloseItem.Name = "clmCloseItem";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.chkSelectedOnly);
            this.pnlBottom.Controls.Add(this.linkTaxCalculator);
            this.pnlBottom.Controls.Add(this.cmbRoute);
            this.pnlBottom.Controls.Add(this.btnAgreement);
            this.pnlBottom.Controls.Add(this.linkLabelSynchronize);
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 574);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1080, 67);
            this.pnlBottom.TabIndex = 1;
            // 
            // chkSelectedOnly
            // 
            this.chkSelectedOnly.AutoSize = true;
            this.chkSelectedOnly.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkSelectedOnly.Location = new System.Drawing.Point(201, 34);
            this.chkSelectedOnly.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectedOnly.Name = "chkSelectedOnly";
            this.chkSelectedOnly.Size = new System.Drawing.Size(150, 29);
            this.chkSelectedOnly.TabIndex = 53;
            this.chkSelectedOnly.Text = "Print Selected";
            this.chkSelectedOnly.UseVisualStyleBackColor = true;
            // 
            // linkTaxCalculator
            // 
            this.linkTaxCalculator.AutoSize = true;
            this.linkTaxCalculator.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkTaxCalculator.LinkColor = System.Drawing.Color.Black;
            this.linkTaxCalculator.Location = new System.Drawing.Point(196, 5);
            this.linkTaxCalculator.Name = "linkTaxCalculator";
            this.linkTaxCalculator.Size = new System.Drawing.Size(188, 25);
            this.linkTaxCalculator.TabIndex = 35;
            this.linkTaxCalculator.TabStop = true;
            this.linkTaxCalculator.Text = "Calculate Agreement";
            this.linkTaxCalculator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTaxCalculator_LinkClicked);
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(18, 15);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(157, 33);
            this.cmbRoute.TabIndex = 34;
            // 
            // btnAgreement
            // 
            this.btnAgreement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgreement.BackColor = System.Drawing.Color.Purple;
            this.btnAgreement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgreement.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgreement.ForeColor = System.Drawing.Color.White;
            this.btnAgreement.Location = new System.Drawing.Point(465, 8);
            this.btnAgreement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgreement.Name = "btnAgreement";
            this.btnAgreement.Size = new System.Drawing.Size(142, 47);
            this.btnAgreement.TabIndex = 5;
            this.btnAgreement.Text = "&Agreement";
            this.btnAgreement.UseVisualStyleBackColor = false;
            this.btnAgreement.Click += new System.EventHandler(this.ButtonEvents);
            this.btnAgreement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // linkLabelSynchronize
            // 
            this.linkLabelSynchronize.AutoSize = true;
            this.linkLabelSynchronize.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkLabelSynchronize.Location = new System.Drawing.Point(174, 23);
            this.linkLabelSynchronize.Name = "linkLabelSynchronize";
            this.linkLabelSynchronize.Size = new System.Drawing.Size(16, 25);
            this.linkLabelSynchronize.TabIndex = 2;
            this.linkLabelSynchronize.TabStop = true;
            this.linkLabelSynchronize.Text = ".";
            this.linkLabelSynchronize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(612, 8);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            this.btnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(950, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(836, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(723, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            this.btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Window;
            this.pnlTop.Controls.Add(this.btnUpdateDates);
            this.pnlTop.Controls.Add(this.syncCustomer);
            this.pnlTop.Controls.Add(this.cmbLoadType);
            this.pnlTop.Controls.Add(this.linkClose);
            this.pnlTop.Controls.Add(this.lblAgreement);
            this.pnlTop.Controls.Add(this.linkLabel1);
            this.pnlTop.Controls.Add(this.txtPerMonth);
            this.pnlTop.Controls.Add(this.label12);
            this.pnlTop.Controls.Add(this.dtpAgreementTo);
            this.pnlTop.Controls.Add(this.dtpAgreementFrom);
            this.pnlTop.Controls.Add(this.label10);
            this.pnlTop.Controls.Add(this.label11);
            this.pnlTop.Controls.Add(this.label9);
            this.pnlTop.Controls.Add(this.cmbReturnType);
            this.pnlTop.Controls.Add(this.txtQty);
            this.pnlTop.Controls.Add(this.label8);
            this.pnlTop.Controls.Add(this.txtAssetAmount);
            this.pnlTop.Controls.Add(this.label7);
            this.pnlTop.Controls.Add(this.cmbEmployee);
            this.pnlTop.Controls.Add(this.txtOtherAssetDetails);
            this.pnlTop.Controls.Add(this.label6);
            this.pnlTop.Controls.Add(this.label5);
            this.pnlTop.Controls.Add(this.dtpGivenDate);
            this.pnlTop.Controls.Add(this.label4);
            this.pnlTop.Controls.Add(this.txtBarcode);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.cmbItemName);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.cmbCustomerName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1080, 296);
            this.pnlTop.TabIndex = 0;
            // 
            // btnUpdateDates
            // 
            this.btnUpdateDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateDates.BackColor = System.Drawing.Color.Teal;
            this.btnUpdateDates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDates.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDates.ForeColor = System.Drawing.Color.White;
            this.btnUpdateDates.Location = new System.Drawing.Point(799, 250);
            this.btnUpdateDates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdateDates.Name = "btnUpdateDates";
            this.btnUpdateDates.Size = new System.Drawing.Size(258, 41);
            this.btnUpdateDates.TabIndex = 52;
            this.btnUpdateDates.Text = "&Update dates";
            this.btnUpdateDates.UseVisualStyleBackColor = false;
            this.btnUpdateDates.Visible = false;
            this.btnUpdateDates.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // syncCustomer
            // 
            this.syncCustomer.AutoSize = true;
            this.syncCustomer.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.syncCustomer.Location = new System.Drawing.Point(514, 52);
            this.syncCustomer.Name = "syncCustomer";
            this.syncCustomer.Size = new System.Drawing.Size(50, 25);
            this.syncCustomer.TabIndex = 51;
            this.syncCustomer.TabStop = true;
            this.syncCustomer.Text = "Sync";
            this.syncCustomer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.syncCustomer_LinkClicked);
            // 
            // cmbLoadType
            // 
            this.cmbLoadType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLoadType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadType.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbLoadType.FormattingEnabled = true;
            this.cmbLoadType.Location = new System.Drawing.Point(533, 221);
            this.cmbLoadType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLoadType.Name = "cmbLoadType";
            this.cmbLoadType.Size = new System.Drawing.Size(233, 33);
            this.cmbLoadType.TabIndex = 50;
            this.cmbLoadType.SelectedIndexChanged += new System.EventHandler(this.cmbLoadType_SelectedIndexChanged);
            // 
            // linkClose
            // 
            this.linkClose.AutoSize = true;
            this.linkClose.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.linkClose.LinkColor = System.Drawing.Color.Maroon;
            this.linkClose.Location = new System.Drawing.Point(822, 223);
            this.linkClose.Name = "linkClose";
            this.linkClose.Size = new System.Drawing.Size(163, 25);
            this.linkClose.TabIndex = 49;
            this.linkClose.TabStop = true;
            this.linkClose.Text = "Close Agreement";
            this.linkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClose_LinkClicked);
            // 
            // lblAgreement
            // 
            this.lblAgreement.AutoSize = true;
            this.lblAgreement.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.lblAgreement.Location = new System.Drawing.Point(1005, 223);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(25, 25);
            this.lblAgreement.TabIndex = 48;
            this.lblAgreement.Text = "A";
            this.lblAgreement.Click += new System.EventHandler(this.lblAgreement_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkLabel1.Location = new System.Drawing.Point(514, 14);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(34, 25);
            this.linkLabel1.TabIndex = 46;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "All";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // txtPerMonth
            // 
            this.txtPerMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerMonth.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPerMonth.Location = new System.Drawing.Point(418, 137);
            this.txtPerMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPerMonth.MaxLength = 50;
            this.txtPerMonth.Name = "txtPerMonth";
            this.txtPerMonth.Size = new System.Drawing.Size(90, 31);
            this.txtPerMonth.TabIndex = 45;
            this.txtPerMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label12.Location = new System.Drawing.Point(311, 137);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 25);
            this.label12.TabIndex = 44;
            this.label12.Text = "Per Month";
            // 
            // dtpAgreementTo
            // 
            this.dtpAgreementTo.CustomFormat = "dd/MM/yyyy";
            this.dtpAgreementTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpAgreementTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAgreementTo.Location = new System.Drawing.Point(759, 180);
            this.dtpAgreementTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtpAgreementTo.Name = "dtpAgreementTo";
            this.dtpAgreementTo.Size = new System.Drawing.Size(288, 31);
            this.dtpAgreementTo.TabIndex = 43;
            // 
            // dtpAgreementFrom
            // 
            this.dtpAgreementFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpAgreementFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpAgreementFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAgreementFrom.Location = new System.Drawing.Point(179, 179);
            this.dtpAgreementFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpAgreementFrom.Name = "dtpAgreementFrom";
            this.dtpAgreementFrom.Size = new System.Drawing.Size(329, 31);
            this.dtpAgreementFrom.TabIndex = 42;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label10.Location = new System.Drawing.Point(602, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 25);
            this.label10.TabIndex = 41;
            this.label10.Text = "Agreement To";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label11.Location = new System.Drawing.Point(14, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 25);
            this.label11.TabIndex = 40;
            this.label11.Text = "Agreement From";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(602, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 25);
            this.label9.TabIndex = 37;
            this.label9.Text = "Tran Mode";
            // 
            // cmbReturnType
            // 
            this.cmbReturnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReturnType.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbReturnType.FormattingEnabled = true;
            this.cmbReturnType.Items.AddRange(new object[] {
            "Delivery",
            "Return",
            "Repair",
            "Repaired"});
            this.cmbReturnType.Location = new System.Drawing.Point(760, 133);
            this.cmbReturnType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReturnType.Name = "cmbReturnType";
            this.cmbReturnType.Size = new System.Drawing.Size(287, 33);
            this.cmbReturnType.TabIndex = 7;
            this.cmbReturnType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtQty.Location = new System.Drawing.Point(179, 135);
            this.txtQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtQty.MaxLength = 50;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(116, 31);
            this.txtQty.TabIndex = 6;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            this.txtQty.Validated += new System.EventHandler(this.txtQty_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(14, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 25);
            this.label8.TabIndex = 34;
            this.label8.Text = "Qty";
            // 
            // txtAssetAmount
            // 
            this.txtAssetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAssetAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtAssetAmount.Location = new System.Drawing.Point(759, 50);
            this.txtAssetAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAssetAmount.MaxLength = 50;
            this.txtAssetAmount.Name = "txtAssetAmount";
            this.txtAssetAmount.Size = new System.Drawing.Size(288, 31);
            this.txtAssetAmount.TabIndex = 3;
            this.txtAssetAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtAssetAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(14, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 25);
            this.label7.TabIndex = 32;
            this.label7.Text = "Employee";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(179, 91);
            this.cmbEmployee.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(328, 33);
            this.cmbEmployee.TabIndex = 4;
            this.cmbEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtOtherAssetDetails
            // 
            this.txtOtherAssetDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherAssetDetails.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtOtherAssetDetails.Location = new System.Drawing.Point(179, 221);
            this.txtOtherAssetDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOtherAssetDetails.MaxLength = 50;
            this.txtOtherAssetDetails.Name = "txtOtherAssetDetails";
            this.txtOtherAssetDetails.Size = new System.Drawing.Size(329, 31);
            this.txtOtherAssetDetails.TabIndex = 10;
            this.txtOtherAssetDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(14, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 25);
            this.label6.TabIndex = 30;
            this.label6.Text = "Asset Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(602, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 25);
            this.label5.TabIndex = 26;
            this.label5.Text = "Rate";
            // 
            // dtpGivenDate
            // 
            this.dtpGivenDate.CustomFormat = "dd/MM/yyyy";
            this.dtpGivenDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpGivenDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGivenDate.Location = new System.Drawing.Point(759, 9);
            this.dtpGivenDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpGivenDate.Name = "dtpGivenDate";
            this.dtpGivenDate.Size = new System.Drawing.Size(288, 31);
            this.dtpGivenDate.TabIndex = 1;
            this.dtpGivenDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(602, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "Delivery Date";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBarcode.Location = new System.Drawing.Point(759, 93);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBarcode.MaxLength = 50;
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(288, 31);
            this.txtBarcode.TabIndex = 5;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(602, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "Serial Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(14, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Item Name";
            // 
            // cmbItemName
            // 
            this.cmbItemName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbItemName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbItemName.FormattingEnabled = true;
            this.cmbItemName.Location = new System.Drawing.Point(179, 48);
            this.cmbItemName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(328, 33);
            this.cmbItemName.TabIndex = 2;
            this.cmbItemName.SelectedIndexChanged += new System.EventHandler(this.cmbItemName_SelectedIndexChanged);
            this.cmbItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "Customer";
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomerName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(179, 9);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(328, 33);
            this.cmbCustomerName.TabIndex = 0;
            this.cmbCustomerName.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerName_SelectedIndexChanged);
            this.cmbCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // CustomerAssetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 641);
            this.Controls.Add(this.pnlMain);
            this.Name = "CustomerAssetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Asset";
            this.Load += new System.EventHandler(this.CustomerAssetForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.grpAgreementCalculator.ResumeLayout(false);
            this.grpAgreementCalculator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAsset)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.DataGridView dgAsset;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbItemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.DateTimePicker dtpGivenDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.TextBox txtOtherAssetDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAssetAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ComboBox cmbReturnType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel linkLabelSynchronize;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpAgreementTo;
        private System.Windows.Forms.DateTimePicker dtpAgreementFrom;
        private System.Windows.Forms.Button btnAgreement;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPerMonth;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblAgreement;
        private System.Windows.Forms.LinkLabel linkClose;
        private System.Windows.Forms.ComboBox cmbLoadType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAssetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateGiven;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmReturnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPerMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAssetDetails;
        private System.Windows.Forms.DataGridViewLinkColumn clmUpdate;
        private System.Windows.Forms.DataGridViewLinkColumn clmCloseItem;
        private System.Windows.Forms.LinkLabel syncCustomer;
        private System.Windows.Forms.LinkLabel linkTaxCalculator;
        private System.Windows.Forms.GroupBox grpAgreementCalculator;
        private System.Windows.Forms.TextBox txtAgreement;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTaxRate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chkSelectedOnly;
        private System.Windows.Forms.Button btnUpdateDates;
    }
}