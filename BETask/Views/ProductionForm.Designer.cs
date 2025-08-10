namespace BETask.Views
{
    partial class ProductionForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSaveContent = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.gridProducts = new System.Windows.Forms.DataGridView();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmPacking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmViewRaw = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmProductionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDeleteProduction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalProducts = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlLeftMain = new System.Windows.Forms.Panel();
            this.gridDates = new System.Windows.Forms.DataGridView();
            this.clmProductionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDateFilter = new System.Windows.Forms.Panel();
            this.dtpDateFilter = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.pnlLeftMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDates)).BeginInit();
            this.pnlDateFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlSaveContent);
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.pnlLeftMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2320, 1338);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.Controls.Add(this.pnlGrid);
            this.pnlSaveContent.Controls.Add(this.pnlHeader);
            this.pnlSaveContent.Controls.Add(this.pnlFooter);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(270, 0);
            this.pnlSaveContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(2050, 1221);
            this.pnlSaveContent.TabIndex = 2;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.gridProducts);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 84);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(2050, 1037);
            this.pnlGrid.TabIndex = 3;
            // 
            // gridProducts
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProducts.BackgroundColor = System.Drawing.Color.White;
            this.gridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItemId,
            this.clmItemName,
            this.clmPacking,
            this.clmQty,
            this.clmCost,
            this.clmUnitCost,
            this.clmViewRaw,
            this.clmDelete,
            this.clmProductionId,
            this.clmDeleteProduction});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProducts.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProducts.Location = new System.Drawing.Point(0, 0);
            this.gridProducts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.RowHeadersVisible = false;
            this.gridProducts.RowHeadersWidth = 51;
            this.gridProducts.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridProducts.RowTemplate.Height = 30;
            this.gridProducts.Size = new System.Drawing.Size(2050, 1037);
            this.gridProducts.TabIndex = 0;
            this.gridProducts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProducts_CellContentClick);
            this.gridProducts.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProducts_CellValueChanged);
            this.gridProducts.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridProducts_CurrentCellDirtyStateChanged);
            this.gridProducts.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridProducts_EditingControlShowing);
            this.gridProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridProducts_KeyDown);
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "Item Id";
            this.clmItemId.MinimumWidth = 6;
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.Visible = false;
            this.clmItemId.Width = 125;
            // 
            // clmItemName
            // 
            this.clmItemName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.clmItemName.HeaderText = "Item Name";
            this.clmItemName.MaxDropDownItems = 100;
            this.clmItemName.MinimumWidth = 6;
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.Width = 200;
            // 
            // clmPacking
            // 
            this.clmPacking.HeaderText = "Packing";
            this.clmPacking.MinimumWidth = 6;
            this.clmPacking.Name = "clmPacking";
            this.clmPacking.ReadOnly = true;
            this.clmPacking.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmPacking.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmPacking.Width = 125;
            // 
            // clmQty
            // 
            this.clmQty.HeaderText = "Qty Produced";
            this.clmQty.MinimumWidth = 6;
            this.clmQty.Name = "clmQty";
            this.clmQty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmQty.Width = 90;
            // 
            // clmCost
            // 
            this.clmCost.HeaderText = "Cost";
            this.clmCost.MinimumWidth = 6;
            this.clmCost.Name = "clmCost";
            this.clmCost.Width = 125;
            // 
            // clmUnitCost
            // 
            this.clmUnitCost.HeaderText = "Unit Cost";
            this.clmUnitCost.MinimumWidth = 6;
            this.clmUnitCost.Name = "clmUnitCost";
            this.clmUnitCost.Visible = false;
            this.clmUnitCost.Width = 90;
            // 
            // clmViewRaw
            // 
            this.clmViewRaw.HeaderText = "Rawmaterials";
            this.clmViewRaw.MinimumWidth = 6;
            this.clmViewRaw.Name = "clmViewRaw";
            this.clmViewRaw.Width = 80;
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "Remove";
            this.clmDelete.MinimumWidth = 6;
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.Width = 80;
            // 
            // clmProductionId
            // 
            this.clmProductionId.HeaderText = "ProductionId";
            this.clmProductionId.MinimumWidth = 6;
            this.clmProductionId.Name = "clmProductionId";
            this.clmProductionId.Visible = false;
            this.clmProductionId.Width = 125;
            // 
            // clmDeleteProduction
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.clmDeleteProduction.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDeleteProduction.HeaderText = "Delete";
            this.clmDeleteProduction.MinimumWidth = 6;
            this.clmDeleteProduction.Name = "clmDeleteProduction";
            this.clmDeleteProduction.Width = 125;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.dtpDate);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(2050, 84);
            this.pnlHeader.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(159, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Production Date";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(391, 14);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(298, 46);
            this.dtpDate.TabIndex = 0;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.txtRemarks);
            this.pnlFooter.Controls.Add(this.label1);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 1121);
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(2050, 100);
            this.pnlFooter.TabIndex = 0;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(0, 54);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(2050, 46);
            this.txtRemarks.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remarks";
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlButton.Controls.Add(this.panelSummary);
            this.pnlButton.Controls.Add(this.btnSave);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.btnNew);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(270, 1221);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(2050, 117);
            this.pnlButton.TabIndex = 1;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.Silver;
            this.panelSummary.Controls.Add(this.label5);
            this.panelSummary.Controls.Add(this.txtTotalValue);
            this.panelSummary.Controls.Add(this.label4);
            this.panelSummary.Controls.Add(this.txtTotalProducts);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSummary.Location = new System.Drawing.Point(0, 0);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(668, 117);
            this.panelSummary.TabIndex = 12;
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
            this.label4.Size = new System.Drawing.Size(139, 40);
            this.label4.TabIndex = 5;
            this.label4.Text = "Products";
            // 
            // txtTotalProducts
            // 
            this.txtTotalProducts.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalProducts.Location = new System.Drawing.Point(28, 59);
            this.txtTotalProducts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTotalProducts.Name = "txtTotalProducts";
            this.txtTotalProducts.ReadOnly = true;
            this.txtTotalProducts.Size = new System.Drawing.Size(298, 46);
            this.txtTotalProducts.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1872, 27);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 73);
            this.btnSave.TabIndex = 9;
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
            this.btnCancel.Location = new System.Drawing.Point(1704, 27);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 73);
            this.btnCancel.TabIndex = 10;
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
            this.btnClose.Location = new System.Drawing.Point(1536, 27);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 73);
            this.btnClose.TabIndex = 11;
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
            this.btnNew.Location = new System.Drawing.Point(1366, 27);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(160, 73);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // pnlLeftMain
            // 
            this.pnlLeftMain.Controls.Add(this.gridDates);
            this.pnlLeftMain.Controls.Add(this.pnlDateFilter);
            this.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftMain.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlLeftMain.Name = "pnlLeftMain";
            this.pnlLeftMain.Size = new System.Drawing.Size(270, 1338);
            this.pnlLeftMain.TabIndex = 0;
            // 
            // gridDates
            // 
            this.gridDates.AllowUserToAddRows = false;
            this.gridDates.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            this.gridDates.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridDates.BackgroundColor = System.Drawing.Color.White;
            this.gridDates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmProductionDate});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridDates.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDates.Location = new System.Drawing.Point(0, 67);
            this.gridDates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridDates.Name = "gridDates";
            this.gridDates.ReadOnly = true;
            this.gridDates.RowHeadersVisible = false;
            this.gridDates.RowHeadersWidth = 51;
            this.gridDates.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridDates.RowTemplate.Height = 24;
            this.gridDates.Size = new System.Drawing.Size(270, 1271);
            this.gridDates.TabIndex = 1;
            this.gridDates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDates_CellClick);
            this.gridDates.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDates_CellClick);
            // 
            // clmProductionDate
            // 
            this.clmProductionDate.HeaderText = "Prod Date";
            this.clmProductionDate.MinimumWidth = 6;
            this.clmProductionDate.Name = "clmProductionDate";
            this.clmProductionDate.ReadOnly = true;
            this.clmProductionDate.Width = 150;
            // 
            // pnlDateFilter
            // 
            this.pnlDateFilter.Controls.Add(this.dtpDateFilter);
            this.pnlDateFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDateFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlDateFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDateFilter.Name = "pnlDateFilter";
            this.pnlDateFilter.Size = new System.Drawing.Size(270, 67);
            this.pnlDateFilter.TabIndex = 0;
            // 
            // dtpDateFilter
            // 
            this.dtpDateFilter.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateFilter.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateFilter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFilter.Location = new System.Drawing.Point(0, 0);
            this.dtpDateFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDateFilter.Name = "dtpDateFilter";
            this.dtpDateFilter.Size = new System.Drawing.Size(270, 46);
            this.dtpDateFilter.TabIndex = 1;
            this.dtpDateFilter.ValueChanged += new System.EventHandler(this.dtpDateFilter_ValueChanged);
            // 
            // ProductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2320, 1338);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ProductionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Production";
            this.Load += new System.EventHandler(this.ProductionForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.pnlLeftMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDates)).EndInit();
            this.pnlDateFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Panel pnlLeftMain;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalProducts;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Panel pnlDateFilter;
        private System.Windows.Forms.DataGridView gridDates;
        private System.Windows.Forms.DateTimePicker dtpDateFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProductionDate;
        private System.Windows.Forms.DataGridView gridProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUnitCost;
        private System.Windows.Forms.DataGridViewLinkColumn clmViewRaw;
        private System.Windows.Forms.DataGridViewLinkColumn clmDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProductionId;
        private System.Windows.Forms.DataGridViewButtonColumn clmDeleteProduction;
    }
}