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
   public class PurchaseReturnBAL
    {
        public PurchaseReturnDAL objPurchase = new PurchaseReturnDAL();
        public int SavePurchaseReturnDetails(EDMX.purchase_return purchase, List<EDMX.purchase_return_item> items)
        {

            int purchaseId = 0;
            try
            {
                CustomerBAL customer = new CustomerBAL();
                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                DAL.Model.PurchaseAccountPostModel purchaseAccount = new DAL.Model.PurchaseAccountPostModel
                {
                    PurchaseAmount = purchase.gross_amount,
                    DiscountRecievedAmount = purchase.total_discount,
                    RoundOffAmount = purchase.roundup,
                    VatOnPurchaseAmount = purchase.total_vat,
                    CreditPurchaseAmount = purchase.net_amount,
                    CreditPurchaseLedger = customer.GetCustomerDetail(purchase.vendor_id).LedgerId,
                    CashPurchaseAmount = purchase.payment_mode.ToLower().Equals("cash") ? purchase.net_amount : 0,
                    BankPurchaseAmount = purchase.payment_mode.ToLower().Equals("bank") ? purchase.net_amount : 0,
                    BankPurchaseLedger = Convert.ToInt32(purchase.payment_mode.ToLower().Equals("bank") ? purchase.bank_id : 0),

                };
                purchaseId = objPurchase.SavePurchaseReturn(purchase, items, purchaseAccount);
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
                    summary = $" Saving Purchase Return . Net Amount={purchase.net_amount}, Items={items.Count}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return purchaseId;
        }
        public EDMX.purchase_return GetPurchaseReturnDetails(int purchaseId)
        {
            try
            {
                return objPurchase.GetPurchaseReturnDetails(purchaseId);
            }
            catch { throw; }
        }
        public List<EDMX.purchase_return> SearchPurchaseReturn(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            try
            {
                return objPurchase.SearchPurchaseReturn(dateFrom, dateTo, vendorId);
            }
            catch { throw; }
        }
        public void PrintPurchase(int purchaseId)
        {
            try
            {
                EDMX.purchase_return _purchase = GetPurchaseReturnDetails(purchaseId);
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
                        dataRow["PurchaseId"] = _purchase.purchase_return_id;
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
                    foreach (EDMX.purchase_return_item item in _purchase.purchase_return_item)
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
                        RPT reportForm = new RPT(RPT.EnumReportType.PurchaseInvoice, "Purchase Return", tblPurchase, tblPurchaseItems);
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
                    summary = $" Print Purchase Invoice  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
    }
}
