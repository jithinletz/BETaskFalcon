using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class PurchaseOrderForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            New,
            Save,
            Cancel,
            Close,
            Update,
            Other,
            Search,
            Print
        }
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        BAL.PurchaseOrderBAL purchaseBAL = new BAL.PurchaseOrderBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<EDMX.item> listItem = new List<EDMX.item>();
        List<CustomerModel> _lstCustomers = null;
        List<EDMX.account_ledger> listBank = new List<EDMX.account_ledger>();
        PurchaseOrderButtonCollection button;
       int itemId = -1;

        decimal totalBeforeVat = 0;
        decimal totalDiscount = 0;
        decimal totalVat = 0;
        decimal totalTaxableAmount = 0;
        decimal totalNetAmount = 0;
        int purchaseId = 0;
        public PurchaseOrderForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        private void LoadItem(int itemId)
        {
            try
            {
                listItem = itemBAL.GetAllItem(itemId);
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
        private void GetAllSuppliers()
        {
            try
            {
                _lstCustomers = _customerBAL.GetAllCustomers(-1, string.Empty, 2);
                foreach (CustomerModel cust in _lstCustomers)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = cust.Customer_Name,
                        Value = cust.Customer_Id
                    };
                    cmbSupplier.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllBanks()
        {
            try
            {
                cmbBank.Items.Clear();
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                int groupId=Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                listBank = accountLedgerDAL.GetAllAccountLedger(groupId);
                foreach (EDMX.account_ledger bank in listBank)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = bank.ledger_name,
                        Value = bank.ledger_id
                    };
                    cmbBank.Items.Add(_cmbItem);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
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
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    pnlSaveContent.Enabled = false;
                    General.ClearTextBoxes(this);
                    ResetForms();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SavePurchase();
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
      
        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            
        }

        private void UpdateGridAutoComplete_Item()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgItems.Columns["ItemName"];
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

        private void dgItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    if (e.ColumnIndex == 0)
                    {
                        DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgItems.Rows[e.RowIndex].Cells[0];
                        if (cb.Value != null)
                        {
                            string _itemName = dgItems[0, e.RowIndex].Value.ToString();
                            if (!String.IsNullOrEmpty(_itemName))
                            {
                                var _item = listItem.Where(x => x.item_name == _itemName).FirstOrDefault();
                                dgItems["Packing", e.RowIndex].Value = _item.uom_setting == null ? string.Empty : _item.uom_setting.uom_name;
                                dgItems["Qty", e.RowIndex].Value = 1;
                                dgItems["ID", e.RowIndex].Value = _item.item_id;
                                dgItems["Rate", e.RowIndex].Value = _item.purchase_rate;
                                dgItems["Discount", e.RowIndex].Value = 0;
                                dgItems["Vat", e.RowIndex].Value = _item.tax_setting.tax_value;
                              //  dgItems.CurrentCell = dgItems.Rows[e.RowIndex].Cells["Qty"]; //while focusing cannot use down arrow on grid combo

                            }

                        }
                    }
                    AutoPopulateGridColumns(e.RowIndex);
                }
                dgItems.Invalidate();
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
        /// <param name="rowIndex"></param>
        private void AutoPopulateGridColumns(int rowIndex)
        {
            try
            {
                int quantity = Convert.ToInt32(dgItems["Qty", rowIndex].Value);
                decimal rate = Convert.ToDecimal(dgItems["Rate", rowIndex].Value);
                decimal discount = General.ParseDecimal(Convert.ToString(dgItems["Discount", rowIndex].Value));
                int vat = Convert.ToInt32(dgItems["Vat", rowIndex].Value);

                decimal gross = 0;
                decimal discountedAmount = 0;
                decimal vatAmount = 0;

                if (quantity > 0)
                {
                    gross = quantity * rate;
                    discountedAmount = gross;
                }
                if (discount > 0)
                {
                    discountedAmount = General.TruncateDecimalPlaces(discountedAmount - discount);
                }
                if (vat > 0)
                {
                    vatAmount = General.TruncateDecimalPlaces((discountedAmount * vat) / 100);
                }
                decimal netAmount = discountedAmount + vatAmount;

                dgItems["Gross", rowIndex].Value = gross;
                dgItems["VatAmount", rowIndex].Value = vatAmount;
                dgItems["Net", rowIndex].Value = netAmount;

                CalucateTotals();

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
        private void CalucateTotals()
        {
            try
            {
                totalBeforeVat = 0;
                totalDiscount = 0;
                totalVat = 0;
                totalNetAmount = 0;
                totalTaxableAmount = 0;
                totalNetAmount = 0;

                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    totalBeforeVat += General.ParseDecimal(Convert.ToString(dr.Cells["Gross"].Value));
                    totalDiscount += General.ParseDecimal(Convert.ToString(dr.Cells["Discount"].Value));
                    totalVat += General.ParseDecimal(Convert.ToString(dr.Cells["VatAmount"].Value));
                    totalNetAmount += General.ParseDecimal(Convert.ToString(dr.Cells["Net"].Value)); 
                    totalTaxableAmount = totalBeforeVat - totalDiscount; 
                }
                if (!string.IsNullOrEmpty(txtRoundUp.Text))
                    totalNetAmount = totalNetAmount - General.ParseDecimal(txtRoundUp.Text);
                txtTotalBeforeVat.Text = Convert.ToString(totalBeforeVat);
                txtTotalDiscount.Text = Convert.ToString(totalDiscount);
                txtTaxableAmount.Text = Convert.ToString(totalTaxableAmount);
                txtTotalVat.Text = Convert.ToString(totalVat);
                txtNetAmount.Text = Convert.ToString(totalNetAmount);
                if (rdlCash.Checked)
                    txtCashPaid.Text = txtNetAmount.Text;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void txtRoundUp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtNetAmount.Text = Convert.ToString(General.TruncateDecimalPlaces((General.ParseDecimal(txtTaxableAmount.Text) + General.ParseDecimal(txtTotalVat.Text)) - General.ParseDecimal(txtRoundUp.Text)));
                if (rdlCash.Checked)
                    txtCashPaid.Text = txtNetAmount.Text;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
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
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePurchase()
        {
            try
            {
                string errorMessage = ValidateForm();
                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes)
                    {
                        EDMX.purchase_order _purchase = GetPurchaseItem();
                        List<EDMX.purchase_order_item> purchaseItems = GetAllPurchaseItems();
                        int _savedId = purchaseBAL.SavePurchaseDetails(_purchase, purchaseItems);

                        General.Action($"Purchase Order Saved {cmbSupplier.Text} Invoice {_purchase.invoice_number} Net {_purchase.net_amount}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Purchase Order Successfully Saved");
                        ButtonActive(EnumFormEvents.Cancel);
                        ResetForms();
                       // PopulatePurchaseDetails(_savedId);
                    }
                }
                else {
                    General.ShowMessage(General.EnumMessageTypes.Error, errorMessage);
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
        /// <returns></returns>
        private EDMX.purchase_order GetPurchaseItem()
        {
            EDMX.purchase_order _purchaseItem = null;
            try
            {
                int bankId = 0;
                if (rdlBank.Checked)
                {
                    Object bank = cmbBank.SelectedItem;
                     bankId = (int)((BETask.Views.ComboboxItem)bank).Value;
                }
                _purchaseItem = new EDMX.purchase_order()
                {
                    purchase_id = Convert.ToInt32(txtPurchaseNo.Text),
                    purchase_order1 = Convert.ToInt32(General.IsTextboxEmpty(txtOrder)?"0":txtOrder.Text),
                    bank_id = bankId,
                    cash_paid = General.ParseDecimal(txtCashPaid.Text),
                    cheque_no = txtCheque.Text,
                    vendor_id = _lstCustomers.Where(u => u.Customer_Name == cmbSupplier.Text).Select(i => i.Customer_Id).FirstOrDefault(),
                    balance_amount = General.ParseDecimal(txtBalance.Text),
                    gross_amount = General.ParseDecimal(txtTotalBeforeVat.Text),
                    invoice_date = Convert.ToDateTime(txtInvoiceDate.Text),
                    invoice_number = txtInvoiceNo.Text,
                    net_amount = General.ParseDecimal(txtNetAmount.Text),
                    purchase_date = DateTime.Now,
                    payment_mode = rdlBank.Checked ? rdlBank.Text : rdlCash.Checked ? rdlCash.Text : rdlCredit.Text,
                    remarks = txtRemarks.Text,
                    roundup = General.ParseDecimal(txtRoundUp.Text),
                    status = 1,
                    total_beforevat = General.ParseDecimal(txtTotalBeforeVat.Text),
                    total_discount = General.ParseDecimal(txtTotalDiscount.Text),
                    total_vat = General.ParseDecimal(txtTotalVat.Text)
                };
            }
            catch { throw; }

            return _purchaseItem;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<EDMX.purchase_order_item> GetAllPurchaseItems()
        {
            List<EDMX.purchase_order_item> purchaseItems = new List<EDMX.purchase_order_item>();
            try
            {
                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        purchaseItems.Add(new EDMX.purchase_order_item()
                        {
                            purchase_id = Convert.ToInt32(txtPurchaseNo.Text),
                            discount = Convert.ToDecimal(dr.Cells["Discount"].Value),
                            gross_amount = Convert.ToDecimal(dr.Cells["Gross"].Value),
                            item_id = Convert.ToInt32(dr.Cells["Id"].Value),
                            net_amount = Convert.ToDecimal(dr.Cells["Net"].Value),
                            qty = Convert.ToDecimal(dr.Cells["Qty"].Value),
                            rate = Convert.ToDecimal(dr.Cells["Rate"].Value),
                            total_beforevat = Convert.ToDecimal(dr.Cells["Gross"].Value),
                            vat_amount = Convert.ToDecimal(dr.Cells["VatAmount"].Value),
                            status = 1
                        });
                    }
                }
            }
            catch { throw; }
            return purchaseItems;
        }

        private void Print()
        {
            try
            {
                int purchaseId = 0;
                int.TryParse(txtPurchaseNo.Text,out purchaseId);
               purchaseBAL.PrintPurchase(purchaseId);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void Search()
        {
            PurchaseSearchForm purchaseSearchForm = new PurchaseSearchForm(false,true);
            DialogResult result = purchaseSearchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int _purchaseId = 0;
                _purchaseId = purchaseSearchForm.purchaseId;
                if (_purchaseId > 0)
                {
                    ButtonActive(EnumFormEvents.Cancel);
                    PopulatePurchaseDetails(_purchaseId);
                    ButtonActive(EnumFormEvents.Update);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            }

            if (dgItems.CurrentRow.Index >= 0 && (dgItems.CurrentCell.ColumnIndex >1))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(General.TxtOnlyDecimal);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ResetForms() {
            try
            {
                General.ClearTextBoxes(this);
                General.ClearGrid(dgItems);
                txtPurchaseNo.Focus();
                cmbBank.SelectedIndex = -1;
                cmbSupplier.SelectedIndex = -1;
                txtAddress.Text = string.Empty;
                rdlCash.Checked = true;
                txtPurchaseNo.Text = "0";
                this.purchaseId = 0;
                
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
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            { 
               General.NextFocus(sender, e);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ValidateForm() {
            string errorMsg = string.Empty;
            if (txtInvoiceDate.Text == string.Empty)  
                errorMsg = "Please enter invoice date";
            if(cmbSupplier.Text==string.Empty)
                errorMsg = "Please select supplier";
            if (General.IsTextboxEmpty(txtInvoiceNo))
            {
               // errorMsg = "Please enter Invoice number";
                //txtInvoiceNo.Focus();
            }
            if (dgItems.Rows.Count==1)
                errorMsg = "Please add items";
            if (rdlBank.Checked && cmbBank.Text==string.Empty)
            {
                errorMsg ="Please select Bank account";
                cmbBank.Focus();
            }
            return errorMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        private void PopulatePurchaseDetails(int purchaseId) {
            try {
                EDMX.purchase_order _purchase = purchaseBAL.GetPurchaseDetails(purchaseId);
                if (_purchase != null) {
                    cmbSupplier.Text = _lstCustomers.Where(x => x.Customer_Id == _purchase.vendor_id).FirstOrDefault().Customer_Name;
                    txtPurchaseNo.Text =Convert.ToString( _purchase.purchase_id);
                    purchaseId = _purchase.purchase_id;
                    txtInvoiceNo.Text= Convert.ToString(_purchase.invoice_number);
                    txtInvoiceDate.Text = Convert.ToString(_purchase.invoice_date);
                    txtOrder.Text = Convert.ToString(_purchase.purchase_order1);
                    txtCheque.Text = Convert.ToString(_purchase.cheque_no);

                    txtRemarks.Text = Convert.ToString(_purchase.remarks);
                    txtCashPaid.Text = Convert.ToString(_purchase.cash_paid);
                    txtTaxableAmount.Text = Convert.ToString(_purchase.gross_amount);
                    txtTotalVat.Text = Convert.ToString(_purchase.total_vat);
                    txtTotalBeforeVat.Text = Convert.ToString(_purchase.gross_amount);
                    txtTotalDiscount.Text = Convert.ToString(_purchase.total_discount);
                    txtTaxableAmount.Text = Convert.ToString(_purchase.total_beforevat);
                    txtRoundUp.Text = Convert.ToString(_purchase.roundup);
                    txtNetAmount.Text = Convert.ToString(_purchase.net_amount);
                    txtBalance.Text = Convert.ToString(_purchase.balance_amount);
                    if (_purchase.payment_mode == "Cash")
                        rdlCash.Checked = true;
                    else if (_purchase.payment_mode == "Bank")
                    {
                        GetAllBanks();
                        rdlBank.Checked = true;
                        cmbBank.SelectedIndex = cmbBank.FindStringExact(listBank.Where(b => b.ledger_id == _purchase.bank_id).FirstOrDefault().ledger_name);
                    }
                    else
                        rdlCredit.Checked = true;

                    PopulatepurchaseItems(_purchase.purchase_order_item.ToList());
                    this.purchaseId = _purchase.purchase_id;
                    ButtonActive(EnumFormEvents.Update);
                  //  CalucateTotals();
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void ValidateDecimalPercision(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;
            General.DecimalValidationText(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierName"></param>
        public void GetSupplierDetails(string supplierName) {

            try {
                var objSupplier = _lstCustomers.Where(s => s.Customer_Name == supplierName).FirstOrDefault();
                if (objSupplier != null) {
                    txtAddress.Text = string.Format("{0}\n{1}\n{2}\n{3}\n", objSupplier.Address1, objSupplier.Address2, objSupplier.City, objSupplier.POBox);
                    txtTin.Text = objSupplier.Trn;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void FormLoad()
        {
            button = new PurchaseOrderButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
            ButtonActive(EnumFormEvents.FormLoad);

            
            LoadItem(itemId);
            Application.DoEvents();
            GetAllSuppliers();
            Application.DoEvents();
            UpdateGridAutoComplete_Item();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSupplierDetails(cmbSupplier.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstPurchaseItems"></param>
        private void PopulatepurchaseItems(List<EDMX.purchase_order_item> lstPurchaseItems) {
            try {
                General.ClearGrid(dgItems);
                foreach (EDMX.purchase_order_item pi in lstPurchaseItems) {
                    dgItems.Rows.Add(pi.item.item_name, pi.item.item_id, pi.item.uom_setting.uom_name,pi.qty, pi.rate, pi.gross_amount, pi.discount, pi.item.tax_setting.tax_value, pi.vat_amount, pi.net_amount);
                    if (pi.original_prurchase_id != null)
                        dgItems.Rows[dgItems.Rows.Count - 2].DefaultCellStyle.BackColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void dgItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    if (dgItems.Rows.Count > 1)
                    {
                        int row = dgItems.CurrentRow.Index;
                        if (dgItems[0, row].Value != null)
                        {
                            dgItems.Rows.RemoveAt(row);
                            CalucateTotals();
                        }
                    }
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
            else if (e.KeyCode == Keys.F3)
            {
                Views.ItemForm itemForm = new ItemForm();
                itemForm.ShowDialog();
                LoadItem(-1);
                UpdateGridAutoComplete_Item();

            }
        }

        private void rdlBank_CheckedChanged(object sender, EventArgs e)
        {
            GetAllBanks();
        }

    
    }
    class PurchaseOrderButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
