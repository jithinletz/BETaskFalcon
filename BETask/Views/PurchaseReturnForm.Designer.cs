namespace BETask.Views
{
    partial class PurchaseReturnForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSaveContent = new System.Windows.Forms.Panel();
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
            this.rdlCash = new System.Windows.Forms.RadioButton();
            this.rdlBank = new System.Windows.Forms.RadioButton();
            this.rdlCredit = new System.Windows.Forms.RadioButton();
            this.txtCheque = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTin = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPurchaseNo = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
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
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1191, 654);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlSaveContent.Controls.Add(this.panel3);
            this.pnlSaveContent.Controls.Add(this.txtInvoiceDate);
            this.pnlSaveContent.Controls.Add(this.groupBox1);
            this.pnlSaveContent.Controls.Add(this.txtCheque);
            this.pnlSaveContent.Controls.Add(this.label10);
            this.pnlSaveContent.Controls.Add(this.cmbBank);
            this.pnlSaveContent.Controls.Add(this.label9);
            this.pnlSaveContent.Controls.Add(this.label8);
            this.pnlSaveContent.Controls.Add(this.txtTin);
            this.pnlSaveContent.Controls.Add(this.txtAddress);
            this.pnlSaveContent.Controls.Add(this.label7);
            this.pnlSaveContent.Controls.Add(this.cmbSupplier);
            this.pnlSaveContent.Controls.Add(this.label6);
            this.pnlSaveContent.Controls.Add(this.label5);
            this.pnlSaveContent.Controls.Add(this.txtOrder);
            this.pnlSaveContent.Controls.Add(this.label4);
            this.pnlSaveContent.Controls.Add(this.label3);
            this.pnlSaveContent.Controls.Add(this.txtInvoiceNo);
            this.pnlSaveContent.Controls.Add(this.label2);
            this.pnlSaveContent.Controls.Add(this.txtPurchaseNo);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1191, 585);
            this.pnlSaveContent.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgItems);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 221);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1191, 364);
            this.panel3.TabIndex = 1;
            // 
            // dgItems
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.RowTemplate.Height = 30;
            this.dgItems.Size = new System.Drawing.Size(1191, 215);
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
            this.ItemName.Name = "ItemName";
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ItemName.Width = 250;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Packing
            // 
            this.Packing.HeaderText = "Packing";
            this.Packing.Name = "Packing";
            this.Packing.Width = 90;
            // 
            // QTY
            // 
            this.QTY.HeaderText = "QTY";
            this.QTY.Name = "QTY";
            this.QTY.Width = 50;
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Width = 70;
            // 
            // Gross
            // 
            this.Gross.HeaderText = "Gross";
            this.Gross.Name = "Gross";
            this.Gross.Width = 70;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "DIscount";
            this.Discount.Name = "Discount";
            this.Discount.Width = 50;
            // 
            // Vat
            // 
            this.Vat.HeaderText = "Vat%";
            this.Vat.Name = "Vat";
            this.Vat.Width = 40;
            // 
            // VatAmount
            // 
            this.VatAmount.HeaderText = "Vat Amount";
            this.VatAmount.Name = "VatAmount";
            this.VatAmount.Width = 90;
            // 
            // Net
            // 
            this.Net.HeaderText = "Net";
            this.Net.Name = "Net";
            this.Net.Width = 90;
            // 
            // panel4
            // 
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
            this.panel4.Location = new System.Drawing.Point(0, 215);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1191, 149);
            this.panel4.TabIndex = 0;
            // 
            // txtTaxableAmount
            // 
            this.txtTaxableAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxableAmount.Enabled = false;
            this.txtTaxableAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTaxableAmount.Location = new System.Drawing.Point(700, 89);
            this.txtTaxableAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTaxableAmount.MaxLength = 50;
            this.txtTaxableAmount.Name = "txtTaxableAmount";
            this.txtTaxableAmount.Size = new System.Drawing.Size(187, 31);
            this.txtTaxableAmount.TabIndex = 47;
            this.txtTaxableAmount.Tag = "Dec";
            this.txtTaxableAmount.Text = "0.00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label11.Location = new System.Drawing.Point(607, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 25);
            this.label11.TabIndex = 48;
            this.label11.Text = "Taxable";
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmount.Enabled = false;
            this.txtNetAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNetAmount.Location = new System.Drawing.Point(1005, 89);
            this.txtNetAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNetAmount.MaxLength = 50;
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(170, 31);
            this.txtNetAmount.TabIndex = 44;
            this.txtNetAmount.Tag = "Dec";
            this.txtNetAmount.Text = "0.00";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label19.Location = new System.Drawing.Point(901, 91);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(42, 25);
            this.label19.TabIndex = 45;
            this.label19.Text = "Net";
            // 
            // txtRoundUp
            // 
            this.txtRoundUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoundUp.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRoundUp.Location = new System.Drawing.Point(440, 56);
            this.txtRoundUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRoundUp.MaxLength = 50;
            this.txtRoundUp.Name = "txtRoundUp";
            this.txtRoundUp.Size = new System.Drawing.Size(170, 31);
            this.txtRoundUp.TabIndex = 42;
            this.txtRoundUp.Tag = "Dec";
            this.txtRoundUp.Text = "0.00";
            this.txtRoundUp.Visible = false;
            this.txtRoundUp.TextChanged += new System.EventHandler(this.txtRoundUp_TextChanged);
            this.txtRoundUp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            this.txtRoundUp.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label17.Location = new System.Drawing.Point(407, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 25);
            this.label17.TabIndex = 43;
            this.label17.Text = "Round Off";
            this.label17.Visible = false;
            // 
            // txtTotalVat
            // 
            this.txtTotalVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalVat.Enabled = false;
            this.txtTotalVat.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalVat.Location = new System.Drawing.Point(1005, 47);
            this.txtTotalVat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotalVat.MaxLength = 50;
            this.txtTotalVat.Name = "txtTotalVat";
            this.txtTotalVat.Size = new System.Drawing.Size(170, 31);
            this.txtTotalVat.TabIndex = 40;
            this.txtTotalVat.Tag = "Dec";
            this.txtTotalVat.Text = "0.00";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(897, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 25);
            this.label18.TabIndex = 41;
            this.label18.Text = "Total Vat";
            // 
            // txtTotalDiscount
            // 
            this.txtTotalDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalDiscount.Enabled = false;
            this.txtTotalDiscount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalDiscount.Location = new System.Drawing.Point(423, 89);
            this.txtTotalDiscount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotalDiscount.MaxLength = 50;
            this.txtTotalDiscount.Name = "txtTotalDiscount";
            this.txtTotalDiscount.Size = new System.Drawing.Size(187, 31);
            this.txtTotalDiscount.TabIndex = 38;
            this.txtTotalDiscount.Tag = "Dec";
            this.txtTotalDiscount.Text = "0.00";
            this.txtTotalDiscount.Visible = false;
            this.txtTotalDiscount.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label15.Location = new System.Drawing.Point(418, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 25);
            this.label15.TabIndex = 39;
            this.label15.Text = "Discount";
            this.label15.Visible = false;
            // 
            // txtTotalBeforeVat
            // 
            this.txtTotalBeforeVat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBeforeVat.Enabled = false;
            this.txtTotalBeforeVat.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalBeforeVat.Location = new System.Drawing.Point(700, 47);
            this.txtTotalBeforeVat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotalBeforeVat.MaxLength = 50;
            this.txtTotalBeforeVat.Name = "txtTotalBeforeVat";
            this.txtTotalBeforeVat.Size = new System.Drawing.Size(187, 31);
            this.txtTotalBeforeVat.TabIndex = 36;
            this.txtTotalBeforeVat.Tag = "Dec";
            this.txtTotalBeforeVat.Text = "0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label16.Location = new System.Drawing.Point(607, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 25);
            this.label16.TabIndex = 37;
            this.label16.Text = "Gross";
            // 
            // txtBalance
            // 
            this.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalance.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBalance.Location = new System.Drawing.Point(121, 96);
            this.txtBalance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBalance.MaxLength = 50;
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(275, 31);
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
            this.label14.Location = new System.Drawing.Point(15, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 25);
            this.label14.TabIndex = 35;
            this.label14.Text = "Balance";
            this.label14.Visible = false;
            // 
            // txtCashPaid
            // 
            this.txtCashPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashPaid.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCashPaid.Location = new System.Drawing.Point(121, 58);
            this.txtCashPaid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCashPaid.MaxLength = 50;
            this.txtCashPaid.Name = "txtCashPaid";
            this.txtCashPaid.Size = new System.Drawing.Size(275, 31);
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
            this.label13.Location = new System.Drawing.Point(15, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 25);
            this.label13.TabIndex = 33;
            this.label13.Text = "Cash Paid";
            this.label13.Visible = false;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(121, 20);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemarks.MaxLength = 50;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(470, 31);
            this.txtRemarks.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label12.Location = new System.Drawing.Point(16, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 25);
            this.label12.TabIndex = 27;
            this.label12.Text = "Remarks";
            // 
            // txtInvoiceDate
            // 
            this.txtInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.txtInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtInvoiceDate.Location = new System.Drawing.Point(677, 54);
            this.txtInvoiceDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtInvoiceDate.Name = "txtInvoiceDate";
            this.txtInvoiceDate.Size = new System.Drawing.Size(260, 31);
            this.txtInvoiceDate.TabIndex = 5;
            this.txtInvoiceDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdlCash);
            this.groupBox1.Controls.Add(this.rdlBank);
            this.groupBox1.Controls.Add(this.rdlCredit);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.groupBox1.Location = new System.Drawing.Point(947, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(228, 123);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // rdlCash
            // 
            this.rdlCash.AutoSize = true;
            this.rdlCash.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlCash.Location = new System.Drawing.Point(20, 20);
            this.rdlCash.Margin = new System.Windows.Forms.Padding(4);
            this.rdlCash.Name = "rdlCash";
            this.rdlCash.Size = new System.Drawing.Size(74, 29);
            this.rdlCash.TabIndex = 23;
            this.rdlCash.Text = "Cash";
            this.rdlCash.UseVisualStyleBackColor = true;
            this.rdlCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // rdlBank
            // 
            this.rdlBank.AutoSize = true;
            this.rdlBank.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlBank.Location = new System.Drawing.Point(20, 81);
            this.rdlBank.Margin = new System.Windows.Forms.Padding(4);
            this.rdlBank.Name = "rdlBank";
            this.rdlBank.Size = new System.Drawing.Size(74, 29);
            this.rdlBank.TabIndex = 25;
            this.rdlBank.Text = "Bank";
            this.rdlBank.UseVisualStyleBackColor = true;
            this.rdlBank.CheckedChanged += new System.EventHandler(this.rdlBank_CheckedChanged);
            this.rdlBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // rdlCredit
            // 
            this.rdlCredit.AutoSize = true;
            this.rdlCredit.Checked = true;
            this.rdlCredit.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlCredit.Location = new System.Drawing.Point(20, 50);
            this.rdlCredit.Margin = new System.Windows.Forms.Padding(4);
            this.rdlCredit.Name = "rdlCredit";
            this.rdlCredit.Size = new System.Drawing.Size(84, 29);
            this.rdlCredit.TabIndex = 24;
            this.rdlCredit.TabStop = true;
            this.rdlCredit.Text = "Credit";
            this.rdlCredit.UseVisualStyleBackColor = true;
            this.rdlCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtCheque
            // 
            this.txtCheque.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCheque.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCheque.Location = new System.Drawing.Point(677, 133);
            this.txtCheque.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCheque.MaxLength = 50;
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.Size = new System.Drawing.Size(261, 31);
            this.txtCheque.TabIndex = 7;
            this.txtCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label10.Location = new System.Drawing.Point(527, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 25);
            this.label10.TabIndex = 20;
            this.label10.Text = "Cheque#";
            // 
            // cmbBank
            // 
            this.cmbBank.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBank.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(677, 172);
            this.cmbBank.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(260, 33);
            this.cmbBank.TabIndex = 8;
            this.cmbBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(527, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 25);
            this.label9.TabIndex = 18;
            this.label9.Text = "Bank";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(16, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 25);
            this.label8.TabIndex = 16;
            this.label8.Text = "TRN Number";
            // 
            // txtTin
            // 
            this.txtTin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTin.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTin.Location = new System.Drawing.Point(167, 175);
            this.txtTin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTin.MaxLength = 50;
            this.txtTin.Name = "txtTin";
            this.txtTin.Size = new System.Drawing.Size(333, 31);
            this.txtTin.TabIndex = 3;
            this.txtTin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(165, 94);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(332, 72);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.Text = "";
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(15, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 25);
            this.label7.TabIndex = 14;
            this.label7.Text = "Address";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(165, 58);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(332, 33);
            this.cmbSupplier.TabIndex = 1;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            this.cmbSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(15, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Supplier";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(527, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Order";
            // 
            // txtOrder
            // 
            this.txtOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrder.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtOrder.Location = new System.Drawing.Point(677, 91);
            this.txtOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOrder.MaxLength = 50;
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(261, 31);
            this.txtOrder.TabIndex = 6;
            this.txtOrder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(527, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Invoice Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(527, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Invoice#";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtInvoiceNo.Location = new System.Drawing.Point(677, 15);
            this.txtInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInvoiceNo.MaxLength = 50;
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(261, 31);
            this.txtInvoiceNo.TabIndex = 4;
            this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Purchase#";
            // 
            // txtPurchaseNo
            // 
            this.txtPurchaseNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchaseNo.Enabled = false;
            this.txtPurchaseNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPurchaseNo.Location = new System.Drawing.Point(165, 10);
            this.txtPurchaseNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPurchaseNo.MaxLength = 50;
            this.txtPurchaseNo.Name = "txtPurchaseNo";
            this.txtPurchaseNo.Size = new System.Drawing.Size(333, 31);
            this.txtPurchaseNo.TabIndex = 0;
            this.txtPurchaseNo.Text = "0";
            this.txtPurchaseNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnSearch);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnNew);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 585);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1191, 69);
            this.panel5.TabIndex = 46;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(130, 11);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(18, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 47);
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
            this.btnSave.Location = new System.Drawing.Point(1068, 11);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
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
            this.btnCancel.Location = new System.Drawing.Point(956, 11);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
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
            this.btnClose.Location = new System.Drawing.Point(844, 11);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
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
            this.btnNew.Location = new System.Drawing.Point(732, 11);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 47);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // PurchaseReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1191, 654);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PurchaseReturnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Return";
            this.Load += new System.EventHandler(this.PurchaseForm_Load);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.DataGridView dgItems;
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
        private System.Windows.Forms.RadioButton rdlCash;
        private System.Windows.Forms.RadioButton rdlBank;
        private System.Windows.Forms.RadioButton rdlCredit;
        private System.Windows.Forms.TextBox txtCheque;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTin;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPurchaseNo;
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