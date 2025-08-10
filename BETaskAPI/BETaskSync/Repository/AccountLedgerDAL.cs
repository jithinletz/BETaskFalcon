using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskSync.Common;
using BETaskAPI.DAL.DAL;
using BETaskSync.EDMX;
using edmx = BETaskAPI.DAL.EDMX;
using edmxLocal = BETaskSync.EDMX;
using System.Data.Entity;

namespace BETaskSync.Repository
{
    public class AccountLedgerDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public int SaveAccountLedger(account_ledger ledger)
        {
            int ledgerId = 0;
            try
            {
                using (var context = new betaskdbEntitiesLocal())
                {
                    if (ledger.ledger_id > 0)
                    {
                        var ex = context.account_ledger.Where(x => x.ledger_id == ledger.ledger_id).FirstOrDefault();
                        ledger.group_id = ex.group_id;
                    }
                }
                using (var context = new betaskdbEntitiesLocal())
                {

                    context.Entry(ledger).State = ledger.ledger_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                    ledgerId = ledger.ledger_id;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return ledgerId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<account_ledger> GetAllAccountLedger(int group_id)
        {
            List<account_ledger> listAccountLedger = new List<account_ledger>();
            try
            {
                using (var context = new betaskdbEntitiesLocal())
                {
                    listAccountLedger = context.account_ledger.Include(x => x.account_group).Where(x => x.status == 1 && (group_id >= 0 ? (x.group_id == group_id) : x.group_id >= 0)).OrderBy(x => x.group_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }
        public account_ledger GetLedgerDetail(int ledgerId)
        {
            account_ledger accountLedger = new account_ledger();
            try
            {
                using (var context = new betaskdbEntitiesLocal())
                {
                    accountLedger = context.account_ledger.Where(x => x.ledger_id == ledgerId).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return accountLedger;
        }
        public account_ledger GetLedgerDetailByCustomerId(int customerId)
        {
            account_ledger accountLedger = new account_ledger();
            try
            {
                int ledgerId = 0;

                using (var context = new betaskdbEntitiesLocal())
                {
                    customer cust = context.customer.Where(x => x.customer_id == customerId).FirstOrDefault();
                    if (cust != null)
                        ledgerId = Convert.ToInt32(cust.ledger_id);
                    accountLedger = context.account_ledger.Where(x => x.ledger_id == ledgerId).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return accountLedger;
        }

    }
}
