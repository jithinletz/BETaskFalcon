using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Drawing;

namespace BETask.Views
{
    public partial class CustomerSearchForm : Form
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
             
        public enum EnumFormEvents
        {
            FormLoad,
            Search,
            Close

        }
        CustomerSearchButtonCollection button;
        public CustomerSearchForm()
        {
            InitializeComponent();
        }        

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {

            if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);

            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
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

        private void Search()
        {
            try
            {
                General.ClearGrid(gridCustomer);
                CustomerBAL customerBAL = new CustomerBAL();
                int routeId = 0;
                int employeeId = 0;
                string phone = txtPhone.Text;
                int id = !string.IsNullOrEmpty(txtID.Text)?Convert.ToInt32(txtID.Text):0;
                string address = txtAddress.Text.ToLower();
                string name = txtName.Text.ToLower();
                int active = chkActive.Checked ? 1 : 2;
              
                    routeId = General.GetComboBoxSelectedValue(cmbRoute);
                    employeeId = General.GetComboBoxSelectedValue(cmbEmployee);


                List<EDMX.customer> listCustomerSearch = customerBAL.CustomerSearch(name,routeId, employeeId,phone,id,address, active);
                if (listCustomerSearch != null && listCustomerSearch.Count > 0)
                {
                    foreach (EDMX.customer customer in listCustomerSearch)
                    {
                        gridCustomer.Rows.Add(customer.customer_id, customer.customer_name, customer.address1, customer.phone, customer.route.route_name);
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
            button = new CustomerSearchButtonCollection
            {
                BtnClose = btnClose,
                BtnSearch = btnSearch

            };
            GetAllRoutes();
            GetAllEmployees();
            txtName.Focus();
         

        }

        private void SelectCustomer(int row)
        {
            try
            {
                int _customerId = 0;
                int.TryParse(gridCustomer[0, row].Value.ToString(), out _customerId);
                CustomerId = _customerId;
                CustomerName = gridCustomer[1, row].Value.ToString();
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
            catch (Exception ee)
            {
                General.Error(ee.ToString());
               // General.ShowMessage(General.EnumMessageTypes.Error);
                //this.BeginInvoke(new MethodInvoker(Close));
            }
        }
        class CustomerSearchButtonCollection
        {
            public Button BtnClose { get; set; }
            public Button BtnSearch { get; set; }

        }

        private void CustomerSearchForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            //Search();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if(txtName.Text.Length>=5)
            {
                Search();
            }
        }

        private void CustomerSearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
                gridCustomer.Focus();
            else if (e.KeyData == Keys.Enter)
            {
                if (gridCustomer.Rows.Count == 1)
                    SelectCustomer(0);
                else
                    gridCustomer.Focus();
            }

        }

        private void gridCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                SelectCustomer(e.RowIndex);
        }

        private void gridCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (gridCustomer.CurrentRow.Index > 0)
                {
                    e.SuppressKeyPress = true;
                    SelectCustomer(gridCustomer.CurrentRow.Index);
                }
            }
        }
    }
}