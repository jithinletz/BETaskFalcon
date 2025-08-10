namespace BETask.Views
{
    partial class CostCenterSearchForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPrimaryCostCenter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCostCenter = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(64, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Primary Cost Center";
            // 
            // cmbPrimaryCostCenter
            // 
            this.cmbPrimaryCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPrimaryCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrimaryCostCenter.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbPrimaryCostCenter.FormattingEnabled = true;
            this.cmbPrimaryCostCenter.Location = new System.Drawing.Point(69, 50);
            this.cmbPrimaryCostCenter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPrimaryCostCenter.Name = "cmbPrimaryCostCenter";
            this.cmbPrimaryCostCenter.Size = new System.Drawing.Size(261, 33);
            this.cmbPrimaryCostCenter.TabIndex = 0;
            this.cmbPrimaryCostCenter.SelectedIndexChanged += new System.EventHandler(this.cmbPrimaryCostCenter_SelectedIndexChanged);
            this.cmbPrimaryCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(333, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cost Center";
            // 
            // cmbCostCenter
            // 
            this.cmbCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCostCenter.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbCostCenter.FormattingEnabled = true;
            this.cmbCostCenter.Location = new System.Drawing.Point(338, 50);
            this.cmbCostCenter.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCostCenter.Name = "cmbCostCenter";
            this.cmbCostCenter.Size = new System.Drawing.Size(276, 33);
            this.cmbCostCenter.TabIndex = 1;
            this.cmbCostCenter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(661, 36);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 104);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.cmbCostCenter);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbPrimaryCostCenter);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(865, 109);
            this.panel2.TabIndex = 0;
            // 
            // CostCenterSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 104);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "CostCenterSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cost Center Search";
            this.Load += new System.EventHandler(this.CostCenterEntryForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CostCenterEntryForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPrimaryCostCenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCostCenter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}