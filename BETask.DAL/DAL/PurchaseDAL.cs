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
    public class PurchaseDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// 

        public int SavePurchase(EDMX.purchase _purchase, List<EDMX.purchase_item> items, PurchaseAccountPostModel purchaseAccountPost)
        {
            AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
            int _purchaseId = 0;
            bool success = false;
            bool isEdit = false;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var _days = (_purchase.purchase_date - DateTime.Now).Days;
                            int allowedDate = context.system_settings.AsNoTracking().FirstOrDefault(x => x.status == 1).allowed_backdate * -1;
                            if (_days < allowedDate)
                            {
                                PrivilegeDAL privileges = new PrivilegeDAL();
                                if (!privileges.IsPriviligeProvided(Constants.UserId, PrivilegeDAL.Privileges.AllowBackDate, context))
                                    throw new Exception("Backdate entry update is not allowed");
                            }
                            if (_purchase.purchase_id == 0)
                            {
                                _purchase.purchase_item = items;
                                context.Entry(_purchase).State = _purchase.purchase_id == 0 ? EntityState.Added : EntityState.Modified;
                                context.SaveChanges();
                                _purchaseId = _purchase.purchase_id;

                            }
                            else
                            {
                                isEdit = true;
                                _purchaseId = _purchase.purchase_id;
                                context.Entry(_purchase).State = _purchase.purchase_id == 0 ? EntityState.Added : EntityState.Modified;

                                // Remove all the purchase items
                                List<EDMX.purchase_item> pitems = context.purchase_item.Where(p => p.purchase_id == _purchase.purchase_id).ToList();
                                if (pitems != null && pitems.Count > 0)
                                {
                                    context.purchase_item.RemoveRange(pitems);
                                    context.SaveChanges();
                                }
                                foreach (EDMX.purchase_item item in items)
                                {
                                    context.Entry(item).State = EntityState.Added;
                                }

                            }

                            itemTransactionDAL.SaveItemTransaction_Purchase(items, context, isEdit);

                            List<account_transaction> listMultpleSaleAccount = GetPurchaseLedgers_AccountPosting(items, context);

                            accountTransactionDAL.PostPurchase(purchaseAccountPost, _purchase, context, listMultpleSaleAccount);
                            context.SaveChanges();
                            transaction.Commit();
                            success = true;
                        }
                        catch (Exception ee)
                        {

                            transaction.Rollback();
                            throw;
                        }
                        finally
                        {
                            if (success)
                            {
                                ItemDAL itemDAL = new ItemDAL();
                                //itemDAL.CalculateItemCostAsync(items).GetAwaiter().GetResult();
                            }
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

        public bool UpdatePurchaseSupplierPayment(purchase purchase)
        {
            bool resp = false;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(purchase).State = EntityState.Modified;
                            context.SaveChanges();
                            transaction.Commit();
                            resp = true;
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
            return resp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        public EDMX.purchase  GetPurchaseDetails(int purchaseId) {
            EDMX.purchase purchase = new EDMX.purchase();
           
            try
            {
                using (var context = new betaskdbEntities())
                {

                    purchase = context.purchase.Include(p => p.purchase_item.Select(i => i.item).Select(t=>t.tax_setting))
                        .Include(p=>p.purchase_item.Select(i=>i.item).Select(u=>u.uom_setting)).Include(p=>p.customer)
                        .Where(p=>p.purchase_id== purchaseId)
                        .FirstOrDefault(); 
                }
            }
            catch
            {
                throw;
            }
            return purchase;

        }

        private List<account_transaction> GetPurchaseLedgers_AccountPosting(List<purchase_item> listItems, betaskdbEntities context)
        {
            List<account_ledger> listSalesLedger = new List<account_ledger>();
            List<account_transaction> listTransaction = new List<account_transaction>();
            try
            {
                LedgerMappingDAL ledgerMapping = new LedgerMappingDAL();
                ledger_mapping defaultPurchaseLedger = ledgerMapping.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.PURCHASE);
                int dafaultPurchase = 0;
                if (defaultPurchaseLedger != null)
                {
                    dafaultPurchase = Convert.ToInt32(defaultPurchaseLedger.ledger_id);
                    account_ledger ledger = context.account_ledger.Find(dafaultPurchase);
                    if (ledger != null)
                    {
                        listSalesLedger.Add(ledger);

                        listTransaction.Add(new account_transaction
                        {
                            ledger_id = ledger.ledger_id,
                            debit = 0,
                            credit = 0,
                        });
                    }
                }

                List<int> _listItemIds = listItems.Select(x => x.item_id).Distinct().ToList();
                var _lisPurchaseLedgerIds = context.item.AsNoTracking().Where(x => _listItemIds.Contains(x.item_id) && x.purchase_ledger != null).Select(x => x.purchase_ledger).Distinct().ToList();
                if (_lisPurchaseLedgerIds != null && _lisPurchaseLedgerIds.Count > 0)
                {
                    bool multipleSale = false;
                    foreach (int _id in _lisPurchaseLedgerIds)
                    {
                        if (_id != dafaultPurchase)
                        {
                            account_ledger ledger = context.account_ledger.Find(_id);
                            if (ledger != null)
                            {
                                multipleSale = true;
                                listSalesLedger.Add(ledger);//Sale account

                                listTransaction.Add(new account_transaction
                                {
                                    ledger_id = ledger.ledger_id,
                                    debit = 0,
                                    credit = 0,
                                });
                            }
                        }
                    }
                    if (!multipleSale)
                    {
                        return null;
                    }
                }
                else
                    return null;



                //Setting Transaction
                foreach (purchase_item sl in listItems)
                {
                    decimal amount = sl.gross_amount;
                    item it = context.item.Find(sl.item_id);

                    //Default sale account
                    if (it.purchase_ledger == null)
                    {
                        listTransaction[0].debit += amount;
                    }
                    //setting accounts other than default sale account
                    else
                    {
                        foreach (account_transaction at in listTransaction)
                        {
                            if (it.purchase_ledger != null && at.ledger_id == it.purchase_ledger)
                            {
                                at.debit += amount;
                            }
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            return listTransaction;
        }

        public List<purchase> SearchPurchase(DateTime dateFrom, DateTime dateTo,int vendorId)
        {
            List<purchase> listPurchase = new List<purchase>();
            //dateTo = dateTo.AddHours(23).AddMinutes(59).AddSeconds(59);
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchase =context.purchase.Include(x=>x.customer).Where(x=>x.invoice_date>=dateFrom && x.invoice_date <= dateTo && x.status==1).ToList();
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
        public List<purchase> SupplierPurchaseReport(DateTime dateFrom, DateTime dateTo, int vendorId, string paymentmode)
        {
            List<purchase> listPurchase = new List<purchase>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchase = context.purchase.Include(x => x.customer).Where(x => x.invoice_date >= dateFrom && x.invoice_date <= dateTo && x.status == 1).ToList();
                    if (vendorId > 0)
                    {
                        listPurchase = listPurchase.Where(x => x.vendor_id == vendorId).ToList();
                    }
                    if (paymentmode.ToLower() != "all")
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
        public List<purchase> GetSupplierPaymentDueDetails(DateTime dateAsOn, int vendorId)
        {
            List<purchase> listPurchase = new List<purchase>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPurchase = context.purchase.Include(x => x.customer).Where(x =>  x.invoice_date <= dateAsOn && x.vendor_id== vendorId && x.cash_paid<x.net_amount && x.status == 1).ToList();
                    
                }
            }
            catch
            {
                throw;
            }
            return listPurchase;
        }
        public List<purchase_item> ItemPurchaseReport(DateTime dateFrom, DateTime dateTo, int vendorId, int itemId)
        {
            List<purchase_item> listPurchaseItem = new List<purchase_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    // listSale = context.sales.Include(x => x.customer).Include(x=>x.sales_item.Select(i => i.item).Select(u => u.uom_setting)).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1).ToList();
                    listPurchaseItem = context.purchase_item.Include(s => s.purchase).Include(v=>v.purchase.customer).Include(i => i.item).Include(p => p.item.uom_setting).Where(s => s.purchase.invoice_date >= dateFrom && s.purchase.invoice_date <= dateTo).OrderBy(x => x.purchase.invoice_date).ThenBy(x => x.item_id).ToList();
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
