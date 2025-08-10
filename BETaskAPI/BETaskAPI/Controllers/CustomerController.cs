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
using System.Configuration;
using System.IO;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace BETaskAPI.Controllers
{
    public class CustomerController : ApiController
    {
        CustomerDAL customerDALObj = null;
        ItemDAL itemDAL = new ItemDAL();
        public CustomerController()
        {
            customerDALObj = new CustomerDAL();
        }



        /// <summary>
        /// 0, In Valid Request
        /// 1. Valid  
        /// 2. insufficient Balance
        /// 3. Invalid Customer
        /// 4. Invalid Coupon
        /// </summary>
        /// <param name="cusCoupon"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [Route("api/Customer/ValidateCoupon")]
        public IHttpActionResult ValidateCoupon(CustomerCoupon cusCoupon)
        {

            Response<int> response = null;
            try
            {

                if (cusCoupon != null)
                {

                    customer cus = customerDALObj.GetCustomerDetails(cusCoupon.CustomerId, true);
                    Logger.Info($" Coupon validation of {cus.customer_id} - {cus.customer_name} coupon number:{cusCoupon.WalletNumber} , Current balance={cus.wallet_balance} , Request amount:{cusCoupon.Amount}, Credit={cus.credit_limit} ");
                    if (cus != null)
                    {

                        if (cus.wallet_number == cusCoupon.WalletNumber)
                        {
                            cus.wallet_balance = cus.wallet_balance == null ? 0 : cus.wallet_balance;

                            if (cus.wallet_balance >= cusCoupon.Amount)
                            {
                                response = new Response<int>()
                                {
                                    IsError = false,
                                    Message = "Valid",
                                    Result = 1
                                };
                                // Logger.Info($" validation of coupon number:{cusCoupon.WalletNumber} Success");
                            }

                            else
                            {
                                bool isMinusBalancePassed = false;
                                /*
                                 * 16.Mar.2022
                                 * Checking negetive amount
                                 */
                                decimal maxMinusBalance = 0;
                                try
                                {

                                    // maxMinusBalance = Convert.ToDecimal(ConfigurationManager.AppSettings["maxMinusBalance"]);
                                    maxMinusBalance = Convert.ToDecimal(cus.credit_limit);
                                    if (maxMinusBalance > 0)
                                    {
                                        if ((cus.wallet_balance + maxMinusBalance) >= cusCoupon.Amount)
                                        {
                                            isMinusBalancePassed = true;
                                            response = new Response<int>()
                                            {
                                                IsError = false,
                                                Message = $"Customer have minus balance  {(cus.wallet_balance)}",
                                                Result = 1
                                            };
                                        }
                                    }

                                }
                                catch { }

                                /*
                                 * insufficient Balance
                                 */
                                if (!isMinusBalancePassed)
                                {
                                    response = new Response<int>()
                                    {
                                        IsError = false,
                                        Message = "insufficient Balance",
                                        Result = 2
                                    };
                                    Logger.Info($"validation of coupon number:{cusCoupon.WalletNumber} Failed insufficient Balance");
                                }
                            }

                        }
                        else
                        {

                            /*Checking coupons*/
                            /****************/
                            decimal couponAmount = customerDALObj.CheckCouponValidity(cusCoupon.WalletNumber, cus.customer_id);
                            if (couponAmount >= cusCoupon.Amount)
                            {
                                response = new Response<int>()
                                {
                                    IsError = false,
                                    Message = "Valid",
                                    Result = 1
                                };
                                //  Logger.Info($" validation of coupon leafs:{cusCoupon.WalletNumber} Success");
                            }
                            else if (couponAmount < cusCoupon.Amount)
                            {
                                response = new Response<int>()
                                {
                                    IsError = false,
                                    Message = "insufficient coupon Balance",
                                    Result = 2
                                };
                                // Logger.Info($" validation of coupon leafs:{cusCoupon.WalletNumber} Failed");
                            }
                            /*End Checking coupons*/
                            /****************/
                            else
                            {
                                response = new Response<int>()
                                {
                                    IsError = false,
                                    Message = "Invalid Coupon",
                                    Result = 4
                                };
                                // Logger.Info($" validation of coupon :{cusCoupon.WalletNumber} Invalid coupon");
                            }
                        }
                    }
                    else
                    {
                        response = new Response<int>()
                        {
                            IsError = false,
                            Message = "Invalid Customer",
                            Result = 3
                        };
                    }
                }
                else
                {
                    response = new Response<int>()
                    {
                        IsError = false,
                        Message = "Invalid request",
                        Result = 0
                    };
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while validating coupon . Please check the logs for more details", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message = "Invalid coupon details, unable to process"
                };
            }
            return Ok(response);
        }


        [System.Web.Http.HttpPost]
        [Route("api/Customer/SaveCustomer")]
        public IHttpActionResult SaveCustomer(Customer customer)
        {
            Logger.Info($" Interval :{customer.Delivery_interval}");
            Response<int> response = null;
            try
            {

                if (customer != null)
                {

                    var customerObj = GetCustomer(customer);

                    var listAggrement = GetCustomerAgreement(customer.customerAggrements, customer.RateIncludeTax);

                    customerDALObj.SaveCustomer(customerObj, listAggrement);

                    ClearCacheForEmployee(customer.EmployeeId);


                    response = new Response<int>()
                    {
                        IsError = false,
                        Result = 1,
                        Message = "Customer saved successfully",
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
                Logger.Error("Error while saving customer to Temp", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message = $"Unable to  save customer Details . {ex.Message}",
                    Result = 3,
                    TotalRecords = 0
                };
            }
            return Ok(response);
        }

        private customer GetCustomer(Customer customer)
        {
            DateTime date = General.GetArabTme();

            List<customer_aggrement> listAggrement = new List<customer_aggrement>();
            return new DAL.EDMX.customer()
            {
                customer_id = 0,
                customer_name = customer.CustomerName,
                address1 = customer.Address1,
                pobox = customer.POBox,
                mobile = customer.Mobile == null ? customer.Phone : customer.Mobile,
                phone = customer.Phone,
                trn = customer.TRN,
                email = customer.Email,
                lat = customer.LAT,
                lng = customer.LNG,
                status = 1,
                remarks = customer.Remarks,
                route_id = customerDALObj.GetRouteIdByName(customer.Route),
                payment_mode = customer.Payment_mode,
                address2 = customer.Address2,
                delivery_interval = customer.Delivery_interval,
                street = customer.Street,
                building_id = customerDALObj.GeBuildingIdByName(customer.Building),
                city = customer.City,
                employee_id = customer.EmployeeId,
                new_customer = customer.NewCustomer,
                added_time = date,
                credit_limit = 0,
                customer_type = 1,
                wallet_number = string.Empty,
                ledger_id = 0,
                wallet_balance = 0,
                enable_online_payment = General.IsEnableOnlinePayment(),
                enable_offer = 1,
                outstanding_amount = 0
            };
        }



        private List<customer_aggrement> GetCustomerAgreement(List<CustomerAggrement> agreements, int isTaxIncluded)
        {
            List<customer_aggrement> listAgreement = new List<customer_aggrement>();
            foreach (var agreement in agreements)
            {
                if (agreement.MaxQty > 0)
                {
                    decimal unitRate = agreement.UnitPrice;
                    var itemTaxValue = itemDAL.GetItemTaxValue(agreement.ItemId);
                    decimal unitRateCalculated = unitRate;

                    if (isTaxIncluded == 1)
                    {
                        decimal withTax = unitRate;
                        decimal tax = itemTaxValue;
                        decimal taxDeducted = ((withTax * 100) / (100 + tax));
                        taxDeducted = General.TruncateDecimalPlaces(taxDeducted, 4);
                        unitRateCalculated = taxDeducted;
                    }
                    listAgreement.Add(new customer_aggrement
                    {
                        item_id = agreement.ItemId,
                        max_qty = agreement.MaxQty,
                        unit_price = unitRateCalculated,
                        status = 1,
                    });
                }

            }
            return listAgreement;

        }

        [System.Web.Http.HttpPost]
        [Route("api/Customer/UpdateCustomerLocation")]
        public IHttpActionResult UpdateCustomerLocation(Customer customer)
        {
            Response<int> response = null;
            try
            {
                if (customer != null)
                {
                    customer obj = new customer()
                    {
                        customer_id = customer.CustomerId,
                        lat = customer.LAT,
                        lng = customer.LNG
                    };
                    //  customerDALObj.UpdateCustomerLocation(obj);
                    response = new Response<int>()
                    {
                        IsError = false,
                        Result = 1,
                        Message = "Customer Updation temporary blocked",
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
                Logger.Error("Exception occured while updating customer location . Please check the logs for more details", ex);
                response = new Response<int>()
                {
                    IsError = true,
                    Message = "Exception occured while updating customer location . Please check the logs for more details",
                    Result = 3
                };
            }
            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCustomerListAsync(int employeeId)
        {
            Response<List<Customer>> response = null;
            List<Customer> deliveryCustomers = new List<Customer>();
            try
            {

                // Check if the data is already in the cache
                /*
                  var cacheKey = $"CustomerList_{employeeId}";
                  var cachedData = MemoryCache.Default.Get(cacheKey) as List<Customer>;

                  if (cachedData != null)
                  {
                      return Ok(new Response<List<Customer>>()
                      {
                          IsError = false,
                          Message = "Customer details fetched successfully from cache",
                          Result = cachedData,
                          TotalRecords = cachedData.Count
                      });
                  }
                */


                var lstCustomer = await customerDALObj.GetCustomerListAsync(employeeId);
                foreach (SP_GetCustomerListByRoute_Result cust in lstCustomer)
                {

                    deliveryCustomers.Add(new Models.Customer()
                    {
                        CustomerId = cust.customer_id,
                        CustomerName = cust.customer_name,
                        Address1 = cust.address1,
                        Address2 = cust.address2,
                        City = cust.city,//area
                        Street = cust.street,//apartment
                        POBox = cust.pobox,
                        Email = cust.email,
                        Phone = cust.phone,
                        Mobile = cust.mobile,
                        TRN = cust.trn,
                        LAT = cust.lat,
                        LNG = cust.lng,
                        Route = cust.route_name ?? "",
                        Outstanding = Convert.ToDecimal(cust.outstanding_amount),
                        Remarks = "",
                        WalletNumber = cust.wallet_number,
                        Delivery_interval = cust.delivery_interval,
                        Payment_mode = cust.payment_mode,
                        Building = cust.building_name ?? "",
                        DistanceRadious = (int)cust.distance,
                        WalletBalance = (decimal)cust.wallet_balance

                    });
                }

                // Cache the fetched data
                //MemoryCache.Default.Add(cacheKey, deliveryCustomers, DateTimeOffset.UtcNow.AddMinutes(60)); // Cache for 15 minutes


                response = new Response<List<Customer>>()
                {
                    IsError = false,
                    Message = " Customer details fetched sucessfully",
                    Result = deliveryCustomers,
                    TotalRecords = deliveryCustomers.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer . Please check the logs for more details", ex);
                response = new Response<List<Customer>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching customer . Please check the logs for more details"
                };
            }

            return Ok(response);
        }

        /// <summary>
        /// To find nearest customers using LAT and LNG
        /// Any changes made in this function shoul be append in GetCustomerList(int employeeId)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="lat">Current location</param>
        /// <param name="lng">Current location</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCustomerList(int employeeId, decimal lat, decimal lng)
        {
            ClearCacheForEmployee(employeeId);

            Response<List<Customer>> response = null;
            List<Customer> deliveryCustomers = new List<Customer>();
            try
            {
                List<SP_GetCustomerNearest_Result> lstCustomer = customerDALObj.GetCustomerListNearestLocation(employeeId, lat, lng);
                foreach (SP_GetCustomerNearest_Result cust in lstCustomer)
                {
                    int distance = 0;
                    var input = cust.remarks;
                    if (!string.IsNullOrEmpty(cust.remarks))
                    {
                        if (cust.remarks.ToLower().Contains("[") && cust.remarks.Contains("]"))
                        {
                            try
                            {
                                string output = input.Substring(input.IndexOf('[') + 1, input.IndexOf(']') - input.IndexOf('[') - 1);
                                output = System.Text.RegularExpressions.Regex.Replace(output, @"\s+", "");
                                distance = Convert.ToInt32(output);
                            }
                            catch { throw; }
                        }
                    }

                    if (cust != null)
                    {
                        decimal walletBalance = 0;
                        string custName = cust.customer_name;
                        try
                        {
                            if (cust.wallet_balance != null && (cust.payment_mode.ToLower() == "coupon") || cust.wallet_balance != 0)
                            {
                                custName += $" | Wallet: {cust.wallet_balance} |";
                                walletBalance = Convert.ToDecimal(cust.wallet_balance);
                            }
                            else if (cust.payment_mode.ToLower() == "do")
                            {
                                custName += $" | DO |";
                                cust.outstanding_amount = 0;
                            }

                        }
                        catch { }
                        deliveryCustomers.Add(new Models.Customer()
                        {
                            CustomerId = cust.customer_id,
                            CustomerName = custName,
                            Address1 = cust.address1,
                            Address2 = cust.address2,
                            City = cust.city,//area
                            Street = cust.street,//apartment
                            POBox = cust.pobox,
                            Email = cust.email,
                            Phone = cust.phone,
                            Mobile = cust.mobile,
                            TRN = cust.trn,
                            LAT = cust.lat,
                            LNG = cust.lng,
                            Route = cust.route_id != null ? cust.route_name : "",
                            Outstanding = Convert.ToDecimal(cust.outstanding_amount),
                            Remarks = "",
                            WalletNumber = cust.wallet_number,
                            Delivery_interval = cust.delivery_interval,
                            Payment_mode = cust.payment_mode,
                            Building = cust.building_id != null ? cust.building_name : "",
                            DistanceRadious = distance,
                            WalletBalance = walletBalance


                        });
                    }

                }


                response = new Response<List<Customer>>()
                {
                    IsError = false,
                    Message = " Customer details fetched sucessfully",
                    Result = deliveryCustomers,
                    TotalRecords = deliveryCustomers.Count
                };
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer . Please check the logs for more details", ex);
                response = new Response<List<Customer>>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching customer . Please check the logs for more details"
                };
            }

            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        public IHttpActionResult GetCustomerOustanding(int customerId)
        {
            Response<decimal> response = null;
            decimal outstandingAmount = 0;
            try
            {
                customer cus = customerDALObj.GetCustomerDetails(customerId);
                if (cus != null)
                {
                    if (cus.payment_mode.ToLower() == "do")
                        outstandingAmount = 0;
                    else
                        outstandingAmount = Convert.ToDecimal(cus.outstanding_amount);
                    response = new Response<decimal>()
                    {
                        IsError = false,
                        Message = " Customer details fetched sucessfully",
                        Result = outstandingAmount,
                        TotalRecords = 1
                    };
                }
            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer . Please check the logs for more details", ex);
                response = new Response<decimal>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching customer . Please check the logs for more details"
                };
            }
            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerItem")]
        public IHttpActionResult GetCustomerItem(int customerId)
        {

            Response<List<CustomerItem>> response = null;
            List<CustomerItem> CustomerItem = new List<CustomerItem>();
            try
            {
                int defaultItem = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultItem"]);

                DAL.DAL.CustomerAggrementDAL customerAggrementDAL = new CustomerAggrementDAL();
                List<customer_aggrement> listItems = new List<customer_aggrement> { };

                if (defaultItem == -1)
                    listItems = customerAggrementDAL.GetCustomerAggrement(customerId);
                else
                {
                    listItems = customerAggrementDAL.GetCustomerAggrementDefault(customerId, defaultItem);
                }

                if (listItems != null && listItems.Count > 0)
                {

                    customer cs = customerDALObj.GetCustomerDetails(customerId);
                    decimal rechargeTax = 0;
                    int doNewScreen = 2;
                    try
                    {
                        rechargeTax = Convert.ToDecimal(ConfigurationManager.AppSettings["RechargeTax"]);
                        doNewScreen = Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["DoNewScreen"]));
                    }
                    catch { }

                    foreach (customer_aggrement it in listItems)
                    {
                        int showApp = 1;
                        if (it.show_app == null)
                            showApp = 1;
                        else
                        {
                            showApp = Convert.ToInt32(it.show_app);
                        }
                        if (showApp == 1)
                        {
                            decimal vatRate = Convert.ToDecimal(it.item.tax_setting.tax_value);
                            decimal vatAmount = Math.Round(((it.unit_price * vatRate) / 100), 3);
                            decimal rate = vatRate > 0 ? it.unit_price + vatAmount : it.unit_price;
                            //if (rechargeTax > 0 && (cs.payment_mode != null && cs.payment_mode.ToLower() == "coupon"))
                            //    rate = it.unit_price;

                            decimal qty = 0;
                            if (it.item.item_name.ToLower().Contains("drinking water") || it.item.item_name.ToLower().Contains("water"))
                                qty = it.max_qty;
                            else
                                qty = 0;


                            if (it != null)
                            {
                                CustomerItem.Add(new Models.CustomerItem()
                                {
                                    ItemId = it.item_id,
                                    ItemName = (doNewScreen == 1 && cs.payment_mode.ToLower() == "do") ? $"{it.item.item_name} " : $"{it.item.item_name} @ {rate}",
                                    Qty = (int)Decimal.Truncate(qty),
                                    Rate = rate
                                });
                            }
                        }
                    }

                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "Items fetched",
                        Result = CustomerItem,
                        TotalRecords = CustomerItem.Count
                    };
                }
                else
                {
                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "No items found",
                        Result = null,
                        TotalRecords = 0
                    };
                }



            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer items . Please check the logs for more details", ex);
                response = new Response<List<CustomerItem>>()
                {
                    IsError = true,
                    Message = "Unable to get items , please try again or contact for support",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerItemAll")]
        public IHttpActionResult GetCustomerItemAll(int customerId/*,int loadAllItem=2*/)
        {

            Response<List<CustomerItem>> response = null;
            List<CustomerItem> CustomerItem = new List<CustomerItem>();
            try
            {
                DAL.DAL.CustomerAggrementDAL customerAggrementDAL = new CustomerAggrementDAL();
                List<customer_aggrement> listItems = customerAggrementDAL.GetCustomerAggrement(customerId);
                if (listItems != null && listItems.Count > 0)
                {

                    customer cs = customerDALObj.GetCustomerDetails(customerId);
                    decimal rechargeTax = 0;
                    int doNewScreen = 2;
                    try
                    {
                        rechargeTax = Convert.ToDecimal(ConfigurationManager.AppSettings["RechargeTax"]);
                        doNewScreen = Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["DoNewScreen"]));
                    }
                    catch { }

                    foreach (customer_aggrement it in listItems)
                    {
                        int showApp = 1;
                        if (it.show_app == null)
                            showApp = 1;
                        else
                        {
                            showApp = Convert.ToInt32(it.show_app);
                        }
                        if (showApp == 1)
                        {
                            decimal vatRate = Convert.ToDecimal(it.item.tax_setting.tax_value);
                            decimal vatAmount = Math.Round(((it.unit_price * vatRate) / 100), 3);
                            decimal rate = vatRate > 0 ? it.unit_price + vatAmount : it.unit_price;
                            //if (rechargeTax > 0 && (cs.payment_mode != null && cs.payment_mode.ToLower() == "coupon"))
                            //    rate = it.unit_price;

                            decimal qty = it.max_qty;
                            //if (it.item.item_name.ToLower().Contains("drinking water") || it.item.item_name.ToLower().Contains("water"))
                            //    qty = it.max_qty;
                            //else
                            //    qty = 0;


                            if (it != null)
                            {
                                CustomerItem.Add(new Models.CustomerItem()
                                {
                                    ItemId = it.item_id,
                                    ItemName = (doNewScreen == 1 && cs.payment_mode.ToLower() == "do") ? $"{it.item.item_name} " : $"{it.item.item_name} @ {rate}",
                                    Qty = (int)Decimal.Truncate(qty),
                                    Rate = rate
                                });
                            }
                        }
                    }

                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "Items fetched",
                        Result = CustomerItem,
                        TotalRecords = CustomerItem.Count
                    };
                }
                else
                {
                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "No items found",
                        Result = null,
                        TotalRecords = 0
                    };
                }



            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer items . Please check the logs for more details", ex);
                response = new Response<List<CustomerItem>>()
                {
                    IsError = true,
                    Message = "Unable to get items , please try again or contact for support",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }

        /// <summary>
        /// ItemType= Deposit // will get some items like emtybottle
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerItemSpecial")]
        public IHttpActionResult GetCustomerItemSpecial(int customerId, string itemType)
        {
            Response<List<CustomerItem>> response = null;
            List<CustomerItem> CustomerItem = new List<CustomerItem>();
            try
            {
                DAL.DAL.CustomerAggrementDAL customerAggrementDAL = new CustomerAggrementDAL();
                List<customer_aggrement> listItems = customerAggrementDAL.GetCustomerAggrement(customerId);
                if (listItems != null && listItems.Count > 0)
                {

                    customer cs = customerDALObj.GetCustomerDetails(customerId);
                    decimal rechargeTax = 0;
                    int doNewScreen = 2;
                    try
                    {
                        rechargeTax = Convert.ToDecimal(ConfigurationManager.AppSettings["RechargeTax"]);
                        doNewScreen = Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["DoNewScreen"]));
                    }
                    catch { }

                    foreach (customer_aggrement it in listItems)
                    {
                        int showApp = 1;
                        if (it.show_app == null)
                            showApp = 1;
                        else
                        {
                            showApp = Convert.ToInt32(it.show_app);
                        }
                        if (showApp == 1)
                        {
                            decimal vatRate = Convert.ToDecimal(it.item.tax_setting.tax_value);
                            decimal vatAmount = Math.Round(((it.unit_price * vatRate) / 100), 2);
                            decimal rate = vatRate > 0 ? it.unit_price + vatAmount : it.unit_price;
                            //if (rechargeTax > 0 && (cs.payment_mode != null && cs.payment_mode.ToLower() == "coupon"))
                            //    rate = it.unit_price;

                            decimal qty = it.max_qty;
                            //if (it.item.item_name.ToLower().Contains("drinking water") || it.item.item_name.ToLower().Contains("water"))
                            //    qty = it.max_qty;
                            //else
                            //    qty = 0;


                            if (it != null)
                            {
                                CustomerItem.Add(new Models.CustomerItem()
                                {
                                    ItemId = it.item_id,
                                    ItemName = (doNewScreen == 1 && cs.payment_mode.ToLower() == "do") ? $"{it.item.item_name} " : $"{it.item.item_name} @ {rate}",
                                    Qty = (int)Decimal.Truncate(qty),
                                    Rate = rate
                                });
                            }
                        }
                    }

                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "Items fetched",
                        Result = CustomerItem,
                        TotalRecords = CustomerItem.Count
                    };
                }
                else
                {
                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "No items found",
                        Result = null,
                        TotalRecords = 0
                    };
                }



            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer items . Please check the logs for more details", ex);
                response = new Response<List<CustomerItem>>()
                {
                    IsError = true,
                    Message = "Unable to get items , please try again or contact for support",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerAgreement")]
        public IHttpActionResult GetCustomerAgreement(int customerId)
        {
            Response<List<CustomerItem>> response = null;
            List<CustomerItem> CustomerItem = new List<CustomerItem>();
            try
            {
                DAL.DAL.CustomerAggrementDAL customerAggrementDAL = new CustomerAggrementDAL();
                List<customer_aggrement> listItems = customerAggrementDAL.GetCustomerAggrement(customerId);
                if (listItems != null && listItems.Count > 0)
                {
                    foreach (customer_aggrement it in listItems)
                    {

                        if (it != null)
                        {
                            decimal vatRate = Convert.ToDecimal(it.item.tax_setting.tax_value);
                            decimal vatAmount = Math.Round(((it.unit_price * vatRate) / 100), 2);
                            decimal rate = vatRate > 0 ? it.unit_price + vatAmount : it.unit_price;
                            CustomerItem.Add(new Models.CustomerItem()
                            {
                                ItemId = it.item_id,
                                ItemName = it.item.item_name,
                                Qty = (int)Decimal.Truncate(it.max_qty),
                                Rate = rate
                            });
                        }

                    }

                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "Items fetched",
                        Result = CustomerItem,
                        TotalRecords = CustomerItem.Count
                    };
                }
                else
                {
                    response = new Response<List<CustomerItem>>()
                    {
                        IsError = false,
                        Message = "No items found",
                        Result = null,
                        TotalRecords = 0
                    };
                }



            }

            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching customer items . Please check the logs for more details", ex);
                response = new Response<List<CustomerItem>>()
                {
                    IsError = true,
                    Message = "Unable to get items , please try again or contact for support",
                    Result = null,
                    TotalRecords = 0
                };
            }

            return Ok(response);
        }


        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetPaymentmodesandInterval")]
        public IHttpActionResult GetPaymentmodesandInterval(int employeeId = 0)
        {
            Response<Models.PaymentModes> response = null;
            try
            {
                PaymentModes paymentModes = new PaymentModes();

                paymentModes.Routes = customerDALObj.GetRoutes();
                if (employeeId > 0)
                {
                    try
                    {
                        string route = customerDALObj.GetEmployeeRoute(employeeId);
                        if (!string.IsNullOrEmpty(route))
                        {
                            paymentModes.Routes.Insert(0, route);
                        }
                    }
                    catch { }
                }
                paymentModes.Building = customerDALObj.GetBuilding(employeeId);

                response = new Response<Models.PaymentModes>()
                {
                    IsError = false,
                    Message = "Details fetched sucessfully",
                    Result = paymentModes,
                    TotalRecords = 1
                };


            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching payment modes . Please check the logs for more details", ex);
                response = new Response<Models.PaymentModes>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching payment modes . Please check the logs for more details"
                };
            }
            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetCustomerDefaultPaymentMode")]
        public IHttpActionResult GetCustomerDefaultPaymentMode(int customerId)
        {
            Response<CustomerPaymentMode> response = null;
            try
            {
                string paymentMode = string.Empty, walletnumber = string.Empty;
                List<CustomerDivision> customerDivision = null;
                customer cs = customerDALObj.GetCustomerDetails(customerId);
                if (cs != null)
                {
                    paymentMode = cs.payment_mode;
                    walletnumber = cs.wallet_number;

                }
                var divisionList = customerDALObj.GetCustomerDivision(customerId);
                if (divisionList != null)
                {
                    customerDivision = new List<CustomerDivision>();
                    foreach (var dv in divisionList)
                    {
                        customerDivision.Add(new CustomerDivision
                        {
                            DivisionId = dv.division_id,
                            DivisionName = dv.division_name
                        });
                    }
                }


                Models.CustomerPaymentMode _paymentMode = new CustomerPaymentMode
                {
                    PaymentMode = paymentMode,
                    CouponCode = walletnumber,
                    Division = customerDivision

                };

                response = new Response<Models.CustomerPaymentMode>()
                {
                    IsError = false,
                    Message = "Details fetched sucessfully",
                    Result = _paymentMode,
                    TotalRecords = 1
                };


            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while fetching payment modes . Please check the logs for more details", ex);
                response = new Response<Models.CustomerPaymentMode>()
                {
                    IsError = true,
                    Message = "Exception occured while fetching payment modes . Please check the logs for more details"
                };
            }
            return Ok(response);
        }
        [System.Web.Http.HttpGet]
        [Route("api/Customer/ValidateDocumentUpdateActivation")]
        public IHttpActionResult ValidateDocumentUpdateActivation(int employeeId)
        {
            Response<List<string>> response = null;
            try
            {

                int result = Convert.ToInt32(Convert.ToDecimal(ConfigurationManager.AppSettings["EnableUploadDoc"]));
                string message = result == 1 ? "Document upload activated" : "Document upload not activated\n please contact vendor";

                List<string> listDocTypes = new List<string> { "Agreement", "DamagePhoto", "OfficePhoto", "ProductPhoto" };
                if (result != 1)
                {
                    listDocTypes = new List<string>();
                }
                response = new Response<List<string>>()
                {
                    IsError = false,
                    Message = message,
                    Result = listDocTypes,
                    TotalRecords = result == 1 ? 1 : 0
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while checking documnet upload activation status", ex);
                response = new Response<List<string>>()
                {
                    IsError = true,
                    Message = "Exception occured while checking documnet upload activation status",
                    TotalRecords = 0,
                    Result = new List<string>()
                };
            }
            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [Route("api/Customer/GetEmployeePunchStatus")]
        public IHttpActionResult GetEmployeePunchStatus(int employeeId)
        {
            Response<Models.PunchStatus> response = null;
            try
            {

                var punchStatus = customerDALObj.GetEmployeePunchStatus(employeeId);

                Models.PunchStatus punch = new Models.PunchStatus
                {
                    PunchDate = punchStatus.PunchDate,
                    PunchIn = punchStatus.PunchIn,
                    PunchOut = punchStatus.PunchOut,
                    PunchType = punchStatus.PunchType,
                    PunchId = punchStatus.PunchId,
                    EmployeeName=punchStatus.EmployeeName
                    
                };

                response = new Response<Models.PunchStatus>()
                {
                    IsError = false,
                    Message = "Success",
                    Result = punch,
                    TotalRecords = 1
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while checking documnet upload activation status", ex);
                response = new Response<Models.PunchStatus>()
                {
                    IsError = true,
                    Message = "Exception occured while checking documnet upload activation status",
                    TotalRecords = 0,
                    Result = new PunchStatus { }
                };
            }
            return Ok(response);
        }

        [System.Web.Http.HttpPost]
        [Route("api/Customer/SavePunch")]
        public IHttpActionResult SavePunch(Punch punch)
        {
            punch.AppDate = DateTime.Now;
            Response<string> response = null;
            try
            {
                Logger.Error($"Punch ID :{punch.PunchId} , Punch Date:{punch.PunchDate} , EmployeeId {punch.EmployeeId}");


                DAL.PunchModel punchModel = new DAL.PunchModel
                {
                    PunchId=punch.PunchId,
                    AppDate = punch.AppDate,
                    AppVersion = punch.AppVersion,
                    EmployeeId = punch.EmployeeId,
                    Lat = punch.Lat,
                    Lng = punch.Lng,
                    LocationDetails = punch.LocationDetails,
                    PunchDate = DateTime.Now,
                    PunchIn = DateTime.Now,
                    PunchOut = DateTime.Now,
                    Remarks = string.Empty,
                    Status = 1,
                };

                var punchStatus = customerDALObj.InsertPunchAsync(punchModel);

                response = new Response<string>()
                {
                    IsError = false,
                    Message = "Success",
                    Result = "Success",
                    TotalRecords = 1
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured while checking documnet upload activation status", ex);
                response = new Response<string>()
                {
                    IsError = true,
                    Message = "Exception occured while checking documnet upload activation status",
                    TotalRecords = 0,
                    Result = "Error"
                };
            }
            return Ok(response);
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
