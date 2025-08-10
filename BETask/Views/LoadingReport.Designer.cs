namespace BETask.Views
{
    partial class LoadingReport
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
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgLoading = new System.Windows.Forms.DataGridView();
            this.clmDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHelper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOldStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmpty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDamage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNewLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTotalLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmExtra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNewStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOffload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmActStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(610, 80);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(183, 31);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(494, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 43;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(610, 27);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(183, 31);
            this.dtpFromDate.TabIndex = 2;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(494, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 25);
            this.label4.TabIndex = 41;
            this.label4.Text = "From Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(12, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 25);
            this.label9.TabIndex = 39;
            this.label9.Text = "Employee";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Items.AddRange(new object[] {
            "Delivery",
            "Return"});
            this.cmbEmployee.Location = new System.Drawing.Point(167, 83);
            this.cmbEmployee.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(287, 33);
            this.cmbEmployee.TabIndex = 1;
            this.cmbEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.BackColor = System.Drawing.Color.Teal;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Location = new System.Drawing.Point(862, 64);
            this.btnShow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(107, 47);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "&Show";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.ButtonEvents);
            this.btnShow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // cmbItem
            // 
            this.cmbItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbItem.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(167, 24);
            this.cmbItem.Margin = new System.Windows.Forms.Padding(4);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(287, 33);
            this.cmbItem.TabIndex = 0;
            this.cmbItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(1023, 14);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            this.btnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.dtpToDate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpFromDate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cmbEmployee);
            this.panel2.Controls.Add(this.btnShow);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbItem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1273, 142);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "Item";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1144, 14);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            this.btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 579);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1273, 72);
            this.panel3.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgLoading);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1273, 651);
            this.panel1.TabIndex = 1;
            // 
            // dgLoading
            // 
            this.dgLoading.AllowUserToAddRows = false;
            this.dgLoading.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgLoading.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgLoading.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLoading.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgLoading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLoading.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDate,
            this.clmEmployee,
            this.clmHelper,
            this.clmItem,
            this.clmOldStock,
            this.clmEmpty,
            this.clmDamage,
            this.clmBalance,
            this.clmNewLoad,
            this.clmTotalLoad,
            this.clmShort,
            this.clmExtra,
            this.clmNewStock,
            this.clmOffload,
            this.clmActStock,
            this.clmSold});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLoading.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLoading.Location = new System.Drawing.Point(0, 142);
            this.dgLoading.Name = "dgLoading";
            this.dgLoading.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLoading.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgLoading.RowHeadersVisible = false;
            this.dgLoading.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgLoading.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgLoading.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgLoading.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgLoading.RowTemplate.Height = 28;
            this.dgLoading.Size = new System.Drawing.Size(1273, 437);
            this.dgLoading.TabIndex = 5;
            // 
            // clmDate
            // 
            this.clmDate.HeaderText = "Date";
            this.clmDate.MinimumWidth = 6;
            this.clmDate.Name = "clmDate";
            this.clmDate.ReadOnly = true;
            this.clmDate.Width = 80;
            // 
            // clmEmployee
            // 
            this.clmEmployee.HeaderText = "Employee";
            this.clmEmployee.MinimumWidth = 6;
            this.clmEmployee.Name = "clmEmployee";
            this.clmEmployee.ReadOnly = true;
            this.clmEmployee.Width = 125;
            // 
            // clmHelper
            // 
            this.clmHelper.HeaderText = "Helper";
            this.clmHelper.MinimumWidth = 6;
            this.clmHelper.Name = "clmHelper";
            this.clmHelper.ReadOnly = true;
            this.clmHelper.Width = 90;
            // 
            // clmItem
            // 
            this.clmItem.HeaderText = "Item";
            this.clmItem.MinimumWidth = 6;
            this.clmItem.Name = "clmItem";
            this.clmItem.ReadOnly = true;
            this.clmItem.Width = 125;
            // 
            // clmOldStock
            // 
            this.clmOldStock.HeaderText = "Old Stock";
            this.clmOldStock.MinimumWidth = 6;
            this.clmOldStock.Name = "clmOldStock";
            this.clmOldStock.ReadOnly = true;
            this.clmOldStock.Width = 50;
            // 
            // clmEmpty
            // 
            this.clmEmpty.HeaderText = "Empty";
            this.clmEmpty.MinimumWidth = 6;
            this.clmEmpty.Name = "clmEmpty";
            this.clmEmpty.ReadOnly = true;
            this.clmEmpty.Width = 50;
            // 
            // clmDamage
            // 
            this.clmDamage.HeaderText = "Damage";
            this.clmDamage.MinimumWidth = 6;
            this.clmDamage.Name = "clmDamage";
            this.clmDamage.ReadOnly = true;
            this.clmDamage.Width = 50;
            // 
            // clmBalance
            // 
            this.clmBalance.HeaderText = "Balance";
            this.clmBalance.MinimumWidth = 6;
            this.clmBalance.Name = "clmBalance";
            this.clmBalance.ReadOnly = true;
            this.clmBalance.Width = 50;
            // 
            // clmNewLoad
            // 
            this.clmNewLoad.HeaderText = "New Load";
            this.clmNewLoad.MinimumWidth = 6;
            this.clmNewLoad.Name = "clmNewLoad";
            this.clmNewLoad.ReadOnly = true;
            this.clmNewLoad.Width = 50;
            // 
            // clmTotalLoad
            // 
            this.clmTotalLoad.HeaderText = "Total Load";
            this.clmTotalLoad.MinimumWidth = 6;
            this.clmTotalLoad.Name = "clmTotalLoad";
            this.clmTotalLoad.ReadOnly = true;
            this.clmTotalLoad.Width = 50;
            // 
            // clmShort
            // 
            this.clmShort.HeaderText = "Short";
            this.clmShort.MinimumWidth = 6;
            this.clmShort.Name = "clmShort";
            this.clmShort.ReadOnly = true;
            this.clmShort.Width = 50;
            // 
            // clmExtra
            // 
            this.clmExtra.HeaderText = "Extra";
            this.clmExtra.MinimumWidth = 6;
            this.clmExtra.Name = "clmExtra";
            this.clmExtra.ReadOnly = true;
            this.clmExtra.Width = 50;
            // 
            // clmNewStock
            // 
            this.clmNewStock.HeaderText = "New Stock";
            this.clmNewStock.MinimumWidth = 6;
            this.clmNewStock.Name = "clmNewStock";
            this.clmNewStock.ReadOnly = true;
            this.clmNewStock.Width = 50;
            // 
            // clmOffload
            // 
            this.clmOffload.HeaderText = "Offload";
            this.clmOffload.MinimumWidth = 6;
            this.clmOffload.Name = "clmOffload";
            this.clmOffload.ReadOnly = true;
            this.clmOffload.Width = 50;
            // 
            // clmActStock
            // 
            this.clmActStock.HeaderText = "Act Stock";
            this.clmActStock.MinimumWidth = 6;
            this.clmActStock.Name = "clmActStock";
            this.clmActStock.ReadOnly = true;
            this.clmActStock.Width = 50;
            // 
            // clmSold
            // 
            this.clmSold.HeaderText = "Sold";
            this.clmSold.MinimumWidth = 6;
            this.clmSold.Name = "clmSold";
            this.clmSold.ReadOnly = true;
            this.clmSold.Width = 50;
            // 
            // LoadingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 651);
            this.Controls.Add(this.panel1);
            this.Name = "LoadingReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading Report";
            this.Load += new System.EventHandler(this.Loading_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgLoading;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHelper;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOldStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDamage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNewLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTotalLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmExtra;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNewStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOffload;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmActStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSold;
    }
}