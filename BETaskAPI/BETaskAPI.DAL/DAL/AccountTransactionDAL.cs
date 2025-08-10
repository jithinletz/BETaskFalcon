using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskAPI.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETaskAPI.DAL.Model;
using System.Data.SqlClient;
using System.Data.Entity.Validation;


namespace BETaskAPI.DAL.DAL
{
    public class AccountTransactionDAL
    {
        public enum EnumTransactionTypes { PURCHASE, SALE, PRETURN, SRETURN, PAYMENT, RECIEPT, WALLET, JOURNAL, OPENING, PETTY }
        public enum EnumDocuments { SALE, ACCOUNT, PAYMENT, RECIEPT, DO, DOINV, AGREEMENT, DOSALE, PETTY, JOURNAL };//DOSALE for DO Delivery


        readonly LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();

        public List<account_transaction> PostCashBankSale(betaskdbEntities context, SaleAccountPostModel saleAccountPosting, sales sale,  List<account_transaction> listMultipleSaleAccounts = null)
        {
            /*
             * Ledger    Name                      Debit    Credit 
                58		  Cash Account				11.00	0.00
                4		  Vat Collected on Sales	0.00	0.52
                57		  Sales Account				0.00	10.48
             */
            List<account_transaction> listAccount = new List<account_transaction>();

            try
            {
                saleAccountPosting = ledgerMappingDAL.SetSaleAccountLedger(saleAccountPosting);
                var route = context.route.AsNoTracking().FirstOrDefault(x => x.route_id == sale.route_id);
                string routeName = route != null ? route.route_name : "";
                decimal totalCR = 0, totalDR = 0;
                string customer = sale.customer != null ? sale.customer.customer_name : "";
                string _narration = $"{sale.payment_mode} Sale of {sale.net_amount} {sale.remarks} - {routeName} - {sale.sales_order}";
                string tranType = EnumTransactionTypes.SALE.ToString();
                string voucherNumber = sale.payment_mode.ToLower();
                long tranTypeId = sale.sales_id;
                DateTime tranDate = sale.sales_date;

                //Cash / Bank
                totalDR = AddDebit(saleAccountPosting.CashSaleLedger, listAccount, sale.net_amount, totalDR, _narration);


                //Cr
                //VAT
                totalCR = AddCredit(saleAccountPosting.VatOnSaleLedger, listAccount, sale.total_vat,totalCR, _narration);

                //Sales ledger
                if (listMultipleSaleAccounts == null)
                {
                    totalCR = AddCredit(saleAccountPosting.SalesLedger, listAccount, sale.gross_amount, totalCR, _narration);
                }
                else
                {
                    foreach (account_transaction _tranSale in listMultipleSaleAccounts)
                    {
                        totalCR = AddCredit(_tranSale.ledger_id, listAccount, _tranSale.credit, totalCR, _narration);
                    }
                }

                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int.TryParse(GetNextDocument(context, EnumDocuments.ACCOUNT.ToString()), out int tranNumber);


                if (tranNumber > 0 && listAccount.Count > 0)
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
                    }
                    context.account_transaction.AddRange(listAccount);
                }


            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
            return listAccount;
        }

        private static decimal AddCredit(int ledgerid,  List<account_transaction> listAccount, decimal credit,decimal totalCR, string _narration)
        {
            if (credit != 0)
            {
                account_transaction transaction = new EDMX.account_transaction()
                {
                    ledger_id = ledgerid,
                    debit = 0,
                    credit =credit,
                    narration = _narration

                };
                totalCR += credit;
                listAccount.Add(transaction);
            }

            return totalCR;
        }

        private static decimal AddDebit(int ledgerId,  List<account_transaction> listAccount, decimal debit,decimal totalDR, string _narration)
        {
            account_transaction transaction = new EDMX.account_transaction()
            {
                ledger_id =ledgerId,
                debit = debit,
                credit = 0,
                narration = _narration,

            };
            totalDR += debit;
            listAccount.Add(transaction);
            return totalDR;
        }

