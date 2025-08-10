using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using BETask.APP.DAL;

namespace BETask.BAL
{

    class CustomerBAL
    {
        CustomerDAL customerDAL = new CustomerDAL();
        CustomerDAL objCustomerDAL = null;
        public CustomerBAL()
        {
            objCustomerDAL = new CustomerDAL();
        }


        public List<EDMX.customer> GetALLCustomerDirect(int maxCount, string searchValue, int customerType, int route = 0, int employeeId = 0, bool addAgreement = false, bool addClosingStock = false, string paymentMode = "")
        {
            try
            {
                return objCustomerDAL.GetAllCustomers(maxCount, searchValue, customerType, route, employeeId, addAgreement, addClosingStock, paymentMode);
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public DataTable GetCustomerListRouteWise(int routeId, int employeeId, bool isActiveCustomers, bool isDatewise, DateTime dateFrom, DateTime dateTo, string paymentMode = "")
        {
            try
            {
                return objCustomerDAL.GetCustomerListRouteWise(routeId, employeeId, isActiveCustomers, isDatewise, dateFrom, dateTo, paymentMode);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<EDMX.customer> GetALLCustomerWithInPeriod(DateTime dateFrom, DateTime dateTo, int maxCount, string searchValue, int customerType, int route = 0, int employeeId = 0, bool addAgreement = false, string paymentMode = "")
        {
            try
            {
                return objCustomerDAL.GetALLCustomerWithInPeriod(dateFrom, dateTo, maxCount, searchValue, customerType, route, employeeId, addAgreement, paymentMode);
            }
            catch (Exception ee)
            {
                throw;
            }
        }



        public betaskdbEntities GetBetaskdbEntities()
        {
            return objCustomerDAL.GetBetaskdbEntities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCount"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<CustomerModel> GetAllCustomers(int maxCount, string searchValue, int customerType, int route = 0, string paymentMode = "", betaskdbEntities context = null)
        {
            List<CustomerModel> lstCustomers = new List<CustomerModel>();
            try
            {
                List<EDMX.customer> _customers = objCustomerDAL.GetAllCustomers(maxCount, searchValue, customerType, route, false, paymentMode, context);
                if (_customers != null && _customers.Count > 0)
                {

                    foreach (EDMX.customer objCus in _customers)
                    {
                        int ledgerId = 0;
                        if (objCus.ledger_id == null) ledgerId = 0;
                        else
                            ledgerId = Convert.ToInt32(objCus.ledger_id);
                        if (objCus != null)
                        {
                            lstCustomers.Add(new CustomerModel()
                            {
                                Customer_Name = objCus.customer_name,
                                Mobile = objCus.mobile,
                                Phone = objCus.phone,
                                Customer_Id = objCus.customer_id,
                                Address1 = objCus.address1,
                                Address2 = objCus.address2,
                                Trn = objCus.trn,
                                WalletBalance = Convert.ToDecimal(objCus.wallet_balance),
                                WalletNumber = objCus.wallet_number,
                                LedgerId = ledgerId,
                                CloudSunc = objCus.cloud_sync,
                                agreement = objCus.remarks,
                                status = objCus.status,
                                RouteId = objCus.route_id != null ? Convert.ToInt32(objCus.route_id) : 0
                            });
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return lstCustomers;

        }
        public List<CustomerModel> GetAllCustomers(int routeId)
        {
            List<CustomerModel> lstCustomers = new List<CustomerModel>();
            try
            {
                var customers = objCustomerDAL.GetCustomerListByRoute(routeId);
                if (customers.Rows.Count > 0)
                {

                    foreach (DataRow dr in customers.Rows)
                    {
                        lstCustomers.Add(new CustomerModel()
                        {
                            Customer_Name = Convert.ToString(dr["customer_name"]),
                            Customer_Id = Convert.ToInt32(dr["customer_id"]),

                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            return lstCustomers;

        }

        public EDMX.customer GetCustomerDetailsByLedger(int ledgerId)
        {
            return customerDAL.GetCustomerDetailsByLedger(ledgerId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CustomerModel GetCustomerDetail(int customerId)
        {
            CustomerModel customer = null;
            try
            {
                EDMX.customer _customer = objCustomerDAL.GetCustomerDetails(customerId);
                if (_customer != null)
                {
                    if (StaticDBData.listEmployee == null || StaticDBData.listEmployee.Count == 0)
                    {
                        StaticDBData.LoadEmployee();
                    }
                    var employee = StaticDBData.listEmployee.Find(x => x.employee_id == _customer.employee_id);


                    customer = new CustomerModel()
                    {
                        Address1 = _customer.address1,
                        Address2 = _customer.address2,
                        City = _customer.city,
                        Customer_Id = _customer.customer_id,
                        Email = _customer.email,
                        Mobile = _customer.mobile,
                        Customer_Name = _customer.customer_name,
                        Phone = _customer.phone,
                        POBox = _customer.pobox,
                        Street = _customer.street,
                        Customer_Type = _customer.customer_type,
                        ContactPerson = _customer.contact_person,
                        Remarks = _customer.remarks,
                        Trn = _customer.trn,
                        WalletNumber = _customer.wallet_number,
                        WalletBalance = Convert.ToDecimal(_customer.wallet_balance),
                        status = _customer.status,
                        LedgerId = Convert.ToInt32(_customer.ledger_id),
                        Lat = _customer.lat,
                        Lng = _customer.lng,
                        RouteId = Convert.ToInt32(_customer.route_id),
                        BuildingId = Convert.ToInt32(_customer.building_id),
                        DeliveryInterval = _customer.delivery_interval,
                        Paymentmode = _customer.payment_mode,
                        AddedOn = _customer.added_time.ToString(),
                        NewCustomer = Convert.ToInt32(_customer.new_customer) == 0 ? 1 : Convert.ToInt32(_customer.new_customer),
                        EmployeeId = Convert.ToInt32(_customer.employee_id) == 0 ? 2 : Convert.ToInt32(_customer.employee_id),
                        EmployeeName = Convert.ToInt32(_customer.employee_id) == 0 ? "" : String.Format("{0} {1} ", employee.first_name, employee.last_name),
                        CreditLimit = _customer.credit_limit,
                        OfferId = _customer.offer_id == null ? 0 : Convert.ToInt32(_customer.offer_id),
                        EnableOffer = Convert.ToInt32(_customer.enable_offer),
                        EnableOnlinePayment = Convert.ToInt32(_customer.enable_online_payment),
                        LocationDistance = _customer.location_distance,
                        is_group = _customer.is_group,
                        group_id = Convert.ToInt32(_customer.group_id)

                    };
                }
            }
            catch
            {
                throw;
            }
            return customer;
        }
        public CustomerModel GetCustomerDetailsByName(string custmomername)
        {
            CustomerModel customer = null;

            try
            {
                EDMX.customer _customer = objCustomerDAL.GetCustomerDetailsByName(custmomername);
                if (_customer != null)
                {
                    customer = new CustomerModel()
                    {
                        Address1 = _customer.address1,
                        Address2 = _customer.address2,
                        City = _customer.city,
                        Customer_Id = _customer.customer_id,
                        Email = _customer.email,
                        Mobile = _customer.mobile,
                        Customer_Name = _customer.customer_name,
                        Phone = _customer.phone,
                        POBox = _customer.pobox,
                        Street = _customer.street,
                        Customer_Type = _customer.customer_type,
                        ContactPerson = _customer.contact_person,
                        Remarks = _customer.remarks,
                        Trn = _customer.trn,
                        WalletNumber = _customer.wallet_number,
                        WalletBalance = Convert.ToDecimal(_customer.wallet_balance),
                        status = _customer.status,
                        LedgerId = Convert.ToInt32(_customer.ledger_id),
                        Lat = _customer.lat,
                        Lng = _customer.lng,
                        RouteId = Convert.ToInt32(_customer.route_id),
                        BuildingId = Convert.ToInt32(_customer.building_id),
                        DeliveryInterval = _customer.delivery_interval,
                        Paymentmode = _customer.payment_mode,
                        AddedOn = _customer.added_time.ToString(),

                    };
                }
            }
            catch
            {
                throw;
            }
            return customer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_customer"></param>
        /// <param name="selectedPendingCusId"></param>
        /// <param name="apponly">While update wallet , no need to update local data again</param>
        public void SaveCustomer(CustomerModel _customer)
        {
            try
            {

                int customerId = 0; bool newCustomer = _customer.Customer_Id == 0 ? true : false;
                EDMX.customer customer = new EDMX.customer();
                if (_customer != null)
                {
                    customer = new EDMX.customer()
                    {
                        address1 = _customer.Address1,
                        address2 = _customer.Address2,
                        city = _customer.City,
                        customer_id = _customer.Customer_Id,
                        email = _customer.Email,
                        mobile = _customer.Mobile,
                        customer_name = _customer.Customer_Name,
                        phone = _customer.Phone,
                        pobox = _customer.POBox,
                        street = _customer.Street,
                        customer_type = _customer.Customer_Type,
                        contact_person = _customer.ContactPerson,
                        remarks = _customer.Remarks,
                        trn = _customer.Trn,
                        wallet_number = _customer.WalletNumber,
                        status = _customer.status,
                        lat = _customer.Lat,
                        lng = _customer.Lng,
                        route_id = _customer.RouteId,
                        building_id = _customer.BuildingId,
                        ledger_id = _customer.LedgerId,
                        wallet_balance = _customer.WalletBalance,
                        cloud_sync = 1,
                        payment_mode = _customer.Paymentmode,
                        delivery_interval = _customer.DeliveryInterval,
                        added_time = DateTime.Now,
                        credit_limit = _customer.CreditLimit,
                        employee_id = _customer.EmployeeId,
                        new_customer = _customer.NewCustomer,
                        offer_id = _customer.OfferId,
                        enable_offer = _customer.EnableOffer,
                        enable_online_payment = _customer.EnableOnlinePayment,
                        location_distance = _customer.LocationDistance,
                        is_group = _customer.is_group,
                        group_id = _customer.group_id
                    };

                    customerId = objCustomerDAL.SaveCustomer(customer);

                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = _customer.Customer_Id,
                    summary = $" Saving Customer {_customer.Customer_Name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }

        }
        public void UpdateCustomerOnlineProfile(EDMX.customer customer)
        {
            try
            {
                customerDAL.UpdateCustomerOnlineProfile(customer);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = customer.customer_id,
                    summary = $" Saving Customer {customer.customer_name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<EDMX.customer> CustomerSearch(string name, int routeId, int employeeId, string phone, int id, string address, int active = 1)
        {

            try
            {


                return customerDAL.CustomerSearch(name, routeId, employeeId, phone, id, address, active);
            }
            catch (Exception ee)
            {
                throw;
            }

        }
        public void PrintGetCustomerListRouteWise(int routeId, int employeeId, bool isActiveCustomers, bool isDatewise, DateTime dateFrom, DateTime dateTo, string paymentMode = "", string routeName = "")
        {

            try
            {
                string contact = string.Empty;
                string dateAdded = string.Empty;

                DataTable tblCustomerReport = objCustomerDAL.GetCustomerListRouteWise(routeId, employeeId, isActiveCustomers, isDatewise, dateFrom, dateTo, paymentMode);


                if (tblCustomerReport != null && tblCustomerReport.Rows.Count > 0)
                {
                    DataTable tblCustomerList = new DataTable();
                    BETask.Report.DSReports.CustomerListDataTable customerListDataTable = new Report.DSReports.CustomerListDataTable();
                    tblCustomerList = customerListDataTable.Clone();
                    foreach (DataRow dr in tblCustomerReport.Rows)
                    {

                        {
                            DataRow dataRow = tblCustomerList.NewRow();

                            dateAdded = Convert.ToString(dr["addedOn"]);

                            dataRow["DateAdded"] = string.IsNullOrEmpty(dateAdded) ? "" : DateTime.Parse(dateAdded).ToString("dd/MM/yyyy");
                            dataRow["CustomerID"] = Convert.ToInt32(dr["customer_id"]);
                            dataRow["CustomerName"] = Convert.ToString(dr["customer_name"]);
                            dataRow["Address1"] = $"{Convert.ToString(dr["address1"])} , { Convert.ToString(dr["city"])} , { Convert.ToString(dr["street"])}";
                            dataRow["Route"] = $"{Convert.ToString(dr["route_name"]) }";
                            dataRow["City"] = Convert.ToString(dr["city"]);
                            dataRow["Street"] = Convert.ToString(dr["street"]);
                            dataRow["ContactPerson"] = Convert.ToString(dr["contact_person"]);
                            dataRow["Contact"] = Convert.ToString(dr["Phone"] + "\n" + dr["Mobile"] + "\n" + dr["ProfilePhone"] + "\n" + dr["email"]);
                            dataRow["Email"] = Convert.ToString(dr["email"]); ;
                            tblCustomerList.Rows.Add(dataRow);
                        }
                    }
                    if (tblCustomerList != null && tblCustomerList.Rows.Count > 0)
                    {
                        int custType = 1;
                        RPT reportForm = new RPT(RPT.EnumReportType.CustomerList, custType == 1 ? $"Customer List {routeName}" : "Supplier List", tblCustomerList);
                        reportForm.Show();
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Print Customer List ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }


        }
        public void CustomerList(int custType, int route, string routeName = "", string paymentMode = "")
        {

            try
            {
                string contact = string.Empty;
                string dateAdded = string.Empty;

                List<EDMX.customer> _customers = objCustomerDAL.GetAllCustomers(0, "", custType, route, false, paymentMode);


                if (_customers != null && _customers.Count > 0)
                {
                    DataTable tblCustomerList = new DataTable();
                    BETask.Report.DSReports.CustomerListDataTable customerListDataTable = new Report.DSReports.CustomerListDataTable();
                    tblCustomerList = customerListDataTable.Clone();
                    foreach (EDMX.customer objCus in _customers)
                    {
                        if (objCus.status == 1)
                        {

                            string building = objCus.building != null ? $", {objCus.building.building_name}" : "";

                            DataRow dataRow = tblCustomerList.NewRow();
                            if (objCus.added_time == null) { dateAdded = ""; } else { dateAdded = Convert.ToString(General.ConvertDateAppFormat(objCus.added_time.Value)); }
                            dataRow["DateAdded"] = dateAdded;
                            dataRow["CustomerID"] = objCus.customer_id;
                            dataRow["CustomerName"] = objCus.customer_name;
                            dataRow["Address1"] = objCus.address1;
                            dataRow["Route"] = $"{objCus.route.route_name}{building}";
                            dataRow["City"] = objCus.city;
                            dataRow["Street"] = objCus.street;
                            dataRow["ContactPerson"] = objCus.contact_person;
                            if (objCus.phone == objCus.mobile) { dataRow["Contact"] = string.Format("{0}", objCus.phone); }
                            else { dataRow["Contact"] = string.Format("{0},{1}", objCus.phone, objCus.mobile); }
                            dataRow["Email"] = objCus.email;
                            tblCustomerList.Rows.Add(dataRow);
                        }
                    }
                    if (tblCustomerList != null && tblCustomerList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.CustomerList, custType == 1 ? $"Customer List {routeName}" : "Supplier List", tblCustomerList);
                        reportForm.Show();
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Print Customer List ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }


        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="amount"></param>
        public decimal UpdateWalletBalance(int customerId, decimal amount)
        {
            try
            {

                return objCustomerDAL.UpdateWalletBalance(customerId, amount);
            }
            catch
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = customerId,
                    summary = $" Update Wallet balance {amount} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }


        public DataTable GetCustomerPerformance(DateTime dateFrom, DateTime dateTo, int transFrom, int transTo, int routeId, int employeeId)
        {
            try
            {
                return objCustomerDAL.GetCustomerPerfomance(dateFrom, dateTo, transFrom, transTo, routeId, employeeId);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.CustomerPerformanceModel> GetCustomerPerformanceOL(DateTime dateFrom, DateTime dateTo, int transFrom, int transTo, int routeId)
        {
            try
            {
                return objCustomerDAL.GetCustomerPerfomanceOL(dateFrom, dateTo, transFrom, transTo, routeId);
            }
            catch
            {
                throw;
            }
        }

        public List<DAL.Model.GroupCustomerModel> GetGroupCustomer(betaskdbEntities context)
        {
            if (context == null)
                return customerDAL.GetGroupCustomer();
            else
                return customerDAL.GetGroupCustomer(context);
        }

        public void PrintCustomerPerformanceReport(DateTime dateFrom, DateTime dateTo, int transFrom, int transTo, int routeId, string header, int employeeId)
        {
            try
            {
                DataTable tblReport = objCustomerDAL.GetCustomerPerfomance(dateFrom, dateTo, transFrom, transTo, routeId, employeeId);
                if (tblReport != null && tblReport.Rows.Count > 0)
                {
                    DataTable tblDReport = new DataTable();
                    BETask.Report.DSReports.CustomerrPerformanceDataTable customerrPerformanceDataTable = new Report.DSReports.CustomerrPerformanceDataTable();
                    tblDReport = customerrPerformanceDataTable.Clone();
                    foreach (DataRow row in tblReport.Rows)
                    {

                        DataRow dr = tblDReport.NewRow();
                        dr["Route"] = row["Route"];
                        dr["Customer"] = $"{row["Customer"]}";
                        dr["Phone"] = row["Mobile"];
                        dr["AddedOn"] = row["AddedOn"];
                        dr["Transactions"] = row["Transactions"];
                        dr["LastTransaction"] = row["LastSale"];
                        dr["Agreement"] = row["Agreement"];
                        dr["PaymentMode"] = row["PaymentMode"];
                        dr["WalletBalance"] = row["Wallet"];

                        tblDReport.Rows.Add(dr);
                    }

                    RPT reportForm = new RPT(RPT.EnumReportType.CustomerPerformance, header, tblDReport);
                    reportForm.Show();

                }
            }
            catch (Exception ee)
            {
                throw;

            }
        }


        public List<DAL.Model.CustomerOutstandingvsWallet> GetCustomerOutstandingvsWallet(int routeId, int customerId, DateTime date)
        {
            try
            {
                return objCustomerDAL.GetCustomerOutstandingvsWallet(routeId, customerId, date);
            }
            catch { throw; }
        }
        public List<EDMX.customer> GetWalletCustomer(int routeId, string walletNumber, bool onlyBelowZero = false, bool noWallet = false)
        {
            try
            {
                return objCustomerDAL.GetWalletCustomer(routeId, walletNumber, onlyBelowZero, noWallet);
            }
            catch { throw; }
        }
        public void PrintCustomerWalletMisc(List<EDMX.customer> listCustomer, int routeId, string walletNumber, bool onlyBelowZero = false, bool noWallet = false)
        {
            try
            {
                if (listCustomer != null && listCustomer.Count > 0)
                {
                    DataTable tblDReport = new DataTable();
                    BETask.Report.DSReports.CustomerWalletMiscDataTable customerWalletMiscDataTable = new Report.DSReports.CustomerWalletMiscDataTable();
                    tblDReport = customerWalletMiscDataTable.Clone();
                    foreach (EDMX.customer cp in listCustomer)
                    {

                        DataRow dr = tblDReport.NewRow();
                        dr["CustomerName"] = cp.customer_name + "\n" + "Phone:" + cp.app_phone;
                        dr["Route"] = cp.route.route_name;
                        dr["WalletNumber"] = cp.wallet_number;
                        dr["Balance"] = cp.wallet_balance;
                        tblDReport.Rows.Add(dr);
                    }

                    RPT reportForm = new RPT(RPT.EnumReportType.CustomerWalletMisc, "", tblDReport);
                    reportForm.Show();
                }
            }
            catch { throw; }
        }
        public List<BETask.APP.EDMX.customer> SelectFilterCustomerApp(List<int> customerIds)
        {
            try
            {
                BETask.APP.DAL.CustomerAppDAL customerAppDAL = new CustomerAppDAL();
                return customerAppDAL.SelectFilterCustomer(customerIds);
            }
            catch { throw; }
        }

        public List<DAL.Model.CustomerAgreementBalanceModel> CustomerStockBalance(int routeId, int customerId, int itemId, string itemName)
        {
            try
            {
                return objCustomerDAL.CustomerStockBalance(routeId, customerId, itemId, itemName);
            }
            catch { throw; }
        }
        public List<DAL.Model.CustomerAgreementBalanceModel> CustomerStockBalanceDateEnd(int routeId, int customerId, int itemId, string itemName, DateTime endDate)
        {
            try
            {
                return objCustomerDAL.CustomerStockBalanceDateEnd(routeId, customerId, itemId, itemName, endDate);
            }
            catch { throw; }
        }
        public List<DAL.Model.CustomerAgreementBalanceModel> CustomerStockBalance(int routeId, int customerId, int itemId, string itemName, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return objCustomerDAL.CustomerStockBalance(routeId, customerId, itemId, itemName, dateFrom, dateTo);
            }
            catch { throw; }
        }
        public List<DAL.Model.CustomerAgreementBalanceModel> CustomerStockBalanceDetailed(int customerId, int itemId, string itemName, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return objCustomerDAL.CustomerStockBalanceDetailed(customerId, itemId, itemName, dateFrom, dateTo);
            }
            catch { throw; }
        }
        public List<DAL.Model.CustomerStatementDetailedModel> CustomerStatementDetailed(DateTime dateFrom, DateTime dateTo, int customerId)
        {
            try
            {
                return objCustomerDAL.CustomerStatementDetailed(dateFrom, dateTo, customerId);
            }
            catch { throw; }
        }
        public List<DAL.CustomerMonthlyOutstandingModel> GetCustomerOutstandingMonthly(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            try
            {
                return objCustomerDAL.GetCustomerMonthlyOutstanding(dateFrom, dateTo, routeId);
            }
            catch { throw; }
        }
        public void PrintCustomerStatementAdvanced(DataTable tblData)
        {
            try
            {
                string header = General.companyName;
                BAL.CompanyBAL companyBAL = new CompanyBAL();
                string companyAddress = companyBAL.GetCompanyAddress();

                RPT reportForm = new RPT(RPT.EnumReportType.CustomerStatementAdvanced, header, companyAddress, tblData);
                reportForm.Show();
            }
            catch { throw; }
        }
        public void PrintCustomerOutstandingMonthly(DateTime dateFrom, DateTime dateTo, int routeId, string header)
        {
            try
            {
                List<DAL.CustomerMonthlyOutstandingModel> listCustomer = objCustomerDAL.GetCustomerMonthlyOutstanding(dateFrom, dateTo, routeId);
                if (listCustomer != null && listCustomer.Count > 0)
                {
                    DataTable tblDReport = new DataTable();
                    BETask.Report.DSReports.CustomerMonthlyOutstandingDataTable customerMonthlyOutstandingDataTable = new Report.DSReports.CustomerMonthlyOutstandingDataTable();
                    tblDReport = customerMonthlyOutstandingDataTable.Clone();
                    foreach (DAL.CustomerMonthlyOutstandingModel cp in listCustomer)
                    {

                        DataRow dr = tblDReport.NewRow();
                        dr["CustomerName"] = cp.CustomerName;
                        dr["OB"] = cp.OB;
                        dr["Debit"] = cp.Debit;
                        dr["Credit"] = cp.Credit;
                        dr["Balance"] = cp.CB;
                        tblDReport.Rows.Add(dr);
                    }

                    RPT reportForm = new RPT(RPT.EnumReportType.CustomerMonthlyOutstanding, header, tblDReport);
                    reportForm.Show();
                }
            }
            catch { throw; }
        }

        public void WalletDirectChangeLog(int customerId, string newBalance)
        {
            try
            {
                Model.CustomerModel cs = GetCustomerDetail(customerId);
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = "WalletDirectChangeLog",
                    reference_id = customerId,
                    summary = $" Updating wallet balance of {cs.Customer_Name}, Old balance={cs.WalletBalance} , New Balance={newBalance}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                // General.ShowMessage(General.EnumMessageTypes.Error, "Error while updating wallete balance");

            }
        }

        public void UpdateLedgerId(int customerId, int salesManLedgerId, int newLedgerId)
        {
            try
            {
                CustomerDAL customerDAL = new CustomerDAL();
                EDMX.customer customer = customerDAL.GetCustomerDetails(customerId);
                customerDAL.UpdateCustomerLedgerId(customer, salesManLedgerId, newLedgerId);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = "LedgerIdChange",
                    reference_id = customerId,
                    summary = newLedgerId > 0 ? $" Updating Ledger id of {customer.customer_name}, Old ledgerId={customer.ledger_id} , New ledger Id={newLedgerId}" : $"Updating Ledger id of {customer.customer_name}, Old ledgerId={customer.salesman_ledger} , New ledger Id={salesManLedgerId}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                // General.ShowMessage(General.EnumMessageTypes.Error, "Error while updating wallete balance");

            }
        }

        public void UpdateCustomerLedgerName(int customerId, string xLedgerName, string ledgerName)
        {
            try
            {
                CustomerDAL customerDAL = new CustomerDAL();
                EDMX.customer customer = customerDAL.GetCustomerDetails(customerId);
                customerDAL.UpdateCustomerLedgerName(customer, ledgerName);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = "LedgerNameChange",
                    reference_id = customerId,
                    summary = $"Ledger name changed {xLedgerName} to {ledgerName}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                // General.ShowMessage(General.EnumMessageTypes.Error, "Error while updating wallete balance");

            }
        }
        public void SaveDivision(EDMX.customer_division division)
        {
            try
            {
                CustomerDAL customerDAL = new CustomerDAL();
                customerDAL.SaveCustomerDivision(division);
            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = division.customer_id,
                    summary = $" Saving of new division {division.customer_id}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
                try
                {
                    SynchronizationBAL sync = new SynchronizationBAL();
                    sync.Route();
                }
                catch (Exception ee)
                {
                    General.Error(ee.ToString());
                }
            }
        }

      
    }
}
