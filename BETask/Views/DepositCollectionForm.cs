using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Drawing;
using System.Data;

namespace BETask.Views
{
    public partial class DepositCollectionForm:Form
    {
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<CustomerModel> _lstCustomers = null;
        List<EDMX.item> listProducts = new List<EDMX.item>();
        int xCustomerId = 0;
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Search,
            Cancel,
            Close,
            Print          

        }
        DepositCollectionButtonCollection button;
        public DepositCollectionForm()
        {
            InitializeComponent();
        }

        public DepositCollectionForm(DateTime date)
        {
            InitializeComponent();
            dtpFrom.Value = date;
            dtpTo.Value = date;
            FormLoad();            
            Search();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    General.ClearGrid(gridCollection);

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

            if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
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

        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();
                cmbRoute.Items.Clear();
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
            LoadProducts();
            GetAllRoutes();
            Search();

        }

        private void Search()
        {
            try
            {
                General.ClearGrid(gridCollection);
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int routeId = 0, itemId = 0,employeeId=0;
                int vendorId = xCustomerId;

                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (cmbSupplier.Text != "")
                {
                    
                    Object selectedProduct = cmbSupplier.SelectedItem;
                    vendorId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }

                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }


                DataTable collections = deliveryBAL.GetDepositCollection(General.ConvertDateServerFormatWithStartTime(dtpFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpTo.Value), vendorId, itemId, routeId);
                if (collections != null && collections.Rows.Count > 0)
                {
                    foreach (DataRow dr in collections.Rows)
                    {
                        gridCollection.Rows.Add(General.ConvertDateAppFormat(Convert.ToDateTime(dr["DeliveryTime"].ToString())), dr["RouteName"], dr["CustomerName"], dr["ItemName"],dr["Qty"], dr["CollectionAmount"],dr["IsRefund"]);
                    }
                    gridCollection.Rows.Add("","","","", collections.Compute("Sum(Qty)","").ToString(), collections.Compute("Sum(CollectionAmount)", "").ToString(), "");
                    General.GridBackcolorYellow(gridCollection);
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
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int routeId = 0, itemId = 0;
                int vendorId = xCustomerId;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (cmbProductName.Text != "")
                {
                    Object selectedRoute = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (cmbSupplier.Text != "")
                {
                    Object selectedRoute = cmbSupplier.SelectedItem;
                    vendorId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                deliveryBAL.PrintDepositCollection(dtpFrom.Value, dtpTo.Value, vendorId, itemId, routeId,cmbProductName.Text);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void DepositCollection_Load(object sender, EventArgs e)
        {
            button = new DepositCollectionButtonCollection
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
                
            };
            
            FormLoad();
            General.SetScreenSize(sender, e, this);
        }

       

        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetAllItem_Sellable();
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

            cmbSupplier.Items.Clear();
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
                    cmbSupplier.Items.Add(_cmbItem);
                    cmbSupplier.SelectedIndex = 0;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void cmbSupplier_KeyDown(object sender, KeyEventArgs e)
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
    }
    class DepositCollectionButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }

    }
}
