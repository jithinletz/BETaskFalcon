using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class JournalSearchForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
        }
        public int transactionNumber = 0;
        public bool copied = false;
        BAL.AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
        JournalSearchSearchButtonCollection button;
        bool openingBalance = false;
        string JournalType { get; set; }

        public JournalSearchForm()
        {
            InitializeComponent();
        }
        public JournalSearchForm(string type,DateTime date)
        {
            InitializeComponent();
            dtpDateFrom.Value = date;
            dtpDateTo.Value = date;
            JournalType = type;
        }
        public JournalSearchForm(bool _openingBalance)
        {
            this.openingBalance = _openingBalance;
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
                    ButtonActive(EnumFormEvents.FormLoad);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
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
        }
        private void Search()
        {
            try
            {

                General.ClearGrid(gridTransaction);
                List<EDMX.account_transaction> listTransaction = null;
                if (!this.openingBalance)
                {
                    if(JournalType==null || (JournalType!="PETTY" && JournalType != "OPENING"))
                    listTransaction = accountTransactionBAL.SearchTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.JOURNAL,0,txtContains.Text);
                    else if(JournalType=="OPENING")
                        listTransaction = accountTransactionBAL.SearchTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.OPENING, 0, txtContains.Text);

                    else
                        listTransaction = accountTransactionBAL.SearchTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.PETTY, 0,txtContains.Text);

                }
                else
                    listTransaction = accountTransactionBAL.SearchTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.OPENING);

                foreach (EDMX.account_transaction tran in listTransaction)
                {

                    gridTransaction.Rows.Add(tran.transaction_number, tran.transaction_type_id, General.ConvertDateAppFormat(tran.transaction_date), tran.account_ledger.ledger_name, tran.debit, tran.credit, $"{tran.transaction_type} - {tran.narration}","Copy this");
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }

        }
        private void SubmitItem()
        {
            try
            {
                if (gridTransaction.Rows.Count > 0)
                {
                    int ridx = gridTransaction.CurrentRow.Index;
                    int.TryParse(gridTransaction[ridx, 0].Value.ToString(), out transactionNumber);
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

        private void PaymentSearchForm_Load(object sender, EventArgs e)
        {
            button = new JournalSearchSearchButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose
            };
            Search();
        }

        private void gridTransaction_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                  
                    int.TryParse(gridTransaction[0, e.RowIndex].Value.ToString(), out transactionNumber);
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                    this.BeginInvoke(new MethodInvoker(Close));
                }
            }
        }

        private void gridTransaction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SubmitItem();
            }
        }

        private void gridTransaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                Clipboard.SetText(gridTransaction.Rows[e.RowIndex].Cells["clmTranId"].Value.ToString());
                this.transactionNumber = Convert.ToInt32(gridTransaction.Rows[e.RowIndex].Cells["clmTranId"].Value.ToString());
                copied = true;
                this.DialogResult = DialogResult.OK;

                try
                {
                    this.Close();
                }
                catch
                {
                    this.BeginInvoke(new MethodInvoker(Close));

                }
            }
        }

        private void txtContains_TextChanged(object sender, EventArgs e)
        {
            if (txtContains.Text.Length > 2 || string.IsNullOrEmpty(txtContains.Text))
            {
                Search();
            }
        }
       
    }
    class JournalSearchSearchButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }

    }
}
