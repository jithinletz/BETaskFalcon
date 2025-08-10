using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;
using System.Data.SqlClient;
using BETask.DAL.DAL;

namespace BETask.DAL.Interface
{
    public static class  APosting
    {
        public static void OutstandingCollectionPost(daily_collection coll, betaskdbEntities context)
        {
            try
            {
                int result = 0;
                List<account_transaction> listAccount = new List<account_transaction>();
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                CustomerDAL customerDAL = new CustomerDAL();
                var customerLedgerId = customerDAL.GetCustomerDetails(coll.customer_id, context).ledger_id;
                int _customerLedgerId = 0;
                if (customerLedgerId != null)
                    _customerLedgerId = Convert.ToInt32(customerLedgerId);

                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                employee emp = context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == coll.employee_id);
                int routeId = Convert.ToInt32(emp.route_id);

                if (coll.is_deposit == 1)
                {
                    //Deposit ledger credit here
                    ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.DEPOSITCOLLECTION);
                    if (ledgerDeposit != null)
                        _customerLedgerId = Convert.ToInt32(ledgerDeposit.ledger_id);

                }
                //CR
                listAccount.Add(new EDMX.account_transaction
                {
                    ledger_id = _customerLedgerId,
                    transaction_date = coll.delivery_time,
                    credit = coll.collected_amount,
                    debit = 0,
                    narration =coll.is_deposit!=1? $"{coll.payment_mode}  Collection {coll.remarks}": $"{coll.remarks}",
                    transaction_type = AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                    status = 1,
                    transaction_number = 0,
                    transaction_type_id = 0,
                    voucher_number = coll.daily_collection_id.ToString(),
                    route_id = routeId,


                });

                int creditLedger = 0;
                AccountLedgerDAL accountLedgerDAL = new AccountLedgerDAL();
                if (coll.payment_mode.ToLower() == "bank")
                {
                    var onlineRecharge = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.ONLINEPAYMENTBANK);
                    int bankId = 0;
                    if (onlineRecharge == null)
                    {
                        int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(BETask.DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                         bankId = accountLedgerDAL.GetAllAccountLedger(groupId)[0].ledger_id;
                    }
                    else
                    {
                        bankId = Convert.ToInt32(onlineRecharge.ledger_id);
                    }

                    creditLedger = bankId;
                }
                else if (coll.payment_mode.ToLower() == "cash")
                {
                    int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                    creditLedger = cashLedger;
                }
                else
                {
                    int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                    creditLedger = cashLedger;
                }
                //DR
                listAccount.Add(new EDMX.account_transaction
                {
                    ledger_id = creditLedger,
                    transaction_date = coll.delivery_time,
                    credit = 0,
                    debit = coll.collected_amount,
                    narration = $"{coll.payment_mode}  Collection {coll.remarks}",
                    transaction_type = AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                    status = 1,
                    transaction_number = 0,
                    transaction_type_id = 0,
                    voucher_number = coll.daily_collection_id.ToString(),
                    route_id = routeId

                });
                accountTransactionDAL.SaveAccountTransactionList(listAccount, AccountTransactionDAL.EnumTransactionTypes.RECIEPT, context);
                //Updatting Wallet

                customer objCustomer = context.customer.Where(c => c.customer_id == coll.customer_id).FirstOrDefault();

                if ((coll.payment_mode.ToLower() == "coupon" || coll.payment_mode.ToLower() == "bank") && coll.is_deposit != 1)
                {


                    var employee = context.employee.AsNoTracking().Where(x => x.employee_id == coll.employee_id).FirstOrDefault();

                    EDMX.wallet_history history = new EDMX.wallet_history()
                    {
                        amount = coll.collected_amount,
                        customer_id = coll.customer_id,
                        recharge_by =coll.payment_mode.ToLower()=="bank"?"Online": employee.first_name,
                        wallet_number = objCustomer.wallet_number,
                        date = coll.delivery_time,
                        remarks = $"{coll.remarks}",
                        payment_mode = coll.payment_mode
                    };

                    context.Entry(history).State = EntityState.Added;
                    context.SaveChanges();
                }


                //Updating wallet balance for all type of payment modes if the customer is an wallet customer
                //else
                //{
                    if (!string.IsNullOrEmpty(objCustomer.wallet_number))
                    {
                        objCustomer.wallet_balance = Convert.ToDecimal(objCustomer.wallet_balance) + coll.collected_amount;
                        context.Entry(objCustomer).State = EntityState.Modified;
                        context.SaveChanges();

                        //var employee = context.employee.AsNoTracking().Where(x => x.employee_id == coll.employee_id).FirstOrDefault();

                        //EDMX.wallet_history history = new EDMX.wallet_history()
                        //{
                        //    amount = coll.collected_amount,
                        //    customer_id = coll.customer_id,
                        //    recharge_by = employee.first_name,
                        //    wallet_number = objCustomer.wallet_number,
                        //    date = DateTime.Now,
                        //    remarks = $"{coll.remarks} {coll.payment_mode}",
                        //    payment_mode = coll.payment_mode
                        //};
                    }
                //}
                DAL.SynchronizationDAL synchronization = new SynchronizationDAL(context);
                decimal newBal = 0;
                synchronization.UpdateWalletAsOutstanding(coll.customer_id, ref newBal);
                result++;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
   
}
