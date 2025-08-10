using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;

namespace BETask.BAL
{

    class WalletBAL
    {
        WalletDAL objWalletHostoryDAL = null;

       public WalletBAL() {
            objWalletHostoryDAL = new WalletDAL();
        }

       

        /// <summary>
        /// adjustment means , app user collected amount as cash , it will automatically adujust to custmer account 
        /// but will not be update in customer balance . in that case 
        /// </summary>
        /// <param name="_company"></param>
        public void SaveWalletHistory(string walletNo,int customerId,decimal amount,string user,string remarks,DateTime deliveryDate,string paymentMode,int bankId,bool adjustment=false) {
            try
            {
                    CustomerBAL customer = new CustomerBAL();
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                    DAL.Model.WalletAccountPostModel walletAccountPostModel = new DAL.Model.WalletAccountPostModel
                    {
                        CustomerLedger = customer.GetCustomerDetail(customerId).LedgerId,
                        CustomerAmount = amount,
                        BankLedgerAmount = paymentMode.ToLower().Equals("bank") ? amount : 0,
                        BankLedger = Convert.ToInt32(paymentMode.ToLower().Equals("bank") ? bankId : 0),
                        CashLedger = cashLedger,
                        CashAmount = paymentMode.ToLower().Equals("cash") ? amount : 0
                    };

                remarks = adjustment && String.IsNullOrEmpty(remarks) ? "Adjust entry" : remarks;

                EDMX.wallet_history history = new EDMX.wallet_history() {
                    amount=amount,
                    customer_id=customerId,
                    recharge_by=user,
                    wallet_number=walletNo,
                    date= deliveryDate,
                    remarks=remarks,
                    payment_mode=paymentMode
                };
                 
                objWalletHostoryDAL.SaveWalletHostory(history, walletAccountPostModel,adjustment);
                
            }
            catch {
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<EDMX.wallet_history> GetCustomerWalletHistory(int customerId) {
            List<EDMX.wallet_history> history = null; 
            try {
                history = objWalletHostoryDAL.GetCustomerWalletHistory(customerId);
            }
            catch
            {
                throw;
            }
            return history;
        }

    }
}
