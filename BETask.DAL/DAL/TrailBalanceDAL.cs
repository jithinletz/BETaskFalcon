using System;
using System.Collections.Generic;
using System.Linq;
using BETask.DAL.EDMX;
using System.Data;
using BETask.DAL.Model;
using System.Data.SqlClient;

namespace BETask.DAL.DAL
{
    public class TrailBalanceDAL
    {
        string plGroups = "";

        public List<TrailBalanceModel> TrialBalance(DateTime dateFrom, DateTime dateTo, bool opening = true)
        {
            List<TrailBalanceModel> listTrial = new List<TrailBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;

                    ProfitAndLossDAL pl = new ProfitAndLossDAL();
                    //decimal closingStock = pl.ClosingStock(dateFrom, dateTo.AddDays(1));
                    var comp = context.company.Where(x => x.status == 1).FirstOrDefault();
                    var sett = context.system_settings.FirstOrDefault(x => x.status == 1);

                    DateTime xStart = comp.financial_from_date.AddYears(-1);
                    DateTime xEnd = comp.financial_to_date.AddYears(-1);
                    //  decimal openingStock = CurrentyearOpeningStock(xStart, xEnd, context);
                    ProfitAndLossDAL profit = new ProfitAndLossDAL { };
                    decimal openingStock = OpeningStock(sett.software_startdate, sett.software_startdate);
                    openingStock = 0;
                    listTrial.Add(new TrailBalanceModel
                    {
                        Description = "Opening Stock",
                        Debit = openingStock,
                        Credit = 0,
                    });

                    //Getting summary
                    List<TrailBalanceModel> listSumamry = Summary(dateTo, context, opening);
                    foreach (TrailBalanceModel rs in listSumamry)
                    {
                        if (rs != null)
                        {
                            listTrial.Add(new TrailBalanceModel
                            {
                                Description = rs.Description,
                                Debit = rs.Debit,
                                Credit = rs.Credit,
                            });
                        }
                    }

                }
            }
            catch { throw; }


