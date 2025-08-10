namespace BETask.Views
{
    partial class DeliveryBookForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlRouteChange = new System.Windows.Forms.Panel();
            this.lblBookNo = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpIssued = new System.Windows.Forms.DateTimePicker();
            this.txtLeafEnd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBookNo = new System.Windows.Forms.TextBox();
            this.txtLeafStart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.gridLeaf = new System.Windows.Forms.DataGridView();
            this.clmBookId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeaf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmReddeemed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDeliveryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDeactivate = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gridDeliveryBooks = new System.Windows.Forms.DataGridView();
            this.clmBookNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIssueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeafFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLeafTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRedeemed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTransfer = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNewBook = new System.Windows.Forms.Button();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtLeafSearch = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridEmployee = new System.Windows.Forms.DataGridView();
            this.ClmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRouteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlRouteChange.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaf)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveryBooks)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1269, 714);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlRouteChange);
            this.panel3.Controls.Add(this.pnlSave);
            this.panel3.Controls.Add(this.gridDeliveryBooks);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(500, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(769, 714);
            this.panel3.TabIndex = 2;
            // 
            // pnlRouteChange
            // 
            this.pnlRouteChange.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlRouteChange.Controls.Add(this.lblBookNo);
            this.pnlRouteChange.Controls.Add(this.label18);
            this.pnlRouteChange.Controls.Add(this.cmbRoute);
            this.pnlRouteChange.Location = new System.Drawing.Point(131, 101);
            this.pnlRouteChange.Name = "pnlRouteChange";
            this.pnlRouteChange.Size = new System.Drawing.Size(415, 145);
            this.pnlRouteChange.TabIndex = 3;
            this.pnlRouteChange.Visible = false;
            // 
            // lblBookNo
            // 
            this.lblBookNo.AutoSize = true;
            this.lblBookNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblBookNo.Location = new System.Drawing.Point(4, 9);
            this.lblBookNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBookNo.Name = "lblBookNo";
            this.lblBookNo.Size = new System.Drawing.Size(0, 25);
            this.lblBookNo.TabIndex = 86;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(14, 26);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(158, 25);
            this.label18.TabIndex = 85;
            this.label18.Text = "Select New Route";
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(19, 64);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(348, 33);
            this.cmbRoute.TabIndex = 84;
            this.cmbRoute.SelectedIndexChanged += new System.EventHandler(this.cmbRoute_SelectedIndexChanged);
            // 
            // pnlSave
            // 
            this.pnlSave.Controls.Add(this.panel6);
            this.pnlSave.Controls.Add(this.gridLeaf);
            this.pnlSave.Controls.Add(this.panel5);
            this.pnlSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSave.Enabled = false;
            this.pnlSave.Location = new System.Drawing.Point(0, 267);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(769, 447);
            this.pnlSave.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnAdd);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.dtpIssued);
            this.panel6.Controls.Add(this.txtLeafEnd);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.txtBookNo);
            this.panel6.Controls.Add(this.txtLeafStart);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.txtPrefix);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(769, 139);
            this.panel6.TabIndex = 30;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.Green;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(641, 73);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 29);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Issued Date";
            // 
            // dtpIssued
            // 
            this.dtpIssued.CustomFormat = "dd/MM/yyyy";
            this.dtpIssued.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpIssued.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIssued.Location = new System.Drawing.Point(120, 19);
            this.dtpIssued.Margin = new System.Windows.Forms.Padding(4);
            this.dtpIssued.Name = "dtpIssued";
            this.dtpIssued.Size = new System.Drawing.Size(193, 31);
            this.dtpIssued.TabIndex = 0;
            this.dtpIssued.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtLeafEnd
            // 
            this.txtLeafEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeafEnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLeafEnd.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLeafEnd.Location = new System.Drawing.Point(427, 71);
            this.txtLeafEnd.Margin = new System.Windows.Forms.Padding(4);
            this.txtLeafEnd.Name = "txtLeafEnd";
            this.txtLeafEnd.Size = new System.Drawing.Size(151, 31);
            this.txtLeafEnd.TabIndex = 4;
            this.txtLeafEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtLeafEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(320, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "Book No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(320, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 25);
            this.label4.TabIndex = 26;
            this.label4.Text = "Leaf From";
            // 
            // txtBookNo
            // 
            this.txtBookNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBookNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBookNo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBookNo.Location = new System.Drawing.Point(427, 19);
            this.txtBookNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtBookNo.Name = "txtBookNo";
            this.txtBookNo.Size = new System.Drawing.Size(194, 31);
            this.txtBookNo.TabIndex = 1;
            this.txtBookNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtLeafStart
            // 
            this.txtLeafStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeafStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLeafStart.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLeafStart.Location = new System.Drawing.Point(168, 71);
            this.txtLeafStart.Margin = new System.Windows.Forms.Padding(4);
            this.txtLeafStart.Name = "txtLeafStart";
            this.txtLeafStart.Size = new System.Drawing.Size(145, 31);
            this.txtLeafStart.TabIndex = 3;
            this.txtLeafStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtLeafStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(4, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 25);
            this.label3.TabIndex = 23;
            this.label3.Text = "Leaf From";
            // 
            // txtPrefix
            // 
            this.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrefix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrefix.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPrefix.Location = new System.Drawing.Point(120, 71);
            this.txtPrefix.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(40, 31);
            this.txtPrefix.TabIndex = 2;
            this.txtPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // gridLeaf
            // 
            this.gridLeaf.AllowUserToAddRows = false;
            this.gridLeaf.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.gridLeaf.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLeaf.BackgroundColor = System.Drawing.Color.White;
            this.gridLeaf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLeaf.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmBookId,
            this.clmLeaf,
            this.clmReddeemed,
            this.clmDeliveryId,
            this.clmDeactivate,
            this.clmCustomer});
            this.gridLeaf.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridLeaf.Location = new System.Drawing.Point(0, 139);
            this.gridLeaf.Margin = new System.Windows.Forms.Padding(4);
            this.gridLeaf.Name = "gridLeaf";
            this.gridLeaf.ReadOnly = true;
            this.gridLeaf.RowHeadersWidth = 50;
            this.gridLeaf.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridLeaf.RowTemplate.Height = 30;
            this.gridLeaf.Size = new System.Drawing.Size(769, 247);
            this.gridLeaf.TabIndex = 29;
            this.gridLeaf.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLeaf_CellClick);
            // 
            // clmBookId
            // 
            this.clmBookId.HeaderText = "BookId";
            this.clmBookId.MinimumWidth = 6;
            this.clmBookId.Name = "clmBookId";
            this.clmBookId.ReadOnly = true;
            this.clmBookId.Visible = false;
            this.clmBookId.Width = 125;
            // 
            // clmLeaf
            // 
            this.clmLeaf.HeaderText = "Leaf Number";
            this.clmLeaf.MinimumWidth = 6;
            this.clmLeaf.Name = "clmLeaf";
            this.clmLeaf.ReadOnly = true;
            this.clmLeaf.Width = 150;
            // 
            // clmReddeemed
            // 
            this.clmReddeemed.HeaderText = "Redeemed";
            this.clmReddeemed.MinimumWidth = 6;
            this.clmReddeemed.Name = "clmReddeemed";
            this.clmReddeemed.ReadOnly = true;
            this.clmReddeemed.Width = 150;
            // 
            // clmDeliveryId
            // 
            this.clmDeliveryId.HeaderText = "Delivery Id";
            this.clmDeliveryId.MinimumWidth = 6;
            this.clmDeliveryId.Name = "clmDeliveryId";
            this.clmDeliveryId.ReadOnly = true;
            this.clmDeliveryId.Visible = false;
            this.clmDeliveryId.Width = 125;
            // 
            // clmDeactivate
            // 
            this.clmDeactivate.HeaderText = "De Activate";
            this.clmDeactivate.MinimumWidth = 6;
            this.clmDeactivate.Name = "clmDeactivate";
            this.clmDeactivate.ReadOnly = true;
            this.clmDeactivate.Width = 125;
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.MinimumWidth = 6;
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Visible = false;
            this.clmCustomer.Width = 125;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnCancel);
            this.panel5.Controls.Add(this.btnSave);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 386);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(769, 61);
            this.panel5.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(433, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(659, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(546, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // gridDeliveryBooks
            // 
            this.gridDeliveryBooks.AllowUserToAddRows = false;
            this.gridDeliveryBooks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.gridDeliveryBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridDeliveryBooks.BackgroundColor = System.Drawing.Color.White;
            this.gridDeliveryBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDeliveryBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmBookNo,
            this.clmIssueDate,
            this.clmLeafFrom,
            this.clmLeafTo,
            this.clmRedeemed,
            this.clmBalance,
            this.clmTransfer});
            this.gridDeliveryBooks.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridDeliveryBooks.Location = new System.Drawing.Point(0, 56);
            this.gridDeliveryBooks.Margin = new System.Windows.Forms.Padding(4);
            this.gridDeliveryBooks.Name = "gridDeliveryBooks";
            this.gridDeliveryBooks.ReadOnly = true;
            this.gridDeliveryBooks.RowHeadersVisible = false;
            this.gridDeliveryBooks.RowHeadersWidth = 51;
            this.gridDeliveryBooks.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridDeliveryBooks.RowTemplate.Height = 30;
            this.gridDeliveryBooks.Size = new System.Drawing.Size(769, 211);
            this.gridDeliveryBooks.TabIndex = 1;
            this.gridDeliveryBooks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDeliveryBooks_CellClick);
            // 
            // clmBookNo
            // 
            this.clmBookNo.HeaderText = "Book";
            this.clmBookNo.MinimumWidth = 6;
            this.clmBookNo.Name = "clmBookNo";
            this.clmBookNo.ReadOnly = true;
            this.clmBookNo.Width = 125;
            // 
            // clmIssueDate
            // 
            this.clmIssueDate.HeaderText = "Issued";
            this.clmIssueDate.MinimumWidth = 6;
            this.clmIssueDate.Name = "clmIssueDate";
            this.clmIssueDate.ReadOnly = true;
            this.clmIssueDate.Width = 125;
            // 
            // clmLeafFrom
            // 
            this.clmLeafFrom.HeaderText = "From";
            this.clmLeafFrom.MinimumWidth = 6;
            this.clmLeafFrom.Name = "clmLeafFrom";
            this.clmLeafFrom.ReadOnly = true;
            this.clmLeafFrom.Width = 80;
            // 
            // clmLeafTo
            // 
            this.clmLeafTo.HeaderText = "To";
            this.clmLeafTo.MinimumWidth = 6;
            this.clmLeafTo.Name = "clmLeafTo";
            this.clmLeafTo.ReadOnly = true;
            this.clmLeafTo.Width = 80;
            // 
            // clmRedeemed
            // 
            this.clmRedeemed.HeaderText = "Redeemed";
            this.clmRedeemed.MinimumWidth = 6;
            this.clmRedeemed.Name = "clmRedeemed";
            this.clmRedeemed.ReadOnly = true;
            this.clmRedeemed.Width = 80;
            // 
            // clmBalance
            // 
            this.clmBalance.HeaderText = "Balance";
            this.clmBalance.MinimumWidth = 6;
            this.clmBalance.Name = "clmBalance";
            this.clmBalance.ReadOnly = true;
            this.clmBalance.Width = 80;
            // 
            // clmTransfer
            // 
            this.clmTransfer.HeaderText = "Transfer";
            this.clmTransfer.MinimumWidth = 6;
            this.clmTransfer.Name = "clmTransfer";
            this.clmTransfer.ReadOnly = true;
            this.clmTransfer.Width = 125;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.btnSearch);
            this.panel4.Controls.Add(this.btnNewBook);
            this.panel4.Controls.Add(this.lblCustomerName);
            this.panel4.Controls.Add(this.txtLeafSearch);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(769, 56);
            this.panel4.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(216, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 25);
            this.label5.TabIndex = 22;
            this.label5.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(522, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 39);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnNewBook
            // 
            this.btnNewBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewBook.BackColor = System.Drawing.Color.Green;
            this.btnNewBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBook.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewBook.ForeColor = System.Drawing.Color.White;
            this.btnNewBook.Location = new System.Drawing.Point(635, 11);
            this.btnNewBook.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewBook.Name = "btnNewBook";
            this.btnNewBook.Size = new System.Drawing.Size(121, 39);
            this.btnNewBook.TabIndex = 29;
            this.btnNewBook.Text = "New Book";
            this.btnNewBook.UseVisualStyleBackColor = false;
            this.btnNewBook.Visible = false;
            this.btnNewBook.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCustomerName.Location = new System.Drawing.Point(3, 18);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(63, 25);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "label1";
            // 
            // txtLeafSearch
            // 
            this.txtLeafSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeafSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLeafSearch.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLeafSearch.Location = new System.Drawing.Point(292, 16);
            this.txtLeafSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtLeafSearch.Name = "txtLeafSearch";
            this.txtLeafSearch.Size = new System.Drawing.Size(194, 31);
            this.txtLeafSearch.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridEmployee);
            this.panel2.Controls.Add(this.txtCusName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 714);
            this.panel2.TabIndex = 1;
            // 
            // gridEmployee
            // 
            this.gridEmployee.AllowUserToAddRows = false;
            this.gridEmployee.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.gridEmployee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridEmployee.BackgroundColor = System.Drawing.Color.White;
            this.gridEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClmName,
            this.clmDRoute,
            this.clmRouteId,
            this.ClmCustomerId});
            this.gridEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmployee.Location = new System.Drawing.Point(0, 31);
            this.gridEmployee.Margin = new System.Windows.Forms.Padding(4);
            this.gridEmployee.Name = "gridEmployee";
            this.gridEmployee.ReadOnly = true;
            this.gridEmployee.RowHeadersVisible = false;
            this.gridEmployee.RowHeadersWidth = 51;
            this.gridEmployee.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridEmployee.RowTemplate.Height = 30;
            this.gridEmployee.Size = new System.Drawing.Size(500, 683);
            this.gridEmployee.TabIndex = 0;
            this.gridEmployee.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCustomers_CellClick);
            // 
            // ClmName
            // 
            this.ClmName.HeaderText = "Name";
            this.ClmName.MinimumWidth = 6;
            this.ClmName.Name = "ClmName";
            this.ClmName.ReadOnly = true;
            this.ClmName.Width = 150;
            // 
            // clmDRoute
            // 
            this.clmDRoute.HeaderText = "Route";
            this.clmDRoute.MinimumWidth = 6;
            this.clmDRoute.Name = "clmDRoute";
            this.clmDRoute.ReadOnly = true;
            this.clmDRoute.Width = 150;
            // 
            // clmRouteId
            // 
            this.clmRouteId.HeaderText = "RouteId";
            this.clmRouteId.MinimumWidth = 6;
            this.clmRouteId.Name = "clmRouteId";
            this.clmRouteId.ReadOnly = true;
            this.clmRouteId.Visible = false;
            this.clmRouteId.Width = 125;
            // 
            // ClmCustomerId
            // 
            this.ClmCustomerId.HeaderText = "ID";
            this.ClmCustomerId.MinimumWidth = 6;
            this.ClmCustomerId.Name = "ClmCustomerId";
            this.ClmCustomerId.ReadOnly = true;
            this.ClmCustomerId.Visible = false;
            this.ClmCustomerId.Width = 50;
            // 
            // txtCusName
            // 
            this.txtCusName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCusName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCusName.Location = new System.Drawing.Point(0, 0);
            this.txtCusName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(500, 31);
            this.txtCusName.TabIndex = 100;
            this.txtCusName.Tag = "cSearch";
            // 
            // DeliveryBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 714);
            this.Controls.Add(this.panel1);
            this.Name = "DeliveryBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Book - DO customers";
            this.Load += new System.EventHandler(this.DeliveryBookForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlRouteChange.ResumeLayout(false);
            this.pnlRouteChange.PerformLayout();
            this.pnlSave.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaf)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDeliveryBooks)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridEmployee;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView gridDeliveryBooks;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Panel pnlSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DateTimePicker dtpIssued;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBookNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TextBox txtLeafStart;
        private System.Windows.Forms.TextBox txtLeafEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView gridLeaf;
        private System.Windows.Forms.Button btnNewBook;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBookNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIssueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeafTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRedeemed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLeafSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBookId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLeaf;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmReddeemed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeliveryId;
        private System.Windows.Forms.DataGridViewLinkColumn clmDeactivate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.Panel pnlRouteChange;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label lblBookNo;
        private System.Windows.Forms.DataGridViewLinkColumn clmTransfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRouteId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmCustomerId;
    }
}