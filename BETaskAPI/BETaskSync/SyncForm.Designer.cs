namespace BETaskSync
{
    partial class SyncForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlManualUpdate = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblPending = new System.Windows.Forms.Label();
            this.gridCustomersUpdated = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmupdatedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkUpdate = new System.Windows.Forms.LinkLabel();
            this.lblNextSync = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.gridNewCustomers = new System.Windows.Forms.DataGridView();
            this.clmTempId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBuilding = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSavedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.pnlManualUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomersUpdated)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNewCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.pnlManualUpdate);
            this.panel1.Controls.Add(this.lblPending);
            this.panel1.Controls.Add(this.gridCustomersUpdated);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.gridNewCustomers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1339, 693);
            this.panel1.TabIndex = 0;
            // 
            // pnlManualUpdate
            // 
            this.pnlManualUpdate.Controls.Add(this.btnUpdate);
            this.pnlManualUpdate.Controls.Add(this.richTextBox1);
            this.pnlManualUpdate.Location = new System.Drawing.Point(178, 53);
            this.pnlManualUpdate.Name = "pnlManualUpdate";
            this.pnlManualUpdate.Size = new System.Drawing.Size(541, 503);
            this.pnlManualUpdate.TabIndex = 4;
            this.pnlManualUpdate.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(449, 453);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 35);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(541, 447);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.Location = new System.Drawing.Point(1051, 254);
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(46, 17);
            this.lblPending.TabIndex = 3;
            this.lblPending.Text = "label1";
            // 
            // gridCustomersUpdated
            // 
            this.gridCustomersUpdated.AllowUserToAddRows = false;
            this.gridCustomersUpdated.AllowUserToDeleteRows = false;
            this.gridCustomersUpdated.BackgroundColor = System.Drawing.Color.Yellow;
            this.gridCustomersUpdated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomersUpdated.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.clmupdatedOn});
            this.gridCustomersUpdated.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridCustomersUpdated.Location = new System.Drawing.Point(0, 274);
            this.gridCustomersUpdated.Name = "gridCustomersUpdated";
            this.gridCustomersUpdated.ReadOnly = true;
            this.gridCustomersUpdated.RowHeadersWidth = 51;
            this.gridCustomersUpdated.RowTemplate.Height = 24;
            this.gridCustomersUpdated.Size = new System.Drawing.Size(1339, 343);
            this.gridCustomersUpdated.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Customer";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Route";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Address";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 250;
            // 
            // clmupdatedOn
            // 
            this.clmupdatedOn.HeaderText = "UpdatedOn";
            this.clmupdatedOn.MinimumWidth = 6;
            this.clmupdatedOn.Name = "clmupdatedOn";
            this.clmupdatedOn.ReadOnly = true;
            this.clmupdatedOn.Width = 125;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Controls.Add(this.linkUpdate);
            this.panel2.Controls.Add(this.lblNextSync);
            this.panel2.Controls.Add(this.btnRestart);
            this.panel2.Controls.Add(this.lblError);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 623);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1339, 70);
            this.panel2.TabIndex = 1;
            // 
            // linkUpdate
            // 
            this.linkUpdate.AutoSize = true;
            this.linkUpdate.Location = new System.Drawing.Point(1089, 16);
            this.linkUpdate.Name = "linkUpdate";
            this.linkUpdate.Size = new System.Drawing.Size(104, 17);
            this.linkUpdate.TabIndex = 3;
            this.linkUpdate.TabStop = true;
            this.linkUpdate.Text = "Manual Update";
            this.linkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUpdate_LinkClicked);
            // 
            // lblNextSync
            // 
            this.lblNextSync.AutoSize = true;
            this.lblNextSync.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNextSync.Location = new System.Drawing.Point(0, 53);
            this.lblNextSync.Name = "lblNextSync";
            this.lblNextSync.Size = new System.Drawing.Size(46, 17);
            this.lblNextSync.TabIndex = 2;
            this.lblNextSync.Text = "label1";
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(1235, 3);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(92, 43);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblError.Location = new System.Drawing.Point(0, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(46, 17);
            this.lblError.TabIndex = 0;
            this.lblError.Text = "label1";
            // 
            // gridNewCustomers
            // 
            this.gridNewCustomers.AllowUserToAddRows = false;
            this.gridNewCustomers.AllowUserToDeleteRows = false;
            this.gridNewCustomers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.gridNewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNewCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTempId,
            this.clmCustomer,
            this.clmRoute,
            this.clmAddress,
            this.clmBuilding,
            this.clmSavedTime});
            this.gridNewCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridNewCustomers.Location = new System.Drawing.Point(0, 0);
            this.gridNewCustomers.Name = "gridNewCustomers";
            this.gridNewCustomers.ReadOnly = true;
            this.gridNewCustomers.RowHeadersWidth = 51;
            this.gridNewCustomers.RowTemplate.Height = 24;
            this.gridNewCustomers.Size = new System.Drawing.Size(1339, 274);
            this.gridNewCustomers.TabIndex = 0;
            // 
            // clmTempId
            // 
            this.clmTempId.HeaderText = "Id";
            this.clmTempId.MinimumWidth = 6;
            this.clmTempId.Name = "clmTempId";
            this.clmTempId.ReadOnly = true;
            this.clmTempId.Width = 80;
            // 
            // clmCustomer
            // 
            this.clmCustomer.HeaderText = "Customer";
            this.clmCustomer.MinimumWidth = 6;
            this.clmCustomer.Name = "clmCustomer";
            this.clmCustomer.ReadOnly = true;
            this.clmCustomer.Width = 200;
            // 
            // clmRoute
            // 
            this.clmRoute.HeaderText = "Route";
            this.clmRoute.MinimumWidth = 6;
            this.clmRoute.Name = "clmRoute";
            this.clmRoute.ReadOnly = true;
            this.clmRoute.Width = 125;
            // 
            // clmAddress
            // 
            this.clmAddress.HeaderText = "Address";
            this.clmAddress.MinimumWidth = 6;
            this.clmAddress.Name = "clmAddress";
            this.clmAddress.ReadOnly = true;
            this.clmAddress.Width = 400;
            // 
            // clmBuilding
            // 
            this.clmBuilding.HeaderText = "Building / Apartment";
            this.clmBuilding.MinimumWidth = 6;
            this.clmBuilding.Name = "clmBuilding";
            this.clmBuilding.ReadOnly = true;
            this.clmBuilding.Width = 250;
            // 
            // clmSavedTime
            // 
            this.clmSavedTime.HeaderText = "AddedOn";
            this.clmSavedTime.MinimumWidth = 6;
            this.clmSavedTime.Name = "clmSavedTime";
            this.clmSavedTime.ReadOnly = true;
            this.clmSavedTime.Width = 125;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 693);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BETask Auto Synchroniser [Do not close]";
            this.Load += new System.EventHandler(this.SyncForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlManualUpdate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomersUpdated)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNewCustomers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridNewCustomers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView gridCustomersUpdated;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmupdatedOn;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblNextSync;
        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTempId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBuilding;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSavedTime;
        private System.Windows.Forms.Panel pnlManualUpdate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.LinkLabel linkUpdate;
    }
}

