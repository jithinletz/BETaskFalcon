using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.Common;
using BETask.BAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class ItemForm : Form
    {
        BAL.ItemBAL itemBAL = new ItemBAL();
        List<EDMX.tax_setting> listTax = new List<EDMX.tax_setting>();
        List<EDMX.uom_setting> listUOM = new List<EDMX.uom_setting>();
        List<EDMX.item> listItem = new List<EDMX.item>();
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Update,
            Cancel,
            Close,
            NodeClick
        }
        ButtonCollectionItem button;
        public ItemForm()
        {
            InitializeComponent();
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtOtherDetails)
                    txtCost.Focus();
                else if (sender == txtSaleRate)
                    txtOpeningStock.Focus();
                else if (sender == txtOpeningStock)
                    btnSave.Focus();
                else
                    General.NextFocus(sender, e);

            }
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            button = new ButtonCollectionItem
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            FillUOM();
            FillTax();
            FillLedger();

        }

        bool isEnterEventAdded = false;
        private void gridItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (!isEnterEventAdded)
                    {
                        gridItem.CellEnter += gridItem_CellClick;
                        isEnterEventAdded = true;
                    }
                    int itemId = 0;
                    int.TryParse(gridItem["clmItemId", e.RowIndex].Value.ToString(), out itemId);
                    FillItem(itemId);
                    ButtonActive(EnumFormEvents.Update);
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchItem();
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    pnlSaveConent.Enabled = false;
                    rdbAll.Checked = true;
                    gridItem.CellEnter -= gridItem_CellClick;
                    LoadItem(-1);
                    txtSearch.Focus();
                    break;

                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveConent.Enabled = true;
                    txtName.Focus();
                    if (btnNew.Text == "&New")
                    NextBarcode();
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    pnlSaveConent.Enabled = false;
                    General.ClearTextBoxes(this);
                    chkSellable.Checked = false;
                    btnNew.Text = "&New";
                    btnSave.Text = "&Save";
                    break;

                case EnumFormEvents.Close:
                   this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Save:
                    SaveItem();
                    break;

                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    btnNew.Text = "&Edit";
                    btnSave.Text = "&Update";
                    break;
                case EnumFormEvents.NodeClick:
                    btnCancel.Enabled = true;
                    break;
                default:
                    break;

            }
        }
        /// <summary>
        /// ButtonEvents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// ValidateDecimalPercision
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateDecimalPercision(object sender, EventArgs  e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
        /// <summary>
        /// Validation
        /// </summary>
        /// <returns></returns>
        private bool Validation()
        {
            bool resp = false;
            if (General.IsTextboxEmpty(txtName)) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter valid itemname");txtName.Focus() ; resp = false;goto H; } else resp = true;
            if (cmbUOM.SelectedIndex <0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select valid Packing"); resp = false;cmbUOM.Focus(); goto H; }  else resp = true;
            if (cmbTax.SelectedIndex <0) { General.ShowMessage(General.EnumMessageTypes.Warning, "Please Select valid Tax"); resp = false;cmbTax.Focus(); goto H; } else resp = true;
            decimal cost = 0, purchaseRate = 0, SalesRate = 0, OpeningStock = 0;
            decimal.TryParse(txtCost.Text, out cost);txtCost.Text = String.Format("{0:0.00}", cost);
            decimal.TryParse(txtPurchaseRate.Text, out purchaseRate); txtPurchaseRate.Text = String.Format("{0:0.00}", purchaseRate);
            decimal.TryParse(txtSaleRate.Text, out SalesRate); txtSaleRate.Text = String.Format("{0:0.00}", SalesRate);
            decimal.TryParse(txtOpeningStock.Text, out OpeningStock); txtOpeningStock.Text = String.Format("{0:0.00}", OpeningStock);
            H:
            return resp;
        }

        /// <summary>
        /// FillUOM
        /// </summary>
        private void FillUOM()
        {
            try
            {
                bool isItems = false;
                BAL.CommonSettingBAL common = new CommonSettingBAL();
               listUOM = common.GetAllUOM(-1);
                foreach (EDMX.uom_setting uom in listUOM)
                {
                    ComboboxItem item = new ComboboxItem()
                    {
                        Text = uom.uom_name,
                        Value = uom.uom_id
                    };
                    cmbUOM.Items.Add(item);
                    isItems = true;
                }
                cmbUOM.SelectedIndex = isItems ? 0 : -1;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        /// <summary>
        /// FillTax
        /// </summary>
        private void FillTax()
        {
            try
            {
                bool isItems = false;
                BAL.CommonSettingBAL common = new CommonSettingBAL();
                 listTax= common.GetAllTaxes(-1);
                foreach (EDMX.tax_setting uom in listTax)
                {
                    ComboboxItem item = new ComboboxItem()
                    {
                        Text = uom.tax_name,
                        Value = uom.tax_id
                    };
                    cmbTax.Items.Add(item);
                    isItems = true;
                }
                cmbTax.SelectedIndex = isItems ? 0 : -1;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void FillLedger()
        {
            try
            {
                bool isItems = false;
                BAL.AccountLedgerBAL common = new AccountLedgerBAL();
                List<Model.AccountLedgerModel> listLedger= common.GetAllAccountLedgerNonCustomer(-1);
                foreach (Model.AccountLedgerModel ld in listLedger)
                {
                    ComboboxItem item = new ComboboxItem()
                    {
                        Text = ld.Ledger_name,
                        Value = ld.Ledger_id
                    };
                    cmbSaleAccount.Items.Add(item);
                    cmbPurchaseAccount.Items.Add(item);
                  
                }
                cmbSaleAccount.SelectedIndex = isItems ? 0 : -1;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        /// <summary>
        /// LoadItem
        /// </summary>
        /// <param name="itemId"></param>
        private void LoadItem(int itemId)
        {
            try
            {
                General.ClearGrid(gridItem);
                listItem = itemBAL.GetAllItem(itemId);
                int i = 0;
                foreach (EDMX.item item in listItem)
                {
                    gridItem.Rows.Add(item.item_id, $" {item.item_name} - {item.uom_setting.uom_name}");
                    gridItem.Rows[i].HeaderCell.Value = item.barcode;
                    if (gridItem.Rows.Count > 50)
                        break;
                    i++;
                }
                lblResultCount.Text = gridItem.Rows.Count.ToString() + "search results";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        /// <summary>
        /// FillItem
        /// </summary>
        /// <param name="itemId"></param>
        private void FillItem(int itemId)
        {
            try
            {
                // EDMX.item item = listItem.Where(x=>x.item_id==itemId).FirstOrDefault();
                EDMX.item item = itemBAL.GetItemDetails(itemId);
                if (item != null)
                {
                    txtId.Text = item.item_id.ToString();
                    txtName.Text = item.item_name;
                    txtBarcode.Text = item.barcode;
                    txtBrand.Text = item.brand;
                    txtOtherDetails.Text = item.description;
                    cmbUOM.Text = item.uom_setting.uom_name;
                    cmbTax.Text = item.tax_setting.tax_name;
                    txtCost.Text = item.cost.ToString();
                    txtPurchaseRate.Text = item.purchase_rate.ToString();
                    txtSaleRate.Text = item.sale_rate.ToString();
                    txtOpeningStock.Text = item.opening_stock.ToString();
                    chkActive.Checked = item.status == 1 ? true : false;
                    chkRawmeterial.Checked = item.rawmeterial == 1 ? true : false;
                    chkSellable.Checked = item.sellable == 1 ? true : false;
                    chkStock.Checked = item.stockable == 1 ? true : false;
                    txtStock.Text = item.stockable == 1 ? item.Stock.ToString() : "";
                    txtGodownstock.Text = item.stockable == 1 ? item.godown_stock.ToString() : "";
                    chkAgreement.Checked = item.agreement_item == 1 ? true : false;
                    if (item.account_ledger1 != null)
                        cmbSaleAccount.Text = item.account_ledger1.ledger_name;
                    else
                    {
                        cmbSaleAccount.Text = "";
                        cmbSaleAccount.SelectedIndex = -1;
                    }
                    if (item.account_ledger != null)
                        cmbPurchaseAccount.Text = item.account_ledger.ledger_name;
                    else
                    {
                        cmbPurchaseAccount.Text = "";
                        cmbPurchaseAccount.SelectedIndex = -1;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void NextBarcode()
        {
            txtBarcode.Text = itemBAL.NextBarcode().ToString();
        }

        /// <summary>
        /// SaveItem
        /// </summary>
        private void SaveItem()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (Validation())
                {
                   

                        int uomId = listUOM.Where(x => x.uom_name == cmbUOM.Text).FirstOrDefault().uom_id;
                        int taxId = listTax.Where(x => x.tax_name == cmbTax.Text).FirstOrDefault().tax_id;
                        decimal _cost = General.ParseDecimal(txtCost.Text) == 0 ? General.ParseDecimal(txtPurchaseRate.Text) : General.ParseDecimal(txtCost.Text);
                        int saleLedger = General.GetComboBoxSelectedValue(cmbSaleAccount), purchaseLedger = General.GetComboBoxSelectedValue(cmbPurchaseAccount);

                       
                        if (!String.IsNullOrEmpty(cmbSaleAccount.Text) && cmbSaleAccount.SelectedItem!=null)
                        {
                            Object selectedRoute = cmbSaleAccount.SelectedItem;
                            saleLedger = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                        }


                        EDMX.item item = new EDMX.item()
                        {
                            item_id = General.IsTextboxEmptyMakeZero(txtId.Text),
                            item_name = txtName.Text.Trim(),
                            barcode = txtBarcode.Text.Trim(),
                            brand = txtBrand.Text.ToString(),
                            description = txtOtherDetails.Text.Trim(),
                            uom = uomId,//  General.IsTextboxEmptyMakeZero(cmbUOM.SelectedValue.ToString()),
                            tax = taxId,
                            cost = _cost,
                            purchase_rate = General.ParseDecimal(txtPurchaseRate.Text),
                            sale_rate = General.ParseDecimal(txtSaleRate.Text),
                            opening_stock = General.ParseDecimal(txtOpeningStock.Text),
                            rawmeterial = chkRawmeterial.Checked ? 1 : 2,
                            sellable = chkSellable.Checked ? 1 : 2,
                            stockable = chkStock.Checked ? 1 : 2,
                            status = chkActive.Checked ? 1 : 2,
                            Stock = General.ParseDecimal(txtStock.Text),
                            sale_ledger = saleLedger,
                            purchase_ledger=purchaseLedger,
                            agreement_item=chkAgreement.Checked?1:2

                        };

                        itemBAL.SaveItem(item);
                        if(!btnNew.Text.Contains("Edit"))
                        General.Action($"New Item Saved {txtName.Text} packing  {cmbUOM.Text}");
                        else
                            General.Action($" Item Updated {txtName.Text} packing  {cmbUOM.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Item Successfully Saved ");
                        LoadItem(-1);
                        ButtonActive(EnumFormEvents.Cancel);
                    }
                }
            }
            catch (Exception ee)
            {
                
                General.Error(ee.ToString());
                if (ee.InnerException != null)
                {
                    if (ee.InnerException.ToString().Contains("Violation of UNIQUE KEY"))
                        General.ShowMessage(General.EnumMessageTypes.Error, "Item Name already Exist . Please try with another Item Name");
                }
                else
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        /// <summary>
        /// SearchItem
        /// </summary>
        private void SearchItem()
        {
            try
            {
                if (txtSearch.Text.Length > 0)
                {
                    List<EDMX.item> searcList = new List<EDMX.item>();
                    General.ClearGrid(gridItem);
                    if (txtSearch.Text.ToLower() == "rawmaterial")
                    {
                        searcList = listItem.Where(x => x.rawmeterial == 1).ToList();
                    }
                    else if (txtSearch.Text.ToLower() == "sellable")
                    {
                        searcList = listItem.Where(x => x.sellable == 1).ToList();
                    }
                    else
                    {
                        searcList = listItem.Where(x => x.item_name.ToUpper().Contains(txtSearch.Text)).ToList();
                    }
                    int i = 0;
                    foreach (EDMX.item item in searcList)
                    {

                        gridItem.Rows.Add(item.item_id, $" {item.item_name} - {item.uom_setting.uom_name}");
                        gridItem.Rows[i].HeaderCell.Value = item.barcode;
                        if (gridItem.Rows.Count > 50)
                            break;
                        i++;
                    }
                    lblResultCount.Text = gridItem.Rows.Count.ToString() + "search results";
                }
                else
                {
                    LoadItem(-1);
                }
            }
            catch { throw; }
        }

        private void SearchFilterClick(object sender, EventArgs e)
        {

            if (rdbAll.Checked)
            {
                txtSearch.Clear();
            }
            else if (rdbRawmaterial.Checked)
            {
                txtSearch.Text = "rawmaterial";
            }
            else if (rdbSellable.Checked)
            {
                txtSearch.Text = "sellable";
            }

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                gridItem.Focus();
            }
        }

        private void lnkItemTransaction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ItemTransactionReportForm item = new ItemTransactionReportForm(txtName.Text);
            item.ShowDialog();
        }
    }
    class ButtonCollectionItem
        {
            public Button BtnNew { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }
            public Button BtnSave { get; set; }
        }
}
