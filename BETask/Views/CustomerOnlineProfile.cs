using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.APP.EDMX;
using System.Text.RegularExpressions;

namespace BETask.Views
{
    public partial class CustomerOnlineProfile : Form
    {
        public enum EnumFormEvents
        {
            Save,
            Close
        }

        public int CustomerId { get; set; }
        public CustomerOnlineProfile(int cutomerId)
        {
            InitializeComponent();
            CustomerId = cutomerId;
            FetchCustomer(CustomerId);
        }
        CustomerProfileButtonCollection button;
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.Save:
                    UpdateProfile();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                default:
                    break;
            }
        }
        private void FetchCustomer(int customerId)
        {
            try
            {
                DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                var customer = customerDAL.GetCustomerDetails(customerId);
                if (customer != null)
                {
                    txtName.Text = customer.app_customer_name;
                    txtPhone.Text = customer.app_phone;
                    txtEmail.Text = customer.app_email;
                    txtPassword.Text = customer.app_password;
                    txtAddress1.Text = customer.app_address1;
                    txtAddress2.Text = customer.app_address2;
                    txtWalletBalance.Text = customer.wallet_balance.ToString();
                    txtOutstanding.Text = customer.outstanding_amount.ToString();
                    txtOffer.Text = customer.offer_category;
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, ex.Message);
            }
        }

        private void lnkCoupon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtPasswordShow.Text == this.CustomerId.ToString())
            {
                txtPassword.UseSystemPasswordChar = false;
                Application.DoEvents();
                System.Threading.Thread.Sleep(5000);
                txtPassword.UseSystemPasswordChar = true;
            }
        }
        private void UpdateProfile()
        {
            if (ValidateForm())
            {
                if (General.ShowMessageConfirm("Are you sure want to save customer details") == DialogResult.Yes)
                {
                    DAL.EDMX.customer customer = new DAL.EDMX.customer
                    {
                        app_customer_name = txtName.Text,
                        customer_id = this.CustomerId,
                        app_address1 = txtAddress1.Text,
                        app_address2 = txtAddress2.Text,
                        app_phone = txtPhone.Text,
                        app_password = txtPassword.Text,
                        app_email = txtEmail.Text
                    };
                    CustomerBAL customerBAL = new CustomerBAL();
                    customerBAL.UpdateCustomerOnlineProfile(customer);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Customer profile updated", "Verify by reopen");
                    ButtonActive(EnumFormEvents.Close);
                }
            }
        }
        private bool ValidateForm()
        {
            // Flag to track if the form is valid
            bool isValid = true;

            // Validate Name (must not be empty)
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider.SetError(txtName, "Name is required.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtName, "");
            }

            // Validate Phone (use regular expression for phone format)
            if (!Regex.IsMatch(txtPhone.Text, @"^\d{5,}$")) // Example for a 5-digit number
            {
                errorProvider.SetError(txtPhone, "Phone number minimum must be 5 digits.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtPhone, "");
            }

            // Validate Email
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txtEmail, "Invalid email format.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }

            // Validate Password (not empty, add further checks if needed)
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Password is required.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtPassword, "");
            }

            // Validate Address1 (must not be empty)
            if (string.IsNullOrWhiteSpace(txtAddress1.Text))
            {
                errorProvider.SetError(txtAddress1, "Address1 is required.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtAddress1, "");
            }

            if (string.IsNullOrWhiteSpace(txtAddress2.Text))
            {
                errorProvider.SetError(txtAddress2, "Address2 is required.");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtAddress2, "");
            }
            // Address2 can be optional, add validation only if needed.

            return isValid;
        }

        private void CustomerOnlineProfile_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void FormLoad()
        {
            button = new CustomerProfileButtonCollection { 
            BtnClose=btnClose,
            BtnSave=btnSave
            };
        }
    }
    class CustomerProfileButtonCollection
    {
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }

    }
}
