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
                using (var context = new betaskdbEntities())
                {
                    /*
                    if (ledger.ledger_id > 0)
                    {
                        var ex = context.account_ledger.Where(x => x.ledger_id == ledger.ledger_id).FirstOrDefault();
                        ledger.group_id = ex.group_id;
                    }
                    */
                }
                using (var context = new betaskdbEntities())
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
                using (var context = new betaskdbEntities())
                {
                    listAccountLedger = context.account_ledger.AsNoTracking().Include(x => x.account_group).Where(x => x.status == 1 && (group_id >= 0 ? (x.group_id == group_id) : x.group_id >= 0)).OrderBy(x => x.group_id).ThenBy(x => x.ledger_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }

        public async Task<List<account_ledger>> GetAllAccountLedgers(int group_id)
        {
            List<account_ledger> listAccountLedger = new List<account_ledger>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    //listAccountLedger = context.account_ledger.Include(x=>x.account_group).Where(x => x.status == 1 &&  ( group_id>=0? (x.group_id==group_id): x.group_id>=0)).OrderBy(x => x.group_id).ThenBy(x=>x.ledger_name).ToList();
                    var query = await context.account_ledger
             .Include(x => x.account_group)
             .Where(x => x.status == 1 && (group_id >= 0 ? (x.group_id == group_id) : x.group_id >= 0))
             .OrderBy(x => x.group_id)
             .ThenBy(x => x.ledger_name)
             .ToListAsync();
                    return query;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            // return listAccountLedger;
        }

        public List<account_ledger> GetOtherBankAccountLedger()
        {
            List<account_ledger> listAccountLedger = new List<account_ledger>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listAccountLedger = context.account_ledger.Where(x => x.description == "Bank" && x.status == 1).OrderBy(x => x.group_id).ThenBy(x => x.ledger_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }

        public List<account_ledger> GetAllAccountLedger()
        {
            List<account_ledger> listAccountLedger = new List<account_ledger>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listAccountLedger = context.account_ledger.Include(x => x.account_group).Where(x => x.status == 1).OrderBy(x => x.group_id).ThenBy(x => x.ledger_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }

        public List<account_ledger> GetAllAccountLedgerNonCustomer(int group_id)
        {
            List<account_ledger> listAccountLedger = new List<account_ledger>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listAccountLedger = context.account_ledger.Include(x => x.account_group).Where(x => x.description != "CUSTOMER" && x.status == 1 && (group_id > 0 ? (x.group_id == group_id) : x.group_id >= 0)).OrderBy(x => x.ledger_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }



        public List<customer> GetAllAccountLedgerCustomerOnly()
        {
            List<customer> listAccountLedger = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    //int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.CUSTOMER).group_id);
                    listAccountLedger = context.customer.Where(x => x.status >= 1).OrderBy(x => x.customer_name).ToList();
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
                using (var context = new betaskdbEntities())
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

                using (var context = new betaskdbEntities())
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
        public bool ValidateLedger(string ledgerName)
        {
            bool resp = false;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    account_ledger ledger = context.account_ledger.FirstOrDefault(x => x.ledger_name.ToLower() == ledgerName.ToLower());
                    if (ledger == null)
                        resp = true;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return resp;
        }
        public List<account_ledger> GetAllSalesmanCreditLedger()
        {
            List<account_ledger> listAccountLedger = new List<account_ledger>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listAccountLedger = context.account_ledger.Include(x => x.account_group).Where(x => x.status == 1 && x.description == "SALESMANCREDIT").OrderBy(x => x.group_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }
        public List<account_ledger> SearchLedger(string ledgerName, int groupId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (ledgerName == "")
                        return context.account_ledger.Include(x => x.account_group).Where(x => x.status == 1 && (groupId > 0 ? x.group_id == groupId : x.group_id > 0)).OrderBy(x => x.ledger_name).ToList();
                    else
                        return context.account_ledger.Include(x => x.account_group).Where(x => x.status == 1 && (x.ledger_name.Contains(ledgerName)) && (groupId > 0 ? x.group_id == groupId : x.group_id > 0)).OrderBy(x => x.ledger_name).ToList();


                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        public List<account_group> GetAccountsGroups()
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.account_group.OrderBy(t => t.group_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }
}
