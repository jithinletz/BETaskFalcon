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
   public class SaleReturnDAL
    {
        public int SaveSaleReturn(EDMX.sales_return _sale, List<EDMX.sales_return_item> items, SaleAccountPostModel saleAccountPostModel)
        {

            int _saleId = 0;
            try
            {
                //string saleNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.SALE.ToString());
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (_sale.sales_return_id == 0)
                            {
                                if (_sale.sales_order == 0)
                                    _sale.sales_order = null;
                                _sale.sales_return_item = items;
                                _sale.sales_number = _sale.sales_number;
                                context.Entry(_sale).State = EntityState.Added;
                                context.SaveChanges();
                                _saleId = _sale.sales_return_id;

                                int deliveryId = 0;
                                int.TryParse(Convert.ToString(_sale.sales_order), out deliveryId);

                                //Updating Wallet
                                if (_sale.payment_mode.ToLower() == "coupon")
                                {
                                    var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                    decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                    cust.wallet_balance = walletBalance + _sale.net_amount;
                                    context.Entry(cust).State = EntityState.Modified;
                                    context.SaveChanges();
                                }

                                
                            }
                            else
                            {
                                _saleId = _sale.sales_return_id;
                                //Updating Wallet
                                var existSale = context.sales_return.AsNoTracking().Where(x => x.sales_return_id == _saleId).FirstOrDefault();
                                if (existSale.payment_mode.ToLower() == "coupon")
                                {
                                    var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                    decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                    cust.wallet_balance = walletBalance - existSale.net_amount;
                                    context.Entry(cust).State = EntityState.Modified;
                                    context.SaveChanges();
                                }

                                if (_sale.payment_mode.ToLower() == "coupon")
                                {
                                    var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                    decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                    cust.wallet_balance = walletBalance + _sale.net_amount;
                                    context.Entry(cust).State = EntityState.Modified;
                                    context.SaveChanges();
                                }


                                _sale.sales_order = _sale.sales_order == 0 ? null : _sale.sales_order;

                                context.Entry(_sale).State = EntityState.Modified;
                                context.SaveChanges();


                                // Remove all the purchase items
                                List<EDMX.sales_return_item> sitems = context.sales_return_item.Where(p => p.sales_return_id == _sale.sales_return_id).ToList();
                                if (sitems != null && sitems.Count > 0)
                                {
                                    context.sales_return_item.RemoveRange(sitems);
                                    context.SaveChanges();
                                }
                                foreach (EDMX.sales_return_item item in items)
                                {
                                    context.Entry(item).State = EntityState.Added;
                                }
                                context.SaveChanges();

                            }
                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_SalesReturn(items, context);
                            AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                            accountTransactionDAL.PostSaleReturn(saleAccountPostModel,_sale, context);
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            if (transaction != null)
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
            return _saleId;
        }
        public EDMX.sales_return GetSaleReturnDetails(int saleId)
        {
            EDMX.sales_return sale = new EDMX.sales_return();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    sale = context.sales_return.Include(p => p.sales_return_item.Select(i => i.item).Select(t => t.tax_setting))
                        .Include(p => p.sales_return_item.Select(i => i.item).Select(u => u.uom_setting)).Include(p => p.customer)
                        .Where(p => p.sales_return_id == saleId)
                        .FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return sale;

        }
        public List<sales_return> SearchSales(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            List<sales_return> listSale = new List<sales_return>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listSale = context.sales_return.Include(x => x.customer).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1).ToList();
                    if (vendorId > 0)
                    {
                        listSale = listSale.Where(x => x.customer_id == vendorId).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listSale;
        }
    }
}
