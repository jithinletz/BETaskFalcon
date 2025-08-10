using BETaskAPI.Common;
using BETaskAPI.DAL.EDMX;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BETaskAPI.DAL.DAL;
using System.Configuration;

namespace BETaskAPI.Controllers
{
    public class ReportController : ApiController
    {

        ReportDAL customerDALObj = null;

        public ReportController() {
            customerDALObj = new ReportDAL();
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/GetItemDeliveryReport")]
        public IHttpActionResult GetItemDeliveryReport(int employeeId, DateTime fromDate, DateTime toDate)
        {
            Response<List<ItemDeliveryReport>> response = null;
            List<ItemDeliveryReport> rpt = new List<ItemDeliveryReport>();
            try
            {
                fromDate = ConvertDateServerFormat(fromDate);
                toDate = ConvertDateServerFormat(toDate);
                List<RPT_ItemDeliverySummary_Result> result = customerDALObj.RPTItemDeliverySummary(employeeId, fromDate, toDate);
                foreach (RPT_ItemDeliverySummary_Result obj in result) {
                    rpt.Add(new ItemDeliveryReport() {
                        ItemName = obj.item_name,
                        ItemId = obj.item_id,
                        Total = obj.Total == null ? 0 : obj.Total,
                        Completed = obj.Completed == null ? 0 : obj.Completed

                    });

                }

                response = new Response<List<ItemDeliveryReport>>()
                {
                    IsError = false,
                    Message = " GetItemDeliveryReport fetched sucessfully",
                    Result = rpt,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching report -  GetItemDeliveryReport. Please check the logs for more details", ex);
                response = new Response<List<ItemDeliveryReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  GetItemDeliveryReport . Please check the logs for more details"
                };
            }

            return Ok(response);
        }
        [System.Web.Http.HttpGet]
        [Route("api/Report/GetItemDeliveryReturnReport")]
        public IHttpActionResult GetItemDeliveryReturnReport(int employeeId, DateTime fromDate, DateTime toDate)
        {
            Response<List<RPT_ItemDeliveryReturnSummary_Result>> response = null;
            List<RPT_ItemDeliveryReturnSummary_Result> rpt = new List<RPT_ItemDeliveryReturnSummary_Result>();
            List<RPT_ItemDeliveryReturnSummary_Result> result = null;
            try
            {
                fromDate = ConvertDateServerFormat(fromDate);
                toDate = ConvertDateServerFormat(toDate);
                 result = customerDALObj.RPTItemDeliveryReturnSummary(employeeId, fromDate, toDate);
                //foreach (RPT_ItemDeliveryReturnSummary_Result obj in result)
                //{
                //    rpt.Add(new RPT_ItemDeliveryReturnSummary_Result()
                //    {
                //        ite

                //    });

                //}

                response = new Response<List<RPT_ItemDeliveryReturnSummary_Result>>()
                {
                    IsError = false,
                    Message = " GetItemDeliveryReturnReport fetched sucessfully",
                    Result = result,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching report -  GetItemDeliveryReport. Please check the logs for more details", ex);
                response = new Response<List<RPT_ItemDeliveryReturnSummary_Result>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  GetItemDeliveryReturnReport . Please check the logs for more details"
                };
            }

            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        [Route("api/Report/GetDailyCollectionSummary")]
        public IHttpActionResult GetDailyCollectionSummary(int employeeId, DateTime fromDate, DateTime toDate)
        {
            Response<List<DailyCollectionReport>> response = null;
            List<DailyCollectionReport> rpt = new List<DailyCollectionReport>();
            try
            {
                fromDate = ConvertDateServerFormatWithStartTime(fromDate);
                toDate = ConvertDateServerFormatWithEndTime(toDate);
                List<RPT_DailyCollectionSummary_Result> result = customerDALObj.RPTDailyCollectionSummary(employeeId, fromDate, toDate);
                foreach (RPT_DailyCollectionSummary_Result obj in result)
                {
                    DateTime dtColl = new DateTime();
                    if (obj.Date_Collected != null)
                        dtColl =DateTime.Parse( obj.Date_Collected.ToString());
                    rpt.Add(new DailyCollectionReport()
                    {
                        DateCollected = dtColl.ToString("dd/MM/yyyy"),
                        AmountClosed=obj.Amount_Closed,
                        TotalCollected=obj.Total_Collected,
                        BalanceAmount= obj.Total_Collected- obj.Amount_Closed

                    });

                }

                response = new Response<List<DailyCollectionReport>>()
                {
                    IsError = false,
                    Message = " GetDailyCollectionSummary details fetched sucessfully",
                    Result = rpt,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching report -  GetDailyCollectionSummary. Please check the logs for more details", ex);
                response = new Response<List<DailyCollectionReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  GetDailyCollectionSummary . Please check the logs for more details"
                };
            }

            return Ok(response);
        }



        [System.Web.Http.HttpGet]
        [Route("api/Report/GetDailyCollectionDetail")]
        public IHttpActionResult GetDailyCollectionDetail(int employeeId, DateTime date)
        {
            Response<List<DailyCollectionDetailReport>> response = null;
            List<DailyCollectionDetailReport> rpt = new List<DailyCollectionDetailReport>();
            try
            {
                date = ConvertDateServerFormat(date); 
                List<RPT_DailyCollectionDetails_Result> result = customerDALObj.RPTDailyCollectionDetails(employeeId, date);
                foreach (RPT_DailyCollectionDetails_Result obj in result)
                {
                    rpt.Add(new DailyCollectionDetailReport()
                    {
                       CollectedAmount= obj.collected_amount,
                       CustomerName=obj.customer_name,
                       DeliveryTime=obj.delivery_time.ToString("dd/MM/yyyy hh:mm:ss tt"),
                       NetAmount=obj.net_amount,
                       Status=obj.Status
                    });

                }

                response = new Response<List<DailyCollectionDetailReport>>()
                {
                    IsError = false,
                    Message = " GetDialyCollectionSummmary details fetched sucessfully",
                    Result = rpt,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching report -  GetDailyCollectionDetail. Please check the logs for more details", ex);
                response = new Response<List<DailyCollectionDetailReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  GetDailyCollectionDetail . Please check the logs for more details"
                };
            }

            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        [Route("api/Report/GetDailyCollectionSummaryAllPayments")]
        public IHttpActionResult GetDailyCollectionSummaryAllPayments(int employeeId, DateTime fromDate, DateTime toDate)
        {
            Response<List<DailyCollectionAllPaymentsReport>> response = null;
            List<DailyCollectionAllPaymentsReport> rpt = new List<DailyCollectionAllPaymentsReport>();
            try
            {
                fromDate = ConvertDateServerFormatWithStartTime(fromDate);
                toDate = ConvertDateServerFormatWithEndTime(toDate);
                List<RPT_DailyCollectionSummaryAllPayments_Result> result = customerDALObj.RPTDailyCollectionSummaryAllPayments(employeeId, fromDate, toDate);
                string date = "";
                decimal netAmount = 0,deliveryAmount=0;
                int listLength = result.Count;
                decimal cash = 0, credit = 0, bank = 0, coupon = 0;
                foreach (RPT_DailyCollectionSummaryAllPayments_Result obj in result)
                {
                    DateTime dtColl = new DateTime();
                    if (obj.Date_Collected != null)
                        dtColl = DateTime.Parse(obj.Date_Collected.ToString());

                    
                    if (date != obj.Date_Collected.ToString() && date!="")
                    {
                        rpt.Add(new DailyCollectionAllPaymentsReport()
                        {
                            CollectionDate =DateTime.Parse(date).ToString("dd/MM/yyyy"),
                            DeliveryAmount =deliveryAmount,
                            ColelctedAmount = Math.Round(netAmount, 3),
                            Cash=cash,
                            Coupon = coupon,
                            Credit =credit,
                            Bank=bank,
                           

                        });
                        netAmount = 0;
                        deliveryAmount = 0;
                        cash = 0;
                        bank = 0;
                        credit = 0;
                        coupon = 0;
                    }
                    
                    {
                      
                      
                       
                        if (obj.payment_mode.ToLower() == "cash")
                            cash = obj.CollectedAmount;
                        else if (obj.payment_mode.ToLower() == "credit")
                            credit = obj.CollectedAmount;
                        else if (obj.payment_mode.ToLower() == "bank")
                            bank = obj.CollectedAmount;
                        else if (obj.payment_mode.ToLower() == "coupon")
                            coupon = obj.CollectedAmount;
                        netAmount += obj.CollectedAmount;
                        deliveryAmount += obj.DeliveryAmount;

                    }
                    date = obj.Date_Collected.ToString();
                    listLength--;
                    if (listLength == 0)
                    {
                        rpt.Add(new DailyCollectionAllPaymentsReport()
                        {
                            CollectionDate = dtColl.ToString("dd/MM/yyyy"),
                            DeliveryAmount = obj.DeliveryAmount,
                            ColelctedAmount = Math.Round(netAmount, 3),
                            Cash = cash,
                            Credit = credit,
                            Bank = bank,
                            Coupon = coupon

                        });
                    }
                }

                response = new Response<List<DailyCollectionAllPaymentsReport>>()
                {
                    IsError = false,
                    Message = " GetDailyCollectionSummary details fetched sucessfully",
                    Result = rpt,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching report -  GetDailyCollectionSummary. Please check the logs for more details", ex);
                response = new Response<List<DailyCollectionAllPaymentsReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  GetDailyCollectionSummary . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/GetDailyCollectionAllPaymentDetail")]
        public IHttpActionResult GetDailyCollectionAllPaymentDetail(int employeeId, DateTime date)
        {
            Response<List<DailyCollectionDetailAllPaymentReport>> response = null;
            List<DailyCollectionDetailAllPaymentReport> rpt = new List<DailyCollectionDetailAllPaymentReport>();
            try
            {
                date = ConvertDateServerFormat(date);
                List<RPT_DailyCollectionAllPaymentDetails_Result> result = customerDALObj.RPTDailyCollectionAllPaymentDetails(employeeId, date);
                foreach (RPT_DailyCollectionAllPaymentDetails_Result obj in result)
                {
                    rpt.Add(new DailyCollectionDetailAllPaymentReport()
                    {
                        CollectedAmount = obj.collected_amount,
                        CustomerName = obj.customer_name,
                        PaymentMode=obj.payment_mode,
                        DeliveryTime = obj.delivery_time.ToString("dd/MM/yyyy hh:mm:ss tt"),
                        NetAmount = obj.net_amount,
                      //  DeliveryId=obj.
                        Status = obj.Status
                    });

                }

                response = new Response<List<DailyCollectionDetailAllPaymentReport>>()
                {
                    IsError = false,
                    Message = " GetDialyCollectionSummmary details fetched sucessfully",
                    Result = rpt,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching report -  GetDailyCollectionDetail. Please check the logs for more details", ex);
                response = new Response<List<DailyCollectionDetailAllPaymentReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  GetDailyCollectionDetail . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/GetRouteItemSummary")]
        public IHttpActionResult GetRouteItemSummary(int employeeId)
        {
            Response<List<RouteItemSummaryReport>> response = null;
            List<RouteItemSummaryReport> rpt = new List<RouteItemSummaryReport>();
            try
            {
               
                List<RPT_GetRouteItemsSummary_Result> result = customerDALObj.RPTRouteItemSummary(employeeId);
                foreach (RPT_GetRouteItemsSummary_Result obj in result)
                {
                    rpt.Add(new RouteItemSummaryReport()
                    {
                       ItemName=obj.item_name,
                       RouteName=obj.route_name,
                       TotalItems=Convert.ToDecimal(obj.TotalItems),
                    });

                }

                response = new Response<List<RouteItemSummaryReport>>()
                {
                    IsError = false,
                    Message = " Get Route Item Summary details fetched sucessfully",
                    Result = rpt,
                    TotalRecords = rpt.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception Get Route Item Summary details. Please check the logs for more details", ex);
                response = new Response<List<RouteItemSummaryReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report -  Get Route Item Summary details . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/GetAllRouteItemSummary")]
        public IHttpActionResult GetAllRouteItemSummaryDetails(int employeeId)
        {
            Response<List<RouteItemSummaryReport>> response = null;
            List<RouteItemSummaryReport> rpt = new List<RouteItemSummaryReport>();
            try
            {
                bool noPrivilege = false;
                List<RPT_GetAllRouteItemsSummary_Result> result = customerDALObj.RPTRouteItemSummaryAll( employeeId,out noPrivilege);
                if (result != null && result.Count > 0)
                {
                    foreach (RPT_GetAllRouteItemsSummary_Result obj in result)
                    {
                        rpt.Add(new RouteItemSummaryReport()
                        {
                            ItemName = obj.item_name,
                            RouteName = obj.route_name,
                            TotalItems = Convert.ToDecimal(obj.TotalItems),
                        });

                    }

                    response = new Response<List<RouteItemSummaryReport>>()
                    {
                        IsError = false,
                        Message = " Get Route Item Summary details fetched sucessfully",
                        Result = rpt,
                        TotalRecords = rpt.Count
                    };
                }
                else
                {
                    List<RPT_GetRouteItemsSummary_Result> result1 = customerDALObj.RPTRouteItemSummary(employeeId);
                    foreach (RPT_GetRouteItemsSummary_Result obj in result1)
                    {
                        rpt.Add(new RouteItemSummaryReport()
                        {
                            ItemName = obj.item_name,
                            RouteName = obj.route_name,
                            TotalItems = Convert.ToDecimal(obj.TotalItems),
                        });

                    }
                    response = new Response<List<RouteItemSummaryReport>>()
                    {
                        IsError = false,
                        Message = " Get Route Item Summary details fetched sucessfully",
                        Result = rpt,
                        TotalRecords = rpt.Count
                    };
                }
            }

            catch (Exception ex)
            {
             
                Logger.Error("Exception Get All Route Item Summary details. Please check the logs for more details", ex);
                string _message = ex.ToString().Contains("You have no privilege to view this report")? "You have no privilege to view this report":"";

                response = new Response<List<RouteItemSummaryReport>>()
                {
                    IsError = true,
                    Message = _message==""? "Exception occured while fetching report -  Get All Route Item Summary details . Please check the logs for more details":_message
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/GetCustomerOutstanding")]
        public IHttpActionResult GetCustomerOutstanding(int employeeId)
        {
            Response<decimal> response = null;
            decimal oustatnding = 0;
            
            try
            {
                CustomerDAL customer = new CustomerDAL();
                oustatnding = customer.CustomerOutstandingTotal(employeeId);
               

                response = new Response<decimal>()
                {
                    IsError = false,
                    Message = " Customer outstanding fetched sucessfully",
                    Result = oustatnding,
                    TotalRecords = 1
                };
            }

            catch (Exception ex)
            {
               // Logger.Error("Exception Customer outstanding fetched sucessfully. Please check the logs for more details", ex);
                response = new Response<decimal>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/CustomerOutstandingReport")]
        public IHttpActionResult CustomerOutstandingReport(int employeeId)
        {
            OutstandingReportResponse<List<Models.CustomerOutstandigReport>> response = null;

            try
            {
                ReportDAL report = new ReportDAL();
                List<customer> listCustomer = report.CustomerOutstandingReport (employeeId);
                List <Models.CustomerOutstandigReport> customerOutstandingReport = new List<CustomerOutstandigReport>();

                if (listCustomer != null && listCustomer.Count > 0)
                {
                    foreach (customer cs in listCustomer)
                    {
                        customerOutstandingReport.Add(new CustomerOutstandigReport {
                            CustomerId=cs.customer_id,
                            CustomerName=$"{cs.customer_name} {cs.street}",
                            Route=cs.route.route_name,
                            Outstanding=Convert.ToDecimal(cs.outstanding_amount)
                        });
                    }
                }

                decimal totalOutstandng = Convert.ToDecimal(customerOutstandingReport.Where(x => x.Outstanding > 0).Sum(x => x.Outstanding));
                response = new OutstandingReportResponse<List<Models.CustomerOutstandigReport>>()
                {
                    IsError = false,
                    Message = " Customer outstanding fetched successfully",
                    Result = customerOutstandingReport,
                    TotalRecords = customerOutstandingReport.Count,
                    TotalOutstanding= totalOutstandng
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception Customer outstanding report", ex);
                response = new OutstandingReportResponse<List<Models.CustomerOutstandigReport>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/DeliveryReportCustomerwise")]
        public IHttpActionResult DeliveryReportCustomerwise(DateTime fromDate,DateTime toDate,int customerId,int employeeId)
        {
            CustomerwiseDeliveryReportResponse<List<Models.DeliveryItemCustomerwise>> response = null;

            try
            {
                if (customerId > 0)
                {
                    fromDate = DateTime.Now.AddMonths(-3);
                    toDate = DateTime.Now;
                }
                ReportDAL report = new ReportDAL();
                List<delivery_items> listItems = report.DeliveryReportCustomerwise(ConvertDateServerFormatWithStartTime(fromDate), ConvertDateServerFormatWithEndTime(toDate),employeeId, customerId);
                List<DeliveryItemCustomerwise> deliveryItems = new List<DeliveryItemCustomerwise>();

                if (listItems != null && listItems.Count > 0)
                {
                    foreach (delivery_items dl in listItems)
                    {
                        deliveryItems.Add(new DeliveryItemCustomerwise
                        {
                            ItemId = dl.item_id,
                            ItemName = dl.item.item_name,
                            Delivered = dl.delivered_qty,
                            NetAmount = dl.net_amount,
                            CustomerName = dl.customer.customer_name,
                            DeliveryTime = dl.delivery_time.ToString()
                        });
                    }
                }
                decimal totalDelivery = Convert.ToDecimal(deliveryItems.Sum(x => x.Delivered));
                decimal totalAmount = Convert.ToDecimal(deliveryItems.Sum(x => x.NetAmount));
                response = new CustomerwiseDeliveryReportResponse<List<Models.DeliveryItemCustomerwise>>()
                {
                    IsError = false,
                    Message = " delivery items fetched successfully",
                    Result = deliveryItems,
                    TotalRecords = deliveryItems.Count,
                    TotalDelivery = totalDelivery,
                    TotalAmount = totalAmount
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception  delivery items customerwise report", ex);
                response = new CustomerwiseDeliveryReportResponse<List<Models.DeliveryItemCustomerwise>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Report/DeliveryReturnReportCustomerwise")]
        public IHttpActionResult DeliveryReturnReportCustomerwise(DateTime fromDate, DateTime toDate,int employeeId, int customerId)
        {
            CustomerwiseDeliveryReportResponse<List<Models.DeliveryReturnItemCustomerwise>> response = null;

            try
            {
                if (customerId > 0)
                {
                    fromDate = DateTime.Now.AddMonths(-3);
                    toDate = DateTime.Now;
                }
                ReportDAL report = new ReportDAL();
                List<delivery_return> listItems = report.DeliveryReturnReportCustomerwise(ConvertDateServerFormat(fromDate), ConvertDateServerFormat(toDate), employeeId,customerId);
                List<DeliveryReturnItemCustomerwise> returnItems = new List<DeliveryReturnItemCustomerwise>();

                if (listItems != null && listItems.Count > 0)
                {
                    foreach (delivery_return dl in listItems)
                    {
                        returnItems.Add(new DeliveryReturnItemCustomerwise
                        {
                            ItemId = dl.item_id,
                            ItemName = dl.item.item_name,
                            Returned = dl.qty,
                            CustomerName = dl.customer.customer_name,
                            ReturnDate = dl.return_date.ToString("dd/MM/yyyy")
                        });
                    }
                }
                decimal totalDelivery = Convert.ToDecimal(returnItems.Sum(x => x.Returned));
                response = new CustomerwiseDeliveryReportResponse<List<Models.DeliveryReturnItemCustomerwise>>()
                {
                    IsError = false,
                    Message = " delivery return items fetched successfully",
                    Result = returnItems,
                    TotalRecords = returnItems.Count,
                    TotalDelivery= totalDelivery,
                    TotalAmount=0
                    
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception  delivery return items customerwise report", ex);
                response = new CustomerwiseDeliveryReportResponse<List<Models.DeliveryReturnItemCustomerwise>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching report . Please check the logs for more details"
                };
            }

            return Ok(response);
        }



        public static DateTime ConvertDateServerFormatWithStartTime(DateTime dateTime)
        {
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 00, 00, 01);
            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static DateTime ConvertDateServerFormatWithEndTime(DateTime dateTime)
        {
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static DateTime ConvertDateServerFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd"));
        }

    }
    
}