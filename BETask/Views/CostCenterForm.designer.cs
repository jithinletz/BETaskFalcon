namespace BETask.Views
{
    partial class CostCenterForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlReport = new System.Windows.Forms.Panel();
            this.gridReport = new System.Windows.Forms.DataGridView();
            this.clmLedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkReport = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlSaveConent = new System.Windows.Forms.Panel();
            this.lnkChangeParent = new System.Windows.Forms.LinkLabel();
            this.rdbSub = new System.Windows.Forms.RadioButton();
            this.rdbPrimary = new System.Windows.Forms.RadioButton();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtCostCenterName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCostCenterId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtParentCostCenterId = new System.Windows.Forms.TextBox();
            this.txtParentCostCenterName = new System.Windows.Forms.TextBox();
            this.trvContent = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlSaveConent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlReport);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.trvContent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1419, 919);
            this.panel1.TabIndex = 0;
            // 
            // pnlReport
            // 
            this.pnlReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlReport.Controls.Add(this.gridReport);
            this.pnlReport.Controls.Add(this.panel5);
            this.pnlReport.Location = new System.Drawing.Point(18, 64);
            this.pnlReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new System.Drawing.Size(201, 223);
            this.pnlReport.TabIndex = 2;
            this.pnlReport.Visible = false;
            // 
            // gridReport
            // 
            this.gridReport.AllowUserToAddRows = false;
            this.gridReport.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.gridReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridReport.BackgroundColor = System.Drawing.Color.White;
            this.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmLedgerName,
            this.clmGroupName});
            this.gridReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReport.Location = new System.Drawing.Point(0, 55);
            this.gridReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridReport.Name = "gridReport";
            this.gridReport.ReadOnly = true;
            this.gridReport.RowHeadersWidth = 82;
            this.gridReport.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridReport.RowTemplate.Height = 30;
            this.gridReport.Size = new System.Drawing.Size(201, 168);
            this.gridReport.TabIndex = 1;
            // 
            // clmLedgerName
            // 
            this.clmLedgerName.HeaderText = "Ledger Name";
            this.clmLedgerName.MinimumWidth = 10;
            this.clmLedgerName.Name = "clmLedgerName";
            this.clmLedgerName.ReadOnly = true;
            this.clmLedgerName.Width = 400;
            // 
            // clmGroupName
            // 
            this.clmGroupName.HeaderText = "Group Name";
            this.clmGroupName.MinimumWidth = 10;
            this.clmGroupName.Name = "clmGroupName";
            this.clmGroupName.ReadOnly = true;
            this.clmGroupName.Width = 400;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(201, 55);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(1214, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Close X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pnlSaveConent);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(636, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 919);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.linkReport);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnNew);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 750);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(783, 169);
            this.panel3.TabIndex = 12;
            // 
            // linkReport
            // 
            this.linkReport.AutoSize = true;
            this.linkReport.LinkColor = System.Drawing.Color.White;
            this.linkReport.Location = new System.Drawing.Point(586, 16);
            this.linkReport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkReport.Name = "linkReport";
            this.linkReport.Size = new System.Drawing.Size(161, 25);
            this.linkReport.TabIndex = 7;
            this.linkReport.TabStop = true;
            this.linkReport.Text = "View All Ledger";
            this.linkReport.Visible = false;
            this.linkReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkReport_LinkClicked);
            this.linkReport.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(603, 66);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(159, 73);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(435, 66);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(159, 73);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Canc&el";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.Enabled = false;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(267, 66);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(159, 73);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.Green;
            this.btnNew.Enabled = false;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(99, 66);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(159, 73);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // pnlSaveConent
            // 
            this.pnlSaveConent.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSaveConent.Controls.Add(this.lnkChangeParent);
            this.pnlSaveConent.Controls.Add(this.rdbSub);
            this.pnlSaveConent.Controls.Add(this.rdbPrimary);
            this.pnlSaveConent.Controls.Add(this.chkActive);
            this.pnlSaveConent.Controls.Add(this.txtCostCenterName);
            this.pnlSaveConent.Controls.Add(this.txtDescription);
            this.pnlSaveConent.Controls.Add(this.label3);
            this.pnlSaveConent.Controls.Add(this.txtCostCenterId);
            this.pnlSaveConent.Controls.Add(this.label2);
            this.pnlSaveConent.Controls.Add(this.label4);
            this.pnlSaveConent.Controls.Add(this.txtParentCostCenterId);
            this.pnlSaveConent.Controls.Add(this.txtParentCostCenterName);
            this.pnlSaveConent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveConent.Enabled = false;
            this.pnlSaveConent.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveConent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSaveConent.Name = "pnlSaveConent";
            this.pnlSaveConent.Size = new System.Drawing.Size(783, 919);
            this.pnlSaveConent.TabIndex = 11;
            // 
            // lnkChangeParent
            // 
            this.lnkChangeParent.AutoSize = true;
            this.lnkChangeParent.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lnkChangeParent.Location = new System.Drawing.Point(51, 664);
            this.lnkChangeParent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkChangeParent.Name = "lnkChangeParent";
            this.lnkChangeParent.Size = new System.Drawing.Size(237, 40);
            this.lnkChangeParent.TabIndex = 9;
            this.lnkChangeParent.TabStop = true;
            this.lnkChangeParent.Text = "Change Parent of";
            this.lnkChangeParent.Visible = false;
            this.lnkChangeParent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChangeParent_LinkClicked);
            // 
            // rdbSub
            // 
            this.rdbSub.AutoSize = true;
            this.rdbSub.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdbSub.Location = new System.Drawing.Point(390, 119);
            this.rdbSub.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdbSub.Name = "rdbSub";
            this.rdbSub.Size = new System.Drawing.Size(243, 44);
            this.rdbSub.TabIndex = 7;
            this.rdbSub.Text = "Sub cost center";
            this.rdbSub.UseVisualStyleBackColor = true;
            this.rdbSub.CheckedChanged += new System.EventHandler(this.rdbSub_CheckedChanged);
            // 
            // rdbPrimary
            // 
            this.rdbPrimary.AutoSize = true;
            this.rdbPrimary.Checked = true;
            this.rdbPrimary.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.rdbPrimary.Location = new System.Drawing.Point(226, 119);
            this.rdbPrimary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdbPrimary.Name = "rdbPrimary";
            this.rdbPrimary.Size = new System.Drawing.Size(145, 44);
            this.rdbPrimary.TabIndex = 7;
            this.rdbPrimary.TabStop = true;
            this.rdbPrimary.Text = "Primary";
            this.rdbPrimary.UseVisualStyleBackColor = true;
            this.rdbPrimary.CheckedChanged += new System.EventHandler(this.rdbPrimary_CheckedChanged);
            // 
            // chkActive
            // 
            this.chkActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.Location = new System.Drawing.Point(616, 516);
            this.chkActive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(127, 44);
            this.chkActive.TabIndex = 6;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // txtCostCenterName
            // 
            this.txtCostCenterName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCostCenterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostCenterName.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostCenterName.Location = new System.Drawing.Point(362, 195);
            this.txtCostCenterName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCostCenterName.MaxLength = 50;
            this.txtCostCenterName.Name = "txtCostCenterName";
            this.txtCostCenterName.Size = new System.Drawing.Size(383, 46);
            this.txtCostCenterName.TabIndex = 0;
            this.txtCostCenterName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(243, 283);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(501, 46);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 283);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 40);
            this.label3.TabIndex = 1;
            this.label3.Text = "Description";
            // 
            // txtCostCenterId
            // 
            this.txtCostCenterId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCostCenterId.BackColor = System.Drawing.SystemColors.Control;
            this.txtCostCenterId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostCenterId.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostCenterId.Location = new System.Drawing.Point(243, 195);
            this.txtCostCenterId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCostCenterId.Name = "txtCostCenterId";
            this.txtCostCenterId.ReadOnly = true;
            this.txtCostCenterId.Size = new System.Drawing.Size(114, 46);
            this.txtCostCenterId.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cost Center";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 362);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 40);
            this.label4.TabIndex = 5;
            this.label4.Text = "Under";
            // 
            // txtParentCostCenterId
            // 
            this.txtParentCostCenterId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParentCostCenterId.BackColor = System.Drawing.SystemColors.Control;
            this.txtParentCostCenterId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParentCostCenterId.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParentCostCenterId.Location = new System.Drawing.Point(243, 362);
            this.txtParentCostCenterId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtParentCostCenterId.Name = "txtParentCostCenterId";
            this.txtParentCostCenterId.ReadOnly = true;
            this.txtParentCostCenterId.Size = new System.Drawing.Size(114, 46);
            this.txtParentCostCenterId.TabIndex = 4;
            // 
            // txtParentCostCenterName
            // 
            this.txtParentCostCenterName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParentCostCenterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParentCostCenterName.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParentCostCenterName.Location = new System.Drawing.Point(362, 362);
            this.txtParentCostCenterName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtParentCostCenterName.MaxLength = 100;
            this.txtParentCostCenterName.Name = "txtParentCostCenterName";
            this.txtParentCostCenterName.ReadOnly = true;
            this.txtParentCostCenterName.Size = new System.Drawing.Size(383, 46);
            this.txtParentCostCenterName.TabIndex = 4;
            // 
            // trvContent
            // 
            this.trvContent.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvContent.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvContent.Location = new System.Drawing.Point(0, 0);
            this.trvContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trvContent.Name = "trvContent";
            this.trvContent.Size = new System.Drawing.Size(636, 919);
            this.trvContent.TabIndex = 0;
            this.trvContent.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvContent_NodeMouseClick);
            // 
            // CostCenterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 919);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CostCenterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cost Center";
            this.Load += new System.EventHandler(this.CostCenterForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlSaveConent.ResumeLayout(false);
            this.pnlSaveConent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtParentCostCenterName;
        private System.Windows.Forms.TextBox txtParentCostCenterId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCostCenterId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCostCenterName;
        private System.Windows.Forms.TreeView trvContent;
        private System.Windows.Forms.Panel pnlSaveConent;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel linkReport;
        private System.Windows.Forms.Panel pnlReport;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmGroupName;
        private System.Windows.Forms.RadioButton rdbPrimary;
        private System.Windows.Forms.RadioButton rdbSub;
        private System.Windows.Forms.LinkLabel lnkChangeParent;
    }
}