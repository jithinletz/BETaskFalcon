namespace BETask.Views
{
    partial class CostCenterEntryForm
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
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.clmEntryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLedgerTotal = new System.Windows.Forms.Label();
            this.lblLedgerName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCostCenter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrimaryCostCenter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.dgItems);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 544);
            this.panel1.TabIndex = 0;
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItems.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmEntryId,
            this.clmLedgerId,
            this.clmLedger,
            this.clmAmount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(0, 109);
            this.dgItems.Margin = new System.Windows.Forms.Padding(4);
            this.dgItems.Name = "dgItems";
            this.dgItems.ReadOnly = true;
            this.dgItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dgItems.RowTemplate.Height = 35;
            this.dgItems.Size = new System.Drawing.Size(865, 362);
            this.dgItems.TabIndex = 2;
            this.dgItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgItems_KeyDown);
            // 
            // clmEntryId
            // 
            this.clmEntryId.HeaderText = "EntryId";
            this.clmEntryId.Name = "clmEntryId";
            this.clmEntryId.ReadOnly = true;
            this.clmEntryId.Visible = false;
            // 
            // clmLedgerId
            // 
            this.clmLedgerId.HeaderText = "ID";
            this.clmLedgerId.Name = "clmLedgerId";
            this.clmLedgerId.ReadOnly = true;
            this.clmLedgerId.Visible = false;
            // 
            // clmLedger
            // 
            this.clmLedger.HeaderText = "Account";
            this.clmLedger.Name = "clmLedger";
            this.clmLedger.ReadOnly = true;
            this.clmLedger.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmLedger.Width = 300;
            // 
            // clmAmount
            // 
            this.clmAmount.HeaderText = "Amount";
            this.clmAmount.Name = "clmAmount";
            this.clmAmount.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Info;
            this.panel3.Controls.Add(this.lblTotalAmount);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.lblLedgerTotal);
            this.panel3.Controls.Add(this.lblLedgerName);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 471);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(865, 73);
            this.panel3.TabIndex = 1;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblTotalAmount.Location = new System.Drawing.Point(255, 37);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(46, 25);
            this.lblTotalAmount.TabIndex = 8;
            this.lblTotalAmount.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(249, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Total";
            // 
            // lblLedgerTotal
            // 
            this.lblLedgerTotal.AutoSize = true;
            this.lblLedgerTotal.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblLedgerTotal.Location = new System.Drawing.Point(12, 37);
            this.lblLedgerTotal.Name = "lblLedgerTotal";
            this.lblLedgerTotal.Size = new System.Drawing.Size(46, 25);
            this.lblLedgerTotal.TabIndex = 6;
            this.lblLedgerTotal.Text = "0.00";
            // 
            // lblLedgerName
            // 
            this.lblLedgerName.AutoSize = true;
            this.lblLedgerName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblLedgerName.Location = new System.Drawing.Point(12, 4);
            this.lblLedgerName.Name = "lblLedgerName";
            this.lblLedgerName.Size = new System.Drawing.Size(180, 25);
            this.lblLedgerName.TabIndex = 5;
            this.lblLedgerName.Text = "Primary Cost Center";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(746, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.txtAmount);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbCostCenter);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbPrimaryCostCenter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(865, 109);
            this.panel2.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(735, 31);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtAmount.Location = new System.Drawing.Point(569, 40);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(160, 31);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            this.txtAmount.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(564, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 31;
            this.label4.Text = "Amount";
            // 
            // cmbCostCenter
            // 
            this.cmbCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCostCenter.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCostCenter.FormattingEnabled = true;
            this.cmbCostCenter.Location = new System.Drawing.Point(286, 38);
            this.cmbCostCenter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCostCenter.Name = "cmbCostCenter";
            this.cmbCostCenter.Size = new System.Drawing.Size(276, 33);
            this.cmbCostCenter.TabIndex = 1;
            this.cmbCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(281, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cost Center";
            // 
            // cmbPrimaryCostCenter
            // 
            this.cmbPrimaryCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPrimaryCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrimaryCostCenter.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbPrimaryCostCenter.FormattingEnabled = true;
            this.cmbPrimaryCostCenter.Location = new System.Drawing.Point(17, 38);
            this.cmbPrimaryCostCenter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPrimaryCostCenter.Name = "cmbPrimaryCostCenter";
            this.cmbPrimaryCostCenter.Size = new System.Drawing.Size(261, 33);
            this.cmbPrimaryCostCenter.TabIndex = 0;
            this.cmbPrimaryCostCenter.SelectedIndexChanged += new System.EventHandler(this.cmbPrimaryCostCenter_SelectedIndexChanged);
            this.cmbPrimaryCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Primary Cost Center";
            // 
            // CostCenterEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 544);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "CostCenterEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cost Center Entry";
            this.Load += new System.EventHandler(this.CostCenterEntryForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CostCenterEntryForm_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgItems;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPrimaryCostCenter;
        private System.Windows.Forms.ComboBox cmbCostCenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLedgerTotal;
        private System.Windows.Forms.Label lblLedgerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEntryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAmount;
    }
}