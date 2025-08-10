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
    /// DISC: all functions related to Login
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class LogInDAL
    {
        DataTable tblData = new DataTable();
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        public Customer UserLogin(LogIn objLogIn)
        {
            Customer objCustomer = new Customer();

            string errorLog =string.Empty;
            try
            {
                //List<CustomerAggrement> lstCustomerAggrement = new List<CustomerAggrement>();
                // List<Customer> lstCustomer = new List<Customer>();

                SqlServerDBConnectionLogic.SqlServerDBConnection(Convert.ToString(objLogIn.Company), Convert.ToString(objLogIn.Location));
                errorLog = SqlServerDBConnectionLogic.Maincn.ConnectionString;

                SqlCommand cmd = new SqlCommand("APP_UserLogIn", SqlServerDBConnectionLogic.Maincn);
                cmd.Parameters.AddWithValue("@userid", Convert.ToString(objLogIn.UserId));
                cmd.Parameters.AddWithValue("@password", Convert.ToString(objLogIn.Password));
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            objCustomer = FillCustomerDetails( reader,out errorLog);

                        }
                    }
                }

               SqlCommand cmd2 = new SqlCommand("APP_CustomerAggrement", SqlServerDBConnectionLogic.Maincn);
               cmd2.Parameters.AddWithValue("@customerId", objCustomer.CustomerId);              
               cmd2.CommandType = CommandType.StoredProcedure;
                
                using (System.Data.SqlClient.SqlDataReader CustReader = cmd2.ExecuteReader())
                {
                    if (CustReader.HasRows)
                    {
                        objCustomer.customerAggrements = new List<CustomerAggrement>();
                        while (CustReader.Read())
                        {
                            //errorLog += $"-> Read agreement started ";

                            CustomerAggrement objCustomerAggrement = new CustomerAggrement();
                            objCustomerAggrement.CustomerAggrementId = Convert.ToInt32(CustReader["customer_aggrement_id"]);
                            objCustomerAggrement.ItemId = Convert.ToInt32(CustReader["item_id"]);
                            objCustomerAggrement.ItemName = Convert.ToString(CustReader["item_name"]);
                            objCustomerAggrement.MaxQty = Convert.ToDecimal(CustReader["max_qty"]);
                            objCustomerAggrement.UnitPrice = Convert.ToDecimal(CustReader["unit_price"]);
                            objCustomer.customerAggrements.Add(objCustomerAggrement);
                            errorLog += $"-> Read agreement Completed ";

                        }

                    }
                }
              
            }
            catch (Exception ex)
            {
                MailSettingsDAL objMailSettingsDAL = new MailSettingsDAL();

                var mailSett = objMailSettingsDAL.GetMailSettings(objLogIn.Company, objLogIn.Location);

                objMailSettingsDAL.EmailError($"Customer Login - {ex.Message}", $"User {objLogIn.UserId} - {objLogIn.Password}", true, mailSett, objLogIn.Location);
                throw new Exception($"{errorLog} -> { ex }");
            }
            return objCustomer;

        }

        public Customer FillCustomerDetails( SqlDataReader reader,out string errorLog)
        {
             errorLog = "";
            Customer objCustomer = new Customer { };
            errorLog += $"-> Read started ";
            objCustomer.CustomerId = Convert.ToInt32(reader["customer_id"]);
            objCustomer.CustomerName = Convert.ToString(reader["customer_name"]);
            objCustomer.Address1 = Convert.ToString(reader["address1"]);
            objCustomer.City = Convert.ToString(reader["city"]);
            objCustomer.Street = Convert.ToString(reader["street"]);
            objCustomer.POBox = Convert.ToString(reader["pobox"]);
            objCustomer.Email = Convert.ToString(reader["email"]);
            objCustomer.Phone = Convert.ToString(reader["phone"]);
            objCustomer.Mobile = Convert.ToString(reader["mobile"]);
            objCustomer.Remarks = Convert.ToString(reader["remarks"]);
            objCustomer.Outstanding = Convert.ToDecimal(reader["outstanding_amount"]);
            objCustomer.Payment_mode = Convert.ToString(reader["payment_mode"]);
            objCustomer.Delivery_interval = Convert.ToString(reader["delivery_interval"]);
            objCustomer.WalletNumber = Convert.ToString(reader["wallet_number"]);
            objCustomer.Outstanding = Convert.ToDecimal(reader["outstanding_amount"]);

            objCustomer.APP_CustomerName = reader["app_customer_name"] != DBNull.Value ? Convert.ToString(reader["app_customer_name"]) : null;
            objCustomer.APP_Address1 = reader["app_address1"] != DBNull.Value ? Convert.ToString(reader["app_address1"]) : null;
            objCustomer.APP_Address2 = reader["app_address2"] != DBNull.Value ? Convert.ToString(reader["app_address2"]) : null;
            objCustomer.APP_Email = reader["app_email"] != DBNull.Value ? Convert.ToString(reader["app_email"]) : null;
            objCustomer.APP_Phone = reader["app_phone"] != DBNull.Value ? Convert.ToString(reader["app_phone"]) : null;

            string password = reader["app_password"].ToString();
            errorLog += $"-> Read untill password ";

            if (!string.IsNullOrEmpty(password))
                objCustomer.LoginKey = BETaskCustomerAPI.Common.Encryption.EncryptString(password);
            errorLog += $"-> Read profile completed ";
            return objCustomer;
        }
    }
}