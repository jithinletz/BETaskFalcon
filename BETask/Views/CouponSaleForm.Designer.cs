namespace BETask.Views
{
    partial class CouponSaleForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabNewCoupon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlCouponleafs = new System.Windows.Forms.Panel();
            this.gridCouponLeafs = new System.Windows.Forms.DataGridView();
            this.clmLeafId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeafNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeafrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeafCancel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmLeafRedeem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmLeafStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalLeafs = new System.Windows.Forms.Label();
            this.btnGenerateLeafs = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.chkBookStatus = new System.Windows.Forms.CheckBox();
            this.txtBookAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRatePerLeaf = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLeafEndNo = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLeafStartNo = new System.Windows.Forms.NumericUpDown();
            this.txtBookNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lnkReport = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblResultCount = new System.Windows.Forms.Label();
            this.gridDeliveries = new System.Windows.Forms.DataGridView();
            this.clmCouponId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBookno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearchCoupnBook = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabNewCoupon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlCouponleafs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCouponLeafs)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeafEndNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeafStartNo)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveries)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.tabNewCoupon);
            this.panel1.Controls.Add(this.pnlFooter);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 763);
            this.panel1.TabIndex = 0;
            // 
            // tabNewCoupon
            // 
            this.tabNewCoupon.Controls.Add(this.tabPage1);
            this.tabNewCoupon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNewCoupon.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.tabNewCoupon.Location = new System.Drawing.Point(363, 0);
            this.tabNewCoupon.Name = "tabNewCoupon";
            this.tabNewCoupon.SelectedIndex = 0;
            this.tabNewCoupon.Size = new System.Drawing.Size(867, 693);
            this.tabNewCoupon.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlCouponleafs);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(859, 655);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New Coupon Book";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlCouponleafs
            // 
            this.pnlCouponleafs.Controls.Add(this.gridCouponLeafs);
            this.pnlCouponleafs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCouponleafs.Location = new System.Drawing.Point(3, 330);
            this.pnlCouponleafs.Name = "pnlCouponleafs";
            this.pnlCouponleafs.Size = new System.Drawing.Size(853, 322);
            this.pnlCouponleafs.TabIndex = 1;
            // 
            // gridCouponLeafs
            // 
            this.gridCouponLeafs.AllowUserToAddRows = false;
            this.gridCouponLeafs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridCouponLeafs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridCouponLeafs.BackgroundColor = System.Drawing.Color.White;
            this.gridCouponLeafs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCouponLeafs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmLeafId,
            this.clmLeafNo,
            this.clmLeafrate,
            this.clmLeafCancel,
            this.clmLeafRedeem,
            this.clmLeafStatus});
            this.gridCouponLeafs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCouponLeafs.Location = new System.Drawing.Point(0, 0);
            this.gridCouponLeafs.Name = "gridCouponLeafs";
            this.gridCouponLeafs.RowHeadersVisible = false;
            this.gridCouponLeafs.RowHeadersWidth = 51;
            this.gridCouponLeafs.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridCouponLeafs.RowTemplate.Height = 30;
            this.gridCouponLeafs.Size = new System.Drawing.Size(853, 322);
            this.gridCouponLeafs.TabIndex = 0;
            this.gridCouponLeafs.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridCouponLeafs_CellBeginEdit);
            // 
            // clmLeafId
            // 
            this.clmLeafId.HeaderText = "LeafId";
            this.clmLeafId.MinimumWidth = 6;
            this.clmLeafId.Name = "clmLeafId";
            this.clmLeafId.Visible = false;
            this.clmLeafId.Width = 125;
            // 
            // clmLeafNo
            // 
            this.clmLeafNo.HeaderText = "LeafNo";
            this.clmLeafNo.MinimumWidth = 6;
            this.clmLeafNo.Name = "clmLeafNo";
            this.clmLeafNo.Width = 125;
            // 
            // clmLeafrate
            // 
            this.clmLeafrate.HeaderText = "Rate";
            this.clmLeafrate.MinimumWidth = 6;
            this.clmLeafrate.Name = "clmLeafrate";
            this.clmLeafrate.Width = 125;
            // 
            // clmLeafCancel
            // 
            this.clmLeafCancel.HeaderText = "Cancel";
            this.clmLeafCancel.MinimumWidth = 6;
            this.clmLeafCancel.Name = "clmLeafCancel";
            this.clmLeafCancel.Width = 125;
            // 
            // clmLeafRedeem
            // 
            this.clmLeafRedeem.HeaderText = "Redeemed";
            this.clmLeafRedeem.MinimumWidth = 6;
            this.clmLeafRedeem.Name = "clmLeafRedeem";
            this.clmLeafRedeem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmLeafRedeem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmLeafRedeem.Width = 125;
            // 
            // clmLeafStatus
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmLeafStatus.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmLeafStatus.HeaderText = "Leaf Status";
            this.clmLeafStatus.MinimumWidth = 6;
            this.clmLeafStatus.Name = "clmLeafStatus";
            this.clmLeafStatus.ReadOnly = true;
            this.clmLeafStatus.Width = 300;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtpDeliveryDate);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.cmbCustomerName);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lblTotalLeafs);
            this.panel3.Controls.Add(this.btnGenerateLeafs);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtRemarks);
            this.panel3.Controls.Add(this.chkBookStatus);
            this.panel3.Controls.Add(this.txtBookAmount);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtRatePerLeaf);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtLeafEndNo);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtLeafStartNo);
            this.panel3.Controls.Add(this.txtBookNo);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(853, 327);
            this.panel3.TabIndex = 0;
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDeliveryDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(572, 70);
            this.dtpDeliveryDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(227, 31);
            this.dtpDeliveryDate.TabIndex = 2;
            this.dtpDeliveryDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(423, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 25);
            this.label9.TabIndex = 17;
            this.label9.Text = "Issue Date";
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(161, 20);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(638, 33);
            this.cmbCustomerName.TabIndex = 0;
            this.cmbCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(10, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 25);
            this.label8.TabIndex = 14;
            this.label8.Text = "Customer Name";
            // 
            // lblTotalLeafs
            // 
            this.lblTotalLeafs.AutoSize = true;
            this.lblTotalLeafs.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblTotalLeafs.Location = new System.Drawing.Point(156, 274);
            this.lblTotalLeafs.Name = "lblTotalLeafs";
            this.lblTotalLeafs.Size = new System.Drawing.Size(22, 25);
            this.lblTotalLeafs.TabIndex = 13;
            this.lblTotalLeafs.Text = "0";
            // 
            // btnGenerateLeafs
            // 
            this.btnGenerateLeafs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateLeafs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnGenerateLeafs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateLeafs.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateLeafs.ForeColor = System.Drawing.Color.White;
            this.btnGenerateLeafs.Location = new System.Drawing.Point(445, 264);
            this.btnGenerateLeafs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerateLeafs.Name = "btnGenerateLeafs";
            this.btnGenerateLeafs.Size = new System.Drawing.Size(354, 47);
            this.btnGenerateLeafs.TabIndex = 8;
            this.btnGenerateLeafs.Text = "Generate coupon leafs";
            this.btnGenerateLeafs.UseVisualStyleBackColor = false;
            this.btnGenerateLeafs.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(9, 274);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Total Leafs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(12, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 25);
            this.label6.TabIndex = 11;
            this.label6.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(161, 225);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(638, 31);
            this.txtRemarks.TabIndex = 7;
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // chkBookStatus
            // 
            this.chkBookStatus.AutoSize = true;
            this.chkBookStatus.Checked = true;
            this.chkBookStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBookStatus.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkBookStatus.Location = new System.Drawing.Point(228, 274);
            this.chkBookStatus.Name = "chkBookStatus";
            this.chkBookStatus.Size = new System.Drawing.Size(84, 29);
            this.chkBookStatus.TabIndex = 9;
            this.chkBookStatus.Text = "Status";
            this.chkBookStatus.UseVisualStyleBackColor = true;
            // 
            // txtBookAmount
            // 
            this.txtBookAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBookAmount.Location = new System.Drawing.Point(572, 168);
            this.txtBookAmount.Name = "txtBookAmount";
            this.txtBookAmount.Size = new System.Drawing.Size(227, 31);
            this.txtBookAmount.TabIndex = 6;
            this.txtBookAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtBookAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(421, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Book Amount";
            // 
            // txtRatePerLeaf
            // 
            this.txtRatePerLeaf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRatePerLeaf.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRatePerLeaf.Location = new System.Drawing.Point(161, 168);
            this.txtRatePerLeaf.Name = "txtRatePerLeaf";
            this.txtRatePerLeaf.Size = new System.Drawing.Size(232, 31);
            this.txtRatePerLeaf.TabIndex = 5;
            this.txtRatePerLeaf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtRatePerLeaf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(12, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Rate per Leaf";
            // 
            // txtLeafEndNo
            // 
            this.txtLeafEndNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeafEndNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLeafEndNo.Location = new System.Drawing.Point(572, 116);
            this.txtLeafEndNo.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.txtLeafEndNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLeafEndNo.Name = "txtLeafEndNo";
            this.txtLeafEndNo.Size = new System.Drawing.Size(227, 31);
            this.txtLeafEndNo.TabIndex = 4;
            this.txtLeafEndNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLeafEndNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(423, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Leaf Ends";
            // 
            // txtLeafStartNo
            // 
            this.txtLeafStartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeafStartNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLeafStartNo.Location = new System.Drawing.Point(161, 116);
            this.txtLeafStartNo.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.txtLeafStartNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLeafStartNo.Name = "txtLeafStartNo";
            this.txtLeafStartNo.Size = new System.Drawing.Size(232, 31);
            this.txtLeafStartNo.TabIndex = 3;
            this.txtLeafStartNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLeafStartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtBookNo
            // 
            this.txtBookNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBookNo.Location = new System.Drawing.Point(161, 67);
            this.txtBookNo.Name = "txtBookNo";
            this.txtBookNo.Size = new System.Drawing.Size(232, 31);
            this.txtBookNo.TabIndex = 1;
            this.txtBookNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Leaf Start From ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "BookNo";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Controls.Add(this.lnkReport);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnClose);
            this.pnlFooter.Controls.Add(this.btnNew);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(363, 693);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(867, 70);
            this.pnlFooter.TabIndex = 2;
            // 
            // lnkReport
            // 
            this.lnkReport.AutoSize = true;
            this.lnkReport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkReport.LinkColor = System.Drawing.Color.White;
            this.lnkReport.Location = new System.Drawing.Point(24, 24);
            this.lnkReport.Name = "lnkReport";
            this.lnkReport.Size = new System.Drawing.Size(141, 23);
            this.lnkReport.TabIndex = 4;
            this.lnkReport.TabStop = true;
            this.lnkReport.Text = "Advanced Report";
            this.lnkReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReport_LinkClicked);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(748, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(636, 12);
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
            this.btnClose.Enabled = false;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(524, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.Green;
            this.btnNew.Enabled = false;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(412, 12);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 47);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblResultCount);
            this.panel2.Controls.Add(this.gridDeliveries);
            this.panel2.Controls.Add(this.txtSearchCoupnBook);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 763);
            this.panel2.TabIndex = 1;
            // 
            // lblResultCount
            // 
            this.lblResultCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultCount.AutoSize = true;
            this.lblResultCount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblResultCount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblResultCount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblResultCount.Location = new System.Drawing.Point(89, 727);
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(142, 25);
            this.lblResultCount.TabIndex = 3;
            this.lblResultCount.Text = "0 search results";
            // 
            // gridDeliveries
            // 
            this.gridDeliveries.AllowUserToAddRows = false;
            this.gridDeliveries.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            this.gridDeliveries.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gridDeliveries.BackgroundColor = System.Drawing.Color.White;
            this.gridDeliveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDeliveries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCouponId,
            this.clmBookno,
            this.clmCustomer});
            this.gridDeliveries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDeliveries.Location = new System.Drawing.Point(0, 30);
            this.gridDeliveries.Margin = new System.Windows.Forms.Padding(4);
            this.gridDeliveries.Name = "gridDeliveries";
            this.gridDeliveries.ReadOnly = true;
            this.gridDeliveries.RowHeadersVisible = false;
            this.gridDeliveries.RowHeadersWidth = 51;
            this.gridDeliveries.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridDeliveries.RowTemplate.Height = 30;
            this.gridDeliveries.Size = new System.Drawing.Size(363, 733);
            this.gridDeliveries.TabIndex = 2;
            this.gridDeliveries.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDeliveries_CellClick);
            // 
            // clmCouponId
            // 
            this.clmCouponId.HeaderText = "CouponId";
            this.clmCouponId.MinimumWidth = 6;
            this.clmCouponId.Name = "clmCouponId";
            this.clmCouponId.ReadOnly = true;
            this.clmCouponId.Visible = false;
            this.clmCouponId.Width = 60;
            // 
            // clmBookno
            // 
            this.clmBookno.HeaderText = "BookNo";
            this.clmBookno.MinimumWidth = 6;
            this.clmBookno.Name = "clmBookno";
            this.clmBookno.ReadOnly = true;
            this.clmBookno.Width = 125;
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.MinimumWidth = 6;
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Width = 200;
            // 
            // txtSearchCoupnBook
            // 
            this.txtSearchCoupnBook.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearchCoupnBook.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchCoupnBook.Location = new System.Drawing.Point(0, 0);
            this.txtSearchCoupnBook.Name = "txtSearchCoupnBook";
            this.txtSearchCoupnBook.Size = new System.Drawing.Size(363, 30);
            this.txtSearchCoupnBook.TabIndex = 0;
            this.txtSearchCoupnBook.TextChanged += new System.EventHandler(this.txtSearchCoupnBook_TextChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(184, 12);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 47);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // CouponSaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 763);
            this.Controls.Add(this.panel1);
            this.Name = "CouponSaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coupon Sale";
            this.Load += new System.EventHandler(this.CouponSaleForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabNewCoupon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlCouponleafs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCouponLeafs)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeafEndNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeafStartNo)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabNewCoupon;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtSearchCoupnBook;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtBookAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRatePerLeaf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtLeafEndNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtLeafStartNo;
        private System.Windows.Forms.TextBox txtBookNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBookStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Button btnGenerateLeafs;
        private System.Windows.Forms.Panel pnlCouponleafs;
        private System.Windows.Forms.DataGridView gridCouponLeafs;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblTotalLeafs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView gridDeliveries;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCouponId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBookno;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.Label lblResultCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafrate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmLeafCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmLeafRedeem;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafStatus;
        private System.Windows.Forms.LinkLabel lnkReport;
        private System.Windows.Forms.Button btnDelete;
    }
}