        public List<account_transaction> PostSale(betaskdbEntities context, SaleAccountPostModel saleAccountPosting, sales sale,  List<account_transaction> listMultipleSaleAccounts = null)
        {
            /* Coupon sale
             * Ledger    Name                      Debit    Credit 
               674894	  dubai dawn site office	63.00	 0.00
               4	      Vat Collected on Sales	0.00	 3.00
               57	      Sales Account	            0.00     60.00
             */

            string err="";
            List<account_transaction> listAccount = new List<EDMX.account_transaction>();

            try
            {
                var route = context.route.AsNoTracking().FirstOrDefault(x=>x.route_id==sale.route_id);
                string routeName = route!=null ?route.route_name:"";
                saleAccountPosting = ledgerMappingDAL.SetSaleAccountLedger(saleAccountPosting);
                decimal totalCR = 0, totalDR = 0;
                string customer = sale.customer != null ? sale.customer.customer_name : "";

                //Getting division
                string division = "";
                if (sale.division_id != null && sale.division_id != 0)
                    division = context.customer_division.AsNoTracking().FirstOrDefault(x => x.division_id == sale.division_id).division_name;

                string _narration = sale.payment_mode.ToLower() == "salesmancredit" ? customer : "" + $"{division} {sale.payment_mode} Sale of {sale.net_amount} {(sale.old_leaf_count > 0 ? "-OldLeaf" : "")} - {routeName}-{sale.sales_order}".TrimStart();

                string tranType = EnumTransactionTypes.SALE.ToString();
                string voucherNumber = sale.payment_mode.ToLower();
                long tranTypeId = sale.sales_id;
                DateTime tranDate = sale.sales_date;

                //Customer
                totalDR = AddDebit(saleAccountPosting.CreditSaleLedger, listAccount, sale.net_amount, totalDR, _narration);

                err = $"AddDebit - > saleAccountPosting.CreditSaleLedger ={saleAccountPosting.CreditSaleLedger} , sale.net_amount={sale.net_amount} , totalDR={totalDR}\n";

                //Cr
                //VAT
                totalCR = AddCredit(saleAccountPosting.VatOnSaleLedger, listAccount, sale.total_vat, totalCR, _narration);
                err += $"AddCredit - > saleAccountPosting.VatOnSaleLedger={saleAccountPosting.VatOnSaleLedger} , sale.gross_amount={sale.total_vat} , totalCR={totalCR}\n";

                if (listMultipleSaleAccounts == null)
                {
                    totalCR = AddCredit(saleAccountPosting.SalesLedger, listAccount, sale.gross_amount, totalCR, _narration);
                    err += $"AddCredit - > saleAccountPosting.SalesLedger={saleAccountPosting.SalesLedger} , sale.gross_amount={sale.gross_amount} , totalCR={totalCR}\n";
                }
                else
                {
                    foreach (account_transaction _tranSale in listMultipleSaleAccounts)
                    {
                        totalCR = AddCredit(_tranSale.ledger_id, listAccount, _tranSale.credit, totalCR, _narration);
                    }
                }

                if (totalDR != totalCR)
                {
                    throw new Exception($"Amount mismatch while posting DR={totalDR} , CR={totalCR}");
                }


                int.TryParse(GetNextDocument(context, EnumDocuments.ACCOUNT.ToString()), out int tranNumber);


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
                        tran.added_time = DateTime.Now;
                    }
                    context.account_transaction.AddRange(listAccount);
                }

            }
            catch (Exception ee)
            {
                throw new Exception($"{err} {ee}");
            }
            return listAccount;
        }

        public string PostCollection(betaskdbEntities context, daily_collection collection)
        {
            string info = "";// $"\nPost collection for customer: {collection.customer_id} , employee: {collection.employee_id} , {collection.collected_amount} \n";
            try
            {
                string custName = collection.customer_id>0? context.customer.Find(collection.customer_id).customer_name:"";
                string routeName = collection.route_id != null ? context.route.Find(collection.route_id).route_name : "";
                string employee = collection.employee_id > 0 ? context.employee.Find(collection.employee_id).first_name : "";
                decimal totalDR = 0, totalCR=0 ;
                List<account_transaction> listAccount = new List<account_transaction>();
                int cashLedger = Convert.ToInt32( ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CASH).ledger_id);
                var customer = context.customer.Find(collection.customer_id);
                int customerLedger = Convert.ToInt32(customer.ledger_id);
                if (customer.group_id > 0)
                     customerLedger = Convert.ToInt32(context.customer.AsNoTracking().FirstOrDefault(x => x.customer_id == customer.group_id).ledger_id);
                string depositrem = collection.is_deposit == 1 ? $" Deposit {collection.delivery_id} - ":"";
                string narration = $"{collection.payment_mode} coll, -{ depositrem}{routeName}-{custName} by {employee}";

                AddDebit(cashLedger, listAccount, collection.collected_amount, totalDR, narration);
                if (collection.is_deposit == 1)
                {
                    //Deposit ledger credit here
                    ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.DEPOSITCOLLECTION);
                    if (ledgerDeposit != null)
                        customerLedger = Convert.ToInt32(ledgerDeposit.ledger_id);

                }
                AddCredit(customerLedger, listAccount, collection.collected_amount, totalCR, narration);
                int.TryParse(GetNextDocument(context, EnumDocuments.ACCOUNT.ToString()), out int tranNumber);
                //info += $"Total CR={totalCR} , Total DR={totalDR}\n";

                if (tranNumber > 0 && listAccount.Count > 0)
                {
                    foreach (account_transaction tran in listAccount)
                    {
                        //info += $"Tran Number {tranNumber} ledger:{tran.ledger_id} , {tran.debit} , {tran.credit}\n";
                        tran.transaction_number = tranNumber;
                        tran.transaction_type = EnumTransactionTypes.RECIEPT.ToString();
                        tran.transaction_type_id = collection.daily_collection_id;
                        tran.transaction_date = collection.delivery_time;
                        tran.status = 1;
                        tran.voucher_number = "Collection";
                        tran.route_id = collection.route_id;
                        tran.route_id = collection.route_id;
                        tran.added_time = DateTime.Now;
                    }
                    context.account_transaction.AddRange(listAccount);
                    context.SaveChanges();
                    //info += $"Tran Number {tranNumber} Saving completed";
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return info;
        }



        public string GetNextDocument(betaskdbEntities context, string docType)
        {
            int nextSerial = 0;
            string nextSerialWithPrefix = string.Empty;
            try
            {
                // using (var context = new betaskdbEntities())
                {
                    var doc = context.document_serial.Where(x => x.document_type == docType).FirstOrDefault();
                    if (doc != null)
                    {
                        nextSerial = doc.next_number;

                        if (doc.prefix != null)
                            nextSerialWithPrefix = String.Format("{0}{1}", doc.prefix, doc.next_number);

                        else
                            nextSerialWithPrefix = doc.next_number.ToString();

                        doc.next_number++;

                        context.document_serial.Attach(doc);
                        context.Entry(doc).Property(x => x.next_number).IsModified = true;
                    }
                    else
                        throw new Exception("No document serial found");

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return nextSerialWithPrefix;
        }
    }
}
