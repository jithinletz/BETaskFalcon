using BETask.Common;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BETask.BAL;
using EDMX = BETask.DAL.EDMX;
using System.Data;

namespace BETask.Views
{
    public partial class CustomerDeliveryReportStaffwise : Form
    {

        public enum EnumFormEvents
        {
            FormLoad,
            Close,
            Print,
            PrintCollection,
            Save,
            Hide,
            Search

        }
        int deliveryNo { get; set; }
        int routeId { get; set; }
        int oldLeafCount = 0;
        decimal oldLeafAmount = 0;
        CustomerDeliveryDaybookCollection button;
        int itemId { get; set; }
        EDMX.delivery delivery = null;
        DeliveryBAL deliveryBAL = new DeliveryBAL();
        DataTable tblDelivery, tblDetail, tblSumamry = new DataTable();
        List<EDMX.delivery_item_summary> listDeliveryIitemSummary = null;
        public CustomerDeliveryReportStaffwise(int _deliveryNo, bool routeWise = false)
        {
            InitializeComponent();
            this.deliveryNo = _deliveryNo;
            GetDeliveryDetaiils();
            if (!routeWise)
                routeId = 0;
            LoadProducts();
            LoadDeliveryData();
            Application.DoEvents();
            //if (General.viewLoadingDsr)
            //    LoadDeliveryDetails();
        }


