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
    public partial class LoadManagementForm : Form
    {
        LoadManageButtonCollection button;
        LoadingBAL loadingBAL = new LoadingBAL();
        DAL.EDMX.system_settings setting = null;
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Save,
            Search,
            Print
        }

        public LoadManagementForm()
        {
            InitializeComponent();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                   // ResetForms();
                    GetAllEmployees();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbEmployee.Text = string.Empty;
                    dgDelivery.Enabled = false;
                    General.ClearTextBoxes(this);
                    General.ClearGrid(dgDelivery);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveLoad();
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

        }
        private void GetAllEmployees(int _empId = 0)
        {
            try
            {
                BAL.EmployeeBAL employeeBAL = new BAL.EmployeeBAL();
                cmbEmployee.Items.Clear();
                List<EDMX.employee> _lstEmployee = employeeBAL.GetAllEmployee();
                foreach (EDMX.employee emp in _lstEmployee)
                {
                    string routeName = emp.route_id != null ? $"({emp.route.route_name})" : "";
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {

                        Text = String.Format("{0} {1} {2}", emp.first_name, emp.last_name, routeName),
                        Value = emp.employee_id
                    };
                    cmbEmployee.Items.Add(_cmbItem);
                }
                if (_empId >= 1)
                    cmbEmployee.Text = _lstEmployee.Where(x => x.employee_id == _empId).FirstOrDefault().first_name;
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
                        cmbProductName.Items.Add(_cmbItem);
                      
                   
                }
           
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void FillEmployeeDetails()
        {
            try
            {
                int employeeId = General.GetComboBoxSelectedValue(cmbEmployee);
                if (employeeId > 0)
                {
                    EmployeeBAL employeeBAL = new EmployeeBAL();
                    EDMX.employee employee = employeeBAL.GetAllEmployeeDetails(employeeId);
                    if (employee != null)
                    {
                        txtHelper.Text = employee.helper;
                        txtVehicle.Text = employee.vehicle;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void FormLoad()
        {
            button = new LoadManageButtonCollection
            {
                BtnCancel=btnCancel,
                BtnSave=btnSave,
                BtnClose=btnClose,
                BtnPrint=btnPrint

            };
            GetAllEmployees();
            LoadItem();
            DAL.DAL.CompanyDAL company = new DAL.DAL.CompanyDAL();
          setting = company.GetSystemSettings();
        }

        private void LoadManagementForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            General.SetScreenSize(sender,e,this);
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillEmployeeDetails();
                GetPreviousBalance();
                GetAllLoading();
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Something went wrong");
            }
           
        }
        private void Calculation()
        {
            try
            {
               
                decimal oldStock = General.ParseDecimal(txtOldStock.Text);
                decimal damage = General.ParseDecimal(txtDamage.Text);
                decimal newLoad= General.ParseDecimal(txtNewLoad.Text);
                decimal offLoad = General.ParseDecimal(txtOffload.Text);
                decimal totalLoad = General.ParseDecimal(txtTotalLoading.Text);
                decimal empty = General.ParseDecimal(txtEmpty.Text);
                decimal balance = General.ParseDecimal(txtBalance.Text);
                decimal totalEmpty = General.ParseDecimal(txtTotalEmpty.Text);
                decimal totalExtra= General.ParseDecimal(txtTotalExtra.Text);
                decimal totalShort = General.ParseDecimal(txtTotalShort.Text);


                decimal newStock = balance + newLoad-(offLoad);
                //decimal _short = totalLoad - (totalEmpty+empty + balance + damage);
                decimal _short = oldStock - ( empty + balance + damage);
                //_short -= totalShort;
                decimal extra = 0;
                if(_short<0)
                { extra = _short*-1;_short = 0; }

                txtShort.Text = _short.ToString();
                txtExtra.Text = extra.ToString();
                txtNewStock.Text = newStock.ToString();
                txtActualStock.Text = newStock.ToString();

                int itemId = General.GetComboBoxSelectedValue(cmbProductName);
                //Short and extra only for 5gallon
                if (setting != null && setting.default_item_id > 0 && setting.default_item_id != itemId)
                {
                    txtTotalShort.Text = "0";
                    txtTotalExtra.Text = "0";
                    txtShort.Text = "0";
                    txtExtra.Text = "0";
                }

            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex,"Error while calculation");
            }

        }
        private void SaveLoad()
        {
            try
            {
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        EDMX.loading loading = GetLoading();
                        if (loading != null)
                        {
                            loadingBAL.SaveLoad(loading);
                            General.ShowMessage(General.EnumMessageTypes.Success, "Loading Succefully saved");
                            ButtonActive(EnumFormEvents.Cancel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Something went wrong, Unable to save");
                return;
            }
        }

        private EDMX.loading GetLoading()
        {
            return new EDMX.loading
            {
                act_stock=General.ParseDecimal(txtActualStock.Text),
                balance=General.ParseDecimal(txtBalance.Text),
                damage =General.ParseDecimal(txtDamage.Text),
                empty = General.ParseDecimal(txtEmpty.Text),
                extra = General.ParseDecimal(txtExtra.Text),
                new_load = General.ParseDecimal(txtNewLoad.Text),
                new_stock = General.ParseDecimal(txtNewStock.Text),
                old_stock = General.ParseDecimal(txtOldStock.Text),
                offload = General.ParseDecimal(txtOffload.Text),
                @short = General.ParseDecimal(txtShort.Text),
                delivery_id=GetDeliveryId(),
                total_load=General.ParseDecimal(txtTotalLoading.Text),

                employee_id=General.GetComboBoxSelectedValue(cmbEmployee),
                helper=txtHelper.Text,
                item_id=General.GetComboBoxSelectedValue(cmbProductName),
                load_date=General.ConvertDateTimeServerFormat(dtpDeliveryDate.Value),
                load_id=0,
                remarks=txtRemarks.Text,
                server_time=DateTime.Now,
                status=1,
                vehicle=txtVehicle.Text
            };
        }
        private bool Validation()
        {
            bool resp = false;

            if (GetDeliveryId() <= 0)
            {
                General.ShowMessageConfirm("No delivery id generated for the given date. ");
            }
            if (General.GetComboBoxSelectedValue(cmbEmployee) <= 0 || General.GetComboBoxSelectedValue(cmbProductName) <= 0)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Employee or Item missing");
            }
            else if (string.IsNullOrEmpty(txtNewLoad.Text))
            {
                General.ShowMessage(General.EnumMessageTypes.Warning, "Enter load qty"); 
            }

            else resp = true;


            return resp;
                 
        }
        private int GetDeliveryId()
        {
            try
            {
                DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                return deliveryDAL.GetDeliveryId(General.ConvertDateServerFormat(dtpDeliveryDate.Value), General.GetComboBoxSelectedValue(cmbEmployee));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetAllLoading()
        {
            try
            {
                int deliverId = GetDeliveryId();
                if (deliverId > 0)
                {
                    General.ClearGrid(dgDelivery);
                    DAL.DAL.LoadDAL loadDAL = new DAL.DAL.LoadDAL();
                    List<EDMX.loading> listLoading = loadDAL.GetLoading(deliverId);
                    if (listLoading != null && listLoading.Count > 0)
                    {
                        foreach (EDMX.loading ld in listLoading)
                        {
                            dgDelivery.Rows.Add(ld.load_id, General.ConvertDateTimeAppFormat(ld.load_date),ld.item_id, ld.item.item_name, ld.old_stock, ld.empty, ld.damage, ld.balance, ld.new_load, ld.@short, ld.extra, ld.new_stock, ld.offload, ld.act_stock,"Remove");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load");
            }
        }
        int cItemId = 0;
        private void GetPreviousBalance()
        {
            try
            {
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int empId = General.GetComboBoxSelectedValue(cmbEmployee);
                int itemId = General.GetComboBoxSelectedValue(cmbProductName);
                int deliveryId = GetDeliveryId();
                if (empId > 0 && itemId > 0 && deliveryId>0)
                {

                    lblPrvBalance.Text = "";
                    decimal totalLoading = 0,totalEmpty=0,totalShort=0,totalExtra=0;
                    bool firstLoad = false;

                    decimal prvBalance = deliveryBAL.GetPreviousDayBalanceandLoading(General.ConvertDateServerFormat(dtpDeliveryDate.Value), deliveryId,empId, itemId,ref totalLoading,ref totalEmpty,ref totalShort,ref totalExtra,ref firstLoad);
                    txtOldStock.Text = prvBalance.ToString();
                    decimal sold =firstLoad?0: deliveryBAL.GetSoldQuantity(deliveryId, itemId);
                    txtTotalLoading.Text = totalLoading.ToString();
                    txtTotalEmpty.Text = totalEmpty.ToString();
                    txtTotalShort.Text = totalShort.ToString();
                    txtTotalExtra.Text = totalExtra.ToString();
                    prvBalance = deliveryBAL.GetPreviousDayBalance(General.ConvertDateServerFormat(dtpDeliveryDate.Value), empId, itemId);
                    lblPrvBalance.Text = prvBalance.ToString();
                    txtBalance.Text =sold>0?(prvBalance - sold).ToString():totalLoading.ToString();

                    if (setting != null && setting.default_item_id > 0 && setting.default_item_id != itemId)
                    {
                       
                        if(totalLoading>0)
                            txtBalance.Text = (totalLoading - sold).ToString();
                    }
                        
                  

                    //Short and extra only for 5gallon
                    if (setting != null && setting.default_item_id > 0 && setting.default_item_id != itemId)
                    {
                        txtTotalShort.Text = "0";
                        txtTotalExtra.Text = "0";
                        txtShort.Text = "0";
                        txtExtra.Text = "0";
                    }
                }
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
                DAL.DAL.LoadDAL loadDAL = new DAL.DAL.LoadDAL();
                int deliveryId = GetDeliveryId();
                if (deliveryId > 0)
                {
                    List<EDMX.loading> listLoading = loadDAL.GetLoading(deliveryId);
                    loadingBAL.PrintLoadingSlip(listLoading);
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unabale to print");
            }
        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);
            }
        }

        private void txtOldStock_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void dtpDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetAllLoading();
                GetPreviousBalance();
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Something went wrong");
            }
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetPreviousBalance();
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Something went wrong");
            }
        }

        private void linkReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadingReport loadingReport = new LoadingReport();
            loadingReport.ShowDialog();
        }

        private void dgDelivery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgDelivery.ColumnCount - 1)
            {
                try
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        int itemId = Convert.ToInt32(dgDelivery["clmItemId", e.RowIndex].Value);
                        int deliveryId = GetDeliveryId();
                        int loadId = Convert.ToInt32(dgDelivery["clmLoadId", e.RowIndex].Value);
                        if (deliveryId > 0 && loadId > 0 && itemId > 0)
                        {
                            int result = loadingBAL.DeleteLoad(loadId, deliveryId, itemId);
                            General.ShowMessage(General.EnumMessageTypes.Success, $"{result} loading deleted");
                            GetAllLoading();

                        }
                    }
                }
                catch (Exception ex)
                {
                    General.LogExceptionWithShowError(ex, "Unable to remove");
                }
            }
        }
    }
    class LoadManageButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnSave { get; set; }

    }
}
