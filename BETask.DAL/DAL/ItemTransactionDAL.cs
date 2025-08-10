using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;
using System.Data.SqlClient;

namespace BETask.DAL.DAL
{
    public enum EnumTransactionTypes { PURCHASE, SALE, PRETURN, SRETURN, DRETURN, PRODUCTION, OPENING, DAMAGE }
    public class ItemTransactionDAL
    {
        public void SaveItemTransaction(EDMX.item_transaction item, betaskdbEntities context)
        {
            try
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Opening(EDMX.item item, betaskdbEntities context)
        {
            try
            {
                if (GetTransactionCount(item.item_id, context) == 0)
                {
                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = item.cost,
                        qty_added = item.opening_stock,
                        qty_reduced = 0,
                        transaction_date = DateTime.Now,
                        closing_stock = item.opening_stock,
                        closing_value = item.cost * item.opening_stock,
                        narration = "Opening Stock",
                        transaction_type = EnumTransactionTypes.OPENING.ToString(),
                        transaction_type_id = 0,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    item.Stock = item.Stock + item.opening_stock;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_BulkOpening(List<EDMX.item_transaction> listItem, bool updateStock = false)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    foreach (EDMX.item_transaction item in listItem)
                    {
                        try
                        {
                            var _days = (listItem[0].transaction_date - DateTime.Now).Days;
                            int allowedDate = context.system_settings.AsNoTracking().FirstOrDefault(x => x.status == 1).allowed_backdate * -1;
                            if (_days < allowedDate)
                            {
                                PrivilegeDAL privileges = new PrivilegeDAL();
                                if (!privileges.IsPriviligeProvided(Constants.UserId, PrivilegeDAL.Privileges.AllowBackDate, context))
                                    throw new Exception("Backdate entry update is not allowed");
                            }

                            var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                            if (xItem != null)
                            {
                                var xItemTran = context.item_transaction.Where(x => x.item_id == item.item_id && x.transaction_type_id == item.transaction_type_id && x.transaction_type == item.transaction_type && x.transaction_date == item.transaction_date && x.status == 1).OrderByDescending(o => o.item_transaction_id).FirstOrDefault();
                                if (xItemTran != null)
                                {
                                    xItemTran.status = 2;
                                    xItemTran.narration = "Deleted";
                                    context.Entry(xItemTran).State = EntityState.Modified;
                                    context.SaveChanges();
                                }

                                {

                                    EDMX.item_transaction itemTran = new item_transaction()
                                    {
                                        item_id = item.item_id,
                                        item_cost = item.item_cost,
                                        qty_added = item.qty_added,
                                        qty_reduced = 0,
                                        transaction_date = item.transaction_date,
                                        closing_stock = item.closing_stock,
                                        closing_value = item.closing_value,
                                        narration = item.narration,
                                        transaction_type = EnumTransactionTypes.OPENING.ToString(),
                                        transaction_type_id = item.transaction_type_id,
                                        status = 1

                                    };
                                    context.Entry(itemTran).State = EntityState.Added;
                                    context.SaveChanges();

                                    if (item.item != null && updateStock)
                                    {
                                        xItem.Stock = item.item.Stock;
                                        xItem.godown_stock = item.item.godown_stock;
                                        xItem.cost = item.item.cost;
                                        context.Entry(xItem).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }



                                }
                            }
                        }

                        catch { }
                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Purchase(List<EDMX.purchase_item> listItem, betaskdbEntities context,bool isEdit)
        {
            try
            {
                context.Database.CommandTimeout = 1500;
                int _custId = listItem[0].purchase.vendor_id;
                foreach (EDMX.purchase_item item in listItem)
                {
                    var xItem = context.item.Find(item.item_id);
                    if (!isEdit)
                    {

                        EDMX.item_transaction itemTran = new item_transaction()
                        {
                            item_id = item.item_id,
                            item_cost = xItem.cost,
                            qty_added = item.qty,
                            qty_reduced = 0,
                            transaction_date = item.purchase.purchase_date,
                            closing_stock = xItem.Stock + item.qty,
                            closing_value = xItem.cost * (xItem.Stock + item.qty),
                            narration = "Purchase",
                            transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                            transaction_type_id = item.purchase_id,
                            status = 1

                        };
                        context.Entry(itemTran).State = EntityState.Added;
                        xItem.Stock = item.item.Stock + item.qty;
                        xItem.godown_stock = xItem.godown_stock + item.qty;
                        context.Entry(xItem).State = EntityState.Modified;
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_PurchaseReturn(List<EDMX.purchase_return_item> listItem, betaskdbEntities context)
        {
            try
            {

                foreach (EDMX.purchase_return_item item in listItem)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();
                    string supplier = context.customer.Where(c => c.customer_id == item.purchase_return.vendor_id).FirstOrDefault().customer_name;

                    var xItemTran = context.item_transaction.Where(x => x.item_id == item.item_id && x.transaction_type_id == item.purchase_return_id).OrderByDescending(o => o.item_transaction_id).FirstOrDefault();
                    if (xItemTran == null)
                    {
                        EDMX.item_transaction itemTran = new item_transaction()
                        {
                            item_id = item.item_id,
                            item_cost = xItem.cost,
                            qty_added = 0,
                            qty_reduced = item.qty,
                            transaction_date = item.purchase_return.purchase_date,
                            closing_stock = xItem.Stock - item.qty,
                            closing_value = xItem.cost * (xItem.Stock - item.qty),
                            narration = $"Purchase return to {supplier}",
                            transaction_type = EnumTransactionTypes.PRETURN.ToString(),
                            transaction_type_id = item.purchase_return_id,
                            status = 1

                        };
                        context.Entry(itemTran).State = EntityState.Added;
                        context.SaveChanges();


                        xItem.Stock = item.item.Stock - item.qty;
                        context.Entry(xItem).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Sales(List<EDMX.sales_item> listItem, betaskdbEntities context)
        {
            try
            {
                int _custId = listItem[0].sales.customer_id;
                string supplier = context.customer.Where(c => c.customer_id == _custId).FirstOrDefault().customer_name;
                foreach (EDMX.sales_item item in listItem)
                {
                    h:
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    item_transaction xItemTran = context.item_transaction.Where(x => x.item_id == item.item_id && x.transaction_type_id == item.sales_id && x.transaction_type=="SALE").OrderByDescending(o => o.item_transaction_id).FirstOrDefault();
                    if (xItemTran == null)
                    {
                        EDMX.item_transaction itemTran = new item_transaction()
                        {
                            item_id = item.item_id,
                            item_cost = xItem.cost,
                            qty_added = 0,
                            qty_reduced = item.qty,
                            transaction_date = item.sales.sales_date,
                            closing_stock = xItem.Stock - item.qty,
                            closing_value = xItem.cost * (xItem.Stock - item.qty),
                            narration = $"Sale to {supplier}",
                            transaction_type = EnumTransactionTypes.SALE.ToString(),
                            transaction_type_id = item.sales_id,
                            status = 1

                        };
                        context.Entry(itemTran).State = EntityState.Added;
                        context.SaveChanges();


                        xItem.Stock = item.item.Stock - item.qty;
                        context.Entry(xItem).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.item_transaction.Remove(xItemTran);
                        context.SaveChanges();

                        xItem.Stock = item.item.Stock + xItemTran.qty_reduced;
                        context.Entry(xItem).State = EntityState.Modified;
                        context.SaveChanges();
                        goto h;
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_SalesReturn(List<EDMX.sales_return_item> listItem, betaskdbEntities context)
        {
            try
            {

                foreach (EDMX.sales_return_item item in listItem)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();
                    string supplier = context.customer.Where(c => c.customer_id == item.sales_return.customer_id).FirstOrDefault().customer_name;

                    var xItemTran = context.item_transaction.Where(x => x.item_id == item.item_id && x.transaction_type_id == item.sales_return_id).OrderByDescending(o => o.item_transaction_id).FirstOrDefault();
                    if (xItemTran == null)
                    {
                        EDMX.item_transaction itemTran = new item_transaction()
                        {
                            item_id = item.item_id,
                            item_cost = xItem.cost,
                            qty_added = item.qty,
                            qty_reduced = 0,
                            transaction_date = item.sales_return.sales_date,
                            closing_stock = xItem.Stock + item.qty,
                            closing_value = xItem.cost * (xItem.Stock + item.qty),
                            narration = $"Sale return from {supplier}",
                            transaction_type = EnumTransactionTypes.SRETURN.ToString(),
                            transaction_type_id = item.sales_return_id,
                            status = 1

                        };
                        context.Entry(itemTran).State = EntityState.Added;
                        context.SaveChanges();


                        xItem.Stock = item.item.Stock + item.qty;
                        context.Entry(xItem).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Production(EDMX.production item, betaskdbEntities context)
        {
            try
            {
                //  if (GetTransactionCount(item.item_id, context) == 0)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = xItem.cost,
                        qty_added = item.qty,
                        qty_reduced = 0,
                        transaction_date = item.production_date,
                        closing_stock = xItem.Stock + item.qty,
                        closing_value = xItem.cost * (xItem.Stock + item.qty),
                        narration = "Production Stock",
                        transaction_type = EnumTransactionTypes.PRODUCTION.ToString(),
                        transaction_type_id = item.production_id,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock + item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Production_Delete(EDMX.production item, betaskdbEntities context)
        {
            try
            {
                //  if (GetTransactionCount(item.item_id, context) == 0)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = xItem.cost,
                        qty_added = 0,
                        qty_reduced = item.qty,
                        transaction_date = item.production_date,
                        closing_stock = xItem.Stock - item.qty,
                        closing_value = xItem.cost * (xItem.Stock - item.qty),
                        narration = "Production Stock Delete",
                        transaction_type = EnumTransactionTypes.PRODUCTION.ToString(),
                        transaction_type_id = item.production_id,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock - item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_ProductionRawmaterial(List<EDMX.production_rawmaterial> listItem, betaskdbEntities context)
        {
            try
            {
                foreach (EDMX.production_rawmaterial item in listItem)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    //decimal cost = Math.Round((item.item_value / item.qty), 2);
                    decimal cost = xItem.cost;

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = cost,
                        qty_added = 0,
                        qty_reduced = item.qty,
                        transaction_date = item.production.production_date,
                        closing_stock = xItem.Stock - item.qty,
                        closing_value = xItem.cost * (xItem.Stock - item.qty),
                        narration = "Production rowmaterail Stock reduced",
                        transaction_type = EnumTransactionTypes.PRODUCTION.ToString(),
                        transaction_type_id = item.production_id,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock - item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public void SaveItemTransaction_ProductionRawmaterial_Delete(List<EDMX.production_rawmaterial> listItem, betaskdbEntities context)
        {
            try
            {
                foreach (EDMX.production_rawmaterial item in listItem)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = xItem.cost,
                        qty_added = item.qty,
                        qty_reduced = 0,
                        transaction_date = item.production.production_date,
                        closing_stock = xItem.Stock + item.qty,
                        closing_value = xItem.cost * (xItem.Stock + item.qty),
                        narration = "Production rowmaterail Stock reduced reverse",
                        transaction_type = EnumTransactionTypes.PRODUCTION.ToString(),
                        transaction_type_id = item.production_id,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock + item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Damage(EDMX.item_damage item, betaskdbEntities context)
        {
            try
            {
                //  if (GetTransactionCount(item.item_id, context) == 0)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = xItem.cost,
                        qty_added = 0,
                        qty_reduced = item.qty,
                        transaction_date = item.damage_date,
                        closing_stock = xItem.Stock - item.qty,
                        closing_value = xItem.cost * (xItem.Stock - item.qty),
                        narration = "Damage Entry ",
                        transaction_type = EnumTransactionTypes.DAMAGE.ToString(),
                        transaction_type_id = 0,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock - item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_DamageReturn(EDMX.item_damage item, betaskdbEntities context)
        {
            try
            {
                //  if (GetTransactionCount(item.item_id, context) == 0)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id,
                        item_cost = xItem.cost,
                        qty_added = item.qty,
                        qty_reduced = 0,
                        transaction_date = DateTime.Now,
                        closing_stock = xItem.Stock + item.qty,
                        closing_value = xItem.cost * (xItem.Stock + item.qty),
                        narration = "Damage Entry return ",
                        transaction_type = EnumTransactionTypes.DAMAGE.ToString(),
                        transaction_type_id = 0,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock + item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Damage_ScrapUpdate(EDMX.item_damage item, betaskdbEntities context)
        {
            try
            {
                //  if (GetTransactionCount(item.item_id, context) == 0)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id_damaged).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id_damaged,
                        item_cost = xItem.cost,
                        qty_added = item.qty,
                        qty_reduced = 0,
                        transaction_date = item.damage_date,
                        closing_stock = xItem.Stock + item.qty,
                        closing_value = xItem.cost * (xItem.Stock + item.qty),
                        narration = "Damage Entry Scrap added",
                        transaction_type = EnumTransactionTypes.DAMAGE.ToString(),
                        transaction_type_id = 0,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock + item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Damage_ScrapUpdateReturn(EDMX.item_damage item, betaskdbEntities context)
        {
            try
            {
                //  if (GetTransactionCount(item.item_id, context) == 0)
                {
                    var xItem = context.item.Where(i => i.item_id == item.item_id_damaged).FirstOrDefault();

                    EDMX.item_transaction itemTran = new item_transaction()
                    {
                        item_id = item.item_id_damaged,
                        item_cost = xItem.cost,
                        qty_added = 0,
                        qty_reduced = item.qty,
                        transaction_date = DateTime.Now,
                        closing_stock = xItem.Stock - item.qty,
                        closing_value = xItem.cost * (xItem.Stock - item.qty),
                        narration = "Damage Entry Scrap added return",
                        transaction_type = EnumTransactionTypes.DAMAGE.ToString(),
                        transaction_type_id = 0,
                        status = 1

                    };
                    context.Entry(itemTran).State = EntityState.Added;
                    context.SaveChanges();

                    xItem.Stock = xItem.Stock - item.qty;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_DeliveryReturn(EDMX.delivery_return item, betaskdbEntities context)
        {
            try
            {

                //Checking Exception
                int exceptionItemId = 0;
                if (context.item_return_exception.Any(x => x.item_id == item.item_id))
                {
                    var itException = context.item_return_exception.AsNoTracking().Where(x => x.item_id == item.item_id && x.status == 1).FirstOrDefault();
                    if (itException != null)
                        exceptionItemId = itException.return_item_id;
                }
                var xItem = context.item.Where(i => i.item_id == (exceptionItemId == 0 ? item.item_id : exceptionItemId)).FirstOrDefault();
                decimal cost = xItem.cost;
                if (exceptionItemId > 0)
                {
                    cost = 0;
                   //production_mapping_rowmaterial row= context.production_mapping_rowmaterial.Where(x => x.item_id == exceptionItemId).FirstOrDefault();
                   // if (row != null)
                   //     cost = Math.Round((row.item_value / row.qty), 2);
                }


                EDMX.item_transaction itemTran = new item_transaction()
                {
                    item_id = xItem.item_id,
                    //item_cost = cost,
                    item_cost = cost,
                    qty_added = item.qty,
                    qty_reduced = 0,
                    transaction_date = item.return_date,
                    closing_stock = xItem.Stock + item.qty,
                    closing_value = xItem.cost * (xItem.Stock + item.qty),
                    narration = "Delivery Return Item",
                    transaction_type = EnumTransactionTypes.DRETURN.ToString(),
                    transaction_type_id = item.delivery_return_id,
                    status = 1

                };
                context.Entry(itemTran).State = EntityState.Added;
                context.SaveChanges();

                xItem.Stock = xItem.Stock + item.qty;
                context.Entry(xItem).State = EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void SaveItemTransaction_Transfer(transfer_item transfer, betaskdbEntities context)
        {
            try
            {
                var xItem = context.item.Where(i => i.item_id == transfer.item_id).FirstOrDefault();
                if (xItem != null)
                {


                    if (transfer.transfer_type == "IN")
                        xItem.godown_stock = xItem.godown_stock + transfer.qty;
                    else
                        xItem.godown_stock = xItem.godown_stock - transfer.qty;

                    context.Entry(xItem).State = EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public int GetTransactionCount(int itemId, betaskdbEntities context)
        {
            int tranCount = 0;
            try
            {
                tranCount = context.item_transaction.Where(x => x.item_id == itemId).ToList().Count();
            }
            catch (Exception ee)
            {
                throw;
            }
            return tranCount;
        }
        public List<EDMX.item_transaction> GetItemTransaction(DateTime dateFrom, DateTime dateTo, int itemId, out decimal openingStock)
        {
            openingStock = 0;
            List<EDMX.item_transaction> listItemTransaction = new List<item_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listItemTransaction = context.item_transaction.Include(i => i.item).Include(u => u.item.uom_setting).Where(x => x.item_id == itemId && x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).OrderBy(x =>x.transaction_date ).ThenBy(x=> x.item_transaction_id).ToList();
                    if (listItemTransaction != null && listItemTransaction.Count > 0)
                    {
                        long minId = listItemTransaction.Min(x => x.item_transaction_id);
                        var xOpen = context.item_transaction.Where(x => x.item_id == itemId && x.item_transaction_id < minId).OrderByDescending(x => x.item_transaction_id).FirstOrDefault();
                        if (xOpen != null)
                            openingStock = xOpen.closing_stock;
                    }
                }
            }
            catch
            {
                throw;
            }
            return listItemTransaction;
        }
        //-----------------prakash tmr added-------------------------------------------------------------------
        public List<ItemStockReportModel> GetItemStockReportData(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            decimal OpeningStock = 0;
            decimal ClosingStock = 0;
            decimal transferIn = 0;
            decimal transferOut = 0;
            
            List<EDMX.item_transaction> listItemTransaction = new List<item_transaction>();
            EDMX.production production = new production();
            List<EDMX.production> listProduction = new List<production>();
            List<EDMX.item> listitem = new List<item>();
            List<ItemStockReportModel> listItemStockReportModel = new List<ItemStockReportModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    //opening and closing balance not correct due and previous date entries
                    var xOpen = context.item_transaction.Where(x => x.item_id == itemId && x.transaction_date < dateFrom).OrderByDescending(x => x.transaction_date).FirstOrDefault();
                    if (xOpen != null)
                        OpeningStock = xOpen.closing_stock;
                    for (DateTime ReportDate = dateFrom; ReportDate.Date <= dateTo.Date; ReportDate = ReportDate.AddDays(1))
                    {
                        // itemId = 24;
                       listitem = context.item.Where(x => (itemId>0? x.item_id== itemId: x.item_id>0) && x.status == 1).ToList(); 
                      
                        foreach (EDMX.item Reportitem in listitem)
                        {
                             transferIn = 0;
                             transferOut = 0;                            
                            listItemTransaction = context.item_transaction.Include(i => i.item).Include(u => u.item.uom_setting).Where(x => x.item_id == Reportitem.item_id && x.status == 1 && x.transaction_date == ReportDate).OrderBy(x => x.item_transaction_id).ToList();                                                    
                        
                            ProductionDAL ProductionDAL = new ProductionDAL();
                            ItemStockReportModel ItemStockReportModel = new ItemStockReportModel();
                            ItemStockReportModel.Transaction_Date = ReportDate;
                            ItemStockReportModel.item_id = Reportitem.item_id;
                            ItemStockReportModel.Item_name = Reportitem.item_name;
                            if (listItemTransaction != null && listItemTransaction.Count > 0)
                            {
                                foreach (EDMX.item_transaction item in listItemTransaction)
                                {
                                    if (ClosingStock != 0)
                                    {   OpeningStock=ClosingStock;                                     
                                        ItemStockReportModel.Closing_Stock = item.closing_stock;
                                        ClosingStock = item.closing_stock;
                                    }
                                    else
                                    { ClosingStock = OpeningStock; }
                                    if (item.transaction_type == "PRODUCTION")
                                    {
                                        ItemStockReportModel.ProductionIn += item.qty_added;
                                        ItemStockReportModel.ProductionOut += item.qty_reduced;

                                    }
                                    else if (item.transaction_type == "DRETURN")
                                    {
                                        ItemStockReportModel.Return += item.qty_added;
                                    }
                                    else if (item.transaction_type == "SRETURN")
                                    {
                                        ItemStockReportModel.Sale += (item.qty_reduced + item.qty_added);
                                    }
                                    else if (item.transaction_type == "PRETURN")
                                    {
                                        ItemStockReportModel.Purchase += (item.qty_added - item.qty_reduced);
                                    }
                                    else
                                    {
                                        ItemStockReportModel.Sale += item.qty_reduced;
                                        ItemStockReportModel.Purchase += item.qty_added;
                                    }
                                    ItemStockReportModel.Closing_Stock = ClosingStock;
                                }                                 
                                                                                                  
                            }
                                List<transfer_item> listTransfer = new List<transfer_item>();
                                DateTime ReportDateTo = ReportDate.AddHours(24);
                                listTransfer = context.transfer_item.Where(x => x.transfer_date <= ReportDateTo && x.transfer_date >= ReportDate && x.status == 1 && x.item_id == Reportitem.item_id).ToList();
                                if (listTransfer.Count > 0)
                                {
                                foreach (transfer_item transfer_item in listTransfer)
                                {
                                    if (transfer_item.transfer_type== "IN")
                                    { transferIn += transferIn + transfer_item.qty; }
                                    else
                                    { transferOut += transferOut + transfer_item.qty;}
                                }
                                }


                                if (ItemStockReportModel.Closing_Stock == 0) { ItemStockReportModel.Closing_Stock = OpeningStock; }

                                ItemStockReportModel.TransferIn = transferIn;
                                ItemStockReportModel.TransferOut = transferOut;
                                ItemStockReportModel.Opening_Stock = OpeningStock;
                                listItemStockReportModel.Add(ItemStockReportModel);                           
                            
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return listItemStockReportModel;
        }
        public List<EDMX.item_transaction> GetOpeningStockReport(DateTime dateFrom, DateTime dateTo)
        {
            List<EDMX.item_transaction> listItemTransaction = new List<item_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listItemTransaction = context.item_transaction.Include(i => i.item).AsNoTracking().Where(x => x.transaction_type == "OPENING" && x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo ).OrderBy(x => x.item_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listItemTransaction;
        }
        //------------------------------------------------------------------------------------------------------
        public void RemoveItemOpening(int itemTransactionId)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    item_transaction it = context.item_transaction.Where(x=>x.item_transaction_id==itemTransactionId).FirstOrDefault();
                    if (it != null)
                    {
                        it.status = 2;
                        context.Entry(it).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetClosingValueDetailed(string date)
        {
            DataTable tblData = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string sql = "SELECT a.item_id as ItemId,c.item_name as ItemName,max(item_cost) as Cost , max(closing_stock) as ClosingStock,max(closing_value) as closingValue " +
                        " FROM [dbo].[item_transaction] a" +
                        " inner join " +
                        " (select item_id,max([item_transaction_id]) as lastid from [item_transaction] where [transaction_date]<'"+date+"' and status=1 group by item_id) b" +
                        " on a.item_transaction_id=b.lastid " +
                        " inner join item c on c.item_id=a.item_id and c.stockable=1 " +
                        " group by a.item_id,c.item_name ";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                           
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tblData;
        }


    }
}
