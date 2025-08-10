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
    public class TransferItemsDAL
    {
        public void SaveTransfer(transfer_item transfer)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(transfer).State = EntityState.Added ;
                            context.SaveChanges();

                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_Transfer(transfer, context);
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
        }
        public List<transfer_item> GetTransferItem(DateTime dateFrom, DateTime dateTo)
        {
            List<transfer_item> listTransfer = new List<transfer_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listTransfer = context.transfer_item.AsNoTracking().Include(i => i.item).Include(e => e.employee).Where(x => x.transfer_date >= dateFrom && x.transfer_date <= dateTo && x.status == 1).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listTransfer;
        }
        public List<transfer_item> GetTransferItemByItemId(DateTime dateFrom, DateTime dateTo,int ItemId)
        {
            List<transfer_item> listTransfer = new List<transfer_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (ItemId > 0)
                    { listTransfer = context.transfer_item.AsNoTracking().Include(i => i.item).Include(e => e.employee).Where(x => x.transfer_date >= dateFrom && x.transfer_date <= dateTo && x.status == 1 && x.item_id== ItemId).ToList(); }
                    else
                    { listTransfer = context.transfer_item.AsNoTracking().Include(i => i.item).Include(e => e.employee).Where(x => x.transfer_date >= dateFrom && x.transfer_date <= dateTo && x.status == 1).ToList(); }

                    
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listTransfer;
        }
        public transfer_item GetTransferById(int transferId)
        {
            transfer_item _Item = new transfer_item();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _Item = context.transfer_item.AsNoTracking().Include(i => i.item).Include(e => e.employee).Where(x => x.transfer_id==transferId).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return _Item;
        }
    }
}
