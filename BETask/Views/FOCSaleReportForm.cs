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
    public partial class FOCSaleReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }

        FOCSaleReportButtonCollection button;
        SaleBAL saleBAL = new SaleBAL();
        public FOCSaleReportForm()
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
                    cmbCustomer.Text = string.Empty;
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
        private void GetAllSuppliers()
        {
            try
            {
                BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
                List<CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
                foreach (CustomerModel cust in _lstCustomers)
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
        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                List<EDMX.item>  listProducts = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listProducts)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbProductName.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                List<EDMX.route>  listRoute = routeBAL.GetAllRoutes();
                txtRoute.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    txtRoute.Items.Add(_cmbItem);

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
                int customerId = 0;
                int routeId = 0;
                int itemId = 0;

                if (!String.IsNullOrEmpty(txtRoute.Text))
                {
                    Object selectedRoute = txtRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }

                if (!String.IsNullOrEmpty(cmbCustomer.Text))
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }

                List<EDMX.sales_item> listSale = saleBAL.SearchItemFocReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), customerId, routeId, itemId);

                General.ClearGrid(gridSales);
                if (listSale != null && listSale.Count > 0)
                {
                    foreach (EDMX.sales_item sl in listSale)
                    {
                        gridSales.Rows.Add(sl.sales.customer.route.route_name, sl.sales.customer.customer_name, General.ConvertDateTimeAppFormat(sl.sales.sales_date), sl.sales_id, sl.item_id, sl.item.item_name, sl.qty);
                    }
                    gridSales.Rows.Add("", "", "", "", "", "Total", listSale.Sum(x => x.qty));
                    gridSales.Rows[gridSales.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                }
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
                int customerId = 0;
                int routeId = 0;
                int itemId = 0;

                if (!String.IsNullOrEmpty(txtRoute.Text))
                {
                    Object selectedRoute = txtRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }

                if (!String.IsNullOrEmpty(cmbCustomer.Text))
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                string header = $"{General.companyName} - Date between {dtpDateFrom.Text} and {dtpDateTo.Text} ";
               saleBAL.PrintFOCSalesReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), customerId, routeId, itemId,header);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
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
        private void FormLoad()
        {
            button = new FOCSaleReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            LoadProducts();
            GetAllRoutes();
            //GetAllSuppliers();
            Search();
        }

        private void FOCSaleReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);

            }
            else if (e.KeyData == Keys.Down)
            {
                CustomerSearch();
            }
            else if (e.KeyData != Keys.Back && e.KeyData != Keys.Delete)
                CustomerSearch();
        }
        private void CustomerSearch()
        {
            CustomerSearchForm searchForm = new CustomerSearchForm();
            DialogResult result = searchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int _customerId = 0;
                _customerId = searchForm.CustomerId;
                if (_customerId > 0)
                {
                    //ButtonActive(EnumFormEvents.Cancel);
                    GetSupplierDetailsById(_customerId);

                }
            }
        }
        private void GetSupplierDetailsById(int id)
        {

            cmbCustomer.Items.Clear();
            BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
            var objSupplier = customerBAL.GetCustomerDetail(id); //_lstCustomers.Where(s => s.Customer_Name == supplierName).FirstOrDefault();
            if (objSupplier != null)
            {
                GetAllSuppliers(objSupplier);
            }
        }
        private void GetAllSuppliers(CustomerModel customer)
        {
            try
            {

                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = customer.Customer_Name,
                        Value = customer.Customer_Id
                    };
                    cmbCustomer.Items.Add(_cmbItem);
                    cmbCustomer.SelectedIndex = 0;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

    }
    class FOCSaleReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
