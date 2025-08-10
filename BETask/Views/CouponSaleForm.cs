using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;

namespace BETask.Views
{
    public partial class CouponSaleForm : Form
    {
        int couponId = 0, customerId=0;
        string bookNumber = string.Empty;
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
            Print,
            Delete
        }
        CouponButtonCollection button;

        
        public CouponSaleForm()
        {
            InitializeComponent();
        }
        public CouponSaleForm(int _customerId)
        {
            InitializeComponent();
            this.customerId = _customerId;
            Search();
        }



        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    //  pnlSaveContent.Enabled = false;
                    button.BtnNew.Enabled = true;
                    button.BtnSave.Enabled = false;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = false;
                    Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    button.BtnNew.Text = "&New";
                    button.BtnSave.Text = "&Save";
                    ResetForms();
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Save:
                    SaveCoupon();
                    break;
                case EnumFormEvents.New:
                    
                    button.BtnNew.Enabled = false;
                    button.BtnSave.Enabled = true;
                    button.BtnClose.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    if (button.BtnNew.Text.Contains( "New"))
                        ResetForms();
                    else
                    { }
                   
                    break;
                case EnumFormEvents.Update:
                    button.BtnNew.Enabled = true;
                    button.BtnCancel.Enabled = true;
                    button.BtnNew.Text = "&Edit";
                    button.BtnSave.Text = "&Update";
                    button.BtnSave.Enabled = false;
                    break;
                case EnumFormEvents.Other:

