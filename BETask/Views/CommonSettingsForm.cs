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
    public partial class CommonSettingsForm : Form
    {
        CommonSettingBAL commonSettingBAL = new CommonSettingBAL();
        public bool isUpdate = false;
        int taxId = 0,uomId=0;
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
        ButtonCollectionCommnSettings button;
        public CommonSettingsForm()
        {
            InitializeComponent();
        }

        private void CommonSettingsForm_Load(object sender, EventArgs e)
        {

        }
        private void AccountGroupForm_Load(object sender, EventArgs e)
        {

        }

        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == txtTaxValuePercentage)
                    btnSaveTax.Focus();
               else if (sender == txtUOMDescription)
                    btnSaveUOM.Focus();
                else
                    General.NextFocus(sender, e);

            }
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }


        private void txtTaxValuePercentage_Validated(object sender, EventArgs e)
        {
         General.DecimalValidationText(txtTaxValuePercentage);
           
        }

        #region Tax

        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActiveTax(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActiveTax(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActiveTax(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActiveTax(EnumFormEvents.Close);
            }
        }

        private void ButtonActiveTax(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    chkStatusTax.Enabled = false;
                    btnNewTax.Text = "&New";
                    btnSaveTax.Text = "&Save";
                    break;

                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlTaxSaveContent.Enabled = true;
                    txtTaxName.Focus();
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActiveTax(EnumFormEvents.FormLoad);
                    pnlTaxSaveContent.Enabled = false;
                    isUpdate = false;
                    taxId = 0;
                    chkStatusTax.Checked = true;
                    General.ClearTextBoxes(tabCommonSettings);
                    break;

                case EnumFormEvents.Close:
                   this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Save:
                    SaveTax();
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Text = "&Update";
                    isUpdate = true;
                    break;
                default:
                    break;

            }
        }
        private void tabPageTax_Enter(object sender, EventArgs e)
        {
            button = new ButtonCollectionCommnSettings
            {
                BtnNew = btnNewTax,
                BtnCancel = btnCancelTax,
                BtnClose = btnCloseTax,
                BtnSave = btnSaveTax
            };
            ButtonActiveTax(EnumFormEvents.FormLoad);
            LoadAllTaxes();
        }

        private bool ValidationTax()
        {
            bool response = false;
            try
            {
                if (General.IsTextboxEmpty(txtTaxName)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter tax name like Vat-5%"); txtTaxName.Focus(); response = false; } else response = true;
                decimal taxValue = 0;
                decimal.TryParse(txtTaxValuePercentage.Text, out taxValue);
                txtTaxValuePercentage.Text = taxValue.ToString();
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning);
            }
            return response;
        }

        private void SaveTax()
        {
            try
            {

                if (ValidationTax())
                {
                    TaxSettingModel taxSetting = new TaxSettingModel
                    {
                        Tax_id = taxId,
                        Tax_name = txtTaxName.Text.Trim(),
                        Description = txtTaxDescription.Text.Trim(),
                        Tax_value = General.IsTextboxEmpty(txtTaxValuePercentage) ? 0 : Convert.ToDecimal(txtTaxValuePercentage.Text),
                        Status = chkStatusTax.Checked ? 1 : 2
                    };
                    commonSettingBAL.SaveTaxSetting(taxSetting);
                    General.Action($"New Tax Saved {txtTaxName.Text} under  {txtTaxValuePercentage.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "Tax Setting Successfully Saved ");
                    ButtonActiveTax(EnumFormEvents.Cancel);
                    LoadAllTaxes();
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void LoadAllTaxes()
        {
            try
            {
                General.ClearGrid(gridTaxSett);
                List<EDMX.tax_setting> listTax = commonSettingBAL.GetAllTaxes(-1);
                foreach (EDMX.tax_setting tax in listTax)
                {
                    gridTaxSett.Rows.Add(tax.tax_id, tax.tax_name, tax.description, tax.tax_value);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void FillContentTax(TaxSettingModel tax)
        {
            try
            {
                txtTaxName.Text = tax.Tax_name;
                txtTaxDescription.Text = tax.Description;
                txtTaxValuePercentage.Text = tax.Tax_value.ToString();
                taxId = tax.Tax_id;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }


        }

        private void gridTaxSett_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                TaxSettingModel tax = new TaxSettingModel
                {
                    Tax_id = Convert.ToInt32(gridTaxSett["clmTaxId", e.RowIndex].Value),
                    Tax_name = gridTaxSett["clmTaxName", e.RowIndex].Value.ToString(),
                    Description = gridTaxSett["clmTaxDesc", e.RowIndex].Value.ToString(),
                    Tax_value = Convert.ToDecimal(gridTaxSett["clmTaxValue", e.RowIndex].Value)
                };
                FillContentTax(tax);
                ButtonActiveTax(EnumFormEvents.Update);
            }
        }

        #endregion Tax

        #region UOM


        private void tabPageUnits_Enter(object sender, EventArgs e)
        {
            button = new ButtonCollectionCommnSettings
            {
                BtnNew = btnNewUOM,
                BtnCancel = btnCancelUOM,
                BtnClose = btnCloseUOM,
                BtnSave = btnSaveUOM
            };
            ButtonActiveUOM(EnumFormEvents.FormLoad);
            LoadAllUOM();
        }


        private void ButtonEventsUOM(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActiveUOM(EnumFormEvents.New);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActiveUOM(EnumFormEvents.Cancel);
            }
            else if (sender == button.BtnSave)
            {
                ButtonActiveUOM(EnumFormEvents.Save);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActiveUOM(EnumFormEvents.Close);
            }
        }
        private void ButtonActiveUOM(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    chkUOMStatus.Enabled = false;
                    btnNewUOM.Text = "&New";
                    btnSaveUOM.Text = "&Save";
                    pnlSaveContentUOM.Enabled = false;
                    pnlSaveContentUOM.BackColor = Color.White;
                    break;

                case EnumFormEvents.New:
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    pnlSaveContentUOM.Enabled = true;
                    txtUOM.Focus();
                    break;

                case EnumFormEvents.Cancel:
                    ButtonActiveUOM(EnumFormEvents.FormLoad);
                    pnlSaveContentUOM.Enabled = false;
                    isUpdate = false;
                    uomId = 0;
                    chkUOMStatus.Checked = true;
                    General.ClearTextBoxes(tabCommonSettings);
                    break;

                case EnumFormEvents.Close:
                   this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Save:
                    SaveUOM();
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Text = "&Update";
                    isUpdate = true;
                    break;
                default:
                    break;

            }
        }


        private bool ValidationUOM()
        {
            bool response = false;
            try
            {
                if (General.IsTextboxEmpty(txtUOM)) { response = false; General.ShowMessage(General.EnumMessageTypes.Warning, "Please enter UOM  like BOX(1x12)"); txtUOM.Focus(); response = false; } else response = true;
                
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Warning);
            }
            return response;
        }

        private void SaveUOM()
        {
            try
            {

                if (ValidationUOM())
                {
                    EDMX.uom_setting uomSetting = new EDMX.uom_setting
                    {
                        uom_id= uomId,
                        uom_name = txtUOM.Text.Trim(),
                        description = txtUOMDescription.Text.Trim(),
                        status = chkUOMStatus.Checked ? 1 : 2
                    };
                    commonSettingBAL.SaveUOMSetting(uomSetting);
                    General.Action($"New UOM Saved {txtUOM.Text} description  {lblUOMDescription.Text}");
                    General.ShowMessage(General.EnumMessageTypes.Success, "UOM Setting Successfully Saved ");
                    ButtonActiveUOM(EnumFormEvents.Cancel);
                    LoadAllUOM();
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

       

        private void LoadAllUOM()
        {
            try
            {
                General.ClearGrid(gridUOM);
                List<EDMX.uom_setting> listUom = commonSettingBAL.GetAllUOM(-1);
                foreach (EDMX.uom_setting uom in listUom)
                {
                    gridUOM.Rows.Add(uom.uom_id, uom.uom_name, uom.description);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tax"></param>
        private void FillContentUOM(UOMSettingModel uom)
        {
            try
            {
                txtUOM.Text = uom.UOM_name;
                txtUOMDescription.Text = uom.Description;
                uomId = uom.UOM_id;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }


        }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridUOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    UOMSettingModel uom = new UOMSettingModel
                    {
                        UOM_id = Convert.ToInt32(gridUOM["clmUOMId", e.RowIndex].Value),
                        UOM_name = gridUOM["clmUOMName", e.RowIndex].Value.ToString(),
                        Description = gridUOM["clmUOMDesc", e.RowIndex].Value.ToString(),
                    };
                    FillContentUOM(uom);
                    ButtonActiveUOM(EnumFormEvents.Update);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        #endregion UOM



    }

    class ButtonCollectionCommnSettings
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
