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
    public partial class ItemDamageForm : Form
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
        ItemDamageButtonCollection button;
        List<EDMX.item> listRawmaterial = new List<EDMX.item>();
        List<EDMX.item> listProducts = new List<EDMX.item>();
        List<EDMX.employee> _lstEmployee = null;
        int _mappingId = 0,_productId=0,_productionId=0;
        string _productName = string.Empty;
      
        public ItemDamageForm()
        {
            InitializeComponent();
        }
        public ItemDamageForm(int productId,string productName,int productionId)
        {
            InitializeComponent();
            this._productId = productId;
            this._productName = productName;
            this._productionId = productionId;
            if (productionId>0)
            {
                panelButton.Visible = false;
               
            }
        }
        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                
                    General.NextFocus(sender, e);

            }
        }

        #endregion NextFocus
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
                    
                    //txtCusName.Focus();
                    //GetAllCustomers();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    //_customerId = 0;
                    pnlSaveContent.Enabled = false;
                    General.ClearTextBoxes(this);
                    General.ClearGrid(gridRawmeterial);
                    _mappingId = 0;
                    break;
                case EnumFormEvents.Close:
                   this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                   SaveItemDamage();
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
                    //txtName.Focus();
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


        private void LoadProducts()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetAllItem(-1);
                foreach (EDMX.item item in listProducts)
                {
                    if (item.description.ToString() != "DAMAGE")
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = $"{item.item_name} ({item.uom_setting.uom_name})",
                            Value = item.item_id
                        };

                        cmbProductName.Items.Add(_cmbItem);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void LoadProductsDamaged()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetAllItem(-1);
                foreach (EDMX.item item in listProducts)
                {
                    if (item.description.ToString() == "DAMAGE")
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = $"{item.item_name} ({item.uom_setting.uom_name})",
                            Value = item.item_id
                        };

                        cmbProductDamaged.Items.Add(_cmbItem);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
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

        private void CalcSummary()
        {
            try
            {
                int _cnt = 0;
                decimal _value = 0;
                foreach (DataGridViewRow dr in gridRawmeterial.Rows)
                {
                    if (dr.Cells[1].Value != null && !String.IsNullOrEmpty(dr.Cells[1].Value.ToString()))
                    {
                        _cnt++;
                        if (dr.Cells[6].Value != null && !String.IsNullOrEmpty(dr.Cells[6].Value.ToString()))
                        {
                            _value += General.ParseDecimal(dr.Cells[6].Value.ToString());
                        }
                    }
                }

                txtTotalRawmaterial.Text = _cnt.ToString();
                txtTotalValue.Text = General.TruncateDecimalPlaces(_value).ToString();
            }
            catch (Exception ee)
            {
                //General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        
        #region GridEvents

       

        private void gridRawmeterial_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

            gridRawmeterial.CommitEdit(DataGridViewDataErrorContexts.Commit);

            //gridRawmeterial.EndEdit();
        }

        private void gridRawmeterial_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Item Selection
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)gridRawmeterial.Rows[e.RowIndex].Cells[1];
                    if (cb.Value != null)
                    {
                        string _itemName = gridRawmeterial[1, e.RowIndex].Value.ToString();
                        if (!String.IsNullOrEmpty(_itemName))
                        {
                            var _item = listRawmaterial.Where(x => x.item_name == _itemName).FirstOrDefault();
                            gridRawmeterial["clmItemId", e.RowIndex].Value = _item.item_id;
                            gridRawmeterial["clmPacking", e.RowIndex].Value = _item.uom_setting.uom_name;
                            gridRawmeterial["clmQty", e.RowIndex].Value = "1";
                            gridRawmeterial["clmRate", e.RowIndex].Value = _item.cost;
                            gridRawmeterial["clmValue", e.RowIndex].Value = _item.cost;
                            gridRawmeterial["clmDelete", e.RowIndex].Value = "Remove";
                            CalcSummary();
                        }
                    }


                }
                else if (e.RowIndex >= 0 && e.ColumnIndex == 3)
                {
                    if (gridRawmeterial["clmRate", e.RowIndex].Value != null)
                    {
                        decimal _qty = gridRawmeterial["clmQty", e.RowIndex].Value != null ? General.ParseDecimal(gridRawmeterial["clmQty", e.RowIndex].Value.ToString()) : 0;
                        decimal _cost = General.ParseDecimal(gridRawmeterial["clmRate", e.RowIndex].Value.ToString());
                        decimal _value = General.TruncateDecimalPlaces(_qty * _cost);
                        gridRawmeterial["clmValue", e.RowIndex].Value = _value.ToString();
                        CalcSummary();
                    }
                }
               
                gridRawmeterial.Invalidate();
                

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Something went wrong");
            }
        }
        #endregion gridEvent

        #region SaveUpdateSelect

        private bool Validation()
        {
            bool resp = false;
            if (cmbEmployee.SelectedIndex < 0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select Employee"); cmbEmployee.Focus(); return false; } else resp = true;
             if (cmbProductName.SelectedIndex < 0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select Item"); cmbProductName.Focus(); return false; } else resp = true;
            if (cmbProductDamaged.SelectedIndex < 0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select Damaged Item"); cmbProductDamaged.Focus(); return false; } else resp = true;
            if (General.ParseDecimal(txtQty.Text)==0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Enter Item Qty"); txtQty.Focus(); return false; } else resp = true;

            //  if(gridRawmeterial.Rows.Count<=0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select rawmaterials"); gridRawmeterial.Focus(); resp = false; } else resp = true;
            // if(General.IsTextboxEmpty(txtTotalValue)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select rawmaterials"); gridRawmeterial.Focus(); resp = false; } else resp = true;
            return resp;
        }
        private void SaveItemDamage()
        {
            try
            {
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                       
                        int _itemId = 0;
                        Object selectedItem = cmbProductName.SelectedItem;
                        _itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;

                        int _itemIdDamaged = 0;
                        Object selectedItemDamage = cmbProductDamaged.SelectedItem;
                        _itemIdDamaged = (int)((BETask.Views.ComboboxItem)selectedItemDamage).Value;

                        int _empId = 0;
                        Object selectedEmployee = cmbEmployee.SelectedItem;
                        _empId = (int)((BETask.Views.ComboboxItem)selectedEmployee).Value;

                        if (_empId > 0 && _itemId > 0 && _itemIdDamaged>0)
                        {
                            ItemBAL itemBAL = new ItemBAL();
                            var it = itemBAL.GetItemDetails(_itemId);
                            EDMX.item_damage product = new EDMX.item_damage()
                            {
                                item_id = _itemId,
                                item_id_damaged=_itemIdDamaged,
                                qty = General.ParseDecimal(Convert.ToString(txtQty.Text)),
                                damage_date = General.ConvertDateServerFormat(dtpDate.Value),
                                cost = it.cost,
                                status = 1,
                                remarks = txtRemarks.Text,
                                employee_id = _empId
                            };

                            ProductionBAL productMapBAL = new ProductionBAL();
                            productMapBAL.SaveItemDamage(product);
                            General.Action($"New Item Damage Saved for {cmbProductName.Text} Qty {txtQty.Text}");
                            General.ShowMessage(General.EnumMessageTypes.Success, "New Item Damage Saved successfully !!");
                            ButtonActive(EnumFormEvents.Cancel);
                            GetDamage();
                        }
                       
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                if (ee.InnerException.ToString().Contains("Violation of UNIQUE KEY"))
                    General.ShowMessage(General.EnumMessageTypes.Error,"Product already saved . Please update instead of saving New Mapping ");
                else
                    General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

     

        private bool GetDamage()
        {
            bool resp = false;
            try
            {
                General.ClearGrid(gridRawmeterial);
                ProductionBAL productMapBAL = new ProductionBAL();
                List<EDMX.item_damage> listItem = productMapBAL.GetItemDamageByDate(General.ConvertDateServerFormat(dtpDate.Value));
                if (listItem != null && listItem.Count > 0)
                {
                   
                    foreach (EDMX.item_damage raw in listItem)
                    {
                        gridRawmeterial.Rows.Add($"{raw.employee.first_name} {raw.employee.last_name}",raw.item_id, raw.item.item_name, raw.item.uom_setting.uom_name, raw.qty, raw.item.cost, General.TruncateDecimalPlaces(raw.qty*raw.cost), "Remove",raw.item_damage_id);
                    }
                    CalcSummary();
                    resp = true;
                }
                else
                {
                   // ButtonActive(EnumFormEvents.Other);
                }
            }
            catch { throw; }
            return resp;
        }

        #endregion SaveUpdateSelect

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            GetDamage();
        }

      

        private void ProductMapForm_Load(object sender, EventArgs e)
        {
            button = new ItemDamageButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            LoadProducts();
            LoadProductsDamaged();
            GetAllEmployees();
            GetDamage();
        }
       

        private void gridRawmeterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 7)
                {
                    int damageId = Convert.ToInt32(gridRawmeterial["clmDamageId", e.RowIndex].Value);
                    if (damageId > 0)
                    {
                        if (General.ShowMessageConfirm("Are you sure want to remove this") == DialogResult.Yes)
                        {
                            ProductionBAL productionBAL = new ProductionBAL();
                            productionBAL.DamageReturn(damageId);
                            gridRawmeterial.Rows.RemoveAt(e.RowIndex);
                            CalcSummary();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
    }

    class ItemDamageButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
