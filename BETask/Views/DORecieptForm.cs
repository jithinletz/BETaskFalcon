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
    public partial class DORecieptForm : Form
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
        CustomerRecieptButtonCollection button;
        List<Model.AccountLedgerModel> listLedger;
        int transactionNumber = 0, transactionTypeId = 0;
        System.Guid guid;

        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<EDMX.item> listItem = new List<EDMX.item>();
        List<CustomerModel> lstCustomers = new List<CustomerModel>();
        List<EDMX.purchase> lstPurchase = new List<EDMX.purchase>();
        DAL.DAL.DOSaleDAL saleDAL = new DAL.DAL.DOSaleDAL();
        BAL.DOSaleBAL saleBAL = new BAL.DOSaleBAL();
        BAL.AccountLedgerBAL objAccountLedgerBAL = new BAL.AccountLedgerBAL();

        public DORecieptForm()
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
                    Search(0);
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
                    if (General.ShowMessageConfirm()== DialogResult.Yes)
                        SaveReciept();
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
                int customerId = General.GetComboBoxSelectedValue(cmbCustomer);
                string header = $"Date as on {General.ConvertDateAppFormat(dtpDate.Value)} {cmbCustomer.Text}";
                saleBAL.PrintPendingInvoices(customerId, General.ConvertDateServerFormatWithEndTime(dtpDate.Value), header);
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
            int customerId = General.GetComboBoxSelectedValue(cmbCustomer);
            decimal amount = Convert.ToDecimal(lblTotalPaid.Text);

            if (customerId <= 0)
            { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select Customer"); resp = false; cmbCustomer.Focus(); }

            else if (txtRemarks.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter remarks");
                txtRemarks.Focus();
                resp = false;
            }
            else if (amount <= 0)
            { General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid Amount"); resp = false; }
            return resp;
        }
        private void GetAllCustomer()
        {
            try
            {
                lstCustomers = _customerBAL.GetAllCustomers(-1, string.Empty, 1,0,"DO");
                foreach (CustomerModel cust in lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cust.Customer_Name,
                        Value = cust.Customer_Id
                    };
                    cmbCustomer.Items.Add(_cmbItem);
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
   

        private List<EDMX.account_transaction> GetAccountTransactionList()
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {
                int ledgerId = General.GetComboBoxSelectedValue(cmbLedger);

                int customerId = General.GetComboBoxSelectedValue(cmbCustomer);

                if (ledgerId > 0)
                {
                    //Dr
                    decimal totAmt = Convert.ToDecimal(lblTotalPaid.Text);
                    listAccount.Add(new EDMX.account_transaction
                    {
                        ledger_id = ledgerId,
                        transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                        debit = General.TruncateDecimalPlaces(totAmt),
                        credit = 0,
                        narration = txtRemarks.Text,
                        transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                        status = 1,
                        transaction_number = this.transactionNumber,
                        transaction_type_id = 0,

                    });


                    //Cr
                    //listLedger = objAccountLedgerBAL.GetAllAccountLedger(-1);
                    BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
                    Model.CustomerModel customer = customerBAL.GetCustomerDetail(customerId);
                    int CustomerLedgerId = customer.LedgerId;

                    string remarks = $"Reciept Against invoice ";
                    decimal totalpaidAmt = 0;
                    string voucher = rdlCash.Checked ? rdlCash.Text.ToString() : rdlBank.Text.ToString();
                    string referenceId = null;
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        if (row.Cells["clmRecievedAmount"].Value != null)
                        {
                            decimal paidAmt = 0;
                            paidAmt = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmRecievedAmount"].Value.ToString()));
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
                            ledger_id = CustomerLedgerId,
                            transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                            credit = totalpaidAmt,
                            debit = 0,
                            narration = remarks,
                            transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
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


        /*
         * Saving PDC 
         */
        private void SavePDC()
        {
            int bankId = General.GetComboBoxSelectedValue(cmbLedger);
            int customerId = General.GetComboBoxSelectedValue(cmbCustomer);
            if (customerId > 0 && bankId > 0)
            {
                int customerLedger = 0;
                BAL.CompanyBAL companyBAL = new BAL.CompanyBAL();
                BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
                Model.CustomerModel customer = customerBAL.GetCustomerDetail(customerId);
                if (customer != null)
                    customerLedger = customer.LedgerId;
                if (customerLedger > 0)
                {
                    foreach (DataGridViewRow dr in dgItems.Rows)
                    {
                        if (dr.Cells["clmRecievedAmount"].Value!=null && General.ParseDecimal(dr.Cells["clmRecievedAmount"].Value.ToString()) > 0)
                        {
                            EDMX.pdc pdc = new EDMX.pdc
                            {
                                ledger_id = bankId,
                                party_id = customerLedger,
                                doc_date = General.ConvertDateServerFormat(dtpDate.Value),
                                cheque_date = General.ConvertDateServerFormat(dtpChequeDate.Value),
                                cheque_number = txtChequeNumber.Text,
                                amount = General.ParseDecimal(dr.Cells["clmRecievedAmount"].Value.ToString()),
                                pdc_mode = "reciept",
                                remarks = $"DORECIEPT-{txtRemarks.Text}",
                                status = 1,
                                cheque_status = "Collected",
                                updated_on = DateTime.Today,
                                
                            };
                            BAL.PDCBAL pdcBal = new BAL.PDCBAL();
                            pdcBal.SavePDC(pdc);
                            SaveChequeDetailsPDC(pdc);
                            transactionNumber = pdcBal.GetPDCTransactionNumber(pdc);
                        }
                    }
                }
            }
        }
        private void SaveChequeDetailsPDC(EDMX.pdc pdc)
        {
            if (!String.IsNullOrEmpty(pdc.cheque_number))
            {
                BAL.PDCBAL pdcBal = new BAL.PDCBAL();
                int transactionNumber = 0;
                try
                {
                    transactionNumber = pdcBal.GetPDCTransactionNumber(pdc);
                    EDMX.account_transaction_cheque cheque = new EDMX.account_transaction_cheque
                    {
                        cheque_date = pdc.cheque_date,
                        cheque_number = txtChequeNumber.Text,
                        // bank = pdc.ba,
                        other_details = pdc.remarks,
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
        /*
         * END PDC
         */
        private void SaveReciept()
        {
            try
            {
                if (Validation())
                {
                    DAL.DAL.CompanyDAL companyDAL = new REP.CompanyDAL();
                    EDMX.system_settings system_Settings = companyDAL.GetSystemSettings();

                    /*If bank and PDC enabled then it will save as PDC
                        PDC Recieved A/c Dr  To
                          Customer A/c
                     */

                    if (rdlBank.Checked && system_Settings.pdc_enable == 1 && !string.IsNullOrEmpty(txtChequeNumber.Text))
                    {
                        SavePDC();
                      
                    }

                    /*If PDC not enabled and BANK or Cash
                      Cash or Bank A/c Dr  To
                          Customer A/c
                     */

                    else if (rdlCash.Checked || (rdlBank.Checked && (system_Settings.pdc_enable != 1 || string.IsNullOrEmpty(txtChequeNumber.Text))))
                    {
                        List<EDMX.account_transaction> listAccount = GetAccountTransactionList();
                        if (listAccount != null && listAccount.Count > 0)
                        {
                            BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                            string referance = "";
                            this.transactionNumber = accountTransactionBAL.SaveAccountTransaction(listAccount, REP.AccountTransactionDAL.EnumTransactionTypes.RECIEPT,ref referance);

                            if (chkCheque.Checked)
                            {
                                SaveChequeDetails(this.transactionNumber);
                            }
                            

                        }
                    }

                    //Updating paid amount in DO Table
                    UpdateDOReciept();
                    General.ShowMessage(General.EnumMessageTypes.Success, "Customer DO Reciept Successfully saved");
                    ButtonActive(EnumFormEvents.Cancel);
                }
                   
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void UpdateDOReciept()
        {
            try
            {
               
                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    if (dr.Cells["clmRecievedAmount"].Value!=null && General.ParseDecimal(dr.Cells["clmRecievedAmount"].Value.ToString()) > 0)
                    {
                        int doId = General.ParseInt(dr.Cells["clmDoId"].Value.ToString());
                        decimal amount = General.ParseDecimal(dr.Cells["clmRecievedAmount"].Value.ToString());
                        saleBAL.UpdateDOReciept(doId, amount, dtpDate.Value, dr.Cells["clmInvoiceNo"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void SaveChequeDetails(int transactionNumber)
        {
            if (chkCheque.Checked && transactionNumber > 0 && !String.IsNullOrEmpty(txtChequeNumber.Text))
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
                    accountTransactionBAL.PrintPaymentVoucher(this.transactionNumber,"Reciept Voucher");
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
                    if (row.Cells["clmRecievedAmount"].Value != null)
                    {
                        _paid = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmRecievedAmount"].Value.ToString()));
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
                    if (dgItems["clmRecievedAmount", e.RowIndex].Value != null)
                    {
                        _paid = General.TruncateDecimalPlaces(General.ParseDecimal(dgItems["clmRecievedAmount", e.RowIndex].Value.ToString()));
                        if (_paid > _dueAmt)
                        {
                            dgItems["clmRecievedAmount", e.RowIndex].Value = _dueAmt;
                            General.ShowMessage(General.EnumMessageTypes.Warning, "Paid amound greater than due amount");
                        }
                    }
                    _tobepaid = _dueAmt - _paid;
                    dgItems["clmTobePaid", e.RowIndex].Value = _tobepaid;
                    CalculateTotal();
                }
                dgItems.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
     

        private void chkCheque_CheckedChanged(object sender, EventArgs e)
        {
            grpChequeDetails.Visible = chkCheque.Checked;           
        }

        private void SupplierPaymentForm_Load(object sender, EventArgs e)
        {
            button = new CustomerRecieptButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnDuePrint = btnDuePrint,
                BtnPrint = btnPrint
            };
            GetAllCustomer();
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
            int customerId = General.GetComboBoxSelectedValue(cmbCustomer);
            Search(customerId);
          

        }

        private void Search(int customerId)
        {
            try
            {
                dgItems.Rows.Clear();
                if (customerId == 0)
                {
                    dgItems.Columns["clmRecievedAmount"].Visible = false;
                    dgItems.Columns["clmToBePaid"].Visible = false;
                }
                else
                {
                    dgItems.Columns["clmRecievedAmount"].Visible = true;
                    dgItems.Columns["clmToBePaid"].Visible = true;
                }
                List<EDMX.do_sales> listSale = saleDAL.GetPendingInvoices(General.ConvertDateServerFormatWithEndTime(dtpDate.Value), customerId);
                foreach (EDMX.do_sales sale in listSale)
                {
                    if (sale.net_amount > sale.amount_paid)
                    {
                        decimal dueAmount = Convert.ToDecimal(sale.net_amount) - Convert.ToDecimal(sale.amount_paid);
                        dgItems.Rows.Add(sale.do_id, sale.customer_id, sale.customer.customer_name, sale.customer.route.route_name, General.ConvertDateAppFormat(sale.do_date), sale.do_invoice_number, sale.net_amount, dueAmount, "0", dueAmount);
                    }
                }
                CalculateTotal();
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load due details");
            }
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

        private void cmbCustomer_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbCustomer.Text)) Search(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
    }



    class CustomerRecieptButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnDuePrint { get; set; }
        public Button BtnPrint { get; set; }
    }
}
