using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Data;

namespace BETask.Views
{
    public partial class DeliveryReturnReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Search,
            Cancel,
            Close,
            Print,

        }
        DataTable tblReturn;
        DeliveryReturReportButtonCollection button;
        List<EDMX.item> listItem;
        public DeliveryReturnReportForm()
        {
            InitializeComponent();
        }
        #region Buttonfunction
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    lblQty.Text = "";
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
            #endregion Buttonfunction
        }
        private void LoadItem(int itemId)
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                  listItem = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listItem)
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
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllSuppliers()
        {
            try
            {
                Application.DoEvents();
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                if (routeId > 0)
                {
                    lblLoading.Text = "Loading route customers .................";
                    lblLoading.Show();
                    Application.DoEvents();
                    CustomerBAL _customerBAL = new CustomerBAL();
                    List<Model.CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1, routeId);
                    foreach (Model.CustomerModel customer in _lstCustomers)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = customer.Customer_Name,
                            Value = customer.Customer_Id
                        };
                        cmbCustomer.Items.Add(_cmbItem);
                    }

                    lblLoading.Hide();
                    Application.DoEvents();

                }
            }
            catch (Exception ee)
            {
                lblLoading.Hide();
                Application.DoEvents();
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
        private void FormLoad()
        {
            GetAllEmployees();
            
          
            GetAllRoutes();
            LoadItem(-1);
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
                int routeId = 0, customerId = 0, employeeId = 0, itemId = 0;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (cmbCustomer.Text != "")
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                if (cmbEmployee.Text != "")
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                lblLoading.Text = "Loading return data .................";
                lblLoading.Show();
                Application.DoEvents();
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                tblReturn = deliveryBAL.GetDeliveryReturnReport(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), routeId, customerId, employeeId, itemId, rdbPermanantReturn.Checked ? 2 : 1);
                General.ClearGrid(dgItems);
                lblQty.Text = "";
                if (tblReturn != null && tblReturn.Rows.Count > 0)
                {
                    foreach (DataRow dr in tblReturn.Rows)
                    {
                        dgItems.Rows.Add(dr["delivery_return_id"], General.ConvertDateAppFormat(DateTime.Parse(dr["return_date"].ToString())), dr["item_name"], dr["qty"], dr["Employee"], dr["route_name"], dr["customer_name"]);
                    }
                    lblQty.Text = String.Format("{0} {1}", "Total Qty", tblReturn.Compute("Sum(qty)", ""));
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            finally
            {
                lblLoading.Hide();
                Application.DoEvents();
            }
        }
        private void Print()
        {
            try
            {
                int routeId = 0, customerId = 0, employeeId = 0, itemId = 0;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (cmbCustomer.Text != "")
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                if (cmbEmployee.Text != "")
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                if (cmbProductName.Text != "")
                {
                    Object selectedProduct = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedProduct).Value;
                }
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                deliveryBAL.PrintDeliveryReturn(General.ConvertDateServerFormat(dtpFrom.Value), General.ConvertDateServerFormat(dtpTo.Value), routeId, customerId, employeeId, itemId,rdbPermanantReturn.Checked?2:1, tblReturn);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
        private void DeliveryReturnForm_Load(object sender, EventArgs e)
        {
            button = new DeliveryReturReportButtonCollection
            {
                
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,
            };
            FormLoad();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem(-1);
        }

        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllSuppliers();
        }
    }
    class DeliveryReturReportButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
