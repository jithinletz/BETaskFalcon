using System;
using System.Collections.Generic;
using BETask.BAL;
using BETask.Common;
using BETask.Model;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class EmployeeForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Other
        }
        public int _employeeId = 0,_salemanCreditId=0;
        public List<EDMX.employee> listEmployee = new List<EDMX.employee>();
        EmployeeButtonCollection button;
        public EmployeeForm()
        {
            InitializeComponent();
        }
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
                    gridEmployee.CellEnter -= gridEmployee_CellClick;
                    LoadEmployee();
                    GetAllRoutes();
                    GetAllSalesmanCreditLedgers();
                   
                    //txtCusName.Focus();
                    //GetAllCustomers();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    pnlSaveContent.Enabled = false;
                    _employeeId = 0;
                    General.ClearTextBoxes(this);
                   
                    // _mappingId = 0;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveEmployee();
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
                    txtCode.Focus();
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
        }

        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == cmbRoute)
                    btnSave.Focus();
                else
                    General.NextFocus(sender, e);

            }
        }

        #endregion NextFocus

        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
        private bool Validation()
        {
            bool resp = false;
            if (General.IsTextboxEmpty(txtCode)) resp = false; else resp = true;
            if((General.IsTextboxEmpty(txtFirstname))) resp = false; else resp = true;
            //if (!string.IsNullOrEmpty(cmbSalesmanAccount.Text) && this._salemanCreditId==0)
            //{
            //    DAL.DAL.AccountLedgerDAL ledgerDAL = new DAL.DAL.AccountLedgerDAL();
            //    resp = ledgerDAL.ValidateLedger(cmbSalesmanAccount.Text);
            //    if (!resp)
            //    {
            //        General.ShowMessage(General.EnumMessageTypes.Error, "Salesman credit ledgername already exist");
            //        cmbSalesmanAccount.Focus();
            //    }

            //}
            return resp;
        }

        //private int CreateSalesmanCreditLedger()
        //{
        //    int ledgerId = 0; 
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(txtSalesmanCredit.Text))
        //        {
        //            EmployeeBAL employeeBAL = new EmployeeBAL();
        //            ledgerId= employeeBAL.CreateSalesmanLedger(txtSalesmanCredit.Text, this._employeeId,this._salemanCreditId);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return ledgerId;
        //}

        private void SaveEmployee()
        {
            try
            {
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        int routeId = 0;
                        if (!String.IsNullOrEmpty(cmbRoute.Text))
                        {
                            Object selectedRoute = cmbRoute.SelectedItem;
                            routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                        }
                        // if( _salemanCreditId==0)
                        if (!string.IsNullOrEmpty(cmbSalesmanAccount.Text))
                            _salemanCreditId = General.GetComboBoxSelectedValue(cmbSalesmanAccount);
                        else
                            _salemanCreditId = 0;

                        EDMX.employee employee = new EDMX.employee()
                        {
                            employee_id = _employeeId,
                            employee_code = txtCode.Text.Trim(),
                            first_name = txtFirstname.Text.Trim(),
                            last_name = txtLastname.Text.Trim(),
                            username = txtUsername.Text.Trim(),
                            passport = txtPassport.Text.Trim(),
                            join_date = General.ConvertDateServerFormat(dtpJoin.Value),
                            resign_date = chkWorking.Checked == false ? General.ConvertDateServerFormat(dtpResign.Value) : General.ConvertDateServerFormat(new DateTime(1900, 01, 01)),
                            national_id = txtNationalId.Text.Trim(),
                            password = txtPassword.Text.Trim(),
                            visa = txtVisa.Text.Trim(),
                            department = txtDepartment.Text.Trim(),
                            designation = txtDesignation.Text.Trim(),
                            phone = txtPhone.Text.Trim(),
                            email = txtEmail.Text.Trim(),
                            salary = General.ParseDecimal(txtSalary.Text),
                            nationality = txtNationality.Text.Trim(),
                            state = txtSate.Text.Trim(),
                            address1 = txtAddress1.Text.Trim(),
                            address2 = txtAddress2.Text.Trim(),
                            other_details = txtOtherdetails.Text.Trim(),
                            dob = General.ConvertDateServerFormat(dtpDOB.Value),
                            status = chkWorking.Checked ? 1 : 2,
                            gender = cmbGender.Text,
                            route_id=routeId,
                            salesman_credit_ledger=this._salemanCreditId,
                            vehicle=txtVehicle.Text,
                            helper=txtHelper.Text
                           
                        };
                        EmployeeBAL employeeBAL = new EmployeeBAL();
                        employeeBAL.SaveEmployee(employee);

                        General.Action($"Employee details saved {txtCode.Text} {txtFirstname.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success,"New employee saved");
                        ButtonActive(EnumFormEvents.Cancel); 
                    }

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error,$"Unable to save employee something went wrong Please close the form & try again \n{ee.Message}");
                
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
        private void LoadEmployee()
        {
            try
            {
                EmployeeBAL employeeBAL = new EmployeeBAL();
                listEmployee = employeeBAL.GetAllEmployee().ToList();
                General.ClearGrid(gridEmployee);
                foreach (EDMX.employee employee in listEmployee)
                {
                    gridEmployee.Rows.Add(employee.employee_id, employee.first_name);
                    if (employee.department == "RouteUser")
                        General.GridBackcolorOrange(gridEmployee);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, $"Unable to save employee something went wrong{ee.Message}");
            }
        }
        private void Search()
        {
            try
            {
                if (txtSearch.Text.Length > 0)
                {
                    List<EDMX.employee> searcList = new List<EDMX.employee>();
                    General.ClearGrid(gridEmployee);

                    try
                    {
                        searcList = listEmployee.Where(x => x.first_name.ToLower().Contains(txtSearch.Text) || x.last_name.ToLower().Contains(txtSearch.Text) || (x.employee_code!=null && x.employee_code.Contains(txtSearch.Text)) || (x.phone!=null && x.phone.Contains(txtSearch.Text))).ToList();
                    }
                    catch { throw; }

                    foreach (EDMX.employee employee in searcList)
                    {
                        gridEmployee.Rows.Add(employee.employee_id, $" {employee.first_name} - {employee.last_name}");
                      
                    }
                }
                else
                {
                    LoadEmployee();
                }
            }
            catch(Exception ex) { }
        }
        private void FillEmployee(int employeeId)
        {
            try
            {
                EDMX.employee employee = listEmployee.Where(x => x.employee_id == employeeId).FirstOrDefault();
                this._employeeId = employee.employee_id;
                txtCode.Text = employee.employee_code;
                txtFirstname.Text = employee.first_name;
                txtLastname.Text = employee.last_name;
                txtPhone.Text = employee.phone;
                txtEmail.Text = employee.email;
                txtNationalId.Text = employee.national_id;
                txtPassport.Text = employee.passport;
                txtVisa.Text = employee.visa;
                txtDepartment.Text = employee.department;
                if (txtDepartment.Text == "RouteUser")
                    rdbRouteUser.Checked = true;
                else
                    rdbEmployee.Checked = true;
                txtDesignation.Text = employee.designation;
                dtpJoin.Value = employee.join_date;
                dtpDOB.Value = DateTime.Parse(employee.dob.ToString());
                if (employee.resign_date != null)
                    dtpResign.Value = DateTime.Parse(employee.resign_date.ToString());
                txtSalary.Text = employee.salary.ToString();
                txtUsername.Text = employee.username;
                txtPassword.Text = employee.password;
                chkWorking.Checked = employee.status == 1 ? true : false;
                cmbGender.Text = employee.gender;
                txtSate.Text = employee.state;
                txtNationality.Text = employee.nationality;
                txtAddress1.Text = employee.address1;
                txtAddress2.Text = employee.address2;
                txtOtherdetails.Text = employee.other_details;
                cmbSalesmanAccount.Text = employee.salesman_credit_ledger != null ? employee.account_ledger.ledger_name : "";
                _salemanCreditId = employee.salesman_credit_ledger != null ? Convert.ToInt32(employee.salesman_credit_ledger) : 0;
                txtVehicle.Text = employee.vehicle;
                txtHelper.Text = employee.helper;
                if (_salemanCreditId > 0)
                {
                    linkUpdateCustomerLedger.Show();
                    linkSalesmanCreditStatement.Show();
                }
                else
                {
                    linkUpdateCustomerLedger.Hide();
                    linkSalesmanCreditStatement.Hide();
                }
                if (employee.route != null)
                    cmbRoute.Text = employee.route.route_name;

                ButtonActive(EnumFormEvents.Update);
            }
            catch
            {
                throw;
            }
        }
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            button = new EmployeeButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            Application.DoEvents();
            
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        bool isEnterEventAdded = false;

        private void linkUpdateCustomerLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (General.ShowMessageConfirm($"Do you want to update all salesman credit customers sales ledger to {cmbSalesmanAccount.Text} ") == DialogResult.Yes)
            {
                UpdateCustomerLedger();
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
        private void UpdateCustomerLedger()
        {
            try
            {
                EmployeeBAL employeeBAL = new EmployeeBAL();
                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
               
                if (routeId > 0 && this._salemanCreditId > 0)
                {
                    int result = employeeBAL.UpdateCustomerLedger_SalesmanCredit(routeId, this._employeeId, this._salemanCreditId, cmbSalesmanAccount.Text, General.userName);
                    General.ShowMessage(General.EnumMessageTypes.Success, $"{result} succesffuly updated, please cross check");
                    linkUpdateCustomerLedger.Hide();
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Success,"Invalid route or salesman ledger");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error,"Unable to update customer ledger");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (this._salemanCreditId > 0)
            {
                CustomerStatementForm statementForm = new CustomerStatementForm(cmbSalesmanAccount.Text,"SALESMANCREDIT");
                statementForm.ShowDialog();
            }
        }

        private void rdbRouteUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRouteUser.Checked)
                txtDepartment.Text = "RouteUser";
            else
                txtDepartment.Clear();
        }

        private void gridEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    General.ClearTextBoxes(this);
                    if (!isEnterEventAdded)
                    {
                        gridEmployee.CellEnter += gridEmployee_CellClick;
                        isEnterEventAdded = true;
                    }
                    int employeeId = 0;
                    int.TryParse(gridEmployee["clmId", e.RowIndex].Value.ToString(), out employeeId);
                    FillEmployee(employeeId);
                }
                catch(Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error);
                }
            }
        }
    }
    class EmployeeButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
