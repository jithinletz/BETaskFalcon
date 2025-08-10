using BETaskAPI.Common;
using BETaskAPI.DAL.EDMX;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BETaskAPI.DAL.DAL;
namespace BETaskAPI.Controllers
{
    public class ItemController : ApiController
    {
        ItemDAL itemDALObj = null;

        public ItemController() {
            itemDALObj = new ItemDAL();
        }

         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetItems()
        {
            Response<List<Item>> response = null;
            List<Item> items = new List<Item>();
            try
            {
                List<item> listItem = itemDALObj.GetItems();
                foreach (item _item in listItem)
                {
                    items.Add(new Item()
                    {
                        ItemId = _item.item_id,
                        ItemName = _item.item_name
                    });
                }

                response = new Response<List<Item>>()
                {
                    IsError = false,
                    Message = " Item details fetched sucessfully",
                    Result = items,
                    TotalRecords = items.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer . Please check the logs for more details", ex);
                response = new Response<List<Item>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching items . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

         
    }
}
