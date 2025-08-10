using BETaskAPI.Common;
using EDMX= BETaskAPI.DAL.EDMX;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BETaskAPI.DAL.DAL;
using System.Configuration;
using System.IO;


namespace BETaskAPI.Controllers
{
    public class UploadController : ApiController
    {
        [Route("api/Upload/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            try
            {
                Response<string> response = null;
                //Create the Directory.
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (System.Web.HttpContext.Current.Request.Files != null)
                {
                    //Fetch the File.
                    System.Web.HttpPostedFile postedFile = System.Web.HttpContext.Current.Request.Files[0];

                    //setting up table fields
                    int customerId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["CustomerId"]);
                    int employeeId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["EmployeeId"]);
                    string documentType = Convert.ToString(System.Web.HttpContext.Current.Request.Form["DocumentType"]);
                    string description = Convert.ToString(System.Web.HttpContext.Current.Request.Form["Description"]);
                    string fileName = Path.GetFileName(postedFile.FileName).ToLower();
                    string extention = System.IO.Path.GetExtension(fileName);
                    string folderPath = "Uploads";
                    string newFileName = $"{DateTime.Now.Ticks}{extention}";
                    //--------------------------------------

                    //Fetch the File Name.

                    string filename = $"{path}\\{newFileName}";
                    if (!File.Exists(filename))
                    {
                        UploadDocument(newFileName, folderPath, documentType, description, customerId, employeeId);
                        //Save the File.
                        postedFile.SaveAs(path + newFileName);
                        response = new Response<string>
                        {
                            Message = "Success",
                            TotalRecords = 1,
                            Result = "betask_srb_test.letzservices.com/Uploads/" + newFileName
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response = new Response<string>
                        {
                            Message = "File already exist. Chagne file name try again",
                            TotalRecords = 0,
                            Result = "Failed"
                        };
                        return Request.CreateResponse(HttpStatusCode.Ambiguous, response);

                    }
                }
                else
                {
                    response = new Response<string>
                    {
                        Message = "No content",
                        TotalRecords = 0,
                        Result = "Failed"
                    };
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No content");
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex.Message);
            }

            //Send OK Response to Client.
        }
        private void UploadDocument(string fileName, string filePath, string documentType, string description, int customerId, int employeeId)
        {
            try
            {
                var date = System.TimeZoneInfo.ConvertTimeFromUtc(
                    DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));

                EDMX.customer_upload customer_Upload = new EDMX.customer_upload
                {
                    customer_id = customerId,
                    description = description,
                    document_type = documentType,
                    employee_id = employeeId,
                    filename = fileName,
                    filepath = filePath,
                    status = 1,
                    upload_time = date
                };

                DAL.DAL.UploadDAL uploadDAL = new UploadDAL();
                uploadDAL.UploadDocument(customer_Upload);
            }
            catch
            { new Exception("Error while saving document details"); }
        }
    }
}
