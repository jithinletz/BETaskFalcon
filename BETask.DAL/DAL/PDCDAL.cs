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
    public class PDCDAL
    {
        public int SavePDC(EDMX.pdc pdc)
        {
            int pdcId = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.Entry(pdc).State = pdc.pdc_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();
                            pdcId = pdc.pdc_id;

                            //Account posting

                            account_transaction xTransaction = context.account_transaction.AsNoTracking().Where(x => x.voucher_number == pdc.cheque_number && x.transaction_type.ToLower() == pdc.pdc_mode.ToLower() && x.status == 1 && x.ledger_id==pdc.party_id).FirstOrDefault();
                            if (xTransaction == null)
                            {
                                if (pdc.pdc_mode.ToLower() == "payment")
                                    PostPDCIssued(pdc, context, transaction);
                                else
                                    PostPDCRecieved(pdc, context, transaction);
                            }

                            transaction.Commit();
                        }
                        catch
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
            return pdcId;
        }
        public List<EDMX.pdc> SearchPDC(string cheuqeStatus, DateTime chequeDate, DateTime chequeDateTo)
        {
            List<EDMX.pdc> listPDC = new List<pdc>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listPDC = context.pdc.Include(a => a.account_ledger).Include(a => a.account_ledger1).Where(x => x.cheque_date >= chequeDate && x.cheque_date <= chequeDateTo && x.status == 1).ToList();
                    if (cheuqeStatus != "")
                    {
                        listPDC = listPDC.Where(x => x.cheque_status.ToLower().Contains(cheuqeStatus.ToLower())).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listPDC;
        }
        public EDMX.pdc SearchPDCbyId(int pdcId)
        {
            EDMX.pdc PDC = new pdc();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    PDC = context.pdc.Include(a => a.account_ledger).Include(a => a.account_ledger1).Where(x => x.pdc_id == pdcId).FirstOrDefault();

                }
            }
            catch
            {
                throw;
            }
            return PDC;
        }
        public void DeletePDC(int pdcId)
        {
            try
            {

                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            EDMX.pdc pdc = context.pdc.Where(x => x.pdc_id == pdcId).FirstOrDefault();
                            if (pdc != null)
                            {
                                pdc.status = 2;
                                context.Entry(pdc).State = EntityState.Modified;
                                context.SaveChanges();
                            }

                            List<account_transaction> lisTransaction = context.account_transaction.AsNoTracking().Where(x => x.transaction_type_id == pdcId && x.voucher_number == pdc.cheque_number && x.transaction_type.ToLower() == pdc.pdc_mode).ToList();
                            if (lisTransaction != null && lisTransaction.Count > 0)
                            {
                                lisTransaction.ForEach(x => x.status = 2);

                            }
                            context.SaveChanges();

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
            catch
            {
                throw;
            }
        }
        public void UpdatePDC(int pdcId, string status,DateTime date)
        {
            try
            {
                EDMX.pdc pdc = new pdc();
                int tranNumber = 0;

                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            pdc = context.pdc.Where(x => x.pdc_id == pdcId).FirstOrDefault();
                            if (pdc != null)
                            {
                                pdc.cheque_status = status;
                                context.Entry(pdc).State = EntityState.Modified;
                                context.SaveChanges();
                                pdc = context.pdc.Include(p => p.account_ledger1).AsNoTracking().Where(x => x.pdc_id == pdcId).FirstOrDefault();

                                if (status.ToLower() == "done")
                                {
                                    //docdate selected in PDC while done
                                    pdc.updated_on = date;
                                    /*Posting Account*/
                                    if (pdc.pdc_mode.ToLower() == "payment")
                                        tranNumber = PostPDCPayment(pdc, context, transaction);
                                    else
                                        tranNumber = PostPDCReceipt(pdc, context, transaction);


                                    /*End Posting Account*/
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw;
                        }
                        finally
                        {

                            //Saving Cheque
                            string bank = "";
                            try
                            {
                                if (!string.IsNullOrEmpty(pdc.remarks))
                                {
                                    if (pdc.remarks.ToLower().Contains("bank = ") || pdc.remarks.ToLower().Contains("bank=") || pdc.remarks.ToLower().Contains("bank =") || pdc.remarks.ToLower().Contains("bank= "))
                                    {
                                        string remarks = pdc.remarks.ToLower();
                                        string[] stringSeparators = new string[] { "bank" };
                                        List<string> splited = remarks.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();
                                        if (splited != null && splited.Count > 0)
                                        {
                                            bank = splited[splited.Count - 1].Replace("=", "");
                                        }
                                    }
                                }
                            }
                            catch { }

                            EDMX.account_transaction_cheque cheque = new EDMX.account_transaction_cheque
                            {
                                cheque_date = pdc.cheque_date,
                                cheque_number = pdc.cheque_number,
                                bank = bank,
                                other_details = pdc.remarks,
                                account_transaction_number = tranNumber,
                                status = 1

                            };
                            AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                            accountTransactionDAL.SaveAccountTransactionCheque(cheque);
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        private int PostPDCPayment(EDMX.pdc pdc, betaskdbEntities context, DbContextTransaction transaction)
        {
            int tranNumber = 0;
            try
            {
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                ledger_mapping ledger = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.PDCISSUED);
                if (pdc.pdc_mode.ToLower() == "payment")
                {
                    //Dr
                    EDMX.account_transaction account_Transaction = new account_transaction
                    {
                        ledger_id = Convert.ToInt32(ledger.ledger_id),//pdc issued account
                        transaction_date = pdc.updated_on,
                        transaction_type = AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                        debit = pdc.amount,
                        credit = 0,
                        transaction_type_id = pdc.pdc_id,
                        voucher_number = pdc.cheque_number,
                        narration = $"{pdc.remarks} {pdc.account_ledger1.ledger_name}",
                        status = 1
                    };
                    listAccount.Add(account_Transaction);
                    //Cr
                    account_Transaction = new account_transaction
                    {
                        ledger_id = pdc.ledger_id,
                        transaction_date = pdc.updated_on,
                        transaction_type = AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                        debit = 0,
                        credit = pdc.amount,
                        transaction_type_id = pdc.pdc_id,
                        voucher_number = pdc.cheque_number,
                        narration = $"{pdc.remarks} {pdc.account_ledger1.ledger_name}",
                        status = 1
                    };
                    listAccount.Add(account_Transaction);
                    string referance = "";
                    tranNumber = accountTransactionDAL.SaveAccountTransactionList(listAccount, AccountTransactionDAL.EnumTransactionTypes.PAYMENT,ref referance);
                }
            }
            catch
            {
                throw;
            }
            return tranNumber;
        }
        private int PostPDCReceipt(EDMX.pdc pdc, betaskdbEntities context, DbContextTransaction transaction)
        {
            int tranNumber = 0;
            try
            {
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                ledger_mapping ledger = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.PDCRECIEVED);
                if (pdc.pdc_mode.ToLower() == "reciept")
                {
                    //Dr
                    EDMX.account_transaction account_Transaction = new account_transaction
                    {
                        ledger_id = pdc.ledger_id,//Bank account
                        transaction_date = pdc.updated_on,
                        transaction_type = AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                        debit = pdc.amount,
                        credit = 0,
                        transaction_type_id = pdc.pdc_id,
                        voucher_number = pdc.cheque_number,
                        narration = $" PDC cheque cleared for receipt {pdc.account_ledger1.ledger_name}",
                        status = 1
                    };
                    listAccount.Add(account_Transaction);
                    //Cr
                    account_Transaction = new account_transaction
                    {
                        ledger_id = Convert.ToInt32(ledger.ledger_id),//pdc reciept account
                        transaction_date = pdc.updated_on,
                        transaction_type = AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                        debit = 0,
                        credit = pdc.amount,
                        transaction_type_id = pdc.pdc_id,
                        voucher_number = pdc.cheque_number,
                        narration = $"  PDC cheque cleared for receipt {pdc.account_ledger1.ledger_name}",
                        status = 1
                    };
                    listAccount.Add(account_Transaction);
                    string referance = "";
                    tranNumber = accountTransactionDAL.SaveAccountTransactionList(listAccount, AccountTransactionDAL.EnumTransactionTypes.RECIEPT,ref referance);
                }
            }
            catch
            {
                throw;
            }
            return tranNumber;
        }
        /// <summary>
        /// Supplier a/c Dr To
        ///   PDC Issued
        /// </summary>
        /// <param name="pdc"></param>
        /// <param name="context"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private int PostPDCIssued(EDMX.pdc pdc, betaskdbEntities context, DbContextTransaction transaction)
        {
            int tranNumber = 0;
            try
            {
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
                string ledgerName = string.Empty;
                ledgerName = context.account_ledger.AsNoTracking().Where(x => x.ledger_id == pdc.party_id).Select(l => l.ledger_name).FirstOrDefault();
                if (pdc.pdc_mode.ToLower() == "payment")
                {
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    ledger_mapping ledger = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.PDCISSUED);
                    if (ledger != null)
                    {
                        //Dr
                        EDMX.account_transaction account_Transaction = new account_transaction
                        {
                            ledger_id = pdc.party_id,
                            transaction_date = pdc.doc_date,
                            transaction_type = AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                            debit = pdc.amount,
                            credit = 0,
                            transaction_type_id = pdc.pdc_id,
                            voucher_number = pdc.cheque_number,
                            narration = $"{pdc.remarks} {ledgerName}",
                            status = 1
                        };
                        listAccount.Add(account_Transaction);
                        //Cr
                        account_Transaction = new account_transaction
                        {
                            ledger_id = Convert.ToInt32(ledger.ledger_id),
                            transaction_date = pdc.doc_date,
                            transaction_type = AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString(),
                            debit = 0,
                            credit = pdc.amount,
                            transaction_type_id = pdc.pdc_id,
                            voucher_number = pdc.cheque_number,
                            narration = $"{pdc.remarks} {ledgerName}",
                            status = 1
                        };
                        listAccount.Add(account_Transaction);

                        string referance = "";
                        tranNumber = accountTransactionDAL.SaveAccountTransactionList(listAccount, AccountTransactionDAL.EnumTransactionTypes.PAYMENT, ref referance);
                    }
                }
            }
            catch
            {
                throw;
            }
            return tranNumber;
        }
        /// <summary>
        /// PDC Recieved a/c dr To
        ///   Cutomer a/c
        /// </summary>
        /// <param name="pdc"></param>
        /// <param name="context"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private int PostPDCRecieved(EDMX.pdc pdc, betaskdbEntities context, DbContextTransaction transaction)
        {
            int tranNumber = 0;
            try
            {
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
                if (pdc.pdc_mode.ToLower() == "reciept")
                {
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    ledger_mapping ledger = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.PDCRECIEVED);
                    string ledgerName = string.Empty;
                    ledgerName = context.account_ledger.AsNoTracking().Where(x => x.ledger_id == pdc.party_id).Select(l => l.ledger_name).FirstOrDefault();
                    //Dr
                    EDMX.account_transaction account_Transaction = new account_transaction
                    {
                        ledger_id = Convert.ToInt32(ledger.ledger_id),//pdc recieved account
                        transaction_date = pdc.doc_date,
                        transaction_type = AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                        debit = pdc.amount,
                        credit = 0,
                        transaction_type_id = pdc.pdc_id,
                        voucher_number = pdc.cheque_number,
                        narration = $" PDC recieved {pdc.remarks} {ledgerName}",
                        status = 1
                    };
                    listAccount.Add(account_Transaction);
                    //Cr
                    account_Transaction = new account_transaction
                    {
                        ledger_id = pdc.party_id,
                        transaction_date = pdc.doc_date,
                        transaction_type = AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString(),
                        debit = 0,
                        credit = pdc.amount,
                        transaction_type_id = pdc.pdc_id,
                        voucher_number = pdc.cheque_number,
                        narration = $" PDC recieved {pdc.remarks} {ledgerName}",
                        status = 1
                    };
                    listAccount.Add(account_Transaction);
                    string referance = "";
                    tranNumber = accountTransactionDAL.SaveAccountTransactionList(listAccount, AccountTransactionDAL.EnumTransactionTypes.RECIEPT,ref referance);
                }
            }
            catch
            {
                throw;
            }
            return tranNumber;
        }
        public int GetPDCTransactionNumber(pdc _pdc)
        {
            int tranNumber = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var accTran = context.account_transaction.FirstOrDefault(x => x.transaction_date==_pdc.cheque_date && x.voucher_number == _pdc.cheque_number);
                    if (accTran != null)
                    {
                        tranNumber = accTran.transaction_number;
                    }
                }
            }
            catch
            {
                throw;
            }
            return tranNumber;
        }
    }
}


