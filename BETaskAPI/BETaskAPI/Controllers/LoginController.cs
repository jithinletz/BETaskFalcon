using BETaskAPI.Common;
using BETaskAPI.DAL.DAL;
using BETaskAPI.DAL.EDMX;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BETaskAPI.Controllers
{
   // [ExceptionHandling]
    public class LoginController : ApiController
    {

        UserDAL objUser = null;

        /// <summary>
        /// 
        /// </summary>
        public LoginController() {
            objUser = new UserDAL();
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult TestTestAPI()
        {
            Response<string> response = null;
            try
            {

                Logger.Error("Test  successfully");
                objUser.TestAPI();
               
                    response = new Response<string>()
                    {
                        IsError = false,
                        Message = "Test  successfully",
                        Result = objUser.TestAPI()
            };
               
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while login . Please check the logs for more details", ex);
                response = new Response<string>()
                {
                    IsError = true,
                    Message = "Test",
                    Result = ex.Message,
                };
            }
            return Ok(response);
        }

        [System.Web.Http.Route("api/Login/GetCompany")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCompany()
        {
            Response<BETaskAPI.Models.Company> response = null;
            try
            {


               BETaskAPI.DAL.EDMX.company _company= objUser.GetCompany();
                int invoiceShare = 2,showCoupon=2,doNewScreen=2;
                string currency = "AED";
                try
                {
                    invoiceShare= Convert.ToInt32(ConfigurationManager.AppSettings["EnableInvoiceShare"]);
                    invoiceShare = invoiceShare== 0 ? 2 : invoiceShare;
                    currency = ConfigurationManager.AppSettings["DefaultCurrency"].ToString();
                    showCoupon = Convert.ToInt32(ConfigurationManager.AppSettings["ShowCouponNumber"]);
                    showCoupon = showCoupon == 0 ? 2 : showCoupon;
                    doNewScreen = Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["DoNewScreen"]));
                }
                catch { }

                BETaskAPI.Models.Company company = new Company
                {

                    CompanyName=_company.company_name,
                    Adress=_company.address1,
                    City=_company.city,
                    Email=_company.email,
                    Phone=_company.phone,
                    TRN=_company.tin,
                    EnableInvoiceShare=invoiceShare,
                    DefaultCurrency=currency,
                    ShowCoupon=showCoupon,
                    DoNewScreen=doNewScreen

                };

                response = new Response<BETaskAPI.Models.Company>()
                {
                    IsError = false,
                    Message = "Company details succesfully fetched",
                    Result = company,
                    TotalRecords=1
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while getting company . Please check the logs for more details", ex);
                response = new Response<BETaskAPI.Models.Company>()
                {
                    IsError = true,
                    Message = ex.Message,
                    Result = null,
                };
            }
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginController"></param>
        /// <returns></returns> 
        /// HttpGet
        [System.Web.Http.HttpPost]
        public IHttpActionResult ValidateUser(Login loginData) {
            Response<LoginResponse> response = null; 
            try
            {
               

                Logger.Info(string.Format("Start - Validate User - {0}", loginData.UserName));
                employee result = objUser.ValidateUser(loginData.UserName, loginData.Password);
                if (result!=null && result.employee_id>0)
                {
                    ClearCacheForEmployee(result.employee_id);
                    int userType = 2;
                    if (result.designation!=null && result.designation.ToLower() == "executive")
                        userType = 1;
                    else
                        userType = 2;
                    LoginResponse login = new LoginResponse {
                        UserId = result.employee_id,
                        UserName = result.username,
                        EmployeeName = String.Format("{0} {1}",result.first_name,result.last_name),
                        UserType= userType
                    };
                   // Logger.Info(string.Format("Validated the user details - {0}",loginData.UserName));
                    response = new Response<LoginResponse>()
                    {
                        IsError = false,
                        Message = "Login successfully",
                        Result = login
                    };
                }
                else {
                   // Logger.Info(string.Format("Invalid login credentials - {0}", loginData.UserName));
                    response = new Response<LoginResponse>()
                    {
                        IsError = false,
                        Message = "Invalid login credentials",
                        Result = null,
                    };
                }
            }
            catch(Exception ex) {
                Logger.Error("Exception occured while login .", ex);
                response = new Response<LoginResponse>()
                {
                    IsError = true,
                    Message = "Exception occured while login . Please check the logs for more details",
                    Result = null,
                };
            }
            return Ok(response);
        }

        public void ClearCacheForEmployee(int employeeId)
        {
            try
            {
                var cacheKey = $"CustomerList_{employeeId}";

                // Clear the cache for the specified key
                System.Runtime.Caching.MemoryCache.Default.Remove(cacheKey);
            }
            catch { }
        }


    }
}
