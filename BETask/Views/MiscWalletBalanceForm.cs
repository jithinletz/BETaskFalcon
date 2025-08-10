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
    public partial class MiscWalletBalanceForm : Form
    {
        CustomerWalletButtonCollection button;
        List<EDMX.customer> listCustomer = new List<EDMX.customer>();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            Update
        }

        public MiscWalletBalanceForm()
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
                case EnumFormEvents.Update:
                    UpdateAppWallet();
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
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnUpdate)
            {
                ButtonActive(EnumFormEvents.Update);
            }
        }
       

        private void GetAllRoutes()
        {
            try
            {
               BAL.RouteBAL  routeBAL = new BAL.RouteBAL() ;
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

        private void Search()
        {
            try
            {
                if (rdbCompare.Checked) btnSave.Show(); else btnSave.Hide();
                CustomerBAL customerBAL = new CustomerBAL();
                General.ClearGrid(gridCustomers);
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
              
                if (rdbCompare.Checked)
                {
                     listCustomer = customerBAL.GetWalletCustomer(routeId, txtWallet.Text, rdbMinus.Checked, rdbNoWallet.Checked);
                    if (listCustomer != null && listCustomer.Count > 0)
                    {
                        List<BETask.APP.EDMX.customer> listAppCutomer = customerBAL.SelectFilterCustomerApp(listCustomer.Select(x => x.customer_id).ToList());
                        if (listAppCutomer != null && listAppCutomer.Count > 0)
                        {
                            CompareWithCloud(listCustomer, listAppCutomer);
                        }
                    }
                    gridCustomers.Columns["clmAppBalance"].Visible = true;
                    gridCustomers.Columns["clmOutstanding"].Visible = true;
                }
                else
                {
                    gridCustomers.Columns["clmAppBalance"].Visible = false;
                    listCustomer = customerBAL.GetWalletCustomer(routeId, txtWallet.Text, rdbMinus.Checked, rdbNoWallet.Checked);
                    foreach (EDMX.customer cust in listCustomer)
                    {
                        gridCustomers.Rows.Add(cust.customer_id, cust.route.route_name, cust.customer_name, cust.wallet_number, cust.wallet_balance,"","",cust.app_phone is null ?cust.phone:cust.app_phone);
                    }
                    gridCustomers.Rows.Add("", "", "", "", listCustomer.Sum(x => x.wallet_balance));
                    General.GridBackcolorYellow(gridCustomers);
                    this.Text = $"Wallet Balance Report Customers count {listCustomer.Count()}";
                }
               
                
                General.GridRownumber(gridCustomers);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void Print()
        {
            CustomerBAL customerBAL = new CustomerBAL();
            int routeId = 0;
            if (cmbRoute.Text != "")
            {
                Object slectedRoute = cmbRoute.SelectedItem;
                routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;


            }
            customerBAL.PrintCustomerWalletMisc(listCustomer, routeId, txtWallet.Text, rdbMinus.Checked, rdbNoWallet.Checked);
        }

        private void CompareWithCloud(List<EDMX.customer> listCustomer, List<BETask.APP.EDMX.customer> listCustomerApp)
        {
            try
            {
                this.Text = $"{this.Text}   Customers count {listCustomer.Count()}";
                foreach (EDMX.customer cust in listCustomer)
                {
                    var appCustomer = listCustomerApp.Where(x => x.customer_id == cust.customer_id).FirstOrDefault();
                    if (appCustomer != null)
                    {
                        if (appCustomer.wallet_balance != cust.wallet_balance)
                        {
                            gridCustomers.Rows.Add(cust.customer_id, cust.route.route_name, cust.customer_name, cust.wallet_number, cust.wallet_balance, appCustomer.wallet_balance, cust.remarks);
                            Application.DoEvents();
                        }
                    }
                }
             
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async void UpdateAppWallet()
        {
            try
            {
                if (rdbCompare.Checked)
                {
                    List<EDMX.customer> listCustomer = new List<EDMX.customer>();
                    foreach (DataGridViewRow dr in gridCustomers.Rows)
                    {
                        if (dr != null)
                        {
                            decimal walletBal = dr.Cells["clmBalance"].Value == null ? 0 : Convert.ToDecimal(dr.Cells["clmBalance"].Value);
                            decimal outstanding = dr.Cells["clmOutstanding"].Value == null ? 0 : Convert.ToDecimal(dr.Cells["clmOutstanding"].Value);
                            decimal walletBalApp = dr.Cells["clmAppBalance"].Value == null ? 0 : Convert.ToDecimal(dr.Cells["clmAppBalance"].Value);
                            if (dr.Cells["clmCustomerId"].Value != null && ((walletBal == outstanding) && walletBal != walletBalApp))
                            {
                                listCustomer.Add(new EDMX.customer
                                {
                                    customer_id = Convert.ToInt32(dr.Cells["clmCustomerId"].Value),
                                    wallet_balance = Convert.ToDecimal(dr.Cells["clmBalance"].Value),
                                });
                            }
                        }
                    }
                    if (listCustomer.Count > 0)
                    {
                        SynchronizationBAL syncBAL = new SynchronizationBAL();
                        syncBAL.SyncLatestWallet(listCustomer);
                        General.ShowMessage(General.EnumMessageTypes.Success, $"Will be Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex,"Unable to Update");
            }
        }
    
        private void FormLoad()
        {
            button = new CustomerWalletButtonCollection
            {
                BtnSearch=btnSearch,
                BtnPrint=btnPrint,
                BtnClose=btnClose,
                BtnUpdate=btnSave
            };
            GetAllRoutes();
        }

        private void MiscWalletBalanceForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

      
    }
    class CustomerWalletButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnUpdate { get; set; }
    }
}
