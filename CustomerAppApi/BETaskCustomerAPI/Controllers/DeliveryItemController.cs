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
    public class DeliveryItemController : ApiController
    {


        /// <summary>
        /// Author :Prakash Tmr 10-12-2021
        /// Dashboard Delivery Request
        [System.Web.Http.HttpGet]
        [Route("api/DeliveryItem/DashboardDeliveryRequest")]
        public IHttpActionResult DashboardDeliveryRequest(string company, string location, string customerId, DateTime fromDate , DateTime toDate )
        {
            Logger.Info($"Request for DashboardDeliveryRequest {company} - {location} - {customerId} \n");

            DeliveryItemDAL objDeliveryItemDAL = new DeliveryItemDAL();
            Response<DeliveryHistory> response = null;
            //List<DeliveryItem> lstDeliveryItem = new List<DeliveryItem>();
            DeliveryHistory DeliveryHistory = new DeliveryHistory();
            try
            {
                DeliveryHistory = objDeliveryItemDAL.DashboardDeliveryRequest(company, location, customerId, fromDate, toDate);
                if (DeliveryHistory != null && DeliveryHistory.listDeliveryItem.Count > 0 || DeliveryHistory.listRechargeHistory.Count>0)
                {
                    response = new Response<DeliveryHistory>
                    {
                        IsError = false,
                        Message = "Delivery History retreaved",
                        Result = DeliveryHistory,
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<DeliveryHistory>
                    {
                        IsError = false,
                        Message = "No Item found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Item . Please check the logs for more details", ex);
                response = new Response<DeliveryHistory>
                {
                    IsError = true,
                    Message = "Error while retreaving Delivery Hostory",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }
        //SaveDeliveryRequest

        /// <summary>
        /// Author :Prakash Tmr 10-12-2021
        /// Save DeliveryRequest
        [System.Web.Http.HttpPost]
        [Route("api/DeliveryItem/SaveDeliveryRequest")]
        public IHttpActionResult SaveDeliveryRequest(string company, string location,DeliveryRequest objDeliveryRequest )
        {
            Logger.Info($"Request for SaveDeliveryRequest {company} - {location} -{objDeliveryRequest.CustomerId} \n");

            bool result = false;
            DeliveryItemDAL objDeliveryItemDAL = new DeliveryItemDAL();
            Response<List<DeliveryRequest>> response = null;
            try
            {
                result = objDeliveryItemDAL.SaveDeliveryRequest(objDeliveryRequest, company, location);
                if (result)
                {
                    response = new Response<List<DeliveryRequest>>
                    {
                        IsError = false,
                        Message = "Order successfully placed , we will contact you soon. Thanks for being our valued customer",
                        Result = null,
                        TotalRecords = 1
                    };
                    objDeliveryItemDAL.SetMailContent(company, location, objDeliveryRequest);
                }
                else
                {
                    Logger.Error($"Unknown error");
                    response = new Response<List<DeliveryRequest>>
                    {

                        IsError = true,
                        Message = $"Sorry something went wrong, Order failed. Please contact us  ",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error($"Exception occured while placing Order Please check the logs for more details", ex);
                response = new Response<List<DeliveryRequest>>
                {
                    IsError = true,
                    Message = $"Error while submitting placing Order {ex.Message}",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

    }
}
