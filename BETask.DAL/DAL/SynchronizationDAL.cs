using BETask.DAL.EDMX;
using BETask.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BETask.DAL.DAL
{
    public class SynchronizationDAL
    {
        private readonly betaskdbEntities _context;
        public SynchronizationDAL(betaskdbEntities context)
        {
            _context = context;
        }

        public List<EDMX.SP_SyncCustomerOutstanding_Result> CustomerOutstanding()
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    return context.SP_SyncCustomerOutstanding().ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public int CustomerDateUpdation()
        {
            int result = 0;
            try
            {
                List<EDMX.customer> listCustomer = new List<customer>();
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            listCustomer = context.customer.Where(x => x.added_time == null).ToList();
                            if (listCustomer != null && listCustomer.Count > 0)
                            {
                                int cnt = 1;
                                foreach (EDMX.customer xCustomer in listCustomer)
                                {
                                    if (xCustomer != null)
                                    {
                                        if (context.sales.Where(x => x.customer_id == xCustomer.customer_id).Any())
                                        {
                                            var fristTran = context.sales.AsNoTracking().Where(x => x.customer_id == xCustomer.customer_id).Min(x => x.sales_date);
                                            xCustomer.added_time = fristTran;
                                            context.Entry(xCustomer).State = xCustomer.customer_id == 0 ? EntityState.Added : EntityState.Modified;
                                            context.SaveChanges();
                                            result = cnt + 1;

                                        }
                                    }
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            if (transaction != null)
                                transaction.Rollback();
                            string e = ee.ToString();
                            throw;

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<EDMX.SP_SyncCustomerOutstandingRoutewise_Result> CustomerOutstandingRoutewise(int routeId, int customerId = 0)
        {
            List<SP_SyncCustomerOutstandingRoutewise_Result> sP_SyncCustomerOutstandingRoutewise1_Result = new List<SP_SyncCustomerOutstandingRoutewise_Result> { };

            try
            {
                using (var context = new betaskdbEntities())
                {
                    //context.Database.CommandTimeout = 1500;
                    // return context.SP_SyncCustomerOutstandingRoutewise1(routeId,customerId).ToList();

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {

                        conn.Open();
                        DataTable tblData = new DataTable();
                        string connectionString = "";
                        if (customerId > 0)
                        {
                            connectionString = $"select c.customer_id,c.customer_name,max(c.wallet_balance) as wallet_balance,(sum(debit) - sum(credit)) as outstanding from account_transaction a" +
                                                    " inner join account_ledger l on l.ledger_id=a.ledger_id and l.description='CUSTOMER' " +
                                                     $" inner join customer c on c.customer_id={customerId}  where a.ledger_id=(select ledger_id from customer where customer_id={customerId}) and a.status=1 group by  c.customer_id,c.customer_name order by c.customer_id";
                        }
                        if (routeId > 0 && customerId == 0)
                        {
                            connectionString = $"select c.customer_id,c.customer_name,max(c.wallet_balance) as wallet_balance,(sum(debit) - sum(credit)) as outstanding from account_transaction a" +
                        " inner join account_ledger  l on l.ledger_id=a.ledger_id and l.description='CUSTOMER' " +
                         $" inner join customer c on c.ledger_id=a.ledger_id and c.route_id={routeId} where a.status=1 group by  c.customer_id,c.customer_name order by c.customer_id";
                        }

                        using (SqlCommand cmd = new SqlCommand(connectionString, conn))
                        {
                            cmd.CommandTimeout = 1500;

                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                            if (tblData != null && tblData.Rows.Count > 0)
                            {
                                foreach (DataRow dr in tblData.Rows)
                                {
                                    sP_SyncCustomerOutstandingRoutewise1_Result.Add(new SP_SyncCustomerOutstandingRoutewise_Result
                                    {
                                        customer_id = Convert.ToInt32(dr["customer_id"].ToString()),
                                        customer_name = dr["customer_name"].ToString(),
                                        wallet_balance = Convert.ToDecimal(dr["wallet_balance"].ToString()),
                                        outstanding = Convert.ToDecimal(dr["outstanding"].ToString()),

                                    });
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
            return sP_SyncCustomerOutstandingRoutewise1_Result;
        }



        public List<EDMX.route> Route()
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    return context.route.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.building> Building(int buildingId = 0)
        {
            List<building> listBuilding = new List<building>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (buildingId > 0)
                        listBuilding = context.building.Where(x => x.status == 1 && x.building_id == buildingId).ToList();
                    else
                        listBuilding = context.building.Where(x => x.status == 1).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listBuilding;
        }


        public int DailyCollectionOutstanding(List<EDMX.daily_collection> listDailyCollection, ref List<customer> listUpdatedCustomer)
        {
            int result = 0;
            string lastCust = "";
            try
            {
                CustomerDAL customerDAL = new CustomerDAL();
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();

                using (var context = new betaskdbEntities())
                {

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var routes = context.route.AsNoTracking().ToList();

                            SynchronizationDAL synchronizationDAL = new SynchronizationDAL(context);
                            DateTime date = DateTime.Today.AddDays(-3);
                            foreach (EDMX.daily_collection coll in listDailyCollection)
                            {
                                long appDailyCollId = coll.daily_collection_id;
                                lastCust = appDailyCollId.ToString();
                                customer cs = context.customer.Where(x => x.customer_id == coll.customer_id).FirstOrDefault();
                                int routeId = Convert.ToInt32(cs.route_id);

                                daily_collection dColl = context.daily_collection.AsNoTracking().Where(x => x.remarks.Contains(coll.daily_collection_id.ToString()) && x.delivery_time >= date && x.customer_id == coll.customer_id && x.collected_amount == coll.collected_amount && x.status == 4).FirstOrDefault();
                                if (dColl == null)
                                {
                                    employee emp = context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == coll.employee_id);
                                    coll.route_id = emp.route_id;
                                    context.Entry(coll).State = EntityState.Added;
                                    context.SaveChanges();


                                    if (routes.Any(x => x.route_id == coll.route_id))
                                    {
                                        coll.remarks += $" for {routes.FirstOrDefault(x => x.route_id == coll.route_id).route_name}";
                                    }
                                    coll.remarks += $" by {emp.first_name}";
                                    coll.remarks += $" - {cs.customer_name}";


                                    APosting.OutstandingCollectionPost(coll, context);
                                }

                                //Updatin wallet as outstanding
                                decimal walletBal = 0;
                                synchronizationDAL.UpdateWalletAsOutstanding(coll.customer_id, ref walletBal);
                                listUpdatedCustomer.Add(new customer
                                {
                                    customer_id = coll.customer_id,
                                    wallet_balance = walletBal,
                                    customer_name = cs.customer_name
                                });

                            }
                            transaction.Commit();
                            transaction.Dispose();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw new Exception(lastCust);
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<int> UpdateRouteLocation(int routeId, string range, bool isEnable)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_UpdateRouteLocation", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add the parameters
                            command.Parameters.AddWithValue("@RouteId", routeId);
                            command.Parameters.AddWithValue("@IsEnable", isEnable);
                            command.Parameters.AddWithValue("@Range", range ?? (object)DBNull.Value);

                            // Open the connection
                            int affectedRows = await command.ExecuteNonQueryAsync();

                            // Return the number of affected rows
                            return affectedRows;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateRouteCreditLimit(int routeId, decimal creditlimit)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("SP_UpdateRouteCreditLimit", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add the parameters
                            command.Parameters.AddWithValue("@RouteId", routeId);
                            command.Parameters.AddWithValue("@CreditLimit", creditlimit);

                            // Open the connection
                            int affectedRows = await command.ExecuteNonQueryAsync();

                            // Return the number of affected rows
                            return affectedRows;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DataTable> GetOnlineTransactionReport(DateTime dateFrom, DateTime dateTo,string status,int customerId)
        {
            DataTable tblData = new DataTable();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (SqlCommand command = new SqlCommand("APP_GetAppTransactions", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add the parameters
                            command.Parameters.AddWithValue("@StartDate", dateFrom);
                            command.Parameters.AddWithValue("@EndDate", dateTo);
                            command.Parameters.AddWithValue("@Status", status);

                            using (SqlDataAdapter adr = new SqlDataAdapter(command))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tblData;
        }

      

        
        public int SyncDeliveryReturnIytems(List<delivery_return> listDeliveryReturn)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (delivery_return dr in listDeliveryReturn)
                            {
                                // delivery_return xReturn = context.delivery_return.Where(x => x.customer_id == dr.customer_id  && x.return_date == dr.return_date && x.remarks.Contains(dr.delivery_return_id.ToString())).FirstOrDefault();
                                delivery_return xReturn = context.delivery_return.Where(x => x.customer_id == dr.customer_id && x.return_date == dr.return_date && x.qty == dr.qty).FirstOrDefault();
                                if (xReturn == null)
                                {
                                    context.Entry(dr).State = EntityState.Added;
                                    result++;
                                    if (dr.return_type != null && dr.return_type == 2)
                                    {
                                        CustomerAssetDAL customerAssetDAL = new CustomerAssetDAL(context);
                                        customerAssetDAL.UpdateAsseFromReturn(dr);
                                    }
                                }
                            }
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ex)
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
            return result;
        }
        public List<int> ReddemedCoupon(List<EDMX.coupon_leaf> listLeaf)
        {
            List<int> listUpdatedLeaf = new List<int>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (coupon_leaf leaf in listLeaf)
                            {
                                var xLeaf = context.coupon_leaf.Where(x => x.leaf_id == leaf.leaf_id).FirstOrDefault();
                                if (xLeaf != null)
                                {
                                    xLeaf.status = leaf.status;
                                    xLeaf.redeem_date = leaf.redeem_date;
                                    xLeaf.remarks = leaf.remarks;
                                    context.Entry(xLeaf).State = EntityState.Modified;
                                    context.SaveChanges();
                                    listUpdatedLeaf.Add(leaf.leaf_id);
                                }
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
            return listUpdatedLeaf;
        }

        public int ReRunDeliveryReturn(string returnDate)
        {
            int result = 0;
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();

                        if (!ExistDeliveryReturnTempTable(conn))
                            CreateTempDeliveryReturnTable(conn);

                        DataTable tblData = new DataTable();
                        string connectionString = " select min(delivery_return_id) from delivery_return " +
                                                    $" where return_date = '{returnDate}'" +
                                                    "  group by item_id, qty, customer_id,server_time having COUNT(customer_id) > 1";
                        using (SqlCommand cmdExist = new SqlCommand(connectionString, conn))
                        {
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmdExist))
                            {

                                adr.Fill(tblData);

                            }
                        }
                        if (tblData != null && tblData.Rows.Count > 0)
                        {
                            connectionString = " insert into temp_delivery_return    select * from delivery_return where delivery_return_id in( " +
                                              $" select min(delivery_return_id) from delivery_return  where return_date = '{returnDate}'" +
                                               "  group by item_id,qty,customer_id,server_time having COUNT(customer_id) > 1 )";
                            using (SqlCommand cmdInsert = new SqlCommand(connectionString, conn))
                            {
                                int rowsInserted = cmdInsert.ExecuteNonQuery();
                            }


                            connectionString = $" delete from delivery_return where delivery_return_id in (" +
                                                       " select min(delivery_return_id) from delivery_return " +
                                                       $" where return_date = '{returnDate}'" +
                                                       "  group by item_id, qty, customer_id,server_time having COUNT(customer_id) > 1);";

                            using (SqlCommand cmd = new SqlCommand(connectionString, conn))
                            {
                                result = cmd.ExecuteNonQuery();
                            }
                        }
                    }


                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return result;
        }
        public bool ExistDeliveryReturnTempTable(SqlConnection conn)
        {
            bool exist = true;
            try
            {
                string sql = "select count (*) as res  from information_schema.tables  where table_name = 'temp_delivery_return'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    int res = (int)cmd.ExecuteScalar();
                    exist = res == 0 ? false : true;
                }
            }
            catch (Exception ex)
            { }

            return exist;
        }

        public void CreateTempDeliveryReturnTable(SqlConnection conn)
        {
            bool exist = true;
            try
            {
                string sql = " CREATE TABLE temp_delivery_return( " +
    " [delivery_return_id] [int]  NOT NULL," +
    " [return_date] [date] NOT NULL," +
    " [item_id] [int] NOT NULL," +
    " [qty] [decimal](10, 2) NOT NULL," +
    " [customer_id] [int] NOT NULL," +
    " [employee_id] [int] NOT NULL," +
    " [status] [int] NOT NULL," +
    " [remarks] [nvarchar](150) NULL)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            { }

        }

        public bool UpdateWalletAsOutstanding(int customerId, ref decimal newBal)
        {
            newBal = 0;
            decimal walletBal = 0; bool isUpdate = false;
            try
            {
                betaskdbEntities context;
                if (_context == null)
                {
                    context = new betaskdbEntities();
                }
                else
                    context = _context;

                customer _customer = _context.customer.Find(customerId);
                decimal debit = context.account_transaction.Where(x => x.ledger_id == _customer.ledger_id && x.status == 1).Select(x => x.debit).DefaultIfEmpty(0).Sum();
                decimal credit = context.account_transaction.Where(x => x.ledger_id == _customer.ledger_id && x.status == 1).Select(x => x.credit).DefaultIfEmpty(0).Sum();
                walletBal = (debit - credit) * -1;

                if (_customer.wallet_balance != walletBal)
                {
                    _customer.wallet_balance = walletBal;
                    context.customer.Attach(_customer);
                    context.Entry(_customer).Property(x => x.wallet_balance).IsModified = true;
                    context.SaveChanges();
                    isUpdate = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            newBal = walletBal;
            return isUpdate;
        }

        public int SynchronizeCustomerOutstanding(List<CustomerOutstandingModel> listCustomerOutstanding, out string error)
        {
            error = string.Empty;
            int result = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    //  List<customer> listCust = context.customer.Where(x => x.status == 1).ToList();
                    foreach (CustomerOutstandingModel cOutstanding in listCustomerOutstanding)
                    {
                        var cust = context.customer.Where(x => x.status == 1 && x.customer_id == cOutstanding.customer_id).FirstOrDefault();// listCust.Where(x => x.customer_id == cOutstanding.customer_id).FirstOrDefault();
                        if (cust != null)
                        {
                            result++;
                            cust.outstanding_amount = Convert.ToDecimal(cOutstanding.outstanding);
                            cust.wallet_balance = (!String.IsNullOrEmpty(cust.wallet_number) && cust.status == 1) ? (cOutstanding.wallet_balance != 0 ? cOutstanding.wallet_balance : cust.wallet_balance) : cust.wallet_balance;
                            context.Entry(cust).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                    }

                }
            }
            catch (Exception ee)
            {

                error = ee.ToString();
                return result;
            }
            return result;
        }

        public void UpdateTransactionReponse(TransactionResponse transactionResponse)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("APP_UpdateTransactionResponse", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@referenceId", transactionResponse.ReferenceId);
                        cmd.Parameters.AddWithValue("@amount_received", Convert.ToDecimal(transactionResponse.AmountReceived));
                        cmd.Parameters.AddWithValue("@payment_reference_id", Convert.ToString(transactionResponse.PaymentReferenceId));
                        cmd.Parameters.AddWithValue("@payment_mode", Convert.ToString(transactionResponse.PaymentMode));
                        cmd.Parameters.AddWithValue("@status_text", Convert.ToString(transactionResponse.StatusText));
                        cmd.Parameters.AddWithValue("@response", Convert.ToString(transactionResponse.Response));
                        cmd.Parameters.AddWithValue("@end_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@version", Convert.ToString(transactionResponse.Version));
                        cmd.ExecuteNonQuery();
                        if (Convert.ToString(transactionResponse.StatusText).ToLower().Equals("successful") || Convert.ToString(transactionResponse.StatusText).ToLower().Equals("shipped"))
                        {
                            SaveDailyCollection(transactionResponse, conn);
                        }
                        else if(Convert.ToString(transactionResponse.StatusText).ToLower().Equals("initiated"))
                        {
                            UpdateInitialStatusAfterValidation(transactionResponse,conn);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void SaveDailyCollection(TransactionResponse transactionResponse, SqlConnection connection)
        {
            try
            {
                int customerId = 0;
                int employeeId = 0;
                decimal amount = 0;

                using (SqlCommand cmdGetCustOnfo = new SqlCommand("APP_GetCustomerInfoByTransaction", connection))
                {
                    cmdGetCustOnfo.CommandType = CommandType.StoredProcedure;
                    cmdGetCustOnfo.Parameters.AddWithValue("@referenceId", Convert.ToString(transactionResponse.ReferenceId));
                    using (SqlDataReader reader = cmdGetCustOnfo.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customerId = reader.GetInt32(reader.GetOrdinal("customer_id"));
                            employeeId = reader.GetInt32(reader.GetOrdinal("employee_id"));
                            amount = reader.GetDecimal(reader.GetOrdinal("amount"));
                        }
                    }
                }


                SqlCommand cmd = new SqlCommand("APP_SaveDailyCollectionAfterPayment", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@netAmount", Convert.ToDecimal(transactionResponse.AmountReceived));
                cmd.Parameters.AddWithValue("@collectedAmount", Convert.ToDecimal(amount));
                cmd.Parameters.AddWithValue("@remarks", Convert.ToString(transactionResponse.ReferenceId));
                cmd.Parameters.AddWithValue("@paymentMode", "Bank");
                cmd.Parameters.AddWithValue("@deliveryTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@status", 4);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                cmd.ExecuteNonQuery();

                //58  Cash Account    89.250  0.000 Debit
                //1046700 Al zain metal scrap     0.000   89.250 Credit
            }
            catch (Exception ex)
            {
                throw new Exception("Error APP_SaveDailyCollectionAfterPayment " + ex.ToString());
            }
        }

        private void UpdateInitialStatusAfterValidation(TransactionResponse transactionResponse, SqlConnection connection)
        {
            try
            {
                

             
                    
                SqlCommand cmd = new SqlCommand("APP_UpdateTransactionInitialStatusAfterManualValidation", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@referenceId", transactionResponse.ReferenceId);
                cmd.ExecuteNonQuery();

                //58  Cash Account    89.250  0.000 Debit
                //1046700 Al zain metal scrap     0.000   89.250 Credit
            }
            catch (Exception ex)
            {
                throw new Exception("Error APP_SaveDailyCollectionAfterPayment " + ex.ToString());
            }
        }

    }
}
