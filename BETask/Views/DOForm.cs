using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;
using objDAL=BETask.DAL.DAL;
using System.ComponentModel;
using System.Drawing;

namespace BETask.Views
{

    public partial class DOForm : Form
    {
        DOButtonCollection button;
        BAL.CustomerBAL _customerBAL = new BAL.CustomerBAL();
        BAL.ItemBAL itemBAL = new BAL.ItemBAL();
        BAL.SaleBAL saleBAL = new BAL.SaleBAL();
        BAL.DOSaleBAL DOSaleBAL = new BAL.DOSaleBAL();
        List<EDMX.sales> listSales = new List<EDMX.sales>();
        List<EDMX.sales> lstTempDOsales = new List<EDMX.sales>();
        List<EDMX.do_sales> listDOSale = new List<EDMX.do_sales>();
        string customerName = "",doInvoice="";
        int customerId = 0, doId = 0, gridSelectedRow = 0, saleId = 0;
  
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Load,
            Save,
            New,
            Print,
            DivisionClose,
            DivisionApply,
            LeafClose,
            LeafApply
        }
        public DOForm()
        {
            InitializeComponent();
        }
        public void ResetForms()
        {
            try
            {
                General.ClearTextBoxes(this);
                dtpDateFrom.Value = dtpDateTo.Value = dtpDOdate.Value = dtpSearchDate.Value = DateTime.Today;
                dtpDOdate.Focus();
                cmbRoute.SelectedIndex = -1;
                txtDoNo.Text = string.Empty;
                txtDoNo.Text = "0";
                lnkSelectAll.Text = "Select All";
                General.ClearGrid(dgvDoSalesItemDetails);
                General.ClearGrid(dgDOSales);
                pnlCustomerSaleDetails.Visible = false;
                lnkSelectAll.Visible = false;
                btnSave.Enabled = false;
                btnPrint.Visible = false;
              
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GetAllRoutes()
        {
            try
            {
                BAL.RouteBAL routeBAL = new BAL.RouteBAL();
                List<EDMX.route> listRoute = routeBAL.GetAllRoutes();

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
        private void FormLoad()
        {
            button = new DOButtonCollection
            {
                BtnSave = btnSave,
                BtnSearch = btnSearch,
                BtnLoad = btnLoad,
                BtnClose = btnClose,
                BtnCancel = btnCancel,
                BtnNew = btnNew,
                BtnPrint = btnPrint,
                BtnDivisionApply=btnDivisionApply,
                BtnDivisionClose=btnDivisiobClose,
                BtnLeafApply = btnLeafApply,
                BtnLeafClose = btnLeafClose
            };
            ResetForms();
            GetAllRoutes();
            GetGroupCustomer();

        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnNew)
            {
                ButtonActive(EnumFormEvents.New);
            }
            if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
            if (sender == button.BtnLoad)
            {
                ButtonActive(EnumFormEvents.Load);
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
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnDivisionClose)
            {
                ButtonActive(EnumFormEvents.DivisionClose);
            }
            else if (sender == button.BtnDivisionApply)
            {
                ButtonActive(EnumFormEvents.DivisionApply);
            }
            else if (sender == button.BtnLeafClose)
            {
                ButtonActive(EnumFormEvents.LeafClose);
            }
            else if (sender == button.BtnLeafApply)
            {
                ButtonActive(EnumFormEvents.LeafApply);
            }
        }
        private void ButtonActive(Enum activeEvent)
        {
            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    ResetForms();
                    btnNew.Enabled = true;
                    break;
                case EnumFormEvents.Cancel:
                    ButtonActive(EnumFormEvents.FormLoad);
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;
                case EnumFormEvents.Load:
                    LoadDOSales();
                    break;
                case EnumFormEvents.Search:
                    Search();
                    btnNew.Enabled = false;
                    btnSave.Enabled = false;
                    gbLoadDO.Visible = false;
                         
                    break;
                case EnumFormEvents.Save:
                    SaveDOSale();
                    break;
                case EnumFormEvents.New:
                    //btnSave.Enabled = true;
                    btnNew.Enabled = false;
                    gbLoadDO.Visible = true;
                    break;
                case EnumFormEvents.Print:
                    PrintSavedDOList();
                    break;
                case EnumFormEvents.DivisionApply:
                    ApplyDivisionFilter();
                    break;
                case EnumFormEvents.DivisionClose:
                    pnlDivision.Hide();
                    break;
                case EnumFormEvents.LeafClose:
                    pnlDeliveryLeaf.Hide();
                    break;
                case EnumFormEvents.LeafApply:
                    UpdateDeliveryLeaf();
                    break;
                default:
                    break;

            }
        }
        private void DOForm_Load(object sender, EventArgs e)
        {
            FormLoad();
            General.SetScreenSize(sender,e,this);

        }
        private void PrintSavedDOList()
        {
            try
            {
                General.ClearGrid(dgDOSales);
                General.ClearGrid(dgvDoSalesItemDetails);

                listDOSale = DOSaleBAL.SearchDOSales(General.ConvertDateServerFormat(dtpSearchDate.Value));
                if (listDOSale != null && listDOSale.Count > 0)
                {
                    DOSaleBAL.PrintSavedDOList(listDOSale);                   
                }
                else
                {                  
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No DOSales found");
                }
                btnPrint.Visible = false;
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
                General.ClearGrid(dgDOSales);
                General.ClearGrid(dgvDoSalesItemDetails);

                listDOSale = DOSaleBAL.SearchDOSales(General.ConvertDateServerFormat(dtpSearchDate.Value));
                if (listDOSale != null && listDOSale.Count > 0)
                {
                    PopulateSavedDOSale(listDOSale);
                    btnPrint.Visible = true;
                }
                else
                {
                    btnPrint.Visible = false;
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No DOSales found");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private string ValidateForm()
        {
            string errorMsg = string.Empty;
            if (dgDOSales.Rows.Count < 1)
                errorMsg = "Please Select DO sales";
            return errorMsg;
        }
        /// <summary>
        /// 
        /// </summary>
        private void LoadDOSales()
        {
            try
            {
                General.ClearGrid(dgDOSales);
                General.ClearGrid(dgvDoSalesItemDetails);
                int routeId = 0,groupId=0;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (cmbGroup.Text != "")
                {
                    Object selectedRoute = cmbGroup.SelectedItem;
                    groupId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                listSales = DOSaleBAL.SearchDOinSales(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), routeId, groupId);
                if (listSales != null && listSales.Count > 0)
                {                  
                    PopulateDOSale(listSales);
                    btnSave.Enabled = true;
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No DOSales found");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }        
        /// <summary>
        /// save sale data to do_sale table
        /// </summary>
        private void SaveDOSale()
        {
            try
            {
                string errorMessage = ValidateForm();
                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (General.ShowMessageConfirm() == DialogResult.Yes && General.CheckFinancialDate(dtpDOdate.Value))
                    {
                        string saleNumber = "";
                        int res = SaveDOSaleDetails(ref saleNumber);
                        if (res > 0)
                        {
                            General.ShowMessage(General.EnumMessageTypes.Success, $"DO Sale  Successfully Saved. Invoice Number : {saleNumber}");
                            ButtonActive(EnumFormEvents.Cancel);
                            ResetForms();
                        }
                    }

                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, errorMessage);
                }
            }
            catch (Exception ee)
            {
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private int SaveDOSaleDetails(ref string saleNumber)
        {
            int _savedId = 0;
            int _CustomerId = 0;
            List<EDMX.do_sales> _listDOSale = new List<EDMX.do_sales>();
            try
            {
                int routeId = 0,groupId=0;
                bool itemSelected = false;

                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                if (cmbGroup.Text != "")
                {
                    Object selectedGroup = cmbGroup.SelectedItem;
                    groupId = (int)((BETask.Views.ComboboxItem)selectedGroup).Value;
                }

                if (routeId == 0 && groupId==0)
                {
                    General.ShowMessage(General.EnumMessageTypes.Error, "Please select route or group");
                    cmbRoute.Focus();
                    return 0;
                }

                foreach (DataGridViewRow dr in dgDOSales.Rows)
                {

                    if (dr.Cells["chkSelect"].Value != null && Convert.ToBoolean(dr.Cells["chkSelect"].Value) != false)
                    {
                        _CustomerId = Convert.ToInt32(dr.Cells["clmCustomerId"].Value);
                        List<int> divisionIds = GenerateDivisionIds(dr.Index);
                        List<EDMX.do_sales_item> _listDOSaleItem = GetDOSaleItems(_CustomerId, divisionIds);
                        EDMX.do_sales _doSale = new EDMX.do_sales()
                        {
                            do_date = General.ConvertDateServerFormatWithCurrentTime(dtpDOdate.Value),
                            //do_invoice_number = objDAL.DocumentSerialDAL.GetNextDocument(objDAL.DocumentSerialDAL.EnumDocuments.DOINV.ToString()),
                            route_id = routeId,
                            customer_id = _CustomerId,
                            gross_amount = _listDOSaleItem.Sum(x => x.gross_amount),
                            total_discount = _listDOSaleItem.Sum(x => x.total_discount),
                            total_beforevat = _listDOSaleItem.Sum(x => x.total_beforevat),
                            total_vat = _listDOSaleItem.Sum(x => x.total_vat),
                            net_amount = _listDOSaleItem.Sum(x => x.net_amount),
                            remarks = txtRemarks.Text,
                            status = 1,
                            do_sales_item = _listDOSaleItem,
                            group_id= groupId
                        };
                        itemSelected = true;
                        _listDOSale.Add(_doSale);
                    }
                }
                if (itemSelected)
                {
                    _savedId = DOSaleBAL.SaveDOSale(_listDOSale,ref saleNumber);
                    Clipboard.SetText(saleNumber);
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Success, "No item Selected");
                }
            }
            catch
            {
                _savedId = -1;
                General.ShowMessage(General.EnumMessageTypes.Error, " Please try again or contact for support");
                throw;
            }

            return _savedId;
        }
        /// <summary>
        /// print do invoice 
        /// </summary>
        /// <param name="doId"></param>
        private void PrintDOInvoice(int doId)
        {
            try
            {

                DOSaleBAL.PrintDOSaleInvoice(doId);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        /// <summary>
        /// get all sale data from sales table 
        /// </summary>
        /// <param name="_CustomerId"></param>
        /// <returns></returns>
        private List<EDMX.do_sales_item> GetDOSaleItems(int _CustomerId,List<int> divisionIds)
        {
            List<EDMX.do_sales_item> listDOSales = new List<EDMX.do_sales_item>();
            EDMX.do_sales_item _doSaleItem = null;
            try
            {


                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object selectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                }

                listSales = DOSaleBAL.SearchSaleWithCustomer(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), _CustomerId, routeId, divisionIds);
                foreach (EDMX.sales ps in listSales)
                {
                    _doSaleItem = new EDMX.do_sales_item()
                    {
                        sales_id = ps.sales_id,
                        do_number = ps.do_number,
                        //do_number = ps.delivery_leaf,
                        gross_amount = ps.gross_amount,
                        total_discount = ps.total_discount,
                        total_vat = ps.total_vat,
                        total_beforevat = ps.total_beforevat,
                        net_amount = ps.net_amount,
                        status = 1,
                    };
                    listDOSales.Add(_doSaleItem);
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
            return listDOSales;
        }
        /// <summary>
        /// calculate total data grid checked rows
        /// </summary>
        private void CalucualteGridSelectedTotal()
        {
            int[] Customer = new int[dgDOSales.Rows.Count];
            int count = 0;
            int index = 0;         
            foreach (DataGridViewRow dr in dgDOSales.Rows)
            {
                if (Convert.ToInt32(dr.Cells["clmCustomerId"].Value) > 0)
                {
                    if (Convert.ToBoolean(dr.Cells["chkSelect"].Value) != false)
                    {
                        Customer[count] = Convert.ToInt32(dr.Cells["clmCustomerId"].Value);
                        count++;                  
                        lnkSelectAll.Text = "Deselect All";
                    }
                    else{ lnkSelectAll.Text = "Select All"; }
                }
                else
                {
                    dgDOSales.Rows.Remove(dgDOSales.Rows[index]);
                }
                index++;
            }

            dgDOSales.Rows.Add(0, 0, "", false, "Selected Total", lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.gross_amount), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.total_discount), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.total_beforevat), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.total_vat), lstTempDOsales.Where(x => Customer.Contains(x.customer_id)).Sum(x => x.net_amount));
            dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
            dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
            dgDOSales.Rows[dgDOSales.Rows.Count - 1].ReadOnly = true;
        }
        /// <summary>
        /// load data from do_sales table
        /// </summary>
        /// <param name="lstDOsales"></param>
        private void PopulateSavedDOSale(List<EDMX.do_sales> lstDOsales)
        {
            try
            {
                General.ClearGrid(dgDOSales);
                foreach (EDMX.do_sales dos in lstDOsales)
                {
                   dgDOSales.Rows.Add(dos.do_id,dos.customer_id, dos.do_invoice_number, true, dos.customer.customer_name, dos.gross_amount, dos.total_discount, dos.total_beforevat, dos.total_vat, dos.net_amount,"+","Print");
               
                }
                //  CalucualteGridSelectedTotal();
                dgDOSales.Columns["clmPrint"].Visible = true;
                dgDOSales.Columns["chkSelect"].Visible = false;
                dgDOSales.Columns["clmLinkDetails"].Visible = true;
                dgDOSales.Columns["clmDoInvNo"].Visible = true;
                lnkSelectAll.Visible = false;
                dgDOSales.Rows.Add(0,0, "", false ,"Total", lstDOsales.Sum(x => x.gross_amount), lstDOsales.Sum(x => x.total_discount), lstDOsales.Sum(x => x.total_beforevat), lstDOsales.Sum(x => x.total_vat), lstDOsales.Sum(x => x.net_amount));
                dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                dgDOSales.Rows[dgDOSales.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                dgDOSales.Rows[dgDOSales.Rows.Count - 1].ReadOnly = true;

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        /// <summary>
        /// load data from sales table
        /// </summary>
        /// <param name="lstsales"></param>
        private void PopulateDOSale(List<EDMX.sales> lstsales)
        {
            try
            {
                General.ClearGrid(dgDOSales);
                lstTempDOsales = lstsales;
                foreach (EDMX.sales dos in lstsales)
                {
                    dgDOSales.Rows.Add(0,dos.customer_id,"",true, dos.remarks, dos.gross_amount, dos.total_discount, dos.total_beforevat, dos.total_vat,dos.net_amount,"+","","Division");
                    
                }
                dgDOSales.Columns["clmPrint"].Visible = false;
                dgDOSales.Columns["chkSelect"].Visible = true;
                dgDOSales.Columns["clmDoInvNo"].Visible = false;
                dgDOSales.Columns["clmLinkDetails"].Visible = true;
                lnkSelectAll.Visible = true;
                //dgDOSales.Rows.Add(0, false, "Total", lstDOsales.Sum(x => x.gross_amount), lstDOsales.Sum(x => x.total_discount), lstDOsales.Sum(x => x.total_beforevat), lstDOsales.Sum(x => x.total_vat), lstDOsales.Sum(x => x.net_amount));                

                CalucualteGridSelectedTotal();
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }
        /// <summary>
        /// load data from sales item table
        /// </summary>
        /// <param name="lstSaleItems"></param>
        private void PopulateDOSaleItems(List<EDMX.sales> lstSaleItems)
        {
            try
            {
                EDMX.item objItem = new EDMX.item();                
                General.ClearGrid(dgvDoSalesItemDetails);               
                int count = 0;
                foreach (EDMX.sales ps in lstSaleItems)
                {
                    string division = ps.customer_division != null ? ps.customer_division.division_name : "";
                    dgvDoSalesItemDetails.Rows.Add(ps.sales_id, "", "", "", $"{General.ConvertDateAppFormat(ps.sales_date)} DO No:{ ps.delivery_leaf}", "", "", "", "", "", "", ps.net_amount);

                    dgvDoSalesItemDetails.Rows[count].DefaultCellStyle.BackColor = Color.Gainsboro;
                    //  dgvDoSalesItemDetails.Rows[count].DefaultCellStyle.ForeColor= Color.Blue;
                    foreach (EDMX.sales_item pi in ps.sales_item)
                    {
                        // objItem = itemBAL.GetItemDetails(pi.item_id);
                        dgvDoSalesItemDetails.Rows.Add(ps.sales_id, "", ps.do_id, pi.item_id, pi.item.item_name, "", pi.qty, pi.rate, pi.discount, pi.gross_amount, pi.vat_amount, pi.rate > 0 ? pi.net_amount : 0);
                        count++;

                    }
                    count++;

                }
                pnlCustomerSaleDetails.Visible = true;
               
                pnlCustomerSaleDetails.Dock = DockStyle.Fill;
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }                     
        private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlCustomerSaleDetails.Visible = false;
            lnkSelectAll.Visible = true;
            //dgDOSales.Visible = true;
        } 
        private void lnkSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           if(dgDOSales.Rows.Count>0)
            {
                foreach(DataGridViewRow dr in dgDOSales.Rows)
                {
                    if (Convert.ToInt32(dr.Cells["clmCustomerId"].Value) > 0)
                    {
                        if (lnkSelectAll.Text.ToUpper() == "SELECT ALL")
                        { dr.Cells["chkSelect"].Value = true; }
                        else { dr.Cells["chkSelect"].Value = false; }
                    }
                }
                if (lnkSelectAll.Text.ToUpper() == "SELECT ALL")
                { lnkSelectAll.Text = "Deselect All";}
                else { lnkSelectAll.Text = "Select All"; }
                CalucualteGridSelectedTotal();
            }
            

        }
        private void dgDOSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0 && e.ColumnIndex>0)
                {
                    int _doId = Convert.ToInt32(dgDOSales["clmDOId", e.RowIndex].Value);
                    int _CustomerId = Convert.ToInt32(dgDOSales["clmCustomerId", e.RowIndex].Value);
                    string _CustomerName = Convert.ToString(dgDOSales["clmCustomerName", e.RowIndex].Value);
                    this.doInvoice = Convert.ToString(dgDOSales["clmDoInvNo", e.RowIndex].Value);
                    this.customerId = _CustomerId;
                    this.customerName = _CustomerName;
                    this.doId = _doId;
                    if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value) == "+")
                    {
                        int routeId = 0;
                        if (cmbRoute.Text != "")
                        {
                            Object selectedRoute = cmbRoute.SelectedItem;
                            routeId = (int)((BETask.Views.ComboboxItem)selectedRoute).Value;
                        }


                        //Listing Saved DO Invoiced item details
                        if (_doId > 0)
                        {
                            if (chkNewSale.Checked)
                                listSales = DOSaleBAL.SearchSalesByDOId(_doId, _CustomerId, routeId);
                            else
                            {
                                List<int> divisionId = GenerateDivisionIds(e.RowIndex);
                                listSales = DOSaleBAL.SearchSaleWithCustomer(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), _CustomerId, routeId, divisionId, false);

                            }
                        }

                        //Listing of item details datewaise , not saved
                        else
                        {
                            List<int> divisionIds = GenerateDivisionIds(e.RowIndex);
                            listSales = DOSaleBAL.SearchSaleWithCustomer(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), _CustomerId, routeId, divisionIds); }

                        PopulateDOSaleItems(listSales);
                        lblSaleDetailsCaption.Text = $"DO Sale Deatails for {_CustomerName}";
                        lnkSelectAll.Visible = false;
                        //dgDOSales.Visible = false;

                    }
                    else if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "PRINT" || Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "PRINTED")
                    {
                        bool print = true;
                        if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "PRINTED")
                        {
                            print = General.ShowMessageConfirm("Already printed. do you want to print again") == DialogResult.Yes ? true : false;
                        }
                        if (print)
                        {
                             _doId = Convert.ToInt32(dgDOSales["clmdoId", e.RowIndex].Value);
                            PrintDOInvoice(_doId);
                            dgDOSales[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Blue;
                            dgDOSales[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.White;
                        }
                        dgDOSales[e.ColumnIndex, e.RowIndex].Value = "Printed";
                        //CalucualteGridSelectedTotal();
                    }
                    else if (Convert.ToString(dgDOSales[e.ColumnIndex, e.RowIndex].Value).ToUpper() == "DIVISION")
                    {
                        //To collect division of customer
                        gridSelectedRow = e.RowIndex;
                        if (!pnlDivision.Visible)
                            FillDivision();
                        else
                            pnlDivision.Hide();
                        
                        
                    }

                }
                
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private List<int> GenerateDivisionIds(int rowIndex)
        {
            char[] seperator = { ',' };
            string[] divisions = dgDOSales["clmDivision", rowIndex].ToolTipText.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            List<int> divisionIds = new List<int>();
            if (divisions != null && divisions.Length > 0)
            {
                foreach (string s in divisions)
                    divisionIds.Add(Convert.ToInt32(s));
            }
            else { divisionIds = null; }
            return divisionIds;
        }
        private void dgDOSales_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgDOSales.CommitEdit(DataGridViewDataErrorContexts.Commit);
            CalucualteGridSelectedTotal();
        }

        private void linkPrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PrintDeliveryItems();
        }

        private void ApplyDivisionFilter()
        {
            try
            {
                string selectedDivision = "";
              foreach(string item in chkDivisions.Items)
                {
                   bool isChecked= chkDivisions.GetItemChecked(chkDivisions.FindStringExact(item));
                    if (isChecked)
                    {
                        if (item.Contains("||"))
                        {
                            string[] seperator = { "||" };
                            selectedDivision += $"{item.Split(seperator, StringSplitOptions.RemoveEmptyEntries)[1]},";
                            isChecked = false;
                        }
                    }
                }
                dgDOSales["clmDivision", gridSelectedRow].ToolTipText = selectedDivision;
                pnlDivision.Hide();


                if (!string.IsNullOrEmpty(dgDOSales["clmDivision", gridSelectedRow].ToolTipText))
                {
                   
                    List<int> divisionIds = GenerateDivisionIds(gridSelectedRow);
                    //Filter 
                    int customerId = Convert.ToInt32(dgDOSales["clmCustomerId", gridSelectedRow].Value);
                    List<EDMX.sales> listSalesCust = DOSaleBAL.SearchDOinSalesFillerDivision(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), customerId, divisionIds);

                    //Update Grid
                    if (listSalesCust != null && listSalesCust.Count > 0)
                    {
                        foreach (EDMX.sales dos in listSalesCust)
                        {
                            dgDOSales["clmGross", gridSelectedRow].Value = dos.gross_amount;
                            dgDOSales["Discount", gridSelectedRow].Value = dos.total_discount;
                            dgDOSales["clmTotal", gridSelectedRow].Value = dos.total_beforevat;
                            dgDOSales["clmVat", gridSelectedRow].Value = dos.total_vat;
                            dgDOSales["Net", gridSelectedRow].Value = dos.net_amount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to apply division filter");
            }
        }

        private void dgvDoSalesItemDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.saleId = Convert.ToInt32(dgvDoSalesItemDetails["colSalesId", e.RowIndex].Value);
                pnlDeliveryLeaf.Show();
                txtLeafNo.Focus();
                txtLeafNo.Tag = e.RowIndex;
            }
        }

        private void chkNewSale_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNewSale.Checked)
                gbLoadDO.Visible = true;
            else
                gbLoadDO.Visible = false;
        }


        private void GetGroupCustomer()
        {
            try
            {
               var listGroup = _customerBAL.GetGroupCustomer(null);
                if (listGroup != null && listGroup.Count > 0)
                {
                    foreach (var group in listGroup)
                    {
                        ComboboxItem _cmbItem = new ComboboxItem()
                        {
                            Text = group.GroupName,
                            Value = group.GroupId
                        };
                        cmbGroup.Items.Add(_cmbItem);
                    }
                    cmbGroup.SelectedIndex = -1;

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
                DAL.DAL.SaleDAL customerDAL = new DAL.DAL.SaleDAL();
                List<EDMX.customer_division> listDivision = customerDAL.GetCustomerDivisionFromSale(this.customerId, General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value));
                if (pnlDivision != null)
                {
                    chkDivisions.Items.Clear();
                    
                    foreach (EDMX.customer_division dv in listDivision)
                    {
                        if (string.IsNullOrEmpty( dgDOSales["clmDivision", gridSelectedRow].ToolTipText))
                            chkDivisions.Items.Add($"{dv.division_name.PadRight(100)}||{dv.division_id}", true);
                        else
                        {
                            bool sel = dgDOSales["clmDivision", gridSelectedRow].ToolTipText.ToString().Contains(dv.division_id.ToString()) ? true : false; ;
                            chkDivisions.Items.Add($"{dv.division_name.PadRight(100)}||{dv.division_id}", sel);
                        }
                       
                    }
                    if(chkDivisions.Items.Count>0)
                    pnlDivision.Show();
                }
                else
                    pnlDivision.Hide();

            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to load divisions");
            }
        }

        private void UpdateDeliveryLeaf()
        {
            try
            {
                string deliveryLeaf = txtLeafNo.Text;
                if (!string.IsNullOrEmpty(deliveryLeaf))
                {
                    DOSaleBAL.UpateDeliveryLeaf(saleId,deliveryLeaf);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Succesfully updated");
                    
                    pnlDeliveryLeaf.Hide();
                    if (txtLeafNo.Tag != null)
                    {
                        int row = Convert.ToInt32(txtLeafNo.Tag);
                        if (row >= 0)
                        {
                            dgvDoSalesItemDetails["colQty",row].Value = txtLeafNo.Text;
                        }
                    }
                    txtLeafNo.Clear();
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Something went wrong. Unable to update leaf");
            }
        }

        private void PrintDeliveryItems()
        {
            try
            {
                if (dgvDoSalesItemDetails.Rows.Count > 0 && this.doId > 0)
                {
                    string header = $"Invoiced - Do delivery items - {this.customerName}";
                    string subhead = $"Do invoice:{this.doInvoice}";
                    DOSaleBAL.PrintDoDeliveryItems(this.doId, this.customerId, header, subhead, General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithStartTime(dtpDateTo.Value), chkNewSale.Checked);
                }
                else
                {
                    PrintDeliveryItemsNonInvoiced();
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Somethong wen wrong. Unable to print");
            }
        }
        private void PrintDeliveryItemsNonInvoiced()
        {
            try
            {
                if (dgvDoSalesItemDetails.Rows.Count > 0 )
                {
                    string header = $"Do delivery items - {this.customerName}";
                    string subhead = $"Not invoiced {General.ConvertDateAppFormat( dtpDateFrom.Value)} to {General.ConvertDateAppFormat(dtpDateTo.Value)}";
                    List<int> deliveryIds = GenerateDivisionIds(gridSelectedRow);
                    DOSaleBAL.PrintDoDeliveryItemsNoInvoiced(dtpDateFrom.Value,dtpDateTo.Value, this.customerId, header, subhead, deliveryIds);
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "Nothing to print");
                }
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Somethong wen wrong. Unable to print");
            }
        }
    }
    class DOButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnLoad { get; set; }
        public Button BtnSave { get; set; }
        public Button BtnNew { get; set; }
        public Button BtnPrint { get; set; }
        public Button BtnDivisionClose { get; set; }
        public Button BtnDivisionApply { get; set; }
        public Button BtnLeafClose { get; set; }
        public Button BtnLeafApply { get; set; }
    }
}
