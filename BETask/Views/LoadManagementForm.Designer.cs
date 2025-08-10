namespace BETask.Views
{
    partial class LoadManagementForm
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
            this.dgDelivery = new System.Windows.Forms.DataGridView();
            this.clmLoadId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOldStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmpty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDamage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmExtra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOffload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmActStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkReport = new System.Windows.Forms.LinkLabel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblPrvBalance = new System.Windows.Forms.Label();
            this.txtTotalEmpty = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTotalLoading = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtActualStock = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEmpty = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNewStock = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNewLoad = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalExtra = new System.Windows.Forms.TextBox();
            this.txtExtra = new System.Windows.Forms.TextBox();
            this.txtTotalShort = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtShort = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOffload = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDamage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOldStock = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtHelper = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.txtVehicle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDelivery)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.dgDelivery);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2104, 1113);
            this.panel1.TabIndex = 0;
            // 
            // dgDelivery
            // 
            this.dgDelivery.AllowUserToAddRows = false;
            this.dgDelivery.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgDelivery.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDelivery.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDelivery.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDelivery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDelivery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmLoadId,
            this.clmDateTime,
            this.clmItemId,
            this.clmItemName,
            this.clmOldStock,
            this.clmEmpty,
            this.clmDamage,
            this.clmBalance,
            this.clmLoad,
            this.clmShort,
            this.clmExtra,
            this.clmStock,
            this.clmOffload,
            this.clmActStock,
            this.clmDelete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDelivery.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDelivery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDelivery.Location = new System.Drawing.Point(0, 461);
            this.dgDelivery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgDelivery.Name = "dgDelivery";
            this.dgDelivery.ReadOnly = true;
            this.dgDelivery.RowHeadersVisible = false;
            this.dgDelivery.RowHeadersWidth = 82;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgDelivery.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDelivery.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgDelivery.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dgDelivery.RowTemplate.Height = 28;
            this.dgDelivery.Size = new System.Drawing.Size(2104, 496);
            this.dgDelivery.TabIndex = 2;
            this.dgDelivery.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDelivery_CellClick);
            // 
            // clmLoadId
            // 
            this.clmLoadId.HeaderText = "LoadId";
            this.clmLoadId.MinimumWidth = 10;
            this.clmLoadId.Name = "clmLoadId";
            this.clmLoadId.ReadOnly = true;
            this.clmLoadId.Visible = false;
            this.clmLoadId.Width = 200;
            // 
            // clmDateTime
            // 
            this.clmDateTime.HeaderText = "Date Time";
            this.clmDateTime.MinimumWidth = 10;
            this.clmDateTime.Name = "clmDateTime";
            this.clmDateTime.ReadOnly = true;
            this.clmDateTime.Width = 150;
            // 
            // clmItemId
            // 
            this.clmItemId.HeaderText = "Item Id";
            this.clmItemId.MinimumWidth = 10;
            this.clmItemId.Name = "clmItemId";
            this.clmItemId.ReadOnly = true;
            this.clmItemId.Visible = false;
            this.clmItemId.Width = 200;
            // 
            // clmItemName
            // 
            this.clmItemName.HeaderText = "Item Name";
            this.clmItemName.MinimumWidth = 10;
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.ReadOnly = true;
            this.clmItemName.Width = 200;
            // 
            // clmOldStock
            // 
            this.clmOldStock.HeaderText = "Old Stock";
            this.clmOldStock.MinimumWidth = 10;
            this.clmOldStock.Name = "clmOldStock";
            this.clmOldStock.ReadOnly = true;
            this.clmOldStock.Width = 60;
            // 
            // clmEmpty
            // 
            this.clmEmpty.HeaderText = "Empty";
            this.clmEmpty.MinimumWidth = 10;
            this.clmEmpty.Name = "clmEmpty";
            this.clmEmpty.ReadOnly = true;
            this.clmEmpty.Width = 60;
            // 
            // clmDamage
            // 
            this.clmDamage.HeaderText = "Damage";
            this.clmDamage.MinimumWidth = 10;
            this.clmDamage.Name = "clmDamage";
            this.clmDamage.ReadOnly = true;
            this.clmDamage.Width = 60;
            // 
            // clmBalance
            // 
            this.clmBalance.HeaderText = "Balance";
            this.clmBalance.MinimumWidth = 10;
            this.clmBalance.Name = "clmBalance";
            this.clmBalance.ReadOnly = true;
            this.clmBalance.Width = 60;
            // 
            // clmLoad
            // 
            this.clmLoad.HeaderText = "Load";
            this.clmLoad.MinimumWidth = 10;
            this.clmLoad.Name = "clmLoad";
            this.clmLoad.ReadOnly = true;
            this.clmLoad.Width = 60;
            // 
            // clmShort
            // 
            this.clmShort.HeaderText = "Short";
            this.clmShort.MinimumWidth = 10;
            this.clmShort.Name = "clmShort";
            this.clmShort.ReadOnly = true;
            this.clmShort.Width = 60;
            // 
            // clmExtra
            // 
            this.clmExtra.HeaderText = "Extra";
            this.clmExtra.MinimumWidth = 10;
            this.clmExtra.Name = "clmExtra";
            this.clmExtra.ReadOnly = true;
            this.clmExtra.Width = 60;
            // 
            // clmStock
            // 
            this.clmStock.HeaderText = "Stock";
            this.clmStock.MinimumWidth = 10;
            this.clmStock.Name = "clmStock";
            this.clmStock.ReadOnly = true;
            this.clmStock.Width = 60;
            // 
            // clmOffload
            // 
            this.clmOffload.HeaderText = "OffLoad";
            this.clmOffload.MinimumWidth = 10;
            this.clmOffload.Name = "clmOffload";
            this.clmOffload.ReadOnly = true;
            this.clmOffload.Width = 60;
            // 
            // clmActStock
            // 
            this.clmActStock.HeaderText = "ActStock";
            this.clmActStock.MinimumWidth = 10;
            this.clmActStock.Name = "clmActStock";
            this.clmActStock.ReadOnly = true;
            this.clmActStock.Width = 60;
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "Remove";
            this.clmDelete.MinimumWidth = 10;
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.ReadOnly = true;
            this.clmDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmDelete.Width = 200;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.linkReport);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 957);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2104, 156);
            this.panel3.TabIndex = 1;
            // 
            // linkReport
            // 
            this.linkReport.AutoSize = true;
            this.linkReport.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkReport.Location = new System.Drawing.Point(21, 64);
            this.linkReport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkReport.Name = "linkReport";
            this.linkReport.Size = new System.Drawing.Size(102, 40);
            this.linkReport.TabIndex = 30;
            this.linkReport.TabStop = true;
            this.linkReport.Text = "Report";
            this.linkReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkReport_LinkClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(558, 49);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(160, 73);
            this.btnPrint.TabIndex = 29;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1918, 49);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 73);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1748, 49);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 73);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1580, 49);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 73);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2104, 461);
            this.panel2.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel5.Controls.Add(this.lblPrvBalance);
            this.panel5.Controls.Add(this.txtTotalEmpty);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.txtTotalLoading);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.txtRemarks);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.txtActualStock);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.txtEmpty);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.txtNewStock);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.txtNewLoad);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.txtBalance);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.txtTotalExtra);
            this.panel5.Controls.Add(this.txtExtra);
            this.panel5.Controls.Add(this.txtTotalShort);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.txtShort);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.txtOffload);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.txtDamage);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.txtOldStock);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.cmbProductName);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 162);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(2104, 299);
            this.panel5.TabIndex = 36;
            // 
            // lblPrvBalance
            // 
            this.lblPrvBalance.AutoSize = true;
            this.lblPrvBalance.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblPrvBalance.Location = new System.Drawing.Point(1320, 5);
            this.lblPrvBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrvBalance.Name = "lblPrvBalance";
            this.lblPrvBalance.Size = new System.Drawing.Size(22, 25);
            this.lblPrvBalance.TabIndex = 41;
            this.lblPrvBalance.Text = "0";
            // 
            // txtTotalEmpty
            // 
            this.txtTotalEmpty.BackColor = System.Drawing.Color.White;
            this.txtTotalEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalEmpty.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalEmpty.Location = new System.Drawing.Point(646, 197);
            this.txtTotalEmpty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalEmpty.MaxLength = 50;
            this.txtTotalEmpty.Name = "txtTotalEmpty";
            this.txtTotalEmpty.ReadOnly = true;
            this.txtTotalEmpty.Size = new System.Drawing.Size(146, 46);
            this.txtTotalEmpty.TabIndex = 39;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label17.Location = new System.Drawing.Point(639, 150);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(145, 40);
            this.label17.TabIndex = 40;
            this.label17.Text = "Tot Empty";
            // 
            // txtTotalLoading
            // 
            this.txtTotalLoading.BackColor = System.Drawing.Color.White;
            this.txtTotalLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalLoading.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTotalLoading.Location = new System.Drawing.Point(654, 73);
            this.txtTotalLoading.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalLoading.MaxLength = 50;
            this.txtTotalLoading.Name = "txtTotalLoading";
            this.txtTotalLoading.ReadOnly = true;
            this.txtTotalLoading.Size = new System.Drawing.Size(146, 46);
            this.txtTotalLoading.TabIndex = 37;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label16.Location = new System.Drawing.Point(646, 28);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 40);
            this.label16.TabIndex = 38;
            this.label16.Text = "Total Load";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRemarks.Location = new System.Drawing.Point(26, 197);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRemarks.MaxLength = 50;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(605, 46);
            this.txtRemarks.TabIndex = 35;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label15.Location = new System.Drawing.Point(18, 155);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(124, 40);
            this.label15.TabIndex = 36;
            this.label15.Text = "Remarks";
            // 
            // txtActualStock
            // 
            this.txtActualStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActualStock.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtActualStock.Location = new System.Drawing.Point(1431, 197);
            this.txtActualStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtActualStock.MaxLength = 50;
            this.txtActualStock.Name = "txtActualStock";
            this.txtActualStock.Size = new System.Drawing.Size(146, 46);
            this.txtActualStock.TabIndex = 10;
            this.txtActualStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label12.Location = new System.Drawing.Point(1424, 152);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 40);
            this.label12.TabIndex = 33;
            this.label12.Text = "Act Stock";
            // 
            // txtEmpty
            // 
            this.txtEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpty.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtEmpty.Location = new System.Drawing.Point(966, 75);
            this.txtEmpty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEmpty.MaxLength = 50;
            this.txtEmpty.Name = "txtEmpty";
            this.txtEmpty.Size = new System.Drawing.Size(146, 46);
            this.txtEmpty.TabIndex = 2;
            this.txtEmpty.TextChanged += new System.EventHandler(this.txtOldStock_TextChanged);
            this.txtEmpty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label11.Location = new System.Drawing.Point(958, 30);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 40);
            this.label11.TabIndex = 31;
            this.label11.Text = "Empty";
            // 
            // txtNewStock
            // 
            this.txtNewStock.BackColor = System.Drawing.Color.White;
            this.txtNewStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewStock.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNewStock.Location = new System.Drawing.Point(1116, 197);
            this.txtNewStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNewStock.MaxLength = 50;
            this.txtNewStock.Name = "txtNewStock";
            this.txtNewStock.ReadOnly = true;
            this.txtNewStock.Size = new System.Drawing.Size(146, 46);
            this.txtNewStock.TabIndex = 8;
            this.txtNewStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label10.Location = new System.Drawing.Point(1108, 152);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(151, 40);
            this.label10.TabIndex = 29;
            this.label10.Text = "New Stock";
            // 
            // txtNewLoad
            // 
            this.txtNewLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewLoad.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNewLoad.Location = new System.Drawing.Point(1442, 77);
            this.txtNewLoad.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNewLoad.MaxLength = 50;
            this.txtNewLoad.Name = "txtNewLoad";
            this.txtNewLoad.Size = new System.Drawing.Size(146, 46);
            this.txtNewLoad.TabIndex = 5;
            this.txtNewLoad.TextChanged += new System.EventHandler(this.txtOldStock_TextChanged);
            this.txtNewLoad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(1434, 30);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 40);
            this.label9.TabIndex = 27;
            this.label9.Text = "New Load";
            // 
            // txtBalance
            // 
            this.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalance.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBalance.Location = new System.Drawing.Point(1282, 77);
            this.txtBalance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBalance.MaxLength = 50;
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(146, 46);
            this.txtBalance.TabIndex = 4;
            this.txtBalance.TextChanged += new System.EventHandler(this.txtOldStock_TextChanged);
            this.txtBalance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(1275, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 40);
            this.label7.TabIndex = 25;
            this.label7.Text = "Balance";
            // 
            // txtTotalExtra
            // 
            this.txtTotalExtra.BackColor = System.Drawing.Color.White;
            this.txtTotalExtra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalExtra.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtTotalExtra.Location = new System.Drawing.Point(960, 250);
            this.txtTotalExtra.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalExtra.MaxLength = 50;
            this.txtTotalExtra.Name = "txtTotalExtra";
            this.txtTotalExtra.ReadOnly = true;
            this.txtTotalExtra.Size = new System.Drawing.Size(146, 36);
            this.txtTotalExtra.TabIndex = 7;
            this.txtTotalExtra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtExtra
            // 
            this.txtExtra.BackColor = System.Drawing.Color.White;
            this.txtExtra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExtra.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtExtra.Location = new System.Drawing.Point(960, 197);
            this.txtExtra.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtExtra.MaxLength = 50;
            this.txtExtra.Name = "txtExtra";
            this.txtExtra.ReadOnly = true;
            this.txtExtra.Size = new System.Drawing.Size(146, 46);
            this.txtExtra.TabIndex = 7;
            this.txtExtra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtTotalShort
            // 
            this.txtTotalShort.BackColor = System.Drawing.Color.White;
            this.txtTotalShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalShort.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtTotalShort.Location = new System.Drawing.Point(800, 250);
            this.txtTotalShort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTotalShort.MaxLength = 50;
            this.txtTotalShort.Name = "txtTotalShort";
            this.txtTotalShort.ReadOnly = true;
            this.txtTotalShort.Size = new System.Drawing.Size(146, 36);
            this.txtTotalShort.TabIndex = 6;
            this.txtTotalShort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label14.Location = new System.Drawing.Point(952, 150);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 40);
            this.label14.TabIndex = 23;
            this.label14.Text = "Extra";
            // 
            // txtShort
            // 
            this.txtShort.BackColor = System.Drawing.Color.White;
            this.txtShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShort.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtShort.Location = new System.Drawing.Point(800, 197);
            this.txtShort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtShort.MaxLength = 50;
            this.txtShort.Name = "txtShort";
            this.txtShort.ReadOnly = true;
            this.txtShort.Size = new System.Drawing.Size(146, 46);
            this.txtShort.TabIndex = 6;
            this.txtShort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label13.Location = new System.Drawing.Point(792, 150);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 40);
            this.label13.TabIndex = 23;
            this.label13.Text = "Short";
            // 
            // txtOffload
            // 
            this.txtOffload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOffload.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtOffload.Location = new System.Drawing.Point(1272, 197);
            this.txtOffload.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOffload.MaxLength = 50;
            this.txtOffload.Name = "txtOffload";
            this.txtOffload.Size = new System.Drawing.Size(146, 46);
            this.txtOffload.TabIndex = 9;
            this.txtOffload.TextChanged += new System.EventHandler(this.txtOldStock_TextChanged);
            this.txtOffload.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(1264, 150);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 40);
            this.label5.TabIndex = 23;
            this.label5.Text = "Offload";
            // 
            // txtDamage
            // 
            this.txtDamage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDamage.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDamage.Location = new System.Drawing.Point(1126, 75);
            this.txtDamage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDamage.MaxLength = 50;
            this.txtDamage.Name = "txtDamage";
            this.txtDamage.Size = new System.Drawing.Size(146, 46);
            this.txtDamage.TabIndex = 3;
            this.txtDamage.TextChanged += new System.EventHandler(this.txtOldStock_TextChanged);
            this.txtDamage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(1119, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 40);
            this.label1.TabIndex = 23;
            this.label1.Text = "Damage";
            // 
            // txtOldStock
            // 
            this.txtOldStock.BackColor = System.Drawing.Color.White;
            this.txtOldStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldStock.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtOldStock.Location = new System.Drawing.Point(810, 75);
            this.txtOldStock.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOldStock.MaxLength = 50;
            this.txtOldStock.Name = "txtOldStock";
            this.txtOldStock.Size = new System.Drawing.Size(146, 46);
            this.txtOldStock.TabIndex = 1;
            this.txtOldStock.TextChanged += new System.EventHandler(this.txtOldStock_TextChanged);
            this.txtOldStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(802, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 40);
            this.label3.TabIndex = 21;
            this.label3.Text = "Last Load";
            // 
            // cmbProductName
            // 
            this.cmbProductName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(26, 73);
            this.cmbProductName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(612, 48);
            this.cmbProductName.TabIndex = 0;
            this.cmbProductName.SelectedIndexChanged += new System.EventHandler(this.cmbProductName_SelectedIndexChanged);
            this.cmbProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 40);
            this.label2.TabIndex = 19;
            this.label2.Text = "Item Name";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtHelper);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.dtpDeliveryDate);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.cmbEmployee);
            this.panel4.Controls.Add(this.txtVehicle);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(2104, 162);
            this.panel4.TabIndex = 35;
            // 
            // txtHelper
            // 
            this.txtHelper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHelper.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHelper.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtHelper.Location = new System.Drawing.Point(1206, 78);
            this.txtHelper.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtHelper.MaxLength = 50;
            this.txtHelper.Name = "txtHelper";
            this.txtHelper.Size = new System.Drawing.Size(298, 46);
            this.txtHelper.TabIndex = 3;
            this.txtHelper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label24.Location = new System.Drawing.Point(1198, 34);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(102, 40);
            this.label24.TabIndex = 34;
            this.label24.Text = "Helper";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(68, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 40);
            this.label4.TabIndex = 24;
            this.label4.Text = "Date and Time";
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpDeliveryDate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(75, 73);
            this.dtpDeliveryDate.Margin = new System.Windows.Forms.Padding(6);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(368, 46);
            this.dtpDeliveryDate.TabIndex = 0;
            this.dtpDeliveryDate.ValueChanged += new System.EventHandler(this.dtpDeliveryDate_ValueChanged);
            this.dtpDeliveryDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(974, 34);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 40);
            this.label8.TabIndex = 28;
            this.label8.Text = "Vehicle";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(498, 77);
            this.cmbEmployee.Margin = new System.Windows.Forms.Padding(6);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(454, 48);
            this.cmbEmployee.TabIndex = 1;
            this.cmbEmployee.SelectedIndexChanged += new System.EventHandler(this.cmbEmployee_SelectedIndexChanged);
            this.cmbEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtVehicle
            // 
            this.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVehicle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtVehicle.Location = new System.Drawing.Point(981, 78);
            this.txtVehicle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtVehicle.MaxLength = 50;
            this.txtVehicle.Name = "txtVehicle";
            this.txtVehicle.Size = new System.Drawing.Size(215, 46);
            this.txtVehicle.TabIndex = 2;
            this.txtVehicle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(490, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 40);
            this.label6.TabIndex = 26;
            this.label6.Text = "Employee";
            // 
            // LoadManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2104, 1113);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LoadManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Loading";
            this.Text = "Load Management";
            this.Load += new System.EventHandler(this.LoadManagementForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDelivery)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpDeliveryDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtVehicle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtHelper;
        private System.Windows.Forms.DataGridView dgDelivery;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOldStock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDamage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOffload;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNewLoad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNewStock;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEmpty;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtActualStock;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtShort;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtExtra;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtTotalLoading;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtTotalEmpty;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTotalExtra;
        private System.Windows.Forms.TextBox txtTotalShort;
        private System.Windows.Forms.LinkLabel linkReport;
        private System.Windows.Forms.Label lblPrvBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLoadId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOldStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDamage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmExtra;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOffload;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmActStock;
        private System.Windows.Forms.DataGridViewLinkColumn clmDelete;
    }
}