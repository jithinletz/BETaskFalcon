namespace BETask.Views
{
    partial class SynchronizationForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWalletSync = new System.Windows.Forms.Button();
            this.lblWalletStatus = new System.Windows.Forms.Label();
            this.btnBuilding = new System.Windows.Forms.Button();
            this.btnWalletGeneration = new System.Windows.Forms.Button();
            this.btnCustomerDateUpdation = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.btnDisableLocation = new System.Windows.Forms.Button();
            this.btnEnableLocation = new System.Windows.Forms.Button();
            this.btnRouteCustomerOutstanding = new System.Windows.Forms.Button();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.btnCoupon = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnDelivery = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnDeliveryReturnItems = new System.Windows.Forms.Button();
            this.btnCustomerCollection = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnRoute = new System.Windows.Forms.Button();
            this.btnCustomerOutstanding = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnUpdateCreditLimit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.btnWalletSync);
            this.panel1.Controls.Add(this.lblWalletStatus);
            this.panel1.Controls.Add(this.btnBuilding);
            this.panel1.Controls.Add(this.btnWalletGeneration);
            this.panel1.Controls.Add(this.btnCustomerDateUpdation);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnCoupon);
            this.panel1.Controls.Add(this.dtpDate);
            this.panel1.Controls.Add(this.btnDelivery);
            this.panel1.Controls.Add(this.btnBackup);
            this.panel1.Controls.Add(this.btnDeliveryReturnItems);
            this.panel1.Controls.Add(this.btnCustomerCollection);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnRoute);
            this.panel1.Controls.Add(this.btnCustomerOutstanding);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 515);
            this.panel1.TabIndex = 0;
            // 
            // btnWalletSync
            // 
            this.btnWalletSync.Location = new System.Drawing.Point(537, 201);
            this.btnWalletSync.Name = "btnWalletSync";
            this.btnWalletSync.Size = new System.Drawing.Size(149, 63);
            this.btnWalletSync.TabIndex = 80;
            this.btnWalletSync.Text = "Wallet Synch";
            this.btnWalletSync.UseVisualStyleBackColor = true;
            this.btnWalletSync.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // lblWalletStatus
            // 
            this.lblWalletStatus.AutoSize = true;
            this.lblWalletStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblWalletStatus.ForeColor = System.Drawing.Color.Maroon;
            this.lblWalletStatus.Location = new System.Drawing.Point(516, 182);
            this.lblWalletStatus.Name = "lblWalletStatus";
            this.lblWalletStatus.Size = new System.Drawing.Size(0, 18);
            this.lblWalletStatus.TabIndex = 79;
            // 
            // btnBuilding
            // 
            this.btnBuilding.Location = new System.Drawing.Point(361, 201);
            this.btnBuilding.Name = "btnBuilding";
            this.btnBuilding.Size = new System.Drawing.Size(149, 63);
            this.btnBuilding.TabIndex = 78;
            this.btnBuilding.Text = "Building";
            this.btnBuilding.UseVisualStyleBackColor = true;
            this.btnBuilding.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnWalletGeneration
            // 
            this.btnWalletGeneration.Location = new System.Drawing.Point(537, 116);
            this.btnWalletGeneration.Name = "btnWalletGeneration";
            this.btnWalletGeneration.Size = new System.Drawing.Size(149, 63);
            this.btnWalletGeneration.TabIndex = 77;
            this.btnWalletGeneration.Text = "Wallet Generation";
            this.btnWalletGeneration.UseVisualStyleBackColor = true;
            this.btnWalletGeneration.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCustomerDateUpdation
            // 
            this.btnCustomerDateUpdation.Location = new System.Drawing.Point(361, 116);
            this.btnCustomerDateUpdation.Name = "btnCustomerDateUpdation";
            this.btnCustomerDateUpdation.Size = new System.Drawing.Size(149, 63);
            this.btnCustomerDateUpdation.TabIndex = 77;
            this.btnCustomerDateUpdation.Text = "Customer Date Updation";
            this.btnCustomerDateUpdation.UseVisualStyleBackColor = true;
            this.btnCustomerDateUpdation.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.btnUpdateCreditLimit);
            this.panel3.Controls.Add(this.txtRange);
            this.panel3.Controls.Add(this.btnDisableLocation);
            this.panel3.Controls.Add(this.btnEnableLocation);
            this.panel3.Controls.Add(this.btnRouteCustomerOutstanding);
            this.panel3.Controls.Add(this.cmbRoute);
            this.panel3.Location = new System.Drawing.Point(12, 324);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(674, 128);
            this.panel3.TabIndex = 76;
            // 
            // txtRange
            // 
            this.txtRange.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtRange.Location = new System.Drawing.Point(538, 7);
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(125, 31);
            this.txtRange.TabIndex = 78;
            this.txtRange.Text = "[5000]";
            // 
            // btnDisableLocation
            // 
            this.btnDisableLocation.Location = new System.Drawing.Point(290, 46);
            this.btnDisableLocation.Name = "btnDisableLocation";
            this.btnDisableLocation.Size = new System.Drawing.Size(107, 63);
            this.btnDisableLocation.TabIndex = 77;
            this.btnDisableLocation.Text = "Disable Location";
            this.btnDisableLocation.UseVisualStyleBackColor = true;
            this.btnDisableLocation.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnEnableLocation
            // 
            this.btnEnableLocation.Location = new System.Drawing.Point(177, 46);
            this.btnEnableLocation.Name = "btnEnableLocation";
            this.btnEnableLocation.Size = new System.Drawing.Size(107, 63);
            this.btnEnableLocation.TabIndex = 76;
            this.btnEnableLocation.Text = "Enable Location";
            this.btnEnableLocation.UseVisualStyleBackColor = true;
            this.btnEnableLocation.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnRouteCustomerOutstanding
            // 
            this.btnRouteCustomerOutstanding.Location = new System.Drawing.Point(19, 46);
            this.btnRouteCustomerOutstanding.Name = "btnRouteCustomerOutstanding";
            this.btnRouteCustomerOutstanding.Size = new System.Drawing.Size(152, 63);
            this.btnRouteCustomerOutstanding.TabIndex = 7;
            this.btnRouteCustomerOutstanding.Text = "Route Customer Outstanding";
            this.btnRouteCustomerOutstanding.UseVisualStyleBackColor = true;
            this.btnRouteCustomerOutstanding.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(19, 7);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(513, 33);
            this.cmbRoute.TabIndex = 75;
            // 
            // btnCoupon
            // 
            this.btnCoupon.Location = new System.Drawing.Point(184, 116);
            this.btnCoupon.Name = "btnCoupon";
            this.btnCoupon.Size = new System.Drawing.Size(149, 63);
            this.btnCoupon.TabIndex = 6;
            this.btnCoupon.Text = "Coupon Redeedmed";
            this.btnCoupon.UseVisualStyleBackColor = true;
            this.btnCoupon.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(12, 91);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(149, 22);
            this.dtpDate.TabIndex = 5;
            // 
            // btnDelivery
            // 
            this.btnDelivery.Location = new System.Drawing.Point(12, 116);
            this.btnDelivery.Name = "btnDelivery";
            this.btnDelivery.Size = new System.Drawing.Size(149, 63);
            this.btnDelivery.TabIndex = 4;
            this.btnDelivery.Text = "Delivery by date";
            this.btnDelivery.UseVisualStyleBackColor = true;
            this.btnDelivery.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(12, 201);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(149, 63);
            this.btnBackup.TabIndex = 3;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnDeliveryReturnItems
            // 
            this.btnDeliveryReturnItems.Location = new System.Drawing.Point(537, 22);
            this.btnDeliveryReturnItems.Name = "btnDeliveryReturnItems";
            this.btnDeliveryReturnItems.Size = new System.Drawing.Size(149, 63);
            this.btnDeliveryReturnItems.TabIndex = 3;
            this.btnDeliveryReturnItems.Text = "Return Items";
            this.btnDeliveryReturnItems.UseVisualStyleBackColor = true;
            this.btnDeliveryReturnItems.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCustomerCollection
            // 
            this.btnCustomerCollection.Location = new System.Drawing.Point(361, 22);
            this.btnCustomerCollection.Name = "btnCustomerCollection";
            this.btnCustomerCollection.Size = new System.Drawing.Size(149, 63);
            this.btnCustomerCollection.TabIndex = 2;
            this.btnCustomerCollection.Text = "Customer Collection";
            this.btnCustomerCollection.UseVisualStyleBackColor = true;
            this.btnCustomerCollection.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 477);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(742, 38);
            this.panel2.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.Maroon;
            this.lblResult.Location = new System.Drawing.Point(7, 4);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(70, 25);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "label1";
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(184, 22);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(149, 63);
            this.btnRoute.TabIndex = 0;
            this.btnRoute.Text = "Route";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCustomerOutstanding
            // 
            this.btnCustomerOutstanding.Location = new System.Drawing.Point(3, 22);
            this.btnCustomerOutstanding.Name = "btnCustomerOutstanding";
            this.btnCustomerOutstanding.Size = new System.Drawing.Size(149, 63);
            this.btnCustomerOutstanding.TabIndex = 0;
            this.btnCustomerOutstanding.Text = "Customer Outstanding";
            this.btnCustomerOutstanding.UseVisualStyleBackColor = true;
            this.btnCustomerOutstanding.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnUpdateCreditLimit
            // 
            this.btnUpdateCreditLimit.Location = new System.Drawing.Point(403, 46);
            this.btnUpdateCreditLimit.Name = "btnUpdateCreditLimit";
            this.btnUpdateCreditLimit.Size = new System.Drawing.Size(107, 63);
            this.btnUpdateCreditLimit.TabIndex = 79;
            this.btnUpdateCreditLimit.Text = "Update Credit Limit";
            this.btnUpdateCreditLimit.UseVisualStyleBackColor = true;
            this.btnUpdateCreditLimit.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // SynchronizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 515);
            this.Controls.Add(this.panel1);
            this.Name = "SynchronizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synchronization";
            this.Load += new System.EventHandler(this.SynchronizationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCustomerOutstanding;
        private System.Windows.Forms.Button btnRoute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnCustomerCollection;
        private System.Windows.Forms.Button btnDeliveryReturnItems;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnDelivery;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnCoupon;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnRouteCustomerOutstanding;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Button btnCustomerDateUpdation;
        private System.Windows.Forms.Button btnWalletGeneration;
        private System.Windows.Forms.Button btnBuilding;
        private System.Windows.Forms.Label lblWalletStatus;
        private System.Windows.Forms.Button btnWalletSync;
        private System.Windows.Forms.Button btnDisableLocation;
        private System.Windows.Forms.Button btnEnableLocation;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.Button btnUpdateCreditLimit;
    }
}