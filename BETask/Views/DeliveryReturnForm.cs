using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class DeliveryReturnForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Search,
            Cancel,
            Close,
            Print,
            Approve
        }
        DeliveryReturButtonCollection button;
        List<EDMX.item> listItem;
        public DeliveryReturnForm()
        {
            InitializeComponent();
        }
        string employeeName  { get; set; }
        string customerName { get; set; }
        public DeliveryReturnForm(string employeeName, string customerName, DateTime date)
        {
            InitializeComponent();
            this.employeeName = employeeName;
            this.customerName = customerName;
            dtpDeliveryDate.Value = date;
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:

                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    General.ClearTextBoxes(this);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                SaveDeliveryReturn();
                    break;
                case EnumFormEvents.Search:
                    GetDeliveryRetun();
                    break;
                case EnumFormEvents.Approve:
                    ApproveDeliveryReturn();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnApproveALl)
            {
                ButtonActive(EnumFormEvents.Approve);
            }

        }
        private void LoadItem(int itemId)
        {
            try
            {
                //  listItem = itemBAL.GetAllItem_Sellable();
                cmbProductName.Items.Clear();
                if (cmbCustomer.SelectedItem != null)
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    int customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                    if (customerId > 0)
                    {
                        BAL.CustomerAggrementBAL customerAggrementBAL = new BAL.CustomerAggrementBAL();
                       List<EDMX.customer_aggrement> listAgreedItems = customerAggrementBAL.GetCustomerAggrements(customerId);
                        foreach (EDMX.customer_aggrement item in listAgreedItems)
                        {
                            if (item.item.rawmeterial == 1 || item.item.sellable==1)
                            {
                                ComboboxItem _cmbItem = new ComboboxItem()
                                {
                                    Text = item.item.item_name,
                                    Value = item.item_id
                                };
                                cmbProductName.Items.Add(_cmbItem);
                                if(cmbProductName.Items.Count>0)
                                cmbProductName.SelectedIndex = 0;
                            }
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
           // GetAllSuppliers();
          
            //ReRun(false);
            if (!string.IsNullOrEmpty(this.employeeName))
            {
                cmbEmployee.Text = this.employeeName;
                cmbCustomer.Text = this.customerName;
              
            }
            GetDeliveryRetun();

        }
        private bool Validation()
        {
            bool resp = true;
            if (String.IsNullOrEmpty(cmbCustomer.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Customer"); cmbCustomer.Focus(); resp = false; }
            else if (String.IsNullOrEmpty(cmbEmployee.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Employee"); cmbEmployee.Focus(); resp = false; }
            else if (String.IsNullOrEmpty(cmbProductName.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Product"); cmbProductName.Focus(); resp = false; }
            if (General.IsTextboxEmpty(txtQty)) { txtQty.Focus(); resp = false; }
            return resp;
        }
        private void SaveDeliveryReturn()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (General.CheckFinancialDate(dtpDeliveryDate.Value))
                    {
                        DeliveryBAL deliveryBAL = new DeliveryBAL();
                        EDMX.delivery_return delivery_Return = GetReturnItems();
                        if (delivery_Return != null && delivery_Return.item_id != 0)
                        {
                            deliveryBAL.SaveDeliveryReturn(delivery_Return);
                            General.Action($"Delivery return of item succesfully saved Customer={cmbCustomer.Text}, Employee={cmbEmployee.Text} , Item={cmbProductName.Text} , Qty={txtQty.Text}");
                            General.ShowMessage(General.EnumMessageTypes.Success, "Delivery return of item succesfully saved", "Saved");
                            cmbCustomer.SelectedIndex = -1;
                            cmbProductName.SelectedIndex = -1;
                            cmbCustomer.Focus();
                            txtQty.Text = "0";
                            GetDeliveryRetun();
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
        private void ApproveDeliveryReturn()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    DeliveryBAL deliveryBAL = new DeliveryBAL();
                   List<EDMX.delivery_return> delivery_Return = GetReturnItems_Approve();
                    if (delivery_Return != null && delivery_Return.Count != 0)
                    {
                        deliveryBAL.ApproveDeliveryReturn(delivery_Return);
                        General.Action($"Delivery return of item succesfully Approved ");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Delivery return of item succesfully Approved", "Approved");
                        cmbCustomer.SelectedIndex = -1;
                        cmbProductName.SelectedIndex = -1;
                        cmbCustomer.Focus();
                        txtQty.Text = "0";
                        GetDeliveryRetun();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private EDMX.delivery_return GetReturnItems()
        {
            EDMX.delivery_return delivery_Return=new EDMX.delivery_return();
            try
            {
               
                int employeeId = 0, customerId = 0, itemId = 0;
                Object selectedEmployee = cmbEmployee.SelectedItem;
                employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;

                Object selectedCustomer = cmbCustomer.SelectedItem;
                customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;

                if (cmbProductName.SelectedItem != null)
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                    delivery_Return = new EDMX.delivery_return
                    {
                        return_date = General.ConvertDateServerFormat(dtpDeliveryDate.Value),
                        customer_id = customerId,
                        employee_id = employeeId,
                        item_id = itemId,
                        qty = General.ParseDecimal(txtQty.Text),
                        status = 4,
                        return_type=rdbDailyReturn.Checked?1:2,
                        server_time=DateTime.Now

                    };
                }
            }
            catch
            {
                throw;
            }
            return delivery_Return;
        }
        private List<EDMX.delivery_return> GetReturnItems_Approve()
        {
            List<EDMX.delivery_return> delivery_Return = new List<EDMX.delivery_return>();
            try
            {

              

                if (dgItems.Rows.Count>0 && rdbPending.Checked)
                {
                    foreach (DataGridViewRow dr in dgItems.Rows)
                    {
                        if (dr.Cells["clmId"].Value.ToString() != "")
                        {
                            if (General.ParseDecimal(dr.Cells["clmQty"].Value.ToString()) > 0)
                            {
                                delivery_Return.Add(new EDMX.delivery_return
                                {

                                    return_date = General.ConvertDateServerFormat(dtpDeliveryDate.Value),
                                    delivery_return_id = Convert.ToInt32(dr.Cells["clmId"].Value),
                                    customer_id = Convert.ToInt32(dr.Cells["clmCustomerId"].Value),
                                    employee_id = Convert.ToInt32(dr.Cells["clmEmployeeId"].Value),
                                    item_id = Convert.ToInt32(dr.Cells["clmItemId"].Value),
                                    qty = General.ParseDecimal(dr.Cells["clmQty"].Value.ToString()),
                                    status = 4,

                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return delivery_Return;
        }
        private void GetDeliveryRetun()
        {
            try
            {
                //SynchronizationBAL sync = new SynchronizationBAL();
                //sync.DeliveryReturnItems();

                General.ClearGrid(dgItems);
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int employeeId = 0,customerId=0;
                employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                customerId = General.GetComboBoxSelectedValue(cmbCustomer);
                List<EDMX.delivery_return> listReturn = deliveryBAL.GetDelliveryReturn(General.ConvertDateServerFormat(dtpDeliveryDate.Value),rdbApproved.Checked?4:1,employeeId);
                if (customerId > 0)
                    listReturn = listReturn.Where(x => x.customer_id == customerId).ToList();
                if (listReturn != null && listReturn.Count>0)
                {
                    if (rdbApproved.Checked)
                        dgItems.Columns["clmDelete"].Visible = false;
                    else
                        dgItems.Columns["clmDelete"].Visible = true;
                    foreach (EDMX.delivery_return del in listReturn)
                    {
                        dgItems.Rows.Add(del.delivery_return_id, String.Format("{0} {1}", del.employee.first_name, del.employee.last_name), del.customer.route_id != null ? del.customer.route.route_name : "", del.customer.customer_name, del.item.item_name, del.qty, "Delete", del.employee_id, del.customer_id, del.item_id);
                    }
                    dgItems.Rows.Add("", "", "", "", "", listReturn.Sum(x => x.qty));
                    dgItems.Rows[dgItems.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private bool DeleteNotApprovedDeliveryReturn(int deliveryReturnId)
        {
            bool resp = false;
            try
            {
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                resp= deliveryBAL.DeleteNotApprovedDeliveryReturn(deliveryReturnId);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            return resp;
        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
            else if (e.KeyData == Keys.Down && sender==cmbCustomer)
            {
                CustomerSearch();
            }
            else if (e.KeyData != Keys.Back && e.KeyData != Keys.Delete && sender == cmbCustomer)
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
                    ButtonActive(EnumFormEvents.Cancel);
                    GetSupplierDetailsById(_customerId);
                    ButtonActive(EnumFormEvents.New);
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
            button = new DeliveryReturButtonCollection
            {
                
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch=btnSearch,
                BtnApproveALl=btnApprove,
                
            };
            FormLoad();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem(-1);
        }

        private void dtpDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            GetDeliveryRetun();
        }

        private void rdbApproved_CheckedChanged(object sender, EventArgs e)
        {
            GetDeliveryRetun();
        }

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Delete DeliveryReturn
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 6)
                {
                    if (General.ShowMessageConfirm("Are you  sure want to delete this return") == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dgItems["clmId", e.RowIndex].Value);
                        if (DeleteNotApprovedDeliveryReturn(id))
                            dgItems.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void cmbEmployee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDeliveryRetun();
        }
        private void ReRun(bool message)
        {
            //Deleting duplicate records
            try
            {
                BETask.BAL.SynchronizationBAL sync = new BAL.SynchronizationBAL();
                int result = sync.ReRunDeliveryReturn(dtpDeliveryDate.Value.ToString("yyyy-MM-dd"));
                if (message)
                    General.ShowMessage(General.EnumMessageTypes.Success, $"{result} executed");
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void linkRerun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReRun(true);
        }

        
    }
    class DeliveryReturButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnApproveALl { get; set; }
    }
}
