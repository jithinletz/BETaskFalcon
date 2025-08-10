using BETaskAPI.Common;
using BETaskAPI.DAL.DAL;
using BETaskAPI.DAL.EDMX;
using BETaskAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BETaskAPI.Controllers
{
    
    public class DeliveryController : ApiController
    {

        DeliveryDAL objDelivery = null;
        CustomerAggrementDAL objAggrement = null;
        public bool DoNewScreen { get; set; } = false;
        public bool DOValidateDODeliveryLeaf { get; set; } = false;


        public DeliveryController()
        {
            objDelivery = new DeliveryDAL();
            objAggrement = new CustomerAggrementDAL();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]

        public IHttpActionResult GetAllDeliveryCustomers(DateTime selectedDate, int userId, string customerName)
        {
            Response<List<DeliveryCustomer>> response = null;
            int a = 0;
            List<DeliveryCustomer> deliveryCustomers = new List<DeliveryCustomer>();
            try
            {


                List<delivery> lstDelivery = objDelivery.GetAllDeliveryCustomers(selectedDate, userId, customerName);
                foreach (delivery del in lstDelivery)
                {
                    
                    del.delivery_items = del.delivery_items.OrderBy(x=>x.delivery_time).ToList();
                    foreach (delivery_items delItems in del.delivery_items)
                    {
                        ++a;

                        /*Distance calculation*/
                        int distance = 0;
                        int.TryParse(delItems.customer.location_distance,out distance); 
                        //var input = delItems.customer.remarks;
                        //if (!string.IsNullOrEmpty(input))
                        //{
                        //    if (input.Contains("[") && input.Contains("]"))
                        //    {
                        //        try
                        //        {
                        //            string output = input.Substring(input.IndexOf('[') + 1, input.IndexOf(']') - input.IndexOf('[') - 1);
                        //            output = System.Text.RegularExpressions.Regex.Replace(output, @"\s+", "");
                        //            distance = Convert.ToInt32(output);
                        //        }
                        //        catch { throw; }
                        //    }
                        //}
                        ///*End  Distance calculation*/
                        

                        if (delItems.customer != null)
                        {
                            if (!deliveryCustomers.Any(c => c.CustomerId == delItems.customer.customer_id && c.DeliveryId==delItems.delivery_id))
                            {
                                decimal outstanding = 0,walletBalance=0;
                                if (delItems.customer.wallet_balance != null)
                                    walletBalance = Convert.ToDecimal(delItems.customer.wallet_balance);
                               
                                if (delItems.customer.payment_mode!=null && delItems.customer.payment_mode.ToLower() == "do")
                                    outstanding = 0;
                                int delStatus = delItems.status == 1 ? 3 : delItems.status;
                                string deliveryTime = "";
                                if (delItems.delivery_time != null)
                                {
                                    deliveryTime = DateTime.Parse(delItems.delivery_time.ToString()).ToString("dd/MM/yyyy hh:mm tt");
                                }
                                deliveryCustomers.Add(new DeliveryCustomer()
                                {
                                    DeliveryId = del.delivery_id,
                                    DeliveryDate = del.delivery_date.ToString("dd/MM/yyyy"),
                                    DeliveredDateTime = deliveryTime,
                                    CustomerId = delItems.customer == null ? 0 : delItems.customer.customer_id,
                                    CustomerName = delItems.customer == null ? string.Empty : delItems.customer.customer_name,
                                    Address1 = delItems.customer == null ? string.Empty : delItems.customer.address1,
                                    Address2 = delItems.customer == null ? string.Empty : delItems.customer.address2,
                                    City = delItems.customer == null ? string.Empty : delItems.customer.city,
                                    Pobox = delItems.customer == null ? string.Empty : delItems.customer.pobox,
                                    Street = delItems.customer == null ? string.Empty : delItems.customer.street,
                                    Lat = delItems.customer == null ? string.Empty : delItems.customer.lat,
                                    Lng = delItems.customer == null ? string.Empty : delItems.customer.lng,
                                    Phone = delItems.customer.phone == null ? string.Empty : delItems.customer.phone,
                                    Mobile = delItems.customer.phone == null ? string.Empty : delItems.customer.mobile,
                                    DeliveryStatus = delStatus,//3 Pending , 4 Delivered, 5 Removed , 6 Hold,
                                    WalletNumber = delItems.customer == null ? string.Empty : delItems.customer.wallet_number,
                                    WalletBalance = delItems.customer == null ? 0 : Convert.ToDecimal(delItems.customer.wallet_balance),
                                    Remarks="doordelivery",
                                    DistanceRadious=distance,
                                    Outstanding=outstanding,
                                    Payment_mode= delItems.customer == null ? string.Empty : delItems.customer.payment_mode,
                                    
                                });
                            }
                        }
                    }
                }

                response = new Response<List<DeliveryCustomer>>()
                {
                    IsError = false,
                    Message = "Delivery Customer details fetched sucessfully",
                    Result = deliveryCustomers.OrderBy(x=>x.DeliveryStatus).ToList(),
                    TotalRecords = deliveryCustomers.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer . Please check the logs for more details", ex);
                response = new Response<List<DeliveryCustomer>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching customer . Please check the logs for more details"
                };
            }

            return Ok(response);
        }


        [System.Web.Http.HttpGet]

        public IHttpActionResult GetDeliveryItems(int customerId, int deliveryId)
        {
            Response<CustomerDelivery> response = null;
            decimal total_amount = 0;
            CustomerDelivery customerDelivery = new CustomerDelivery();
            List<DeliveryItems> items = new List<DeliveryItems>();

            try
            {
                List<delivery_items> lstDeliveryItems = objDelivery.GetCustomerDeliveryItems(deliveryId, customerId);
                foreach (delivery_items del in lstDeliveryItems)
                {

                    DeliveryItems obj = new DeliveryItems();
                    obj.ItemId = del.item_id;
                    obj.ItemName = del.item == null ? "" : del.item.item_name;
                    obj.NetAmount = del.net_amount;
                    obj.Qty = del.qty;
                    obj.Rate = del.rate;
                    obj.CustomerItemMaxQty = objAggrement.GetCustomerAggrement(customerId, del.item_id) == null ? -1 : objAggrement.GetCustomerAggrement(customerId, del.item_id).max_qty;
                    total_amount += del.net_amount;
                    items.Add(obj);

                }
                customerDelivery.DeliveryItems = items;
                customerDelivery.TotalAmount = total_amount;

                response = new Response<CustomerDelivery>()
                {
                    IsError = false,
                    Message = "Delivery Customer items fetched sucessfully",
                    Result = customerDelivery,
                    TotalRecords = lstDeliveryItems.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer . Please check the logs for more details", ex);
                response = new Response<CustomerDelivery>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching customer delivery items . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpPost]
        [Route("api/Delivery/SaveDailyCollections")]
        public IHttpActionResult SaveDailyCollections(DailyCollection collection)
        {
            ClearCacheForEmployee(collection.EmployeeId);
            //Logger.Error($"Collectioin:{collection.CollectedAmount} , Customer:{collection.CustomerId},DeliveryId:{collection.DeliveryId}, OldLeaf:{collection.OldLeafCount} , Payment:{collection.PaymentMode},Employee:{collection.EmployeeId}, IsDep:{collection.IsDeposit} , IsRefund:{collection.IsRefund} - >");
            // Logger.Error($"DelId-{collection.DeliveryId} ");

            try
            {
                var props = collection.GetType().GetProperties();
                var logBuilder = new StringBuilder("DailyCollection Properties => ");

                foreach (var prop in props)
                {
                    var name = prop.Name;
                    var value = prop.GetValue(collection, null);
                    logBuilder.AppendFormat("{0}:{1}, ", name, value ?? "null");
                }

                Logger.Error(logBuilder.ToString().TrimEnd(',', ' '));
            }
            catch (Exception ex)
            {

                throw;
            }
          


            var date = System.TimeZoneInfo.ConvertTimeFromUtc(
 DateTime.UtcNow,
 TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));

            Response<int> response = null;
            try
            {
                int enableSundayDelivery = General.IsSundayDelivery();

                if (date.DayOfWeek == DayOfWeek.Sunday && enableSundayDelivery != 1)
                {
                    throw new Exception("Sorry delivery not allowed today");
                }


                if (!objDelivery.IsDeliveryEnabled(collection.EmployeeId, new DateTime(date.Year, date.Month, date.Day)))
                {
                    throw new Exception($"Sorry delivery not allowed after cash collection for employee : {collection.EmployeeId}");

                }
                collection.IsDeposit = collection.IsDeposit == 0 ? 2 : collection.IsDeposit;
                //Checking and setting default payment mode
                collection.PaymentMode = SetDeafultPaymentMode(collection.CustomerId, collection.PaymentMode);

                if (collection.PaymentMode.ToLower() == "cash")
                {
                    throw new Exception("Cash collection won't allow to cash customer"); 
                }
                /* if (collection.CollectedAmount == 0 && collection.PaymentMode.ToLower() == "coupon")
                     collection.PaymentMode = "Credit";*/



                if (collection != null)
                {

                    int? deliveryId = null;
                    if (collection.DeliveryId > 0)
                        deliveryId = collection.DeliveryId;

                    //   string data = $"customer={collection.CustomerId},delive={deliveryId},collection={collection.CollectedAmount},{date},net={ collection.NetAmount},{(collection.PaymentMode == "Banking" ? "Bank" : collection.PaymentMode)},Remarks={collection.Remarks},Employee={collection.EmployeeId},{collection.CouponNumber},Div={collection.DivisionId}";
                    // Logger.Error(data);
                    daily_collection obj = new daily_collection()
                    {
                        customer_id = collection.CustomerId,
                        delivery_id = deliveryId,
                        collected_amount = collection.CollectedAmount,
                        delivery_time = date,
                        net_amount = collection.NetAmount,
                        payment_mode = collection.PaymentMode == "Banking" ? "Bank" : collection.PaymentMode,
                        remarks = collection.Remarks,
                        employee_id = collection.EmployeeId,
                        status = 1,
                        delivery_leaf = collection.CouponNumber,
                        division_id = collection.DivisionId,
                        is_deposit = collection.IsDeposit == 0 ? 2 : collection.IsDeposit,
                        is_refund = collection.IsRefund == 0 ? 2 : collection.IsRefund,
                        old_leaf_count = collection.OldLeafCount
                    };
                    string resp = objDelivery.SaveDailyCollections(obj, collection.CouponNumber);

                   // Logger.Error($"Collection update : {resp}");

                    response = new Response<int>()
                    {
                        IsError = false,
                        Result = 1,
                        Message = "Daily collection saved successfully",
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<int>()
                    {
                        IsError = true,
                        Message = "Invalid Request",
                        Result = 2
                    };
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(collection.CouponNumber.ToString());
                Logger.Error("Error on Collections . Please check the logs for more details", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message = "Error on Collections "+ex.Message,
                    Result = 0
                };
            }
            return Ok(response);
        }

       
        [System.Web.Http.HttpPost]
        [Route("api/Delivery/SaveDailyCollectionsWithItems")]
        public IHttpActionResult SaveDailyCollectionsWithItems(DailyCollectionWithItem collection)
        {
            Response<int> response;
            try
            {
                if (collection != null)
                {
                    int defaultItem = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultItem"]);
                    ValidateSundayDelivery();
                    var customerItem = ValidateItemCount(collection);
                    ValidateDefaultPaymentModeAndOldLeaf(collection, customerItem);
                    ValidatePaymentForNonDefaultItems(collection, customerItem, defaultItem);
                    ValidateDo(collection);


                    decimal qtyNonFoc = 0;
                    var dailyCollection = GetDailyCollection(collection);
                    var listItems = GetDeliveryCustomerAgreementItems(collection, customerItem, defaultItem, dailyCollection,ref qtyNonFoc);
                    decimal rechargeTax = General.GetRechargeTax();

                    //Saving
                    objDelivery.SaveDailyCollections(dailyCollection, listItems, collection.CouponNumber, rechargeTax, qtyNonFoc);

                  

                    response = new Response<int>()
                    {
                        IsError = false,
                        Result = 1,
                        Message = "Daily collection saved successfully",
                        TotalRecords = 1
                    };
                  
                }
                else
                {
                    response = new Response<int>()
                    {
                        IsError = true,
                        Message = "Invalid Request",
                        Result = 2
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error while Saving  Collections .", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message = $"{ex.Message} ",
                    Result = 0
                };
            }
            return Ok(response);
        }

        private void ValidateDefaultPaymentModeAndOldLeaf(DailyCollectionWithItem collection, List<CustomerItem> customerItem)
        {
            //Checking and setting default payment mode
            if (collection.IsDeposit != 1)
                collection.PaymentMode = SetDeafultPaymentMode(collection.CustomerId, collection.PaymentMode);

            if (collection.OldLeafCount > 0)
            {
                int itemCount = customerItem.Sum(x => x.Qty);
                if (collection.OldLeafCount != itemCount)
                    collection.OldLeafCount = itemCount;
            }
        }

        private static void ValidatePaymentForNonDefaultItems(DailyCollectionWithItem collection, List<CustomerItem> customerItem, int defaultItem)
        {
            if (customerItem != null && customerItem.Count == 1 && collection.PaymentMode == "Coupon")
            {
                BETaskAPI.Models.CustomerItem _customerItem = customerItem.Single();
                //Logger.Info(_customerItem.ItemId.ToString());
                if (_customerItem.ItemId != defaultItem)
                {
                    collection.PaymentMode = "Cash";
                    collection.OldLeafCount = 0;
                }
            }
        }

        private static List<CustomerItem> ValidateItemCount(DailyCollectionWithItem collection)
        {
            if (collection.DeliveryId > 0 && collection.CustomerItem.Count == 0)
            {
                Logger.Error($"Some of entered qty is not allowed . Employee {collection.EmployeeId} / {collection.DeliveryId}");
                throw new Exception("Some of entered qty is not allowed");
            }
            else
            {
                List<CustomerItem> customerItem = new List<CustomerItem>();

                foreach (CustomerItem ci in collection.CustomerItem)
                {
                    if (ci.Qty > 0)
                        customerItem.Add(ci);
                }
                return customerItem;
            }
        }

        private static void ValidateSundayDelivery()
        {
            DateTime date = General.GetArabTme();
            int enableSundayDelivery = General.IsSundayDelivery();

            if (date.DayOfWeek == DayOfWeek.Sunday && enableSundayDelivery != 1)
            {
                throw new Exception("Sorry delivery not allowed today");
            }
        }

        private void ValidateDo(DailyCollectionWithItem collection)
        {
            //If coupon customer try to enter 0 , it will be credit sale
            /* if (collection.CollectedAmount == 0 && collection.PaymentMode.ToLower() == "coupon" && collection.IsDeposit != 1)
                 collection.PaymentMode = "Credit";*/
            if (collection.PaymentMode.ToLower() == "do")
            {
                DOValidation();
                if (this.DOValidateDODeliveryLeaf)
                {
                    if (!string.IsNullOrEmpty(collection.CouponNumber))
                    {
                        int leafStatus = objDelivery.ValidateDeliveryLeaf(collection.CouponNumber.ToUpper(), collection.EmployeeId);
                        if (leafStatus > 1)
                            throw new Exception("Invalid delivery leaf");
                    }
                }
            }
        }

        private static List<customer_aggrement> GetDeliveryCustomerAgreementItems(DailyCollectionWithItem collection, List<CustomerItem> customerItem, int defaultItem, daily_collection dailyCollection,ref decimal qtyNonFoc  )
        {
            CustomerAggrementDAL customerDAL = new CustomerAggrementDAL();
            // Logger.Error($"# DO check {obj.customer_id} {obj.payment_mode}#");
            List<customer_aggrement> listItems = new List<customer_aggrement>();
            if (collection.IsDeposit != 1)
            {
                foreach (CustomerItem ci in customerItem)
                {
                    decimal allowedQty = customerDAL.GetCustomerAggrement(dailyCollection.customer_id, ci.ItemId).max_qty;

                    if (ci.Qty <= allowedQty)
                    {
                        if (ci.Qty > 0)
                        {
                            listItems.Add(new customer_aggrement()
                            {
                                item_id = ci.ItemId,
                                max_qty = ci.Qty,
                                unit_price = ci.Rate
                            });
                        }
                        if (ci.ItemId == defaultItem && ci.Rate > 0)
                            qtyNonFoc += ci.Qty;
                    }
                    else throw new Exception("Unable to save , You are trying to add invalid qty");
                }
            }
            //Deposit items
            else
            {
                // Logger.Error($"{collection.CollectedAmount} / {collection.NetAmount}");
                foreach (CustomerItem ci in customerItem)
                {

                    listItems.Add(new customer_aggrement()
                    {
                        item_id = ci.ItemId,
                        max_qty = ci.Qty,
                        unit_price = collection.CollectedAmount / ci.Qty
                    });

                }
            }

            return listItems;
        }

        private static daily_collection GetDailyCollection(DailyCollectionWithItem collection)
        {
            int? deliveryId = null;

            if (collection.DeliveryId > 0)
                deliveryId = collection.DeliveryId;
            return new daily_collection()
            {
                customer_id = collection.CustomerId,
                delivery_id = deliveryId,
                collected_amount = collection.CollectedAmount,
                delivery_time = General.GetArabTme(),
                net_amount = collection.NetAmount,
                payment_mode = collection.PaymentMode == "Banking" ? "Bank" : collection.PaymentMode,
                remarks = collection.Remarks,
                employee_id = collection.EmployeeId,
                status = 1,
                delivery_leaf = collection.CouponNumber,
                division_id = collection.DivisionId,
                old_leaf_count = collection.OldLeafCount,
                is_deposit = collection.IsDeposit == 0 ? 2 : collection.IsDeposit,
                is_refund = collection.IsRefund == 0 ? 2 : collection.IsRefund,
            };
        }

        [System.Web.Http.HttpGet]
        [Route("api/Delivery/GetReturnItems")]

        public IHttpActionResult GetReturnItems(int customerId)
        {
            Response<List<ItemMini>> response = null; 
            List<ItemMini> items = new List<ItemMini>();
            //Logger.Error("GetReturnItems - 1");
            try
            {
                List<customer_aggrement> listAggrement = objAggrement.GetCustomerAggrement(customerId);
                listAggrement = listAggrement.Where(x => x.show_app == 1 || x.show_app==null).ToList();
                foreach (customer_aggrement obj in listAggrement)
                {
                    if (obj.item != null)
                    {
                        if (!items.Exists(x => x.ItemId == obj.item_id))
                        {
                            DeliveryDAL deliveryDAL = new DeliveryDAL();

                            //Get last delivery quantity
                            decimal maxQty = deliveryDAL.GetLastDeliveryQty(obj.item_id, customerId);
                            decimal _maxQtyAgree = 0;

                            if (maxQty>= 0)
                            {
                                try
                                {
                                    //Agreement quantity
                                    _maxQtyAgree = listAggrement.Where(x => x.item_id == obj.item_id).Sum(x => x.max_qty);
                                }
                                catch { }

                                if (maxQty <= 0)
                                    maxQty = _maxQtyAgree;

                                if (obj.item.item_name.ToLower().Contains("drinking water") || obj.item.item_name.ToLower().Contains("gallon water"))
                                {
                                    items.Add(new ItemMini()
                                    {
                                        ItemId = obj.item.item_id,
                                        ItemName = obj.item.item_name,
                                        Qty = (int)Decimal.Truncate(maxQty)

                                    });
                                }
                            }
                        }
                    }
                } 
                response = new Response<List<ItemMini>>()
                {
                    IsError = false,
                    Message = "Return items fetched sucessfully",
                    Result = items,
                    TotalRecords = items.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while GetReturnItems . Please check the logs for more details", ex);
                response =new Response<List<ItemMini>>()
                {
                    IsError = true,
                    Message = "Exception occured while GetReturnItems . Please check the logs for more details"
                };
            } 
            return Ok(response);
        }

        /*
        [Route("api/Delivery/GetPermanantReturnItems")]
        public IHttpActionResult GetPermanantReturnItems(int customerId)
        {
            
            Response<List<ItemMini>> response = null;
            List<ItemMini> items = new List<ItemMini>();
            try
            {
                List<customer_aggrement> listAggrement = objAggrement.GetCustomerAggrement(customerId);
                listAggrement = listAggrement.Where(x => x.show_app == 1 || x.show_app == null).ToList();
                foreach (customer_aggrement obj in listAggrement)
                {
                    if (obj.item != null)
                    {
                        if (!items.Exists(x => x.ItemId == obj.item_id))
                        {
                            DeliveryDAL deliveryDAL = new DeliveryDAL();

                            //Get last delivery quantity
                            decimal maxQty = deliveryDAL.GetLastDeliveryQty(obj.item_id, customerId);
                            decimal _maxQtyAgree = 0;

                            if (maxQty >= 0)
                            {
                                try
                                {
                                    //Agreement quantity
                                    _maxQtyAgree = listAggrement.Where(x => x.item_id == obj.item_id).Sum(x => x.max_qty);
                                }
                                catch { }

                                if (maxQty <= 0)
                                    maxQty = _maxQtyAgree;

                                if (obj.item.item_name.ToLower().Contains("drinking water") || obj.item.item_name.ToLower().Contains("gallon water"))
                                {
                                    items.Add(new ItemMini()
                                    {
                                        ItemId = obj.item.item_id,
                                        ItemName = obj.item.item_name,
                                        Qty = (int)Decimal.Truncate(maxQty)

                                    });
                                }
                            }
                        }
                    }
                }
                response = new Response<List<ItemMini>>()
                {
                    IsError = false,
                    Message = "Return items fetched sucessfully",
                    Result = items,
                    TotalRecords = items.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while GetReturnItems . Please check the logs for more details", ex);
                response = new Response<List<ItemMini>>()
                {
                    IsError = true,
                    Message = "Exception occured while GetReturnItems . Please check the logs for more details"
                };
            }
            return Ok(response);
        }
        */
        public IHttpActionResult GetPermanantReturnItems(int customerId)
        {
            

            Response<List<ItemMini>> response = null;
            List<ItemMini> items = new List<ItemMini>();
            //Logger.Error("GetPermanantReturnItems - 2");
            try
            {
                List<customer_aggrement> listAggrement = objAggrement.GetCustomerAggrement(customerId);
                listAggrement = listAggrement.Where(x => x.show_app == 1 || x.show_app == null).ToList();
                foreach (customer_aggrement obj in listAggrement)
                {
                    if (obj.item != null)
                    {
                        items.Add(new ItemMini()
                        {
                            ItemId = obj.item.item_id,
                            ItemName = obj.item.item_name,
                            Qty = (int)Decimal.Truncate(obj.max_qty)

                        });

                            }
                        }
                response = new Response<List<ItemMini>>()
                {
                    IsError = false,
                    Message = "Return items fetched sucessfully",
                    Result = items,
                    TotalRecords = items.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while GetReturnItems . Please check the logs for more details", ex);
                response = new Response<List<ItemMini>>()
                {
                    IsError = true,
                    Message = "Exception occured while GetReturnItems . Please check the logs for more details"
                };
            }
            return Ok(response);
        }

        [System.Web.Http.HttpPost]
        [Route("api/Delivery/SaveDeliveryReturn")]
        public IHttpActionResult SaveDeliveryReturn(DeliveryReturn deliveryReturn)
        {
            Response<int> response = null;
            List<delivery_return> returnItems = new List<delivery_return>();
            try
            {
                var date = System.TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));
                if (deliveryReturn != null && deliveryReturn.ReturnItems!=null)
                {

                    foreach (DeliveryReturnItem _return in deliveryReturn.ReturnItems) {
                        if (_return.Qty > 0)
                        {
                            returnItems.Add(new delivery_return()
                            {
                                customer_id = deliveryReturn.CustomerId,
                                employee_id = deliveryReturn.EmployeeId,
                                item_id = _return.ItemId,
                                qty = _return.Qty,
                                remarks = deliveryReturn.Remarks,
                                return_date = date,
                                status = 4,
                                return_type=deliveryReturn.ReturnType,
                                server_time=date
                            });
                        }
                    }                      
                    objDelivery.SaveDeliveryReturn(returnItems);
                    response = new Response<int>()
                    {
                        IsError = false,
                        Result = 1,
                        Message = "Delivery Return saved successfully",
                        TotalRecords = 1
                    };
                }
                else
                {
                    response = new Response<int>()
                    {
                        IsError = true,
                        Message = "Invalid Request",
                        Result = 2
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while Saving delivery return. Please check the logs for more details", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message =$"Erro occured while Saving delivery return . {ex.Message}",
                    Result = 3
                };
            }
            return Ok(response);
        }

        /*Delivery schedule from app*/
        /*26-may-2021*/
        [System.Web.Http.HttpGet]
        [Route("api/Delivery/GetTodayDeliveryId")]
        public IHttpActionResult GetTodayDeliveryId(int employeeId, DateTime deliveryDate)
        {

            Response<int> response = null;
            try
            {
                DateTime date = ConvertDateServerFormat(deliveryDate);
                int deliveryId = objDelivery.GetTodayDeliveryId(employeeId, date);


                if (deliveryId >= 0)
                {
                    response = new Response<int>()
                    {
                        IsError = false,
                        Message = deliveryId > 0 ? "Valid" : "You have no permission to create delivery for requested date",
                        Result = deliveryId,
                        TotalRecords=1
                        
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while getting todaydeliveryid . Please check the logs for more details", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message = "Error while getting delivery id, Please check the logs for more details"
                };
            }
            return Ok(response);
        }

        [System.Web.Http.HttpPost]
        [Route("api/Delivery/GetCartTotal")]
        public IHttpActionResult GetCartTotal(List<ItemCart> itemCart)
        {
            Response<decimal> response = new Response<decimal>();
            decimal cartTotal = 0;
            try
            {
               
                if (itemCart != null && itemCart.Count > 0)
                {
                    int customerId = itemCart[0].CustomerId;
                    List<DAL.EDMX.customer_aggrement> listAgree = objAggrement.GetCustomerAggrement(customerId);
                    if (customerId > 0)
                    {
                        foreach (ItemCart it in itemCart)
                        {
                            if (it.Qty > 0 && it.Rate != 0)
                            {

                                DAL.EDMX.customer_aggrement agreement = listAgree.Where(x => x.item_id == it.ItemId && x.unit_price != 0 && x.unit_price <=it.Rate).FirstOrDefault();
                                if (agreement != null)
                                {
                                    decimal rate = General.GetRateWithTax(agreement);
                                    decimal total = rate * it.Qty;
                                    total = General.TruncateDecimalPlaces(total);
                                    cartTotal += total;
                                }
                            }
                        }
                        cartTotal= General.TruncateDecimalPlaces(cartTotal);
                        response = new Response<decimal>()
                        {
                            IsError = false,
                            Message = "Getting cart total",
                            Result = cartTotal,
                            TotalRecords = 1

                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while getting GetCartTotal", ex);
                response = new Response<decimal>()
                {
                    IsError = true,
                    Message = "Something went wrog. please try again"
                };
            }
            return Ok(response);
        }
        [System.Web.Http.HttpGet]
        [Route("api/Delivery/ValidateDeliveryLeaf")]
        public IHttpActionResult ValidateDeliveryLeaf(string leafNo,int employeeId)
        {
            int result = 2;
            Response<int> response = null;
            try
            {
                //setting of Delivery validation
                DOValidation();
                if (!string.IsNullOrEmpty(leafNo))
                {
                    DeliveryDAL delivery = new DeliveryDAL();
                    if (!this.DOValidateDODeliveryLeaf)
                        result = 1;
                    else
                        result = delivery.ValidateDeliveryLeaf(leafNo.ToUpper(),employeeId);
                    string message = "";
                    switch (result)
                    {
                        case 1:
                            message = "valid";
                            break;
                        case 2:
                            message = "Invalid leaf";
                            break;
                        case 3:
                            message = "Invalid leaf";
                            break;
                        case 4:
                            message = "Leaf already redeemed";
                            break;
                    }
                    response = new Response<int>()
                    {
                        IsError = false,
                        Message = message,
                        Result = result,
                        TotalRecords=1
                    };
                    //Logger.Info($" Validation of Leaf {leafNo} is - {message}");
                }
            }
            catch (Exception ex) { }
            return Ok(response);
        }
        /// <summary>
        /// if newScreen then new delivery screen will show
        /// if DOValidateDODeliveryLeaf will validate delivery entered in new DO delivery mode leafs
        /// </summary>
        private void DOValidation()
        {
            try
            {
                int newScreen =Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["DoNewScreen"]));
                int doValidatiion = Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["ValidateDoLeaf"]));
                this.DoNewScreen = newScreen == 1 ? true : false;
                this.DOValidateDODeliveryLeaf = doValidatiion == 1 ? true : false;
            }
            catch { }
        }

        public static DateTime ConvertDateServerFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd"));
        }
        private string SetDeafultPaymentMode(int customerId,string selectedPayment)
        {
            string paymentMode = selectedPayment;
            Models.CustomerPaymentMode customerPaymentMode = new CustomerPaymentMode();
            try
            {
                CustomerDAL customerDALObj = new CustomerDAL();
                if (customerPaymentMode.PaymentModeLocked == 1)
                {
                    customer cs = customerDALObj.GetCustomerDetails(customerId);
                    if (cs != null && !string.IsNullOrEmpty(cs.payment_mode))
                    {
                        paymentMode = cs.payment_mode;
                    }
                }
            }
            catch (Exception ex) { }
            return paymentMode;
        }

        public void ClearCacheForEmployee(int employeeId)
        {
            try
            {
                var cacheKey = $"CustomerList_{employeeId}";

                // Clear the cache for the specified key
                System.Runtime.Caching.MemoryCache.Default.Remove(cacheKey);
            }
            catch { }
        }
       

       


    }
}
