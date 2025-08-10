using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;

namespace BETask.BAL
{
    public class SaleReturnBAL
    {
        public SaleReturnDAL objSale = new SaleReturnDAL();
        public int SaveSaleReturn(EDMX.sales_return sale, List<EDMX.sales_return_item> items)
        {
            int saleId = 0;
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
                    CreditPSaleAmount = sale.net_amount,
                    CreditSaleLedger = customer.GetCustomerDetail(sale.customer_id).LedgerId,
                    CashSaleAmount = sale.payment_mode.ToLower().Equals("cash") ? sale.net_amount : 0,
                    BankSaleAmount = sale.payment_mode.ToLower().Equals("bank") ? sale.net_amount : 0,
                    BankSaleLedger = Convert.ToInt32(sale.payment_mode.ToLower().Equals("bank") ? sale.bank_id : 0),

                };
                saleId = objSale.SaveSaleReturn(sale, items, saleAccount);
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
                    summary = $" Saving Sale . Items={items.Count} Net Amount={sale.net_amount} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return saleId;
        }
        public EDMX.sales_return GetSaleDetails(int saleId)
        {
            try
            {
                return objSale.GetSaleReturnDetails(saleId);
            }
            catch { throw; }
        }
        public List<EDMX.sales_return> SearchSale(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            try
            {
                return objSale.SearchSales(dateFrom, dateTo, vendorId);
            }
            catch { throw; }
        }
        public void PrintSale(int saleId)
        {
            try
            {
                EDMX.sales_return _sale = GetSaleDetails(saleId);
                if (_sale != null)
                {
                    string customerEmail = " ";
                    DataTable tblSales = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.SalesHeadDataTable salesHeadDataTable = new Report.DSReports.SalesHeadDataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblSales = salesHeadDataTable.Clone();
                    tblSalesItems = salesItemDataTable.Clone();
                    if (_sale.net_amount > 0)
                    {
                        DataRow dataRow = tblSales.NewRow();
                        customerEmail = _sale.customer.email;
                        dataRow["CustomerName"] = _sale.customer.customer_name;
                        dataRow["Address"] = string.Format("{0}\n{1}\n{2}\n{3}\n", _sale.customer.address1, _sale.customer.address2, _sale.customer.city, _sale.customer.pobox);
                        dataRow["SalesId"] = _sale.sales_return_id;
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
                    foreach (EDMX.sales_return_item item in _sale.sales_return_item)
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
                        RPT reportForm = new RPT(RPT.EnumReportType.SalesInvoice, "Sale Return Invoice", tblSales, tblSalesItems, "", address);
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
                    reference_id = saleId,
                    summary = $" Print Sales Return Invoice  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
    }
}
