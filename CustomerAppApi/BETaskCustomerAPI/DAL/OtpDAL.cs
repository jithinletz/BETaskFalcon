using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BETaskAPI.Models;
using System.Data.SqlClient;
using System.Data;
namespace BETaskAPI.DAL
{
    /// <summary>
    /// <summary>
    /// AUTHOR : PRAKASH TMR
    /// DISC: all functions related to  Complaints
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class OTPDAL
    {
        
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <param name="otp"></param>
        public void SaveOTP(string company, string location, int customerId,string otp)
        { 
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location); 
            try
            {
                SqlCommand cmd = new SqlCommand("APP_SaveOTP", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@otp", Convert.ToString(otp)); 
                int result=cmd.ExecuteNonQuery(); 
            }
            catch
            {
                throw;
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string GetLastActiveOTP(string company, string location, int customerId) {
            string result = string.Empty;
            try {

                SqlServerDBConnectionLogic.SqlServerDBConnection(Convert.ToString(company), Convert.ToString(location)); 
                SqlCommand cmd = new SqlCommand("APP_GetLastActiveOTP", SqlServerDBConnectionLogic.Maincn);
                cmd.Parameters.AddWithValue("@customerid", Convert.ToInt32(customerId));
                cmd.CommandType = CommandType.StoredProcedure;

                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result= Convert.ToString(reader["otp"]);
                        }
                    }
                }
            }
            catch {
                throw;
            }
            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
         
        public void UpdateOTPCompletedDate(string company, string location, int customerId)
        {
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
            try
            {
                SqlCommand cmd = new SqlCommand("APP_UpdateOTPCompletedDate", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", customerId); 
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }

        }
    }
}