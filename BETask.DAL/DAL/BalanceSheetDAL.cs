using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BETask.DAL.Model;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;

namespace BETask.DAL.DAL
{
    public class BalanceSheetDAL
    {
        public List<BalanceSheetModel> BalanceSheet(DateTime dateFrom, DateTime dateTo, decimal currentProfit = 0)
        {
            List<BalanceSheetModel> listBalanceSheet = new List<BalanceSheetModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {

                    context.Database.CommandTimeout = 1500;
                    List<BalanceSheetModel> listAsset = CreateBalanceSheetAsset(context, dateFrom, dateTo);
                    List<BalanceSheetModel> listLiability = CreateBalanceSheetLiability(context, dateFrom, dateTo, currentProfit);

                    if (listAsset != null && listAsset.Count > 0)
                    {
                        foreach (BalanceSheetModel bal in listAsset)
                        {
                            listBalanceSheet.Add(new BalanceSheetModel
                            {
                                Asset = bal.Asset,
                                AssetAmount = bal.AssetAmount,

                            });
                        }


                    }

                    if (listLiability != null && listLiability.Count > 0)
                    {
                        foreach (BalanceSheetModel bal in listLiability)
                        {
                            listBalanceSheet.Add(new BalanceSheetModel
                            {
                                Liability = bal.Liability,
                                LiabliltyAmount = bal.LiabliltyAmount,

                            });
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return listBalanceSheet;
        }
        public List<BalanceSheetModel> GetAccountBalanceAsset(betaskdbEntities context, List<account_group> listAssetGroups, DateTime dateFrom, DateTime dateTo)
        {
            List<BalanceSheetModel> listBalance = new List<BalanceSheetModel>();
            try
            {
                foreach (account_group ag in listAssetGroups)
                {
                    if (ag != null)
                    {
                        List<account_transaction> listTransactions = context.account_transaction.AsNoTracking().Include(l => l.account_ledger).Where(x => x.account_ledger.group_id == ag.group_id && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.status == 1).ToList();
                        if (listTransactions != null)
                        {
                            decimal debit = 0, credit = 0, balance = 0;
                            debit = listTransactions.Sum(x => x.debit);
                            credit = listTransactions.Sum(x => x.credit);
                            balance = debit - credit;
                            if (balance != 0)
                            {
                                listBalance.Add(new BalanceSheetModel
                                {
                                    Asset = ag.group_name,
                                    AssetAmount = balance
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
            return listBalance;
        }
        private List<BalanceSheetModel> GetAccountTransactionTypeListLedgers(int groupId, betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<BalanceSheetModel> balnceSheet = new List<BalanceSheetModel>();
            try
            {
                string group = context.account_group.Where(x => x.group_id == groupId).FirstOrDefault().group_name;
                balnceSheet.Add(new BalanceSheetModel
                {

                    Asset = group.PadLeft(5, ' ')

                });
                List<int> ledgerIds = context.account_ledger.Where(x => x.group_id == groupId && x.status == 1).Select(x => x.ledger_id).ToList();
                List<account_transaction> listTransaction = null;
                if (ledgerIds != null && ledgerIds.Count > 0)
                {
                    decimal total = 0;
                    foreach (int id in ledgerIds)
                    {
                        listTransaction = context.account_transaction.Where(x => x.status == 1 && x.ledger_id == id && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.transaction_type != "OPENING").ToList();
                        if (listTransaction != null && listTransaction.Count > 0)
                        {
                            decimal _amount = 0;
                            decimal.TryParse(listTransaction.Sum(x => x.debit).ToString(), out _amount);
                            total = total + _amount;
                            balnceSheet.Add(new BalanceSheetModel
                            {
                                AssetAmount = _amount,

                                Asset = context.account_ledger.Where(x => x.ledger_id == id && x.status == 1).FirstOrDefault().ledger_name.PadLeft(10, ' '),
                            });
                        }
                    }
                    balnceSheet[0].LiabliltyAmount = total;


                }
            }
            catch { throw; }
            return balnceSheet;

        }
        public List<account_group> GetParents(betaskdbEntities context, int Id)
        {
            List<account_group> listGroup = new List<account_group>();
            try
            {

                List<int> listParentIds = new List<int>();

                //under asset
                List<int> listP1 = context.account_group.Where(x => x.parent_id == Id).Select(x => x.group_id).ToList();

                foreach (int p1 in listP1)
                {
                    //under asset 1
                    List<int> listP2 = context.account_group.Where(x => x.parent_id == p1).Select(x => x.group_id).ToList();

                    foreach (int p2 in listP2)
                    {
                        if (context.account_ledger.Where(x => x.group_id == p2 && x.status == 1).ToList().Count > 0)
                        {
                            account_group gp = context.account_group.AsNoTracking().Where(x => x.group_id == p2 && x.status == 1).FirstOrDefault();
                            if (gp != null)
                                listGroup.Add(gp);

                            //under asset 2
                            List<int> listP3 = context.account_group.Where(x => x.parent_id == p2).Select(x => x.group_id).ToList();
                            if (listP3 != null && listP3.Count > 0)
                            {
                                foreach (int p3 in listP3)
                                {
                                    if (context.account_ledger.Where(x => x.group_id == p3 && x.status == 1).ToList().Count > 0)
                                    {
                                        gp = context.account_group.AsNoTracking().Where(x => x.group_id == p3 && x.status == 1).FirstOrDefault();
                                        if (gp != null)
                                            listGroup.Add(gp);
                                    }
                                }

                            }

                        }
                        else
                        {
                            account_group gp = context.account_group.AsNoTracking().Where(x => x.group_id == p2 && x.status == 1).FirstOrDefault();
                            if (gp != null)
                                listGroup.Add(gp);

                            //under asset 2
                            List<int> listP3 = context.account_group.Where(x => x.parent_id == p2).Select(x => x.group_id).ToList();
                            if (listP3 != null && listP3.Count > 0)
                            {
                                foreach (int p3 in listP3)
                                {
                                    if (context.account_ledger.Where(x => x.group_id == p3 && x.status == 1).ToList().Count > 0)
                                    {
                                        gp = context.account_group.AsNoTracking().Where(x => x.group_id == p3 && x.status == 1).FirstOrDefault();
                                        if (gp != null)
                                            listGroup.Add(gp);
                                    }
                                }

                            }
                        }
                    }
                }

                var xPar = context.ledger_mapping.Where(x => x.ledger_type == "ACCUMULATED" && x.status == 1).FirstOrDefault();
                if (xPar != null)
                {

                    if (xPar.group_id != null)
                    {
                        account_group acc = new account_group();
                        foreach (account_group ac in listGroup)
                        {
                            if (xPar.group_id == ac.group_id)
                            {
                                acc = ac;
                                break;
                            }
                        }
                        if (acc != null && acc.group_name != null)
                        {
                            bool remove = listGroup.Remove(acc);
                        }
                    }
                }

            }
            catch { throw; }
            return listGroup;
        }

        public List<BalanceSheetModel> CreateBalanceSheetAsset(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            decimal netBookvalue = 0;
            List<BalanceSheetModel> listBalance = new List<BalanceSheetModel>();
            List<account_group> listGroup = new List<account_group>();
            try
            {
                //   decimal
                var xAsset = context.ledger_mapping.Where(x => x.ledger_type == "ASSET" && x.status == 1).FirstOrDefault();

                if (xAsset != null)
                {
                    int assetId = Convert.ToInt32(xAsset.group_id);

                    List<int> listParentIds = new List<int>();

                    int Id = assetId;

                    //Step 1
                    List<account_group> listP1 = context.account_group.Where(x => x.parent_id == Id).ToList();

                    foreach (account_group g1 in listP1)
                    {
                        int p1 = g1.group_id;
                        listBalance.Add(new BalanceSheetModel
                        {
                            Asset = $"     {g1.group_name}"
                        });
                        int headIdx = listBalance.Count - 1;
                        decimal headAmount = 0;

                        //Step 2
                        List<account_group> listP2 = context.account_group.Where(x => x.parent_id == p1).ToList();

                        foreach (account_group g2 in listP2)
                        {
                            int p2 = g2.group_id;
                            decimal assetAmount = 0;
                            int depreciationGroup = 0;
                            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "ACCUMULATED" && x.status == 1).FirstOrDefault();
                            if (xPar != null)
                                depreciationGroup = Convert.ToInt32(xPar.group_id);

                            if (p2 != depreciationGroup)// if not depreciation
                            {

                                //List<account_transaction> listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p2 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).ToList();
                                List<account_transaction> listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p2 && x.transaction_date <= dateTo).ToList();

                                List<account_ledger> listLedgers = context.account_ledger.Where(x => x.group_id == p2 && x.status == 1).ToList();


                                if (listTransaction != null && listTransaction.Count > 0)
                                {
                                    decimal _amount = 0;
                                    decimal.TryParse((listTransaction.Sum(x => x.debit) - listTransaction.Sum(x => x.credit)).ToString(), out _amount);
                                    assetAmount = assetAmount + _amount;

                                }
                                listBalance.Add(new BalanceSheetModel
                                {
                                    Asset = $"          {g2.group_name}.",
                                    AssetAmount = assetAmount
                                });
                                headAmount += assetAmount;


                                List<account_group> listP3 = context.account_group.Where(x => x.parent_id == p2).ToList();
                                if (listP3 != null && listP3.Count > 0)
                                {

                                    foreach (account_group g3 in listP3)
                                    {
                                        int p3 = g3.group_id;
                                        //  listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p3 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).ToList();
                                        listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p3 && x.transaction_date <= dateTo).ToList();
                                        if (listTransaction != null && listTransaction.Count > 0)
                                        {
                                            decimal _amount = 0;
                                            //decimal.TryParse((listTransaction.Sum(x => x.credit) - listTransaction.Sum(x => x.debit)).ToString(), out _amount);
                                            decimal.TryParse((listTransaction.Sum(x => x.debit) - listTransaction.Sum(x => x.credit)).ToString(), out _amount);
                                            assetAmount = assetAmount + _amount;

                                        }
                                        listBalance.Add(new BalanceSheetModel
                                        {
                                            Liability = $"          {g3.group_name}.",
                                            LiabliltyAmount = assetAmount
                                        });
                                        headAmount += assetAmount;
                                        assetAmount = 0;
                                    }
                                }
                            }


                        }
                        listBalance[headIdx].AssetAmount = headAmount;


                        if (listBalance[headIdx].Asset.Contains("Fixed Asset") || listBalance[headIdx].Asset.Contains("Fixed Assets"))
                        {

                            decimal depreciation = 0;
                            depreciation = Depreciation(context, dateFrom, dateTo);
                            listBalance.Add(new BalanceSheetModel
                            {
                                Asset = $"    Less Accumulated Depreciation",
                                AssetAmount = depreciation * -1
                            });
                            netBookvalue -= depreciation;
                        }

                        if (listBalance[headIdx].Asset.Contains("Current Asset") || listBalance[headIdx].Asset.Contains("Current Asset"))
                        {
                            ProfitAndLossDAL profitAndLoss = new ProfitAndLossDAL();
                            decimal closingStock = profitAndLoss.ClosingStock(dateFrom, dateTo.AddDays(1));

                            listBalance.Add(new BalanceSheetModel
                            {
                                Asset = $"    Closing Stock",
                                AssetAmount = closingStock
                            });
                            netBookvalue += closingStock;


                        }

                        //Net Book value
                        netBookvalue += headAmount;





                    }
                    listBalance.Add(new BalanceSheetModel
                    {
                        Asset = $"                 Total Assets",
                        AssetAmount = netBookvalue
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listBalance;
        }

        public List<BalanceSheetModel> CreateBalanceSheetLiability(betaskdbEntities context, DateTime dateFrom, DateTime dateTo, decimal currentProfit = 0)
        {
            decimal netBookvalue = 0;
            List<BalanceSheetModel> listBalance = new List<BalanceSheetModel>();
            List<account_group> listGroup = new List<account_group>();
            try
            {
                var xAsset = context.ledger_mapping.Where(x => x.ledger_type == "LIABILITY" && x.status == 1).FirstOrDefault();
                var pandLMap = context.ledger_mapping.Where(x => x.ledger_type == "PROFITLOSS" && x.status == 1).FirstOrDefault();

                if (xAsset != null)
                {
                    int assetId = Convert.ToInt32(xAsset.group_id);

                    List<int> listParentIds = new List<int>();

                    int Id = assetId;

                    //Step 1
                    List<account_group> listP1 = context.account_group.Where(x => x.parent_id == Id).ToList();

                    foreach (account_group g1 in listP1)
                    {
                        int p1 = g1.group_id;
                        listBalance.Add(new BalanceSheetModel
                        {
                            Liability = $"     {g1.group_name}"
                        });
                        int headIdx = listBalance.Count - 1;
                        decimal headAmount = 0;

                        //Step 2
                        List<account_group> listP2 = context.account_group.Where(x => x.parent_id == p1).ToList();

                        foreach (account_group g2 in listP2)
                        {
                            int p2 = g2.group_id;
                            decimal assetAmount = 0;

                            {

                                // List<account_transaction> listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p2 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).ToList();
                                List<account_transaction> listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p2 && x.transaction_date <= dateTo).ToList();


                                if (listTransaction != null && listTransaction.Count > 0)
                                {
                                    decimal _amount = 0;
                                    decimal.TryParse((listTransaction.Sum(x => x.credit) - listTransaction.Sum(x => x.debit)).ToString(), out _amount);
                                    assetAmount = assetAmount + _amount;
                                    listBalance.Add(new BalanceSheetModel
                                    {
                                        Liability = $"          {g2.group_name}.",
                                        LiabliltyAmount = assetAmount
                                    });
                                    headAmount += assetAmount;
                                    assetAmount = 0;

                                }

                                /*Profit and loss adding below*/
                                #region profitlossadding
                                //Adding current year profit and loss
                                if (pandLMap != null && pandLMap.group_id == p2)
                                {

                                    bool pandlExist = (context.account_transaction.Any(x => x.transaction_date == dateTo && x.ledger_id == pandLMap.ledger_id && x.status == 1));
                                    if (pandlExist)
                                        currentProfit = 0;

                                    //Old profit Head and value
                                    listBalance.Add(new BalanceSheetModel
                                    {
                                        Liability = $"       Profit and Loss",
                                        LiabliltyAmount = assetAmount + currentProfit
                                    });



                                    //Old profit and loss
                                    listBalance.Add(new BalanceSheetModel
                                    {
                                        Liability = $"          {g2.group_name}.",
                                        LiabliltyAmount = assetAmount
                                    });
                                    headAmount += assetAmount + currentProfit;

                                    if (currentProfit > 0 && !pandlExist)
                                    {
                                        listBalance.Add(new BalanceSheetModel
                                        {
                                            Liability = $"          Profit and loss current period.",
                                            LiabliltyAmount = currentProfit
                                        });
                                    }
                                }
                                #endregion profitlossadding


                                List<account_group> listP3 = context.account_group.Where(x => x.parent_id == p2).ToList();
                                if (listP3 != null && listP3.Count > 0)
                                {

                                    foreach (account_group g3 in listP3)
                                    {
                                        int p3 = g3.group_id;
                                        // listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p3 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo).ToList();
                                        listTransaction = context.account_transaction.Include(l => l.account_ledger).Where(x => x.status == 1 && x.account_ledger.group_id == p3 && x.transaction_date <= dateTo).ToList();
                                        if (listTransaction != null && listTransaction.Count > 0)
                                        {
                                            decimal _amount = 0;
                                            decimal.TryParse((listTransaction.Sum(x => x.credit) - listTransaction.Sum(x => x.debit)).ToString(), out _amount);
                                            assetAmount = assetAmount + _amount;

                                        }
                                        listBalance.Add(new BalanceSheetModel
                                        {
                                            Liability = $"          {g3.group_name}.",
                                            LiabliltyAmount = assetAmount
                                        });
                                        headAmount += assetAmount;
                                        assetAmount = 0;
                                    }
                                }
                            }


                        }
                        listBalance[headIdx].LiabliltyAmount = headAmount;


                        //Net Book value
                        netBookvalue += headAmount;

                    }
                    listBalance.Add(new BalanceSheetModel
                    {
                        Liability = $"                 Total Liabilities",
                        LiabliltyAmount = netBookvalue
                    });
                }
            }
            catch { throw; }
            return listBalance;
        }

        private List<BalanceSheetModel> Assets(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            List<BalanceSheetModel> listAccountTransaction = new List<BalanceSheetModel>();
            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "ASSET" && x.status == 1).FirstOrDefault();
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

                            {
                                List<int> subAccountId = context.account_group.Where(x => x.parent_id == i).Select(x => x.group_id).ToList();

                                //Adding ledgers 1st from subaccount
                                List<BalanceSheetModel> _pl = (GetAccountTransactionTypeListLedgers(i, context, dateFrom, dateTo));
                                foreach (BalanceSheetModel pl in _pl)
                                {
                                    listAccountTransaction.Add(pl);
                                }


                                _pl.Clear();

                                foreach (int j in subAccountId)
                                {
                                    //Adding ledgers 2nd from subaccount
                                    _pl = (GetAccountTransactionTypeListLedgers(j, context, dateFrom, dateTo));
                                    foreach (BalanceSheetModel pl in _pl)
                                    {
                                        listAccountTransaction.Add(pl);
                                    }
                                    _pl.Clear();

                                    //Adding ledgers 3rd from subaccount
                                    if (context.account_group.AsNoTracking().Where(x => x.parent_id == j && x.status == 1).ToList().Count > 0)
                                    {
                                        List<int> subAccountId_1 = context.account_group.Where(x => x.parent_id == j).Select(x => x.group_id).ToList();
                                        foreach (int k in subAccountId_1)
                                        {
                                            _pl = (GetAccountTransactionTypeListLedgers(k, context, dateFrom, dateTo));
                                            foreach (BalanceSheetModel pl in _pl)
                                            {
                                                listAccountTransaction.Add(pl);
                                            }
                                            _pl.Clear();
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

        private decimal Depreciation(betaskdbEntities context, DateTime dateFrom, DateTime dateTo)
        {
            decimal depreciation = 0;
            var xPar = context.ledger_mapping.Where(x => x.ledger_type == "ACCUMULATED" && x.status == 1).FirstOrDefault();
            if (xPar != null)
            {
                if (xPar.group_id != null)
                {
                    int groupId = Convert.ToInt32(xPar.group_id);
                    if (groupId > 0)
                    {
                        List<account_ledger> listLedger = context.account_ledger.Where(x => x.group_id == groupId && x.status == 1).ToList();
                        if (listLedger != null && listLedger.Count > 0)
                        {
                            foreach (account_ledger lg in listLedger)
                            {
                                int ledgerId = lg.ledger_id;
                                List<account_transaction> listTransaction = context.account_transaction.Where(x => x.ledger_id == ledgerId && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.status == 1).ToList();
                                if (listTransaction != null && listTransaction.Count > 0)
                                {
                                    decimal credit = 0;
                                    credit = listTransaction.Sum(x => x.credit);
                                    depreciation += credit;
                                }
                            }
                        }
                    }
                }
            }
            return depreciation;
        }
    }
}
