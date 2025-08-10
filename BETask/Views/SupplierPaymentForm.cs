using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;

namespace BETask.Views
{
    public partial class SupplierPaymentForm : Form
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
            DuePrint,
            Print
        }
        SupplierPaymentButtonCollection button;
        List<Model.AccountLedgerModel> listLedger;
        int transactionNumber = 0, transactionTypeId = 0;
        System.Guid guid;

        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<EDMX.item> listItem = new List<EDMX.item>();
        List<CustomerModel> lstCustomers = new List<CustomerModel>();
        List<EDMX.purchase> lstPurchase = new List<EDMX.purchase>();
        BAL.PurchaseBAL objPurchaseBAL = new BAL.PurchaseBAL();
        BAL.AccountLedgerBAL objAccountLedgerBAL = new BAL.AccountLedgerBAL();
        public SupplierPaymentForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    pnlSaveContent.Enabled = false;
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    pnlSaveContent.Enabled = false;
                    //this.transactionNumber = 0;
                    this.transactionTypeId = 0;
                    General.ClearTextBoxes(this);
                    ResetForms();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveSupplierPayment();
                    button.BtnPrint.Enabled = true;
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    button.BtnPrint.Enabled = false;
                    this.transactionNumber = 0;
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Text = "&Update";
                    button.BtnSave.Enabled = false;
                    break;
                case EnumFormEvents.DuePrint:
                    button.BtnPrint.Enabled = false;
                    DueReportPrint();                    
                    break;                
                case EnumFormEvents.Print:
                    Print();
                    break;
                default:
                    break;

            }
        }

        private void DueReportPrint()
        {
            try
            {
                int supplierId = 0;
                if (!String.IsNullOrEmpty(cmbSupplier.Text) && cmbSupplier.SelectedItem != null)
                {
                    Object selectedSupplier = cmbSupplier.SelectedItem;
                    supplierId = (int)((BETask.Views.ComboboxItem)selectedSupplier).Value;
                }
                if (supplierId > 0)
                    objPurchaseBAL.PrintSupplierDueReport(General.ConvertDateServerFormatWithStartTime(dtpDate.Value), supplierId);
            }
            catch (Exception ex)
            {
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void ResetForms()
        {
            rdlCash.Checked = true;
            cmbLedger.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;
            General.ClearGrid(dgItems);
            chkCheque.Checked = false;
            lblTotalDue.Text = "0.00";
            lblTotalPaid.Text = "0.00";
            lblTotalTobePaid.Text = "0.00";
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
            else if (sender == button.BtnDuePrint)
            {
                ButtonActive(EnumFormEvents.DuePrint);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }
       

        private bool Validation()
        {
            bool resp = true;
            if (cmbSupplier.Text == string.Empty)
            { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select supplier"); resp = false;cmbSupplier.Focus(); }
            if (txtRemarks.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter remarks");
                txtRemarks.Focus();
                resp = false;
            }           
            return resp;
        }
        private void GetAllSuppliers()
        {
            try
            {
                lstCustomers = _customerBAL.GetAllCustomers(-1, string.Empty, 2);
                foreach (CustomerModel cust in lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cust.Customer_Name,
                        Value = cust.Customer_Id
                    };
                    cmbSupplier.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void LoadCompanyLedger()
        {
            DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
            try
            {
                List<EDMX.ledger_mapping> listLedger = ledgerMappingDAL.GetCompanyLedger();
                foreach (EDMX.ledger_mapping ledger in listLedger)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.account_ledger.ledger_name,
                        Value = ledger.account_ledger.ledger_id
                    };
                    cmbLedger.Items.Add(_cmbItem);
                }
                cmbLedger.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void SaveSupplierPayment()
        {
            try
            {
                int purchaseId = 0;
                bool saveAcTran = false;
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {                        
                        decimal paidAmt = 0;
                        decimal dueAmt = 0;
                        decimal balanceAmt = 0;
                        EDMX.purchase objPurchase = new EDMX.purchase();
                        foreach (DataGridViewRow row in dgItems.Rows)
                        {
                            purchaseId = 0;
                            if (row.Cells["clmDueAmount"].Value != null)
                            {
                                purchaseId = Convert.ToInt32(General.ParseInt(row.Cells["clmPurchaseId"].Value.ToString()));                                
                            }
                            if (row.Cells["clmPaidAmount"].Value != null)
                            {
                                paidAmt = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmPaidAmount"].Value.ToString()));                           
                            }
                            if (row.Cells["clmTobePaid"].Value != null)
                            {
                                balanceAmt = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmTobePaid"].Value.ToString()));
                            }
                            if (purchaseId > 0 && paidAmt>0)
                            {
                                objPurchase = objPurchaseBAL.GetPurchaseDetails(purchaseId);
                                objPurchase.cash_paid = objPurchase.cash_paid + paidAmt;
                                objPurchase.balance_amount = balanceAmt;
                                saveAcTran=objPurchaseBAL.UpdatePurchaseSupplierPayment(objPurchase);                                
                            }

                        }
                        if(saveAcTran)
                        SavePayment();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private List<EDMX.account_transaction> GetAccountTransactionList()
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {
                int ledgerId = 0;
                int supllierledgerId = 0;
                string supplier = string.Empty;
                //cr
                if (cmbLedger.Text != "")
                {
                    Object selectedLedger = cmbLedger.SelectedItem;
                    ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                }
                if (cmbSupplier.Text != "")
                {
                    Object selectedSupplier = cmbSupplier.SelectedItem;
                    supplier = (string)((BETask.Views.ComboboxItem)selectedSupplier).Text;
                }
                if (ledgerId > 0)
                {
                    //Cr
                    decimal totAmt = Convert.ToDecimal(lblTotalPaid.Text);
                    listAccount.Add(new EDMX.account_transaction
                    {
                        ledger_id = ledgerId,
                        transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                        credit = General.TruncateDecimalPlaces(totAmt),
                        debit = 0,
                        narration = txtRemarks.Text,
                        transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                        status = 1,
                        transaction_number = this.transactionNumber,
                        transaction_type_id = 0,

                    });
                    //Dr
                    listLedger = objAccountLedgerBAL.GetAllAccountLedger(-1);
                    var ledger = listLedger.Where(l => l.Ledger_name == supplier).FirstOrDefault();
                    supllierledgerId = Convert.ToInt32(ledger.Ledger_id);
                    string remarks = $"Payment Against invoice ";
                    decimal totalpaidAmt = 0;
                    string voucher = rdlCash.Checked ? rdlCash.Text.ToString() : rdlBank.Text.ToString();
                    string referenceId = null;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        if (row.Cells["clmPaidAmount"].Value != null)
                        {
                            decimal paidAmt = 0;
                            paidAmt = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmPaidAmount"].Value.ToString()));
                            if (paidAmt > 0)
                            {
                                remarks = $"{ remarks},{ row.Cells["clmInvoiceNo"].Value.ToString()}";  
                            }
                            totalpaidAmt += paidAmt;
                        }
                    }
                    if (totalpaidAmt > 0)
                    {
                        listAccount.Add(new EDMX.account_transaction
                        {
                            ledger_id = supllierledgerId,
                            transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                            credit = 0,
                            debit = totalpaidAmt,
                            narration = remarks,
                            transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                            transaction_type_id = 0,
                            voucher_number = voucher,
                            status = 1,
                            reference_id = referenceId

                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return listAccount;
        }
        private void SavePayment()
        {
            try
            { 

                        List<EDMX.account_transaction> listAccount = GetAccountTransactionList();
                        if (listAccount != null && listAccount.Count > 0)
                        {
                            BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                    string referance = "";
                    this.transactionNumber = accountTransactionBAL.SaveAccountTransaction(listAccount, REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT,ref referance);
                           
                            if (chkCheque.Checked)
                            {
                                SaveChequeDetails(this.transactionNumber);
                            }
                            General.ShowMessage(General.EnumMessageTypes.Success, "Supplier Payment Successfully saved");
                            ButtonActive(EnumFormEvents.Cancel);
                            //PopulatePaymentDetails(transactionNumber);
                        }
                   
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SaveChequeDetails(int transactionNumber)
        {
            if ( transactionNumber > 0 && !String.IsNullOrEmpty(txtChequeNumber.Text) && rdlBank.Checked)
            {
                try
                {
                    EDMX.account_transaction_cheque cheque = new EDMX.account_transaction_cheque
                    {
                        cheque_date = General.ConvertDateServerFormat(dtpChequeDate.Value),
                        cheque_number = txtChequeNumber.Text,
                        bank = txtChequeBank.Text,
                        other_details = txtChequeOther.Text,
                        account_transaction_number = transactionNumber,
                        status = 1

                    };
                    BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                    accountTransactionBAL.SaveAccountTransactionCheque(cheque);
                }
                catch (Exception ex)
                {
                    General.Error(ex.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Success, "Cheque details not saved");
                }
            }
        }
    
        private void Print()
        {
            try
            {
                if (this.transactionNumber > 0)
                {
                    BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                    accountTransactionBAL.PrintPaymentVoucher(this.transactionNumber);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);

            }
        }
       
        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
       
        private void CalculateTotal()
        {
            try
            {
                
                decimal totalPaid = 0;
                decimal totalDueAmt = 0;
                decimal totalTobePaid = 0;
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    decimal _total = 0;
                    decimal _paid = 0;
                    decimal _dueAmt = 0;
                    if (row.Cells["clmDueAmount"].Value != null)
                    {
                        _dueAmt = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmDueAmount"].Value.ToString()));
                        totalDueAmt += _dueAmt;
                    }
                    if (row.Cells["clmPaidAmount"].Value != null)
                    {
                        _paid = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmPaidAmount"].Value.ToString()));
                        totalPaid += _paid;
                    }                  
                }
                totalTobePaid = totalDueAmt - totalPaid;
                lblTotalDue.Text =$"Total Due : { totalDueAmt.ToString()}";
                lblTotalPaid.Text = totalPaid.ToString();
                lblTotalTobePaid.Text = $"Balance : {  totalTobePaid.ToString()}";

            }
            catch
            {
                throw;
            }
        }

        private void dgItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
           
        }


        private void dgItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    decimal _tobepaid = 0;
                    decimal _paid = 0;
                    decimal _dueAmt = 0;
                    if (dgItems["clmDueAmount", e.RowIndex].Value != null)
                    {
                        _dueAmt = General.TruncateDecimalPlaces(General.ParseDecimal(dgItems["clmDueAmount", e.RowIndex].Value.ToString()));
                    }
                    if (dgItems["clmPaidAmount", e.RowIndex].Value != null)
                    {
                        _paid = General.TruncateDecimalPlaces(General.ParseDecimal(dgItems["clmPaidAmount", e.RowIndex].Value.ToString()));
                        if (_paid > _dueAmt)
                        {
                            dgItems["clmPaidAmount", e.RowIndex].Value = _dueAmt;
                            General.ShowMessage(General.EnumMessageTypes.Warning, "Paid amound greater than due amount");
                        }
                    }
                    _tobepaid = _dueAmt - _paid;
                    dgItems["clmToBePaid", e.RowIndex].Value = _tobepaid;
                    CalculateTotal();
                }
                dgItems.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void chkCheque_CheckedChanged(object sender, EventArgs e)
        {
            grpChequeDetails.Visible = chkCheque.Checked;           
        }

        private void SupplierPaymentForm_Load(object sender, EventArgs e)
        {
            button = new SupplierPaymentButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnDuePrint = btnDuePrint,
                BtnPrint = btnPrint
            };
            GetAllSuppliers();
            LoadCompanyLedger();
            ButtonActive(EnumFormEvents.FormLoad);
        }

        private void rdlBank_CheckedChanged(object sender, EventArgs e)
        {
            pnlBank.Enabled = rdlBank.Checked;
            chkCheque.Checked = false;
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int supplierId = 0;
            if (!String.IsNullOrEmpty(cmbSupplier.Text) && cmbSupplier.SelectedItem != null)
            {
                Object selectedSupplier = cmbSupplier.SelectedItem;
                supplierId = (int)((BETask.Views.ComboboxItem)selectedSupplier).Value;               
            }
            GetSupplierPaymentDueDetails(supplierId);
        }

        private void GetSupplierPaymentDueDetails(int supplierId)
        {
            dgItems.Rows.Clear();
            lstPurchase =objPurchaseBAL.GetSupplierPaymentDueDetails(General.ConvertDateServerFormatWithStartTime(dtpDate.Value),supplierId);
            foreach (EDMX.purchase objpurchase in lstPurchase)
            {
                decimal dueAmount = Convert.ToDecimal(objpurchase.net_amount) - Convert.ToDecimal(objpurchase.cash_paid);
                dgItems.Rows.Add(objpurchase.purchase_id, objpurchase.invoice_date.ToShortDateString(), objpurchase.invoice_number, objpurchase.net_amount, dueAmount, "0", objpurchase.balance_amount);
            }
            CalculateTotal();
        }

        private void dgItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
            if (dgItems.CurrentRow.Index >= 0 && (dgItems.CurrentCell.ColumnIndex == 5))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                     tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                    
                }
            }
        }

        private void rdlCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlCash.Checked)
            { cmbLedger.SelectedIndex = 0; }
            else { cmbLedger.SelectedIndex = 1; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
    }



    class SupplierPaymentButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnDuePrint { get; set; }
        public Button BtnPrint { get; set; }
    }
}
