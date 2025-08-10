namespace BETask.Views
{
    partial class DeliveryRequestForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlDelievryRequestItem = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPanelClose = new System.Windows.Forms.Button();
            this.dgvDeliveryItems = new System.Windows.Forms.DataGridView();
            this.colItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDeliveryRequest = new System.Windows.Forms.DataGridView();
            this.colRequestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRequestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOtherDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colView = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblNetSaleAmount = new System.Windows.Forms.Label();
            this.lblNetAmount = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.panel1.SuspendLayout();
            this.pnlDelievryRequestItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryRequest)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlDelievryRequestItem);
            this.panel1.Controls.Add(this.dgvDeliveryRequest);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1120, 637);
            this.panel1.TabIndex = 0;
            // 
            // pnlDelievryRequestItem
            // 
            this.pnlDelievryRequestItem.Controls.Add(this.label4);
            this.pnlDelievryRequestItem.Controls.Add(this.btnPanelClose);
            this.pnlDelievryRequestItem.Controls.Add(this.dgvDeliveryItems);
            this.pnlDelievryRequestItem.Location = new System.Drawing.Point(288, 66);
            this.pnlDelievryRequestItem.Name = "pnlDelievryRequestItem";
            this.pnlDelievryRequestItem.Size = new System.Drawing.Size(832, 507);
            this.pnlDelievryRequestItem.TabIndex = 4;
            this.pnlDelievryRequestItem.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Delivery Request Item Details";
            // 
            // btnPanelClose
            // 
            this.btnPanelClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPanelClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnPanelClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanelClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPanelClose.ForeColor = System.Drawing.Color.White;
            this.btnPanelClose.Location = new System.Drawing.Point(778, 2);
            this.btnPanelClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPanelClose.Name = "btnPanelClose";
            this.btnPanelClose.Size = new System.Drawing.Size(52, 35);
            this.btnPanelClose.TabIndex = 6;
            this.btnPanelClose.Text = "X";
            this.btnPanelClose.UseVisualStyleBackColor = false;
            this.btnPanelClose.Click += new System.EventHandler(this.btnPanelClose_Click);
            // 
            // dgvDeliveryItems
            // 
            this.dgvDeliveryItems.AllowUserToAddRows = false;
            this.dgvDeliveryItems.AllowUserToDeleteRows = false;
            this.dgvDeliveryItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDeliveryItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDeliveryItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveryItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItemId,
            this.colItemName,
            this.colRate,
            this.colQty,
            this.colNetAmount});
            this.dgvDeliveryItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvDeliveryItems.Location = new System.Drawing.Point(0, 39);
            this.dgvDeliveryItems.Name = "dgvDeliveryItems";
            this.dgvDeliveryItems.ReadOnly = true;
            this.dgvDeliveryItems.RowHeadersWidth = 51;
            this.dgvDeliveryItems.RowTemplate.Height = 24;
            this.dgvDeliveryItems.Size = new System.Drawing.Size(832, 468);
            this.dgvDeliveryItems.TabIndex = 3;
            // 
            // colItemId
            // 
            this.colItemId.HeaderText = "ItemId";
            this.colItemId.MinimumWidth = 10;
            this.colItemId.Name = "colItemId";
            this.colItemId.ReadOnly = true;
            this.colItemId.Visible = false;
            this.colItemId.Width = 125;
            // 
            // colItemName
            // 
            this.colItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colItemName.HeaderText = "Item Name";
            this.colItemName.MinimumWidth = 6;
            this.colItemName.Name = "colItemName";
            this.colItemName.ReadOnly = true;
            this.colItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colRate
            // 
            this.colRate.HeaderText = "Rate";
            this.colRate.MinimumWidth = 6;
            this.colRate.Name = "colRate";
            this.colRate.ReadOnly = true;
            this.colRate.Width = 125;
            // 
            // colQty
            // 
            this.colQty.HeaderText = "Qty";
            this.colQty.MinimumWidth = 6;
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            this.colQty.Width = 80;
            // 
            // colNetAmount
            // 
            this.colNetAmount.HeaderText = "Net Amount";
            this.colNetAmount.MinimumWidth = 6;
            this.colNetAmount.Name = "colNetAmount";
            this.colNetAmount.ReadOnly = true;
            this.colNetAmount.Width = 125;
            // 
            // dgvDeliveryRequest
            // 
            this.dgvDeliveryRequest.AllowUserToAddRows = false;
            this.dgvDeliveryRequest.AllowUserToDeleteRows = false;
            this.dgvDeliveryRequest.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDeliveryRequest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDeliveryRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveryRequest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRequestId,
            this.colCustomerName,
            this.colCustStatus,
            this.colAddress,
            this.colAddress1,
            this.colTotalItem,
            this.colNet,
            this.colRequestTime,
            this.colOtherDetails,
            this.colView,
            this.colAction});
            this.dgvDeliveryRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDeliveryRequest.Location = new System.Drawing.Point(0, 66);
            this.dgvDeliveryRequest.Name = "dgvDeliveryRequest";
            this.dgvDeliveryRequest.ReadOnly = true;
            this.dgvDeliveryRequest.RowHeadersVisible = false;
            this.dgvDeliveryRequest.RowHeadersWidth = 51;
            this.dgvDeliveryRequest.RowTemplate.Height = 24;
            this.dgvDeliveryRequest.Size = new System.Drawing.Size(1120, 507);
            this.dgvDeliveryRequest.TabIndex = 2;
            this.dgvDeliveryRequest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeliveryRequest_CellClick);
            // 
            // colRequestId
            // 
            this.colRequestId.HeaderText = "RequestId";
            this.colRequestId.MinimumWidth = 10;
            this.colRequestId.Name = "colRequestId";
            this.colRequestId.ReadOnly = true;
            this.colRequestId.Visible = false;
            this.colRequestId.Width = 125;
            // 
            // colCustomerName
            // 
            this.colCustomerName.HeaderText = "Customer Name";
            this.colCustomerName.MinimumWidth = 6;
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            this.colCustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCustomerName.Width = 150;
            // 
            // colCustStatus
            // 
            this.colCustStatus.HeaderText = "Status";
            this.colCustStatus.Name = "colCustStatus";
            this.colCustStatus.ReadOnly = true;
            this.colCustStatus.Width = 80;
            // 
            // colAddress
            // 
            this.colAddress.HeaderText = "Address";
            this.colAddress.MinimumWidth = 6;
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Width = 150;
            // 
            // colAddress1
            // 
            this.colAddress1.HeaderText = "Address1";
            this.colAddress1.MinimumWidth = 6;
            this.colAddress1.Name = "colAddress1";
            this.colAddress1.ReadOnly = true;
            this.colAddress1.Width = 80;
            // 
            // colTotalItem
            // 
            this.colTotalItem.HeaderText = "TotalItem";
            this.colTotalItem.MinimumWidth = 6;
            this.colTotalItem.Name = "colTotalItem";
            this.colTotalItem.ReadOnly = true;
            this.colTotalItem.Width = 50;
            // 
            // colNet
            // 
            this.colNet.HeaderText = "Net Amount";
            this.colNet.Name = "colNet";
            this.colNet.ReadOnly = true;
            // 
            // colRequestTime
            // 
            this.colRequestTime.HeaderText = "Request Time";
            this.colRequestTime.Name = "colRequestTime";
            this.colRequestTime.ReadOnly = true;
            // 
            // colOtherDetails
            // 
            this.colOtherDetails.HeaderText = "OtherDetails";
            this.colOtherDetails.Name = "colOtherDetails";
            this.colOtherDetails.ReadOnly = true;
            this.colOtherDetails.Visible = false;
            // 
            // colView
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkTurquoise;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.colView.DefaultCellStyle = dataGridViewCellStyle5;
            this.colView.HeaderText = "";
            this.colView.MinimumWidth = 6;
            this.colView.Name = "colView";
            this.colView.ReadOnly = true;
            this.colView.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colView.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colView.Width = 90;
            // 
            // colAction
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            this.colAction.DefaultCellStyle = dataGridViewCellStyle6;
            this.colAction.HeaderText = "Action";
            this.colAction.MinimumWidth = 6;
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colAction.Width = 90;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.lblNetSaleAmount);
            this.panel3.Controls.Add(this.lblNetAmount);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 573);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1120, 64);
            this.panel3.TabIndex = 1;
            // 
            // lblNetSaleAmount
            // 
            this.lblNetSaleAmount.AutoSize = true;
            this.lblNetSaleAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblNetSaleAmount.ForeColor = System.Drawing.Color.White;
            this.lblNetSaleAmount.Location = new System.Drawing.Point(464, 16);
            this.lblNetSaleAmount.Name = "lblNetSaleAmount";
            this.lblNetSaleAmount.Size = new System.Drawing.Size(0, 25);
            this.lblNetSaleAmount.TabIndex = 11;
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
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1001, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 4;
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
            this.btnClose.Location = new System.Drawing.Point(889, 6);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.dtpDateTo);
            this.panel2.Controls.Add(this.dtpDateFrom);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1120, 66);
            this.panel2.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(837, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(159, 47);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Searc&h";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(498, 21);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(200, 31);
            this.dtpDateTo.TabIndex = 3;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(129, 21);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(200, 31);
            this.dtpDateFrom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(408, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date From";
            // 
            // DeliveryRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 637);
            this.Controls.Add(this.panel1);
            this.Name = "DeliveryRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Delivery Request";
            this.Load += new System.EventHandler(this.DeliveryRequestForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlDelievryRequestItem.ResumeLayout(false);
            this.pnlDelievryRequestItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryRequest)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblNetAmount;
        private System.Windows.Forms.Label lblNetSaleAmount;
        private System.Windows.Forms.DataGridView dgvDeliveryRequest;
        private System.Windows.Forms.DataGridView dgvDeliveryItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetAmount;
        private System.Windows.Forms.Panel pnlDelievryRequestItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPanelClose;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequestId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRequestTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOtherDetails;
        private System.Windows.Forms.DataGridViewButtonColumn colView;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
    }
}