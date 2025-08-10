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
using System.Data.Entity.Validation;

namespace BETask.DAL.DAL
{
    public class AccountTransactionDAL
    {
        LedgerMappingDAL LedgerMappingDAL = new LedgerMappingDAL();
        public enum EnumTransactionTypes { PURCHASE, SALE, PRETURN, SRETURN, PAYMENT, RECIEPT, WALLET, JOURNAL, OPENING, PETTY }
        DocumentSerialDAL documentSerialDAL = new DocumentSerialDAL();
        public void SaveAccountTransaction(account_transaction accountTransaction, betaskdbEntities context)
        {
            try
            {
                if (accountTransaction.credit != 0 || accountTransaction.debit != 0)
                {
                    //var dQuery = context.Database.SqlQuery<DateTime>("SELECT getdate()");
                    //DateTime dbDate = dQuery.AsEnumerable().First();
                    //accountTransaction.added_time = dbDate;
                    context.Entry(accountTransaction).State = EntityState.Added;
                    context.SaveChanges();
                }

            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<account_transaction> GetLatestTransaction(int limit)
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1).Distinct().OrderByDescending(x => x.transaction_number).Take(limit).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listTransactions;
        }

        public List<account_transaction> GetLatestTransactionBySaleId(int saleId)
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.transaction_type == "SALE" && x.transaction_type_id == saleId).Distinct().OrderByDescending(x => x.transaction_number).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listTransactions;
        }

        public List<account_transaction> GetLatestTransactionByVoucher(string voucher)
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.transaction_type == "RECIEPT" && (x.voucher_number == voucher || x.reference_id == voucher)).Distinct().OrderByDescending(x => x.transaction_number).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listTransactions;
        }

        public List<account_transaction> GetLatestTransactionByTransaction(int transaction)
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.transaction_number == transaction).Distinct().OrderByDescending(x => x.transaction_number).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listTransactions;
        }

        public int SaveAccountTransactionList(List<account_transaction> accountTransaction, Enum transactionType, betaskdbEntities context)
        {
            int tranNumber = 0;
            try
            {
                // using (betaskdbEntities context = new betaskdbEntities())
                {
                    //  using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            bool isUpdate = false;
                            DocumentSerialDAL documentSerialDAL = new DocumentSerialDAL();
                            string transactionTypeId = "0";
                            if (accountTransaction[0].transaction_number == 0)
                            {
                                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                                if (transactionType != null)
                                    transactionTypeId = documentSerialDAL.GetNextDocument(transactionType.ToString(), context);

                            }
                            else
                            {
                                isUpdate = true;
                                tranNumber = accountTransaction[0].transaction_number;
                                transactionTypeId = accountTransaction[0].transaction_type_id.ToString();

                                //Journal Delete
                                if (transactionType.Equals(AccountTransactionDAL.EnumTransactionTypes.JOURNAL))
                                    DeleteAccountTransactionsJournal(transactionType, tranNumber, context);
                                //Otherthan journals
                                else
                                    DeleteAccountTransactions(transactionType, Convert.ToInt32(transactionTypeId), context);
                            }
                            if (tranNumber > 0)
                            {
                                var dQuery = context.Database.SqlQuery<DateTime>("SELECT getdate()");
                                DateTime dbDate = dQuery.AsEnumerable().First();

                                foreach (account_transaction tran in accountTransaction)
                                {
                                    tran.transaction_number = tranNumber;
                                    tran.transaction_type_id = Convert.ToInt32(transactionTypeId);
                                    tran.added_time = dbDate;
                                    context.Entry(tran).State = EntityState.Added;
                                    context.SaveChanges();

                                    //Updating cost center
                                    UpdateCostCenterTransaction(tran, context);

                                }
                            }
                            if (!isUpdate)
                            {
                                documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                                documentSerialDAL.UpdateNextDocument(transactionType, context);
                            }
                            //transaction.Commit();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            string error = "";
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    error = ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                                }
                            }
                            throw new Exception(error);
                        }
                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return tranNumber;
        }
        public int SaveAccountTransactionList(List<account_transaction> accountTransaction, Enum transactionType, ref string referance)
        {
            int tranNumber = 0;
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var _days = (accountTransaction[0].transaction_date - DateTime.Now).Days;
                            int allowedDate = context.system_settings.AsNoTracking().FirstOrDefault(x => x.status == 1).allowed_backdate * -1;
                            if (_days < allowedDate)
                            {
                                PrivilegeDAL privileges = new PrivilegeDAL();
                                if (!privileges.IsPriviligeProvided(Constants.UserId, PrivilegeDAL.Privileges.AllowBackDate, context))
                                    throw new Exception("Backdate entry update is not allowed");
                            }

                            int PDCEnabled = context.system_settings.FirstOrDefault(x => x.status == 1).pdc_enable;
                            bool isUpdate = false;
                            DocumentSerialDAL documentSerialDAL = new DocumentSerialDAL();
                            string transactionTypeId = "0";

                            if (accountTransaction[0].transaction_number == 0)
                            {
                                h:
                                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                                if (transactionType != null)
                                    transactionTypeId = documentSerialDAL.GetNextDocument(transactionType.ToString(), context);
                                if (context.account_transaction.Any(x => x.transaction_number == tranNumber))
                                {
                                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                                    goto h;
                                }
                            }
                            else
                            {
                                isUpdate = true;
                                tranNumber = accountTransaction[0].transaction_number;
                                transactionTypeId = accountTransaction[0].transaction_type_id.ToString();

                                //Journal Delete
                                if (transactionType.Equals(AccountTransactionDAL.EnumTransactionTypes.JOURNAL) || transactionType.Equals(AccountTransactionDAL.EnumTransactionTypes.PETTY) || transactionType.Equals(AccountTransactionDAL.EnumTransactionTypes.OPENING))
                                    DeleteAccountTransactionsJournal(transactionType, tranNumber, context);
                                //Otherthan journals
                                else
                                    DeleteAccountTransactions(transactionType, Convert.ToInt32(transactionTypeId), context);

                                DeleteCostCenter(tranNumber, context);
                            }
                            if (tranNumber > 0)
                            {
                                var dQuery = context.Database.SqlQuery<DateTime>("SELECT getdate()");
                                DateTime dbDate = dQuery.AsEnumerable().First();

                                foreach (account_transaction tran in accountTransaction)
                                {
                                    if (!string.IsNullOrEmpty(tran.voucher_number) && PDCEnabled != 1)
                                    {
                                        tran.reconcil_date = DateTime.Now;
                                    }

                                    tran.transaction_number = tranNumber;
                                    tran.transaction_type_id = Convert.ToInt32(transactionTypeId);
                                    tran.added_time = dbDate;
                                    context.Entry(tran).State = EntityState.Added;
                                    context.SaveChanges();

                                    //Updating cost center
                                    UpdateCostCenterTransaction(tran, context);

                                }
                            }
                            if (!isUpdate)
                            {
                                documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                                documentSerialDAL.UpdateNextDocument(transactionType, context);
                            }
                            referance = transactionTypeId;
                            //Updating wallet
                            SynchronizationDAL synchronization = new SynchronizationDAL(context);
                            foreach (account_transaction tran in accountTransaction)
                            {
                                if (context.customer.Any(x => x.ledger_id == tran.ledger_id))
                                {
                                    int custId = context.customer.FirstOrDefault(x => x.ledger_id == tran.ledger_id).customer_id;
                                    decimal walletBal = 0;
                                    synchronization.UpdateWalletAsOutstanding(custId, ref walletBal);
                                }
                            }

                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            string error = "";
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    error = ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                                }
                            }
                            transaction.Rollback();
                            throw new Exception(error);
                        }
                        //catch (Exception ee)
                        //{

                        //    transaction.Rollback();
                        //    throw;

                        //}
                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return tranNumber;
        }

        private void UpdateCostCenterTransaction(account_transaction tran, betaskdbEntities context)
        {
            try
            {
                if (!string.IsNullOrEmpty(tran.reference_id))
                {
                    List<cost_center_transaction> xCost = context.cost_center_transaction.Where(x => x.reference_id == tran.reference_id && (x.status == 3)).ToList();
                    if (xCost != null && xCost.Count > 0)
                    {
                        foreach (cost_center_transaction ct in xCost)
                        {
                            ct.status = 1;
                            ct.ledger_id = tran.ledger_id;
                            ct.transaction_id = tran.account_transaction_id;
                            ct.transaction_number = tran.transaction_number;
                            ct.transaction_date = tran.transaction_date;
                            ct.transaction_type_id = tran.transaction_type_id;
                            context.Entry(ct).State = EntityState.Modified;
                        }
                        //context.cost_center_transaction.RemoveRange(context.cost_center_transaction.Where(x => x.reference_id == tran.reference_id && x.status == 2).ToList());
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void SaveAccountTransactionCheque(EDMX.account_transaction_cheque cheque)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {

                    if (cheque != null)
                    {
                        account_transaction transaction = context.account_transaction.Where(x => x.transaction_number == cheque.account_transaction_number).FirstOrDefault();
                        if (transaction != null)
                        {
                            EDMX.account_transaction_cheque xCheque = context.account_transaction_cheque.Where(x => x.account_transaction_number == cheque.account_transaction_number && x.status == 1).FirstOrDefault();
                            if (xCheque != null)
                            {
                                xCheque.cheque_number = cheque.cheque_number;
                                xCheque.cheque_date = cheque.cheque_date;
                                xCheque.bank = cheque.bank;
                                xCheque.other_details = cheque.other_details;
                                context.Entry(xCheque).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                cheque.account_transaction_id = transaction.account_transaction_id;
                                context.Entry(cheque).State = EntityState.Added;
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        /// <summary>
        /// Posting profit and loss
        /// 1) Get proft ledger by calling from ledger map by 'PROFITLOSS'
        /// 2)IF PROFIT
        ///      PROFIT AND LOSS ACCOUNT CREDIT POST
        /// 3)IF LOSS
        ///      PROFIR AND LOSS ACCOUNT DEBIT POST 
        /// </summary>
        /// <param name="profitloss"></param>
        /// <param name="date"></param>
        public void PostProfitAndLoss(decimal debit, decimal credit, DateTime date)
        {

            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    var xProfit = context.ledger_mapping.Where(x => x.ledger_type == "PROFITLOSS" && x.status == 1).FirstOrDefault();
                    if (xProfit != null)
                    {

                        List<account_transaction> listTransaction = context.account_transaction.Where(x => x.transaction_date == date && x.transaction_type == "PROFITLOSS" && x.status == 1).ToList();
                        if (listTransaction != null)
                        {
                            foreach (account_transaction tr in listTransaction)
                            {
                                tr.status = 2;
                                context.Entry(tr).State = EntityState.Modified;
                            }
                            context.SaveChanges();
                        }

                        account_transaction account = new account_transaction
                        {
                            ledger_id = Convert.ToInt32(xProfit.ledger_id),
                            transaction_date = date,
                            added_time = DateTime.Now,
                            credit = credit,
                            debit = debit,
                            status = 1,
                            transaction_number = 0,
                            narration = $"Profit and loss for the year {date.Year}",
                            reconcil_date = null,
                            transaction_type = "JOURNAL",
                            transaction_type_id = -1,
                            voucher_number = null

                        };
                        context.Entry(account).State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EDMX.account_transaction> PostWallet(WalletAccountPostModel walletAccountPosting, wallet_history wallet, betaskdbEntities context)
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {
                /*
                 * Wallet Recharge
                    Bank/ Cash Dr
                         To customer a/c
                 */

                //Dr
                if (walletAccountPosting.CashAmount > 0)
                {
                    EDMX.account_transaction _tranCash = new EDMX.account_transaction()
                    {
                        ledger_id = walletAccountPosting.CashLedger,
                        transaction_date = wallet.date,
                        transaction_type = EnumTransactionTypes.RECIEPT.ToString(),
                        debit = walletAccountPosting.CashAmount,
                        credit = 0,
                        transaction_type_id = wallet.wallet_history_id,
                        narration = $" Wallet Recharge by Cash aginst Wallet Number {wallet.wallet_number} ",

                    };
                    listAccount.Add(_tranCash);
                }
                else if (walletAccountPosting.BankLedgerAmount > 0)
                {
                    EDMX.account_transaction _tranBank = new EDMX.account_transaction()
                    {
                        ledger_id = walletAccountPosting.BankLedger,
                        transaction_date = wallet.date,
                        transaction_type = EnumTransactionTypes.WALLET.ToString(),
                        debit = walletAccountPosting.BankLedgerAmount,
                        credit = 0,
                        transaction_type_id = wallet.wallet_history_id,
                        narration = $" Wallet Recharge by Bank aginst Wallet Number {wallet.wallet_number} ",

                    };
                    listAccount.Add(_tranBank);
                }

                //Cr
                if (walletAccountPosting.CustomerAmount > 0)
                {
                    //Checking Tax for recharge 31.Jan.2022


                    EDMX.account_transaction _tranCustomer = new EDMX.account_transaction()
                    {
                        ledger_id = walletAccountPosting.CustomerLedger,
                        transaction_date = wallet.date,
                        transaction_type = DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(),
                        debit = 0,
                        credit = walletAccountPosting.CustomerAmount,
                        transaction_type_id = wallet.wallet_history_id,
                        narration = $" Wallet Recharge by Cash aginst Wallet Number {wallet.wallet_number} ",

                    };
                    listAccount.Add(_tranCustomer);


                }

                int tranNumber = 0;
                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);
                if (tranNumber > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        tran.transaction_number = tranNumber;
                        tran.status = 1;
                        SaveAccountTransaction(tran, context);
                    }
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccount;
        }
        public List<EDMX.account_transaction> PostPurchase(PurchaseAccountPostModel purchaseAccountPosting, EDMX.purchase purchase, betaskdbEntities context, List<account_transaction> listMultiplePurchaseAccounts)
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {
                purchaseAccountPosting = LedgerMappingDAL.SetPurchaseAccountLedger(purchaseAccountPosting);
                DeleteAccountTransactions(EnumTransactionTypes.PURCHASE, purchase.purchase_id, context);
                //Creating account posting List
                /*
                 * -- Supplier
                 * Purchase a/c Dr 
                 * VatOnPurchse Dr
                 * Roundoff (eg:15.6 to 16) Dr
                 *  To Discount Recieved cr
                 *  To Roundoff (eg:15.6 to 15) cr
                 *  To Supplier cr
                 *  
                 *  -- if it is cash
                 *  Supplier a/c Dr To
                 *   Cash a/c cr
                 */
                string vendor = context.account_ledger.Find(purchaseAccountPosting.CreditPurchaseLedger).ledger_name;
                decimal totalCR = 0, totalDR = 0;
                //Purchase
                if (listMultiplePurchaseAccounts == null)
                {
                    EDMX.account_transaction _tranPurchase = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.PurchaseLedger,
                        transaction_date = purchase.purchase_date,
                        transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                        debit = purchase.gross_amount,
                        credit = 0,
                        transaction_type_id = purchase.purchase_id,
                        narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} from {vendor} {purchase.invoice_number}",

                    };
                    totalDR += purchase.gross_amount;
                    listAccount.Add(_tranPurchase);
                }
                else
                {
                    foreach (account_transaction _tranSale in listMultiplePurchaseAccounts)
                    {
                        EDMX.account_transaction tran = new EDMX.account_transaction()
                        {
                            ledger_id = _tranSale.ledger_id,
                            debit = _tranSale.debit,
                            credit = _tranSale.credit,
                            narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} from {vendor} {purchase.invoice_number}",

                        };
                        totalDR += tran.debit;
                        listAccount.Add(tran);
                    }
                }

                //VAT
                if (purchase.total_vat != 0)
                {
                    EDMX.account_transaction _tranVat = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.VatOnPurchaseLedger,
                        transaction_date = purchase.purchase_date,
                        transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                        debit = purchase.total_vat,
                        credit = 0,
                        transaction_type_id = purchase.purchase_id,
                        narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} {purchase.invoice_number} "

                    };
                    totalDR += purchase.total_vat;
                    listAccount.Add(_tranVat);
                }


                //Round to upper
                if (purchase.roundup < 0)
                {
                    EDMX.account_transaction _tranRound = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.RoundOffLedger,
                        transaction_date = purchase.purchase_date,
                        transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                        debit = purchase.roundup * -1,
                        credit = 0,
                        transaction_type_id = purchase.purchase_id,
                        narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount}  from {vendor} {purchase.invoice_number} "

                    };
                    totalDR += (purchase.roundup * -1);
                    listAccount.Add(_tranRound);
                }
                else if (purchase.roundup > 0)
                {
                    EDMX.account_transaction _tranRound = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.RoundOffLedger,
                        transaction_date = purchase.purchase_date,
                        transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                        debit = 0,
                        credit = purchase.roundup,
                        transaction_type_id = purchase.purchase_id,
                        narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} from {vendor} {purchase.invoice_number}"

                    };
                    totalCR += purchase.roundup;
                    listAccount.Add(_tranRound);
                }

                //Cr
                //Discount
                if (purchase.total_discount > 0)
                {
                    EDMX.account_transaction _tranDiscount = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.DiscountRecievedLedger,
                        transaction_date = purchase.purchase_date,
                        transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                        debit = 0,
                        credit = purchase.total_discount,
                        transaction_type_id = purchase.purchase_id,
                        narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} from {vendor} {purchase.invoice_number} "

                    };
                    totalCR += purchase.total_discount;
                    listAccount.Add(_tranDiscount);
                }

                EDMX.account_transaction _tranSupplier = new EDMX.account_transaction()
                {
                    ledger_id = purchaseAccountPosting.CreditPurchaseLedger,
                    transaction_date = purchase.purchase_date,
                    transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                    debit = 0,
                    credit = purchase.net_amount,
                    transaction_type_id = purchase.purchase_id,
                    narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} from {vendor} {purchase.invoice_number} "

                };
                totalCR += purchase.net_amount;
                listAccount.Add(_tranSupplier);
                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int tranNumber = 0;
                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                if (tranNumber > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        tran.transaction_number = tranNumber;
                        tran.transaction_date = purchase.purchase_date;
                        tran.transaction_type = EnumTransactionTypes.PURCHASE.ToString();
                        tran.transaction_type_id = purchase.purchase_id;
                        tran.status = 1;
                        // SaveAccountTransaction(tran, context);
                        context.Entry(tran).State = EntityState.Added;

                    }
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                }

                //if Cash/Bank Purchase
                if (purchase.payment_mode.ToLower() == "cash" || purchase.payment_mode.ToLower() == "bank")
                {
                    listAccount.Clear();
                    EDMX.account_transaction _tranSupplierDR = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.CreditPurchaseLedger,
                        transaction_date = purchase.purchase_date,
                        transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                        debit = purchase.net_amount,
                        credit = 0,
                        transaction_type_id = purchase.purchase_id,
                        narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} "

                    };
                    listAccount.Add(_tranSupplierDR);
                    if (purchase.payment_mode.ToLower() == "cash")
                    {
                        EDMX.account_transaction _cash = new EDMX.account_transaction()
                        {
                            ledger_id = purchaseAccountPosting.CashPurchaseLedger,
                            transaction_date = purchase.purchase_date,
                            transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                            debit = 0,
                            credit = purchase.net_amount,
                            transaction_type_id = purchase.purchase_id,
                            narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} "

                        };
                        listAccount.Add(_cash);
                    }
                    else if (purchase.payment_mode.ToLower() == "bank")
                    {
                        EDMX.account_transaction _bank = new EDMX.account_transaction()
                        {
                            ledger_id = purchaseAccountPosting.BankPurchaseLedger,
                            transaction_date = purchase.purchase_date,
                            transaction_type = EnumTransactionTypes.PURCHASE.ToString(),
                            debit = 0,
                            credit = purchase.net_amount,
                            transaction_type_id = purchase.purchase_id,
                            narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} "

                        };
                        listAccount.Add(_bank);
                    }

                    int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);
                    if (tranNumber > 0)
                    {
                        foreach (account_transaction tran in listAccount)
                        {
                            tran.transaction_number = tranNumber;
                            tran.status = 1;
                            //SaveAccountTransaction(tran, context);
                            context.Entry(tran).State = EntityState.Added;
                        }
                        context.SaveChanges();
                        documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);

                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccount;
        }

        public List<EDMX.account_transaction> PostPurchaseReturn(PurchaseAccountPostModel purchaseAccountPosting, EDMX.purchase_return purchase, betaskdbEntities context)
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();
            try
            {
                purchaseAccountPosting = LedgerMappingDAL.SetPurchaseAccountLedger(purchaseAccountPosting);
                DeleteAccountTransactions(EnumTransactionTypes.PRETURN, purchase.purchase_return_id, context);
                //Creating account posting List
                /*
                 * -- Supplier
                 * Discount Recieved Dr
                 * Supplier Dr
                 *   To Purchase a/c cr 
                 *   To VatOnPurchse cr

                 *  
                 *  -- if it is cash
                 *  Cash a/c Dr
                 *    To Supplier a/c cr
                 * 
                 */
                decimal totalCR = 0, totalDR = 0;
                string tranType = EnumTransactionTypes.PRETURN.ToString();
                int tranTypeId = purchase.purchase_return_id;
                DateTime tranDate = purchase.purchase_date;
                //Purchase
                EDMX.account_transaction _tranPurchase = new EDMX.account_transaction()
                {
                    ledger_id = purchaseAccountPosting.PurchaseLedger,
                    debit = 0,
                    credit = purchase.gross_amount,
                    narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} ",

                };
                totalDR += purchase.gross_amount;
                listAccount.Add(_tranPurchase);

                //VAT
                if (purchase.total_vat != 0)
                {
                    EDMX.account_transaction _tranVat = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.VatOnPurchaseLedger,
                        debit = 0,
                        credit = purchase.total_vat,
                        narration = $" {purchase.payment_mode} Purchase Return of {purchase.net_amount} "

                    };
                    totalDR += purchase.total_vat;
                    listAccount.Add(_tranVat);
                }


                //Dr
                //Discount
                if (purchase.total_discount > 0)
                {
                    EDMX.account_transaction _tranDiscount = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.DiscountRecievedLedger,
                        debit = purchase.total_discount,
                        credit = 0,
                        narration = $" {purchase.payment_mode} Purchase Return of {purchase.net_amount} "

                    };
                    totalCR += purchase.total_discount;
                    listAccount.Add(_tranDiscount);
                }

                EDMX.account_transaction _tranSupplier = new EDMX.account_transaction()
                {
                    ledger_id = purchaseAccountPosting.CreditPurchaseLedger,
                    debit = purchase.net_amount,
                    credit = 0,
                    narration = $" {purchase.payment_mode} Purchase Return of {purchase.net_amount} "

                };
                totalCR += purchase.net_amount;
                listAccount.Add(_tranSupplier);
                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int tranNumber = 0;
                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                if (tranNumber > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        tran.transaction_number = tranNumber;
                        tran.transaction_type = tranType;
                        tran.transaction_type_id = tranTypeId;
                        tran.transaction_date = tranDate;
                        tran.status = 1;
                        SaveAccountTransaction(tran, context);
                    }
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                }

                //if Cash/Bank Purchase
                if (purchase.payment_mode.ToLower() == "cash" || purchase.payment_mode.ToLower() == "bank")
                {
                    listAccount.Clear();
                    EDMX.account_transaction _tranSupplierDR = new EDMX.account_transaction()
                    {
                        ledger_id = purchaseAccountPosting.CreditPurchaseLedger,
                        debit = 0,
                        credit = purchase.net_amount,
                        narration = $" {purchase.payment_mode} Purchase Return {purchase.net_amount} "

                    };
                    listAccount.Add(_tranSupplierDR);
                    if (purchase.payment_mode.ToLower() == "cash")
                    {
                        EDMX.account_transaction _cash = new EDMX.account_transaction()
                        {
                            ledger_id = purchaseAccountPosting.CashPurchaseLedger,
                            debit = purchase.net_amount,
                            credit = 0,
                            narration = $" {purchase.payment_mode} Purchase Return {purchase.net_amount} "

                        };
                        listAccount.Add(_cash);
                    }
                    else if (purchase.payment_mode.ToLower() == "bank")
                    {
                        EDMX.account_transaction _bank = new EDMX.account_transaction()
                        {
                            ledger_id = purchaseAccountPosting.BankPurchaseLedger,
                            debit = purchase.net_amount,
                            credit = 0,
                            narration = $" {purchase.payment_mode} Purchase of {purchase.net_amount} "

                        };
                        listAccount.Add(_bank);
                    }

                    int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);
                    if (tranNumber > 0)
                    {
                        foreach (account_transaction tran in listAccount)
                        {
                            tran.transaction_number = tranNumber;
                            tran.transaction_type = tranType;
                            tran.transaction_type_id = tranTypeId;
                            tran.transaction_date = tranDate;
                            tran.status = 1;
                            SaveAccountTransaction(tran, context);
                        }
                        documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);

                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccount;
        }

        public List<EDMX.account_transaction> PostSale(SaleAccountPostModel saleAccountPosting, EDMX.sales sale, betaskdbEntities context, List<account_transaction> listMultipleSaleAccounts = null)
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();

            try
            {

                saleAccountPosting = LedgerMappingDAL.SetSaleAccountLedger(saleAccountPosting);
                DeleteAccountTransactions(EnumTransactionTypes.SALE, sale.sales_id, context);
                //Creating account posting List
                /*
                 * -- Supplier
                 * Debtors/Customer a/c Dr 
                 * Roundoff (eg:15.6 to 15) Dr
                 * Discount Allowed Dr
                 *   To Sale a/c cr
                 *   To Roundoff (eg:15.6 to 16) cr
                 *  
                 *  -- if it is cash
                 *  Cash a/c Dr To
                 *   Debtors/Customer a/c cr
                 */
                decimal totalCR = 0, totalDR = 0;
                string customer = sale.customer != null ? sale.customer.customer_name : "";

                //Getting division
                string division = "";
                if (sale.division_id != null && sale.division_id != 0)
                    division = context.customer_division.AsNoTracking().FirstOrDefault(x => x.division_id == sale.division_id).division_name;

                string _narration = sale.payment_mode.ToLower() == "salesmancredit" ? customer : "" + $"{division} {sale.payment_mode} Sale of {sale.net_amount} {(sale.old_leaf_count > 0 ? "-OldLeaf" : "")} - {sale.remarks.Replace("Auto synched ", "")}".TrimStart();

                string tranType = EnumTransactionTypes.SALE.ToString();
                string voucherNumber = sale.payment_mode.ToLower();
                long tranTypeId = sale.sales_id;
                DateTime tranDate = sale.sales_date;
                if (sale.net_amount != (sale.gross_amount + sale.roundup + sale.total_vat))
                    sale.net_amount = (sale.gross_amount + sale.roundup + sale.total_vat);
                //Sale
                EDMX.account_transaction _tranCusomer = new EDMX.account_transaction()
                {
                    ledger_id = saleAccountPosting.CreditSaleLedger,
                    debit = sale.net_amount,
                    credit = 0,
                    narration = _narration,

                };
                totalDR += Convert.ToDecimal(sale.net_amount);
                listAccount.Add(_tranCusomer);

                //Discount
                if (sale.total_discount > 0)
                {
                    EDMX.account_transaction _tranDiscount = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.DiscountAllowedLedger,
                        debit = sale.total_discount,
                        credit = 0,
                        narration = _narration

                    };
                    totalDR += sale.total_discount;
                    listAccount.Add(_tranDiscount);
                }

                //Round to upper
                if (sale.roundup < 0)
                {
                    EDMX.account_transaction _tranRound = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.RoundOffLedger,
                        debit = 0,
                        credit = sale.roundup * -1,
                        narration = _narration

                    };
                    totalCR += (sale.roundup * -1);
                    listAccount.Add(_tranRound);
                }
                else if (sale.roundup > 0)
                {
                    EDMX.account_transaction _tranRound = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.RoundOffLedger,
                        debit = sale.roundup,
                        credit = 0,
                        narration = _narration

                    };
                    totalDR += (sale.roundup);
                    listAccount.Add(_tranRound);
                }


                //Cr
                //VAT
                if (sale.total_vat != 0)
                {
                    EDMX.account_transaction _tranVat = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.VatOnSaleLedger,
                        debit = 0,
                        credit = sale.total_vat,
                        narration = _narration

                    };
                    totalCR += sale.total_vat;
                    listAccount.Add(_tranVat);
                }
                if (listMultipleSaleAccounts == null)
                {
                    EDMX.account_transaction _tranSale = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.SalesLedger,
                        debit = 0,
                        credit = sale.gross_amount,
                        narration = _narration

                    };
                    totalCR += sale.gross_amount;
                    listAccount.Add(_tranSale);
                }
                else
                {
                    foreach (account_transaction _tranSale in listMultipleSaleAccounts)
                    {
                        EDMX.account_transaction tran = new EDMX.account_transaction()
                        {
                            ledger_id = _tranSale.ledger_id,
                            debit = 0,
                            credit = _tranSale.credit,
                            narration = _narration

                        };
                        totalCR += _tranSale.credit;
                        listAccount.Add(tran);
                    }
                }

                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int tranNumber = 0;
                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                if (tranNumber > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        tran.transaction_number = tranNumber;
                        tran.transaction_type = tranType;
                        tran.transaction_type_id = tranTypeId;
                        tran.transaction_date = tranDate;
                        tran.status = 1;
                        tran.route_id = sale.customer.route_id;
                        tran.voucher_number = voucherNumber;
                        SaveAccountTransaction(tran, context);
                    }
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                }

                //if Cash/Bank Purchase
                _narration = $"Collecting Amount from Customer by {sale.payment_mode}. Sale of {sale.net_amount}. {sale.remarks.Replace("Auto synched ", "")}";
                if (sale.payment_mode.ToLower() == "cash" || sale.payment_mode.ToLower() == "bank")
                {
                    listAccount.Clear();
                    if (sale.payment_mode.ToLower() == "cash")
                    {
                        EDMX.account_transaction _cash = new EDMX.account_transaction()
                        {
                            ledger_id = saleAccountPosting.CashSaleLedger,
                            debit = saleAccountPosting.CashBalance == 0 ? sale.net_amount : sale.net_amount - saleAccountPosting.CashBalance,
                            credit = 0,
                            narration = _narration

                        };
                        listAccount.Add(_cash);
                    }
                    else if (sale.payment_mode.ToLower() == "bank")
                    {
                        EDMX.account_transaction _bank = new EDMX.account_transaction()
                        {
                            ledger_id = saleAccountPosting.BankSaleLedger,
                            debit = sale.net_amount,
                            credit = 0,
                            narration = _narration

                        };
                        listAccount.Add(_bank);
                    }
                    EDMX.account_transaction _traCustomerCR = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.CreditSaleLedger,
                        debit = 0,
                        //credit = sale.net_amount,
                        credit = saleAccountPosting.CashBalance == 0 ? sale.net_amount : sale.net_amount - saleAccountPosting.CashBalance,
                        narration = _narration

                    };
                    listAccount.Add(_traCustomerCR);


                    int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);
                    if (tranNumber > 0)
                    {
                        foreach (account_transaction tran in listAccount)
                        {
                            tran.transaction_number = tranNumber;
                            tran.transaction_type = tranType;
                            tran.transaction_type_id = tranTypeId;
                            tran.transaction_date = tranDate;
                            tran.status = 1;
                            tran.route_id = sale.customer.route_id;
                            SaveAccountTransaction(tran, context);
                        }
                        documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);

                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccount;
        }

        public List<EDMX.account_transaction> PostCashBankSale(SaleAccountPostModel saleAccountPosting, EDMX.sales sale, betaskdbEntities context, List<account_transaction> listMultipleSaleAccounts = null)
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();

            try
            {
                saleAccountPosting = LedgerMappingDAL.SetSaleAccountLedger(saleAccountPosting);
                DeleteAccountTransactions(EnumTransactionTypes.SALE, sale.sales_id, context);
                //Creating account posting List
                /*
                 * -- Supplier
                 * Debtors/Customer a/c Dr 
                 * Roundoff (eg:15.6 to 15) Dr
                 * Discount Allowed Dr
                 *   To Sale a/c cr
                 *   To Roundoff (eg:15.6 to 16) cr
                 *  
                 *  -- if it is cash
                 *  Cash a/c Dr To
                 *   Debtors/Customer a/c cr
                 */
                decimal totalCR = 0, totalDR = 0;
                string customer = sale.customer != null ? sale.customer.customer_name : "";
                string _narration = $"{sale.payment_mode} Sale of {sale.net_amount}  - {sale.remarks.Replace("Auto synched ", "")}";
                string tranType = EnumTransactionTypes.SALE.ToString();
                string voucherNumber = sale.payment_mode.ToLower();
                long tranTypeId = sale.sales_id;
                DateTime tranDate = sale.sales_date;
                //Sale
                EDMX.account_transaction _tranCusomer = new EDMX.account_transaction()
                {
                    ledger_id = saleAccountPosting.CashSaleLedger,
                    debit = sale.net_amount,
                    credit = 0,
                    narration = _narration,

                };
                totalDR += Convert.ToDecimal(sale.net_amount);
                listAccount.Add(_tranCusomer);

                //Discount
                if (sale.total_discount > 0)
                {
                    EDMX.account_transaction _tranDiscount = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.DiscountAllowedLedger,
                        debit = sale.total_discount,
                        credit = 0,
                        narration = _narration

                    };
                    totalDR += sale.total_discount;
                    listAccount.Add(_tranDiscount);
                }

                //Round to upper
                if (sale.roundup < 0)
                {
                    EDMX.account_transaction _tranRound = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.RoundOffLedger,
                        debit = 0,
                        credit = sale.roundup * -1,
                        narration = _narration

                    };
                    totalCR += (sale.roundup * -1);
                    listAccount.Add(_tranRound);
                }
                else if (sale.roundup > 0)
                {
                    EDMX.account_transaction _tranRound = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.RoundOffLedger,
                        debit = sale.roundup,
                        credit = 0,
                        narration = _narration

                    };
                    totalDR += (sale.roundup);
                    listAccount.Add(_tranRound);
                }


                //Cr
                //VAT
                if (sale.total_vat != 0)
                {
                    EDMX.account_transaction _tranVat = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.VatOnSaleLedger,
                        debit = 0,
                        credit = sale.total_vat,
                        narration = _narration

                    };
                    totalCR += sale.total_vat;
                    listAccount.Add(_tranVat);
                }

                if (listMultipleSaleAccounts == null)
                {
                    EDMX.account_transaction _tranSale = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.SalesLedger,
                        debit = 0,
                        credit = sale.gross_amount,
                        narration = _narration

                    };
                    totalCR += sale.gross_amount;
                    listAccount.Add(_tranSale);

                }
                else
                {
                    foreach (account_transaction _tranSale in listMultipleSaleAccounts)
                    {
                        EDMX.account_transaction tran = new EDMX.account_transaction()
                        {
                            ledger_id = _tranSale.ledger_id,
                            debit = 0,
                            credit = _tranSale.credit,
                            narration = _narration

                        };
                        totalCR += _tranSale.credit;
                        listAccount.Add(tran);
                    }
                }
                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int tranNumber = 0;
                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                if (tranNumber > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        tran.transaction_number = tranNumber;
                        tran.transaction_type = tranType;
                        tran.transaction_type_id = tranTypeId;
                        tran.transaction_date = tranDate;
                        tran.status = 1;
                        tran.voucher_number = voucherNumber;
                        tran.route_id = sale.route_id == null ? sale.customer.route_id : sale.route_id;
                        SaveAccountTransaction(tran, context);
                    }
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                }


            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccount;
        }

        public List<EDMX.account_transaction> PostSaleReturn(SaleAccountPostModel saleAccountPosting, EDMX.sales_return sale, betaskdbEntities context)
        {
            List<EDMX.account_transaction> listAccount = new List<EDMX.account_transaction>();

            try
            {
                saleAccountPosting = LedgerMappingDAL.SetSaleAccountLedger(saleAccountPosting);
                DeleteAccountTransactions(EnumTransactionTypes.SRETURN, sale.sales_return_id, context);


                //Creating account posting List
                /*
                 * -- Supplier
                 * Sale A/c Dr To
                 * VAT a/c Dr
                 *    Debtors/Customer a/c cr 
                 *    Discount Allowed cr
                 *  
                 *  -- if it is cash
                 *  
                 *  Debtors/Customer a/c Dr
                 *    To Cash a/c 
                 */
                decimal totalCR = 0, totalDR = 0;
                string _narration = $" {sale.payment_mode} Sale of {sale.net_amount} ";
                string tranType = EnumTransactionTypes.SRETURN.ToString();
                int tranTypeId = sale.sales_return_id;
                DateTime tranDate = sale.sales_date;
                //Purchase
                EDMX.account_transaction _tranCusomer = new EDMX.account_transaction()
                {
                    ledger_id = saleAccountPosting.CreditSaleLedger,
                    debit = 0,
                    credit = sale.net_amount,
                    narration = $" {sale.payment_mode} Sale return {sale.net_amount} ",

                };
                totalDR += Convert.ToDecimal(sale.net_amount);
                listAccount.Add(_tranCusomer);

                //Discount
                if (sale.total_discount > 0)
                {
                    EDMX.account_transaction _tranDiscount = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.DiscountAllowedLedger,
                        debit = 0,
                        credit = sale.total_discount,
                        narration = _narration

                    };
                    totalDR += sale.total_discount;
                    listAccount.Add(_tranDiscount);
                }


                //Dr
                //VAT
                if (sale.total_vat != 0)
                {
                    EDMX.account_transaction _tranVat = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.VatOnSaleLedger,
                        debit = sale.total_vat,
                        credit = 0,
                        narration = _narration

                    };
                    totalCR += sale.total_vat;
                    listAccount.Add(_tranVat);
                }

                EDMX.account_transaction _tranSale = new EDMX.account_transaction()
                {
                    ledger_id = saleAccountPosting.SalesLedger,
                    debit = sale.gross_amount,
                    credit = 0,
                    narration = _narration

                };
                totalCR += sale.gross_amount;
                listAccount.Add(_tranSale);
                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int tranNumber = 0;
                int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);


                if (tranNumber > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        tran.transaction_number = tranNumber;
                        tran.transaction_type = tranType;
                        tran.transaction_type_id = tranTypeId;
                        tran.transaction_date = tranDate;
                        tran.status = 1;
                        tran.route_id = sale.customer.route_id;
                        SaveAccountTransaction(tran, context);
                    }
                    documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);
                }

                //if Cash/Bank 
                if (sale.payment_mode.ToLower() == "cash" || sale.payment_mode.ToLower() == "bank")
                {
                    listAccount.Clear();
                    if (sale.payment_mode.ToLower() == "cash")
                    {
                        EDMX.account_transaction _cash = new EDMX.account_transaction()
                        {
                            ledger_id = saleAccountPosting.CashSaleLedger,
                            debit = 0,
                            credit = sale.net_amount,
                            narration = $"Collecting Amount frm Customer by {sale.payment_mode} Sale of {sale.net_amount} "

                        };
                        listAccount.Add(_cash);
                    }
                    else if (sale.payment_mode.ToLower() == "bank")
                    {
                        EDMX.account_transaction _bank = new EDMX.account_transaction()
                        {
                            ledger_id = saleAccountPosting.BankSaleLedger,
                            debit = 0,
                            credit = sale.net_amount,
                            transaction_type_id = tranTypeId,
                            narration = $"Collecting Amount frm Customer by {sale.payment_mode} Sale of {sale.net_amount} "

                        };
                        listAccount.Add(_bank);
                    }
                    EDMX.account_transaction _traCustomerCR = new EDMX.account_transaction()
                    {
                        ledger_id = saleAccountPosting.CreditSaleLedger,
                        debit = sale.net_amount,
                        credit = 0,
                        transaction_type_id = tranTypeId,
                        narration = $"Collecting Amount frm Customer by {sale.payment_mode} Sale of {sale.net_amount} "

                    };
                    listAccount.Add(_traCustomerCR);


                    int.TryParse(documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT.ToString(), context), out tranNumber);
                    if (tranNumber > 0)
                    {
                        foreach (account_transaction tran in listAccount)
                        {
                            tran.transaction_number = tranNumber;
                            tran.transaction_type = tranType;
                            tran.transaction_type_id = tranTypeId;
                            tran.transaction_date = tranDate;
                            tran.status = 1;
                            SaveAccountTransaction(tran, context);
                        }
                        documentSerialDAL.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.ACCOUNT, context);

                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccount;
        }

        public void DeleteAccountTransactions(Enum tranType, long tranId, betaskdbEntities context)
        {
            try
            {
                string _tranType = tranType.ToString();

                var xAccounts = context.account_transaction.Where(f => f.transaction_type == _tranType && f.transaction_type_id == tranId).ToList();
                if (xAccounts != null && xAccounts.Count > 0)
                {
                    xAccounts.ForEach(a => a.status = 2);
                    //context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void DeleteAccountTransactionsJournal(Enum tranType, int tranNumber, betaskdbEntities context)
        {
            try
            {
                string _tranType = tranType.ToString();

                var xAccounts = context.account_transaction.Where(f => f.transaction_type == _tranType && f.transaction_number == tranNumber).ToList();
                xAccounts.ForEach(a => a.status = 2);
                context.SaveChanges();

            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void DeleteCostCenter(int tranNumber, betaskdbEntities context)
        {
            try
            {

                var xAccounts = context.cost_center_transaction.Where(f => f.transaction_number == tranNumber && f.status == 1).ToList();
                xAccounts.ForEach(a => a.status = 3);
                context.SaveChanges();

            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<account_transaction> SearchTransaction(DateTime dateFrom, DateTime dateTo, Enum tranType, int ledgerId = 0, string search = "")
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    //listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo)).Distinct().OrderByDescending(x => x.transaction_number).OrderBy(x => x.account_transaction_id).ToList();

                    if (tranType != null)
                    {
                        switch (tranType)
                        {
                            case EnumTransactionTypes.PAYMENT:
                                //listTransactions = listTransactions.Where(x => x.transaction_type == EnumTransactionTypes.PAYMENT.ToString() && x.debit > 0).ToList();
                                listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.transaction_type == EnumTransactionTypes.PAYMENT.ToString() && x.debit > 0).Distinct().OrderByDescending(x => x.transaction_number).OrderBy(x => x.account_transaction_id).ToList();
                                break;
                            case EnumTransactionTypes.RECIEPT:
                                //listTransactions = listTransactions.Where(x => ((x.transaction_type == EnumTransactionTypes.RECIEPT.ToString()) || (x.transaction_type == EnumTransactionTypes.WALLET.ToString())) && x.credit > 0).ToList();
                                listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && ((x.transaction_type == EnumTransactionTypes.RECIEPT.ToString()) || (x.transaction_type == EnumTransactionTypes.WALLET.ToString())) && x.credit > 0).Distinct().OrderByDescending(x => x.transaction_number).OrderBy(x => x.account_transaction_id).ToList();
                                break;
                            case EnumTransactionTypes.JOURNAL:
                                //listTransactions = listTransactions.Where(x => x.transaction_type == EnumTransactionTypes.JOURNAL.ToString() && (x.debit > 0 || x.debit < 0) || (x.credit > 0 || x.credit < 0)).ToList();
                                //listTransactions = listTransactions.Where(x => x.transaction_type == EnumTransactionTypes.JOURNAL.ToString()).ToList();
                                listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.transaction_type == EnumTransactionTypes.JOURNAL.ToString()).Distinct().OrderByDescending(x => x.transaction_number).OrderBy(x => x.account_transaction_id).ToList();
                                break;
                            case EnumTransactionTypes.PETTY:
                                //listTransactions = listTransactions.Where(x => x.transaction_type == EnumTransactionTypes.JOURNAL.ToString() && (x.debit > 0 || x.debit < 0) || (x.credit > 0 || x.credit < 0)).ToList();
                                //listTransactions = listTransactions.Where(x => (x.transaction_type == EnumTransactionTypes.PETTY.ToString() || x.voucher_number == "PETTY") && x.debit > 0).ToList();
                                listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && (x.transaction_type == EnumTransactionTypes.PETTY.ToString() || x.voucher_number == "PETTY") && x.debit > 0).Distinct().OrderByDescending(x => x.transaction_number).OrderBy(x => x.account_transaction_id).ToList();
                                break;
                            case EnumTransactionTypes.OPENING:
                                listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && (x.transaction_type == EnumTransactionTypes.OPENING.ToString()) && (x.debit > 0 || x.credit > 0)).Distinct().OrderByDescending(x => x.transaction_number).OrderBy(x => x.account_transaction_id).ToList();
                                break;
                        }
                        if (listTransactions.Count > 0 && ledgerId > 0)
                        {
                            listTransactions = listTransactions.Where(x => x.ledger_id == ledgerId).ToList();
                        }
                        if (search != "")
                        {
                            listTransactions = listTransactions.Where(x => x.account_ledger.ledger_name.Contains(search) || x.transaction_number.ToString() == search || x.narration.Contains(search) || (x.debit.ToString().Contains(search) || x.credit.ToString().Contains(search)) || x.transaction_type_id.ToString().Contains(search)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listTransactions;
        }
        public EDMX.account_transaction_cheque GetCheque(int transactionNumber)
        {
            account_transaction_cheque cheque = new account_transaction_cheque();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    cheque = context.account_transaction_cheque.Where(x => x.account_transaction_number == transactionNumber).FirstOrDefault();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return cheque;
        }
        public List<account_transaction> GetAccountTransactionDetail(int transactionNumber, string transactionType = "")
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.transaction_number == transactionNumber && x.status == 1 && (transactionType != "" ? x.transaction_type == transactionType : x.transaction_type != null)).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listTransactions;
        }

        public DebitCreditModel GetAccountOpenings(DateTime dateFrom, int ledgerId, SqlConnection connection)
        {
            DebitCreditModel debitCreditModel = new DebitCreditModel { };
            using (SqlCommand command = new SqlCommand("SP_GetAccountOpenings", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@dateFrom", dateFrom);
                command.Parameters.AddWithValue("@ledgerId", ledgerId);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Assuming the stored procedure always returns a row
                        decimal debit = reader.GetDecimal(reader.GetOrdinal("debit"));
                        decimal credit = reader.GetDecimal(reader.GetOrdinal("credit"));

                        debitCreditModel.Debit = debit;
                        debitCreditModel.Credit = credit;
                    }

                }
            }
            return debitCreditModel;
        }


        public DataTable GetAccountTransactionStatement(DateTime dateFrom, DateTime dateTo, out decimal openingDebit, out decimal openingCredit, int ledgerId = 0, bool hideNonReconsiled = true, bool includeCostCneter = false, int primaryCostCenter = 0, int costCenter = 0, string search = "", int groupId = 0)
        {
            DataTable tblStatement = new DataTable();

            try
            {

                using (var context = new betaskdbEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        var openings = GetAccountOpenings(dateFrom, ledgerId, conn);
                        openingDebit = openings.Debit; openingCredit = openings.Credit;
                        using (SqlCommand command = new SqlCommand("SP_GetAccountStatment", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            // Add parameters
                            command.Parameters.AddWithValue("@dateFrom", dateFrom);
                            command.Parameters.AddWithValue("@dateTo", dateTo);
                            command.Parameters.AddWithValue("@ledgerId", ledgerId);
                            command.Parameters.AddWithValue("@search", search);
                            command.Parameters.AddWithValue("@groupId", groupId);

                            using (SqlDataAdapter adr = new SqlDataAdapter(command))
                            {
                                adr.Fill(tblStatement);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return tblStatement;
        }

        public List<account_transaction> GetAccountTransactionStatementOl(DateTime dateFrom, DateTime dateTo, out decimal openingDebit, out decimal openingCredit, int ledgerId = 0, bool hideNonReconsiled = true, bool includeCostCneter = false, int primaryCostCenter = 0, int costCenter = 0, string search = "")
        {
            openingDebit = 0; openingCredit = 0;
            List<account_transaction> listTransactions = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    // List<account_transaction> xOpening = context.account_transaction.Where(x => (x.transaction_date < dateFrom) && x.status == 1 && x.ledger_id == ledgerId).ToList();
                    // List<account_transaction>  = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.transaction_date < dateFrom && (x.debit != 0 || x.credit != 0) && (ledgerId > 0 ? x.ledger_id == ledgerId : x.ledger_id > 0)).OrderBy(d => d.transaction_date).ThenBy(t => t.transaction_type_id).ToList();

                    List<account_transaction> xOpening = context.account_transaction.AsNoTracking()
     .Where(x => x.status == 1 && x.transaction_date < dateFrom &&
            (x.debit != 0 || x.credit != 0) && (ledgerId <= 0 || x.ledger_id == ledgerId))
     .OrderBy(d => d.transaction_date)
     .ThenBy(t => t.transaction_type_id)
     .Select(x => new
     {
         x.transaction_date,
         x.transaction_type_id,
         x.account_transaction_id,
         x.debit,
         x.credit,
         x.transaction_number,
         x.reconcil_date
     })
     .ToList()
     .Select(x => new account_transaction
     {
         transaction_date = x.transaction_date,
         transaction_type_id = x.transaction_type_id,
         account_transaction_id = x.account_transaction_id,
         debit = x.debit,
         credit = x.credit,
         transaction_number = x.transaction_number,
         reconcil_date = x.reconcil_date
         // Set other properties as needed
     })
     .ToList();


                    if (includeCostCneter)
                    {
                        if (costCenter > 0)
                        {
                            List<long> transactionIds = xOpening.Select(x => x.account_transaction_id).ToList();
                            List<long> costCenterTransactions = context.cost_center_transaction.Where(x => x.cost_center_id == costCenter && transactionIds.Contains((int)x.transaction_id) && x.transaction_id != null && x.status == 1).Select(x => x.transaction_id.Value).ToList();
                            xOpening = xOpening.Where(x => costCenterTransactions.Contains(x.account_transaction_id)).ToList();
                        }
                    }
                    if (xOpening != null)
                    {
                        openingDebit = xOpening.Sum(x => x.debit);
                        openingCredit = xOpening.Sum(x => x.credit);
                    }

                    // listTransactions = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && (x.debit != 0 || x.credit != 0) && (ledgerId>0?x.ledger_id==ledgerId:x.ledger_id>0)).OrderBy(d => d.transaction_date ).ThenBy(t => t.transaction_type_id).ToList();

                    listTransactions = context.account_transaction
    .Where(x =>
        x.status == 1 &&
        x.transaction_date >= dateFrom &&
        x.transaction_date <= dateTo &&
        (x.debit != 0 || x.credit != 0) &&
        (ledgerId <= 0 || x.ledger_id == ledgerId)
    )
    .OrderBy(d => d.transaction_date)
    .ThenBy(t => t.transaction_type_id)
    .Select(x => new
    {
        x.transaction_date,
        x.transaction_type_id,
        x.account_transaction_id,
        x.debit,
        x.credit,
        x.transaction_number,
        x.reconcil_date,
        x.narration,
        x.account_ledger.ledger_name,
        x.transaction_type
    })
     .ToList()
     .Select(x => new account_transaction
     {
         transaction_date = x.transaction_date,
         transaction_type_id = x.transaction_type_id,
         account_transaction_id = x.account_transaction_id,
         debit = x.debit,
         credit = x.credit,
         transaction_number = x.transaction_number,
         reconcil_date = x.reconcil_date,
         narration = x.narration,
         transaction_type = x.transaction_type,
         account_ledger = new account_ledger
         {
             ledger_name = x.ledger_name,

         }
         // Set other properties as needed
     })
     .ToList();

                    if (includeCostCneter)
                    {
                        if (costCenter > 0)
                        {
                            List<long> transactionIds = listTransactions.Select(x => x.account_transaction_id).ToList();
                            List<long> costCenterTransactions = context.cost_center_transaction.Where(x => x.cost_center_id == costCenter && transactionIds.Contains((int)x.transaction_id) && x.transaction_id != null && x.status == 1).Select(x => x.transaction_id.Value).ToList();
                            listTransactions = listTransactions.Where(x => costCenterTransactions.Contains(x.account_transaction_id)).ToList();
                        }
                    }

                    if (ledgerId > 0)
                    {
                        //listTransactions = listTransactions.Where(x => x.ledger_id == ledgerId).ToList();


                        //Checking ledger is bank a bank . ony reconciled data would show
                        //LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                        //int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                        List<EDMX.account_transaction> transRemove = new List<account_transaction>();
                        //if (context.account_ledger.Where(x => x.ledger_id == ledgerId).FirstOrDefault().group_id == groupId)
                        if (hideNonReconsiled)
                        {

                            foreach (account_transaction acc in listTransactions)
                            {
                                EDMX.account_transaction_cheque cheque = context.account_transaction_cheque.AsNoTracking().Where(x => x.account_transaction_number == acc.transaction_number && x.status == 1).FirstOrDefault();
                                if (cheque != null)
                                {
                                    if (acc.reconcil_date == null)
                                    {
                                        transRemove.Add(acc);
                                    }
                                }
                            }

                            if (transRemove != null && transRemove.Count > 0)
                            {
                                foreach (account_transaction dAcc in transRemove)
                                    listTransactions.Remove(dAcc);
                            }
                            //listTransactions = listTransactions.Where(x => x.reconcil_date != null).ToList();
                        }
                        else
                        {
                            foreach (account_transaction acc in listTransactions)
                            {
                                EDMX.account_transaction_cheque cheque = context.account_transaction_cheque.AsNoTracking().Where(x => x.account_transaction_number == acc.transaction_number && x.status == 1).FirstOrDefault();
                                if (cheque != null)
                                {
                                    if (acc.reconcil_date == null)
                                    {
                                        acc.voucher_number = acc.voucher_number + "*";
                                    }
                                }
                            }


                        }

                        //Checking previous in reconsiled or not
                        if (xOpening != null)
                        {
                            if (hideNonReconsiled)
                            {
                                transRemove = new List<account_transaction>();
                                //xOpening = context.account_transaction.Where(x => (x.transaction_date < dateFrom) && x.status == 1 && x.ledger_id == ledgerId && x.reconcil_date != null).ToList();

                                List<int> chequeIds = context.account_transaction_cheque.Include(a => a.account_transaction).Where(x => x.account_transaction.transaction_date < dateFrom).Select(x => x.account_transaction_number).ToList();
                                if (chequeIds != null && chequeIds.Count > 0)
                                {
                                    foreach (int cn in chequeIds)
                                    {
                                        account_transaction acc = xOpening.Where(x => x.transaction_number == cn && x.reconcil_date == null).FirstOrDefault();
                                        if (acc != null)
                                            transRemove.Add(acc);
                                    }
                                }

                                if (transRemove != null && transRemove.Count > 0)
                                {
                                    foreach (account_transaction dAcc in transRemove)
                                        xOpening.Remove(dAcc);
                                }
                            }
                            else
                            {
                                List<int> chequeIds = context.account_transaction_cheque.Include(a => a.account_transaction).Where(x => x.account_transaction.transaction_date < dateFrom).Select(x => x.account_transaction_number).ToList();
                                if (chequeIds != null && chequeIds.Count > 0)
                                {
                                    foreach (int cn in chequeIds)
                                    {
                                        account_transaction acc = xOpening.Where(x => x.transaction_number == cn && x.reconcil_date == null).FirstOrDefault();
                                        if (acc != null)
                                            acc.voucher_number = acc.voucher_number + "*";
                                    }
                                }
                            }
                            openingDebit = xOpening.Sum(x => x.debit);
                            openingCredit = xOpening.Sum(x => x.credit);


                        }

                        if (search != "")
                        {
                            listTransactions = listTransactions.Where(x => x.account_ledger.ledger_name.Contains(search) || x.transaction_number.ToString() == search || x.narration.Contains(search) || (x.debit.ToString().Contains(search) || x.credit.ToString().Contains(search))).ToList();
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return listTransactions;
        }


        public List<account_transaction> GetPaymentRecieptReport(int ledger, DateTime dateFrom, DateTime dateTo)
        {
            List<account_transaction> listAccountTransaction = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listAccountTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && (x.transaction_type == AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString() || x.transaction_type == AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString() || x.transaction_type == AccountTransactionDAL.EnumTransactionTypes.WALLET.ToString())).OrderBy(x => x.account_transaction_id).ToList();
                    if (ledger > 0)
                    {
                        listAccountTransaction = listAccountTransaction.Where(x => x.ledger_id == ledger).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountTransaction;
        }

        public List<DaybookModel> Daybook(DateTime date)
        {
            List<DaybookModel> listDaybook = new List<DaybookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    //var bDaybook = context.account_transaction.Include(x => x.account_ledger).Where(x => x.transaction_date == date && x.status == 1).ToList();
                    //var _bDaybook = bDaybook.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    var bDaybook = context.account_transaction.Include(x => x.account_ledger).Include(a => a.account_ledger.account_group).Where(x => x.transaction_date == date && x.status == 1).ToList();
                    var _bDaybook = bDaybook.GroupBy(x => x.account_ledger.account_group.group_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    //  string[] propertyNames = _bDaybook.GetType().GetProperties().Select(p => p.Name).ToArray();
                    foreach (var prop in _bDaybook)
                    {
                        listDaybook.Add(new DaybookModel
                        {
                            TransactionType = prop.TransactionType,
                            Credit = prop.Credit,
                            Debit = prop.Debit
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listDaybook;
        }
        public List<DaybookDetailedModel> DaybookDetailed(DateTime date)
        {
            List<DaybookDetailedModel> listDaybook = new List<DaybookDetailedModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    //var bDaybook = context.account_transaction.Include(x => x.account_ledger).Where(x => x.transaction_date == date && x.status == 1).ToList();
                    //var _bDaybook = bDaybook.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    var bDaybook = context.account_transaction.Include(x => x.account_ledger).Include(a => a.account_ledger.account_group).Where(x => x.transaction_date == date && x.status == 1).ToList();
                    var _bDaybook = bDaybook.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit), TransactionGroup = g.Max(x => x.account_ledger.account_group.group_name) });
                    //  string[] propertyNames = _bDaybook.GetType().GetProperties().Select(p => p.Name).ToArray();
                    foreach (var prop in _bDaybook)
                    {
                        listDaybook.Add(new DaybookDetailedModel
                        {
                            TransactionGroup = prop.TransactionGroup,
                            TransactionType = prop.TransactionType,
                            Credit = prop.Credit,
                            Debit = prop.Debit
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listDaybook;
        }

        public List<DaybookModel> CustomerStatementSummary(DateTime dateTo, int ledgerId, int customerType = 1, int routeId = 0)
        {
            List<DaybookModel> listCustomer = new List<DaybookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {

                    var xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date <= dateTo) && x.status == 1 && (ledgerId > 0 ? (x.ledger_id == ledgerId) : (x.ledger_id > 0))).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    //if (ledgerId > 0)
                    //{
                    //    xCustomerList = xCustomerList.Where(x => x.ledger_id == ledgerId  ).ToList();
                    //}

                    //List<customer> listCustomerType = context.customer.AsNoTracking().Where(x => x.customer_type == customerType && x.status == 1).OrderBy(x => x.customer_name).ToList();
                    List<customer> listCustomerType = context.customer.AsNoTracking().Where(x => x.ledger_id == ledgerId).OrderBy(x => x.customer_name).ToList();
                    if (routeId > 0)
                    {
                        listCustomerType = listCustomerType.Where(x => x.route_id == routeId).ToList();
                    }
                    List<account_transaction> listAccountTransaction = new List<account_transaction>();
                    foreach (customer cu in listCustomerType)
                    {
                        foreach (account_transaction at in xCustomerList)
                        {
                            if (cu.ledger_id == at.ledger_id)
                            {
                                listAccountTransaction.Add(at);
                            }
                        }
                    }



                    //  var _customerGroup = xCustomerList.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });

                    var _customerGroup = listAccountTransaction.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    //  string[] propertyNames = _bDaybook.GetType().GetProperties().Select(p => p.Name).ToArray();
                    foreach (var prop in _customerGroup)
                    {

                        listCustomer.Add(new DaybookModel
                        {
                            TransactionType = prop.TransactionType,
                            Credit = prop.Credit,
                            Debit = prop.Debit
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }
        public List<DaybookModel> CustomerStatementSummaryBySalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int salesmanLedger, int customerType = 1, int routeId = 0)
        {
            List<DaybookModel> listCustomer = new List<DaybookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<account_transaction> xCustomerList = null;
                    if (ledgerId > 0)
                        xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date <= dateTo) && x.status == 1 && (ledgerId > 0 ? (x.ledger_id == ledgerId) : (x.ledger_id > 0))).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    List<int> ledgerIds = context.customer.Where(x => x.salesman_ledger == salesmanLedger).Select(x => (int)(x.ledger_id)).ToList();
                    if (salesmanLedger > 0 && ledgerId == 0)
                    {

                        xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.status == 1 && ledgerIds.Contains(x.ledger_id)).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    }

                    List<customer> listCustomerType = context.customer.Where(x => x.customer_type == customerType && x.status == 1 && x.salesman_ledger == salesmanLedger).OrderBy(x => x.customer_name).ToList();

                    List<account_transaction> listAccountTransaction = new List<account_transaction>();
                    foreach (customer cu in listCustomerType)
                    {
                        foreach (account_transaction at in xCustomerList)
                        {
                            if (cu.ledger_id == at.ledger_id)
                            {
                                listAccountTransaction.Add(at);
                            }
                        }
                    }



                    //  var _customerGroup = xCustomerList.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });

                    var _customerGroup = listAccountTransaction.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    //  string[] propertyNames = _bDaybook.GetType().GetProperties().Select(p => p.Name).ToArray();
                    foreach (var prop in _customerGroup)
                    {

                        listCustomer.Add(new DaybookModel
                        {
                            TransactionType = prop.TransactionType,
                            Credit = prop.Credit,
                            Debit = prop.Debit
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }

        public List<DaybookModel> CustomerStatementSummaryBySalesman(DateTime dateTo, int ledgerId, int salesmanLedger, int customerType = 1, int routeId = 0)
        {
            List<DaybookModel> listCustomer = new List<DaybookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<account_transaction> xCustomerList = null;
                    if (ledgerId > 0)
                        xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date <= dateTo) && x.status == 1 && (ledgerId > 0 ? (x.ledger_id == ledgerId) : (x.ledger_id > 0))).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    List<int> ledgerIds = context.customer.Where(x => x.salesman_ledger == salesmanLedger).Select(x => (int)(x.ledger_id)).ToList();
                    if (salesmanLedger > 0 && ledgerId == 0)
                    {

                        xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date <= dateTo) && x.status == 1 && ledgerIds.Contains(x.ledger_id)).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    }

                    List<customer> listCustomerType = context.customer.Where(x => x.customer_type == customerType && x.status == 1 && x.salesman_ledger == salesmanLedger).OrderBy(x => x.customer_name).ToList();

                    List<account_transaction> listAccountTransaction = new List<account_transaction>();
                    foreach (customer cu in listCustomerType)
                    {
                        foreach (account_transaction at in xCustomerList)
                        {
                            if (cu.ledger_id == at.ledger_id)
                            {
                                listAccountTransaction.Add(at);
                            }
                        }
                    }



                    //  var _customerGroup = xCustomerList.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });

                    var _customerGroup = listAccountTransaction.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    //  string[] propertyNames = _bDaybook.GetType().GetProperties().Select(p => p.Name).ToArray();
                    foreach (var prop in _customerGroup)
                    {

                        listCustomer.Add(new DaybookModel
                        {
                            TransactionType = prop.TransactionType,
                            Credit = prop.Credit,
                            Debit = prop.Debit
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }
        public decimal CustomerOutstanding(int ledgerId, DateTime date, betaskdbEntities context)
        {
            decimal outstanding = 0;
            List<DaybookModel> listCustomer = new List<DaybookModel>();
            try
            {
                // using (betaskdbEntities context = new betaskdbEntities())
                {

                    List<account_transaction> listTransaction = context.account_transaction.Where(x => x.ledger_id == ledgerId && (x.transaction_date <= date) && x.status == 1).ToList();
                    if (listTransaction != null && listTransaction.Count > 0)
                    {
                        outstanding = listTransaction.Sum(x => x.debit) - listTransaction.Sum(x => x.credit);
                    }

                }
            }
            catch
            {
                throw;
            }
            return outstanding;
        }
        public List<account_transaction> CustomerStatementDetailed(DateTime dateFrom, DateTime dateTo, int ledgerId, out decimal openingDebit, out decimal openingCredit, int routeId = 0)
        {
            List<account_transaction> listCustomerDaybook = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {

                    List<int?> routeIds = null;
                    if (routeId > 0)
                        routeIds = context.customer.AsNoTracking().Where(x => x.route_id == routeId && x.customer_type == 1 && x.status == 1 && x.ledger_id != null).Select(x => x.ledger_id).ToList();

                    var xOpening = routeIds == null ? context.account_transaction.AsNoTracking().Where(x => (x.transaction_date < dateFrom) && x.status == 1 && x.ledger_id == ledgerId).ToList() : context.account_transaction.AsNoTracking().Where(x => (x.transaction_date < dateFrom) && x.status == 1 && routeIds.Contains(x.ledger_id)).ToList();
                    openingDebit = xOpening.Sum(x => (decimal?)x.debit) ?? 0;
                    openingCredit = xOpening.Sum(x => (decimal?)x.credit) ?? 0;


                    if (ledgerId > 0)
                        listCustomerDaybook = context.account_transaction.Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.ledger_id == ledgerId && x.status == 1 && (x.debit != 0 || x.credit != 0)).OrderBy(x => x.account_ledger.ledger_name).ToList();

                    else if (ledgerId == 0 && routeIds != null)
                        listCustomerDaybook = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).AsNoTracking().Include(c => c.account_ledger).AsNoTracking().Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && routeIds.Contains(x.ledger_id) && x.status == 1 && (x.debit != 0 || x.credit != 0)).OrderBy(x => x.account_ledger.ledger_name).ToList();


                    //foreach (var tran in listCustomerDaybook)
                    //{
                    //    var saleId = tran.transaction_type_id;
                    //    string saleNo = string.Empty;
                    //    if (tran.transaction_type.ToLower() == "sale")
                    //    {
                    //        saleNo = context.sales.AsNoTracking().FirstOrDefault(x => x.sales_id == saleId).sales_number;
                    //    }
                    //    tran.voucher_number = saleNo;
                    //}

                    // Step 1: Get all sale IDs up front (only for "sale" type)
                    var saleIds = listCustomerDaybook
                                    .Where(tran => tran.transaction_type.Equals("sale", StringComparison.OrdinalIgnoreCase))
                                    .Select(tran => tran.transaction_type_id)
                                    .Distinct()
                                    .ToList();

                    // Step 2: Fetch all matching sales at once
                    var salesMap = context.sales
                                    .AsNoTracking()
                                    .Where(s => saleIds.Contains(s.sales_id))
                                    .ToDictionary(s => s.sales_id, s => s.sales_number);

                    // Step 3: Update the voucher_number
                    foreach (var tran in listCustomerDaybook)
                    {
                        if (tran.transaction_type.Equals("sale", StringComparison.OrdinalIgnoreCase))
                        {
                            if (salesMap.TryGetValue(tran.transaction_type_id.Value, out string saleNo))
                            {
                                tran.voucher_number = saleNo;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomerDaybook;
        }
        public List<DaybookModel> CustomerStatementSummaryBySalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int salesmanLedger, out decimal openingDebit, out decimal openingCredit, int customerType = 1, int routeId = 0)
        {
            openingDebit = 0; openingCredit = 0;
            List<DaybookModel> listCustomer = new List<DaybookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<account_transaction> xCustomerList = null;
                    if (ledgerId > 0)
                        xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date <= dateTo) && x.status == 1 && (ledgerId > 0 ? (x.ledger_id == ledgerId) : (x.ledger_id > 0))).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    List<int> ledgerIds = context.customer.Where(x => x.salesman_ledger == salesmanLedger).Select(x => (int)(x.ledger_id)).ToList();
                    if (salesmanLedger > 0 && ledgerId == 0)
                    {
                        var xOpening = context.account_transaction.Where(x => (x.transaction_date < dateFrom) && x.status == 1 && ledgerIds.Contains(x.ledger_id)).ToList();
                        openingDebit = xOpening.Sum(x => x.debit);
                        openingCredit = xOpening.Sum(x => x.credit);
                        xCustomerList = context.account_transaction.AsNoTracking().Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.status == 1 && ledgerIds.Contains(x.ledger_id)).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    }

                    List<customer> listCustomerType = context.customer.Where(x => x.customer_type == customerType && x.salesman_ledger == salesmanLedger).OrderBy(x => x.customer_name).ToList();

                    List<account_transaction> listAccountTransaction = new List<account_transaction>();
                    foreach (customer cu in listCustomerType)
                    {
                        foreach (account_transaction at in xCustomerList)
                        {
                            if (cu.ledger_id == at.ledger_id)
                            {
                                listAccountTransaction.Add(at);
                            }
                        }
                    }



                    //  var _customerGroup = xCustomerList.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });

                    var _customerGroup = listAccountTransaction.GroupBy(x => x.account_ledger.ledger_name).Select(g => new { TransactionType = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) });
                    //  string[] propertyNames = _bDaybook.GetType().GetProperties().Select(p => p.Name).ToArray();
                    foreach (var prop in _customerGroup)
                    {

                        listCustomer.Add(new DaybookModel
                        {
                            TransactionType = prop.TransactionType,
                            Credit = prop.Credit,
                            Debit = prop.Debit
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }
        public List<account_transaction> CustomerStatementDetailedBySalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int salesmanLedger, out decimal openingDebit, out decimal openingCredit)
        {
            List<account_transaction> listCustomerDaybook = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    if (ledgerId > 0)
                    {
                        var xOpening = context.account_transaction.Where(x => (x.transaction_date < dateFrom) && x.status == 1 && x.ledger_id == ledgerId).ToList();
                        openingDebit = xOpening.Sum(x => x.debit);
                        openingCredit = xOpening.Sum(x => x.credit);
                        listCustomerDaybook = context.account_transaction.Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.ledger_id == ledgerId && x.status == 1 && (x.debit != 0 || x.credit != 0)).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    }
                    else
                    {
                        List<int> ledgerIds = context.customer.Where(x => x.salesman_ledger == salesmanLedger).Select(x => (int)(x.ledger_id)).ToList();
                        var xOpening = context.account_transaction.Where(x => (x.transaction_date < dateFrom) && x.status == 1 && ledgerIds.Contains(x.ledger_id)).ToList();
                        openingDebit = xOpening.Sum(x => x.debit);
                        openingCredit = xOpening.Sum(x => x.credit);
                        listCustomerDaybook = context.account_transaction.Include(x => x.account_ledger).Include(c => c.account_ledger).Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && ledgerIds.Contains(x.ledger_id) && x.status == 1 && (x.debit != 0 || x.credit != 0)).OrderBy(x => x.account_ledger.ledger_name).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCustomerDaybook;
        }
        public List<account_transaction> GetOpening(int routeId = 0)
        {
            List<account_transaction> listAccountTransaction = new List<account_transaction>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    if (routeId > 0)
                    {
                        List<account_transaction> tlistAccountTransaction = context.account_transaction.Include(l => l.account_ledger).Include(a => a.account_ledger.account_group).Where(x => x.transaction_type == "OPENING" && x.status == 1 && (x.debit != 0 || x.credit != 0)).OrderBy(x => x.account_ledger.ledger_name).ToList();
                        foreach (account_transaction acc in tlistAccountTransaction)
                        {
                            customer cust = context.customer.AsNoTracking().Where(x => x.ledger_id == acc.ledger_id && x.route_id == routeId).FirstOrDefault();
                            if (cust != null)
                            {

                                listAccountTransaction.Add(acc);

                            }
                        }
                    }
                    else
                        listAccountTransaction = context.account_transaction.Include(l => l.account_ledger).Include(a => a.account_ledger.account_group).Where(x => x.transaction_type == "OPENING" && x.status == 1 && (x.debit != 0 || x.credit != 0)).OrderBy(x => x.account_ledger.ledger_name).ToList();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listAccountTransaction;
        }

        public void UpdateOpening(int transactionId, int ledgerId, decimal debit, decimal credit)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    account_transaction xTran = context.account_transaction.Where(x => x.account_transaction_id == transactionId && x.ledger_id == ledgerId).FirstOrDefault();
                    if (xTran != null)
                    {
                        xTran.debit = debit;
                        xTran.credit = credit;
                        context.Entry(xTran).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }




        public List<Model.CashStatementRoutewiseModel> GetCashStatementRoutewise(DateTime date, int routeId)
        {
            List<Model.CashStatementRoutewiseModel> listStatement = new List<Model.CashStatementRoutewiseModel>();
            try
            {

                LedgerMappingDAL lm = new LedgerMappingDAL();
                int cashLedger = Convert.ToInt32(lm.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<customer> listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => (routeId == 0 ? x.route_id > 0 : x.route_id == routeId)).ToList();

                    if (listCustomer != null && listCustomer.Count > 0)
                    {
                        foreach (customer cs in listCustomer)
                        {



                            DateTime dt = new DateTime(date.Year, date.Month, date.Day, 00, 00, 00);
                            DateTime dt1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                            //List<account_transaction> listTransactionCredit = context.account_transaction.AsNoTracking().Where(x => x.transaction_date == date && x.status == 1 && x.ledger_id == cs.ledger_id && x.credit > 0
                            // && (x.transaction_type == "WALLET" || x.transaction_type == "RECIEPT" || x.transaction_type == "SALE")).OrderBy(x => x.transaction_type).ToList();
                            List<daily_collection> listTransactionCredit = context.daily_collection.Where(x => x.customer_id == cs.customer_id && (x.payment_mode.ToLower() == "cash" || (x.delivery_id == null && x.payment_mode.ToLower() == "coupon")) && (x.delivery_time >= dt && x.delivery_time <= dt1) && x.status == 4).ToList();

                            foreach (daily_collection ac in listTransactionCredit)
                            {


                                decimal debit = ac.collected_amount;
                                decimal credit = 0;
                                listStatement.Add(new Model.CashStatementRoutewiseModel
                                {
                                    CustomerId = cs.customer_id,
                                    CustomerName = cs.customer_name,
                                    Credit = credit,
                                    Debit = debit,
                                    Narration = $"",
                                    RouteName = cs.route.route_name,
                                    TranDate = date,
                                    TranType = ac.delivery_id == null ? "Collection" : "Cash"
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listStatement;

        }
        public List<ReconciliationModel> ReconciliationStatement(DateTime dateFrom, DateTime dateTo, int ledgerId, string mode)
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            List<ReconciliationModel> listRecocil = new List<ReconciliationModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).Where(x => x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.ledger_id == ledgerId && (x.debit != 0 || x.credit != 0) && x.transaction_type != "OPENING" && x.reconcil_date == null).OrderBy(d => d.transaction_date).ThenBy(t => t.transaction_type_id).ToList();
                    if (mode == "RECIEPT")
                        listTransactions = listTransactions.Where(x => x.debit > 0).ToList();
                    else if (mode == "PAYMENT")
                        listTransactions = listTransactions.Where(x => x.credit > 0).ToList();

                    var tranNumbers = listTransactions.Select(x => x.transaction_number).Distinct().ToList();
                    var listTran = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && tranNumbers.Contains(x.transaction_number)).ToList();
                    var cheques = context.account_transaction_cheque.Where(x => x.status == 1 && tranNumbers.Contains(x.account_transaction_number)).ToList();

                    if (listTransactions != null)
                    {
                        foreach (account_transaction ac in listTransactions)
                        {
                            string partyAccount = "";
                            List<string> listParty = new List<string>();
                            if (ac.debit > 0)
                            {
                                // var _party = context.account_transaction.Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).ToList();
                                //listParty = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.credit > 0 && x.status == 1).Select(x => x.account_ledger.ledger_name).ToList();
                                listParty = listTran.Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).Select(x => x.account_ledger.ledger_name).ToList();

                            }
                            else
                            {
                                // listParty = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.debit > 0 && x.status == 1).Select(x => x.account_ledger.ledger_name).ToList();
                                listParty = listTran.Where(x => x.transaction_number == ac.transaction_number && x.debit > 0).Select(x => x.account_ledger.ledger_name).ToList();
                            }

                            if (listParty != null)
                                partyAccount = string.Join(",", listParty);

                            account_transaction_cheque cheque = cheques.Where(x => x.account_transaction_number == ac.transaction_number).FirstOrDefault();

                            listRecocil.Add(new ReconciliationModel
                            {
                                Credit = ac.credit,
                                Debit = ac.debit,
                                PartyAccount = partyAccount,
                                TransactionDate = ac.transaction_date,
                                TransactionNumber = ac.transaction_number,
                                Narration = ac.narration,
                                TransactionType = ac.transaction_type,
                                Cheque = cheque != null ? cheque.cheque_number : "",
                                ChequeBank = cheque != null ? cheque.bank : "",
                                ChequeDate = cheque != null ? cheque.cheque_date.ToString("dd/MM/yyyy") : "",
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listRecocil;
        }
        public List<ReconciliationModel> ReconciliationStatementReconciledOL(DateTime dateFrom, DateTime dateTo, int ledgerId)
        {
            List<account_transaction> listTransactions = new List<account_transaction>();
            List<ReconciliationModel> listRecocil = new List<ReconciliationModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listTransactions = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.ledger_id == ledgerId && (x.debit != 0 || x.credit != 0) && x.transaction_type != "OPENING" && x.reconcil_date != null).OrderBy(d => d.transaction_date).ThenBy(t => t.transaction_type_id).ToList();


                    if (listTransactions != null)
                    {
                        foreach (account_transaction ac in listTransactions)
                        {
                            string partyAccount = "";
                            List<string> listParty = new List<string>();
                            if (ac.debit > 0)
                            {
                                // var _party = context.account_transaction.Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).ToList();
                                listParty = context.account_transaction.Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).Select(x => x.account_ledger.ledger_name).ToList();
                            }
                            else
                                listParty = context.account_transaction.Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.debit > 0).Select(x => x.account_ledger.ledger_name).ToList();


                            if (listParty != null)
                                partyAccount = string.Join(",", listParty);

                            account_transaction_cheque cheque = context.account_transaction_cheque.AsNoTracking().Where(x => x.account_transaction_number == ac.transaction_number && x.status == 1).FirstOrDefault();

                            listRecocil.Add(new ReconciliationModel
                            {
                                Credit = ac.credit,
                                Debit = ac.debit,
                                PartyAccount = partyAccount,
                                TransactionDate = ac.transaction_date,
                                TransactionNumber = ac.transaction_number,
                                Narration = ac.narration,
                                TransactionType = ac.transaction_type,
                                Cheque = cheque != null ? cheque.cheque_number : "",
                                ChequeBank = cheque != null ? cheque.bank : "",
                                ChequeDate = cheque != null ? cheque.cheque_date.ToString("dd/MM/yyyy") : "",
                                ReconcilDate = ac.reconcil_date.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listRecocil;
        }

        public List<ReconciliationModel> ReconciliationStatementReconciled(DateTime dateFrom, DateTime dateTo, int ledgerId)
        {
            List<ReconciliationModel> listRecocil = new List<ReconciliationModel>();

            using (var context = new betaskdbEntities())
            {
                var listTransactions = context.account_transaction
                    .AsNoTracking()
                    .Include(x => x.account_ledger)
                    .Where(x =>
                        x.status == 1 &&
                        x.transaction_date >= dateFrom &&
                        x.transaction_date <= dateTo &&
                        x.ledger_id == ledgerId &&
                        (x.debit != 0 || x.credit != 0) &&
                        x.transaction_type != "OPENING" &&
                        x.reconcil_date != null)
                    .OrderBy(x => x.transaction_date)
                    .ThenBy(x => x.transaction_type_id)
                    .ToList();


                var transactionNumbers = listTransactions
    .Select(x => x.transaction_number)
    .Distinct()
    .ToList();

                var chequeRecords = context.account_transaction_cheque
    .AsNoTracking()
    .Where(x => transactionNumbers.Contains(x.account_transaction_number) && x.status == 1)
    .ToList();

                var accountTransactionCredit= context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => transactionNumbers.Contains(x.transaction_number) && x.credit > 0 && x.status==1).ToList();
                var accountTransactionDebit = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => transactionNumbers.Contains(x.transaction_number) && x.debit > 0 && x.status == 1).ToList();


                foreach (var ac in listTransactions)
                {
                    string partyAccount = "";
                    List<string> listParty = new List<string>();
                    if (ac.debit > 0)
                    {
                        // var _party = context.account_transaction.Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).ToList();
                        //listParty = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).Select(x => x.account_ledger.ledger_name).ToList();
                        var creditLedgerLookup = accountTransactionCredit
                            .Where(x => x.credit > 0)
                            .ToLookup(x => x.transaction_number, x => x.account_ledger.ledger_name);
                        listParty = creditLedgerLookup[ac.transaction_number].ToList();
                    }
                    else
                    {
                        // listParty = context.account_transaction.Include(l => l.account_ledger).AsNoTracking().Where(x => x.transaction_number == ac.transaction_number && x.debit > 0).Select(x => x.account_ledger.ledger_name).ToList();
                        // listParty = accountTransactionDebit.Where(x => x.transaction_number == ac.transaction_number && x.credit > 0).Select(x => x.account_ledger.ledger_name).ToList();
                        var debitLedgerLookup = accountTransactionDebit
                           .Where(x => x.credit > 0)
                           .ToLookup(x => x.transaction_number, x => x.account_ledger.ledger_name);
                        listParty = debitLedgerLookup[ac.transaction_number].ToList();
                    }

                    if (listParty != null)
                        partyAccount = string.Join(",", listParty);

                    // account_transaction_cheque cheque = chequeRecords.Where(x => x.account_transaction_number == ac.transaction_number && x.status == 1).FirstOrDefault();
                    var chequeLookup = chequeRecords.ToLookup(x => x.account_transaction_number);
                    var cheque = chequeLookup[ac.transaction_number].FirstOrDefault();


                    listRecocil.Add(new ReconciliationModel
                    {
                        Credit = ac.credit,
                        Debit = ac.debit,
                        PartyAccount = partyAccount,
                        TransactionDate = ac.transaction_date,
                        TransactionNumber = ac.transaction_number,
                        Narration = ac.narration,
                        TransactionType = ac.transaction_type,
                        Cheque = cheque != null ? cheque.cheque_number : "",
                        ChequeBank = cheque != null ? cheque.bank : "",
                        ChequeDate = cheque != null ? cheque.cheque_date.ToString("dd/MM/yyyy") : "",
                        ReconcilDate = ac.reconcil_date?.ToString() ?? ""
                    });
                }
            }

            return listRecocil;
        }

        public void SaveReconciliation(List<Model.ReconcilUpdateModel> listTransaction)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    foreach (var tn in listTransaction)
                    {
                        List<account_transaction> transaction = context.account_transaction.Where(x => x.transaction_number == tn.Id).ToList();
                        foreach (account_transaction ac in transaction)
                        {
                            ac.reconcil_date = tn.Date;
                            context.Entry(ac).State = EntityState.Modified;
                        }

                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<RoutewiseCashbookModel> RoutewiseCashbookBLOCKED(DateTime dateFrom, DateTime dateTo)
        {
            List<RoutewiseCashbookModel> listRoutewiseCashbookGrouped = new List<RoutewiseCashbookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<RoutewiseCashbookModel> listRoutewiseCashbook = new List<RoutewiseCashbookModel>();
                    var ledgerIds = context.customer.Where(x => x.status == 1 && x.customer_type == 1).Select(x => x.ledger_id).ToList();
                    if (ledgerIds.Count > 0)
                    {
                        /*Getting transaction from account transaction and grouped by ledger id*/
                        var listTransaction = context.account_transaction.AsNoTracking().Where(x => ledgerIds.Contains(x.ledger_id) && x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).
                            GroupBy(x => x.ledger_id).Select(g => new { ledgerId = g.Key, Credit = g.Sum(x => x.credit), Debit = g.Sum(x => x.debit) }).ToList();

                        if (listTransaction != null && listTransaction.Count > 0)
                        {
                            LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();

                            int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                            foreach (var prop in listTransaction)
                            {
                                if (prop.ledgerId == cashLedger)
                                {

                                }
                                /*Getting customer by ledger id*/
                                customer cs = context.customer.AsNoTracking().Where(x => x.ledger_id == prop.ledgerId).FirstOrDefault();
                                route _route = new route();

                                /*Getting route by customer*/
                                if (cs != null)
                                {
                                    _route = context.route.AsNoTracking().Where(x => x.route_id == cs.route_id).FirstOrDefault();
                                }

                                /*Updating model list*/
                                if (_route != null)
                                {
                                    listRoutewiseCashbook.Add(new RoutewiseCashbookModel
                                    {

                                        RouteId = _route.route_id,
                                        RouteName = _route.route_name,
                                        Debit = prop.Debit,
                                        Credit = prop.Credit
                                    });
                                }
                            }
                        }

                        /*Grouping model list by route to be returned*/
                        if (listRoutewiseCashbook != null && listRoutewiseCashbook.Count > 0)
                        {
                            var routewiseCashbookGrouped = listRoutewiseCashbook.GroupBy(x => x.RouteId).Select(g => new { routeId = g.Key, RouteName = g.Max(x => x.RouteName), Debit = g.Sum(x => x.Debit), Credit = g.Sum(x => x.Credit) }).ToList();

                            if (routewiseCashbookGrouped != null && routewiseCashbookGrouped.Count > 0)
                            {
                                foreach (var prop in routewiseCashbookGrouped)
                                {
                                    listRoutewiseCashbookGrouped.Add(new RoutewiseCashbookModel
                                    {
                                        RouteId = prop.routeId,
                                        RouteName = prop.RouteName,
                                        Debit = prop.Debit,
                                        Credit = prop.Credit
                                    });
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
            return listRoutewiseCashbookGrouped;
        }

        public List<RoutewiseCashbookModel> RoutewiseCashbook(DateTime dateFrom, DateTime dateTo)
        {
            List<RoutewiseCashbookModel> listRoutewiseCashbookGrouped = new List<RoutewiseCashbookModel>();
            try
            {
                DataTable tblData = new DataTable();
                using (betaskdbEntities context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();

                        using (SqlCommand command = new SqlCommand("SP_GetRouteWiseCashBook", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            // Add parameters
                            command.Parameters.AddWithValue("@StartDate", dateFrom);
                            command.Parameters.AddWithValue("@EndDate", dateTo);

                            using (SqlDataAdapter adr = new SqlDataAdapter(command))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }

                    if (tblData != null && tblData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tblData.Rows)
                        {
                            listRoutewiseCashbookGrouped.Add(new RoutewiseCashbookModel
                            {
                                RouteId = Convert.ToInt32(dr["route_id"]),
                                RouteName = Convert.ToString(dr["route_name"]),
                                Debit = Convert.ToDecimal(dr["debit"]),
                                Credit = Convert.ToDecimal(dr["credit"]),
                            });
                        }
                    }

                    //                    List<RoutewiseCashbookModel> listRoutewiseCashbook = new List<RoutewiseCashbookModel>();
                    //                    var ledgerIds = context.customer.AsNoTracking().Where(x => x.customer_type == 1).Select(x => x.ledger_id).ToList();
                    //                    if (ledgerIds.Count > 0)
                    //                    {

                    //                        LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    //                        int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                    ////                        List<int> tranIds = context.account_transaction.AsNoTracking().Include(r => r.route).Where(x => x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.ledger_id == cashLedger && x.status == 1).Select(x => x.transaction_number).Distinct().ToList();

                    //                        List<account_transaction> listTransaction = context.account_transaction.AsNoTracking().Where(x => x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.ledger_id == cashLedger && x.status == 1 && x.route_id != null).ToList();


                    //                            /*Grouping model list by route to be returned*/
                    //                            if (listTransaction != null && listTransaction.Count > 0)
                    //                            {
                    //                                var routewiseCashbookGrouped = listTransaction.GroupBy(x => x.route_id).Select(g => new { routeId = g.Key, RouteName = g.Max(x => x.route.route_name), Debit = g.Sum(x => x.debit), Credit = g.Sum(x => x.credit) }).ToList();

                    //                                if (routewiseCashbookGrouped != null && routewiseCashbookGrouped.Count > 0)
                    //                                {
                    //                                    foreach (var prop in routewiseCashbookGrouped)
                    //                                    {
                    //                                        listRoutewiseCashbookGrouped.Add(new RoutewiseCashbookModel
                    //                                        {
                    //                                            RouteId = Convert.ToInt32(prop.routeId),
                    //                                            RouteName = prop.RouteName,
                    //                                            Debit = prop.Debit,
                    //                                            Credit = prop.Credit
                    //                                        });
                    //                                    }
                    //                                }
                    //                            }

                    //                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listRoutewiseCashbookGrouped;
        }

        public List<RoutewiseCashbookModel> RoutewiseCashbookNoInUSE(DateTime dateFrom, DateTime dateTo)
        {
            List<RoutewiseCashbookModel> listRoutewiseCashbookGrouped = new List<RoutewiseCashbookModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<RoutewiseCashbookModel> listRoutewiseCashbook = new List<RoutewiseCashbookModel>();
                    var ledgerIds = context.customer.Where(x => x.status == 1 && x.customer_type == 1).Select(x => x.ledger_id).ToList();
                    if (ledgerIds.Count > 0)
                    {

                        LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                        int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                        List<int> tranIds = context.account_transaction.Where(x => x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.ledger_id == cashLedger && x.status == 1).Select(x => x.transaction_number).Distinct().ToList();

                        List<account_transaction> listTransaction = context.account_transaction.AsNoTracking().Where(x => x.transaction_date >= dateFrom && x.transaction_date <= dateTo && tranIds.Contains(x.transaction_number) && (ledgerIds.Contains(x.ledger_id) || x.ledger_id == cashLedger) && x.status == 1).ToList();

                        if (listTransaction != null && listTransaction.Count > 0)
                        {



                            foreach (account_transaction prop in listTransaction)
                            {
                                if (prop.ledger_id != cashLedger && prop.status == 1)
                                {

                                    /*Getting customer by ledger id*/
                                    customer cs = context.customer.AsNoTracking().Where(x => x.ledger_id == prop.ledger_id).FirstOrDefault();
                                    route _route = new route();

                                    /*Getting route by customer*/
                                    if (cs != null)
                                    {
                                        _route = context.route.AsNoTracking().Where(x => x.route_id == cs.route_id).FirstOrDefault();
                                    }

                                    decimal debit = listTransaction.Where(x => x.ledger_id == cashLedger && x.transaction_number == prop.transaction_number).FirstOrDefault().debit;
                                    decimal credit = listTransaction.Where(x => x.ledger_id == cashLedger && x.transaction_number == prop.transaction_number).FirstOrDefault().credit;
                                    /*Updating model list*/
                                    if (_route != null)
                                    {
                                        listRoutewiseCashbook.Add(new RoutewiseCashbookModel
                                        {

                                            RouteId = _route.route_id,
                                            RouteName = _route.route_name,
                                            Debit = debit,
                                            Credit = credit
                                        });
                                    }
                                }
                            }
                        }

                        /*Grouping model list by route to be returned*/
                        if (listRoutewiseCashbook != null && listRoutewiseCashbook.Count > 0)
                        {
                            var routewiseCashbookGrouped = listRoutewiseCashbook.GroupBy(x => x.RouteId).Select(g => new { routeId = g.Key, RouteName = g.Max(x => x.RouteName), Debit = g.Sum(x => x.Debit), Credit = g.Sum(x => x.Credit) }).ToList();

                            if (routewiseCashbookGrouped != null && routewiseCashbookGrouped.Count > 0)
                            {
                                foreach (var prop in routewiseCashbookGrouped)
                                {
                                    listRoutewiseCashbookGrouped.Add(new RoutewiseCashbookModel
                                    {
                                        RouteId = prop.routeId,
                                        RouteName = prop.RouteName,
                                        Debit = prop.Debit,
                                        Credit = prop.Credit
                                    });
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
            return listRoutewiseCashbookGrouped;
        }

        public void UpdateRouteInTransaction(DateTime date)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                    List<int> transactionNumnbers = context.account_transaction.AsNoTracking().Where(x => x.ledger_id == cashLedger && x.status == 1 && x.route_id == null && x.transaction_date == date && x.transaction_type == "SALE").Select(x => x.transaction_number).Distinct().ToList();
                    if (transactionNumnbers.Count > 0)
                    {
                        foreach (int tran in transactionNumnbers)
                        {
                            List<account_transaction> listTransactions = context.account_transaction.Where(x => x.transaction_number == tran).ToList();
                            foreach (account_transaction ac in listTransactions)
                            {
                                if (ac.ledger_id != cashLedger)
                                {
                                    if (context.sales.Any(x => x.sales_id == ac.transaction_type_id))
                                    {
                                        int routeId = Convert.ToInt32(context.sales.FirstOrDefault(x => x.sales_id == ac.transaction_type_id).route_id);
                                        //int routeId =Convert.ToInt32( context.customer.AsNoTracking().FirstOrDefault(x => x.ledger_id == ac.ledger_id).route_id);
                                        if (routeId > 0)
                                        {
                                            //Updating routeIds
                                            List<account_transaction> acupdate = listTransactions.Where(x => x.transaction_number == ac.transaction_number).ToList();
                                            foreach (account_transaction up in acupdate)
                                            {
                                                up.route_id = routeId;
                                                context.Entry(up).State = EntityState.Modified;
                                            }
                                            context.SaveChanges();

                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        public DataTable GetRoutewiseCashDetailed(DateTime date, int routeId)
        {
            DataTable tblData = new DataTable();
            try
            {

                using (var context = new betaskdbEntities())
                {
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    int cashLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        //string connectionString = $@"select CASE WHEN a.transaction_type='SALE' then c.customer_name 
                        //                            when a.transaction_type = 'RECIEPT' then (select customer_name from customer where customer_id =
                        //                            (select customer_id from daily_collection where daily_collection_id = a.voucher_number and status != 2))
                        //                             else '' end as CustomerName,a.debit as Amount,a.transaction_type as TransactionType,a.narration as Narration,a.added_time as AddedOn from account_transaction a
                        //                             left  join sales b on b.sales_id = a.transaction_type_id left  join customer c on c.customer_id = b.customer_id
                        //                            where transaction_date = '{date}' and a.ledger_id = {cashLedger} and a.route_id = {routeId} and a.status = 1 order by CustomerName";


                        string connectionString = $@"select c.customer_name as CustomerName,a.debit as Amount,a.transaction_type as TransactionType,a.narration as Narration,a.added_time as AddedOn from account_transaction a
                                                     inner join sales b on b.sales_id = a.transaction_type_id
                                                     inner join customer c on c.customer_id = b.customer_id
                                                     where  cast(transaction_date as date)=@date and a.ledger_id = @cashLedger and a.route_id = @routeId and a.status = 1 and debit> 0 and a.transaction_type = 'SALE'
                                                     union all
                                                     select b.ledger_name as CustomerName,a.credit as Amount,a.transaction_type as TransactionType,a.narration as Narration,a.added_time as AddedOn from account_transaction a
                                                     inner join account_ledger b on b.ledger_id = a.ledger_id
                                                     where cast(transaction_date as date)=@date and a.ledger_id != @cashLedger and a.route_id =  @routeId  and a.status = 1 and a.transaction_type = 'RECIEPT' and credit> 0 order by TransactionType,CustomerName";

                        using (SqlCommand cmd = new SqlCommand(connectionString, conn))
                        {
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@cashLedger", cashLedger);
                            cmd.Parameters.AddWithValue("@routeId", routeId);

                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return tblData;
        }

        public decimal GetLedgerOpening(int ledgerId, DateTime dateFrom, DateTime dateTo)
        {
            decimal opening = 0;
            try
            {
                                using (var context = new betaskdbEntities())
                                {
                                    decimal debit = context.account_transaction
                    .AsNoTracking()
                    .Where(x => x.ledger_id == ledgerId &&
                                x.status == 1 &&
                                x.transaction_date < dateFrom)
                    .Sum(x => (decimal?)x.debit) ?? 0;

                    decimal credit = context.account_transaction
                        .AsNoTracking()
                        .Where(x => x.ledger_id == ledgerId &&
                                    x.status == 1 &&
                                    x.transaction_date < dateFrom)
                        .Sum(x => (decimal?)x.credit) ?? 0;

                    //var opDebitList = context.account_transaction.Where(x => x.ledger_id == ledgerId && x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.transaction_type == "OPENING").ToList();
                    //decimal openingDebit = opDebitList.Count > 0 ? opDebitList.Sum(d => d.debit) : 0;

                    //var opCreditList = context.account_transaction.Where(x => x.ledger_id == ledgerId && x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.transaction_type == "OPENING").ToList();
                    //decimal openingCredit = opCreditList.Count > 0 ? opDebitList.Sum(d => d.credit) : 0;

                    decimal openingDebit = context.account_transaction
                    .Where(x => x.ledger_id == ledgerId &&
                                x.status == 1 &&
                                x.transaction_date >= dateFrom &&
                                x.transaction_date <= dateTo &&
                                x.transaction_type == "OPENING")
                    .Sum(x => (decimal?)x.debit) ?? 0;

                    decimal openingCredit = context.account_transaction
                        .Where(x => x.ledger_id == ledgerId &&
                                    x.status == 1 &&
                                    x.transaction_date >= dateFrom &&
                                    x.transaction_date <= dateTo &&
                                    x.transaction_type == "OPENING")
                        .Sum(x => (decimal?)x.credit) ?? 0;

                    opening = ((debit + openingDebit) - (credit + openingCredit));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return opening;

        }

        public decimal GetReconciledBalance(int ledgerId, DateTime dateFrom, DateTime dateTo, decimal _reconciledBalance)
        {
            decimal reconciledBalance = _reconciledBalance;
            try
            {
                //reconciledBalance = GetLedgerOpening(ledgerId, dateFrom, dateTo);
                using (var context = new betaskdbEntities())
                {
                    if (context.account_transaction.Any(x => x.ledger_id == ledgerId && x.status == 1 && x.reconcil_date >= dateFrom && x.reconcil_date <= dateTo && x.reconcil_date != null))
                    {
                        var debit = IsReconiledDebitExist(ledgerId, dateFrom, dateTo, context) ? context.account_transaction.Where(x => x.ledger_id == ledgerId && x.status == 1 && x.reconcil_date >= dateFrom && x.reconcil_date <= dateTo && x.reconcil_date != null && x.debit > 0).Sum(d => (decimal?)d.debit ?? 0) : 0;
                        var credit = IsReconiledCreditExist(ledgerId, dateFrom, dateTo, context) ? context.account_transaction.Where(x => x.ledger_id == ledgerId && x.status == 1 && x.reconcil_date >= dateFrom && x.reconcil_date <= dateTo && x.reconcil_date != null && x.credit > 0).Sum(d => (decimal?)d.credit ?? 0) : 0;
                        reconciledBalance -= credit;
                        reconciledBalance += debit;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return reconciledBalance;
        }
        private static bool IsReconiledCreditExist(int ledgerId, DateTime dateFrom, DateTime dateTo, betaskdbEntities context)
        {
            return context.account_transaction.Any(x => x.ledger_id == ledgerId && x.status == 1 && x.reconcil_date >= dateFrom && x.reconcil_date <= dateTo && x.reconcil_date != null && x.credit > 0);
        }

        private static bool IsReconiledDebitExist(int ledgerId, DateTime dateFrom, DateTime dateTo, betaskdbEntities context)
        {
            return context.account_transaction.Any(x => x.ledger_id == ledgerId && x.status == 1 && x.reconcil_date >= dateFrom && x.reconcil_date <= dateTo && x.reconcil_date != null && x.debit > 0);
        }
        public void RemoveReconciliation(int transactionNumber)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var transactions = context.account_transaction.Where(x => x.transaction_number == transactionNumber && x.status == 1).ToList();
                    if (transactions != null && transactions.Count > 0)
                    {
                        foreach (var transaction in transactions)
                        {
                            transaction.reconcil_date = null;
                            context.Entry(transaction).Property(x => x.reconcil_date).IsModified = true;
                            context.SaveChanges();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }

}
