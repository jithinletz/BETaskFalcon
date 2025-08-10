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
    public class PurchaseReturnDAL
    {
        public int SavePurchaseReturn(EDMX.purchase_return _purchase, List<EDMX.purchase_return_item> items, PurchaseAccountPostModel purchaseAccountPost)
        {
            AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
            int _purchaseId = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (_purchase.purchase_return_id == 0)
                            {
                                _purchase.purchase_return_item = items;
                                context.Entry(_purchase).State = _purchase.purchase_return_id == 0 ? EntityState.Added : EntityState.Modified;
                                context.SaveChanges();
                                _purchaseId = _purchase.purchase_return_id;

                            }
                            else
                            {
                                _purchaseId = _purchase.purchase_return_id;
                                context.Entry(_purchase).State = _purchase.purchase_return_id == 0 ? EntityState.Added : EntityState.Modified;
                                context.SaveChanges();

                                // Remove all the purchase items
                                List<EDMX.purchase_return_item> pitems = context.purchase_return_item.Where(p => p.purchase_return_id == _purchase.purchase_return_id).ToList();
                                if (pitems != null && pitems.Count > 0)
                                {
                                    context.purchase_return_item.RemoveRange(pitems);
                                    context.SaveChanges();
                                }
                                foreach (EDMX.purchase_return_item item in items)
                                {
                                    context.Entry(item).State = EntityState.Added;
                                }
                                context.SaveChanges();

                            }
                            itemTransactionDAL.SaveItemTransaction_PurchaseReturn(items, context);
                            accountTransactionDAL.PostPurchaseReturn(purchaseAccountPost, _purchase, context);
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
        public EDMX.purchase_return GetPurchaseReturnDetails(int purchaseId)
        {
            EDMX.purchase_return purchase = new EDMX.purchase_return();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    purchase = context.purchase_return.Include(p => p.purchase_return_item.Select(i => i.item).Select(t => t.tax_setting))
                        .Include(p => p.purchase_return_item.Select(i => i.item).Select(u => u.uom_setting)).Include(p => p.customer)
                        .Where(p => p.purchase_return_id == purchaseId)
                        .FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return purchase;

        }
        public List<purchase_return> SearchPurchaseReturn(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            List<purchase_return> listPurchase = new List<purchase_return>();
            //dateTo = dateTo.AddHours(23).AddMinutes(59).AddSeconds(59);
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchase = context.purchase_return.Include(x => x.customer).Where(x => x.invoice_date >= dateFrom && x.invoice_date <= dateTo && x.status == 1).ToList();
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
    }
}
