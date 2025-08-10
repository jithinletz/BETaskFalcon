namespace BETask.Views
{
    partial class SaleForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSaveContent = new System.Windows.Forms.Panel();
            this.txtLPO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Packing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VatAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Net = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbDivision = new System.Windows.Forms.ComboBox();
            this.txtTaxableAmount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtRoundUp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTotalVat = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTotalDiscount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTotalBeforeVat = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCashPaid = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTerms = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDeliveryLeaf = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCouponBalance = new System.Windows.Forms.Label();
            this.txtCheque = new System.Windows.Forms.TextBox();
            this.lblCheque = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTin = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSaleNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSaleNo = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtInvoiceSerch = new System.Windows.Forms.TextBox();
            this.lnkTrackDelivery = new System.Windows.Forms.LinkLabel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSaveContent);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1786, 1022);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlSaveContent.Controls.Add(this.txtLPO);
            this.pnlSaveContent.Controls.Add(this.label5);
            this.pnlSaveContent.Controls.Add(this.txtOrder);
            this.pnlSaveContent.Controls.Add(this.panel3);
            this.pnlSaveContent.Controls.Add(this.txtInvoiceDate);
            this.pnlSaveContent.Controls.Add(this.groupBox1);
            this.pnlSaveContent.Controls.Add(this.txtCheque);
            this.pnlSaveContent.Controls.Add(this.lblCheque);
            this.pnlSaveContent.Controls.Add(this.cmbBank);
            this.pnlSaveContent.Controls.Add(this.label9);
            this.pnlSaveContent.Controls.Add(this.label8);
            this.pnlSaveContent.Controls.Add(this.txtTin);
            this.pnlSaveContent.Controls.Add(this.txtAddress);
            this.pnlSaveContent.Controls.Add(this.label7);
            this.pnlSaveContent.Controls.Add(this.cmbSupplier);
            this.pnlSaveContent.Controls.Add(this.label6);
            this.pnlSaveContent.Controls.Add(this.label4);
            this.pnlSaveContent.Controls.Add(this.label3);
            this.pnlSaveContent.Controls.Add(this.txtSaleNumber);
            this.pnlSaveContent.Controls.Add(this.label2);
            this.pnlSaveContent.Controls.Add(this.txtSaleNo);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveContent.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1786, 914);
            this.pnlSaveContent.TabIndex = 0;
            // 
            // txtLPO
            // 
            this.txtLPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLPO.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLPO.Location = new System.Drawing.Point(981, 88);
            this.txtLPO.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLPO.MaxLength = 50;
            this.txtLPO.Name = "txtLPO";
            this.txtLPO.Size = new System.Drawing.Size(390, 46);
            this.txtLPO.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(790, 148);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 40);
            this.label5.TabIndex = 28;
            this.label5.Text = "Order";
            // 
            // txtOrder
            // 
            this.txtOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrder.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtOrder.Location = new System.Drawing.Point(981, 147);
            this.txtOrder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOrder.MaxLength = 50;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(390, 46);
            this.txtOrder.TabIndex = 27;
            this.txtOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgItems);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 345);
            this.panel3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1786, 569);
            this.panel3.TabIndex = 1;
            // 
            // dgItems
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.ID,
            this.Packing,
            this.QTY,
            this.Rate,
            this.Gross,
            this.Discount,
            this.Vat,
            this.VatAmount,
            this.Net});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersWidth = 51;
            this.dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.RowTemplate.Height = 30;
            this.dgItems.Size = new System.Drawing.Size(1786, 336);
            this.dgItems.TabIndex = 1;
            this.dgItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellValueChanged);
            this.dgItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgItems_CurrentCellDirtyStateChanged);
            this.dgItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItems_EditingControlShowing);
            this.dgItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyDown);
            // 
            // ItemName
            // 
            this.ItemName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.MaxDropDownItems = 100;
            this.ItemName.MinimumWidth = 6;
            this.ItemName.Name = "ItemName";
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ItemName.Width = 250;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 125;
            // 
            // Packing
            // 
            this.Packing.HeaderText = "Packing";
            this.Packing.MinimumWidth = 6;
            this.Packing.Name = "Packing";
            this.Packing.Width = 90;
            // 
            // QTY
            // 
            this.QTY.HeaderText = "QTY";
            this.QTY.MinimumWidth = 6;
            this.QTY.Name = "QTY";
            this.QTY.Width = 50;
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.MinimumWidth = 6;
            this.Rate.Name = "Rate";
            this.Rate.Width = 70;
            // 
            // Gross
            // 
            this.Gross.HeaderText = "Gross";
            this.Gross.MinimumWidth = 6;
            this.Gross.Name = "Gross";
            this.Gross.Width = 70;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "DIscount";
            this.Discount.MinimumWidth = 6;
            this.Discount.Name = "Discount";
            this.Discount.Width = 50;
            // 
            // Vat
            // 
            this.Vat.HeaderText = "Vat%";
            this.Vat.MinimumWidth = 6;
            this.Vat.Name = "Vat";
            this.Vat.Width = 40;
            // 
            // VatAmount
            // 
            this.VatAmount.HeaderText = "Vat Amount";
            this.VatAmount.MinimumWidth = 6;
            this.VatAmount.Name = "VatAmount";
            this.VatAmount.Width = 90;
            // 
            // Net
            // 
            this.Net.HeaderText = "Net";
            this.Net.MinimumWidth = 6;
            this.Net.Name = "Net";
            this.Net.Width = 90;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbRoute);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.cmbDivision);
            this.panel4.Controls.Add(this.txtTaxableAmount);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.txtNetAmount);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.txtRoundUp);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.txtTotalVat);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.txtTotalDiscount);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.txtTotalBeforeVat);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.txtBalance);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.txtCashPaid);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.txtRemarks);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 336);
            this.panel4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1786, 233);
            this.panel4.TabIndex = 0;
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(586, 156);
            this.cmbRoute.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(298, 48);
            this.cmbRoute.TabIndex = 81;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label22.Location = new System.Drawing.Point(460, 161);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 40);
            this.label22.TabIndex = 80;
            this.label22.Text = "Route";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label21.Location = new System.Drawing.Point(28, 161);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(117, 40);
            this.label21.TabIndex = 79;
            this.label21.Text = "Division";
            this.label21.Visible = false;
            // 
            // cmbDivision
            // 
            this.cmbDivision.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbDivision.FormattingEnabled = true;
            this.cmbDivision.Location = new System.Drawing.Point(182, 156);
            this.cmbDivision.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbDivision.Name = "cmbDivision";
            this.cmbDivision.Size = new System.Drawing.Size(250, 48);
            this.cmbDivision.TabIndex = 78;
            // 
            // txtTaxableAmount
            // 
            this.txtTaxableAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxableAmount.Enabled = false;
            this.txtTaxableAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTaxableAmount.Location = new System.Drawing.Point(1050, 150);
            this.txtTaxableAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTaxableAmount.MaxLength = 50;
            this.txtTaxableAmount.Name = "txtTaxableAmount";
            this.txtTaxableAmount.Size = new System.Drawing.Size(280, 46);
            this.txtTaxableAmount.TabIndex = 47;
            this.txtTaxableAmount.Tag = "Dec";
            this.txtTaxableAmount.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label11.Location = new System.Drawing.Point(910, 153);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 40);
            this.label11.TabIndex = 48;
            this.label11.Text = "Taxable";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmount.Enabled = false;
            this.txtNetAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNetAmount.Location = new System.Drawing.Point(1508, 150);
            this.txtNetAmount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNetAmount.MaxLength = 50;
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(254, 46);
            this.txtNetAmount.TabIndex = 44;
            this.txtNetAmount.Tag = "Dec";
            this.txtNetAmount.Text = "0.00";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label19.Location = new System.Drawing.Point(1352, 153);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 40);
            this.label19.TabIndex = 45;
            this.label19.Text = "Net";
            // 
            // txtRoundUp
            // 
            this.txtRoundUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoundUp.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRoundUp.Location = new System.Drawing.Point(1508, 91);
            this.txtRoundUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRoundUp.MaxLength = 50;
            this.txtRoundUp.Name = "txtRoundUp";
            this.txtRoundUp.Size = new System.Drawing.Size(254, 46);
            this.txtRoundUp.TabIndex = 42;
            this.txtRoundUp.Tag = "Dec";
            this.txtRoundUp.Text = "0.00";
            this.txtRoundUp.TextChanged += new System.EventHandler(this.txtRoundUp_TextChanged);
            this.txtRoundUp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            this.txtRoundUp.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label17.Location = new System.Drawing.Point(1352, 94);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(147, 40);
            this.label17.TabIndex = 43;
            this.label17.Text = "Round Off";
            // 
            // txtTotalVat
            // 
            this.txtTotalVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalVat.Enabled = false;
            this.txtTotalVat.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalVat.Location = new System.Drawing.Point(1508, 31);
            this.txtTotalVat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalVat.MaxLength = 50;
            this.txtTotalVat.Name = "txtTotalVat";
            this.txtTotalVat.Size = new System.Drawing.Size(254, 46);
            this.txtTotalVat.TabIndex = 40;
            this.txtTotalVat.Tag = "Dec";
            this.txtTotalVat.Text = "0.00";
            this.txtTotalVat.DoubleClick += new System.EventHandler(this.txtTotalVat_DoubleClick);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(1346, 39);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 40);
            this.label18.TabIndex = 41;
            this.label18.Text = "Total Vat";
            // 
            // txtTotalDiscount
            // 
            this.txtTotalDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDiscount.Enabled = false;
            this.txtTotalDiscount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalDiscount.Location = new System.Drawing.Point(1050, 91);
            this.txtTotalDiscount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalDiscount.MaxLength = 50;
            this.txtTotalDiscount.Name = "txtTotalDiscount";
            this.txtTotalDiscount.Size = new System.Drawing.Size(280, 46);
            this.txtTotalDiscount.TabIndex = 38;
            this.txtTotalDiscount.Tag = "Dec";
            this.txtTotalDiscount.Text = "0.00";
            this.txtTotalDiscount.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label15.Location = new System.Drawing.Point(910, 94);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(128, 40);
            this.label15.TabIndex = 39;
            this.label15.Text = "Discount";
            // 
            // txtTotalBeforeVat
            // 
            this.txtTotalBeforeVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBeforeVat.Enabled = false;
            this.txtTotalBeforeVat.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalBeforeVat.Location = new System.Drawing.Point(1050, 31);
            this.txtTotalBeforeVat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalBeforeVat.MaxLength = 50;
            this.txtTotalBeforeVat.Name = "txtTotalBeforeVat";
            this.txtTotalBeforeVat.Size = new System.Drawing.Size(280, 46);
            this.txtTotalBeforeVat.TabIndex = 36;
            this.txtTotalBeforeVat.Tag = "Dec";
            this.txtTotalBeforeVat.Text = "0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label16.Location = new System.Drawing.Point(910, 34);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 40);
            this.label16.TabIndex = 37;
            this.label16.Text = "Gross";
            // 
            // txtBalance
            // 
            this.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalance.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBalance.Location = new System.Drawing.Point(586, 91);
            this.txtBalance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBalance.MaxLength = 50;
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(299, 46);
            this.txtBalance.TabIndex = 34;
            this.txtBalance.Tag = "Dec";
            this.txtBalance.Text = "0.00";
            this.txtBalance.Visible = false;
            this.txtBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label14.Location = new System.Drawing.Point(460, 94);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 40);
            this.label14.TabIndex = 35;
            this.label14.Text = "Balance";
            this.label14.Visible = false;
            // 
            // txtCashPaid
            // 
            this.txtCashPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashPaid.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCashPaid.Location = new System.Drawing.Point(182, 91);
            this.txtCashPaid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCashPaid.MaxLength = 50;
            this.txtCashPaid.Name = "txtCashPaid";
            this.txtCashPaid.Size = new System.Drawing.Size(251, 46);
            this.txtCashPaid.TabIndex = 32;
            this.txtCashPaid.Tag = "Dec";
            this.txtCashPaid.Text = "0.00";
            this.txtCashPaid.Visible = false;
            this.txtCashPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label13.Location = new System.Drawing.Point(22, 94);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 40);
            this.label13.TabIndex = 33;
            this.label13.Text = "Cash Paid";
            this.label13.Visible = false;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(182, 9);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRemarks.MaxLength = 50;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(704, 76);
            this.txtRemarks.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label12.Location = new System.Drawing.Point(22, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 40);
            this.label12.TabIndex = 27;
            this.label12.Text = "Remarks";
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.txtInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtInvoiceDate.Location = new System.Drawing.Point(458, 16);
            this.txtInvoiceDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Size = new System.Drawing.Size(290, 46);
            this.txtInvoiceDate.TabIndex = 5;
            this.txtInvoiceDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTerms);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtDeliveryLeaf);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.cmbPaymentMode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblCouponBalance);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.groupBox1.Location = new System.Drawing.Point(1383, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(384, 339);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // cmbTerms
            // 
            this.cmbTerms.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTerms.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTerms.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbTerms.FormattingEnabled = true;
            this.cmbTerms.Items.AddRange(new object[] {
            "30 days",
            "60 days",
            "15 days",
            "10 days",
            "7 days"});
            this.cmbTerms.Location = new System.Drawing.Point(27, 183);
            this.cmbTerms.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbTerms.MaxLength = 25;
            this.cmbTerms.Name = "cmbTerms";
            this.cmbTerms.Size = new System.Drawing.Size(320, 48);
            this.cmbTerms.TabIndex = 84;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label10.Location = new System.Drawing.Point(20, 138);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 40);
            this.label10.TabIndex = 83;
            this.label10.Text = "Terms";
            // 
            // txtDeliveryLeaf
            // 
            this.txtDeliveryLeaf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeliveryLeaf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeliveryLeaf.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDeliveryLeaf.Location = new System.Drawing.Point(27, 281);
            this.txtDeliveryLeaf.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDeliveryLeaf.MaxLength = 50;
            this.txtDeliveryLeaf.Name = "txtDeliveryLeaf";
            this.txtDeliveryLeaf.Size = new System.Drawing.Size(322, 46);
            this.txtDeliveryLeaf.TabIndex = 80;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label20.Location = new System.Drawing.Point(20, 239);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(198, 40);
            this.label20.TabIndex = 79;
            this.label20.Text = "Delivered Leaf";
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(27, 80);
            this.cmbPaymentMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(320, 48);
            this.cmbPaymentMode.TabIndex = 77;
            this.cmbPaymentMode.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 40);
            this.label1.TabIndex = 78;
            this.label1.Text = "Payment Mode";
            // 
            // lblCouponBalance
            // 
            this.lblCouponBalance.AutoSize = true;
            this.lblCouponBalance.ForeColor = System.Drawing.Color.Green;
            this.lblCouponBalance.Location = new System.Drawing.Point(280, 33);
            this.lblCouponBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCouponBalance.Name = "lblCouponBalance";
            this.lblCouponBalance.Size = new System.Drawing.Size(71, 40);
            this.lblCouponBalance.TabIndex = 27;
            this.lblCouponBalance.Text = "0.00";
            // 
            // txtCheque
            // 
            this.txtCheque.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheque.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCheque.Location = new System.Drawing.Point(980, 208);
            this.txtCheque.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCheque.MaxLength = 50;
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.Size = new System.Drawing.Size(390, 46);
            this.txtCheque.TabIndex = 7;
            this.txtCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // lblCheque
            // 
            this.lblCheque.AutoSize = true;
            this.lblCheque.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCheque.Location = new System.Drawing.Point(789, 211);
            this.lblCheque.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCheque.Name = "lblCheque";
            this.lblCheque.Size = new System.Drawing.Size(131, 40);
            this.lblCheque.TabIndex = 20;
            this.lblCheque.Text = "Cheque#";
            // 
            // cmbBank
            // 
            this.cmbBank.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBank.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(980, 269);
            this.cmbBank.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(388, 48);
            this.cmbBank.TabIndex = 8;
            this.cmbBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(789, 273);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 40);
            this.label9.TabIndex = 18;
            this.label9.Text = "Bank";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(24, 277);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(184, 40);
            this.label8.TabIndex = 16;
            this.label8.Text = "TRN Number";
            // 
            // txtTin
            // 
            this.txtTin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTin.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTin.Location = new System.Drawing.Point(250, 273);
            this.txtTin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTin.MaxLength = 50;
            this.txtTin.Name = "txtTin";
            this.txtTin.Size = new System.Drawing.Size(498, 46);
            this.txtTin.TabIndex = 3;
            this.txtTin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(248, 147);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(496, 110);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.Text = "";
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(22, 153);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 40);
            this.label7.TabIndex = 14;
            this.label7.Text = "Address";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(248, 83);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(496, 48);
            this.cmbSupplier.TabIndex = 1;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            this.cmbSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(22, 91);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 40);
            this.label6.TabIndex = 12;
            this.label6.Text = "Customer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(790, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 40);
            this.label4.TabIndex = 8;
            this.label4.Text = "LPO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(790, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 40);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sale Number";
            // 
            // txtSaleNumber
            // 
            this.txtSaleNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaleNumber.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSaleNumber.Location = new System.Drawing.Point(981, 23);
            this.txtSaleNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSaleNumber.MaxLength = 50;
            this.txtSaleNumber.Name = "txtSaleNumber";
            this.txtSaleNumber.Size = new System.Drawing.Size(390, 46);
            this.txtSaleNumber.TabIndex = 4;
            this.txtSaleNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(22, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sale#";
            this.label2.DoubleClick += new System.EventHandler(this.label2_DoubleClick);
            // 
            // txtSaleNo
            // 
            this.txtSaleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSaleNo.Enabled = false;
            this.txtSaleNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSaleNo.Location = new System.Drawing.Point(248, 16);
            this.txtSaleNo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSaleNo.MaxLength = 50;
            this.txtSaleNo.Name = "txtSaleNo";
            this.txtSaleNo.Size = new System.Drawing.Size(198, 46);
            this.txtSaleNo.TabIndex = 0;
            this.txtSaleNo.Text = "0";
            this.txtSaleNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.txtInvoiceSerch);
            this.panel5.Controls.Add(this.lnkTrackDelivery);
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnSearch);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnNew);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 914);
            this.panel5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1786, 108);
            this.panel5.TabIndex = 46;
            // 
            // txtInvoiceSerch
            // 
            this.txtInvoiceSerch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvoiceSerch.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtInvoiceSerch.Location = new System.Drawing.Point(6, 30);
            this.txtInvoiceSerch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtInvoiceSerch.MaxLength = 50;
            this.txtInvoiceSerch.Name = "txtInvoiceSerch";
            this.txtInvoiceSerch.Size = new System.Drawing.Size(234, 46);
            this.txtInvoiceSerch.TabIndex = 12;
            // 
            // lnkTrackDelivery
            // 
            this.lnkTrackDelivery.AutoSize = true;
            this.lnkTrackDelivery.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lnkTrackDelivery.LinkColor = System.Drawing.Color.White;
            this.lnkTrackDelivery.Location = new System.Drawing.Point(675, 33);
            this.lnkTrackDelivery.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkTrackDelivery.Name = "lnkTrackDelivery";
            this.lnkTrackDelivery.Size = new System.Drawing.Size(214, 40);
            this.lnkTrackDelivery.TabIndex = 11;
            this.lnkTrackDelivery.TabStop = true;
            this.lnkTrackDelivery.Text = "Import Delivery";
            this.lnkTrackDelivery.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTrackDelivery_LinkClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(417, 17);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(160, 73);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(249, 17);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(160, 73);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1602, 17);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 73);
            this.btnSave.TabIndex = 4;
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
            this.btnCancel.Location = new System.Drawing.Point(1434, 17);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 73);
            this.btnCancel.TabIndex = 5;
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
            this.btnClose.Location = new System.Drawing.Point(1266, 17);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 73);
            this.btnClose.TabIndex = 6;
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
            this.btnNew.Location = new System.Drawing.Point(1098, 17);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(160, 73);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // SaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1786, 1022);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales";
            this.Load += new System.EventHandler(this.SaleForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SaleForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.pnlSaveContent.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtRoundUp;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTotalVat;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTotalDiscount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTotalBeforeVat;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCashPaid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCheque;
        private System.Windows.Forms.Label lblCheque;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTin;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSaleNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSaleNo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtTaxableAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker txtInvoiceDate;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.LinkLabel lnkTrackDelivery;
        private System.Windows.Forms.Label lblCouponBalance;
        private System.Windows.Forms.TextBox txtInvoiceSerch;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeliveryLeaf;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbDivision;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.TextBox txtLPO;
        private System.Windows.Forms.ComboBox cmbTerms;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.DataGridViewComboBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Packing;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gross;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vat;
        private System.Windows.Forms.DataGridViewTextBoxColumn VatAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Net;
    }
}