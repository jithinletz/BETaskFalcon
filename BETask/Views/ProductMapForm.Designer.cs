namespace BETask.Views
{
    partial class ProductMapForm
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
            this.panelGrid = new System.Windows.Forms.Panel();
            this.gridRawmeterial = new System.Windows.Forms.DataGridView();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmPacking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPacking = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButton = new System.Windows.Forms.Panel();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalRawmaterial = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
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
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1456, 945);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSaveContent.Controls.Add(this.panelGrid);
            this.pnlSaveContent.Controls.Add(this.txtQty);
            this.pnlSaveContent.Controls.Add(this.cmbProductName);
            this.pnlSaveContent.Controls.Add(this.label2);
            this.pnlSaveContent.Controls.Add(this.label3);
            this.pnlSaveContent.Controls.Add(this.txtPacking);
            this.pnlSaveContent.Controls.Add(this.label1);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1456, 823);
            this.pnlSaveContent.TabIndex = 1;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.gridRawmeterial);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGrid.Location = new System.Drawing.Point(0, 262);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1456, 561);
            this.panelGrid.TabIndex = 8;
            // 
            // gridRawmeterial
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.gridRawmeterial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRawmeterial.BackgroundColor = System.Drawing.Color.White;
            this.gridRawmeterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRawmeterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItemId,
            this.clmItemName,
            this.clmPacking,
            this.clmQty,
            this.clmRate,
            this.clmValue,
            this.clmDelete});
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
            this.gridRawmeterial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridRawmeterial.Name = "gridRawmeterial";
            this.gridRawmeterial.RowHeadersVisible = false;
            this.gridRawmeterial.RowHeadersWidth = 82;
            this.gridRawmeterial.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridRawmeterial.RowTemplate.Height = 30;
            this.gridRawmeterial.Size = new System.Drawing.Size(1456, 561);
            this.gridRawmeterial.TabIndex = 7;
            this.gridRawmeterial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRawmeterial_CellContentClick);
            this.gridRawmeterial.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRawmeterial_CellValueChanged);
            this.gridRawmeterial.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridRawmeterial_CurrentCellDirtyStateChanged);
            this.gridRawmeterial.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridRawmeterial_EditingControlShowing);
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "Item ID";
            this.clmItemId.MinimumWidth = 10;
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.Visible = false;
            this.clmItemId.Width = 200;
            // 
            // clmItemName
            // 
            this.clmItemName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.clmItemName.HeaderText = "Item Name";
            this.clmItemName.MaxDropDownItems = 100;
            this.clmItemName.MinimumWidth = 10;
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmItemName.Width = 250;
            // 
            // clmPacking
            // 
            this.clmPacking.HeaderText = "Packing";
            this.clmPacking.MinimumWidth = 10;
            this.clmPacking.Name = "clmPacking";
            this.clmPacking.ReadOnly = true;
            this.clmPacking.Width = 200;
            // 
            // clmQty
            // 
            this.clmQty.HeaderText = "Qty Used";
            this.clmQty.MinimumWidth = 10;
            this.clmQty.Name = "clmQty";
            this.clmQty.Width = 200;
            // 
            // clmRate
            // 
            this.clmRate.HeaderText = "Rate";
            this.clmRate.MinimumWidth = 10;
            this.clmRate.Name = "clmRate";
            this.clmRate.Width = 200;
            // 
            // clmValue
            // 
            this.clmValue.HeaderText = "Value";
            this.clmValue.MinimumWidth = 10;
            this.clmValue.Name = "clmValue";
            this.clmValue.ReadOnly = true;
            this.clmValue.Width = 200;
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "Delete";
            this.clmDelete.MinimumWidth = 10;
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.Width = 200;
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtQty.Location = new System.Drawing.Point(914, 153);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(202, 46);
            this.txtQty.TabIndex = 6;
            this.txtQty.Text = "1.00";
            // 
            // cmbProductName
            // 
            this.cmbProductName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(278, 67);
            this.cmbProductName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(838, 48);
            this.cmbProductName.TabIndex = 2;
            this.cmbProductName.SelectedIndexChanged += new System.EventHandler(this.cmbProductName_SelectedIndexChanged);
            this.cmbProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(60, 161);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Packing";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(843, 158);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 40);
            this.label3.TabIndex = 5;
            this.label3.Text = "Qty";
            // 
            // txtPacking
            // 
            this.txtPacking.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPacking.Location = new System.Drawing.Point(278, 153);
            this.txtPacking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPacking.Name = "txtPacking";
            this.txtPacking.ReadOnly = true;
            this.txtPacking.Size = new System.Drawing.Size(512, 46);
            this.txtPacking.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(60, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Product Name";
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.panelSummary);
            this.panelButton.Controls.Add(this.btnSave);
            this.panelButton.Controls.Add(this.btnCancel);
            this.panelButton.Controls.Add(this.btnClose);
            this.panelButton.Controls.Add(this.btnNew);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 823);
            this.panelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(1456, 122);
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
            this.panelSummary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(668, 122);
            this.panelSummary.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(330, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 40);
            this.label5.TabIndex = 7;
            this.label5.Text = "Total Value";
            // 
            // txtTotalValue
            // 
            this.txtTotalValue.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalValue.Location = new System.Drawing.Point(338, 59);
            this.txtTotalValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalValue.Name = "txtTotalValue";
            this.txtTotalValue.ReadOnly = true;
            this.txtTotalValue.Size = new System.Drawing.Size(226, 46);
            this.txtTotalValue.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(21, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(278, 40);
            this.label4.TabIndex = 5;
            this.label4.Text = "Total Rowmaterials";
            // 
            // txtTotalRawmaterial
            // 
            this.txtTotalRawmaterial.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalRawmaterial.Location = new System.Drawing.Point(28, 59);
            this.txtTotalRawmaterial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalRawmaterial.Name = "txtTotalRawmaterial";
            this.txtTotalRawmaterial.ReadOnly = true;
            this.txtTotalRawmaterial.Size = new System.Drawing.Size(226, 46);
            this.txtTotalRawmaterial.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1278, 31);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 73);
            this.btnSave.TabIndex = 5;
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
            this.btnCancel.Location = new System.Drawing.Point(1110, 31);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 73);
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
            this.btnClose.Location = new System.Drawing.Point(942, 31);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 73);
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
            this.btnNew.Location = new System.Drawing.Point(772, 31);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(160, 73);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // ProductMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 945);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "ProductMapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Mapping";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPacking;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValue;
        private System.Windows.Forms.DataGridViewLinkColumn clmDelete;
    }
}