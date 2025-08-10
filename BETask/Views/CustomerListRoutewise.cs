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
    public partial class CustomerListRoutewise : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print,
            UpdateSave,
            UpdateClose
        }
        public bool isSupplier=false;
        BAL.AccountTransactionBAL accountLedgerBAL = new AccountTransactionBAL();
        CustomerListButtonCollection button ;
        public CustomerListRoutewise()
        {
            InitializeComponent();
           // GetAllCustomers(1);
        }
    
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    //Search();
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
            else if (sender == button.BtnUpdateSave)
            {
                ButtonActive(EnumFormEvents.UpdateSave);
            }
            else if (sender == button.BtnUpdateClose)
            {
                ButtonActive(EnumFormEvents.UpdateClose);
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
       
        private void Search()
        {
            try
            {
                gridAccounts.DataSource = null;
                SearchSummary();
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
                int routeId = 0;
                string paymentMode = string.Empty;
                lblTotalCount.Text = "";
                    routeId = General.GetComboBoxSelectedValue(cmbRoute); ;
                    paymentMode = cmbPaymentMode.Text;
 
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                CustomerBAL customerBAL = new CustomerBAL();

                DataTable tblCustomerReport = customerBAL.GetCustomerListRouteWise(routeId, employeeId, chkOnlyActive.Checked, chkDateCheck.Checked, General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value),paymentMode);

                gridAccounts.DataSource = tblCustomerReport;
                lblTotalCount.Text = tblCustomerReport.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
     
        private void Print()
        {
            try
            {
                int ledgerId = 0;
                string paymentMode = "";
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                if (cmbPaymentMode.Text != "")
                {
                   
                    paymentMode = cmbPaymentMode.Text;
                }
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                string route = cmbRoute.Text;
                if (employeeId > 0)
                    route += cmbEmployee.Text.PadLeft(5, ' ');
                

                CustomerBAL customerBAL = new CustomerBAL();
                customerBAL.PrintGetCustomerListRouteWise(routeId, employeeId, chkOnlyActive.Checked, chkDateCheck.Checked, General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), paymentMode,cmbRoute.Text);
                

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
              
            }
        }
       /// <summary>
       /// prakash tmr added account leder id updation
       /// </summary>
       
     

        private void gridPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
               // submitItem();
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new CustomerListButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
            };
            GetAllRoutes();
            GetAllEmployees();
            //LoadAllLedger();
            chkOnlyActive.Checked = true;
            pnlDatePanel.Visible = false;
            chkOnlyActive.Checked = false;
            General.BindPaymentModes(cmbPaymentMode);
            Application.DoEvents();
            cmbPaymentMode.SelectedIndex = -1;
            // Search();
        }

        private void gridAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    gridAccounts.Columns[e.ColumnIndex].Visible = false;
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    // General.ShowMessage(General.EnumMessageTypes.Error);

                }
            }
        }

        private void lnkShowHidden_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i <= gridAccounts.Columns.Count - 1; i++)
            {
                gridAccounts.Columns[i].Visible = true;
            }
        }

        private void chkDateCheck_CheckedChanged(object sender, EventArgs e)
        {
            pnlDatePanel.Visible = chkDateCheck.Checked;
        }

       
      
        private void TransferOldSales(string customerName)
        {

        }

       
        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = String.Format("{0} {1}", emp.first_name, emp.last_name),
                        Value = emp.employee_id
                    };
                    cmbEmployee.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
    }

    class CustomerListButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnUpdateSave { get; set; }
        public Button BtnUpdateClose { get; set; }

    }
}
