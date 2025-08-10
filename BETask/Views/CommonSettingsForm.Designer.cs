namespace BETask.Views
{
    partial class CommonSettingsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabCommonSettings = new System.Windows.Forms.TabControl();
            this.tabPageTax = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.gridTaxSett = new System.Windows.Forms.DataGridView();
            this.clmTaxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTaxName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTaxDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSaveTax = new System.Windows.Forms.Button();
            this.btnCancelTax = new System.Windows.Forms.Button();
            this.btnCloseTax = new System.Windows.Forms.Button();
            this.btnNewTax = new System.Windows.Forms.Button();
            this.pnlTaxSaveContent = new System.Windows.Forms.Panel();
            this.chkStatusTax = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTaxValuePercentage = new System.Windows.Forms.TextBox();
            this.txtTaxDescription = new System.Windows.Forms.TextBox();
            this.txtTaxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageUnits = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.gridUOM = new System.Windows.Forms.DataGridView();
            this.clmUOMId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUOMName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUOMDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pnlSaveContentUOM = new System.Windows.Forms.Panel();
            this.chkUOMStatus = new System.Windows.Forms.CheckBox();
            this.txtUOMDescription = new System.Windows.Forms.TextBox();
            this.txtUOM = new System.Windows.Forms.TextBox();
            this.lblUOMDescription = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnSaveUOM = new System.Windows.Forms.Button();
            this.btnCancelUOM = new System.Windows.Forms.Button();
            this.btnCloseUOM = new System.Windows.Forms.Button();
            this.btnNewUOM = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabCommonSettings.SuspendLayout();
            this.tabPageTax.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTaxSett)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTaxSaveContent.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPageUnits.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUOM)).BeginInit();
            this.panel8.SuspendLayout();
            this.pnlSaveContentUOM.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.tabCommonSettings);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 578);
            this.panel1.TabIndex = 0;
            // 
            // tabCommonSettings
            // 
            this.tabCommonSettings.Controls.Add(this.tabPageTax);
            this.tabCommonSettings.Controls.Add(this.tabPageUnits);
            this.tabCommonSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCommonSettings.Location = new System.Drawing.Point(0, 0);
            this.tabCommonSettings.Name = "tabCommonSettings";
            this.tabCommonSettings.SelectedIndex = 0;
            this.tabCommonSettings.Size = new System.Drawing.Size(901, 578);
            this.tabCommonSettings.TabIndex = 0;
            // 
            // tabPageTax
            // 
            this.tabPageTax.Controls.Add(this.panel3);
            this.tabPageTax.Location = new System.Drawing.Point(4, 25);
            this.tabPageTax.Name = "tabPageTax";
            this.tabPageTax.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTax.Size = new System.Drawing.Size(893, 549);
            this.tabPageTax.TabIndex = 0;
            this.tabPageTax.Text = "Tax Settings";
            this.tabPageTax.UseVisualStyleBackColor = true;
            this.tabPageTax.Enter += new System.EventHandler(this.tabPageTax_Enter);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(887, 543);
            this.panel3.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.gridTaxSett);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(479, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(408, 543);
            this.panel6.TabIndex = 2;
            // 
            // gridTaxSett
            // 
            this.gridTaxSett.AllowUserToAddRows = false;
            this.gridTaxSett.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.gridTaxSett.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridTaxSett.BackgroundColor = System.Drawing.Color.White;
            this.gridTaxSett.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTaxSett.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTaxId,
            this.clmTaxName,
            this.clmTaxDesc,
            this.clmTaxValue});
            this.gridTaxSett.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTaxSett.Location = new System.Drawing.Point(0, 0);
            this.gridTaxSett.Name = "gridTaxSett";
            this.gridTaxSett.ReadOnly = true;
            this.gridTaxSett.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridTaxSett.RowTemplate.Height = 30;
            this.gridTaxSett.Size = new System.Drawing.Size(408, 543);
            this.gridTaxSett.TabIndex = 0;
            this.gridTaxSett.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTaxSett_CellClick);
            // 
            // clmTaxId
            // 
            this.clmTaxId.HeaderText = "Tax Id";
            this.clmTaxId.Name = "clmTaxId";
            this.clmTaxId.ReadOnly = true;
            this.clmTaxId.Visible = false;
            // 
            // clmTaxName
            // 
            this.clmTaxName.HeaderText = "Tax Name";
            this.clmTaxName.Name = "clmTaxName";
            this.clmTaxName.ReadOnly = true;
            this.clmTaxName.Width = 200;
            // 
            // clmTaxDesc
            // 
            this.clmTaxDesc.HeaderText = "Tax Description";
            this.clmTaxDesc.Name = "clmTaxDesc";
            this.clmTaxDesc.ReadOnly = true;
            this.clmTaxDesc.Visible = false;
            // 
            // clmTaxValue
            // 
            this.clmTaxValue.HeaderText = "Tax %";
            this.clmTaxValue.Name = "clmTaxValue";
            this.clmTaxValue.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.pnlTaxSaveContent);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(479, 543);
            this.panel5.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel4.Controls.Add(this.btnSaveTax);
            this.panel4.Controls.Add(this.btnCancelTax);
            this.panel4.Controls.Add(this.btnCloseTax);
            this.panel4.Controls.Add(this.btnNewTax);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 451);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(479, 92);
            this.panel4.TabIndex = 1;
            // 
            // btnSaveTax
            // 
            this.btnSaveTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveTax.BackColor = System.Drawing.Color.Teal;
            this.btnSaveTax.Enabled = false;
            this.btnSaveTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTax.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveTax.ForeColor = System.Drawing.Color.White;
            this.btnSaveTax.Location = new System.Drawing.Point(367, 24);
            this.btnSaveTax.Name = "btnSaveTax";
            this.btnSaveTax.Size = new System.Drawing.Size(106, 47);
            this.btnSaveTax.TabIndex = 1;
            this.btnSaveTax.Text = "&Save";
            this.btnSaveTax.UseVisualStyleBackColor = false;
            this.btnSaveTax.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCancelTax
            // 
            this.btnCancelTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelTax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancelTax.Enabled = false;
            this.btnCancelTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTax.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelTax.ForeColor = System.Drawing.Color.White;
            this.btnCancelTax.Location = new System.Drawing.Point(255, 24);
            this.btnCancelTax.Name = "btnCancelTax";
            this.btnCancelTax.Size = new System.Drawing.Size(106, 47);
            this.btnCancelTax.TabIndex = 2;
            this.btnCancelTax.Text = "Canc&el";
            this.btnCancelTax.UseVisualStyleBackColor = false;
            this.btnCancelTax.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCloseTax
            // 
            this.btnCloseTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseTax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCloseTax.Enabled = false;
            this.btnCloseTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseTax.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseTax.ForeColor = System.Drawing.Color.White;
            this.btnCloseTax.Location = new System.Drawing.Point(143, 24);
            this.btnCloseTax.Name = "btnCloseTax";
            this.btnCloseTax.Size = new System.Drawing.Size(106, 47);
            this.btnCloseTax.TabIndex = 3;
            this.btnCloseTax.Text = "&Close";
            this.btnCloseTax.UseVisualStyleBackColor = false;
            this.btnCloseTax.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnNewTax
            // 
            this.btnNewTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTax.BackColor = System.Drawing.Color.Green;
            this.btnNewTax.Enabled = false;
            this.btnNewTax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewTax.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewTax.ForeColor = System.Drawing.Color.White;
            this.btnNewTax.Location = new System.Drawing.Point(31, 24);
            this.btnNewTax.Name = "btnNewTax";
            this.btnNewTax.Size = new System.Drawing.Size(106, 47);
            this.btnNewTax.TabIndex = 0;
            this.btnNewTax.Text = "&New";
            this.btnNewTax.UseVisualStyleBackColor = false;
            this.btnNewTax.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // pnlTaxSaveContent
            // 
            this.pnlTaxSaveContent.BackColor = System.Drawing.SystemColors.Window;
            this.pnlTaxSaveContent.Controls.Add(this.chkStatusTax);
            this.pnlTaxSaveContent.Controls.Add(this.panel2);
            this.pnlTaxSaveContent.Controls.Add(this.txtTaxValuePercentage);
            this.pnlTaxSaveContent.Controls.Add(this.txtTaxDescription);
            this.pnlTaxSaveContent.Controls.Add(this.txtTaxName);
            this.pnlTaxSaveContent.Controls.Add(this.label3);
            this.pnlTaxSaveContent.Controls.Add(this.label2);
            this.pnlTaxSaveContent.Controls.Add(this.label1);
            this.pnlTaxSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaxSaveContent.Enabled = false;
            this.pnlTaxSaveContent.Location = new System.Drawing.Point(0, 0);
            this.pnlTaxSaveContent.Name = "pnlTaxSaveContent";
            this.pnlTaxSaveContent.Size = new System.Drawing.Size(479, 543);
            this.pnlTaxSaveContent.TabIndex = 0;
            // 
            // chkStatusTax
            // 
            this.chkStatusTax.AutoSize = true;
            this.chkStatusTax.Checked = true;
            this.chkStatusTax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStatusTax.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkStatusTax.Location = new System.Drawing.Point(366, 317);
            this.chkStatusTax.Name = "chkStatusTax";
            this.chkStatusTax.Size = new System.Drawing.Size(84, 29);
            this.chkStatusTax.TabIndex = 6;
            this.chkStatusTax.Text = "Status";
            this.chkStatusTax.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 53);
            this.panel2.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 32);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tax Settings";
            // 
            // txtTaxValuePercentage
            // 
            this.txtTaxValuePercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxValuePercentage.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTaxValuePercentage.Location = new System.Drawing.Point(176, 268);
            this.txtTaxValuePercentage.MaxLength = 5;
            this.txtTaxValuePercentage.Name = "txtTaxValuePercentage";
            this.txtTaxValuePercentage.Size = new System.Drawing.Size(288, 31);
            this.txtTaxValuePercentage.TabIndex = 2;
            this.txtTaxValuePercentage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtTaxValuePercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            this.txtTaxValuePercentage.Validated += new System.EventHandler(this.txtTaxValuePercentage_Validated);
            // 
            // txtTaxDescription
            // 
            this.txtTaxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxDescription.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTaxDescription.Location = new System.Drawing.Point(176, 212);
            this.txtTaxDescription.MaxLength = 150;
            this.txtTaxDescription.Name = "txtTaxDescription";
            this.txtTaxDescription.Size = new System.Drawing.Size(288, 31);
            this.txtTaxDescription.TabIndex = 1;
            this.txtTaxDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtTaxName
            // 
            this.txtTaxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxName.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtTaxName.Location = new System.Drawing.Point(176, 161);
            this.txtTaxName.MaxLength = 50;
            this.txtTaxName.Name = "txtTaxName";
            this.txtTaxName.Size = new System.Drawing.Size(288, 31);
            this.txtTaxName.TabIndex = 0;
            this.txtTaxName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(19, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tax Value in %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(19, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(19, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tax Name";
            // 
            // tabPageUnits
            // 
            this.tabPageUnits.Controls.Add(this.panel7);
            this.tabPageUnits.Location = new System.Drawing.Point(4, 25);
            this.tabPageUnits.Name = "tabPageUnits";
            this.tabPageUnits.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUnits.Size = new System.Drawing.Size(893, 549);
            this.tabPageUnits.TabIndex = 1;
            this.tabPageUnits.Text = "Unit Settings";
            this.tabPageUnits.UseVisualStyleBackColor = true;
            this.tabPageUnits.Enter += new System.EventHandler(this.tabPageUnits_Enter);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel10);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(887, 543);
            this.panel7.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.gridUOM);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(474, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(413, 543);
            this.panel10.TabIndex = 1;
            // 
            // gridUOM
            // 
            this.gridUOM.AllowUserToAddRows = false;
            this.gridUOM.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridUOM.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridUOM.BackgroundColor = System.Drawing.Color.White;
            this.gridUOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUOM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmUOMId,
            this.clmUOMName,
            this.clmUOMDesc});
            this.gridUOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridUOM.Location = new System.Drawing.Point(0, 0);
            this.gridUOM.Name = "gridUOM";
            this.gridUOM.ReadOnly = true;
            this.gridUOM.RowHeadersVisible = false;
            this.gridUOM.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.gridUOM.RowTemplate.Height = 30;
            this.gridUOM.Size = new System.Drawing.Size(413, 543);
            this.gridUOM.TabIndex = 1;
            this.gridUOM.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridUOM_CellClick);
            // 
            // clmUOMId
            // 
            this.clmUOMId.HeaderText = "UOM ID";
            this.clmUOMId.Name = "clmUOMId";
            this.clmUOMId.ReadOnly = true;
            this.clmUOMId.Visible = false;
            // 
            // clmUOMName
            // 
            this.clmUOMName.HeaderText = "UOM";
            this.clmUOMName.Name = "clmUOMName";
            this.clmUOMName.ReadOnly = true;
            this.clmUOMName.Width = 200;
            // 
            // clmUOMDesc
            // 
            this.clmUOMDesc.HeaderText = "Description";
            this.clmUOMDesc.Name = "clmUOMDesc";
            this.clmUOMDesc.ReadOnly = true;
            this.clmUOMDesc.Width = 200;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.pnlSaveContentUOM);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(474, 543);
            this.panel8.TabIndex = 0;
            // 
            // pnlSaveContentUOM
            // 
            this.pnlSaveContentUOM.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSaveContentUOM.Controls.Add(this.chkUOMStatus);
            this.pnlSaveContentUOM.Controls.Add(this.txtUOMDescription);
            this.pnlSaveContentUOM.Controls.Add(this.txtUOM);
            this.pnlSaveContentUOM.Controls.Add(this.lblUOMDescription);
            this.pnlSaveContentUOM.Controls.Add(this.label7);
            this.pnlSaveContentUOM.Controls.Add(this.panel11);
            this.pnlSaveContentUOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContentUOM.Location = new System.Drawing.Point(0, 0);
            this.pnlSaveContentUOM.Name = "pnlSaveContentUOM";
            this.pnlSaveContentUOM.Size = new System.Drawing.Size(474, 463);
            this.pnlSaveContentUOM.TabIndex = 1;
            // 
            // chkUOMStatus
            // 
            this.chkUOMStatus.AutoSize = true;
            this.chkUOMStatus.Checked = true;
            this.chkUOMStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUOMStatus.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkUOMStatus.Location = new System.Drawing.Point(382, 278);
            this.chkUOMStatus.Name = "chkUOMStatus";
            this.chkUOMStatus.Size = new System.Drawing.Size(84, 29);
            this.chkUOMStatus.TabIndex = 2;
            this.chkUOMStatus.Text = "Status";
            this.chkUOMStatus.UseVisualStyleBackColor = true;
            // 
            // txtUOMDescription
            // 
            this.txtUOMDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUOMDescription.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtUOMDescription.Location = new System.Drawing.Point(125, 216);
            this.txtUOMDescription.MaxLength = 50;
            this.txtUOMDescription.Name = "txtUOMDescription";
            this.txtUOMDescription.Size = new System.Drawing.Size(330, 31);
            this.txtUOMDescription.TabIndex = 1;
            this.txtUOMDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtUOM
            // 
            this.txtUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUOM.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtUOM.Location = new System.Drawing.Point(125, 165);
            this.txtUOM.MaxLength = 25;
            this.txtUOM.Name = "txtUOM";
            this.txtUOM.Size = new System.Drawing.Size(330, 31);
            this.txtUOM.TabIndex = 0;
            this.txtUOM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // lblUOMDescription
            // 
            this.lblUOMDescription.AutoSize = true;
            this.lblUOMDescription.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblUOMDescription.Location = new System.Drawing.Point(8, 222);
            this.lblUOMDescription.Name = "lblUOMDescription";
            this.lblUOMDescription.Size = new System.Drawing.Size(108, 25);
            this.lblUOMDescription.TabIndex = 10;
            this.lblUOMDescription.Text = "Description";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(8, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "UOM";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel11.Controls.Add(this.label5);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(474, 56);
            this.panel11.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(5, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 32);
            this.label5.TabIndex = 1;
            this.label5.Text = "UOM Settings";
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel9.Controls.Add(this.btnSaveUOM);
            this.panel9.Controls.Add(this.btnCancelUOM);
            this.panel9.Controls.Add(this.btnCloseUOM);
            this.panel9.Controls.Add(this.btnNewUOM);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 463);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(474, 80);
            this.panel9.TabIndex = 1;
            // 
            // btnSaveUOM
            // 
            this.btnSaveUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveUOM.BackColor = System.Drawing.Color.Teal;
            this.btnSaveUOM.Enabled = false;
            this.btnSaveUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveUOM.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveUOM.ForeColor = System.Drawing.Color.White;
            this.btnSaveUOM.Location = new System.Drawing.Point(349, 18);
            this.btnSaveUOM.Name = "btnSaveUOM";
            this.btnSaveUOM.Size = new System.Drawing.Size(106, 47);
            this.btnSaveUOM.TabIndex = 1;
            this.btnSaveUOM.Text = "&Save";
            this.btnSaveUOM.UseVisualStyleBackColor = false;
            this.btnSaveUOM.Click += new System.EventHandler(this.ButtonEventsUOM);
            // 
            // btnCancelUOM
            // 
            this.btnCancelUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelUOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancelUOM.Enabled = false;
            this.btnCancelUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelUOM.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelUOM.ForeColor = System.Drawing.Color.White;
            this.btnCancelUOM.Location = new System.Drawing.Point(237, 18);
            this.btnCancelUOM.Name = "btnCancelUOM";
            this.btnCancelUOM.Size = new System.Drawing.Size(106, 47);
            this.btnCancelUOM.TabIndex = 2;
            this.btnCancelUOM.Text = "Canc&el";
            this.btnCancelUOM.UseVisualStyleBackColor = false;
            this.btnCancelUOM.Click += new System.EventHandler(this.ButtonEventsUOM);
            // 
            // btnCloseUOM
            // 
            this.btnCloseUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseUOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCloseUOM.Enabled = false;
            this.btnCloseUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseUOM.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseUOM.ForeColor = System.Drawing.Color.White;
            this.btnCloseUOM.Location = new System.Drawing.Point(125, 18);
            this.btnCloseUOM.Name = "btnCloseUOM";
            this.btnCloseUOM.Size = new System.Drawing.Size(106, 47);
            this.btnCloseUOM.TabIndex = 3;
            this.btnCloseUOM.Text = "&Close";
            this.btnCloseUOM.UseVisualStyleBackColor = false;
            this.btnCloseUOM.Click += new System.EventHandler(this.ButtonEventsUOM);
            // 
            // btnNewUOM
            // 
            this.btnNewUOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewUOM.BackColor = System.Drawing.Color.Green;
            this.btnNewUOM.Enabled = false;
            this.btnNewUOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewUOM.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewUOM.ForeColor = System.Drawing.Color.White;
            this.btnNewUOM.Location = new System.Drawing.Point(13, 18);
            this.btnNewUOM.Name = "btnNewUOM";
            this.btnNewUOM.Size = new System.Drawing.Size(106, 47);
            this.btnNewUOM.TabIndex = 0;
            this.btnNewUOM.Text = "&New";
            this.btnNewUOM.UseVisualStyleBackColor = false;
            this.btnNewUOM.Click += new System.EventHandler(this.ButtonEventsUOM);
            // 
            // CommonSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 578);
            this.Controls.Add(this.panel1);
            this.Name = "CommonSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Common Settings";
            this.Load += new System.EventHandler(this.CommonSettingsForm_Load);
            this.panel1.ResumeLayout(false);
            this.tabCommonSettings.ResumeLayout(false);
            this.tabPageTax.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTaxSett)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlTaxSaveContent.ResumeLayout(false);
            this.pnlTaxSaveContent.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPageUnits.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridUOM)).EndInit();
            this.panel8.ResumeLayout(false);
            this.pnlSaveContentUOM.ResumeLayout(false);
            this.pnlSaveContentUOM.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabCommonSettings;
        private System.Windows.Forms.TabPage tabPageTax;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlTaxSaveContent;
        private System.Windows.Forms.TabPage tabPageUnits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTaxValuePercentage;
        private System.Windows.Forms.TextBox txtTaxDescription;
        private System.Windows.Forms.TextBox txtTaxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveTax;
        private System.Windows.Forms.Button btnCancelTax;
        private System.Windows.Forms.Button btnCloseTax;
        private System.Windows.Forms.Button btnNewTax;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chkStatusTax;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView gridTaxSett;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaxId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaxName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaxDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTaxValue;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel pnlSaveContentUOM;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkUOMStatus;
        private System.Windows.Forms.TextBox txtUOMDescription;
        private System.Windows.Forms.TextBox txtUOM;
        private System.Windows.Forms.Label lblUOMDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSaveUOM;
        private System.Windows.Forms.Button btnCancelUOM;
        private System.Windows.Forms.Button btnCloseUOM;
        private System.Windows.Forms.Button btnNewUOM;
        private System.Windows.Forms.DataGridView gridUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUOMId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUOMName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUOMDesc;
    }
}