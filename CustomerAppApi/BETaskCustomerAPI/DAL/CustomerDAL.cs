using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BETaskAPI.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace BETaskAPI.DAL
{
    /// <summary>
    /// <summary>
    /// AUTHOR : PRAKASH TMR
    /// DISC: all functions related to Login
    /// DATE: 10-12-2021
    /// </summary> 
    /// </summary>
    public class CustomerDAL
    {
        DataTable tblData = new DataTable();
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        Customer objCustomer = new Customer();
        public Customer GetCustomer(string company, string location, int customerId,string email="")
        {
            try
            {

                SqlServerDBConnectionLogic.SqlServerDBConnection(Convert.ToString(company), Convert.ToString(location));

                SqlCommand cmd;
                if (customerId > 0)
                {
                     cmd = new SqlCommand("APP_GetCustomer", SqlServerDBConnectionLogic.Maincn);
                    cmd.Parameters.AddWithValue("@customerid", Convert.ToInt32(customerId));
                }
                else
                {
                     cmd = new SqlCommand("APP_GetCustomerByMail", SqlServerDBConnectionLogic.Maincn);
                    cmd.Parameters.AddWithValue("@email",email);
                }

                cmd.CommandType = CommandType.StoredProcedure;

                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
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
                            objCustomer.WalletBalance = Convert.ToDecimal(string.IsNullOrEmpty(reader["wallet_balance"].ToString()) ? 0 : reader["wallet_balance"]);
                            string password = reader["app_password"].ToString();
                            if (!string.IsNullOrEmpty(password))
                                objCustomer.LoginKey = BETaskCustomerAPI.Common.Encryption.EncryptString(password);
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
                            CustomerAggrement objCustomerAggrement = new CustomerAggrement();
                            objCustomerAggrement.CustomerAggrementId = Convert.ToInt32(CustReader["customer_aggrement_id"]);
                            objCustomerAggrement.ItemId = Convert.ToInt32(CustReader["item_id"]);
                            objCustomerAggrement.ItemName = Convert.ToString(CustReader["item_name"]);
                            objCustomerAggrement.MaxQty = Convert.ToDecimal(CustReader["max_qty"]);
                            objCustomerAggrement.UnitPrice = Convert.ToDecimal(CustReader["unit_price"]);
                            objCustomer.customerAggrements.Add(objCustomerAggrement);

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return objCustomer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerProfile GetCustomerProfile(string company, string location, int customerId,string email="")
        {
            CustomerProfile customerProfile = new CustomerProfile();
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(Convert.ToString(company), Convert.ToString(location));
                SqlCommand cmd;
                if (customerId > 0)
                {
                    cmd = new SqlCommand("APP_GetCustomer", SqlServerDBConnectionLogic.Maincn);
                    cmd.Parameters.AddWithValue("@customerid", Convert.ToInt32(customerId));
                }
                else
                {
                    cmd = new SqlCommand("APP_GetCustomerByMail", SqlServerDBConnectionLogic.Maincn);
                    cmd.Parameters.AddWithValue("@email", email);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customerProfile.CustomerId = Convert.ToInt32(reader["customer_id"]);
                            customerProfile.CustomerName = Convert.ToString(reader["customer_name"]);
                            customerProfile.Address1 = Convert.ToString(reader["address1"]);
                            customerProfile.Email = Convert.ToString(reader["email"]);
                            customerProfile.Mobile = Convert.ToString(reader["mobile"]);

                             customerProfile.APP_CustomerName = !reader.IsDBNull(reader.GetOrdinal("app_customer_name"))? Convert.ToString(reader["app_customer_name"]):"";
                            customerProfile.APP_Email = Convert.ToString(reader["app_email"]);
                            customerProfile.APP_Phone = Convert.ToString(reader["app_phone"]);
                            customerProfile.APP_Address1 = Convert.ToString(reader["app_address1"]);
                            customerProfile.APP_Address2 = Convert.ToString(reader["app_address2"]);
                            customerProfile.LoginKey = !string.IsNullOrEmpty(reader["app_password"].ToString()) ? BETaskCustomerAPI.Common.Encryption.EncryptString(reader["app_password"].ToString()) : null;


                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return customerProfile;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCustomer"></param>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool UpdateCustomerProfile(CustomerProfile objCustomerProfile)
        {
            bool spStasus = false;
            SqlServerDBConnectionLogic.SqlServerDBConnection(objCustomerProfile.Company, objCustomerProfile.Location);

            try
            {
                SqlCommand cmd = new SqlCommand("APP_UpdateCustomerProfile", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", Convert.ToString(objCustomerProfile.CustomerId));
                cmd.Parameters.AddWithValue("@APP_CustomerName", Convert.ToString(objCustomerProfile.APP_CustomerName));
                cmd.Parameters.AddWithValue("@APP_Phone", Convert.ToString(objCustomerProfile.APP_Phone));
                cmd.Parameters.AddWithValue("@APP_Email", Convert.ToString(objCustomerProfile.APP_Email));
                cmd.Parameters.AddWithValue("@APP_Address1", Convert.ToString(objCustomerProfile.APP_Address1));
                cmd.Parameters.AddWithValue("@APP_Address2", Convert.ToString(objCustomerProfile.APP_Address2));
                cmd.Parameters.AddWithValue("@APP_Password", Convert.ToString(objCustomerProfile.APP_Password));


                int output = cmd.ExecuteNonQuery();
                if (output == 1) { spStasus = true; } else { spStasus = false; };
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"Customer Id :{objCustomerProfile.CustomerId} ");
                sb.AppendLine($"Customer Name :{objCustomerProfile.CustomerName} ");
                sb.AppendLine($"Customer Phone :{objCustomerProfile.APP_Phone} ");
                sb.AppendLine($"Customer Email :{objCustomerProfile.APP_Email} ");
                sb.AppendLine($"Customer Password :{objCustomerProfile.APP_Password} ");
                sb.AppendLine($"Customer Address1 :{objCustomerProfile.APP_Address1} ");
                sb.AppendLine($"Customer Address2 :{objCustomerProfile.APP_Address2} ");

                MailSettingsDAL objMailSettingsDAL = new MailSettingsDAL();

                var mailSett = objMailSettingsDAL.GetMailSettings(objCustomerProfile.Company, objCustomerProfile.Location);

                objMailSettingsDAL.EmailError($"Customer Profile - {ex.Message}", sb.ToString(), true, mailSett, objCustomerProfile.Location);
                throw new Exception(ex.ToString());
            }
            return spStasus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerCoupon GetCustomerWalletInfo(string company, string location, int customerId)
        {
            string errorLog="";

            CustomerCoupon customerCoupon = null;
            try
            {
                SqlServerDBConnectionLogic.SqlServerDBConnection(Convert.ToString(company), Convert.ToString(location));

                SqlCommand cmd = new SqlCommand("APP_GetCustomerWalletInfo", SqlServerDBConnectionLogic.Maincn);
                cmd.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(customerId));
                cmd.CommandType = CommandType.StoredProcedure;

                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int isProfileUpdated = 2;
                            if (!string.IsNullOrEmpty(reader["AppEmail"].ToString()) || !string.IsNullOrWhiteSpace(reader["AppEmail"].ToString()))
                            {
                                isProfileUpdated = 1;
                            }
                            object amount = reader["Amount"];
                            if (!DBNull.Value.Equals(amount))
                            {
                                customerCoupon = new CustomerCoupon()
                                {
                                    CustomerId = customerId,
                                    WalletNumber = Convert.ToString(reader["WalletNumber"]),
                                    Amount = Convert.ToDecimal(reader["Amount"]),
                                    Qty = Convert.ToInt32(reader["Qty"] != DBNull.Value? reader["Qty"]:0),
                                    UnitPrice = Convert.ToInt32(reader["UnitPrice"] != DBNull.Value ? reader["UnitPrice"]:0),
                                    UnitPriceWithTax = Convert.ToInt32(reader["UnitPriceWithTax"] != DBNull.Value ?  reader["UnitPriceWithTax"]:0),
                                    LoginKey = !string.IsNullOrEmpty(reader["app_password"].ToString()) ? BETaskCustomerAPI.Common.Encryption.EncryptString(reader["app_password"].ToString()) : null,
                                    EnableOffer = Convert.ToInt32(reader["EnableOffer"] != DBNull.Value ? reader["EnableOffer"]:2),
                                    EnablePaymentGateway = Convert.ToInt32(reader["OnlinePayment"] != DBNull.Value ? reader["OnlinePayment"]:2),
                                    IsProfileUpdated = isProfileUpdated
                                };
                            }
                        }
                    }
                }

                SqlCommand cmd2 = new SqlCommand("APP_CustomerOffers", SqlServerDBConnectionLogic.Maincn);
                cmd2.Parameters.AddWithValue("@customerId", customerId);
                cmd2.CommandType = CommandType.StoredProcedure;

                using (System.Data.SqlClient.SqlDataReader CustReader = cmd2.ExecuteReader())
                {
                    if (CustReader.HasRows)
                    {
                        customerCoupon.CustomerOffers = new List<CustomerOffer>();
                        while (CustReader.Read())
                        {
                            CustomerOffer offer = new CustomerOffer()
                            {
                                OfferId = Convert.ToInt32(CustReader["offer_id"]),
                                Amount = Convert.ToDecimal(CustReader["amount"]),
                                OfferCategory = Convert.ToString(CustReader["category"]),
                                OfferName = Convert.ToString(CustReader["offer_name"]),
                                PaymentFee = BETaskCustomerAPI.Common.PaymentFee.GetPaymentFee(Convert.ToDecimal(CustReader["amount"])),
                                NetAmount = BETaskCustomerAPI.Common.PaymentFee.GetNetAmount(Convert.ToDecimal(CustReader["amount"])),
                                Notification = "Do not press back button while processing"
                            };
                            customerCoupon.CustomerOffers.Add(offer);

                        }

                    }
                }

                SqlCommand cmd3 = new SqlCommand("APP_GetCustomerDetailsById", SqlServerDBConnectionLogic.Maincn);
                cmd3.Parameters.AddWithValue("@customerId", customerId);
                cmd3.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd3.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            LogInDAL logInDAL = new LogInDAL();
                            objCustomer = logInDAL.FillCustomerDetails(reader, out errorLog);
                            customerCoupon.Customer = objCustomer;

                        }
                    }
                }

                if (customerCoupon != null && customerCoupon.CustomerOffers == null)
                {
                    customerCoupon.CustomerOffers = new List<CustomerOffer>();
                    customerCoupon.CustomerOffers.Add(new CustomerOffer
                    {
                        OfferId = 0,
                        Amount = 0,
                        OfferCategory = "No Offer",
                        PaymentFee = 0,
                        NetAmount = 0,
                        OfferName = "No Active Offers",
                        Notification = "Do not press back button while processing"
                    });
                    customerCoupon.EnablePaymentGateway = 2;
                }

            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message} \n ErrorLog:{errorLog}");
            }

            return customerCoupon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdateCustomerPassword(string company, string location, int customerId, string password)
        {
            bool spStasus = false;
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);

            try
            {
                SqlCommand cmd = new SqlCommand("APP_UpdateUserPassword", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(customerId));
                cmd.Parameters.AddWithValue("@password", Convert.ToString(password));
                int output = cmd.ExecuteNonQuery();
                if (output == 1) { spStasus = true; } else { spStasus = false; };
            }
            catch
            {
                throw;
            }
            return spStasus;
        }
    }
}