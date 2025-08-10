namespace BETask.Views
{
    partial class DailyCollectionForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlEditColl = new System.Windows.Forms.Panel();
            this.lblOldLeaf = new System.Windows.Forms.Label();
            this.linkRemap = new System.Windows.Forms.LinkLabel();
            this.txtNetAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblCollId = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdateClose = new System.Windows.Forms.Button();
            this.txtCollectionAmount = new System.Windows.Forms.TextBox();
            this.cmbNewPaymenyMode = new System.Windows.Forms.ComboBox();
            this.lblOldPaymentMode = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblDeliveryId = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gridCollection = new System.Windows.Forms.DataGridView();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDeposit = new System.Windows.Forms.CheckBox();
            this.rdlAll = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.chkCollection = new System.Windows.Forms.CheckBox();
            this.lblCouponBalance = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblQty = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.clmDeliveryId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCollectedAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddtoWallet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmUpdateMode = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmCollectionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOldLeaf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pnlEditColl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCollection)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlEditColl);
            this.panel1.Controls.Add(this.gridCollection);
            this.panel1.Controls.Add(this.pnlHeader);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1161, 705);
            this.panel1.TabIndex = 0;
            // 
            // pnlEditColl
            // 
            this.pnlEditColl.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlEditColl.Controls.Add(this.lblOldLeaf);
            this.pnlEditColl.Controls.Add(this.linkRemap);
            this.pnlEditColl.Controls.Add(this.txtNetAmount);
            this.pnlEditColl.Controls.Add(this.label10);
            this.pnlEditColl.Controls.Add(this.label9);
            this.pnlEditColl.Controls.Add(this.txtPassword);
            this.pnlEditColl.Controls.Add(this.lblCollId);
            this.pnlEditColl.Controls.Add(this.btnSave);
            this.pnlEditColl.Controls.Add(this.btnUpdateClose);
            this.pnlEditColl.Controls.Add(this.txtCollectionAmount);
            this.pnlEditColl.Controls.Add(this.cmbNewPaymenyMode);
            this.pnlEditColl.Controls.Add(this.lblOldPaymentMode);
            this.pnlEditColl.Controls.Add(this.lblCustomer);
            this.pnlEditColl.Controls.Add(this.lblDeliveryId);
            this.pnlEditColl.Controls.Add(this.label8);
            this.pnlEditColl.Controls.Add(this.label7);
            this.pnlEditColl.Controls.Add(this.label5);
            this.pnlEditColl.Controls.Add(this.label3);
            this.pnlEditColl.Controls.Add(this.label2);
            this.pnlEditColl.Location = new System.Drawing.Point(224, 254);
            this.pnlEditColl.Name = "pnlEditColl";
            this.pnlEditColl.Size = new System.Drawing.Size(690, 338);
            this.pnlEditColl.TabIndex = 6;
            this.pnlEditColl.Visible = false;
            // 
            // lblOldLeaf
            // 
            this.lblOldLeaf.AutoSize = true;
            this.lblOldLeaf.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblOldLeaf.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblOldLeaf.Location = new System.Drawing.Point(555, 59);
            this.lblOldLeaf.Name = "lblOldLeaf";
            this.lblOldLeaf.Size = new System.Drawing.Size(22, 25);
            this.lblOldLeaf.TabIndex = 99;
            this.lblOldLeaf.Text = "0";
            // 
            // linkRemap
            // 
            this.linkRemap.AutoSize = true;
            this.linkRemap.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkRemap.LinkColor = System.Drawing.Color.Maroon;
            this.linkRemap.Location = new System.Drawing.Point(587, 227);
            this.linkRemap.Name = "linkRemap";
            this.linkRemap.Size = new System.Drawing.Size(78, 25);
            this.linkRemap.TabIndex = 98;
            this.linkRemap.TabStop = true;
            this.linkRemap.Text = "RE MAP";
            this.linkRemap.Visible = false;
            this.linkRemap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemap_LinkClicked);
            // 
            // txtNetAmount
            // 
            this.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNetAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNetAmount.Location = new System.Drawing.Point(292, 227);
            this.txtNetAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtNetAmount.Name = "txtNetAmount";
            this.txtNetAmount.Size = new System.Drawing.Size(224, 31);
            this.txtNetAmount.TabIndex = 97;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label10.Location = new System.Drawing.Point(44, 229);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 25);
            this.label10.TabIndex = 96;
            this.label10.Text = "Net amount";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(3, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 25);
            this.label9.TabIndex = 95;
            this.label9.Text = "Password";
            this.label9.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPassword.Location = new System.Drawing.Point(4, 300);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '#';
            this.txtPassword.Size = new System.Drawing.Size(224, 31);
            this.txtPassword.TabIndex = 94;
            this.txtPassword.Visible = false;
            // 
            // lblCollId
            // 
            this.lblCollId.AutoSize = true;
            this.lblCollId.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCollId.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCollId.Location = new System.Drawing.Point(555, 18);
            this.lblCollId.Name = "lblCollId";
            this.lblCollId.Size = new System.Drawing.Size(22, 25);
            this.lblCollId.TabIndex = 93;
            this.lblCollId.Text = "0";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(573, 280);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 91;
            this.btnSave.Text = "Update";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnUpdateClose
            // 
            this.btnUpdateClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnUpdateClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateClose.ForeColor = System.Drawing.Color.White;
            this.btnUpdateClose.Location = new System.Drawing.Point(459, 280);
            this.btnUpdateClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdateClose.Name = "btnUpdateClose";
            this.btnUpdateClose.Size = new System.Drawing.Size(107, 47);
            this.btnUpdateClose.TabIndex = 92;
            this.btnUpdateClose.Text = "&Close";
            this.btnUpdateClose.UseVisualStyleBackColor = false;
            this.btnUpdateClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // txtCollectionAmount
            // 
            this.txtCollectionAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCollectionAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCollectionAmount.Location = new System.Drawing.Point(292, 187);
            this.txtCollectionAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtCollectionAmount.Name = "txtCollectionAmount";
            this.txtCollectionAmount.Size = new System.Drawing.Size(224, 31);
            this.txtCollectionAmount.TabIndex = 90;
            this.txtCollectionAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCollectionAmount_KeyPress);
            // 
            // cmbNewPaymenyMode
            // 
            this.cmbNewPaymenyMode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbNewPaymenyMode.FormattingEnabled = true;
            this.cmbNewPaymenyMode.Location = new System.Drawing.Point(292, 147);
            this.cmbNewPaymenyMode.Name = "cmbNewPaymenyMode";
            this.cmbNewPaymenyMode.Size = new System.Drawing.Size(224, 33);
            this.cmbNewPaymenyMode.TabIndex = 89;
            // 
            // lblOldPaymentMode
            // 
            this.lblOldPaymentMode.AutoSize = true;
            this.lblOldPaymentMode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblOldPaymentMode.ForeColor = System.Drawing.SystemColors.Control;
            this.lblOldPaymentMode.Location = new System.Drawing.Point(287, 104);
            this.lblOldPaymentMode.Name = "lblOldPaymentMode";
            this.lblOldPaymentMode.Size = new System.Drawing.Size(173, 25);
            this.lblOldPaymentMode.TabIndex = 88;
            this.lblOldPaymentMode.Text = "Old payment mode";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblCustomer.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCustomer.Location = new System.Drawing.Point(287, 59);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(93, 25);
            this.lblCustomer.TabIndex = 87;
            this.lblCustomer.Text = "Customer";
            // 
            // lblDeliveryId
            // 
            this.lblDeliveryId.AutoSize = true;
            this.lblDeliveryId.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblDeliveryId.ForeColor = System.Drawing.SystemColors.Control;
            this.lblDeliveryId.Location = new System.Drawing.Point(287, 18);
            this.lblDeliveryId.Name = "lblDeliveryId";
            this.lblDeliveryId.Size = new System.Drawing.Size(101, 25);
            this.lblDeliveryId.TabIndex = 86;
            this.lblDeliveryId.Text = "Delivery id";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(44, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 25);
            this.label8.TabIndex = 85;
            this.label8.Text = "Collection amount";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(44, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 25);
            this.label7.TabIndex = 84;
            this.label7.Text = "New payment mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(44, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 25);
            this.label5.TabIndex = 83;
            this.label5.Text = "Old payment mode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(44, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 25);
            this.label3.TabIndex = 82;
            this.label3.Text = "Customer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(44, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 81;
            this.label2.Text = "Delivery id";
            // 
            // gridCollection
            // 
            this.gridCollection.AllowUserToAddRows = false;
            this.gridCollection.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridCollection.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCollection.BackgroundColor = System.Drawing.Color.White;
            this.gridCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCollection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDeliveryId,
            this.clmCustomerId,
            this.clmEmployee,
            this.clmRoute,
            this.clmCustomer,
            this.clmPaymentMode,
            this.clmNetAmount,
            this.clmCollectedAmount,
            this.clmAddtoWallet,
            this.clmUpdateMode,
            this.clmCollectionId,
            this.clmOldLeaf});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCollection.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCollection.Location = new System.Drawing.Point(0, 183);
            this.gridCollection.Margin = new System.Windows.Forms.Padding(4);
            this.gridCollection.Name = "gridCollection";
            this.gridCollection.ReadOnly = true;
            this.gridCollection.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridCollection.RowTemplate.Height = 35;
            this.gridCollection.Size = new System.Drawing.Size(1161, 458);
            this.gridCollection.TabIndex = 5;
            this.gridCollection.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCollection_CellClick);
            this.gridCollection.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCollection_CellContentDoubleClick);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.groupBox1);
            this.pnlHeader.Controls.Add(this.btnSearch);
            this.pnlHeader.Controls.Add(this.label18);
            this.pnlHeader.Controls.Add(this.cmbRoute);
            this.pnlHeader.Controls.Add(this.dtpFrom);
            this.pnlHeader.Controls.Add(this.label4);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.cmbCustomer);
            this.pnlHeader.Controls.Add(this.label6);
            this.pnlHeader.Controls.Add(this.cmbEmployee);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1161, 183);
            this.pnlHeader.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDeposit);
            this.groupBox1.Controls.Add(this.rdlAll);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbPaymentMode);
            this.groupBox1.Controls.Add(this.chkCollection);
            this.groupBox1.Controls.Add(this.lblCouponBalance);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.groupBox1.Location = new System.Drawing.Point(13, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(943, 82);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // chkDeposit
            // 
            this.chkDeposit.AutoSize = true;
            this.chkDeposit.Location = new System.Drawing.Point(721, 34);
            this.chkDeposit.Name = "chkDeposit";
            this.chkDeposit.Size = new System.Drawing.Size(139, 29);
            this.chkDeposit.TabIndex = 81;
            this.chkDeposit.Text = "Deposit only";
            this.chkDeposit.UseVisualStyleBackColor = true;
            // 
            // rdlAll
            // 
            this.rdlAll.AutoSize = true;
            this.rdlAll.Checked = true;
            this.rdlAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rdlAll.Location = new System.Drawing.Point(19, 34);
            this.rdlAll.Name = "rdlAll";
            this.rdlAll.Size = new System.Drawing.Size(64, 29);
            this.rdlAll.TabIndex = 80;
            this.rdlAll.Text = "ALL";
            this.rdlAll.UseVisualStyleBackColor = true;
            this.rdlAll.CheckedChanged += new System.EventHandler(this.rdlAll_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label11.Location = new System.Drawing.Point(156, 33);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 25);
            this.label11.TabIndex = 79;
            this.label11.Text = "Payment Mode";
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(301, 30);
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(215, 33);
            this.cmbPaymentMode.TabIndex = 78;
            // 
            // chkCollection
            // 
            this.chkCollection.AutoSize = true;
            this.chkCollection.Location = new System.Drawing.Point(555, 34);
            this.chkCollection.Name = "chkCollection";
            this.chkCollection.Size = new System.Drawing.Size(160, 29);
            this.chkCollection.TabIndex = 29;
            this.chkCollection.Text = "Collection only";
            this.chkCollection.UseVisualStyleBackColor = true;
            // 
            // lblCouponBalance
            // 
            this.lblCouponBalance.AutoSize = true;
            this.lblCouponBalance.ForeColor = System.Drawing.Color.Green;
            this.lblCouponBalance.Location = new System.Drawing.Point(46, 148);
            this.lblCouponBalance.Name = "lblCouponBalance";
            this.lblCouponBalance.Size = new System.Drawing.Size(46, 25);
            this.lblCouponBalance.TabIndex = 27;
            this.lblCouponBalance.Text = "0.00";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(989, 109);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(159, 47);
            this.btnSearch.TabIndex = 84;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(202, 17);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(133, 25);
            this.label18.TabIndex = 83;
            this.label18.Text = "Delivery Route";
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(207, 45);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(224, 33);
            this.cmbRoute.TabIndex = 82;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(13, 45);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(175, 31);
            this.dtpFrom.TabIndex = 76;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 25);
            this.label4.TabIndex = 81;
            this.label4.Text = "Date From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(436, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 25);
            this.label1.TabIndex = 80;
            this.label1.Text = "Customer";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(441, 47);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(331, 33);
            this.cmbCustomer.TabIndex = 77;
            this.cmbCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomer_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(775, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 25);
            this.label6.TabIndex = 79;
            this.label6.Text = "Employee";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(780, 47);
            this.cmbEmployee.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(300, 33);
            this.cmbEmployee.TabIndex = 78;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.lblQty);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.lblNetAmount);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 641);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1161, 64);
            this.panel3.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1051, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblQty.ForeColor = System.Drawing.Color.White;
            this.lblQty.Location = new System.Drawing.Point(464, 16);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(0, 25);
            this.lblQty.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(939, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Purple;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(3, 6);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 47);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.AutoSize = true;
            this.lblNetAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblNetAmount.ForeColor = System.Drawing.Color.White;
            this.lblNetAmount.Location = new System.Drawing.Point(163, 16);
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.Size = new System.Drawing.Size(0, 25);
            this.lblNetAmount.TabIndex = 6;
            // 
            // clmDeliveryId
            // 
            this.clmDeliveryId.HeaderText = "DeliveryId";
            this.clmDeliveryId.Name = "clmDeliveryId";
            this.clmDeliveryId.ReadOnly = true;
            this.clmDeliveryId.Visible = false;
            // 
            // clmCustomerId
            // 
            this.clmCustomerId.HeaderText = "CustomerId";
            this.clmCustomerId.Name = "clmCustomerId";
            this.clmCustomerId.ReadOnly = true;
            this.clmCustomerId.Visible = false;
            // 
            // clmEmployee
            // 
            this.clmEmployee.HeaderText = "Employee";
            this.clmEmployee.Name = "clmEmployee";
            this.clmEmployee.ReadOnly = true;
            // 
            // clmRoute
            // 
            this.clmRoute.HeaderText = "Route";
            this.clmRoute.Name = "clmRoute";
            this.clmRoute.ReadOnly = true;
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmCustomer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmCustomer.Width = 150;
            // 
            // clmPaymentMode
            // 
            this.clmPaymentMode.HeaderText = "PaymentMode";
            this.clmPaymentMode.Name = "clmPaymentMode";
            this.clmPaymentMode.ReadOnly = true;
            this.clmPaymentMode.Width = 80;
            // 
            // clmNetAmount
            // 
            this.clmNetAmount.HeaderText = "Net Amount";
            this.clmNetAmount.Name = "clmNetAmount";
            this.clmNetAmount.ReadOnly = true;
            this.clmNetAmount.Visible = false;
            this.clmNetAmount.Width = 80;
            // 
            // clmCollectedAmount
            // 
            this.clmCollectedAmount.HeaderText = "Collection Amount";
            this.clmCollectedAmount.Name = "clmCollectedAmount";
            this.clmCollectedAmount.ReadOnly = true;
            // 
            // clmAddtoWallet
            // 
            this.clmAddtoWallet.HeaderText = "Add to Wallet";
            this.clmAddtoWallet.Name = "clmAddtoWallet";
            this.clmAddtoWallet.ReadOnly = true;
            this.clmAddtoWallet.Visible = false;
            // 
            // clmUpdateMode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            this.clmUpdateMode.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmUpdateMode.HeaderText = "Update";
            this.clmUpdateMode.Name = "clmUpdateMode";
            this.clmUpdateMode.ReadOnly = true;
            // 
            // clmCollectionId
            // 
            this.clmCollectionId.HeaderText = "CollectionId";
            this.clmCollectionId.Name = "clmCollectionId";
            this.clmCollectionId.ReadOnly = true;
            // 
            // clmOldLeaf
            // 
            this.clmOldLeaf.HeaderText = "OldLeaf";
            this.clmOldLeaf.Name = "clmOldLeaf";
            this.clmOldLeaf.ReadOnly = true;
            this.clmOldLeaf.Visible = false;
            // 
            // DailyCollectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 705);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "DailyCollectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Collection";
            this.Load += new System.EventHandler(this.DailyCollectionForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DailyCollectionForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.pnlEditColl.ResumeLayout(false);
            this.pnlEditColl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCollection)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.DataGridView gridCollection;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCouponBalance;
        private System.Windows.Forms.CheckBox chkCollection;
        private System.Windows.Forms.Panel pnlEditColl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDeliveryId;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblOldPaymentMode;
        private System.Windows.Forms.ComboBox cmbNewPaymenyMode;
        private System.Windows.Forms.TextBox txtCollectionAmount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdateClose;
        private System.Windows.Forms.Label lblCollId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtNetAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox rdlAll;
        private System.Windows.Forms.CheckBox chkDeposit;
        private System.Windows.Forms.LinkLabel linkRemap;
        private System.Windows.Forms.Label lblOldLeaf;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeliveryId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPaymentMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNetAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCollectedAmount;
        private System.Windows.Forms.DataGridViewButtonColumn clmAddtoWallet;
        private System.Windows.Forms.DataGridViewButtonColumn clmUpdateMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCollectionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOldLeaf;
    }
}