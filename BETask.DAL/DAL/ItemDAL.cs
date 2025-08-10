using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;

namespace BETask.DAL.DAL
{
   public class ItemDAL
    {
        public int SaveItem(item item)
        {
            int itemId = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            item.sale_ledger = item.sale_ledger == 0 ? null : item.sale_ledger;
                            item.purchase_ledger = item.purchase_ledger == 0 ? null : item.purchase_ledger;
                            context.Entry(item).State = item.item_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();
                            itemId = item.item_id;

                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_Opening(item, context);
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return itemId;
        }


        public item GetItemDteials(int item_id)
        {
            item _tem = new item();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _tem = context.item.Include(x => x.uom_setting).Include(x => x.tax_setting).Include(x=>x.account_ledger).Include(x=>x.account_ledger1).Where(x => x.status == 1 && x.item_id == item_id).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return _tem;
        }
        public int NextBarcode()
        {
            int barcode = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    barcode = context.item.Max(x => x.item_id) + 1;
                }
            }
            catch (Exception ee)
            {
                barcode = 1;
            }
            return barcode;
        }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public List<item> GetAllItem(int item_id,bool sortStock=false)
        {
            List<item> listItem = new List<item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listItem = context.item.Include(x => x.uom_setting).Include(x=>x.tax_setting).Where(x => x.status == 1 && (item_id > 0 ? (x.item_id == item_id) : x.item_id >= 0)).OrderBy(x => x.item_name).ToList();
                    if (sortStock)
                        listItem = listItem.OrderByDescending(x => x.Stock).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listItem;
        }
        public List<item> GetAllItem_Rawmaterials(bool sortStock = false)
        {
            List<item> listItem = new List<item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listItem = context.item.Include(x => x.uom_setting).Where(x => x.status == 1 && x.rawmeterial==1).OrderBy(x => x.item_name).ToList();
                    if (sortStock)
                        listItem = listItem.OrderByDescending(x => x.Stock).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listItem;
        }
        public List<item> GetAllItem_Sellable(bool sortStock = false)
        {
            List<item> listItem = new List<item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listItem = context.item.Include(x => x.uom_setting).Include(x => x.tax_setting).Where(x => x.status == 1 && x.sellable == 1).OrderBy(x => x.item_name).ToList();
                    if (sortStock)
                        listItem = listItem.OrderByDescending(x => x.Stock).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listItem;
        }
       
        public List<Model.CustomerAgreement_withClosingStockModel> GetCustomerRouteItems(int customerId, int routeId, int itemId)
        {
            List<customer_aggrement> listItems = new List<customer_aggrement>();
            List<Model.CustomerAgreement_withClosingStockModel> listItemsModel = new List<Model.CustomerAgreement_withClosingStockModel>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (customerId != 0)
                        listItems = context.customer_aggrement.Include(i => i.item).Include(c => c.customer).Include(u => u.item.uom_setting).Where(x => x.customer_id == customerId).ToList();
                    else if (routeId != 0)
                    {
                        listItems = context.customer_aggrement.Include(i => i.item).Include(c => c.customer).Include(u => u.item.uom_setting).Where(x => x.customer.route_id == routeId && x.customer.status==1).ToList();
                        if (itemId != 0)
                        {
                            listItems = context.customer_aggrement.Include(i => i.item).Include(c => c.customer).Include(u => u.item.uom_setting).Where(x => x.customer.route_id == routeId && x.item.item_id == itemId && x.customer.status == 1).ToList();
                        }
                    }
                    else
                    {
                        listItems = context.customer_aggrement.Include(i => i.item).Include(c => c.customer).Include(r=>r.customer.route).Include(u => u.item.uom_setting).Where(x=> x.customer.status == 1).ToList();
                        if (itemId != 0)
                        {
                            listItems = context.customer_aggrement.Include(i => i.item).Include(c => c.customer).Include(u => u.item.uom_setting).Where(x =>  x.item.item_id == itemId && x.customer.status == 1).ToList();
                        }
                    }
                    CustomerDAL customerDAL = new CustomerDAL();
                    foreach (EDMX.customer_aggrement item in listItems)
                    {
                      
                        listItemsModel.Add(new Model.CustomerAgreement_withClosingStockModel()
                        {
                            ItemId=item.item_id,
                            ItemName=item.item.item_name,
                            UnitPrice=item.unit_price,
                            CustomerId=item.customer_id,
                            MaxQty=item.max_qty,
                            Packing=item.item.uom_setting.uom_name,
                            CustomerName=item.customer.customer_name,
                            RouteName=item.customer.route.route_name,
                            ClosingStock = customerDAL.CustomerStockBalanceInPerformance(item.customer_id, item.item_id, context).Closing
                    });

                    }
                    listItems = listItems.OrderBy(x=>x.customer.route_id).ThenBy(x=>x.customer.customer_name).ThenBy(x=>x.item_id).ToList();
                }
            }
            catch { throw; }
            return listItemsModel;
        }

        public async Task CalculateItemCostAsync(List<EDMX.purchase_item> items)
        {
            decimal cost = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    foreach (var item in items)
                    {
                        var xItem =  context.item.Find(item.item_id);
                        if (xItem != null)
                        {
                            decimal stock = xItem.Stock;
                            decimal qn_x_rn = 0; // Quatnity x rate of n purchases
                            decimal qn = 0; // n Number of Quantity

                            int _purchaseId = 0;
                            while (stock > 0)
                            {
                                EDMX.purchase_item _purchase = null;
                                if (_purchaseId == 0)
                                {
                                    _purchase = await context.purchase_item.AsNoTracking().Where(p => p.item_id == item.item_id)
                                        .OrderByDescending(x => x.purchase_id).FirstOrDefaultAsync();
                                }
                                else if (_purchaseId > 0)
                                {
                                    _purchase = await context.purchase_item.AsNoTracking()
                                        .Where(p => p.item_id == item.item_id && p.purchase_id < _purchaseId)
                                        .OrderByDescending(x => x.purchase_id).FirstOrDefaultAsync();
                                }

                                if (_purchase != null)
                                {
                                    qn_x_rn += _purchase.qty * _purchase.rate;
                                    if (stock >= _purchase.qty)
                                        qn += _purchase.qty;
                                    else
                                    {
                                        decimal _getStock = _purchase.qty - stock;
                                        qn += _purchase.qty - _getStock;
                                    }
                                    stock -= _purchase.qty;
                                    _purchaseId = _purchase.purchase_id;
                                    cost = qn_x_rn / qn;
                                }
                                else
                                    break;
                            }

                            if (cost > 0)
                            {
                                // Updating cost in item
                                xItem.cost = Math.Round(cost, 2);
                                context.Entry(xItem).State = EntityState.Modified;
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
     
        public List<item> GetDistinctDeliveryItemListByDate(DateTime dateFrom,DateTime dateTo)
        {
            List<item> listItem = new List<item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    

                    //string sql = query.ToQueryString();
                    List <delivery_items> delItem = context.delivery_items.Include(i => i.item).Where(d => d.delivery_time >= dateFrom && d.delivery_time <= dateTo).ToList();
                    listItem = delItem.Select(x=>x.item).Distinct().OrderBy(x=>x.item_id).ToList();
                    

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listItem;
        }

        public List<item> GetDistinctDeliveryItemListByDate(int deliveryId)
        {
            List<item> listItem = new List<item>();
            try
            {
                using (var context = new betaskdbEntities())
                {


                    //string sql = query.ToQueryString();
                    List<delivery_items> delItem = context.delivery_items.Include(i => i.item).Where(d => d.delivery_id==deliveryId).ToList();
                    listItem = delItem.Select(x => x.item).Distinct().OrderBy(x => x.item_id).ToList();


                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listItem;
        }

    }
}
