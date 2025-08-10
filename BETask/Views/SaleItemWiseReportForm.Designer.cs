namespace BETask.Views
{
    partial class SaleItemWiseReportForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridPurchase = new System.Windows.Forms.DataGridView();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPacking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTaxable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInvoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbPaymentmode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRangeTo = new System.Windows.Forms.TextBox();
            this.txtRangeFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdlAll = new System.Windows.Forms.RadioButton();
            this.lblCouponBalance = new System.Windows.Forms.Label();
            this.rdlCoupon = new System.Windows.Forms.RadioButton();
            this.rdlCash = new System.Windows.Forms.RadioButton();
            this.rdlBank = new System.Windows.Forms.RadioButton();
            this.rdlCredit = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblCustType = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPurchase)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.gridPurchase);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1289, 711);
            this.panel1.TabIndex = 0;
            // 
            // gridPurchase
            // 
            this.gridPurchase.AllowUserToAddRows = false;
            this.gridPurchase.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.gridPurchase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridPurchase.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridPurchase.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPurchase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCustomer,
            this.clmDate,
            this.clmItemId,
            this.clmItemName,
            this.clmPacking,
            this.clmQty,
            this.clmRate,
            this.clmGross,
            this.clmDiscount,
            this.clmTaxable,
            this.clmVat,
            this.clmNet,
            this.clmInvoice,
            this.clmPaymentMode});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridPurchase.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridPurchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPurchase.Location = new System.Drawing.Point(0, 176);
            this.gridPurchase.Name = "gridPurchase";
            this.gridPurchase.ReadOnly = true;
            this.gridPurchase.RowHeadersVisible = false;
            this.gridPurchase.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridPurchase.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridPurchase.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridPurchase.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.gridPurchase.RowTemplate.Height = 30;
            this.gridPurchase.Size = new System.Drawing.Size(1289, 471);
            this.gridPurchase.TabIndex = 2;
            this.gridPurchase.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPurchase_CellClick);
            this.gridPurchase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridPurchase_KeyDown);
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.MinimumWidth = 6;
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Width = 150;
            // 
            // clmDate
            // 
            this.clmDate.HeaderText = "Date";
            this.clmDate.MinimumWidth = 6;
            this.clmDate.Name = "clmDate";
            this.clmDate.ReadOnly = true;
            this.clmDate.Width = 125;
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
            // clmItemName
            // 
            this.clmItemName.HeaderText = "Item Name";
            this.clmItemName.MinimumWidth = 6;
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.ReadOnly = true;
            this.clmItemName.Width = 150;
            // 
            // clmPacking
            // 
            this.clmPacking.HeaderText = "Packing";
            this.clmPacking.MinimumWidth = 6;
            this.clmPacking.Name = "clmPacking";
            this.clmPacking.ReadOnly = true;
            this.clmPacking.Visible = false;
            this.clmPacking.Width = 80;
            // 
            // clmQty
            // 
            this.clmQty.HeaderText = "Qty";
            this.clmQty.MinimumWidth = 6;
            this.clmQty.Name = "clmQty";
            this.clmQty.ReadOnly = true;
            this.clmQty.Width = 80;
            // 
            // clmRate
            // 
            this.clmRate.HeaderText = "Rate";
            this.clmRate.MinimumWidth = 6;
            this.clmRate.Name = "clmRate";
            this.clmRate.ReadOnly = true;
            this.clmRate.Width = 125;
            // 
            // clmGross
            // 
            this.clmGross.HeaderText = "Gross";
            this.clmGross.MinimumWidth = 6;
            this.clmGross.Name = "clmGross";
            this.clmGross.ReadOnly = true;
            this.clmGross.Width = 125;
            // 
            // clmDiscount
            // 
            this.clmDiscount.HeaderText = "Discount";
            this.clmDiscount.MinimumWidth = 6;
            this.clmDiscount.Name = "clmDiscount";
            this.clmDiscount.ReadOnly = true;
            this.clmDiscount.Visible = false;
            this.clmDiscount.Width = 125;
            // 
            // clmTaxable
            // 
            this.clmTaxable.HeaderText = "Taxable";
            this.clmTaxable.MinimumWidth = 6;
            this.clmTaxable.Name = "clmTaxable";
            this.clmTaxable.ReadOnly = true;
            this.clmTaxable.Width = 125;
            // 
            // clmVat
            // 
            this.clmVat.HeaderText = "Vat";
            this.clmVat.MinimumWidth = 6;
            this.clmVat.Name = "clmVat";
            this.clmVat.ReadOnly = true;
            this.clmVat.Width = 125;
            // 
            // clmNet
            // 
            this.clmNet.HeaderText = "Net";
            this.clmNet.MinimumWidth = 6;
            this.clmNet.Name = "clmNet";
            this.clmNet.ReadOnly = true;
            this.clmNet.Width = 125;
            // 
            // clmInvoice
            // 
            this.clmInvoice.HeaderText = "Invoice";
            this.clmInvoice.MinimumWidth = 6;
            this.clmInvoice.Name = "clmInvoice";
            this.clmInvoice.ReadOnly = true;
            this.clmInvoice.Width = 125;
            // 
            // clmPaymentMode
            // 
            this.clmPaymentMode.HeaderText = "Payment Mode";
            this.clmPaymentMode.MinimumWidth = 6;
            this.clmPaymentMode.Name = "clmPaymentMode";
            this.clmPaymentMode.ReadOnly = true;
            this.clmPaymentMode.Width = 125;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.lblCount);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.lblNetAmount);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 647);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1289, 64);
            this.panel3.TabIndex = 1;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCount.ForeColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(644, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 25);
            this.lblCount.TabIndex = 11;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(8, 6);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblNetAmount.ForeColor = System.Drawing.Color.White;
            this.lblNetAmount.Location = new System.Drawing.Point(288, 16);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(0, 25);
            this.lblNetAmount.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1170, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 4;
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
            this.btnClose.Location = new System.Drawing.Point(1058, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbPaymentmode);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtRangeTo);
            this.panel2.Controls.Add(this.txtRangeFrom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmbRoute);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.cmbProductName);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.cmbSupplier);
            this.panel2.Controls.Add(this.lblCustType);
            this.panel2.Controls.Add(this.dtpDateTo);
            this.panel2.Controls.Add(this.dtpDateFrom);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1289, 176);
            this.panel2.TabIndex = 0;
            // 
            // cmbPaymentmode
            // 
            this.cmbPaymentmode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbPaymentmode.FormattingEnabled = true;
            this.cmbPaymentmode.Location = new System.Drawing.Point(426, 122);
            this.cmbPaymentmode.Name = "cmbPaymentmode";
            this.cmbPaymentmode.Size = new System.Drawing.Size(295, 33);
            this.cmbPaymentmode.TabIndex = 83;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(421, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 25);
            this.label5.TabIndex = 82;
            this.label5.Text = "Payment Mode";
            // 
            // txtRangeTo
            // 
            this.txtRangeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRangeTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRangeTo.Location = new System.Drawing.Point(831, 124);
            this.txtRangeTo.MaxLength = 13;
            this.txtRangeTo.Name = "txtRangeTo";
            this.txtRangeTo.Size = new System.Drawing.Size(86, 31);
            this.txtRangeTo.TabIndex = 81;
            this.txtRangeTo.Tag = "Dec";
            this.txtRangeTo.Text = "100";
            // 
            // txtRangeFrom
            // 
            this.txtRangeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRangeFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRangeFrom.Location = new System.Drawing.Point(732, 124);
            this.txtRangeFrom.MaxLength = 13;
            this.txtRangeFrom.Name = "txtRangeFrom";
            this.txtRangeFrom.Size = new System.Drawing.Size(93, 31);
            this.txtRangeFrom.TabIndex = 80;
            this.txtRangeFrom.Tag = "Dec";
            this.txtRangeFrom.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(727, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 79;
            this.label3.Text = "Price Ranges";
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(1015, 45);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(250, 33);
            this.cmbRoute.TabIndex = 77;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(1010, 13);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(133, 25);
            this.label18.TabIndex = 78;
            this.label18.Text = "Delivery Route";
            // 
            // cmbProductName
            // 
            this.cmbProductName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(426, 45);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(295, 33);
            this.cmbProductName.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(421, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 25);
            this.label4.TabIndex = 28;
            this.label4.Text = "Product Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdlAll);
            this.groupBox1.Controls.Add(this.lblCouponBalance);
            this.groupBox1.Controls.Add(this.rdlCoupon);
            this.groupBox1.Controls.Add(this.rdlCash);
            this.groupBox1.Controls.Add(this.rdlBank);
            this.groupBox1.Controls.Add(this.rdlCredit);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.groupBox1.Location = new System.Drawing.Point(13, 137);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(549, 37);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            this.groupBox1.Visible = false;
            // 
            // rdlAll
            // 
            this.rdlAll.AutoSize = true;
            this.rdlAll.Checked = true;
            this.rdlAll.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlAll.Location = new System.Drawing.Point(34, 33);
            this.rdlAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdlAll.Name = "rdlAll";
            this.rdlAll.Size = new System.Drawing.Size(63, 29);
            this.rdlAll.TabIndex = 28;
            this.rdlAll.TabStop = true;
            this.rdlAll.Text = "ALL";
            this.rdlAll.UseVisualStyleBackColor = true;
            // 
            // lblCouponBalance
            // 
            this.lblCouponBalance.AutoSize = true;
            this.lblCouponBalance.ForeColor = System.Drawing.Color.Green;
            this.lblCouponBalance.Location = new System.Drawing.Point(46, 148);
            this.lblCouponBalance.Name = "lblCouponBalance";
            this.lblCouponBalance.Size = new System.Drawing.Size(46, 25);
            this.lblCouponBalance.TabIndex = 27;
            this.lblCouponBalance.Text = "0.00";
            // 
            // rdlCoupon
            // 
            this.rdlCoupon.AutoSize = true;
            this.rdlCoupon.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlCoupon.Location = new System.Drawing.Point(437, 33);
            this.rdlCoupon.Margin = new System.Windows.Forms.Padding(4);
            this.rdlCoupon.Name = "rdlCoupon";
            this.rdlCoupon.Size = new System.Drawing.Size(100, 29);
            this.rdlCoupon.TabIndex = 26;
            this.rdlCoupon.Text = "Coupon";
            this.rdlCoupon.UseVisualStyleBackColor = true;
            // 
            // rdlCash
            // 
            this.rdlCash.AutoSize = true;
            this.rdlCash.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlCash.Location = new System.Drawing.Point(121, 33);
            this.rdlCash.Margin = new System.Windows.Forms.Padding(4);
            this.rdlCash.Name = "rdlCash";
            this.rdlCash.Size = new System.Drawing.Size(74, 29);
            this.rdlCash.TabIndex = 23;
            this.rdlCash.Text = "Cash";
            this.rdlCash.UseVisualStyleBackColor = true;
            // 
            // rdlBank
            // 
            this.rdlBank.AutoSize = true;
            this.rdlBank.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlBank.Location = new System.Drawing.Point(333, 32);
            this.rdlBank.Margin = new System.Windows.Forms.Padding(4);
            this.rdlBank.Name = "rdlBank";
            this.rdlBank.Size = new System.Drawing.Size(74, 29);
            this.rdlBank.TabIndex = 25;
            this.rdlBank.Text = "Bank";
            this.rdlBank.UseVisualStyleBackColor = true;
            // 
            // rdlCredit
            // 
            this.rdlCredit.AutoSize = true;
            this.rdlCredit.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlCredit.Location = new System.Drawing.Point(221, 32);
            this.rdlCredit.Margin = new System.Windows.Forms.Padding(4);
            this.rdlCredit.Name = "rdlCredit";
            this.rdlCredit.Size = new System.Drawing.Size(84, 29);
            this.rdlCredit.TabIndex = 24;
            this.rdlCredit.Text = "Credit";
            this.rdlCredit.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1103, 124);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(159, 47);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(727, 45);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(282, 33);
            this.cmbSupplier.TabIndex = 5;
            this.cmbSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplier_KeyDown);
            // 
            // lblCustType
            // 
            this.lblCustType.AutoSize = true;
            this.lblCustType.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCustType.Location = new System.Drawing.Point(722, 13);
            this.lblCustType.Name = "lblCustType";
            this.lblCustType.Size = new System.Drawing.Size(93, 25);
            this.lblCustType.TabIndex = 4;
            this.lblCustType.Text = "Customer";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(220, 47);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(200, 31);
            this.dtpDateTo.TabIndex = 3;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(8, 47);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(200, 31);
            this.dtpDateFrom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(215, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date From";
            // 
            // SaleItemWiseReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 711);
            this.Controls.Add(this.panel1);
            this.Name = "SaleItemWiseReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Item wise Report";
            this.Load += new System.EventHandler(this.PurchaseSearchForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPurchase)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblCustType;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.DataGridView gridPurchase;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdlAll;
        private System.Windows.Forms.Label lblCouponBalance;
        private System.Windows.Forms.RadioButton rdlCoupon;
        private System.Windows.Forms.RadioButton rdlCash;
        private System.Windows.Forms.RadioButton rdlBank;
        private System.Windows.Forms.RadioButton rdlCredit;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGross;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaxable;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVat;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNet;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInvoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRangeTo;
        private System.Windows.Forms.TextBox txtRangeFrom;
        private System.Windows.Forms.ComboBox cmbPaymentmode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentMode;
        private System.Windows.Forms.Label lblCount;
    }
}