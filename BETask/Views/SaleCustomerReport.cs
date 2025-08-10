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
    public partial class SaleCustomerReport : Form
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
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<CustomerModel> _lstCustomers = null;
        SaleCustomerReportButtonCollection button;
        bool purchaseSearch = false, purchaseOrderSearch = false;
        string xCustomerName = string.Empty;
        int xCustomerId = 0;
        public SaleCustomerReport()
        {
            InitializeComponent();
        }
        public SaleCustomerReport(bool _purchaseSearch, bool _purchaseOrderSearch = false)
        {
            InitializeComponent();
            this.purchaseSearch = _purchaseSearch;
            this.purchaseOrderSearch = _purchaseOrderSearch;
            this.Text = !_purchaseOrderSearch ? "Purchase supplier wise report" : "Purchase order supplier wise report";
            lblCustType.Text = "Supplier";
            clmSupplier.HeaderText = "Supplier";
        }
        public SaleCustomerReport(string customerName, int customerId)
        {
            InitializeComponent();
            xCustomerName = customerName;
            xCustomerId = customerId;
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
                    cmbSupplier.Text = string.Empty;
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
        private void SubmitItem()
        {
            try
            {
                if (gridPurchase.Rows.Count > 0)
                {
                    int ridx = gridPurchase.CurrentRow.Index;
                    int _purchaseId = 0;
                    int.TryParse(gridPurchase[ridx, 0].Value.ToString(), out _purchaseId);
                    saleId = _purchaseId;
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
        private void GetAllSuppliers(string customerName = "")
        {
            try
            {
                int searchType = purchaseSearch ? 2 : 1;
                _lstCustomers = _customerBAL.GetAllCustomers(0, customerName, searchType);
                foreach (CustomerModel cust in _lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cust.Customer_Name,
                        Value = cust.Customer_Id
                    };
                    cmbSupplier.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {

                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
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
        private void Search()
        {
            List<EDMX.sales> listSale = null;
            List<EDMX.purchase> listPurchase = null;
            List<EDMX.purchase_order> listPurchaseOrder = null;
            try
            {

                General.ClearGrid(gridPurchase);
                int vendorId = xCustomerId > 0 ? xCustomerId : 0;
                if (vendorId == 0)
                {
                    if (!String.IsNullOrEmpty(cmbSupplier.Text))
                        vendorId = General.GetComboBoxSelectedValue(cmbSupplier);
                }
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                string paymentMode = "All";
                if (cmbPaymentMode.Text != "")
                {
                    if (General.ValidatePaymentModes(cmbPaymentMode.Text))
                        paymentMode = cmbPaymentMode.Text;
                }
                if (!purchaseSearch)
                {
                    listSale = saleBAL.CustomerSalesReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, paymentMode, routeId);
                    foreach (EDMX.sales sale in listSale)
                    {
                        gridPurchase.Rows.Add(sale.sales_id, sale.customer.customer_name, General.ConvertDateAppFormat(sale.sales_date), sale.sales_number, sale.net_amount, sale.payment_mode, sale.balance_amount);
                    }
                    lblNetAmount.Text = String.Format("{0} {1}", "Net Sales Amount ", listSale.Sum(c => c.net_amount));
                }
                else
                {
                    if (purchaseSearch)
                    {
                        PurchaseBAL purchaseBAL = new PurchaseBAL();
                        listPurchase = purchaseBAL.SupplierPurchaeReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId, paymentMode);
                        foreach (EDMX.purchase purchase in listPurchase)
                        {
                            gridPurchase.Rows.Add(purchase.purchase_id, purchase.customer.customer_name, General.ConvertDateAppFormat(purchase.invoice_date), purchase.invoice_number, purchase.net_amount, purchase.payment_mode, purchase.balance_amount);
                        }
                        lblNetAmount.Text = String.Format("{0} {1}", "Net Purchase Amount ", listPurchase.Sum(c => c.net_amount));
                    }
                    else
                    {
                        PurchaseOrderBAL purchaseOrderBAL = new PurchaseOrderBAL();
                        listPurchaseOrder = purchaseOrderBAL.SupplierPurchaeReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId, paymentMode);
                        foreach (EDMX.purchase purchase in listPurchase)
                        {
                            gridPurchase.Rows.Add(purchase.purchase_id, purchase.customer.customer_name, General.ConvertDateAppFormat(purchase.invoice_date), purchase.purchase_id, purchase.net_amount, purchase.payment_mode, purchase.balance_amount);
                        }
                        lblNetAmount.Text = String.Format("{0} {1}", "Net Purchase Order Amount ", listPurchase.Sum(c => c.net_amount));
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

        private void Print()
        {
            try
            {
                int vendorId = 0;
                if (xCustomerId == 0)
                {
                    if (!String.IsNullOrEmpty(cmbSupplier.Text))
                        int.TryParse(_lstCustomers.Where(x => x.Customer_Name == cmbSupplier.Text).FirstOrDefault().Customer_Id.ToString(), out vendorId);
                }
                else
                    vendorId = xCustomerId;
                string paymentMode = "All";
                if (cmbPaymentMode.Text != "")
                {
                    if (General.ValidatePaymentModes(cmbPaymentMode.Text))
                        paymentMode = cmbPaymentMode.Text;
                }
                if (!this.purchaseSearch && !this.purchaseOrderSearch)
                {
                    int routeId = 0;
                    if (!String.IsNullOrEmpty(cmbRoute.Text))
                    {
                        Object selectedRoute = cmbRoute.SelectedItem;
                        routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                    }
                    string header = "";
                    header = $"{General.companyName}, Date between {General.ConvertDateAppFormat(dtpDateFrom.Value)} and {General.ConvertDateAppFormat(dtpDateTo.Value)} , {cmbRoute.Text} ";
                    saleBAL.PrintCustomerSalesReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, paymentMode.ToLower(), routeId, header);
                }
                else
                {
                    if (this.purchaseSearch)
                    {
                        PurchaseBAL purchaseBAL = new PurchaseBAL();
                        purchaseBAL.PrintSupplierSalesReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, paymentMode);
                    }
                    else
                    {
                        PurchaseOrderBAL purchaseBAL = new PurchaseOrderBAL();
                        purchaseBAL.PrintSupplierSalesReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, paymentMode);
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
        private void gridPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    int _saleId = 0;
                    int.TryParse(gridPurchase[0, e.RowIndex].Value.ToString(), out _saleId);
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

        private void rdlAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlAll.Checked)
                cmbPaymentMode.Text = "";
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
                ComboboxItem _cmbItem = new ComboboxItem()
                {
                    Text = customer.Customer_Name,
                    Value = customer.Customer_Id
                };
                cmbSupplier.Items.Add(_cmbItem);
                cmbSupplier.SelectedIndex = 0;

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new SaleCustomerReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };

            General.BindPaymentModes(cmbPaymentMode);
            cmbPaymentMode.SelectedIndex = -1;
            //GetAllSuppliers(xCustomerName.ToString());
            GetAllRoutes();

            if (!string.IsNullOrEmpty(xCustomerName))
            {
                cmbSupplier.Text = xCustomerName;
                dtpDateFrom.Value = DateTime.Now.AddMonths(-3);
            }
            Search();

        }

    }
    class SaleCustomerReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
