namespace BETask.Views
{
    partial class EmployeeForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSaveContent = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCompany = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbRouteUser = new System.Windows.Forms.RadioButton();
            this.rdbEmployee = new System.Windows.Forms.RadioButton();
            this.txtDesignation = new System.Windows.Forms.ComboBox();
            this.txtSalesmanCredit = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dtpResign = new System.Windows.Forms.DateTimePicker();
            this.chkWorking = new System.Windows.Forms.CheckBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.dtpJoin = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtVehicle = new System.Windows.Forms.TextBox();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNationalId = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassport = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtHelper = new System.Windows.Forms.TextBox();
            this.txtVisa = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabAddress = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtOtherdetails = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNationality = new System.Windows.Forms.TextBox();
            this.txtSate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.linkSalesmanCreditStatement = new System.Windows.Forms.LinkLabel();
            this.linkUpdateCustomerLedger = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.gridEmployee = new System.Windows.Forms.DataGridView();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.pnlSaveContent.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCompany.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabAddress.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pnlSaveContent);
            this.panel1.Controls.Add(this.pnlButton);
            this.panel1.Controls.Add(this.pnlLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 717);
            this.panel1.TabIndex = 0;
            // 
            // pnlSaveContent
            // 
            this.pnlSaveContent.Controls.Add(this.tabControl1);
            this.pnlSaveContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaveContent.Enabled = false;
            this.pnlSaveContent.Location = new System.Drawing.Point(351, 0);
            this.pnlSaveContent.Name = "pnlSaveContent";
            this.pnlSaveContent.Size = new System.Drawing.Size(798, 656);
            this.pnlSaveContent.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCompany);
            this.tabControl1.Controls.Add(this.tabAddress);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 656);
            this.tabControl1.TabIndex = 11;
            // 
            // tabCompany
            // 
            this.tabCompany.Controls.Add(this.panel2);
            this.tabCompany.Location = new System.Drawing.Point(4, 25);
            this.tabCompany.Name = "tabCompany";
            this.tabCompany.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompany.Size = new System.Drawing.Size(790, 627);
            this.tabCompany.TabIndex = 0;
            this.tabCompany.Text = "Company Details";
            this.tabCompany.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbRouteUser);
            this.panel2.Controls.Add(this.rdbEmployee);
            this.panel2.Controls.Add(this.txtDesignation);
            this.panel2.Controls.Add(this.txtSalesmanCredit);
            this.panel2.Controls.Add(this.label23);
            this.panel2.Controls.Add(this.cmbRoute);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.dtpResign);
            this.panel2.Controls.Add(this.chkWorking);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.txtFirstname);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtUsername);
            this.panel2.Controls.Add(this.dtpJoin);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.txtLastname);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.txtVehicle);
            this.panel2.Controls.Add(this.txtDepartment);
            this.panel2.Controls.Add(this.txtSalary);
            this.panel2.Controls.Add(this.txtPhone);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtNationalId);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtPassport);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtHelper);
            this.panel2.Controls.Add(this.txtVisa);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 621);
            this.panel2.TabIndex = 16;
            // 
            // rdbRouteUser
            // 
            this.rdbRouteUser.AutoSize = true;
            this.rdbRouteUser.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.rdbRouteUser.Location = new System.Drawing.Point(167, 19);
            this.rdbRouteUser.Name = "rdbRouteUser";
            this.rdbRouteUser.Size = new System.Drawing.Size(131, 29);
            this.rdbRouteUser.TabIndex = 76;
            this.rdbRouteUser.TabStop = true;
            this.rdbRouteUser.Text = "Route User";
            this.rdbRouteUser.UseVisualStyleBackColor = true;
            this.rdbRouteUser.CheckedChanged += new System.EventHandler(this.rdbRouteUser_CheckedChanged);
            // 
            // rdbEmployee
            // 
            this.rdbEmployee.AutoSize = true;
            this.rdbEmployee.Checked = true;
            this.rdbEmployee.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.rdbEmployee.Location = new System.Drawing.Point(17, 19);
            this.rdbEmployee.Name = "rdbEmployee";
            this.rdbEmployee.Size = new System.Drawing.Size(119, 29);
            this.rdbEmployee.TabIndex = 75;
            this.rdbEmployee.TabStop = true;
            this.rdbEmployee.Text = "Employee";
            this.rdbEmployee.UseVisualStyleBackColor = true;
            // 
            // txtDesignation
            // 
            this.txtDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtDesignation.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDesignation.FormattingEnabled = true;
            this.txtDesignation.Items.AddRange(new object[] {
            "Delivery",
            "Helper",
            "Executive",
            "Office"});
            this.txtDesignation.Location = new System.Drawing.Point(502, 353);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(218, 33);
            this.txtDesignation.TabIndex = 74;
            // 
            // txtSalesmanCredit
            // 
            this.txtSalesmanCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalesmanCredit.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSalesmanCredit.Location = new System.Drawing.Point(167, 523);
            this.txtSalesmanCredit.MaxLength = 150;
            this.txtSalesmanCredit.Name = "txtSalesmanCredit";
            this.txtSalesmanCredit.Size = new System.Drawing.Size(553, 31);
            this.txtSalesmanCredit.TabIndex = 15;
            this.txtSalesmanCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label23.Location = new System.Drawing.Point(17, 525);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(148, 25);
            this.label23.TabIndex = 73;
            this.label23.Text = "Salesman Credit";
            // 
            // cmbRoute
            // 
            this.cmbRoute.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(167, 477);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(553, 33);
            this.cmbRoute.TabIndex = 14;
            this.cmbRoute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label18.Location = new System.Drawing.Point(17, 480);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(133, 25);
            this.label18.TabIndex = 72;
            this.label18.Text = "Delivery Route";
            // 
            // dtpResign
            // 
            this.dtpResign.CustomFormat = "dd/MM/yyyy";
            this.dtpResign.Enabled = false;
            this.dtpResign.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpResign.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpResign.Location = new System.Drawing.Point(499, 71);
            this.dtpResign.Name = "dtpResign";
            this.dtpResign.Size = new System.Drawing.Size(221, 31);
            this.dtpResign.TabIndex = 19;
            this.dtpResign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // chkWorking
            // 
            this.chkWorking.AutoSize = true;
            this.chkWorking.Checked = true;
            this.chkWorking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWorking.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.chkWorking.Location = new System.Drawing.Point(388, 73);
            this.chkWorking.Name = "chkWorking";
            this.chkWorking.Size = new System.Drawing.Size(105, 29);
            this.chkWorking.TabIndex = 18;
            this.chkWorking.Text = "Working";
            this.chkWorking.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtCode.Location = new System.Drawing.Point(167, 71);
            this.txtCode.MaxLength = 25;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(195, 31);
            this.txtCode.TabIndex = 0;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label22.Location = new System.Drawing.Point(17, 73);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(132, 25);
            this.label22.TabIndex = 17;
            this.label22.Text = "Emloyee Code";
            // 
            // txtFirstname
            // 
            this.txtFirstname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstname.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtFirstname.Location = new System.Drawing.Point(167, 112);
            this.txtFirstname.MaxLength = 150;
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(553, 31);
            this.txtFirstname.TabIndex = 1;
            this.txtFirstname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label21.Location = new System.Drawing.Point(17, 434);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(97, 25);
            this.label21.TabIndex = 15;
            this.label21.Text = "Username";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label9.Location = new System.Drawing.Point(17, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 25);
            this.label9.TabIndex = 2;
            this.label9.Text = "First Name";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtUsername.Location = new System.Drawing.Point(167, 434);
            this.txtUsername.MaxLength = 15;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(195, 31);
            this.txtUsername.TabIndex = 12;
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // dtpJoin
            // 
            this.dtpJoin.CustomFormat = "dd/MM/yyyy";
            this.dtpJoin.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpJoin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpJoin.Location = new System.Drawing.Point(167, 393);
            this.dtpJoin.Name = "dtpJoin";
            this.dtpJoin.Size = new System.Drawing.Size(195, 31);
            this.dtpJoin.TabIndex = 10;
            this.dtpJoin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label24.Location = new System.Drawing.Point(17, 565);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 25);
            this.label24.TabIndex = 15;
            this.label24.Text = "Vehicle";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label16.Location = new System.Drawing.Point(17, 349);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 25);
            this.label16.TabIndex = 15;
            this.label16.Text = "Department";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label14.Location = new System.Drawing.Point(380, 393);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 25);
            this.label14.TabIndex = 12;
            this.label14.Text = "Salary";
            // 
            // txtLastname
            // 
            this.txtLastname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastname.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtLastname.Location = new System.Drawing.Point(167, 150);
            this.txtLastname.MaxLength = 150;
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(553, 31);
            this.txtLastname.TabIndex = 2;
            this.txtLastname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label15.Location = new System.Drawing.Point(17, 393);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 25);
            this.label15.TabIndex = 12;
            this.label15.Text = "Join Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.Location = new System.Drawing.Point(17, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last Name";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label20.Location = new System.Drawing.Point(380, 434);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(91, 25);
            this.label20.TabIndex = 12;
            this.label20.Text = "Password";
            // 
            // txtVehicle
            // 
            this.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVehicle.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtVehicle.Location = new System.Drawing.Point(167, 565);
            this.txtVehicle.MaxLength = 25;
            this.txtVehicle.Name = "txtVehicle";
            this.txtVehicle.Size = new System.Drawing.Size(195, 31);
            this.txtVehicle.TabIndex = 17;
            this.txtVehicle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtDepartment
            // 
            this.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtDepartment.Location = new System.Drawing.Point(167, 349);
            this.txtDepartment.MaxLength = 25;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(195, 31);
            this.txtDepartment.TabIndex = 8;
            this.txtDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtSalary
            // 
            this.txtSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalary.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSalary.Location = new System.Drawing.Point(502, 393);
            this.txtSalary.MaxLength = 11;
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(218, 31);
            this.txtSalary.TabIndex = 11;
            this.txtSalary.Tag = "Dec";
            this.txtSalary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            this.txtSalary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            this.txtSalary.Validated += new System.EventHandler(this.ValidateDecimalPercision);
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPhone.Location = new System.Drawing.Point(167, 187);
            this.txtPhone.MaxLength = 50;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(553, 31);
            this.txtPhone.TabIndex = 3;
            this.txtPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPassword.Location = new System.Drawing.Point(502, 434);
            this.txtPassword.MaxLength = 15;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(218, 31);
            this.txtPassword.TabIndex = 13;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label7.Location = new System.Drawing.Point(17, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 25);
            this.label7.TabIndex = 7;
            this.label7.Text = "Phone";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtEmail.Location = new System.Drawing.Point(167, 227);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(553, 31);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label8.Location = new System.Drawing.Point(17, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Email";
            // 
            // txtNationalId
            // 
            this.txtNationalId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNationalId.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNationalId.Location = new System.Drawing.Point(167, 268);
            this.txtNationalId.MaxLength = 50;
            this.txtNationalId.Name = "txtNationalId";
            this.txtNationalId.Size = new System.Drawing.Size(553, 31);
            this.txtNationalId.TabIndex = 5;
            this.txtNationalId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label25.Location = new System.Drawing.Point(376, 567);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(68, 25);
            this.label25.TabIndex = 12;
            this.label25.Text = "Helper";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label13.Location = new System.Drawing.Point(380, 351);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 25);
            this.label13.TabIndex = 12;
            this.label13.Text = "Designation";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label10.Location = new System.Drawing.Point(17, 270);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 25);
            this.label10.TabIndex = 9;
            this.label10.Text = "National ID";
            // 
            // txtPassport
            // 
            this.txtPassport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassport.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtPassport.Location = new System.Drawing.Point(167, 310);
            this.txtPassport.MaxLength = 50;
            this.txtPassport.Name = "txtPassport";
            this.txtPassport.Size = new System.Drawing.Size(195, 31);
            this.txtPassport.TabIndex = 6;
            this.txtPassport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label11.Location = new System.Drawing.Point(17, 312);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 25);
            this.label11.TabIndex = 7;
            this.label11.Text = "Passport";
            // 
            // txtHelper
            // 
            this.txtHelper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHelper.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtHelper.Location = new System.Drawing.Point(502, 565);
            this.txtHelper.MaxLength = 50;
            this.txtHelper.Name = "txtHelper";
            this.txtHelper.Size = new System.Drawing.Size(218, 31);
            this.txtHelper.TabIndex = 18;
            this.txtHelper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtVisa
            // 
            this.txtVisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVisa.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtVisa.Location = new System.Drawing.Point(502, 310);
            this.txtVisa.MaxLength = 50;
            this.txtVisa.Name = "txtVisa";
            this.txtVisa.Size = new System.Drawing.Size(218, 31);
            this.txtVisa.TabIndex = 7;
            this.txtVisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label12.Location = new System.Drawing.Point(380, 312);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 25);
            this.label12.TabIndex = 7;
            this.label12.Text = "Visa";
            // 
            // tabAddress
            // 
            this.tabAddress.Controls.Add(this.panel3);
            this.tabAddress.Location = new System.Drawing.Point(4, 25);
            this.tabAddress.Name = "tabAddress";
            this.tabAddress.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddress.Size = new System.Drawing.Size(738, 627);
            this.tabAddress.TabIndex = 1;
            this.tabAddress.Text = "Personal Details";
            this.tabAddress.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtpDOB);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.cmbGender);
            this.panel3.Controls.Add(this.txtOtherdetails);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtAddress1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtAddress2);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtNationality);
            this.panel3.Controls.Add(this.txtSate);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(732, 621);
            this.panel3.TabIndex = 19;
            // 
            // dtpDOB
            // 
            this.dtpDOB.CustomFormat = "dd/MM/yyyy";
            this.dtpDOB.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOB.Location = new System.Drawing.Point(507, 42);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(257, 31);
            this.dtpDOB.TabIndex = 1;
            this.dtpDOB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label17.Location = new System.Drawing.Point(394, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 25);
            this.label17.TabIndex = 19;
            this.label17.Text = "DOB";
            // 
            // cmbGender
            // 
            this.cmbGender.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.cmbGender.Location = new System.Drawing.Point(132, 44);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(246, 33);
            this.cmbGender.TabIndex = 0;
            this.cmbGender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtOtherdetails
            // 
            this.txtOtherdetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherdetails.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtOtherdetails.Location = new System.Drawing.Point(135, 398);
            this.txtOtherdetails.MaxLength = 200;
            this.txtOtherdetails.Multiline = true;
            this.txtOtherdetails.Name = "txtOtherdetails";
            this.txtOtherdetails.Size = new System.Drawing.Size(631, 87);
            this.txtOtherdetails.TabIndex = 6;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label19.Location = new System.Drawing.Point(7, 400);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(122, 25);
            this.label19.TabIndex = 17;
            this.label19.Text = "Other Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Gender";
            // 
            // txtAddress1
            // 
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtAddress1.Location = new System.Drawing.Point(135, 160);
            this.txtAddress1.MaxLength = 200;
            this.txtAddress1.Multiline = true;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(631, 94);
            this.txtAddress1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.Location = new System.Drawing.Point(7, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Address1";
            // 
            // txtAddress2
            // 
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtAddress2.Location = new System.Drawing.Point(137, 277);
            this.txtAddress2.MaxLength = 200;
            this.txtAddress2.Multiline = true;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(631, 100);
            this.txtAddress2.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label5.Location = new System.Drawing.Point(5, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "State";
            // 
            // txtNationality
            // 
            this.txtNationality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNationality.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNationality.Location = new System.Drawing.Point(507, 102);
            this.txtNationality.MaxLength = 25;
            this.txtNationality.Name = "txtNationality";
            this.txtNationality.Size = new System.Drawing.Size(257, 31);
            this.txtNationality.TabIndex = 3;
            this.txtNationality.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // txtSate
            // 
            this.txtSate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSate.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSate.Location = new System.Drawing.Point(133, 102);
            this.txtSate.MaxLength = 25;
            this.txtSate.Name = "txtSate";
            this.txtSate.Size = new System.Drawing.Size(245, 31);
            this.txtSate.TabIndex = 2;
            this.txtSate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NextFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label6.Location = new System.Drawing.Point(394, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Nationality";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label4.Location = new System.Drawing.Point(9, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Address2";
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlButton.Controls.Add(this.linkSalesmanCreditStatement);
            this.pnlButton.Controls.Add(this.linkUpdateCustomerLedger);
            this.pnlButton.Controls.Add(this.btnSave);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnClose);
            this.pnlButton.Controls.Add(this.btnNew);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(351, 656);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(798, 61);
            this.pnlButton.TabIndex = 1;
            // 
            // linkSalesmanCreditStatement
            // 
            this.linkSalesmanCreditStatement.AutoSize = true;
            this.linkSalesmanCreditStatement.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkSalesmanCreditStatement.LinkColor = System.Drawing.Color.White;
            this.linkSalesmanCreditStatement.Location = new System.Drawing.Point(193, 18);
            this.linkSalesmanCreditStatement.Name = "linkSalesmanCreditStatement";
            this.linkSalesmanCreditStatement.Size = new System.Drawing.Size(96, 25);
            this.linkSalesmanCreditStatement.TabIndex = 75;
            this.linkSalesmanCreditStatement.TabStop = true;
            this.linkSalesmanCreditStatement.Text = "Statement";
            this.linkSalesmanCreditStatement.Visible = false;
            this.linkSalesmanCreditStatement.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkUpdateCustomerLedger
            // 
            this.linkUpdateCustomerLedger.AutoSize = true;
            this.linkUpdateCustomerLedger.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.linkUpdateCustomerLedger.LinkColor = System.Drawing.Color.White;
            this.linkUpdateCustomerLedger.Location = new System.Drawing.Point(15, 18);
            this.linkUpdateCustomerLedger.Name = "linkUpdateCustomerLedger";
            this.linkUpdateCustomerLedger.Size = new System.Drawing.Size(164, 25);
            this.linkUpdateCustomerLedger.TabIndex = 4;
            this.linkUpdateCustomerLedger.TabStop = true;
            this.linkUpdateCustomerLedger.Text = "Update customers";
            this.linkUpdateCustomerLedger.Visible = false;
            this.linkUpdateCustomerLedger.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUpdateCustomerLedger_LinkClicked);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(670, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 47);
            this.btnSave.TabIndex = 0;
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
            this.btnCancel.Location = new System.Drawing.Point(558, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 47);
            this.btnCancel.TabIndex = 1;
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
            this.btnClose.Location = new System.Drawing.Point(446, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 47);
            this.btnClose.TabIndex = 2;
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
            this.btnNew.Location = new System.Drawing.Point(334, 8);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 47);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.ButtonEvents);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gridEmployee);
            this.pnlLeft.Controls.Add(this.txtSearch);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(351, 717);
            this.pnlLeft.TabIndex = 0;
            // 
            // gridEmployee
            // 
            this.gridEmployee.AllowUserToAddRows = false;
            this.gridEmployee.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.gridEmployee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridEmployee.BackgroundColor = System.Drawing.Color.White;
            this.gridEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmId,
            this.clmName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridEmployee.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmployee.Location = new System.Drawing.Point(0, 31);
            this.gridEmployee.Name = "gridEmployee";
            this.gridEmployee.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridEmployee.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridEmployee.RowHeadersVisible = false;
            this.gridEmployee.RowTemplate.Height = 24;
            this.gridEmployee.Size = new System.Drawing.Size(351, 686);
            this.gridEmployee.TabIndex = 2;
            this.gridEmployee.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEmployee_CellClick);
            this.gridEmployee.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEmployee_CellClick);
            // 
            // clmId
            // 
            this.clmId.HeaderText = "Id";
            this.clmId.Name = "clmId";
            this.clmId.ReadOnly = true;
            // 
            // clmName
            // 
            this.clmName.HeaderText = "Employee";
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            this.clmName.Width = 200;
            // 
            // txtSearch
            // 
            this.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(351, 31);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 717);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnlSaveContent.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabCompany.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabAddress.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.pnlButton.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlSaveContent;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNationalId;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCompany;
        private System.Windows.Forms.TabPage tabAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPassport;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtVisa;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox txtNationality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpJoin;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtOtherdetails;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox chkWorking;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtpResign;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView gridEmployee;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtSalesmanCredit;
        private System.Windows.Forms.LinkLabel linkUpdateCustomerLedger;
        private System.Windows.Forms.LinkLabel linkSalesmanCreditStatement;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtVehicle;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtHelper;
        private System.Windows.Forms.ComboBox txtDesignation;
        private System.Windows.Forms.RadioButton rdbEmployee;
        private System.Windows.Forms.RadioButton rdbRouteUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
    }
}