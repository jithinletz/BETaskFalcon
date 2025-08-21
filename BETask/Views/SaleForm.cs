using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;

namespace BETask.Views
{
    public partial class SaleForm : Form
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
        BAL.SaleBAL saleBAL = new BAL.SaleBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<EDMX.item> listItem = new List<EDMX.item>();
        List<CustomerModel> _lstCustomers = null;
        List<EDMX.account_ledger> listBank = new List<EDMX.account_ledger>();
        List<EDMX.customer_aggrement> listAgreedItems = null;
        SaleButtonCollection button;
        int itemId = -1;

        decimal totalBeforeVat = 0;
        decimal totalDiscount = 0;
        decimal totalVat = 0;
        decimal totalTaxableAmount = 0;
        decimal totalNetAmount = 0;
        long saleId = 0;
        bool loadfromCollection = false;
        public SaleForm()
        {
            InitializeComponent();
        }
        public SaleForm(string salesNumber)
        {
            InitializeComponent();
            FormLoad();
            txtInvoiceSerch.Text = salesNumber;
            Search();
            loadfromCollection = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        private void LoadItem(int itemId)
        {
            try
            {
                //  listItem = itemBAL.GetAllItem_Sellable();
                if (cmbSupplier.SelectedItem != null)
                {
                    Object selectedCustomer = cmbSupplier.SelectedItem;
                    int customerId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                    if (customerId > 0)
                    {
                        BAL.CustomerAggrementBAL customerAggrementBAL = new BAL.CustomerAggrementBAL();
                        listAgreedItems = customerAggrementBAL.GetCustomerAggrements(customerId,0);
                        UpdateGridAutoComplete_Item();
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
        private void GetAllSuppliers()
        {
            try
            {
                _lstCustomers = _customerBAL.GetAllCustomers(-1, string.Empty, 1);
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
        private void GetAllSuppliers(CustomerModel customer)
        {
            try
            {
              
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = customer.Customer_Name,
                        Value = customer.Customer_Id
                    };
                    cmbSupplier.Items.Add(_cmbItem);
                    cmbSupplier.SelectedIndex = 0;
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
                    txtSaleNumber.Text = REP.DocumentSerialDAL.GetNextDocument(REP.DocumentSerialDAL.EnumDocuments.SALE.ToString());
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
                    SaveSale();
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
                        CalucateTotals();
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


        private void UpdateGridAutoComplete_Item()
        {
            try
            {
                DataGridViewComboBoxColumn comboItem = (DataGridViewComboBoxColumn)dgItems.Columns["ItemName"];
                comboItem.Items.Clear();
                comboItem.HeaderText = "Select Item Name";
                foreach (EDMX.customer_aggrement raw in listAgreedItems)
                {
                    // col.Add(raw.item_name);  
                    comboItem.Items.Add(raw.item.item_name);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void GetAllBanks()
        {
            try
            {
                cmbBank.Items.Clear();
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new DAL.DAL.LedgerMappingDAL();
                DAL.DAL.AccountLedgerDAL accountLedgerDAL = new DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
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
                                var _item = listAgreedItems.Select(i=>i.item).Where(x => x.item_name == _itemName).FirstOrDefault();
                                if (_item != null)
                                {
                                    var _itemAgreed = listAgreedItems.Where(x => x.item_id == _item.item_id).FirstOrDefault();
                                    dgItems["Packing", e.RowIndex].Value = _item.uom_setting == null ? string.Empty : _item.uom_setting.uom_name;
                                    dgItems["Qty", e.RowIndex].Value = 1;
                                    dgItems["ID", e.RowIndex].Value = _item.item_id;
                                    dgItems["Rate", e.RowIndex].Value = _itemAgreed.unit_price;
                                    dgItems["Discount", e.RowIndex].Value = 0;
                                    dgItems["Vat", e.RowIndex].Value = _item.tax_setting.tax_value;
                                }
                                // dgItems.CurrentCell = dgItems.Rows[e.RowIndex].Cells["Qty"];////while focusing cannot use down arrow on grid combo
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
                if (dgItems["ID", rowIndex].Value != null)
                {
                    decimal quantity = Convert.ToInt32(dgItems["Qty", rowIndex].Value);
                    int itemId = Convert.ToInt32(dgItems["ID", rowIndex].Value.ToString());
                    decimal maxAllowedQty = listAgreedItems.Where(x => x.item_id == itemId).FirstOrDefault().max_qty;
                    if (maxAllowedQty != 0 )
                    {
                        if (quantity > maxAllowedQty && (cmbPaymentMode.Text!="DO" && Convert.ToInt32(txtSaleNo.Text)<=0) )
                        {
                            dgItems["Qty", rowIndex].Value = maxAllowedQty;
                            quantity = maxAllowedQty;
                        }
                    }
                    else
                    {
                        dgItems["Qty", rowIndex].Value = quantity;
                    }
                    decimal rate = Convert.ToDecimal(dgItems["Rate", rowIndex].Value);
                    decimal discount = General.ParseDecimal(Convert.ToString(dgItems["Discount", rowIndex].Value));
                    int vat = Convert.ToInt32(dgItems["Vat", rowIndex].Value);

                    //Checking Tax for recharge 31.Jan.2022
                    DAL.DAL.WalletDAL walletDAL = new DAL.DAL.WalletDAL();
                    decimal rechargeTax = walletDAL.GetRechargeTax(null);
                    if (cmbPaymentMode.Text.ToLower() == "coupon" && rechargeTax > 0)
                        vat = 0;

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
                        /*Vat is calculating singe rate and multply with qty*/
                        /*
                        decimal vatSingleUnit = General.TruncateDecimalPlaces((rate * vat) / 100, 2);
                        vatAmount = vatSingleUnit * quantity;
                        */

                        /*Vat is calculating with gross amount*/
                         vatAmount = General.TruncateDecimalPlaces((discountedAmount * vat) / 100,2);
                    }
                    decimal netAmount = discountedAmount + vatAmount;

                    dgItems["Gross", rowIndex].Value = General.TruncateDecimalPlaces(gross);
                    dgItems["VatAmount", rowIndex].Value = General.TruncateDecimalPlaces(vatAmount);
                    dgItems["Net", rowIndex].Value = General.TruncateDecimalPlaces(netAmount);

                    CalucateTotals();
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
                if (cmbPaymentMode.Text=="Cash")
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
                if (cmbPaymentMode.Text == "Cash")
                    txtCashPaid.Text = txtNetAmount.Text;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void Search()
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoiceSerch.Text))
                {
                    SaleSearchForm saleSearchForm = new SaleSearchForm();
                    DialogResult result = saleSearchForm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        int _saleId = 0;
                        _saleId = saleSearchForm.saleId;
                        if (_saleId > 0)
                        {
                            ButtonActive(EnumFormEvents.Cancel);
                            PopulateSaleDetails(_saleId);
                            ButtonActive(EnumFormEvents.Update);
                        }
                    }
                }
                else
                {
                    long saleId = saleBAL.GetSaleIdByInvoice(txtInvoiceSerch.Text);
                    if (saleId > 0)
                    {
                        ButtonActive(EnumFormEvents.Cancel);
                        PopulateSaleDetails(saleId);
                        ButtonActive(EnumFormEvents.Update);
                    }
                }
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
        private void SaveSale()
        {
            try
            {
                if (General.ShowMessageConfirm() == DialogResult.Yes)
                {
                    string errorMessage = ValidateForm();
                    if (string.IsNullOrEmpty(errorMessage) && General.CheckFinancialDate(txtInvoiceDate.Value))
                    {
                        CalucateTotals();
                        EDMX.sales sales = GetSaleItem();
                        List<EDMX.sales_item> saleItens = GetAllSaleItems();
                        long _savedId = saleBAL.SaveSale(sales, saleItens);
                        General.Action($"Sale  Saved {cmbSupplier.Text} Invoice {sales.sales_id} Net {sales.net_amount}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Sale  Successfully Saved");
                        ButtonActive(EnumFormEvents.Cancel);
                        ResetForms();
                       // PopulateSaleDetails(_savedId);
                        if (cmbPaymentMode.Text=="Coupon")
                        {
                            CouponSaleForm couponSale = new CouponSaleForm(sales.customer_id);
                            couponSale.ShowDialog();
                        }
                    }
                    else
                    {
                        if(errorMessage!="")
                        General.ShowMessage(General.EnumMessageTypes.Error, errorMessage);
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                if (ee.ToString().Contains("Violation of UNIQUE KEY constraint 'UK_sales_number'"))
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Sales Number already exist . Please try again or contact for support");
                }
                else
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        } 
       

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private EDMX.sales GetSaleItem()
        {
            EDMX.sales _sale = null;
            try
            {
                int bankId = 0,divisionId=0,oldLeafCount=0;
                if (cmbPaymentMode.Text=="Bank")
                {
                    Object bank = cmbBank.SelectedItem;
                    bankId = (int)((BETask.Views.ComboboxItem)bank).Value;
                }
                if (cmbPaymentMode.Text == "Coupon")
                    oldLeafCount =!string.IsNullOrEmpty(txtCheque.Text)? Convert.ToInt32(txtCheque.Text) :0;

                int customerId = General.GetComboBoxSelectedValue(cmbSupplier);

                int routeId = General.GetComboBoxSelectedValue(cmbRoute);
                divisionId = General.GetComboBoxSelectedValue(cmbDivision);
                _sale = new EDMX.sales()
                {
                    sales_id = Convert.ToInt32(txtSaleNo.Text),
                    bank_id = bankId,
                    cash_paid = General.ParseDecimal(txtCashPaid.Text),
                    cheque_no = txtCheque.Text,
                    customer_id = customerId,//_lstCustomers.Where(u => u.Customer_Name == cmbSupplier.Text).Select(i => i.Customer_Id).FirstOrDefault(),
                    balance_amount = General.ParseDecimal(txtBalance.Text),
                    gross_amount = General.ParseDecimal(txtTotalBeforeVat.Text),
                    sales_date = General.ConvertDateServerFormatWithCurrentTime(txtInvoiceDate.Value),
                    sales_number = txtSaleNumber.Text,
                    net_amount = General.ParseDecimal(txtNetAmount.Text),
                    payment_mode = cmbPaymentMode.Text,//rdlBank.Checked ? rdlBank.Text : rdlCash.Checked ? rdlCash.Text : rdlCredit.Checked? rdlCredit.Text:rdlCoupon.Text,
                    remarks = !String.IsNullOrEmpty(txtRemarks.Text) || !txtRemarks.Text.ToLower().Contains("edited") ? txtRemarks.Text : $"Edited on { DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")}",
                    roundup = General.ParseDecimal(txtRoundUp.Text),
                    status = 1,
                    total_beforevat = General.ParseDecimal(txtTaxableAmount.Text),
                    total_discount = General.ParseDecimal(txtTotalDiscount.Text),
                    total_vat = General.ParseDecimal(txtTotalVat.Text),
                    sales_order = !String.IsNullOrEmpty(txtOrder.Text) ? General.ParseInt(Convert.ToString(txtOrder.Text)) : 0,
                    division_id = divisionId,
                    delivery_leaf = string.IsNullOrEmpty(txtDeliveryLeaf.Text) ? null : txtDeliveryLeaf.Text,
                    old_leaf_count = oldLeafCount,
                    route_id = routeId,
                    lpo_number = txtLPO.Text,
                    payment_terms = cmbTerms.Text

                };
            }
            catch { throw; }

            return _sale;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<EDMX.sales_item> GetAllSaleItems()
        {
            List<EDMX.sales_item> saleItems = new List<EDMX.sales_item>();
            try
            {
                foreach (DataGridViewRow dr in dgItems.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        saleItems.Add(new EDMX.sales_item()
                        {
                            sales_id = Convert.ToInt32(txtSaleNo.Text),
                            discount = Convert.ToDecimal(dr.Cells["Discount"].Value),
                            gross_amount = Convert.ToDecimal(dr.Cells["Gross"].Value),
                            item_id = Convert.ToInt32(dr.Cells["Id"].Value),
                            net_amount = Convert.ToDecimal(dr.Cells["Net"].Value),
                            qty = Convert.ToDecimal(dr.Cells["Qty"].Value),
                            rate = Convert.ToDecimal(dr.Cells["Rate"].Value),
                            total_beforvat =General.TruncateDecimalPlaces( Convert.ToDecimal(dr.Cells["Gross"].Value)- Convert.ToDecimal(dr.Cells["Discount"].Value)),
                            vat_amount = Convert.ToDecimal(dr.Cells["VatAmount"].Value),
                            status = 1
                        });
                    }
                }
            }
            catch { throw; }
            return saleItems;
        }

        private void Print()
        {
            try
            {
                int salesId = 0;
                int.TryParse(txtSaleNo.Text,out salesId);
                 saleBAL.PrintSale(salesId);

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
                txtSaleNo.Focus();
                cmbBank.SelectedIndex = -1;
                cmbSupplier.SelectedIndex = -1;
                txtAddress.Text = string.Empty;
               
                txtSaleNo.Text = "0";
                this.saleId = 0;
                txtInvoiceDate.Value = DateTime.Today;
                txtSaleNumber.Text = REP.DocumentSerialDAL.GetNextDocument(REP.DocumentSerialDAL.EnumDocuments.SALE.ToString());
                lblCheque.Text = "Cheque#";
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
            else if (e.KeyData == Keys.Down)
            {
                CustomerSearch();
            }
            else if (e.KeyData != Keys.Back && e.KeyData != Keys.Delete && sender==cmbSupplier)
                CustomerSearch();

        }

        private void CustomerSearch()
        {
            CustomerSearchForm searchForm = new CustomerSearchForm();
            DialogResult result = searchForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int _customerId = 0;
                _customerId = searchForm.CustomerId;
                if (_customerId > 0)
                {
                    ButtonActive(EnumFormEvents.Cancel);
                    GetSupplierDetailsById(_customerId);
                    ButtonActive(EnumFormEvents.New);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ValidateForm() {
            string errorMsg = string.Empty;
            if (txtInvoiceDate.Text == string.Empty)  
                errorMsg = "Please enter sale date";
            if(cmbSupplier.Text==string.Empty)
                errorMsg = "Please select customer";
            if (General.IsTextboxEmpty(txtSaleNumber))
            {
                errorMsg = "Please enter Sale number";
                txtSaleNumber.Focus();
            }
            if (dgItems.Rows.Count==1)
                errorMsg = "Please add items";
            if (cmbPaymentMode.Text=="Coupon")
            {
                decimal netAmount = General.ParseDecimal(Convert.ToString( txtNetAmount.Text));
                decimal walletAmount= General.ParseDecimal(Convert.ToString(txtNetAmount.Text));
                if (netAmount > walletAmount)
                {
                    errorMsg =$"Wallet does not have the balance to make this transaction . Need {netAmount-walletAmount} more in wallet \n Please recharge wallet";

                }
                
            }
            if (cmbPaymentMode.Text=="Bank")
            {
                errorMsg = "Please select Bank account";
                cmbBank.Focus();
            }
            if (string.IsNullOrWhiteSpace(cmbPaymentMode.Text) || string.IsNullOrEmpty(cmbPaymentMode.Text) || !General.ValidatePaymentModes(cmbPaymentMode.Text))
            {
                errorMsg = "Please select valid payment mode";
                cmbPaymentMode.Focus();
            }

            if (cmbPaymentMode.Text.ToLower() == "do" && string.IsNullOrEmpty(txtDeliveryLeaf.Text.Trim()))
            {
                errorMsg = "Please enter delivery leaf number";
                txtDeliveryLeaf.Focus();
            }

            //var _days = (General.ConvertDateServerFormat(txtInvoiceDate.Value) - DateTime.Now).Days;
            //if (_days < 0)
            //    errorMsg = ("Backdate entry update is not allowed");
            return errorMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        private void PopulateSaleDetails(long saleId) {
            try
            {

                EDMX.sales _sale = saleBAL.GetSaleDetails(saleId);
                if (_sale != null)
                {
                    GetSupplierDetailsById(_sale.customer_id);
                    string division = _sale.customer_division != null ? _sale.customer_division.division_name : "";
                    //cmbSupplier.Text = _lstCustomers.Where(x => x.Customer_Id == _sale.customer_id).FirstOrDefault().Customer_Name;
                    txtSaleNo.Text = Convert.ToString(_sale.sales_id);
                    saleId = _sale.sales_id;
                    txtSaleNumber.Text = Convert.ToString(_sale.sales_number);
                    txtInvoiceDate.Text = Convert.ToString(_sale.sales_date);
                    txtCheque.Text = Convert.ToString(_sale.cheque_no);
                    txtOrder.Text = _sale.sales_order.ToString();
                    txtRemarks.Text = $"{Convert.ToString(_sale.remarks)}";
                    txtCashPaid.Text = Convert.ToString(_sale.cash_paid);
                    txtTaxableAmount.Text = Convert.ToString(_sale.gross_amount);
                    txtTotalVat.Text = Convert.ToString(_sale.total_vat);
                    txtTotalBeforeVat.Text = Convert.ToString(_sale.gross_amount);
                    txtTotalDiscount.Text = Convert.ToString(_sale.total_discount);
                    txtTaxableAmount.Text = Convert.ToString(_sale.total_beforevat);
                    txtRoundUp.Text = Convert.ToString(_sale.roundup);
                    txtNetAmount.Text = Convert.ToString(_sale.net_amount);
                    txtBalance.Text = Convert.ToString(_sale.balance_amount);
                    cmbPaymentMode.Text = _sale.payment_mode;
                    txtDeliveryLeaf.Text = _sale.delivery_leaf;
                    txtLPO.Text = _sale.lpo_number;
                    cmbTerms.Text = _sale.payment_terms;
                    FillDivision();
                    cmbDivision.Text = division;



                    if (_sale.payment_mode == "Bank")
                    {
                        GetAllBanks();
                        // rdlBank.Checked = true;
                        cmbBank.SelectedIndex = cmbBank.FindStringExact(listBank.Where(b => b.ledger_id == _sale.bank_id).FirstOrDefault().ledger_name);
                    }
                    if (_sale.old_leaf_count > 0)
                        txtCheque.Text = _sale.old_leaf_count.ToString();
                    if (_sale.route_id != null)
                    {
                        BAL.RouteBAL routeBAL = new BAL.RouteBAL();
                        var _route = routeBAL.GetRoute(Convert.ToInt32(_sale.route_id));
                        cmbRoute.Text = _route.route_name;
                    }


                    PopulateSaleItems(_sale.sales_item.ToList());
                    this.saleId = _sale.sales_id;
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
        public void GetSupplierDetails(string supplierName)
        {

            try
            {
                var objSupplier =_lstCustomers.Where(s => s.Customer_Name == supplierName).FirstOrDefault();
                if (objSupplier != null)
                {
                    txtAddress.Text = string.Format("{0}\n{1}\n{2}\n{3}\n", objSupplier.Address1, objSupplier.Address2, objSupplier.City, objSupplier.POBox);
                    txtTin.Text = objSupplier.Trn;
                    CustomerModel customer = _customerBAL.GetCustomerDetail(objSupplier.Customer_Id);
                    lblCouponBalance.Text = customer.WalletBalance.ToString();
                    if (!string.IsNullOrEmpty(customer.Paymentmode))
                        cmbPaymentMode.Text = customer.Paymentmode;

                    BAL.RouteBAL routeBAL = new BAL.RouteBAL();
                    if (objSupplier.RouteId > 0)
                    {
                        var _route = routeBAL.GetRoute(objSupplier.RouteId);
                        cmbRoute.Text = _route.route_name;
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        public void GetSupplierDetailsById(int  id)
        {

            try
            {
                cmbSupplier.Items.Clear();
                                BAL.CustomerBAL customerBAL = new BAL.CustomerBAL();
                var objSupplier = customerBAL.GetCustomerDetail(id); //_lstCustomers.Where(s => s.Customer_Name == supplierName).FirstOrDefault();
                if (objSupplier != null)
                {
                    GetAllSuppliers(objSupplier);
                    txtAddress.Text = string.Format("{0}\n{1}\n{2}\n{3}\n", objSupplier.Address1, objSupplier.Address2, objSupplier.City, objSupplier.POBox);
                    txtTin.Text = objSupplier.Trn;
                    CustomerModel customer = _customerBAL.GetCustomerDetail(objSupplier.Customer_Id);
                    lblCouponBalance.Text = customer.WalletBalance.ToString();
                    if (!string.IsNullOrEmpty(customer.Paymentmode))
                        cmbPaymentMode.Text = customer.Paymentmode;

                    BAL.RouteBAL routeBAL = new BAL.RouteBAL();
                    if (objSupplier.RouteId > 0)
                    {
                        var _route = routeBAL.GetRoute(objSupplier.RouteId);
                        cmbRoute.Text = _route.route_name;
                    }
                    if (objSupplier.Paymentmode.ToLower() == "coupon")
                        lblCheque.Text = "Old Leafs";
                    else
                        lblCheque.Text = "Cheque#";
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        private void FillDivision()
        {
            try
            {
                int customerId = General.GetComboBoxSelectedValue(cmbSupplier);
                DAL.DAL.CustomerDAL customerDAL =  new REP.CustomerDAL();
                List<EDMX.customer_division> listDivision = customerDAL.GetCustomerDivision(customerId);
                if (listDivision != null)
                {
                    cmbDivision.Items.Clear();
                    foreach (EDMX.customer_division dv in listDivision)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = dv.division_name,
                            Value = dv.division_id
                        };
                        cmbDivision.Items.Add(_cmbItem);
                    }
                   
                }
             

            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load divisions");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbSupplier.Text))
            {
                //GetSupplierDetails(cmbSupplier.Text);
                LoadItem(itemId);
                FillDivision();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstPurchaseItems"></param>
        private void PopulateSaleItems(List<EDMX.sales_item> lstsaleItems) {
            try {
                General.ClearGrid(dgItems);
                foreach (EDMX.sales_item pi in lstsaleItems) {
                    dgItems.Rows.Add(pi.item.item_name, pi.item.item_id, pi.item.uom_setting.uom_name,pi.qty, pi.rate, pi.gross_amount, pi.discount, pi.item.tax_setting.tax_value, pi.vat_amount, pi.net_amount);

                }
                dgItems.Refresh();

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
                Views.CustomerAggrementForm itemForm = new CustomerAggrementForm();
                itemForm.ShowDialog();
                LoadItem(-1);
              

            }

        }

        private void lnkTrackDelivery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImportDelivery();
        }
        private void ImportDelivery()
        {
            DeliveryTrackForm deliveryTrackForm = new DeliveryTrackForm(true);
            DialogResult result = deliveryTrackForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                int customerId = deliveryTrackForm.CustomerId;
                int deliveryId = deliveryTrackForm.deliveryId;
                string customerName = deliveryTrackForm.CustomerName;
                int invoiceNo = deliveryTrackForm.InvoiceNo;
                if (invoiceNo > 0)
                    PopulateSaleDetails(invoiceNo);
                else
                {
                    ImportDeliveryItems(deliveryId,customerId);
                }
            }
           
        }
        private void ImportDeliveryItems(int deliveryId,int customerId)
        {
            try
            {
                BAL.DeliveryBAL deliveryBAL = new BAL.DeliveryBAL();
                EDMX.delivery delivery= deliveryBAL.GetDeliveryDetails(deliveryId);
                if (delivery !=null)
                {
                    ButtonActive(EnumFormEvents.New);
                    List<EDMX.delivery_items> deliveryItems = delivery.delivery_items.ToList().Where(customer=>customer.customer_id==customerId).ToList();
                    cmbSupplier.Text = deliveryItems[0].customer.customer_name;
                    txtOrder.Text = deliveryId.ToString();
                    GetSupplierDetails(deliveryItems[0].customer.customer_name);
                    txtRemarks.Text = $"Imported delivery {txtOrder.Text}";
                    foreach(EDMX.delivery_items pi in deliveryItems)
                    {
                        dgItems.Rows.Add(pi.item.item_name, pi.item.item_id, pi.item.uom_setting.uom_name, pi.qty, pi.rate, pi.gross_amount, pi.discount, pi.item.tax_setting.tax_value, pi.vat_amount, pi.net_amount);
                        txtDeliveryLeaf.Text = pi.delivery_leaf;
                    }
                    CalucateTotals();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void rdlBank_CheckedChanged(object sender, EventArgs e)
        {
            GetAllBanks();
        }

        private void FormLoad()
        {
            button = new SaleButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnPrint = btnPrint
            };
            ButtonActive(EnumFormEvents.FormLoad);
            //GetAllSuppliers();
            General.BindPaymentModes(cmbPaymentMode);
            Application.DoEvents();
            GetAllRoutes();

        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            if (!loadfromCollection)
                FormLoad();
            General.SetScreenSize(sender, e, this);
        }

        private void cmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbPaymentMode.Text=="Bank")
                GetAllBanks();
        }

        private void SaleForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                ButtonActive(EnumFormEvents.Close);
        }
        private void GetAllRoutes()
        {
            try
            {
                BAL.RouteBAL routeBAL = new BAL.RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();
                cmbRoute.Items.Clear();
                foreach (EDMX.route route in listRoute)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = route.route_name,
                        Value = route.route_id
                    };
                    cmbRoute.Items.Add(_cmbItem);

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

       
        private void label2_DoubleClick(object sender, EventArgs e)
        {
            int saleId = Convert.ToInt32(txtSaleNo.Text);
            ViewJournalForm viewJournalForm = new ViewJournalForm(saleId);
            viewJournalForm.ShowDialog();
        }

        private void txtTotalVat_DoubleClick(object sender, EventArgs e)
        {
            CalucateTotals();
        }
    }
    class SaleButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
    }
}
