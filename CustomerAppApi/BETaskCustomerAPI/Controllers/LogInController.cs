using BETaskAPI.Common;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BETaskAPI.DAL;

namespace BETaskAPI.Controllers
{
    public class LogInController : ApiController
    {


        /// <summary>
        /// Author :Prakash Tmr 13-12-2021
        /// USER LOGIN
        [System.Web.Http.HttpPost]
        [Route("api/LogIn/UserLogin")]
        public IHttpActionResult UserLogin(LogIn objLogIn)
        {
            Logger.Info($"\n User Login-> company={objLogIn.Company} , location ={objLogIn.Location} , userId={objLogIn.UserId} , password={objLogIn.Password} , Version={objLogIn.Version} , Device {TransactionDAL.GetAppType()}\n");

            LogInDAL objLogInDAL = new LogInDAL();
            Response<Customer> response = null;           
            Customer objCustomer = new Customer();
            try
            {
                objCustomer = objLogInDAL.UserLogin(objLogIn);
                if (objCustomer.customerAggrements != null && objCustomer.customerAggrements.Count > 0)
                {
                    Logger.Info($"Login success {objCustomer.CustomerName}");
                    response = new Response<Customer>
                    {
                        IsError = false,
                        Message = "You have successfully logged in",
                        Result = objCustomer,
                        TotalRecords = objCustomer.customerAggrements.Count
                    };
                }
                else
                {
                    Logger.Error($"Login failed , Agreement count->{objCustomer.customerAggrements.Count}");
                    response = new Response<Customer>
                    {
                        IsError = true,
                        Message = "Login faild, No agreements found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Login Failed", ex);
                response = new Response<Customer>
                {
                    IsError = true,
                    Message = " Login Failed ! ",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }
    }
}
