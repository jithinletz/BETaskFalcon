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
    public partial class ProductionForm : Form
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
        ProductionButtonCollection button;
        public List<EDMX.production_mapping> listProducts = new List<EDMX.production_mapping>();
        public List<EDMX.production> listProduction = new List<EDMX.production>();
        public ProductionForm()
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
                    LoadProduction();
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
                    General.ClearGrid(gridProducts);
                   // _mappingId = 0;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                   SaveProduction();
                    break;
                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContent.Enabled = true;
                    General.ClearGrid(gridProducts);
                    if (button.BtnNew.Text == "&Edit")
                    {
                        button.BtnNew.Enabled = false;
                       // button.BtnSave.Enabled = true;
                    }
                    //txtName.Focus();
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                   // button.BtnNew.Text = "&Edit";
                    //button.BtnSave.Text = "&Update";
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

        private void LoadAllProducts()
        {
            try
            {
                ProductionBAL prodBAL = new ProductionBAL();
                listProducts = prodBAL.GetMappedProducts();

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
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)gridProducts.Columns["clmItemName"];
                comboItem.HeaderText = "Select Products";
                foreach (EDMX.production_mapping raw in listProducts)
                {
                    comboItem.Items.Add(raw.item.item_name);
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
                foreach (DataGridViewRow dr in gridProducts.Rows)
                {
                    if (dr.Cells[0].Value != null && !String.IsNullOrEmpty(dr.Cells[0].Value.ToString()))
                    {
                        _cnt++;
                        if (dr.Cells[5].Value != null && !String.IsNullOrEmpty(dr.Cells[4].Value.ToString()))
                        {
                            _value += General.ParseDecimal(dr.Cells[4].Value.ToString());
                        }
                    }
                }

                txtTotalProducts.Text = _cnt.ToString();
                txtTotalValue.Text = General.TruncateDecimalPlaces(_value).ToString();
            }
            catch (Exception ee)
            {
                //General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private bool Validation()
        {
            bool resp = false;
            if (gridProducts.Rows.Count <= 0) resp = false; else resp = true;
            if (gridProducts.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in gridProducts.Rows)
                {
                    int _productionId = 0;
                    int.TryParse(dr.Cells["clmProductionId"].Value.ToString(),out _productionId);
                    if (_productionId > 0)
                    {
                        resp = true;
                        break;
                    }
                }
            }
            return resp;
        }
        private void SaveProduction()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (General.CheckFinancialDate(dtpDate.Value))
                    {
                        ProductionBAL productionBAL = new ProductionBAL();
                        int cnt = 0;
                        foreach (DataGridViewRow dr in gridProducts.Rows)
                        {
                            int _productionId = 0;

                            if (dr.Cells["clmItemId"].Value != null)

                            {
                                int.TryParse(dr.Cells["clmProductionId"].Value.ToString(), out _productionId);
                                if (_productionId == 0)
                                {
                                    decimal _qty = 0, _cost = 0;
                                    int _itemId = 0;
                                    decimal.TryParse(dr.Cells["clmQty"].Value.ToString(), out _qty);
                                    decimal.TryParse(dr.Cells["clmCost"].Value.ToString(), out _cost);
                                    int.TryParse(dr.Cells["clmItemId"].Value.ToString(), out _itemId);
                                    EDMX.production production = new EDMX.production()
                                    {
                                        production_id = 0,
                                        item_id = _itemId,
                                        production_date = General.ConvertDateServerFormat(dtpDate.Value),
                                        qty = _qty,
                                        cost = _cost,
                                        remarks = txtRemarks.Text,
                                        status = 1

                                    };

                                    List<EDMX.production_mapping_rowmaterial> listRawmaterial = productionBAL.GetMappedDetails(_itemId);
                                    List<EDMX.production_rawmaterial> listProductRawmaterials = new List<EDMX.production_rawmaterial>();
                                    if (listProducts.Count > 0)
                                    {
                                        foreach (EDMX.production_mapping_rowmaterial raw in listRawmaterial)
                                        {
                                            listProductRawmaterials.Add(new EDMX.production_rawmaterial()
                                            {
                                                item_id = raw.item_id,
                                                qty = General.TruncateDecimalPlaces(_qty * (raw.qty/raw.production_mapping.qty)),
                                                item_value = General.TruncateDecimalPlaces(General.TruncateDecimalPlaces(_qty) * (raw.item.cost/ raw.production_mapping.qty)),
                                                status = 1,
                                            });
                                        }
                                    }
                                    productionBAL.SaveProduction(production, listProductRawmaterials);
                                    cnt++;
                                    General.Action($"New Production  for {dr.Cells[1].Value.ToString()} Qty {_qty}");
                                }
                            }
                        }

                        General.ShowMessage(General.EnumMessageTypes.Success, $" {cnt} Production  Saved successfully !");
                        ButtonActive(EnumFormEvents.Cancel);
                        LoadProduction();
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                if (ee.InnerException!=null && ee.InnerException.ToString().Contains("Violation of UNIQUE KEY"))
                    General.ShowMessage(General.EnumMessageTypes.Error, "Product already saved . Please update instead of saving New Mapping ");
                else
                    General.ShowMessage(General.EnumMessageTypes.Error);
            }

        }
        private void LoadProduction()
        {
            try
            {
                General.ClearGrid(gridDates);
                ProductionBAL productionBAL = new ProductionBAL();
                listProduction = productionBAL.GetProduction();
                var dates = listProduction.Select(x => x.production_date).Distinct().OrderByDescending(x => x.Date).ToList().Take(30);
                foreach (DateTime dt in dates)
                {
                    gridDates.Rows.Add(dt.ToString("dd/MM/yyyy"));
                    
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void LoadProduction_ByDate(DateTime prodDate)
        {
            try
            {
                General.ClearGrid(gridProducts);
                ProductionBAL productionBAL = new ProductionBAL();
                listProduction = productionBAL.GetProduction_ByDate(prodDate);
                if (listProduction.Count > 0)
                {
                    foreach (EDMX.production prod in listProduction)
                    {
                        gridProducts.Rows.Add(prod.item_id, prod.item.item_name, prod.item.uom_setting.uom_name, prod.qty, prod.cost, General.TruncateDecimalPlaces(prod.cost / prod.qty), "Rawmaterial", "Remove",prod.production_id, "Delete");
                        gridProducts.Rows[gridProducts.Rows.Count-2].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    CalcSummary();
                    ButtonActive(EnumFormEvents.Update);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        #region gridEvents
        private void gridProducts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            gridProducts.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gridProducts_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                if (gridProducts.CurrentRow.Index >= 0 && (gridProducts.CurrentCell.ColumnIndex ==3|| gridProducts.CurrentCell.ColumnIndex == 4))
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

        

        private void gridProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Item Selection
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)gridProducts.Rows[e.RowIndex].Cells[1];
                    if (cb.Value != null)
                    {
                        string _itemName = gridProducts[1, e.RowIndex].Value.ToString();
                        if (!String.IsNullOrEmpty(_itemName))
                        {
                            var _item = listProducts.Where(x => x.item.item_name == _itemName).FirstOrDefault();
                            gridProducts["clmItemId", e.RowIndex].Value = _item.item_id;
                            gridProducts["clmPacking", e.RowIndex].Value = _item.item.uom_setting.uom_name;
                            gridProducts["clmQty", e.RowIndex].Value = "1";
                            gridProducts["clmCost", e.RowIndex].Value = General.TruncateDecimalPlaces(_item.total_value/_item.qty);
                            gridProducts["clmUnitCost", e.RowIndex].Value = General.TruncateDecimalPlaces(_item.total_value / _item.qty);
                            gridProducts["clmViewRaw", e.RowIndex].Value = "View Rawmaterial";
                            gridProducts["clmDelete", e.RowIndex].Value = "Remove";
                            gridProducts["clmProductionId", e.RowIndex].Value = "0";


                            CalcSummary();
                        }

                    }
                

                }
                //Qty Changed
                else if (e.RowIndex >= 0 && e.ColumnIndex == 3)
                {
                    if (gridProducts["clmUnitCost", e.RowIndex].Value != null)
                    {
                        decimal _qty = gridProducts["clmQty", e.RowIndex].Value != null ? General.ParseDecimal(gridProducts["clmQty", e.RowIndex].Value.ToString()) : 0;
                        decimal _cost = General.ParseDecimal(gridProducts["clmUnitCost", e.RowIndex].Value.ToString());
                        decimal _value = General.TruncateDecimalPlaces(_qty * _cost);
                        gridProducts["clmCost", e.RowIndex].Value = _value.ToString();
                        CalcSummary();
                    }
                }

                gridProducts.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Something went wrong");
            }

        }
        private void gridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (e.ColumnIndex == 7)
                    {
                        if (gridProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Yellow)
                        {
                            General.ShowMessage(General.EnumMessageTypes.Warning, "Cannot remove saved details . Please press 'Delete' to delete the production details");
                        }
                        else
                            gridProducts.Rows.RemoveAt(e.RowIndex);
                    }
                    else if (e.ColumnIndex == 6)//Show Rawmaterils
                    {
                        string _productName = gridProducts["clmItemName", e.RowIndex].Value.ToString();
                        int _productionId = 0;
                        int.TryParse(gridProducts["clmProductionId", e.RowIndex].Value.ToString(), out _productionId);
                        int _productId = 0;
                        int.TryParse(gridProducts["clmItemId", e.RowIndex].Value.ToString(), out _productId);

                        if (_productId > 0 && _productName != string.Empty)
                        {
                            if (_productionId == 0)
                            {
                                ProductMapForm productMapForm = new ProductMapForm(_productId, _productName, 0);
                                productMapForm.ShowDialog();
                            }
                            else
                            {
                                ProductMapForm productMapForm = new ProductMapForm(_productId, _productName, _productionId);
                                productMapForm.ShowDialog();
                            }
                        }
                    }
                    else if (e.ColumnIndex ==9)
                    {
                        if (gridProducts[9, e.RowIndex].Value.ToString() == "Delete")
                        {
                            int _productionId = 0;
                            int.TryParse(gridProducts["clmProductionId", e.RowIndex].Value.ToString(),out _productionId);
                            if (_productionId > 0)
                            {
                                if (General.ShowMessageConfirm("Are you sure want to delete this production") == DialogResult.Yes)
                                {
                                    ProductionBAL productionBAL = new ProductionBAL();
                                    productionBAL.DeleteProduction(_productionId);
                                    General.Action($"Production delete id={_productionId} , product={gridProducts["clmItemName", e.RowIndex].Value} , qty={gridProducts["clmqty", e.RowIndex].Value} ");
                                    General.ShowMessage(General.EnumMessageTypes.Success, "Successfully deleted");
                                    gridProducts.Rows.RemoveAt(e.RowIndex);
                                }
                            }
                        }
                    }
                }
             
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Something went wrong");
            }
        }
        #endregion gridEvents
        private void ProductionForm_Load(object sender, EventArgs e)
        {
            button = new ProductionButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            LoadAllProducts();
            UpdateGridAutoComplete_Item();
            SetScreenSize(sender,e);
        }
        private void SetScreenSize(object sender, EventArgs e)
        {
            // Get the current screen's working area (exclude taskbar and other docked elements)
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;

            // Set the form's size to 70% of the screen's width and height
            this.Width = (int)(screen.Width * 0.7);
            this.Height = (int)(screen.Height * 0.7);
        }
        private void gridDates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    string date =DateTime.Parse( gridDates[0, e.RowIndex].Value.ToString()).ToString("dd/MM/yyyy");
                    LoadProduction_ByDate(Convert.ToDateTime(DateTime.Parse(date)));
                }
            }
            catch (Exception ee)
            { }
        }

        private void dtpDateFilter_ValueChanged(object sender, EventArgs e)
        {
            try
            {
               
                    LoadProduction_ByDate(General.ConvertDateServerFormat(dtpDateFilter.Value));
            }
            catch (Exception ee)
            { }
        }

        private void gridProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    if (gridDates.Rows.Count > 1)
                    {
                        int row = gridProducts.CurrentRow.Index;
                        if (gridProducts[0, row].Value != null)
                        {
                            gridProducts.Rows.RemoveAt(row);
                            CalcSummary();
                        }
                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
            if (e.KeyData == Keys.F3)
            {
                Views.ProductMapForm productMapForm = new ProductMapForm();
                productMapForm.ShowDialog();
                LoadAllProducts();
                UpdateGridAutoComplete_Item();

            }
        }
    }
    class ProductionButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
