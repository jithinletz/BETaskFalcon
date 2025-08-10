using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;

namespace BETask.DAL.DAL
{
    public class LoadDAL
    {
        public void SaveLoad(EDMX.loading loading)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(loading).State = loading.load_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();

                            DeliveryDAL deliveryDAL = new DeliveryDAL();
                            decimal soldQty = deliveryDAL.GetSoldQuantity(loading.delivery_id, loading.item_id);
                            List<EDMX.delivery_item_summary> listSummary = null;

                            if (loading.offload != 0 && loading.new_load == 0)
                            {
                                loading.new_stock = loading.offload * -1;
                                loading.new_load = loading.offload * -1;
                            }

                            //if(context.loading.AsNoTracking().Any(x=>x.))

                            //If delivery item summary has no rows
                            if (!context.delivery_item_summary.Any(x => x.delivery_id == loading.delivery_id && x.item_id == loading.item_id && x.qty > 0 && x.status == 1))
                            {
                                listSummary = new List<delivery_item_summary> { new delivery_item_summary
                            {
                                delivery_id=loading.delivery_id,
                                item_id=loading.item_id,
                                damage_qty=loading.damage,
                                qty=loading.new_load,
                               // qty=loading.new_stock,
                                used_qty=soldQty,
                                status=1,
                                balance_qty=loading.new_stock
                            } };
                            }

                            //If delivery item summary has  rows , will be added with existing qtys
                            else
                            {
                                listSummary = new List<delivery_item_summary> { new delivery_item_summary
                            {
                                delivery_id=loading.delivery_id,
                                item_id=loading.item_id,
                                damage_qty=loading.damage,
                                    qty=loading.new_load,
                                   // qty=loading.new_stock,
                                    status =1,
                                used_qty=soldQty,
                                balance_qty=loading.new_load
                            } };
                            }

                            if (!context.delivery_item_summary.Any(x => x.delivery_id == loading.delivery_id && x.item_id == loading.item_id))
                            {
                                listSummary = new List<delivery_item_summary> { new delivery_item_summary
                            {
                                delivery_id=loading.delivery_id,
                                item_id=loading.item_id,
                                damage_qty=loading.damage,
                              //  qty=loading.new_load,
                                qty=loading.new_stock,
                                used_qty=soldQty,
                                status=1,
                                balance_qty=loading.new_stock
                            } };
                            }

                            deliveryDAL.SaveDeliveryItemSummary(listSummary, context, loading.load_id);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List <EDMX.loading>GetAllLoad(int item,int employee, DateTime fromDate, DateTime toDate)
        {
            List<EDMX.loading> listLoading = new List<loading>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listLoading = context.loading.Include(x => x.employee).Include(x => x.item).Where(x => x.status == 1 && (item > 0 ? x.item_id == item : x.item_id > 0) && x.status == 1 && (employee > 0 ? x.employee_id == employee : x.employee_id > 0) &&  x.load_date >= fromDate && x.load_date <= toDate).OrderBy(x => x.load_date).ToList();

                    DeliveryDAL deliveryDAL = new DeliveryDAL();
                    foreach (loading load in listLoading)
                    {
                        load.remarks = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == load.delivery_id && x.item_id == load.item_id).Select(x => x.delivered_qty).DefaultIfEmpty(0).Sum().ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return listLoading;
        }
        public List<loading> GetLoading(int deliveryId)
        {
            List<loading> listLoading = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listLoading = context.loading.Include(i=>i.item).AsNoTracking().Where(x => x.delivery_id == deliveryId && x.status == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listLoading;
        }
        public decimal GetNewLoad(int deliveryId, int itemId)
        {
            decimal newLoad = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    newLoad = context.loading.AsNoTracking().Where(x => x.delivery_id == deliveryId && x.item_id == itemId && x.status == 1).Select(x => x.new_load).DefaultIfEmpty(0).Sum();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return newLoad;
        }
        public int DeleteLoad(int loadId, int deliveryId, int itemId)
        {
            int resp = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<loading> listLoading = context.loading.Where(x => x.delivery_id == deliveryId && x.item_id == itemId && x.status == 1).ToList();

                    resp = listLoading.Count;
                    foreach (loading load in listLoading)
                    {
                        load.status = 2;
                        context.Entry(load).Property(x => x.status).IsModified = true;

                       
                    }
                    delivery_item_summary summary = context.delivery_item_summary.FirstOrDefault(x => x.delivery_id == deliveryId && x.item_id == itemId && x.status == 1);
                    if (summary != null)
                    {
                        summary.qty = 0;
                        summary.balance_qty = 0;
                        context.Entry(summary).Property(x => x.qty).IsModified = true;
                        context.Entry(summary).Property(x => x.balance_qty).IsModified = true;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;

        }
       
    }
}
