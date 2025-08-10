using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;

namespace BETask.APP.DAL
{
    public class WalletAppDAL
    {
        public int GenerateWalletNumber(List<customer> listCustomer)
        {
            int result = 0;
            using (var context = new betaskdbEntitiesAPP())
            {
                try
                {
                    foreach (customer _cs in listCustomer)
                    {
                        if (!string.IsNullOrEmpty(_cs.wallet_number))
                        {
                            customer cs = context.customer.Where(x => x.customer_id == _cs.customer_id).FirstOrDefault();
                            cs.wallet_number = _cs.wallet_number;
                            context.customer.Attach(cs);
                            context.Entry(cs).Property(x => x.wallet_number).IsModified = true;
                            context.SaveChanges();
                            result++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return result;
        }
        public async Task<int> SyncWallet(List<customer> listCustomer)
        {
            int result = 0;
            using (var context = new betaskdbEntitiesAPP())
            {
                try
                {
                    foreach (customer _cs in listCustomer)
                    {
                        if (_cs.customer_id>0)
                        {
                            customer cs = context.customer.Where(x => x.customer_id == _cs.customer_id).FirstOrDefault();
                            
                            cs.wallet_balance = _cs.wallet_balance??0;
                            cs.outstanding_amount= _cs.wallet_balance??0;
                            context.customer.Attach(cs);
                            context.Entry(cs).Property(x => x.wallet_balance).IsModified = true;
                            context.Entry(cs).Property(x => x.outstanding_amount).IsModified = true;
                            await context.SaveChangesAsync();
                            result++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return result;
        }
    }
}
