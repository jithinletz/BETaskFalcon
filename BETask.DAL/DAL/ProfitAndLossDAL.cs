using System;
using System.Collections.Generic;
using System.Linq;
using BETask.DAL.EDMX;
using System.Data;
using BETask.DAL.Model;
using System.Data.SqlClient;

namespace BETask.DAL.DAL
{
    public class ProfitAndLossDAL
    {
        LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
        enum AccountTypes { DIRECTEXPENSE, INDIRECTEXPENCE, INDIRECTINCOME, DIRECTINCOME }
        public List<account_ledger> LedgerList { get; set; }
        public List<ProfitandLossModel> GenerateProfitandLoss(DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> listPandL = new List<ProfitandLossModel>();
            decimal closingStock = ClosingStock(dateFrom, dateTo.AddDays(1));
            decimal openingStock = OpeningStock(dateFrom, dateTo);
            try
            {
                using (var context = new betaskdbEntities())
                {


                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Sales",
                        amount1 = 0,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "T"
                    });
                    List<account_transaction> saleList = new List<account_transaction>();
                    decimal sales = Sale(context, dateFrom, dateTo, out saleList);
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Sales",
                        amount1 = sales,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "V"
                    });
                    decimal salesReturn = SaleReturn(context, dateFrom, dateTo, saleList);
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Less:- Sales Return",
                        amount1 = salesReturn,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "V"
                    });

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Total Sales",
                        amount1 = 0,
                        amount2 = (sales - salesReturn),
                        amount3 = 0,
                        HeadType = "V"
                    });

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Less:- Cost of Goods Sold:",
                        amount1 = 0,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "T"
                    });



                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Purchases:",
                        amount1 = 0,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "T"
                    });
                    List<account_transaction> purchaseList = new List<account_transaction>();

                    decimal purchase = Purchase(context, dateFrom, dateTo, out purchaseList);
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Purchase",
                        amount1 = purchase,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "V"
                    });
                    decimal purchaseReturn = PurchaseReturn(context, dateFrom, dateTo, purchaseList);
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Less:- Purchase Return",
                        amount1 = purchaseReturn,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "V"
                    });

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Total Purchase",
                        amount1 = 0,
                        amount2 = (purchase - purchaseReturn),
                        amount3 = 0,
                        HeadType = "V"
                    });

                    //Direct Expense
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Direct Expense",
                        amount1 = 0,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "T"
                    });

                    decimal totaldirectExp = 0, totalInDirectExp = 0, totalIndirectIncome = 0;
                    List<ProfitandLossModel> listDirect = GetAccountTransactionTypeList(AccountTypes.DIRECTEXPENSE, context, dateFrom, dateTo);
                    if (listDirect != null && listDirect.Count > 0)
                    {

                        int cnt = 0;
                        foreach (ProfitandLossModel pl in listDirect)
                        {
                            cnt++;
                            listPandL.Add(pl);
                            totaldirectExp += pl.amount1;
                            //if (cnt == listDirect.Count)
                            //    pl.amount2 = totaldirectExp;
                        }
                    }

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Total Direct Expense",
                        amount1 = 0,
                        amount2 = totaldirectExp,
                        amount3 = 0,
                        HeadType = "V"
                    });

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Less:- ClosingStcok",
                        amount1 = closingStock,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "V"
                    });

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Total COGS",
                        amount1 = 0,
                        amount2 = (openingStock + ((purchase + totaldirectExp) - purchaseReturn)) - closingStock,
                        amount3 = 0,
                        HeadType = "V"
                    });

                    decimal grossProfit = 0;
                    //PROFIT
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Gross Profit",
                        amount1 = 0,
                        amount2 = ((sales - salesReturn) - ((openingStock + ((purchase + totaldirectExp) - purchaseReturn)) - closingStock)),
                        //amount3 = ((sales - salesReturn) - ((openingStock + (purchase - purchaseReturn)) - closingStock)),
                        HeadType = "V"
                    });
                    grossProfit = ((sales - salesReturn) - ((openingStock + ((purchase + totaldirectExp) - purchaseReturn)) - closingStock));



                    //iNDIRECT Expense
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "INDirect Expense:",
                        amount1 = 0,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "T"
                    });
                    List<ProfitandLossModel> listIndirect = GetAccountTransactionTypeList(AccountTypes.INDIRECTEXPENCE, context, dateFrom, dateTo);
                    if (listIndirect != null && listIndirect.Count > 0)
                    {

                        int cnt = 0;
                        foreach (ProfitandLossModel pl in listIndirect)
                        {
                            listPandL.Add(pl);
                            totalInDirectExp += pl.amount1;
                            //if (cnt == listIndirect.Count)
                            //    pl.amount2 = totalInDirectExp;
                        }
                    }
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Total Indirect Expense:",
                        amount1 = 0,
                        amount2 = totalInDirectExp,
                        // amount3 = (totaldirectExp+totalInDirectExp),
                        HeadType = "V"
                    });

                    //iNDIRECT Income
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "INDirect Income:",
                        amount1 = 0,
                        amount2 = 0,
                        amount3 = 0,
                        HeadType = "T"
                    });
                    List<ProfitandLossModel> listIndirectIncme = GetAccountTransactionTypeList(AccountTypes.INDIRECTINCOME, context, dateFrom, dateTo);
                    if (listIndirectIncme != null && listIndirectIncme.Count > 0)
                    {
                        int cnt = 0;
                        foreach (ProfitandLossModel pl in listIndirectIncme)
                        {
                            listPandL.Add(pl);
                            totalIndirectIncome += pl.amount1;
                            //if (cnt == listIndirectIncme.Count)
                            //    pl.amount2 = totalIndirectIncome;
                        }
                    }
                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Total IndirectIncome:",
                        amount1 = 0,
                        amount2 = (totalIndirectIncome),
                        // amount3 = (totalIndirectIncome),
                        HeadType = "V"
                    });

                    listPandL.Add(new ProfitandLossModel
                    {
                        Description = "Net Profit:",
                        amount1 = 0,
                        amount2 = (grossProfit + totalIndirectIncome) - totalInDirectExp,
                        // amount3 = (totalIndirectIncome),
                        HeadType = "V"
                    });
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listPandL;
        }
        public ProfitandLossModelNew GenerateProfitandLossNew(DateTime dateFrom, DateTime dateTo)
        {
            ProfitandLossModelNew listPandL = new ProfitandLossModelNew();
            //decimal closingStock = ClosingStock(dateFrom, dateTo.AddDays(1));
            //decimal openingStock = OpeningStock(dateFrom, dateTo);
            try
            {
                using (var context = new betaskdbEntities())
                {

                    context.Database.CommandTimeout = 1500;

                    LedgerList = context.account_ledger.AsNoTracking().Where(x => x.description != "CUSTOMER").ToList();


                    List<account_transaction> listPurchases = new List<account_transaction>();
                    decimal purchase = Purchase(context, dateFrom, dateTo, out listPurchases);
                    decimal purchaseReturn = PurchaseReturn(context, dateFrom, dateTo, listPurchases);
                    List<account_transaction> salesList = new List<account_transaction>();
                    decimal sale = Sale(context, dateFrom, dateTo, out salesList);
                    decimal saleReturn = SaleReturn(context, dateFrom, dateTo, salesList);

                    listPandL = new ProfitandLossModelNew
                    {
                        OpeningStock = OpeningStock(dateFrom, dateTo),
                        ClosingStock = ClosingStock(dateFrom, dateTo.AddDays(1)),
                        Purchase = purchase,
                        Sale = sale,
                        PurchaseReturn = purchaseReturn,
                        SaleReturn = saleReturn,
                        //SaleTax=VATCollected(context, dateFrom, dateTo),
                        //PurchaseTax= VATPaid(context, dateFrom, dateTo),


                        ListDirectExp = GetAccountTransactionTypeList(AccountTypes.DIRECTEXPENSE, context, dateFrom, dateTo),
                        ListINDirectExp = GetAccountTransactionTypeList(AccountTypes.INDIRECTEXPENCE, context, dateFrom, dateTo),
                        ListINDirectIncome = GetAccountTransactionTypeList(AccountTypes.INDIRECTINCOME, context, dateFrom, dateTo),
                        ListDirectIncome = GetAccountTransactionTypeList(AccountTypes.DIRECTINCOME, context, dateFrom, dateTo),

                    };
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listPandL;
        }
        //private decimal OpeningSTock(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        //{
        //    decimal openingStock = 0;
        //    try
        //    {
        //        //var _openingStock = context.item_transaction.Where(x => x.transaction_date < dateFrom).Sum(x => x.closing_value);
        //        //var _openingStock = context.item_transaction.Where(x => x.transaction_date < dateFrom).GroupBy(x=>x.item_id).Select(g=>new {itemid=g.Key }) .Sum(x => x.closing_value);
        //        var _openingStock = context.SP_OpeningStockPandL(dateFrom);
        //        decimal.TryParse(_openingStock.ToString(), out openingStock);
        //        // openingStock = Convert.ToDecimal(_openingStock.ToString());
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return openingStock;
        //}
        //private decimal Sale(betaskdbEntities context, DateTime dateFrom, DateTime dateTo,out List<account_transaction> listAccountReUse)
        //{
        //    decimal sales = 0;
        //    try
        //    {
        //        List<account_transaction> listAccount = new List<account_transaction>();
        //        var saleLedgers = ledgerMappingDAL.GetLegerMappingList(LedgerMappingDAL.EnumLedgerMap.SALEGROUP);
        //        foreach (ledger_mapping lm in saleLedgers)
        //        {
        //            int salesLedger = Convert.ToInt32(lm.ledger_id);

        //            listAccount.AddRange(context.account_transaction.AsNoTracking().Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.status == 1 && x.ledger_id == salesLedger ).ToList());
        //        }
        //        if (listAccount != null)
        //            decimal.TryParse(listAccount.Sum(x => x.credit).ToString(), out sales);
        //        listAccountReUse = listAccount;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return sales;
        //}
        private decimal Sale(betaskdbEntities context, DateTime dateFrom, DateTime dateTo, out List<account_transaction> listAccountReUse)
        {
            listAccountReUse = null;
            var saleAmount = context.Database.SqlQuery<decimal>(
     "EXEC SP_GetSalesTotal @DateFrom, @DateTo",
     new SqlParameter("@DateFrom", dateFrom),
     new SqlParameter("@DateTo", dateTo)
 ).FirstOrDefault();
            return saleAmount;
        }
        private decimal VATCollected(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            decimal sales = 0;
            try
            {

                int vatLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.VATONSALE).ledger_id);
                List<account_transaction> listAccount = context.account_transaction.Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.status == 1 && x.ledger_id == vatLedger /*&& x.transaction_type.ToLower() == "sale"*/).ToList();
                if (listAccount != null)
                    decimal.TryParse(listAccount.Sum(x => x.credit).ToString(), out sales);
            }
            catch
            {
                throw;
            }
            return sales;
        }
        private decimal VATPaid(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            decimal sales = 0;
            try
            {

                int vatLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.VATONPURCHASE).ledger_id);
                List<account_transaction> listAccount = context.account_transaction.Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.status == 1 && x.ledger_id == vatLedger /*&& x.transaction_type.ToLower() == "purchase"*/).ToList();
                if (listAccount != null)
                    decimal.TryParse(listAccount.Sum(x => x.debit).ToString(), out sales);
            }
            catch
            {
                throw;
            }
            return sales;
        }
        private decimal SaleReturn(betaskdbEntities context, DateTime dateFrom, DateTime dateTo, List<account_transaction> listAccount)
        {
            //decimal sales = 0;
            //try
            //{
            //    if (listAccount != null)
            //        decimal.TryParse(listAccount.Sum(x => x.debit).ToString(), out sales);
            //}
            //catch
            //{
            //    throw;
            //}
            var saleAmount = context.Database.SqlQuery<decimal>(
     "EXEC SP_GetSalesReturnTotal @DateFrom, @DateTo",
     new SqlParameter("@DateFrom", dateFrom),
     new SqlParameter("@DateTo", dateTo)
 ).FirstOrDefault();
            return saleAmount;
        }
        private decimal Purchase(betaskdbEntities context, DateTime dateFrom, DateTime dateTo, out List<account_transaction> listAccountReUse)
        {
            decimal purchase = 0;
            try
            {
                List<account_transaction> listAccount = new List<account_transaction>();
                var purchaseLedgers = ledgerMappingDAL.GetLegerMappingList(LedgerMappingDAL.EnumLedgerMap.PURCHASEGROUP);
                foreach (ledger_mapping lm in purchaseLedgers)
                {
                    int purchaseLedger = Convert.ToInt32(lm.ledger_id);
                    listAccount.AddRange(context.account_transaction.AsNoTracking().Where(x => (x.transaction_date >= dateFrom && x.transaction_date <= dateTo) && x.status == 1 && x.ledger_id == purchaseLedger).ToList());
                }
                if (listAccount != null)
                    decimal.TryParse(listAccount.Sum(x => x.debit).ToString(), out purchase);
                listAccountReUse = listAccount;
            }
            catch (Exception ex)
            {
                throw;
            }
            return Math.Round(purchase, 3);
        }
        private decimal PurchaseReturn(betaskdbEntities context, DateTime dateFrom, DateTime dateTo, List<account_transaction> listAccount)
        {
            decimal purchase = 0;
            try
            {

                if (listAccount != null)
                    decimal.TryParse(listAccount.Sum(x => x.credit).ToString(), out purchase);
            }
            catch
            {
                throw;
            }
            return Math.Round(purchase, 3);
        }
        public decimal ClosingStock(DateTime dateFrom, DateTime dateTo)
        {
            decimal closingStock = 0;
            try
            {

                try
                {

                    using (var context = new betaskdbEntities())
                    {

                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(context.Database.Connection.ConnectionString))
                        {
                            conn.Open();
                            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SP_OpeningStockPandL", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@date", dateTo);

                                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                                {

                                    if (reader.HasRows)
                                    {
                                        if (reader.Read())
                                        {
                                            decimal.TryParse(reader["ClosingValue"].ToString(), out closingStock);
                                        }
                                    }

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
            catch
            {
                throw;
            }
            return closingStock;
        }
        public decimal OpeningStock(DateTime dateFrom, DateTime dateTo)
        {
            decimal openingStock = 0;
            try
            {

                try
                {

                    using (var context = new betaskdbEntities())
                    {

                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(context.Database.Connection.ConnectionString))
                        {
                            conn.Open();
                            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SP_OpeningStockPandL", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@date", dateFrom);

                                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                                {

                                    if (reader.HasRows)
                                    {
                                        if (reader.Read())
                                        {
                                            decimal.TryParse(reader["ClosingValue"].ToString(), out openingStock);
                                        }
                                    }

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
            catch
            {
                throw;
            }
            return openingStock;
        }
        private List<ProfitandLossModel> GetAccountTransactionTypeList(Enum transactionType, betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> listAccountTransaction = new List<ProfitandLossModel>();
            List<ProfitandLossModel> _pl = new List<ProfitandLossModel>();
            switch (transactionType)
            {
                case AccountTypes.DIRECTEXPENSE:
                    _pl = (DirectExpense(context, dateFrom, dateTo));
                    foreach (ProfitandLossModel pl in _pl)
                    {
                        listAccountTransaction.Add(pl);
                    }
                    break;
                case AccountTypes.INDIRECTEXPENCE:
                    _pl = (InDirectExpense(context, dateFrom, dateTo));
                    foreach (ProfitandLossModel pl in _pl)
                    {
                        listAccountTransaction.Add(pl);
                    }
                    break;
                case AccountTypes.INDIRECTINCOME:
                    _pl = (INDirectIncome(context, dateFrom, dateTo));
                    foreach (ProfitandLossModel pl in _pl)
                    {
                        listAccountTransaction.Add(pl);
                    }
                    break;
                case AccountTypes.DIRECTINCOME:
                    _pl = (DirectIncome(context, dateFrom, dateTo));
                    foreach (ProfitandLossModel pl in _pl)
                    {
                        listAccountTransaction.Add(pl);
                    }
                    break;
            }
            return listAccountTransaction;
        }
        private List<ProfitandLossModel> DirectExpense(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> listAccountTransaction = new List<ProfitandLossModel>();
            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "DIRECTEXPENSE" && x.status == 1).FirstOrDefault();
            if (xPar != null)
            {
                int parentId = Convert.ToInt32(xPar.group_id);
                if (parentId > 0)
                {
                    List<int> accountId = new List<int>();
                    List<int> ledgerId = new List<int>();

                    accountId = context.account_group.Where(x => x.parent_id == parentId).Select(x => x.group_id).ToList();
                    if (accountId != null && accountId.Count > 0)
                    {

                        foreach (int i in accountId)
                        {
                            //checking again it work as sub account
                            if (context.account_group.AsNoTracking().Where(x => x.parent_id == i && x.status == 1).ToList().Count == 0)
                            {
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                            }
                            //If again it as parent
                            else
                            {
                                List<int> subAccountId = context.account_group.Where(x => x.parent_id == i).Select(x => x.group_id).ToList();
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                                _pl.Clear();
                                foreach (int j in subAccountId)
                                {
                                    //checking again it work as sub account
                                    if (context.account_group.AsNoTracking().Where(x => x.parent_id == j && x.status == 1).ToList().Count == 0)
                                    {
                                        _pl = (GetAccountTransactionTypeListLedgers(j, context, dateFrom, dateTo));
                                        foreach (ProfitandLossModel pl in _pl)
                                        {
                                            listAccountTransaction.Add(pl);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return listAccountTransaction;
        }

        private List<ProfitandLossModel> InDirectExpense(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> listAccountTransaction = new List<ProfitandLossModel>();
            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "INDIRECTEXPENSE" && x.status == 1).FirstOrDefault();
            if (xPar != null)
            {
                int parentId = Convert.ToInt32(xPar.group_id);
                if (parentId > 0)
                {
                    List<int> accountId = new List<int>();
                    List<int> ledgerId = new List<int>();

                    accountId = context.account_group.Where(x => x.parent_id == parentId).Select(x => x.group_id).ToList();
                    if (accountId != null && accountId.Count > 0)
                    {

                        foreach (int i in accountId)
                        {
                            //checking again it work as sub account
                            if (context.account_group.AsNoTracking().Where(x => x.parent_id == i && x.status == 1).ToList().Count == 0)
                            {
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                            }
                            //If again it as parent
                            else
                            {
                                List<int> subAccountId = context.account_group.Where(x => x.parent_id == i).Select(x => x.group_id).ToList();
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                                _pl.Clear();

                                foreach (int j in subAccountId)
                                {
                                    //checking again it work as sub account
                                    if (context.account_group.AsNoTracking().Where(x => x.parent_id == j && x.status == 1).ToList().Count == 0)
                                    {
                                        _pl = (GetAccountTransactionTypeListLedgers(j, context, dateFrom, dateTo));
                                        foreach (ProfitandLossModel pl in _pl)
                                        {
                                            listAccountTransaction.Add(pl);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return listAccountTransaction;
        }

        private List<ProfitandLossModel> INDirectIncome(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> listAccountTransaction = new List<ProfitandLossModel>();
            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "INDIRECTEINCOME" && x.status == 1).FirstOrDefault();
            if (xPar != null)
            {
                int parentId = Convert.ToInt32(xPar.group_id);
                if (parentId > 0)
                {
                    List<int> accountId = new List<int>();
                    List<int> ledgerId = new List<int>();

                    accountId = context.account_group.Where(x => x.parent_id == parentId).Select(x => x.group_id).ToList();
                    if (accountId != null && accountId.Count > 0)
                    {

                        foreach (int i in accountId)
                        {
                            //checking again it work as sub account
                            if (context.account_group.AsNoTracking().Where(x => x.parent_id == i && x.status == 1).ToList().Count == 0)
                            {
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgersIncome(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                            }
                            //If again it as parent
                            else
                            {
                                List<int> subAccountId = context.account_group.Where(x => x.parent_id == i).Select(x => x.group_id).ToList();
                                //List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgersIncome(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                                _pl.Clear();
                                foreach (int j in subAccountId)
                                {
                                    //checking again it work as sub account
                                    if (context.account_group.AsNoTracking().Where(x => x.parent_id == j && x.status == 1).ToList().Count == 0)
                                    {
                                        //_pl = (GetAccountTransactionTypeListLedgers(j, context, dateFrom, dateTo));
                                        _pl = (GetAccountTransactionTypeListLedgersIncome(j, context, dateFrom, dateTo));
                                        foreach (ProfitandLossModel pl in _pl)
                                        {
                                            listAccountTransaction.Add(pl);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return listAccountTransaction;
        }
        private List<ProfitandLossModel> DirectIncome(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> listAccountTransaction = new List<ProfitandLossModel>();
            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "DIRECTEINCOME" && x.status == 1).FirstOrDefault();
            if (xPar != null)
            {
                int parentId = Convert.ToInt32(xPar.group_id);
                if (parentId > 0)
                {
                    List<int> accountId = new List<int>();
                    List<int> ledgerId = new List<int>();

                    accountId = context.account_group.Where(x => x.parent_id == parentId).Select(x => x.group_id).ToList();
                    if (accountId != null && accountId.Count > 0)
                    {

                        foreach (int i in accountId)
                        {
                            //checking again it work as sub account
                            if (context.account_group.AsNoTracking().Where(x => x.parent_id == i && x.status == 1).ToList().Count == 0)
                            {
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgersIncome(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                            }
                            //If again it as parent
                            else
                            {
                                List<int> subAccountId = context.account_group.Where(x => x.parent_id == i).Select(x => x.group_id).ToList();
                                List<ProfitandLossModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                foreach (ProfitandLossModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }
                                _pl.Clear();
                                foreach (int j in subAccountId)
                                {
                                    //checking again it work as sub account
                                    if (context.account_group.AsNoTracking().Where(x => x.parent_id == j && x.status == 1).ToList().Count == 0)
                                    {
                                        _pl = (GetAccountTransactionTypeListLedgers(j, context, dateFrom, dateTo));
                                        foreach (ProfitandLossModel pl in _pl)
                                        {
                                            listAccountTransaction.Add(pl);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return listAccountTransaction;
        }

        private List<ProfitandLossModel> GetAccountTransactionTypeListLedgers(int groupId, betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            int purchaseLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.PURCHASE).ledger_id);
            int salesLedger = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.SALE).ledger_id);

            List<ProfitandLossModel> profitandLoss = new List<ProfitandLossModel>();
            try
            {
                string group = context.account_group.Where(x => x.group_id == groupId).FirstOrDefault().group_name;
                profitandLoss.Add(new ProfitandLossModel
                {
                    amount1 = 0,
                    amount2 = -1,
                    amount3 = -1,
                    Description = $"{group.PadLeft(5, ' ')}#"
                });
                List<int> ledgerIds = LedgerList.Where(x => x.group_id == groupId && x.status == 1).Select(x => x.ledger_id).ToList();
                List<account_transaction> listTransaction = null;
                if (ledgerIds != null && ledgerIds.Count > 0)
                {
                    decimal total = 0;
                    foreach (int id in ledgerIds)
                    {
                        if (id != purchaseLedger && id != salesLedger)
                        {
                            listTransaction = context.account_transaction.Where(x => x.status == 1 && x.ledger_id == id && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.transaction_type != "OPENING").ToList();
                            if (listTransaction != null && listTransaction.Count > 0)
                            {
                                decimal debit, credit, _amount = 0;
                                decimal.TryParse(listTransaction.Sum(x => x.debit).ToString(), out debit);
                                decimal.TryParse(listTransaction.Sum(x => x.credit).ToString(), out credit);
                                _amount = debit - credit;
                                total = total + _amount;
                                profitandLoss.Add(new ProfitandLossModel
                                {
                                    amount1 = _amount,
                                    amount2 = 0,
                                    amount3 = 0,
                                    Description = LedgerList.Where(x => x.ledger_id == id && x.status == 1).FirstOrDefault().ledger_name.PadLeft(10, ' '),
                                });
                            }
                        }
                    }
                    profitandLoss[0].amount3 = total;

                }
            }
            catch { throw; }
            return profitandLoss;

        }
        private List<ProfitandLossModel> GetAccountTransactionTypeListLedgersIncome(int groupId, betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<ProfitandLossModel> profitandLoss = new List<ProfitandLossModel>();
            try
            {
                List<int> ledgerIds = LedgerList.Where(x => x.group_id == groupId && x.status == 1).Select(x => x.ledger_id).ToList();
                List<account_transaction> listTransaction = null;
                if (ledgerIds != null && ledgerIds.Count > 0)
                {
                    foreach (int id in ledgerIds)
                    {
                        listTransaction = context.account_transaction.Where(x => x.status == 1 && x.ledger_id == id && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).ToList();
                        if (listTransaction != null && listTransaction.Count > 0)
                        {
                            decimal debit = 0, credit = 0, _amount = 0;
                            decimal.TryParse(listTransaction.Sum(x => x.credit).ToString(), out credit);
                            decimal.TryParse(listTransaction.Sum(x => x.debit).ToString(), out debit);
                            _amount = credit - debit;
                            profitandLoss.Add(new ProfitandLossModel
                            {
                                amount1 = _amount,
                                amount2 = 0,
                                amount3 = 0,
                                Description = LedgerList.Where(x => x.ledger_id == id && x.status == 1).FirstOrDefault().ledger_name
                            });
                        }
                    }
                }
            }
            catch { throw; }
            return profitandLoss;

        }




    }
}
