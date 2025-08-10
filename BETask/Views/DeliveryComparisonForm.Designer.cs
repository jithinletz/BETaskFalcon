
namespace BETask.Views
{
    partial class DeliveryComparisonForm
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
            this.gridAppDeliveryData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridLocalDeliveryData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridAppDeliveryData)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLocalDeliveryData)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAppDeliveryData
            // 
            this.gridAppDeliveryData.AllowUserToAddRows = false;
            this.gridAppDeliveryData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridAppDeliveryData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridAppDeliveryData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAppDeliveryData.BackgroundColor = System.Drawing.Color.White;
            this.gridAppDeliveryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAppDeliveryData.Location = new System.Drawing.Point(800, 0);
            this.gridAppDeliveryData.Name = "gridAppDeliveryData";
            this.gridAppDeliveryData.ReadOnly = true;
            this.gridAppDeliveryData.RowHeadersWidth = 51;
            this.gridAppDeliveryData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridAppDeliveryData.RowTemplate.Height = 30;
            this.gridAppDeliveryData.Size = new System.Drawing.Size(779, 682);
            this.gridAppDeliveryData.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridAppDeliveryData);
            this.panel1.Controls.Add(this.gridLocalDeliveryData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1579, 676);
            this.panel1.TabIndex = 1;
            // 
            // gridLocalDeliveryData
            // 
            this.gridLocalDeliveryData.AllowUserToAddRows = false;
            this.gridLocalDeliveryData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLocalDeliveryData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridLocalDeliveryData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLocalDeliveryData.BackgroundColor = System.Drawing.Color.White;
            this.gridLocalDeliveryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLocalDeliveryData.Location = new System.Drawing.Point(0, 0);
            this.gridLocalDeliveryData.Name = "gridLocalDeliveryData";
            this.gridLocalDeliveryData.ReadOnly = true;
            this.gridLocalDeliveryData.RowHeadersWidth = 51;
            this.gridLocalDeliveryData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gridLocalDeliveryData.RowTemplate.Height = 30;
            this.gridLocalDeliveryData.Size = new System.Drawing.Size(864, 682);
            this.gridLocalDeliveryData.TabIndex = 7;
            // 
            // DeliveryComparisonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 676);
            this.Controls.Add(this.panel1);
            this.Name = "DeliveryComparisonForm";
            this.Text = "DeliveryComparisonForm";
            this.Load += new System.EventHandler(this.DeliveryComparisonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridAppDeliveryData)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLocalDeliveryData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridAppDeliveryData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridLocalDeliveryData;
    }
}