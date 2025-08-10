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
    public class ItemController : ApiController
    {

        /// <summary>
        /// Author :Prakash Tmr 10-12-2021
        /// getting item all details by item name
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetItemDetails")]        
        public IHttpActionResult GetItemDetails(string company, string location,String itemName)
        {
            ItemDAL objItemDAL = new ItemDAL();
            Response<List<Item>> response = null;
            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = objItemDAL.GetItemDetails(company,location,itemName);
                if (lstItem != null && lstItem.Count > 0)
                {
                    response = new Response<List<Item>>
                    {
                        IsError = false,
                        Message = "Item Details succefully retreaved",
                        Result = lstItem,
                        TotalRecords = lstItem.Count
                    };
                }
                else
                {
                    response = new Response<List<Item>>
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
                response = new Response<List<Item>>
                {
                    IsError = true,
                    Message = "Error while retreaving Item Details",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        /// <summary>
        /// Author :Prakash Tmr 10-12-2021
        /// getting item details by category name
        [System.Web.Http.HttpGet]
        [Route("api/Item/GetItemByCategory")]

        public IHttpActionResult GetItemByCategory(string company, string location,string categoryName)
        {
            ItemDAL objItemDAL = new ItemDAL();
            Response<List<Item>> response = null;
            List<Item> lstItem = new List<Item>();
            try
            {
                lstItem = objItemDAL.GetItemDetailsByCategory(company,location,categoryName);
                if (lstItem != null && lstItem.Count > 0)
                {
                    response = new Response<List<Item>>
                    {
                        IsError = false,
                        Message = "Item Details succefully retreaved",
                        Result = lstItem,
                        TotalRecords = lstItem.Count
                    };
                }
                else
                {
                    response = new Response<List<Item>>
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
                response = new Response<List<Item>>
                {
                    IsError = true,
                    Message = "Error while retreaving Item Details",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Item/GetItemDetailsByItemId")]
        public IHttpActionResult GetItemDetailsByItemId(string company, string location, int itemId)
        {
            ItemDAL objItemDAL = new ItemDAL();
            Response<Item> response = null;
            Item objItem = new Item();
            try
            {
                objItem = objItemDAL.GetItemDetailsById(company, location, itemId);
                if (objItem != null && objItem.ItemId > 0)
                {
                    response = new Response<Item>
                    {
                        IsError = false,
                        Message = "Item Details succefully retreaved",
                        Result = objItem,
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<Item>
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
                response = new Response<Item>
                {
                    IsError = true,
                    Message = "Error while retreaving Item Details by item id",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }
    }
}
