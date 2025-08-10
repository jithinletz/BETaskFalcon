using BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.DAL
{
    public class WalletDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<EDMX.wallet_history> GetCustomerWalletHistory(int customerId)
        {
            List<EDMX.wallet_history> objHistory = new List<EDMX.wallet_history>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    objHistory = context.wallet_history.Where(w => w.customer_id == customerId).ToList();
                }
            }
            catch
            {
                throw;
            }
            return objHistory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_history"></param>
        public void SaveWalletHostory(EDMX.wallet_history _history, Model.WalletAccountPostModel walletAccountPostModel, bool adjustment = false)
        {
            try
            {
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(_history).State = EntityState.Added;
                            context.SaveChanges();
                            if (!adjustment)
                            {
                                accountTransactionDAL.PostWallet(walletAccountPostModel, _history, context);

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
            catch (Exception ee)
            {
                throw;
            }
        }

        public int WalletPendingCount()
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.customer.Where(x => x.wallet_number == null || x.wallet_number == "" && x.payment_mode.ToLower() == "Coupon").ToList().Count;
                }
            }
            catch { throw; }
        }
        public List<customer> GenerateWalletNumber()
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    system_settings _settings = context.system_settings.AsNoTracking().Where(x => x.status == 1).FirstOrDefault();
                    if (_settings != null)
                    {

                        List<customer> _listCustomer = context.customer.Where(x => x.wallet_number == null || x.wallet_number == "" && x.payment_mode.ToLower() == "coupon").ToList();
                        if (_listCustomer != null && _listCustomer.Count > 0)
                        {
                            foreach (customer cs in _listCustomer)
                            {
                                if (string.IsNullOrEmpty(cs.wallet_number))
                                {
                                    _settings.wallet_prefix = _settings.wallet_prefix == null ? "" : _settings.wallet_prefix;
                                    int pading = _settings.min_wallet_length - (cs.customer_id.ToString().Length + _settings.wallet_prefix.Length);
                                    pading = pading < 0 ? 0 : pading;
                                    string padstring = "".PadRight(pading, '0');
                                    string walletNumber = $"{_settings.wallet_prefix}{padstring}{cs.customer_id}";

                                    cs.wallet_number = walletNumber;
                                    context.customer.Attach(cs);
                                    context.Entry(cs).Property(x => x.wallet_number).IsModified = true;
                                    context.SaveChanges();

                                    listCustomer.Add(cs);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listCustomer;
        }

        public List<customer> GenerateWalletSync(int routeId,int customerId=0)
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (customerId == 0)
                        context.SP_SyncOutstandingToWallet();
                    else
                    {
                        var commandText = $"update customer set wallet_balance=(select (sum(debit)-sum(credit))*-1 from [dbo].[account_transaction] where status=1 and  ledger_id=customer.ledger_id) where customer_id={customerId} and status = 1 and payment_mode = 'Coupon'; ";
                        var name = new System.Data.SqlClient.SqlParameter("@CategoryName", "Test");
                        context.Database.ExecuteSqlCommand(commandText, name);
                    }
                    listCustomer = context.customer.Where(x => x.payment_mode.ToLower() == "coupon" && x.status == 1 && (customerId>0?x.customer_id==customerId:x.customer_id>0) && (routeId > 0 ? x.route_id == routeId : x.route_id > 0)).ToList();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return listCustomer;
        }

        public decimal GetRechargeTax(betaskdbEntities context)
        {
            decimal tax = 0;
            try
            {
                context = context == null ? new betaskdbEntities() : context;
                {
                    system_settings settings = context.system_settings.Where(x => x.status == 1).FirstOrDefault();
                    if (settings != null)
                        tax = settings.tax_on_recharge;

                }
            }
            catch
            {
                throw;
            }
            return tax;
        }

        public decimal SplitTaxinRecharge(decimal rechargeAmount, decimal rechargeTax, out decimal vatAmount)
        {
            decimal finalAmount = 0;
            try
            {
                decimal withTax = rechargeAmount;
                finalAmount = ((withTax * 100) / (100 + rechargeTax));
                finalAmount = Math.Round(finalAmount, 2);
                vatAmount = rechargeAmount - finalAmount;
            }
            catch (Exception ex)
            {
                throw;
            }
            return finalAmount;
        }
    }
}
