namespace BETask.Views
{
    partial class SupplierPaymentForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSaveContent = new System.Windows.Forms.Panel();
            this.grpChequeDetails = new System.Windows.Forms.GroupBox();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.txtChequeOther = new System.Windows.Forms.TextBox();
            this.txtChequeBank = new System.Windows.Forms.TextBox();
            this.txtChequeNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnDuePrint = new System.Windows.Forms.Button();
            this.lblpaid = new System.Windows.Forms.Label();
            this.lblTotalTobePaid = new System.Windows.Forms.Label();
            this.lblTotalDue = new System.Windows.Forms.Label();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdlCash = new System.Windows.Forms.RadioButton();
            this.rdlBank = new System.Windows.Forms.RadioButton();
            this.pnlBank = new System.Windows.Forms.Panel();
            this.cmbLedger = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCheque = new System.Windows.Forms.CheckBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.clmPurchaseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDueAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmToBePaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.grpChequeDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlBank.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlSaveContent);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1081, 715);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.Controls.Add(this.grpChequeDetails);
            this.pnlSaveContent.Controls.Add(this.dgItems);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 170);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1081, 451);
            this.pnlSaveContent.TabIndex = 48;
            // 
            // grpChequeDetails
            // 
            this.grpChequeDetails.BackColor = System.Drawing.SystemColors.Highlight;
            this.grpChequeDetails.Controls.Add(this.dtpChequeDate);
            this.grpChequeDetails.Controls.Add(this.txtChequeOther);
            this.grpChequeDetails.Controls.Add(this.txtChequeBank);
            this.grpChequeDetails.Controls.Add(this.txtChequeNumber);
            this.grpChequeDetails.Controls.Add(this.label9);
            this.grpChequeDetails.Controls.Add(this.label8);
            this.grpChequeDetails.Controls.Add(this.label7);
            this.grpChequeDetails.Controls.Add(this.label6);
            this.grpChequeDetails.Location = new System.Drawing.Point(720, 25);
            this.grpChequeDetails.Name = "grpChequeDetails";
            this.grpChequeDetails.Size = new System.Drawing.Size(353, 299);
            this.grpChequeDetails.TabIndex = 33;
            this.grpChequeDetails.TabStop = false;
            this.grpChequeDetails.Text = "Cheque Details";
            this.grpChequeDetails.Visible = false;
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.CustomFormat = "dd/MM/yyyy";
            this.dtpChequeDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChequeDate.Location = new System.Drawing.Point(21, 113);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(315, 31);
            this.dtpChequeDate.TabIndex = 1;
            // 
            // txtChequeOther
            // 
            this.txtChequeOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeOther.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtChequeOther.Location = new System.Drawing.Point(21, 246);
            this.txtChequeOther.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChequeOther.MaxLength = 250;
            this.txtChequeOther.Name = "txtChequeOther";
            this.txtChequeOther.Size = new System.Drawing.Size(315, 31);
            this.txtChequeOther.TabIndex = 3;
            // 
            // txtChequeBank
            // 
            this.txtChequeBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeBank.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtChequeBank.Location = new System.Drawing.Point(21, 183);
            this.txtChequeBank.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChequeBank.MaxLength = 250;
            this.txtChequeBank.Name = "txtChequeBank";
            this.txtChequeBank.Size = new System.Drawing.Size(315, 31);
            this.txtChequeBank.TabIndex = 2;
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeNumber.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtChequeNumber.Location = new System.Drawing.Point(21, 50);
            this.txtChequeNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChequeNumber.MaxLength = 250;
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(315, 31);
            this.txtChequeNumber.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(16, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 25);
            this.label9.TabIndex = 10;
            this.label9.Text = "Other";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(16, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 25);
            this.label8.TabIndex = 9;
            this.label8.Text = "Bank Details";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(16, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "Cheque Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(16, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Cheque Number";
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmPurchaseId,
            this.clmInvoiceDate,
            this.clmInvoiceNo,
            this.clmNetAmount,
            this.clmDueAmount,
            this.clmPaidAmount,
            this.clmToBePaid});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.8F);
            this.dgItems.RowTemplate.Height = 30;
            this.dgItems.Size = new System.Drawing.Size(1081, 451);
            this.dgItems.TabIndex = 0;
            this.dgItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellValueChanged);
            this.dgItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgItems_CurrentCellDirtyStateChanged);
            this.dgItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItems_EditingControlShowing);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.btnDuePrint);
            this.panel5.Controls.Add(this.lblpaid);
            this.panel5.Controls.Add(this.lblTotalTobePaid);
            this.panel5.Controls.Add(this.lblTotalDue);
            this.panel5.Controls.Add(this.lblTotalPaid);
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnNew);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 621);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1081, 94);
            this.panel5.TabIndex = 47;
            // 
            // btnDuePrint
            // 
            this.btnDuePrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDuePrint.BackColor = System.Drawing.Color.Purple;
            this.btnDuePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuePrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuePrint.ForeColor = System.Drawing.Color.White;
            this.btnDuePrint.Location = new System.Drawing.Point(131, 36);
            this.btnDuePrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDuePrint.Name = "btnDuePrint";
            this.btnDuePrint.Size = new System.Drawing.Size(179, 47);
            this.btnDuePrint.TabIndex = 10;
            this.btnDuePrint.Text = "&Due Report Print";
            this.btnDuePrint.UseVisualStyleBackColor = false;
            this.btnDuePrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // lblpaid
            // 
            this.lblpaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblpaid.AutoSize = true;
            this.lblpaid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaid.ForeColor = System.Drawing.SystemColors.Window;
            this.lblpaid.Location = new System.Drawing.Point(714, 6);
            this.lblpaid.Name = "lblpaid";
            this.lblpaid.Size = new System.Drawing.Size(81, 20);
            this.lblpaid.TabIndex = 9;
            this.lblpaid.Text = "Total Paid :";
            // 
            // lblTotalTobePaid
            // 
            this.lblTotalTobePaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalTobePaid.AutoSize = true;
            this.lblTotalTobePaid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTobePaid.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalTobePaid.Location = new System.Drawing.Point(934, 6);
            this.lblTotalTobePaid.Name = "lblTotalTobePaid";
            this.lblTotalTobePaid.Size = new System.Drawing.Size(36, 20);
            this.lblTotalTobePaid.TabIndex = 8;
            this.lblTotalTobePaid.Text = "0.00";
            // 
            // lblTotalDue
            // 
            this.lblTotalDue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalDue.AutoSize = true;
            this.lblTotalDue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDue.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalDue.Location = new System.Drawing.Point(544, 6);
            this.lblTotalDue.Name = "lblTotalDue";
            this.lblTotalDue.Size = new System.Drawing.Size(36, 20);
            this.lblTotalDue.TabIndex = 7;
            this.lblTotalDue.Text = "0.00";
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPaid.AutoSize = true;
            this.lblTotalPaid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPaid.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTotalPaid.Location = new System.Drawing.Point(809, 6);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(36, 20);
            this.lblTotalPaid.TabIndex = 6;
            this.lblTotalPaid.Text = "0.00";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(20, 36);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(958, 36);
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
            this.btnCancel.Location = new System.Drawing.Point(846, 36);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cance&l";
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
            this.btnClose.Location = new System.Drawing.Point(734, 36);
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
            this.btnNew.Location = new System.Drawing.Point(622, 36);
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
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.cmbSupplier);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.txtRemarks);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1081, 170);
            this.panel2.TabIndex = 0;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(20, 123);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(332, 33);
            this.cmbSupplier.TabIndex = 37;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(16, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "Supplier";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdlCash);
            this.groupBox1.Controls.Add(this.rdlBank);
            this.groupBox1.Controls.Add(this.pnlBank);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.groupBox1.Location = new System.Drawing.Point(474, -4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(603, 121);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Mode";
            // 
            // rdlCash
            // 
            this.rdlCash.AutoSize = true;
            this.rdlCash.Checked = true;
            this.rdlCash.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlCash.Location = new System.Drawing.Point(20, 32);
            this.rdlCash.Margin = new System.Windows.Forms.Padding(4);
            this.rdlCash.Name = "rdlCash";
            this.rdlCash.Size = new System.Drawing.Size(74, 29);
            this.rdlCash.TabIndex = 23;
            this.rdlCash.TabStop = true;
            this.rdlCash.Text = "Cash";
            this.rdlCash.UseVisualStyleBackColor = true;
            this.rdlCash.CheckedChanged += new System.EventHandler(this.rdlCash_CheckedChanged);
            // 
            // rdlBank
            // 
            this.rdlBank.AutoSize = true;
            this.rdlBank.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdlBank.Location = new System.Drawing.Point(20, 69);
            this.rdlBank.Margin = new System.Windows.Forms.Padding(4);
            this.rdlBank.Name = "rdlBank";
            this.rdlBank.Size = new System.Drawing.Size(74, 29);
            this.rdlBank.TabIndex = 25;
            this.rdlBank.Text = "Bank";
            this.rdlBank.UseVisualStyleBackColor = true;
            this.rdlBank.CheckedChanged += new System.EventHandler(this.rdlBank_CheckedChanged);
            // 
            // pnlBank
            // 
            this.pnlBank.Controls.Add(this.cmbLedger);
            this.pnlBank.Controls.Add(this.label1);
            this.pnlBank.Controls.Add(this.chkCheque);
            this.pnlBank.Enabled = false;
            this.pnlBank.Location = new System.Drawing.Point(214, 16);
            this.pnlBank.Name = "pnlBank";
            this.pnlBank.Size = new System.Drawing.Size(381, 98);
            this.pnlBank.TabIndex = 36;
            // 
            // cmbLedger
            // 
            this.cmbLedger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLedger.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbLedger.FormattingEnabled = true;
            this.cmbLedger.Location = new System.Drawing.Point(53, 33);
            this.cmbLedger.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLedger.Name = "cmbLedger";
            this.cmbLedger.Size = new System.Drawing.Size(313, 33);
            this.cmbLedger.TabIndex = 1;
            this.cmbLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(48, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bank";
            // 
            // chkCheque
            // 
            this.chkCheque.AutoSize = true;
            this.chkCheque.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkCheque.Location = new System.Drawing.Point(54, 67);
            this.chkCheque.Name = "chkCheque";
            this.chkCheque.Size = new System.Drawing.Size(161, 29);
            this.chkCheque.TabIndex = 34;
            this.chkCheque.Text = "Cheque Details";
            this.chkCheque.UseVisualStyleBackColor = true;
            this.chkCheque.CheckedChanged += new System.EventHandler(this.chkCheque_CheckedChanged);
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(474, 123);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemarks.MaxLength = 250;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(604, 31);
            this.txtRemarks.TabIndex = 4;
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(370, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Remarks";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(16, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Payment Date";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(20, 42);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 31);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // clmPurchaseId
            // 
            this.clmPurchaseId.HeaderText = "PurchaseID";
            this.clmPurchaseId.Name = "clmPurchaseId";
            this.clmPurchaseId.Visible = false;
            // 
            // clmInvoiceDate
            // 
            this.clmInvoiceDate.HeaderText = "Invoice Date";
            this.clmInvoiceDate.Name = "clmInvoiceDate";
            this.clmInvoiceDate.Width = 150;
            // 
            // clmInvoiceNo
            // 
            this.clmInvoiceNo.HeaderText = "Invoice No";
            this.clmInvoiceNo.Name = "clmInvoiceNo";
            this.clmInvoiceNo.Width = 80;
            // 
            // clmNetAmount
            // 
            this.clmNetAmount.HeaderText = "Net Amount";
            this.clmNetAmount.Name = "clmNetAmount";
            this.clmNetAmount.Width = 150;
            // 
            // clmDueAmount
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDueAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDueAmount.HeaderText = "Due Amount";
            this.clmDueAmount.Name = "clmDueAmount";
            this.clmDueAmount.Width = 150;
            // 
            // clmPaidAmount
            // 
            this.clmPaidAmount.HeaderText = "Paid Amount";
            this.clmPaidAmount.Name = "clmPaidAmount";
            this.clmPaidAmount.Width = 150;
            // 
            // clmToBePaid
            // 
            this.clmToBePaid.HeaderText = "To Be Paid";
            this.clmToBePaid.Name = "clmToBePaid";
            this.clmToBePaid.Width = 150;
            // 
            // SupplierPaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 715);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "SupplierPaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupplierPayment";
            this.Load += new System.EventHandler(this.SupplierPaymentForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.grpChequeDetails.ResumeLayout(false);
            this.grpChequeDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlBank.ResumeLayout(false);
            this.pnlBank.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLedger;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.GroupBox grpChequeDetails;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.TextBox txtChequeOther;
        private System.Windows.Forms.TextBox txtChequeBank;
        private System.Windows.Forms.TextBox txtChequeNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkCheque;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.Panel pnlBank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdlCash;
        private System.Windows.Forms.RadioButton rdlBank;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Label lblTotalTobePaid;
        private System.Windows.Forms.Label lblTotalDue;
        private System.Windows.Forms.Label lblpaid;
        private System.Windows.Forms.Button btnDuePrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPurchaseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNetAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDueAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaidAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmToBePaid;
    }
}