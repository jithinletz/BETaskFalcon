namespace BETask.Views
{
    partial class BuildingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblResultCount = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgBuilding = new System.Windows.Forms.DataGridView();
            this.ClmBuildingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmBuildingName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRoute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClmArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDeactive = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearchBuiding = new System.Windows.Forms.TextBox();
            this.pnlSaveContent = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtBuilding = new System.Windows.Forms.TextBox();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.clbRoute = new System.Windows.Forms.CheckedListBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuilding)).BeginInit();
            this.pnlSaveContent.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1097, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(985, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cance&l";
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
            this.btnClose.Location = new System.Drawing.Point(873, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.Green;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(761, 9);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 47);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel6.Controls.Add(this.lblResultCount);
            this.panel6.Controls.Add(this.btnSave);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.btnClose);
            this.panel6.Controls.Add(this.btnNew);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 546);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1207, 62);
            this.panel6.TabIndex = 14;
            // 
            // lblResultCount
            // 
            this.lblResultCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultCount.AutoSize = true;
            this.lblResultCount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblResultCount.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.lblResultCount.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblResultCount.Location = new System.Drawing.Point(12, 18);
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(142, 25);
            this.lblResultCount.TabIndex = 102;
            this.lblResultCount.Text = "0 search results";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1207, 608);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.pnlSaveContent);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1207, 608);
            this.panel3.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgBuilding);
            this.panel2.Controls.Add(this.txtSearchBuiding);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(726, 546);
            this.panel2.TabIndex = 17;
            // 
            // dgBuilding
            // 
            this.dgBuilding.AllowUserToAddRows = false;
            this.dgBuilding.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            this.dgBuilding.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgBuilding.BackgroundColor = System.Drawing.Color.White;
            this.dgBuilding.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBuilding.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClmBuildingId,
            this.ClmBuildingName,
            this.clmRoute,
            this.ClmArea,
            this.clmDeactive,
            this.clmDelete});
            this.dgBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBuilding.Location = new System.Drawing.Point(0, 31);
            this.dgBuilding.Margin = new System.Windows.Forms.Padding(4);
            this.dgBuilding.Name = "dgBuilding";
            this.dgBuilding.ReadOnly = true;
            this.dgBuilding.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dgBuilding.RowTemplate.Height = 30;
            this.dgBuilding.Size = new System.Drawing.Size(726, 515);
            this.dgBuilding.TabIndex = 0;
            this.dgBuilding.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBuilding_CellClick);
            this.dgBuilding.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgBuilding_KeyPress);
            // 
            // ClmBuildingId
            // 
            this.ClmBuildingId.HeaderText = "ID";
            this.ClmBuildingId.Name = "ClmBuildingId";
            this.ClmBuildingId.ReadOnly = true;
            this.ClmBuildingId.Visible = false;
            this.ClmBuildingId.Width = 50;
            // 
            // ClmBuildingName
            // 
            this.ClmBuildingName.HeaderText = "Name";
            this.ClmBuildingName.Name = "ClmBuildingName";
            this.ClmBuildingName.ReadOnly = true;
            this.ClmBuildingName.Width = 150;
            // 
            // clmRoute
            // 
            this.clmRoute.HeaderText = "Route";
            this.clmRoute.Name = "clmRoute";
            this.clmRoute.ReadOnly = true;
            this.clmRoute.Width = 150;
            // 
            // ClmArea
            // 
            this.ClmArea.HeaderText = "Area";
            this.ClmArea.Name = "ClmArea";
            this.ClmArea.ReadOnly = true;
            // 
            // clmDeactive
            // 
            this.clmDeactive.HeaderText = "";
            this.clmDeactive.Name = "clmDeactive";
            this.clmDeactive.ReadOnly = true;
            // 
            // clmDelete
            // 
            this.clmDelete.HeaderText = "";
            this.clmDelete.Name = "clmDelete";
            this.clmDelete.ReadOnly = true;
            // 
            // txtSearchBuiding
            // 
            this.txtSearchBuiding.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearchBuiding.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSearchBuiding.Location = new System.Drawing.Point(0, 0);
            this.txtSearchBuiding.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchBuiding.Name = "txtSearchBuiding";
            this.txtSearchBuiding.Size = new System.Drawing.Size(726, 31);
            this.txtSearchBuiding.TabIndex = 100;
            this.txtSearchBuiding.Tag = "cSearch";
            this.txtSearchBuiding.TextChanged += new System.EventHandler(this.txtSearchBuiding_TextChanged);
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSaveContent.Controls.Add(this.panel5);
            this.pnlSaveContent.Controls.Add(this.panel4);
            this.pnlSaveContent.Controls.Add(this.chkActive);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSaveContent.Location = new System.Drawing.Point(726, 0);
            this.pnlSaveContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(481, 546);
            this.pnlSaveContent.TabIndex = 16;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtBuilding);
            this.panel5.Controls.Add(this.txtArea);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(6, -2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(463, 160);
            this.panel5.TabIndex = 42;
            // 
            // txtBuilding
            // 
            this.txtBuilding.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuilding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuilding.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtBuilding.Location = new System.Drawing.Point(21, 31);
            this.txtBuilding.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuilding.Name = "txtBuilding";
            this.txtBuilding.Size = new System.Drawing.Size(415, 31);
            this.txtBuilding.TabIndex = 39;
            // 
            // txtArea
            // 
            this.txtArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArea.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtArea.Location = new System.Drawing.Point(22, 86);
            this.txtArea.Margin = new System.Windows.Forms.Padding(4);
            this.txtArea.Multiline = true;
            this.txtArea.Name = "txtArea";
            this.txtArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtArea.Size = new System.Drawing.Size(435, 72);
            this.txtArea.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(17, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 40;
            this.label1.Text = "Building Name";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(16, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "City/Area";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.clbRoute);
            this.panel4.Location = new System.Drawing.Point(6, 164);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(463, 385);
            this.panel4.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(17, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 39;
            this.label2.Text = "Route";
            // 
            // clbRoute
            // 
            this.clbRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbRoute.CheckOnClick = true;
            this.clbRoute.Font = new System.Drawing.Font("Segoe UI", 12.8F);
            this.clbRoute.FormattingEnabled = true;
            this.clbRoute.Location = new System.Drawing.Point(22, 28);
            this.clbRoute.Name = "clbRoute";
            this.clbRoute.Size = new System.Drawing.Size(414, 314);
            this.clbRoute.TabIndex = 0;
            this.clbRoute.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbRoute_ItemCheck);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkActive.Location = new System.Drawing.Point(673, 619);
            this.chkActive.Margin = new System.Windows.Forms.Padding(4);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(85, 29);
            this.chkActive.TabIndex = 16;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // BuildingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 608);
            this.Controls.Add(this.panel1);
            this.Name = "BuildingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Building";
            this.Text = "Building";
            this.Load += new System.EventHandler(this.BuildingForm_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBuilding)).EndInit();
            this.pnlSaveContent.ResumeLayout(false);
            this.pnlSaveContent.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbRoute;
        private System.Windows.Forms.TextBox txtBuilding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblResultCount;
        private System.Windows.Forms.DataGridView dgBuilding;
        private System.Windows.Forms.TextBox txtSearchBuiding;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmBuildingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmBuildingName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClmArea;
        private System.Windows.Forms.DataGridViewButtonColumn clmDeactive;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDelete;
    }
}