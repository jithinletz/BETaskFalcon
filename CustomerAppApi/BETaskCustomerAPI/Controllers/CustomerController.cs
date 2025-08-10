using BETaskAPI.Common;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BETaskAPI.DAL;
using System.Configuration;

namespace BETaskAPI.Controllers
{
    public class CustomerController : ApiController
    {

        /// <summary>
        /// Author :Prakash Tmr 13-12-2021
        /// get customer details and customer eggrements parameter customer id
        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomer")]
        public IHttpActionResult GetCustomer(string company, string location, int customerId)
        {

            DeliveryItemDAL objDeliveryItemDAL = new DeliveryItemDAL();

            objDeliveryItemDAL.SetMailContent(company, location, null);
            Logger.Info($"Request for GetCustomer {company} - {location} - {customerId} \n");

            CustomerDAL objCustomerDAL = new CustomerDAL();
            Response<Customer> response = null;
            Customer objCustomer = new Customer();
            try
            {
                objCustomer = objCustomerDAL.GetCustomer(company,location,customerId);
                if (objCustomer.customerAggrements != null && objCustomer.customerAggrements.Count > 0)
                {
                    response = new Response<Customer>
                    {
                        IsError = false,
                        Message = "Customer details successfully retreved",
                        Result = objCustomer,
                        TotalRecords = objCustomer.customerAggrements.Count
                    };
                }
                else
                {
                    response = new Response<Customer>
                    {
                        IsError = false,
                        Message = "No details found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Customer . Please check the logs for more details", ex);
                response = new Response<Customer>
                {
                    IsError = true,
                    Message = "Error while getting customer details ",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="company"></param>
    /// <param name="location"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerProfile")]
        public IHttpActionResult GetCustomerProfile(string company, string location, int customerId)
        {
            Logger.Info($"Request for GetCustomerProfile {company} - {location} - {customerId} \n");

            CustomerDAL objCustomerDAL = new CustomerDAL();
            Response<CustomerProfile> response = null;
            CustomerProfile objCustomerProfile = new CustomerProfile();
            try
            {
                objCustomerProfile = objCustomerDAL.GetCustomerProfile(company, location, customerId);
                if (objCustomerProfile != null)
                {
                    response = new Response<CustomerProfile>
                    {
                        IsError = false,
                        Message = "Customer details successfully retreved",
                        Result = objCustomerProfile,
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<CustomerProfile>
                    {
                        IsError = true,
                        Message = "No details found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Customer . Please check the logs for more details", ex);
                response = new Response<CustomerProfile>
                {
                    IsError = true,
                    Message = "Error while getting customer details ",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objCustomer"></param>
        /// <param name="company"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [Route("api/Customer/UpdateCustomerProfile")]
        public IHttpActionResult UpdateCustomerProfile(CustomerProfile objCustomerProfile)
        {
            Logger.Info($"Request for GetCustomerProfile1 {objCustomerProfile.Company} - {objCustomerProfile.Location} - {objCustomerProfile.CustomerId} \n");

            bool result = false;
            CustomerDAL objCustomerDAL = new CustomerDAL();
            Response<string> response = null;

            try
            {

                if (string.IsNullOrEmpty(objCustomerProfile.APP_Password))
                {
                    Logger.Info($"No Password Update profile error 2 - Name : {objCustomerProfile.APP_CustomerName} , Email : {objCustomerProfile.APP_Email}, Phone : {objCustomerProfile.APP_Phone}, Password : {objCustomerProfile.APP_Password} , Address1 : {objCustomerProfile.APP_Address1}, Address2 : {objCustomerProfile.APP_Address2} - {objCustomerProfile.Location}");
                    response = new Response<string>
                    {
                        IsError = true,
                        Message = " Password is empty ",
                        Result = null,
                        TotalRecords = 0
                    };
                    return Ok(response);
                }

                result = objCustomerDAL.UpdateCustomerProfile(objCustomerProfile);
                if (result)
                {
                    response = new Response<string>
                    {
                        IsError = false,
                        Message ="Customer profile updated sucessfull",
                        Result = null,
                        TotalRecords = 1
                    };
                }
                else
                {
                    Logger.Info($"Update profile error 1 - Id : {objCustomerProfile.CustomerId} Name : {objCustomerProfile.APP_CustomerName} , Email : {objCustomerProfile.APP_Email}, Phone : {objCustomerProfile.APP_Phone}, Password : {objCustomerProfile.APP_Password} , Address1 : {objCustomerProfile.APP_Address1}, Address2 : {objCustomerProfile.APP_Address2} - {objCustomerProfile.Location}");

                    response = new Response<string>
                    {
                        IsError = true,
                        Message = "Currently, we are unable to update your profile,\n  We recommend trying again later or please contacting the vendor",
                        Result = null,
                        TotalRecords = 0
                    };

                   

                }
            }

            catch (Exception ex)
            {
                Logger.Error($" Customer Profile Exception {objCustomerProfile.CustomerId}", ex);
                Logger.Info($"Update profile error 2 - Name : {objCustomerProfile.APP_CustomerName} , Email : {objCustomerProfile.APP_Email}, Phone : {objCustomerProfile.APP_Phone}, Password : {objCustomerProfile.APP_Password} , Address1 : {objCustomerProfile.APP_Address1}, Address2 : {objCustomerProfile.APP_Address2} - {objCustomerProfile.Location}");
                response = new Response<string>
                {
                    IsError = true,
                    Message = "Currently, we are unable to update your profile, \n We recommend trying again later or contacting the vendor ",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerWalletInfo")]
        public IHttpActionResult GetCustomerWalletInfo(string company, string location, int customerId)
        {
           

            Logger.Info($"Request for GetCustomerWalletInfo {company} - {location} - {customerId} \n");

            CustomerDAL objCustomerDAL = new CustomerDAL();
            Response<CustomerCoupon> response = null;
            CustomerCoupon objCustomerCoupon = new CustomerCoupon();
            try
            {
               
                objCustomerCoupon = objCustomerDAL.GetCustomerWalletInfo(company, location, customerId);
                if (objCustomerCoupon != null)
                {
                    if (objCustomerCoupon.EnablePaymentGateway != 2)
                        objCustomerCoupon.EnablePaymentGateway = GetPaymentGatewayStatus();
                    
                    response = new Response<CustomerCoupon>
                    {
                        IsError = false,
                        Message = "Customer details successfully retreved",
                        Result = objCustomerCoupon,
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<CustomerCoupon>
                    {
                        IsError = false,
                        Message = "No details found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Customer . Please check the logs for more details", ex);
                response = new Response<CustomerCoupon>
                {
                    IsError = true,
                    Message = "Error while getting customer wallet details ",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }



        [System.Web.Http.HttpPost]
        [Route("api/Customer/UpdateCustomerPassword")]
        public IHttpActionResult UpdateCustomerPassword(string company, string location, int customerId,string password)
        {
            Logger.Info($"Request for UpdateCustomerPassword {company} - {location} - {customerId} \n");

            bool result = false; 
            Response<string> response = null;
            CustomerDAL objCustomerDAL = new CustomerDAL();
            try
            {
                result = objCustomerDAL.UpdateCustomerPassword(company, location, customerId, password);
                if (result)
                {
                    response = new Response<string>
                    {
                        IsError = false,
                        Message = "Customer password updated sucessfull",
                        Result = null,
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<string>
                    {
                        IsError = true,
                        Message = "Error while updating customer password",
                        Result = null,
                        TotalRecords = 1
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Error while updating customer profile", ex);
                response = new Response<string>
                {
                    IsError = true,
                    Message = "Error while updating customer password",
                    Result = null,
                    TotalRecords = 1
                };
            }

            return Ok(response);
        }

        private int GetPaymentGatewayStatus()
        {
            int enableOnlinePayment = 1;
            try
            {
                enableOnlinePayment = Convert.ToInt32(ConfigurationManager.AppSettings["EnableOnlinePayment"]);
            }
            catch { }
            return enableOnlinePayment;
        }
    }

    
         

}

