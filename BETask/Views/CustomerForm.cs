using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class CustomerForm : Form
    {

        int _customerId = 0;
        int _customerType = 1;
        int _customerTempId = 0;
        int _divisionId = 0;
        CustomerBAL _customerBAL = null;
        List<DAL.EDMX.route> listRoute;
        List<DAL.EDMX.building> listBuilding;
        List<DAL.Model.GroupCustomerModel> listGroup;
        CustomerButtonCollection button;
        int selectedPendingCusId = 0;
        public CustomerForm(int customerType)
        {
            InitializeComponent();
            _customerType = customerType;
            if (_customerType == 1)
            {
                this.Text = "Customer";

            }
            else
            {
                this.Text = "Supplier";
                lnkCustomerItems.Visible = false;
                lnkMap.Visible = false;
                txtWalletNumber.ReadOnly = true;
                cmbRoute.Visible = false;
                cmbBuilding.Visible = false;
                cmbRouteSearch.Visible = false;
                linkAddAmount.Visible = false;
                txtWalletNumber.Visible = false;
                linkStatementDetailed.Hide();
                //cmbBuilding.Visible = false;
                chkSun.Hide(); chkMon.Hide(); chkTue.Hide(); chkWed.Hide(); chkThu.Hide(); chkFri.Hide(); chkSat.Hide();
                lnkCoupon.Hide();
                lblWalletBalance.Hide();

            }
            _customerBAL = new CustomerBAL();
        }
        public CustomerForm(int customerType,string customerName)
        {
            InitializeComponent();
            _customerType = customerType;
            _customerBAL = new CustomerBAL();
            if (_customerType == 1)
            {
                
                this.Text = "Customer";
                txtCusName.Text = customerName;

            }
            else
            {
                this.Text = "Supplier";
                lnkCustomerItems.Visible = false;
                lnkMap.Visible = false;
                txtWalletNumber.ReadOnly = true;
                cmbRoute.Visible = false;
                cmbBuilding.Visible = false;
                cmbRouteSearch.Visible = false;
                linkAddAmount.Visible = false;
                txtWalletNumber.Visible = false;
                linkStatementDetailed.Hide();
                //cmbBuilding.Visible = false;
                chkSun.Hide(); chkMon.Hide(); chkTue.Hide(); chkWed.Hide(); chkThu.Hide(); chkFri.Hide(); chkSat.Hide();
                lnkCoupon.Hide();
            }
           
        }

        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Search,
            UpdateLedger,
            UpdateClose,
            DivisionSave,
            DivisionClose
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
            else if (sender == button.BtnUpdateLedger)
            {
                ButtonActive(EnumFormEvents.UpdateLedger);
            }
            else if (sender == button.BtnUpdateClose)
            {
                ButtonActive(EnumFormEvents.UpdateClose);
            }
            else if (sender == button.BtnDivisionSave)
            {
                ButtonActive(EnumFormEvents.DivisionSave);
            }
            else if (sender == button.BtnDivisionClose)
            {
                ButtonActive(EnumFormEvents.DivisionClose);
            }

        }
        private void ButtonActive(Enum activeEvent, betaskdbEntities context = null)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    pnlSaveContent.Enabled = false;
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    //button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    dgCustomers.CellEnter -= dgCustomers_CellClick;
                    GetAllCustomers(context);
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    btnNew.Text = "&New";
                    btnSave.Text = "&Save";
                    _customerId = 0;
                    _customerTempId = 0;
                    lblLedger.Text = "";
                    lblLedgerName.Text = "";
                    lblWalletBalance.Text = "0";
                    lblOutstanding.Text = "Outstanding";
                    lblAddedOn.Text = "";
                    pnlSaveContent.Enabled = false;
                    General.ClearTextBoxes(this);
                    txtCusName.Clear();
                    if (cmbOffer.Text != "")
                        cmbOffer.SelectedIndex = -1;

                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveCutomer();
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    txtName.Focus();
                    if (button.BtnNew.Text == "&New")
                    {
                        lblLedger.Text = "";
                        lblLedgerName.Text = "";
                        lblWalletBalance.Text = "0";
                        lblOutstanding.Text = "Outstanding";
                        linkAddAmount.Hide();
                    }
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    btnNew.Text = "&Edit";
                    btnSave.Text = "&Update";
                    break;
                case EnumFormEvents.Search:
                    ButtonActive(EnumFormEvents.Cancel);
                    break;
                case EnumFormEvents.UpdateLedger:
                    UpdateLedger();
                    break;
                case EnumFormEvents.UpdateClose:
                    pnlUpdate.Hide();
                    break;
                case EnumFormEvents.DivisionSave:
                    SaveDivision();
                    break;
                case EnumFormEvents.DivisionClose:
                    pnlDivision.Hide();
                    break;
                default:
                    break;

            }
        }

        private void GetAllRoutes(betaskdbEntities context = null)
        {
            try
            {
                RouteBAL routeBAL = new RouteBAL();
                listRoute = routeBAL.GetAllRoutes(context);
                cmbRoute.Items.Clear();
                cmbRouteSearch.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    cmbRoute.Items.Add(_cmbItem);
                    cmbRouteSearch.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetGroupCustomer(betaskdbEntities context)
        {
            try
            {
                listGroup = _customerBAL.GetGroupCustomer(context);
                if (listGroup != null && listGroup.Count > 0)
                {
                    foreach (var group in listGroup)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = group.GroupName,
                            Value = group.GroupId
                        };
                        cmbGroup.Items.Add(_cmbItem);
                    }
                    cmbGroup.SelectedIndex = -1;

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        /// <summary>
        /// Praash Tmr added on 14-12-2021
        /// get all building from building table
        /// </summary>
        private void GetAllBuildings(betaskdbEntities context = null)
        {
            try
            {
                if (StaticDBData.listBuilding == null || StaticDBData.listBuilding.Count == 0)
                {
                    BuildingBAL buildingBAL = new BuildingBAL();
                    listBuilding = buildingBAL.GetAllBuildings(context);
                }
                else
                    listBuilding = StaticDBData.listBuilding;

                cmbBuilding.Items.Clear();
                foreach (EDMX.building building in listBuilding)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = building.building_name,
                        Value = building.building_id
                    };
                    cmbBuilding.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        //==================================
        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == cmbRoute)
                    btnSave.Focus();
                else if (sender == txtDivision)
                    btnAddDivision.Focus();
                else
                    General.NextFocus(sender, e);
            }
           else if (e.KeyData == Keys.F3)
            {
                RouteForm route = new RouteForm();
                route.ShowDialog();
                GetAllRoutes();
                GetAllBuildings();
            }
           
        }

        #endregion NextFocus

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateCustomer()
        {
            bool response = false;
            if (General.IsTextboxEmpty(txtMobile) && !General.IsTextboxEmpty(txtMobile))
            {
                txtMobile.Text = txtPhone.Text;
            }
            if (General.IsTextboxEmpty(txtName)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please Enter Customer Name"); txtCusName.Focus(); }
            else if (General.IsTextboxEmpty(txtPhone)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please Enter Customer Mobile"); txtPhone.Focus(); }
            else if (String.IsNullOrEmpty(cmbRoute.Text) && _customerType == 1) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Customer Route"); cmbRoute.Focus(); }
            else if (cmbRoute.SelectedIndex == -1 && _customerType == 1) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Customer Route"); cmbRoute.Focus(); }
            //else if (String.IsNullOrEmpty(cmbBuilding.Text) && _customerType == 1) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Customer Building"); cmbBuilding.Focus(); }
            //else if (cmbBuilding.SelectedIndex == -1 && _customerType == 1) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Customer Building"); cmbBuilding.Focus(); }
            else response = true;
            return response;
        }

        private CustomerModel GetCustomerDetail()
        {
            CustomerModel customerModel;
            try
            {
                int routeId = 0,                 buildingId = 0,groupId=0;
                decimal walletBalnce = 0;
                if (!String.IsNullOrEmpty(cmbRoute.Text))
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }
                if (!String.IsNullOrEmpty(cmbBuilding.Text))
                {
                    Object selectedBuilding = cmbBuilding.SelectedItem;
                    buildingId = (int)((BETask.Views.ComboboxItem)selectedBuilding).Value;
                    // buildingName = (string)((BETask.Views.ComboboxItem)selectedBuilding).Text;
                }
                if (!String.IsNullOrEmpty(cmbGroup.Text))
                {
                    Object group = cmbGroup.SelectedItem;
                    groupId = (int)((BETask.Views.ComboboxItem)group).Value;
                }
                customerModel = new CustomerModel()
                {
                    Customer_Id = _customerId,
                    Address1 = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    City = txtCity.Text,
                    Email = txtEmail.Text,
                    Mobile = txtMobile.Text,
                    Customer_Name = txtName.Text,
                    Phone = txtPhone.Text,
                    POBox = txtPoBox.Text,
                    Street = txtStreet.Text,
                    ContactPerson = txtContactPerson.Text,
                    Remarks = txtRemarks.Text,
                    Customer_Type = _customerType,
                    Trn = txtTRN.Text,
                    WalletNumber = txtWalletNumber.Text,
                    status = chkActive.Checked ? 1 : 2,
                    Lat = txtLat.Text,
                    Lng = txtLng.Text,
                    RouteId = routeId,
                    BuildingId = buildingId,
                    LedgerId = General.ParseInt(lblLedger.Text),
                    WalletBalance = General.ParseDecimal(lblWalletBalance.Text),
                    DeliveryInterval = FormatDeliveryInterval(),
                    Paymentmode = cmbPaymentMode.Text,
                    CustomerTempId = _customerTempId,
                    CreditLimit = string.IsNullOrEmpty(txtCreditLimit.Text) ? 0 : Convert.ToDecimal(txtCreditLimit.Text),
                    NewCustomer = chkNewCustomer.Checked ? 1 : 2,
                    EmployeeId = General.GetComboBoxSelectedValue(cmbEmployee),
                    OfferId = General.GetComboBoxSelectedValue(cmbOffer),
                    EnableOffer = chkEnableOffer.Checked ? 1 : 2,
                    EnableOnlinePayment=chkOnlinePayment.Checked?1:2,
                    LocationDistance=txtLocationDistance.Text.Trim(),
                    is_group=chkHeadOffice.Checked,
                    group_id=groupId

                };

            }
            catch { throw; }
            return customerModel;
        }

        private string FormatDeliveryInterval(int type=1,string _interval="")
        {
            string interval = string.Empty;
            if (type == 1)
            {

                string weekdays = string.Empty;
                if (chkSun.Checked) weekdays = chkSun.Tag.ToString();
                if (chkMon.Checked) { weekdays += weekdays != "" ? "," : ""; weekdays += chkMon.Tag.ToString(); }
                if (chkTue.Checked) { weekdays += weekdays != "" ? "," : ""; weekdays += chkTue.Tag.ToString(); }
                if (chkWed.Checked) { weekdays += weekdays != "" ? "," : ""; weekdays += chkWed.Tag.ToString(); }
                if (chkThu.Checked) { weekdays += weekdays != "" ? "," : ""; weekdays += chkThu.Tag.ToString(); }
                if (chkFri.Checked) { weekdays += weekdays != "" ? "," : ""; weekdays += chkFri.Tag.ToString(); }
                if (chkSat.Checked) { weekdays += weekdays != "" ? "," : ""; weekdays += chkSat.Tag.ToString(); }
                interval = $"{weekdays}_{txtInterval.Text.Trim().Replace(",", ".")}";
            }
            else if (type == 2)
            {
                try
                {
                    if (_interval != null && _interval != string.Empty)
                    {
                        if (_interval.Contains("_"))
                        {
                            string weekdays = _interval.Split('_')[0];
                            if (weekdays != null && weekdays.Length > 0)
                            {
                                foreach (char ch in weekdays)
                                {
                                    switch (ch)
                                    {
                                        case '1':
                                            chkSun.Checked = true;
                                            break;
                                        case '2':
                                            chkMon.Checked = true;
                                            break;
                                        case '3':
                                            chkTue.Checked = true;
                                            break;
                                        case '4':
                                            chkWed.Checked = true;
                                            break;
                                        case '5':
                                            chkThu.Checked = true;
                                            break;
                                        case '6':
                                            chkFri.Checked = true;
                                            break;
                                        case '7':
                                            chkSat.Checked = true;
                                            break;
                                    }
                                }
                            }
                            interval = _interval.Split('_')[1];
                        }
                        else
                        {
                            interval = _interval;
                        }
                    }
                }
                catch (Exception ex)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Errow while setting interval");
                }
            }
            return interval;
        }
        private void SaveCutomer()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (ValidateCustomer())
                    {
                        Model.CustomerModel customerModel = GetCustomerDetail();
                        if (customerModel.group_id > 0 && customerModel.Paymentmode != "DO")
                        {
                            throw new Exception($"The group customer cannot be set to a {customerModel.Paymentmode} customer");
                        }
                        _customerBAL.SaveCustomer(customerModel);
                        Application.DoEvents();
                        string _custT = _customerType == 1 ? "Customer" : "Supplier";
                        General.Action($"New {_custT} {txtName.Text} Saved");
                        General.ShowMessage(General.EnumMessageTypes.Success, $"{_custT} Saved successfully !!");
                        UpdateCustomerBalance(customerModel);
                        ButtonActive(EnumFormEvents.Cancel);
                        Application.DoEvents();
                        //GetAllCustomers();
                        //SearchPendingCustomer();
                        linkAddAmount.Hide();
                        pnlMore.Hide();

                    }

                }
            }
            catch (Exception ee)
            {
                General.Error("Error while saving customer , Please check log\n"+ee.Message);
                if (ee.InnerException != null && ee.InnerException.ToString().Contains("Violation of UNIQUE KEY"))
                    General.ShowMessage(General.EnumMessageTypes.Error, "Customer/Supplier Name already Exist . Please try with another Customer/Supplier Name");
                else
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private static void UpdateCustomerBalance(CustomerModel customerModel)
        {
            try
            {
                if (customerModel.Customer_Id > 0)
                {
                    SynchronizationBAL sync = new SynchronizationBAL();
                    sync.CustomerOutstandingRoutewise(0,customerModel.Customer_Id);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetCustomerData(int _customerId)
        {
            try
            {
                linkAddAmount.Show();
                lblWalletBalance.Text = "0";
                CustomerModel _cusModel = _customerBAL.GetCustomerDetail(_customerId);

                General.ClearTextBoxes(this);

                if (_cusModel != null)
                {
                    _customerId = _cusModel.Customer_Id;
                    txtName.Text = _cusModel.Customer_Name;
                    txtAddress1.Text = _cusModel.Address1;
                    txtCity.Text = _cusModel.City;
                    txtStreet.Text = _cusModel.Street;
                    txtAddress2.Text = _cusModel.Address2;
                    txtEmail.Text = _cusModel.Email;
                    txtPhone.Text = _cusModel.Phone;
                    txtMobile.Text = _cusModel.Mobile;
                    txtPoBox.Text = _cusModel.POBox;
                    txtContactPerson.Text = _cusModel.ContactPerson;
                    txtRemarks.Text = _cusModel.Remarks;
                    txtTRN.Text = _cusModel.Trn;
                    txtWalletNumber.Text = _cusModel.WalletNumber;
                    lblWalletBalance.Text = Convert.ToString(_cusModel.WalletBalance);
                    txtLat.Text = _cusModel.Lat;
                    txtLng.Text = _cusModel.Lng;
                    lblLedger.Text = _cusModel.LedgerId.ToString();
                    lblOutstanding.Text = "Outstanding";
                    cmbPaymentMode.Text = _cusModel.Paymentmode;
                    txtInterval.Text = _cusModel.DeliveryInterval!=null?FormatDeliveryInterval(2,_cusModel.DeliveryInterval):"";
                    lblAddedOn.Text = _cusModel.AddedOn;

                    if (_cusModel.RouteId != 0)
                    {
                        cmbRoute.Text = listRoute.Where(x => x.route_id == _cusModel.RouteId).FirstOrDefault().route_name;
                    }
                    if (_cusModel.BuildingId != 0)
                    {
                        cmbBuilding.Text = listBuilding.Where(x => x.building_id == _cusModel.BuildingId).FirstOrDefault().building_name;
                    }
                    if (_cusModel.status == 1)
                        chkActive.Checked = true;
                    else
                    {
                        chkActive.Checked = false;
                    }
                    if (txtWalletNumber.Text.Length <= 3)
                    {
                        linkAddAmount.Hide();
                    }
                    else
                        linkAddAmount.Show();
                    if (_cusModel.LedgerId > 0)
                    {
                        AccountLedgerBAL accountLedger = new AccountLedgerBAL();
                        lblLedgerName.Text = accountLedger.GetLedgerDetail(_cusModel.LedgerId).ledger_name;
                    }
                  
                    chkNewCustomer.Checked = _cusModel.NewCustomer == 1 ? true : false;
                    cmbEmployee.Text = _cusModel.EmployeeName;
                    txtCreditLimit.Text = _cusModel.CreditLimit.ToString();
                    if (_cusModel.OfferId > 0)
                    {
                        OfferBAL offerBAL = new OfferBAL();
                        List<EDMX.offer> listOffers = new List<EDMX.offer>();
                        listOffers = offerBAL.GetOffers();
                        cmbOffer.Text = listOffers.FirstOrDefault(x=>x.offer_id==_cusModel.OfferId).offer_name;
                    }
                    chkEnableOffer.Checked = _cusModel.EnableOffer == 1 ? true : false;
                    chkOnlinePayment.Checked = _cusModel.EnableOnlinePayment == 1 ? true : false;
                    txtLocationDistance.Text = _cusModel.LocationDistance;
                    chkHeadOffice.Checked = _cusModel.is_group;
                    if (_cusModel.group_id > 0 && listGroup.Count > 0)
                    {
                        cmbGroup.Text = listGroup.FirstOrDefault(x=>x.GroupId==_cusModel.group_id).GroupName;
                    }

                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            finally
            {
                //ShowOutstanding();
            }

        }

        private betaskdbEntities GetBetaskdbEntities() {
            return _customerBAL.GetBetaskdbEntities();
        }


        /// <summary>
        /// 
        /// </summary>
        private void GetAllCustomers(betaskdbEntities context=null)
        {
            try
            {
                General.ClearGrid(dgCustomers);
                int routeId = 0;
                if (!String.IsNullOrEmpty(cmbRouteSearch.Text))
                {
                    if (cmbRouteSearch.SelectedItem != null)
                    {
                        Object selectedRoute = cmbRouteSearch.SelectedItem;
                        routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                    }
                }
                List<CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(100, txtCusName.Text, _customerType, routeId,"", context);
                foreach (CustomerModel obj in _lstCustomers)
                {
                    dgCustomers.Rows.Add(obj.Customer_Name, obj.Customer_Id, obj.Phone);
                    if (obj.CloudSunc == 2)
                        dgCustomers.Rows[dgCustomers.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    if (obj.status !=1)
                        dgCustomers.Rows[dgCustomers.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.RosyBrown;
                }
                lblResultCount.Text = dgCustomers.Rows.Count.ToString() + "search results";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
      
        private void FormLoad()
        {
            button = new CustomerButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnUpdateLedger=btnUpdateSave,
                BtnUpdateClose=btnUpdateClose,
                BtnDivisionSave=btnAddDivision,
                BtnDivisionClose=btnCloseDivision
            };
            Application.DoEvents();
            ButtonActive(EnumFormEvents.FormLoad, null);
            betaskdbEntities context = GetBetaskdbEntities();
            Application.DoEvents();
            GetAllRoutes(context);
            GetAllBuildings(context);
            GetAllEmployees(0,context);
            GetOffers(context);
            GetGroupCustomer(context);
            context.Dispose();
        }


        bool isEnterEventAdded = false;
        private void dgCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                _customerId = Convert.ToInt32(dgCustomers["ClmCustomerId", e.RowIndex].Value);
                GetCustomerData(_customerId);
                ButtonActive(EnumFormEvents.Update);
            }

        }

        private void txtCusName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCusName_TextChanged(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void lnkCustomerItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_customerId > 0)
            {
                CustomerAggrementForm customerAggrement = new CustomerAggrementForm(txtName.Text.Trim(), _customerId);
                customerAggrement.ShowDialog();
            }
        }
        private void lnkMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtLat.Text) && !String.IsNullOrEmpty(txtLng.Text))
                {
                    System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo($"http://www.google.com/maps/place/{txtLat.Text},{txtLng.Text}");
                    System.Diagnostics.Process.Start(sInfo);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Unable to Load Location");
            }
        }

        private void txtCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgCustomers.Focus();
            }
        }

        private void linkAddAmount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtWalletNumber.Text.Length >= 3)
            {
                Views.WalletForm wallet = new WalletForm(txtName.Text, _customerId);
                wallet.ShowDialog();
                GetCustomerData(this._customerId);
            }
            else
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Please Add wallet number first");
                return;
            }

        }

        private void CustomerForm_Enter(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void cmbRouteSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void cmbRouteSearch_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbRoute.Text))
            {
                GetAllCustomers();
            }
        }

       
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            General.SetScreenSize_customer(sender, e,this);
        }

        private void dgCustomers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (dgCustomers.CurrentRow.Index >= 0 && dgCustomers.CurrentCell.ColumnIndex >= 0)
                {
                    if (!isEnterEventAdded)
                    {
                        //  dgCustomers.CellEnter += dgCustomers_CellClick;
                        //isEnterEventAdded = true;
                    }
                    _customerId = Convert.ToInt32(dgCustomers["ClmCustomerId", dgCustomers.CurrentRow.Index].Value);
                    GetCustomerData(_customerId);
                    ButtonActive(EnumFormEvents.Update);
                }
            }
        }

        private void lnkCoupon_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._customerId != 0)
            {
                CouponSaleForm coupon = new CouponSaleForm(_customerId);
                coupon.ShowDialog();
            }
        }

        

        private void linkStatement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._customerId > 0)
            {
                if (cmbPaymentMode.Text.ToLower() == "salesmancredit" || cmbPaymentMode.Text.ToLower()=="cash")
                {
                    SaleCustomerReport saleCustomerReport = new SaleCustomerReport(txtName.Text, this._customerId);
                    saleCustomerReport.ShowDialog();
                }
                else
                {
                    int ledgerId = 0;
                    ledgerId = Convert.ToInt32(lblLedger.Text);
                    if (ledgerId > 0)
                    {
                        if (this._customerType == 1)
                        {
                            CustomerStatementForm statementForm = new CustomerStatementForm(txtName.Text, ledgerId);
                            statementForm.ShowDialog();
                        }
                        else
                        {
                            CustomerStatementForm statementForm = new CustomerStatementForm(txtName.Text, ledgerId, true);
                            statementForm.ShowDialog();
                        }

                    }
                }
            }
        }
        private void ShowOutstanding()
        {
            try
            {
                int ledgerId = 0;
                int.TryParse(lblLedger.Text,out ledgerId);
                if (ledgerId > 0)
                {
                    BAL.AccountTransactionBAL accountTransactionBAL = new AccountTransactionBAL();
                    List<DAL.Model.DaybookModel> listAccountTransaction = accountTransactionBAL.CustomerStatementSummary(General.ConvertDateServerFormat(DateTime.Today), ledgerId, 1, 0);
                    if (listAccountTransaction != null)
                    {
                        lblOutstanding.Text = $"Outstanding : { listAccountTransaction.Sum(x=>x.Debit)-listAccountTransaction.Sum(x=>x.Credit) }";
                    }
                }
               
            }
            catch { }
        }

        private void lblOutstanding_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowOutstanding();
        }

        private void linkStatementDetailed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._customerId > 0)
            {
                if (cmbPaymentMode.Text.ToLower() == "salesmancredit" || cmbPaymentMode.Text.ToLower() == "cash")
                {
                    SaleItemWiseReportForm saleCustomerReport = new SaleItemWiseReportForm(txtName.Text,this._customerId);
                    saleCustomerReport.ShowDialog();
                }
                else
                {
                    CustomerStatementDetailedForm statementForm = new CustomerStatementDetailedForm(txtName.Text, this._customerId);
                    statementForm.ShowDialog();
                }
            }
        }

        private void lblWalletBalance_Validated(object sender, EventArgs e)
        {
            CustomerModel cs = _customerBAL.GetCustomerDetail(this._customerId);
            if (lblWalletBalance.Text != cs.WalletBalance.ToString())
            {
                if (General.ShowMessageConfirm("You are trying to change custmer wallet balance directly. Log will be saved against this entry") == DialogResult.No)
                {
                    lblWalletBalance.Text = cs.WalletBalance.ToString();
                }
                else
                {
                    _customerBAL.WalletDirectChangeLog(this._customerId, lblWalletBalance.Text);
                }
            }
        }
        private void LoadAllSalemanLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

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
                    cmbSalesmanLedger.Items.Add(_cmbItem);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void LoadAllLedger()
        {
            BAL.AccountLedgerBAL accountLedgerBAL = new BAL.AccountLedgerBAL();

            try
            {
                cmbLedgerAccount.Items.Clear();
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
               int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.CUSTOMER).group_id);
               List< Model.AccountLedgerModel> listLedger = accountLedgerBAL.GetAllAccountLedger(groupId);
              
               
                foreach (Model.AccountLedgerModel ledger in listLedger)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = ledger.Ledger_name,
                        Value = ledger.Ledger_id
                    };
                    cmbLedgerAccount.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                //  General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void UpdateLedger()
        {
            if (cmbLedgerAccount.Text != "" || !string.IsNullOrEmpty( txtLedgerName.Text))
            {
             
               int newLedgerId = General.GetComboBoxSelectedValue(cmbLedgerAccount);
                int salesmanLedger = General.GetComboBoxSelectedValue(cmbSalesmanLedger);
                if (rdbLedger.Checked)
                    salesmanLedger = 0;
                else if (rdbSalesmanLedger.Checked)
                    newLedgerId = 0;
                else if (rdbChangeLedgerName.Checked)
                {
                    newLedgerId = 0;salesmanLedger = 0;
                }
                if (newLedgerId > 0 || salesmanLedger > 0)
                {
                    _customerBAL.UpdateLedgerId(this._customerId, salesmanLedger, newLedgerId);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Ledger succesfully updated ");
                    lblExistingLedgerId.Text = string.Empty;
                    cmbLedgerAccount.SelectedIndex = -1;
                    pnlUpdate.Hide();
                    GetCustomerData(_customerId);
                }
                else if(rdbChangeLedgerName.Checked && (newLedgerId==0&&salesmanLedger==0))
                {
                    _customerBAL.UpdateCustomerLedgerName(this._customerId, lblLedgerName.Text, txtLedgerName.Text);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Ledger succesfully updated ");
                    pnlUpdate.Hide();
                    GetCustomerData(_customerId);
                }
            }
            else { General.ShowMessage(General.EnumMessageTypes.Warning, "Select new ledger"); }


        }

        private void lblChangeLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                rdbLedger.Checked = false;
                rdbSalesmanLedger.Checked = false;
                string ledgerName = "";
                if (cmbPaymentMode.Text.ToLower() == "salesmancredit")
                {
                    int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                    DAL.DAL.EmployeeDAL employeeDAL = new DAL.DAL.EmployeeDAL();
                    DAL.EDMX.account_ledger sledger = employeeDAL.GetSalemanCreditLedgerByRoute(routeId);
                    if (sledger != null)
                        ledgerName = sledger.ledger_name;
                }
                int xLedgerId = Convert.ToInt32(lblLedger.Text);
                lblExistingLedgerId.Text = xLedgerId.ToString();
                lblUpdateLedgerName.Text = lblLedgerName.Text;
                pnlUpdate.Show();

              
                if (ledgerName != "")
                    cmbLedgerAccount.Text = ledgerName;

                if (this._customerId > 0)
                {
                    DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                    EDMX.account_ledger sLedger = customerDAL.GetCustomerSalemanLedger(this._customerId);
                    if (sLedger != null)
                        lblXSalesmanLedger.Text = sLedger.ledger_name;
                    else
                        lblXSalesmanLedger.Text = "";
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error,"Something went wrong");
            }
        }

        private void rdbLedger_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLedger.Checked)
            {
                LoadAllLedger();
                cmbLedgerAccount.Enabled = true;
                cmbSalesmanLedger.Enabled = false;
            }
            else if(rdbSalesmanLedger.Checked)
            {
                LoadAllSalemanLedger();
                cmbLedgerAccount.Enabled = false;
                cmbSalesmanLedger.Enabled = true;
            }
        }
        private void SaveDivision()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDivision.Text))
                {

                    EDMX.customer_division division = new EDMX.customer_division
                    {
                        division_id = this._divisionId,
                        customer_id = this._customerId,
                        division_name = txtDivision.Text,
                        status = 1
                    };
                    _customerBAL.SaveDivision(division);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Division saved");
                    txtDivision.Clear();
                    LoadDivsion();
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Error while saving division. Please try later");
            }
        }
        private void LoadDivsion()
        {
            try
            {
                this._divisionId = 0;
                General.ClearGrid(gridDivision);
                BETask.DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();
                List<EDMX.customer_division> listDivision = customerDAL.GetCustomerDivision(this._customerId);
                if(listDivision!=null && listDivision.Count>0)
                {
                    foreach (EDMX.customer_division dv in listDivision)
                    {
                        gridDivision.Rows.Add(dv.division_id,dv.division_name);
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "unable to load division");
            }
        }

        private void linkDivision_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!pnlDivision.Visible)
            {
                pnlDivision.Show();
                LoadDivsion();
                txtDivision.Focus();
            }
            else
                pnlDivision.Hide();
        }

        private void gridDivision_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (!string.IsNullOrEmpty(gridDivision["clmDivisionName", e.RowIndex].Value.ToString()))
                {
                    txtDivision.Text = gridDivision["clmDivisionName", e.RowIndex].Value.ToString();
                    _divisionId = Convert.ToInt32(gridDivision["clmDivisionId", e.RowIndex].Value.ToString());
                }
            }
        }

        private void lnkAsset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_customerId > 0)
            {
                CustomerAssetForm customerAsset = new CustomerAssetForm(_customerId, txtName.Text.Trim());
                customerAsset.ShowDialog();
            }
        }

        private void linkUploads_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerDocumentsForm customerDocuments = new CustomerDocumentsForm( _customerId);
            customerDocuments.ShowDialog();
        }

        private void linkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlMore.Visible)
                pnlMore.Hide();
            else
            {
               
                pnlMore.Show();
            }

        }
        private void GetAllEmployees(int _empId = 0, EDMX.betaskdbEntities context = null)
        {
            try
            {
                DAL.DAL.EmployeeDAL employeeBAL = new DAL.DAL.EmployeeDAL();
                cmbEmployee.Items.Clear();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployeeOtherthanRouteuser(context);
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    string routeName = emp.route_id != null ? $"({emp.route.route_name})" : "";
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {

                        Text = String.Format("{0} {1} ", emp.first_name, emp.last_name),
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

        private void linkOffer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OfferForm offerForm = new OfferForm();
            offerForm.ShowDialog();
            GetOffers();
        }
        private void GetOffers(betaskdbEntities context = null)
        {
            try
            {
               cmbOffer.Items.Clear();
                OfferBAL offerBAL = new OfferBAL();
                List<EDMX.offer> listOffers = new List<EDMX.offer>();
                listOffers = offerBAL.GetOffers(context);

                if (listOffers != null && listOffers.Count > 0)
                {
                    foreach (EDMX.offer offers in listOffers)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {

                            Text = offers.offer_name,
                            Value = offers.offer_id
                        };
                        cmbOffer.Items.Add(_cmbItem);
                        
                    }
                    if (cmbOffer.Items.Count > 0)
                        cmbOffer.SelectedIndex = -1;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
            }
        }

        private void linkOnline_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerOnlineProfile customerOnline = new CustomerOnlineProfile(_customerId);
            customerOnline.ShowDialog();
        }

        private void chkHeadOffice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHeadOffice.Checked)
                cmbGroup.Enabled=false;
            else
                cmbGroup.Enabled = true;
        }

        

    }

    class CustomerButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnUpdateLedger { get; set; }
        public Button BtnUpdateClose { get; set; }
        public Button BtnDivisionSave { get; set; }
        public Button BtnDivisionClose { get; set; }
    }
}
