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
  public  class ItemBAL
    {

        REP.ItemDAL itemDAL = new REP.ItemDAL();
        public void SaveItem(EDMX.item item)
        {
            try
            {

                int itemId = itemDAL.SaveItem(item);                
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
                    reference_id = item.item_id,
                    summary = $" Saving of Item {item.item_name}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public EDMX.item GetItemDetails(int item_id)
        {
            try
            {
                return itemDAL.GetItemDteials(item_id);
            }
            catch { throw; }
        }
        public int NextBarcode()
        {
            try
            {
                return itemDAL.NextBarcode();
            }
            catch { throw; }
        }

        public List<EDMX.item> GetAllItem(int item_id)
        {
            try
            {
               return itemDAL.GetAllItem(item_id);
            }
            catch { throw; }
        }
        public List<EDMX.item> GetAllItem_Rawmaterial()
        {
            try
            {
                return itemDAL.GetAllItem_Rawmaterials();
            }
            catch { throw; }
        }
        public List<EDMX.item> GetAllItem_Sellable()
        {
            try
            {
                return itemDAL.GetAllItem_Sellable();
            }
            catch { throw; }
        }
        public List<BETask.DAL.Model.CustomerAgreement_withClosingStockModel> GetCustomerRouteItems(int customerId, int routeId, int itemId)
        {
            try
            {
                return itemDAL.GetCustomerRouteItems( customerId,  routeId,  itemId);
            }
            catch { throw; }
        }
        public List<EDMX.item> GetDistinctDeliveryItemListByDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return itemDAL.GetDistinctDeliveryItemListByDate(dateFrom, dateTo);
            }
            catch { throw; }
        }
        public List<EDMX.item> GetDistinctDeliveryItemListByDate(int deliveryId)
        {
            try
            {
                return itemDAL.GetDistinctDeliveryItemListByDate(deliveryId);
            }
            catch { throw; }
        }
        public void PrintItemList(bool rowmaterial,bool sellable)
        {
            try
            {

                List<EDMX.item> _items=null;
                if (rowmaterial && sellable)
                    _items = itemDAL.GetAllItem(-1,true);
                else if (rowmaterial && !sellable)
                    _items = itemDAL.GetAllItem_Rawmaterials(true);
                else if (!rowmaterial && sellable)
                    _items = itemDAL.GetAllItem_Sellable(true);

                
                if (_items != null && _items.Count > 0)
                {
                    DataTable tblItemsList = new DataTable();
                    BETask.Report.DSReports.ItemsDataTable  itemsListDataTable = new Report.DSReports.ItemsDataTable();
                    tblItemsList = itemsListDataTable.Clone();
                    foreach (EDMX.item item in _items)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["Itemid"] = item.item_id;
                        dataRow["ItemName"] = item.item_name;
                        dataRow["Barcode"] = item.barcode;
                        dataRow["Brand"] = item.brand;
                        dataRow["Packing"] = item.uom_setting.uom_name;
                        dataRow["Cost"] = item.cost;
                        dataRow["PurchaseRate"] = item.purchase_rate;
                        dataRow["SalesRate"] = item.sale_rate;
                        dataRow["Stock"] = item.Stock;
                        dataRow["GodownStock"] = item.godown_stock;
                        dataRow["ItemType"] = item.rawmeterial==1?"Rawmaterial":"";
                        dataRow["ItemType"] += item.sellable == 1 ? " Prduct" : "";

                        tblItemsList.Rows.Add(dataRow);
                    }
                    
                    if (tblItemsList != null && tblItemsList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.ItemList, "Item List", tblItemsList);
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
                    summary = $" Item List Print  ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintItemValue(bool rowmaterial, bool sellable)
        {
            try
            {

                List<EDMX.item> _items = null;
                if (!rowmaterial && !sellable)
                    _items = itemDAL.GetAllItem(-1);
                else if (rowmaterial && !sellable)
                    _items = itemDAL.GetAllItem_Rawmaterials();
                else if (!rowmaterial && sellable)
                    _items = itemDAL.GetAllItem_Sellable();
                if (_items != null && _items.Count > 0)
                {
                    DataTable tblItemsList = new DataTable();
                    BETask.Report.DSReports.ItemValueDataTable itemsListDataTable = new Report.DSReports.ItemValueDataTable();
                    tblItemsList = itemsListDataTable.Clone();
                    foreach (EDMX.item item in _items)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["Itemid"] = item.item_id;
                        dataRow["ItemName"] = item.item_name;
                        dataRow["Packing"] = item.uom_setting.uom_name;
                        dataRow["Cost"] = item.cost;
                        dataRow["Stock"] = item.Stock;
                        dataRow["ItemValue"] = General.TruncateDecimalPlaces(item.cost*item.Stock);

                        tblItemsList.Rows.Add(dataRow);
                    }
                    if (tblItemsList != null && tblItemsList.Rows.Count > 0)
                    {
                        string subHead = "";
                        if (rowmaterial)
                            subHead = "Rawmaterial";
                        else if(sellable)
                            subHead = "Sellable";
                        string header = $"Item Stock & Value {subHead}";
                        RPT reportForm = new RPT(RPT.EnumReportType.ItemValue, header, tblItemsList);
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
                    summary = $" Item List Print  ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintItemDRouteReport( int customerId,  int routeId  ,int itemId,string header)
        {
            try
            {
                List<BETask.DAL.Model.CustomerAgreement_withClosingStockModel> listItems = GetCustomerRouteItems(customerId, routeId,itemId);
                DataTable tblItems = new DataTable();
                BETask.Report.DSReports.RouteItemsDataTable routeItemsDataTable = new Report.DSReports.RouteItemsDataTable();
                tblItems = routeItemsDataTable.Clone();
                foreach (BETask.DAL.Model.CustomerAgreement_withClosingStockModel item in listItems)
                {
                    string customerName = routeId == 0 ? $"{item.RouteName} - {item.CustomerName}" : item.CustomerName;
                    DataRow dr = tblItems.NewRow();
                  
                    dr["CustomerName"] = customerName;
                    dr["ItemName"] = item.ItemName;
                    dr["Packing"] = item.Packing;
                    dr["Qty"] = item.MaxQty;
                    dr["Rate"] = item.UnitPrice;
                    dr["ClosingStock"] = item.ClosingStock;                    
                    tblItems.Rows.Add(dr);
                }
                if (tblItems != null && tblItems.Rows.Count > 0)
                {
                    
                   
                    RPT reportForm = new RPT(RPT.EnumReportType.RouteItemReport, header, tblItems);
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
                    summary = $" Print Delivry Staffwise Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
    }
}
