using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;


namespace BETask.Views
{
    public partial class DeliveryLoadReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        public int saleId = 0;
        SaleBAL saleBAL = new SaleBAL();
        BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
        List<EDMX.employee> _lstEmployee = null;
        DeliveryLoadReportButtonCollection button;
        List<EDMX.item> listItem = new List<EDMX.item>();
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        public int mDeliveryId = 0, empId = 0, itemId = 0;
        public DeliveryLoadReportForm()
        {
            InitializeComponent();
        }

        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    ResetForms();
                    GetAllEmployees();
                    LoadItem();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbEmployee.Text = string.Empty;
                    dgDelivery.Enabled = false;
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
            else if (sender == button.BtnSave)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }
        private void DeliveryLoadReportForm_Load(object sender, EventArgs e)
        {
            if (mDeliveryId == 0)
            { ResetForms(); }
            button = new DeliveryLoadReportButtonCollection()
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSave = btnSearch
            };
            LoadItem();
            Application.DoEvents();
            General.ClearGrid(dgDelivery);
            GetAllEmployees();
            LoadItem();
           
           

        }
        public void ResetForms()
        {
            try
            {
                General.ClearTextBoxes(this);
                General.ClearGrid(dgDelivery);
                // dgDelivery.Enabled = true;                
                cmbEmployee.SelectedIndex = -1;

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
                cmbEmployee.Items.Clear();
                _lstEmployee = employeeBAL.GetAllEmployee();
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
                cmbItem.Items.Clear();
                listItem = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listItem)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {

                        Text = String.Format("{0}", item.item_name),
                        Value = item.item_id
                    };
                    cmbItem.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void UpdateGridAutoComplete_Item()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgDelivery.Columns["clmItemName"];
                comboItem.HeaderText = "Select Item";

                foreach (EDMX.item raw in listItem)
                {
                    comboItem.Items.Add(raw.item_name);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private string ValidateForm()
        {
            string errorMsg = string.Empty;
            if (dtpDateFrom.Text == string.Empty)
                errorMsg = "Please enter delivery from date";
            if (dtpDateTo.Text == string.Empty)
                errorMsg = "Please enter delivery to date";
            else if (cmbEmployee.Text == string.Empty)
                errorMsg = "Please select Delivery Staff";
            return errorMsg;
        }
        private void Search()
        {
            //ValidateForm();
            
                dgDelivery.Rows.Clear();
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                decimal prvbal = 0;
                decimal balance = 0;
                decimal totalQty = 0;
                decimal soldQty = 0;
                empId = 0; itemId = 0;
                if (cmbEmployee.SelectedItem != null && empId <= 0)
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                if (cmbItem.SelectedItem != null && itemId <= 0)
                {
                    Object selectedItem = cmbItem.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                List<BETask.DAL.Model.DeliveryLoadReportModel> listDeliveryIitemSummary = deliveryBAL.GetDeliveryLoadDetails(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), empId,itemId);
                if (listDeliveryIitemSummary != null && listDeliveryIitemSummary.Count > 0)
                {

                    foreach (BETask.DAL.Model.DeliveryLoadReportModel data in listDeliveryIitemSummary)
                    {                    
                        dgDelivery.Rows.Add(data.ItemId,General.ConvertDateAppFormat(data.Date), data.ItemName, data.PreviousBalance, data.LoadQty,data.TotalQty, data.SoldQty,data.DamageQty , data.BalanceQty, data.Remarks);
                    }

                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No data found", "info");
                }
            
        }

        private void Print()
        {
            try
            {
                empId = 0;
                itemId = 0;
                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }

                if (cmbItem.SelectedItem != null && itemId <= 0)
                {
                    Object selectedItem = cmbItem.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }

                deliveryBAL.PrintDeliveryLoadReport(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), empId,itemId);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }

    }
  

    class DeliveryLoadReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnSave { get; set; }

    }
}
