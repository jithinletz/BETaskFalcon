using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using System.Data;

namespace BETask.Views
{
    public partial class CouponReportForm : Form
    {
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
        }
        CouponBAL couponBAL = new CouponBAL();
        CouponReportButtonCollection button;
        string searchType = "all";
        public CouponReportForm()
        {
            InitializeComponent();
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
                    break;
                case EnumFormEvents.Close:
                    this.BeginInvoke(new MethodInvoker(Close));
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
            else if (sender == button.BtnPrint)
            {
                ButtonActive(EnumFormEvents.Print);
            }
        }
        private void Search()
        {
            if (rdbAll.Checked)
                searchType = "all";
            else if (rdbActive.Checked)
                searchType = "active";
            else if (rdbUsed.Checked)
                searchType = "used";


            General.ClearGrid(gridReport);
            if (cmbReportType.Text == "Books Issued")
            {
                SearchBook();
            }
           else if (cmbReportType.Text == "Redeemed")
            {
                SearchRedeemeds();
            }
        }
        private void SearchBook()
        {
            try
            {
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                List<EDMX.coupon> listCoupon = couponBAL.GetCouponBooks(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value),routeId);
               
                if (listCoupon != null && listCoupon.Count > 0)
                {
                    GridTemplateBook();
                    int availableTotal = 0,leafTotal=0;
                    decimal bookTotal = 0;
                    foreach (EDMX.coupon cp in listCoupon)
                    {
                        int available = 0;
                        var leafs = cp.coupon_leaf.ToList();
                        available = leafs.Where(x => x.status == 1).Count();
                        if (searchType == "all")
                        {
                            gridReport.Rows.Add(cp.coupon_id, cp.book_number, cp.customer_id, cp.customer.customer_name, General.ConvertDateAppFormat(cp.issue_date), cp.leaf_count, cp.leaf_rate, cp.book_rate, available);

                            availableTotal += available;
                            bookTotal += cp.book_rate;
                            leafTotal += cp.leaf_count;
                        }
                        else if (searchType == "active")
                        {
                            if (available > 0)
                            {
                                gridReport.Rows.Add(cp.coupon_id, cp.book_number, cp.customer_id, cp.customer.customer_name, General.ConvertDateAppFormat(cp.issue_date), cp.leaf_count, cp.leaf_rate, cp.book_rate, available);
                                availableTotal += available;
                                bookTotal += cp.book_rate;
                                leafTotal += cp.leaf_count;
                            }
                        }
                        else if (searchType == "used")
                        {
                            if (available <= 0)
                            {
                                gridReport.Rows.Add(cp.coupon_id, cp.book_number, cp.customer_id, cp.customer.customer_name, General.ConvertDateAppFormat(cp.issue_date), cp.leaf_count, cp.leaf_rate, cp.book_rate, available);
                                availableTotal += available;
                                bookTotal += cp.book_rate;
                                leafTotal += cp.leaf_count;
                            }
                        }
                       
                    }
                    gridReport.Rows.Add("","","","", "",leafTotal, "",bookTotal,availableTotal);
                    gridReport.Rows[gridReport.Rows.Count-1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                    General.GridRownumber(gridReport);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void SearchRedeemeds()
        {
            try
            {
                TimeSpan ts = new TimeSpan(00, 00, 01);
                TimeSpan ts1 = new TimeSpan(23, 59, 59);
                dtpDateFrom.Value = General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value);   
                dtpDateTo.Value = General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value);


                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                List<EDMX.coupon_leaf> listCoupon = couponBAL.GetRedeemedLeafs(General.ConvertDateServerFormatWithStartTime(dtpDateFrom.Value), General.ConvertDateServerFormatWithEndTime(dtpDateTo.Value), routeId);

                if (listCoupon != null && listCoupon.Count > 0)
                {
                    GridTemplateRedeemed();
                    decimal redeemedTotal = 0;
                    foreach (EDMX.coupon_leaf cp in listCoupon)
                    {
                        
                       
                        gridReport.Rows.Add(cp.coupon_id, cp.coupon.book_number, cp.coupon.customer_id, cp.coupon.customer.customer_name, General.ConvertDateAppFormat(cp.issue_date), cp.leaf_number, cp.leaf_rate, cp.redeem_date, cp.remarks);

                        redeemedTotal += cp.leaf_rate;
                       
                    }
                    gridReport.Rows.Add("", "", "", "", "", "", redeemedTotal, "", "","");
                    gridReport.Rows[gridReport.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                    General.GridRownumber(gridReport);
                    
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void GridTemplateBook()
        {
            gridReport.Columns.Clear();
            DataGridViewTextBoxColumn clmCouponId = new DataGridViewTextBoxColumn(); clmCouponId.HeaderText = "CouponId"; clmCouponId.Visible = false;
            DataGridViewTextBoxColumn clmBookNumber = new DataGridViewTextBoxColumn(); clmBookNumber.HeaderText = "Book No";
            DataGridViewTextBoxColumn clmCustomerId = new DataGridViewTextBoxColumn(); clmCouponId.HeaderText = "CustomerId"; clmCustomerId.Visible = false; clmCustomerId.Name = "clmCustomerId";
             DataGridViewTextBoxColumn clmCustmer = new DataGridViewTextBoxColumn(); clmCustmer.HeaderText = "Customer"; clmCustmer.Width = 300;
            DataGridViewTextBoxColumn clmIssueDate = new DataGridViewTextBoxColumn(); clmIssueDate.HeaderText = "Issued On"; clmCustmer.Width = 150;
            DataGridViewTextBoxColumn clmLeafcount = new DataGridViewTextBoxColumn(); clmLeafcount.HeaderText = "Leafs";
            DataGridViewTextBoxColumn clmLeaferate = new DataGridViewTextBoxColumn(); clmLeaferate.HeaderText = "LeafRate";
            DataGridViewTextBoxColumn clmBookAmount = new DataGridViewTextBoxColumn(); clmBookAmount.HeaderText = "Amount";
            DataGridViewTextBoxColumn clmAvailabe = new DataGridViewTextBoxColumn(); clmAvailabe.HeaderText = "Available";
            gridReport.Columns.Add(clmCouponId);
            gridReport.Columns.Add(clmBookNumber);
            gridReport.Columns.Add(clmCustomerId);
            gridReport.Columns.Add(clmCustmer);
            gridReport.Columns.Add(clmIssueDate);
            gridReport.Columns.Add(clmLeafcount);
            gridReport.Columns.Add(clmLeaferate);
            gridReport.Columns.Add(clmBookAmount);
            gridReport.Columns.Add(clmAvailabe);
        }
        private void GridTemplateRedeemed()
        {
            gridReport.Columns.Clear();
            DataGridViewTextBoxColumn clmCouponId = new DataGridViewTextBoxColumn(); clmCouponId.HeaderText = "CouponId"; clmCouponId.Visible = false;
            DataGridViewTextBoxColumn clmBookNumber = new DataGridViewTextBoxColumn(); clmBookNumber.HeaderText = "Book No";
            DataGridViewTextBoxColumn clmCustomerId = new DataGridViewTextBoxColumn(); clmCouponId.HeaderText = "CustomerId"; clmCustomerId.Visible = false; clmCustomerId.Name = "clmCustomerId";
            DataGridViewTextBoxColumn clmCustmer = new DataGridViewTextBoxColumn(); clmCustmer.HeaderText = "Customer"; clmCustmer.Width = 300;
            DataGridViewTextBoxColumn clmIssueDate = new DataGridViewTextBoxColumn(); clmIssueDate.HeaderText = "Issued On"; clmCustmer.Width = 150;
            DataGridViewTextBoxColumn clmLeaf = new DataGridViewTextBoxColumn(); clmLeaf.HeaderText = "Leaf";
            DataGridViewTextBoxColumn clmLeafRate = new DataGridViewTextBoxColumn(); clmLeafRate.HeaderText = "Amount";
            DataGridViewTextBoxColumn clmDeliveryTime = new DataGridViewTextBoxColumn(); clmDeliveryTime.HeaderText = "Delivery Time"; clmCustmer.Width = 200;
            DataGridViewTextBoxColumn clmLeafStatus = new DataGridViewTextBoxColumn(); clmLeafStatus.HeaderText = "Remarks";

            gridReport.Columns.Add(clmCouponId);
            gridReport.Columns.Add(clmBookNumber);
            gridReport.Columns.Add(clmCustomerId);
            gridReport.Columns.Add(clmCustmer);
            gridReport.Columns.Add(clmIssueDate);
            gridReport.Columns.Add(clmLeaf);
            gridReport.Columns.Add(clmLeafRate);
            gridReport.Columns.Add(clmDeliveryTime);
            gridReport.Columns.Add(clmLeafStatus);
        }

        private void Print()
        {
            try
            {
                int routeId = 0;
                if (cmbRoute.Text != "")
                {
                    Object slectedRoute = cmbRoute.SelectedItem;
                    routeId = (int)((BETask.Views.ComboboxItem)slectedRoute).Value;
                }
                string header = "";
                if (cmbReportType.Text == "Books Issued")
                {
                    header = $"{General.companyName} , Issue date between {General.ConvertDateAppFormat(dtpDateFrom.Value)} and {General.ConvertDateAppFormat(dtpDateTo.Value)} {cmbRoute.Text}";
                    couponBAL.PrintCouponReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), routeId, header,searchType);
                }
                else if (cmbReportType.Text == "Redeemed")
                {
                    header = $"{General.companyName} , Redeemed date between {General.ConvertDateAppFormat(dtpDateFrom.Value)} and {General.ConvertDateAppFormat(dtpDateTo.Value)} {cmbRoute.Text}";
                    couponBAL.PrintCouponRedeemedReport(General.ConvertDateServerFormat(dtpDateFrom.Value), General.ConvertDateServerFormat(dtpDateTo.Value), routeId, header);
                }
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
                RouteBAL routeBAL = new RouteBAL();
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
            button = new CouponReportButtonCollection()
            {
                BtnSearch = btnSearch,
                BtnClose = btnClose,
                BtnPrint = btnPrint
            };
            GetAllRoutes();
            cmbReportType.SelectedIndex = 0;
            dtpDateFrom.Value= dtpDateFrom.Value.AddMonths(-3);
        }

        private void CouponReportForm_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void gridReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               // if (cmbReportType.Text == "Books Issued")
                {
                    int _customerId = Convert.ToInt32(gridReport["clmCustomerId", e.RowIndex].Value);
                    if (_customerId != 0)
                    {
                        CouponSaleForm coupon = new CouponSaleForm(_customerId);
                        coupon.ShowDialog();
                    }
                }
               
            }
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.Text == "Redeemed")
            {
                rdbActive.Hide();
                rdbUsed.Hide();
                rdbAll.Hide();
            }
            else
            {
                rdbActive.Show();
                rdbUsed.Show();
                rdbAll.Show();
            }
        }
    }
    class CouponReportButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }

    }
}