                    GenerateLeafs();
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.Delete:
                    Delete();
                    break;
                case EnumFormEvents.Print:
                    // Print();
                    break;
                default:
                    break;

            }
        }
        private void ResetForms()
        {
            chkBookStatus.Checked = true;
            string customerName = cmbCustomerName.Text;
            cmbCustomerName.Text = "";
            General.ClearTextBoxes(this);
            cmbCustomerName.Text = customerName;
            txtLeafStartNo.ResetText();
            txtLeafEndNo.ResetText();
            lblTotalLeafs.Text = "0";
            General.ClearGrid(gridCouponLeafs); ;
            btnNew.Text = "&New";
            btnSave.Text = "&Save";
            couponId = 0;
            cmbCustomerName.Enabled = true;
            
           

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
            else if (sender == button.BtnGen)
            {
                ButtonActive(EnumFormEvents.Other);
            }
            else if (sender == button.BtnDelete)
            {
                ButtonActive(EnumFormEvents.Delete);
            }
        }
        private bool Validation()
        {
            bool resp = false;
            if (String.IsNullOrEmpty(cmbCustomerName.Text)) { resp = false; General.ShowMessage(General.EnumMessageTypes.Error, "Please select customer"); cmbCustomerName.Focus(); } else resp = true;
            if (String.IsNullOrEmpty(txtBookNo.Text)) resp = false; else resp = true;
            if (String.IsNullOrEmpty(txtBookAmount.Text)) { resp = false;General.ShowMessage(General.EnumMessageTypes.Error, "Please enter Book Amount");txtBookAmount.Focus(); } else resp = true;
            if (String.IsNullOrEmpty(txtRatePerLeaf.Text)) { resp = false; General.ShowMessage(General.EnumMessageTypes.Error, "Please enter Leaf Amount");txtRatePerLeaf.Focus(); } else resp = true;

            return resp;
        }

        private void SaveCoupon()
        {
            if (Validation())
            {
                if (General.ShowMessageConfirm("Are you sure want to save this coupon details") == DialogResult.Yes)
                {
                    BAL.CouponBAL couponBAL = new BAL.CouponBAL();
                    try
                    {
                        EDMX.coupon coupon = GetCoupon();
                        List<EDMX.coupon_leaf> listCoupon = GetCouponLeaf();
                        couponBAL.SaveCoupon(coupon, listCoupon);
                        General.Action($"Coupon Saved {txtBookNo.Text}  Leafs {coupon.leaf_count} Leafs {coupon.leaf_from} -{coupon.leaf_end} ,  Rate {coupon.leaf_rate} Book Amount {coupon.book_rate}");
                        General.ShowMessage(General.EnumMessageTypes.Success, "Coupon Successfully Saved");
                        ButtonActive(EnumFormEvents.Cancel);

                    }
                    catch (Exception ee)
                    {
                        if (ee.ToString().ToLower().Contains("unique key"))
                            General.ShowMessage(General.EnumMessageTypes.Error, "Leaf already assigned to another user");
                        else
                        {
                            if (ee.InnerException == null)
                                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                            else
                                General.ShowMessage(General.EnumMessageTypes.Error, ee.InnerException.ToString());
                        }
                        General.Error(ee.ToString());
                        
                    }
                }
            }
        }

        private void Search()
        {
            try
            {
                General.ClearGrid(gridDeliveries);
                BAL.CouponBAL couponBAL = new BAL.CouponBAL();
                List<EDMX.coupon> listCoupon = couponBAL.SearchCoupon(txtSearchCoupnBook.Text, customerId);
                if (listCoupon != null && listCoupon.Count > 0)
                {
                    foreach (EDMX.coupon cp in listCoupon)
                    {
                        gridDeliveries.Rows.Add(cp.coupon_id, cp.book_number, cp.customer.customer_name);
                        //if (listCoupon.Count == 1)
                        //{
                        //    GetCoupon(cp.coupon_id);
                        //}
                    }
                   
                }
               
                lblResultCount.Text = gridDeliveries.Rows.Count.ToString() + "search results";
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void Delete()
        {
            if (this.couponId > 0)
            {
                if (General.ShowMessageConfirm("Are you sure want to delete this coupon book") == DialogResult.Yes)
                {
                    BAL.CouponBAL couponBAL = new BAL.CouponBAL();
                    if (couponBAL.DeleteCoupon(couponId,bookNumber))
                    {
                        General.ShowMessage(General.EnumMessageTypes.Success, "Coupon successfully deleted");
                        GetCoupon(couponId);
                        Search();
                        button.BtnDelete.Visible = false;

                    }
                    else
                    {
                        General.ShowMessage(General.EnumMessageTypes.Error, "Unable to delete coupon");

                    }

                }
            }
        }
        private List<EDMX.coupon_leaf> GetCouponLeaf()
        {
            List<EDMX.coupon_leaf> listLeafs = new List<EDMX.coupon_leaf>();
            try
            {
                foreach (DataGridViewRow row in gridCouponLeafs.Rows)
                {
                    if (row.DefaultCellStyle.BackColor != System.Drawing.Color.Orange)
                    {
                        int status = Convert.ToBoolean(row.Cells["clmLeafCancel"].Value) == true ? 2 : 1;

                        //check redeemed
                        status = status == 1 ? (Convert.ToBoolean(row.Cells["clmLeafRedeem"].Value) == true) ? 4 : 1 : status;

                        listLeafs.Add(new EDMX.coupon_leaf()
                        {
                            coupon_id = couponId,
                            leaf_id = General.ParseInt(row.Cells["clmLeafId"].Value.ToString()),
                            leaf_number = Convert.ToInt64(row.Cells["clmLeafNo"].Value.ToString()),
                            issue_date = General.ConvertDateServerFormat(dtpDeliveryDate.Value),
                            leaf_rate = Convert.ToDecimal(txtRatePerLeaf.Text),
                            status = status
                           
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listLeafs;
        }
        private EDMX.coupon GetCoupon()
        {
            EDMX.coupon coupon = new EDMX.coupon();
            try
            {
                int custId = 0;
                if (cmbCustomerName.SelectedItem != null)
                {
                    Object selectedCustomer = cmbCustomerName.SelectedItem;
                    custId = (int)((BETask.Views.ComboboxItem)selectedCustomer).Value;
                }

                coupon = new EDMX.coupon
                {
                    coupon_id = couponId,
                    customer_id = custId,
                    issue_date=General.ConvertDateServerFormat(dtpDeliveryDate.Value),
                    book_number = txtBookNo.Text.Trim(),
                    book_rate = General.ParseDecimal(txtBookAmount.Text),
                    leaf_count = gridCouponLeafs.Rows.Count,
                    leaf_from = Convert.ToInt64(txtLeafStartNo.Value.ToString()),
                    leaf_end = Convert.ToInt64(txtLeafEndNo.Value.ToString()),
                    leaf_rate = General.ParseDecimal(txtRatePerLeaf.Text),
                    remarks = txtRemarks.Text.Trim(),
                    status = 1
                };
            }
            catch
            {
                throw;
            }
            return coupon;
        }
        private void GetCoupon(int couponId)
        {
            
            try
            {
                ResetForms();
                this.couponId = couponId;
                BAL.CouponBAL couponBAL = new BAL.CouponBAL();
                EDMX.coupon coupon = couponBAL.GetCoupon(couponId);
                if (coupon != null)
                {
                    txtBookNo.Text = coupon.book_number;
                    dtpDeliveryDate.Value = coupon.issue_date;
                    txtLeafStartNo.Value = coupon.leaf_from;
                    txtLeafEndNo.Value = coupon.leaf_end;
                    txtRatePerLeaf.Text = coupon.leaf_rate.ToString();
                    txtBookAmount.Text = coupon.book_rate.ToString();
                    txtRemarks.Text = coupon.remarks;
                    chkBookStatus.Checked = coupon.status == 1 ? true : false;
                    lblTotalLeafs.Text = ((coupon.leaf_end - coupon.leaf_from) + 1).ToString();
                    cmbCustomerName.Text = coupon.customer.customer_name;

                    List<EDMX.coupon_leaf> listLeaf = coupon.coupon_leaf.ToList();
                    foreach (EDMX.coupon_leaf leaf in listLeaf)
                    {
                        gridCouponLeafs.Rows.Add(leaf.leaf_id, leaf.leaf_number, leaf.leaf_rate, leaf.status == 2 ? true : false,leaf.status==4?true:false,leaf.remarks);
                        if (leaf.status != 1)
                            gridCouponLeafs.Rows[gridCouponLeafs.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Orange;
                    }
                    cmbCustomerName.Enabled = false;

                }
                
            }
            catch
            {
                throw;
            }
          
        }
        private void GenerateLeafs()
        {
            try
            {
                BAL.CouponBAL couponBAL = new BAL.CouponBAL();
                General.ClearGrid(gridCouponLeafs);
                string validation = (couponBAL.LeafExistCheck(Convert.ToInt64(txtLeafStartNo.Value), Convert.ToInt64(txtLeafEndNo.Value), txtBookNo.Text));
                if (String.IsNullOrEmpty(validation))
                {
                    decimal leafRate = General.ParseDecimal(txtRatePerLeaf.Text);
                    Int64 startNo = Convert.ToInt64(txtLeafStartNo.Text);
                    Int64 endNo = Convert.ToInt64(txtLeafEndNo.Text);
                    lblTotalLeafs.Text = ((endNo - startNo) + 1).ToString();
                    for (Int64 i = startNo; i <= endNo; i++)
                    {
                        gridCouponLeafs.Rows.Add(0, i, leafRate, false, false, "Not Saved");
                    }
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, validation);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                if (ee.InnerException == null)
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                else
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.InnerException.ToString());
            }
        }
        private void GetAllCustomers()
        {
            try
            {
                BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
                if (this.customerId == 0)
                {
                    List<CustomerModel> _lstCustomers = _customerBAL.GetAllCustomers(0, string.Empty, 1, 0);
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
                    CustomerModel cust= _customerBAL.GetCustomerDetail(this.customerId);
                    if (cust != null)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = cust.Customer_Name,
                            Value = cust.Customer_Id
                        };
                        cmbCustomerName.Items.Add(_cmbItem);
                        cmbCustomerName.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void NextFocus(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                General.NextFocus(sender, e);

            }
        }
        private void CouponSaleForm_Load(object sender, EventArgs e)
        {
            button = new CouponButtonCollection
            {
                BtnNew = btnNew,
                BtnCancel = btnCancel,
                BtnClose = btnClose,
                BtnSave = btnSave,
                BtnGen = btnGenerateLeafs,
                BtnDelete=btnDelete
            };
            ButtonActive(EnumFormEvents.FormLoad);
            GetAllCustomers();
            button.BtnDelete.Visible = false;

        }

        private void gridDeliveries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {

                    int _couponId = Convert.ToInt32(gridDeliveries["clmCouponId", e.RowIndex].Value);
                    this.couponId = _couponId;
                    this.bookNumber = gridDeliveries["clmBookno", e.RowIndex].Value.ToString();
                    GetCoupon(_couponId);
                    ButtonActive(EnumFormEvents.Update);
                    button.BtnDelete.Visible = true;

                    button.BtnDelete.Enabled = true;

                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                    General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
                }
            }
        }

        private void gridCouponLeafs_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex<3)
                e.Cancel = true;
            if (gridCouponLeafs.CurrentRow.DefaultCellStyle.BackColor == System.Drawing.Color.Orange)
                    e.Cancel = true;
           
        }
        private void DecimalOnly(object sender, KeyPressEventArgs e)
        {
            General.TxtOnlyDecimal(sender, e);
        }

        private void lnkReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CouponReportForm cpr = new CouponReportForm();
            cpr.ShowDialog();
        }

        private void txtSearchCoupnBook_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchCoupnBook.Text.Length > 1)
            {
                Search();
            }
            else if (txtSearchCoupnBook.Text.Length == 0)
                Search();
        }
    }


    class CouponButtonCollection
    {
        public Button BtnNew { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnGen { get; set; }
        public Button BtnDelete { get; set; }
    }
}
