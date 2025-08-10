namespace BETask.Views
{
    partial class ItemDamageForm
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
            this.cmbProductDamaged = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.gridRawmeterial = new System.Windows.Forms.DataGridView();
            this.clmEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPacking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmDamageId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButton = new System.Windows.Forms.Panel();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalRawmaterial = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRawmeterial)).BeginInit();
            this.panelButton.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSaveContent);
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1090, 729);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSaveContent.Controls.Add(this.cmbProductDamaged);
            this.pnlSaveContent.Controls.Add(this.label8);
            this.pnlSaveContent.Controls.Add(this.label2);
            this.pnlSaveContent.Controls.Add(this.cmbEmployee);
            this.pnlSaveContent.Controls.Add(this.txtRemarks);
            this.pnlSaveContent.Controls.Add(this.dtpDate);
            this.pnlSaveContent.Controls.Add(this.label6);
            this.pnlSaveContent.Controls.Add(this.panelGrid);
            this.pnlSaveContent.Controls.Add(this.btnSave);
            this.pnlSaveContent.Controls.Add(this.txtQty);
            this.pnlSaveContent.Controls.Add(this.cmbProductName);
            this.pnlSaveContent.Controls.Add(this.label7);
            this.pnlSaveContent.Controls.Add(this.label3);
            this.pnlSaveContent.Controls.Add(this.label1);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1090, 651);
            this.pnlSaveContent.TabIndex = 1;
            // 
            // cmbProductDamaged
            // 
            this.cmbProductDamaged.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbProductDamaged.FormattingEnabled = true;
            this.cmbProductDamaged.Location = new System.Drawing.Point(554, 103);
            this.cmbProductDamaged.Name = "cmbProductDamaged";
            this.cmbProductDamaged.Size = new System.Drawing.Size(332, 33);
            this.cmbProductDamaged.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(549, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 25);
            this.label8.TabIndex = 86;
            this.label8.Text = "Damaged Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(238, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 25);
            this.label2.TabIndex = 85;
            this.label2.Text = "Employee";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(243, 36);
            this.cmbEmployee.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(305, 33);
            this.cmbEmployee.TabIndex = 1;
            this.cmbEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(243, 103);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(305, 31);
            this.txtRemarks.TabIndex = 4;
            this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDate.Location = new System.Drawing.Point(35, 38);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 31);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(30, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Date";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.gridRawmeterial);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGrid.Location = new System.Drawing.Point(0, 153);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1090, 498);
            this.panelGrid.TabIndex = 8;
            // 
            // gridRawmeterial
            // 
            this.gridRawmeterial.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.gridRawmeterial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRawmeterial.BackgroundColor = System.Drawing.Color.White;
            this.gridRawmeterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRawmeterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmEmployee,
            this.clmItemId,
            this.clmItemName,
            this.clmPacking,
            this.clmQty,
            this.clmRate,
            this.clmValue,
            this.clmDelete,
            this.clmDamageId});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRawmeterial.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridRawmeterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRawmeterial.Location = new System.Drawing.Point(0, 0);
            this.gridRawmeterial.Name = "gridRawmeterial";
            this.gridRawmeterial.RowHeadersVisible = false;
            this.gridRawmeterial.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridRawmeterial.RowTemplate.Height = 30;
            this.gridRawmeterial.Size = new System.Drawing.Size(1090, 498);
            this.gridRawmeterial.TabIndex = 0;
            this.gridRawmeterial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRawmeterial_CellContentClick);
            this.gridRawmeterial.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRawmeterial_CellValueChanged);
            this.gridRawmeterial.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridRawmeterial_CurrentCellDirtyStateChanged);
            // 
            // clmEmployee
            // 
            this.clmEmployee.HeaderText = "Employee";
            this.clmEmployee.Name = "clmEmployee";
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "Item ID";
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.Visible = false;
            // 
            // clmItemName
            // 
            this.clmItemName.HeaderText = "Item Name";
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmItemName.Width = 250;
            // 
            // clmPacking
            // 
            this.clmPacking.HeaderText = "Packing";
            this.clmPacking.Name = "clmPacking";
            this.clmPacking.ReadOnly = true;
            this.clmPacking.Width = 150;
            // 
            // clmQty
            // 
            this.clmQty.HeaderText = "Qty";
            this.clmQty.Name = "clmQty";
            // 
            // clmRate
            // 
            this.clmRate.HeaderText = "Rate";
            this.clmRate.Name = "clmRate";
            this.clmRate.ReadOnly = true;
            // 
            // clmValue
            // 
            this.clmValue.HeaderText = "Value";
            this.clmValue.Name = "clmValue";
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "Delete";
            this.clmDelete.Name = "clmDelete";
            // 
            // clmDamageId
            // 
            this.clmDamageId.HeaderText = "DamageId";
            this.clmDamageId.Name = "clmDamageId";
            this.clmDamageId.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(897, 96);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 47);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtQty.Location = new System.Drawing.Point(35, 103);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(202, 31);
            this.txtQty.TabIndex = 3;
            this.txtQty.Text = "1.00";
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // cmbProductName
            // 
            this.cmbProductName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(555, 36);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(332, 33);
            this.cmbProductName.TabIndex = 2;
            this.cmbProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(249, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 25);
            this.label7.TabIndex = 3;
            this.label7.Text = "Remarks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(32, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Qty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(550, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item";
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.panelSummary);
            this.panelButton.Controls.Add(this.btnCancel);
            this.panelButton.Controls.Add(this.btnClose);
            this.panelButton.Controls.Add(this.btnNew);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 651);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(1090, 78);
            this.panelButton.TabIndex = 0;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.Silver;
            this.panelSummary.Controls.Add(this.label5);
            this.panelSummary.Controls.Add(this.txtTotalValue);
            this.panelSummary.Controls.Add(this.label4);
            this.panelSummary.Controls.Add(this.txtTotalRawmaterial);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSummary.Location = new System.Drawing.Point(0, 0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(445, 78);
            this.panelSummary.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(220, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Total Value";
            // 
            // txtTotalValue
            // 
            this.txtTotalValue.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalValue.Location = new System.Drawing.Point(225, 38);
            this.txtTotalValue.Name = "txtTotalValue";
            this.txtTotalValue.ReadOnly = true;
            this.txtTotalValue.Size = new System.Drawing.Size(152, 31);
            this.txtTotalValue.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(14, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Total Item";
            // 
            // txtTotalRawmaterial
            // 
            this.txtTotalRawmaterial.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalRawmaterial.Location = new System.Drawing.Point(19, 38);
            this.txtTotalRawmaterial.Name = "txtTotalRawmaterial";
            this.txtTotalRawmaterial.ReadOnly = true;
            this.txtTotalRawmaterial.Size = new System.Drawing.Size(152, 31);
            this.txtTotalRawmaterial.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(971, 20);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 6;
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
            this.btnClose.Location = new System.Drawing.Point(859, 20);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.Green;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(746, 20);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 47);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // ItemDamageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 729);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "ItemDamageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product / Item Damage";
            this.Load += new System.EventHandler(this.ProductMapForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.pnlSaveContent.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRawmeterial)).EndInit();
            this.panelButton.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gridRawmeterial;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalRawmaterial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalValue;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.ComboBox cmbProductDamaged;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValue;
        private System.Windows.Forms.DataGridViewLinkColumn clmDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDamageId;
    }
}