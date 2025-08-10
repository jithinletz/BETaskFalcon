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
    public class PurchaseOrderDAL
    {
        public int SavePurchaseOrder(EDMX.purchase_order _purchase, List<EDMX.purchase_order_item> items)
        {

            int _purchaseId = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (_purchase.purchase_id == 0)
                            {
                                _purchase.purchase_order_item = items;
                                context.Entry(_purchase).State = _purchase.purchase_id == 0 ? EntityState.Added : EntityState.Modified;
                                context.SaveChanges();
                                _purchaseId = _purchase.purchase_id;


                            }
                            else
                            {
                                _purchaseId = _purchase.purchase_id;
                                context.Entry(_purchase).State = _purchase.purchase_id == 0 ? EntityState.Added : EntityState.Modified;
                                context.SaveChanges();

                                // Remove all the purchase items
                                List<EDMX.purchase_order_item> pitems = context.purchase_order_item.Where(p => p.purchase_id == _purchase.purchase_id).ToList();
                                if (pitems != null && pitems.Count > 0)
                                {
                                    context.purchase_order_item.RemoveRange(pitems);
                                    context.SaveChanges();
                                }
                                foreach (EDMX.purchase_order_item item in items)
                                {
                                    context.Entry(item).State = EntityState.Added;
                                }
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
            return _purchaseId;
        }
        public EDMX.purchase_order GetPurchaseDetails(int purchaseId)
        {
            EDMX.purchase_order purchase = new EDMX.purchase_order();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    purchase = context.purchase_order.Include(p => p.purchase_order_item.Select(i => i.item).Select(t => t.tax_setting))
                        .Include(p => p.purchase_order_item.Select(i => i.item).Select(u => u.uom_setting)).Include(p => p.customer)
                        .Where(p => p.purchase_id == purchaseId)
                        .FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return purchase;

        }
        public List<purchase_order> SearchPurchase(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            List<purchase_order> listPurchase = new List<purchase_order>();
            //dateTo = dateTo.AddHours(23).AddMinutes(59).AddSeconds(59);
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchase = context.purchase_order.Include(x => x.customer).Where(x => x.invoice_date >= dateFrom && x.invoice_date <= dateTo && x.status == 1).ToList();
                    if (vendorId > 0)
                    {
                        listPurchase = listPurchase.Where(x => x.vendor_id == vendorId).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listPurchase;
        }
        public List<purchase_order_item> SearchPurchaseItem(int purchaseId)
        {
            List<purchase_order_item> listPurchaseItem = new List<purchase_order_item>();
            //dateTo = dateTo.AddHours(23).AddMinutes(59).AddSeconds(59);
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchaseItem = context.purchase_order_item.Include(x => x.item).Include(u => u.item.uom_setting).Include(t => t.item.tax_setting).Where(x => x.purchase_id == purchaseId).ToList();

                }
            }
            catch
            {
                throw;
            }
            return listPurchaseItem;
        }
        public void UpdateImportedItems(int[] orderitemsId, int purchaseItemId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    for (int i = 0; i < orderitemsId.Length; i++)
                    {
                        int _pid = orderitemsId[i];
                        var orderItems = context.purchase_order_item.Where(x => x.purchase_item_id == _pid).FirstOrDefault();
                        orderItems.original_prurchase_id = purchaseItemId;
                        context.Entry(orderItems).State= EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public List<purchase_order> SupplierPurchaseReport(DateTime dateFrom, DateTime dateTo, int vendorId, string paymentmode)
        {
            List<purchase_order> listPurchase = new List<purchase_order>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchase = context.purchase_order.Include(x => x.customer).Where(x => x.invoice_date >= dateFrom && x.invoice_date <= dateTo && x.status == 1).ToList();
                    if (vendorId > 0)
                    {
                        listPurchase = listPurchase.Where(x => x.vendor_id == vendorId).ToList();
                    }
                    if (paymentmode != "all")
                    {
                        listPurchase = listPurchase.Where(x => x.payment_mode.ToLower() == paymentmode).OrderBy(x => x.purchase_date).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listPurchase;
        }

        public List<purchase_order_item> ItemPurchaseReport(DateTime dateFrom, DateTime dateTo, int vendorId, int itemId)
        {
            List<purchase_order_item> listPurchaseItem = new List<purchase_order_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    // listSale = context.sales.Include(x => x.customer).Include(x=>x.sales_item.Select(i => i.item).Select(u => u.uom_setting)).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1).ToList();
                    listPurchaseItem = context.purchase_order_item.Include(s => s.purchase_order).Include(v=>v.purchase_order.customer).Include(i => i.item).Include(p => p.item.uom_setting).Where(s => s.purchase_order.invoice_date >= dateFrom && s.purchase_order.invoice_date <= dateTo).OrderBy(x => x.purchase_order.invoice_date).ThenBy(x => x.item_id).ToList();
                    if (vendorId > 0)
                    {
                        listPurchaseItem = listPurchaseItem.Where(x => x.purchase.vendor_id == vendorId).ToList();
                    }
                    if (itemId > 0)
                    {
                        listPurchaseItem = listPurchaseItem.Where(x => x.item_id == itemId).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listPurchaseItem;
        }

    }
}
