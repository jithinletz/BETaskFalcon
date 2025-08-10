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
    public partial class DailyCollectionForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Search,
            Cancel,
            Close,
            Print,
            Update,
            UpdateClose

        }
        DailyCollectionButtonCollection button;
        public DailyCollectionForm()
        {
            InitializeComponent();
        }
        public DailyCollectionForm(DateTime date, string customerName,int customerId)
        {
            InitializeComponent();
            dtpFrom.Value = date;
            FormLoad();
            ComboboxItem combobox = new ComboboxItem {
                Text = customerName,
                Value = customerId
            };
            cmbCustomer.Items.Add(combobox);
            cmbCustomer.SelectedIndex = 0;
            Search();
        }
        public DailyCollectionForm(DateTime date, string customerName, int customerId,int deliveryItemId)
        {
            InitializeComponent();
            dtpFrom.Value = date;
            FormLoad();
            ComboboxItem combobox = new ComboboxItem
            {
                Text = customerName,
                Value = customerId
            };
            cmbCustomer.Items.Add(combobox);
            cmbCustomer.SelectedIndex = 0;
            linkRemap.Show();
            linkRemap.Tag = deliveryItemId;
            Search();
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
                    General.ClearGrid(gridCollection);
                    
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
                case EnumFormEvents.Update:
                    UpdateCollection();
                    break;
                case EnumFormEvents.UpdateClose:
                    pnlEditColl.Hide();
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
            else if (sender == button.BtnUpdate)
            {
                ButtonActive(EnumFormEvents.Update);
            }
            else if (sender == button.BtnUpdateClose)
            {
                ButtonActive(EnumFormEvents.UpdateClose);
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
        private void FormLoad()
        {
            GetAllEmployees();
           // GetAllSuppliers();
            GetAllRoutes();
            General.BindPaymentModes(cmbPaymentMode);
            General.BindPaymentModes(cmbNewPaymenyMode); 
            cmbPaymentMode.Text = ""; cmbNewPaymenyMode.Text = "";
           // Search();
   
        }
        private void Search()
        {
            try
            {
                General.ClearGrid(gridCollection);
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int routeId = 0, customerId = 0, employeeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (cmbCustomer.Text != ""&& cmbCustomer.SelectedItem!=null)
                {
                    Object selectedCustomer = cmbCustomer.SelectedItem;
                    customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }
                if (cmbEmployee.Text != "")
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                string paymentMode = "All";
                if (cmbPaymentMode.Text != "")
                {
                    if (General.ValidatePaymentModes(cmbPaymentMode.Text))
                        paymentMode = cmbPaymentMode.Text;
                }

                List<EDMX.daily_collection> listDailyCollection = deliveryBAL.GetDailyColelction(dtpFrom.Value, routeId, employeeId, customerId, paymentMode);
                if (listDailyCollection != null && listDailyCollection.Count > 0)
                {
                    foreach (EDMX.daily_collection coll in listDailyCollection)
                    {
                        if (!chkCollection.Checked && !chkDeposit.Checked)
                        {
                            gridCollection.Columns[8].Visible = false;
                            gridCollection.Rows.Add(coll.delivery_id, coll.customer_id, coll.employee.first_name, coll.customer.route.route_name, coll.customer.customer_name, coll.payment_mode, coll.net_amount, coll.collected_amount, "", "Update", coll.daily_collection_id,coll.old_leaf_count);
                            
                        }
                        else if (chkCollection.Checked && chkDeposit.Checked)
                        {
                            if (coll.delivery_id == null)
                            {
                                gridCollection.Columns[8].Visible = true;
                                gridCollection.Rows.Add(coll.delivery_id, coll.customer_id, coll.employee.first_name, coll.customer.route.route_name, coll.customer.customer_name, coll.payment_mode, coll.net_amount, coll.collected_amount, "To wallet", "Update", coll.daily_collection_id, coll.old_leaf_count);
                            }
                        }
                        else if (chkCollection.Checked && !chkDeposit.Checked)
                        {
                            if (coll.delivery_id == null && coll.is_deposit==2)
                            {
                                gridCollection.Columns[8].Visible = true;
                                gridCollection.Rows.Add(coll.delivery_id, coll.customer_id, coll.employee.first_name, coll.customer.route.route_name, coll.customer.customer_name, coll.payment_mode, coll.net_amount, coll.collected_amount, "To wallet", "Update", coll.daily_collection_id, coll.old_leaf_count);
                            }
                        }
                        else if (!chkCollection.Checked && chkDeposit.Checked)
                        {
                            if (coll.is_deposit == 1)
                            {
                                gridCollection.Columns[8].Visible = false;
                                gridCollection.Rows.Add(coll.delivery_id, coll.customer_id, coll.employee.first_name, coll.customer.route.route_name, coll.customer.customer_name, coll.payment_mode, coll.net_amount, coll.collected_amount, "To wallet", "Update", coll.daily_collection_id, coll.old_leaf_count);
                            }
                        }

                        if (coll.status == 2)
                        {
                            gridCollection.Rows[gridCollection.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    if (!chkCollection.Checked)
                        gridCollection.Rows.Add("", "", "", "", "", "Total", listDailyCollection.Sum(x => x.net_amount), listDailyCollection.Sum(x => x.collected_amount));
                    else
                        gridCollection.Rows.Add("", "", "", "", "", "Total", listDailyCollection.Where(x=>x.delivery_id==null).Sum(x => x.net_amount), listDailyCollection.Where(x => x.delivery_id == null).Sum(x => x.collected_amount));

                    gridCollection.Rows[gridCollection.Rows.Count-1].DefaultCellStyle.BackColor = Color.Yellow;
                    gridCollection.Rows[gridCollection.Rows.Count-1].DefaultCellStyle.ForeColor = Color.Red;
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
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int routeId = 0, customerId = 0, employeeId = 0;
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
                string paymentMode = "All";
                if (cmbPaymentMode.Text != "")
                {
                    if (General.ValidatePaymentModes(cmbPaymentMode.Text))
                        paymentMode = cmbPaymentMode.Text;
                }
                deliveryBAL.PrintDailyCollection(dtpFrom.Value, routeId, employeeId, customerId, paymentMode,chkCollection.Checked);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void UpdateCollection()
        {
            try
            {
                string password = $"{lblCollId.Text.Substring(0,1)}{lblDeliveryId.Text.Substring(0,1)}{lblCustomer.Text.Substring(0,1)}{txtCollectionAmount.Text.Substring(0,1)}";
                if (!General.ValidatePaymentModes(cmbNewPaymenyMode.Text))
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid payment mode. cannot process request");
                    return;
                }
                else if (Convert.ToDecimal(txtCollectionAmount.Text) < 0)
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid collection amount. cannot process request");
                    return;
                }
                //else if (password.ToLower() != txtPassword.Text.ToLower())
                //{
                //    //General.ShowMessage(General.EnumMessageTypes.Warning, "Invalid password. cannot process request");
                //    //return;
                //}

                else
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        EDMX.daily_collection _collection = new EDMX.daily_collection
                        {
                            daily_collection_id = Convert.ToInt32(lblCollId.Text),
                            payment_mode = cmbNewPaymenyMode.Text,
                            collected_amount = Convert.ToDecimal(txtCollectionAmount.Text),
                            net_amount = Convert.ToDecimal(txtNetAmount.Text),
                            delivery_id = Convert.ToInt32(lblDeliveryId.Text),
                        };
                        DeliveryBAL deliveryBAL = new DeliveryBAL();
                        deliveryBAL.UpdateDailyCollection(_collection);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Successfully updated");
                        pnlEditColl.Hide();
                        Search();

                        //Loading Sales
                        BETask.DAL.DAL.SaleDAL sale = new DAL.DAL.SaleDAL();
                        string saleNumber = sale.GetSaleumberByCollection(Convert.ToInt32(lblCollId.Text), Convert.ToInt32(lblDeliveryId.Text));
                        if (!string.IsNullOrEmpty(saleNumber))
                        {
                            SaleForm saleForm = new SaleForm(saleNumber);
                            saleForm.ShowDialog();
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
        private void DailyCollectionForm_Load(object sender, EventArgs e)
        {
            button = new DailyCollectionButtonCollection
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,
                BtnUpdate=btnSave,
                BtnUpdateClose=btnUpdateClose
            };
            FormLoad();
        }

        private bool UpdateDailyCollectionStatus(int deliveryId, int customerId, int status)
        {
            try
            {
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                deliveryBAL.UpdateDailyCollectionStatus( deliveryId,  customerId,  status);
                return true;
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void gridCollection_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string voucher = gridCollection["clmCollectionId", e.RowIndex].Value.ToString();
                    ViewJournalForm viewJournalForm = new ViewJournalForm(voucher);
                    viewJournalForm.ShowDialog();

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            
        }

        private void gridCollection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Wallet
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    int custId = 0;
                    string customerName = "";
                    decimal amount = 0, netAmount;
                    int collId = Convert.ToInt32(gridCollection["clmCollectionId", e.RowIndex].Value);
                    switch (e.ColumnIndex)
                    {
                        case 8:
                            custId = Convert.ToInt32(gridCollection["clmCustomerId", e.RowIndex].Value);
                            customerName = Convert.ToString(gridCollection["clmCustomer", e.RowIndex].Value);
                            amount = Convert.ToDecimal(gridCollection["clmCollectedAmount", e.RowIndex].Value);
                            WalletForm walletForm = new WalletForm(customerName, custId, amount);
                            walletForm.ShowDialog();
                            break;
                        case  9:

                            lblDeliveryId.Text = "0"; lblCustomer.Text = ""; txtCollectionAmount.Text = "0"; txtNetAmount.Text = "0"; cmbNewPaymenyMode.Text = ""; lblCollId.Text = "0"; txtPassword.Clear();
                            custId = Convert.ToInt32(gridCollection["clmCustomerId", e.RowIndex].Value);
                            customerName = Convert.ToString(gridCollection["clmCustomer", e.RowIndex].Value);
                            amount = Convert.ToDecimal(gridCollection["clmCollectedAmount", e.RowIndex].Value);
                            netAmount = Convert.ToDecimal(gridCollection["clmNetAmount", e.RowIndex].Value);
                            int deliveryId = 0;
                            if (gridCollection["clmDeliveryId", e.RowIndex].Value != null)
                            {
                                deliveryId = Convert.ToInt32(gridCollection["clmDeliveryId", e.RowIndex].Value);
                            }

                            string paymentMode = gridCollection["clmPaymentMode", e.RowIndex].Value.ToString();
                            lblDeliveryId.Text = deliveryId.ToString();
                            lblCollId.Text = collId.ToString();
                            lblCustomer.Text = customerName.ToString();
                            lblOldPaymentMode.Text = paymentMode;
                            txtCollectionAmount.Text = String.Format("{0:0.00}", amount);
                            txtNetAmount.Text = String.Format("{0:0.00}", netAmount);
                            if (gridCollection["clmOldLeaf", e.RowIndex].Value != null && Convert.ToInt32(gridCollection["clmOldLeaf", e.RowIndex].Value.ToString()) > 0)
                                lblOldLeaf.Text = $"Old Leafs-{gridCollection["clmOldLeaf", e.RowIndex].Value.ToString()}";
                            else
                                lblOldLeaf.Text = "";
                            cmbNewPaymenyMode.Text = paymentMode;
                            pnlEditColl.Show();
                            break;

                        default:
                            lblCollId.Text = collId.ToString();
                            break;

                    }
                }
            }
            catch(Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void txtCollectionAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void rdlAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdlAll.Checked)
                cmbPaymentMode.Text = "";
        }

        private void DailyCollectionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                ButtonActive(EnumFormEvents.Close);
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
            else if(e.KeyData!=Keys.Back && e.KeyData != Keys.Delete)
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

        private void linkRemap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                deliveryBAL.MapCollectionIdToDelieveryItem(Convert.ToInt32(lblCollId.Text), Convert.ToInt32(linkRemap.Tag.ToString()));
                General.ShowMessage(General.EnumMessageTypes.Success, "Updated");
                pnlEditColl.Hide();
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex,"Unable to update");
            }
        }
    }
    class DailyCollectionButtonCollection
    {
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnUpdateClose { get; set; }
        public Button BtnUpdate { get; set; }

    }
}
