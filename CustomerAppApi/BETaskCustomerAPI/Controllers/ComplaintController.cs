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
    
    public class ComplaintController : ApiController
    {
        /// <summary>
        /// Author :Prakash Tmr 10-12-2021
        /// getting complaint details by complaint id
        [System.Web.Http.HttpGet]
        [Route("api/Complaint/GetComplaintDetails")]
        public IHttpActionResult GetComplaintDetails(string company, string location,int complaintId)
        {
            
            ComplaintDAL objComplaintDAL = new ComplaintDAL();
            Response<List<Complaint>> response = null;
            List<Complaint> lstComplaint = new List<Complaint>();
            try
            {
                lstComplaint = objComplaintDAL.GetComplaintDetails(company,location,complaintId);
                if (lstComplaint != null && lstComplaint.Count > 0)
                {
                    response = new Response<List<Complaint>>
                    {
                        IsError = false,
                        Message = "Complaint Details succefully retreaved",
                        Result = lstComplaint,
                        TotalRecords = lstComplaint.Count
                    };
                }
                else
                {
                    response = new Response<List<Complaint>>
                    {
                        IsError = false,
                        Message = "No Complaint found",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching Complaint . Please check the logs for more details", ex);
                response = new Response<List<Complaint>>
                {
                    IsError = true,
                    Message = "Error while retreaving Complaint Details",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        /// <summary>
        /// Author :Prakash Tmr 10-12-2021
        /// getting complaint details by complaint id
        [System.Web.Http.HttpPost]
        [Route("api/Complaint/SaveComplaint")]
        public IHttpActionResult SaveComplaint(Complaint objComplaint,string company,string location)
        {
            bool result = false;
            ComplaintDAL objComplaintDAL = new ComplaintDAL();
            Response<List<Complaint>> response = null;
        
            try
            {
                result = objComplaintDAL.SaveComplaint(objComplaint, company, location);
                if (result)
                {
                    response = new Response<List<Complaint>>
                    {
                        IsError = false,
                        Message = $"Dear customer your {objComplaint.ComplaintType} has been submitted ",
                        Result = null,
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<List<Complaint>>
                    {
                        IsError = false,
                        Message = $"Error occured while submiting {objComplaint.ComplaintType}. please try again ",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error($"Exception occured while submiting {objComplaint.ComplaintType} . Please check the logs for more details", ex);
                response = new Response<List<Complaint>>
                {
                    IsError = true,
                    Message = "Error while submitting Complaint Details",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }        

    }
}
