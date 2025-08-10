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
    public partial class ReconsiliationForm : Form
    {
        ReconcilButtonCollection button = new ReconcilButtonCollection();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            Save
        }

        public ReconsiliationForm()
        {
            InitializeComponent();
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
                    break;
                case EnumFormEvents.Cancel:
                    ClearAll();
                    cmbLedgerAccount.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Save:
                    SaveReconcil();
                    break;
                case EnumFormEvents.Print:
                    // Print();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
        }
        private void GetAllBanks()
        {
            try
            {
                cmbLedgerAccount.Items.Clear();
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                List<EDMX.account_ledger> listBank = accountLedgerDAL.GetAllAccountLedger(groupId);
                listBank.AddRange(accountLedgerDAL.GetOtherBankAccountLedger());
                foreach (EDMX.account_ledger bank in listBank)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = bank.ledger_name,
                        Value = bank.ledger_id
                    };
                    cmbLedgerAccount.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void Clear()
        {
            lblDebit.Text = "0.00"; lblCredit.Text = "0.00";
            lblCreditReconcil.Text = "0.00"; lblDebitReconcil.Text = "0.00";
        }
        private void ClearAll()
        {
            lblDebit.Text = "0.00"; lblCredit.Text = "0.00";
            lblCreditReconcil.Text = "0.00"; lblDebitReconcil.Text = "0.00";
            General.ClearGrid(gridAccounts);
        }
        private void Search()
        {
            try
            {
                Clear();

                General.ClearGrid(gridAccounts);
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId = 0;
                if (cmbLedgerAccount.Text != "")
                {
                    Object selectedLedger = cmbLedgerAccount.SelectedItem;
                    ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                }

                if (ledgerId > 0)
                {

                    decimal opening = accountTransactionBAL.GetLedgerOpening(ledgerId,General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                    txtLedgerBalance.Text = opening.ToString();

                    decimal reconciledBalance = accountTransactionBAL.GetReconciledBalance(ledgerId, General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), opening);
                    txtReconciledBalance.Text = reconciledBalance.ToString();

                    List <DAL.Model.ReconciliationModel> listAccountTransaction = accountTransactionBAL.ReconciliationStatement(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, cmbType.Text);

                    foreach (DAL.Model.ReconciliationModel tran in listAccountTransaction)
                    {

                        gridAccounts.Rows.Add(false, General.ConvertDateAppFormat(tran.TransactionDate), tran.PartyAccount, tran.Debit, tran.Credit, tran.TransactionType, tran.Cheque, tran.ChequeBank, tran.ChequeDate, tran.Narration,tran.TransactionNumber,"");

                    }
                    General.GridRownumber(gridAccounts);
                    lblDebit.Text = listAccountTransaction.Sum(x => x.Debit).ToString();
                    lblCredit.Text = listAccountTransaction.Sum(x => x.Credit).ToString();
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SearchReconciled()
        {
            try
            {
                Clear();

                General.ClearGrid(gridReconciled);
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId = 0;
                if (cmbLedgerAccount.Text != "")
                {
                    Object selectedLedger = cmbLedgerAccount.SelectedItem;
                    ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                }

                if (ledgerId > 0)
                {

                    List<DAL.Model.ReconciliationModel> listAccountTransaction = accountTransactionBAL.ReconciliationStatementReconciled(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId);

                    foreach (DAL.Model.ReconciliationModel tran in listAccountTransaction)
                    {


                        gridReconciled.Rows.Add(tran.ReconcilDate, General.ConvertDateAppFormat(tran.TransactionDate), tran.PartyAccount, tran.Debit, tran.Credit, tran.TransactionType, tran.Cheque, tran.ChequeBank, tran.ChequeDate, tran.Narration, tran.TransactionNumber,"Undo");

                    }
                    General.GridRownumber(gridReconciled);
                    lblDebit.Text = listAccountTransaction.Sum(x => x.Debit).ToString();
                    lblCredit.Text = listAccountTransaction.Sum(x => x.Credit).ToString();
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void gridAccounts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex!=gridAccounts.Columns.Count-1)
                e.Cancel = true;
            else
            {
                if (gridAccounts.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Orange)
                    e.Cancel = true;
            }
        }


        private void Calculation()
        {
            try
            {
                gridAccounts.Refresh();
                decimal debitR = 0, creditR = 0;
                foreach (DataGridViewRow dr in gridAccounts.Rows)
                {
                    if (dr.Cells["chkReconsil"].Value.Equals(true))
                    {
                        debitR += Convert.ToDecimal(dr.Cells["clmDebit"].Value.ToString());
                        creditR += Convert.ToDecimal(dr.Cells["clmCredit"].Value.ToString());
                    }
                }
                lblCreditReconcil.Text = creditR.ToString();
                lblDebitReconcil.Text = debitR.ToString();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SaveReconcil()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    List<DAL.Model.ReconcilUpdateModel> listTransaction = new List<DAL.Model.ReconcilUpdateModel>();
                    foreach (DataGridViewRow dr in gridAccounts.Rows)
                    {
                        if (dr.Cells["chkReconsil"].Value.Equals(true))
                        {
                            if (!string.IsNullOrEmpty(dr.Cells["clmReconcilDateAdd"].Value.ToString()))
                            {
                                listTransaction.Add(new DAL.Model.ReconcilUpdateModel
                                {
                                    Id = Convert.ToInt32(dr.Cells["clmTransactionNumber"].Value.ToString()),
                                    Date = General.ConvertDateServerFormat_string(dr.Cells["clmReconcilDateAdd"].Value.ToString())
                                });
                            }
                        }

                    }
                    if (listTransaction != null && listTransaction.Count > 0)
                    {
                        AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                        accountTransactionBAL.SaveReconciliation(listTransaction, dtpDateFrom.Text, dtpDateTo.Text, cmbLedgerAccount.Text);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Reconciliation saved", "Success");
                    }
                    Search();
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void FormLoad()
        {
            button = new ReconcilButtonCollection
            {
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch
            };
            GetAllBanks();

        }
        private void ReconsiliationForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

       

      

        private void gridAccounts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridAccounts.CurrentCell.OwningColumn == gridAccounts.Columns["chkReconsil"] && gridAccounts.IsCurrentCellDirty)
            {
                gridAccounts.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Calculation();
                //your code goes here
            }
        }

        private void linkPrevious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkPrevious.Hide();
            Application.DoEvents();


            if (gridReconciled.Visible)
            {
                gridReconciled.Hide();
                Clear();
            }
            else
            {
                gridReconciled.Show();
                SearchReconciled();
            }
            linkPrevious.Show();
            Application.DoEvents();
        }

        private void gridReconciled_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 11)
            {
                try
                {
                    int tranNumber = Convert.ToInt32(gridReconciled[10, e.RowIndex].Value.ToString());
                    AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                    accountTransactionBAL.RemoveReconciliation(tranNumber);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Removed");
                    SearchReconciled();
                        
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }
    }
    class ReconcilButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnPrint { get; set; }

    }

}
