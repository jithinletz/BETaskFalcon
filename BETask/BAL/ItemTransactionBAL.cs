using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using RPT = BETask.Report.ReportForm;

namespace BETask.BAL
{

    public class ItemTransactionBAL
    {
        DAL.DAL.ItemTransactionDAL itemTransactionDAL = new REP.ItemTransactionDAL();

        public void SaveItemTransaction_BulkOpening(List<EDMX.item_transaction> listItem,bool updateStock)
        {
            try
            {
                itemTransactionDAL.SaveItemTransaction_BulkOpening(listItem, updateStock);
            }
            catch (Exception ee)
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
                    module_action = "Save Bulk Item Opening",
                    reference_id = 0,
                    summary = $" Item Bulk opening as on {listItem[0].transaction_date} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.item_transaction> GetItemOpeningReport(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return itemTransactionDAL.GetOpeningStockReport(dateFrom, dateTo);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }
        public void RemoveItemOpening(int itemTransactionId)
        {
            try
            {
                itemTransactionDAL.RemoveItemOpening(itemTransactionId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetClosingValueDetailed(string date)
        {
            try
            {
               return itemTransactionDAL.GetClosingValueDetailed(date);
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.item_transaction> GetItemTransaction(DateTime dateFrom, DateTime dateTo, int itemId, out decimal openingStock)
        {
            try
            {
                return itemTransactionDAL.GetItemTransaction(dateFrom, dateTo, itemId, out openingStock);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }
        public List<DAL.Model.ItemStockReportModel> GetItemStockReportData(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            try
            {
                return itemTransactionDAL.GetItemStockReportData(dateFrom, dateTo, itemId);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }



        public void PrintItemStockReportData(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            
            string header = $"Item Stock Report for the date between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)}";
            try
            {
                List<DAL.Model.ItemStockReportModel> listItemStockReportModel = GetItemStockReportData(dateFrom, dateTo, itemId);
                if (listItemStockReportModel != null && listItemStockReportModel.Count > 0)
                {

                    DataTable tblItemStock = new DataTable();
                    BETask.Report.DSReports.ItemStockDataTable ItemStockDataTable = new Report.DSReports.ItemStockDataTable();
                    tblItemStock = ItemStockDataTable.Clone();
                    foreach (DAL.Model.ItemStockReportModel item in listItemStockReportModel)                    {
                        DataRow dataRow = tblItemStock.NewRow();
                       // dataRow["Itemid"] = item.item_id;
                        dataRow["ItemName"] = item.Item_name;
                        dataRow["OpeningStock"] = item.Opening_Stock;
                        dataRow["Purchase"] = item.Purchase;
                        dataRow["ProductionIn"] = item.ProductionIn;                       
                        dataRow["ProductionOut"] = item.ProductionOut;
                        dataRow["Sale"] = item.Sale;
                        dataRow["Damage"] = item.Damage;
                        dataRow["TransferIn"] = item.TransferIn;
                        dataRow["TransferOut"] = item.TransferOut;                       
                        dataRow["ClosingStock"] = item.Closing_Stock;                     
                        dataRow["Date"] = General.ConvertDateAppFormat(item.Transaction_Date).ToString();
                        tblItemStock.Rows.Add(dataRow);
                        
                    }
                    if (tblItemStock != null && tblItemStock.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.ItemStockReport, header, tblItemStock);
                        reportForm.Show();
                    }
                }

            }
            catch
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
                    reference_id = 0,
                    summary = $" Item transaction Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintItemTransaction(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            decimal openingStock = 0;
            string header = $"Item Transaction Report for the date between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)}";
            try
            {
                List<EDMX.item_transaction> listItemTransaction = GetItemTransaction(dateFrom, dateTo, itemId, out openingStock);
                if (listItemTransaction != null && listItemTransaction.Count > 0)
                {
                    header += $" of {listItemTransaction[0].item.item_name}";
                    DataTable tblItemsList = new DataTable();
                    BETask.Report.DSReports.ItemTransactionDataTable itemsListDataTable = new Report.DSReports.ItemTransactionDataTable();
                    tblItemsList = itemsListDataTable.Clone();
                    foreach (EDMX.item_transaction item in listItemTransaction)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["Itemid"] = item.item_id;
                        dataRow["ItemName"] = item.item.item_name;
                        dataRow["Packing"] = item.item.uom_setting.uom_name;
                        dataRow["Cost"] = item.item_cost;
                        dataRow["TransactionType"] = item.transaction_type;
                        dataRow["AddedQty"] = item.qty_added;
                        dataRow["ReducedQty"] = item.qty_reduced;
                        dataRow["ClosingStock"] = item.closing_stock;
                        dataRow["StockValue"] = item.closing_value;
                        dataRow["OpeningStock"] = openingStock;
                        dataRow["TransactionDate"] = General.ConvertDateAppFormat(item.transaction_date).ToString();
                        tblItemsList.Rows.Add(dataRow);
                        openingStock = item.closing_stock;
                    }
                    if (tblItemsList != null && tblItemsList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.TransferItemReportDateWise, header, tblItemsList);
                        reportForm.Show();
                    }
                }

            }
            catch
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
                    reference_id = 0,
                    summary = $" Item transaction Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
    }
}
