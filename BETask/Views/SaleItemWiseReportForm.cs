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
    public partial class SaleItemWiseReportForm : Form
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
        List<EDMX.item> listProducts = new List<EDMX.item>();
        SaleItemwiseReportButtonCollection button;
        bool purchaseSearch = false, purchaseOrderSearch = false;
        string xCustomerName = string.Empty;
        int xCustomerId = 0;

        public SaleItemWiseReportForm()
        {
            InitializeComponent();

        }
        public SaleItemWiseReportForm(string customerName,int customerId)
        {
            InitializeComponent();
            xCustomerName = customerName;
            xCustomerId = customerId;

        }
        public SaleItemWiseReportForm(bool _purchaseSearch, bool _purchaseOrderSearch = false)
        {
            InitializeComponent();
            this.purchaseSearch = _purchaseSearch;
            this.purchaseOrderSearch = _purchaseOrderSearch;
            this.Text = !_purchaseOrderSearch ? "Purchase item report" : "Purchase order item report";
            lblCustType.Text = "Supplier";

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

        private void GetAllSuppliers()
        {
            try
            {
                _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
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
            try
            {

                General.ClearGrid(gridPurchase);
                int vendorId = xCustomerId, routeId = 0, itemId = 0;

                if (vendorId == 0)
                {
                    if (!String.IsNullOrEmpty(cmbSupplier.Text))
                        vendorId = General.GetComboBoxSelectedValue(cmbSupplier);
                }

                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }


                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                string paymentMode = "all";
                if (rdlBank.Checked) paymentMode = "bank";
                else if (rdlCash.Checked) paymentMode = "cash";
                else if (rdlCoupon.Checked) paymentMode = "coupon";
                else if (rdlCredit.Checked) paymentMode = "credit";
                decimal rangeFrom = Convert.ToDecimal(txtRangeFrom.Text);
                decimal rangeTo = Convert.ToDecimal(txtRangeTo.Text);

                if (!purchaseSearch && !purchaseOrderSearch)
                {



                    var listSale = saleBAL.ItemSalesReportNew(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, itemId, routeId,rangeFrom,rangeTo,cmbPaymentmode.Text.ToLower());

                    gridPurchase.SuspendLayout();
                    List<DataGridViewRow> rows = new List<DataGridViewRow>();

                    foreach (var sale in listSale)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(gridPurchase,sale.CustomerName, General.ConvertDateAppFormat(sale.SalesDate), sale.ItemId, sale.ItemName, sale.UOMName, sale.Quantity,General.TruncateDecimalPlaces( sale.Rate), General.TruncateDecimalPlaces(sale.GrossAmount), General.TruncateDecimalPlaces(sale.Discount), General.TruncateDecimalPlaces(sale.TotalBeforeVAT), General.TruncateDecimalPlaces(sale.VATAmount), General.TruncateDecimalPlaces(sale.NetAmount), sale.SalesNumber, sale.PaymentMode);
                        rows.Add(row);
                    }

                    gridPurchase.Rows.AddRange(rows.ToArray()); // Add all rows at once
                    gridPurchase.ResumeLayout(); // Resume layout updates
                    gridPurchase.RowTemplate.Height = 60;


                    //foreach (EDMX.sales_item sale in listSale)
                    //{
                    //    gridPurchase.Rows.Add(sale.sales.customer.customer_name, General.ConvertDateAppFormat(sale.sales.sales_date), sale.item_id, sale.item.item_name, sale.item.uom_setting.uom_name, sale.qty, sale.rate, sale.gross_amount, sale.discount, sale.total_beforvat, sale.vat_amount, sale.net_amount, sale.sales.sales_number,sale.sales.payment_mode);
                    //}
                    decimal foc = 0;
                    string _foc = "";
                    try
                    {
                        foc = listSale.Where(x => x.Rate <= 0).Sum(x => x.Quantity);

                        if (foc > 0) _foc = $"FOC {foc }";
                    }
                    catch { }
                    gridPurchase.Rows.Add($"({listSale.Count.ToString()})  rows", "", "", "", _foc, listSale.Sum(x => x.Quantity), "0", General.TruncateDecimalPlaces(listSale.Sum(x => x.GrossAmount)), General.TruncateDecimalPlaces(listSale.Sum(x => x.Discount)), General.TruncateDecimalPlaces(listSale.Sum(x => x.TotalBeforeVAT)), General.TruncateDecimalPlaces(listSale.Sum(x => x.VATAmount)), General.TruncateDecimalPlaces(listSale.Sum(x => x.NetAmount)));
                    General.GridBackcolorYellow(gridPurchase);


                    //lblNetAmount.Text = String.Format("{0} {1}", "Net Sales Amount ", listSale.Sum(c => c.net_amount));
                }
                else
                {
                    if (purchaseSearch)
                    {
                        PurchaseBAL purchaseBAL = new PurchaseBAL();
                        List<EDMX.purchase_item> listPurchase = purchaseBAL.ItemPurchaseReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, itemId);
                        foreach (EDMX.purchase_item purchase in listPurchase)
                        {
                            gridPurchase.Rows.Add(purchase.purchase.customer.customer_name, General.ConvertDateAppFormat(purchase.purchase.purchase_date), purchase.item_id, purchase.item.item_name, purchase.item.uom_setting.uom_name, purchase.qty, purchase.rate, purchase.gross_amount, purchase.discount, purchase.total_beforevat, purchase.vat_amount, purchase.net_amount, purchase.purchase.invoice_number);
                        }
                        lblNetAmount.Text = String.Format("{0} {1}", "Net Sales Amount ", listPurchase.Sum(c => c.net_amount));
                    }
                    else
                    {
                        PurchaseOrderBAL purchaseBAL = new PurchaseOrderBAL();
                        List<EDMX.purchase_order_item> listPurchase = purchaseBAL.ItemPurchaseReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, itemId);
                        foreach (EDMX.purchase_order_item purchase in listPurchase)
                        {
                            gridPurchase.Rows.Add(purchase.purchase_order.customer.customer_name, General.ConvertDateAppFormat(purchase.purchase_order.purchase_date), purchase.item_id, purchase.item.item_name, purchase.item.uom_setting.uom_name, purchase.qty, purchase.rate, purchase.gross_amount, purchase.discount, purchase.total_beforevat, purchase.vat_amount, purchase.net_amount);
                        }
                        lblNetAmount.Text = String.Format("{0} {1}", "Net Sales Amount ", listPurchase.Sum(c => c.net_amount));
                    }
                }
                lblCount.Text = ($"Count :{gridPurchase.Rows.Count - 1}");

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
               // this.BeginInvoke(new MethodInvoker(Close));
            }

        }

        private void Print()
        {
            try
            {
                int vendorId = 0;
                if (xCustomerId == 0)
                {
                    try
                    {
                        xCustomerId = General.GetComboBoxSelectedValue(cmbSupplier);
                    }
                    catch { }
                    if (!String.IsNullOrEmpty(cmbSupplier.Text) && xCustomerId == 0 )
                    {
                        CustomerBAL customerBAL = new CustomerBAL();
                        var customer = customerBAL.GetCustomerDetailsByName(cmbSupplier.Text);
                        xCustomerId = customer.Customer_Id;
                    }
                }
                vendorId = xCustomerId;

                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                    int.TryParse(listProducts.Where(x => x.item_name == cmbProductName.Text).FirstOrDefault().item_id.ToString(), out itemId);
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (!purchaseSearch && !purchaseOrderSearch)
                {
                    decimal rangeFrom = Convert.ToDecimal(txtRangeFrom.Text);
                    decimal rangeTo = Convert.ToDecimal(txtRangeTo.Text);
                    string header = $"Sales Report Itemwise . Date between {dtpDateFrom.Text} and {dtpDateTo.Text} {cmbRoute.Text}";
                    if (cmbSupplier.Text != "")
                        header = header += $" . Customer {cmbSupplier.Text} ";
                    saleBAL.PrintItemSalesReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, itemId, header, routeId,rangeFrom,rangeTo,cmbPaymentmode.Text.ToLower());
                }
                else
                {
                    if (this.purchaseSearch)
                    {
                        PurchaseBAL purchaseBAL = new PurchaseBAL();
                        string header = $"Purchase Report Itemwise . Date between {dtpDateFrom.Text} and {dtpDateTo.Text}";
                        if (cmbSupplier.Text != "")
                            header = header += $" . Supplier {cmbSupplier.Text} ";
                        purchaseBAL.PrintItemPurchaseReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, itemId, header);

                    }
                    else
                    {
                        PurchaseOrderBAL purchaseBAL = new PurchaseOrderBAL();
                        string header = $"Purchase order report item wise . Date between {dtpDateFrom.Text} and {dtpDateTo.Text}";
                        if (cmbSupplier.Text != "")
                            header = header += $" . Supplier {cmbSupplier.Text} ";
                        purchaseBAL.PrintItemPurchaseReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), vendorId, itemId, header);

                    }
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

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new SaleItemwiseReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            LoadProducts();
           // GetAllSuppliers();
            GetAllRoutes();
            if (!string.IsNullOrEmpty(xCustomerName))
            {
                cmbSupplier.Text = xCustomerName;
                dtpDateFrom.Value = DateTime.Now.AddMonths(-3);
            }
            General.BindPaymentModes(cmbPaymentmode);
            cmbPaymentmode.SelectedIndex = -1;
        }

    }
    class SaleItemwiseReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
