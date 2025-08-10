using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace BETask.Report
{
    public partial class ReportForm : Form
    {
          //string reportPath = Properties.Settings.Default.reportPath;
       string reportPath =String.Format("{0}{1}",Application.StartupPath,"//Reports");
        string emailHead = "Document";

        public enum EnumReportType
        {
            CustomerList, SupplierList, PurchaseSupplierDue,
            PurchaseInvoice,PurchaseOrderInvoice ,PurchaseRportSupplierwise, PurchaseOrderRportSupplierwise, Purchase, ReportItemWise,
            SalesInvoice, SalesInvoiceDO, DOSalesInvoice, SalesReportCustomerwise, SaleReportItemWise,
            ItemList,ItemValue,ItemTransaction,
            DeliveryInvoice, DeliveryReportStaffWise, DeliveryReportItemWise,DeliveryReturnReport,DailyCollection,DepositCollection,
            Payment, Reciept, PaymentReport, Journal,AccountStatement,DaybookSummary, DaybookDetailed,CustomerStatementSummary, CustomerStatementDetailed,
            ProductionReport,PaymentVoucher,
            ProductionRawmaterialReport,RouteItemReport, RoutewiseCashbook,
            EmployeeReport,PunchReport,CustomerDeliveryDaybook,PDCInvoice,PDCReport,RouteSaleReport,ProfitandLoss,Balancesheet,CouponBookReport,CouponRedeemedReport,
            CustomerPerformance,CustomerMonthlyOutstanding,MonthlyAnalyse,FOCSales,ItemDeliveryProduction,TrialBalance, TrialBalanceDatewise, TrialBalanceSummary, TrialBalanceSummaryDatewise, CustomerStatementAdvanced,DamageReport,TransferItem,
            TransferItemReportDateWise, ItemStockReport, DeliveryLoadReport ,DOSaleList,CostCenterSummary, CostCenterDetailed,CustomerSummary,DoDeliveryItems,DOPendingInvoices,LoadingSlip,CustomerAsset,CustomerAssetAgreement, CustomerAssetAll,Loading,CustomerStock,
            DSRCollection,DeliverySummary,CustomerWalletMisc,CollectionReport,OnlineReport

        }
       
        public ReportForm()
        {
            InitializeComponent();
        }
        public ReportForm(Enum reportType,string header,DataTable tblData)
        {
            InitializeComponent();
            this.rv.RefreshReport();
            switch (reportType)
            {
                case EnumReportType.CustomerList:
                    CustomerList(header, tblData);
                    break;
                case EnumReportType.ItemList:
                   ItemsList(header, tblData);
                    break;
                case EnumReportType.ItemValue:
                    ItemsValue(header, tblData);
                    break;
                case EnumReportType.ItemTransaction:
                    ItemsTransaction(header, tblData);
                    break;
                case EnumReportType.ProductionReport:
                    ProductionReport(tblData);
                    break;
                case EnumReportType.ProductionRawmaterialReport:
                    ProductionRawmaterialReport(tblData);
                    break;
                case EnumReportType.SalesReportCustomerwise:
                    SalesReportCustomerwise(tblData,header);
                    break;
               
                case EnumReportType.PurchaseRportSupplierwise:
                    PurchaseReportSupplierwise(tblData);
                    break;
                case EnumReportType.PurchaseOrderRportSupplierwise:
                    PurchaseOrderReportSupplierwise(tblData);
                    break;
                case EnumReportType.DeliveryReportStaffWise:
                    DeliveryReportStaffwise(tblData);
                    break;
                case EnumReportType.DeliveryReturnReport:
                    DeliveryReturnReport(header,tblData);
                    break;
                case EnumReportType.DeliveryReportItemWise:
                    DeliveryReportItemwise(header,tblData);
                    break;
                case EnumReportType.DailyCollection:
                    DailyCollection(header, tblData);
                    break;
                case EnumReportType.DepositCollection:
                    DepositCollection(header, tblData);
                    break;

                case EnumReportType.PaymentReport:
                    PaymentReport(header,tblData);
                    break;
                         
                case EnumReportType.EmployeeReport:
                    EmployeeReport(header, tblData);
                    break;
                case EnumReportType.PunchReport:
                    PunchReport(header, tblData);
                    break;
                case EnumReportType.DaybookSummary:
                    DaybookSummary(header, tblData);
                    break;
                case EnumReportType.DaybookDetailed:
                    DaybookDetailed(header, tblData);
                    break;
                case EnumReportType.CustomerStatementSummary:
                    CustomerStatementSummary(header, tblData);
                    break;
                case EnumReportType.CustomerStatementDetailed:
                    CustomerStatementDetailed(header, tblData);
                    break;
                case EnumReportType.RouteItemReport:
                    RouteItemReport(header, tblData);
                    break;
                case EnumReportType.RoutewiseCashbook:
                    RoutewiseCashbook(header, tblData);
                    break;
                    
                case EnumReportType.PDCReport:
                    PDCReport(header, tblData);
                    break;
                case EnumReportType.CouponBookReport:
                    CouponBook(header, tblData);
                    break;
                case EnumReportType.CouponRedeemedReport:
                    CouponRedeemed(header, tblData);
                    break;
                case EnumReportType.CustomerPerformance:
                    CustomerPerformance(header, tblData);
                    break;
                case EnumReportType.CustomerMonthlyOutstanding:
                    CustomerMonthlyOutstanding(header, tblData);
                    break;
                case EnumReportType.CustomerWalletMisc:
                    CustomerWalletMisc(header, tblData);
                    break;
                case EnumReportType.MonthlyAnalyse:
                    MonthlyAnalyse(header, tblData);
                    break;
                case EnumReportType.FOCSales:
                    FOCSales(header, tblData);
                    break;
                case EnumReportType.ItemDeliveryProduction:
                    ItemDeliveryProduction(header, tblData);
                    break;
                case EnumReportType.DamageReport:
                    DamageReport(header, tblData);
                    break;
                case EnumReportType.TransferItem:
                    TransferItem(header, tblData);
                    break;
                case EnumReportType.TransferItemReportDateWise:
                    TransferItemReportDateWise(header, tblData);
                    break;
                case EnumReportType.ItemStockReport:
                    ItemStockReport(header, tblData);
                    break;
                case EnumReportType.DeliveryLoadReport:
                    DeliveryLoadReport(header, tblData);
                    break;
                case EnumReportType.DOSaleList:
                    DOSaleList(header, tblData);
                    break;
                case EnumReportType.CostCenterSummary:
                    CostCenterSummary(header, tblData);
                    break;
                case EnumReportType.CostCenterDetailed:
                    CostCenterDetailed(header, tblData);
                    break;
                case EnumReportType.CustomerSummary:
                    CustomerSummary(header, tblData);
                    break;
                case EnumReportType.CustomerAssetAll:
                    CustomerAssetAll(header, tblData);
                    break;
                case EnumReportType.Loading:
                    Loading(header, tblData);
                    break;
                case EnumReportType.CustomerStock:
                    CustomerStock(header, tblData);
                    break;
                case EnumReportType.DeliverySummary:
                    DeliverySummary(header, tblData);
                    break;
                case EnumReportType.CollectionReport:
                    CollectionReport(header, tblData);
                    break;
                case EnumReportType.OnlineReport:
                    OnlineReport(header, tblData);
                    break;

            }
        }
        public ReportForm(Enum reportType, string header, DataTable tblData, DataTable tblData1,string senderEmail="",string companyAddress="",string customerEmail="")
        {
            InitializeComponent();
            this.rv.RefreshReport();
            txtEmail.Text = senderEmail;
            switch (reportType)
            {
                case EnumReportType.PurchaseInvoice:
                    PurchaseInvoice(header, tblData, tblData1);
                    break;
                case EnumReportType.PurchaseOrderInvoice:
                    PurchaseOrderInvoice(header, tblData, tblData1);
                    emailHead = "Purchase Order";
                    break;
                case EnumReportType.SalesInvoice:
                    SaleInvoice(header, tblData, tblData1,companyAddress,customerEmail);
                    break;
                case EnumReportType.SalesInvoiceDO:
                    SaleInvoiceDO(header, tblData, tblData1, companyAddress, customerEmail);
                    break;
                case EnumReportType.DOSalesInvoice:
                    DOSaleInvoice(header, tblData, tblData1, companyAddress, customerEmail);
                    break;
                case EnumReportType.CustomerDeliveryDaybook:
                    CustomerDeliveryDaybook(header, tblData, tblData1);
                    break;
                case EnumReportType.PDCReport:
                    PDCReport(header, tblData);
                    break;
            }
        }
        
        private void PurchaseSupplierDue(string header,  string companyAddress, DataTable tblData, string customerEmail="")
        {
            try
            {
                txtEmail.Text = customerEmail;
                
                rv.LocalReport.ReportPath = $"{reportPath}\\PurchaseSupplierDueReport.rdlc";
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);                
                parameters[1] = new ReportParameter("companyAddress", companyAddress);
                ReportDataSource rd = new ReportDataSource("dsPurchase", tblData);               
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.EnableExternalImages = true;
                rv.LocalReport.DataSources.Add(rd);                
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ReportForm(Enum reportType, string header, DataTable tblData, DataTable tblData1, DataTable tblDataPaymentMode, DataTable tblLoading, string senderEmail = "", string companyAddress = "", string customerEmail = "")
        {
            InitializeComponent();
            this.rv.RefreshReport();
            txtEmail.Text = senderEmail;
            switch (reportType)
            {
                case EnumReportType.CustomerDeliveryDaybook:
                    CustomerDeliveryDaybook(header, tblData, tblData1, tblDataPaymentMode, tblLoading);
                    break;
                
            }
        }
     
        //Payment & Reciept Voucher
        public ReportForm(string header,string address,string tranType, DataTable tblData, DataTable tblData1)
        {
            InitializeComponent();
            this.rv.RefreshReport();
            PaymentVoucher(header, address, tranType, tblData, tblData1);
        }
        public ReportForm(Enum reportType,string header, string header2, string header3, DataTable tblData)
        {
            InitializeComponent();
            this.rv.RefreshReport();
            switch (reportType)
            {
                case EnumReportType.CustomerAssetAgreement:
                    CustomerAssetAgreement(header, header2, header3, tblData);
                    break;

                case EnumReportType.CustomerAsset:
                    CustomerAsset(header, header2, header3, tblData);
                    break;

                case EnumReportType.Journal:
                    Journal(header,header2,header3, tblData);
                    break;

            }
        }
        

        //PRINT PDC INVOICE
        public ReportForm(Enum reportType, string header, string header1, DataTable tblData)
        {
            InitializeComponent();
            this.rv.RefreshReport();
            switch (reportType)
            {
                case EnumReportType.PurchaseSupplierDue:
                    PurchaseSupplierDue(header, header1, tblData);
                    break;
                case EnumReportType.PDCInvoice:
                    PDCInvoice(header, header1, tblData);
                    break;
                case EnumReportType.RouteSaleReport:
                    RouteSale(header, header1, tblData);
                    break;
                case EnumReportType.ProfitandLoss:
                    ProfitandLoss(header, header1, tblData);
                    break;
                case EnumReportType.Balancesheet:
                    Balancesheet(header, header1, tblData);
                    break;
                case EnumReportType.AccountStatement:
                    AccountStatement(header, header1, tblData);
                    break;
                case EnumReportType.TrialBalance:
                    TrialBalance(header, header1, tblData);
                    break;
                case EnumReportType.TrialBalanceDatewise:
                    TrialBalanceDatewise(header, header1, tblData);
                    break;
                case EnumReportType.TrialBalanceSummary:
                    TrialBalanceSummary(header, header1, tblData);
                    break;
                case EnumReportType.TrialBalanceSummaryDatewise:
                    TrialBalanceSummaryDatewise(header, header1, tblData);
                    break;
                case EnumReportType.SaleReportItemWise:
                    SalesReportItemwise(header, header1, tblData);
                    break;
                case EnumReportType.CustomerStatementAdvanced:
                    CustomerStatementAdvanced(header, header1, tblData);
                    break;
                case EnumReportType.DoDeliveryItems:
                    DoDeliveryItems(header, header1, tblData);
                    break;
                case EnumReportType.DOPendingInvoices:
                    DOPendingInvoices(header, header1, tblData);
                    break;
                case EnumReportType.LoadingSlip:
                    LoadingSlip(header, header1, tblData);
                    break;
                case EnumReportType.DSRCollection:
                    DSRCollection(header, header1, tblData);
                    break;

            }
                   
        }
        /// <summary>
        /// For 3 tables Include 
        /// DeliveryNote Invoice
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="header"></param>
        /// <param name="tblData"></param>
        /// <param name="tblData1"></param>
        /// <param name="tblData3"></param>
        public ReportForm(Enum reportType, string header, DataTable tblData, DataTable tblData1,DataTable tblData2)
        {
            InitializeComponent();
            this.rv.RefreshReport();
            switch (reportType)
            {
                case EnumReportType.DeliveryInvoice:
                    DeliveryInvoice(header, tblData, tblData1,tblData2);
                    break;
            }
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {

            
          
        }

        private void PaymentVoucher(string header, string address,string tranType, DataTable tblData, DataTable tblData1)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PaymentVoucher.rdlc";
                ReportDataSource rd = new ReportDataSource("dsPaymentVoucher", tblData);
                ReportDataSource rd1 = new ReportDataSource("dsPaymentVoucherDetails", tblData1);
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("companyAddress", address);
                parameters[2] = new ReportParameter("tranType", tranType);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void PDCInvoice(string header, string address, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PDCInvoice.rdlc";
                ReportDataSource rd = new ReportDataSource("dsPDCInvoice", tblData);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("companyAddress", address);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void RouteSale(string header, string header1, DataTable tblData)
        {
            try
            {
                bool withDo = false;
                try
                {
                    DataRow[] dorows = tblData.Select("DoSale >0 or SalesmanCredit>0");

                    if (dorows.Length > 0) withDo = true;
                }
                catch { }
                rv.LocalReport.ReportPath = $"{reportPath}\\RoutewisesaleReport.rdlc";
                if(withDo)
                    rv.LocalReport.ReportPath = $"{reportPath}\\RoutewisesaleReport1.rdlc";
                ReportDataSource rd = new ReportDataSource("dsRoutewiseSale", tblData);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("head", header);
                parameters[1] = new ReportParameter("employee", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ProfitandLoss(string header, string header1, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\ProfitandLossReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsPL", tblData);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("address", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void Balancesheet(string header, string header1, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\Balancesheet.rdlc";
                ReportDataSource rd = new ReportDataSource("dsBalancesheet", tblData);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("address", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void TrialBalance(string header, string header1, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\TrialBalance.rdlc";
                ReportDataSource rd = new ReportDataSource("dsTrialBalance", tblData);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("header1", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void TrialBalanceDatewise(string header, string header1, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\TrialBalanceDatewise.rdlc";
                ReportDataSource rd = new ReportDataSource("dsTrialBalance", tblData);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("header1", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void TrialBalanceSummary(string header, string header1, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\TrailBalanceSummaryReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsTrailBalanceSummary", tblData);
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
               // parameters[1] = new ReportParameter("address", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void TrialBalanceSummaryDatewise(string header, string header1, DataTable tblData)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\TrailBalanceSummaryDatewiseReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsTrailBalanceSummary", tblData);
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                // parameters[1] = new ReportParameter("address", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void PDCReport(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\pdcReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsPDCInvoice", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CouponBook(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CouponBook.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCouponBook", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CouponRedeemed(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CouponRedeemed.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCouponRedeemed", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CustomerPerformance(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerPerformance.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerPerformance", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CustomerMonthlyOutstanding(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerMonthlyOutstandingReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerMonthlyOutstanding", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CustomerWalletMisc(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerWalletMiscReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerWalletMisc", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[0];
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void MonthlyAnalyse(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\SaleMonthlyAnalyse.rdlc";
                ReportDataSource rd = new ReportDataSource("dsSaleMonthlyAnalyse", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void FOCSales(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\FOCSaleReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsFocReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void ItemDeliveryProduction(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\ItemDeliveryProductionReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsItemProductionDelivery", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DamageReport(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DamageReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDamageReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void TransferItem(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\TransferItem.rdlc";
                ReportDataSource rd = new ReportDataSource("dsTransferItem", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        // Prakash Tmr added
        private void TransferItemReportDateWise(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\ItemTransferDateWiseReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsItemTransferDateWiseReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ItemStockReport(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\ItemStockReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsItemStockReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DOSaleList(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DOSalesList.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDOSales", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CostCenterSummary(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CostCenterSummaryReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCostCenterSummary", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CostCenterDetailed(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CostCenterDetailedReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCostCenterDatewiseDetailed", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CustomerSummary(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerSummary.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerSummary", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void CustomerAsset(string header, string company, string companyAddress, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerAssetReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerAsset", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("company", company);
                parameters[2] = new ReportParameter("comanyAddress", companyAddress);

                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }


        public void CustomerAssetAgreement(string header, string header1, string rateandqty, DataTable tblData)
        {
            try
            {
                string rate = "", qty = "";
                if (!string.IsNullOrEmpty(rateandqty) && rateandqty.Contains("-"))
                {
                    rate = rateandqty.Split('-')[0];
                    qty = rateandqty.Split('-')[1];
                }

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerAssetAgreementReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerAssetAgreement", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("FirstParty", header);
                parameters[1] = new ReportParameter("SecondParty", header1);
                parameters[2] = new ReportParameter("monthlyPurchase", qty);
                parameters[3] = new ReportParameter("monthlyRate", rate);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public void DSRCollection(string header, string header1, DataTable tblData)
        {
            try
            {
               
                rv.LocalReport.ReportPath = $"{reportPath}\\DSRCollectionOnly.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCollectionOnly", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("EmployeeName", header);
                parameters[1] = new ReportParameter("DeliveryDate", header1);
                //parameters[2] = new ReportParameter("monthlyPurchase", qty);
               
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void CustomerAssetAll(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerAssetAllReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerAssetAll", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }


        public void Loading(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\LoadingReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsLoadReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void DeliverySummary(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DeliverySummaryReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDeliverySummary", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public void CollectionReport(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CollectionReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCollectionReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        public void OnlineReport(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\OnlineReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsOnlineReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public void CustomerStock(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerStock.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerStockReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }



        //---------------------------
        private void DeliveryLoadReport(string header, DataTable tblData)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DeliveryLoadReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDeliveryLoadReport", tblData);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CustomerList(string header,DataTable tblCustomer)
        {
            try
            {
                rv.LocalReport.ReportPath =  $"{reportPath}\\CustomerList.rdlc";
                
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("clmHead", header);
                
                ReportDataSource rd = new ReportDataSource("DataSetReport", tblCustomer);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void EmployeeReport(string header, DataTable tblCustomer)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\EmployeeReport.rdlc";

                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                ReportDataSource rd = new ReportDataSource("dsEmployeeReport", tblCustomer);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void PunchReport(string header, DataTable tblPunch)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PunchReport.rdlc";

                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                ReportDataSource rd = new ReportDataSource("dsPunchReport", tblPunch);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ProductionReport( DataTable tblProduction)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\ProductionReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsProductionReport", tblProduction);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ProductionRawmaterialReport(DataTable tblProduction)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\ProductionRawmaterialReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsProductionRawmaterialReport", tblProduction);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ItemsList(string header, DataTable tblItems)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\ItemList.rdlc";

                ReportParameter[] parameters = new ReportParameter[1];
                ReportDataSource rd = new ReportDataSource("dsItemList", tblItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ItemsValue(string header, DataTable tblItems)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\ItemValue.rdlc";
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("clmHeader", header);
                ReportDataSource rd = new ReportDataSource("dsItemValueReport", tblItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void ItemsTransaction(string header, DataTable tblItems)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\ItemTransaction.rdlc";
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                ReportDataSource rd = new ReportDataSource("dsItemTransaction", tblItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void PurchaseInvoice(string header, DataTable purchase,DataTable purchaseItems)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PurchaseInvoice.rdlc";

                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);

                ReportDataSource rd = new ReportDataSource("dsPurchase", purchase);
                ReportDataSource rd1 = new ReportDataSource("dsPurchaseItems", purchaseItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void PurchaseOrderInvoice(string header, DataTable purchase, DataTable purchaseItems)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PurchaseOrderInvoice.rdlc";

                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);

                ReportDataSource rd = new ReportDataSource("dsPurchase", purchase);
                ReportDataSource rd1 = new ReportDataSource("dsPurchaseItems", purchaseItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        string logoPath = $"file:///{Application.StartupPath}\\Images\\InvoiceLogo.jpg";
        private void SetInvoiceLogo()
        {
            try
            {
                if (!File.Exists($"{Application.StartupPath}\\Images\\InvoiceLogo.jpg"))
                    logoPath = $"file:///{Application.StartupPath}\\Images\\LogoBeTask.png";
            }
            catch (Exception ex)
            { }
        }
        private void SaleInvoice(string header, DataTable sale, DataTable saleItems,string companyAddress,string customerEmail)
        {
            try
            {
                txtEmail.Text = customerEmail;
                SetInvoiceLogo();
                rv.LocalReport.ReportPath = $"{reportPath}\\SalesInvoice.rdlc";
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("logopath", logoPath,true);
                parameters[2] = new ReportParameter("address", companyAddress);
                ReportDataSource rd = new ReportDataSource("dsSales", sale);
                ReportDataSource rd1 = new ReportDataSource("dsSalesItems", saleItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.EnableExternalImages = true;
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"> header means location</param>
        /// <param name="sale"></param>
        /// <param name="saleItems"></param>
        /// <param name="companyAddress"></param>
        /// <param name="customerEmail"></param>
        private void SaleInvoiceDO(string header, DataTable sale, DataTable saleItems, string companyAddress, string customerEmail)
        {
            try
            {
                string reportName = $"SalesInvoiceDO{header}.rdlc";
                txtEmail.Text = customerEmail;
                if (File.Exists($"{reportPath}\\{reportName}"))
                {
                    rv.LocalReport.ReportPath = $"{reportPath}\\{reportName}";
                }
                else
                {
                    rv.LocalReport.ReportPath = $"{reportPath}\\SalesInvoice.rdlc";

                }
                txtEmail.Text = customerEmail;
                
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("logopath", logoPath, true);
                parameters[2] = new ReportParameter("address", companyAddress);
                ReportDataSource rd = new ReportDataSource("dsSales", sale);
                ReportDataSource rd1 = new ReportDataSource("dsSalesItems", saleItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.EnableExternalImages = true;
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DOSaleInvoice(string header, DataTable sale, DataTable saleItems, string companyAddress, string customerEmail)
        {
            try
            {
                txtEmail.Text = customerEmail;
                SetInvoiceLogo();
                rv.LocalReport.ReportPath = $"{reportPath}\\DOSalesInvoice.rdlc";
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("logopath", logoPath, true);
                parameters[2] = new ReportParameter("address", companyAddress);
                ReportDataSource rd = new ReportDataSource("dsSales", sale);
                ReportDataSource rd1 = new ReportDataSource("dsSalesItems", saleItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.EnableExternalImages = true;
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void SalesReportCustomerwise(DataTable tblCustomer,string header)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\SalesReportCustomerwise.rdlc";
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                ReportDataSource rd = new ReportDataSource("dsSalesReportCustomerWise", tblCustomer);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void PurchaseReportSupplierwise(DataTable tblCustomer)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PurchaseReportSupplierwise.rdlc";
                ReportDataSource rd = new ReportDataSource("dsSalesReportCustomerWise", tblCustomer);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void PurchaseOrderReportSupplierwise(DataTable tblCustomer)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\PurchaseOrderReportSupplierwise.rdlc";
                ReportDataSource rd = new ReportDataSource("dsSalesReportCustomerWise", tblCustomer);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }


        private void SalesReportItemwise(string header,string header1,DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\SalesReportItemwise.rdlc";
                ReportDataSource rd = new ReportDataSource("dsSaleReportItemwise", tblItems);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("foc", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CustomerStatementAdvanced(string header, string header1, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerStatementAdvanced.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerStatementAdvanced", tblItems);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("Address", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DoDeliveryItems(string header, string header1, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DoDeliveryItemsReport.rdlc";
                ReportDataSource rd = new ReportDataSource("DSDoDeliveryItems", tblItems);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("subhead", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DOPendingInvoices(string header, string header1, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DODueInvoiceReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDueInvoice", tblItems);
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
             //   parameters[1] = new ReportParameter("subhead", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void LoadingSlip(string header, string header1, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\LoadingSlip.rdlc";
                ReportDataSource rd = new ReportDataSource("dsLoadingSlip", tblItems);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("deliveryid", header1);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="sale"></param>
        /// <param name="saleItems"></param>
        private void DeliveryInvoice(string header, DataTable delivery, DataTable deliveryItems,DataTable deliverySummary)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\DeliveryInvoice.rdlc";
                
                ReportDataSource rd = new ReportDataSource("dsDeliveryHead", delivery);
                ReportDataSource rd1 = new ReportDataSource("dsDeliveryItems", deliveryItems);
                ReportDataSource rd2 = new ReportDataSource("dsDeliveryItemSummary", deliverySummary);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.DataSources.Add(rd2);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="sale"></param>
        /// <param name="saleItems"></param>
        private void CustomerDeliveryDaybook(string header, DataTable deliveryHeader, DataTable deliveryDetail)
        {
            try
            {
                bool withDo = false;
                try
                {
                    DataRow[] dorows = deliveryDetail.Select("DoSale >0 or SalesmanCredit>0");

                    if (dorows.Length>0) withDo = true;
                }
                catch { }
                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerDeliveryDaybook.rdlc";
                if(withDo)
                    rv.LocalReport.ReportPath = $"{reportPath}\\CustomerDeliveryDaybook1.rdlc";
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("Header", header);
                rv.LocalReport.DataSources.Clear();
                ReportDataSource rd = new ReportDataSource("dsCustomerDeliveryDaybook", deliveryHeader);
                ReportDataSource rd1 = new ReportDataSource("dsCustomerDeliveryDaybookDetail", deliveryDetail);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CustomerDeliveryDaybook(string header, DataTable deliveryHeader, DataTable deliveryDetail,DataTable tblPaymentModes,DataTable tblLoading)
        {
            try
            {
                bool withDo = false;
                try
                {
                    string filter= "Paymentmode='DO' or Paymentmode='SalesmanCredit'";

                    // DataRow[] dorows = deliveryDetail.Select("DoSale >0 or SalesmanCredit>0");
                    DataRow[] dorows = deliveryDetail.Select(filter);

                    if (dorows.Length > 0) withDo = true;
                }
                catch { }
                 rv.LocalReport.ReportPath = $"{reportPath}\\CustomerDeliveryDaybook.rdlc";
               // rv.LocalReport.ReportPath = $"{reportPath}\\CustomerDeliveryDaybookDo.rdlc";
                if (withDo)
                    rv.LocalReport.ReportPath = $"{reportPath}\\CustomerDeliveryDaybookDo.rdlc";
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("Header", header);
                rv.LocalReport.DataSources.Clear();
                ReportDataSource rd = new ReportDataSource("dsCustomerDeliveryDaybook", deliveryHeader);
                ReportDataSource rd1 = new ReportDataSource("dsCustomerDeliveryDaybookDetail", deliveryDetail);
                ReportDataSource rd2 = new ReportDataSource("dsCustomerDeliveryPaymentModes", tblPaymentModes);
                ReportDataSource rd3 = new ReportDataSource("dsCustomerDeliveryItemLoading", tblLoading);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.DataSources.Add(rd1);
                rv.LocalReport.DataSources.Add(rd2);
                rv.LocalReport.DataSources.Add(rd3);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DeliveryReportStaffwise(DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DeliveryReportStaffWise.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDeliveryReportStaffwise", tblItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DeliveryReturnReport(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DeliveryReturnReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDeliveryReturnReport", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                 rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DeliveryReportItemwise(string header,DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DeliveryReportItemWise.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDeliveryReportItemwise", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void RoutewiseCashbook(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\RoutewiseCashbook.rdlc";
                ReportDataSource rd = new ReportDataSource("dsRoutewiseCashbook", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void RouteItemReport(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\RoutewiseItem.rdlc";
                ReportDataSource rd = new ReportDataSource("dsRoutewiseItem", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DailyCollection(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DailyCollection.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDailyCollection", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void DepositCollection(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DepositCollection.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDepositCollection", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void PaymentReport(string header,DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\PaymentReport.rdlc";
                ReportDataSource rd = new ReportDataSource("dsPaymentReport", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void Journal(string company,string address,string tranType ,DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\JournalVoucher.rdlc";
                ReportDataSource rd = new ReportDataSource("dsJournalVoucher", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("header", company);
                parameters[1] = new ReportParameter("address", address);
                parameters[2] = new ReportParameter("tranType", tranType);

                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void AccountStatement(string header, string header1, DataTable tblItems)
        {
            try
            {
                rv.LocalReport.ReportPath = $"{reportPath}\\AccountStatement.rdlc";

                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("header", header);
                parameters[1] = new ReportParameter("address", header1);
                ReportDataSource rd = new ReportDataSource("dsAccountStatement", tblItems);
                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DaybookSummary(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DaybookSummary.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDaybookSummary", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void DaybookDetailed(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\DaybookDetailed.rdlc";
                ReportDataSource rd = new ReportDataSource("dsDaybookDetailed", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CustomerStatementSummary(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerStatementSummary.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerStatementSummary", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        private void CustomerStatementDetailed(string header, DataTable tblItems)
        {
            try
            {

                rv.LocalReport.ReportPath = $"{reportPath}\\CustomerStatementDetailed.rdlc";
                ReportDataSource rd = new ReportDataSource("dsCustomerStatementDetailed", tblItems);
                rv.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("header", header);
                rv.LocalReport.DataSources.Add(rd);
                rv.LocalReport.SetParameters(parameters);
                rv.LocalReport.Refresh();
                rv.RefreshReport();
                rv.Show();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        /*
         Email Start
         */

        private bool ConvertReportToPDF(ReportViewer rep,out string exportPath)
        {
            exportPath = "";
            try
            {
                string reportType = "PDF";
                string mimeType;
                string encoding;

                string deviceInfo = "<DeviceInfo>" +
                   "  <OutputFormat>PDF</OutputFormat>" +
                   "  <PageWidth>8.27in</PageWidth>" +
                   "  <PageHeight>6.0in</PageHeight>" +
                   "  <MarginTop>0.2in</MarginTop>" +
                   "  <MarginLeft>0.2in</MarginLeft>" +
                   "  <MarginRight>0.2in</MarginRight>" +
                   "  <MarginBottom>0.2in</MarginBottom>" +
                   "</DeviceInfo>";

                Warning[] warnings;
                string[] streamIds;
                string extension = string.Empty;
                byte[] Bytes = rv.LocalReport.Render(format: "PDF", deviceInfo: "");
                //byte[] bytes = rep.LocalReport.Render(reportType, deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);
                //string localPath = System.Configuration.ConfigurationManager.AppSettings["TempFiles"].ToString();  
                // string localPath = AppDomain.CurrentDomain.BaseDirectory;
                string localPath = Application.StartupPath + "\\Exports\\";
                string fileName = Guid.NewGuid().ToString() + ".pdf";
                localPath = localPath + fileName;
                System.IO.File.WriteAllBytes(localPath, Bytes);
                exportPath = localPath;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Sorry Unable to Export reprot ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool SavePDF(ReportViewer viewer, string savePath)
        {
            try
            {
               
                byte[] Bytes = rv.LocalReport.Render(format: "PDF", deviceInfo: "");
              
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
            }
            catch (ReportViewerException ee)
            {
                MessageBox.Show("Sorry Unable to send email now ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ee.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        private void Email(string FilePath, string Email, bool MessageEmail)
        {
            if (!CheckNet())
            {
                if (MessageEmail)
                {
                    MessageBox.Show("No Internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            using (MailMessage mm = new MailMessage())
            {

                BETask.DAL.DAL.CompanyDAL comp = new BETask.DAL.DAL.CompanyDAL();
                var mailSett= comp.GetMailSettings();
                try
                {
                    mm.Subject = mailSett.mail_subject + " - " +emailHead;
                    //mm.Body = body + "<br /> FROM : " + HttpContext.Current.Session["cleintip"].ToString() + "<br />USER : " + HttpContext.Current.Session["username"].ToString() + "<br /><hr /><br /><br /><b>* Please do not reply to this message. Replies to this message are routed to an unmonitored mailbox. If you have questions please contact Your IT Team.</b>";
                    string html = " <br /> Please find the attached document ("+ emailHead + ") ";
                    StringBuilder sb = new StringBuilder();
                    string tab = "\t";
                    sb.AppendLine("<html>");
                    sb.AppendLine(tab + "<body>");
                    sb.Append("<h1><br /> Please find the attached  (" + emailHead + ") </h1> ");
                    sb.AppendLine(tab + "</body>");
                    sb.AppendLine("</html>");
                    mm.Body = sb.ToString();
                    string[] email = Email.Split(',');
                    for (int i = 0; i < email.Length; i++)
                    {
                        if (email[i] != string.Empty)
                        {

                            //if (System.Text.RegularExpressions.Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                            {
                                mm.To.Add(email[i].ToLower().ToString());

                            }

                        }
                    }
                    if (!String.IsNullOrEmpty( mailSett.cc1))
                    {
                        mm.CC.Add(mailSett.cc1);
                    }
                    if (!String.IsNullOrEmpty(mailSett.cc2))
                    {
                        mm.CC.Add(mailSett.cc1);
                    }
                    if (!String.IsNullOrEmpty(mailSett.bcc1))
                    {
                        mm.Bcc.Add(mailSett.bcc1);
                    }
                  
                  
                    try
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = mailSett.smtp_host;
                        smtp.EnableSsl = mailSett.enable_ssl == 1 ? true : false;
                        NetworkCredential NetworkCred = new NetworkCredential(mailSett.from_mail, mailSett.password);
                        mm.From = new MailAddress(mailSett.from_mail);
                        mm.IsBodyHtml = true;
                        Attachment att = new Attachment(FilePath);
                        mm.Attachments.Add(att);
                        smtp.UseDefaultCredentials = mailSett.smtp_use_deafaultcredential == 1 ? true : false; ;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = mailSett.smtp_port;
                        smtp.Timeout = mailSett.smtp_timeout;
                        smtp.Send(mm);
                        mm.Dispose();
                        if (MessageEmail)
                            MessageBox.Show("Email Sent", "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SmtpException ee)
                    {
                        if (MessageEmail)
                            MessageBox.Show(ee.Message, "Mail configuration missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "" /*&& System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")*/)
            {
                if (!System.IO.Directory.Exists(Application.StartupPath + "\\Exports\\"))
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Exports\\");

                string exportPath = "";
                //if (ConvertReportToPDF(reportViewer1, Application.StartupPath + "\\Exports\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".pdf"))
                //    Email(Application.StartupPath + "\\Exports\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".pdf", txtEmail.Text, true);

                if (ConvertReportToPDF(reportViewer1, out exportPath))
                    Email(exportPath, txtEmail.Text, true);
            }
            else
            {
                MessageBox.Show("Please Enter Valid Email ID", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
        }

        /*Email End*/
    }
}
