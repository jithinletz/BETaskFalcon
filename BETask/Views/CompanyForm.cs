using BETask.BAL;
using BETask.Common;
using BETask.Model;
using System;
using System.Windows.Forms;

namespace BETask.Views
{
    public partial class CompanyForm : Form
    {
        CompanyBAL companyBAL;
        int companyId = 0;
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Update,
            Cancel,
            Close,
            NodeClick
        } 
        CompanyButtonCollection button; 
        public CompanyForm()
        {
            companyBAL = new CompanyBAL();
            InitializeComponent();
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    GetCompanyData();
                    break;
                case EnumFormEvents.Cancel:
                   this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveCompanyDetails();
                    break;
                default:
                    break;

            }
        }

        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }

        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtWeb)
                    btnSave.Focus();
                else
                    General.NextFocus(sender, e);

            }
        }

        #endregion NextFocus

        private void CompanyForm_Load(object sender, EventArgs e)
        {
            button = new CompanyButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
        }


        #region Custom Methods
        private void GetCompanyData() { 
            try
            {
                CompanyModel companyModel = companyBAL.GetCompanyDetails();
                if (companyModel != null)
                {
                    companyId = companyModel.Company_id;
                    
                    txtName.Text = companyModel.Name;
                    txtDec.Text = companyModel.Description;
                    txtAddress1.Text = companyModel.Address1;
                    txtCity.Text = companyModel.City;
                    txtStreet.Text = companyModel.Street;
                    txtAddress2.Text = companyModel.Address2;
                    txtEmail.Text = companyModel.Email;
                    txtPhone.Text = companyModel.Phone;
                    txtMobile.Text = companyModel.Mobile;
                    txtTin.Text = companyModel.Tin;
                    txtWeb.Text = companyModel.Web;
                    txtPoBox.Text = companyModel.POBox;
                    dtpFinDateFrom.Value = companyModel.FinancialDateFrom;
                    dtpFinDateTo.Value = companyModel.FinancialDateTo;
                }
            }
            catch(Exception ee) {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            
        }

        private void SaveCompanyDetails() {
            if (ValidateCompany())
            {
                CompanyModel companyModel = new CompanyModel() {
                    Company_id=companyId,
                    Address1 = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    City = txtCity.Text,
                    Description = txtDec.Text,
                    Email = txtEmail.Text,
                    Mobile = txtMobile.Text,
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                    POBox = txtPoBox.Text,
                    Street = txtStreet.Text,
                    Tin = txtTin.Text,
                    Web = txtWeb.Text            ,
                    FinancialDateFrom=General.ConvertDateServerFormat(dtpFinDateFrom.Value),
                    FinancialDateTo = General.ConvertDateServerFormat(dtpFinDateTo.Value),
                    
                };
                companyBAL.SaveCompany(companyModel);
                General.ShowMessage(General.EnumMessageTypes.Success,"Company Saved successfully !!");
            }
           
        }

        public bool ValidateCompany() {
            bool response = false;
            if (General.IsTextboxEmpty(txtName)) { response = false; 
                    General.ShowMessage(General.EnumMessageTypes.Error, "Please enter company name !!");
               
            } else response = true;

            double months= dtpFinDateTo.Value.Subtract(dtpFinDateFrom.Value).Days / (365.25 / 12);
            if (months > 12 || months<0)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, "Invalid dates");
                response = false;
            }
            return response;
        }
        #endregion

    }

    class CompanyButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }

}
