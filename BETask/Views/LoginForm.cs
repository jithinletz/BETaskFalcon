using System;
using System.Collections.Generic;
using BETask.BAL;
using BETask.Common;
using BETask.Model;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class LoginForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Login,
            Cancel,
            Close,
            Update,
            Other
        }
        public LoginForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.Login:
                    Login();
                    break;
                case EnumFormEvents.Close:
                    Application.Exit();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == btnLogin)
            {
                ButtonActive(EnumFormEvents.Login);
            }
            else if (sender == btnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
           
        }
        private bool Validation()
        {
            bool resp = false;
            if (txtUsername.Text.Trim().Length <= 1) { resp = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter username"); txtUsername.Focus(); } else resp = true;
            if (txtPassword.Text.Trim().Length <= 0) { resp = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter password"); txtPassword.Focus(); } else resp = true;

            return false;
        }
        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtPassword)
                {
                    Login();
                    btnLogin.Focus();
                }
                else
                    General.NextFocus(sender, e);

            }
        }
        #endregion

        private void Login()
        {
            if (!General.IsTextboxEmpty(txtUsername) && !General.IsTextboxEmpty(txtPassword))
            {
                try
                {
                    EmployeeBAL employeeBAL = new EmployeeBAL();
                    var resp = employeeBAL.Login(txtUsername.Text, txtPassword.Text);
                    if ( resp>0)
                    {
                        General.Action($"Login By {txtUsername.Text}");

                        General.userName = txtUsername.Text;
                        DAL.Model.Constants.UserName = General.userName;

                        General.userId = resp;
                        DAL.Model.Constants.UserId = resp;
                        CompanyBAL companyBAL = new CompanyBAL();
                        // companyBAL.SetMailDefaults();
                      ////  GetBuilding();
                       // StaticDBData.LoadEmployee();
                        MDIBETask mDIBETask = new MDIBETask();
                        mDIBETask.Show();
                        this.Hide();
                    }
                    else
                    {
                        General.Error($"invalid try to login {txtUsername.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Error, "Invalid username or password", "Failed");
                        txtPassword.Focus();
                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.Message);
                    General.ShowMessage(General.EnumMessageTypes.Error, $"Unable to Login Now Please Inform Vendor \n {ee.Message}");
                }

            }
        }
        private async Task GetBuilding()
        {
            StaticDBData.LoadBuildings();
        }
       
        private String BuildFormTitle()
        {
            String AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            String FormTitle = String.Format("{0} {1} ({2})",
                                             AppName,
                                             Application.ProductName,
                                             Application.ProductVersion);
            lblVersion.Text = "Version " + Application.ProductVersion.ToString() + " | " + Application.ProductName.ToString();
            return FormTitle;
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            LoginButtonCollection button = new LoginButtonCollection()
            {
                BtnLogin = btnLogin,
                BtnClose = btnClose
            };
            txtUsername.Focus();
            BuildFormTitle();
        }
    }

    class LoginButtonCollection
    {
        public Button BtnLogin { get; set; }
        public Button BtnClose { get; set; }
    }
}
