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
    public partial class PaymentForm : Form
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
        PaymentButtonCollection button;
        List<Model.AccountLedgerModel> listLedger;
        int transactionNumber = 0, transactionTypeId = 0;
        System.Guid guid;
        public PaymentForm()
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
                    this.transactionNumber = 0;
                    this.transactionTypeId = 0;
                    General.ClearTextBoxes(this);
                    ResetForms();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SavePayment();
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    if (btnNew.Text.ToLower().Contains("new"))
                        txtDocumentNo.Text = DAL.DAL.DocumentSerialDAL.GetNextDocument(DAL.DAL.DocumentSerialDAL.EnumDocuments.PAYMENT.ToString()).ToString();
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Text = "&Update";
                    button.BtnSave.Enabled = false;
                    break;
                case EnumFormEvents.Other:
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    button.BtnSave.Enabled = true;
                    button.BtnNew.Enabled = false;
                    break;
                case EnumFormEvents.Search:
                    PaymentSearchForm paymentSearchForm = new PaymentSearchForm();
                    DialogResult result = paymentSearchForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        int _transactionNumber = 0;
                        _transactionNumber = paymentSearchForm.transactionNumber;
                        if (_transactionNumber > 0)
                        {
                            ButtonActive(EnumFormEvents.Cancel);
                            PopulatePaymentDetails(_transactionNumber, paymentSearchForm.copied);

                            if (!paymentSearchForm.copied)
                                ButtonActive(EnumFormEvents.Update);
                            else
                                ButtonActive(EnumFormEvents.New);

                        }
                    }
                    break;
                case EnumFormEvents.Print:
                    Print();
                    break;
                default:
                    break;

            }
        }
        private void ResetForms()
        {
            cmbLedger.Text = string.Empty;
            dtpDate.Value = DateTime.Today;
            General.ClearGrid(dgItems);
            chkCheque.Checked = false;
            transactionTypeId = 0;
            transactionNumber = 0;
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
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
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
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                listLedger = accountLedgerBAL.GetAllAccountLedger(-1);
                DataGridViewComboBoxColumn comboLedger = (DataGridViewComboBoxColumn)dgItems.Columns["clmLedger"];
                comboLedger.HeaderText = "Select Account";
                foreach (Model.AccountLedgerModel ledger in listLedger)
                {
                    comboLedger.Items.Add(ledger.Ledger_name);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private bool Validation()
        {
            bool resp = true;


            if (cmbLedger.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Account");
                cmbLedger.Focus();
                resp = false;
            }
            else if (txtAmount.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter payment Amount");
                txtAmount.Focus();
                resp = false;
            }
            else if (General.ParseDecimal(txtAmount.Text) != General.ParseDecimal(txtLedgerAmount.Text))
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Debit & Credit amount does not match");
                txtAmount.Focus();
                resp = false;
            }
            else if (txtRemarks.Text == string.Empty)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter narration");
                txtRemarks.Focus();
                resp = false;
            }


            else
            {
                Object selectedLedger = cmbLedger.SelectedItem;
                if (selectedLedger == null)
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Please select ledger");
                    cmbLedger.Focus();
                    resp = false;
                }
                else
                    resp = General.CheckFinancialDate(dtpDate.Value);
            }
            if (resp)
            {
                BAL.CostCenterBAL costCenterBAL = new BAL.CostCenterBAL();
                resp = costCenterBAL.ValidateCostCenterAdded(dgItems);

            }
            return resp;
        }

        private List<EDMX.account_transaction> GetAccountTransactionList()
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {


                //cr
                Object selectedLedger = cmbLedger.SelectedItem;
                int ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                if (ledgerId > 0)
                {

                    listAccount.Add(new EDMX.account_transaction
                    {
                        ledger_id = ledgerId,
                        transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                        credit = General.TruncateDecimalPlaces(General.ParseDecimal(txtAmount.Text)),
                        debit = 0,
                        narration = txtRemarks.Text,
                        transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                        status = 1,
                        transaction_number = this.transactionNumber,
                        transaction_type_id = this.transactionTypeId,


                    });

                    //Dr
                    foreach (DataGridViewRow row in dgItems.Rows)
                    {
                        if (row.Cells["clmAmount"].Value != null && row.Cells["clmLedgerId"].Value != null)
                        {
                            decimal _total = 0;
                            _total = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmAmount"].Value.ToString()));
                            int _ledgerId = General.ParseInt(row.Cells["clmLedgerId"].Value.ToString());
                            string remarks = row.Cells["clmRemarks"].Value == null ? txtRemarks.Text : row.Cells["clmRemarks"].Value.ToString();
                            string voucher = row.Cells["clmVoucher"].Value == null ? string.Empty : row.Cells["clmVoucher"].Value.ToString();
                            string referenceId = row.Cells["clmCostEntryId"].Value == null ? string.Empty : row.Cells["clmCostEntryId"].Value.ToString();
                            listAccount.Add(new EDMX.account_transaction
                            {
                                ledger_id = _ledgerId,
                                transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                                credit = 0,
                                debit = _total,
                                narration = remarks,
                                transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                                transaction_type_id = this.transactionTypeId,
                                voucher_number = voucher,
                                status = 1,
                                reference_id = referenceId

                            });
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            return listAccount;
        }
        private bool VaidateCostCenterLedger()
        {
            bool resp = true;

            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();
            try
            {
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    int ledgerId = General.ParseInt(row.Cells["clmLedgerId"].Value.ToString());
                    if (accountLedgerBAL.IsCostCenterEnabled(ledgerId))
                    {
                        string referenceId = row.Cells["clmCostEntryId"].Value == null ? string.Empty : row.Cells["clmCostEntryId"].Value.ToString();
                        if (string.IsNullOrEmpty(referenceId))
                        {

                            General.ShowMessage(General.EnumMessageTypes.Error, $" No cost center transaction added for the ledger {row.Cells["clmLedger"].Value.ToString()}");
                            resp = false;
                            return resp;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resp;
        }
        private void SavePayment()
        {
            try
            {
                BAL.CostCenterBAL costCenterBAL = new BAL.CostCenterBAL();

                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {

                        List<EDMX.account_transaction> listAccount = GetAccountTransactionList();
                        if (listAccount.Sum(x => x.debit) != listAccount.Sum(x => x.credit))
                        {
                            General.ShowMessage(General.EnumMessageTypes.Warning, "Debit and credit are not equal");
                            return;
                        }


                        if (listAccount != null && listAccount.Count > 0)
                        {
                            if (costCenterBAL.ValidateCostCenter(listAccount))
                            {
                                BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                                string referance = "";
                                int transactionNumber = accountTransactionBAL.SaveAccountTransaction(listAccount, REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT, ref referance);
                                General.Action($"New Payment Saved {txtAmount.Text} {txtRemarks.Text}");
                                if (chkCheque.Checked)
                                {
                                    SaveChequeDetails(transactionNumber);
                                }
                                General.ShowMessage(General.EnumMessageTypes.Success, $"Payment Successfully saved Reference: {referance}");
                                ButtonActive(EnumFormEvents.Cancel);
                                //PopulatePaymentDetails(transactionNumber);
                            }
                            else
                            {
                                General.ShowMessage(General.EnumMessageTypes.Warning, "There is a mismatch in Cost center please confirm first", "Cost center mismatch found");
                                return;
                            }
                        }

                    }
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
        private void PopulatePaymentDetails(int _transactionNumber, bool copied = false)
        {
            try
            {
                General.ClearGrid(dgItems);
                this.transactionNumber = !copied ? _transactionNumber : 0;
                BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                List<EDMX.account_transaction> listTransaction = accountTransactionBAL.GetAccountTransactionDetail(_transactionNumber, REP.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString());
                this.transactionTypeId = Convert.ToInt32(listTransaction[0].transaction_type_id);
                dtpDate.Value = !copied ? listTransaction[0].transaction_date : DateTime.Now;
                txtDocumentNo.Text = listTransaction[0].transaction_type_id.ToString();
                foreach (EDMX.account_transaction tran in listTransaction)
                {
                    if (tran.credit > 0)
                    {
                        cmbLedger.Text = tran.account_ledger.ledger_name;
                        txtAmount.Text = tran.credit.ToString();
                        txtLedgerAmount.Text = tran.credit.ToString();
                        txtRemarks.Text = tran.narration;
                    }
                    else
                    {
                        dgItems.Rows.Add(tran.ledger_id, tran.account_ledger.ledger_name, tran.debit, tran.voucher_number, !copied ? tran.narration : "", !copied ? tran.reference_id : "");
                    }
                }

                PopulateCheque(_transactionNumber);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void PopulateCheque(int transactionNumber)
        {
            try
            {
                BAL.AccountTransactionBAL transactionBAL = new BAL.AccountTransactionBAL();
                EDMX.account_transaction_cheque cheque = transactionBAL.GetCheque(transactionNumber);
                if (cheque != null)
                {
                    chkCheque.Checked = true;
                    grpChequeDetails.Show();
                    txtChequeNumber.Clear();
                    txtChequeBank.Clear();
                    txtChequeOther.Clear();

                    txtChequeNumber.Text = cheque.cheque_number;
                    txtChequeBank.Text = cheque.bank;
                    txtChequeOther.Text = cheque.other_details;
                    dtpChequeDate.Value = cheque.cheque_date;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Error while getting cheque details");
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
        private void FormLoad()
        {
            button = new PaymentButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
            ButtonActive(EnumFormEvents.FormLoad);
            LoadCompanyLedger();
            //LoadAllLedger();

        }
        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
        private void PaymentForm_Enter(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void CalcTotal()
        {
            try
            {
                decimal total = 0;
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    if (row.Cells["clmAmount"].Value != null)
                    {
                        decimal _total = 0;
                        _total = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmAmount"].Value.ToString()));
                        total += _total;
                    }
                }
                txtLedgerAmount.Text = total.ToString();
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

        private void dgItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }

            if (dgItems.CurrentRow.Index >= 0 && (dgItems.CurrentCell.ColumnIndex == 2))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    //   tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                }
            }
        }

        private void dgItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    if (e.ColumnIndex == 1)
                    {
                        //DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells["clmLedger"];
                        //if (cb.Value != null)
                        //{
                        //    string ledgerName = dgItems.Rows[e.RowIndex].Cells["clmLedger"].Value.ToString();
                        //    var ledger = listLedger.Where(l => l.Ledger_name == ledgerName).FirstOrDefault();
                        //    if (ledger != null)
                        //    {
                        //        dgItems["clmLedgerId", e.RowIndex].Value = ledger.Ledger_id;
                        //        dgItems["clmAmount", e.RowIndex].Value = 0;

                        //    }

                        //}
                    }
                    CalcTotal();
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
            if (chkCheque.Checked)
                grpChequeDetails.Show();
            else
                grpChequeDetails.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }



        private void PaymentForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.F3)
            //{
            //    if (dgItems.CurrentRow != null)
            //        LedgerSearch(dgItems.CurrentRow.Index);
            //}
            //else
            if (e.KeyData == Keys.F4)
            {
                LoadCostCenter();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dgItems.CurrentRow != null)
            {
                if (sender == linkSerach)
                    LedgerSearch(dgItems.CurrentRow.Index);
                else if (sender == linkCostCenter)
                    LoadCostCenter();
            }
        }



        private void LedgerSearch(int row)
        {
            LedgerSearchForm searchForm = new LedgerSearchForm();
            DialogResult result = searchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int ledgerId = 0;
                string ledgerName = string.Empty;
                ledgerId = searchForm.ledgerId;
                ledgerName = searchForm.ledgerName;
                if (ledgerId > 0)
                {
                    //DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[row].Cells["clmLedger"];
                    //cb.Value = ledgerName;
                    dgItems["clmLedgerId", row].Value = ledgerId;
                    dgItems["clmLedger", row].Value = ledgerName;
                }
            }
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {

        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.dgItems.CurrentCell.ColumnIndex == 1 && (e.KeyCode == Keys.F3))
            {
                LedgerSearch(dgItems.CurrentRow.Index);


            }
        }

        private void linkHide_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grpChequeDetails.Hide();
        }

        private void LoadCostCenter()
        {
            try
            {
                int row = dgItems.CurrentRow.Index;
                if (row >= 0)
                {
                    if (dgItems["clmLedgerId", row].Value != null)
                    {
                        int ledgerId = Convert.ToInt32(dgItems["clmLedgerId", row].Value);
                        BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();
                        if (accountLedgerBAL.IsCostCenterEnabled(ledgerId))
                        {
                            string ledgerName = dgItems["clmLedger", row].Value.ToString();
                            decimal amount = Convert.ToDecimal(dgItems["clmAmount", row].Value);
                            string _guid = Convert.ToString(dgItems["clmCostEntryId", row].Value);
                            //guid = Convert _guid;
                            if (ledgerId > 0 && amount > 0)
                            {
                                CostCenterEntryForm cost = new CostCenterEntryForm(_guid, ledgerId, ledgerName, amount, true);
                                DialogResult result = cost.ShowDialog();
                                if (result == DialogResult.OK)
                                {
                                    dgItems["clmRemarks", row].Value = "";
                                    if (!string.IsNullOrEmpty(cost.savedCostCenter))
                                    {
                                        dgItems["clmCostEntryId", row].Value = cost.guid;
                                        dgItems["clmRemarks", row].Value = cost.savedCostCenter;
                                        int numLines = cost.savedCostCenter.Split('\n').Length - 1;
                                        dgItems.Rows[row].Height = numLines * 30;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ex.Message);
            }
        }


    }



    class PaymentButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
