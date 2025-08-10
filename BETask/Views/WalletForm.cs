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
    public partial class WalletForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Other,
            Search,
            Print
        }

        WalletButtonCollection button;
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        BAL.WalletBAL _walletHistoryBAL = new BAL.WalletBAL();
        List<EDMX.account_ledger> listBank =null;
        List<CustomerModel> _lstCustomers = null;
        int customerId = 0;
        string customerName = "";

        public WalletForm()
        {
            InitializeComponent();
        }
        public WalletForm(string _customerName,int _customerId)
        {
            InitializeComponent();
            this.customerName = _customerName;
            this.customerId = _customerId;
            lblCustomerName.Text = this.customerName;
            LoadCustomerData(customerId);
            // cmbCustomerName.Enabled = false;
            cmbCustomerName.Hide();
            lblCustomerName.ForeColor = Color.DarkRed;
            chkAdjustment.Checked = false;
        }
        public WalletForm(string _customerName, int _customerId,decimal amount)
        {
            InitializeComponent();
            this.customerName = _customerName;
            this.customerId = _customerId;
            lblCustomerName.Text = this.customerName;
            LoadCustomerData(customerId);
            // cmbCustomerName.Enabled = false;
            cmbCustomerName.Hide();
            lblCustomerName.ForeColor = Color.DarkRed;
            txtAmount.Text = amount.ToString();
            txtRemarks.Text = "Cash collection to wallet ";
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    //pnlSaveContent.Enabled = false;
                   
                    break;
                case EnumFormEvents.Cancel:
                  
                    lblBalance.Text = string.Empty;
                    lblwalletNumber.Text = string.Empty;
                    General.ClearTextBoxes(this);
                    General.ClearGrid(dgRechargeHistory);
                    rdlCash.Checked = true;
                    cmbCustomerName.Text = string.Empty;
                    chkAdjustment.Checked = false;
                    button.BtnSave.Show();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveWalletRecharge();
                    break;
              
                  
                case EnumFormEvents.Print:
                   // Print();
                    break;
                default:
                    break;

            }
        }


        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == cmbCustomerName)
                    dgRechargeHistory.Focus();
                else
                    General.NextFocus(sender, e);

            }
        }

        #endregion NextFocus

        private void GetAllBanks()
        {
            try
            {
                cmbBank.Items.Clear();
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                listBank = accountLedgerDAL.GetAllAccountLedger(groupId);
                foreach (EDMX.account_ledger bank in listBank)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = bank.ledger_name,
                        Value = bank.ledger_id
                    };
                    cmbBank.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        public void LoadCustomerData(int customerId)
        {
            try
            {
                General.ClearGrid(dgRechargeHistory);
                General.ClearTextBoxes(this);
                rdlCash.Checked = true;
                // _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
                CustomerModel customer = _customerBAL.GetCustomerDetail(customerId);//  _lstCustomers.Where(c => c.Customer_Id == customerId).FirstOrDefault();
                lblwalletNumber.Text = customer.WalletNumber;
                lblBalance.Text = Convert.ToString(General.TruncateDecimalPlaces(customer.WalletBalance));
                List<EDMX.wallet_history> listHistory = _walletHistoryBAL.GetCustomerWalletHistory(customerId);
                if (listHistory != null && listHistory.Count > 0)
                {
                    foreach (EDMX.wallet_history history in listHistory)
                    {
                        dgRechargeHistory.Rows.Add(history.date, history.amount, history.recharge_by, history.payment_mode, history.remarks);
                    }
                    dgRechargeHistory.Rows.Add("", listHistory.Sum(x => x.amount), "", "", "");
                    General.GridBackcolorYellow(dgRechargeHistory);
                    General.GridRownumber(dgRechargeHistory);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }



        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Object selectedItem = cmbCustomerName.SelectedItem;
                customerId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                LoadCustomerData(customerId);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void CustomerAggrementForm_Load(object sender, EventArgs e)
        {
            button = new WalletButtonCollection {
                BtnSave=btnSave,
                BtnCancel=btnCancel,
                BtnClose=btnClose
            };
            try
            {
               // GetAllCustomers();
                
                GetAllBanks();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetAllCustomers()
        {
            try
            {
                _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
                cmbCustomerName.Items.Clear();
                foreach (CustomerModel cust in _lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cust.Customer_Name,
                        Value = cust.Customer_Id
                    };
                    cmbCustomerName.Items.Add(_cmbItem);
                  
                }
               
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnCancel)
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

      
        private void SaveWalletRecharge()
        {
            try
            {
                if (ValidateWallet())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        button.BtnSave.Hide();
                        int bankId = 0;
                        decimal amount = General.ParseDecimal(txtAmount.Text);
                        if (_customerBAL.UpdateWalletBalance(customerId, amount) > 0)
                        {
                            _walletHistoryBAL.SaveWalletHistory(lblwalletNumber.Text, customerId, amount, General.userName, txtRemarks.Text, General.ConvertDateServerFormat(dtpDeliveryDate.Value),rdlBank.Checked ? "Bank" : "Cash", bankId, chkAdjustment.Checked);

                            //Updating APP
                            try
                            {
                                CustomerBAL customerBAL = new CustomerBAL();
                                Model.CustomerModel customerModel = customerBAL.GetCustomerDetail(customerId);
                                //customerBAL.SaveCustomer(customerModel, 0, true);
                            }
                            catch (Exception ee)
                            { }

                            General.Action($"Wallet recharge for {cmbCustomerName.Text} , Amount={amount}");
                            General.ShowMessage(General.EnumMessageTypes.Success, "Wallet recharge success", "Success");
                            LoadCustomerData(customerId);
                            chkAdjustment.Checked = false;
                        }
                        else
                        {
                            General.Action($"Wallet recharge failed for {cmbCustomerName.Text} , Amount={amount}");
                            General.ShowMessage(General.EnumMessageTypes.Warning, "Wallet recharge failed due to balance ZERO");
                            LoadCustomerData(customerId);
                            chkAdjustment.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                button.BtnSave.Show();
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
}

        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            try
            {
                TextBox text = (TextBox)sender;
                General.DecimalValidationText(text);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidateWallet()
        {

            bool result = true;

            if (customerId == 0)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please select customer ");
                result = false;
            }
            //else if (txtAmount.Text == "0.00" || txtAmount.Text == "")
            //{
            //    General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter valid amount ");
            //    result = false;
            //}
            //else if (txtRemarks.Text == "")
            //{
            //    General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter remark");
            //    result = false;
            //}
            else if (rdlBank.Checked && String.IsNullOrEmpty(cmbBank.Text))
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select bank account");
                result = false;
            }

            return result;
        }

       
    }
    class WalletButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }


}
