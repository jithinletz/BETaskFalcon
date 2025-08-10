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
    public class PurchaseOrderBAL
    {
        public PurchaseOrderDAL objPurchase = new PurchaseOrderDAL();
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchase"></param>
        public int SavePurchaseDetails(EDMX.purchase_order purchase, List<EDMX.purchase_order_item> items)
        {

            int purchaseId = 0;
            try
            {
                CustomerBAL customer = new CustomerBAL();
              
                purchaseId = objPurchase.SavePurchaseOrder(purchase, items);
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
                    reference_id = purchaseId,
                    summary = $" Saving Purchase Order . Net Amount={purchase.net_amount}, Items={items.Count}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return purchaseId;
        }
        public EDMX.purchase_order GetPurchaseDetails(int purchaseId)
        {
            try
            {
                return objPurchase.GetPurchaseDetails(purchaseId);
            }
            catch { throw; }
        }
        public void PrintPurchase(int purchaseId)
        {
            try
            {
                EDMX.purchase_order _purchase = GetPurchaseDetails(purchaseId);
                if (_purchase != null)
                {

                    DataTable tblPurchase = new DataTable();
                    DataTable tblPurchaseItems = new DataTable();
                    BETask.Report.DSReports.PurchaseHeadDataTable purchaseHeadDataTable = new Report.DSReports.PurchaseHeadDataTable();
                    BETask.Report.DSReports.PurchaseItemsDataTable purchaseItemDataTable = new Report.DSReports.PurchaseItemsDataTable();
                    tblPurchase = purchaseHeadDataTable.Clone();
                    tblPurchaseItems = purchaseItemDataTable.Clone();
                    if (_purchase.net_amount > 0)
                    {
                        DataRow dataRow = tblPurchase.NewRow();
                        dataRow["SupplierName"] = _purchase.customer.customer_name;
                        dataRow["Address"] = string.Format("{0}\n{1}\n{2}\n{3}\n", _purchase.customer.address1, _purchase.customer.address2, _purchase.customer.city, _purchase.customer.pobox);
                        dataRow["PurchaseId"] = _purchase.purchase_id;
                        dataRow["Gross"] = _purchase.gross_amount;
                        dataRow["Discount"] = _purchase.total_discount;
                        dataRow["Taxable"] = _purchase.total_beforevat;
                        dataRow["VATAmount"] = _purchase.total_vat;
                        dataRow["RoundUp"] = _purchase.roundup;
                        dataRow["Net"] = _purchase.net_amount;
                        dataRow["AmountInWord"] = General.NumToWord(_purchase.net_amount, false);
                        dataRow["Cashpaid"] = _purchase.cash_paid;
                        dataRow["Remarks"] = _purchase.remarks;
                        dataRow["PaymentMode"] = _purchase.payment_mode;
                        dataRow["InvoiceNumber"] = _purchase.invoice_number;
                        dataRow["InvoiceDate"] = General.ConvertDateAppFormat(_purchase.invoice_date);
                        dataRow["PurchaseDate"] = _purchase.purchase_date;
                        dataRow["TRN"] = _purchase.customer.trn;
                        tblPurchase.Rows.Add(dataRow);
                    }
                    foreach (EDMX.purchase_order_item item in _purchase.purchase_order_item)
                    {
                        if (item != null)
                        {
                            DataRow row = tblPurchaseItems.NewRow();
                            row["ItemCode"] = item.item.barcode;
                            row["ItemId"] = item.item_id;
                            row["ItemName"] = item.item.item_name;
                            row["Qty"] = item.qty;
                            row["Rate"] = item.rate;
                            row["Gross"] = item.gross_amount;
                            row["Discount"] = item.discount;
                            row["Taxable"] = item.total_beforevat;
                            row["VatAmount"] = item.vat_amount;
                            row["NetAmount"] = item.net_amount;
                            tblPurchaseItems.Rows.Add(row);
                        }
                    }

                    if (tblPurchase != null && tblPurchase.Rows.Count > 0)
                    {
                        CustomerBAL customerBAL = new CustomerBAL();
                        string email = customerBAL.GetCustomerDetail(_purchase.vendor_id).Email;
                        RPT reportForm = new RPT(RPT.EnumReportType.PurchaseOrderInvoice, "Purchase Order (LPO)", tblPurchase, tblPurchaseItems,email);
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
                    reference_id = purchaseId,
                    summary = $" Print Purchase Return Invoice  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.purchase_order> SearchPurchase(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            try
            {
                return objPurchase.SearchPurchase(dateFrom, dateTo, vendorId);
            }
            catch { throw; }
        }
        public List<EDMX.purchase_order_item> SearchPurchaseItems(int purchaseId)
        {
            try
            {
                return objPurchase.SearchPurchaseItem(purchaseId);
            }
            catch { throw; }
        }
        public void UpdateImportedItems(int[] orderitemsId, int purchaseItemId)
        {
            try
            {
                objPurchase.UpdateImportedItems(orderitemsId, purchaseItemId);
            }
            catch { throw; }
        }

        public List<EDMX.purchase_order> SupplierPurchaeReport(DateTime dateFrom, DateTime dateTo, int vendorId, string paymentmode)
        {
            try
            {
                return objPurchase.SupplierPurchaseReport(dateFrom, dateTo, vendorId, paymentmode);
            }
            catch { throw; }
        }
        public void PrintSupplierSalesReport(DateTime dateFrom, DateTime dateTo, int customerId, string paymentmode)
        {
            try
            {
                List<EDMX.purchase_order> listPurchase = SupplierPurchaeReport(dateFrom, dateTo, customerId, paymentmode);
                if (listPurchase != null && listPurchase.Count >= 0)
                {
                    DataTable tblPurchase = new DataTable();
                    DataTable tblSalesItems = new DataTable();
                    BETask.Report.DSReports.SalesHeadDataTable salesHeadDataTable = new Report.DSReports.SalesHeadDataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblPurchase = salesHeadDataTable.Clone();
                    tblSalesItems = salesItemDataTable.Clone();
                    foreach (EDMX.purchase_order _purchase in listPurchase)
                    {
                        if (_purchase != null)
                        {
                            DataRow dataRow = tblPurchase.NewRow();
                            dataRow["CustomerName"] = _purchase.customer.customer_name;
                            dataRow["Address"] = string.Format("{0}\n{1}\n{2}\n{3}\n", _purchase.customer.address1, _purchase.customer.address2, _purchase.customer.city, _purchase.customer.pobox);
                            dataRow["SalesId"] = _purchase.purchase_id;
                            dataRow["Gross"] = _purchase.gross_amount;
                            dataRow["Discount"] = _purchase.total_discount;
                            dataRow["Taxable"] = _purchase.total_beforevat;
                            dataRow["VATAmount"] = _purchase.total_vat;
                            dataRow["RoundUp"] = _purchase.roundup;
                            dataRow["Net"] = _purchase.net_amount;
                            // dataRow["AmountInWord"] = General.NumToWord(_purchase.net_amount, false);
                            dataRow["Cashpaid"] = _purchase.cash_paid;
                            dataRow["Remarks"] = _purchase.remarks;
                            dataRow["PaymentMode"] = _purchase.payment_mode;
                            dataRow["SalesNumber"] = _purchase.invoice_number;
                            dataRow["SalesDate"] = General.ConvertDateAppFormat(_purchase.invoice_date);
                            dataRow["TRN"] = _purchase.customer.trn;
                            tblPurchase.Rows.Add(dataRow);
                        }
                    }
                    if (tblPurchase != null && tblPurchase.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.PurchaseOrderRportSupplierwise, "Purchase Order", tblPurchase);
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
        public List<EDMX.purchase_order_item> ItemPurchaseReport(DateTime dateFrom, DateTime dateTo, int vendorId, int itemId)
        {
            try
            {
                return objPurchase.ItemPurchaseReport(dateFrom, dateTo, vendorId, itemId);
            }
            catch { throw; }
        }
        public void PrintItemPurchaseReport(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, string header)
        {
            try
            {
                PurchaseOrderDAL objPurchase = new PurchaseOrderDAL();
                List<EDMX.purchase_order_item> listPurchase = objPurchase.ItemPurchaseReport(dateFrom, dateTo, customerId, itemId);
                if (listPurchase != null && listPurchase.Count > 0)
                {

                    DataTable tblPurchaseItems = new DataTable();
                    BETask.Report.DSReports.SalesItemsDataTable salesItemDataTable = new Report.DSReports.SalesItemsDataTable();
                    tblPurchaseItems = salesItemDataTable.Clone();
                    foreach (EDMX.purchase_order_item _purchase in listPurchase)
                    {
                        if (_purchase != null)
                        {
                            DataRow dataRow = tblPurchaseItems.NewRow();
                            dataRow["ItemName"] = _purchase.item.item_name;
                            dataRow["Packing"] = _purchase.purchase.customer.customer_name;//_purchase.item.uom_setting.uom_name;
                            dataRow["SalesDate"] = General.ConvertDateAppFormat(_purchase.purchase_order.purchase_date);
                            dataRow["Qty"] = _purchase.qty;
                            dataRow["Rate"] = _purchase.rate;
                            dataRow["Gross"] = _purchase.gross_amount;
                            dataRow["Discount"] = _purchase.discount;
                            dataRow["Taxable"] = _purchase.total_beforevat;
                            dataRow["VatAmount"] = _purchase.vat_amount;
                            dataRow["NetAmount"] = _purchase.net_amount;
                            tblPurchaseItems.Rows.Add(dataRow);
                        }
                    }
                    if (tblPurchaseItems != null && tblPurchaseItems.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.SaleReportItemWise, header,"", tblPurchaseItems);
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
    }
}
