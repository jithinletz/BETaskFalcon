using BETaskAPI.Common;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BETaskAPI.DAL;
using BETaskCustomerAPI.Common;
namespace BETaskCustomerAPI.Controllers
{
    public class EmailController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Email/SendForgotEmail")]
        public IHttpActionResult SendForgotEmail(string company, string location, int customerId, string email)
        {
            bool result = true;
            string message ;
            Response<Customer> response ;
            Customer Customer = new Customer();
            try
            {
                // #1 . Validate the customer Email 
                CustomerDAL objCustomerDAL = new CustomerDAL();
                CustomerProfile customer = objCustomerDAL.GetCustomerProfile(company, location, customerId,email);
                if (customer != null)
                {
                    if (customer.APP_Email == email) {
                        Random random = new Random();
                        int randomOTP = random.Next(1000, 9999);
                        string OtpCode = Convert.ToString(randomOTP);
                        Email.SendPasswordResetEmail(customer.CustomerName, email, OtpCode);

                        // #2 . Save OTP
                        OTPDAL objOTPDAL = new OTPDAL();
                        objOTPDAL.SaveOTP(company, location, customer.CustomerId, OtpCode);

                        result = false;
                        message = "OTP generated Sucessfully!!";
                        Customer.CustomerId = customer.CustomerId;
                    }
                    else
                    {
                        
                        result = true;
                        message = "Email id is not matching with the record!!";
                    }
                }
                else {
                    result = false;
                    message = "Customer is invalid !!";
                }
               
            }
            catch (Exception ex)
            {
                Logger.Error("Error while updating customer profile", ex);
                message = "Exception occured while sending email";
            }
            response = new Response<Customer>() {
                IsError=result,
                Message= message,
                Result=Customer
            };
            return Ok(response);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <param name="customerId"></param>
        /// <param name="OTP"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Email/ValidateOTP")]
        public IHttpActionResult ValidateOTP(string company, string location, int customerId, string OTP) {
            Logger.Error($"Validate OTP {location} , {customerId} , {OTP}");
            bool result = true;
            string message = string.Empty;
            Response<string> response = null;
            try
            {
                // #1 . Validate the OTP
                OTPDAL objOTPDAL = new OTPDAL();
                string LastActiveOTP = objOTPDAL.GetLastActiveOTP(company, location, customerId);
                if (LastActiveOTP != null)
                {
                    if (LastActiveOTP == OTP)
                    {
                        // #2 . Update date completed in OTP
                        objOTPDAL.UpdateOTPCompletedDate(company, location, customerId);
                        result = false;
                        message = "OTP validated  Sucessfully!!";
                    }
                    else
                    {
                        result = true;
                        //message = "OTP is not valid !!";
                        message = "Sorry this service is currently not available , please contact falcon customer service";
                    }
                }
                else
                {
                    result = true;
                    message = "No valid OTP available !!";
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error while updating customer profile", ex);
                message = "Exception occured while fetching OTP";
            }
            response = new Response<string>()
            {
                IsError = result,
                Message = message
            };
            return Ok(response);
        }

       
    }

}