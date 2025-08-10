using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;
using objDAL = BETask.DAL.DAL;
using System.ComponentModel;
using System.Drawing;

namespace BETask.Views
{
    public partial class DOSalesReportForm : Form
    {
        DOSalesReportButtonCollection button;
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        BAL.SaleBAL saleBAL = new BAL.SaleBAL();
        BAL.DOSaleBAL DOSaleBAL = new BAL.DOSaleBAL();
        List<EDMX.sales> listSales = new List<EDMX.sales>();
        List<EDMX.sales> lstTempDOsales = new List<EDMX.sales>();
        List<EDMX.do_sales> listDOSale = new List<EDMX.do_sales>();
        string customerName = "", doInvoice = "";
        int customerId = 0, doId = 0;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public DOSalesReportForm()
        {
            InitializeComponent();
        }

        public void ResetForms()
        {
            try
            {
                General.ClearTextBoxes(this);
                dtpDateFrom.Value = dtpDateTo.Value = DateTime.Today;
                cmbRoute.SelectedIndex = -1;
                txtDoInvNo.Text = string.Empty;

                General.ClearGrid(dgvDoSalesItemDetails);
                General.ClearGrid(dgDOSales);
                pnlCustomerSaleDetails.Visible = false;
                btnPrint.Visible = false;
                //GetAllSuppliers();
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
                BAL.RouteBAL routeBAL = new BAL.RouteBAL();
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
            button = new DOSalesReportButtonCollection
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose,
                BtnCancel = btnCancel,
                BtnPrint = btnPrint
            };
            ResetForms();
            GetAllRoutes();
           // GetAllSuppliers();
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
        private void ButtonActive(Enum activeEvent)
        {
            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    ResetForms();

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
                    PrintSavedDOList();
                    break;
                default:
                    break;

            }
        }
        private void DOSalesReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void PrintSavedDOList()
        {
            try
            {
                General.ClearGrid(dgDOSales);
                General.ClearGrid(dgvDoSalesItemDetails);
                int CustomerId = 0;
                int routeId = 0;
                string salesDoNo = string.Empty;
                string doInvNo = string.Empty;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (cmbCustomer.Text != "")
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    CustomerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                salesDoNo = txtSalesDoNo.Text;
                doInvNo = txtDoInvNo.Text;
                listDOSale = DOSaleBAL.SearchDOSales(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), CustomerId, routeId, salesDoNo);
                if (listDOSale != null && listDOSale.Count > 0)
                {
                    DOSaleBAL.PrintSavedDOList(listDOSale);
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No DOSales found");
                }
                btnPrint.Visible = false;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllSuppliers()
        {
            try
            {
                CustomerBAL _customerBAL = new CustomerBAL();
                List<Model.CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
                foreach (Model.CustomerModel customer in _lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = customer.Customer_Name,
                        Value = customer.Customer_Id
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
        private void Search()
        {
            try
            {
                General.ClearGrid(dgDOSales);
                General.ClearGrid(dgvDoSalesItemDetails);
                int CustomerId = 0;
                int routeId = 0;
                string salesDoNo = string.Empty;
                string doInvNo = string.Empty;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (cmbCustomer.Text != "")
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    CustomerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                salesDoNo = txtSalesDoNo.Text;
                doInvNo = txtDoInvNo.Text;
                listDOSale = DOSaleBAL.SearchDOSales(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), CustomerId, routeId, doInvNo, salesDoNo);
                if (listDOSale != null && listDOSale.Count > 0)
                {
                    PopulateSavedDOSale(listDOSale);
                    btnPrint.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No DOSales found");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }


        /// <summary>
        /// calculate total data grid checked rows
        /// </summary>
        private void CalucualteGridSelectedTotal()
        {
            int[] Customer = new int[dgDOSales.Rows.Count];
            int count = 0;
            int index = 0;
            foreach (DataGridViewRow dr in dgDOSales.Rows)
            {
                if (Convert.ToInt32(dr.Cells["clmCustomerId"].Value) > 0)
                {
                    if (Convert.ToBoolean(dr.Cells["chkSelect"].Value) != false)
                    {
                        Customer[count] = Convert.ToInt32(dr.Cells["clmCustomerId"].Value);
                        count++;

                    }

                }
                else
                {
                    dgDOSales.Rows.Remove(dgDOSales.Rows[index]);
                }
                index++;
            }

            dgDOSales.Rows.Add(0, 0, "", false, "Selected Total", lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.gross_amount), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.total_discount), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.total_beforevat), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.total_vat), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.net_amount));
            dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
            dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
            dgDOSales.Rows[dgDOSales.Rows.Count - 1].ReadOnly = true;
        }
        /// <summary>
        /// load data from do_sales table
        /// </summary>
        /// <param name="lstDOsales"></param>
        private void PopulateSavedDOSale(List<EDMX.do_sales> lstDOsales)
        {
            try
            {
                General.ClearGrid(dgDOSales);
                foreach (EDMX.do_sales dos in lstDOsales)
                {
                    dgDOSales.Rows.Add(dos.do_id, dos.customer_id, General.ConvertDateTimeServerFormat(dos.do_date), dos.do_invoice_number, dos.customer.customer_name, dos.gross_amount, dos.total_discount, dos.total_beforevat, dos.total_vat, dos.net_amount, "Print", "+");

                }
                //  CalucualteGridSelectedTotal();
                dgDOSales.Columns["clmPrint"].Visible = true;
                dgDOSales.Columns["clmLinkDetails"].Visible = true;
                dgDOSales.Columns["clmDoInvNo"].Visible = true;

                dgDOSales.Rows.Add("","", "", "", "Total", lstDOsales.Sum(x => x.gross_amount), lstDOsales.Sum(x => x.total_discount), lstDOsales.Sum(x => x.total_beforevat), lstDOsales.Sum(x => x.total_vat), lstDOsales.Sum(x => x.net_amount));
                dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                dgDOSales.Rows[dgDOSales.Rows.Count - 1].ReadOnly = true;

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlCustomerSaleDetails.Visible = false;
        }
        /// <summary>
        /// load data from sales table
        /// </summary>
        /// <param name="lstsales"></param>
        private void PopulateDOSale(List<EDMX.sales> lstsales)
        {
            try
            {
                General.ClearGrid(dgDOSales);
                lstTempDOsales = lstsales;
                foreach (EDMX.sales dos in lstsales)
                {
                    dgDOSales.Rows.Add(0, dos.customer_id, "", true, dos.remarks, dos.gross_amount, dos.total_discount, dos.total_beforevat, dos.total_vat, dos.net_amount, "+");

                }
                dgDOSales.Columns["clmPrint"].Visible = false;
                dgDOSales.Columns["chkSelect"].Visible = true;
                dgDOSales.Columns["clmDoInvNo"].Visible = false;
                dgDOSales.Columns["clmLinkDetails"].Visible = true;

                //dgDOSales.Rows.Add(0, false, "Total", lstDOsales.Sum(x => x.gross_amount), lstDOsales.Sum(x => x.total_discount), lstDOsales.Sum(x => x.total_beforevat), lstDOsales.Sum(x => x.total_vat), lstDOsales.Sum(x => x.net_amount));                

                CalucualteGridSelectedTotal();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        /// <summary>
        /// load data from sales item table
        /// </summary>
        /// <param name="lstSaleItems"></param>
        private void PopulateDOSaleItems(List<EDMX.sales> lstSaleItems)
        {
            try
            {
                EDMX.item objItem = new EDMX.item();
                //General.ClearGrid(dgCustomerSales);
                int count = 0;
                foreach (EDMX.sales ps in lstSaleItems)
                {

                    dgvDoSalesItemDetails.Rows.Add(ps.sales_id,"" , "", "", $"Date :{General.ConvertDateAppFormat(ps.sales_date)} DO No :{ps.delivery_leaf}", "", "", "", "", "", "", ps.net_amount);
                    dgvDoSalesItemDetails.Rows[count].DefaultCellStyle.BackColor = Color.Gainsboro;
                    //  dgCustomerSales.Rows[count].DefaultCellStyle.ForeColor= Color.Blue;
                    foreach (EDMX.sales_item pi in ps.sales_item)
                    {
                        objItem = itemBAL.GetItemDetails(pi.item_id);
                        dgvDoSalesItemDetails.Rows.Add(ps.sales_id, General.ConvertDateAppFormat(ps.sales_date), ps.do_number, pi.item_id, pi.item.item_name, "", pi.qty, pi.rate, pi.discount, pi.gross_amount, pi.vat_amount, pi.rate > 0 ? pi.net_amount : 0);
                        count++;

                    }
                    count++;

                }

                pnlCustomerSaleDetails.Visible = true;
                pnlCustomerSaleDetails.Show();
               // dgDOSales.Visible = false;
                pnlCustomerSaleDetails.Dock = DockStyle.Fill;
                pnlCustomerSaleDetails.BringToFront();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }


        private void dgDOSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {
                    if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value) == "+")
                    {
                        int routeId = 0;
                        if (cmbRoute.Text != "")
                        {
                            Object selectedRoute = cmbRoute.SelectedItem;
                            routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                        }

                        int _CustomerId = Convert.ToInt32(dgDOSales["clmCustomerId", e.RowIndex].Value);
                        int _doId = Convert.ToInt32(dgDOSales["clmDOId", e.RowIndex].Value);
                        string _CustomerName = Convert.ToString(dgDOSales["clmCustomerName", e.RowIndex].Value);
                        this.doInvoice = Convert.ToString(dgDOSales["clmDoInvNo", e.RowIndex].Value);
                        this.customerId = _CustomerId;
                        this.customerName = _CustomerName;
                        this.doId = _doId;
                        listSales = DOSaleBAL.SearchSalesByDOId(_doId, _CustomerId, routeId);
                        lblSaleDetailsCaption.Text = $"DO Sale Deatails for {_CustomerName}";
                        PopulateDOSaleItems(listSales);                        

                    }
                    else if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "PRINT" || Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "PRINTED")
                    {
                        bool print = true;
                        if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "PRINTED")
                        {
                            print = General.ShowMessageConfirm("Already printed. do you want to print again") == DialogResult.Yes ? true : false;
                        }
                        if (print)
                        {
                            int _doId = Convert.ToInt32(dgDOSales["clmdoId", e.RowIndex].Value);
                            PrintDOInvoice(_doId);
                            dgDOSales[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Blue;
                            dgDOSales[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.White;
                        }
                        dgDOSales[e.ColumnIndex, e.RowIndex].Value = "Printed";
                        //CalucualteGridSelectedTotal();
                    }
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        /// <summary>
        /// print do invoice 
        /// </summary>
        /// <param name="doId"></param>
        private void PrintDOInvoice(int doId)
        {
            try
            {

                DOSaleBAL.PrintDOSaleInvoice(doId);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void lnkClose_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlCustomerSaleDetails.Visible = false;
            dgDOSales.Visible = true;
            General.ClearGrid(dgvDoSalesItemDetails);
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

        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PrintDeliveryItems();
        }
        private void PrintDeliveryItems()
        {
            try
            {
                if (dgvDoSalesItemDetails.Rows.Count > 0 && this.doId > 0)
                {
                    string header = $"Invoiced - Do delivery items - {this.customerName}";
                    string subhead = $"Do invoice:{this.doInvoice}";
                    DOSaleBAL.PrintDoDeliveryItems(this.doId, this.customerId, header, subhead, dtpDateFrom.Value, dtpDateTo.Value, false);
                }

            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Somethong wen wrong. Unable to print");
            }
        }
    }
    class DOSalesReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
    }
}
