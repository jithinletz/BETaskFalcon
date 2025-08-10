namespace BETask.Views
{
    partial class CustomerAggrementForm
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
            this.pnlSaveContent = new System.Windows.Forms.Panel();
            this.linkTaxCalculator = new System.Windows.Forms.LinkLabel();
            this.grpAgreementCalculator = new System.Windows.Forms.GroupBox();
            this.txtAgreement = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTaxRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.pnlAgreementTemp = new System.Windows.Forms.Panel();
            this.gridTemp = new System.Windows.Forms.DataGridView();
            this.clmTempItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTempItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTemppacking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTempMaxCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTempActualRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTempSpecualRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTempSavedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lnkClose = new System.Windows.Forms.LinkLabel();
            this.dgAggrement = new System.Windows.Forms.DataGridView();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmPacking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSpecialRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmShowApp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmSerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAgreementId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRemove = new System.Windows.Forms.DataGridViewLinkColumn();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButton = new System.Windows.Forms.Panel();
            this.linkClosedAgreement = new System.Windows.Forms.LinkLabel();
            this.lnkLoadAgreement = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.grpAgreementCalculator.SuspendLayout();
            this.panelGrid.SuspendLayout();
            this.pnlAgreementTemp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemp)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAggrement)).BeginInit();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSaveContent);
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 641);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSaveContent.Controls.Add(this.linkTaxCalculator);
            this.pnlSaveContent.Controls.Add(this.grpAgreementCalculator);
            this.pnlSaveContent.Controls.Add(this.panelGrid);
            this.pnlSaveContent.Controls.Add(this.cmbCustomerName);
            this.pnlSaveContent.Controls.Add(this.label1);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(1117, 563);
            this.pnlSaveContent.TabIndex = 1;
            // 
            // linkTaxCalculator
            // 
            this.linkTaxCalculator.AutoSize = true;
            this.linkTaxCalculator.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkTaxCalculator.LinkColor = System.Drawing.Color.Black;
            this.linkTaxCalculator.Location = new System.Drawing.Point(871, 116);
            this.linkTaxCalculator.Name = "linkTaxCalculator";
            this.linkTaxCalculator.Size = new System.Drawing.Size(188, 25);
            this.linkTaxCalculator.TabIndex = 10;
            this.linkTaxCalculator.TabStop = true;
            this.linkTaxCalculator.Text = "Calculate Agreement";
            this.linkTaxCalculator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTaxCalculator_LinkClicked);
            // 
            // grpAgreementCalculator
            // 
            this.grpAgreementCalculator.BackColor = System.Drawing.SystemColors.Info;
            this.grpAgreementCalculator.Controls.Add(this.txtAgreement);
            this.grpAgreementCalculator.Controls.Add(this.label5);
            this.grpAgreementCalculator.Controls.Add(this.txtTaxRate);
            this.grpAgreementCalculator.Controls.Add(this.label3);
            this.grpAgreementCalculator.Controls.Add(this.txtTaxAmount);
            this.grpAgreementCalculator.Controls.Add(this.label4);
            this.grpAgreementCalculator.Controls.Add(this.txtRate);
            this.grpAgreementCalculator.Controls.Add(this.label2);
            this.grpAgreementCalculator.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.grpAgreementCalculator.Location = new System.Drawing.Point(615, 12);
            this.grpAgreementCalculator.Name = "grpAgreementCalculator";
            this.grpAgreementCalculator.Size = new System.Drawing.Size(444, 101);
            this.grpAgreementCalculator.TabIndex = 11;
            this.grpAgreementCalculator.TabStop = false;
            this.grpAgreementCalculator.Text = "Calculate Rate";
            this.grpAgreementCalculator.Visible = false;
            // 
            // txtAgreement
            // 
            this.txtAgreement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAgreement.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtAgreement.Location = new System.Drawing.Point(326, 50);
            this.txtAgreement.Margin = new System.Windows.Forms.Padding(4);
            this.txtAgreement.Name = "txtAgreement";
            this.txtAgreement.ReadOnly = true;
            this.txtAgreement.Size = new System.Drawing.Size(83, 25);
            this.txtAgreement.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label5.Location = new System.Drawing.Point(322, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "Agreement";
            // 
            // txtTaxRate
            // 
            this.txtTaxRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtTaxRate.Location = new System.Drawing.Point(110, 50);
            this.txtTaxRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxRate.Name = "txtTaxRate";
            this.txtTaxRate.Size = new System.Drawing.Size(50, 25);
            this.txtTaxRate.TabIndex = 6;
            this.txtTaxRate.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.Location = new System.Drawing.Point(106, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "TaxRate";
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxAmount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtTaxAmount.Location = new System.Drawing.Point(235, 50);
            this.txtTaxAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.ReadOnly = true;
            this.txtTaxAmount.Size = new System.Drawing.Size(83, 25);
            this.txtTaxAmount.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.Location = new System.Drawing.Point(231, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tax Amount";
            // 
            // txtRate
            // 
            this.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtRate.Location = new System.Drawing.Point(19, 50);
            this.txtRate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(83, 25);
            this.txtRate.TabIndex = 4;
            this.txtRate.TextChanged += new System.EventHandler(this.txtRate_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.Location = new System.Drawing.Point(15, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rate WithTax";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.pnlAgreementTemp);
            this.panelGrid.Controls.Add(this.dgAggrement);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGrid.Location = new System.Drawing.Point(0, 157);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1117, 406);
            this.panelGrid.TabIndex = 8;
            // 
            // pnlAgreementTemp
            // 
            this.pnlAgreementTemp.Controls.Add(this.gridTemp);
            this.pnlAgreementTemp.Controls.Add(this.panel3);
            this.pnlAgreementTemp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAgreementTemp.Location = new System.Drawing.Point(0, 213);
            this.pnlAgreementTemp.Name = "pnlAgreementTemp";
            this.pnlAgreementTemp.Size = new System.Drawing.Size(1117, 193);
            this.pnlAgreementTemp.TabIndex = 8;
            this.pnlAgreementTemp.Visible = false;
            // 
            // gridTemp
            // 
            this.gridTemp.AllowUserToAddRows = false;
            this.gridTemp.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.gridTemp.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTemp.BackgroundColor = System.Drawing.Color.Silver;
            this.gridTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTemp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTempItemId,
            this.clmTempItemName,
            this.clmTemppacking,
            this.clmTempMaxCount,
            this.clmTempActualRate,
            this.clmTempSpecualRate,
            this.clmTempSavedPrice});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTemp.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTemp.Location = new System.Drawing.Point(0, 33);
            this.gridTemp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTemp.Name = "gridTemp";
            this.gridTemp.RowHeadersVisible = false;
            this.gridTemp.RowHeadersWidth = 51;
            this.gridTemp.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridTemp.RowTemplate.Height = 30;
            this.gridTemp.Size = new System.Drawing.Size(1117, 160);
            this.gridTemp.TabIndex = 8;
            // 
            // clmTempItemId
            // 
            this.clmTempItemId.HeaderText = "Item ID";
            this.clmTempItemId.MinimumWidth = 6;
            this.clmTempItemId.Name = "clmTempItemId";
            this.clmTempItemId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmTempItemId.Visible = false;
            this.clmTempItemId.Width = 125;
            // 
            // clmTempItemName
            // 
            this.clmTempItemName.HeaderText = "Item Name";
            this.clmTempItemName.MinimumWidth = 6;
            this.clmTempItemName.Name = "clmTempItemName";
            this.clmTempItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmTempItemName.Width = 250;
            // 
            // clmTemppacking
            // 
            this.clmTemppacking.HeaderText = "Packing";
            this.clmTemppacking.MinimumWidth = 6;
            this.clmTemppacking.Name = "clmTemppacking";
            this.clmTemppacking.Width = 150;
            // 
            // clmTempMaxCount
            // 
            this.clmTempMaxCount.HeaderText = "Max Count";
            this.clmTempMaxCount.MinimumWidth = 6;
            this.clmTempMaxCount.Name = "clmTempMaxCount";
            this.clmTempMaxCount.Width = 125;
            // 
            // clmTempActualRate
            // 
            this.clmTempActualRate.HeaderText = "Actual Rate";
            this.clmTempActualRate.MinimumWidth = 6;
            this.clmTempActualRate.Name = "clmTempActualRate";
            this.clmTempActualRate.Width = 125;
            // 
            // clmTempSpecualRate
            // 
            this.clmTempSpecualRate.HeaderText = "Special Rate";
            this.clmTempSpecualRate.MinimumWidth = 6;
            this.clmTempSpecualRate.Name = "clmTempSpecualRate";
            this.clmTempSpecualRate.Width = 125;
            // 
            // clmTempSavedPrice
            // 
            this.clmTempSavedPrice.HeaderText = "SavedPrice";
            this.clmTempSavedPrice.MinimumWidth = 6;
            this.clmTempSavedPrice.Name = "clmTempSavedPrice";
            this.clmTempSavedPrice.Width = 125;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel3.Controls.Add(this.linkLabel1);
            this.panel3.Controls.Add(this.lnkClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1117, 33);
            this.panel3.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 12.8F);
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(3, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(234, 30);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "APP saved agreements";
            // 
            // lnkClose
            // 
            this.lnkClose.AutoSize = true;
            this.lnkClose.Font = new System.Drawing.Font("Segoe UI", 12.8F);
            this.lnkClose.LinkColor = System.Drawing.Color.White;
            this.lnkClose.Location = new System.Drawing.Point(881, 0);
            this.lnkClose.Name = "lnkClose";
            this.lnkClose.Size = new System.Drawing.Size(85, 30);
            this.lnkClose.TabIndex = 0;
            this.lnkClose.TabStop = true;
            this.lnkClose.Text = "Close X";
            this.lnkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClose_LinkClicked);
            // 
            // dgAggrement
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.dgAggrement.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgAggrement.BackgroundColor = System.Drawing.Color.White;
            this.dgAggrement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAggrement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItemId,
            this.clmItemName,
            this.clmPacking,
            this.MaxCount,
            this.clmRate,
            this.clmSpecialRate,
            this.clmShowApp,
            this.clmSerialNo,
            this.clmRemarks,
            this.clmAgreementId,
            this.clmRemove});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAggrement.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgAggrement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAggrement.Location = new System.Drawing.Point(0, 0);
            this.dgAggrement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgAggrement.Name = "dgAggrement";
            this.dgAggrement.RowHeadersVisible = false;
            this.dgAggrement.RowHeadersWidth = 51;
            this.dgAggrement.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dgAggrement.RowTemplate.Height = 30;
            this.dgAggrement.Size = new System.Drawing.Size(1117, 406);
            this.dgAggrement.TabIndex = 7;
            this.dgAggrement.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAggrement_CellClick);
            this.dgAggrement.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAggrement_CellValueChanged);
            this.dgAggrement.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgAggrement_CurrentCellDirtyStateChanged);
            this.dgAggrement.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgAggrement_EditingControlShowing);
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "Item ID";
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
            this.clmItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmItemName.Width = 250;
            // 
            // clmPacking
            // 
            this.clmPacking.HeaderText = "Packing";
            this.clmPacking.MinimumWidth = 6;
            this.clmPacking.Name = "clmPacking";
            this.clmPacking.ReadOnly = true;
            this.clmPacking.Width = 125;
            // 
            // MaxCount
            // 
            this.MaxCount.HeaderText = "Max Count";
            this.MaxCount.MinimumWidth = 6;
            this.MaxCount.Name = "MaxCount";
            this.MaxCount.Width = 125;
            // 
            // clmRate
            // 
            this.clmRate.HeaderText = "Actual Rate";
            this.clmRate.MinimumWidth = 6;
            this.clmRate.Name = "clmRate";
            this.clmRate.ReadOnly = true;
            this.clmRate.Width = 125;
            // 
            // clmSpecialRate
            // 
            this.clmSpecialRate.HeaderText = "Special Rate";
            this.clmSpecialRate.MinimumWidth = 6;
            this.clmSpecialRate.Name = "clmSpecialRate";
            this.clmSpecialRate.Width = 125;
            // 
            // clmShowApp
            // 
            this.clmShowApp.HeaderText = "Show App";
            this.clmShowApp.MinimumWidth = 6;
            this.clmShowApp.Name = "clmShowApp";
            this.clmShowApp.Width = 50;
            // 
            // clmSerialNo
            // 
            this.clmSerialNo.HeaderText = "SerialNo";
            this.clmSerialNo.MinimumWidth = 6;
            this.clmSerialNo.Name = "clmSerialNo";
            this.clmSerialNo.Width = 150;
            // 
            // clmRemarks
            // 
            this.clmRemarks.HeaderText = "Remarks";
            this.clmRemarks.MinimumWidth = 6;
            this.clmRemarks.Name = "clmRemarks";
            this.clmRemarks.Width = 150;
            // 
            // clmAgreementId
            // 
            this.clmAgreementId.HeaderText = "AgreementId";
            this.clmAgreementId.MinimumWidth = 6;
            this.clmAgreementId.Name = "clmAgreementId";
            this.clmAgreementId.Visible = false;
            this.clmAgreementId.Width = 125;
            // 
            // clmRemove
            // 
            this.clmRemove.HeaderText = "Remove";
            this.clmRemove.MinimumWidth = 6;
            this.clmRemove.Name = "clmRemove";
            this.clmRemove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmRemove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmRemove.Width = 125;
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(207, 43);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(560, 33);
            this.cmbCustomerName.TabIndex = 2;
            this.cmbCustomerName.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerName_SelectedIndexChanged);
            this.cmbCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(40, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customer Name";
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.linkClosedAgreement);
            this.panelButton.Controls.Add(this.lnkLoadAgreement);
            this.panelButton.Controls.Add(this.btnSave);
            this.panelButton.Controls.Add(this.btnCancel);
            this.panelButton.Controls.Add(this.btnClose);
            this.panelButton.Controls.Add(this.btnNew);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(0, 563);
            this.panelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(1117, 78);
            this.panelButton.TabIndex = 0;
            // 
            // linkClosedAgreement
            // 
            this.linkClosedAgreement.AutoSize = true;
            this.linkClosedAgreement.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkClosedAgreement.LinkColor = System.Drawing.Color.White;
            this.linkClosedAgreement.Location = new System.Drawing.Point(187, 27);
            this.linkClosedAgreement.Name = "linkClosedAgreement";
            this.linkClosedAgreement.Size = new System.Drawing.Size(120, 25);
            this.linkClosedAgreement.TabIndex = 9;
            this.linkClosedAgreement.TabStop = true;
            this.linkClosedAgreement.Text = "Show Closed";
            this.linkClosedAgreement.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClosedAgreement_LinkClicked);
            // 
            // lnkLoadAgreement
            // 
            this.lnkLoadAgreement.AutoSize = true;
            this.lnkLoadAgreement.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lnkLoadAgreement.LinkColor = System.Drawing.Color.White;
            this.lnkLoadAgreement.Location = new System.Drawing.Point(12, 27);
            this.lnkLoadAgreement.Name = "lnkLoadAgreement";
            this.lnkLoadAgreement.Size = new System.Drawing.Size(150, 25);
            this.lnkLoadAgreement.TabIndex = 8;
            this.lnkLoadAgreement.TabStop = true;
            this.lnkLoadAgreement.Text = "Load from cloud";
            this.lnkLoadAgreement.Visible = false;
            this.lnkLoadAgreement.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(738, 17);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
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
            this.btnCancel.Location = new System.Drawing.Point(626, 17);
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
            this.btnClose.Location = new System.Drawing.Point(514, 17);
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
            this.btnNew.Location = new System.Drawing.Point(401, 17);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 47);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // CustomerAggrementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 641);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.Name = "CustomerAggrementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Mapping";
            this.Load += new System.EventHandler(this.CustomerAggrementForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.pnlSaveContent.PerformLayout();
            this.grpAgreementCalculator.ResumeLayout(false);
            this.grpAgreementCalculator.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            this.pnlAgreementTemp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTemp)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAggrement)).EndInit();
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.DataGridView dgAggrement;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.LinkLabel lnkLoadAgreement;
        private System.Windows.Forms.Panel pnlAgreementTemp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel lnkClose;
        private System.Windows.Forms.DataGridView gridTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTemppacking;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempMaxCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempActualRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempSpecualRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempSavedPrice;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkTaxCalculator;
        private System.Windows.Forms.GroupBox grpAgreementCalculator;
        private System.Windows.Forms.TextBox txtAgreement;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTaxRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSpecialRate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmShowApp;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRemarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAgreementId;
        private System.Windows.Forms.DataGridViewLinkColumn clmRemove;
        private System.Windows.Forms.LinkLabel linkClosedAgreement;
    }
}