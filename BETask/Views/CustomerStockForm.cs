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
    public partial class CustomerStockForm : Form
    {
        CustomerStockButtonCollection button;
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public CustomerStockForm()
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
                General.ClearGrid(gridCustomers);
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                int customerId = 0;
                if (!String.IsNullOrEmpty(cmbSupplier.Text))
                {
                    Object selectedCustomer = cmbSupplier.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                if (itemId > 0)
                {
                    List<DAL.Model.CustomerAgreementBalanceModel> listCustomerStock = new List<DAL.Model.CustomerAgreementBalanceModel> { };
                    string dt1 = DateTime.Now.ToString();
                    if (!chkDateSearch.Checked)
                        listCustomerStock = _customerBAL.CustomerStockBalance(routeId, customerId, itemId, cmbProductName.Text);
                    else
                        //Detailed checking
                        // listCustomerStock = _customerBAL.CustomerStockBalance(routeId, customerId, itemId, cmbProductName.Text, General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value));
                        listCustomerStock = _customerBAL.CustomerStockBalanceDateEnd(routeId, customerId, itemId, cmbProductName.Text,General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value));
                    string dt2 = DateTime.Now.ToString();
                    if (listCustomerStock != null)
                    {
                        foreach (DAL.Model.CustomerAgreementBalanceModel cl in listCustomerStock)
                        {
                            gridCustomers.Rows.Add(cl.customerId, cl.Route, cl.CustomerName, cl.ItemId,cl.Agreement, cl.Opening, cl.Delivered, cl.Returned, cl.Closing);
                            if (cl.Opening != cl.Closing)
                                General.GridBackcolorOrange(gridCustomers);
                            else if (cl.Agreement != cl.Opening)
                                General.GridBackcolorRed(gridCustomers);
                            else if (cl.Delivered == 0)
                                General.GridBackcolorPink(gridCustomers);
                        }
                        gridCustomers.Rows.Add("", "", "", "", listCustomerStock.Sum(x => x.Agreement), listCustomerStock.Sum(x => x.Opening), listCustomerStock.Sum(x => x.Delivered), listCustomerStock.Sum(x => x.Returned), listCustomerStock.Sum(x => x.Closing));
                        General.GridBackcolorYellow(gridCustomers);
                        General.GridRownumber(gridCustomers);
                    }
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
                string header = $"{cmbSupplier.Text}{cmbRoute.Text}{cmbProductName.Text}";
                DAL.DAL.CustomerDAL customer = new DAL.DAL.CustomerDAL();
                
                
                DataTable tblData = new DataTable();
                BETask.Report.DSReports.CustomerStockDataTable detailedDataTable = new Report.DSReports.CustomerStockDataTable();
                tblData = detailedDataTable.Clone();

                for (int i = 0; i < gridCustomers.Rows.Count - 1; i++)
                {
                    DataRow rowItem = tblData.NewRow();
                    rowItem["Customer"] = gridCustomers["clmCustomer", i].Value;
                    rowItem["Agreement"] = gridCustomers["clmAgreement", i].Value;
                    rowItem["Opening"] = gridCustomers["clmOpening", i].Value;
                    rowItem["Delivered"] = gridCustomers["clmDelivered", i].Value;
                    rowItem["Returned"] = gridCustomers["clmReturned", i].Value;
                    rowItem["Closing"] = gridCustomers["clmClosing", i].Value;
                    tblData.Rows.Add(rowItem);
                }

                BETask.Report.ReportForm reportForm = new BETask.Report.ReportForm(BETask.Report.ReportForm.EnumReportType.CustomerStock, header,tblData);
                reportForm.Show();

            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load report");
            }
        }
        private void SearchDetailed(int customerId,int itemId)
        {
            try
            {
                General.ClearGrid(gridDetailed);
                List<DAL.Model.CustomerAgreementBalanceModel> listCustomerStock = new List<DAL.Model.CustomerAgreementBalanceModel> { };
               
                    listCustomerStock = _customerBAL.CustomerStockBalanceDetailed( customerId, itemId, cmbProductName.Text, General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value));
                if (listCustomerStock != null)
                {
                    foreach (DAL.Model.CustomerAgreementBalanceModel cl in listCustomerStock)
                    {
                        gridDetailed.Rows.Add(cl.Date,cl.Agreement, cl.Opening, cl.Delivered, cl.Returned, cl.Closing);
                    }
                    gridDetailed.Rows.Add("", "","",  listCustomerStock.Sum(x => x.Delivered), listCustomerStock.Sum(x => x.Returned));
                    General.GridBackcolorYellow(gridDetailed);
                    General.GridRownumber(gridDetailed);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllCustomers()
        {
            try
            {
               
              
                List<CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
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
                List<EDMX.item> listProducts  = itemBAL.GetAllItem_Sellable();
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
            button = new CustomerStockButtonCollection
            {
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,
                BtnClose = btnClose
            };
            GetAllRoutes();
            GetAllCustomers();
            LoadProducts();
            DAL.DAL.CompanyDAL companyDAL = new DAL.DAL.CompanyDAL();
            DateTime minDate = companyDAL.GetSoftwareStartDate();
            dtpDateFrom.Value = minDate;
        }

        private void CustomerStockForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void chkDateSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateSearch.Checked)
                grpDatesearch.Enabled = true;
            else
                grpDatesearch.Enabled = false;
        }

        private void gridDetailed_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0&& e.ColumnIndex>=0)
            {
                int itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                int customerId = Convert.ToInt32(gridCustomers["clmCustomerId", e.RowIndex].Value);
                if (customerId > 0)
                {
                    SearchDetailed(customerId, itemId);
                    pnlDetailed.Show();
                }
            }
        }

        private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlDetailed.Hide();
        }

       
    }
    class CustomerStockButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
    }
}
