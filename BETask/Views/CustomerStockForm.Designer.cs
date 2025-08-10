namespace BETask.Views
{
    partial class CustomerStockForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlDetailed = new System.Windows.Forms.Panel();
            this.gridDetailed = new System.Windows.Forms.DataGridView();
            this.clmDet_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDet_Agreement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDet_Opening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDet_Delivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDet_Returned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDet_Closng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lnkClose = new System.Windows.Forms.LinkLabel();
            this.gridCustomers = new System.Windows.Forms.DataGridView();
            this.clmCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAgreement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOpening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDelivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmReturned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmClosing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.chkDateSearch = new System.Windows.Forms.CheckBox();
            this.grpDatesearch = new System.Windows.Forms.GroupBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblCustType = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlDetailed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetailed)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            this.panel2.SuspendLayout();
            this.grpDatesearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlDetailed);
            this.panel1.Controls.Add(this.gridCustomers);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1277, 784);
            this.panel1.TabIndex = 0;
            // 
            // pnlDetailed
            // 
            this.pnlDetailed.Controls.Add(this.gridDetailed);
            this.pnlDetailed.Controls.Add(this.panel3);
            this.pnlDetailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailed.Location = new System.Drawing.Point(0, 166);
            this.pnlDetailed.Name = "pnlDetailed";
            this.pnlDetailed.Size = new System.Drawing.Size(1277, 618);
            this.pnlDetailed.TabIndex = 6;
            this.pnlDetailed.Visible = false;
            // 
            // gridDetailed
            // 
            this.gridDetailed.AllowUserToAddRows = false;
            this.gridDetailed.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridDetailed.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridDetailed.BackgroundColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridDetailed.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridDetailed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDetailed.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDet_Date,
            this.clmDet_Agreement,
            this.clmDet_Opening,
            this.clmDet_Delivered,
            this.clmDet_Returned,
            this.clmDet_Closng});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDetailed.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridDetailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetailed.Location = new System.Drawing.Point(0, 44);
            this.gridDetailed.Name = "gridDetailed";
            this.gridDetailed.ReadOnly = true;
            this.gridDetailed.RowHeadersWidth = 100;
            this.gridDetailed.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridDetailed.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridDetailed.RowTemplate.Height = 25;
            this.gridDetailed.Size = new System.Drawing.Size(1277, 574);
            this.gridDetailed.TabIndex = 6;
            // 
            // clmDet_Date
            // 
            this.clmDet_Date.HeaderText = "Date";
            this.clmDet_Date.MinimumWidth = 6;
            this.clmDet_Date.Name = "clmDet_Date";
            this.clmDet_Date.ReadOnly = true;
            this.clmDet_Date.Width = 125;
            // 
            // clmDet_Agreement
            // 
            this.clmDet_Agreement.HeaderText = "Agreement";
            this.clmDet_Agreement.MinimumWidth = 6;
            this.clmDet_Agreement.Name = "clmDet_Agreement";
            this.clmDet_Agreement.ReadOnly = true;
            this.clmDet_Agreement.Width = 125;
            // 
            // clmDet_Opening
            // 
            this.clmDet_Opening.HeaderText = "Opening";
            this.clmDet_Opening.MinimumWidth = 6;
            this.clmDet_Opening.Name = "clmDet_Opening";
            this.clmDet_Opening.ReadOnly = true;
            this.clmDet_Opening.Width = 125;
            // 
            // clmDet_Delivered
            // 
            this.clmDet_Delivered.HeaderText = "Delivered";
            this.clmDet_Delivered.MinimumWidth = 6;
            this.clmDet_Delivered.Name = "clmDet_Delivered";
            this.clmDet_Delivered.ReadOnly = true;
            this.clmDet_Delivered.Width = 125;
            // 
            // clmDet_Returned
            // 
            this.clmDet_Returned.HeaderText = "Returned";
            this.clmDet_Returned.MinimumWidth = 6;
            this.clmDet_Returned.Name = "clmDet_Returned";
            this.clmDet_Returned.ReadOnly = true;
            this.clmDet_Returned.Width = 125;
            // 
            // clmDet_Closng
            // 
            this.clmDet_Closng.HeaderText = "Closing";
            this.clmDet_Closng.MinimumWidth = 6;
            this.clmDet_Closng.Name = "clmDet_Closng";
            this.clmDet_Closng.ReadOnly = true;
            this.clmDet_Closng.Width = 125;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel3.Controls.Add(this.lnkClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1277, 44);
            this.panel3.TabIndex = 1;
            // 
            // lnkClose
            // 
            this.lnkClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkClose.AutoSize = true;
            this.lnkClose.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lnkClose.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lnkClose.Location = new System.Drawing.Point(1216, 12);
            this.lnkClose.Name = "lnkClose";
            this.lnkClose.Size = new System.Drawing.Size(58, 25);
            this.lnkClose.TabIndex = 0;
            this.lnkClose.TabStop = true;
            this.lnkClose.Text = "Close";
            this.lnkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClose_LinkClicked);
            // 
            // gridCustomers
            // 
            this.gridCustomers.AllowUserToAddRows = false;
            this.gridCustomers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridCustomers.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCustomerId,
            this.clmRoute,
            this.clmCustomer,
            this.clmItemId,
            this.clmAgreement,
            this.clmOpening,
            this.clmDelivered,
            this.clmReturned,
            this.clmClosing});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCustomers.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomers.Location = new System.Drawing.Point(0, 166);
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.ReadOnly = true;
            this.gridCustomers.RowHeadersWidth = 51;
            this.gridCustomers.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridCustomers.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridCustomers.RowTemplate.Height = 30;
            this.gridCustomers.Size = new System.Drawing.Size(1277, 618);
            this.gridCustomers.TabIndex = 5;
            this.gridCustomers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDetailed_CellDoubleClick);
            // 
            // clmCustomerId
            // 
            this.clmCustomerId.HeaderText = "Id";
            this.clmCustomerId.MinimumWidth = 6;
            this.clmCustomerId.Name = "clmCustomerId";
            this.clmCustomerId.ReadOnly = true;
            this.clmCustomerId.Width = 50;
            // 
            // clmRoute
            // 
            this.clmRoute.HeaderText = "Route";
            this.clmRoute.MinimumWidth = 6;
            this.clmRoute.Name = "clmRoute";
            this.clmRoute.ReadOnly = true;
            this.clmRoute.Width = 125;
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.MinimumWidth = 6;
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Width = 220;
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "ItemId";
            this.clmItemId.MinimumWidth = 6;
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.ReadOnly = true;
            this.clmItemId.Visible = false;
            this.clmItemId.Width = 125;
            // 
            // clmAgreement
            // 
            this.clmAgreement.HeaderText = "Agreement";
            this.clmAgreement.MinimumWidth = 6;
            this.clmAgreement.Name = "clmAgreement";
            this.clmAgreement.ReadOnly = true;
            this.clmAgreement.Width = 125;
            // 
            // clmOpening
            // 
            this.clmOpening.HeaderText = "Opening";
            this.clmOpening.MinimumWidth = 6;
            this.clmOpening.Name = "clmOpening";
            this.clmOpening.ReadOnly = true;
            this.clmOpening.Width = 125;
            // 
            // clmDelivered
            // 
            this.clmDelivered.HeaderText = "Delivered";
            this.clmDelivered.MinimumWidth = 6;
            this.clmDelivered.Name = "clmDelivered";
            this.clmDelivered.ReadOnly = true;
            this.clmDelivered.Width = 125;
            // 
            // clmReturned
            // 
            this.clmReturned.HeaderText = "Returned";
            this.clmReturned.MinimumWidth = 6;
            this.clmReturned.Name = "clmReturned";
            this.clmReturned.ReadOnly = true;
            this.clmReturned.Width = 125;
            // 
            // clmClosing
            // 
            this.clmClosing.HeaderText = "Closing";
            this.clmClosing.MinimumWidth = 6;
            this.clmClosing.Name = "clmClosing";
            this.clmClosing.ReadOnly = true;
            this.clmClosing.Width = 125;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.chkDateSearch);
            this.panel2.Controls.Add(this.grpDatesearch);
            this.panel2.Controls.Add(this.cmbProductName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbSupplier);
            this.panel2.Controls.Add(this.lblCustType);
            this.panel2.Controls.Add(this.cmbRoute);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1277, 166);
            this.panel2.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Teal;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(1024, 84);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // chkDateSearch
            // 
            this.chkDateSearch.AutoSize = true;
            this.chkDateSearch.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkDateSearch.Location = new System.Drawing.Point(18, 63);
            this.chkDateSearch.Name = "chkDateSearch";
            this.chkDateSearch.Size = new System.Drawing.Size(150, 29);
            this.chkDateSearch.TabIndex = 100;
            this.chkDateSearch.Text = "Date between";
            this.chkDateSearch.UseVisualStyleBackColor = true;
            this.chkDateSearch.CheckedChanged += new System.EventHandler(this.chkDateSearch_CheckedChanged);
            // 
            // grpDatesearch
            // 
            this.grpDatesearch.Controls.Add(this.dtpDateTo);
            this.grpDatesearch.Controls.Add(this.label1);
            this.grpDatesearch.Controls.Add(this.dtpDateFrom);
            this.grpDatesearch.Controls.Add(this.label2);
            this.grpDatesearch.Enabled = false;
            this.grpDatesearch.Location = new System.Drawing.Point(18, 84);
            this.grpDatesearch.Name = "grpDatesearch";
            this.grpDatesearch.Size = new System.Drawing.Size(735, 72);
            this.grpDatesearch.TabIndex = 100;
            this.grpDatesearch.TabStop = false;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(462, 34);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(200, 31);
            this.dtpDateTo.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(45, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 96;
            this.label1.Text = "Date From";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(157, 34);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(200, 31);
            this.dtpDateFrom.TabIndex = 98;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(381, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 97;
            this.label2.Text = "Date To";
            // 
            // cmbProductName
            // 
            this.cmbProductName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(642, 27);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(300, 33);
            this.cmbProductName.TabIndex = 95;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(637, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 25);
            this.label4.TabIndex = 94;
            this.label4.Text = "Product Name";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(325, 27);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(311, 33);
            this.cmbSupplier.TabIndex = 93;
            // 
            // lblCustType
            // 
            this.lblCustType.AutoSize = true;
            this.lblCustType.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCustType.Location = new System.Drawing.Point(325, 0);
            this.lblCustType.Name = "lblCustType";
            this.lblCustType.Size = new System.Drawing.Size(93, 25);
            this.lblCustType.TabIndex = 92;
            this.lblCustType.Text = "Customer";
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(12, 27);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(307, 33);
            this.cmbRoute.TabIndex = 90;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(13, -1);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(133, 25);
            this.label18.TabIndex = 91;
            this.label18.Text = "Delivery Route";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1137, 13);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 89;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1024, 13);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 47);
            this.btnSearch.TabIndex = 88;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // CustomerStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 784);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerStockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Stock";
            this.Load += new System.EventHandler(this.CustomerStockForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlDetailed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetailed)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpDatesearch.ResumeLayout(false);
            this.grpDatesearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridCustomers;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblCustType;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpDatesearch;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDateSearch;
        private System.Windows.Forms.Panel pnlDetailed;
        private System.Windows.Forms.DataGridView gridDetailed;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel lnkClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDet_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDet_Agreement;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDet_Opening;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDet_Delivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDet_Returned;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDet_Closng;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAgreement;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOpening;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDelivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmReturned;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmClosing;
        private System.Windows.Forms.Button btnPrint;
    }
}