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
    public partial class CustomerStatementDetailedForm : Form
    {
        CustomerStatementDetailedButtonCollection button;
        CustomerBAL customerBAL = new CustomerBAL();
        int customerId = 0;
        string customerName;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public CustomerStatementDetailedForm()
        {
            InitializeComponent();
            this.customerId = 0;
        }
        public CustomerStatementDetailedForm(string cusomerName, int customerId)
        {
            InitializeComponent();
            cmbCustomer.Text = cusomerName;
            this.customerId = customerId;
            this.customerName = customerName;
            dtpDateFrom.Value = DateTime.Today.AddMonths(-1);
            Search();
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
                General.ClearGrid(gridAccounts);
                int _customerId = 0;
                if (this.customerId == 0)
                {
                    if (!String.IsNullOrEmpty(cmbCustomer.Text))
                    {
                        Object selectedCustomer = cmbCustomer.SelectedItem;
                        _customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                    }
                    this.customerId = _customerId;
                }
                if (customerId > 0)
                {
                    List<DAL.Model.CustomerStatementDetailedModel> listCustomer = customerBAL.CustomerStatementDetailed(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), customerId);
                    listCustomer = listCustomer.OrderBy(x => x.Date).ToList();
                    if (listCustomer != null && listCustomer.Count > 0)
                    {
                        decimal balance = 0;
                        foreach (DAL.Model.CustomerStatementDetailedModel cs in listCustomer)
                        {
                            balance += (cs.Debit - cs.Credit);
                            gridAccounts.Rows.Add(General.ConvertDateAppFormat(cs.Date), cs.TransactionType, cs.Description, cs.Qty, cs.Debit, cs.Credit, General.TruncateDecimalPlaces(balance, 3));

                        }
                        gridAccounts.Rows.Add("", "", "Total", listCustomer.Sum(x => x.Qty), listCustomer.Sum(x => x.Debit), listCustomer.Sum(x => x.Credit), "");

                        General.GridBackcolorYellow(gridAccounts);

                    }
                }
                //   this.customerId = 0;
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
                if (gridAccounts.Rows.Count > 0)
                {

                    if (customerId > 0)
                    {
                        Model.CustomerModel customer = customerBAL.GetCustomerDetail(customerId);
                        string trn = customer.Trn;
                        DataTable tblDReport = new DataTable();
                        BETask.Report.DSReports.CustomerStatementAdvancedDataTable customerStatementAdvancedDataTable = new Report.DSReports.CustomerStatementAdvancedDataTable();
                        tblDReport = customerStatementAdvancedDataTable.Clone();
                        foreach (DataGridViewRow drg in gridAccounts.Rows)
                        {
                            if (drg.Index < gridAccounts.Rows.Count - 1)
                            {
                                DataRow dr = tblDReport.NewRow();
                                dr["CustomerName"] = cmbCustomer.Text;
                                dr["TRN"] = trn;
                                dr["TranDate"] = General.ConvertDateAppFormat(Convert.ToDateTime(drg.Cells["clmDate"].Value));
                                dr["Description"] = drg.Cells["clmDescription"].Value;
                                dr["Qty"] = drg.Cells["clmQty"].Value;
                                dr["Debit"] = drg.Cells["clmDebit"].Value;
                                dr["Credit"] = drg.Cells["clmCredit"].Value;
                                dr["Balance"] = drg.Cells["clmBalance"].Value;
                                tblDReport.Rows.Add(dr);
                            }
                        }
                        customerBAL.PrintCustomerStatementAdvanced(tblDReport);
                    }
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

                if (this.customerId == 0)
                {
                    List<CustomerModel> _lstCustomers = customerBAL.GetAllCustomers(0, string.Empty, 1);
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

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void FormLoad()
        {
            button = new CustomerStatementDetailedButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            // Search();
        }

        private void CustomerStatementDetailedForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbCustomer.Text))
            {
                Object selectedCustomer = cmbCustomer.SelectedItem;
                this.customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
            }

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
    class CustomerStatementDetailedButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
