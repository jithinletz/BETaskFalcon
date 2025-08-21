using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;
using System.Drawing;

namespace BETask.Views
{
    public partial class CustomerStatementForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public bool isSupplier = false, isSalesman = false;
        SaleBAL saleBAL = new SaleBAL();
        string customerName = "";
        BAL.AccountTransactionBAL accountLedgerBAL = new AccountTransactionBAL();



        CustomerStatementButtonCollection button;


        public CustomerStatementForm()
        {
            InitializeComponent();
            this.Text = "Customer Statement";
            rdbCustomer.Checked = true;
            rdbSupplier.Enabled = false;
        }
        public CustomerStatementForm(bool isSupplier)
        {
            InitializeComponent();
            this.Text = "Supplier Statement";
            rdbSupplier.Checked = true;
            rdbCustomer.Enabled = false;
            lblRoute.Visible = false;
            this.isSupplier = isSupplier;
            cmbRoute.Visible = false;
            GetAllCustomers(2);
        }
        public CustomerStatementForm(string cusomerName, int ledgerId, bool supplier = false)
        {
            InitializeComponent();
            this.Text = "Customer Statement";
            rdbCustomer.Checked = true;
            rdbSupplier.Enabled = false;
            if (!supplier && ledgerId <= 0)
                GetAllCustomers(1);
            else if (!supplier && ledgerId > 0)
                GetCustomerDetailByLedger(ledgerId);
            else
                GetAllCustomers(2);
            cmbLedgerAccount.Text = cusomerName;
            rdbDetaild.Checked = true;
            dtpDateFrom.Value = DateTime.Now.AddDays(-30);
            //  Search();

        }
        public CustomerStatementForm(string ledgerName, string ledgerType)
        {
            InitializeComponent();
            this.Text = ledgerType;
            rdbSupplier.Checked = false;
            rdbCustomer.Enabled = true;
            lblRoute.Hide();
            cmbRoute.Hide();
            rdbSupplier.Hide();
            lblSalemanAccount.Show();
            cmbSalesmanAccount.Show();
            GetAllSalesmanCreditLedgers();
            cmbSalesmanAccount.Text = ledgerName;
            GetAllCustomerBySalesmanCreditLedger();
            dtpDateFrom.Value = DateTime.Now.AddDays(-30);
            rdbDetaild.Checked = true;
            isSalesman = true;
            chkHideOpening.Show();
            //rdbSummary.Hide();
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
                    cmbLedgerAccount.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                    if (rdbSummary.Checked)
                        Print();
                    else
                        PrintDetailed();
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

        private void GetCustomerDetailByLedger(int ledgerId)
        {
            try
            {
                CustomerBAL customerBAL = new CustomerBAL();
                var customer = customerBAL.GetCustomerDetailsByLedger(ledgerId);
                cmbLedgerAccount.Items.Clear();
                ComboboxItem _cmbItem = new ComboboxItem()
                {
                    Text = customer.customer_name,
                    Value = customer.ledger_id
                };
                cmbLedgerAccount.Items.Add(_cmbItem);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllCustomers(int custType)
        {
            try
            {

                CustomerBAL _customerBAL = new CustomerBAL();
                List<CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, "", custType);
                foreach (CustomerModel ledger in _lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.Customer_Name,
                        Value = ledger.LedgerId
                    };
                    cmbLedgerAccount.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void GetAllSalesmanCreditLedgers()
        {
            try
            {

                AccountLedgerBAL accountLedger = new AccountLedgerBAL();
                List<EDMX.account_ledger> _lsLedgers = accountLedger.GetAllSalesmanCreditLedger();
                foreach (EDMX.account_ledger ledger in _lsLedgers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.ledger_name,
                        Value = ledger.ledger_id
                    };
                    cmbSalesmanAccount.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllCustomerBySalesmanCreditLedger()
        {
            try
            {
                int ledgerId = General.GetComboBoxSelectedValue(cmbSalesmanAccount);
                if (ledgerId > 0)
                {
                    DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                    List<EDMX.customer> listCustomer = customerDAL.GetAllcustomerBySalesmanLedger(ledgerId);
                    if (listCustomer != null)
                    {
                        foreach (EDMX.customer ledger in listCustomer)
                        {
                            ComboboxItem _cmbItem = new ComboboxItem()
                            {
                                Text = ledger.customer_name,
                                Value = ledger.ledger_id
                            };
                            cmbLedgerAccount.Items.Add(_cmbItem);
                        }
                    }
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
        private void GenerateGridFormat()
        {
            try
            {
                if (rdbCustomer.Checked)
                {
                    if (rdbSummary.Checked)
                    {
                        gridAccounts.Columns.Add("clmCustomerName", "Customer");
                        gridAccounts.Columns[0].Width = 200;
                        gridAccounts.Columns.Add("clmDebit", "Debit");
                        gridAccounts.Columns[1].Width = 150;
                        gridAccounts.Columns.Add("clmCredit", "Credit");
                        gridAccounts.Columns[2].Width = 150;
                        gridAccounts.Columns.Add("clmOutstanding", "Outstanding");
                        gridAccounts.Columns[3].Width = 150;
                        gridAccounts.Columns.Add("clmAdvance", "Advance");
                        gridAccounts.Columns[4].Width = 150;
                    }
                    else if (rdbDetaild.Checked)
                    {
                        gridAccounts.Columns.Add("clmDate", "Date");
                        gridAccounts.Columns[0].Width = 150;
                        gridAccounts.Columns.Add("clmDebit", "Debit");
                        gridAccounts.Columns[1].Width = 120;
                        gridAccounts.Columns.Add("clmCredit", "Credit");
                        gridAccounts.Columns[2].Width = 120;
                        gridAccounts.Columns.Add("clmOutstanding", "Outstanding");
                        gridAccounts.Columns[3].Width = 130;
                        gridAccounts.Columns.Add("clmAdvance", "Advance");
                        gridAccounts.Columns[4].Width = 130;
                        gridAccounts.Columns.Add("clmInvoice", "Invoice");
                        gridAccounts.Columns[5].Width = 250;
                        gridAccounts.Columns.Add("clmNarration", "Narration");
                        gridAccounts.Columns[6].Width = 250;
                    }
                }
                if (rdbSupplier.Checked)
                {
                    if (rdbSummary.Checked)
                    {
                        gridAccounts.Columns.Add("clmSupplierName", "Supplier");
                        gridAccounts.Columns[0].Width = 200;
                        gridAccounts.Columns.Add("clmDebit", "Debit");
                        gridAccounts.Columns[1].Width = 150;
                        gridAccounts.Columns.Add("clmCredit", "Credit");
                        gridAccounts.Columns[2].Width = 150;
                        gridAccounts.Columns.Add("clmOutstanding", "Outstanding");
                        gridAccounts.Columns[3].Width = 150;
                        gridAccounts.Columns.Add("clmAdvance", "Advance");
                        gridAccounts.Columns[4].Width = 150;
                    }
                    else if (rdbDetaild.Checked)
                    {
                        gridAccounts.Columns.Add("clmDate", "Date");
                        gridAccounts.Columns[0].Width = 150;
                        gridAccounts.Columns.Add("clmDebit", "Debit");
                        gridAccounts.Columns[1].Width = 120;
                        gridAccounts.Columns.Add("clmCredit", "Credit");
                        gridAccounts.Columns[2].Width = 120;
                        gridAccounts.Columns.Add("clmOutstanding", "Outstanding");
                        gridAccounts.Columns[3].Width = 120;
                        gridAccounts.Columns.Add("clmAdvance", "Advance");
                        gridAccounts.Columns[4].Width = 120;
                        gridAccounts.Columns.Add("clmNarration", "Narration");
                        gridAccounts.Columns[5].Width = 200;
                    }
                }
            }
            catch (Exception ee)
            { }
        }
        private void Search()
        {
            try
            {
                lblPayment.Text = "";
                lblReciept.Text = "";
                gridAccounts.Columns.Clear();
                GenerateGridFormat();
                General.ClearGrid(gridAccounts);

                if (rdbSummary.Checked)
                    SearchSummary();

                else
                    SearchDetailed();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }

        }
        private void SearchSummary()
        {
            try
            {
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId = General.GetComboBoxSelectedValue(cmbLedgerAccount);
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);

                int customerType = rdbSupplier.Checked ? 2 : 1;

                List<DAL.Model.DaybookModel> listAccountTransaction = null;
                if (!isSalesman)
                    listAccountTransaction = accountTransactionBAL.CustomerStatementSummary(General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, customerType, routeId);
                else
                {
                    int salesmanLedger = General.GetComboBoxSelectedValue(cmbSalesmanAccount);
                    if (!chkHideOpening.Checked)
                        listAccountTransaction = accountTransactionBAL.CustomerStatementSummaryBySalesman(General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, salesmanLedger, customerType, routeId);
                    else
                        listAccountTransaction = accountTransactionBAL.CustomerStatementSummaryBySalesman(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, salesmanLedger, customerType, routeId);

                }
                decimal totalOutstanding = 0, totalAdvance = 0;
                foreach (DAL.Model.DaybookModel tran in listAccountTransaction)
                {

                    decimal outstanding = 0, advance = 0;
                    if (rdbCustomer.Checked)
                    {
                        if ((tran.Credit - tran.Debit) < 0)
                        {
                            outstanding = tran.Debit - tran.Credit;
                            totalOutstanding += outstanding;
                        }
                        else
                        {
                            advance = tran.Credit - tran.Debit;
                            totalAdvance += advance;
                        }
                    }
                    else
                    {
                        if ((tran.Credit - tran.Debit) < 0)
                        {
                            advance = tran.Debit - tran.Credit;
                            totalAdvance += advance;
                        }
                        else
                        {
                            outstanding = tran.Credit - tran.Debit;
                            totalOutstanding += outstanding;
                        }
                    }
                    gridAccounts.Rows.Add(tran.TransactionType, tran.Debit, tran.Credit, outstanding, advance);

                }
                gridAccounts.Rows.Add("Total", "", "", totalOutstanding, totalAdvance);
                gridAccounts.Rows[gridAccounts.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                gridAccounts.Rows[gridAccounts.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                gridAccounts.Rows[gridAccounts.Rows.Count - 1].DefaultCellStyle.Font.Bold.Equals(true);
                lblPayment.Text = String.Format("{0} {1}", "Total Debit", General.TruncateDecimalPlaces(listAccountTransaction.Sum(x => x.Debit)));
                lblReciept.Text = String.Format("{0} {1}", "Total Credit", General.TruncateDecimalPlaces(listAccountTransaction.Sum(x => x.Credit)));
            }
            catch
            {
                throw;
            }
        }
        private void SearchDetailed()
        {
            try
            {
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int ledgerId = General.GetComboBoxSelectedValue(cmbLedgerAccount);
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);

                int customerType = rdbSupplier.Checked ? 2 : 1;
                decimal openingDebit = 0, openingCredit = 0;

                List<EDMX.account_transaction> listAccountTransaction = null;
                if (!isSalesman)
                    listAccountTransaction = accountTransactionBAL.CustomerStatementDetailed(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, out openingDebit, out openingCredit, routeId);
                else
                {
                    int salesmanledger = General.GetComboBoxSelectedValue(cmbSalesmanAccount);
                    listAccountTransaction = accountTransactionBAL.CustomerStatementDetailedBySalesman(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, salesmanledger, out openingDebit, out openingCredit);
                }
                if (chkOrderByDate.Checked)
                    listAccountTransaction = listAccountTransaction.OrderBy(x => x.transaction_date).ThenBy(x => x.transaction_type).ToList();
                // decimal totalOutstanding = 0, totalAdvance = 0;
                decimal opAdvance = 0, opOutstanding = 0;
                if (rdbCustomer.Checked)
                {
                    if (openingCredit - openingDebit < 0)
                    {
                        opOutstanding = openingDebit - openingCredit;
                    }
                    else
                    {
                        opAdvance = openingCredit - openingDebit;

                    }
                }
                else
                {
                    if ((openingCredit - openingDebit) < 0)
                    {
                        opAdvance = openingDebit - openingCredit;

                    }
                    else
                    {
                        opOutstanding = openingCredit - openingDebit;

                    }
                }
                if (openingDebit != 0 || openingCredit != 0 && opOutstanding != 0 && opAdvance != 0)
                    gridAccounts.Rows.Add("Opening", openingDebit, openingCredit, opOutstanding, opAdvance, "Opening Balance");
                decimal outstanding = opOutstanding, advance = opAdvance;
                foreach (EDMX.account_transaction tran in listAccountTransaction)
                {


                    if (rdbCustomer.Checked)
                    {
                        if ((tran.credit - tran.debit) < 0)
                        {
                            if (advance <= 0)
                            {
                                outstanding += tran.debit - tran.credit;
                            }
                            else
                            {
                                if (advance > 0)
                                    advance -= (tran.debit - tran.credit);
                                if (advance < 0)
                                {
                                    outstanding += advance * -1;
                                    advance = 0;
                                }

                            }

                        }
                        else
                        {
                            bool _outEmptyTran = false;
                            if (outstanding > 0)
                            {
                                _outEmptyTran = true;
                                outstanding -= (tran.credit - tran.debit);
                                //  totalOutstanding = outstanding;
                            }
                            if (outstanding <= 0)
                            {
                                if (outstanding == 0 && !_outEmptyTran)
                                {
                                    advance += tran.credit - tran.debit;
                                    //advance = advance;
                                }
                                //advance += tran.credit - tran.debit;
                                else
                                {
                                    advance += outstanding * -1;
                                    outstanding = 0;
                                }
                                //  totalAdvance += advance;
                            }
                        }
                    }
                    else
                    {
                        if ((tran.credit - tran.debit) < 0)
                        {
                            if (outstanding > 0)
                            {
                                outstanding -= (tran.debit - tran.credit);
                                //  totalOutstanding = outstanding;
                            }
                            if (outstanding <= 0)
                            {
                                if (outstanding == 0)
                                    advance += tran.credit - tran.debit;
                                else
                                {
                                    advance += outstanding * -1;
                                    outstanding = 0;
                                }
                                //  totalAdvance += advance;
                            }
                        }
                        else
                        {
                            if (advance <= 0)
                            {
                                outstanding += tran.credit - tran.debit;
                            }
                            else
                            {
                                if (advance > 0)
                                    advance -= (tran.credit - tran.debit);
                                if (advance < 0)
                                {
                                    outstanding += advance * -1;
                                    advance = 0;
                                }

                            }
                        }
                    }
                    //  gridAccounts.Rows.Add(General.ConvertDateAppFormat(tran.transaction_date), tran.debit, tran.credit, (advance<=0)?advance*-1:0, (advance>=0)?advance:0,tran.narration);
                    if(rdbCustomer.Checked)
                        gridAccounts.Rows.Add(General.ConvertDateAppFormat(tran.transaction_date), tran.debit, tran.credit, outstanding, advance,tran.voucher_number, tran.narration);
                    else
                    gridAccounts.Rows.Add(General.ConvertDateAppFormat(tran.transaction_date), tran.debit, tran.credit, outstanding, advance, tran.narration);

                }
                //  gridAccounts.Rows.Add("Total", "", "", totalOutstanding+opOutstanding, totalAdvance+opAdvance);
                //gridAccounts.Rows[gridAccounts.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                //gridAccounts.Rows[gridAccounts.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                //gridAccounts.Rows[gridAccounts.Rows.Count - 1].DefaultCellStyle.Font.Bold.Equals(true);
                lblPayment.Text = String.Format("{0} {1}", "Total Debit", General.TruncateDecimalPlaces(listAccountTransaction.Sum(x => x.debit) + openingDebit));
                lblReciept.Text = String.Format("{0} {1}", "Total Credit", General.TruncateDecimalPlaces(listAccountTransaction.Sum(x => x.credit) + openingCredit));
            }
            catch(Exception e)
            {
                throw;
            }
        }

        private void Print()
        {
            try
            {
                int ledgerId = 0;
                if (cmbLedgerAccount.Text != "")
                {
                    Object selectedLedger = cmbLedgerAccount.SelectedItem;
                    ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                }
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                int customerType = rdbCustomer.Checked ? 1 : 2;
                string custType = customerType == 1 ? "Customer" : "Supplier";
                string route = cmbRoute.Text != "" ? cmbRoute.Text : "";
                string header = $"{custType} Statement for the date {General.ConvertDateAppFormat(dtpDateTo.Value)} , Route : {route} ";
                if (!this.isSalesman)
                    accountTransactionBAL.PrintCustomerAccountStatement(General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, customerType, routeId, header);
                else
                {
                    ledgerId = General.GetComboBoxSelectedValue(cmbSalesmanAccount);
                    accountTransactionBAL.PrintCustomerAccountStatementSalesman(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, customerType, routeId, header,chkHideOpening.Checked);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }
        }
        private void PrintDetailed()
        {
            try
            {
                if (gridAccounts.Rows.Count > 0)
                {
                    int ledgerId = 0;
                    if (cmbLedgerAccount.Text != "")
                    {
                        Object selectedLedger = cmbLedgerAccount.SelectedItem;
                        ledgerId = (int)((BETask.Views.ComboboxItem)selectedLedger).Value;
                    }
                    int routeId = 0;

                    AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                    int customerType = rdbCustomer.Checked ? 1 : 2;
                    string custType = customerType == 1 ? "Customer" : "Supplier";

                    string header = $"{custType} {cmbLedgerAccount.Text} Statement for the date {General.ConvertDateAppFormat(dtpDateTo.Value)}  ";
                    if (isSalesman)
                        header = $"{cmbSalesmanAccount.Text} - {cmbLedgerAccount.Text} Statement for the date {General.ConvertDateAppFormat(dtpDateTo.Value)}  ";
                    //accountTransactionBAL.PrintCustomerAccountStatement(General.ConvertDateServerFormat(dtpDateTo.Value), ledgerId, routeId, customerType, header);
                    DataTable tblDetailed = new DataTable();
                    BETask.Report.DSReports.CustomerStatementDetailedDataTable customerStatementDetailedDataTable = new Report.DSReports.CustomerStatementDetailedDataTable();
                    tblDetailed = customerStatementDetailedDataTable.Clone();
                    foreach (DataGridViewRow dr in gridAccounts.Rows)
                    {
                        DataRow dataRow = tblDetailed.NewRow();
                        dataRow["TranDate"] = dr.Cells["clmDate"].Value.ToString();
                        dataRow["Debit"] = dr.Cells["clmDebit"].Value.ToString();
                        dataRow["Credit"] = dr.Cells["clmCredit"].Value.ToString();
                        dataRow["Outstanding"] = dr.Cells["clmOutstanding"].Value.ToString();
                        dataRow["Advance"] = dr.Cells["clmAdvance"].Value.ToString();
                        dataRow["Narration"] = dr.Cells["clmNarration"].Value != null ? dr.Cells["clmNarration"].Value.ToString() : string.Empty;
                        tblDetailed.Rows.Add(dataRow);
                    }
                    accountLedgerBAL.PrintCustomerAccountStatementDetailed(tblDetailed, header);
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
                    int.TryParse(gridAccounts[0, e.RowIndex].Value.ToString(), out _saleId);
                    //saleId = _saleId;

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

       
        private void cmbLedgerAccount_KeyDown(object sender, KeyEventArgs e)
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

            cmbLedgerAccount.Items.Clear();
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
                        Value = customer.LedgerId
                    };
                    cmbLedgerAccount.Items.Add(_cmbItem);
                    cmbLedgerAccount.SelectedIndex = 0;
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
            button = new CustomerStatementButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            // Search();
            GetAllRoutes();

        }


    }
    class CustomerStatementButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
