namespace BETask.Views
{
    partial class PaymentForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.clmLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVoucher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCostEntryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.linkCostCenter = new System.Windows.Forms.LinkLabel();
            this.linkSerach = new System.Windows.Forms.LinkLabel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDocumentNo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkCheque = new System.Windows.Forms.CheckBox();
            this.txtLedgerAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLedger = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.linkHide = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.grpChequeDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1084, 715);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.Controls.Add(this.grpChequeDetails);
            this.pnlSaveContent.Controls.Add(this.dgItems);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 170);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1084, 476);
            this.pnlSaveContent.TabIndex = 48;
            // 
            // grpChequeDetails
            // 
            this.grpChequeDetails.BackColor = System.Drawing.SystemColors.Highlight;
            this.grpChequeDetails.Controls.Add(this.linkHide);
            this.grpChequeDetails.Controls.Add(this.dtpChequeDate);
            this.grpChequeDetails.Controls.Add(this.txtChequeOther);
            this.grpChequeDetails.Controls.Add(this.txtChequeBank);
            this.grpChequeDetails.Controls.Add(this.txtChequeNumber);
            this.grpChequeDetails.Controls.Add(this.label9);
            this.grpChequeDetails.Controls.Add(this.label8);
            this.grpChequeDetails.Controls.Add(this.label7);
            this.grpChequeDetails.Controls.Add(this.label6);
            this.grpChequeDetails.Location = new System.Drawing.Point(712, 6);
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
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmLedgerId,
            this.clmLedger,
            this.clmAmount,
            this.clmVoucher,
            this.clmRemarks,
            this.clmCostEntryId});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.8F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 0);
            this.dgItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersWidth = 51;
            this.dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.8F);
            this.dgItems.RowTemplate.Height = 30;
            this.dgItems.Size = new System.Drawing.Size(1084, 476);
            this.dgItems.TabIndex = 0;
            this.dgItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgItems_CellValueChanged);
            this.dgItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgItems_CurrentCellDirtyStateChanged);
            this.dgItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgItems_EditingControlShowing);
            this.dgItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyDown);
            // 
            // clmLedgerId
            // 
            this.clmLedgerId.HeaderText = "ID";
            this.clmLedgerId.MinimumWidth = 6;
            this.clmLedgerId.Name = "clmLedgerId";
            this.clmLedgerId.Visible = false;
            this.clmLedgerId.Width = 125;
            // 
            // clmLedger
            // 
            this.clmLedger.HeaderText = "Account";
            this.clmLedger.MinimumWidth = 6;
            this.clmLedger.Name = "clmLedger";
            this.clmLedger.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmLedger.Width = 250;
            // 
            // clmAmount
            // 
            this.clmAmount.HeaderText = "Amount";
            this.clmAmount.MinimumWidth = 6;
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.Width = 125;
            // 
            // clmVoucher
            // 
            this.clmVoucher.HeaderText = "VocuherNo";
            this.clmVoucher.MinimumWidth = 6;
            this.clmVoucher.Name = "clmVoucher";
            this.clmVoucher.Width = 150;
            // 
            // clmRemarks
            // 
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmRemarks.DefaultCellStyle = dataGridViewCellStyle8;
            this.clmRemarks.HeaderText = "Remarks";
            this.clmRemarks.MinimumWidth = 6;
            this.clmRemarks.Name = "clmRemarks";
            this.clmRemarks.Width = 300;
            // 
            // clmCostEntryId
            // 
            this.clmCostEntryId.HeaderText = "CostEntryId";
            this.clmCostEntryId.MinimumWidth = 6;
            this.clmCostEntryId.Name = "clmCostEntryId";
            this.clmCostEntryId.Visible = false;
            this.clmCostEntryId.Width = 125;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.linkCostCenter);
            this.panel5.Controls.Add(this.linkSerach);
            this.panel5.Controls.Add(this.btnPrint);
            this.panel5.Controls.Add(this.btnSearch);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Controls.Add(this.btnNew);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 646);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1084, 69);
            this.panel5.TabIndex = 47;
            // 
            // linkCostCenter
            // 
            this.linkCostCenter.AutoSize = true;
            this.linkCostCenter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkCostCenter.ForeColor = System.Drawing.Color.White;
            this.linkCostCenter.LinkColor = System.Drawing.Color.White;
            this.linkCostCenter.Location = new System.Drawing.Point(375, 25);
            this.linkCostCenter.Name = "linkCostCenter";
            this.linkCostCenter.Size = new System.Drawing.Size(114, 20);
            this.linkCostCenter.TabIndex = 6;
            this.linkCostCenter.TabStop = true;
            this.linkCostCenter.Text = "F4 - Cost Center";
            this.linkCostCenter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkSerach
            // 
            this.linkSerach.AutoSize = true;
            this.linkSerach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkSerach.ForeColor = System.Drawing.Color.White;
            this.linkSerach.LinkColor = System.Drawing.Color.White;
            this.linkSerach.Location = new System.Drawing.Point(267, 25);
            this.linkSerach.Name = "linkSerach";
            this.linkSerach.Size = new System.Drawing.Size(82, 20);
            this.linkSerach.TabIndex = 6;
            this.linkSerach.TabStop = true;
            this.linkSerach.Text = "F3 - Search";
            this.linkSerach.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(129, 11);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 4;
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
            this.btnSearch.Location = new System.Drawing.Point(17, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 47);
            this.btnSearch.TabIndex = 5;
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
            this.btnSave.Location = new System.Drawing.Point(961, 11);
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
            this.btnCancel.Location = new System.Drawing.Point(849, 11);
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
            this.btnClose.Location = new System.Drawing.Point(737, 11);
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
            this.btnNew.Location = new System.Drawing.Point(625, 11);
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
            this.panel2.Controls.Add(this.txtDocumentNo);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.chkCheque);
            this.panel2.Controls.Add(this.txtLedgerAmount);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtAmount);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtRemarks);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmbLedger);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1084, 170);
            this.panel2.TabIndex = 0;
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumentNo.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.txtDocumentNo.Location = new System.Drawing.Point(12, 47);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.ReadOnly = true;
            this.txtDocumentNo.Size = new System.Drawing.Size(146, 31);
            this.txtDocumentNo.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(7, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 25);
            this.label10.TabIndex = 36;
            this.label10.Text = "Document";
            // 
            // chkCheque
            // 
            this.chkCheque.AutoSize = true;
            this.chkCheque.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkCheque.Location = new System.Drawing.Point(792, 88);
            this.chkCheque.Name = "chkCheque";
            this.chkCheque.Size = new System.Drawing.Size(161, 29);
            this.chkCheque.TabIndex = 34;
            this.chkCheque.Text = "Cheque Details";
            this.chkCheque.UseVisualStyleBackColor = true;
            this.chkCheque.CheckedChanged += new System.EventHandler(this.chkCheque_CheckedChanged);
            // 
            // txtLedgerAmount
            // 
            this.txtLedgerAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtLedgerAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLedgerAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLedgerAmount.Location = new System.Drawing.Point(867, 49);
            this.txtLedgerAmount.Name = "txtLedgerAmount";
            this.txtLedgerAmount.ReadOnly = true;
            this.txtLedgerAmount.Size = new System.Drawing.Size(205, 31);
            this.txtLedgerAmount.TabIndex = 7;
            this.txtLedgerAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(862, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 25);
            this.label5.TabIndex = 31;
            this.label5.Text = "Ledger Amount(Dr.)";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtAmount.Location = new System.Drawing.Point(660, 49);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(201, 31);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.txtAmount.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(655, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 25);
            this.label4.TabIndex = 29;
            this.label4.Text = "Payment Amount(Cr.)";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(20, 123);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemarks.MaxLength = 250;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(939, 31);
            this.txtRemarks.TabIndex = 4;
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(15, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transaction Narration";
            // 
            // cmbLedger
            // 
            this.cmbLedger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLedger.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbLedger.FormattingEnabled = true;
            this.cmbLedger.Location = new System.Drawing.Point(379, 47);
            this.cmbLedger.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLedger.Name = "cmbLedger";
            this.cmbLedger.Size = new System.Drawing.Size(274, 33);
            this.cmbLedger.TabIndex = 1;
            this.cmbLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(374, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "From Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(167, 19);
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
            this.dtpDate.Location = new System.Drawing.Point(172, 47);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 31);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // linkHide
            // 
            this.linkHide.AutoSize = true;
            this.linkHide.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkHide.ForeColor = System.Drawing.Color.Black;
            this.linkHide.LinkColor = System.Drawing.Color.Black;
            this.linkHide.Location = new System.Drawing.Point(296, 18);
            this.linkHide.Name = "linkHide";
            this.linkHide.Size = new System.Drawing.Size(41, 20);
            this.linkHide.TabIndex = 11;
            this.linkHide.TabStop = true;
            this.linkHide.Text = "Hide";
            this.linkHide.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHide_LinkClicked);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 715);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            this.Enter += new System.EventHandler(this.PaymentForm_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaymentForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.grpChequeDetails.ResumeLayout(false);
            this.grpChequeDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLedger;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtLedgerAmount;
        private System.Windows.Forms.Label label5;
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
        private System.Windows.Forms.LinkLabel linkSerach;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.LinkLabel linkCostCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVoucher;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRemarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCostEntryId;
        private System.Windows.Forms.TextBox txtDocumentNo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkHide;
    }
}