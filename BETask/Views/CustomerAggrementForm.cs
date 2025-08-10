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
    public partial class CustomerAggrementForm : Form
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
        CustomerAggrementButtonCollection button;
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        BAL.CustomerAggrementBAL aggrementBAL = new BAL.CustomerAggrementBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<CustomerModel> _lstCustomers = null;
        int customerId = 0;
        int itemId = -1;
        string customerName = string.Empty;
        List<EDMX.item> listItem = new List<EDMX.item>();
        public CustomerAggrementForm()
        {
            InitializeComponent();
        }
        public CustomerAggrementForm(string _customerName,int _customerId)
        {
            InitializeComponent();
            this.customerName = _customerName;
            this.customerId = _customerId;


        }

        #region NextFocus
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == cmbCustomerName)
                    dgAggrement.Focus();
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
                    General.ClearGrid(dgAggrement);
                    cmbCustomerName.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveCustomerAggrement();
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

        private void LoadItem(int itemId)
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

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Object selectedItem = cmbCustomerName.SelectedItem;
                customerId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                PopulateCustomerAggrement(customerId);
            }
            catch { }
        }

        private void CustomerAggrementForm_Load(object sender, EventArgs e)
        {
            button = new CustomerAggrementButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave
            };
            ButtonActive(EnumFormEvents.FormLoad);
            GetAllCustomers();
            LoadItem(itemId);
            UpdateGridAutoComplete_Item();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetAllCustomers()
        {
            try
            {
                if (customerId == 0)
                {
                    _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1, 0);
                    foreach (CustomerModel cust in _lstCustomers)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = cust.Customer_Name,
                            Value = cust.Customer_Id
                        };
                        cmbCustomerName.Items.Add(_cmbItem);
                    }
                }
                else
                {
                    var cust = _customerBAL.GetCustomerDetail(customerId);
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cust.Customer_Name,
                        Value = cust.Customer_Id
                    };
                    cmbCustomerName.Items.Add(_cmbItem);
                }
                
                if (this.customerName != string.Empty)
                {
                    isPopulated = false;
                    cmbCustomerName.Text = customerName;
                    if (!isPopulated)
                        PopulateCustomerAggrement(customerId);
                  
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        bool isPopulated = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// 
        private void PopulateCustomerAggrement(int customerId,int status=1) {
            try {
                if (customerId > 0)
                {
                    isPopulated = true;
                    General.ClearGrid(dgAggrement);
                    List<EDMX.customer_aggrement> lstAggrements = aggrementBAL.GetCustomerAggrements(customerId, status);
                    foreach (EDMX.customer_aggrement obj in lstAggrements)
                    {
                        dgAggrement.Rows.Add(obj.item.item_id, obj.item.item_name, obj.item.uom_setting == null ? "" : obj.item.uom_setting.uom_name, obj.max_qty, obj.item.sale_rate,General.TruncateDecimalPlaces( obj.unit_price), obj.show_app == 0?1: obj.show_app,obj.serail_number,obj.remarks,obj.customer_aggrement_id,"Remove");
                       
                    }

                    //Pupulating permanant return
                    PopulatePermanantReturn( customerId);
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Unable to find customer Id , try to select customer by clicking 'New' \n please inform vendor");
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        //Pupulating permanant return
        private void PopulatePermanantReturn(int customerId)
        {
            try
            {
                DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                List<EDMX.delivery_return> returnList = deliveryDAL.GetPermanantReturn(customerId);
                if (returnList != null && returnList.Count > 0)
                {
                   
                    foreach (EDMX.delivery_return rt in returnList)
                    {
                        
                        dgAggrement.Rows.Add(rt.item.item_id, rt.item.item_name, rt.item.uom_setting == null ? "" : rt.item.uom_setting.uom_name, rt.qty, 0, 0, 2, "", $"{General.ConvertDateAppFormat(rt.server_time)} by {rt.employee.first_name}");
                        dgAggrement.Rows[dgAggrement.Rows.Count - 2].DefaultCellStyle.BackColor = Color.Red;
                        dgAggrement.Rows[dgAggrement.Rows.Count - 2].DefaultCellStyle.ForeColor = Color.White;
                    }
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
        private void UpdateGridAutoComplete_Item()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgAggrement.Columns["clmItemName"];
                comboItem.HeaderText = "Select Item Name";
                foreach (EDMX.item raw in listItem)
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

        /// <summary>
        /// 
        /// </summary>
        private void SaveCustomerAggrement() {
            try {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    if (customerId > 0)
                    {
                        List<EDMX.customer_aggrement> cusAggrements = GetCustomerAggrements();
                        aggrementBAL.SaveCustomerAggrement(cusAggrements, customerId);

                        General.Action($"Aggrement Saved {cmbCustomerName.Text}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Agreement Successfully Saved");
                        pnlAgreementTemp.Hide();
                    }
                    else
                    {
                        General.ShowMessage(General.EnumMessageTypes.Warning, "Please select customer");
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<EDMX.customer_aggrement> GetCustomerAggrements()
        {
            List<EDMX.customer_aggrement> customerAggrements = new List<EDMX.customer_aggrement>();
            try
            {
                foreach (DataGridViewRow dr in dgAggrement.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        //bool ckbVal = (bool)(dr.Cells["clmShowApp"].Value);
                        if (dr.DefaultCellStyle.BackColor != System.Drawing.Color.Red)
                        {
                            customerAggrements.Add(new EDMX.customer_aggrement()
                            {
                                customer_aggrement_id = 0,
                                customer_id = customerId,
                                item_id = Convert.ToInt32(dr.Cells["clmItemId"].Value),
                                max_qty = Convert.ToInt32(dr.Cells["MaxCount"].Value),
                                unit_price = Convert.ToDecimal(dr.Cells["clmSpecialRate"].Value),
                                serail_number = Convert.ToString(dr.Cells["clmSerialNo"].Value),
                                remarks = Convert.ToString(dr.Cells["clmRemarks"].Value),
                                show_app = Convert.ToInt32(dr.Cells["clmShowApp"].Value),
                                status = 1
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { throw; }
            return customerAggrements;
        }




        private void dgAggrement_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgAggrement.Rows[e.RowIndex].Cells[1];
                    if (cb.Value != null)
                    {
                        string _itemName = dgAggrement[1, e.RowIndex].Value.ToString();
                        if (!String.IsNullOrEmpty(_itemName))
                        {
                            var _item = listItem.Where(x => x.item_name == _itemName).FirstOrDefault();
                            dgAggrement["clmItemId", e.RowIndex].Value = _item.item_id;
                            dgAggrement["clmPacking", e.RowIndex].Value = _item.uom_setting.uom_name;
                            dgAggrement["clmRate", e.RowIndex].Value = _item.sale_rate;
                            dgAggrement["clmSpecialRate", e.RowIndex].Value = _item.sale_rate;
                            dgAggrement["MaxCount", e.RowIndex].Value = "0";
                            dgAggrement["clmShowApp", e.RowIndex].Value = 1;
                            
                        }
                    }
                   
                }
                dgAggrement.Invalidate();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void dgAggrement_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
                if (dgAggrement.CurrentRow.Index >= 0 && dgAggrement.CurrentCell.ColumnIndex == 5)
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

        private void dgAggrement_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgAggrement.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlAgreementTemp.Visible)
                pnlAgreementTemp.Hide();
            else
                AgreementLoadFromCloud();
        }
        private void AgreementLoadFromCloud()
        {
            try
            {
                if (this.customerId > 0)
                {
                    APP.DAL.CustomerAppDAL customerApp = new APP.DAL.CustomerAppDAL();
                    List<APP.EDMX.customer_aggrement_temp> listAgreement = aggrementBAL.GetCustomerAgreementTemp(this.customerId);

                    General.ClearGrid(gridTemp);

                    foreach (APP.EDMX.customer_aggrement_temp obj in listAgreement)
                    {

                        EDMX.item item = itemBAL.GetItemDetails(obj.item_id);
                        decimal unitRate = obj.unit_price;

                        decimal tax = 5;
                        if (item != null)
                        {
                            if (obj.is_tax_included == 1)
                            {
                                if (item.tax_setting != null)
                                {
                                    tax = Convert.ToDecimal(item.tax_setting.tax_value);
                                }
                                decimal withTax = obj.unit_price;
                                decimal taxDeducted = ((withTax * 100) / (100 + tax));

                                taxDeducted = General.TruncateDecimalPlaces(taxDeducted, 3);
                                unitRate = taxDeducted;
                            }
                            gridTemp.Rows.Add(item.item_id, item.item_name, item.uom_setting == null ? "" :item.uom_setting.uom_name, obj.max_qty, item.sale_rate, unitRate, obj.unit_price);
                            pnlAgreementTemp.Show();
                        }

                    }
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, "Error while getting data");
                General.Error(ee.ToString());
            }
        }

        private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlAgreementTemp.Hide();
        }

        private void linkTaxCalculator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!grpAgreementCalculator.Visible)
                grpAgreementCalculator.Show();
            else
                grpAgreementCalculator.Hide();
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
                    taxAmount = General.TruncateDecimalPlaces((rateWithTax * tax) / (100 + tax), 3);
                    agreement = rateWithTax - taxAmount;
                    txtTaxAmount.Text = taxAmount.ToString();
                    txtAgreement.Text = agreement.ToString();


                }
            }
            catch { }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            AgreementCalculator();
        }

        private void dgAggrement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex ==dgAggrement.ColumnCount-1)
                {
                    if (General.ShowMessageConfirm("Are you sure want to remove this") == DialogResult.Yes)
                    {
                        if (dgAggrement["clmAgreementId", e.RowIndex].Value != null)
                        {
                            int agreementId = Convert.ToInt32(dgAggrement["clmAgreementId", e.RowIndex].Value);
                            if (agreementId > 0)
                            {
                                aggrementBAL.RemoveAgreement(agreementId, this.customerId);
                                General.ShowMessage(General.EnumMessageTypes.Success,"Removed");
                                PopulateCustomerAggrement(this.customerId);
                            }
                        }
                    }
                }
               // int agreementId=
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to remove");
            }
        }

        private void linkClosedAgreement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkClosedAgreement.LinkColor == Color.White)
            {
                PopulateCustomerAggrement(this.customerId, 2);
                linkClosedAgreement.LinkColor = Color.Red;
            }
            else
            {
                PopulateCustomerAggrement(this.customerId);
                linkClosedAgreement.LinkColor = Color.White;
            }
        }
    }





    class CustomerAggrementButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
    }
}
