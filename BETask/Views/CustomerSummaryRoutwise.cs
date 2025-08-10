using BETask.Common;
using BETask.BAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.Report;
using System.Data;

namespace BETask.Views
{
    public partial class CustomerSummaryRoutwise : Form
    {
        DAL.DAL.CustomerDAL customer = new DAL.DAL.CustomerDAL();
        CustomerSummaryButtonCollection button = new CustomerSummaryButtonCollection();
        public CustomerSummaryRoutwise()
        {
            InitializeComponent();
        }
        public enum EnumFormEvents
        {
            FormLoad,
            Cancel,
            Close,
            Search,
            Print
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
            try
            {
                List<EDMX.SP_CustomerSummary_Result> listCustomer = customer.GetCustomerSummaryByPayment();
                if (listCustomer != null && listCustomer.Count > 0)
                {
                    List<string> listRoutes = new List<string>();
                    foreach (EDMX.SP_CustomerSummary_Result rs in listCustomer)
                    {
                        listRoutes = listCustomer.Select(x => x.route_name).Distinct().ToList();
                    }
                    decimal totalT=0,cashT = 0, couponT = 0, creditT = 0, doT = 0, salesmanT = 0, otherT = 0, bankT = 0;
                    foreach (string rt in listRoutes)
                    {
                        //totalT = 0; cashT = 0; couponT = 0; creditT = 0; doT = 0; salesmanT = 0; otherT = 0; bankT = 0;
                        var _total = listCustomer.Where(x => x.route_name == rt).ToList();
                        decimal total = _total != null ? Convert.ToDecimal(_total.Sum(x => x.customerCount)) : 0;
                        totalT += total;

                        var _cash = listCustomer.Where(x => (x.payment_mode != null && x.payment_mode.ToLower() == "cash") && x.route_name == rt).FirstOrDefault();
                        decimal cash = _cash != null ? Convert.ToDecimal(_cash.customerCount) : 0;
                        cashT += cash;

                        var _coupon = listCustomer.Where(x => (x.payment_mode != null && x.payment_mode.ToLower() == "coupon") && x.route_name == rt).FirstOrDefault();
                        decimal coupon = _coupon != null ? Convert.ToDecimal(_coupon.customerCount) : 0;
                        couponT += coupon;

                        var _credit = listCustomer.Where(x => (x.payment_mode != null && x.payment_mode.ToLower() == "credit") && x.route_name == rt).FirstOrDefault();
                        decimal credit = _credit != null ? Convert.ToDecimal(_credit.customerCount) : 0;
                        creditT += credit;

                        var _do = listCustomer.Where(x => (x.payment_mode != null && x.payment_mode.ToLower() == "do") && x.route_name == rt).FirstOrDefault();
                        decimal _doC = _do != null ? Convert.ToDecimal(_do.customerCount) : 0;
                        doT += _doC;


                        var _salesman = listCustomer.Where(x => (x.payment_mode != null && x.payment_mode.ToLower() == "salesmancredit") && x.route_name == rt).FirstOrDefault();
                        decimal salesman = _salesman != null ? Convert.ToDecimal(_salesman.customerCount) : 0;
                        salesmanT += salesman;

                        var _other = listCustomer.Where(x => (string.IsNullOrEmpty(x.payment_mode) || string.IsNullOrWhiteSpace(x.payment_mode) || x.payment_mode.Trim() == "") && x.route_name == rt).ToList();
                        decimal other = 0;
                        foreach (var ot in _other)
                        {
                            other += ot != null ? Convert.ToDecimal(ot.customerCount) : 0;
                        }

                        otherT += other;

                        var _bank = listCustomer.Where(x => (x.payment_mode != null && x.payment_mode.ToLower() == "bank") && x.route_name == rt).FirstOrDefault();
                        decimal bank = _bank != null ? Convert.ToDecimal(_bank.customerCount) : 0;
                        bankT += bank;

                        gridCustomer.Rows.Add(rt, total, cash, coupon, credit, _doC, salesman, other, bank);
                    }
                    gridCustomer.Rows.Add("Total", totalT, cashT, couponT, creditT, doT, salesmanT, otherT, bankT);
                    General.GridBackcolorYellow(gridCustomer);
                    General.GridRownumber(gridCustomer);

                }
            }
            catch (Exception ex)
            {
                General.Error(ex.Message);
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void Print()
        {
            try
            {
                if (gridCustomer.Rows.Count > 0)
                {
                    REP.DSReports.CustomerSummaryPaymentModeDataTable customerSummaryPaymentModeDataTable = new REP.DSReports.CustomerSummaryPaymentModeDataTable();
                    DataTable tblData = customerSummaryPaymentModeDataTable.Clone();
                    foreach (DataGridViewRow row in gridCustomer.Rows)
                    {
                        if (row.Cells["clmRoute"].Value.ToString().ToLower() != "total")
                        {
                            DataRow dr = tblData.NewRow();
                            dr["RouteName"] = row.Cells["clmRoute"].Value;
                            dr["Total"] = row.Cells["clmCustomer"].Value;
                            dr["Cash"] = row.Cells["clmCash"].Value;
                            dr["Coupon"] = row.Cells["clmCoupon"].Value;
                            dr["Credit"] = row.Cells["clmCredit"].Value;
                            dr["Do"] = row.Cells["clmDo"].Value;
                            dr["Salesman"] = row.Cells["clmSalemen"].Value;
                            dr["NoPayment"] = row.Cells["clmNoPaymentmode"].Value;
                            dr["Bank"] = row.Cells["clmBank"].Value;
                            tblData.Rows.Add(dr);
                        }

                    }
                    if (tblData != null && tblData.Rows.Count > 0)
                    {
                        REP.ReportForm reportForm = new REP.ReportForm(REP.ReportForm.EnumReportType.CustomerSummary, $"Customer Summary Report {General.companyName}", tblData);
                        reportForm.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error);
            }
        }

        private void CustomerSummaryRoutwise_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void FormLoad()
        {
            button = new CustomerSummaryButtonCollection
            {
               // BtnSearch = btnSearch,
                BtnPrint = btnPrint,
                BtnClose = btnClose
            };
            Search();
        }
    }
    class CustomerSummaryButtonCollection
    {
        public Button BtnSearch { get; set; }
        public Button BtnCancel { get; set; }
        public Button BtnClose { get; set; }
        public Button BtnPrint { get; set; }
    }
}
