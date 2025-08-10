using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BETask.Views;
using BETask.BAL;
using BETask.Common;
using System.Net;

namespace BETask.Views
{
    public partial class MDIBETask : Form
    {
        private int childFormNumber = 0;
        SynchronizationBAL sync = new SynchronizationBAL();
        int enableAutoSync = 0;
        bool deliveryIdgenerated = false;
        enum EnumForms
        {
            Company,
            Item,
            Customer, CustomerAgreement,
            Supplier, ItemSetting,
            AccountGroup, AccountLedger,
            ProductMap, Production,
            Employee,
            Purchase,
            PurchaseOrder,
            PurchaseReturn,
            Sales,
            SalesReturn,
            DeliveryNote, DeliveryTrack, DeliveryReturn, DailyCollection,
            Wallet,
            Payment,
            Reciept,
            Journal,
            Route,
            Synchronzation,
            OpeningBalance,
            EmployeePrivileges,
            ItemDamage,
            RouteItemReport,
            ProfitandLoss,
            BackUp,
            DeliveryStatus,
            SaleStatus,
            Exit,
            UpdateVersion,
            PDC,
            Screenshot,
            Coupon,
            DeliveryIDGenerate,
            TransferItems,
            Reconciliation,
            BulkItemOpen,
            LedgerMapSetting,
            DOSales,
            Building,
            CostCenter,
            SupplierPayment,
            OnlineOrder,
            IssueDOBook,
            CustomerRecieptDO,
            Loading,
            CustomerAsset,
            SalesMerge,
            PettyPayment,
            SalePending,
            //Reports
            ProductionReport,
            SaleCustomewise,
            SaleItemWIse,
            DeliveryEmployee,
            DeliveryItem,
            DeliveryReturnReport,
            DeliveryItemSummary,
            PurchaseSupplierwise,
            PurchaseItemwise,
            PurchaseOrderSupplierwise,
            PurchaseOrderItemwise,
            PaymentReciept,
            EmployeeReport,
            PunchingReport,
            ItemValueReport,
            LedgerStatement,
            Daybook,
            RoutewiseCashbook,
            CustomerStatement,
            CustomerStatementDetailed,
            SupplierStatement,
            CustomerListRoutewise,
            RoutewiseSale,
            CouponReport,
            CustomerPerformance,
            SalesAnalysis,
            FOCSales,
            ItemDeliveryProduction,
            CustomerOutstandingvsWallet,
            OpeningReport,
            WalletDifference,   
            WalletBalance,
            CustomerStock,
            TrialBalance,
            BalanceSheet,
            SaleDeliveryDiff,
            MonthlyOutstanding,
            RoutewiseCashStatement,
            DamageReport,
            ItemStockReport,
            ItemTransferReport,
            ItemLoading,
            DOSalesReport,
            CostSummaryReport,
            CostDetailedReport,
            CustomerSummary,
            CompareDeliverySaleCollection,
            CustomerAssetReport,
            DepositCollection,
            TrialBalanceDatewise,
            Offer,
            CollectionReport,
            ViewLedger,
            OnlineRecharge

        }
        enum EnumReports { CustomerList, SuppllierList, AllItems, RawmaterialItems, SellableItems }

