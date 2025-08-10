using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Linq;

namespace BETask.BAL
{
    public class SaleBAL
    {
        public SaleDAL objSale = new SaleDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchase"></param>
        public long SaveSale(EDMX.sales sale, List<EDMX.sales_item> items)
        {
            long saleId = 0;
            bool isEdit = sale.sales_id == 0 ? false : true;

            try
            {
                CustomerBAL customer = new CustomerBAL();
                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                DAL.Model.SaleAccountPostModel saleAccount = new DAL.Model.SaleAccountPostModel
                {
                    SalesAmount = sale.gross_amount,
                    DiscountAllowedAmount = sale.total_discount,
                    RoundOffAmount = sale.roundup,
                    VatOnSaleAmount = sale.total_vat,
                    CreditPSaleAmount = sale.net_amount!= (sale.gross_amount+ sale.total_vat+ sale.roundup)? (sale.gross_amount + sale.total_vat + sale.roundup): sale.net_amount,
                    CreditSaleLedger = customer.GetCustomerDetail(sale.customer_id).LedgerId,
                    CashSaleAmount = sale.payment_mode.ToLower().Equals("cash") ? sale.net_amount : 0,
                    BankSaleAmount = sale.payment_mode.ToLower().Equals("bank") ? sale.net_amount : 0,
                    BankSaleLedger = Convert.ToInt32(sale.payment_mode.ToLower().Equals("bank") ? sale.bank_id : 0),

                };
                saleId = objSale.SaveSale(sale, items, saleAccount);
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                string summary = $" Saving Sale  .Id={sale.sales_id} ,Customer={sale.customer_id} , Items={items.Count} ,Net Amount={sale.net_amount} ";
                if (isEdit)
                {
                    summary = $" Edit Sale  .Id={sale.sales_id} ,Customer={sale.customer_id} , Items={items.Count} ,Net Amount={sale.net_amount} ";
                }
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = !isEdit ? sf.GetMethod().Name : "EditSale",
                    reference_id = Convert.ToInt32( saleId.ToString()),
                    summary = summary

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return saleId;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        public EDMX.sales GetSaleDetails(long saleId)
        {
            try
            {
                return objSale.GetSaleDetails(saleId);
            }
            catch { throw; }
        }
        public long GetSaleIdByInvoice(string invoice)
        {
            try
            {
                return objSale.GetSaleIdByInvoice(invoice);
            }
            catch { throw; }
        }
        public List<EDMX.sales> SearchSale(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            try
            {
                return objSale.SearchSales(dateFrom, dateTo, vendorId);
            }
            catch { throw; }
        }

        public List<EDMX.sales_item> SearchSaleNoTax(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return objSale.SearchSaleNoTax(dateFrom, dateTo);
            }
            catch { throw; }
        }
        public void PrintSale(int saleId)
        {
            try
            {
                EDMX.sales _sale = GetSaleDetails(saleId);
                if (_sale != null)
                {

                    DataTable tblSales = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.SalesHeadDataTable salesHeadDataTable = new Report.DSReports.SalesHeadDataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblSales = salesHeadDataTable.Clone();
                    tblSalesItems = salesItemDataTable.Clone();
                    string customerEmail = "";
                    if (_sale.net_amount > 0)
                    {
                        customerEmail = _sale.customer.email;
                        DataRow dataRow = tblSales.NewRow();
                        dataRow["CustomerName"] = _sale.customer.customer_name;
                        dataRow["Address"] = string.Format("{0}\n{1}\n{2}\n{3}\n TRN : {4}", _sale.customer.address1, _sale.customer.address2, _sale.customer.city, _sale.customer.pobox, _sale.customer.trn);
                        dataRow["SalesId"] = _sale.sales_id;
                        dataRow["Gross"] = _sale.gross_amount;
                        dataRow["Discount"] = _sale.total_discount;
                        dataRow["Taxable"] = _sale.total_beforevat;
                        dataRow["VATAmount"] = _sale.total_vat;
                        dataRow["RoundUp"] = _sale.roundup;
                        dataRow["Net"] = _sale.net_amount;
                        dataRow["AmountInWord"] = General.NumToWord(_sale.net_amount, false);
                        dataRow["Cashpaid"] = _sale.cash_paid;
                        dataRow["Remarks"] = _sale.remarks;
                        dataRow["PaymentMode"] = _sale.payment_mode;
                        dataRow["SalesNumber"] = _sale.sales_number;
                        dataRow["SalesDate"] = General.ConvertDateAppFormat(_sale.sales_date);
                        dataRow["SalesDateMonth"] = _sale.sales_date.ToString("MMMM-yyyy"); ;
                        dataRow["TRN"] = _sale.customer.trn;
                        dataRow["LPO"] = _sale.lpo_number ;
                        tblSales.Rows.Add(dataRow);
                    }
                    foreach (EDMX.sales_item item in _sale.sales_item)
                    {
                        if (item != null)
                        {
                            DataRow row = tblSalesItems.NewRow();
                            row["ItemCode"] = item.item.barcode;
                            row["ItemId"] = item.item_id;
                            row["ItemName"] = item.item.item_name;
                            row["Qty"] = item.qty;
                            row["Rate"] = item.rate;
                            row["Gross"] = item.gross_amount;
                            row["Discount"] = item.discount;
                            row["Taxable"] = item.total_beforvat;
                            row["VatAmount"] = item.vat_amount;
                            row["NetAmount"] = item.net_amount;
                            tblSalesItems.Rows.Add(row);
                        }
                    }

                    if (tblSales != null && tblSales.Rows.Count > 0)
                    {
                        CompanyBAL companyBAL = new CompanyBAL();
                        CompanyModel company = companyBAL.GetCompanyDetails();
                        string address = $"{company.Name} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, TRN:{company.Tin}";
                        DataTable tblReport = new DataTable();
                        if (_sale.payment_mode != "DO")
                        {
                            RPT reportForm = new RPT(RPT.EnumReportType.SalesInvoice, "Tax Invoice", tblSales, tblSalesItems, "", address, customerEmail);
                            reportForm.Show();
                        }
                        else
                        {
                            RPT reportForm = new RPT(RPT.EnumReportType.SalesInvoiceDO, company.City.ToLower(), tblSales, tblSalesItems, "", address, customerEmail);
                            reportForm.Show();
                        }

                       
                    }
                }
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = saleId,
                    summary = $" Print Sales Invoice  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<EDMX.sales> CustomerSalesReport(DateTime dateFrom, DateTime dateTo, int customerId, string paymentmode, int routeId = 0)
        {
            try
            {
                return objSale.CustomerSalesReport(dateFrom, dateTo, customerId, paymentmode, routeId);
            }
            catch { throw; }
        }
        public void PrintCustomerSalesReport(DateTime dateFrom, DateTime dateTo, int customerId, string paymentmode, int routeId, string header)
        {
            try
            {
                List<EDMX.sales> listSale = objSale.CustomerSalesReport(dateFrom, dateTo, customerId, paymentmode, routeId);
                if (listSale != null && listSale.Count >= 0)
                {
                    DataTable tblSales = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.SalesHeadDataTable salesHeadDataTable = new Report.DSReports.SalesHeadDataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblSales = salesHeadDataTable.Clone();
                    tblSalesItems = salesItemDataTable.Clone();
                    foreach (EDMX.sales _sale in listSale)
                    {
                        if (_sale != null)
                        {
                            DataRow dataRow = tblSales.NewRow();
                            dataRow["CustomerName"] = _sale.customer.customer_name;
                            dataRow["Address"] = string.Format("{0}\n{1}\n{2}\n{3}\n", _sale.customer.address1, _sale.customer.address2, _sale.customer.city, _sale.customer.pobox);
                            dataRow["SalesId"] = _sale.sales_id;
                            dataRow["Gross"] = _sale.gross_amount;
                            dataRow["Discount"] = _sale.total_discount;
                            dataRow["Taxable"] = _sale.total_beforevat;
                            dataRow["VATAmount"] = _sale.total_vat;
                            dataRow["RoundUp"] = _sale.roundup;
                            dataRow["Net"] = _sale.net_amount;
                            dataRow["AmountInWord"] = General.NumToWord(_sale.net_amount, false);
                            dataRow["Cashpaid"] = _sale.cash_paid;
                            dataRow["Remarks"] = _sale.remarks;
                            dataRow["PaymentMode"] = _sale.payment_mode;
                            dataRow["SalesNumber"] = _sale.sales_number;
                            dataRow["SalesDate"] = General.ConvertDateAppFormat(_sale.sales_date);
                            dataRow["TRN"] = _sale.customer.trn;
                            tblSales.Rows.Add(dataRow);
                        }
                    }
                    if (tblSales != null && tblSales.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.SalesReportCustomerwise, header, tblSales);
                        reportForm.Show();
                    }
                }
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Print Customerwise Sales Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<EDMX.sales_item> ItemSalesReport(DateTime dateFrom, DateTime dateTo, int customerId, int itemId,int routeId=0,decimal rangeFrom =0,decimal rangeTo=0,string paymentMode="")
        {
            try
            {
                return objSale.ItemSalesReport(dateFrom, dateTo, customerId, itemId, routeId,rangeFrom,rangeTo, paymentMode);
            }
            catch { throw; }
        }
        public List<DAL.Model.SalesItem> ItemSalesReportNew(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, int routeId = 0, decimal rangeFrom = 0, decimal rangeTo = 0, string paymentMode = "")
        {
            try
            {
                return objSale.ItemSalesReportNew(dateFrom, dateTo, customerId, itemId, routeId, rangeFrom, rangeTo, paymentMode);
            }
            catch { throw; }
        }

        public void PrintItemSalesReport(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, string header, int routeId = 0, decimal rangeFrom = 0, decimal rangeTo = 0, string paymentMode = "")
        {
            try
            {
                var listSale = objSale.ItemSalesReportNew(dateFrom, dateTo, customerId, itemId, routeId, rangeFrom, rangeTo, paymentMode);
                if (listSale != null && listSale.Count > 0)
                {
                    string _foc = "";
                    try
                    {
                        decimal foc = 0;
                        foc = listSale.Where(x => x.Rate <= 0).Sum(x => x.Quantity);

                        if (foc > 0) _foc = $"FOC {foc }";
                    }
                    catch { }
                    DataTable tblSales = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblSalesItems = salesItemDataTable.Clone();
                    foreach (var _sale in listSale)
                    {
                        if (_sale != null)
                        {
                            DataRow dataRow = tblSalesItems.NewRow();
                            dataRow["ItemName"] = _sale.ItemName;
                            dataRow["Packing"] = _sale.CustomerName;//_sale.item.uom_setting.uom_name;
                            dataRow["SalesDate"] = General.ConvertDateAppFormat(_sale.SalesDate);
                            dataRow["Qty"] = _sale.Quantity;
                            dataRow["Rate"] = _sale.Rate;
                            dataRow["Gross"] = _sale.GrossAmount;
                            dataRow["Discount"] = _sale.Discount;
                            dataRow["Taxable"] = _sale.TotalBeforeVAT;
                            dataRow["VatAmount"] = _sale.VATAmount;
                            dataRow["NetAmount"] = _sale.NetAmount;
                            tblSalesItems.Rows.Add(dataRow);
                        }
                    }
                    if (tblSalesItems != null && tblSalesItems.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.SaleReportItemWise, header, _foc, tblSalesItems);
                        reportForm.Show();
                    }
                }
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Print Itemwise Sales Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.SP_SalesMonthlyAnalysis_Result> GetSaleMonthlyAnaysis(int year)
        {
            try
            {
                return objSale.GetSaleMonthlyAnaysis(year);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<EDMX.SP_SalesMonthlyItemAnalysis_Result> GetSaleMonthlyItemAnaysis(int year, int itemId)
        {
            try
            {
                return objSale.GetSaleMonthlyItemAnaysis(year, itemId);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<EDMX.sales_item> SearchItemFocReport(DateTime dateFrom, DateTime dateTo, int customerId, int routeId, int itemId)
        {
            try
            {
                return objSale.SearchItemFocReport(dateFrom, dateTo, customerId, routeId, itemId);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void PrintFOCSalesReport(DateTime dateFrom, DateTime dateTo, int customerId, int routeId, int itemId, string header)
        {
            try
            {
                List<EDMX.sales_item> listSale = objSale.SearchItemFocReport(dateFrom, dateTo, customerId, routeId, itemId);
                if (listSale != null && listSale.Count > 0)
                {
                    DataTable tblSales = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.FOCSaleDataTable salesItemDataTable = new Report.DSReports.FOCSaleDataTable();
                    tblSalesItems = salesItemDataTable.Clone();
                    foreach (EDMX.sales_item _sale in listSale)
                    {
                        if (_sale != null)
                        {
                            DataRow dataRow = tblSalesItems.NewRow();
                            dataRow["RouteName"] = _sale.sales.customer.route.route_name;
                            dataRow["Customer"] = _sale.sales.customer.customer_name;
                            dataRow["SalesDate"] = General.ConvertDateTimeAppFormat(_sale.sales.sales_date);
                            dataRow["Qty"] = _sale.qty;
                            dataRow["ItemName"] = _sale.item.item_name;

                            tblSalesItems.Rows.Add(dataRow);
                        }
                    }
                    if (tblSalesItems != null && tblSalesItems.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.FOCSales, header, tblSalesItems);
                        reportForm.Show();
                    }
                }
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Print Itemwise Sales Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<DAL.Model.SaleDeliveryDiffModel> GetSalesDeliveryDifference(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            try
            {
                return objSale.GetSalesDeliveryDifference(dateFrom, dateTo, itemId);
            }
            catch
            {
                throw;
            }
        }

        //Chart
        public BETask.DAL.Model.ChartDataModel GetChartData(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            try
            {
                return objSale.GetChartData(dateFrom, dateTo, itemId);
            }
            catch
            {
                throw;
            }
        }

        public List<EDMX.delivery_items> SaleNotGeneratedDeliveries(DateTime date, int itemId)
        {
            try
            {
                return objSale.SaleNotGeneratedDeliveries(date, itemId);
            }
            catch { throw; }
        }

        public long GenerateSaleFromDelivery(int deliveryId, int deliveryItemId,int oldLeaf)
        {
            try
            {
                return objSale.GenerateSaleFromDelivery(deliveryId, deliveryItemId, oldLeaf);
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = deliveryItemId,
                    summary = $" Pending invoice generated {deliveryId}-{deliveryItemId}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }


        }
}
