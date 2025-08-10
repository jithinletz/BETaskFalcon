using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BETask.Common;
using BETask.BAL;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class CustomerAssetForm : Form
    {

        CustomerAssetButtonCollection button;
        public enum EnumFormEvents

        {
            FormLoad,
            Cancel,
            Close,
            Save,
            Search,
            Print,
            Agreement,
            UpdateDate
        }
        public int customerId = 0;
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    // ResetForms();
                    //  GetAllEmployees();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    //   cmbEmployee.Text = string.Empty;
                    dgAsset.Enabled = false;
                    General.ClearTextBoxes(this);
                    General.ClearGrid(dgAsset);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveCustomerAsset();
                    break;
                case EnumFormEvents.Print:
                    Print();
                    break;
                case EnumFormEvents.Agreement:
                    AgreementPrint();
                    break;
                case EnumFormEvents.UpdateDate:
                    UpdateDate();
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

            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }

            else if (sender == button.BtnAgreement)
            {
                ButtonActive(EnumFormEvents.Agreement);
            }
            else if (sender == button.BtnUpdateDate)
            {
                ButtonActive(EnumFormEvents.UpdateDate);
            }
        }
        public CustomerAssetForm()
        {
            InitializeComponent();

        }
        public CustomerAssetForm(int customerId, string cutomerName)
        {
            InitializeComponent();
            this.customerId = customerId;
            try
            {
                GetAllCustomer(cutomerName);
                cmbCustomerName.Text = cutomerName;
                GetCustomerAsset();
            }
            catch (Exception ex)
            {

            }
        }

        private void AgreementPrint(bool isSelecteOnly=false)
        {

            try
            {
                List<int> selectedList = new List<int>();
                if (chkSelectedOnly.Checked)
                {
                    selectedList = GetSelectedAgreements();
                }

                int _customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                _customerId = _customerId == 0 ? this.customerId : _customerId;
                if (_customerId > 0)
                {
                    BAL.CustomerAssetBAL customerAssetBAL = new CustomerAssetBAL();
                    customerAssetBAL.AgreementPrint(_customerId,selectedList);
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load report");
            }
        }

        private List<int> GetSelectedAgreements()
        {
            List<int> listSelected = new List<int> { };
            try
            {
                if (dgAsset.Rows.Count > 0)
                {
                    listSelected = dgAsset.SelectedRows
       .Cast<DataGridViewRow>()
       .Select(row => Convert.ToInt32(row.Cells["clmAssetId"].Value))
       .ToList();

                }
            }
            catch
            {
                General.Error("Error while converting ");
            }
            return listSelected;
        }

        private void Print()
        {
            try {

                List<int> selectedList = new List<int>();
                if (chkSelectedOnly.Checked)
                {
                    selectedList = GetSelectedAgreements();
                }

                int customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                if (customerId > 0)
                {
                    int reportType = General.GetComboBoxSelectedValue(cmbLoadType);
                    BAL.CustomerAssetBAL customerAssetBAL = new CustomerAssetBAL();
                    customerAssetBAL.Print(customerId, cmbCustomerName.Text, reportType, selectedList);
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load report");
            }
        }
        private void FormLoad()
        {
            button = new CustomerAssetButtonCollection
            {
                BtnCancel = btnCancel,
                BtnSave = btnSave,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnAgreement = btnAgreement,
                BtnUpdateDate=btnUpdateDates

            };
            if (this.customerId == 0)
                GetAllCustomer("", 100);
            GetAllEmployees();
            LoadItem();
            GetAllRoutes();
            if(General.userName.ToLower()!="letz")
            {
                linkLabelSynchronize.Hide();
            }



            List<string> listTypes = new List<string> { "Agreement", "ALL Active", "Other Transaction", "Closed Agreements" };
            int id = 0;
            foreach (string it in listTypes)
            {
                ComboboxItem comboboxItem = new ComboboxItem
                {
                    Text = it,
                    Value = id
                };
                cmbLoadType.Items.Add(comboboxItem);
                id++;
            }
            cmbLoadType.SelectedIndex = 0;

        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }

        private bool Validation()
        {
            bool resp = true;
            if (string.IsNullOrEmpty(cmbCustomerName.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Customer"); cmbCustomerName.Focus(); resp = false; }
            else if (string.IsNullOrEmpty(cmbItemName.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Item"); cmbItemName.Focus(); resp = false; }
            else if (string.IsNullOrEmpty(cmbReturnType.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select Return Type"); cmbReturnType.Focus(); resp = false; }
            else if (string.IsNullOrEmpty(txtQty.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Enter QTY"); txtQty.Focus(); resp = false; }
            else if (string.IsNullOrEmpty(txtAssetAmount.Text)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Enter Rate"); txtAssetAmount.Focus(); resp = false; }
            return resp;
        }

        private void SaveCustomerAsset()
        {
            try
            {
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {

                        CustomerAssetBAL customerBAL = new CustomerAssetBAL();
                        EDMX.customer_asset customer_Asset = GetAssetItems();
                        if (customer_Asset != null && customer_Asset.item_id != 0)
                        {
                            customerBAL.SaveCustomerAsset(customer_Asset);
                            General.Action($"Customer Asset Details succesfully saved Customer={cmbCustomerName.Text}, Employee={cmbEmployee.Text} , Item={cmbItemName.Text} , Qty={txtQty.Text}, Amount={txtAssetAmount.Text}, Barcode={txtBarcode.Text} , ReturnType={cmbReturnType.Text} , AgreementFrom={dtpAgreementFrom.Value}, AgreementTo={dtpAgreementTo.Value}, AssetDetails={txtOtherAssetDetails.Text} , Date ={dtpGivenDate.Value}");
                            General.ShowMessage(General.EnumMessageTypes.Success, "Customer Asset Details succesfully saved", "Saved");
                            //cmbCustomerName.SelectedIndex = -1;
                            cmbItemName.SelectedIndex = -1;
                            cmbCustomerName.Focus();
                            txtQty.Text = "0";
                            txtAssetAmount.Clear();
                            txtBarcode.Clear();
                            txtPerMonth.Clear();
                            txtOtherAssetDetails.Clear();
                            GetCustomerAsset();

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

        private void UpdateDate()
        {
            if (General.ShowMessageConfirm("Are you sure want to update agreement dates") == DialogResult.Yes)
            {
                try
                {
                    CustomerAssetBAL customerAssetBAL = new CustomerAssetBAL();
                    customerAssetBAL.UpdateDate(General.ConvertDateServerFormat(dtpAgreementFrom.Value), General.ConvertDateServerFormat(dtpAgreementTo.Value),lblAgreement.Text);
                    General.ShowMessage(General.EnumMessageTypes.Success,"Agreement dates successfully updated");
                    btnUpdateDates.Hide();
                }
                catch (Exception ex)
                {
                    General.LogExceptionWithShowError(ex,"Unable to save agreement dates");
                }
            }
        }

        private EDMX.customer_asset GetAssetItems()
        {
            EDMX.customer_asset customer_Asset = new EDMX.customer_asset();

            try
            {
                int employeeId = 0, _customerId = 0, itemId = 0;
                employeeId = General.GetComboBoxSelectedValue(cmbEmployee);

                _customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                _customerId = _customerId == 0 ? this.customerId : _customerId;

                if (cmbItemName.SelectedItem != null)
                {
                    itemId = General.GetComboBoxSelectedValue(cmbItemName);
                    customer_Asset = new EDMX.customer_asset
                    {
                        delivery_date = General.ConvertDateServerFormat(dtpGivenDate.Value),
                        customer_id = _customerId,
                        employee_id = employeeId,
                        item_id = itemId,
                        qty = General.ParseDecimal(txtQty.Text),
                        monthly_purchase = string.IsNullOrEmpty(txtPerMonth.Text) ? 0 : Convert.ToInt32(txtPerMonth.Text),
                        amount = General.ParseDecimal(txtAssetAmount.Text),
                        other_details = txtOtherAssetDetails.Text,
                        status = 1,
                        delivery_type = cmbReturnType.Text,
                        agreement_from = General.ConvertDateServerFormat(dtpAgreementFrom.Value),
                        agreement_to = General.ConvertDateServerFormat(dtpAgreementTo.Value),
                        barcode = txtBarcode.Text,
                        updated_on = DateTime.Now
                    };
                }
            }
            catch { throw; }
            return customer_Asset;
        }

        /*
         * 0-Agreement
1-ALL
2-OtherTransaction
3-OldAgreements
         */
        private void GetCustomerAsset()
        {
            try
            {
                int reportType = General.GetComboBoxSelectedValue(cmbLoadType);
                General.ClearGrid(dgAsset);
                CustomerAssetBAL customerBAL = new CustomerAssetBAL();
                int customerId = this.customerId;
                if (this.customerId == 0)
                    customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                List<EDMX.customer_asset> listAsset = new List<EDMX.customer_asset>();
                if (reportType == 0)
                    listAsset = customerBAL.GetCustomerAssetAgreement(customerId);
                else if (reportType == 1)
                    listAsset = customerBAL.GetCustomerAsset(customerId);
                else if (reportType == 2)
                    listAsset = customerBAL.GetCustomerAssetTransactions(customerId);
                if (reportType == 3)
                    listAsset = customerBAL.GetCustomerAssetAgreement(customerId, null,5);


                if (listAsset != null && listAsset.Count > 0)
                {
                    foreach (EDMX.customer_asset asset in listAsset)
                    {
                        dgAsset.Rows.Add(asset.customer_asset_id, asset.item.item_name, asset.qty, asset.amount, asset.barcode, General.ConvertDateAppFormat(asset.delivery_date), asset.delivery_type, asset.monthly_purchase, asset.remarks, "Update","Close");
                    }
                    lblAgreement.Text = listAsset[0].agreement_no;
                    dtpAgreementFrom.Value = Convert.ToDateTime(listAsset[0].agreement_from);
                    dtpAgreementTo.Value = Convert.ToDateTime(listAsset[0].agreement_to);
                }
                else
                    lblAgreement.Text = "";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllCustomer(string customerName, int maxCount = 0)
        {
            try
            {
                //CustomerBAL _customerBAL = new CustomerBAL();
                //List<Model.CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1);
                DAL.DAL.CustomerDAL customerDAL = new DAL.DAL.CustomerDAL();

                List<EDMX.customer> _listCustomer = customerDAL.GetAllCustomers(maxCount, customerName, 1, 0);

                //foreach (Model.CustomerModel customer in _lstCustomers)
                foreach (EDMX.customer customer in _listCustomer)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = customer.customer_name,
                        Value = customer.customer_id
                    };
                    cmbCustomerName.Items.Add(_cmbItem);
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
        private void LoadItem()
        {
            try
            {
                BAL.ItemBAL itemBAL = new BAL.ItemBAL();
                List<EDMX.item> listItem = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listItem)
                {

                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbItemName.Items.Add(_cmbItem);


                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void CustomerAssetForm_Load(object sender, EventArgs e)
        {
            FormLoad();

        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerAsset();
            GetRouteEmployee();

        }
        private void GetRouteEmployee()
        {
            try
            {
                int _customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                _customerId = _customerId == 0 ? this.customerId : _customerId;
                DAL.DAL.EmployeeDAL employeeDAL = new DAL.DAL.EmployeeDAL();
                EDMX.employee _employee = employeeDAL.GetEmployeeByCustomer(_customerId);
                string employeeName = String.Format("{0} {1}", _employee.first_name, _employee.last_name);
                cmbEmployee.Text = employeeName;

            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int routeId = General.GetComboBoxSelectedValue(cmbRoute);
            SynchCustomerAsset(routeId);
           
        }
        
        private void SynchCustomerAsset(int routeId,int customerId=0)
        {
            try
            {
                if (General.ShowMessageConfirm("Are you sure want to synchronize. It will take few minutes , because of large quantity of customers") == DialogResult.Yes)
                {
                    customerId = customerId == 0 ? this.customerId : customerId;
                    if (customerId == 0)
                    {
                        General.ShowMessage(General.EnumMessageTypes.Warning,"Customer not selected");
                        return;
                    }
                    CustomerAssetBAL customerBAL = new CustomerAssetBAL();
                    int count = customerBAL.SynchCustomerAsset(routeId, customerId);
                    General.ShowMessage(General.EnumMessageTypes.Success, $"{count} records updated", "Updated");
                    linkLabelSynchronize.Hide();
                    GetCustomerAsset();
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
                      
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void txtQty_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPerMonth.Text))
            {
                DAL.DAL.CompanyDAL companyDAL = new DAL.DAL.CompanyDAL();
                EDMX.system_settings system_Settings = companyDAL.GetSystemSettings();
                int itemId = General.GetComboBoxSelectedValue(cmbItemName);
                if (itemId > 0 && itemId == system_Settings.default_item_id)
                    txtPerMonth.Text = txtQty.Text;
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

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetAllCustomer("", 100000);
        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (General.ShowMessageConfirm($"Are you sure want close agreement of {cmbCustomerName.Text}") == DialogResult.Yes)
                {
                    int _customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                    _customerId = _customerId == 0 ? this.customerId : _customerId;
                    string agreement = lblAgreement.Text;
                    if (_customerId > 0 && !string.IsNullOrEmpty(agreement))
                    {
                        CustomerAssetBAL assetBAL = new CustomerAssetBAL();
                        assetBAL.CloseAgreement(_customerId, agreement);
                        GetAssetItems();
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Error while closing agreements");
            }
        }

        private void dgAsset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int assetId = 0, perMonth = 0;
                string serial="", assetDetails="";
                if (e.RowIndex >= 0)
                {
                    assetId = Convert.ToInt32(dgAsset["clmAssetId", e.RowIndex].Value);
                    serial = Convert.ToString( dgAsset["clmBarcode", e.RowIndex].Value);
                    perMonth = Convert.ToInt32(dgAsset["clmPerMonth", e.RowIndex].Value);
                    assetDetails = txtOtherAssetDetails.Text;
                    txtAssetAmount.Text = dgAsset["clmRate", e.RowIndex].Value.ToString();
                    txtQty.Text = dgAsset["clmQty", e.RowIndex].Value.ToString();
                    txtPerMonth.Text = perMonth.ToString();
                    cmbItemName.Text = dgAsset["clmAssetName", e.RowIndex].Value.ToString();
                    txtBarcode.Text = Convert.ToString(dgAsset["clmBarcode", e.RowIndex].Value);
                    txtOtherAssetDetails.Text = Convert.ToString(dgAsset["clmAssetDetails", e.RowIndex].Value);

                }
                if (e.RowIndex >= 0 &&  e.ColumnIndex == dgAsset.ColumnCount -2)
                {
                    if (General.ShowMessageConfirm("Are you sure want to update this") == DialogResult.Yes)
                    {
                        EDMX.customer_asset asset = new EDMX.customer_asset
                        {
                            customer_asset_id=assetId,
                            barcode = serial,
                            monthly_purchase = perMonth,
                            remarks = assetDetails,

                        };
                        CustomerAssetBAL assetBAL = new CustomerAssetBAL();
                        assetBAL.UpdateAssetDetails(asset);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Successfully updated");
                        GetCustomerAsset();
                    }
                }
                else if (e.RowIndex >= 0 && e.ColumnIndex == dgAsset.ColumnCount - 1)
                {
                    if (General.ShowMessageConfirm("Are you sure want to close this item") == DialogResult.Yes)
                    {

                        CustomerAssetBAL assetBAL = new CustomerAssetBAL();
                        assetBAL.CloseAssetItem(assetId);
                        General.ShowMessage(General.EnumMessageTypes.Success, "Successfully updated");
                        GetCustomerAsset();
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to update");
            }
        }

        private void cmbLoadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomerAsset();
        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExistingItemRate();
        }
        private void GetExistingItemRate()
        {
            try
            {
                int itemId = General.GetComboBoxSelectedValue(cmbItemName);
                int customerId = General.GetComboBoxSelectedValue(cmbCustomerName);
                if (itemId > 0 && customerId > 0)
                {
                    DAL.DAL.CustomerAssetDAL assetDAL = new DAL.DAL.CustomerAssetDAL();
                    decimal rate = assetDAL.GetItemRateExistingAgreement(customerId, itemId);
                    txtAssetAmount.Text = rate.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void syncCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SynchCustomerAsset(0,General.GetComboBoxSelectedValue(cmbCustomerName));
        }

        private void linkTaxCalculator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!grpAgreementCalculator.Visible)
                grpAgreementCalculator.Show();
            else
                grpAgreementCalculator.Hide();
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            AgreementCalculator();
        }
        private void AgreementCalculator()
        {
            try
            {
                decimal rateWithTax = 0, tax = 0, taxAmount = 0, agreement = 0;
                if (!string.IsNullOrEmpty(txtRate.Text))
                    rateWithTax = Convert.ToDecimal(txtRate.Text);
                if (!string.IsNullOrEmpty(txtTaxRate.Text))
                    tax = Convert.ToDecimal(txtTaxRate.Text);

                if (rateWithTax > 0)
                {
                    taxAmount = General.TruncateDecimalPlaces((rateWithTax * tax) / (100 + tax), 2);
                    agreement = rateWithTax - taxAmount;
                    txtTaxAmount.Text = taxAmount.ToString();
                    txtAgreement.Text = agreement.ToString();


                }
            }
            catch { }
        }

        private void lblAgreement_Click(object sender, EventArgs e)
        {
            btnUpdateDates.Show();
        }
    }
    class CustomerAssetButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnAgreement { get; set; }
        public Button BtnUpdateDate { get; set; }

    }
}
