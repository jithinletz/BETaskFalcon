using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace BETask.APP.DAL
{
    public class DeliveryAppDAL
    {
        public int SaveDelivery(EDMX.delivery delivery, List<EDMX.delivery_items> deliveryItems, List<EDMX.delivery_item_summary> delivery_item_summary)
        {
            int _deliveryId = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            delivery.route_id = delivery.route_id == 0 ? null : delivery.route_id;
                            int savedCount = 0;
                            savedCount = context.delivery.Count(x => x.delivery_id == delivery.delivery_id);
                            if (savedCount == 0)
                            {
                                delivery.delivery_items = deliveryItems;
                                delivery.delivery_item_summary = delivery_item_summary;
                                context.Entry(delivery).State = EntityState.Added;
                                context.SaveChanges();
                                _deliveryId = delivery.delivery_id;
                            }
                            else
                            {
                                _deliveryId = delivery.delivery_id;
                                context.Entry(delivery).State = EntityState.Modified;
                                context.SaveChanges();

                                // Remove all the delevery items and save new
                                List<EDMX.delivery_items> _deliveryItems = context.delivery_items.Where(p => p.delivery_id == _deliveryId && p.delivery_time == null).ToList();
                                if (_deliveryItems != null && _deliveryItems.Count > 0)
                                {
                                    context.delivery_items.RemoveRange(_deliveryItems);
                                    context.SaveChanges();
                                }
                                context.delivery_items.AddRange(deliveryItems);
                                context.SaveChanges();


                                // Remove all the delevery item summary and save new
                                List<EDMX.delivery_item_summary> _deliveryItemSummary = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId).ToList();
                                if (_deliveryItemSummary != null && _deliveryItemSummary.Count > 0)
                                {
                                    context.delivery_item_summary.RemoveRange(_deliveryItemSummary);
                                    context.SaveChanges();
                                }
                                context.delivery_item_summary.AddRange(delivery_item_summary);
                                context.SaveChanges();

                            }
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
            catch
            {
                throw;
            }
            return _deliveryId;
        }

        public DataTable GetAppDeliveryReport(int deliveryId)
        {
            DataSet ds = new DataSet();
            try
            {

                using (var context = new betaskdbEntitiesAPP())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetDeliveryItemsByDeliveryId", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {

                                adr.Fill(ds);

                            }
                        }
                    }

                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return ds.Tables[0];
        }

        public int SaveDeliveryItemSummary( List<EDMX.delivery_item_summary> delivery_item_summary)
        {
            int _saveStatus = 0;
            try
            {
                int _deliveryId = delivery_item_summary.FirstOrDefault().delivery_id;
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            // Remove all the delevery item summary and save new
                            List<EDMX.delivery_item_summary> _deliveryItemSummary = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId).ToList();

                            if (_deliveryItemSummary != null && _deliveryItemSummary.Count > 0)
                            {
                                context.delivery_item_summary.RemoveRange(_deliveryItemSummary);
                                context.SaveChanges();
                            }

                            context.delivery_item_summary.AddRange(delivery_item_summary);
                            context.SaveChanges();

                            _saveStatus = 1;
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            _saveStatus = 0;
                            transaction.Rollback();
                            throw;
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return _saveStatus;
        }
        public int GenerateDeliveryId(EDMX.delivery delivery)
        {
            int _deliveryId = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            delivery.route_id = delivery.route_id == 0 ? null : delivery.route_id;
                            int savedCount = 0;
                            savedCount = context.delivery.Count(x => x.delivery_id == delivery.delivery_id);
                            if (savedCount == 0)
                            {
                               
                                context.Entry(delivery).State = EntityState.Added;
                                context.SaveChanges();
                                _deliveryId = delivery.delivery_id;
                            }
                        
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
            catch
            {
                throw;
            }
            return _deliveryId;
        }
        public List<delivery_items> GetDeliveredItems(long[] deliveredItemIds, out List<daily_collection> listDailyCollection, int deposit = 2)
        {
            
            List<delivery_items> listDeliveredItems = new List<delivery_items>();
            listDailyCollection = null;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    var appItems = from item in context.delivery_items
                                   where deliveredItemIds.Contains(item.delivery_item_id_local) && item.status == 4 && item.is_deposit== deposit
                                   select item;

                    foreach (var prop in appItems)
                    {
                        listDeliveredItems.Add(new delivery_items
                        {
                            customer_id = prop.customer_id,
                            delivered_qty = prop.delivered_qty,
                            delivery_item_id = prop.delivery_item_id,
                            delivery_id = prop.delivery_id,
                            delivery_time = prop.delivery_time,
                            status = prop.status,
                            delivery_item_id_local=prop.delivery_item_id_local,
                            daily_collection_id=prop.daily_collection_id,
                            delivery_leaf=prop.delivery_leaf,
                            division_id=prop.division_id,
                            is_deposit=prop.is_deposit,
                            payment_mode=prop.payment_mode

                        });
                    }

                    //Get Daily Collectiion


                    int[] deliveryIds = listDeliveredItems.Select(x => x.delivery_id).Distinct().ToArray();
                    var dailyCollection = context.daily_collection.Where(item => deliveryIds.Contains((int)item.delivery_id)
                                           && item.status == 1 && item.is_deposit== deposit);

                    var custId = listDeliveredItems.Select(x => x.customer_id).Distinct();// Dstinct customer id to avoid previously updated collections

                    //var dailyCollection = from coll in context.daily_collection
                    //                      where deliveryIds.Contains(coll.delivery_id) && coll.status==1
                    //                      select coll;
                    listDailyCollection = new List<daily_collection>();
                    foreach (var prop in dailyCollection)
                    {
                        foreach (int _cId in custId)
                        {
                            if (_cId == prop.customer_id)
                            {
                                listDailyCollection.Add(new daily_collection
                                {
                                    collected_amount = prop.collected_amount,
                                    customer_id = prop.customer_id,
                                    daily_collection_id = prop.daily_collection_id,
                                    delivery_id = prop.delivery_id,
                                    delivery_time = prop.delivery_time,
                                    employee_id = prop.employee_id,
                                    net_amount = prop.net_amount,
                                    payment_mode = prop.payment_mode,
                                    remarks = prop.remarks,
                                    status = prop.status,
                                    delivery_leaf=prop.delivery_leaf,
                                    division_id=prop.division_id,
                                    is_deposit=prop.is_deposit,
                                    is_refund=prop.is_refund,
                                    old_leaf_count=prop.old_leaf_count,
                                    
                                });
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return listDeliveredItems;
        }

        public int GetDeliveryCount(int deliveryId)
        {
            using (var context = new betaskdbEntitiesAPP())
            {
                return context.delivery_items.Where(x => x.delivery_id == deliveryId && x.status == 4).Count();
            }
        }


        public List<delivery_items> GetDeliveredItemsRecheck(long[] deliveredItemIds, out List<daily_collection> listDailyCollection)
        {

            List<delivery_items> listDeliveredItems = new List<delivery_items>();
            listDailyCollection = null;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    var appItems = from item in context.delivery_items
                                   where deliveredItemIds.Contains(item.delivery_item_id_local) && item.status == 4
                                   select item;

                    foreach (var prop in appItems)
                    {
                        listDeliveredItems.Add(new delivery_items
                        {
                            customer_id = prop.customer_id,
                            delivered_qty = prop.delivered_qty,
                            delivery_item_id = prop.delivery_item_id,
                            delivery_id = prop.delivery_id,
                            delivery_time = prop.delivery_time,
                            status = prop.status,
                            delivery_item_id_local = prop.delivery_item_id_local,
                            daily_collection_id = prop.daily_collection_id
                        });
                    }

                    //Get Daily Collectiion


                    int[] deliveryIds = listDeliveredItems.Select(x => x.delivery_id).Distinct().ToArray();
                    var dailyCollection = context.daily_collection.Where(item => deliveryIds.Contains((int)item.delivery_id)
                                           && item.status ==4);

                    var custId = listDeliveredItems.Select(x => x.customer_id).Distinct();// Dstinct customer id to avoid previously updated collections

                    //var dailyCollection = from coll in context.daily_collection
                    //                      where deliveryIds.Contains(coll.delivery_id) && coll.status==1
                    //                      select coll;
                    listDailyCollection = new List<daily_collection>();
                    foreach (var prop in dailyCollection)
                    {
                        foreach (int _cId in custId)
                        {
                            if (_cId == prop.customer_id)
                            {
                                listDailyCollection.Add(new daily_collection
                                {
                                    collected_amount = prop.collected_amount,
                                    customer_id = prop.customer_id,
                                    daily_collection_id = prop.daily_collection_id,
                                    delivery_id = prop.delivery_id,
                                    delivery_time = prop.delivery_time,
                                    employee_id = prop.employee_id,
                                    net_amount = prop.net_amount,
                                    payment_mode = prop.payment_mode,
                                    remarks = prop.remarks,
                                    status = prop.status
                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listDeliveredItems;
        }

        public void UpdateDailyCollectionStatus(int[] collIds)
        {
            try
            {
              //  int[] collIds = listDailyCollection.Select(x => x.daily_collection_id).ToArray();
                using (var context = new betaskdbEntitiesAPP())
                {
                    var listCollection = context.daily_collection.Where(
            i => collIds.Contains(i.daily_collection_id)
            ).ToList();

                    foreach (daily_collection coll in listCollection)
                    {
                        context.daily_collection.Attach(coll);
                        coll.status = 4;
                        context.Entry(coll).State = EntityState.Modified;
                    }

                    context.SaveChanges();

                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public void UpdateDailyCollectionStatus(int deliveryId, int customerId, int status)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    var dailyColl = context.daily_collection.Single(x => x.delivery_id == deliveryId && x.customer_id == customerId);
                    dailyColl.status = status;
                    context.Entry(dailyColl).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void EnableSpotDelivery(int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    delivery _delivery = context.delivery.Single(x => x.delivery_id == deliveryId);
                    _delivery.status = 3;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DisableSpotDelivery(int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    delivery _delivery = context.delivery.Single(x => x.delivery_id == deliveryId);
                    _delivery.status = 4;
                    _delivery.remarks = $"Disabled at {DateTime.Now}";
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteNotApprovedDeliveryReturn(EDMX.delivery_return deliveryReturn)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    var xDeliveryReturn = context.delivery_return.Single(x => x.customer_id == deliveryReturn.customer_id && x.employee_id==deliveryReturn.employee_id && x.item_id==deliveryReturn.item_id && x.return_date==deliveryReturn.return_date && x.qty==deliveryReturn.qty);
                    if (xDeliveryReturn != null)
                    {
                        xDeliveryReturn.status = 2;
                        context.Entry(xDeliveryReturn).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public void SyncRecheck(int deliveryId, int customerId, long deliveryItemId,int itemId,decimal qty)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    List<delivery_items> listItems = context.delivery_items.Where(x => x.delivery_id == deliveryId && x.customer_id == customerId && x.item_id==itemId && x.qty==qty).ToList();
                    if (listItems != null)
                    {
                        if (listItems.Count > 1)
                        {
                            foreach (delivery_items li in listItems)
                            {
                                if (li.delivery_time != null && li.delivery_item_id_local != deliveryItemId)
                                {
                                    li.delivery_item_id_local = deliveryItemId;
                                    context.Entry(li).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                else if (li.delivery_time == null && li.delivery_item_id_local == deliveryItemId)
                                {
                                    context.delivery_items.Remove(li);
                                    context.SaveChanges();
                                }
                               
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

        public void UpdateScehduledDeliveryQty(int deliveryId, int deliveryItemId, decimal qty, decimal gross, decimal vat, decimal net)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    var xDelivery = context.delivery.Where(x => x.delivery_id == deliveryId).FirstOrDefault();
                    if (xDelivery != null)
                    {
                        decimal xGross = Convert.ToDecimal(xDelivery.gross_amount);
                        decimal xTotalBeforeVat = Convert.ToDecimal(xDelivery.total_beforevat);
                        decimal xNet = Convert.ToDecimal(xDelivery.net_amount);
                        decimal xVat = Convert.ToDecimal(xDelivery.total_vat);

                        delivery_items xItems = context.delivery_items.Where(x => x.delivery_id == deliveryId && x.delivery_item_id_local == deliveryItemId && (x.status == 4 ||x.status==1)).FirstOrDefault();
                        if (xItems != null && xItems.delivery_item_id != 0)
                        {
                            xDelivery.gross_amount = (xGross - xItems.gross_amount) + gross;
                            xDelivery.total_beforevat = (xGross - xItems.gross_amount) + gross;
                            xDelivery.total_vat = (xVat - xItems.vat_amount) + vat;

                            xItems.qty = qty;
                            xItems.gross_amount = gross;
                            xItems.total_beforvat = gross;
                            xItems.vat_amount = vat;
                            xItems.net_amount = net;
                        }
                        context.Entry(xDelivery).State = EntityState.Modified;
                        context.Entry(xItems).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                }
            }
            catch (Exception ee) { throw; }
        }

    }
}
