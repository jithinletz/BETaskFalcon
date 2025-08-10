namespace BETask.Views
{
    partial class DeliveryTrackForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlDeliveryUpdate = new System.Windows.Forms.Panel();
            this.gridDeliveredItems = new System.Windows.Forms.DataGridView();
            this.clmDeliveryItemId_delivery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId_delivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName_delivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPacking_delivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQtyToBe_delivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty_delivered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSaveDeliveryItems = new System.Windows.Forms.Button();
            this.btnHideDeliveryItems = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDeliveryLeaf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.chkDaily = new System.Windows.Forms.CheckBox();
            this.dtpDeliveredTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gridCustomers = new System.Windows.Forms.DataGridView();
            this.clmCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDeliveryCompleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddtoSales = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmChangeDelivery = new System.Windows.Forms.DataGridViewLinkColumn();
            this.pnlItemSummary = new System.Windows.Forms.Panel();
            this.gridItemSummary = new System.Windows.Forms.DataGridView();
            this.clmItemName_summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId_summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPacking_summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmQty_summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUsedQty_summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBalanceQty_summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButton = new System.Windows.Forms.Panel();
            this.pnlChangeDelivery = new System.Windows.Forms.Panel();
            this.lblChangeDelivery = new System.Windows.Forms.Label();
            this.linkChangeDelivery = new System.Windows.Forms.LinkLabel();
            this.txtDeliveryId = new System.Windows.Forms.TextBox();
            this.dtpNewDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.lblCount = new System.Windows.Forms.Label();
            this.chkShowSynced = new System.Windows.Forms.CheckBox();
            this.chkSaleNotUpdated = new System.Windows.Forms.CheckBox();
            this.chkSyncRecheck = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridDeliveries = new System.Windows.Forms.DataGridView();
            this.clmDeliveryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVehicle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlDeliveryUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveredItems)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            this.pnlItemSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItemSummary)).BeginInit();
            this.panelButton.SuspendLayout();
            this.pnlChangeDelivery.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveries)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlContent);
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1980, 1161);
            this.panel1.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlGrid);
            this.pnlContent.Controls.Add(this.pnlItemSummary);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(645, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1335, 959);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.pnlDeliveryUpdate);
            this.pnlGrid.Controls.Add(this.gridCustomers);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 223);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1335, 736);
            this.pnlGrid.TabIndex = 1;
            // 
            // pnlDeliveryUpdate
            // 
            this.pnlDeliveryUpdate.Controls.Add(this.gridDeliveredItems);
            this.pnlDeliveryUpdate.Controls.Add(this.panel4);
            this.pnlDeliveryUpdate.Controls.Add(this.panel3);
            this.pnlDeliveryUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDeliveryUpdate.Location = new System.Drawing.Point(0, 0);
            this.pnlDeliveryUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDeliveryUpdate.Name = "pnlDeliveryUpdate";
            this.pnlDeliveryUpdate.Size = new System.Drawing.Size(1335, 736);
            this.pnlDeliveryUpdate.TabIndex = 19;
            this.pnlDeliveryUpdate.Visible = false;
            // 
            // gridDeliveredItems
            // 
            this.gridDeliveredItems.AllowUserToAddRows = false;
            this.gridDeliveredItems.AllowUserToDeleteRows = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.Gainsboro;
            this.gridDeliveredItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.gridDeliveredItems.BackgroundColor = System.Drawing.Color.White;
            this.gridDeliveredItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDeliveredItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDeliveryItemId_delivery,
            this.clmItemId_delivered,
            this.clmItemName_delivered,
            this.clmPacking_delivered,
            this.clmQtyToBe_delivered,
            this.clmQty_delivered});
            this.gridDeliveredItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDeliveredItems.Location = new System.Drawing.Point(0, 148);
            this.gridDeliveredItems.Margin = new System.Windows.Forms.Padding(6);
            this.gridDeliveredItems.Name = "gridDeliveredItems";
            this.gridDeliveredItems.RowHeadersWidth = 51;
            this.gridDeliveredItems.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridDeliveredItems.RowTemplate.Height = 30;
            this.gridDeliveredItems.Size = new System.Drawing.Size(1335, 476);
            this.gridDeliveredItems.TabIndex = 2;
            this.gridDeliveredItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridDeliveredItems_EditingControlShowing);
            // 
            // clmDeliveryItemId_delivery
            // 
            this.clmDeliveryItemId_delivery.HeaderText = "DeliveryItemId";
            this.clmDeliveryItemId_delivery.MinimumWidth = 6;
            this.clmDeliveryItemId_delivery.Name = "clmDeliveryItemId_delivery";
            this.clmDeliveryItemId_delivery.Visible = false;
            this.clmDeliveryItemId_delivery.Width = 125;
            // 
            // clmItemId_delivered
            // 
            this.clmItemId_delivered.HeaderText = "Item ID";
            this.clmItemId_delivered.MinimumWidth = 6;
            this.clmItemId_delivered.Name = "clmItemId_delivered";
            this.clmItemId_delivered.Visible = false;
            this.clmItemId_delivered.Width = 125;
            // 
            // clmItemName_delivered
            // 
            this.clmItemName_delivered.HeaderText = "Item Name";
            this.clmItemName_delivered.MinimumWidth = 6;
            this.clmItemName_delivered.Name = "clmItemName_delivered";
            this.clmItemName_delivered.ReadOnly = true;
            this.clmItemName_delivered.Width = 200;
            // 
            // clmPacking_delivered
            // 
            this.clmPacking_delivered.HeaderText = "Packing";
            this.clmPacking_delivered.MinimumWidth = 6;
            this.clmPacking_delivered.Name = "clmPacking_delivered";
            this.clmPacking_delivered.ReadOnly = true;
            this.clmPacking_delivered.Width = 125;
            // 
            // clmQtyToBe_delivered
            // 
            this.clmQtyToBe_delivered.HeaderText = "Actual Qty";
            this.clmQtyToBe_delivered.MinimumWidth = 6;
            this.clmQtyToBe_delivered.Name = "clmQtyToBe_delivered";
            this.clmQtyToBe_delivered.ReadOnly = true;
            this.clmQtyToBe_delivered.Width = 125;
            // 
            // clmQty_delivered
            // 
            this.clmQty_delivered.HeaderText = "Qty Delivered";
            this.clmQty_delivered.MinimumWidth = 6;
            this.clmQty_delivered.Name = "clmQty_delivered";
            this.clmQty_delivered.Width = 125;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gray;
            this.panel4.Controls.Add(this.btnSaveDeliveryItems);
            this.panel4.Controls.Add(this.btnHideDeliveryItems);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 624);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1335, 112);
            this.panel4.TabIndex = 1;
            // 
            // btnSaveDeliveryItems
            // 
            this.btnSaveDeliveryItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDeliveryItems.BackColor = System.Drawing.Color.Teal;
            this.btnSaveDeliveryItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDeliveryItems.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDeliveryItems.ForeColor = System.Drawing.Color.White;
            this.btnSaveDeliveryItems.Location = new System.Drawing.Point(280, 19);
            this.btnSaveDeliveryItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSaveDeliveryItems.Name = "btnSaveDeliveryItems";
            this.btnSaveDeliveryItems.Size = new System.Drawing.Size(160, 73);
            this.btnSaveDeliveryItems.TabIndex = 3;
            this.btnSaveDeliveryItems.Text = "&Save";
            this.btnSaveDeliveryItems.UseVisualStyleBackColor = false;
            this.btnSaveDeliveryItems.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnHideDeliveryItems
            // 
            this.btnHideDeliveryItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideDeliveryItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnHideDeliveryItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHideDeliveryItems.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideDeliveryItems.ForeColor = System.Drawing.Color.White;
            this.btnHideDeliveryItems.Location = new System.Drawing.Point(111, 19);
            this.btnHideDeliveryItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnHideDeliveryItems.Name = "btnHideDeliveryItems";
            this.btnHideDeliveryItems.Size = new System.Drawing.Size(160, 73);
            this.btnHideDeliveryItems.TabIndex = 4;
            this.btnHideDeliveryItems.Text = "&Exit";
            this.btnHideDeliveryItems.UseVisualStyleBackColor = false;
            this.btnHideDeliveryItems.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtDeliveryLeaf);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cmbPaymentMode);
            this.panel3.Controls.Add(this.chkDaily);
            this.panel3.Controls.Add(this.dtpDeliveredTime);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1335, 148);
            this.panel3.TabIndex = 0;
            // 
            // txtDeliveryLeaf
            // 
            this.txtDeliveryLeaf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeliveryLeaf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeliveryLeaf.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDeliveryLeaf.Location = new System.Drawing.Point(882, 11);
            this.txtDeliveryLeaf.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDeliveryLeaf.MaxLength = 50;
            this.txtDeliveryLeaf.Name = "txtDeliveryLeaf";
            this.txtDeliveryLeaf.Size = new System.Drawing.Size(215, 46);
            this.txtDeliveryLeaf.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(660, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 40);
            this.label2.TabIndex = 4;
            this.label2.Text = "Delivered Leaf";
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(748, 84);
            this.cmbPaymentMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(348, 48);
            this.cmbPaymentMode.TabIndex = 3;
            this.cmbPaymentMode.Visible = false;
            // 
            // chkDaily
            // 
            this.chkDaily.AutoSize = true;
            this.chkDaily.Font = new System.Drawing.Font("Segoe UI", 6.8F);
            this.chkDaily.Location = new System.Drawing.Point(234, 84);
            this.chkDaily.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDaily.Name = "chkDaily";
            this.chkDaily.Size = new System.Drawing.Size(514, 54);
            this.chkDaily.TabIndex = 2;
            this.chkDaily.Text = "Update Daily Collection\r\n[Only for back date Entry and No sale will be generated]" +
    "";
            this.chkDaily.UseVisualStyleBackColor = true;
            this.chkDaily.CheckedChanged += new System.EventHandler(this.chkDaily_CheckedChanged);
            // 
            // dtpDeliveredTime
            // 
            this.dtpDeliveredTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpDeliveredTime.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDeliveredTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveredTime.Location = new System.Drawing.Point(234, 6);
            this.dtpDeliveredTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDeliveredTime.Name = "dtpDeliveredTime";
            this.dtpDeliveredTime.Size = new System.Drawing.Size(415, 46);
            this.dtpDeliveredTime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delivered Time";
            // 
            // gridCustomers
            // 
            this.gridCustomers.AllowUserToAddRows = false;
            this.gridCustomers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.Gainsboro;
            this.gridCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.gridCustomers.BackgroundColor = System.Drawing.Color.White;
            this.gridCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCustomerId,
            this.clmCustomer,
            this.clmNetAmount,
            this.clmDeliveryCompleted,
            this.clmInvoiceNo,
            this.clmAddtoSales,
            this.clmChangeDelivery});
            this.gridCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomers.Location = new System.Drawing.Point(0, 0);
            this.gridCustomers.Margin = new System.Windows.Forms.Padding(6);
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.ReadOnly = true;
            this.gridCustomers.RowHeadersWidth = 51;
            this.gridCustomers.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridCustomers.RowTemplate.Height = 30;
            this.gridCustomers.Size = new System.Drawing.Size(1335, 736);
            this.gridCustomers.TabIndex = 2;
            this.gridCustomers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomers_CellClick);
            this.gridCustomers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomers_CellDoubleClick);
            // 
            // clmCustomerId
            // 
            this.clmCustomerId.HeaderText = "Customer ID";
            this.clmCustomerId.MinimumWidth = 6;
            this.clmCustomerId.Name = "clmCustomerId";
            this.clmCustomerId.ReadOnly = true;
            this.clmCustomerId.Visible = false;
            this.clmCustomerId.Width = 125;
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.MinimumWidth = 6;
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Width = 150;
            // 
            // clmNetAmount
            // 
            this.clmNetAmount.HeaderText = "Net Amount";
            this.clmNetAmount.MinimumWidth = 6;
            this.clmNetAmount.Name = "clmNetAmount";
            this.clmNetAmount.ReadOnly = true;
            this.clmNetAmount.Width = 120;
            // 
            // clmDeliveryCompleted
            // 
            this.clmDeliveryCompleted.HeaderText = "Delivery Completed";
            this.clmDeliveryCompleted.MinimumWidth = 6;
            this.clmDeliveryCompleted.Name = "clmDeliveryCompleted";
            this.clmDeliveryCompleted.ReadOnly = true;
            this.clmDeliveryCompleted.ToolTipText = "Double Click To Update Delivery Time";
            this.clmDeliveryCompleted.Width = 200;
            // 
            // clmInvoiceNo
            // 
            this.clmInvoiceNo.HeaderText = "Invoice";
            this.clmInvoiceNo.MinimumWidth = 6;
            this.clmInvoiceNo.Name = "clmInvoiceNo";
            this.clmInvoiceNo.ReadOnly = true;
            this.clmInvoiceNo.Width = 125;
            // 
            // clmAddtoSales
            // 
            this.clmAddtoSales.HeaderText = "Add To Sales";
            this.clmAddtoSales.MinimumWidth = 6;
            this.clmAddtoSales.Name = "clmAddtoSales";
            this.clmAddtoSales.ReadOnly = true;
            this.clmAddtoSales.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmAddtoSales.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmAddtoSales.Visible = false;
            this.clmAddtoSales.Width = 125;
            // 
            // clmChangeDelivery
            // 
            this.clmChangeDelivery.HeaderText = "Change Date";
            this.clmChangeDelivery.MinimumWidth = 10;
            this.clmChangeDelivery.Name = "clmChangeDelivery";
            this.clmChangeDelivery.ReadOnly = true;
            this.clmChangeDelivery.Width = 200;
            // 
            // pnlItemSummary
            // 
            this.pnlItemSummary.Controls.Add(this.gridItemSummary);
            this.pnlItemSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlItemSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlItemSummary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlItemSummary.Name = "pnlItemSummary";
            this.pnlItemSummary.Size = new System.Drawing.Size(1335, 223);
            this.pnlItemSummary.TabIndex = 0;
            // 
            // gridItemSummary
            // 
            this.gridItemSummary.AllowUserToAddRows = false;
            this.gridItemSummary.AllowUserToDeleteRows = false;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridItemSummary.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle18;
            this.gridItemSummary.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridItemSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItemSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItemName_summary,
            this.clmItemId_summary,
            this.clmPacking_summary,
            this.clmQty_summary,
            this.clmUsedQty_summary,
            this.clmBalanceQty_summary});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridItemSummary.DefaultCellStyle = dataGridViewCellStyle19;
            this.gridItemSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItemSummary.Location = new System.Drawing.Point(0, 0);
            this.gridItemSummary.Margin = new System.Windows.Forms.Padding(6);
            this.gridItemSummary.Name = "gridItemSummary";
            this.gridItemSummary.ReadOnly = true;
            this.gridItemSummary.RowHeadersVisible = false;
            this.gridItemSummary.RowHeadersWidth = 51;
            this.gridItemSummary.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridItemSummary.RowTemplate.Height = 24;
            this.gridItemSummary.Size = new System.Drawing.Size(1335, 223);
            this.gridItemSummary.TabIndex = 18;
            // 
            // clmItemName_summary
            // 
            this.clmItemName_summary.HeaderText = "Item Name";
            this.clmItemName_summary.MinimumWidth = 6;
            this.clmItemName_summary.Name = "clmItemName_summary";
            this.clmItemName_summary.ReadOnly = true;
            this.clmItemName_summary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmItemName_summary.Width = 200;
            // 
            // clmItemId_summary
            // 
            this.clmItemId_summary.HeaderText = "ID";
            this.clmItemId_summary.MinimumWidth = 6;
            this.clmItemId_summary.Name = "clmItemId_summary";
            this.clmItemId_summary.ReadOnly = true;
            this.clmItemId_summary.Visible = false;
            this.clmItemId_summary.Width = 125;
            // 
            // clmPacking_summary
            // 
            this.clmPacking_summary.HeaderText = "Packing";
            this.clmPacking_summary.MinimumWidth = 6;
            this.clmPacking_summary.Name = "clmPacking_summary";
            this.clmPacking_summary.ReadOnly = true;
            this.clmPacking_summary.Width = 80;
            // 
            // clmQty_summary
            // 
            this.clmQty_summary.HeaderText = "QTY";
            this.clmQty_summary.MinimumWidth = 6;
            this.clmQty_summary.Name = "clmQty_summary";
            this.clmQty_summary.ReadOnly = true;
            this.clmQty_summary.Width = 80;
            // 
            // clmUsedQty_summary
            // 
            this.clmUsedQty_summary.HeaderText = "UsedQty";
            this.clmUsedQty_summary.MinimumWidth = 6;
            this.clmUsedQty_summary.Name = "clmUsedQty_summary";
            this.clmUsedQty_summary.ReadOnly = true;
            this.clmUsedQty_summary.Width = 80;
            // 
            // clmBalanceQty_summary
            // 
            this.clmBalanceQty_summary.HeaderText = "Balance Qty";
            this.clmBalanceQty_summary.MinimumWidth = 6;
            this.clmBalanceQty_summary.Name = "clmBalanceQty_summary";
            this.clmBalanceQty_summary.ReadOnly = true;
            this.clmBalanceQty_summary.Width = 80;
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelButton.Controls.Add(this.pnlChangeDelivery);
            this.panelButton.Controls.Add(this.lblCount);
            this.panelButton.Controls.Add(this.chkShowSynced);
            this.panelButton.Controls.Add(this.chkSaleNotUpdated);
            this.panelButton.Controls.Add(this.chkSyncRecheck);
            this.panelButton.Controls.Add(this.linkLabel1);
            this.panelButton.Controls.Add(this.btnCancel);
            this.panelButton.Controls.Add(this.btnClose);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButton.Location = new System.Drawing.Point(645, 959);
            this.panelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(1335, 202);
            this.panelButton.TabIndex = 1;
            // 
            // pnlChangeDelivery
            // 
            this.pnlChangeDelivery.BackColor = System.Drawing.Color.Black;
            this.pnlChangeDelivery.Controls.Add(this.lblChangeDelivery);
            this.pnlChangeDelivery.Controls.Add(this.linkChangeDelivery);
            this.pnlChangeDelivery.Controls.Add(this.txtDeliveryId);
            this.pnlChangeDelivery.Controls.Add(this.dtpNewDeliveryDate);
            this.pnlChangeDelivery.Location = new System.Drawing.Point(52, 27);
            this.pnlChangeDelivery.Name = "pnlChangeDelivery";
            this.pnlChangeDelivery.Size = new System.Drawing.Size(813, 138);
            this.pnlChangeDelivery.TabIndex = 12;
            this.pnlChangeDelivery.Visible = false;
            // 
            // lblChangeDelivery
            // 
            this.lblChangeDelivery.AutoSize = true;
            this.lblChangeDelivery.Font = new System.Drawing.Font("Segoe UI", 10.9F);
            this.lblChangeDelivery.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblChangeDelivery.Location = new System.Drawing.Point(29, 6);
            this.lblChangeDelivery.Name = "lblChangeDelivery";
            this.lblChangeDelivery.Size = new System.Drawing.Size(298, 41);
            this.lblChangeDelivery.TabIndex = 8;
            this.lblChangeDelivery.Text = "Change delivery date";
            // 
            // linkChangeDelivery
            // 
            this.linkChangeDelivery.AutoSize = true;
            this.linkChangeDelivery.Font = new System.Drawing.Font("Segoe UI", 10.9F, System.Drawing.FontStyle.Bold);
            this.linkChangeDelivery.LinkColor = System.Drawing.Color.White;
            this.linkChangeDelivery.Location = new System.Drawing.Point(540, 69);
            this.linkChangeDelivery.Name = "linkChangeDelivery";
            this.linkChangeDelivery.Size = new System.Drawing.Size(122, 41);
            this.linkChangeDelivery.TabIndex = 7;
            this.linkChangeDelivery.TabStop = true;
            this.linkChangeDelivery.Text = "Update";
            this.linkChangeDelivery.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkChangeDelivery_LinkClicked);
            // 
            // txtDeliveryId
            // 
            this.txtDeliveryId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeliveryId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeliveryId.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDeliveryId.Location = new System.Drawing.Point(290, 68);
            this.txtDeliveryId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDeliveryId.MaxLength = 50;
            this.txtDeliveryId.Name = "txtDeliveryId";
            this.txtDeliveryId.ReadOnly = true;
            this.txtDeliveryId.Size = new System.Drawing.Size(215, 46);
            this.txtDeliveryId.TabIndex = 6;
            // 
            // dtpNewDeliveryDate
            // 
            this.dtpNewDeliveryDate.CustomFormat = "dd/MM/yyyy";
            this.dtpNewDeliveryDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpNewDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNewDeliveryDate.Location = new System.Drawing.Point(22, 68);
            this.dtpNewDeliveryDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpNewDeliveryDate.Name = "dtpNewDeliveryDate";
            this.dtpNewDeliveryDate.Size = new System.Drawing.Size(246, 46);
            this.dtpNewDeliveryDate.TabIndex = 2;
            this.dtpNewDeliveryDate.ValueChanged += new System.EventHandler(this.dtpNewDeliveryDate_ValueChanged);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCount.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCount.Location = new System.Drawing.Point(32, 111);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 40);
            this.lblCount.TabIndex = 11;
            // 
            // chkShowSynced
            // 
            this.chkShowSynced.AutoSize = true;
            this.chkShowSynced.BackColor = System.Drawing.Color.Transparent;
            this.chkShowSynced.Checked = true;
            this.chkShowSynced.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSynced.Font = new System.Drawing.Font("Segoe UI", 10.9F);
            this.chkShowSynced.ForeColor = System.Drawing.Color.White;
            this.chkShowSynced.Location = new System.Drawing.Point(1018, 33);
            this.chkShowSynced.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkShowSynced.Name = "chkShowSynced";
            this.chkShowSynced.Size = new System.Drawing.Size(298, 45);
            this.chkShowSynced.TabIndex = 10;
            this.chkShowSynced.Text = "Show downloaded";
            this.chkShowSynced.UseVisualStyleBackColor = false;
            this.chkShowSynced.Visible = false;
            // 
            // chkSaleNotUpdated
            // 
            this.chkSaleNotUpdated.AutoSize = true;
            this.chkSaleNotUpdated.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chkSaleNotUpdated.Font = new System.Drawing.Font("Segoe UI", 10.9F);
            this.chkSaleNotUpdated.Location = new System.Drawing.Point(950, 11);
            this.chkSaleNotUpdated.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSaleNotUpdated.Name = "chkSaleNotUpdated";
            this.chkSaleNotUpdated.Size = new System.Drawing.Size(346, 45);
            this.chkSaleNotUpdated.TabIndex = 9;
            this.chkSaleNotUpdated.Text = "Sync Sale not updated";
            this.chkSaleNotUpdated.UseVisualStyleBackColor = false;
            this.chkSaleNotUpdated.Visible = false;
            // 
            // chkSyncRecheck
            // 
            this.chkSyncRecheck.AutoSize = true;
            this.chkSyncRecheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chkSyncRecheck.Font = new System.Drawing.Font("Segoe UI", 10.9F);
            this.chkSyncRecheck.Location = new System.Drawing.Point(1036, 37);
            this.chkSyncRecheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSyncRecheck.Name = "chkSyncRecheck";
            this.chkSyncRecheck.Size = new System.Drawing.Size(228, 45);
            this.chkSyncRecheck.TabIndex = 5;
            this.chkSyncRecheck.Text = "Sync Recheck";
            this.chkSyncRecheck.UseVisualStyleBackColor = false;
            this.chkSyncRecheck.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(999, 38);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(342, 40);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Get Today\'s Delivery Data";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1156, 111);
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
            this.btnClose.Location = new System.Drawing.Point(988, 111);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 73);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridDeliveries);
            this.panel2.Controls.Add(this.dtpDeliveryDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(645, 1161);
            this.panel2.TabIndex = 0;
            // 
            // gridDeliveries
            // 
            this.gridDeliveries.AllowUserToAddRows = false;
            this.gridDeliveries.AllowUserToDeleteRows = false;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Gainsboro;
            this.gridDeliveries.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle20;
            this.gridDeliveries.BackgroundColor = System.Drawing.Color.White;
            this.gridDeliveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDeliveries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDeliveryId,
            this.clmEmployee,
            this.clmRoute,
            this.clmVehicle});
            this.gridDeliveries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDeliveries.Location = new System.Drawing.Point(0, 46);
            this.gridDeliveries.Margin = new System.Windows.Forms.Padding(6);
            this.gridDeliveries.Name = "gridDeliveries";
            this.gridDeliveries.ReadOnly = true;
            this.gridDeliveries.RowHeadersVisible = false;
            this.gridDeliveries.RowHeadersWidth = 51;
            this.gridDeliveries.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridDeliveries.RowTemplate.Height = 30;
            this.gridDeliveries.Size = new System.Drawing.Size(645, 1115);
            this.gridDeliveries.TabIndex = 1;
            this.gridDeliveries.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDeliveries_CellClick);
            // 
            // clmDeliveryId
            // 
            this.clmDeliveryId.HeaderText = "DeliveryId";
            this.clmDeliveryId.MinimumWidth = 6;
            this.clmDeliveryId.Name = "clmDeliveryId";
            this.clmDeliveryId.ReadOnly = true;
            this.clmDeliveryId.Width = 60;
            // 
            // clmEmployee
            // 
            this.clmEmployee.HeaderText = "Employee";
            this.clmEmployee.MinimumWidth = 6;
            this.clmEmployee.Name = "clmEmployee";
            this.clmEmployee.ReadOnly = true;
            this.clmEmployee.Width = 150;
            // 
            // clmRoute
            // 
            this.clmRoute.HeaderText = "Route";
            this.clmRoute.MinimumWidth = 6;
            this.clmRoute.Name = "clmRoute";
            this.clmRoute.ReadOnly = true;
            this.clmRoute.Width = 125;
            // 
            // clmVehicle
            // 
            this.clmVehicle.HeaderText = "Vehicle";
            this.clmVehicle.MinimumWidth = 6;
            this.clmVehicle.Name = "clmVehicle";
            this.clmVehicle.ReadOnly = true;
            this.clmVehicle.Width = 125;
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDeliveryDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpDeliveryDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(0, 0);
            this.dtpDeliveryDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(645, 46);
            this.dtpDeliveryDate.TabIndex = 0;
            this.dtpDeliveryDate.ValueChanged += new System.EventHandler(this.dtpDeliveryDate_ValueChanged);
            // 
            // DeliveryTrackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1980, 1161);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DeliveryTrackForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Track";
            this.Load += new System.EventHandler(this.DeliveryTrackForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlDeliveryUpdate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveredItems)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            this.pnlItemSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItemSummary)).EndInit();
            this.panelButton.ResumeLayout(false);
            this.panelButton.PerformLayout();
            this.pnlChangeDelivery.ResumeLayout(false);
            this.pnlChangeDelivery.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.DataGridView gridDeliveries;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlItemSummary;
        private System.Windows.Forms.DataGridView gridItemSummary;
        private System.Windows.Forms.DataGridView gridCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName_summary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId_summary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking_summary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty_summary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUsedQty_summary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBalanceQty_summary;
        private System.Windows.Forms.Panel pnlDeliveryUpdate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDeliveredTime;
        private System.Windows.Forms.DataGridView gridDeliveredItems;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSaveDeliveryItems;
        private System.Windows.Forms.Button btnHideDeliveryItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeliveryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVehicle;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chkDaily;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.CheckBox chkSyncRecheck;
        private System.Windows.Forms.CheckBox chkSaleNotUpdated;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeliveryLeaf;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeliveryItemId_delivery;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId_delivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName_delivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPacking_delivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQtyToBe_delivered;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmQty_delivered;
        private System.Windows.Forms.CheckBox chkShowSynced;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNetAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeliveryCompleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInvoiceNo;
        private System.Windows.Forms.DataGridViewLinkColumn clmAddtoSales;
        private System.Windows.Forms.DataGridViewLinkColumn clmChangeDelivery;
        private System.Windows.Forms.Panel pnlChangeDelivery;
        private System.Windows.Forms.DateTimePicker dtpNewDeliveryDate;
        private System.Windows.Forms.TextBox txtDeliveryId;
        private System.Windows.Forms.LinkLabel linkChangeDelivery;
        private System.Windows.Forms.Label lblChangeDelivery;
    }
}