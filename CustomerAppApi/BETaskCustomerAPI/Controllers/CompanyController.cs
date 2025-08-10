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
    /// <summary>
    /// Author :Prakash Tmr 09-12-2021
    /// 0, In Valid Request
    /// 1. Valid       
    /// 2. Invalid Company       
    /// </summary>
    /// <param name="CompanyName"></param>
    /// <returns></returns>
    public class CompanyController : ApiController
    {
        [System.Web.Http.HttpGet]
        [Route("api/Company/GetCompanyLocations")]

        /// <summary>
        /// Author :Prakash Tmr 09-12-2021
        /// getting company location/city
        public IHttpActionResult GetCompanyLocations(String company)
        {
            CompanyDAL objCompanyDAL = new CompanyDAL();
            Response<List<string>> response = null;
            List<string> listLocations = new List<string>();
            try
            {
                listLocations = objCompanyDAL.GetCompanyLocations(company);
                if (listLocations != null && listLocations.Count > 0)
                {
                    response = new Response<List<string>>
                    {
                        IsError = false,
                        Message = "Company locations succefully retreaved",
                        Result = listLocations,
                        TotalRecords = listLocations.Count
                    };
                }
                else
                {
                    response = new Response<List<string>>
                    {
                        IsError = false,
                        Message = "No locations found",
                        Result = null,
                        TotalRecords =0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Company . Please check the logs for more details", ex);
                response = new Response<List<string>>
                {
                    IsError = true,
                    Message = "Error while retreaving locations",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }


        /// <summary>
        /// Author :Prakash Tmr 09-12-2021
        /// getting all company details
        [System.Web.Http.HttpGet]
        [Route("api/Company/GetCompanyDetails")]
        public IHttpActionResult GetCompanyDetails(String company, String location)
        {
            CompanyDAL objCompanyDAL = new CompanyDAL();
            Response<List<Company>> response = null;
            List<Company> lstCompany = new List<Company>();
            try
            {
                lstCompany = objCompanyDAL.GetCompanyDetails(company,location);
                if (lstCompany != null && lstCompany.Count > 0)
                {
                    response = new Response<List<Company>>
                    {
                        IsError = false,
                        Message = "Company Details succefully retreaved",
                        Result = lstCompany,
                        TotalRecords = lstCompany.Count
                    };
                }
                else
                {
                    response = new Response<List<Company>>
                    {
                        IsError = false,
                        Message = "No Company found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Company . Please check the logs for more details", ex);
                response = new Response<List<Company>>
                {
                    IsError = true,
                    Message = "Error while retreaving Company Details",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }
    }
}
