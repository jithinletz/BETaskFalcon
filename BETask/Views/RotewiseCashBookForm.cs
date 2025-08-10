using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.Common;

namespace BETask.Views
{
    public partial class RotewiseCashBookForm : Form
    {
        CashBookButtonCollection button;
        DAL.DAL.AccountTransactionDAL accountTransaction = new DAL.DAL.AccountTransactionDAL();
        BAL.AccountTransactionBAL accountTransactionBAL = new BAL.AccountTransactionBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            Detailed
        }
        public RotewiseCashBookForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                 
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
            if (sender == button.BtnDetailed)
            {
                ButtonActive(EnumFormEvents.Detailed);
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
        private void Search()
        {
            try
            {
                accountTransactionBAL.UpdateRouteInTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value));
                General.ClearGrid(gridDaybook);
                lblDebit.Text = "";
                lblCredit.Text = "";
                List<BETask.DAL.Model.RoutewiseCashbookModel> listAccount = accountTransaction.RoutewiseCashbook(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                listAccount = listAccount.OrderBy(x => x.RouteName).ToList();
                if (listAccount != null && listAccount.Count > 0)
                {
                    foreach (BETask.DAL.Model.RoutewiseCashbookModel rc in listAccount)
                    {
                        gridDaybook.Rows.Add(rc.RouteId, rc.RouteName, rc.Debit, rc.Credit);
                    }
                    lblDebit.Text = listAccount.Sum(x => x.Debit).ToString();
                    lblCredit.Text = listAccount.Sum(x => x.Credit).ToString();
                    General.GridRownumber(gridDaybook);
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }

        }
        private void Print()
        {
            try
            {
                List<BETask.DAL.Model.RoutewiseCashbookModel> listAccount = accountTransaction.RoutewiseCashbook(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                listAccount = listAccount.OrderBy(x => x.RouteName).ToList();
                if (listAccount != null & listAccount.Count > 0)
                {
                    string header = $" Routewise Cash book between { General.ConvertDateAppFormat(dtpDateFrom.Value)} to { General.ConvertDateAppFormat(dtpDateTo.Value)}";
                    accountTransactionBAL.PrintRoutewiseCashbook(listAccount,header);
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
            button = new CashBookButtonCollection
            {
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSearch = btnSearch
            };
            try
            {
                accountTransactionBAL.UpdateRouteInTransaction(General.ConvertDateServerFormat(dtpDateFrom.Value));
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
            }
            //Search();
        }

        private void RotewiseCashBookForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlDetail.Hide();
        }

        private void gridDaybook_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    int routeId = Convert.ToInt32(gridDaybook["clmRouteId", e.RowIndex].Value);
                    DAL.DAL.AccountTransactionDAL account = new DAL.DAL.AccountTransactionDAL();
                    DataTable tblData = account.GetRoutewiseCashDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value), routeId);
                    gridDetailed.DataSource = tblData;
                    pnlDetail.Show();
                    General.GridRownumber(gridDetailed);
                    if (tblData.Rows.Count > 0)
                        lblDetailedTotal.Text = tblData.Compute("sum(Amount)","").ToString();
                }
                catch (Exception ex)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error,ex.Message.ToString());
                    General.Error(e.ToString());
                }
            }
        }
    }
    class CashBookButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnDetailed { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
