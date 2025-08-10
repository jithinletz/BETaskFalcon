using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;

namespace BETask.Views
{
    public partial class PaymentReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public int saleId = 0;
        SaleBAL saleBAL = new SaleBAL();
        BAL.AccountTransactionBAL accountLedgerBAL = new AccountTransactionBAL();


        List<Model.AccountLedgerModel> listLedger;


        PaymentReportButtonCollection button ;


        public PaymentReportForm()
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
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbLedgerAccount.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
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
        }
     
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                listLedger = accountLedgerBAL.GetAllAccountLedger(-1);
                foreach (Model.AccountLedgerModel ledger in listLedger)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.Ledger_name,
                        Value = ledger.Ledger_id
                    };
                    cmbLedgerAccount.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void Search()
        {
            try
            {

                General.ClearGrid(gridAccounts);
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId= 0;
                if (cmbLedgerAccount.Text != "")
                {
                    Object selectedLedger = cmbLedgerAccount.SelectedItem;
                    ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                }
                List<EDMX.account_transaction> listAccountTransaction = accountTransactionBAL.GetPaymentRecieptReport(ledgerId,General.ConvertDateServerFormat(dtpDateFrom.Value),General.ConvertDateServerFormat(dtpDateTo.Value));
                decimal totalPayment = 0, totalReceipt = 0;
                foreach (EDMX.account_transaction tran in listAccountTransaction)
                {
                    if(chkPayment.Checked)
                    {
                        if(tran.debit>0 && tran.transaction_type==DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString())
                        {
                            gridAccounts.Rows.Add(General.ConvertDateAppFormat(tran.transaction_date),tran.account_ledger.ledger_name,tran.narration,tran.debit,0,tran.transaction_type_id);
                            totalPayment += tran.debit;
                        }
                    }
                    if (chkReciept.Checked)
                    {
                        if (tran.credit > 0 && (tran.transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString() || tran.transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.WALLET.ToString()))
                        {
                            gridAccounts.Rows.Add(General.ConvertDateAppFormat(tran.transaction_date), tran.account_ledger.ledger_name, tran.narration, 0,tran.credit,tran.transaction_type_id);
                            totalReceipt += tran.credit;
                        }
                    }

                }
                lblPayment.Text=totalPayment.ToString();
                lblReciept.Text = totalReceipt.ToString();

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
            
        }
       
        private void Print()
        {
            try
            {

                DataTable tblPayment = new DataTable();
                BETask.Report.DSReports.PaymentReportDataTable paymentReportDataTable = new Report.DSReports.PaymentReportDataTable();
                tblPayment = paymentReportDataTable.Clone();
                if (gridAccounts.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dr in gridAccounts.Rows)
                    {
                        DataRow row = tblPayment.NewRow();
                        row["TransactioDate"] = dr.Cells["clmDate"].Value;
                        row["Ledger"] = dr.Cells["clmLedger"].Value;
                        row["Narration"] = dr.Cells["clmNarration"].Value;
                        row["PaymentAmount"] = dr.Cells["clmPayment"].Value;
                        row["RecieptAmount"] = dr.Cells["clmReciept"].Value;
                        row["Document"] = dr.Cells["clmDocument"].Value;
                        tblPayment.Rows.Add(row);
                    }
                    string header = String.Format("{0} {1} {2} {3} {4}", chkPayment.Checked ? " Payment " : "", chkReciept.Checked ? "Reciept" : "", "Report",$" for the date between {dtpDateFrom.Text} and {dtpDateTo.Text} ",cmbLedgerAccount.Text!="" ?$" of {cmbLedgerAccount.Text} ":"");
                    AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                    accountTransactionBAL.PrintPaymentReport(tblPayment, header);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
              
            }
        }
        private void gridPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex>=0)
            {
                try
                {
                    int _saleId = 0;
                    int.TryParse(gridAccounts[0,e.RowIndex].Value.ToString(), out _saleId);
                    saleId = _saleId;
                   
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                    this.BeginInvoke(new MethodInvoker(Close));
                }
            }

        }
     

        private void gridPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
               // submitItem();
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new PaymentReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            Search();
            LoadAllLedger();

        }
      
    }
    class PaymentReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