        public MDIBETask()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void ShowFormEvent(object sender, EventArgs e)
        {
            if (sender == companyToolStripMenuItem)
            {
                ShowForm(EnumForms.Company);
            }
            if (sender == itemToolStrip || sender == toolStripItem)
            {
                ShowForm(EnumForms.Item);
            }
            else if (sender == AccountGroupsToolStrip)
            {
                ShowForm(EnumForms.AccountGroup);
            }
            else if (sender == AccountLedgerToolStrip)
            {
                ShowForm(EnumForms.AccountLedger);
            }
            else if (sender == paymentToolStripMenu)
            {
                ShowForm(EnumForms.Payment);
            }
            else if (sender == recieptToolStripMenu)
            {
                ShowForm(EnumForms.Reciept);
            }
            else if (sender == jounrnalEntryToolStripMenuItem)
            {
                ShowForm(EnumForms.Journal);
            }
            else if (sender == pettyPaymentToolStripMenuItem)
            {
                ShowForm(EnumForms.PettyPayment);
            }
            else if (sender == salePendingToolStripMenuItem)
            {
                ShowForm(EnumForms.SalePending);
            }
            
            else if (sender == itemSettingsToolStrip)
            {
                ShowForm(EnumForms.ItemSetting);
            }
            else if (sender == supplierToolStrip)
            {
                ShowForm(EnumForms.Supplier);
            }
            else if (sender == customerToolStrip || sender==tslCustomer)
            {
                ShowForm(EnumForms.Customer);
            }
            else if (sender == customerPerformanceToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerPerformance);
            }
            else if (sender == customerStockToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerStock);
            }
            else if (sender == monthlyOutstandingToolStripMenuItem)
            {
                ShowForm(EnumForms.MonthlyOutstanding);
            }
            else if (sender == routewiseCashStatementToolStripMenuItem)
            {
                ShowForm(EnumForms.RoutewiseCashStatement);
            }
            else if (sender == buildingToolStripMenuItem)
            {
                ShowForm(EnumForms.Building);
            }
            else if (sender == damageReportToolStripMenuItem)
            {
                ShowForm(EnumForms.DamageReport);
            }
            else if (sender == saleDeliveryDifferenceToolStripMenuItem)
            {
                ShowForm(EnumForms.SaleDeliveryDiff);
            }
            else if (sender == deliveryChartToolstrip)
            {
                ShowForm(EnumForms.SaleStatus);
            }
            else if (sender == salesAnalysysToolStripMenuItem)
            {
                ShowForm(EnumForms.SalesAnalysis);
            }
            else if (sender == couponToolStripMenuItem)
            {
                ShowForm(EnumForms.Coupon);
            }
            else if (sender == customerListRouteWiseToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerListRoutewise);
            }
            else if (sender == customerAgreementToolStrip)
            {
                ShowForm(EnumForms.CustomerAgreement);
            }
            else if (sender == productMapToolStrip)
            {
                ShowForm(EnumForms.ProductMap);
            }
            else if (sender == productionToolStrip || sender == productionToolStripMenu2)
            {
                ShowForm(EnumForms.Production);
            }
            else if (sender == employeeToolStrip)
            {
                ShowForm(EnumForms.Employee);
            }
            else if (sender == purchaseToolStripMenuItem1 || sender == toolStripPurchase)
            {
                ShowForm(EnumForms.Purchase);
            }
            else if (sender == purchaseOrderToolStripMenuItem)
            {
                ShowForm(EnumForms.PurchaseOrder);
            }
            else if (sender == purchaseReturnToolStrip)
            {
                ShowForm(EnumForms.PurchaseReturn);
            }
            else if (sender == purchaseReturnToolStrip)
            {
                ShowForm(EnumForms.PurchaseReturn);
            }
            else if (sender == salesToolStrip || sender == toolStripSales)
            {
                ShowForm(EnumForms.Sales);
            }
            else if (sender == generateDOInvoiceToolStripMenuItem )
            {
                ShowForm(EnumForms.DOSales);
            }
            else if (sender == mergeToolStripMenuItem)
            {
                ShowForm(EnumForms.SalesMerge);
            }
            
            else if (sender == issueDeliveryBookToolStripMenuItem)
            {
                ShowForm(EnumForms.IssueDOBook);
            }
            else if (sender == customerRecieptToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerRecieptDO);
            }
            else if (sender == salesReturnToolStripMenu)
            {
                ShowForm(EnumForms.SalesReturn);
            }
            else if (sender == customerWiseSaleToolStrip)
            {
                ShowForm(EnumForms.SaleCustomewise);
            }
            else if (sender == itemWiseSaleToolStrip)
            {
                ShowForm(EnumForms.SaleItemWIse);
            }
            else if (sender == deliveryNoteScheduleToolStrip || sender == toolStripDelivery)
            {
                ShowForm(EnumForms.DeliveryNote);
            }
            else if (sender == trackDeliveryToolStrip || sender == toolStripTrack)
            {
                ShowForm(EnumForms.DeliveryTrack);
            }
            else if (sender == deliveryIdGenerateToolStripMenuItem)
            {
                ShowForm(EnumForms.DeliveryIDGenerate);
            }
            else if (sender == transferItemsToolStripMenuItem)
            {
                ShowForm(EnumForms.TransferItems);
            }
            else if (sender == dailyCollectionToolStripMenuItem || sender == dailyCollectionReportToolStripMenuItem || sender==tslDailyCollection)
            {
                ShowForm(EnumForms.DailyCollection);
            }
            else if (sender == deliveryReturnToolStripMenuItem|| sender==tslReturn)
            {
                ShowForm(EnumForms.DeliveryReturn);
            }
            else if (sender ==  tslLoading)
            {
                ShowForm(EnumForms.Loading);
            }

            else if (sender == staffDeliveryToolStrip || sender == dailySalesDSRToolStripMenuItem || sender==tslDSR)
            {
                ShowForm(EnumForms.DeliveryEmployee);
            }
            else if (sender == walletRechargeToolStrip)
            {
                ShowForm(EnumForms.Wallet);
            }
            else if (sender == routeToolStripMenuItem)
            {
                ShowForm(EnumForms.Route);
            }
            else if (sender == itemDamageToolStripMenuItem)
            {
                ShowForm(EnumForms.ItemDamage);
            }
            else if (sender == routeItemsToolStripMenuItem)
            {
                ShowForm(EnumForms.RouteItemReport);
            }
            else if (sender == synchronizationToolStripMenuItem)
            {
                ShowForm(EnumForms.Synchronzation);
            }
            else if (sender == profitLossToolStripMenuItem)
            {
                ShowForm(EnumForms.ProfitandLoss);
            }
            else if (sender == trailBalanceToolStripMenuItem)
            {
                ShowForm(EnumForms.TrialBalance);
            }
            else if (sender == trialBalanceDatewiseToolStripMenuItem)
            {
                ShowForm(EnumForms.TrialBalanceDatewise);
            }
            else if (sender == balanceSheetToolStripMenuItem)
            {
                ShowForm(EnumForms.BalanceSheet);
            }
            else if (sender == backupToolStripMenuItem)
            {
                ShowForm(EnumForms.BackUp);
            }
            else if (sender == deliveryStatusToolStripMenuItem)
            {
                ShowForm(EnumForms.DeliveryStatus);
            }
            else if (sender == updateVersionToolStripMenuItem)
            {
                ShowForm(EnumForms.UpdateVersion);
            }
            else if (sender == reconciliationToolStripMenuItem)
            {
                ShowForm(EnumForms.Reconciliation);
            }
            else if (sender == bulkItemOpeningToolStripMenuItem)
            {
                ShowForm(EnumForms.BulkItemOpen);
            }
            else if (sender == ledgerMappingToolStripMenuItem)
            {
                ShowForm(EnumForms.LedgerMapSetting);
            }
            else if (sender == costCenterToolStripMenuItem)
            {
                ShowForm(EnumForms.CostCenter);
            }
            else if (sender == supplierPaymentToolStripMenuItem)
            {
                ShowForm(EnumForms.SupplierPayment);
            }
            else if (sender ==onlineOrdersToolStripMenuItem)
            {
                ShowForm(EnumForms.OnlineOrder);
            }
            else if (sender == assetToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerAsset);
            }
            else if (sender == assetReportToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerAssetReport);
            }
            else if (sender == depositCollectionToolStripMenuItem)
            {
                ShowForm(EnumForms.DepositCollection);
            }
            else if (sender == offerToolStripMenuItem)
            {
                ShowForm(EnumForms.Offer);
            }
            #region Reports
            else if (sender == datewiseProductionToolStrip)
            {
                ShowForm(EnumForms.ProductionReport);
            }
            else if (sender == itemDeliveryToolStrip)
            {
                ShowForm(EnumForms.DeliveryItem);
            }
            else if (sender == deliveryReturnToolStripMenuItem1)
            {
                ShowForm(EnumForms.DeliveryReturnReport);
            }
            else if (sender == supplierwisePurchaseToolStripMenuItem)
            {
                ShowForm(EnumForms.PurchaseSupplierwise);
            }
            else if (sender == itemWisePurchaseToolStripMenuItem)
            {
                ShowForm(EnumForms.PurchaseItemwise);
            }
            else if (sender == supplierWisePOToolStripMenuItem)
            {
                ShowForm(EnumForms.PurchaseOrderSupplierwise);
            }
            else if (sender == itemWisePOToolStripMenuItem)
            {
                ShowForm(EnumForms.PurchaseOrderItemwise);
            }
            else if (sender == deliveryItemSummaryToolStripMenuItem)
            {
                ShowForm(EnumForms.DeliveryItemSummary);
            }
            else if (sender == paymentAndReceiptToolStrip)
            {
                ShowForm(EnumForms.PaymentReciept);
            }
            else if (sender == jounrnalEntryToolStripMenuItem)
            {
                ShowForm(EnumForms.Journal);
            }
            else if (sender == ledgerStatementToolStripMenuItem)
            {
                ShowForm(EnumForms.LedgerStatement);
            }
            else if (sender == employeeReportToolStripMenuItem1)
            {
                ShowForm(EnumForms.EmployeeReport);
            }
            else if (sender == punchingReportToolStripMenuItem)
            {
                ShowForm(EnumForms.PunchingReport);
            }
            else if (sender == itemValueReportToolStripMenuItem)
            {
                ShowForm(EnumForms.ItemValueReport);
            }
            else if (sender == daybookToolStripMenuItem)
            {
                ShowForm(EnumForms.Daybook);
            }
            else if (sender == routewiseCashbookToolStripMenuItem)
            {
                ShowForm(EnumForms.RoutewiseCashbook);
            }
            else if (sender == customerStatementToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerStatement);
            }
            else if (sender == customerStatementDetaildToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerStatementDetailed);
            }
            else if (sender == supplierStatementToolStripMenuItem)
            {
                ShowForm(EnumForms.SupplierStatement);
            }
            else if (sender == routeWiseToolStripMenuItem)
            {
                ShowForm(EnumForms.RoutewiseSale);
            }
            else if (sender == fOCSalesToolStripMenuItem)
            {
                ShowForm(EnumForms.FOCSales);
            }
            else if (sender == openingBalanceToolStripMenuItem)
            {
                ShowForm(EnumForms.OpeningBalance);
            }
            else if (sender == pDCToolStripMenuItem)
            {
                ShowForm(EnumForms.PDC);
            }
            else if (sender == couponToolStripMenuItem1)
            {
                ShowForm(EnumForms.CouponReport);
            }
            else if (sender == itemDeliveryAndProductionToolStripMenuItem)
            {
                ShowForm(EnumForms.ItemDeliveryProduction);
            }
            else if (sender == outstandingAndWalletToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerOutstandingvsWallet);
            }
            else if (sender == openingToolStripMenuItem)
            {
                ShowForm(EnumForms.OpeningReport);
            }
            else if (sender == employeePrivilegesToolStripMenuItem)
            {
                ShowForm(EnumForms.EmployeePrivileges);
            }
            else if (sender == walletDifferenceToolStripMenuItem)
            {
                ShowForm(EnumForms.WalletDifference);
            }
            else if (sender == walletBalanceReportToolStripMenuItem)
            {
                ShowForm(EnumForms.WalletBalance);
            }
            else if (sender == itemStockReportToolStripMenuItem)
            {
                ShowForm(EnumForms.ItemStockReport);
            }
            else if (sender == itemTransferReportToolStripMenuItem)
            {
                ShowForm(EnumForms.ItemTransferReport);
            }
            else if (sender == itemLoadingToolStripMenuItem)
            {
                ShowForm(EnumForms.ItemLoading);
            }
            else if (sender == dOReportToolStripMenuItem)
            {
                ShowForm(EnumForms.DOSalesReport);
            }

            else if (sender == shareScreenshotToolStripMenuItem)
            {
                ShowForm(EnumForms.Screenshot);
            }
            else if (sender == exitToolStripMenuItem)
            {
                ShowForm(EnumForms.Exit);
            }
            else if (sender == CostSummaryToolStripMenuItem)
            {
                ShowForm(EnumForms.CostSummaryReport);
            }
            else if (sender == costcCenterdetailedToolStripMenuItem)
            {
                ShowForm(EnumForms.CostDetailedReport);
            }
            else if (sender == customerSummaryToolStripMenuItem)
            {
                ShowForm(EnumForms.CustomerSummary);
            }
            else if (sender == compareDeliverySaleCollectionToolStripMenuItem)
            {
                ShowForm(EnumForms.CompareDeliverySaleCollection);
            }
            else if (sender == collectionReportToolStripMenuItem)
            {
                ShowForm(EnumForms.CollectionReport);
            }
            else if (sender == viewLedgerToolStripMenuItem1)
            {
                ShowForm(EnumForms.ViewLedger);
            }
            else if (sender == onlineRechargeToolStripMenuItem)
            {
                ShowForm(EnumForms.OnlineRecharge);
            }
            #endregion Reports
        }
        private void ShowReportEvent(object sender, EventArgs e)
        {
            if (sender == customerListToolStrip)
            {
                ShowReport(EnumReports.CustomerList);
            }
            else if (sender == supplierListToolStrip)
            {
                ShowReport(EnumReports.SuppllierList);
            }
            else if (sender == allItemsToolStrip)
            {
                ShowReport(EnumReports.AllItems);
            }
            else if (sender == rawmaterialsToolStrip)
            {
                ShowReport(EnumReports.RawmaterialItems);
            }
            else if (sender == sellableProductsToolStrip)
            {
                ShowReport(EnumReports.SellableItems);
            }
           
        }
        private void ShowReport(Enum report)
        {
            try
            {
                ItemBAL item = new ItemBAL();
                switch (report)
                {
                    case EnumReports.CustomerList:
                        CustomerBAL customerBAL = new CustomerBAL();
                        customerBAL.PrintGetCustomerListRouteWise(0,0,true,false,DateTime.Now,DateTime.Now);
                        break;
                    case EnumReports.SuppllierList:
                        CustomerBAL _customerBAL = new CustomerBAL();
                        _customerBAL.CustomerList(2, 0);
                        break;
                    case EnumReports.AllItems:

                        item.PrintItemList(true, true);
                        break;
                    case EnumReports.RawmaterialItems:
                        item.PrintItemList(true, false);
                        break;
                    case EnumReports.SellableItems:
                        item.PrintItemList(false, true);
                        break;
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }
        }
        private void ShowForm(Enum form)
        {
            switch (form)
            {
                case EnumForms.Company:
                    CompanyForm company = new CompanyForm();
                    company.MdiParent = this;
                    company.Show();
                    break;
                case EnumForms.Item:
                    ItemForm item = new ItemForm();
                    item.MdiParent = this;
                    item.Show();
                    break;
                case EnumForms.ItemValueReport:
                    ItemValueReportForm itemValue = new ItemValueReportForm();
                    itemValue.MdiParent = this;
                    itemValue.Show();
                    break;
                case EnumForms.BulkItemOpen:
                    ItemOpeningStockForm itemOpen = new ItemOpeningStockForm();
                    itemOpen.MdiParent = this;
                    itemOpen.Show();
                    break;
                case EnumForms.AccountGroup:
                    AccountGroupForm account = new AccountGroupForm();
                    account.MdiParent = this;
                    account.Show();
                    break;
                case EnumForms.AccountLedger:
                    AccountLedgerForm ledger = new AccountLedgerForm();
                    ledger.MdiParent = this;
                    ledger.Show();
                    break;
                case EnumForms.LedgerMapSetting:
                    LedgerMappingForm ledgermap = new LedgerMappingForm();
                    ledgermap.MdiParent = this;
                    ledgermap.Show();
                    break;
                case EnumForms.CostCenter:
                    CostCenterForm costCenter = new CostCenterForm();
                    costCenter.MdiParent = this;
                    costCenter.Show();
                    break;
                case EnumForms.SupplierPayment:
                  SupplierPaymentForm supPayment= new SupplierPaymentForm();
                      supPayment.MdiParent = this;
                     supPayment.Show();
                    break;
                case EnumForms.OnlineOrder:
                    DeliveryRequestForm deliveryRequest = new DeliveryRequestForm();
                    deliveryRequest.MdiParent = this;
                    deliveryRequest.Show();
                    break;
                case EnumForms.Loading:
                    LoadManagementForm load = new LoadManagementForm();
                    load.MdiParent = this;
                    load.Show();
                    break;
                case EnumForms.Payment:
                    PaymentForm payment = new PaymentForm();
                    payment.MdiParent = this;
                    payment.Show();
                    break;
                case EnumForms.PaymentReciept:
                    PaymentReportForm paymentReport = new PaymentReportForm();
                    paymentReport.MdiParent = this;
                    paymentReport.Show();
                    break;
                case EnumForms.Reciept:
                    RecieptForm reciept = new RecieptForm();
                    reciept.MdiParent = this;
                    reciept.Show();
                    break;
                case EnumForms.ItemSetting:
                    CommonSettingsForm common = new CommonSettingsForm();
                    common.MdiParent = this;
                    common.Show();
                    break;
                case EnumForms.Supplier:
                    CustomerForm suppllier = new CustomerForm(2);
                    suppllier.MdiParent = this;
                    suppllier.Show();
                    break;
                case EnumForms.SupplierStatement:
                    CustomerStatementForm supplierStatement = new CustomerStatementForm(true);
                    supplierStatement.MdiParent = this;
                    supplierStatement.Show();
                    break;
                case EnumForms.CustomerStatementDetailed:
                    CustomerStatementDetailedForm customerStatementDet = new CustomerStatementDetailedForm();
                    customerStatementDet.MdiParent = this;
                    customerStatementDet.Show();
                    break;
                case EnumForms.CustomerPerformance:
                    CustomerPerformanceForm cpr = new CustomerPerformanceForm();
                    cpr.MdiParent = this;
                    cpr.Show();
                    break;
                case EnumForms.CustomerAsset:
                    CustomerAssetForm assetForm = new CustomerAssetForm();
                    assetForm.MdiParent = this;
                    assetForm.Show();
                    break;
                case EnumForms.CustomerAssetReport:
                    CustomerAssetReportForm assetReportForm = new CustomerAssetReportForm();
                    assetReportForm.MdiParent = this;
                    assetReportForm.Show();
                    break;
                case EnumForms.DepositCollection:
                    DepositCollectionForm depositCollection = new DepositCollectionForm();
                    depositCollection.MdiParent = this;
                    depositCollection.Show();
                    break;
                case EnumForms.SalesAnalysis:
                    SaleAnalyseReport slr = new SaleAnalyseReport();
                    slr.MdiParent = this;
                    slr.Show();
                    break;
                case EnumForms.Customer:
                    CustomerForm customer = new CustomerForm(1);
                    customer.MdiParent = this;
                    customer.Show();
                    break;
                case EnumForms.CustomerAgreement:
                    CustomerAggrementForm agreement = new CustomerAggrementForm();
                    agreement.MdiParent = this;
                    agreement.Show();
                    break;
                case EnumForms.Coupon:
                    CouponSaleForm coupon = new CouponSaleForm();
                    coupon.MdiParent = this;
                    coupon.Show();
                    break;
                case EnumForms.CustomerStatement:
                    CustomerStatementForm customerStatement = new CustomerStatementForm();
                    customerStatement.MdiParent = this;
                    customerStatement.Show();
                    break;
                case EnumForms.ProductMap:
                    ProductMapForm productMap = new ProductMapForm();
                    productMap.MdiParent = this;
                    productMap.Show();
                    break;
                case EnumForms.Production:
                    ProductionForm production = new ProductionForm();
                    production.MdiParent = this;
                    production.Show();
                    break;
                case EnumForms.Employee:
                    EmployeeForm employee = new EmployeeForm();
                    employee.MdiParent = this;
                    employee.Show();
                    break;
                case EnumForms.EmployeeReport:
                    EmployeeReportForm employeeRep = new EmployeeReportForm();
                    employeeRep.MdiParent = this;
                    employeeRep.Show();
                    break;
                case EnumForms.PunchingReport:
                    PunchForm employeePunchRep = new PunchForm();
                    employeePunchRep.MdiParent = this;
                    employeePunchRep.Show();
                    break;
                case EnumForms.Purchase:
                    PurchaseForm purchase = new PurchaseForm();
                    purchase.MdiParent = this;
                    purchase.Show();
                    break;
                case EnumForms.PurchaseOrder:
                    PurchaseOrderForm po = new PurchaseOrderForm();
                    po.MdiParent = this;
                    po.Show();
                    break;
                case EnumForms.PurchaseReturn:
                    PurchaseReturnForm purchaseReturn = new PurchaseReturnForm();
                    purchaseReturn.MdiParent = this;
                    purchaseReturn.Show();
                    break;
                case EnumForms.PurchaseSupplierwise:
                    SaleCustomerReport purcCustReport = new SaleCustomerReport(true);
                    purcCustReport.MdiParent = this;
                    purcCustReport.Show();
                    break;
                case EnumForms.PurchaseItemwise:
                    SaleItemWiseReportForm purchaseItemReport = new SaleItemWiseReportForm(true);
                    purchaseItemReport.MdiParent = this;
                    purchaseItemReport.Show();
                    break;
                case EnumForms.PurchaseOrderSupplierwise:
                    SaleCustomerReport poCustReport = new SaleCustomerReport(false, true);
                    poCustReport.MdiParent = this;
                    poCustReport.Show();
                    break;
                case EnumForms.PurchaseOrderItemwise:
                    SaleItemWiseReportForm poItemReport = new SaleItemWiseReportForm(false, true);
                    poItemReport.MdiParent = this;
                    poItemReport.Show();
                    break;
                case EnumForms.Sales:
                    SaleForm sale = new SaleForm();
                    sale.MdiParent = this;
                    sale.Show();
                    break;
                case EnumForms.DOSales:
                    DOForm doForm = new DOForm();
                    doForm.MdiParent = this;
                    doForm.Show();
                    break;
                case EnumForms.SalesMerge:
                    SaleMergingForm mergeForm = new SaleMergingForm();
                    mergeForm.MdiParent = this;
                    mergeForm.Show();
                    break;
                case EnumForms.IssueDOBook:
                    DeliveryBookForm doBookForm = new DeliveryBookForm();
                    doBookForm.MdiParent = this;
                    doBookForm.Show();
                    break;
                case EnumForms.CustomerRecieptDO:
                    DORecieptForm dOReciept = new DORecieptForm();
                    dOReciept.MdiParent = this;
                    dOReciept.Show();
                    break;
                case EnumForms.SalesReturn:
                    SaleReturnForm saleReturn = new SaleReturnForm();
                    saleReturn.MdiParent = this;
                    saleReturn.Show();
                    break;
                case EnumForms.SaleCustomewise:
                    SaleCustomerReport saleCustReport = new SaleCustomerReport();
                    saleCustReport.MdiParent = this;
                    saleCustReport.Show();
                    break;
                case EnumForms.SaleItemWIse:
                    SaleItemWiseReportForm saleItemReport = new SaleItemWiseReportForm();
                    saleItemReport.MdiParent = this;
                    saleItemReport.Show();
                    break;
                case EnumForms.DeliveryNote:
                    DeliveryForm delivery = new DeliveryForm();
                    delivery.MdiParent = this;
                    delivery.Show();
                    break;
                case EnumForms.DeliveryTrack:
                    DeliveryTrackForm deliveryTrack = new DeliveryTrackForm();
                    deliveryTrack.MdiParent = this;
                    deliveryTrack.Show();
                    break;

                case EnumForms.DeliveryIDGenerate:
                    DeliveryIDGeneratorForm deliveryIDGenerator = new DeliveryIDGeneratorForm();
                    deliveryIDGenerator.MdiParent = this;
                    deliveryIDGenerator.Show();
                    break;
                case EnumForms.TransferItems:
                    TransferItemsForm tranItems = new TransferItemsForm();
                    tranItems.MdiParent = this;
                    tranItems.Show();
                    break;
                case EnumForms.DeliveryReturn:
                    DeliveryReturnForm deliveryReturn = new DeliveryReturnForm();
                    deliveryReturn.MdiParent = this;
                    deliveryReturn.Show();
                    break;
                case EnumForms.DeliveryItem:
                    DeliveryItemReportForm deliveryItemReport = new DeliveryItemReportForm();
                    deliveryItemReport.MdiParent = this;
                    deliveryItemReport.Show();
                    break;
                case EnumForms.DeliveryItemSummary:
                    DeliveryItemSummaryReportForm deliveryItemSummary = new DeliveryItemSummaryReportForm();
                    deliveryItemSummary.MdiParent = this;
                    deliveryItemSummary.Show();
                    break;
                case EnumForms.DeliveryEmployee:
                    DeliveryStaffReportForm deliveryStaffReport = new DeliveryStaffReportForm();
                    deliveryStaffReport.MdiParent = this;
                    deliveryStaffReport.Show();
                    break;
                case EnumForms.DeliveryReturnReport:
                    DeliveryReturnReportForm deliveryReport = new DeliveryReturnReportForm();
                    deliveryReport.MdiParent = this;
                    deliveryReport.Show();
                    break;
                case EnumForms.DailyCollection:
                    DailyCollectionForm dailyCollection = new DailyCollectionForm();
                    dailyCollection.MdiParent = this;
                    dailyCollection.Show();
                    break;
                case EnumForms.Wallet:
                    WalletForm wallet = new WalletForm();
                    wallet.MdiParent = this;
                    wallet.Show();
                    break;
                case EnumForms.ProductionReport:
                    ProductionReportForm productionReport = new ProductionReportForm();
                    productionReport.MdiParent = this;
                    productionReport.Show();
                    break;
                case EnumForms.Route:
                    RouteForm route = new RouteForm();
                    route.MdiParent = this;
                    route.Show();
                    break;
                case EnumForms.Building:
                    BuildingForm building = new BuildingForm();
                    building.MdiParent = this;
                    building.Show();
                    break;
                case EnumForms.Journal:
                    JournalForm journal = new JournalForm();
                    journal.MdiParent = this;
                    journal.Show();
                    break;
                case EnumForms.PettyPayment:
                    PettyCashForm petty = new PettyCashForm();
                    petty.MdiParent = this;
                    petty.Show();
                    break;
                case EnumForms.SalePending:
                    SalePendingDeliveryForm salePending = new SalePendingDeliveryForm();
                    salePending.MdiParent = this;
                    salePending.Show();
                    break;
                case EnumForms.LedgerStatement:
                    AccountStatementForm accStatement = new AccountStatementForm();
                    accStatement.MdiParent = this;
                    accStatement.Show();
                    break;
                case EnumForms.Daybook:
                    DaybookForm daybook = new DaybookForm();
                    daybook.MdiParent = this;
                    daybook.Show();
                    break;
                case EnumForms.RoutewiseCashbook:
                    RotewiseCashBookForm routeCashbook = new RotewiseCashBookForm();
                    routeCashbook.MdiParent = this;
                    routeCashbook.Show();
                    break;
                case EnumForms.Synchronzation:
                    SynchronizationForm synch = new SynchronizationForm();
                    synch.MdiParent = this;
                    synch.Show();
                    break;
                case EnumForms.OpeningBalance:
                    OpeningBalanceForm opn = new OpeningBalanceForm();
                    opn.MdiParent = this;
                    opn.Show();
                    break;
                case EnumForms.OpeningReport:
                    OpeningBalanceReportForm opnR = new OpeningBalanceReportForm();
                    opnR.MdiParent = this;
                    opnR.Show();
                    break;
                case EnumForms.PDC:
                    PDCForm pdc = new PDCForm();
                    pdc.MdiParent = this;
                    pdc.Show();
                    break;
                case EnumForms.EmployeePrivileges:
                    PrivilegeForm prv = new PrivilegeForm();
                    prv.MdiParent = this;
                    prv.Show();
                    break;
                case EnumForms.ItemDamage:
                    ItemDamageForm itDamage = new ItemDamageForm();
                    itDamage.MdiParent = this;
                    itDamage.Show();
                    break;
                case EnumForms.RouteItemReport:
                    ItemRoutewiseReportForm routeReport = new ItemRoutewiseReportForm();
                    routeReport.MdiParent = this;
                    routeReport.Show();
                    break;
                case EnumForms.ProfitandLoss:
                    ProfitandLossForm pandl = new ProfitandLossForm();
                    pandl.MdiParent = this;
                    pandl.Show();
                    break;
                case EnumForms.TrialBalance:
                    TrialBalanceForm trial = new TrialBalanceForm();
                    trial.MdiParent = this;
                    trial.Show();
                    break;
                case EnumForms.TrialBalanceDatewise:
                    TrialBalanceDatewiseForm trialDatewise = new TrialBalanceDatewiseForm();
                    trialDatewise.MdiParent = this;
                    trialDatewise.Show();
                    break;
                case EnumForms.BalanceSheet:
                    BalanceSheetForm balance = new BalanceSheetForm();
                    balance.MdiParent = this;
                    balance.Show();
                    break;
                case EnumForms.CustomerListRoutewise:
                    CustomerListRoutewise routewise = new CustomerListRoutewise();
                    routewise.MdiParent = this;
                    routewise.Show();
                    break;
                case EnumForms.RoutewiseSale:
                    SaleRoutewiseReportForm routeSale = new SaleRoutewiseReportForm();
                    routeSale.MdiParent = this;
                    routeSale.Show();
                    break;
                case EnumForms.FOCSales:
                    FOCSaleReportForm focSale = new FOCSaleReportForm();
                    focSale.MdiParent = this;
                    focSale.Show();
                    break;
                case EnumForms.CouponReport:
                    CouponReportForm custPer = new CouponReportForm();
                    custPer.MdiParent = this;
                    custPer.Show();
                    break;
                case EnumForms.ItemDeliveryProduction:
                    ItemDeliveryProductionReport itdel = new ItemDeliveryProductionReport();
                    itdel.MdiParent = this;
                    itdel.Show();
                    break;
                case EnumForms.CustomerOutstandingvsWallet:
                    CustomerOutstandingWalletDifferenceForm custOutwallet = new CustomerOutstandingWalletDifferenceForm();
                    custOutwallet.MdiParent = this;
                    custOutwallet.Show();
                    break;
                case EnumForms.WalletDifference:
                    MiscWalletDifferenceForm wallDiff = new MiscWalletDifferenceForm();
                    wallDiff.MdiParent = this;
                    wallDiff.Show();
                    break;
                case EnumForms.CustomerStock:
                    CustomerStockForm custStock = new CustomerStockForm();
                    custStock.MdiParent = this;
                    custStock.Show();
                    break;
                case EnumForms.MonthlyOutstanding:
                    CustomerMonthlyOutstandingForm custForm = new CustomerMonthlyOutstandingForm();
                    custForm.MdiParent = this;
                    custForm.Show();
                    break;
                case EnumForms.RoutewiseCashStatement:
                    CashStatementRoutewise cashState = new CashStatementRoutewise();
                    cashState.MdiParent = this;
                    cashState.Show();
                    break;
                case EnumForms.DamageReport:
                    DamageReportForm damageRep = new DamageReportForm();
                    damageRep.MdiParent = this;
                    damageRep.Show();
                    break;
                case EnumForms.SaleDeliveryDiff:
                    SalesDeliveryDiffForm salediff = new SalesDeliveryDiffForm();
                    salediff.MdiParent = this;
                    salediff.Show();
                    break;
                case EnumForms.WalletBalance:
                    MiscWalletBalanceForm wallBal = new MiscWalletBalanceForm();
                    wallBal.MdiParent = this;
                    wallBal.Show();
                    break;
                case EnumForms.ItemStockReport:
                    ItemStockReportForm itemStock = new ItemStockReportForm();
                    itemStock.MdiParent = this;
                    itemStock.Show();
                    break;
                case EnumForms.ItemTransferReport:
                    TransferItemReportForm itemTransfer = new TransferItemReportForm();
                    itemTransfer.MdiParent = this;
                    itemTransfer.Show();
                    break;
                case EnumForms.ItemLoading:
                    DeliveryLoadReportForm loadRep = new DeliveryLoadReportForm();
                    loadRep.MdiParent = this;
                    loadRep.Show();
                    break;
                case EnumForms.DOSalesReport:
                    DOSalesReportForm doReport = new DOSalesReportForm();
                    doReport.MdiParent = this;
                    doReport.Show();
                    break;
                case EnumForms.CostSummaryReport:
                    CostCenterSummaryReportForm costSummary = new CostCenterSummaryReportForm();
                    costSummary.MdiParent = this;
                    costSummary.Show();
                    break;
                case EnumForms.CostDetailedReport:
                    CostCenterDetailedReportForm costDetailed = new CostCenterDetailedReportForm();
                    costDetailed.MdiParent = this;
                    costDetailed.Show();
                    break;
                case EnumForms.CustomerSummary:
                    CustomerSummaryRoutwise custSummary = new CustomerSummaryRoutwise();
                    custSummary.MdiParent = this;
                    custSummary.Show();
                    break;
                case EnumForms.CompareDeliverySaleCollection:
                    MiscDeliverySaleCollectionCompare costDCR = new MiscDeliverySaleCollectionCompare();
                    costDCR.MdiParent = this;
                    costDCR.Show();
                    break;
                case EnumForms.CollectionReport:
                    DailyCollectionReportForm collReport = new DailyCollectionReportForm();
                    collReport.MdiParent = this;
                    collReport.Show();
                    break;
                case EnumForms.ViewLedger:
                    ViewJournalForm viewJournal = new ViewJournalForm();
                    viewJournal.MdiParent = this;
                    viewJournal.Show();
                    break;
                case EnumForms.Reconciliation:
                    ReconsiliationForm reconcil = new ReconsiliationForm();
                    reconcil.MdiParent = this;
                    reconcil.Show();
                    break;
                case EnumForms.SaleStatus:
                    ChartDeliveryStatusForm chart = new ChartDeliveryStatusForm();
                    chart.ShowDialog();
                    break;
                case EnumForms.DeliveryStatus:
                    if (sync.CloudConnectionStatus(General.cloudConnection))
                    {
                        DeliveryStatusForm delStatus = new DeliveryStatusForm();
                        delStatus.ShowDialog();
                    }
                    break;
                case EnumForms.Offer:
                    OfferForm offer = new OfferForm();
                    offer.ShowDialog();
                    break;
                case EnumForms.BackUp:
                    BackUp();
                    break;
                case EnumForms.UpdateVersion:
                    UpdateVersion();
                    break;
                case EnumForms.Screenshot:
                    HelpBAL helpBAL = new HelpBAL();
                    helpBAL.Sharescreenshot();
                    break;
                case EnumForms.Exit:
                    Application.Exit();
                    break;
                case EnumForms.OnlineRecharge:
                    OnlinePaymentTransactionsForm online = new OnlinePaymentTransactionsForm();
                    online.MdiParent = this;
                    online.Show();
                    break;
            }
        }
        private void BackUp()
        {
            try
            {
                General.Backup();
                General.ShowMessage(General.EnumMessageTypes.Success, "Backup Completed");
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                General.ShowMessage(General.EnumMessageTypes.Error, ee.Message);
            }

        }

        private void MDIBETask_Load(object sender, EventArgs e)
        {
            try
            {
                FormLoad();
                CheckSyncronization();
               
               
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
            }

        }
        private void FormLoad()
        {
            CompanyBAL companyBAL = new CompanyBAL();
           var comp= companyBAL.GetCompanyDetails();
            tlsLoggdon.Text = $"User Name {General.userName} on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " "+ comp.City+" ";
            SetBackground();
            BuildFormTitle();
            Application.DoEvents();
            if (General.userName != "LETZ" && General.userName != "Q10")
                SetPrivilege();
            this.Text = $"{this.Text} - {General.companyName} ( {comp.City} )";
            UpdateVersion(true);
            toolStripLocation.Text = comp.City;

            try { 
                int isSundayDeliveryEnabled = BETask.Properties.Settings.Default.isSundayDeliveryEnabled;
                if (isSundayDeliveryEnabled == 1)
                {
                    tlsLoggdon.Text += "Sunday delivery enabled";
                }
            } catch { }


        }
        private void CheckSyncronization()
        {
            /*Checking auto synchronisation 1 is true */
            try { enableAutoSync = BETask.Properties.Settings.Default.enableAutoSync; } catch { }
            if (enableAutoSync == 1)
            {
                timer1.Start();
               // Application.DoEvents();
               //// tlLastSync.Text = $"Next auto sync at { DateTime.Now.AddMinutes(5)}";
               // backupToolStripMenuItem.Visible = true;
               // Application.DoEvents();
            }
            else
            {
                tlLastSync.Visible = false;
                linkSync.Visible = false;
                backupToolStripMenuItem.Visible = false;

            }
            /*********** End ***********/
        }
        private void SetBackground()
        {
            try
            {
                this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\bg.jpg");
                BackgroundImageLayout = ImageLayout.Stretch;


            }
            catch { }
        }
        private String BuildFormTitle()
        {

            String AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            String FormTitle = "";
            try
            {
                FormTitle = String.Format("{0} {1} ({2})",
                                            AppName,
                                            Application.ProductName,
                                            Application.ProductVersion);
                tlVersion.Text = tlVersion.Text += "            Version " + Application.ProductVersion.ToString() + "            " + Application.ProductName.ToString();
            }
            catch { }
            return FormTitle;

        }

        private void viewLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewJournalForm viewJournalForm = new ViewJournalForm();
            viewJournalForm.MdiParent = this;
            viewJournalForm.Show();
        }
        private void SetPrivilege()
        {
            string name = "";
            try
            {

                PrivilegeBAL privilegeBAL = new PrivilegeBAL();
                List<BETask.DAL.EDMX.privileges> listActivePrivileges = privilegeBAL.GetActivePrivileges(General.userId);
                //FULL Privilege
                var fullPrivilege = listActivePrivileges.Where(x => x.privilege_menu.menu_name == "FULL Privilege").FirstOrDefault();
                if (fullPrivilege != null)
                {
                    return;
                }
                foreach (Control ctrl in this.Controls)
                {
                    name = ctrl.Name;
                    MenuStrip menustrip = ctrl as MenuStrip;

                    //Leftside Icons
                    if (ctrl.Text == "ToolStrip")
                    {
                        ToolStrip toolStrip = ctrl as ToolStrip;
                        if (toolStrip != null)
                        {
                            foreach (ToolStripItem toolstripmenuitem in toolStrip.Items)
                            {
                                string menuTag = "";
                                if (toolstripmenuitem.Tag != null)
                                {

                                    menuTag = toolstripmenuitem.Tag.ToString().ToLower();
                                    var prv = listActivePrivileges.Where(x => x.privilege_menu.menu_name.ToLower() == menuTag).FirstOrDefault();
                                    if (prv == null)
                                        toolstripmenuitem.Enabled = false;
                                }

                            }
                        }
                    }

                    if (ctrl.Text == "MenuStrip")
                    {
                        foreach (ToolStripMenuItem toolstripmenuitem in menustrip.Items)
                        {
                            if (!toolstripmenuitem.HasDropDown)
                                continue;
                            foreach (ToolStripMenuItem submenu in toolstripmenuitem.DropDownItems)
                            {
                                string menuTag = "";
                                if (submenu.Tag != null)
                                {

                                    menuTag = submenu.Tag.ToString();
                                    var prv = listActivePrivileges.Where(x => x.privilege_menu.menu_name.ToLower() == menuTag.ToLower()).FirstOrDefault();
                                    if (prv == null)
                                        submenu.Enabled = false;
                                }
                                else
                                {
                                    if (submenu.HasDropDown)
                                    {
                                        foreach (ToolStripMenuItem submenu1 in submenu.DropDownItems)
                                        {

                                            if (submenu1.Tag != null)
                                            {
                                                menuTag = submenu1.Tag.ToString();
                                                var prv = listActivePrivileges.Where(x => x.privilege_menu.menu_name.ToLower() == menuTag.ToLower()).FirstOrDefault();
                                                if (prv == null)
                                                    submenu1.Enabled = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            sync.WalletGeneration();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.bgWorker5minutes.CancelAsync();
        }

        /// <summary>
        /// Every 15 Minutes 900000
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        int timerInterval = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (enableAutoSync == 1)
            {

                if(DateTime.Now.Hour>=1 && DateTime.Now.Hour<=3)
                {
                   
                    if (!deliveryIdgenerated)
                    {
                        int isSundayDeliveryEnabled = 2;
                        try { isSundayDeliveryEnabled = BETask.Properties.Settings.Default.isSundayDeliveryEnabled; } catch { }
                        DeliveryBAL deliveryBAL = new DeliveryBAL();
                        if (DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
                            deliveryBAL.GenerateDeliveryId(General.ConvertDateServerFormat(DateTime.Now));
                        else if (isSundayDeliveryEnabled == 1)
                            deliveryBAL.GenerateDeliveryId(General.ConvertDateServerFormat(DateTime.Now));

                        deliveryIdgenerated = true;
                        General.ErrorMustcheck($"Auto scheduled on {DateTime.Now}");
                    }
                }
                if (DateTime.Now.Hour >= 4 && deliveryIdgenerated)
                    deliveryIdgenerated = false;
            }

        }

        private void Synchronisation()
        {
            linkSync.Visible = false;
            Application.DoEvents();
            try
            {
               
                if (sync.CloudConnectionStatus(General.cloudConnection))
                {
                    Application.DoEvents();
                    tlCloudConnection.Text = "Checking for cloud server connection.... ";
                    tlCloudConnection.BackColor = Color.Orange;
                    Application.DoEvents();
                    this.bgWorker5minutes.RunWorkerAsync(2000);
                    Application.DoEvents();
                    tlCloudConnection.Text = "Cloud Connection Ready ";
                    tlCloudConnection.BackColor = Color.Green;
                }
                else
                {
                    tlLastSync.Text = "Next Sync" + DateTime.Now.AddMinutes(5).ToString("hh:mm:ss tt");
                    tlCloudConnection.Text = "App Connection Failed";
                    tlCloudConnection.BackColor = Color.Red;
                }
            }
            catch (Exception ee)
            {
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Synchronisation {ee.Message}", ee.ToString(), false);
                General.Error("Sync error" + ee.ToString());
            }
            finally
            {
                //linkSync.Visible = true;
            }
        }
       
        private void UpdateVersion(bool showUpdate=false)
        {
            try
            {
                tlsUpdateavailable.Visible = false;
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string version = client.DownloadString("http://productversion.letzservices.com/versions.txt");
                    System.IO.StringReader sr = new System.IO.StringReader(version);

                    string line;
                    bool updateSoftware = false;
                    string fileName = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains($"BETASK_{General.companyName}"))
                        {
                            if (line.Contains("-"))
                            {
                                string newVersion = line.Split('-')[1];
                                if (showUpdate)
                                    tlsUpdateavailable.Text += newVersion;
                                fileName = line.Split('-')[2];
                                if (newVersion == Application.ProductVersion)
                                {
                                    updateSoftware = false;
                                }
                                else
                                {
                                    updateSoftware = true;
                                }
                            }
                            break;
                        }
                    }
                    if (updateSoftware)
                    {
                        if (!showUpdate)
                            DownloadUpdated(fileName);
                        else
                            tlsUpdateavailable.Visible = true;

                    }
                    else
                    {
                        if (!showUpdate)
                            General.ShowMessage(General.EnumMessageTypes.Warning, "No Updates are available");
                       

                    }

                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                if (!showUpdate)
                    General.ShowMessage(General.EnumMessageTypes.Error, "Unable to update");
            }
        }
        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Application.DoEvents();
            tlUpdateStatus.Text = (String.Format("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                          (string)e.UserState,
                          e.BytesReceived,
                          e.TotalBytesToReceive,
                          e.ProgressPercentage));
            try
            {
                if (tlUpdateStatus.Text.Contains("100"))
                {
                    General.ShowMessage(General.EnumMessageTypes.Success, "Download Completed . Application will be closed");
                    System.Diagnostics.Process.Start($"{Application.StartupPath}\\updateBat.bat");
                    if (System.IO.File.Exists("BETaskVersionUpdater.exe"))
                        System.Diagnostics.Process.Start($"{Application.StartupPath}\\BETaskVersionUpdater.exe");
                }
            }
            catch (Exception ee)
            {
                string ss = ee.ToString();
            }

        }

        public void DownloadUpdated(string fileName)
        {
            try
            {
                if (!System.IO.Directory.Exists(Application.StartupPath + "\\Update"))
                    System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Update");
                string xFilename = Application.StartupPath + "Update\\" + fileName;
                if (System.IO.File.Exists(xFilename))
                {
                    System.IO.File.Delete(xFilename);
                }
                try
                {
                    if (System.IO.Directory.Exists(Application.StartupPath + "\\Update\\Reports"))
                    {
                        System.IO.Directory.Delete(Application.StartupPath + "\\Update\\Reports");
                    }
                }
                catch { }

                var webClient = new WebClient();
                webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;

                webClient.DownloadFileAsync(new Uri($"http://productversion.letzservices.com/{fileName}"), $@"{Application.StartupPath}\\Update\\" + fileName);
            }
            catch (Exception ee)
            {
                throw;
            }

        }

        private void linkSync_Click(object sender, EventArgs e)
        {
            Synchronisation();
        }

        /// <summary>
        /// Customer outstandong will take more time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        

        private void MDIBETask_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.Start($"{Application.StartupPath}\\updateBat.bat");
        }

       

        private void bgWorkerOutstanding_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
    }
}
