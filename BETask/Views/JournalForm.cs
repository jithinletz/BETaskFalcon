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
    public partial class JournalForm : Form
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
        JournalButtonCollection button;
        List<Model.AccountLedgerModel> listLedger;
        int transactionNumber = 0, transactionTypeId = 0;

        public JournalForm()
        {
            InitializeComponent();

            this.dgItems.Rows.Add();
            this.dgItems.Columns[0].ReadOnly = true;
            //dgItems.CellEnter += DgItems_CellEnter;

        }

        //private void DgItems_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if(initalload == 0)
        //        {
        //            initalload++;
        //            return;
        //        }
        //        if (this.dgItems.CurrentCell.ColumnIndex == 1)
        //        {
        //            txtCode.Text = "";
        //            txtName.Text = "";
        //            FrmSearchLedger frmSearchLedger = new FrmSearchLedger(panel2, this.txtCode.Name, this.txtName.Name);
        //            frmSearchLedger.ShowDialog();

        //            if (!txtCode.Text.Trim().Equals("") && !txtName.Text.Trim().Equals(""))
        //            {
        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[6].ReadOnly = true;
        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[1].ReadOnly = true;

        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[6].Value = txtCode.Text.Trim().ToString();
        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[1].Value = txtName.Text.Trim().ToString();
        //                if (dgItems.Rows.Count - 1 == dgItems.CurrentRow.Index)
        //                {
        //                    //this.dgItems.Rows.Add();
        //                }
        //            }


        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        General.Error(Ex.Message);
        //    }
        //}

        //private void DgItems_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {

        //        if (this.dgItems.CurrentCell.ColumnIndex == 1 && e.KeyChar == (char)Keys.Down)
        //        {
        //            txtCode.Text = "";
        //            txtName.Text = "";
        //            FrmSearchLedger frmSearchLedger = new FrmSearchLedger(panel2, this.txtCode.Name, this.txtName.Name);
        //            frmSearchLedger.ShowDialog();

        //            if (!txtCode.Text.Trim().Equals("") && !txtName.Text.Trim().Equals(""))
        //            {
        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[6].ReadOnly = true;
        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[1].ReadOnly = true;

        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[6].Value = txtCode.Text.Trim().ToString();
        //                dgItems.Rows[dgItems.CurrentRow.Index].Cells[1].Value = txtName.Text.Trim().ToString();
        //                if (dgItems.Rows.Count - 1 == dgItems.CurrentRow.Index)
        //                {
        //                    this.dgItems.Rows.Add();
        //                }
        //            }


        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        General.Error(Ex.Message);
        //    }
        //}



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
                    SaveJournal();
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    if (btnNew.Text.ToLower().Contains("new"))
                        txtDocumentNo.Text = DAL.DAL.DocumentSerialDAL.GetNextDocument(DAL.DAL.DocumentSerialDAL.EnumDocuments.JOURNAL.ToString()).ToString();

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
                    Search();
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

            dtpDate.Value = DateTime.Today;
            General.ClearGrid(dgItems);
            this.transactionNumber = 0;
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
        private void Search()
        {
            JournalSearchForm journalSearchForm = new JournalSearchForm();
            DialogResult result = journalSearchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int _transactionNumber = 0;
                _transactionNumber = journalSearchForm.transactionNumber;
                if (_transactionNumber > 0)
                {
                    ButtonActive(EnumFormEvents.Cancel);
                    PopulateJournalDetails(_transactionNumber, journalSearchForm.copied);
                    if (!journalSearchForm.copied)
                        ButtonActive(EnumFormEvents.Update);
                    else
                        ButtonActive(EnumFormEvents.New);
                }
            }
        }
        private void LoadAllLedger(int groupId = -1)
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                listLedger = accountLedgerBAL.GetAllAccountLedger(groupId).OrderBy(t => t.Ledger_name).ToList();
                DataGridViewComboBoxColumn comboLedger = (DataGridViewComboBoxColumn)dgItems.Columns["clmLedger"];
                //comboLedger.Items.Clear();
                comboLedger.HeaderText = "Select Account";
                comboLedger.DataSource = listLedger;
                comboLedger.DisplayMember = "Ledger_name";
                comboLedger.ValueMember = "Ledger_id";


                //DataGridViewComboBoxColumn comboLedger1 = (DataGridViewComboBoxColumn)dataGridView1.Columns[1];
                ////comboLedger.Items.Clear();
                //comboLedger1.HeaderText = "Select Account";
                //comboLedger1.DataSource = listLedger;
                //comboLedger1.DisplayMember = "Ledger_name";
                //comboLedger1.ValueMember = "Ledger_id";

                //foreach (Model.AccountLedgerModel ledger in listLedger)
                //{
                //    comboLedger.Items.Add(ledger.Ledger_name);
                //}
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void LoadLedgerbyType()
        {
            try
            {
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
                //  DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                int groupId = -1;
                if (rdbCustomer.Checked)
                    groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.CUSTOMER).group_id);
                else if (rdbSupplier.Checked)
                    groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.SUPPLIER).group_id);
                LoadAllLedger(groupId);

            }
            catch (Exception ee)
            { }
        }

        private bool Validation()
        {
            bool resp = true;


            if (dgItems.Rows.Count <= 1)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please add transactions");
                dgItems.Focus();
                resp = false;
            }

            decimal debit = General.ParseDecimal(txtDebit.Text);
            decimal credit = General.ParseDecimal(txtCredit.Text);
            if (debit <= 0)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please add transactions");
                dgItems.Focus();
                resp = false;
            }
            else if (debit != credit)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Debit and creit side not equal . Please check it");
                dgItems.Focus();
                resp = false;

            }
            if (resp)
            {
                BAL.CostCenterBAL costCenterBAL = new BAL.CostCenterBAL();
                resp = costCenterBAL.ValidateCostCenterAdded(dgItems, "clmName");
            }
            return resp;
        }
        private List<EDMX.account_transaction> GetAccountTransactionList()
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {
                //Dr & Cr
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    if ((row.Cells["clmDebit"].Value != null || row.Cells["clmCredit"].Value != null) && row.Cells["clmLedgerId"].Value != null)
                    {
                        decimal debit = 0, credit = 0;
                        string remarks = "";
                        debit = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmDebit"].Value == null ? "0" : row.Cells["clmDebit"].Value.ToString()));
                        credit = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmCredit"].Value == null ? "0" : row.Cells["clmCredit"].Value.ToString()));
                        int _ledgerId = General.ParseInt(row.Cells["clmLedgerId"].Value.ToString());
                        remarks = row.Cells["clmRemarks"].Value != null ? row.Cells["clmRemarks"].Value.ToString() : "";
                        string referenceId = row.Cells["clmCostEntryId"].Value == null ? string.Empty : row.Cells["clmCostEntryId"].Value.ToString();
                        int transactionTypeId = General.ParseInt(txtDocumentNo.Text);

                        listAccount.Add(new EDMX.account_transaction
                        {
                            ledger_id = _ledgerId,
                            transaction_date = General.ConvertDateServerFormat(dtpDate.Value),
                            credit = credit,
                            debit = debit,
                            narration = remarks,
                            transaction_type = REP.AccountTransactionDAL.EnumTransactionTypes.JOURNAL.ToString(),
                            transaction_type_id = transactionTypeId,
                            transaction_number = this.transactionNumber,
                            voucher_number = "",
                            status = 1,
                            reference_id = referenceId

                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listAccount;
        }
        private void SaveJournal()
        {
            try
            {
                BAL.CostCenterBAL costCenterBAL = new BAL.CostCenterBAL();

                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        decimal debit = Convert.ToDecimal(txtDebit.Text);
                        decimal credit = Convert.ToDecimal(txtCredit.Text);
                        if (debit == credit)
                        {
                            if (General.CheckFinancialDate(dtpDate.Value))
                            {
                                List<EDMX.account_transaction> listAccount = GetAccountTransactionList();

                               
                                    if (listAccount != null && listAccount.Count > 0)
                                    {
                                    if (costCenterBAL.ValidateCostCenter(listAccount))
                                    {
                                        BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                                        string referance = "";
                                        int transactionNumber = accountTransactionBAL.SaveAccountTransaction(listAccount, REP.AccountTransactionDAL.EnumTransactionTypes.JOURNAL, ref referance);
                                        General.Action($"New Jounral Saved for {txtCredit.Text} {txtRemarks.Text}");
                                        General.ShowMessage(General.EnumMessageTypes.Success, "Journal Entry Successfully saved");
                                        ButtonActive(EnumFormEvents.Cancel);
                                        // PopulateJournalDetails(transactionNumber);
                                    }
                                    else
                                    {
                                        General.ShowMessage(General.EnumMessageTypes.Warning, "There is a mismatch in Cost center please confirm first", "Cost center mismatch found");
                                        return;
                                    }

                                }
                               
                            }
                        }
                        else
                        {
                            General.ShowMessage(General.EnumMessageTypes.Warning, "Debit and Credit should be same");
                            return;
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
        private void PopulateJournalDetails(int _transactionNumber, bool copied = false)
        {
            try
            {
                General.ClearGrid(dgItems);
                this.transactionNumber = !copied ? _transactionNumber : 0;
                BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                List<EDMX.account_transaction> listTransaction = accountTransactionBAL.GetAccountTransactionDetail(_transactionNumber, REP.AccountTransactionDAL.EnumTransactionTypes.JOURNAL.ToString());
                this.transactionTypeId = Convert.ToInt32(listTransaction[0].transaction_type_id);
                txtRemarks.Text = listTransaction[0].narration;
                dtpDate.Value = !copied ? listTransaction[0].transaction_date : DateTime.Now;
                txtDocumentNo.Text = listTransaction[0].transaction_type_id.ToString();
                foreach (EDMX.account_transaction tran in listTransaction)
                {
                    dgItems.Rows.Add(tran.ledger_id, tran.account_ledger.ledger_name, tran.debit, tran.credit, tran.narration, tran.reference_id, tran.ledger_id);
                }
                dgItems.Rows.Add();


                CalcTotal();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void Print()
        {
            try
            {
                if (this.transactionNumber > 0)
                {
                    BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
                    accountTransactionBAL.PrintJournalVoucher(this.transactionNumber);
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
            button = new JournalButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
            ButtonActive(EnumFormEvents.FormLoad);
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
                decimal debit = 0, credit = 0;
                foreach (DataGridViewRow row in dgItems.Rows)
                {
                    if (row.Cells["clmDebit"].Value != null)
                    {
                        decimal _total = 0;
                        _total = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmDebit"].Value.ToString()));
                        debit += _total;
                    }
                    if (row.Cells["clmCredit"].Value != null)
                    {
                        decimal _total = 0;
                        _total = General.TruncateDecimalPlaces(General.ParseDecimal(row.Cells["clmCredit"].Value.ToString()));
                        credit += _total;
                    }

                }
                txtDebit.Text = debit.ToString();
                txtCredit.Text = credit.ToString();
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
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells["clmLedger"];
                        if (cb.Value != null)
                        {
                            string ledgerName = dgItems.Rows[e.RowIndex].Cells["clmLedger"].Value.ToString();
                            var ledger = listLedger.Where(l => l.Ledger_name == ledgerName).FirstOrDefault();
                            if (ledger != null)
                            {
                                dgItems["clmLedgerId", e.RowIndex].Value = ledger.Ledger_id;
                                dgItems["clmDebit", e.RowIndex].Value = 0;
                                dgItems["clmCredit", e.RowIndex].Value = 0;
                            }

                        }
                    }
                    CalcTotal();
                }
                dgItems.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            //LoadLedgerbyType();
        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete)
                {

                    if (dgItems.Rows.Count > 1)
                    {
                        int row = dgItems.CurrentRow.Index;
                        if (dgItems[0, row].Value != null)
                        {
                            dgItems.Rows.RemoveAt(row);
                        }
                    }
                }
                if (this.dgItems.CurrentCell.ColumnIndex == 1 && (e.KeyCode == Keys.F3))
                {
                    LedgerSearch(dgItems.CurrentRow.Index);
                }
                CalcTotal();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void JournalForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.F4)
            {
                LoadCostCenter();
            }
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
                            string ledgerName = dgItems["clmName", row].Value.ToString();
                            bool isDebit = true;
                            decimal amount = Convert.ToDecimal(dgItems["clmDebit", row].Value);
                            if (amount == 0)
                            {
                                isDebit = false;
                                amount = Convert.ToDecimal(dgItems["clmCredit", row].Value);
                            }
                            string _guid = Convert.ToString(dgItems["clmCostEntryId", row].Value);
                            //guid = Convert _guid;
                            if (ledgerId > 0 && amount > 0)
                            {
                                CostCenterEntryForm cost = new CostCenterEntryForm(_guid, ledgerId, ledgerName, amount, isDebit);
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

        private void linkSerach_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                    // DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[row].Cells["clmLedger"];
                    // cb.Value = ledgerName;
                    dgItems["clmId", row].Value = ledgerId;
                    dgItems["clmName", row].Value = ledgerName;
                    dgItems["clmLedgerId", row].Value = ledgerId;

                }
            }
        }

        private void JournalForm_Load(object sender, EventArgs e)
        {

        }

        private void dgItems_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
    }
    class JournalButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
