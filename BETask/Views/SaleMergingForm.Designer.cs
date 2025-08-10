namespace BETask.Views
{
    partial class SaleMergingForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgSalemerging = new System.Windows.Forms.DataGridView();
            this.clmSaleid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSaleNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeafNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmView = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDestinationDivision = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSourceDivision = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDestinationCustomer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSourceCustomer = new System.Windows.Forms.ComboBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSalemerging)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 726);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgSalemerging);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 208);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1095, 425);
            this.panel3.TabIndex = 3;
            // 
            // dgSalemerging
            // 
            this.dgSalemerging.AllowUserToAddRows = false;
            this.dgSalemerging.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgSalemerging.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSalemerging.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSalemerging.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgSalemerging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSalemerging.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSaleid,
            this.clmSaleNumber,
            this.clmSaleDate,
            this.clmNetAmount,
            this.clmLeafNumber,
            this.clmView,
            this.clmDelete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSalemerging.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgSalemerging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSalemerging.Location = new System.Drawing.Point(0, 0);
            this.dgSalemerging.Name = "dgSalemerging";
            this.dgSalemerging.ReadOnly = true;
            this.dgSalemerging.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgSalemerging.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgSalemerging.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgSalemerging.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgSalemerging.RowTemplate.Height = 28;
            this.dgSalemerging.Size = new System.Drawing.Size(1095, 425);
            this.dgSalemerging.TabIndex = 4;
            this.dgSalemerging.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSalemerging_CellClick);
            // 
            // clmSaleid
            // 
            this.clmSaleid.HeaderText = "Sale Id";
            this.clmSaleid.MinimumWidth = 6;
            this.clmSaleid.Name = "clmSaleid";
            this.clmSaleid.ReadOnly = true;
            this.clmSaleid.Width = 125;
            // 
            // clmSaleNumber
            // 
            this.clmSaleNumber.HeaderText = "SaleNumber";
            this.clmSaleNumber.MinimumWidth = 6;
            this.clmSaleNumber.Name = "clmSaleNumber";
            this.clmSaleNumber.ReadOnly = true;
            this.clmSaleNumber.Width = 125;
            // 
            // clmSaleDate
            // 
            this.clmSaleDate.HeaderText = "Sale Date";
            this.clmSaleDate.MinimumWidth = 6;
            this.clmSaleDate.Name = "clmSaleDate";
            this.clmSaleDate.ReadOnly = true;
            this.clmSaleDate.Width = 120;
            // 
            // clmNetAmount
            // 
            this.clmNetAmount.HeaderText = "Net Amount";
            this.clmNetAmount.MinimumWidth = 6;
            this.clmNetAmount.Name = "clmNetAmount";
            this.clmNetAmount.ReadOnly = true;
            this.clmNetAmount.Width = 125;
            // 
            // clmLeafNumber
            // 
            this.clmLeafNumber.HeaderText = "Leaf Number";
            this.clmLeafNumber.MinimumWidth = 6;
            this.clmLeafNumber.Name = "clmLeafNumber";
            this.clmLeafNumber.ReadOnly = true;
            this.clmLeafNumber.Width = 80;
            // 
            // clmView
            // 
            this.clmView.HeaderText = "View";
            this.clmView.MinimumWidth = 6;
            this.clmView.Name = "clmView";
            this.clmView.ReadOnly = true;
            this.clmView.Width = 80;
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "Remove";
            this.clmDelete.MinimumWidth = 6;
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.ReadOnly = true;
            this.clmDelete.Width = 80;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnPrint);
            this.panel4.Controls.Add(this.btnMerge);
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Controls.Add(this.btnClose);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 633);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1095, 93);
            this.panel4.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(630, 25);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            this.btnPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMerge.BackColor = System.Drawing.Color.Teal;
            this.btnMerge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMerge.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMerge.ForeColor = System.Drawing.Color.White;
            this.btnMerge.Location = new System.Drawing.Point(976, 25);
            this.btnMerge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(107, 47);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "&Merge";
            this.btnMerge.UseVisualStyleBackColor = false;
            this.btnMerge.Click += new System.EventHandler(this.ButtonEvents);
            this.btnMerge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(863, 25);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(751, 25);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            this.btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cmbDestinationDivision);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbSourceDivision);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmbDestinationCustomer);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbSourceCustomer);
            this.panel2.Controls.Add(this.dtpToDate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpFromDate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1095, 208);
            this.panel2.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(485, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 25);
            this.label6.TabIndex = 56;
            this.label6.Text = "Division";
            // 
            // cmbDestinationDivision
            // 
            this.cmbDestinationDivision.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDestinationDivision.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDestinationDivision.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbDestinationDivision.FormattingEnabled = true;
            this.cmbDestinationDivision.Location = new System.Drawing.Point(609, 136);
            this.cmbDestinationDivision.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDestinationDivision.Name = "cmbDestinationDivision";
            this.cmbDestinationDivision.Size = new System.Drawing.Size(297, 33);
            this.cmbDestinationDivision.TabIndex = 55;
            this.cmbDestinationDivision.SelectedIndexChanged += new System.EventHandler(this.cmbDestinationDivision_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(15, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 25);
            this.label5.TabIndex = 54;
            this.label5.Text = "Division";
            // 
            // cmbSourceDivision
            // 
            this.cmbSourceDivision.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSourceDivision.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSourceDivision.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSourceDivision.FormattingEnabled = true;
            this.cmbSourceDivision.Location = new System.Drawing.Point(178, 131);
            this.cmbSourceDivision.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSourceDivision.Name = "cmbSourceDivision";
            this.cmbSourceDivision.Size = new System.Drawing.Size(287, 33);
            this.cmbSourceDivision.TabIndex = 4;
            this.cmbSourceDivision.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(485, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 25);
            this.label3.TabIndex = 52;
            this.label3.Text = "To Customer";
            // 
            // cmbDestinationCustomer
            // 
            this.cmbDestinationCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDestinationCustomer.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbDestinationCustomer.FormattingEnabled = true;
            this.cmbDestinationCustomer.Location = new System.Drawing.Point(609, 82);
            this.cmbDestinationCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDestinationCustomer.Name = "cmbDestinationCustomer";
            this.cmbDestinationCustomer.Size = new System.Drawing.Size(297, 33);
            this.cmbDestinationCustomer.TabIndex = 3;
            this.cmbDestinationCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbDestinationDivision_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(15, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 50;
            this.label1.Text = "From Customer";
            // 
            // cmbSourceCustomer
            // 
            this.cmbSourceCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSourceCustomer.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbSourceCustomer.FormattingEnabled = true;
            this.cmbSourceCustomer.Location = new System.Drawing.Point(178, 82);
            this.cmbSourceCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSourceCustomer.Name = "cmbSourceCustomer";
            this.cmbSourceCustomer.Size = new System.Drawing.Size(287, 33);
            this.cmbSourceCustomer.TabIndex = 2;
            this.cmbSourceCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbSourceCustomer_SelectedIndexChanged);
            this.cmbSourceCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(609, 32);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(183, 31);
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(485, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 48;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(178, 32);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(183, 31);
            this.dtpFromDate.TabIndex = 0;
            this.dtpFromDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(15, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 25);
            this.label4.TabIndex = 47;
            this.label4.Text = "From Date";
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.BackColor = System.Drawing.Color.Teal;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(935, 124);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(98, 47);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.ButtonEvents);
            this.btnLoad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // SaleMergingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 726);
            this.Controls.Add(this.panel1);
            this.Name = "SaleMergingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sale Merging";
            this.Load += new System.EventHandler(this.SaleMergingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSalemerging)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDestinationCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSourceCustomer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSourceDivision;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDestinationDivision;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgSalemerging;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSaleid;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSaleNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNetAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafNumber;
        private System.Windows.Forms.DataGridViewLinkColumn clmView;
        private System.Windows.Forms.DataGridViewLinkColumn clmDelete;
    }
}