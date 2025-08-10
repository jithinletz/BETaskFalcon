using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace BETask.DAL.DAL
{
    public class CostCenterDAL
    {
        public int SaveCostCenter(cost_center cost)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(cost).State = cost.cost_center_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch
            {
                throw;
            }
            return cost.cost_center_id;
        }
        public List<cost_center> GetAllCostCenter(int primaryCostCenter)
        {
            List<cost_center> listCostCenter = new List<cost_center>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCostCenter = context.cost_center.Where(x => x.status == 1 && (primaryCostCenter >= 0 ? (x.parent_id == primaryCostCenter) : x.parent_id >= 0)).OrderBy(x => x.parent_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCostCenter;
        }
        public cost_center GetCostCenter(int costCenterId, out string parentName)
        {
            parentName = string.Empty;
            cost_center costCenter = new cost_center();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    costCenter = context.cost_center.Find(costCenterId);
                    parentName = context.cost_center.Find(costCenter.parent_id).cost_center_name;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return costCenter;
        }
        public List<cost_center> GetAllSubCostCenter(int primaryCostCenter)
        {
            List<cost_center> listCostCenter = new List<cost_center>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCostCenter = context.cost_center.Where(x => x.status == 1 && x.parent_id > 0 && (primaryCostCenter >= 0 ? (x.parent_id == primaryCostCenter) : x.parent_id >= 0)).OrderBy(x => x.parent_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCostCenter;
        }
        public List<cost_center> GetPrimaryCostCenter()
        {
            List<cost_center> listCostCenter = new List<cost_center>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCostCenter = context.cost_center.Where(x => x.status == 1 && x.parent_id == 0).OrderBy(x => x.cost_center_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCostCenter;
        }
        public int GetGroupLevel(int groupId)
        {
            int level = 1;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int parentId = context.cost_center.Where(x => x.cost_center_id == groupId).FirstOrDefault().parent_id;
                    if (parentId > 0)
                    {
                        level++;
                        cost_center acc = context.cost_center.Where(x => x.cost_center_id == parentId).FirstOrDefault();
                        while (acc.parent_id != 0)
                        {
                            if (acc != null)
                            {
                                level++;
                                acc = context.cost_center.Where(x => x.cost_center_id == acc.parent_id).FirstOrDefault();
                            }
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return level;
        }
        public void SaveCostCenterTransaction(cost_center_transaction cost)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(cost).State = cost.entry_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<cost_center_transaction> GetSavedTransactionByGUID(string guid)
        {
            List<cost_center_transaction> listCS = new List<cost_center_transaction>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCS = context.cost_center_transaction.Include(x => x.cost_center).Where(x => x.reference_id == guid && x.status != 2).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listCS;
        }
        public int DeleteTransactionByGUID(string guid)
        {
            int result = 0;
            List<cost_center_transaction> listCS = new List<cost_center_transaction>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCS = context.cost_center_transaction.Where(x => x.reference_id == guid && x.status == 3).ToList();
                    foreach (cost_center_transaction cs in listCS)
                    {
                        cs.status = 2;
                        context.Entry(cs).State = EntityState.Modified;
                        result++;
                    }
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public int DeleteTransactionByEntryId(int entryId)
        {
            int result = 0;

            try
            {
                using (var context = new betaskdbEntities())
                {

                    cost_center_transaction cs = context.cost_center_transaction.Where(x => x.entry_id == entryId).FirstOrDefault();
                    cs.status = 2;
                    context.Entry(cs).State = EntityState.Modified;
                    context.SaveChanges();
                    result++;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<SP_CostCenterSummary_Result> GetCostCenterSummary(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.SP_CostCenterSummary(dateFrom, dateTo).ToList();
                }
            }
            catch (Exception ex)
            { throw; }
        }
        public List<SP_CostCenterDetailed_Result> GetCostCenterDetailed(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.SP_CostCenterDetailed(dateFrom, dateTo).ToList();
                }
            }
            catch (Exception ex)
            { throw; }
        }
        public List<SP_CostCenterDatewiseDetailed_Result> GetCostCenterDatewiseDetailed(DateTime dateFrom, DateTime dateTo, int primaryId, int costCenterId, int ledgerId)
        {
            List<SP_CostCenterDatewiseDetailed_Result> listTransaction = new List<SP_CostCenterDatewiseDetailed_Result>();
            try
            {

                using (var context = new betaskdbEntities())
                {
                    string ledgerName = "";
                    DataTable dataTable = new DataTable();
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SP_CostCenterDatewiseDetailed", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@dateFrom", dateFrom);
                            command.Parameters.AddWithValue("@dateTo", dateTo);
                            command.Parameters.AddWithValue("@primaryId", primaryId);
                            command.Parameters.AddWithValue("@costCenterId", costCenterId);
                            command.Parameters.AddWithValue("@ledgerId", ledgerId);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(dataTable);
                        }
                    }

                    listTransaction = ConvertCostCenterReportDataTableToList(dataTable);

                    if (ledgerId > 0)
                        ledgerName = context.account_ledger.FirstOrDefault(x => x.ledger_id == ledgerId).ledger_name;
                    //{

                    //    listTransaction = context.SP_CostCenterDatewiseDetailed(dateFrom, dateTo).ToList();
                    //    if (primaryId > 0)
                    //        listTransaction = listTransaction.Where(x => x.parentId == primaryId).ToList();
                    //    if (costCenterId > 0)
                    //        listTransaction = listTransaction.Where(x => x.cost_center_id == costCenterId).ToList();
                    if (ledgerId > 0)
                        listTransaction = listTransaction.Where(x => x.ledger_name == ledgerName).ToList();

                }

            }
            catch (Exception ex)
            { throw; }
            return listTransaction;
        }

        public DataTable GetCostCenterSummaryGrouped(DateTime dateFrom, DateTime dateTo, int primaryId,int ledgerId)
        {
            DataTable tblData;
                try
            {

                using (var context = new betaskdbEntities())
                {
                    string ledgerName = "";
                    tblData = new DataTable();
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SP_CostCenterSummaryGrouped", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@dateFrom", dateFrom);
                            command.Parameters.AddWithValue("@dateTo", dateTo);
                            command.Parameters.AddWithValue("@primaryId", primaryId);
                            command.Parameters.AddWithValue("@ledgerId", ledgerId);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(tblData);
                        }
                    }

                   
                }

            }
            catch (Exception ex)
            { throw; }
            return tblData;
        }

        public List<SP_CostCenterDatewiseDetailed_Result> ConvertCostCenterReportDataTableToList(DataTable dataTable)
        {
            var resultList = new List<SP_CostCenterDatewiseDetailed_Result>();

            foreach (DataRow row in dataTable.Rows)
            {
                var result = new SP_CostCenterDatewiseDetailed_Result
                {
                    transaction_date = Convert.ToDateTime(row["transaction_date"]),
                    ledger_name = row["ledger_name"].ToString(),
                    parentId = Convert.ToInt32(row["parentId"]),
                    parent = row["parent"].ToString(),
                    cost_center_id = Convert.ToInt32(row["cost_center_id"]),
                    cost_center_name = row["cost_center_name"].ToString(),
                    transaction_type = row["transaction_type"].ToString(),
                    debit = Convert.ToDecimal(row["debit"]),
                    credit = Convert.ToDecimal(row["credit"]),
                    narration = row["narration"].ToString()
                };

                resultList.Add(result);
            }

            return resultList;
        }


        public bool ValidateCostCenter(List<account_transaction> listTransaction)
        {
            try
            {
                bool isValid = true;
                using (var context = new betaskdbEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        foreach (var tran in listTransaction)
                        {
                            if (!string.IsNullOrEmpty(tran.reference_id) && tran.reference_id.Length > 15)
                            {
                                using (SqlCommand cmd = new SqlCommand("SP_ValidateCostCenterTransaction", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add(new SqlParameter("@transactionNumber", SqlDbType.BigInt)).Value = tran.transaction_number;
                                    cmd.Parameters.Add(new SqlParameter("@debit", SqlDbType.Decimal)).Value = tran.debit;
                                    cmd.Parameters.Add(new SqlParameter("@credit", SqlDbType.Decimal)).Value = tran.credit;
                                    cmd.Parameters.Add(new SqlParameter("@referanceId", SqlDbType.NVarChar, 50)).Value = tran.reference_id;
                                    SqlParameter outputParam = new SqlParameter("@isValid", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                                    cmd.Parameters.Add(outputParam); 
                                    var resullt =cmd.ExecuteNonQuery();
                                    isValid = (bool)outputParam.Value;
                                }
                                if (!isValid)
                                    break;
                            }
                        }
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
