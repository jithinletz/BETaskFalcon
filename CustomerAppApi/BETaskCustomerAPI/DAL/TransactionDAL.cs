using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BETaskAPI.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;

namespace BETaskAPI.DAL
{
    /// <summary>
    /// <summary>
    /// AUTHOR : PRAKASH TMR
    /// DISC: all functions related to  Complaints
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class TransactionDAL
    {
        DataTable tblData = new DataTable();
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public string SaveTransaction(Transaction transaction)
        {
            string orderId = transaction.ReferenceId;
            SqlServerDBConnectionLogic.SqlServerDBConnection(transaction.Company, transaction.Location);

            try
            {
                SqlCommand cmd = new SqlCommand("APP_SaveTransaction", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@referenceId", orderId);
                cmd.Parameters.AddWithValue("@customerId", Convert.ToInt32(transaction.CustomerId));
                cmd.Parameters.AddWithValue("@customerIP", Convert.ToString(transaction.CustomerIP));
                cmd.Parameters.AddWithValue("@appDate", Convert.ToDateTime(transaction.APPDate));
                cmd.Parameters.AddWithValue("@offerId", Convert.ToInt32(transaction.OfferId));
                cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(transaction.Amount));
                cmd.Parameters.AddWithValue("@offerName", Convert.ToString(transaction.OfferName));
                cmd.Parameters.AddWithValue("@gateway", Convert.ToString(transaction.GateWay));
                cmd.Parameters.AddWithValue("@appType", Convert.ToString(GetAppType()));
                cmd.Parameters.AddWithValue("@version", Convert.ToString(transaction.Version));
                cmd.Parameters.AddWithValue("@tid", Convert.ToString(transaction.TId));
                cmd.Parameters.AddWithValue("@other_notes", Convert.ToString(transaction.OtherNotes));
                cmd.Parameters.AddWithValue("@tracking_id", Convert.ToString(transaction.TrackingId));
                cmd.ExecuteNonQuery();
            }
            catch
            {

                throw;
            }
            return orderId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionResponse"></param>
        /// <returns></returns>
        public void UpdateTransactionReponse(TransactionResponse transactionResponse)
        {

            SqlServerDBConnectionLogic.SqlServerDBConnection(transactionResponse.Company, transactionResponse.Location);
            try
            {

                SqlCommand cmd = new SqlCommand("APP_UpdateTransactionResponse", SqlServerDBConnectionLogic.Maincn);
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

                if (Convert.ToString(transactionResponse.StatusText).ToLower().Equals("successful"))
                {
                    SaveDailyCollection(transactionResponse, SqlServerDBConnectionLogic.Maincn);
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


                SqlCommand cmd = new SqlCommand("APP_SaveDailyCollectionAfterPayment", SqlServerDBConnectionLogic.Maincn);
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

        public   async Task SaveTransactionResponseAsync(string response, string company, string location)
        {

            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
            try
            {

                SqlCommand cmd = new SqlCommand("APP_InserTransactionResponse", SqlServerDBConnectionLogic.Maincn);
                cmd.Parameters.AddWithValue("@response_text", response);

                cmd.CommandType = CommandType.StoredProcedure;

                await cmd.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public List<TransactionHistory> TransactionHistory(string company, string location, string customerId)
        {
            var listHistory = new List<Models.TransactionHistory>();
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);

            try
            {
                SqlCommand cmd = new SqlCommand("[APP_TransactionHistory]", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", customerId);
                using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                {
                    using (var tblData = new DataTable())
                    {
                        adr.Fill(tblData);

                        listHistory = tblData.AsEnumerable().Select(dr => new Models.TransactionHistory
                        {
                            TransactionId = Convert.ToInt32(dr["transaction_id"]),
                            ReferenceId = dr["referance_id"].ToString(),
                            StartDate = dr["start_date"].ToString(),
                            Amount = Convert.ToDecimal(dr["Amount"]),
                            Status = dr["Status"].ToString(),
                            StatusText = dr["status_text"].ToString(),
                            DetailedRespose = ""
                        }).ToList();

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listHistory;
        }


        public static string GetAppType()
        {
            string appType = "Android";
            try
            {
                appType = ConfigurationManager.AppSettings["AppType"];
            }
            catch { }
            return appType;
        }

      
    }
}
