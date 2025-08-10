using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Data;
using System.Drawing;

namespace BETask.Views
{
    public partial class DeliveryForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Other,
            Search,
            Print,
            Add,
            UClose,
            UUpdate
        }
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        BAL.SaleBAL saleBAL = new BAL.SaleBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<EDMX.item> listItem = new List<EDMX.item>();
        List<EDMX.route> listRoute;
        List<CustomerModel> _lstCustomers = null;
        List<EDMX.employee> _lstEmployee = null;
        DeliveryButtonCollection button;
        int itemId = -1;

        decimal totalBeforeVat = 0;
        decimal totalDiscount = 0;
        decimal totalVat = 0;
        decimal totalTaxableAmount = 0;
        decimal totalNetAmount = 0;
        int deliveryId = 0;
        public DeliveryForm()
        {
            InitializeComponent();
        }

        #region Buttonfunction
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    pnlSaveContent.Enabled = false;
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    dgItems.Columns["clmUpdate"].Visible = false;
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    pnlSaveContent.Enabled = false;
                    General.ClearTextBoxes(this);
                    ResetForms();
                    button.BtnNew.Enabled = true;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveDelivery();
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    if (button.BtnNew.Text == "&Edit")
                    {
                        button.BtnNew.Enabled = false;
                        button.BtnSave.Enabled = true;
                    }
                    else
                        dtpCopyPrevious.Show();
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Text = "&Update";
                    button.BtnSave.Enabled = false;
                    break;
                case EnumFormEvents.Other:
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    button.BtnSave.Enabled = true;
                    button.BtnNew.Enabled = false;
                    break;
                case EnumFormEvents.Add:
                    dgItems.Rows.Add();
                    dgItems.Focus();
                    dgItems.CurrentCell = dgItems.Rows[dgItems.Rows.Count - 1].Cells[0];
                    dgItems.CurrentCell.Selected = true;
                    chkUseAddedStock.Checked = false;
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Print:
                    Print();

                    break;
                case EnumFormEvents.UUpdate:
                    UpdateScehduledDeliveryQty();
                    break;
                case EnumFormEvents.UClose:
                    pnlEdit.Hide();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Save);
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
            else if (sender == button.BtnAdd)
            {
                ButtonActive(EnumFormEvents.Add);
            }
            else if (sender == button.BtnUClose)
            {
                ButtonActive(EnumFormEvents.UClose);
            }
            else if (sender == button.BtnUUpdate)
            {
                ButtonActive(EnumFormEvents.UUpdate);
            }
        }
        #endregion Buttonfunction

        #region LoadDatas
        private void GetAllRoutes()
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                listRoute = routeBAL.GetAllRoutes();
                txtRoute.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    txtRoute.Items.Add(_cmbItem);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void LoadItem(int itemId)
        {
            try
            {
                listItem = itemBAL.GetAllItem_Sellable();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllCustomer(int route)
        {
            try
            {
                if (_lstCustomers != null && _lstCustomers.Count > 0)
                    _lstCustomers.Clear();
                _lstCustomers = _customerBAL.GetAllCustomers(route);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        /// <summary>
        /// For getting customers of other route
        /// </summary>
        /// <param name="route"></param>
        private void GetAllCustomerOtherRoute()
        {
            try
            {
                int route = General.GetComboBoxSelectedValue(txtRoute);
                if (route > 0)
                {
                    int defaultrouteId = 0;
                    int empId = General.GetComboBoxSelectedValue(cmbEmployee);
                    GetEmployeeRoute(empId, out defaultrouteId);
                    if (route != defaultrouteId)
                    {
                        List<CustomerModel> _lstCustomersOther = _customerBAL.GetAllCustomers(route);
                        if (_lstCustomersOther != null)
                        {
                            foreach (CustomerModel cm in _lstCustomersOther)
                            {
                                _lstCustomers.Add(cm);
                            }
                            UpdateGridAutoComplete_Customer_otherRoute(_lstCustomersOther);
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
        private void GetAllEmployees()
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                _lstEmployee = employeeBAL.GetAllEmployee();
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
        private void FormLaod()
        {
            button = new DeliveryButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint,
                BtnAdd = btnAddNew,
                BtnUClose = btnUpdateClose,
                BtnUUpdate = btnUpdateSave
            };
            ButtonActive(EnumFormEvents.FormLoad);

            ResetForms();
            GetAllEmployees();
            Application.DoEvents();
            LoadItem(itemId);
            //GetAllSuppliers();



        }

        private void SetScreenSize(object sender, EventArgs e)
        {
            // Get the current screen's working area (exclude taskbar and other docked elements)
            Rectangle screen = Screen.PrimaryScreen.Bounds;

            // Set the form's size to 70% of the screen's width and height
            this.Width = (int)(screen.Width * 0.7);
            this.Height = (int)(screen.Height * 0.7);

            // Center the form on the screen
            this.StartPosition = FormStartPosition.CenterParent;
            this.Left = (screen.Width - this.Width) / 3;
            this.Top = (screen.Height - this.Height) / 4;
        }


        private void UpdateGridAutoComplete_Item()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgItems.Columns["ItemName"];
                DataGridViewComboBoxColumn comboItemSummary = (DataGridViewComboBoxColumn)gridItemSummary.Columns["clmItemName_summary"];
                comboItem.HeaderText = "Select Item";
                comboItemSummary.HeaderText = "Select Item";
                foreach (EDMX.item raw in listItem)
                {
                    // col.Add(raw.item_name);  
                    comboItem.Items.Add(raw.item_name);
                    comboItemSummary.Items.Add(raw.item_name);
                }



            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void UpdateGridAutoComplete_Customer()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgItems.Columns["clmCustomer"];
                comboItem.Items.Clear();
                comboItem.HeaderText = "Select Customer";
                foreach (CustomerModel raw in _lstCustomers)
                {
                    // col.Add(raw.item_name);  
                    comboItem.Items.Add(raw.Customer_Name);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void UpdateGridAutoComplete_Customer_otherRoute(List<CustomerModel> listOther)
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgItems.Columns["clmCustomer"];
               
                foreach (CustomerModel raw in listOther)
                {
                    // col.Add(raw.item_name);  
                    comboItem.Items.Add(raw.Customer_Name);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void Autocomplete_Route()
        {
            BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
            try
            {
                List<string> route = new List<string>();
                List<string> vehicle = new List<string>();
                deliveryBAL.DistinctRout_Vehicle(out route, out vehicle);
                txtRoute.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtRoute.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtVehicle.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtVehicle.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection colRoute = new AutoCompleteStringCollection();
                AutoCompleteStringCollection colVehicle = new AutoCompleteStringCollection();
                colRoute.AddRange(route.ToArray());
                colVehicle.AddRange(vehicle.ToArray());
                txtRoute.AutoCompleteCustomSource = colRoute;
                txtVehicle.AutoCompleteCustomSource = colVehicle;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        public void ResetForms()
        {
            try
            {
                General.ClearTextBoxes(this);
                //General.ClearGrid(dgItems);
                General.ClearGridAdvanced(dgItems);
                General.ClearGrid(gridItemSummary);
                txtDeliveryNo.Focus();
                cmbEmployee.SelectedIndex = -1;
                txtDeliveryNo.Text = "0";
                dtpDeliveryDate.Value = DateTime.Today;
                this.deliveryId = 0;
                dtpCopyPrevious.Hide();
                chkUseAddedStock.Checked = false;
                chkAfterDelivery.Checked = false;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        #endregion LoadDatas

        #region gridCalculation

        private void CalucateTotals()
        {
            try
            {

                totalBeforeVat = 0;
                totalDiscount = 0;
                totalVat = 0;
                totalNetAmount = 0;
                totalTaxableAmount = 0;
                totalNetAmount = 0;

                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    totalBeforeVat += General.ParseDecimal(Convert.ToString(dr.Cells["Gross"].Value));
                    totalDiscount += General.ParseDecimal(Convert.ToString(dr.Cells["Discount"].Value));
                    totalVat += General.ParseDecimal(Convert.ToString(dr.Cells["VatAmount"].Value));
                    totalNetAmount += General.ParseDecimal(Convert.ToString(dr.Cells["Net"].Value));
                    totalTaxableAmount = totalBeforeVat - totalDiscount;
                }
                if (!string.IsNullOrEmpty(txtRoundUp.Text))
                    totalNetAmount = totalNetAmount - General.ParseDecimal(txtRoundUp.Text);
                txtTotalBeforeVat.Text = Convert.ToString(totalBeforeVat);
                txtTotalDiscount.Text = Convert.ToString(totalDiscount);
                txtTaxableAmount.Text = Convert.ToString(totalTaxableAmount);
                txtTotalVat.Text = Convert.ToString(totalVat);
                txtNetAmount.Text = Convert.ToString(totalNetAmount);
                if (totalNetAmount >= 0)
                    UpdateItemSummary();


            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void UpdateItemSummary()
        {
            try
            {
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                if (!chkUseAddedStock.Checked)
                {
                    General.ClearGrid(gridItemSummary);
                    int itemId = 0;
                    List<int> listCheckedItems = new List<int>();
                    foreach (DataGridViewRow dr in dgItems.Rows)
                    {
                        if (dr.Cells["ID"].Value != null)
                        {

                            if (itemId == General.ParseInt(dr.Cells["ID"].Value.ToString()))
                                continue;
                            itemId = General.ParseInt(dr.Cells["ID"].Value.ToString());
                            string itemName = dr.Cells["ItemName"].Value.ToString();
                            string packing = dr.Cells["Packing"].Value.ToString();
                            decimal qty = 0;
                            bool isInList = listCheckedItems.IndexOf(itemId) != -1;
                            if (!isInList)
                            {
                                listCheckedItems.Add(itemId);
                                foreach (DataGridViewRow row in dgItems.Rows)
                                {
                                    if (row.Cells["ID"].Value != null)
                                    {
                                        int _itemid = General.ParseInt(row.Cells["ID"].Value.ToString());
                                        if (itemId == _itemid)
                                        {
                                            qty += General.ParseDecimal(Convert.ToString(row.Cells["Qty"].Value));
                                        }

                                    }
                                }
                                if (this.deliveryId == 0)
                                    gridItemSummary.Rows.Add(itemName, itemId, packing, qty, 0, qty, 0);
                                else
                                {
                                    decimal addQty = 0;
                                    addQty = deliveryBAL.GetDeliveryItemSummaryAdditionalItemQty(this.deliveryId, itemId);
                                    gridItemSummary.Rows.Add(itemName, itemId, packing, (qty + addQty), 0, qty, addQty);
                                }

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

        private int GetCustomerIdFromLoadedList(string customerName)
        {
            int customerId = 0;
            if (_lstCustomers.Any(x => x.Customer_Name == customerName))
            {
                customerId = _lstCustomers.Where(x => x.Customer_Name == customerName).FirstOrDefault().Customer_Id;
            }
            else
            {
                CustomerBAL customerBAL = new CustomerBAL();
                customerId = customerBAL.GetCustomerDetailsByName(customerName).Customer_Id;
            }
            return customerId;
        }
        private void AutoPopulateGridColumns(int rowIndex)
        {
            try
            {
                if (dgItems["Qty", rowIndex].Value != null && dgItems["Rate", rowIndex].Value != null)
                {
                    decimal quantity = Convert.ToDecimal(dgItems["Qty", rowIndex].Value);
                    decimal rate = Convert.ToDecimal(dgItems["Rate", rowIndex].Value);
                    decimal discount = General.ParseDecimal(Convert.ToString(dgItems["Discount", rowIndex].Value));
                    int vat = Convert.ToInt32(dgItems["Vat", rowIndex].Value);

                    string customerName = dgItems["clmCustomer", rowIndex].Value.ToString();
                    int customerId = GetCustomerIdFromLoadedList(customerName);


                    decimal gross = 0;
                    decimal discountedAmount = 0;
                    decimal vatAmount = 0;

                    if (quantity > 0)
                    {
                        gross = quantity * rate;
                        discountedAmount = gross;
                    }
                    if (discount > 0)
                    {
                        discountedAmount = General.TruncateDecimalPlaces(discountedAmount - discount);
                    }
                    if (vat > 0)
                    {
                        /*Vat is calculating singe rate and multply with qty*/
                        decimal vatSingleUnit = General.TruncateDecimalPlaces((rate * vat) / 100, 3);
                        vatAmount = vatSingleUnit * quantity;

                        /*Vat is calculating with gross amount*/
                        //vatAmount = General.TruncateDecimalPlaces((discountedAmount * vat) / 100,2);
                    }
                    decimal netAmount = discountedAmount + vatAmount;

                    dgItems["Gross", rowIndex].Value = gross;
                    dgItems["VatAmount", rowIndex].Value = vatAmount;
                    dgItems["Net", rowIndex].Value = netAmount;

                    CalucateTotals();
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        #endregion gridCalculation

        #region SaveSearchPrintLoad
        private void SaveDelivery()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
                    string errorMessage = ValidateForm();
                    if (string.IsNullOrEmpty(errorMessage) && General.CheckFinancialDate(dtpDeliveryDate.Value))
                    {
                        ShowPicLoading();
                        SynchronizationBAL sync = new SynchronizationBAL();
                        if (sync.CloudConnectionStatus(General.cloudConnection))
                        {
                            ShowPicLoading();
                            EDMX.delivery delivery = GetDeliveryItem();
                            List<EDMX.delivery_items> deliveryItems = GetAllDeliveryItems();
                            List<EDMX.delivery_item_summary> deliveryItemSummary = GetAllDeliveryItemSummary();
                            int _savedId = deliveryBAL.SaveDelivery(delivery, deliveryItems, deliveryItemSummary, chkAfterDelivery.Checked);
                            General.Action($"Delivery Note Saved Employee {cmbEmployee.Text} Vehicle {txtVehicle.Text} Date {dtpDeliveryDate.Text}");
                            string saveMessage =  "Delivery Successfully Saved ";
                            General.ShowMessage(General.EnumMessageTypes.Success, saveMessage);
                            Application.DoEvents();
                            ButtonActive(EnumFormEvents.Cancel);
                            ResetForms();
                            PopulateDeliveryDetails(_savedId);
                        }
                        else
                        {
                            General.ShowMessage(General.EnumMessageTypes.Error, "No Connection with cloud . Please check", errorMessage);
                        }
                    }
                    else
                    {
                        if (errorMessage != "")
                            General.ShowMessage(General.EnumMessageTypes.Error, errorMessage);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
      

        private EDMX.delivery GetDeliveryItem()
        {
            EDMX.delivery _delivery = null;
            try
            {


                int empId = 0;
                Object selectedEmployee = cmbEmployee.SelectedItem;
                empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                int routeId = 0;
                txtRoute.Text = GetEmployeeRoute(empId, out routeId);

                if (!String.IsNullOrEmpty(txtRoute.Text))
                {
                    Object selectedRoute = txtRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
               

                _delivery = new EDMX.delivery()
                {
                    delivery_id = General.ParseInt(txtDeliveryNo.Text),
                    employee_id = empId,
                    delivery_route = txtRoute.Text,
                    vehicle_no = txtVehicle.Text,
                    gross_amount = General.ParseDecimal(txtTotalBeforeVat.Text),
                    delivery_date = General.ConvertDateServerFormat(dtpDeliveryDate.Value),
                    net_amount = General.ParseDecimal(txtNetAmount.Text),
                    remarks = txtRemarks.Text,
                    status = 1,
                    total_beforevat = General.ParseDecimal(txtTotalBeforeVat.Text),
                    total_discount = General.ParseDecimal(txtTotalDiscount.Text),
                    total_vat = General.ParseDecimal(txtTotalVat.Text),
                    customer_count = GetCustomerCount(),
                    route_id = routeId,
                    helper=txtHelper.Text
                };
            }
            catch { throw; }

            return _delivery;
        }
        private bool CopyPrevious(bool empSelect = false)
        {
            try
            {

                int empId = General.GetComboBoxSelectedValue(cmbEmployee);
                int routeId = General.GetComboBoxSelectedValue(txtRoute);
               
                if (empId > 0 )
                {
                    DeliveryBAL deliveryBAL = new DeliveryBAL();
                    EDMX.delivery delivery = deliveryBAL.GetDeliveryDetails(empId, routeId, !empSelect ? General.ConvertDateServerFormat(dtpCopyPrevious.Value) : General.ConvertDateServerFormat(dtpDeliveryDate.Value), empSelect);
                    if (delivery != null)
                    {
                      
                        if (empSelect)
                        {
                            dtpCopyPrevious.Hide();
                            PopulateDeliveryDetails(delivery.delivery_id);

                        }
                        else
                        {
                            //tobe do
                           // PopulateDeliveryItems(delivery.delivery_items.ToList());
                            //CalucateTotals();
                        }
                        return true;
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                // General.ShowMessage(General.EnumMessageTypes.Error, "No data available");
            }
            return false;
        }
        private List<EDMX.delivery_items> GetAllDeliveryItems()
        {
            List<EDMX.delivery_items> deliveryItems = new List<EDMX.delivery_items>();
            try
            {
                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    if (dr.DefaultCellStyle.BackColor != System.Drawing.Color.Green)
                    {
                        if (dr.Cells[0].Value != null)
                        {
                            string customerName = dr.Cells["clmCustomer"].Value.ToString();//_lstCustomers.Where(x => x.Customer_Name == dr.Cells["clmCustomer"].Value.ToString()).FirstOrDefault().Customer_Name;
                            int customerId = GetCustomerIdFromLoadedList(customerName);
                            int deliveryItemId =Convert.ToInt32(dr.Cells["clmDeliveryItemId"].Value) , saleId=0;
                            EDMX.delivery_items tmpItems = new EDMX.delivery_items();
                            //After delivery sales update
                            if (chkAfterDelivery.Checked)
                            {
                                dr.Cells["clmDeliveryItemId"].Value = dr.Cells["clmDeliveryItemId"].Value == null ? "0" : dr.Cells["clmDeliveryItemId"].Value;
                                int.TryParse( dr.Cells["clmDeliveryItemId"].Value.ToString(), out deliveryItemId);

                                dr.Cells["clmInvoice"].Value = dr.Cells["clmInvoice"].Value == null ? 0 : dr.Cells["clmInvoice"].Value;
                                int.TryParse(dr.Cells["clmInvoice"].Value.ToString(), out saleId);
                                if (saleId > 0)
                                    tmpItems.sales_id = saleId;
                                else
                                    tmpItems.sales_id = null;

                               //If manual saving include sale posting,daily collection
                                if (deliveryItemId == 0)
                                {
                                    DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                                    EDMX.customer customer = customerDAL.GetCustomerDetails(customerId);
                                    if (customer.payment_mode.ToLower() == "coupon")
                                    {
                                        int oldLeafCount = 0;
                                        int.TryParse(dr.Cells["clmOldLeafCount"].Value.ToString(), out oldLeafCount);
                                        EDMX.daily_collection dc = new EDMX.daily_collection();
                                        dc.old_leaf_count = oldLeafCount;
                                        tmpItems.daily_collection = dc;
                                    }
                                    else if (customer.payment_mode.ToLower() == "do")
                                    {
                                        tmpItems.delivery_leaf = dr.Cells["clmDeliveryLeaf"].Value.ToString();
                                    }
                                }

                            }
                            deliveryItems.Add(new EDMX.delivery_items()
                            {
                                delivery_id = General.ParseInt(txtDeliveryNo.Text),
                                discount = Convert.ToDecimal(dr.Cells["Discount"].Value),
                                gross_amount = Convert.ToDecimal(dr.Cells["Gross"].Value),
                                customer_id = customerId,
                                item_id = Convert.ToInt32(dr.Cells["Id"].Value),
                                net_amount = Convert.ToDecimal(dr.Cells["Net"].Value),
                                qty = Convert.ToDecimal(dr.Cells["Qty"].Value),
                                delivered_qty = 0,
                                rate = Convert.ToDecimal(dr.Cells["Rate"].Value),
                                total_beforvat = Convert.ToDecimal(dr.Cells["Gross"].Value),
                                vat_amount = Convert.ToDecimal(dr.Cells["VatAmount"].Value),
                                status = 1,
                                delivery_item_id = deliveryItemId,
                                sales_id= tmpItems.sales_id,
                                delivery_leaf= tmpItems.delivery_leaf,
                                daily_collection=tmpItems.daily_collection

                            });
                        }
                    }
                }
            }
            catch { throw; }
            return deliveryItems;
        }

        private List<EDMX.delivery_item_summary> GetAllDeliveryItemSummary()
        {
            List<EDMX.delivery_item_summary> deliveryItemSummary = new List<EDMX.delivery_item_summary>();
            try
            {
                foreach (DataGridViewRow dr in gridItemSummary.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        deliveryItemSummary.Add(new EDMX.delivery_item_summary()
                        {
                            delivery_id = General.ParseInt(txtDeliveryNo.Text),
                            item_id = General.ParseInt(dr.Cells["clmItemId_summary"].Value.ToString()),
                            qty = General.ParseDecimal(dr.Cells["clmQty_summary"].Value.ToString()),
                            balance_qty = General.ParseDecimal(dr.Cells["clmBalanceQty_summary"].Value.ToString()),
                            used_qty = General.ParseDecimal(dr.Cells["clmUsedQty_summary"].Value.ToString()),
                            status = 1
                        });
                    }
                }
            }
            catch { throw; }
            return deliveryItemSummary;
        }

        private int GetCustomerCount()
        {
            List<string> customers = new List<string>();
            try
            {
                if (dgItems.Rows.Count >= 1)
                {

                    string customer = string.Empty;
                    foreach (DataGridViewRow dr in dgItems.Rows)
                    {
                        if (dr.Cells["clmCustomer"].Value != null)
                        {
                            if (dr.Cells["clmCustomer"].Value.ToString() != customer)
                            {
                                customer = dr.Cells["clmCustomer"].Value.ToString();

                                bool isInList = customers.IndexOf(customer) != -1;
                                if (!isInList)
                                {
                                    customers.Add(customer);
                                }
                            }
                        }
                    }
                }
            }
            catch { throw; }
            return customers.Count;
        }

        private string ValidateForm()
        {
            string errorMsg = string.Empty;
            if (dtpDeliveryDate.Text == string.Empty)
                errorMsg = "Please enter delivery date";
            else if (cmbEmployee.Text == string.Empty)
                errorMsg = "Please select Delivery Staff";
            else if (dgItems.Rows.Count <= 0)
                errorMsg = "Please add items";

            return errorMsg;
        }
        private void Search()
        {
            DeliverySearchForm deleSearchForm = new DeliverySearchForm();
            DialogResult result = deleSearchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int _deleId = 0;
                _deleId = deleSearchForm.deleId;

                if (_deleId > 0)
                {
                    ButtonActive(EnumFormEvents.Cancel);
                    this.deliveryId = _deleId;
                    PopulateDeliveryDetails(_deleId);

                }
            }
        }
        private void Print()
        {
            try
            {
                BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
                int deliveryId = 0;
                int.TryParse(txtDeliveryNo.Text, out deliveryId);
                deliveryBAL.PrintDelivery(deliveryId);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        //private void PopulateDeliveryDetails(int deliveryId)
        //{
        //    BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
        //    try
        //    {
        //        EDMX.delivery delivery = deliveryBAL.GetDeliveryDetails(deliveryId);
        //        List<EDMX.delivery_items> deliveryItems = delivery.delivery_items.ToList();
        //        List<EDMX.delivery_item_summary> deliveryItemSummary = delivery.delivery_item_summary.ToList();
        //        if (delivery != null)
        //        {
        //            cmbEmployee.Text = String.Format("{0} {1}", _lstEmployee.Where(x => x.employee_id == delivery.employee_id).FirstOrDefault().first_name, _lstEmployee.Where(x => x.employee_id == delivery.employee_id).FirstOrDefault().last_name);
        //            txtDeliveryNo.Text = Convert.ToString(delivery.delivery_id);
        //            txtVehicle.Text = delivery.vehicle_no;
        //            txtHelper.Text = delivery.helper;
        //            txtRoute.Text = delivery.delivery_route;
        //            txtRemarks.Text = delivery.remarks;
        //            txtTaxableAmount.Text = Convert.ToString(delivery.gross_amount);
        //            txtTotalVat.Text = Convert.ToString(delivery.total_vat);
        //            txtTotalBeforeVat.Text = Convert.ToString(delivery.gross_amount);
        //            txtTotalDiscount.Text = Convert.ToString(delivery.total_discount);
        //            txtTaxableAmount.Text = Convert.ToString(delivery.total_beforevat);
        //            txtNetAmount.Text = Convert.ToString(delivery.net_amount);
        //            deliveryId = delivery.delivery_id;
        //            if (delivery.route_id != null)
        //                txtRoute.Text = listRoute.Any(x => x.route_id == delivery.route_id)? listRoute.Where(x => x.route_id == delivery.route_id).FirstOrDefault().route_name:"";

        //            PopulateDeliveryItems(deliveryItems);
        //            PopulateDeliveryItemsSummary(deliveryItemSummary);
        //            ButtonActive(EnumFormEvents.Update);

        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        General.Error(ee.ToString());
        //        General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
        //    }
        //}
        private void PopulateDeliveryDetails(int deliveryId)
        {
            BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
            try
            {
                DataSet ds = deliveryBAL.GetDelivery(deliveryId);
                DataTable tblDelivery = ds.Tables[0];
                DataTable tblDetail = ds.Tables[1];
                DataTable tblSumamry = ds.Tables[2];
               
                if (tblDelivery != null && tblDelivery.Rows.Count>0)
                {
                    var row = tblDelivery.Rows[0];
                    int employeeId = Convert.ToInt32(row["employee_id"]);
                    int routeId = Convert.ToInt32(row["route_id"]);
                    cmbEmployee.Text = String.Format("{0} {1}", _lstEmployee.Where(x => x.employee_id == employeeId).FirstOrDefault().first_name, _lstEmployee.Where(x => x.employee_id == employeeId).FirstOrDefault().last_name);
                    txtDeliveryNo.Text = Convert.ToString(deliveryId);
                    txtVehicle.Text = Convert.ToString(row["vehicle_no"]);
                    txtHelper.Text = Convert.ToString(row["helper"]);
                    txtRoute.Text = Convert.ToString(row["delivery_route"]);
                    txtRemarks.Text = Convert.ToString(row["remarks"]);
                    txtTaxableAmount.Text = Convert.ToString(row["gross_amount"]);
                    txtTotalVat.Text = Convert.ToString(row["total_vat"]);
                    txtTotalBeforeVat.Text = Convert.ToString(row["gross_amount"]);
                    txtTotalDiscount.Text = Convert.ToString(row["total_discount"]);
                    txtTaxableAmount.Text = Convert.ToString(row["total_beforevat"]);
                    txtNetAmount.Text = Convert.ToString(row["net_amount"]);
                    if (routeId != 0)
                        txtRoute.Text = listRoute.Any(x => x.route_id == routeId) ? listRoute.Where(x => x.route_id == routeId).FirstOrDefault().route_name : "";

                    PopulateDeliveryItems(tblDetail);
                    PopulateDeliveryItemsSummary(tblSumamry);
                    ButtonActive(EnumFormEvents.Update);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }


        private void PopulateDeliveryItems(DataTable tblDetails)
        {
            try
            {
                lblDelivered.Text = tblDetails.AsEnumerable().Where(row => row.Field<int>("Status") == 1 || row.Field<int>("Status") == 3).Count().ToString();
                int undelivered = tblDetails.AsEnumerable().Where(row => row.Field<int>("Status") == 1 || row.Field<int>("Status") == 3)
                              .Sum(row => row.Field<int>("Qty"));
                lblUndelivered.Text = undelivered.ToString();
                int defaultRouteId = General.GetComboBoxSelectedValue(txtRoute);
                //General.ClearGrid(dgItems);
                General.ClearGridAdvanced(dgItems);
                Application.DoEvents();
                chkAfterDelivery.Enabled = true;
                foreach (DataRow row in tblDetails.Rows)
                {
                    int customerRoute = Convert.ToInt32(row["route_id"]);
                    string customerName = Convert.ToString(row["customer_name"]);
                    if (customerRoute != defaultRouteId)
                    {
                        CustomerModel cust = _customerBAL.GetCustomerDetail(Convert.ToInt32(row["customer_id"]));
                        List<CustomerModel> listOther = new List<CustomerModel> { cust };
                        UpdateGridAutoComplete_Customer_otherRoute(listOther);
                    }

                    //weather customer is not exist in the list
                    if (!_lstCustomers.Exists(x => x.Customer_Name == customerName))
                    {
                        DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgItems.Columns["clmCustomer"];
                        comboItem.Items.Add(customerName);
                    }

                    dgItems.Rows.Add(customerName,Convert.ToString(row["item_name"])
                        , Convert.ToString(row["item_id"]),
                        Convert.ToString(row["uom_name"]),
                       row["Qty"], row["rate"], row["gross_amount"], row["discount"], row["tax_value"],row["vat_amount"],
                       row["net_amount"],row["delivery_item_id"], "", "",row["sales_id"], "Collection", "Sales", "Return", "Update");

                    if (Convert.ToInt32(row["status"]) == 4 && !dtpCopyPrevious.Visible)
                    {
                        dgItems.Rows[dgItems.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        dgItems.Columns["clmUpdate"].Visible = true;
                    }
                }
                CheckDeliveryItemsMissingInvoices();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void CheckDeliveryItemsMissingInvoices()
        {
            var list = saleBAL.SaleNotGeneratedDeliveries(General.ConvertDateServerFormat( dtpDeliveryDate.Value),0);
            if (list != null && list.Count > 0)
            {
                pnlDeliveryMissing.Show();
                lblMissingInvoiceCount.Text = list.Count.ToString();
            }

        }
        private void PopulateDeliveryItemsSummary(DataTable tblSummary)
        {
            try
            {
                General.ClearGrid(gridItemSummary);

                foreach (DataRow row in tblSummary.Rows)
                {

                    gridItemSummary.Rows.Add(row["item_name"], row["item_id"], "Nos",row["qty"], row["used_qty"], row["balance_qty"], 0);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void LoadAgreementItems(string customerName, int customerId)
        {
            try
            {
                if (customerId > 0)
                {
                    BAL.CustomerAggrementBAL customerAggrementBAL = new BAL.CustomerAggrementBAL();
                    List<EDMX.customer_aggrement> listAgreedItems = customerAggrementBAL.GetCustomerAggrements(customerId);
                    if (listAgreedItems != null && listAgreedItems.Count > 0)
                    {
                        int itemCount = 0;
                        foreach (EDMX.customer_aggrement pi in listAgreedItems)
                        {
                            itemCount++;
                            decimal taxRate = Convert.ToDecimal(listItem.Where(x => x.item_id == pi.item_id).FirstOrDefault().tax_setting.tax_value);
                            int row = dgItems.CurrentRow.Index;
                            blockChangedEvent = true;
                            dgItems["clmCustomer", row].ReadOnly = true;
                            dgItems["clmCustomer", row].Value = customerName;
                            dgItems["itemName", row].Value = pi.item.item_name;
                            dgItems["ID", row].Value = pi.item_id;
                            dgItems["Packing", row].Value = pi.item.uom_setting.uom_name;
                            dgItems["Qty", row].Value = pi.max_qty;
                            dgItems["Rate", row].Value = pi.unit_price;
                            dgItems["Discount", row].Value = 0;
                            blockChangedEvent = false;
                            dgItems["Vat", row].Value = taxRate;
                            if (itemCount < listAgreedItems.Count)
                                dgItems.Rows.Add();
                            dgItems.CurrentCell = dgItems.Rows[dgItems.RowCount - 1].Cells[0];
                        }
                        dgItems.CurrentCell = dgItems.Rows[dgItems.RowCount - 1].Cells[0];

                    }
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion SaveSearchPrintLoad

        #region GeneralControlFunction
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtRemarks)
                {
                    dgItems.Focus();
                    //dgItems.CurrentCell = dgItems["clmCustomer", 0];
                }
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
        #endregion GeneralControlFunction

        #region DirectControlEvent

        private void txtRoundUp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNetAmount.Text = Convert.ToString(General.TruncateDecimalPlaces((General.ParseDecimal(txtTaxableAmount.Text) + General.ParseDecimal(txtTotalVat.Text)) - General.ParseDecimal(txtRoundUp.Text)));

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void lnkTrackDelivery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DeliveryTrackForm deliveryTrackForm = new DeliveryTrackForm();
            deliveryTrackForm.ShowDialog();
        }
        #endregion DirectControlEvent

        #region GridEvents
        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    if (dgItems.Rows.Count > 1)
                    {
                        int row = dgItems.CurrentRow.Index;
                        if (dgItems[0, row].Value != null)
                        {
                            dgItems.Rows.RemoveAt(row);
                            CalucateTotals();
                        }
                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }
        bool blockChangedEvent = false;
        private void dgItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!blockChangedEvent)
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {

                        if (e.ColumnIndex == 1)
                        {
                            DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells["ItemName"];
                            if (cb.Value != null)
                            {
                                string _itemName = dgItems["ItemName", e.RowIndex].Value.ToString();
                                if (!String.IsNullOrEmpty(_itemName))
                                {
                                    var _item = listItem.Where(x => x.item_name == _itemName).FirstOrDefault();
                                    dgItems["Packing", e.RowIndex].Value = _item.uom_setting == null ? string.Empty : _item.uom_setting.uom_name;
                                    dgItems["Qty", e.RowIndex].Value = 1;
                                    dgItems["ID", e.RowIndex].Value = _item.item_id;
                                    dgItems["Rate", e.RowIndex].Value = _item.sale_rate;
                                    dgItems["Discount", e.RowIndex].Value = 0;
                                    dgItems["Vat", e.RowIndex].Value = _item.tax_setting.tax_value;
                                    // dgItems.CurrentCell = dgItems.Rows[e.RowIndex].Cells["Qty"];
                                }

                            }
                        }
                        else if (e.ColumnIndex == 0)
                        {
                            DataGridViewComboBoxCell cbCustomer = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells["clmCustomer"];
                            if (cbCustomer.Value != null)
                            {
                                string customerName = dgItems["clmCustomer", e.RowIndex].Value.ToString();
                                int customerId = GetCustomerIdFromLoadedList(customerName);
                                LoadAgreementItems(customerName, customerId);
                            }
                        }

                        AutoPopulateGridColumns(e.RowIndex);
                    }

                    dgItems.Invalidate();
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    // General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }
        private void dgItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }

            if (dgItems.CurrentRow.Index >= 0 && (dgItems.CurrentCell.ColumnIndex > 1) )
            {
                TextBox tb = e.Control as TextBox;
                if (dgItems.CurrentCell.ColumnIndex != 13)
                {
                   
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                    }
                }
                else
                {
                    tb.KeyPress -= new KeyPressEventHandler(General.TxtOnlyDecimal);
                }
            }
            
           

        }

        private void gridItemSummary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    if (e.ColumnIndex == 0)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)gridItemSummary.Rows[e.RowIndex].Cells["clmItemName_summary"];
                        if (cb.Value != null)
                        {
                            string _itemName = gridItemSummary["clmItemName_summary", e.RowIndex].Value.ToString();
                            if (!String.IsNullOrEmpty(_itemName))
                            {
                                var _item = listItem.Where(x => x.item_name == _itemName).FirstOrDefault();
                                gridItemSummary["clmPacking_summary", e.RowIndex].Value = _item.uom_setting == null ? string.Empty : _item.uom_setting.uom_name;
                                gridItemSummary["clmQty_summary", e.RowIndex].Value = 1;
                                gridItemSummary["clmItemId_summary", e.RowIndex].Value = _item.item_id;
                                gridItemSummary["clmUsedQty_summary", e.RowIndex].Value = _item.sale_rate;
                                gridItemSummary["clmBalanceQty_summary", e.RowIndex].Value = 1;
                                gridItemSummary.CurrentCell = gridItemSummary.Rows[e.RowIndex].Cells["clmQty_summary"];
                            }

                        }
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        gridItemSummary["clmBalanceQty_summary", e.RowIndex].Value = gridItemSummary["clmQty_summary", e.RowIndex].Value;
                    }
                    //AdditionalQty
                    else if (e.ColumnIndex == 6)
                    {
                        decimal addQty = 0, qty = 0;
                        decimal.TryParse(gridItemSummary["clmAdditionalQty_summary", e.RowIndex].Value.ToString(), out addQty);
                        //if (addQty == 0)
                        {
                            int itemId = 0;
                            int.TryParse(gridItemSummary["clmItemId_summary", e.RowIndex].Value.ToString(), out itemId);
                            if (itemId > 0)
                            {
                                qty = 0;
                                foreach (DataGridViewRow dr in dgItems.Rows)
                                {
                                    if (dr.Cells["ID"].Value.ToString() == itemId.ToString())
                                    {
                                        decimal _qty = 0;
                                        decimal.TryParse(dr.Cells["QTY"].Value.ToString(), out _qty);
                                        qty += _qty;
                                        gridItemSummary["clmQty_summary", e.RowIndex].Value = qty;
                                    }
                                }
                            }
                        }
                        gridItemSummary["clmQty_summary", e.RowIndex].Value = qty + addQty;

                    }

                }
                gridItemSummary.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void gridItemSummary_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            gridItemSummary.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void gridItemSummary_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }

            if (gridItemSummary.CurrentRow.Index >= 0 && (gridItemSummary.CurrentCell.ColumnIndex > 1))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                }
            }
        }
        private void dgItems_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 4)//Qty
                {
                    decimal changedQty = General.ParseDecimal(dgItems["Qty", e.RowIndex].Value.ToString());
                    DataGridViewComboBoxCell cbCustomer = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells["clmCustomer"];
                    if (cbCustomer.Value != null)
                    {
                        string customerName = dgItems["clmCustomer", e.RowIndex].Value.ToString();
                        int itemId = General.ParseInt(dgItems["ID", e.RowIndex].Value.ToString());
                        int customerId = GetCustomerIdFromLoadedList(customerName);
                        decimal maxQty = GetCustomerMaxQty(customerId, itemId);
                        if (maxQty > 0)
                        {
                            if (changedQty > maxQty)
                            {
                                dgItems["Qty", e.RowIndex].Value = maxQty;
                            }
                        }
                        else if(maxQty==0 && changedQty>0)
                            dgItems["Qty", e.RowIndex].Value = changedQty;
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private decimal GetCustomerMaxQty(int customerId, int itemId)
        {
            decimal maxQty = 0;
            try
            {
                BAL.CustomerAggrementBAL customerAggrementBAL = new BAL.CustomerAggrementBAL();
                maxQty = customerAggrementBAL.GetCustomerAggrements(customerId).Where(x => x.item_id == itemId).FirstOrDefault().max_qty;
            }
            catch
            {
                throw;
            }
            return maxQty;
        }




        #endregion



        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex >= 0)
            {

                AutoLoadDelivery();
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                EmployeeBAL employee = new EmployeeBAL();
                EDMX.employee emp = employee.GetAllEmployeeDetails(employeeId);
                if (this.deliveryId == 0 && string.IsNullOrEmpty(txtVehicle.Text))
                    txtVehicle.Text = emp.vehicle;
                if (this.deliveryId == 0 && string.IsNullOrEmpty(txtHelper.Text))
                    txtHelper.Text = emp.helper;
            }
        }
        private void AutoLoadDelivery()
        {
            ButtonActive(EnumFormEvents.New);
            button.BtnNew.Text = "&New";
            button.BtnSave.Text = "&Save";
            DeliveryBAL deliveryBAL = new DeliveryBAL();
            GetAllRoutes();
            int empId = General.GetComboBoxSelectedValue(cmbEmployee);
            if (empId > 0)
            {
                GetEmployeeRoute(empId, out int routeId);
                if (routeId == 0)
                {
                    DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                    routeId = deliveryDAL.GetDeliveryRouteId(empId, General.ConvertDateServerFormat(dtpDeliveryDate.Value));
                }
                txtRoute.Text = routeId.ToString();

                GetAllCustomer(routeId);
                Application.DoEvents();
                General.ClearGrid(gridItemSummary);
                UpdateGridAutoComplete_Item();
                Application.DoEvents();
                General.ClearGridAdvanced(dgItems);
                UpdateGridAutoComplete_Customer();
                if (CopyPrevious(true))
                    ButtonActive(EnumFormEvents.Update);
            }

        }
        private string GetEmployeeRoute(int employeeId, out int routeId)
        {
            string route = "";
            routeId = 0;
            try
            {
                EmployeeBAL employeeBAL = new EmployeeBAL();
                EDMX.employee employee = employeeBAL.GetAllEmployeeDetails(employeeId);
                if (employee != null && employee.route_id != null)
                {
                    route = listRoute.Where(x => x.route_id == employee.route_id).FirstOrDefault().route_name;
                    routeId = General.ParseInt(employee.route_id.ToString());
                }
            }
            catch
            {
                throw;
            }
            return route;
        }

        private void LoadUpdateView(int rowIdx)
        {
            try
            {

                int customerId = _lstCustomers.Where(x => x.Customer_Name == dgItems["clmCustomer", rowIdx].Value.ToString()).FirstOrDefault().Customer_Id;


                pnlEdit.Show();
                lblUDeliveryId.Text = txtDeliveryNo.Text;
                lblUCustomer.Text = dgItems["clmCustomer", rowIdx].Value.ToString();
                lblUCustomerId.Text = customerId.ToString();
                lblUItemId.Text = dgItems["ID", rowIdx].Value.ToString();
                lblUItemName.Text = dgItems["ItemName", rowIdx].Value.ToString();
                lblDeliveryItemId.Text = dgItems["clmDeliveryItemId", rowIdx].Value.ToString();
                lblUVat.Text = dgItems["Vat", rowIdx].Value.ToString();

                txtUXQty.Text = dgItems["QTY", rowIdx].Value.ToString();
                txtUNewQty.Text = dgItems["QTY", rowIdx].Value.ToString();
                txtURate.Text = dgItems["Rate", rowIdx].Value.ToString();
                txtUVat.Text = dgItems["VatAmount", rowIdx].Value.ToString();
                txtUGross.Text = dgItems["Gross", rowIdx].Value.ToString();
                txtUNet.Text = dgItems["Net", rowIdx].Value.ToString();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void UpdateScehduledDeliveryQty()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (Convert.ToDecimal(txtUXQty.Text) == Convert.ToDecimal(txtUNewQty.Text))
                    {
                        General.ShowMessage(General.EnumMessageTypes.Warning, "No changes found");
                        return;
                    }
                    else
                    {
                        DeliveryBAL deliveryBAL = new DeliveryBAL();

                        decimal gross = General.ParseDecimal(txtUGross.Text);
                        decimal vatAmount = General.ParseDecimal(txtUVat.Text);
                        decimal net = General.ParseDecimal(txtUNet.Text);
                        decimal qty = General.ParseDecimal(txtUNewQty.Text);
                        decimal oldQty = General.ParseDecimal(txtUXQty.Text);

                        deliveryBAL.UpdateScehduledDeliveryQty(int.Parse(txtDeliveryNo.Text), Convert.ToInt32(lblDeliveryItemId.Text), oldQty, qty, gross, vatAmount, net);
                        
                        PopulateDeliveryDetails(int.Parse(txtDeliveryNo.Text));
                        General.ShowMessage(General.EnumMessageTypes.Success, "Delivery quantity succesfully updated");
                        pnlEdit.Hide();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void CalulateUpdateNet()
        {
            try
            {
                txtUVat.Text = "0.00";
                txtUGross.Text = "0.00";
                txtUNet.Text = "0.00";

                decimal xqty = General.ParseDecimal(txtUXQty.Text);
                decimal qty =!string.IsNullOrEmpty(txtUNewQty.Text) ?General.ParseDecimal(txtUNewQty.Text):0;
                if (qty > 0)
                {
                    decimal rate = General.ParseDecimal(txtURate.Text);
                    decimal vatP = General.ParseDecimal(lblUVat.Text);
                    decimal vatAmount = General.ParseDecimal(txtUVat.Text);

                    decimal gross = (qty * rate);
                    vatAmount = gross * vatP / 100;

                    decimal net = General.TruncateDecimalPlaces((gross + vatAmount), 2);
                    txtUVat.Text = General.TruncateDecimalPlaces(vatAmount, 2).ToString();
                    txtUGross.Text = General.TruncateDecimalPlaces(gross, 2).ToString();
                    txtUNet.Text = net.ToString();
                }
                else
                {
                    txtUVat.Text = "0.00";
                    txtUGross.Text = "0.00";
                    txtUNet.Text = "0.00";
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private void DeliveryForm_Load(object sender, EventArgs e)
        {

            FormLaod();
            SetScreenSize(sender,e);
        }

        private void dtpCopyPrevious_ValueChanged(object sender, EventArgs e)
        {
            CopyPrevious();
        }
        private void ShowPicLoading()
        {
            if (lblCloudStatus.Visible)
                lblCloudStatus.Hide();
            else
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                Application.DoEvents();
                lblCloudStatus.Show();
                Application.DoEvents();
            }
        }

        private void dgItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (!dgItems.Rows[e.RowIndex].IsNewRow)
            {
                if (dgItems.Rows[e.RowIndex].DefaultCellStyle.BackColor == System.Drawing.Color.Green)
                {
                    e.Cancel = true;
                }
            }
          
        }

        private void gridItemSummary_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex != 6)
                e.Cancel = true;
           
        }

        private void dtpDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            AutoLoadDelivery();
        }

        private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex < 0)
            {
                if (!string.IsNullOrEmpty(dgItems["clmDeliveryItemId", e.RowIndex].Value.ToString()))
                {
                    CustomerForm customer = new CustomerForm(1,dgItems["clmCustomer", e.RowIndex].Value.ToString());
                    customer.ShowDialog();
                }
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                if (dgItems.Rows[e.RowIndex].DefaultCellStyle.BackColor == System.Drawing.Color.Green)
                {
                    if (!chkAfterDelivery.Checked)
                        chkAfterDelivery.Checked = true;
                    dgItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Orange;
                }
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                try
                {
                    int deliveryItemId = Convert.ToInt32(dgItems["clmDeliveryItemId", e.RowIndex].Value.ToString());
                    DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                    decimal rate = deliveryDAL.GetActuaItemRateByDelivery(deliveryItemId);
                    if (rate > 0)
                    {
                        dgItems["Rate", e.RowIndex].Value = rate;
                        dgItems.Invalidate();

                    }
                }
                catch (Exception ex)
                {
                    General.Error(ex.ToString());
                }
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 12)
            {
                int invoiceId = 0;
                int.TryParse(dgItems[e.ColumnIndex, e.RowIndex].Value.ToString(), out invoiceId);
                if (invoiceId > 0)
                {
                    ShowSales(invoiceId);
                }
            }
        }
        private void ShowSales(int saleId)
        {
            try
            {
                DAL.DAL.SaleDAL saleDal = new DAL.DAL.SaleDAL();
                EDMX.sales sales = saleDal.GetSaleDetails(saleId);
                if (sales != null)
                {
                    string salesNumber =sales.sales_number;
                    SaleForm saleForm = new SaleForm(salesNumber);
                    saleForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void chkAfterDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAfterDelivery.Checked)
            {
                dgItems.Columns["clmOldLeafCount"].Visible = true;
                dgItems.Columns["clmDeliveryLeaf"].Visible = true;

            }
            else { 
                dgItems.Columns["clmOldLeafCount"].Visible = false;
                dgItems.Columns["clmDeliveryLeaf"].Visible = false;
            }
        }

        private void linkSpotDelivery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

                DeliveryBAL deliveryBAL = new DeliveryBAL();
                deliveryBAL.EnableSpotDelivery(Convert.ToInt32(txtDeliveryNo.Text));
                General.ShowMessage(General.EnumMessageTypes.Success, "Spot delivery enabled");
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void dgItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int custId = 0;
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (dgItems["clmCustomer", e.RowIndex].Value != null)
                    {
                        if (_lstCustomers.Any(x => x.Customer_Name.ToLower() == dgItems["clmCustomer", e.RowIndex].Value.ToString().ToLower()))
                        {
                            custId = _lstCustomers.FirstOrDefault(x => x.Customer_Name.ToLower() == dgItems["clmCustomer", e.RowIndex].Value.ToString().ToLower()).Customer_Id;

                        }
                    }
                }

            }
            catch { }
            if (e.RowIndex >= 0 && e.ColumnIndex == dgItems.Columns.Count - 1)
            {
                if (dgItems.Rows[e.RowIndex].DefaultCellStyle.BackColor != System.Drawing.Color.Green && dgItems.Rows[e.RowIndex].DefaultCellStyle.BackColor != System.Drawing.Color.Orange)
                    LoadUpdateView(e.RowIndex);
                else
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Delivered items cannot be updated");
            }
            //View Return
            else if (e.RowIndex >= 0 && e.ColumnIndex == dgItems.Columns.Count - 2)
            {
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                DeliveryReturnForm dReturn = new DeliveryReturnForm(cmbEmployee.Text, dgItems["clmCustomer", e.RowIndex].Value.ToString(),dtpDeliveryDate.Value);
                dReturn.ShowDialog();
            }
            //View Sales
            else if (e.RowIndex >= 0 && e.ColumnIndex == dgItems.Columns.Count - 3)
            {
                int deliveryId = Convert.ToInt32(dgItems["clmDeliveryItemId", e.RowIndex].Value.ToString());
                lblSalesCustomer.Text = dgItems["clmCustomer", e.RowIndex].Value.ToString();
                ShowSelectedSales(deliveryId);
            }
            //View collectoion
            else if (e.RowIndex >= 0 && e.ColumnIndex == dgItems.Columns.Count - 4)
            {

                int deliveryId = Convert.ToInt32(dgItems["clmDeliveryItemId", e.RowIndex].Value.ToString());
                string customerName = dgItems["clmCustomer", e.RowIndex].Value.ToString();
                DailyCollectionForm collForm = new DailyCollectionForm(dtpDeliveryDate.Value, customerName, custId, deliveryId);
                collForm.ShowDialog();
            }
           
        }
        private void ShowSelectedSales(int deliveryItemId)
        {
            
            try
            {
                General.ClearGrid(gridSales);
                DAL.DAL.DeliveryDAL deliveryDAL=new DAL.DAL.DeliveryDAL();
                EDMX.delivery_items delivery_Items = deliveryDAL.GetDeliveryItem(deliveryItemId);
                if (delivery_Items != null)
                {
                    pnlSales.Show();
                    List<EDMX.sales> listSale = saleBAL.SearchSale(General.ConvertDateServerFormatWithStartTime(dtpDeliveryDate.Value), General.ConvertDateServerFormatWithEndTime(dtpDeliveryDate.Value), delivery_Items.customer_id);
                    foreach (EDMX.sales sale in listSale)
                    {
                        gridSales.Rows.Add(sale.sales_id, sale.customer.customer_name, General.ConvertDateAppFormat(sale.sales_date), sale.sales_number, sale.net_amount);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void txtUNewQty_TextChanged(object sender, EventArgs e)
        {
            CalulateUpdateNet();
        }

        private void linkLoading_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int deliveryId = 0,employeeId=0;
            deliveryId = Convert.ToInt32(txtDeliveryNo.Text);

            if (deliveryId > 0)
            {
                Object selectedEmployee = cmbEmployee.SelectedItem;
                employeeId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;

                DeliveryLoadManageForm deliveryLoadManageForm = new DeliveryLoadManageForm(deliveryId, employeeId,dtpDeliveryDate.Value);
                deliveryLoadManageForm.ShowDialog();
            }
        }

        private void txtRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllCustomerOtherRoute();
        }

        private void ShowDSR(bool routeWise=false)
        {
            try
            {
                int _saleId =Convert.ToInt32(txtDeliveryNo.Text);
                if (!routeWise)
                {
                    CustomerDeliveryReportStaffwise customerDeliveryReportStaffwise = new CustomerDeliveryReportStaffwise(_saleId);
                    DialogResult result = customerDeliveryReportStaffwise.ShowDialog();
                    if (result == DialogResult.OK)
                        return;
                }
                else
                {
                    CustomerDeliveryReportStaffwise customerDeliveryReportStaffwise = new CustomerDeliveryReportStaffwise(_saleId,true);
                    DialogResult result = customerDeliveryReportStaffwise.ShowDialog();
                    if (result == DialogResult.OK)
                        return;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void linkDSR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDSR();
        }

        private void linkMisc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MiscDeliverySaleCollectionCompare compare = new MiscDeliverySaleCollectionCompare(dtpDeliveryDate.Value);
            compare.Show();
            compare.MdiParent = MdiParent;
        }

        private void linkSaleClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlSales.Hide();
        }

        private void gridSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string salesNumber = gridSales["clmInvoiceNo", e.RowIndex].Value.ToString();
                SaleForm saleForm = new SaleForm(salesNumber);
                saleForm.ShowDialog();
            }
        }

      

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowDSR(true);
        }

        private void linkInvoiceMissingClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlDeliveryMissing.Hide();
        }

        private void linkLoadGenerateInvoice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SalePendingDeliveryForm salePendingDeliveryForm = new SalePendingDeliveryForm();
            salePendingDeliveryForm.ShowDialog();
        }

        private void linkCompare_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int deliveryId = int.Parse(txtDeliveryNo.Text);

            DeliveryComparisonForm compare = new DeliveryComparisonForm(deliveryId);
            compare.Show();
            compare.MdiParent = MdiParent;
        }
    }
    class DeliveryButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnAdd { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnUClose { get; set; }
        public Button BtnUUpdate { get; set; }
    }
}
