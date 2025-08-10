using BETaskAPI.Common;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using BETaskAPI.DAL;
using BETaskCustomerAPI.BAL;
using BETaskCustomerAPI;
using System.Threading.Tasks;
using System.Net.Http;

namespace BETaskAPI.Controllers
{

    public class TransactionController : ApiController
    {
        CCAvenueEncryption CCAvenueEncryption = new CCAvenueEncryption();

       
        [System.Web.Http.HttpPost]
        [Route("api/Transaction/SaveTransaction")]
        public async Task<IHttpActionResult> SaveTransactionAsync(Transaction objTransaction)
        {
            Logger.Info($"Request for SaveTransactionAsync {objTransaction.Company} - {objTransaction.Location} - {objTransaction.CustomerId} \n");

            TransactionDAL objTransactionDAL = new TransactionDAL();
            Response<EncResponseParams> response;
            try
            {
                var initialResult = await CCAvenueEncryption.GenerateEncRequest(Math.Round(objTransaction.Amount, 2));
                objTransaction.TId = initialResult.TId;
                objTransaction.ReferenceId = initialResult.OrderId;
                objTransaction.TrackingId = initialResult.TrackingId;
                objTransaction.OtherNotes = "";
                objTransaction.APPDate = new DateTime(objTransaction.APPDate.Year, objTransaction.APPDate.Month, objTransaction.APPDate.Day,objTransaction.APPDate.Hour,objTransaction.APPDate.Minute,objTransaction.APPDate.Second);
                string result = objTransactionDAL.SaveTransaction(objTransaction);
                if (!string.IsNullOrEmpty(result))
                {
                    Transaction trans = new Transaction()
                    {
                        ReferenceId = result
                    };
                    response = new Response<EncResponseParams>
                    {
                        IsError = false,
                        Message = "Transaction logged  sucessfully",
                        Result = initialResult,
                        TotalRecords = 1
                    };
                }
                else
                {
                    Logger.Error("Error occured while saving transaction !! string.IsNullOrEmpty(result) is true");
                    response = new Response<EncResponseParams>
                    {
                        IsError = true,
                        Message = "Error occured while saving transaction !!",
                        Result = null,
                        TotalRecords = 0
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error($"Exception occured while saving transaction . Please check the logs for more details", ex);
                response = new Response<EncResponseParams>
                {
                    IsError = true,
                    Message = "Error occured while saving transaction !!",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        //[System.Web.Http.HttpPost]
        //[Route("api/Transaction/SaveTransaction")]
        //public async Task<IHttpActionResult> SaveTransactionAsync(Transaction objTransaction)
        //{
        //    TransactionDAL objTransactionDAL = new TransactionDAL();
        //    Response<string> response;
        //    try
        //    {
        //        var initialResult = await CCAvenueEncryption.GenerateEncRequest(objTransaction.Amount);
        //        objTransaction.TId = initialResult.TId;
        //        objTransaction.ReferenceId = initialResult.OrderId;
        //        objTransaction.TrackingId = initialResult.TrackingId;

        //        objTransaction.APPDate = new DateTime(objTransaction.APPDate.Year, objTransaction.APPDate.Month, objTransaction.APPDate.Day);
        //        string result = objTransactionDAL.SaveTransaction(objTransaction);
        //        if (!string.IsNullOrEmpty(result))
        //        {
        //            Transaction trans = new Transaction()
        //            {
        //                ReferenceId = result
        //            };
        //            response = new Response<string>
        //            {
        //                IsError = false,
        //                Message = "Transaction logged  sucessfully",
        //                Result = trans.ReferenceId,
        //                TotalRecords = 1
        //            };
        //        }
        //        else
        //        {
        //            response = new Response<string>
        //            {
        //                IsError = true,
        //                Message = "Error occured while saving transaction !!",
        //                Result = null,
        //                TotalRecords = 0
        //            };
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Logger.Error($"Exception occured while saving transaction . Please check the logs for more details", ex);
        //        response = new Response<string>
        //        {
        //            IsError = true,
        //            Message = "Error occured while saving transaction !!",
        //            Result = null,
        //            TotalRecords = 0
        //        };
        //    }

        //    return Ok(response);
        //}


        [System.Web.Http.HttpPost]
        [Route("api/Transaction/UpdateResponse")]
        public IHttpActionResult UpdateTransactionReponse(TransactionResponse objTransactionResponse)
        {
            Logger.Info($"Request for UpdateTransactionReponse {objTransactionResponse.Company} - {objTransactionResponse.Location}  \n");

            TransactionDAL objTransactionDAL = new TransactionDAL();
            Response<string> response = null;
            string result = Convert.ToString(objTransactionResponse.StatusText) == "Successful" ? "Success" : "Failed";

            try
            {
                Logger.Info($"\n----------------------------------------------\nReferenceId" + objTransactionResponse.ReferenceId.ToString());
                Logger.Info("AmountRecieved:"+objTransactionResponse.AmountReceived.ToString());
                Logger.Info("StatusText:" + objTransactionResponse.StatusText.ToString());
                Logger.Info("Response:" + objTransactionResponse.Response.ToString()+"\n----------------------------------------------\n");


                objTransactionDAL.UpdateTransactionReponse(objTransactionResponse);
                response = new Response<string>
                {
                    IsError = false,
                    Message = "Transaction updated sucessfully",
                    Result = result,
                    TotalRecords = 1
                };
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occured while updating transaction  -  {objTransactionResponse.ReferenceId}. Please check the logs for more details", ex);
                response = new Response<string>
                {
                    IsError = true,
                    Message = "Error occured while updating transaction !!",
                    Result = result,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Transaction/TransactionHistory")]
        public IHttpActionResult TransactionHistory(string company, string location, string customerId,string dateFrom,string dateTo)
        {
            Logger.Info($"Request for TransactionHistory {company} - {location} - {customerId} dates {dateFrom} - {dateTo} \n");

            Response<List<TransactionHistory>> response = null;

            try
            {
                TransactionDAL transactionDAL = new TransactionDAL { };
                var transactionHistory = transactionDAL.TransactionHistory(company, location, customerId);
                if (transactionHistory != null && transactionHistory.Count > 0)
                {
                    response = new Response<List<TransactionHistory>>
                    {
                        IsError = false,
                        Message = "Transaction History retreaved",
                        Result = transactionHistory,
                        TotalRecords = transactionHistory.Count
                    };
                }
                else
                {
                    response = new Response<List<TransactionHistory>>
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
                response = new Response<List<TransactionHistory>>
                {
                    IsError = true,
                    Message = "Error while retreaving Delivery Hostory",
                    Result = null,
                    TotalRecords = 0
                };
            }
            return Ok(response);

        }


        [System.Web.Http.HttpPost]
        [Route("api/Transaction/GenerateEncRequest")]
        public async Task<IHttpActionResult> GenerateEncRequest(EncRequestParams encRequestParams)
        {
            Logger.Info($"EncRequestParams customerId: {encRequestParams.CustomerId} , {encRequestParams.Amount}");

            Response<EncResponseParams> response;
            try
            {
                if (ModelState.IsValid)
                {
                    CCAvenueEncryption CCAvenueEncryption = new CCAvenueEncryption();
                    var result = await CCAvenueEncryption.GenerateEncRequest(encRequestParams.Amount);
                    response = new Response<EncResponseParams>
                    {
                        IsError = false,
                        Message = $"Keys generated succefully",
                        Result = result,
                        TotalRecords = 1
                    };
                    
                }
                else
                {
                    Logger.Error($"Invalid Request");
                    response = new Response<EncResponseParams>
                    {
                        IsError = true,
                        Message = $"Invalid Request",
                        Result = null,
                        TotalRecords = 0
                    };
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception GenerateEncRequest for {encRequestParams.CustomerId}", ex);
                response = new Response<EncResponseParams>
                {
                    IsError = true,
                    Message = $"Error occured while GenerateEncRequest {encRequestParams.CustomerId} {ex.Message}",
                    Result = null,
                    TotalRecords = 0
                };
                
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("api/Transaction/SaveTransactionResponse")]
        public async Task<IHttpActionResult> SaveTransactionResponse(HttpRequestMessage request)
        {
            try
            {
                string jsonData = await request.Content.ReadAsStringAsync();

                TransactionDAL transactionDAL = new TransactionDAL();
                await transactionDAL.SaveTransactionResponseAsync(jsonData, "FALCON", "Dubai");
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
            return Ok("Success");
        }

        [HttpPost]
        [Route("api/Transaction/GetStatusCheckHash")]
        public async Task<IHttpActionResult> GetStatusCheckHashAsync([FromBody] string request)
        {
            try
            {
                CCAvenueEncryption cCAvenueEncryption = new CCAvenueEncryption();
                var resp =await cCAvenueEncryption.GenerateStatusCheckHashAsync(request);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
        }


        [HttpPost]
        [Route("api/Transaction/DecryptTransactionStatus")]
        public IHttpActionResult DecryptTransactionStatus([FromBody] string encResponse)
        {
            try
            {
                CCAvenueEncryption cCAvenueEncryption = new CCAvenueEncryption();
                var resp = cCAvenueEncryption.DecryptStatus(encResponse);
                return Ok(resp);

            }
            catch (Exception ex)
            {
                return Ok("Error");
            }
        }

    }
}
