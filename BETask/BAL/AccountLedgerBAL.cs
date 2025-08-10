using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BETask.BAL
{
    public class AccountLedgerBAL
    {
        REP.AccountLedgerDAL accountLedgerRep = new REP.AccountLedgerDAL();
        public void SaveAccountLedger(AccountLedgerModel accountLedgerModel)
        {
            try
            {

                EDMX.account_ledger accountDAL = new EDMX.account_ledger()
                {

                    group_id = accountLedgerModel.Group_id,
                    ledger_name = accountLedgerModel.Ledger_name,
                    description = accountLedgerModel.Description,
                    ledger_id = accountLedgerModel.Ledger_id,
                    status = accountLedgerModel.Status,
                    enable_cost_center = accountLedgerModel.EnableCostCnetr 


                };
                 accountLedgerRep.SaveAccountLedger(accountDAL);
            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Saving of Ledger { accountLedgerModel.Ledger_name} Id ={accountLedgerModel.Ledger_id}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public bool IsCostCenterEnabled(int ledgerId)
        {
            bool resp = false;
            try
            {
                EDMX.account_ledger ledger = accountLedgerRep.GetLedgerDetail(ledgerId);
                if (ledger != null)
                {
                    if (ledger.enable_cost_center ==1)
                        resp = true;
                    else
                        resp = false;
                }
            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<EDMX.account_ledger> GetAllAccountLedger()
        {
            
            try
            {
                List<EDMX.account_ledger> listAccountLedgerDAL = accountLedgerRep.GetAllAccountLedger();
                return listAccountLedgerDAL;
            }
            catch (Exception ee)
            {
                throw ee;
            }           
        }

        public List<AccountLedgerModel> GetAllAccountLedger(int group_id)
        {
            List<AccountLedgerModel> listAccountLedger = new List<AccountLedgerModel>();
            try
            {
                List<EDMX.account_ledger> listAccountLedgerDAL = accountLedgerRep.GetAllAccountLedger(group_id);
               return CustomMapper.MapAccountLedger(listAccountLedgerDAL);
                //foreach (EDMX.account_ledger account in listAccountLedgerDAL)
                //{
                //    listAccountLedger.Add(new AccountLedgerModel()
                //    {
                //        Group_id = account.group_id,
                //        Ledger_name = account.ledger_name,
                //        Ledger_id = account.ledger_id,
                //        GroupName=account.account_group.group_name

                //    });
                //}

            }
            catch (Exception ee)
            {
                throw;
            }
           
        }
        public async Task<List<AccountLedgerModel>> GetAllAccountLedgersAsync(int group_id)
        {
            List<AccountLedgerModel> listAccountLedger = new List<AccountLedgerModel>();
            try
            {
                List<EDMX.account_ledger> listAccountLedgerDAL =await accountLedgerRep.GetAllAccountLedgers(group_id);
                return CustomMapper.MapAccountLedger(listAccountLedgerDAL);

            }
            catch (Exception ee)
            {
                throw;
            }

        }

        public List<AccountLedgerModel> GetAllAccountLedgerNonCustomer(int group_id)
        {
            List<AccountLedgerModel> listAccountLedger = new List<AccountLedgerModel>();
            try
            {
                List<EDMX.account_ledger> listAccountLedgerDAL = accountLedgerRep.GetAllAccountLedgerNonCustomer(group_id);
                foreach (EDMX.account_ledger account in listAccountLedgerDAL)
                {
                    listAccountLedger.Add(new AccountLedgerModel()
                    {
                        Group_id = account.group_id,
                        Ledger_name = account.ledger_name,
                        Ledger_id = account.ledger_id,
                        GroupName = account.account_group.group_name

                    });
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountLedger;
        }
        public List<EDMX.account_ledger> GetAllSalesmanCreditLedger()
        {
            try
            {
                return accountLedgerRep.GetAllSalesmanCreditLedger();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public EDMX.account_ledger GetLedgerDetailByCustomerId(int customerId)
        {
            try
            {
                return accountLedgerRep.GetLedgerDetailByCustomerId(customerId);
            }
            catch
            { throw; }
        }
        public EDMX.account_ledger GetLedgerDetail(int ledgerId)
        {
            try
            {
                return accountLedgerRep.GetLedgerDetail(ledgerId);
            }
            catch
            { throw; }
        }

        public List<EDMX.ledger_mapping> GetAllLedgerMapping()
        {
            try
            {
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new REP.LedgerMappingDAL();
               return ledgerMappingDAL.GetAllLedgerMapping();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

            public void SaveLedgerMapSetting(EDMX.ledger_mapping ledgerMap)
        {
            try
            {
                DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new REP.LedgerMappingDAL();
                ledgerMappingDAL.SaveLedgerMapSetting(ledgerMap);
            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Saving of Ledger Mapiing {ledgerMap.ledger_type}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
    }
}
