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
    public partial class DeliveryItemReportForm : Form
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
        BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<CustomerModel> _lstCustomers = null;
        List<EDMX.item> listProducts = new List<EDMX.item>();
        DataTable tblReport = new DataTable();

        DeliveryItemReportButtonCollection button;


        public DeliveryItemReportForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                   // Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbProductName.Text = string.Empty;
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

                General.ClearGrid(gridDelivery);
                Application.DoEvents();
                int customerId = 0, itemId = 0;
                decimal rangeFrom = Convert.ToDecimal(txtRangeFrom.Text);
                decimal rangeTo = Convert.ToDecimal(txtRangeTo.Text);

                if (!String.IsNullOrEmpty(cmbSupplier.Text))
                {
                    Object selectedCustomer = cmbSupplier.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }

                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                bool deliveredOnly = chkDeliveredOnly.Checked;
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                tblReport= deliveryBAL.ItemDeliveryReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), customerId, itemId, deliveredOnly, routeId, rangeFrom, rangeTo, cmbPaymentmode.Text.ToLower());

                gridDelivery.SuspendLayout();
                List<DataGridViewRow> rows = new List<DataGridViewRow>();

                // foreach (EDMX.account_transaction tran in listAccountTransaction)
                foreach (DataRow dr in tblReport.Rows)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(gridDelivery,dr["delivery_id"],dr["customer_name"],dr["item_name"],dr["uom_name"],dr["qty"],dr["delivered_qty"],dr["rate"], dr["gross"], dr["vat"], dr["net"], dr["delivery_time"],dr["payment_mode"],dr["employee"]);
                    // row.CreateCells(gridAccounts, General.ConvertDateAppFormat(tran.transaction_date), tran.account_ledger.ledger_name, tran.debit, tran.credit, tran.transaction_type, $" {tran.narration}", tran.transaction_type_id, $"{tran.transaction_number}");
                    rows.Add(row);
                }
               


                gridDelivery.Rows.AddRange(rows.ToArray()); // Add all rows at once

                gridDelivery.ResumeLayout(); // Resume layout updates
                gridDelivery.RowTemplate.Height = 60;
                lblInfo.Text = "";
                if (gridDelivery.Rows.Count > 0)
                {
                    object qty = tblReport.Compute("sum(delivered_qty)","");
                    object gross = tblReport.Compute("sum(gross)", "");
                    object vat = tblReport.Compute("sum(vat)", "");
                    object net = tblReport.Compute("sum(net)", "");
                    lblInfo.Text = $"Records : {gridDelivery.Rows.Count} ,   Qty : {qty}  , ";

                    DataGridViewRow rowFooter = new DataGridViewRow();

                    rowFooter.CreateCells(gridDelivery, "", "", "", "Total",qty, qty,"", gross, vat, net, "", "", "");
                    gridDelivery.Rows.Add(rowFooter); // Add all rows at once
                    General.GridBackcolorYellow(gridDelivery);

                }

                // List<EDMX.delivery_items> listdelivery = deliveryBAL.ItemDeliveryReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), customerId, itemId, deliveredOnly, routeId);

                //foreach (EDMX.delivery_items item in listdelivery)
                //{
                //    string employee = String.Format("{0} {1}", item.delivery.employee.first_name, item.delivery.employee.last_name);
                //    gridDelivery.Rows.Add(item.delivery_id, item.customer.customer_name, item.item.item_name, item.item.uom_setting.uom_name, item.qty, item.delivered_qty, item.rate, item.delivery_time, employee);
                //}
                //if (listdelivery != null && listdelivery.Count > 0)
                //{
                //    gridDelivery.Rows.Add("", "", "", "", listdelivery.Sum(x => x.qty), listdelivery.Sum(x => x.delivered_qty), "", "", "");
                //    gridDelivery.Rows[gridDelivery.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                //}

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                //this.BeginInvoke(new MethodInvoker(Close));
            }

        }

        private void Print()
        {
            try
            {
                if (tblReport.Rows.Count > 0)
                {
                    int customerId = 0, itemId = 0;

                    if (!String.IsNullOrEmpty(cmbSupplier.Text))
                    {
                        Object selectedCustomer = cmbSupplier.SelectedItem;
                        customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                    }

                    if (!String.IsNullOrEmpty(cmbProductName.Text))
                    {
                        Object selectedProduct = cmbProductName.SelectedItem;
                        itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                    }
                    bool deliveredOnly = chkDeliveredOnly.Checked;
                    int routeId = 0;
                    if (!String.IsNullOrEmpty(cmbRoute.Text))
                    {
                        Object selectedRoute = cmbRoute.SelectedItem;
                        routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                    }
                    deliveryBAL.PrintItemDeliveryReport(cmbRoute.Text, tblReport);

                    // saleBAL.PrintCustomerSalesReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId, paymentMode);
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
                    int.TryParse(gridDelivery[0, e.RowIndex].Value.ToString(), out _saleId);
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
            button = new DeliveryItemReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            //Search();
            // GetAllSuppliers();
            LoadProducts();
            GetAllRoutes();
            General.BindPaymentModes(cmbPaymentmode);
            cmbPaymentmode.SelectedIndex = -1;
            General.SetScreenSize(sender, e, this);
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

      
    }

    class DeliveryItemReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
