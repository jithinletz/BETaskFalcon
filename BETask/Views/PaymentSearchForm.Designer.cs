namespace BETask.Views
{
    partial class PaymentSearchForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridTransaction = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContains = new System.Windows.Forms.TextBox();
            this.chkLedger = new System.Windows.Forms.CheckBox();
            this.cmbLedgerAccount = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.clmTranId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmToAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNarration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCopy = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTransaction)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.gridTransaction);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1206, 711);
            this.panel1.TabIndex = 0;
            // 
            // gridTransaction
            // 
            this.gridTransaction.AllowUserToAddRows = false;
            this.gridTransaction.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridTransaction.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTransaction.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTransaction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTransaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTransaction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTranId,
            this.clmId,
            this.clmDate,
            this.clmToAccount,
            this.clmAmount,
            this.clmDocument,
            this.clmNarration,
            this.clmCopy});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTransaction.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTransaction.Location = new System.Drawing.Point(0, 100);
            this.gridTransaction.Name = "gridTransaction";
            this.gridTransaction.ReadOnly = true;
            this.gridTransaction.RowHeadersVisible = false;
            this.gridTransaction.RowHeadersWidth = 51;
            this.gridTransaction.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridTransaction.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridTransaction.RowTemplate.Height = 30;
            this.gridTransaction.Size = new System.Drawing.Size(1206, 547);
            this.gridTransaction.TabIndex = 3;
            this.gridTransaction.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTransaction_CellClick);
            this.gridTransaction.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTransaction_CellDoubleClick);
            this.gridTransaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridTransaction_KeyDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 647);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1206, 64);
            this.panel3.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1087, 6);
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
            this.btnClose.Location = new System.Drawing.Point(975, 6);
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
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtContains);
            this.panel2.Controls.Add(this.chkLedger);
            this.panel2.Controls.Add(this.cmbLedgerAccount);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.dtpDateTo);
            this.panel2.Controls.Add(this.dtpDateFrom);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1206, 100);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(759, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 36;
            this.label3.Text = "Contains";
            // 
            // txtContains
            // 
            this.txtContains.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContains.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtContains.Location = new System.Drawing.Point(764, 40);
            this.txtContains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtContains.MaxLength = 50;
            this.txtContains.Name = "txtContains";
            this.txtContains.Size = new System.Drawing.Size(295, 31);
            this.txtContains.TabIndex = 35;
            this.txtContains.TextChanged += new System.EventHandler(this.txtAfterSearch_TextChanged);
            // 
            // chkLedger
            // 
            this.chkLedger.AutoSize = true;
            this.chkLedger.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkLedger.Location = new System.Drawing.Point(473, 5);
            this.chkLedger.Name = "chkLedger";
            this.chkLedger.Size = new System.Drawing.Size(92, 29);
            this.chkLedger.TabIndex = 34;
            this.chkLedger.Text = "Ledger";
            this.chkLedger.UseVisualStyleBackColor = true;
            this.chkLedger.CheckedChanged += new System.EventHandler(this.chkLedger_CheckedChanged);
            // 
            // cmbLedgerAccount
            // 
            this.cmbLedgerAccount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbLedgerAccount.FormattingEnabled = true;
            this.cmbLedgerAccount.Location = new System.Drawing.Point(473, 40);
            this.cmbLedgerAccount.Name = "cmbLedgerAccount";
            this.cmbLedgerAccount.Size = new System.Drawing.Size(285, 33);
            this.cmbLedgerAccount.TabIndex = 33;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1065, 33);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 47);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(267, 40);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(200, 31);
            this.dtpDateTo.TabIndex = 14;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(37, 40);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(200, 31);
            this.dtpDateFrom.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(262, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Date To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Date From";
            // 
            // clmTranId
            // 
            this.clmTranId.HeaderText = "Doc";
            this.clmTranId.MinimumWidth = 6;
            this.clmTranId.Name = "clmTranId";
            this.clmTranId.ReadOnly = true;
            this.clmTranId.Width = 125;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "ID";
            this.clmId.MinimumWidth = 6;
            this.clmId.Name = "clmId";
            this.clmId.ReadOnly = true;
            this.clmId.Width = 125;
            // 
            // clmDate
            // 
            this.clmDate.HeaderText = "Date";
            this.clmDate.MinimumWidth = 6;
            this.clmDate.Name = "clmDate";
            this.clmDate.ReadOnly = true;
            this.clmDate.Width = 125;
            // 
            // clmToAccount
            // 
            this.clmToAccount.HeaderText = "PaidTo";
            this.clmToAccount.MinimumWidth = 6;
            this.clmToAccount.Name = "clmToAccount";
            this.clmToAccount.ReadOnly = true;
            this.clmToAccount.Width = 150;
            // 
            // clmAmount
            // 
            this.clmAmount.HeaderText = "Amount";
            this.clmAmount.MinimumWidth = 6;
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.ReadOnly = true;
            this.clmAmount.Width = 125;
            // 
            // clmDocument
            // 
            this.clmDocument.HeaderText = "Document";
            this.clmDocument.MinimumWidth = 6;
            this.clmDocument.Name = "clmDocument";
            this.clmDocument.ReadOnly = true;
            this.clmDocument.Width = 125;
            // 
            // clmNarration
            // 
            this.clmNarration.HeaderText = "Narration";
            this.clmNarration.MinimumWidth = 6;
            this.clmNarration.Name = "clmNarration";
            this.clmNarration.ReadOnly = true;
            this.clmNarration.Width = 200;
            // 
            // clmCopy
            // 
            this.clmCopy.HeaderText = "Copy This";
            this.clmCopy.MinimumWidth = 6;
            this.clmCopy.Name = "clmCopy";
            this.clmCopy.ReadOnly = true;
            this.clmCopy.Width = 125;
            // 
            // PaymentSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 711);
            this.Controls.Add(this.panel1);
            this.Name = "PaymentSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Search";
            this.Load += new System.EventHandler(this.PaymentSearchForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTransaction)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView gridTransaction;
        private System.Windows.Forms.ComboBox cmbLedgerAccount;
        private System.Windows.Forms.CheckBox chkLedger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContains;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTranId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmToAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNarration;
        private System.Windows.Forms.DataGridViewLinkColumn clmCopy;
    }
}