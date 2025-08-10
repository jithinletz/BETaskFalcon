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
    public class CategoryController : ApiController
    {

        [System.Web.Http.HttpGet]
        [Route("api/Category/GetItemCategory")]

        public IHttpActionResult GetItemCategory(String company, String location)
        {
            Logger.Info($"Request for GetItemCategory {company} - {location}\n");

            CategoryDAL objCategoryDAL = new CategoryDAL();
            Response<List<Category>> response = null;
            List<Category> lstCategory = new List<Category>();
            try
            {
                lstCategory = objCategoryDAL.GetCategoryImagePath(company, location);
                if (lstCategory != null && lstCategory.Count > 0)
                {
                    response = new Response<List<Category>>
                    {
                        IsError = false,
                        Message = "Category image path succefully retreaved",
                        Result = lstCategory,
                        TotalRecords = lstCategory.Count
                    };
                }
                else
                {
                    response = new Response<List<Category>>
                    {
                        IsError = false,
                        Message = "No Category found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Category . Please check the logs for more details", ex);
                response = new Response<List<Category>>
                {
                    IsError = true,
                    Message = "Error while retreaving Category image path",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }
    }
}