        private void ButtonActive(Enum activeEvent)
        {

            switch (activeEvent)
            {
                case EnumFormEvents.FormLoad:
                    if (this.deliveryNo > 0)
                    {
                        ReRun(false);
                    }
                    break;
                case EnumFormEvents.Save:
                    UpdateAdditionalQty();
                    break;
                case EnumFormEvents.Hide:
                    pnlAddQty.Hide();
                    break;
                case EnumFormEvents.Close:
                    this.DialogResult = DialogResult.OK;
                    this.BeginInvoke(new MethodInvoker(Close));
                    break;

                case EnumFormEvents.Print:
                    Print();
                    break;
                case EnumFormEvents.Search:
                    Search();
                    break;
                case EnumFormEvents.PrintCollection:
                    PrintCollection();
                    break;
                default:
                    break;

            }
        }
        private void ButtonEvents(object sender, EventArgs e)
        {
            if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
            else if (sender == button.BtnClose)
            {
                ButtonActive(EnumFormEvents.Close);
            }
            else if (sender == button.BtnSaveAddQty)
            {
                ButtonActive(EnumFormEvents.Save);
            }
            else if (sender == button.BtnHide)
            {
                ButtonActive(EnumFormEvents.Hide);
            }
            else if (sender == button.BtnPrintColl)
            {
                ButtonActive(EnumFormEvents.PrintCollection);
            }
            else if (sender == button.BtnSearch)
            {
                ButtonActive(EnumFormEvents.Search);
            }
        }
        private void GetDeliveryDetaiils()
        {
            try
            {
                DataSet ds = deliveryBAL.GetDelivery(deliveryNo);
                tblDelivery = ds.Tables[0];
                tblDetail = ds.Tables[1];
                tblSumamry = ds.Tables[2];

                this.routeId = Convert.ToInt32(tblDelivery.Rows[0]["route_id"]);

                if (Convert.ToString(tblDelivery.Rows[0]["remarks"]).ToLower().Contains("cash.collected"))
                {
                    chkCashCollected.Enabled = false;
                    chkCashCollected.Checked = true;

                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }

        private void LoadDeliveryData()
        {
            if (this.tblDelivery != null && tblDelivery.Rows.Count > 0)
            {
                DataRow dr = tblDelivery.Rows[0];
                lblDate.Text = General.ConvertDateAppFormat(dr["delivery_date"].ToString());
                lblSalesman.Text = $"{dr["first_name"]} {dr["last_name"].ToString()}";
                lblVehicle.Text = Convert.ToString(dr["vehicle_no"]);

                //Route
                EmployeeBAL employeeBAL = new EmployeeBAL();
                var employee = employeeBAL.GetAllEmployeeDetails(Convert.ToInt32(dr["employee_id"]));
                lblRoute.Text = employee.route.route_name;

                //Item Counts
                //var foc = delivery.delivery_items.ToList().Where(x => x.rate <= 0 && x.qty > 0 && x.item_id == itemId && x.delivery_time != null).Sum(i => i.delivered_qty);
               

                var sumDeliveredQty = tblDetail.AsEnumerable()
                              .Where(row => Convert.ToDecimal(row["rate"]) <= 0
                                         && Convert.ToInt32(row["qty"]) > 0
                                         && Convert.ToInt32(row["item_id"]) == itemId
                                         && row["delivery_time"] != DBNull.Value)
                              .Sum(row => Convert.ToDecimal(row["delivered_qty"]));
                lblFOC.Text = sumDeliveredQty.ToString();


                //var sale = delivery.delivery_items.ToList().Where(x => x.net_amount >= 0 && x.item_id == itemId && x.delivery_time != null).Sum(i => i.delivered_qty);

                var sumDeliveredSale = tblDetail.AsEnumerable()
                              .Where(row => Convert.ToInt32(row["net_amount"]) > 0
                                         && Convert.ToInt32(row["item_id"]) == itemId
                                         && row["delivery_time"] != DBNull.Value)
                              .Sum(row => Convert.ToDecimal(row["delivered_qty"]));
                lblSale.Text = sumDeliveredSale.ToString();


                //var loading = delivery.delivery_item_summary.ToList().Where(x => x.item_id == this.itemId).Sum(i => i.qty);
                var sumQty = tblSumamry.AsEnumerable()
                       .Where(row => Convert.ToInt32(row["item_id"]) == itemId)
                       .Sum(row => Convert.ToInt32(row["qty"]));
                lblLoading.Text = sumQty.ToString();

                //Delivery No
                lblDelivery.Text = this.deliveryNo.ToString();

                //Return
                var listReturn = deliveryBAL.GetDelliveryReturn(General.ConvertDateServerFormat(DateTime.Parse(dr["delivery_date"].ToString())), 4, Convert.ToInt32(dr["employee_id"]));
                var empty = listReturn.Where(x => x.item_id == this.itemId).Sum(i => i.qty);
                lblEmpty.Text = empty.ToString();
                LoadCustomer();
            }
        }
        private void LoadProducts()
        {
            try
            {
                DAL.DAL.CompanyDAL company = new DAL.DAL.CompanyDAL();
                int itemId = company.GetSystemSettings().default_item_id;
                string defaultItemName = "";
                if (itemId > 0)
                {
                    DAL.DAL.ItemDAL itemDAL = new DAL.DAL.ItemDAL();
                    defaultItemName = itemDAL.GetItemDteials(itemId).item_name;
                }

                List<EDMX.item> listProducts = new List<EDMX.item>();
                ItemBAL itemBAL = new ItemBAL();
                listProducts = itemBAL.GetDistinctDeliveryItemListByDate(this.deliveryNo);
                foreach (EDMX.item item in listProducts)
                {
                    ComboboxItem _cmbItem = new ComboboxItem()
                    {
                        Text = item.item_name,
                        Value = item.item_id
                    };
                    cmbProductName.Items.Add(_cmbItem);
                    if (this.itemId == 0)
                    {
                        this.itemId = item.item_id;
                        cmbProductName.SelectedIndex = 0;
                    }
                    if (!string.IsNullOrEmpty(defaultItemName))
                    {
                        if (listProducts.Any(x => x.item_name == defaultItemName))
                        {
                            cmbProductName.Text = defaultItemName;
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                //General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void LoadCustomer()
        {
            string err = "";

            try
            {
                DataRow row = tblDelivery.Rows[0];
                oldLeafCount = 0; oldLeafAmount = 0;
                General.ClearGrid(gridCustomer);
                System.Data.DataSet ds = new DataSet();
                if (this.routeId == 0)
                    ds = deliveryBAL.GetDeliverySalesReprot(Convert.ToInt32(row["employee_id"]), General.ConvertDateServerFormat(DateTime.Parse(row["delivery_date"].ToString())), General.ConvertDateServerFormat(DateTime.Parse(row["delivery_date"].ToString())), itemId, this.deliveryNo);
                else
                    ds = deliveryBAL.GetDeliverySalesReprotRouteWise(this.routeId, General.ConvertDateServerFormat(DateTime.Parse(row["delivery_date"].ToString())), General.ConvertDateServerFormat(DateTime.Parse(row["delivery_date"].ToString())), itemId, this.deliveryNo);

                List<EDMX.delivery_items> listItemsAll = deliveryBAL.GetDeliveryItemsByIdandCustomer(this.deliveryNo, 0);


                if (ds != null && ds.Tables.Count > 0)
                {
                    //Customer
                    DataTable tblCustomer = ds.Tables[0];
                    DataTable tblEmpty = ds.Tables[1];
                    DataTable tblCollection = ds.Tables[2];
                    DataTable tblCreditCollection = ds.Tables[3];
                    DataTable tblOldLeaf = ds.Tables.Count > 4 ? ds.Tables[4] : null;


                    if (tblCustomer != null && tblCustomer.Rows.Count > 0)
                    {

                        err = "";

                        foreach (DataRow dr in tblCustomer.Rows)
                        {
                            if (dr["customer_name"].ToString() == "SERCK SERVICES LLC (500 ML)")
                            {
                                string ss = "";
                            }
                            if (tblCustomer.Columns.Contains("leaf") && !string.IsNullOrEmpty(dr["leaf"].ToString()) && dr["payment_mode"].ToString() == "DO")
                                dr["customer_name"] = $"{dr["customer_name"]}\n\r{dr["leaf"]}";


                            object rate = Math.Round(decimal.Parse(dr["Rate"].ToString()), 3);// String.Format("{0:0.00}", dr["Rate"].ToString());
                            DataRow[] emptyRows = tblEmpty.Select("customer_id = '" + dr["customer_id"].ToString() + "'");
                            decimal empty = 0;
                            if (emptyRows.Length != 0)
                            {
                                empty = Convert.ToDecimal(emptyRows[0]["Empty"].ToString());
                            }

                            //collection
                            decimal cash = 0, credit = 0, coupon = 0, creditColl = 0, _do = 0, salesmanCredit = 0;
                            DataRow[] collRows = tblCollection.Select("customer_id = '" + dr["customer_id"].ToString() + "'");

                            int _lastCustomerId = 0;
                            if (collRows.Length != 0)
                            {
                                foreach (DataRow coll in collRows)
                                {
                                    /*Chceking more items in one delivery of a customer 
                                     If more items should use _tColl below
                                     */
                                    int customerId = Convert.ToInt32(dr["customer_id"].ToString());
                                    string paymentMode = coll["payment_mode"].ToString().ToLower();
                                    int oldLeafCount = Convert.ToInt32(coll["OldLeaf"].ToString());
                                    decimal oldLeafAmount = Convert.ToDecimal(coll["CollectionAmount"].ToString());
                                    err = "Customer : " + customerId;

                                    if (oldLeafCount > 0)
                                    {


                                        dr["customer_name"] = $"{dr["customer_name"]}\n\r Old Leafs # {oldLeafCount} / ";
                                        if (tblOldLeaf != null && tblOldLeaf.Rows.Count > 0)
                                        {
                                            DataRow[] foundRows = tblOldLeaf.Select("customer_id = " + customerId);
                                            if (foundRows.Length > 0)
                                            {
                                                int oldQty = Convert.ToInt32(tblOldLeaf.Compute("Sum(OldLeafCount)", "customer_id=" + dr["customer_id"].ToString() + ""));
                                                dr["customer_name"] += $"{oldQty} #";
                                                this.oldLeafCount += oldQty;
                                                this.oldLeafAmount += oldLeafAmount;
                                                err += "  line between 280-284";
                                            }
                                        }
                                    }
                                    //if (customerId == 967)
                                    //{
                                    //    string _debug = "";
                                    //}

                                    decimal _tColl = Convert.ToDecimal(coll[0]);
                                    //if (listItems.Select(x => x.item_id).Distinct().Count() > 1)
                                    if (listItemsAll.Any(x => x.customer_id == customerId))
                                    {
                                        var listItems = listItemsAll.Where(x => x.customer_id == customerId);
                                        _tColl = listItems.Where(x => x.item_id == this.itemId).Sum(x => x.net_amount);
                                        try
                                        {
                                            DataTable _tblColl = collRows.CopyToDataTable();
                                            paymentMode = collRows.CopyToDataTable().Select($"CollectionAmount={_tColl}")[0].ItemArray[3].ToString().ToLower();
                                        }
                                        catch (Exception ex)
                                        {
                                            paymentMode = deliveryBAL.GetPaymentModeInExceptionCase(customerId, deliveryNo, itemId, paymentMode);


                                            string error = $"Error while setting payment mode in DSR of {lblSalesman.Text} customer={customerId}, {paymentMode} amount={_tColl} \n{ex.Message}";
                                            General.ErrorMustcheck(error);
                                        }
                                        if (_lastCustomerId > 0 && _lastCustomerId == customerId)
                                        {
                                            _lastCustomerId = 0;
                                            continue;
                                        }
                                        _lastCustomerId = customerId;
                                    }
                                    /* E N D  Chceking more items in one delivery of a customer*/

                                    decimal collectionAmount = _tColl;
                                    // decimal collectionAmount  = _tColl == 0 ? Convert.ToDecimal(coll["CollectionAmount"].ToString()) : _tColl;


                                    if (paymentMode == "cash" /*|| paymentMode == "bank"*/)
                                        cash = collectionAmount;
                                    else if (paymentMode == "credit")
                                        credit = collectionAmount;
                                    else if (paymentMode == "coupon")
                                        coupon = collectionAmount;
                                    else if (paymentMode == "do")
                                        _do = collectionAmount;
                                    else if (paymentMode == "salesmancredit")
                                        salesmanCredit = collectionAmount;


                                }
                            }

                            //credit collection
                            DataRow[] credCollRows = tblCreditCollection.Select("customer_id = '" + dr["customer_id"].ToString() + "'");
                            if (credCollRows.Length != 0)
                            {
                                creditColl = Convert.ToDecimal(credCollRows[0]["CreditCollection"].ToString());
                            }

                            gridCustomer.Rows.Add(dr["customer_id"], dr["customer_name"], rate, dr["Soled"], General.TruncateDecimalPlaces(empty), General.TruncateDecimalPlaces(cash), General.TruncateDecimalPlaces(credit), General.TruncateDecimalPlaces(coupon), General.TruncateDecimalPlaces(creditColl), General.TruncateDecimalPlaces(_do), General.TruncateDecimalPlaces(salesmanCredit), General.TruncateDecimalPlaces(_do), General.TruncateDecimalPlaces(salesmanCredit));
                            if (_do != 0)
                                gridCustomer.Columns["clmDoSale"].Visible = true;
                            if (salesmanCredit != 0)
                                gridCustomer.Columns["clmSalemanCredit"].Visible = true;
                        }
                    }
                    if (tblEmpty != null && tblEmpty.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tblEmpty.Rows)
                        {
                            if (dr["customer_id"].ToString() == "2993577")
                            {
                                string ss = "";
                            }
                            bool exist = false;
                            foreach (DataRow dr1 in tblCustomer.Rows)
                            {
                                if (dr["customer_id"].ToString() == dr1["customer_id"].ToString())
                                {
                                    exist = true;
                                }

                            }
                            if (!exist)
                            {
                                //collection
                                decimal cash1 = 0, credit1 = 0, coupon1 = 0, creditColl1 = 0;
                                DataRow[] collRows = tblCollection.Select("customer_id = '" + dr["customer_id"].ToString() + "'");
                                if (collRows.Length != 0)
                                {
                                    foreach (DataRow coll in collRows)
                                    {
                                        if (coll["payment_mode"].ToString().ToLower() == "cash")
                                            cash1 = Convert.ToDecimal(coll["CollectionAmount"].ToString());
                                        else if (coll["payment_mode"].ToString().ToLower() == "credit")
                                            credit1 = Convert.ToDecimal(coll["CollectionAmount"].ToString());
                                        else if (coll["payment_mode"].ToString().ToLower() == "coupon")
                                            coupon1 = Convert.ToDecimal(coll["CollectionAmount"].ToString());
                                    }
                                }

                                //credit collection
                                DataRow[] credCollRows = tblCreditCollection.Select("customer_id = '" + dr["customer_id"].ToString() + "'");
                                if (credCollRows.Length != 0)
                                {
                                    creditColl1 = Convert.ToDecimal(credCollRows[0]["CreditCollection"].ToString());
                                }
                                gridCustomer.Rows.Add(dr["customer_id"], dr["customer_name"], General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0), dr["Empty"], General.TruncateDecimalPlaces(cash1), General.TruncateDecimalPlaces(credit1), General.TruncateDecimalPlaces(coupon1), General.TruncateDecimalPlaces(creditColl1));
                            }
                        }
                    }

                    if (tblCreditCollection != null && tblCreditCollection.Rows.Count > 0)
                    {

                        foreach (DataRow dr in tblCreditCollection.Rows)
                        {

                            if (dr["customer_id"].ToString() == "2993577")
                            {
                                string ss = "";
                            }
                            bool existCus = false;
                            foreach (DataGridViewRow gr in gridCustomer.Rows)
                            {
                                if (gr.Cells["clmCustomerId"].Value.ToString() == dr["customer_id"].ToString())
                                {
                                    existCus = true;
                                    break;
                                }
                            }
                            if (!existCus)
                            {
                                try
                                {

                                    CustomerBAL customerBAL = new CustomerBAL();
                                    decimal creditColl = 0;
                                    // string customerName = customerBAL.GetCustomerDetail(Convert.ToInt32(dr["customer_id"].ToString())).Customer_Name;
                                    DataRow[] credCollRows = tblCreditCollection.Select("customer_id = '" + dr["customer_id"].ToString() + "'");
                                    if (credCollRows.Length != 0)
                                    {
                                        creditColl = Convert.ToDecimal(credCollRows[0]["CreditCollection"].ToString());
                                    }
                                    gridCustomer.Rows.Add(dr["customer_id"], dr["customer_name"], General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(creditColl), General.TruncateDecimalPlaces(0), General.TruncateDecimalPlaces(0));
                                }
                                catch { }
                            }
                        }
                    }

                    //Calc Sum
                    decimal soledSum = 0, emptySum = 0, cashSaleSum = 0, creditSaleSum = 0, couponSaleSum = 0, creditCollSum = 0, doSaleSum = 0, salesmanCreditSaleSum = 0;
                    foreach (DataGridViewRow dr in gridCustomer.Rows)
                    {

                        soledSum += Convert.ToDecimal(dr.Cells["clmSale"].Value.ToString());
                        emptySum += Convert.ToDecimal(dr.Cells["clmEmpty"].Value.ToString());
                        cashSaleSum += Convert.ToDecimal(dr.Cells["clmCashSale"].Value.ToString());
                        creditSaleSum += Convert.ToDecimal(dr.Cells["clmCreditSale"].Value.ToString());
                        couponSaleSum += Convert.ToDecimal(dr.Cells["clmCouponSale"].Value.ToString());
                        creditCollSum += Convert.ToDecimal(dr.Cells["clmCreditCollection"].Value.ToString());
                        doSaleSum += dr.Cells["clmDoSale"].Value != null ? Convert.ToDecimal(dr.Cells["clmDoSale"].Value.ToString()) : 0;
                        salesmanCreditSaleSum += dr.Cells["clmSalemanCredit"].Value != null ? Convert.ToDecimal(dr.Cells["clmSalemanCredit"].Value.ToString()) : 0;

                    }
                    gridCustomer.Rows.Add("", "Total", "", General.TruncateDecimalPlaces(soledSum), General.TruncateDecimalPlaces(emptySum), General.TruncateDecimalPlaces(cashSaleSum), General.TruncateDecimalPlaces(creditSaleSum), General.TruncateDecimalPlaces(couponSaleSum), General.TruncateDecimalPlaces(creditCollSum), General.TruncateDecimalPlaces(doSaleSum), General.TruncateDecimalPlaces(salesmanCreditSaleSum));
                    gridCustomer.Rows[gridCustomer.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;

                    //Setting paymentmode
                    foreach (DataGridViewRow dr in gridCustomer.Rows)
                    {
                        try
                        {



                            var paymentMode = tblCustomer.Select($"customer_id={ dr.Cells["clmCustomerId"].Value.ToString() }", "").FirstOrDefault();

                            string mode = "";

                            if (paymentMode == null)
                            {
                                CustomerBAL customerBAL = new CustomerBAL();
                                int customerId = Convert.ToInt32(dr.Cells["clmCustomerId"].Value);
                                mode = customerBAL.GetCustomerDetail(customerId).Paymentmode;
                            }
                            else
                                mode = paymentMode["payment_mode"].ToString();

                            dr.Cells["clmCustomer"].Tag = mode;
                            dr.Cells["clmCustomer"].ToolTipText = mode;
                        }
                        catch { }

                    }

                }
                General.GridRownumber(gridCustomer);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int _itemId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    _itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                this.itemId = _itemId;
                try
                {
                    lblAddQty.Text = "";
                    lblAddQty.Text = deliveryBAL.GetDeliveryItemSummaryAdditionalItemQty(this.deliveryNo, this.itemId).ToString();
                }
                catch { }
            }
            catch { }
        }

        private void UpdateAdditionalQty()
        {
            if (txtAddQty.Text != "")
            {
                decimal qty = Convert.ToDecimal(txtAddQty.Text);
                deliveryBAL.UpdateAdditionalDeliveryQty(qty, this.deliveryNo, this.itemId);
                pnlAddQty.Hide();
            }
        }

        private void CustomerDeliveryReportStaffwise_Load(object sender, EventArgs e)
        {
            button = new CustomerDeliveryDaybookCollection
            {

                BtnClose = btnClose,
                BtnPrint = btnPrint,
                BtnHide = btnHide,
                BtnSaveAddQty = btnSave,
                BtnPrintColl = btnClollectionPrint,
                BtnSearch = btnSearch

            };
            ButtonActive(EnumFormEvents.FormLoad);
        }

        private DataTable IncludeOtherDeliveryItems(DataTable tblDeliveryItems)
        {
            try
            {
                DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                int itemId = 0, deliveryId = 0;
                if (!String.IsNullOrEmpty(cmbProductName.Text))
                {
                    Object selectedItem = cmbProductName.SelectedItem;
                    itemId = (int)((BETask.Views.ComboboxItem)selectedItem).Value;
                }
                deliveryId = this.deliveryNo;
                List<DAL.Model.DSRDeliveryItemsModel> listItems = deliveryDAL.GetOtherDeliveryItems(itemId, deliveryId);
                if (listItems != null && listItems.Count > 0)
                {
                    foreach (DAL.Model.DSRDeliveryItemsModel it in listItems)
                    {
                        string paymentmode = "";
                        if (it.CashSale > 0)
                            paymentmode = "Cash";
                        else if (it.CouponSale > 0)
                            paymentmode = "Coupon";
                        else if (it.DoSale > 0)
                            paymentmode = "DO";
                        else if (it.SalesmanCredit > 0)
                            paymentmode = "SalesmanCredit";
                        DataRow rowItem = tblDeliveryItems.NewRow();
                        rowItem["ItemName"] = it.ItemName;
                        rowItem["CustomerName"] = it.CustomerName;
                        rowItem["Rate"] = it.Rate;
                        rowItem["Sale"] = it.Sale;
                        rowItem["Empty"] = it.Empty;
                        rowItem["CashSale"] = it.CashSale;
                        rowItem["CreditSale"] = it.CreditSale;
                        rowItem["CouponSale"] = it.CouponSale;
                        rowItem["CreditCollection"] = 0;
                        rowItem["DoSale"] = it.DoSale;
                        rowItem["SalesmanCredit"] = it.SalesmanCredit;
                        rowItem["Paymentmode"] = paymentmode;
                        tblDeliveryItems.Rows.Add(rowItem);
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, "Unable to get other items delivery");
            }
            return tblDeliveryItems;
        }

        private void Print()
        {
            try
            {
                DAL.DAL.CompanyDAL companyDAL = new DAL.DAL.CompanyDAL();
                EDMX.system_settings sysSettings = companyDAL.GetSystemSettings();
                int defaltItemId = sysSettings.default_item_id;


                DAL.DAL.CompanyDAL company = new DAL.DAL.CompanyDAL();
                DAL.EDMX.system_settings setting = company.GetSystemSettings();

                DataTable tblDeliveryHead = new DataTable();
                DataTable tblDeliveryItems = new DataTable();
                BETask.Report.DSReports.CustomerDeliveryDayReportHeadDataTable deliveryHeadDataTable = new Report.DSReports.CustomerDeliveryDayReportHeadDataTable();
                tblDeliveryHead = deliveryHeadDataTable.Clone();
                BETask.Report.DSReports.CustomerDeliveryDayReportDetailDataTable deliveryItemDataTable = new Report.DSReports.CustomerDeliveryDayReportDetailDataTable();
                tblDeliveryItems = deliveryItemDataTable.Clone();


                DataRow rowHead = tblDeliveryHead.NewRow();
                rowHead["Date"] = lblDate.Text;
                rowHead["SalesMan"] = $"{lblSalesman.Text}-{tblDelivery.Rows[0]["delivery_route"]}";
                if (this.routeId != 0)
                    rowHead["SalesMan"] = $"{tblDelivery.Rows[0]["delivery_route"]} -{lblSalesman.Text}";
                rowHead["Vehicle"] = lblVehicle.Text;
                rowHead["Loaded"] = lblLoading.Text;
                rowHead["Soled"] = lblSale.Text;
                rowHead["Empty"] = lblEmpty.Text;
                rowHead["FOC"] = lblFOC.Text;
                rowHead["ItemName"] = cmbProductName.Text;
                rowHead["DeliveryId"] = this.deliveryNo;
                tblDeliveryHead.Rows.Add(rowHead);

                for (int i = 0; i < gridCustomer.Rows.Count - 1; i++)
                {
                    decimal rate = Convert.ToDecimal(gridCustomer["clmRate", i].Value);
                    if (setting.hide_do == 1 && gridCustomer["clmCustomer", i].Tag != null && gridCustomer["clmCustomer", i].Tag.ToString().ToLower() == "do")
                        rate = 0;
                    DataRow rowItem = tblDeliveryItems.NewRow();
                    rowItem["ItemName"] = cmbProductName.Text;
                    rowItem["CustomerName"] = gridCustomer["clmCustomer", i].Value;
                    rowItem["Rate"] = rate;
                    rowItem["Sale"] = gridCustomer["clmSale", i].Value;
                    rowItem["Empty"] = gridCustomer["clmEmpty", i].Value;
                    rowItem["CashSale"] = gridCustomer["clmCashSale", i].Value;
                    rowItem["CreditSale"] = gridCustomer["clmCreditSale", i].Value;
                    rowItem["CouponSale"] = gridCustomer["clmCouponSale", i].Value;
                    rowItem["CreditCollection"] = gridCustomer["clmCreditCollection", i].Value;
                    rowItem["DoSale"] = setting.hide_do != 1 ? (gridCustomer["clmdoSale", i].Value != null ? gridCustomer["clmdoSale", i].Value : 0) : 0;
                    rowItem["SalesmanCredit"] = gridCustomer["clmSalemanCredit", i].Value != null ? gridCustomer["clmSalemanCredit", i].Value : 0;
                    rowItem["Paymentmode"] = gridCustomer["clmCustomer", i].Tag;
                    tblDeliveryItems.Rows.Add(rowItem);
                }
                tblDeliveryItems = IncludeOtherDeliveryItems(tblDeliveryItems);

                BETask.DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                List<EDMX.SP_DeliverySalePaymentMode_Result> listDeliveryPayment = deliveryDAL.GetDeliveryDaybookPaymentMode(Convert.ToInt32(lblDelivery.Text));
                BETask.Report.DSReports.CustomerDeliveryDaybookPaymentModesDataTable customerDeliveryDaybookPaymentModesDataTable = new Report.DSReports.CustomerDeliveryDaybookPaymentModesDataTable();
                DataTable tblPaymentModes = customerDeliveryDaybookPaymentModesDataTable.Clone();

                decimal foc = tblDetail.AsEnumerable()
                                .Where(row => Convert.ToDecimal(row["rate"]) <= 0
                                           && Convert.ToInt32(row["qty"]) > 0
                                           && Convert.ToInt32(row["item_id"]) == itemId
                                           && row["delivery_time"] != DBNull.Value)
                                .Select(row => row.Field<decimal?>("delivered_qty") ?? 0).Sum(); //delivery.delivery_items.ToList().Where(x => x.rate <= 0 && x.qty > 0 && x.item_id == itemId && x.delivery_time != null).Select(x => x.delivered_qty).DefaultIfEmpty(0).Sum();
             
                List<EDMX.SP_FOCbyPaymentmode_Result> listFocSale = deliveryDAL.GetFOCSale(this.deliveryNo).ToList();
                decimal damage = tblSumamry.AsEnumerable()
                             .Where(row => Convert.ToInt32(row["item_id"]) == itemId)
                             .Select(row => row.Field<decimal?>("damage_qty") ?? 0)
                             .Sum();
                lblFOC.Text = foc.ToString();
                if (listDeliveryPayment != null && listDeliveryPayment.Count > 0)
                {
                    foreach (EDMX.SP_DeliverySalePaymentMode_Result li in listDeliveryPayment)
                    {

                        DataRow rowItem = tblPaymentModes.NewRow();
                        //string paymentMode = (li.NetAmount == 0 && li.Qty > 0) ? "FOC" : li.PaymentMode;
                        decimal qty = (decimal)li.Qty;
                        foreach (EDMX.SP_FOCbyPaymentmode_Result re in listFocSale)
                        {
                            if (re.PaymentMode.ToLower() == li.PaymentMode.ToLower())
                            {
                                if (li.ItemName == cmbProductName.Text)
                                    qty -= (decimal)re.Foc;
                            }

                        }
                        string paymentMode = li.PaymentMode;
                        rowItem["PaymentMode"] = paymentMode;
                        rowItem["Qty"] = qty;//li.Qty;
                        if (setting.hide_do == 1 && paymentMode.ToLower() == "do")
                            rowItem["Amount"] = 0;
                        else
                            rowItem["Amount"] = li.NetAmount;
                        rowItem["ItemName"] = li.ItemName;
                        tblPaymentModes.Rows.Add(rowItem);
                    }

                    decimal sumCreditCollection = Convert.ToDecimal(tblDeliveryItems.Compute("SUM(CreditCollection)", string.Empty));
                    int count = Convert.ToInt32(tblDeliveryItems.Compute("Count(CreditCollection)", "CreditCollection>0"));
                    if (sumCreditCollection > 0)
                    {
                        DataRow rowItem = tblPaymentModes.NewRow();

                        rowItem["PaymentMode"] = "Collection";
                        rowItem["Qty"] = count;
                        rowItem["Amount"] = sumCreditCollection;
                        rowItem["ItemName"] = "Collection";
                        tblPaymentModes.Rows.Add(rowItem);
                    }


                }

                //ADDING foc
                if (foc != 0)
                {
                    DataRow rowItem = tblPaymentModes.NewRow();
                    rowItem["PaymentMode"] = "FOC";
                    rowItem["Qty"] = foc;
                    rowItem["Amount"] = 0;
                    rowItem["ItemName"] = cmbProductName.Text;
                    tblPaymentModes.Rows.Add(rowItem);
                }
                //ADDING Damage
                if (damage != 0)
                {
                    DataRow rowItem = tblPaymentModes.NewRow();
                    rowItem["PaymentMode"] = "DAMAGE";
                    rowItem["Qty"] = damage;
                    rowItem["Amount"] = 0;
                    rowItem["ItemName"] = cmbProductName.Text;
                    tblPaymentModes.Rows.Add(rowItem);
                }
                //Old Leafs
                if (oldLeafCount > 0)
                {
                    DataRow rowItem = tblPaymentModes.NewRow();
                    rowItem["PaymentMode"] = "OLDLEAF";
                    rowItem["Qty"] = oldLeafCount;
                    rowItem["Amount"] = oldLeafAmount;
                    rowItem["ItemName"] = cmbProductName.Text;
                    tblPaymentModes.Rows.Add(rowItem);
                }

                //If Old leaf count>0 then it deducted from Coupon
                if (tblPaymentModes != null && tblPaymentModes.Rows.Count > 0 && oldLeafCount > 0)
                {
                    string paymentMode = "Coupon";
                    if (tblPaymentModes.AsEnumerable().Any(row => paymentMode == row.Field<String>("PaymentMode")))
                    {
                        DataRow[] dr = tblPaymentModes.Select($"PaymentMode='Coupon'", "");
                        dr[0]["Qty"] = Convert.ToDecimal(dr[0]["Qty"]) - oldLeafCount;
                        dr[0]["Amount"] = Convert.ToDecimal(dr[0]["Amount"]) - oldLeafAmount;
                    }
                }
                //Loading
                BETask.Report.DSReports.CustomerDeliveryDaybookLoadingDataTable customerDeliveryDaybookLoadingDataTable = new Report.DSReports.CustomerDeliveryDaybookLoadingDataTable();
                DataTable tblLoading = customerDeliveryDaybookLoadingDataTable.Clone();
                if (dgLoading.Rows.Count > 0)
                {

                    foreach (DataGridViewRow dr in dgLoading.Rows)
                    {
                        DataRow rowItem = tblLoading.NewRow();
                        rowItem["ItemName"] = dr.Cells["clmItemName"].Value;
                        rowItem["Loaded"] = dr.Cells["clmTotalQty"].Value;
                        rowItem["Balance"] = dr.Cells["clmbalance"].Value;
                        tblLoading.Rows.Add(rowItem);
                    }
                }
                #region oldLoading
                /*
                List<EDMX.delivery_item_summary> listSummary = deliveryDAL.GetDeliveryItemSummary(Convert.ToInt32(lblDelivery.Text));
                BETask.Report.DSReports.CustomerDeliveryDaybookLoadingDataTable customerDeliveryDaybookLoadingDataTable = new Report.DSReports.CustomerDeliveryDaybookLoadingDataTable();
                DataTable tblLoading = customerDeliveryDaybookLoadingDataTable.Clone();
                if (listSummary != null && listSummary.Count > 0)
                {
                    int empId = deliveryBAL.GetDeliveryDetails(deliveryNo).employee_id;
                    List<EDMX.delivery_item_summary> listStockItems = deliveryDAL.GetDeliveryItemBalance(this.deliveryNo, empId);
                    if (listStockItems != null && listStockItems.Count > 0)
                    {
                        foreach (EDMX.delivery_item_summary raw in listStockItems)
                        {
                            if (!listSummary.Any(x => x.item_id == raw.item_id))
                            {
                                listSummary.Add(raw);
                            }
                        }
                    }
                    foreach (EDMX.delivery_item_summary li in listSummary)
                    {
                        if (li.qty == 0)
                        {
                            decimal lastBalance=deliveryBAL.GetPreviousDayBalance(delivery.delivery_date, delivery.employee_id, li.item_id);
                            if (lastBalance > 0)
                                li.qty = lastBalance;
                        }
                        DataRow rowItem = tblLoading.NewRow();
                        rowItem["ItemName"] = li.item.item_name;
                        rowItem["Loaded"] = li.qty;
                        rowItem["Balance"] = li.qty-li.used_qty;
                        tblLoading.Rows.Add(rowItem);
                    }
                }
                */
                #endregion oldLoading
                string header = $"{ General.companyName } - Sales Daybook";
                deliveryBAL.PrintCustomerDeliveryDaybook(header, this.deliveryNo, tblDeliveryHead, tblDeliveryItems, tblPaymentModes, tblLoading);

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void PrintCollection()
        {
            try
            {
                deliveryBAL.PrintCollectionOnly(General.ConvertDateServerFormatWithStartTime(delivery.delivery_date), General.ConvertDateServerFormatWithEndTime(delivery.delivery_date), Convert.ToInt32(delivery.route_id), delivery.employee_id, lblSalesman.Text);
            }
            catch (Exception ex)
            {
                General.LogExceptionWithShowError(ex, "Unable to print somthing went wrong");
            }
        }

        private void lblAddQty_Click(object sender, EventArgs e)
        {
            if (lblDelivery.Text != "")
                pnlAddQty.Show();
        }
        private void ReRun(bool message)
        {
            try
            {

                int result = deliveryBAL.ReRunDSR(this.deliveryNo);
                if (message)
                {
                    General.ShowMessage(General.EnumMessageTypes.Success, $"{result} Rows updated");
                    LoadDeliveryData();
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }
        private void linkReRun_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReRun(true);
        }

        private void chkCashCollected_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCashCollected.Checked && chkCashCollected.Enabled)
            {
                if (this.deliveryNo > 0)
                {
                    deliveryBAL.DSRUpdateCollectionRecieved(this.deliveryNo,lblSalesman.Text);
                    General.ShowMessage(General.EnumMessageTypes.Success, "Cash collected");
                    chkCashCollected.Enabled = false;
                }
            }
        }

        private void linkLoadDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (!dgLoading.Visible)
            {
                if (General.viewLoadingDsr)
                {
                    LoadDeliveryDetails();
                    dgLoading.Show();
                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Error,"View loading not enabled");
                }
            }
            else
                dgLoading.Hide();
        }
        private void LoadDeliveryDetails()
        {
            try
            {
                int _deliveryId = this.deliveryNo;
                int _empId = Convert.ToInt32(tblDelivery.Rows[0]["employee_id"]);
                DateTime date = Convert.ToDateTime(tblDelivery.Rows[0]["Delivery_Date"]);

                General.ClearGrid(dgLoading);

                decimal prvbal = 0;
                decimal balance = 0;
                decimal totalQty = 0;
                decimal soldQty = 0;

                if (listDeliveryIitemSummary != null)
                    listDeliveryIitemSummary = deliveryBAL.GetDeliveryItemSummaryById(_deliveryId);
                if (listDeliveryIitemSummary != null && listDeliveryIitemSummary.Count > 0)
                {
                    BETask.DAL.DAL.DeliveryDAL deliveryDAL = new DAL.DAL.DeliveryDAL();
                    List<EDMX.delivery_item_summary> listStockItems = deliveryDAL.GetDeliveryItemBalance(_deliveryId, _empId);
                    if (listStockItems != null && listStockItems.Count > 0)
                    {
                        foreach (EDMX.delivery_item_summary raw in listStockItems)
                        {
                            if (!listDeliveryIitemSummary.Any(x => x.item_id == raw.item_id))
                            {
                                listDeliveryIitemSummary.Add(raw);
                            }
                        }
                    }

                    DAL.DAL.LoadDAL loadDAL = new DAL.DAL.LoadDAL();
                    foreach (EDMX.delivery_item_summary data in listDeliveryIitemSummary)
                    {
                        prvbal = deliveryBAL.GetPreviousDayBalance(General.ConvertDateServerFormat(date), _empId, Convert.ToInt32(data.item_id));
                        if (!string.IsNullOrEmpty(data.remarks) && data.remarks == "#")
                        {
                            if (data.qty > 0)
                            {
                                data.qty += -prvbal;
                            }
                        }

                        soldQty = deliveryBAL.GetSoldQuantity(_deliveryId, Convert.ToInt32(data.item_id));
                        totalQty = data.qty;
                        if (totalQty == 0 && prvbal > 0)
                            totalQty = prvbal;

                        balance = totalQty - soldQty - data.damage_qty; //data.balance_qty;
                        data.used_qty = soldQty;
                        data.balance_qty = balance;

                        decimal newLoad = data.qty - prvbal < 0 ? 0 : (data.qty - prvbal);
                        try
                        {
                            newLoad = loadDAL.GetNewLoad(_deliveryId, data.item_id);
                            if (newLoad < 0)
                                totalQty += newLoad;
                            if (prvbal > 0 && newLoad == 0 && prvbal != totalQty)
                            {
                                totalQty = prvbal;
                                balance = prvbal;
                            }
                        }
                        catch (Exception ex)
                        { }
                        if (data.balance_qty != 0)
                            dgLoading.Rows.Add(General.TruncateDecimalPlaces(data.item.item_id), data.item.item_name, General.TruncateDecimalPlaces(prvbal), General.TruncateDecimalPlaces(newLoad), General.TruncateDecimalPlaces(totalQty), General.TruncateDecimalPlaces(data.used_qty), General.TruncateDecimalPlaces(balance), General.TruncateDecimalPlaces(data.damage_qty), data.remarks);
                    }

                }
                else
                {
                    General.ShowMessage(General.EnumMessageTypes.Warning, "No data found", "info");
                }

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);

            }

        }


        private void MismatchReport()
        {
            try
            {
                BETask.DAL.DAL.MiscellaneousReportDAL reportDAL = new DAL.DAL.MiscellaneousReportDAL();
                DataSet ds = reportDAL.DSRMismatchReport(General.ConvertDateServerFormat(delivery.delivery_date), General.ConvertDateServerFormat(delivery.delivery_date), delivery.delivery_id, delivery.employee_id, Convert.ToInt32(delivery.route_id), this.itemId);
                DSRMismatchForm mismatchForm = new DSRMismatchForm(ds);
                mismatchForm.ShowDialog();

            }
            catch (Exception ex)
            {

            }
        }

        private void linkMismatch_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MismatchReport();
        }

        private void Search()
        {
            LoadDeliveryData();
        }
    }
    class CustomerDeliveryDaybookCollection
    {

        public Button BtnPrint { get; set; }
        public Button BtnSearch { get; set; }
        public Button BtnPrintColl { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnHide { get; set; }
        public Button BtnSaveAddQty { get; set; }

    }
}
