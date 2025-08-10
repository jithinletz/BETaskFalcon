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
    public partial class ProductMapForm : Form
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
        ProductMapButtonCollection button;
        List<EDMX.item> listRawmaterial = new List<EDMX.item>();
        List<EDMX.item> listProducts = new List<EDMX.item>();
        int _mappingId = 0,_productId=0,_productionId=0;
        string _productName = string.Empty;
      
        public ProductMapForm()
        {
            InitializeComponent();
        }
        public ProductMapForm(int productId,string productName,int productionId)
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
                if (sender == cmbProductName)
                    gridRawmeterial.Focus();
                else
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
                    SaveProductMapping();
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
                listProducts = itemBAL.GetAllItem_Sellable();
                foreach (EDMX.item item in listProducts)
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

        private void LoadAllRawmaterial()
        {
            try
            {
                ItemBAL itemBAL = new ItemBAL();
                listRawmaterial = itemBAL.GetAllItem_Rawmaterial();

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void UpdateGridAutoComplete_Item()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)gridRawmeterial.Columns["clmItemName"];
                comboItem.HeaderText = "Select Rawmaterials";
                foreach (EDMX.item raw in listRawmaterial)
                {
                    // col.Add(raw.item_name);


                    comboItem.Items.Add(raw.item_name);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
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
                    if (dr.Cells[0].Value != null && !String.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                    {
                        _cnt++;
                        if (dr.Cells[5].Value != null && !String.IsNullOrEmpty(dr.Cells[5].Value.ToString()))
                        {
                            _value += General.ParseDecimal(dr.Cells[5].Value.ToString());
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

        private void gridRawmeterial_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
                }
                e.Control.KeyPress -= new KeyPressEventHandler(General.TxtOnlyDecimal);
                if (gridRawmeterial.CurrentRow.Index >= 0 && gridRawmeterial.CurrentCell.ColumnIndex <=5/*(gridRawmeterial.CurrentCell.ColumnIndex == 3 || gridRawmeterial.CurrentCell.ColumnIndex == 5)*/)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
            }
        }

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
                else if (e.RowIndex >= 0 && e.ColumnIndex >= 3)
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
            if (cmbProductName.SelectedIndex < 0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select product"); cmbProductName.Focus(); resp = false; } else resp = true;
            if(gridRawmeterial.Rows.Count<=0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select rawmaterials"); gridRawmeterial.Focus(); resp = false; } else resp = true;
            if(General.IsTextboxEmpty(txtTotalValue)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please select rawmaterials"); gridRawmeterial.Focus(); resp = false; } else resp = true;
            return resp;
        }
        private void SaveProductMapping()
        {
            try
            {
                if (Validation())
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        string _itemName = cmbProductName.Text;
                        var item = listProducts.Where(x => x.item_name == _itemName).FirstOrDefault();
                        int _itemId = item.item_id;
                        EDMX.production_mapping product = new EDMX.production_mapping()
                        {
                            item_id = _itemId,
                            qty = General.ParseDecimal(Convert.ToString(txtQty.Text)),
                            mapping_id = _mappingId,
                            total_rawmaterial= General.ParseInt(txtTotalRawmaterial.Text),
                            status = 1,
                            total_value = General.ParseDecimal(txtTotalValue.Text)
                        };
                        //Filling rawmaterial
                        List<EDMX.production_mapping_rowmaterial> listRawmaterial = new List<EDMX.production_mapping_rowmaterial>();
                        foreach (DataGridViewRow dr in gridRawmeterial.Rows)
                        {
                            if (dr != null)
                            {
                                if (dr.Cells["clmItemId"].Value != null && dr.Cells["clmValue"].Value != null && dr.Cells["clmQty"].Value != null)
                                {
                                    listRawmaterial.Add(new EDMX.production_mapping_rowmaterial()
                                    {
                                        item_id = Convert.ToInt32(dr.Cells["clmItemId"].Value),
                                        item_value = General.ParseDecimal(dr.Cells["clmValue"].Value.ToString()),
                                        mapping_id = 0,
                                        mapping_rowmaterial_id = 0,
                                        qty = General.ParseDecimal(dr.Cells["clmQty"].Value.ToString()),
                                        status = 1,
                                    });
                                }
                            }

                        }
                        ProductionBAL productMapBAL = new ProductionBAL();
                        productMapBAL.SaveProductionMapping(product, listRawmaterial);
                        General.Action($"New Product Mapping Saved for {cmbProductName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Product mapping Saved successfully !!");
                        ButtonActive(EnumFormEvents.Cancel);
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

        private bool GetMappedDetails(int item_id)
        {
            bool resp = false;
            try
            {
                General.ClearGrid(gridRawmeterial);
                ProductionBAL productMapBAL = new ProductionBAL();
                List<EDMX.production_mapping_rowmaterial> listRawmaterial = productMapBAL.GetMappedDetails(item_id);
                txtQty.Text = "1";
                if (listRawmaterial != null && listRawmaterial.Count() > 0)
                    txtQty.Text = listRawmaterial[0].production_mapping.qty.ToString();
                if (listRawmaterial != null && listRawmaterial.Count > 0)
                {
                    _mappingId = listRawmaterial[0].production_mapping.mapping_id;
                    
                    foreach (EDMX.production_mapping_rowmaterial raw in listRawmaterial)
                    {
                        gridRawmeterial.Rows.Add(raw.item_id,raw.item.item_name,raw.item.uom_setting.uom_name,raw.qty,raw.item.cost,raw.item_value,"Remove");
                    }
                    CalcSummary();
                    ButtonActive(EnumFormEvents.Update);
                    resp = true;
                }
                else
                {
                    ButtonActive(EnumFormEvents.Other);
                }
            }
            catch { throw; }
            return resp;
        }

        private bool GetMappedDetails_production(int productionId)
        {
            bool resp = false;
            try
            {
                General.ClearGrid(gridRawmeterial);
                ProductionBAL productMapBAL = new ProductionBAL();
                List<EDMX.production_rawmaterial> listRawmaterial = productMapBAL.GetProductionRawmaterial(productionId);
                if (listRawmaterial != null && listRawmaterial.Count > 0)
                {
                   
                    foreach (EDMX.production_rawmaterial raw in listRawmaterial)
                    {
                        gridRawmeterial.Rows.Add(raw.item_id, raw.item.item_name, raw.item.uom_setting.uom_name, raw.qty, raw.item.cost, raw.item_value, "Remove");
                    }
                    CalcSummary();
                    resp = true;
                }
                else
                {
                    ButtonActive(EnumFormEvents.Other);
                }
            }
            catch { throw; }
            return resp;
        }

        #endregion SaveUpdateSelect

        private void ProductMapForm_Load(object sender, EventArgs e)
        {
            button = new ProductMapButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            LoadAllRawmaterial();
            UpdateGridAutoComplete_Item();
            LoadProducts();
            if (_productId > 0)
            {
                cmbProductName.Text = _productName;
                if(_productionId==0)
                GetMappedDetails(this._productId);
            }
        }
        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProductName.SelectedIndex >= 0)
                {
                    General.ClearTextBoxes(this);
                    string _itemName = cmbProductName.Text;
                    Object selectedItem = cmbProductName.SelectedItem;
                    int _pid = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                   // if (!String.IsNullOrEmpty(_itemName))
                   if(_pid>=0)
                    {
                        var item = listProducts.Where(x => x.item_id == _pid).FirstOrDefault();
                        string _packing = item.uom_setting.uom_name;
                        txtPacking.Text = _packing;
                        if (this._productionId == 0)
                            GetMappedDetails(item.item_id);
                        else
                            GetMappedDetails_production(_productionId);
                    }
                }
               
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void gridRawmeterial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 6)
                {
                    gridRawmeterial.Rows.RemoveAt(e.RowIndex);
                    CalcSummary();
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
    }

    class ProductMapButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
