namespace BETask.Views
{
    partial class DOSalesReportForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlDoSale = new System.Windows.Forms.Panel();
            this.pnlCustomerSaleDetails = new System.Windows.Forms.Panel();
            this.dgvDoSalesItemDetails = new System.Windows.Forms.DataGridView();
            this.colSalesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHedarClose = new System.Windows.Forms.Panel();
            this.linkPrint = new System.Windows.Forms.LinkLabel();
            this.lblSaleDetailsCaption = new System.Windows.Forms.Label();
            this.lnkClose = new System.Windows.Forms.LinkLabel();
            this.dgDOSales = new System.Windows.Forms.DataGridView();
            this.clmDOId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDOdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDoInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Net = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmLinkDetails = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDoInvNo = new System.Windows.Forms.TextBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSalesDoNo = new System.Windows.Forms.TextBox();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlDoSale.SuspendLayout();
            this.pnlCustomerSaleDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoSalesItemDetails)).BeginInit();
            this.pnlHedarClose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDOSales)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.Window;
            this.pnlMain.Controls.Add(this.pnlDoSale);
            this.pnlMain.Controls.Add(this.panel5);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1277, 784);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDoSale
            // 
            this.pnlDoSale.Controls.Add(this.pnlCustomerSaleDetails);
            this.pnlDoSale.Controls.Add(this.dgDOSales);
            this.pnlDoSale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDoSale.Location = new System.Drawing.Point(0, 144);
            this.pnlDoSale.Name = "pnlDoSale";
            this.pnlDoSale.Size = new System.Drawing.Size(1277, 571);
            this.pnlDoSale.TabIndex = 49;
            // 
            // pnlCustomerSaleDetails
            // 
            this.pnlCustomerSaleDetails.Controls.Add(this.dgvDoSalesItemDetails);
            this.pnlCustomerSaleDetails.Controls.Add(this.pnlHedarClose);
            this.pnlCustomerSaleDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCustomerSaleDetails.Location = new System.Drawing.Point(291, 0);
            this.pnlCustomerSaleDetails.Name = "pnlCustomerSaleDetails";
            this.pnlCustomerSaleDetails.Size = new System.Drawing.Size(986, 571);
            this.pnlCustomerSaleDetails.TabIndex = 13;
            this.pnlCustomerSaleDetails.Visible = false;
            // 
            // dgvDoSalesItemDetails
            // 
            this.dgvDoSalesItemDetails.AllowUserToAddRows = false;
            this.dgvDoSalesItemDetails.AllowUserToDeleteRows = false;
            this.dgvDoSalesItemDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDoSalesItemDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDoSalesItemDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoSalesItemDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSalesId,
            this.colDate,
            this.colDoNo,
            this.colItemId,
            this.colItemName,
            this.colUnt,
            this.colQty,
            this.colRate,
            this.colDiscount,
            this.colGross,
            this.colTax,
            this.colNet});
            this.dgvDoSalesItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoSalesItemDetails.Location = new System.Drawing.Point(0, 33);
            this.dgvDoSalesItemDetails.Name = "dgvDoSalesItemDetails";
            this.dgvDoSalesItemDetails.ReadOnly = true;
            this.dgvDoSalesItemDetails.RowHeadersWidth = 51;
            this.dgvDoSalesItemDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgvDoSalesItemDetails.RowTemplate.Height = 24;
            this.dgvDoSalesItemDetails.Size = new System.Drawing.Size(986, 538);
            this.dgvDoSalesItemDetails.TabIndex = 1;
            // 
            // colSalesId
            // 
            this.colSalesId.HeaderText = "Sales Id";
            this.colSalesId.MinimumWidth = 10;
            this.colSalesId.Name = "colSalesId";
            this.colSalesId.ReadOnly = true;
            this.colSalesId.Visible = false;
            this.colSalesId.Width = 125;
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDate.HeaderText = "Date";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDate.Visible = false;
            // 
            // colDoNo
            // 
            this.colDoNo.HeaderText = "DoNo";
            this.colDoNo.MinimumWidth = 6;
            this.colDoNo.Name = "colDoNo";
            this.colDoNo.ReadOnly = true;
            this.colDoNo.Visible = false;
            this.colDoNo.Width = 125;
            // 
            // colItemId
            // 
            this.colItemId.HeaderText = "Item Id";
            this.colItemId.MinimumWidth = 6;
            this.colItemId.Name = "colItemId";
            this.colItemId.ReadOnly = true;
            this.colItemId.Visible = false;
            this.colItemId.Width = 80;
            // 
            // colItemName
            // 
            this.colItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colItemName.HeaderText = "Item Name";
            this.colItemName.MinimumWidth = 6;
            this.colItemName.Name = "colItemName";
            this.colItemName.ReadOnly = true;
            // 
            // colUnt
            // 
            this.colUnt.HeaderText = "Unit";
            this.colUnt.Name = "colUnt";
            this.colUnt.ReadOnly = true;
            this.colUnt.Visible = false;
            // 
            // colQty
            // 
            this.colQty.HeaderText = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // colRate
            // 
            this.colRate.HeaderText = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.ReadOnly = true;
            // 
            // colDiscount
            // 
            this.colDiscount.HeaderText = "Discount";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            // 
            // colGross
            // 
            this.colGross.HeaderText = "Gross";
            this.colGross.Name = "colGross";
            this.colGross.ReadOnly = true;
            // 
            // colTax
            // 
            this.colTax.HeaderText = "Tax";
            this.colTax.Name = "colTax";
            this.colTax.ReadOnly = true;
            // 
            // colNet
            // 
            this.colNet.HeaderText = "Net";
            this.colNet.Name = "colNet";
            this.colNet.ReadOnly = true;
            // 
            // pnlHedarClose
            // 
            this.pnlHedarClose.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlHedarClose.Controls.Add(this.linkPrint);
            this.pnlHedarClose.Controls.Add(this.lblSaleDetailsCaption);
            this.pnlHedarClose.Controls.Add(this.lnkClose);
            this.pnlHedarClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHedarClose.Location = new System.Drawing.Point(0, 0);
            this.pnlHedarClose.Name = "pnlHedarClose";
            this.pnlHedarClose.Size = new System.Drawing.Size(986, 33);
            this.pnlHedarClose.TabIndex = 0;
            // 
            // linkPrint
            // 
            this.linkPrint.AutoSize = true;
            this.linkPrint.Font = new System.Drawing.Font("Segoe UI", 12.8F);
            this.linkPrint.LinkColor = System.Drawing.Color.White;
            this.linkPrint.Location = new System.Drawing.Point(785, 0);
            this.linkPrint.Name = "linkPrint";
            this.linkPrint.Size = new System.Drawing.Size(57, 30);
            this.linkPrint.TabIndex = 4;
            this.linkPrint.TabStop = true;
            this.linkPrint.Text = "Print";
            this.linkPrint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPrint_LinkClicked);
            // 
            // lblSaleDetailsCaption
            // 
            this.lblSaleDetailsCaption.AutoSize = true;
            this.lblSaleDetailsCaption.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblSaleDetailsCaption.ForeColor = System.Drawing.Color.White;
            this.lblSaleDetailsCaption.Location = new System.Drawing.Point(25, 8);
            this.lblSaleDetailsCaption.Name = "lblSaleDetailsCaption";
            this.lblSaleDetailsCaption.Size = new System.Drawing.Size(64, 23);
            this.lblSaleDetailsCaption.TabIndex = 2;
            this.lblSaleDetailsCaption.Text = "do sale";
            // 
            // lnkClose
            // 
            this.lnkClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkClose.AutoSize = true;
            this.lnkClose.Font = new System.Drawing.Font("Segoe UI", 12.8F);
            this.lnkClose.LinkColor = System.Drawing.Color.White;
            this.lnkClose.Location = new System.Drawing.Point(889, 2);
            this.lnkClose.Name = "lnkClose";
            this.lnkClose.Size = new System.Drawing.Size(85, 30);
            this.lnkClose.TabIndex = 0;
            this.lnkClose.TabStop = true;
            this.lnkClose.Text = "Close X";
            this.lnkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClose_LinkClicked_1);
            // 
            // dgDOSales
            // 
            this.dgDOSales.AllowUserToAddRows = false;
            this.dgDOSales.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgDOSales.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDOSales.BackgroundColor = System.Drawing.Color.White;
            this.dgDOSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDOSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDOId,
            this.clmCustomerId,
            this.clmDOdate,
            this.clmDoInvNo,
            this.clmCustomerName,
            this.clmGross,
            this.Discount,
            this.clmTotal,
            this.clmVat,
            this.Net,
            this.clmPrint,
            this.clmLinkDetails});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDOSales.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDOSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDOSales.Location = new System.Drawing.Point(0, 0);
            this.dgDOSales.Margin = new System.Windows.Forms.Padding(4);
            this.dgDOSales.Name = "dgDOSales";
            this.dgDOSales.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgDOSales.RowTemplate.Height = 30;
            this.dgDOSales.Size = new System.Drawing.Size(1277, 571);
            this.dgDOSales.TabIndex = 12;
            this.dgDOSales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDOSales_CellClick);
            // 
            // clmDOId
            // 
            this.clmDOId.HeaderText = "DOid";
            this.clmDOId.Name = "clmDOId";
            this.clmDOId.Visible = false;
            // 
            // clmCustomerId
            // 
            this.clmCustomerId.HeaderText = "Customer Id";
            this.clmCustomerId.Name = "clmCustomerId";
            this.clmCustomerId.Visible = false;
            this.clmCustomerId.Width = 80;
            // 
            // clmDOdate
            // 
            this.clmDOdate.HeaderText = "Date";
            this.clmDOdate.Name = "clmDOdate";
            this.clmDOdate.Width = 150;
            // 
            // clmDoInvNo
            // 
            this.clmDoInvNo.HeaderText = "Invoice No";
            this.clmDoInvNo.Name = "clmDoInvNo";
            // 
            // clmCustomerName
            // 
            this.clmCustomerName.HeaderText = "Customer Name";
            this.clmCustomerName.Name = "clmCustomerName";
            this.clmCustomerName.ReadOnly = true;
            this.clmCustomerName.Width = 200;
            // 
            // clmGross
            // 
            this.clmGross.HeaderText = "Gross";
            this.clmGross.Name = "clmGross";
            this.clmGross.ReadOnly = true;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 80;
            // 
            // clmTotal
            // 
            this.clmTotal.HeaderText = "Total";
            this.clmTotal.Name = "clmTotal";
            this.clmTotal.ReadOnly = true;
            // 
            // clmVat
            // 
            this.clmVat.HeaderText = "Vat";
            this.clmVat.Name = "clmVat";
            this.clmVat.ReadOnly = true;
            this.clmVat.Width = 90;
            // 
            // Net
            // 
            this.Net.HeaderText = "Net";
            this.Net.Name = "Net";
            this.Net.ReadOnly = true;
            this.Net.Width = 90;
            // 
            // clmPrint
            // 
            this.clmPrint.HeaderText = "Select";
            this.clmPrint.Name = "clmPrint";
            // 
            // clmLinkDetails
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.clmLinkDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmLinkDetails.HeaderText = "Item Details";
            this.clmLinkDetails.Name = "clmLinkDetails";
            this.clmLinkDetails.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmLinkDetails.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 715);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1277, 69);
            this.panel5.TabIndex = 48;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(14, 12);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1128, 11);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1016, 11);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtDoInvNo);
            this.panel2.Controls.Add(this.dtpDateFrom);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtSalesDoNo);
            this.panel2.Controls.Add(this.cmbRoute);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbCustomer);
            this.panel2.Controls.Add(this.dtpDateTo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1277, 144);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(46, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 25);
            this.label4.TabIndex = 108;
            this.label4.Text = "DO Inv No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(608, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 25);
            this.label7.TabIndex = 109;
            this.label7.Text = "Sales DO No";
            // 
            // txtDoInvNo
            // 
            this.txtDoInvNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDoInvNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDoInvNo.Location = new System.Drawing.Point(179, 98);
            this.txtDoInvNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDoInvNo.MaxLength = 50;
            this.txtDoInvNo.Name = "txtDoInvNo";
            this.txtDoInvNo.Size = new System.Drawing.Size(406, 31);
            this.txtDoInvNo.TabIndex = 106;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(179, 22);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(406, 31);
            this.dtpDateFrom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(46, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 96;
            this.label1.Text = "Date From";
            // 
            // txtSalesDoNo
            // 
            this.txtSalesDoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalesDoNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSalesDoNo.Location = new System.Drawing.Point(737, 98);
            this.txtSalesDoNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSalesDoNo.MaxLength = 50;
            this.txtSalesDoNo.Name = "txtSalesDoNo";
            this.txtSalesDoNo.Size = new System.Drawing.Size(361, 31);
            this.txtSalesDoNo.TabIndex = 107;
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(179, 59);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(406, 33);
            this.cmbRoute.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1128, 82);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 47);
            this.btnSearch.TabIndex = 88;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(46, 59);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 25);
            this.label18.TabIndex = 91;
            this.label18.Text = "Route";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(608, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 97;
            this.label2.Text = "Date To";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(737, 59);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(361, 33);
            this.cmbCustomer.TabIndex = 99;
            this.cmbCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomer_KeyDown);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(737, 22);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(361, 31);
            this.dtpDateTo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(608, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 98;
            this.label3.Text = "Customer";
            // 
            // DOSalesReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 784);
            this.Controls.Add(this.pnlMain);
            this.Name = "DOSalesReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DO Sales Report";
            this.Load += new System.EventHandler(this.DOSalesReportForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlDoSale.ResumeLayout(false);
            this.pnlCustomerSaleDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoSalesItemDetails)).EndInit();
            this.pnlHedarClose.ResumeLayout(false);
            this.pnlHedarClose.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDOSales)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSalesDoNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDoInvNo;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlDoSale;
        private System.Windows.Forms.DataGridView dgDOSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDOId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDOdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDoInvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGross;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Net;
        private System.Windows.Forms.DataGridViewButtonColumn clmPrint;
        private System.Windows.Forms.DataGridViewLinkColumn clmLinkDetails;
        private System.Windows.Forms.Panel pnlCustomerSaleDetails;
        private System.Windows.Forms.Panel pnlHedarClose;
        private System.Windows.Forms.Label lblSaleDetailsCaption;
        private System.Windows.Forms.LinkLabel lnkClose;
        private System.Windows.Forms.DataGridView dgvDoSalesItemDetails;
        private System.Windows.Forms.LinkLabel linkPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalesId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGross;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNet;
    }
}