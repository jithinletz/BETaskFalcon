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
    public partial class CashStatementRoutewise : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        CashSatatementRoutewiseButtonCollection button;
        public CashStatementRoutewise()
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
        private void Search()
        {
            try
            {
                btnSearch.Hide();
                Application.DoEvents();
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                General.ClearGrid(gridCustomers);
                Application.DoEvents();
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                lblCredit.Text = "0.00";
                lblCredit.Text = "0.00";
                List<DAL.Model.CashStatementRoutewiseModel> listCustomer = accountTransactionBAL.GetCashStatementRoutewise(General.ConvertDateServerFormat(dtpDateFrom.Value), routeId);
                listCustomer = listCustomer.OrderBy(x => x.CustomerName).ToList();
                if (listCustomer != null && listCustomer.Count > 0)
                {
                    foreach (DAL.Model.CashStatementRoutewiseModel cs in listCustomer)
                    {
                        gridCustomers.Rows.Add(cs.CustomerId, cs.CustomerName,cs.TranType, cs.Debit, cs.Credit, cs.Narration);
                        //if(cs.Debit!=cs.Credit)
                        //    General.GridBackcolorRed(gridCustomers);
                        Application.DoEvents();
                        gridCustomers.CurrentCell = gridCustomers.Rows[gridCustomers.Rows.Count-1].Cells[0];
                        //gridCustomers.Rows[gridCustomers.Rows.Count - 1].Selected = true;
                    }
                    lblDebit.Text = listCustomer.Sum(x => x.Debit).ToString();
                    lblCredit.Text = listCustomer.Sum(x => x.Credit).ToString();
                    //gridCustomers.Rows.Add("", "", "", listCustomer.Sum(x => x.Debit), listCustomer.Sum(x => x.Credit));
                    // General.GridBackcolorYellow(gridCustomers);
                    General.GridRownumber(gridCustomers);
                   
                }
                btnSearch.Show();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Print()
        { }

        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();

                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    cmbRoute.Items.Add(_cmbItem);

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
            button = new CashSatatementRoutewiseButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            GetAllRoutes();
        }

        private void CashStatementRoutewise_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
    }
    class CashSatatementRoutewiseButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
    }
}