            return listTrial;
        }

        public List<TrailBalanceDatewiseModel> TrialBalanceDatewise(DateTime dateFrom, DateTime dateTo, bool opening = true)
        {
            List<TrailBalanceDatewiseModel> listTrial = new List<TrailBalanceDatewiseModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;

                    ProfitAndLossDAL pl = new ProfitAndLossDAL();
                    //decimal closingStock = pl.ClosingStock(dateFrom, dateTo.AddDays(1));
                    var comp = context.company.Where(x => x.status == 1).FirstOrDefault();
                    var sett = context.system_settings.FirstOrDefault(x => x.status == 1);

                    DateTime xStart = comp.financial_from_date.AddYears(-1);
                    DateTime xEnd = comp.financial_to_date.AddYears(-1);
                    //  decimal openingStock = CurrentyearOpeningStock(xStart, xEnd, context);
                    ProfitAndLossDAL profit = new ProfitAndLossDAL { };
                    decimal openingStock = OpeningStock(dateFrom, dateTo);
                    //openingStock = 0;
                    listTrial.Add(new TrailBalanceDatewiseModel
                    {
                        Description = "Opening Stock",
                        OpeningDebit = openingStock,
                        Debit = 0,
                        Credit = 0,
                    });

                    //Getting summary
                    List<TrailBalanceDatewiseModel> listSumamry = SummaryDatewise(dateFrom, dateTo, context, opening);
                    foreach (TrailBalanceDatewiseModel rs in listSumamry)
                    {
                        if (rs != null)
                        {
                            listTrial.Add(new TrailBalanceDatewiseModel
                            {
                                Description = rs.Description,
                                Debit = rs.Debit,
                                Credit = rs.Credit,
                                OpeningDebit = rs.OpeningDebit,
                                OpeningCredit = rs.OpeningCredit,
                                ClosingDebit = rs.ClosingDebit,
                                ClosingCredit = rs.ClosingCredit
                            });
                        }
                    }

                }
            }
            catch { throw; }


            return listTrial;
        }

        /// <summary>
        /// Parent
        ///   Group1
        ///      Group2
        ///        Group3 
        ///            Ledger
        /// </summary>
        /// <param name="context"></param>
        private List<TrailBalanceModel> Summary(DateTime dateTo, betaskdbEntities context, bool opening = false)
        {
            List<TrailBalanceModel> listTrial = new List<TrailBalanceModel> { };
            try
            {
                List<account_group> listParents = context.account_group.AsNoTracking().Where(x => x.parent_id == 0).OrderBy(x => x.description).ToList();
                if (listParents != null && listParents.Count > 0)
                {
                    /*
                               Level 1 out of 5 
                               Asset,Libilty,Income,Expense
                     */
                    foreach (account_group par in listParents)
                    {
                        /*
                              Level 2 out of 5 
                        */
                        List<account_group> listGroup1 = context.account_group.AsNoTracking().Where(x => x.parent_id == par.group_id).OrderBy(x => x.group_name).ToList();

                        foreach (account_group group1 in listGroup1)
                        {
                            /*
                                  Level 3 out of 5 
                            */
                            List<account_group> listGroup2 = context.account_group.AsNoTracking().Where(x => x.parent_id == group1.group_id).OrderBy(x => x.group_name).ToList();

                            foreach (account_group group2 in listGroup2)
                            {
                                decimal totalDebit = 0, totalCredit = 0;

                                //Getting total 1
                                decimal credit = 0;
                                decimal debit = GetGroupTotal(dateTo, group2.group_id, context, out credit, opening);
                                totalDebit += debit;
                                totalCredit += credit;

                                //making zero
                                debit = 0;
                                credit = 0;

                                /*
                                      Level 4 out of 5 
                                */
                                List<account_group> listGroup3 = context.account_group.AsNoTracking().Where(x => x.parent_id == group2.group_id).OrderBy(x => x.group_name).ToList();

                                foreach (account_group group3 in listGroup3)
                                {
                                    credit = 0;
                                    debit = GetGroupTotal(dateTo, group3.group_id, context, out credit, opening);

                                    totalDebit += debit;
                                    totalCredit += credit;

                                    //making zero
                                    debit = 0;
                                    credit = 0;
                                }


                                //adding group3 to trial balance
                                if (totalDebit != 0 || totalCredit != 0)
                                {
                                    listTrial.Add(new TrailBalanceModel
                                    {
                                        Description = group2.group_name,
                                        Debit = totalDebit,
                                        Credit = totalCredit,
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
            return listTrial;
        }

        private List<TrailBalanceDatewiseModel> SummaryDatewise(DateTime dateFrom, DateTime dateTo, betaskdbEntities context, bool opening = false)
        {
            List<TrailBalanceDatewiseModel> listTrial = new List<TrailBalanceDatewiseModel> { };
            try
            {
                List<account_group> listParents = context.account_group.AsNoTracking().Where(x => x.parent_id == 0).OrderBy(x => x.description).ToList();
                if (listParents != null && listParents.Count > 0)
                {
                    /*
                               Level 1 out of 5 
                               Asset,Libilty,Income,Expense
                     */
                    foreach (account_group par in listParents)
                    {
                        /*
                              Level 2 out of 5 
                        */
                        List<account_group> listGroup1 = context.account_group.AsNoTracking().Where(x => x.parent_id == par.group_id).OrderBy(x => x.group_name).ToList();

                        foreach (account_group group1 in listGroup1)
                        {
                            /*
                                  Level 3 out of 5 
                            */
                            List<account_group> listGroup2 = context.account_group.AsNoTracking().Where(x => x.parent_id == group1.group_id).OrderBy(x => x.group_name).ToList();

                            foreach (account_group group2 in listGroup2)
                            {
                                if (group2.group_id == 74)
                                {
                                    string ss = "";
                                }
                                decimal totalDebit = 0, totalCredit = 0, totalOpDebit = 0, totalOpCredit = 0;

                                //Getting total 1
                                decimal credit = 0;
                                decimal debit = GetGroupTotalDatewise(dateFrom, dateTo, group2.group_id, context, out credit, opening);
                                totalDebit += debit;
                                totalCredit += credit;


                                //making zero
                                debit = 0;
                                credit = 0;

                                //Opening will take only for if parent group is asset or liability
                                if (par.group_id == 1 || par.group_id == 2)
                                {
                                    //opening
                                    debit = GetGroupTotalDatewise(dateFrom, dateTo, group2.group_id, context, out credit, opening, true);
                                    totalOpDebit += debit;
                                    totalOpCredit += credit;
                                    //making zero
                                    debit = 0;
                                    credit = 0;
                                }

                                /*
                                      Level 4 out of 5 
                                */
                                List<account_group> listGroup3 = context.account_group.AsNoTracking().Where(x => x.parent_id == group2.group_id).OrderBy(x => x.group_name).ToList();

                                foreach (account_group group3 in listGroup3)
                                {
                                    credit = 0;
                                    debit = GetGroupTotalDatewise(dateFrom, dateTo, group3.group_id, context, out credit, opening);

                                    totalDebit += debit;
                                    totalCredit += credit;

                                    //making zero
                                    debit = 0;
                                    credit = 0;

                                    //Opening will take only for if parent group is asset or liability
                                    if (par.group_id == 1 || par.group_id == 2)
                                    {
                                        //opening
                                        debit = GetGroupTotalDatewise(dateFrom, dateTo, group3.group_id, context, out credit, opening, true);
                                        totalOpDebit += debit;
                                        totalOpCredit += credit;
                                        //making zero
                                        debit = 0;
                                        credit = 0;
                                    }


                                }


                                //adding group3 to trial balance
                                if (totalDebit != 0 || totalCredit != 0)
                                {
                                    decimal finalDebit = 0, finalCredit = 0, finalOpDebit = 0, finalOpCredit = 0, closingDebit = 0, closingCredit = 0;
                                    if (totalDebit >= totalCredit)
                                    {
                                        finalDebit = totalDebit - totalCredit;
                                    }
                                    else
                                        finalCredit = totalCredit - totalDebit;

                                    if (totalOpDebit >= totalOpCredit)
                                    {
                                        finalOpDebit = totalOpDebit - totalOpCredit;
                                    }
                                    else
                                        finalOpCredit = totalOpCredit - totalOpDebit;

                                    decimal netDebit = 0, netCredit = 0;
                                    netDebit = (finalDebit + finalOpDebit);
                                    netCredit = (finalCredit + finalOpCredit);

                                    if (netDebit >= netCredit)
                                    {
                                        closingDebit = netDebit - netCredit;
                                    }
                                    else
                                        closingCredit = netCredit - netDebit;


                                    listTrial.Add(new TrailBalanceDatewiseModel
                                    {
                                        Description = group2.group_name,
                                        Debit = finalDebit,
                                        Credit = finalCredit,
                                        OpeningDebit = finalOpDebit,
                                        OpeningCredit = finalOpCredit,
                                        ClosingDebit = closingDebit,
                                        ClosingCredit = closingCredit
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
            return listTrial;
        }

        private decimal GetGroupTotal(DateTime dateTo, int groupId, betaskdbEntities context, out decimal credit, bool opening = true)
        {
            decimal debit = 0; credit = 0;
            try
            {
                string sql = " select group_name,sum(debit) as debit,sum(credit) as credit from " +
                              " (" +
                              " select b.ledger_name,c.group_name," +
                              " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                              " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                              " inner join account_ledger b on b.ledger_id=a.ledger_id and b.group_id=" + groupId + "" +
                              " inner join account_group c on c.group_id=b.group_id" +
                              " where  transaction_date<='" + dateTo.ToString("yyyy/MM/dd") + "' and a.status=1 " + (!opening ? " and transaction_type!='OPENING' " : "") +
                              " group by b.ledger_name,c.group_name" +
                              " )rep group by group_name order by group_name";
                DataTable tblSummary = new DataTable();
                using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                        {
                            adr.Fill(tblSummary);
                        }
                    }
                }

                if (tblSummary != null && tblSummary.Rows.Count > 0)
                {
                    var _debit = tblSummary.Compute("SUM(debit)", string.Empty);
                    var _credit = tblSummary.Compute("SUM(credit)", string.Empty);
                    debit = Convert.ToDecimal(_debit);
                    credit = Convert.ToDecimal(_credit);
                }

            }
            catch
            {
                throw;
            }
            return debit;
        }

        private decimal GetGroupTotalDatewise(DateTime dateFrom, DateTime dateTo, int groupId, betaskdbEntities context, out decimal credit, bool opening = true, bool openingTrans = false)
        {
            decimal debit = 0; credit = 0;
            try
            {
                string where = " transaction_date>='" + dateFrom.ToString("yyyy/MM/dd") + "' AND transaction_date<='" + dateTo.ToString("yyyy/MM/dd") + "'";
                opening = false;
                if (openingTrans)
                {
                    where = "transaction_date<'" + dateFrom.ToString("yyyy/MM/dd") + "'";
                    //opening = true;
                }
                //plGroups = GetProfitAndLossGroups(context);
                string excludePlGroups = "";
                //if (plGroups != "" && plGroups != ",")
                //{
                //    excludePlGroups = $" and c.group_id not in ({plGroups}) ";
                //}
                string sql = " select group_name,case when sum(debit)>=sum(credit) then (sum(debit)-sum(credit)) else 0 end as debit,case when sum(credit)>sum(debit) then (sum(credit)-sum(debit)) else 0 end as credit from " +
                              " (" +
                              " select b.ledger_name,c.group_name," +
                              " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                              " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                              " inner join account_ledger b on b.ledger_id=a.ledger_id and b.group_id=" + groupId + "" +
                              " inner join account_group c on c.group_id=b.group_id" + excludePlGroups +
                              " where  " + where + " and a.status=1 " + (!opening ? " and transaction_type!='OPENING' " : "") +
                              " group by b.ledger_name,c.group_name" +
                              " )rep group by group_name";

                if (openingTrans)
                {
                    DateTime date = new DateTime(dateFrom.Year, 01, 01);
                    sql += " union all " +
                          " select group_name,case when sum(debit)>=sum(credit) then (sum(debit)-sum(credit)) else 0 end as debit,case when sum(credit)>sum(debit) then (sum(credit)-sum(debit)) else 0 end as credit from " +
                                " (" +
                                " select b.ledger_name,c.group_name," +
                                " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                                " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                                " inner join account_ledger b on b.ledger_id=a.ledger_id and b.group_id=" + groupId + "" +
                                " inner join account_group c on c.group_id=b.group_id" + excludePlGroups +
                                " where transaction_date>='" + dateFrom.ToString("yyyy/MM/dd") + "' and transaction_date<='" + dateTo.ToString("yyyy/MM/dd") + "'  and a.status=1  and transaction_type='OPENING'" +
                                " group by b.ledger_name,c.group_name" +
                                " )rep group by group_name order by group_name";

                }
                if (!sql.EndsWith("order by group_name"))
                    sql += " order by group_name";

                DataTable tblSummary = new DataTable();
                using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                        {
                            adr.Fill(tblSummary);
                        }
                    }
                }

                if (tblSummary != null && tblSummary.Rows.Count > 0)
                {
                    var _debit = tblSummary.Compute("SUM(debit)", string.Empty);
                    var _credit = tblSummary.Compute("SUM(credit)", string.Empty);
                    debit = Convert.ToDecimal(_debit);
                    credit = Convert.ToDecimal(_credit);
                }

            }
            catch
            {
                throw;
            }
            return debit;
        }

        public List<TrailBalanceModel> TrialBalanceDetailed(DateTime dateFrom, DateTime dateTo, bool opening = true)
        {
            List<TrailBalanceModel> listTrial = new List<TrailBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<SP_TrialBalanceDetailed_Result> listResult = context.SP_TrialBalanceDetailed(dateFrom, dateTo).ToList();
                    if (listResult != null)
                    {
                        // closingStock

                        ProfitAndLossDAL pl = new ProfitAndLossDAL();
                        var comp = context.company.Where(x => x.status == 1).FirstOrDefault();
                        var sett = context.system_settings.FirstOrDefault(x => x.status == 1);
                        DateTime xStart = comp.financial_from_date.AddYears(-1);
                        DateTime xEnd = comp.financial_to_date.AddYears(-1);
                        //decimal openingStock = CurrentyearOpeningStock(xStart, xEnd, context);
                        ProfitAndLossDAL profit = new ProfitAndLossDAL { };

                        decimal openingStock = OpeningStock(sett.software_startdate, sett.software_startdate);
                        openingStock = 0;

                        listTrial.Add(new TrailBalanceModel
                        {
                            Description = "Opening Stock",
                            HeaderType = "Current Assets",
                            Debit = openingStock,
                            Credit = 0,
                        });
                        foreach (SP_TrialBalanceDetailed_Result rs in listResult)
                        {
                            if (rs != null)
                            {

                                listTrial.Add(new TrailBalanceModel
                                {
                                    Description = rs.ledger_name,
                                    HeaderType = rs.group_name,
                                    Debit = Convert.ToDecimal(rs.debit),
                                    Credit = Convert.ToDecimal(rs.credit),
                                });
                            }
                        }
                    }
                }
            }
            catch { throw; }
            return listTrial;
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
                            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SP_OpeningStocktb", conn))
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
        public List<TrailBalanceDatewiseModel> TrialBalanceDetailedDatewise(DateTime dateFrom, DateTime dateTo, bool opening = true, bool excludeCustomer = false)
        {
            List<TrailBalanceDatewiseModel> listTrial = new List<TrailBalanceDatewiseModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    plGroups = GetProfitAndLossGroups(context);
                    //List<SP_TrialBalanceDetailed_Result> listResult = context.SP_TrialBalanceDetailed(dateFrom, dateTo).ToList();
                    //if (listResult != null)
                    {
                        // closingStock

                        ProfitAndLossDAL pl = new ProfitAndLossDAL();
                        var comp = context.company.Where(x => x.status == 1).FirstOrDefault();
                        var sett = context.system_settings.FirstOrDefault(x => x.status == 1);
                        DateTime xStart = comp.financial_from_date.AddYears(-1);
                        DateTime xEnd = comp.financial_to_date.AddYears(-1);
                        //decimal openingStock = CurrentyearOpeningStock(xStart, xEnd, context);
                        ProfitAndLossDAL profit = new ProfitAndLossDAL { };

                        decimal openingStock = OpeningStock(dateFrom, dateTo);
                        decimal closingStock = openingStock;//ClosingStock(dateFrom, dateTo.AddDays(1));
                        //openingStock = 0;

                        listTrial.Add(new TrailBalanceDatewiseModel
                        {
                            Description = "Opening Stock",
                            HeaderType = "Current Assets",
                            OpeningDebit = openingStock,
                            Debit = 0,
                            Credit = 0,
                            ClosingDebit = closingStock
                        });
                        opening = false;

                        try
                        {
                            string where = " transaction_date>='" + dateFrom.ToString("yyyy/MM/dd") + "' AND transaction_date<='" + dateTo.ToString("yyyy/MM/dd") + "'";
                            string excludeCustomerWhere = "";
                            string excludePlGroups = "";
                            if (plGroups != "" && plGroups != ",")
                            {
                                //  excludePlGroups = $" and c.group_id not in ({plGroups}) ";
                            }
                            if (excludeCustomer)
                            {
                                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                                var customerLedger = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CUSTOMER);
                                excludeCustomerWhere = customerLedger != null ? " and b.group_id!=" + customerLedger.group_id + "" : " and b.description !='CUSTOMER'";
                            }

                            string sql = "  select case when c.group_name='Sundry Debtors/ Customers' then concat(d.payment_mode,'-',d.Status,'-',b.ledger_name) else b.ledger_name end  as  ledger_name ,c.group_name,  " +
                                         "  sum(a.debit) as debit, " +
                                         " sum(a.credit) as  credit from account_transaction a" +
                                          // " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                                          // " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                                          " inner join account_ledger b on b.ledger_id=a.ledger_id " + excludeCustomerWhere +
                                          " inner join account_group c on c.group_id=b.group_id" + excludePlGroups +
                                          " left join customer d on d.ledger_id=b.ledger_id " +
                                          " where  " + where + " and a.status=1  " + (!opening ? " and transaction_type!='OPENING' " : "") +
                                          " group by b.ledger_name,c.group_name,d.payment_mode,d.Status order by c.group_name,b.ledger_name";

                            DataTable tblDetailed = new DataTable();
                            using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                                    {
                                        adr.Fill(tblDetailed);
                                    }
                                }
                            }

                            var openingItems = TrialBalanceDetailedDatewiseOpening(dateFrom, dateTo, context, true, excludeCustomer);

                            if (tblDetailed != null && tblDetailed.Rows.Count > 0)
                            {
                                foreach (DataRow dr in tblDetailed.Rows)
                                {
                                    if (dr != null)
                                    {
                                        decimal openingDebit = 0, openingCredit = 0;
                                        if (openingItems.Any(x => x.Description == dr["ledger_name"].ToString()))
                                        {
                                            openingDebit = openingItems.Where(x => x.Description == dr["ledger_name"].ToString()).Sum(x => x.Debit);
                                            openingCredit = openingItems.Where(x => x.Description == dr["ledger_name"].ToString()).Sum(x => x.Credit);
                                            decimal difference = openingDebit - openingCredit;
                                            if (difference >= 0)
                                            {
                                                openingDebit = difference;
                                                openingCredit = 0;
                                            }
                                            else
                                            {
                                                openingCredit = difference * -1;
                                                openingDebit = 0;
                                            }
                                        }
                                        decimal closingDebit = Convert.ToDecimal(dr["debit"].ToString()) + openingDebit;
                                        decimal closingCredit = Convert.ToDecimal(dr["credit"].ToString()) + openingCredit;
                                        if (closingDebit >= closingCredit)
                                        {
                                            closingDebit -= closingCredit;
                                            closingCredit = 0;
                                        }
                                        else
                                        {
                                            closingCredit -= closingDebit;
                                            closingDebit = 0;
                                        }
                                        listTrial.Add(new TrailBalanceDatewiseModel
                                        {
                                            Description = dr["ledger_name"].ToString(),
                                            HeaderType = dr["group_name"].ToString(),
                                            Debit = Convert.ToDecimal(dr["debit"].ToString()),
                                            Credit = Convert.ToDecimal(dr["credit"].ToString()),
                                            OpeningDebit = openingDebit,
                                            OpeningCredit = openingCredit,
                                            ClosingDebit = closingDebit,
                                            ClosingCredit = closingCredit,
                                        });
                                    }
                                }
                            }

                            var excpetItems = openingItems.Where(item => !listTrial.Any(trialItem => trialItem.Description == item.Description)).ToList();
                            if (excpetItems != null && excpetItems.Count > 0)
                            {
                                foreach (var li in excpetItems)
                                {
                                    listTrial.Add(new TrailBalanceDatewiseModel
                                    {
                                        Description = li.Description,
                                        HeaderType = li.HeaderType,
                                        Debit = 0,
                                        Credit = 0,
                                        OpeningDebit = li.Debit,
                                        OpeningCredit = li.Credit,
                                        ClosingDebit = li.Debit,
                                        ClosingCredit = li.Credit,
                                    });
                                }
                            }

                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
            catch { throw; }

            return listTrial;
        }
        public List<TrailBalanceDatewiseModel> TrialBalanceDetailedDatewiseOpening(DateTime dateFrom, DateTime dateTo, betaskdbEntities context, bool opening = true, bool excludeCustomer = false)
        {
            List<TrailBalanceDatewiseModel> listTrial = new List<TrailBalanceDatewiseModel> { };
            try
            {

                {
                    List<SP_TrialBalanceDetailed_Result> listResult = context.SP_TrialBalanceDetailed(dateFrom, dateTo).ToList();
                    if (listResult != null)
                    {
                        // closingStock

                        ProfitAndLossDAL pl = new ProfitAndLossDAL();
                        var comp = context.company.Where(x => x.status == 1).FirstOrDefault();
                        var sett = context.system_settings.FirstOrDefault(x => x.status == 1);
                        DateTime xStart = comp.financial_from_date.AddYears(-1);
                        DateTime xEnd = comp.financial_to_date.AddYears(-1);
                        //decimal openingStock = CurrentyearOpeningStock(xStart, xEnd, context);
                        ProfitAndLossDAL profit = new ProfitAndLossDAL { };

                        decimal openingStock = OpeningStock(sett.software_startdate, sett.software_startdate);
                        openingStock = 0;

                        listTrial.Add(new TrailBalanceDatewiseModel
                        {
                            Description = "Opening Stock",
                            HeaderType = "Current Assets",
                            Debit = openingStock,
                            Credit = 0,
                        });


                        try
                        {
                            string where = "transaction_date<'" + dateFrom.ToString("yyyy/MM/dd") + "'";
                            string excludeCustomerWhere = "";
                            string excludePlGroups = "";
                            if (plGroups != "" && plGroups != ",")
                            {
                                excludePlGroups = $" and c.group_id not in ({plGroups}) ";
                            }
                            if (excludeCustomer)
                            {
                                LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                                var customerLedger = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.CUSTOMER);
                                excludeCustomerWhere = customerLedger != null ? " and b.group_id!=" + customerLedger.group_id + "" : " and b.description !='CUSTOMER'";
                            }
                            //string sql = " SELECT ledger_name,group_name, debit,credit FROM(select case when c.group_name='Sundry Debtors/ Customers' then concat(d.payment_mode,'-',d.Status,'-',b.ledger_name) else b.ledger_name end  as  ledger_name ,c.group_name,   " +
                            //              " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                            //              " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                            //              " inner join account_ledger b on b.ledger_id=a.ledger_id " + excludeCustomerWhere +
                            //              " inner join account_group c on c.group_id=b.group_id" + excludePlGroups+
                            //              " left join customer d on d.ledger_id=b.ledger_id "+
                            //              " where  " + where + " and a.status=1  and a.ledger_id=4325 " + (!opening ? " and transaction_type!='OPENING' " : "") +
                            //              " group by b.ledger_name,c.group_name,d.payment_mode,d.Status" +
                            //              " union all "+
                            //              "  select case when c.group_name='Sundry Debtors/ Customers' then concat(d.payment_mode,'-',d.Status,'-',b.ledger_name) else b.ledger_name end  as  ledger_name ,c.group_name,   " +
                            //              " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                            //              " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                            //              " inner join account_ledger b on b.ledger_id=a.ledger_id " + excludeCustomerWhere +
                            //              " inner join account_group c on c.group_id=b.group_id" +
                            //              " left join customer d on d.ledger_id=b.ledger_id " +
                            //              " where  transaction_date>='" +dateFrom.ToString("yyyy/MM/dd")+ "' and transaction_date<='" + dateTo.ToString("yyyy/MM/dd") + "' and a.status=1  and a.ledger_id=4325 and transaction_type='OPENING' " +
                            //              " group by b.ledger_name,c.group_name,d.payment_mode,d.Status)rep" +
                            //              " ORDER BY     group_name,     ledger_name; ";

                            string sql = " SELECT ledger_name,group_name, debit,credit FROM(select case when c.group_name='Sundry Debtors/ Customers' then concat(d.payment_mode,'-',d.Status,'-',b.ledger_name) else b.ledger_name end  as  ledger_name ,c.group_name,   " +
                                          " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                                          " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                                          " inner join account_ledger b on b.ledger_id=a.ledger_id " + excludeCustomerWhere +
                                          " inner join account_group c on c.group_id=b.group_id" + excludePlGroups +
                                          " left join customer d on d.ledger_id=b.ledger_id " +
                                          " where  " + where + " and a.status=1  " + (!opening ? " and transaction_type!='OPENING' " : "") +
                                          " group by b.ledger_name,c.group_name,d.payment_mode,d.Status" +
                                          " union all " +
                                          "  select case when c.group_name='Sundry Debtors/ Customers' then concat(d.payment_mode,'-',d.Status,'-',b.ledger_name) else b.ledger_name end  as  ledger_name ,c.group_name,   " +
                                          " case when sum(a.debit)>=sum(a.credit) then sum(a.debit)-sum(a.credit) else 0 end as debit, " +
                                          " case when  sum(a.credit)>sum(a.debit) then sum(a.credit)-sum(a.debit) else 0 end as  credit from account_transaction a" +
                                          " inner join account_ledger b on b.ledger_id=a.ledger_id " + excludeCustomerWhere +
                                          " inner join account_group c on c.group_id=b.group_id" +
                                          " left join customer d on d.ledger_id=b.ledger_id " +
                                          " where  transaction_date>='" + dateFrom.ToString("yyyy/MM/dd") + "' and transaction_date<='" + dateTo.ToString("yyyy/MM/dd") + "' and a.status=1  and transaction_type='OPENING' " +
                                          " group by b.ledger_name,c.group_name,d.payment_mode,d.Status)rep" +
                                          " ORDER BY  group_name,ledger_name; ";

                            DataTable tblDetailed = new DataTable();
                            using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand(sql, conn))
                                {
                                    using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                                    {
                                        adr.Fill(tblDetailed);
                                    }
                                }
                            }

                            if (tblDetailed != null && tblDetailed.Rows.Count > 0)
                            {
                                foreach (DataRow dr in tblDetailed.Rows)
                                {
                                    if (dr != null)
                                    {

                                        listTrial.Add(new TrailBalanceDatewiseModel
                                        {
                                            Description = dr["ledger_name"].ToString(),
                                            HeaderType = dr["group_name"].ToString(),
                                            Debit = Convert.ToDecimal(dr["debit"].ToString()),
                                            Credit = Convert.ToDecimal(dr["credit"].ToString()),
                                        });
                                    }
                                }
                            }

                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
            catch { throw; }
            return listTrial;
        }
        private decimal CurrentyearOpeningStock(DateTime lastFinanceStartDate, DateTime lastFinanceEndDate, betaskdbEntities context)
        {
            decimal openingStock = 0;
            try
            {

                // using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string sql = "select sum(c.closingValue) as openingStock from " +
                                         " ( " +
                                         " SELECT a.item_id, max(closing_value) as closingValue" +
                                         " FROM [dbo].[item_transaction] a" +
                                         " inner join " +
                                         " (select item_id,max([item_transaction_id]) as lastid from [item_transaction] where [transaction_date]>='" + lastFinanceStartDate.ToString("yyyy-MM-dd") + "'  and [transaction_date]<='" + lastFinanceEndDate.ToString("yyyy-MM-dd") + "' and status=1 group by  item_id) b" +
                                         " on a.item_transaction_id=b.lastid" +
                                         " inner join item c on c.item_id=a.item_id and c.stockable=1" +
                                         " group by a.item_id" +
                                         " )c";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        if (!DBNull.Value.Equals(reader["openingStock"]))
                                        {
                                            openingStock = Convert.ToDecimal(reader["openingStock"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return openingStock;
        }

        private string GetProfitAndLossGroups(betaskdbEntities context)
        {
            List<int> groups = new List<int>();
            try
            {
                var parentIds = context.account_group.AsNoTracking().Where(x => x.parent_id == 0 && x.status == 1 && (x.group_name.ToLower() == "expense" || x.group_name.ToLower() == "income")).Select(x => x.group_id).ToList();
                if (parentIds != null && parentIds.Count > 0)
                {
                    var group1 = context.account_group.AsNoTracking().Where(x => x.status == 1 && parentIds.Contains(x.parent_id)).Select(x => x.group_id).ToList();
                    if (group1 != null && group1.Count > 0)
                    {
                        groups.AddRange(group1);
                        var group2 = context.account_group.AsNoTracking().Where(x => x.status == 1 && group1.Contains(x.parent_id)).Select(x => x.group_id).ToList();
                        if (group2 != null) groups.AddRange(group2);
                        var group3 = context.account_group.AsNoTracking().Where(x => x.status == 1 && group2.Contains(x.parent_id)).Select(x => x.group_id).ToList();
                        if (group3 != null) groups.AddRange(group3);
                        var group4 = context.account_group.AsNoTracking().Where(x => x.status == 1 && group3.Contains(x.parent_id)).Select(x => x.group_id).ToList();
                        if (group4 != null) groups.AddRange(group4);
                        var group5 = context.account_group.AsNoTracking().Where(x => x.status == 1 && group4.Contains(x.parent_id)).Select(x => x.group_id).ToList();
                        if (group5 != null) groups.AddRange(group5);

                    }
                }
            }
            catch (Exception ex)
            {
            }
            return (groups.Count > 0) ? string.Join(", ", groups) : "";

        }

        #region notinuse
        public List<TrailBalanceModel> TrialBalanceNotinuse(DateTime dateFrom, DateTime dateTo)
        {
            List<TrailBalanceModel> listTrial = new List<TrailBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<SP_TrialBalanceSummary_Result> listResult = context.SP_TrialBalanceSummary(dateFrom, dateTo).ToList();
                    if (listResult != null)
                    {

                        // closingStock

                        ProfitAndLossDAL pl = new ProfitAndLossDAL();
                        //decimal closingStock = pl.ClosingStock(dateFrom, dateTo.AddDays(1));
                        var comp = context.company.Where(x => x.status == 1).FirstOrDefault();
                        DateTime xStart = comp.financial_from_date.AddYears(-1);
                        DateTime xEnd = comp.financial_to_date.AddYears(-1);
                        decimal openingStock = CurrentyearOpeningStock(xStart, xEnd, context);

                        listTrial.Add(new TrailBalanceModel
                        {
                            Description = "Opening Stock",
                            Debit = openingStock,
                            Credit = 0,
                        });
                        foreach (SP_TrialBalanceSummary_Result rs in listResult)
                        {

                            if (rs != null)
                            {


                                listTrial.Add(new TrailBalanceModel
                                {
                                    Description = rs.group_name,
                                    //HeaderType = rs.group_name,
                                    Debit = Convert.ToDecimal(rs.debit),
                                    Credit = Convert.ToDecimal(rs.credit),
                                });
                            }
                        }
                    }
                }
            }
            catch { throw; }


            return listTrial;
        }
        #endregion
    }

}
