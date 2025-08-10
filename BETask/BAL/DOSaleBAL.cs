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
   public class DOSaleBAL
    {
        public DOSaleDAL objDOSale = new DOSaleDAL();
        public int SaveDOSale( List<EDMX.do_sales> listDOSale,ref string saleNumber)
        {
            int doId = 0;
            try
            {
                doId = objDOSale.SaveDOSale(listDOSale,ref saleNumber);
            }
            catch (Exception ex)
            { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = doId,
                    summary = $" do Invoice generated  { saleNumber}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return doId;
        }
        public List<EDMX.do_sales> SearchDOSales(DateTime _date)
        {
            try
            {

                return objDOSale.SearchDOSales(General.ConvertDateServerFormatWithStartTime(_date), General.ConvertDateServerFormatWithEndTime(_date));
            }
            catch { throw; }
        }
        public List<EDMX.do_sales> SearchDOSales(DateTime  dateFrom, DateTime dateTo, int customerId = 0,int routeId = 0, string DoInvNo = "",string salesDoNo="")
        {
            try
            {

                return objDOSale.SearchDOSales(dateFrom, dateTo, customerId, routeId, DoInvNo, salesDoNo);
            }
            catch { throw; }
        }
        public EDMX.do_sales SearchDOSales(int _doId)
        {
            try
            {
                return objDOSale.SearchDOSales(_doId);
            }
            catch { throw; }
        }
        public List<EDMX.sales> SearchDOinSales(DateTime dateFrom, DateTime dateTo, int routeId = 0,int groupId=0)
        {
            try
            {
                return objDOSale.SearchDOinSales(dateFrom, dateTo, routeId, groupId);
            }
            catch { throw; }
        }
        public List<EDMX.sales> SearchDOinSalesFillerDivision(DateTime dateFrom, DateTime dateTo, int customerId,List<int> divisionIds)
        {
            try
            {
                return objDOSale.SearchDOinSalesFilterDivision(dateFrom, dateTo, customerId,divisionIds);
            }
            catch { throw; }
        }

        public List<EDMX.sales> SearchSaleWithCustomer(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0, List<int> divisionIds = null, bool active = true)
        {
            try
            {
                if (active)
                    return objDOSale.SearchSalesWithCustomerId(dateFrom, dateTo, customerId, routeId, divisionIds);
                else
                    return objDOSale.SearchSalesWithCustomerIdDoRemoved(dateFrom, dateTo, customerId, routeId, divisionIds);

            }
            catch { throw; }
        }
        public List<EDMX.sales> SearchSalesByDOId(int doId, int customerId, int routeId = 0)
        {
            try
            {
                return objDOSale.SearchSalesByDOId(doId, customerId, routeId);
            }
            catch { throw; }
        }
        public List<EDMX.sales> SearchSalesByDDate(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0)
        {
            try
            {
                return objDOSale.SearchSalesByDate(dateFrom, dateTo, customerId, routeId);
            }
            catch { throw; }
        }
        public List<EDMX.do_sales> SearchDOSaleWithCustomer(DateTime dateFrom, DateTime dateTo, int customerId, int routeId = 0)
        {
            try
            {
                return objDOSale.SearchDOSalesWithCustomerId(dateFrom, dateTo, customerId, routeId);
            }
            catch { throw; }
        }
        public void UpdateDOReciept(int doId, decimal paidAmount, DateTime date,string invoice)
        {
            try
            {
                objDOSale.UpdateDOReciept(doId, paidAmount, date);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = doId,
                    summary = $" Updating do Sale {invoice}-{paidAmount} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void UpateDeliveryLeaf(int saleId, string leafNo)
        {
            try
            {
                objDOSale.UpateDeliveryLeaf(saleId, leafNo);
            }
            catch (Exception ex)
            {
                throw;
            }
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
                    summary = $" Updating do Sale leaf to {leafNo}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintDOSaleInvoice(int _doid)
        {
            try
            {
                string remarks = "DO numbers";
                EDMX.do_sales _doSale = SearchDOSales(_doid);
                BETask.BAL.SaleBAL saleBAL = new SaleBAL();
                if (_doSale != null)
                {

                    DataTable tblSales = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.SalesHeadDataTable salesHeadDataTable = new Report.DSReports.SalesHeadDataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblSales = salesHeadDataTable.Clone();
                    tblSalesItems = salesItemDataTable.Clone();
                    string customerEmail = " ";
                   
                   
                    foreach (EDMX.do_sales_item dsi in _doSale.do_sales_item)
                    {
                        EDMX.sales _sales = saleBAL.GetSaleDetails(dsi.sales_id);
                        foreach (EDMX.sales_item item in _sales.sales_item)
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
                        remarks = $"{remarks} {_sales.delivery_leaf} ,";
                    }

                    if (_doSale.net_amount > 0)
                    {
                        customerEmail = _doSale.customer.email;
                        DataRow dataRow = tblSales.NewRow();
                        dataRow["CustomerName"] = _doSale.customer.customer_name;
                        dataRow["Address"] = string.Format("{0}\n{1}\n{2}\n{3}\n", _doSale.customer.address1, _doSale.customer.address2, _doSale.customer.city, _doSale.customer.pobox);
                        dataRow["SalesId"] = 0;//_sale.sales_id;
                        dataRow["Gross"] = _doSale.gross_amount;
                        dataRow["Discount"] = _doSale.total_discount;
                        dataRow["Taxable"] = _doSale.total_beforevat;
                        dataRow["VATAmount"] = _doSale.total_vat;
                        dataRow["RoundUp"] = 0;//_sale.roundup;
                        dataRow["Net"] = _doSale.net_amount;
                        dataRow["AmountInWord"] = General.NumToWord(_doSale.net_amount, false);
                        dataRow["Cashpaid"] = 0; //_sale.cash_paid;
                        dataRow["Remarks"] =$"{_doSale.remarks}  {remarks}";
                        dataRow["PaymentMode"] = "DO";//_sale.payment_mode;
                        dataRow["SalesNumber"] = _doSale.do_invoice_number;
                        dataRow["SalesDate"] = General.ConvertDateAppFormat(_doSale.do_date);
                        dataRow["TRN"] = _doSale.customer.trn;
                        tblSales.Rows.Add(dataRow);
                    }
                    if (tblSales != null && tblSales.Rows.Count > 0)
                    {
                        CompanyBAL companyBAL = new CompanyBAL();
                        CompanyModel company = companyBAL.GetCompanyDetails();
                        string address = $"{company.Name} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, TRN:{company.Tin}";
                        DataTable tblReport = new DataTable();
                        RPT reportForm = new RPT(RPT.EnumReportType.DOSalesInvoice, "Tax Invoice", tblSales, tblSalesItems, "", address, customerEmail);

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
                    reference_id = _doid,
                    summary = $" Print DO Sales Invoice  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintSavedDOList(List<EDMX.do_sales> listDOSales)
        {
            try
            {
                string trandate = string.Empty;            
              
                if (listDOSales != null && listDOSales.Count>0)
                {
                    DataTable tblDOSales = new DataTable();
                    DataTable tblDOSalesItems = new DataTable();
                    BETask.Report.DSReports.DOSalesDataTable DOSalesDataTable = new Report.DSReports.DOSalesDataTable();                  
                    tblDOSales = DOSalesDataTable.Clone();         
                    string customerEmail = " ";
                    foreach (EDMX.do_sales ds in listDOSales)
                    {

                        if (ds.net_amount > 0)
                        {
                            customerEmail = ds.customer.email;
                            DataRow dataRow = tblDOSales.NewRow();
                            dataRow["DOId"] = ds.do_id;
                            dataRow["CustomerName"] = ds.customer.customer_name;
                            dataRow["Gross"] = ds.gross_amount;
                            dataRow["Discount"] = ds.total_discount;
                            dataRow["Taxable"] = ds.total_beforevat;
                            dataRow["VATAmount"] = ds.total_vat;
                            dataRow["Net"] = ds.net_amount;
                            //dataRow["AmountInWord"] = General.NumToWord(ds.net_amount, false);
                            dataRow["Remarks"] = ds.remarks;
                            dataRow["DONumber"] = ds.do_invoice_number;
                            dataRow["SalesDate"] = General.ConvertDateTimeAppFormat(ds.do_date);
                            trandate = General.ConvertDateAppFormat(ds.do_date);
                            tblDOSales.Rows.Add(dataRow);
                        }
                    }
                    if (tblDOSales != null && tblDOSales.Rows.Count > 0)
                    {
                        string header = $"DO sale list on {trandate}";                        
                        DataTable tblReport = new DataTable();
                        RPT reportForm = new RPT(RPT.EnumReportType.DOSaleList, header, tblDOSales);
                        reportForm.Show();
                    }
                }
            }
            catch { throw; }
            
        }
        public void PrintDoDeliveryItems(int doId, int customerId, string header, string subhead, DateTime dateFrom, DateTime dateTo, bool activeOnly = true)
        {
            try
            {
                List<EDMX.sales> listSales = new List<EDMX.sales>();
                if (activeOnly)
                    listSales = SearchSalesByDOId(doId, customerId);
                else
                    listSales = SearchSalesByDDate(dateFrom, dateTo, customerId);

                if (listSales != null)
                {
                    DataTable tblDODeliveryItems = new DataTable();
                    BETask.Report.DSReports.DoDeliveryItemsDataTable DOSalesDataTable = new Report.DSReports.DoDeliveryItemsDataTable();
                    tblDODeliveryItems = DOSalesDataTable.Clone();
                    foreach (EDMX.sales ds in listSales)
                    {
                        foreach (EDMX.sales_item pi in ds.sales_item)
                        {
                            DataRow dataRow = tblDODeliveryItems.NewRow();
                            dataRow["DeliveryDate"] = ds.sales_date;
                            dataRow["DeliveryNo"] = ds.delivery_leaf == null ? "" : ds.delivery_leaf.ToUpper();
                            dataRow["ItemName"] = pi.item.item_name;
                            dataRow["Qty"] = pi.qty;
                            dataRow["Division"] = ds.customer_division == null ? "" : ds.customer_division.division_name;
                            tblDODeliveryItems.Rows.Add(dataRow);
                        }
                    }
                    if (tblDODeliveryItems != null && tblDODeliveryItems.Rows.Count > 0)
                    {

                        DataTable tblReport = new DataTable();
                        RPT reportForm = new RPT(RPT.EnumReportType.DoDeliveryItems, header, subhead, tblDODeliveryItems);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void PrintDoDeliveryItemsNoInvoiced(DateTime dateFrom,DateTime dateTo, int customerId, string header, string subhead,List<int> deliveryIds)
        {
            try
            {
                List<EDMX.sales> listSales = SearchSaleWithCustomer(General.ConvertDateServerFormatWithStartTime(dateFrom), General.ConvertDateServerFormatWithEndTime(dateTo), customerId,0, deliveryIds);
                if (listSales != null)
                {
                    DataTable tblDODeliveryItems = new DataTable();
                    BETask.Report.DSReports.DoDeliveryItemsDataTable DOSalesDataTable = new Report.DSReports.DoDeliveryItemsDataTable();
                    tblDODeliveryItems = DOSalesDataTable.Clone();
                    foreach (EDMX.sales ds in listSales)
                    {
                        foreach (EDMX.sales_item pi in ds.sales_item)
                        {
                            DataRow dataRow = tblDODeliveryItems.NewRow();
                            dataRow["DeliveryDate"] = ds.sales_date;
                            dataRow["DeliveryNo"] = ds.delivery_leaf == null ? "" : ds.delivery_leaf.ToUpper();
                            dataRow["ItemName"] = pi.item.item_name;
                            dataRow["Qty"] = pi.qty;
                            dataRow["Division"] = ds.customer_division == null ? "" : ds.customer_division.division_name;
                            tblDODeliveryItems.Rows.Add(dataRow);
                        }
                    }
                    if (tblDODeliveryItems != null && tblDODeliveryItems.Rows.Count > 0)
                    {

                        DataTable tblReport = new DataTable();
                        RPT reportForm = new RPT(RPT.EnumReportType.DoDeliveryItems, header, subhead, tblDODeliveryItems);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void PrintPendingInvoices(int customerId, DateTime date, string header,int routeId = 0)
        {
            try
            {
                List<EDMX.do_sales> listSale = objDOSale.GetPendingInvoices(date, customerId);
                if (listSale != null && listSale.Count > 0)
                {
                    DataTable tblPending = new DataTable();
                    BETask.Report.DSReports.DODueInvoicesDataTable DOSalesDataTable = new Report.DSReports.DODueInvoicesDataTable();
                    tblPending = DOSalesDataTable.Clone();
                    foreach (EDMX.do_sales ds in listSale)
                    {
                        if (ds.net_amount > ds.amount_paid)
                        {
                            DataRow dr = tblPending.NewRow();
                            dr["CustomerName"] = ds.customer.customer_name;
                            dr["Route"] = ds.customer.route!=null? ds.customer.route.route_name:"";
                            dr["InvoiceNumber"] = ds.do_invoice_number;
                            dr["InvoiceDate"] = General.ConvertDateAppFormat(ds.do_date);
                            dr["NetAmount"] = ds.net_amount;
                            dr["Recieved"] = ds.amount_paid;
                            dr["Due"] = ds.net_amount-ds.amount_paid;

                            tblPending.Rows.Add(dr);
                        }
                    }
                    if (tblPending != null && tblPending.Rows.Count > 0)
                    {

                        DataTable tblReport = new DataTable();
                        RPT reportForm = new RPT(RPT.EnumReportType.DOPendingInvoices, header, "", tblPending);
                        reportForm.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
