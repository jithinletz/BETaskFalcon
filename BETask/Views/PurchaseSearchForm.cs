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
    public partial class PurchaseSearchForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
        }
        public int purchaseId = 0;
        PurchaseBAL purchaseBAL = new PurchaseBAL();
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        List<CustomerModel> _lstCustomers = null;
        PurchaseSearchButtonCollection button ;
        bool isReturn = false,isOrder=false;


        public PurchaseSearchForm()
        {
            InitializeComponent();
        }
        public PurchaseSearchForm(bool purchaseReturn,bool _isOrder=false)
        {
            InitializeComponent();
            isReturn = true;
            isOrder = _isOrder;
        }
        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    Search();
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    cmbSupplier.Text = string.Empty;
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;

                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            else if (sender == button.BtnCancel)
            {
                ButtonActive(EnumFormEvents.Cancel);
            }

            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
        }
        private void submitItem()
        {
            try
            {
                if (gridPurchase.Rows.Count > 0)
                {
                    int ridx = gridPurchase.CurrentRow.Index;
                    int _purchaseId = 0;
                    int.TryParse(gridPurchase[ridx, 0].Value.ToString(), out _purchaseId);
                    purchaseId = _purchaseId;
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
        }
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
        private void Search()
        {
            try
            {
               
                General.ClearGrid(gridPurchase);
                int vendorId = 0;
                if (!String.IsNullOrEmpty(cmbSupplier.Text))
                    int.TryParse(_lstCustomers.Where(x => x.Customer_Name == cmbSupplier.Text).FirstOrDefault().Customer_Id.ToString(), out vendorId);

                if (!isReturn && !isOrder)
                {
                    
                    List<EDMX.purchase> listPurchase = purchaseBAL.SearchPurchase(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId);
                    foreach (EDMX.purchase purchase in listPurchase)
                    {
                        gridPurchase.Rows.Add(purchase.purchase_id, purchase.customer.customer_name, General.ConvertDateAppFormat(purchase.invoice_date), purchase.invoice_number, purchase.net_amount);
                    }
                }
                else
                {
                    if(isOrder)
                    {
                        BAL.PurchaseOrderBAL purchaseOrderBal = new BAL.PurchaseOrderBAL();
                        List<EDMX.purchase_order> listPurchaseReturn = purchaseOrderBal.SearchPurchase(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId);
                        foreach (EDMX.purchase_order purchase in listPurchaseReturn)
                        {
                            gridPurchase.Rows.Add(purchase.purchase_id, purchase.customer.customer_name, General.ConvertDateAppFormat(purchase.invoice_date), purchase.purchase_id, purchase.net_amount);
                        }
                    }
                    else 
                    {
                        BAL.PurchaseReturnBAL purchaseReturnBal = new BAL.PurchaseReturnBAL();
                        List<EDMX.purchase_return> listPurchaseReturn = purchaseReturnBal.SearchPurchaseReturn(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), vendorId);
                        foreach (EDMX.purchase_return purchase in listPurchaseReturn)
                        {
                            gridPurchase.Rows.Add(purchase.purchase_return_id, purchase.customer.customer_name, General.ConvertDateAppFormat(purchase.invoice_date), purchase.invoice_number, purchase.net_amount);
                        }
                    }
                    
                }
               

            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error);
                this.BeginInvoke(new MethodInvoker(Close));
            }
            
        }
        private void gridPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex>=0)
            {
                try
                {
                    int _purchaseId = 0;
                    int.TryParse(gridPurchase[0,e.RowIndex].Value.ToString(), out _purchaseId);
                    purchaseId = _purchaseId;
                    this.DialogResult = DialogResult.OK;
                    try
                    {
                        this.Close();
                    }
                    catch
                    {
                        this.BeginInvoke(new MethodInvoker(Close));

                    }
                }
                catch (Exception ee)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error);
                    this.BeginInvoke(new MethodInvoker(Close));
                }
            }

        }
     

        private void gridPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                submitItem();
            }
        }

        private void PurchaseSearchForm_Load(object sender, EventArgs e)
        {
            button = new PurchaseSearchButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnCancel = btnCancel,
                BtnClose = btnClose
            };
            Search();
            GetAllSuppliers();

        }
        class PurchaseSearchButtonCollection
        {
            public Button BtnSearch { get; set; }
            public Button BtnCancel { get; set; }
            public Button BtnClose { get; set; }
          
        }
    }
}
