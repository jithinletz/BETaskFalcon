using System;
using EDMX = BETask.DAL.EDMX;
using EDMXApp = BETask.APP.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using RPT = BETask.Report.ReportForm;


namespace BETask.BAL
{
    public class TransferItemBAL
    {
        DAL.DAL.TransferItemsDAL transferItemsDAL = new DAL.DAL.TransferItemsDAL();
        public void SaveTransfer(EDMX.transfer_item transfer)
        {
            try
            {
                transferItemsDAL.SaveTransfer(transfer);
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
                    reference_id = transfer.item_id,
                    summary = $" Savingtransfer {transfer.transfer_type} Qty {transfer.qty}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.transfer_item> GetTransferItem(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return transferItemsDAL.GetTransferItem(dateFrom, dateTo);
            }
            catch { throw; }
        }
        public List<EDMX.transfer_item> GetTransferItemByItemId(DateTime dateFrom, DateTime dateTo,int ItemId)
        {
            try
            {
                return transferItemsDAL.GetTransferItemByItemId(dateFrom, dateTo, ItemId);
            }
            catch { throw; }
        }
        public EDMX.transfer_item GetTransferById(int transferId)
        {
            try
            {
                return transferItemsDAL.GetTransferById(transferId);
            }
            catch { throw; }
        }

        public void PrintItemTransferReport(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            try
            {
                string header = "";
                    header = $"Item Transfer Report for the date between { General.ConvertDateAppFormat(dateFrom)} and { General.ConvertDateAppFormat(dateTo)}";

                List<EDMX.transfer_item> listTransferItems = GetTransferItemByItemId(General.ConvertDateServerFormatWithStartTime(dateFrom), General.ConvertDateServerFormatWithEndTime(dateTo), itemId);
                DataTable tblTransferItems = new DataTable();
                BETask.Report.DSReports.TransferItemDataTable TransferItemDataTable = new Report.DSReports.TransferItemDataTable();
                tblTransferItems = TransferItemDataTable.Clone();
                foreach (EDMX.transfer_item TransferItem in listTransferItems)
                {                   
                    DataRow dr = tblTransferItems.NewRow();
                    dr["TransferDate"] = General.ConvertDateTimeAppFormat(TransferItem.transfer_date);
                    dr["ItemName"] = TransferItem.item.item_name;
                    dr["Qty"] = TransferItem.qty;
                    dr["TransferredBy"] = TransferItem.employee.first_name;
                    dr["Transfertype"] = TransferItem.transfer_type;
                    dr["Remarks"] = TransferItem.remarks;
                   

                    tblTransferItems.Rows.Add(dr);
                }
                if (tblTransferItems != null && tblTransferItems.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.TransferItemReportDateWise, header, tblTransferItems);
                    reportForm.Show();
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
                    summary = $" Print Item Transfer Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintTransfer(int transferId)
        {
            try
            {
                EDMX.transfer_item transfer_Item = transferItemsDAL.GetTransferById(transferId);
                if (transfer_Item != null)
                {
                    DataTable tblTransfer = new DataTable();

                    BETask.Report.DSReports.TransferItemDataTable transferItemDataTable = new Report.DSReports.TransferItemDataTable();
                    tblTransfer = transferItemDataTable.Clone();


                    DataRow dataRow = tblTransfer.NewRow();
                    dataRow["TransferId"] = transfer_Item.transfer_id;
                    dataRow["TransferDate"] = General.ConvertDateTimeAppFormat(transfer_Item.transfer_date);
                    dataRow["TransferredBy"] = $"{ transfer_Item.employee.first_name} {transfer_Item.employee.last_name}";
                    dataRow["ItemName"] = transfer_Item.item.item_name;
                    dataRow["Qty"] = transfer_Item.qty;
                    dataRow["TransferType"] = transfer_Item.transfer_type;
                    dataRow["Remarks"] = transfer_Item.remarks;

                    tblTransfer.Rows.Add(dataRow);
                        
                  
                    if (tblTransfer != null && tblTransfer.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.TransferItem, General.companyName, tblTransfer);
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
