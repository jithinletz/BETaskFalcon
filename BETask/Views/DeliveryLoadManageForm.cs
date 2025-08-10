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
    public partial class DeliveryLoadManageForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Save,
            Search,
            //Print
        }
        public int saleId = 0;
        SaleBAL saleBAL = new SaleBAL();
        BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
        List<EDMX.employee> _lstEmployee = null;
        DeliveryLoadManageButtonCollection button;
        List<EDMX.item> listItem = new List<EDMX.item>();
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        public int mDeliveryId = 0,empId=0;
        public DeliveryLoadManageForm()
        {
            InitializeComponent();
        }
        public DeliveryLoadManageForm(int _deliveryId,int _empId,DateTime date)
        {
            
            mDeliveryId = _deliveryId;
            InitializeComponent();
            LoadItem();
            GetAllEmployees(_empId);
            this.empId = _empId;
            txtDeliveryNo.Text =Convert.ToString( _deliveryId);
            dtpDate.Value = date;
            dgDelivery.Enabled = true;
            AutoLoadDelivery(_deliveryId, _empId);
            
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    ResetForms();
                    GetAllEmployees();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbEmployee.Text = string.Empty;
                    dgDelivery.Enabled = false;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;               
                case EnumFormEvents.Save:
                    SaveDeliveryItemSummary();                   
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
                ButtonActive(EnumFormEvents.Save);
            }
        }
        private void DeliveryLoadManageForm_Load(object sender, EventArgs e)
        {
            if (mDeliveryId == 0)
            { ResetForms(); }
            button = new DeliveryLoadManageButtonCollection()
            {

                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnSave = btnSave
            };
            LoadItem();
            Application.DoEvents();
            if (mDeliveryId == 0)
            {               
                General.ClearGrid(dgDelivery);
                GetAllEmployees();               
            }
            UpdateGridAutoComplete_Item();
            //dtpDate.MinDate = DateTime.Now.Date;

        }
        public void ResetForms()
        {
            try
            {
                General.ClearTextBoxes(this);
                General.ClearGrid(dgDelivery);
                // dgDelivery.Enabled = true;
                txtDeliveryNo.Focus();
                cmbEmployee.SelectedIndex = -1;
                txtDeliveryNo.Text = "0";

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllEmployees(int _empId=0)
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
                if(_empId>=1)
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
                listItem = itemBAL.GetAllItem_Sellable();
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
            if (dtpDate.Text == string.Empty)
                errorMsg = "Please enter delivery date";
            else if (cmbEmployee.Text == string.Empty)
                errorMsg = "Please select Delivery Staff";

            return errorMsg;
        }
        private void LoadDeliveryDetails(int _deliveryId=0,int _empId=0)
        {
            try
            {
               
                List<EDMX.delivery_item_summary> listDeliveryIitemSummary = new List<EDMX.delivery_item_summary>();
                General.ClearGrid(dgDelivery);
                if (_deliveryId <= 0)
                {
                    //int empId = 0;
                    //int deliveryId = 0;
                    List<EDMX.delivery> listDelivery = new List<EDMX.delivery>();
                    if (!String.IsNullOrEmpty(cmbEmployee.Text))
                    {
                        Object selectedEmployee = cmbEmployee.SelectedItem;
                        _empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                    }
                
                    _deliveryId = deliveryBAL.GetDeliveryId(General.ConvertDateServerFormat(dtpDate.Value), _empId);
                }
                decimal prvbal = 0;
                decimal balance = 0;
                decimal totalQty = 0;
                decimal soldQty = 0;
              
                txtDeliveryNo.Text = Convert.ToString(_deliveryId);
                listDeliveryIitemSummary = deliveryBAL.GetDeliveryItemSummaryById(_deliveryId);
                if (listDeliveryIitemSummary != null && listDeliveryIitemSummary.Count > 0)
                {
                    BETask.DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                    List<EDMX.delivery_item_summary> listStockItems = deliveryDAL.GetDeliveryItemBalance(this.mDeliveryId, this.empId);
                    if (listStockItems != null && listStockItems.Count > 0)
                    {
                        foreach (EDMX.delivery_item_summary raw in listStockItems)
                        {
                            if (!listDeliveryIitemSummary.Any(x => x.item_id == raw.item_id))
                            {
                                listDeliveryIitemSummary.Add(raw);
                            }
                        }
                    }

                    DAL.DAL.LoadDAL loadDAL = new DAL.DAL.LoadDAL();
                    foreach (EDMX.delivery_item_summary data in listDeliveryIitemSummary)
                    {

                        prvbal = deliveryBAL.GetPreviousDayBalance(General.ConvertDateServerFormat(dtpDate.Value), _empId, Convert.ToInt32(data.item_id));
                        if (!string.IsNullOrEmpty(data.remarks) && data.remarks == "#")
                        {
                            if (data.qty > 0)
                            {
                                data.qty += -prvbal;
                            }
                        }
                        soldQty = deliveryBAL.GetSoldQuantity(_deliveryId, Convert.ToInt32(data.item_id));
                        totalQty = data.qty;
                        if (totalQty == 0 && prvbal > 0)
                            totalQty = prvbal;

                        balance = totalQty - soldQty - data.damage_qty; //data.balance_qty;
                        data.used_qty = soldQty;
                        data.balance_qty = balance;

                        decimal newLoad = data.qty - prvbal < 0 ? 0 : (data.qty - prvbal);

                        try
                        {
                            newLoad = loadDAL.GetNewLoad(_deliveryId, data.item_id);
                            if (newLoad < 0)
                                totalQty += newLoad;

                            if (prvbal > 0 && newLoad == 0 && prvbal != totalQty)
                            {
                                totalQty = prvbal;
                                balance = prvbal;
                            }
                        }
                        catch (Exception ex)
                        { }
                        if (data.balance_qty != 0)
                            dgDelivery.Rows.Add(data.item.item_id, data.item.item_name, prvbal, newLoad, totalQty, data.used_qty, balance, data.damage_qty, data.remarks);
                    }

                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No data found", "info");
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }

        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex >= 0) {
                dgDelivery.Enabled = true;
                AutoLoadDelivery();
            }
        }
        private void AutoLoadDelivery(int _deliveryId = 0,int _empId=0)
        {
            try
            {
                LoadDeliveryDetails(_deliveryId, _empId);
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                int empId = 0;
                int deliveryNo = 0;
                if (cmbEmployee.SelectedItem != null)
                {
                    Object selectedEmployee = cmbEmployee.SelectedItem;
                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                }
                deliveryNo = Convert.ToInt32(txtDeliveryNo.Text);
                EDMX.delivery delivery = deliveryBAL.GetDeliveryDetails(deliveryNo);
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        
       

        private void gridDelivery_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dgDelivery.Rows[e.RowIndex].IsNewRow)
            {
                if (dgDelivery.Rows[e.RowIndex].DefaultCellStyle.BackColor == System.Drawing.Color.Green)
                {
                    e.Cancel = true;
                }
            }
        }

        private void gridDelivery_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgDelivery.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridDelivery_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }

            //if (dgDelivery.CurrentRow.Index >= 0 && (dgDelivery.CurrentCell.ColumnIndex > 1) && (dgDelivery.CurrentCell.ColumnIndex <7))
            //{
            //    TextBox tb = e.Control as TextBox;
            //    if (tb != null)
            //    {
            //        tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
            //    }
            //}
        }

        private void gridDelivery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    if (dgDelivery.Rows.Count > 1)
                    {
                        int row = dgDelivery.CurrentRow.Index;
                        if (dgDelivery[0, row].Value != null)
                        {
                            dgDelivery.Rows.RemoveAt(row);

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

        private void dgDelivery_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {

                    if (e.ColumnIndex == 1)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgDelivery.Rows[e.RowIndex].Cells["clmItemName"];
                        if (cb.Value != null)
                        {
                            int empId = 0;
                            bool isNewItem = true;
                            decimal prvBalance = 0;
                            if (this.empId == 0)
                            {
                                if (!String.IsNullOrEmpty(cmbEmployee.Text))
                                {
                                    Object selectedEmployee = cmbEmployee.SelectedItem;
                                    empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;
                                }
                            }
                            else
                                empId = this.empId;

                            string _itemName = dgDelivery["clmItemName", e.RowIndex].Value.ToString();
                            foreach (DataGridViewRow dr in dgDelivery.Rows)
                            {
                                if (dr.Cells[0].Value != null)
                                {

                                    if (_itemName == dr.Cells["clmItemName"].Value.ToString())
                                    {
                                        isNewItem = false;
                                    }
                                }
                            }
                            if (!String.IsNullOrEmpty(_itemName) && isNewItem == true)
                            {
                                var _item = listItem.Where(x => x.item_name == _itemName).FirstOrDefault();

                                prvBalance = deliveryBAL.GetPreviousDayBalance(General.ConvertDateServerFormat(dtpDate.Value), empId, Convert.ToInt32(_item.item_id)); ;
                                dgDelivery["clmPreviousBalance", e.RowIndex].Value = prvBalance;
                                dgDelivery["clmItemId", e.RowIndex].Value = _item.item_id;
                            }
                            else
                            {
                                General.ShowMessage(General.EnumMessageTypes.Error, "Item already in the list");
                                int row = dgDelivery.CurrentRow.Index;
                                if (dgDelivery[1, row].Value != null)
                                {
                                    dgDelivery.Rows.RemoveAt(row);
                                }


                            }

                        }
                    }
                    else
                    {
                        try
                        {
                            decimal totalQty = Convert.ToDecimal(dgDelivery["clmPreviousBalance", e.RowIndex].Value) + Convert.ToDecimal(dgDelivery["clmLoaded", e.RowIndex].Value);
                            decimal damage = Convert.ToDecimal(dgDelivery["clmDamage", e.RowIndex].Value);
                            dgDelivery["clmTotalQty", e.RowIndex].Value = totalQty;
                            decimal balance = Convert.ToDecimal(dgDelivery["clmTotalQty", e.RowIndex].Value) - Convert.ToDecimal(dgDelivery["clmItemSold", e.RowIndex].Value);
                            balance = balance - damage;
                           
                            dgDelivery["clmBalance", e.RowIndex].Value = balance;
                        }
                        catch { }
                    }
                }

                dgDelivery.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                // General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void dgDelivery_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >1)//load
                {
                    if (dgDelivery["clmLoaded", e.RowIndex].Value != null)
                    {
                        decimal changedLoadQty = General.ParseDecimal(dgDelivery["clmLoaded", e.RowIndex].Value.ToString());
                        DataGridViewComboBoxCell cbItem = (DataGridViewComboBoxCell)dgDelivery.Rows[e.RowIndex].Cells["clmItemName"];
                        if (cbItem.Value != null)
                        {
                            decimal totalQty = Convert.ToDecimal(dgDelivery["clmPreviousBalance", e.RowIndex].Value) + Convert.ToDecimal(dgDelivery["clmLoaded", e.RowIndex].Value);
                            decimal balance = Convert.ToDecimal(dgDelivery["clmTotalQty", e.RowIndex].Value) - Convert.ToDecimal(dgDelivery["clmItemSold", e.RowIndex].Value);
                            dgDelivery["clmTotalQty", e.RowIndex].Value = totalQty;
                            dgDelivery["clmBalance", e.RowIndex].Value = balance;
                        }
                    }
                }

                //not used below code
                /*
                if (e.RowIndex >= 0 && e.ColumnIndex == 5)//sold
                {
                    DataGridViewComboBoxCell cbItem = (DataGridViewComboBoxCell)dgDelivery.Rows[e.RowIndex].Cells["clmItemName"];
                    if (cbItem.Value != null)
                    {
                        decimal totalQty = Convert.ToDecimal(dgDelivery["clmPreviousBalance", e.RowIndex].Value) + Convert.ToDecimal(dgDelivery["clmLoaded", e.RowIndex].Value);
                        decimal balance = Convert.ToDecimal(dgDelivery["clmTotalQty", e.RowIndex].Value) -Convert.ToDecimal(dgDelivery["clmItemSold", e.RowIndex].Value);
                        dgDelivery["clmTotalQty", e.RowIndex].Value = totalQty;
                        dgDelivery["clmBalance", e.RowIndex].Value = balance;
                    }
                }
                */
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

      
        private void SaveDeliveryItemSummary()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
                    string errorMessage = ValidateForm();
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                            List<EDMX.delivery_item_summary> deliveryItemSummary = GetAllDeliveryItemSummary();
                            int _savedId = deliveryBAL.SaveDeliveryItemSummary(deliveryItemSummary, Convert.ToInt32(txtDeliveryNo.Text));                            
                            General.ShowMessage(General.EnumMessageTypes.Success, "Loading successfully updated");
                            ButtonActive(EnumFormEvents.Close);
                            ResetForms();
                            Application.DoEvents();
                    }
                    else
                    {
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
        private List<EDMX.delivery_item_summary> GetAllDeliveryItemSummary()
        {
            List<EDMX.delivery_item_summary> deliveryItemSummary = new List<EDMX.delivery_item_summary>();
            try
            {
                foreach (DataGridViewRow dr in dgDelivery.Rows)
                {
                    if (dr.Cells["clmItemId"].Value != null)
                    {
                        deliveryItemSummary.Add(new EDMX.delivery_item_summary()
                        {
                            delivery_id = General.ParseInt(txtDeliveryNo.Text),
                            item_id = General.ParseInt(dr.Cells["clmItemId"].Value.ToString()),
                            qty = dr.Cells["clmTotalQty"].Value!=null? General.ParseDecimal(dr.Cells["clmTotalQty"].Value.ToString()):0,
                            balance_qty = dr.Cells["clmBalance"].Value!=null? General.ParseDecimal(dr.Cells["clmBalance"].Value.ToString()):0,
                            used_qty = dr.Cells["clmItemSold"].Value!=null? General.ParseDecimal(dr.Cells["clmItemSold"].Value.ToString()):0,
                            remarks = dr.Cells["clmRemarks"].Value!=null?(dr.Cells["clmRemarks"].Value.ToString()):"",
                            damage_qty= dr.Cells["clmDamage"].Value != null ? General.ParseDecimal(dr.Cells["clmDamage"].Value.ToString()) : 0,
                            status = 1
                        });
                    }
                }
            }
            catch(Exception ex)
            { throw; }
            return deliveryItemSummary;
        }
    }
  

    class DeliveryLoadManageButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnSave { get; set; }

    }
}
