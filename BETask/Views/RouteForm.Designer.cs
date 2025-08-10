namespace BETask.Views
{
    partial class RouteForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGroupRoute = new System.Windows.Forms.Panel();
            this.gridSubRoute = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGroupSave = new System.Windows.Forms.Button();
            this.btnGroupClose = new System.Windows.Forms.Button();
            this.gridRoutes = new System.Windows.Forms.DataGridView();
            this.clmRouteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRouteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkGroupRoute = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtRouteName = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbRoute = new System.Windows.Forms.RadioButton();
            this.rbBuilding = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.clmCheckSubRoute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmSubRouteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSubRouteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pnlGroupRoute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubRoute)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoutes)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlGroupRoute);
            this.panel1.Controls.Add(this.gridRoutes);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 633);
            this.panel1.TabIndex = 0;
            // 
            // pnlGroupRoute
            // 
            this.pnlGroupRoute.Controls.Add(this.gridSubRoute);
            this.pnlGroupRoute.Controls.Add(this.panel6);
            this.pnlGroupRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGroupRoute.Location = new System.Drawing.Point(0, 77);
            this.pnlGroupRoute.Name = "pnlGroupRoute";
            this.pnlGroupRoute.Size = new System.Drawing.Size(769, 486);
            this.pnlGroupRoute.TabIndex = 4;
            this.pnlGroupRoute.Visible = false;
            // 
            // gridSubRoute
            // 
            this.gridSubRoute.AllowUserToAddRows = false;
            this.gridSubRoute.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridSubRoute.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSubRoute.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSubRoute.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridSubRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSubRoute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCheckSubRoute,
            this.clmSubRouteId,
            this.clmSubRouteName});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSubRoute.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridSubRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSubRoute.Location = new System.Drawing.Point(0, 0);
            this.gridSubRoute.Name = "gridSubRoute";
            this.gridSubRoute.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridSubRoute.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridSubRoute.RowTemplate.Height = 30;
            this.gridSubRoute.Size = new System.Drawing.Size(769, 421);
            this.gridSubRoute.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGroupSave);
            this.panel6.Controls.Add(this.btnGroupClose);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 421);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(769, 65);
            this.panel6.TabIndex = 0;
            // 
            // btnGroupSave
            // 
            this.btnGroupSave.BackColor = System.Drawing.Color.Teal;
            this.btnGroupSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroupSave.ForeColor = System.Drawing.Color.White;
            this.btnGroupSave.Location = new System.Drawing.Point(651, 9);
            this.btnGroupSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGroupSave.Name = "btnGroupSave";
            this.btnGroupSave.Size = new System.Drawing.Size(107, 47);
            this.btnGroupSave.TabIndex = 29;
            this.btnGroupSave.Text = "Save";
            this.btnGroupSave.UseVisualStyleBackColor = false;
            this.btnGroupSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // btnGroupClose
            // 
            this.btnGroupClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGroupClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupClose.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGroupClose.ForeColor = System.Drawing.Color.White;
            this.btnGroupClose.Location = new System.Drawing.Point(537, 9);
            this.btnGroupClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGroupClose.Name = "btnGroupClose";
            this.btnGroupClose.Size = new System.Drawing.Size(107, 47);
            this.btnGroupClose.TabIndex = 30;
            this.btnGroupClose.Text = "Close";
            this.btnGroupClose.UseVisualStyleBackColor = false;
            this.btnGroupClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // gridRoutes
            // 
            this.gridRoutes.AllowUserToAddRows = false;
            this.gridRoutes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridRoutes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridRoutes.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRoutes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridRoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRoutes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmRouteId,
            this.clmRouteName,
            this.clmRemove});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRoutes.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridRoutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRoutes.Location = new System.Drawing.Point(0, 77);
            this.gridRoutes.Name = "gridRoutes";
            this.gridRoutes.ReadOnly = true;
            this.gridRoutes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            this.gridRoutes.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridRoutes.RowTemplate.Height = 30;
            this.gridRoutes.Size = new System.Drawing.Size(769, 486);
            this.gridRoutes.TabIndex = 3;
            this.gridRoutes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRoutes_CellClick);
            // 
            // clmRouteId
            // 
            this.clmRouteId.HeaderText = "RouteId";
            this.clmRouteId.Name = "clmRouteId";
            this.clmRouteId.ReadOnly = true;
            this.clmRouteId.Visible = false;
            this.clmRouteId.Width = 120;
            // 
            // clmRouteName
            // 
            this.clmRouteName.HeaderText = "Route";
            this.clmRouteName.Name = "clmRouteName";
            this.clmRouteName.ReadOnly = true;
            this.clmRouteName.Width = 350;
            // 
            // clmRemove
            // 
            this.clmRemove.HeaderText = "De Activate";
            this.clmRemove.Name = "clmRemove";
            this.clmRemove.ReadOnly = true;
            this.clmRemove.Width = 120;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.linkGroupRoute);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 563);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(769, 70);
            this.panel3.TabIndex = 1;
            // 
            // linkGroupRoute
            // 
            this.linkGroupRoute.AutoSize = true;
            this.linkGroupRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkGroupRoute.LinkColor = System.Drawing.Color.White;
            this.linkGroupRoute.Location = new System.Drawing.Point(11, 31);
            this.linkGroupRoute.Name = "linkGroupRoute";
            this.linkGroupRoute.Size = new System.Drawing.Size(118, 25);
            this.linkGroupRoute.TabIndex = 3;
            this.linkGroupRoute.TabStop = true;
            this.linkGroupRoute.Text = "Group Route";
            this.linkGroupRoute.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGroupRoute_LinkClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(651, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 47);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cance&l";
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
            this.btnClose.Location = new System.Drawing.Point(539, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 47);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtRouteName);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(769, 77);
            this.panel2.TabIndex = 0;
            // 
            // txtRouteName
            // 
            this.txtRouteName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRouteName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtRouteName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRouteName.Location = new System.Drawing.Point(0, 36);
            this.txtRouteName.Name = "txtRouteName";
            this.txtRouteName.Size = new System.Drawing.Size(569, 41);
            this.txtRouteName.TabIndex = 1;
            this.txtRouteName.TextChanged += new System.EventHandler(this.txtRouteName_TextChanged);
            this.txtRouteName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbRoute);
            this.panel5.Controls.Add(this.rbBuilding);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(569, 42);
            this.panel5.TabIndex = 2;
            // 
            // rbRoute
            // 
            this.rbRoute.AutoSize = true;
            this.rbRoute.Checked = true;
            this.rbRoute.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F);
            this.rbRoute.Location = new System.Drawing.Point(10, 5);
            this.rbRoute.Name = "rbRoute";
            this.rbRoute.Size = new System.Drawing.Size(81, 26);
            this.rbRoute.TabIndex = 3;
            this.rbRoute.TabStop = true;
            this.rbRoute.Text = "Route";
            this.rbRoute.UseVisualStyleBackColor = true;
            this.rbRoute.Click += new System.EventHandler(this.rbRoute_Click);
            // 
            // rbBuilding
            // 
            this.rbBuilding.AutoSize = true;
            this.rbBuilding.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F);
            this.rbBuilding.Location = new System.Drawing.Point(332, 8);
            this.rbBuilding.Name = "rbBuilding";
            this.rbBuilding.Size = new System.Drawing.Size(102, 26);
            this.rbBuilding.TabIndex = 2;
            this.rbBuilding.Text = "Building";
            this.rbBuilding.UseVisualStyleBackColor = true;
            this.rbBuilding.Visible = false;
            this.rbBuilding.Click += new System.EventHandler(this.rbBuilding_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(569, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 77);
            this.panel4.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(17, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(158, 65);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // clmCheckSubRoute
            // 
            this.clmCheckSubRoute.HeaderText = "Select";
            this.clmCheckSubRoute.Name = "clmCheckSubRoute";
            // 
            // clmSubRouteId
            // 
            this.clmSubRouteId.HeaderText = "RouteId";
            this.clmSubRouteId.Name = "clmSubRouteId";
            this.clmSubRouteId.Visible = false;
            this.clmSubRouteId.Width = 120;
            // 
            // clmSubRouteName
            // 
            this.clmSubRouteName.HeaderText = "Sub Route";
            this.clmSubRouteName.Name = "clmSubRouteName";
            this.clmSubRouteName.Width = 350;
            // 
            // RouteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 633);
            this.Controls.Add(this.panel1);
            this.Name = "RouteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Route";
            this.Load += new System.EventHandler(this.RouteForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlGroupRoute.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSubRoute)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRoutes)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtRouteName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rbRoute;
        private System.Windows.Forms.RadioButton rbBuilding;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlGroupRoute;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnGroupSave;
        private System.Windows.Forms.Button btnGroupClose;
        private System.Windows.Forms.DataGridView gridSubRoute;
        private System.Windows.Forms.DataGridView gridRoutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRouteId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRouteName;
        private System.Windows.Forms.DataGridViewButtonColumn clmRemove;
        private System.Windows.Forms.LinkLabel linkGroupRoute;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmCheckSubRoute;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSubRouteId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSubRouteName;
    }
}