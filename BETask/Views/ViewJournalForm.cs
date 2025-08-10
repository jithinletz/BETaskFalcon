using System;
using BETask.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;


namespace BETask.Views
{
    public partial class ViewJournalForm : Form
    {
        List<EDMX.account_transaction> listAccountTransaction;
        DAL.DAL.AccountTransactionDAL accountTransactionDAL = new DAL.DAL.AccountTransactionDAL();
        bool isSaleCheck = false;
        public ViewJournalForm()
        {
            InitializeComponent();
        }
        public ViewJournalForm(int saleId)
        {
            InitializeComponent();
            LoadDataSale(saleId);
            isSaleCheck = true;
        }
        public ViewJournalForm(string voucher)
        {
            InitializeComponent();
            LoadDataCollection(voucher);
            isSaleCheck = true;
        }

        public ViewJournalForm(int transactionNumber,bool statement=true)
        {
            InitializeComponent();
            LoadDataStatement(transactionNumber);
            isSaleCheck = true;
        }
        private void LoadData()
        {
            if (!isSaleCheck)
            {
                try
                {
                    listAccountTransaction = accountTransactionDAL.GetLatestTransaction(Convert.ToInt32(numericUpDown1.Value));
                    List<int> listAdded = new List<int>();
                    General.ClearGrid(gridData);
                    foreach (EDMX.account_transaction tran in listAccountTransaction)
                    {
                        bool isInList = listAdded.IndexOf(tran.transaction_number) != -1;
                        if (!isInList)
                        {
                            gridData.Rows.Add(tran.transaction_number, tran.transaction_type, tran.transaction_type_id);
                            listAdded.Add(tran.transaction_number);
                        }
                    }
                }
                catch (Exception ee)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }

        private void LoadDataSale(int saleId)
        {
            try
            {
                listAccountTransaction = accountTransactionDAL.GetLatestTransactionBySaleId(saleId);
                List<int> listAdded = new List<int>();
                General.ClearGrid(gridData);
                foreach (EDMX.account_transaction tran in listAccountTransaction)
                {
                    bool isInList = listAdded.IndexOf(tran.transaction_number) != -1;
                    if (!isInList)
                    {
                        gridData.Rows.Add(tran.transaction_number, tran.transaction_type, tran.transaction_type_id);
                        listAdded.Add(tran.transaction_number);
                    }
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void LoadDataCollection(string voucher)
        {
            try
            {
                listAccountTransaction = accountTransactionDAL.GetLatestTransactionByVoucher(voucher);
                List<int> listAdded = new List<int>();
                General.ClearGrid(gridData);
                foreach (EDMX.account_transaction tran in listAccountTransaction)
                {
                    bool isInList = listAdded.IndexOf(tran.transaction_number) != -1;
                    if (!isInList)
                    {
                        gridData.Rows.Add(tran.transaction_number, tran.transaction_type, tran.transaction_type_id);
                        listAdded.Add(tran.transaction_number);
                    }
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void LoadDataStatement(int transactionNumber)
        {
            try
            {
                listAccountTransaction = accountTransactionDAL.GetLatestTransactionByTransaction(transactionNumber);
                List<int> listAdded = new List<int>();
                General.ClearGrid(gridData);
                foreach (EDMX.account_transaction tran in listAccountTransaction)
                {
                    bool isInList = listAdded.IndexOf(tran.transaction_number) != -1;
                    if (!isInList)
                    {
                        gridData.Rows.Add(tran.transaction_number, tran.transaction_type, tran.transaction_type_id);
                        listAdded.Add(tran.transaction_number);
                    }
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ViewJournalForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    int transaction = Convert.ToInt32(gridData["clmNumber", e.RowIndex].Value);
                    List<EDMX.account_transaction> listSelected = listAccountTransaction.Where(x => x.transaction_number == transaction).OrderByDescending(x => x.debit).ThenBy(x=>x.account_transaction_id).ToList();
                    General.ClearGrid(gridLedger);
                    foreach (EDMX.account_transaction tran in listSelected)
                    {
                        gridLedger.Rows.Add(tran.ledger_id,tran.account_ledger.ledger_name,tran.debit,tran.credit);
                    }
                    }
                catch (Exception ee)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }
    }
}
