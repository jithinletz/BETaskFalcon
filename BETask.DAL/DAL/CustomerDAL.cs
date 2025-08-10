using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace BETask.DAL.DAL
{
    public class CustomerDAL
    {


        public betaskdbEntities GetBetaskdbEntities()
        {
            return new betaskdbEntities();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_customer"></param>
        /// 
        public int SaveCustomer(customer _customer)
        {
            int customerId = _customer.customer_id;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (_customer.building_id == 0)
                                _customer.building_id = null;

                            if (_customer.employee_id == 0)
                                _customer.employee_id = null;

                            if (_customer.offer_id == 0)
                                _customer.offer_id = null;

                            if (customerId != 0)
                            {
                                customer xCustomer = context.customer.AsNoTracking().Where(x => x.customer_id == customerId).FirstOrDefault();
                                if (xCustomer != null)
                                {

                                    _customer.added_time = xCustomer.added_time;
                                    _customer.customer_temp_id = xCustomer.customer_temp_id;
                                    _customer.added_time = xCustomer.added_time;
                                    _customer.app_address1 = xCustomer.app_address1;
                                    _customer.app_address2 = xCustomer.app_address2;
                                    _customer.app_customer_name = xCustomer.app_customer_name;
                                    _customer.app_email = xCustomer.app_email;
                                    _customer.app_password = xCustomer.app_password;
                                    _customer.app_phone = xCustomer.app_phone;
                                    _customer.offer_category = xCustomer.offer_category;
                                    _customer.outstanding_amount = xCustomer.outstanding_amount;

                                    //Changing ledger name
                                    var ledger = context.account_ledger.Single(x => x.ledger_id == xCustomer.ledger_id);
                                    if (ledger.ledger_name != _customer.customer_name)
                                    {
                                        ledger.ledger_name = _customer.customer_name;
                                        context.account_ledger.Attach(ledger);
                                        context.Entry(ledger).Property(x => x.ledger_name).IsModified = true;
                                        context.SaveChanges();
                                    }
                                }

                            }

                            _customer.route_id = _customer.route_id == 0 ? null : _customer.route_id;
                            _customer.ledger_id = _customer.customer_id == 0 ? null : _customer.ledger_id;

                            context.Entry(_customer).State = _customer.customer_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();

                            if (customerId == 0)
                            {
                                LedgerMappingDAL objLedgerMapDAL = new LedgerMappingDAL();
                                AccountLedgerDAL ledgerDAL = new AccountLedgerDAL();
                                EDMX.ledger_mapping mapLedger = new EDMX.ledger_mapping();
                                string custType = _customer.customer_type == 1 ? "CUSTOMER" : "SUPPLIER";
                                if (_customer.customer_type == 1)
                                {
                                    mapLedger = objLedgerMapDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.CUSTOMER);
                                }
                                else
                                {
                                    mapLedger = objLedgerMapDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.SUPPLIER);
                                }

                                if (mapLedger != null && mapLedger.group_id > 0)
                                {
                                    EDMX.account_ledger ledger = new EDMX.account_ledger()
                                    {
                                        group_id = Convert.ToInt32(mapLedger.group_id),
                                        ledger_name = $"{ _customer.customer_name}",
                                        description = custType,
                                        status = 1,
                                    };
                                    int ledgerId = ledgerDAL.SaveAccountLedger(ledger);
                                    if (ledgerId > 0)
                                    {
                                        _customer.ledger_id = ledgerId;
                                        context.Entry(_customer).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                }
                                customerId = _customer.customer_id;
                            }


                            transaction.Commit();
                            if (_customer.customer_type == 1)
                                UpdateAndGetCustomerOutstanding(customerId, context);

                        }
                        catch (Exception ee)
                        {
                            if (transaction != null)
                                transaction.Rollback();
                            string e = ee.ToString();
                            throw;

                        }

                    }
                }
            }
            catch
            {
                throw;
            }

            return customerId;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="amount"></param>
        public decimal UpdateWalletBalance(int customerId, decimal amount)
        {
            decimal xBalace = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    customer objCustomer = context.customer.Where(c => c.customer_id == customerId).FirstOrDefault();
                    if (!String.IsNullOrEmpty(objCustomer.wallet_number))
                    {

                        if (objCustomer != null)
                        {

                            xBalace = Convert.ToDecimal(objCustomer.wallet_balance);
                            //xBalace = xBalace < 0 ? 0 : xBalace;
                            objCustomer.wallet_balance = xBalace + amount;
                            context.SaveChanges();

                            xBalace = xBalace + amount;
                        }
                    }
                    else
                    {
                        throw new Exception("Wallet number doesn't updated yet , please update wallet number first and then add recharge");
                    }
                }
            }
            catch
            {
                throw;
            }
            return xBalace;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCount"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<customer> GetAllCustomers(int maxCount, string searchValue, int customerTypes, int route, bool addAgreement = false, string paymentMode = "", betaskdbEntities contextSource = null)
        {
            List<customer> lstCustomers = new List<customer>();
            try
            {
                betaskdbEntities context = null;
                if (contextSource != null)
                    context = contextSource;
                else
                    context = new betaskdbEntities();

                using (context)
                {

                    var _context = context.customer.AsNoTracking().Include(x => x.route).Include(b => b.building).Where(c => c.customer_type == customerTypes);
                    if (!string.IsNullOrEmpty(paymentMode))
                        _context = _context.Where(x => x.payment_mode == paymentMode);
                    if (!string.IsNullOrEmpty(searchValue))
                        _context = _context.Where(u => u.customer_name.Contains(searchValue) || u.phone.Contains(searchValue) || u.mobile.Contains(searchValue) || u.address1.Contains(searchValue) || u.building.building_name.Contains(searchValue) || u.wallet_number.Contains(searchValue) || u.app_customer_name.Contains(searchValue) || u.app_phone.Contains(searchValue));
                    if (maxCount > 0)
                        lstCustomers = _context.Take(maxCount).OrderBy(x => x.status).ThenBy(x => x.customer_name).ToList();
                    else
                        lstCustomers = _context.ToList();
                    if (route > 0)
                    {
                        List<route_group> listGroupRoute = context.route_group.AsNoTracking().Where(x => x.route_id == route && x.Status == 1).ToList();
                        if (listGroupRoute == null || listGroupRoute.Count == 0)
                            lstCustomers = lstCustomers.Where(x => x.route_id == route).ToList();
                        else
                        {
                            List<int> routeIds = listGroupRoute.Select(x => x.sub_route_id).Distinct().ToList();
                            lstCustomers = lstCustomers.Where(x => x.route_id != null && routeIds.Contains(Convert.ToInt32(x.route_id))).ToList();
                        }
                    }
                    if (paymentMode != string.Empty)
                    {
                        lstCustomers = lstCustomers.Where(x => x.payment_mode == paymentMode).ToList();
                    }
                    if (addAgreement)
                    {
                        foreach (customer cs in lstCustomers)
                        {

                            List<customer_aggrement> listAgreement = cs.customer_aggrement.ToList();

                            string agreement = "";
                            if (listAgreement != null)
                            {
                                string itemName = "";
                                foreach (customer_aggrement ag in listAgreement)
                                {
                                    if (ag != null)
                                    {
                                        decimal closinStock = CustomerStockBalanceInPerformance(ag.customer_id, ag.item_id, context).Closing;
                                        itemName = context.item.AsNoTracking().Where(i => i.item_id == ag.item_id).FirstOrDefault().item_name;
                                        agreement += agreement != "" ? " , " : agreement;
                                        agreement += $"{itemName} - {ag.max_qty} @{ag.unit_price}";
                                        agreement += $", Closing stock={closinStock}";
                                    }
                                }
                            }

                            cs.remarks = agreement;

                        }
                    }
                    lstCustomers = lstCustomers.OrderBy(x => x.route_id).ThenBy(x => x.customer_name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstCustomers;
        }

        public DataTable GetCustomerListByRoute(int routeId)
        {
            DataTable tblCustomer = new DataTable();
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetCustomerDetailsByRoute", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@routeId", routeId);
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblCustomer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tblCustomer;
        }

        public DataTable GetCustomerListRouteWise(int routeId, int employeeId, bool isActiveCustomers, bool isDatewise, DateTime dateFrom, DateTime dateTo, string paymentMode = "")
        {
            DataTable tblCustomer = new DataTable();
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetCustomerList", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@isSearchWithDate", isDatewise); // Set based on your logic
                            cmd.Parameters.AddWithValue("@isActiveCustomers", isActiveCustomers);
                            cmd.Parameters.AddWithValue("@createdBy", employeeId);
                            cmd.Parameters.AddWithValue("@routeId", routeId);
                            if (string.IsNullOrEmpty(paymentMode))
                            {
                                cmd.Parameters.AddWithValue("@paymentMode", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@paymentMode", paymentMode);
                            }
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@dateTo", dateTo.ToString("yyyy-MM-dd"));
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblCustomer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tblCustomer;
        }


        public List<customer> GetAllCustomers(int maxCount, string searchValue, int customerTypes, int route, int employeeId, bool addAgreement = false, bool addClosingStock = false, string paymentMode = "", betaskdbEntities contextSource = null)
        {
            List<customer> lstCustomers = new List<customer>();
            try
            {
                betaskdbEntities context = null;
                if (contextSource != null)
                    context = contextSource;
                else
                    context = new betaskdbEntities();

                using (context)
                {

                    var _context = context.customer.AsNoTracking().Include(x => x.route).Include(b => b.building).Include(b => b.building).Where(c => c.customer_type == customerTypes && (employeeId > 0 ? c.employee_id == employeeId : (c.employee_id == null || c.employee_id >= 0)) && (route > 0 ? c.route_id == route : route >= 0));
                    if (paymentMode != "")
                        _context = _context.Where(x => x.payment_mode == paymentMode);
                    if (!string.IsNullOrEmpty(searchValue))
                        _context = _context.Where(u => u.customer_name.Contains(searchValue) || u.phone.Contains(searchValue) || u.mobile.Contains(searchValue) || u.address1.Contains(searchValue) || u.building.building_name.Contains(searchValue) || u.wallet_number.Contains(searchValue));
                    if (maxCount > 0)
                        lstCustomers = _context.Take(maxCount).OrderBy(x => x.status).ThenBy(x => x.customer_name).ToList();
                    else
                        lstCustomers = _context.ToList();
                    if (route > 0)
                    {
                        List<route_group> listGroupRoute = context.route_group.AsNoTracking().Where(x => x.route_id == route && x.Status == 1).ToList();
                        if (listGroupRoute == null || listGroupRoute.Count == 0)
                            lstCustomers = lstCustomers.Where(x => x.route_id == route).ToList();
                        else
                        {
                            List<int> routeIds = listGroupRoute.Select(x => x.sub_route_id).Distinct().ToList();
                            lstCustomers = lstCustomers.Where(x => x.route_id != null && routeIds.Contains(Convert.ToInt32(x.route_id))).ToList();
                        }
                    }
                    if (paymentMode != string.Empty)
                    {
                        lstCustomers = lstCustomers.Where(x => x.payment_mode == paymentMode).ToList();
                    }
                    if (addAgreement)
                    {
                        foreach (customer cs in lstCustomers)
                        {

                            List<customer_aggrement> listAgreement = cs.customer_aggrement.ToList();

                            string agreement = "";
                            if (listAgreement != null)
                            {
                                string itemName = "";
                                foreach (customer_aggrement ag in listAgreement)
                                {
                                    if (ag != null)
                                    {
                                        decimal closinStock = 0;//CustomerStockBalanceInPerformance(ag.customer_id, ag.item_id, context).Closing;
                                        if (addClosingStock)
                                            closinStock = CustomerStockBalanceInPerformance(ag.customer_id, ag.item_id, context).Closing;

                                        itemName = context.item.AsNoTracking().Where(i => i.item_id == ag.item_id).FirstOrDefault().item_name;
                                        agreement += agreement != "" ? " , " : agreement;
                                        agreement += $"{itemName} - {ag.max_qty} @{ag.unit_price}";
                                        agreement += addClosingStock ? $", Closing stock={closinStock}" : "";
                                    }
                                }
                            }

                            cs.remarks = agreement;

                        }
                    }
                    lstCustomers = lstCustomers.OrderBy(x => x.route_id).ThenBy(x => x.customer_name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstCustomers;
        }


        public List<customer> GetAllcustomerBySalesmanLedger(int saleLedger)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    // return context.customer.AsNoTracking().Include(l => l.account_ledger).Where(x => x.salesman_ledger == saleLedger).ToList();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<customer> GetALLCustomerWithInPeriod(DateTime dateFrom, DateTime dateTo, int maxCount, string searchValue, int customerTypes, int route, int employeeId, bool addAgreement = false, string paymentMode = "")
        {
            List<customer> lstCustomers = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    var _context = context.customer.Include(x => x.route).Include(b => b.building).Where(c => c.customer_type == customerTypes && (c.added_time >= dateFrom && c.added_time <= dateTo) && (employeeId > 0 ? c.employee_id == employeeId : (c.employee_id == null || c.employee_id >= 0)));
                    if (!string.IsNullOrEmpty(searchValue))
                        _context = _context.Where(u => u.customer_name.Contains(searchValue) || u.phone.Contains(searchValue) || u.mobile.Contains(searchValue));
                    if (maxCount > 0)
                        lstCustomers = _context.Take(maxCount).OrderBy(x => x.status).ThenBy(x => x.customer_name).ToList();
                    else
                        lstCustomers = _context.ToList();
                    if (route > 0)
                    {
                        lstCustomers = lstCustomers.Where(x => x.route_id == route).ToList();
                    }
                    if (paymentMode != string.Empty)
                    {
                        lstCustomers = lstCustomers.Where(x => x.payment_mode == paymentMode).ToList();
                    }
                    if (addAgreement)
                    {
                        foreach (customer cs in lstCustomers)
                        {

                            List<customer_aggrement> listAgreement = cs.customer_aggrement.ToList();

                            string agreement = "";
                            if (listAgreement != null)
                            {
                                string itemName = "";
                                foreach (customer_aggrement ag in listAgreement)
                                {
                                    if (ag != null)
                                    {
                                        decimal closinStock = CustomerStockBalanceInPerformance(ag.customer_id, ag.item_id, context).Closing;
                                        itemName = context.item.AsNoTracking().Where(i => i.item_id == ag.item_id).FirstOrDefault().item_name;
                                        agreement += agreement != "" ? " , " : agreement;
                                        agreement += $"{itemName} - {ag.max_qty} @{ag.unit_price}";
                                        agreement += $", Closing stock={closinStock}";
                                    }
                                }
                            }

                            cs.remarks = agreement;

                        }
                    }
                    lstCustomers = lstCustomers.OrderBy(x => x.route_id).ThenBy(x => x.added_time).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstCustomers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public customer GetCustomerDetails(int customerId)
        {
            customer objCustomer = new customer();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objCustomer = context.customer.Find(customerId);
                    if (objCustomer.group_id > 0)
                    {
                        objCustomer.wallet_balance = context.customer.Find(objCustomer.group_id).wallet_balance;
                    }
                }
            }
            catch
            {
                throw;
            }
            return objCustomer;

        }
        public customer GetCustomerDetailsByLedger(int ledgerId)
        {
            customer objCustomer = new customer();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objCustomer = context.customer.Where(c => c.ledger_id == ledgerId).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return objCustomer;

        }
        public customer GetCustomerDetailsByName(string customerName)
        {
            customer objCustomer = new customer();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objCustomer = context.customer.Where(c => c.customer_name == customerName).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return objCustomer;

        }
        public customer GetCustomerDetails(int customerId, betaskdbEntities context)
        {
            customer objCustomer = new customer();
            try
            {
                // using (var context = new betaskdbEntities())
                {
                    objCustomer = context.customer.Where(c => c.customer_id == customerId).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return objCustomer;

        }

        public DataTable GetCustomerPerfomance(DateTime dateFrom, DateTime dateTo, int transFrom, int transTo, int routeId, int employeeId)
        {
            try
            {
                DataTable tblReport = new DataTable();

                using (DbContext context = new betaskdbEntities())
                {

                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetCustomerPerformance", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters with values
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@dateTo", dateTo);
                            cmd.Parameters.AddWithValue("@routeId", routeId);
                            cmd.Parameters.AddWithValue("@countFrom", transFrom);
                            cmd.Parameters.AddWithValue("@countTo", transTo);
                            cmd.Parameters.AddWithValue("@empId", employeeId);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(tblReport);
                            }
                        }
                    }
                }
                return tblReport;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting outstanding {ex.Message}");
            }
            return null;
        }
        public List<Model.CustomerPerformanceModel> GetCustomerPerfomanceOL(DateTime dateFrom, DateTime dateTo, int transFrom, int transTo, int routeId)
        {
            List<Model.CustomerPerformanceModel> listCustomers = new List<Model.CustomerPerformanceModel>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<customer> _customers = context.customer.AsNoTracking().Include(a => a.customer_aggrement).AsNoTracking().Include(r => r.route).Where(x => x.status == 1 && (routeId > 0 ? x.route_id == routeId : x.route_id > 0)).OrderBy(x => x.route_id).ThenBy(x => x.customer_id).ToList();// context.sales.AsNoTracking().Include(c => c.customer).AsNoTracking().Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.customer.route_id == routeId).ToList().Select(x => x.customer).Distinct().ToList();
                    foreach (customer cs in _customers)
                    {
                        //if (cs.customer_name.Contains("UDHEESH 105"))
                        //{
                        //    string ss = "";
                        //}
                        List<customer_aggrement> listAgreement = cs.customer_aggrement.ToList();
                        int trans = context.sales.AsNoTracking().Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.customer_id == cs.customer_id).Count();
                        var lastTran = context.sales.AsNoTracking().Where(x => x.customer_id == cs.customer_id).OrderByDescending(x => x.sales_id).FirstOrDefault();
                        if (trans >= transFrom && trans <= transTo)
                        {
                            string agreement = "";
                            if (listAgreement != null)
                            {
                                string itemName = "";
                                foreach (customer_aggrement ag in listAgreement)
                                {
                                    if (ag != null)
                                    {
                                        decimal closinStock = CustomerStockBalanceInPerformance(ag.customer_id, ag.item_id, context).Closing;
                                        itemName = context.item.AsNoTracking().Where(i => i.item_id == ag.item_id).FirstOrDefault().item_name;
                                        agreement += agreement != "" ? " , " : agreement;
                                        agreement += $"{itemName} - {ag.max_qty} @{ag.unit_price}";
                                        agreement += $", Closing stock={closinStock}";
                                    }
                                }
                            }
                            listCustomers.Add(new Model.CustomerPerformanceModel
                            {
                                CustomerName = $"{cs.customer_name}",
                                Mobile = cs.phone,
                                Route = cs.route.route_name,
                                Transactions = trans,
                                LastTransaction = lastTran != null ? lastTran.sales_date.ToString("dd/MM/yyyy") : "",
                                Agreement = agreement,
                                WalletBalance = Convert.ToDecimal(cs.wallet_balance),
                                Paymentmode = cs.payment_mode
                            });
                        }
                    }
                    listCustomers = listCustomers.OrderBy(x => x.Route).ThenBy(x => x.Transactions).ToList();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomers;
        }

        public List<Model.CustomerOutstandingvsWallet> GetCustomerOutstandingvsWallet(int routeId, int customerId, DateTime date)
        {
            AccountTransactionDAL acc = new AccountTransactionDAL();
            List<Model.CustomerOutstandingvsWallet> customerList = new List<Model.CustomerOutstandingvsWallet>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<customer> listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => x.status == 1).OrderBy(x => x.customer_name).ToList();
                    if (routeId > 0)
                    {
                        listCustomer = listCustomer.Where(x => x.route_id == routeId).OrderBy(x => x.customer_name).ToList();
                    }
                    if (customerId > 0)
                    {
                        listCustomer = listCustomer.Where(x => x.customer_id == customerId).OrderBy(x => x.customer_name).ToList();
                    }
                    foreach (customer cs in listCustomer)
                    {
                        int ledgerId = Convert.ToInt32(cs.ledger_id);
                        decimal walletBalance = 0;
                        if (cs.wallet_balance != null) walletBalance = Convert.ToDecimal(cs.wallet_balance);

                        decimal outstanding = acc.CustomerOutstanding(ledgerId, date, context);
                        customerList.Add(new Model.CustomerOutstandingvsWallet
                        {
                            customerId = cs.customer_id,
                            CustomerName = cs.customer_name,
                            Outstanding = outstanding,
                            Route = cs.route.route_name,
                            WalletBalance = walletBalance,
                            WalletNumber = cs.wallet_number,

                        });

                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return customerList;
        }
        public List<customer> GetWalletCustomer(int routeId, string walletNumber, bool onlyBelowZero = false, bool noWallet = false)
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    if (!noWallet)
                    {
                        if (walletNumber != "")
                        {
                            listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => x.status == 1 && x.wallet_number.Contains(walletNumber)).OrderBy(x => x.wallet_number).ToList();
                        }
                        else
                        {
                            listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => x.status == 1 && (routeId > 0 ? x.route_id == routeId : x.route_id > 0) && x.wallet_number.Length > 0 && (onlyBelowZero ? x.wallet_balance < 0 : (x.wallet_balance <= 0 || x.wallet_balance > 0))).OrderBy(x => x.wallet_number).ToList();
                            var ids = listCustomer.Select(x => x.customer_id).ToList();
                            var customers = context.customer.Where(x => ids.Contains(x.customer_id)).Select(x=>x.app_phone).ToList() ;
                        }
                    }
                    else
                    {
                        listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => x.status == 1 && (routeId > 0 ? x.route_id == routeId : x.route_id > 0) && x.wallet_number.Length == 0 && (x.wallet_balance < 0 || x.wallet_balance > 0)).OrderBy(x => x.wallet_number).ToList();
                    }

                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        List<int> ledgerIds = listCustomer.Where(x => x.ledger_id.HasValue).Select(x => x.ledger_id.Value).Distinct().ToList();
                        var outstatndingList = GetAccountTransactionTotals(ledgerIds, connection);
                        foreach (var cm in listCustomer)
                        {
                            var outCustomer = outstatndingList.FirstOrDefault(x => x.LedgerId == Convert.ToInt32(cm.ledger_id));
                            decimal outstanding = outCustomer != null ? (outCustomer.DebitTotal = outCustomer.CreditTotal) : 0;
                            cm.remarks = (outstanding * -1).ToString();
                        }
                    }


                    //                 foreach (customer cs in listCustomer)
                    //             {
                    //                 //List<account_transaction> listTransactions = context.account_transaction.AsNoTracking().Where(x => x.ledger_id == cs.ledger_id && x.status == 1).ToList();
                    //                 //decimal debit = listTransactions.Select(x => x.debit).DefaultIfEmpty(0).Sum();
                    //                 //decimal credit = listTransactions.Select(x => x.credit).DefaultIfEmpty(0).Sum();
                    //                 var result = context.account_transaction
                    //.Where(t => t.ledger_id == cs.ledger_id && t.status==1)
                    //.GroupBy(t => true) // Grouping by a constant to get a single group
                    //.Select(g => new
                    //{
                    //    DebitTotal = g.Sum(t => t.debit),
                    //    CreditTotal = g.Sum(t => t.credit)
                    //})
                    //.FirstOrDefault();
                    //                 decimal outstanding = (result?.DebitTotal ?? 0) - (result?.CreditTotal ?? 0);
                    //                 cs.remarks = (outstanding * -1).ToString();
                    //             }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }


        public static List<AccountTransactionTotalModel> GetAccountTransactionTotals(List<int> ledgerIds, SqlConnection connection)
        {


            using (SqlCommand command = new SqlCommand("SP_GetAccountTransactionTotals", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Create TVP
                DataTable ledgerIdTable = new DataTable();
                ledgerIdTable.Columns.Add("LedgerId", typeof(int));
                foreach (int ledgerId in ledgerIds)
                {
                    ledgerIdTable.Rows.Add(ledgerId);
                }

                SqlParameter tvpParam = command.Parameters.AddWithValue("@LedgerIds", ledgerIdTable);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "CustomerLedgerIdList";

                connection.Open();

                List<AccountTransactionTotalModel> results = new List<AccountTransactionTotalModel>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new AccountTransactionTotalModel
                        {
                            LedgerId = reader.GetInt32(reader.GetOrdinal("ledger_id")),
                            DebitTotal = reader.GetDecimal(reader.GetOrdinal("DebitTotal")),
                            CreditTotal = reader.GetDecimal(reader.GetOrdinal("CreditTotal"))
                        });
                    }
                }
                return results;
            }

        }

        public List<CustomerMonthlyOutstandingModel> GetCustomerMonthlyOutstanding(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            AccountTransactionDAL account = new AccountTransactionDAL();
            List<CustomerMonthlyOutstandingModel> listCustomerOutstanding = new List<CustomerMonthlyOutstandingModel>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<customer> listCustomer = context.customer.AsNoTracking().Where(x => x.route_id == routeId && x.status == 1).OrderBy(x => x.customer_name).ToList();
                    if (listCustomer != null)
                    {
                        foreach (customer cs in listCustomer)
                        {
                            int ledgerId = Convert.ToInt32(cs.ledger_id);
                            decimal ob = account.CustomerOutstanding(ledgerId, dateFrom.AddDays(-1), context);
                            decimal debit = 0, credit = 0;

                            List<account_transaction> listAccountTransaction = context.account_transaction.AsNoTracking().Where(x => x.ledger_id == ledgerId && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.status == 1).ToList();
                            if (listAccountTransaction != null && listAccountTransaction.Count > 0)
                            {
                                debit = listAccountTransaction.Sum(x => x.debit);
                                credit = listAccountTransaction.Sum(x => x.credit);
                            }
                            decimal cb = ob + (debit - credit);
                            listCustomerOutstanding.Add(new CustomerMonthlyOutstandingModel
                            {
                                CustomerId = cs.customer_id,
                                CustomerName = cs.customer_name,
                                OB = ob,
                                Debit = debit,
                                Credit = credit,
                                CB = cb
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listCustomerOutstanding;
        }
        public List<Model.CustomerAgreementBalanceModel> CustomerStockBalance(int routeId, int customerId, int itemId, string itemName)
        {
            List<Model.CustomerAgreementBalanceModel> listCustomer = new List<Model.CustomerAgreementBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    int defaultItemId = context.system_settings.Where(x => x.status == 1).FirstOrDefault().default_item_id;
                    List<customer> customers = new List<customer>();
                    if (customerId > 0)
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();
                    else
                    {
                        if (routeId > 0)
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.route_id == routeId && x.customer_type == 1).OrderBy(x => x.customer_name).ToList();
                        else
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.status == 1 && x.customer_type == 1).OrderBy(x => x.route_id).ThenBy(x => x.customer_name).ToList();
                    }
                    if (customers != null)
                    {

                        foreach (customer cs in customers)
                        {

                            List<delivery_return> xReturn = new List<delivery_return>();
                            List<sales> xSales = context.sales.AsNoTracking().Where(x => x.customer_id == cs.customer_id).ToList();

                            DateTime minDate = (xSales != null && xSales.Count > 0) ? xSales.Min(x => x.sales_date) : DateTime.Now;

                            if (xSales == null)
                            {
                                xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (xReturn != null && xReturn.Count > 0)
                                    minDate = xReturn.Min(x => x.return_date);

                            }
                            else
                            {
                                xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (xReturn != null && xReturn.Count > 0)
                                {
                                    int dRes = DateTime.Compare(minDate, xReturn.Min(x => x.return_date));
                                    if (dRes > 0)
                                        minDate = xReturn.Min(x => x.return_date);
                                }
                            }

                            decimal agreement = 0;
                            List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                            if (_agreement != null)
                                agreement = _agreement.Sum(x => x.max_qty);


                            decimal delivered = 0;
                            List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                            if (_delivery != null)
                                delivered = _delivery.Sum(x => x.delivered_qty);




                            decimal returned = 0;
                            List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                            if (_returned != null)
                                returned = _returned.Sum(x => x.qty);

                            decimal closing = agreement + (delivered - returned);

                            if (delivered <= agreement && returned == 0)
                                closing = agreement;

                            //List<Model.CustomerAgreementBalanceModel> listDetailed = CustomerStockBalanceDetailed(cs.customer_id, itemId, itemName, minDate, DateTime.Now, context);



                            decimal _closing = closing;

                            if (_returned.Count == 0 && _delivery.Count == 1)
                                _closing = closing = agreement;

                            else
                            {
                                try
                                {
                                    if (defaultItemId == 0 || defaultItemId == itemId)
                                    {
                                        var firstDelivery = context.delivery_items.Include(d => d.delivery).AsNoTracking().Where(x => x.delivery_time != null && x.customer_id == cs.customer_id && x.item_id == itemId).FirstOrDefault();

                                        if (firstDelivery != null)
                                        {
                                            decimal xQty = firstDelivery.qty;
                                            DateTime firstDate = firstDelivery.delivery.delivery_date;
                                            decimal excReteurn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.return_date == firstDate && x.status == 4).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                                            if (excReteurn == 0)
                                                _closing = closing - xQty;
                                            else
                                                _closing = closing;
                                        }
                                    }

                                }
                                catch { }
                            }

                            if (_agreement != null && _agreement.Count >= 0)
                            {
                                if (agreement > 0 /*&& _closing >= 0*/)
                                {
                                    listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                    {
                                        customerId = cs.customer_id,
                                        CustomerName = cs.customer_name,
                                        Route = cs.route != null ? cs.route.route_name : "",
                                        ItemId = itemId,
                                        ItemName = itemName,
                                        Opening = agreement,
                                        Agreement = agreement,
                                        Delivered = delivered,
                                        Returned = returned,
                                        Closing = _closing
                                    });
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }

        public List<Model.CustomerAgreementBalanceModel> CustomerStockBalanceDateEnd(int routeId, int customerId, int itemId, string itemName, DateTime endDate)
        {
            List<Model.CustomerAgreementBalanceModel> listCustomer = new List<Model.CustomerAgreementBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    int defaultItemId = context.system_settings.Where(x => x.status == 1).FirstOrDefault().default_item_id;
                    List<customer> customers = new List<customer>();
                    if (customerId > 0)
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();
                    else
                    {

                        if (routeId > 0)
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.route_id == routeId && x.customer_type == 1 && (x.added_time == null || x.added_time <= endDate)).OrderBy(x => x.customer_name).ToList();
                        else
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.status == 1 && x.customer_type == 1 && (x.added_time == null || x.added_time <= endDate)).OrderBy(x => x.route_id).ThenBy(x => x.customer_name).ToList();
                    }
                    if (customers != null)
                    {
                        CompanyDAL companyDAL = new CompanyDAL();
                        DateTime minDate = companyDAL.GetSoftwareStartDate();
                        foreach (customer cs in customers)
                        {
                            if (cs.customer_id == 4359)
                            {
                                string ss = "";
                            }
                            List<delivery_return> xReturn = new List<delivery_return>();
                            List<sales> xSales = context.sales.AsNoTracking().Where(x => x.customer_id == cs.customer_id).ToList();

                            //  DateTime minDate = (xSales != null && xSales.Count > 0) ? xSales.Min(x => x.sales_date) : DateTime.Now;

                            /*
                            if (xSales == null)
                            {
                                xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (xReturn != null && xReturn.Count > 0)
                                    minDate = xReturn.Min(x => x.return_date);

                            }
                            else
                            {
                                xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (xReturn != null && xReturn.Count > 0)
                                {
                                    int dRes = DateTime.Compare(minDate, xReturn.Min(x => x.return_date));
                                    if (dRes > 0)
                                        minDate = xReturn.Min(x => x.return_date);
                                }
                            }*/

                            decimal agreement = 0;
                            List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                            if (_agreement != null)
                                agreement = _agreement.Sum(x => x.max_qty);


                            decimal delivered = 0;
                            List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time <= endDate && x.status == 4).ToList();
                            if (_delivery != null)
                                delivered = _delivery.Sum(x => x.delivered_qty);




                            decimal returned = 0;
                            List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date <= endDate).ToList();
                            if (_returned != null)
                                returned = _returned.Sum(x => x.qty);

                            decimal closing = agreement + (delivered - returned);

                            if (delivered <= agreement && returned == 0)
                                closing = agreement;

                            //List<Model.CustomerAgreementBalanceModel> listDetailed = CustomerStockBalanceDetailed(cs.customer_id, itemId, itemName, minDate, DateTime.Now, context);



                            decimal _closing = closing;

                            if (_returned.Count == 0 && _delivery.Count == 1)
                                _closing = closing = agreement;

                            else
                            {
                                try
                                {
                                    if (defaultItemId == 0 || defaultItemId == itemId)
                                    {
                                        var firstDelivery = context.delivery_items.Include(d => d.delivery).AsNoTracking().Where(x => x.delivery_time != null && x.customer_id == cs.customer_id && x.item_id == itemId).FirstOrDefault();

                                        if (firstDelivery != null)
                                        {
                                            decimal xQty = firstDelivery.qty;
                                            DateTime firstDate = firstDelivery.delivery.delivery_date;
                                            decimal excReteurn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.return_date == firstDate && x.status == 4).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                                            if (excReteurn == 0 && cs.added_time != null)
                                            {
                                                if (firstDate.Date == cs.added_time.Value.Date)
                                                {
                                                    _closing = (closing) - xQty;
                                                    _closing += xQty;
                                                }
                                            }
                                            else
                                                _closing = closing;
                                        }
                                    }

                                }
                                catch { }
                            }

                            if (_agreement != null && _agreement.Count >= 0)
                            {
                                if (agreement > 0 /*&& _closing >= 0*/)
                                {
                                    listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                    {
                                        customerId = cs.customer_id,
                                        CustomerName = cs.customer_name,
                                        Route = cs.route != null ? cs.route.route_name : "",
                                        ItemId = itemId,
                                        ItemName = itemName,
                                        Opening = agreement,
                                        Agreement = agreement,
                                        Delivered = delivered,
                                        Returned = returned,
                                        Closing = _closing
                                    });
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }

        /// <summary>
        /// Only for customer performance report same as CustomerStockBalance 
        /// If any changes in CustomerStockBalance function should be updated here
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="customerId"></param>
        /// <param name="itemId"></param>
        /// <param name="itemName"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<Model.CustomerAgreementBalanceModel> CustomerStockBalance(int routeId, int customerId, int itemId, string itemName, betaskdbEntities context)
        {
            List<Model.CustomerAgreementBalanceModel> listCustomer = new List<Model.CustomerAgreementBalanceModel> { };
            try
            {
                int defaultItemId = context.system_settings.Where(x => x.status == 1).FirstOrDefault().default_item_id;
                //using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<customer> customers = new List<customer>();
                    if (customerId > 0)
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();
                    else
                    {
                        if (routeId > 0)
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.route_id == routeId && x.customer_type == 1).OrderBy(x => x.customer_name).ToList();
                        else
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.status == 1 && x.customer_type == 1).OrderBy(x => x.route_id).ThenBy(x => x.customer_name).ToList();
                    }
                    if (customers != null)
                    {

                        foreach (customer cs in customers)
                        {

                            List<delivery_return> xReturn = new List<delivery_return>();
                            List<sales> xSales = context.sales.AsNoTracking().Where(x => x.customer_id == cs.customer_id).ToList();

                            DateTime minDate = (xSales != null && xSales.Count > 0) ? xSales.Min(x => x.sales_date) : DateTime.Now;

                            if (xSales == null)
                            {
                                xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (xReturn != null && xReturn.Count > 0)
                                    minDate = xReturn.Min(x => x.return_date);

                            }
                            else
                            {
                                xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (xReturn != null && xReturn.Count > 0)
                                {
                                    int dRes = DateTime.Compare(minDate, xReturn.Min(x => x.return_date));
                                    if (dRes > 0)
                                        minDate = xReturn.Min(x => x.return_date);
                                }
                            }

                            decimal agreement = 0;
                            List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                            if (_agreement != null)
                                agreement = _agreement.Sum(x => x.max_qty);


                            decimal delivered = 0;
                            List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                            if (_delivery != null)
                                delivered = _delivery.Sum(x => x.delivered_qty);




                            decimal returned = 0;
                            List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                            if (_returned != null)
                                returned = _returned.Sum(x => x.qty);

                            decimal closing = agreement + (delivered - returned);

                            if (delivered <= agreement && returned == 0)
                                closing = agreement;

                            //List<Model.CustomerAgreementBalanceModel> listDetailed = CustomerStockBalanceDetailed(cs.customer_id, itemId, itemName, minDate, DateTime.Now, context);



                            decimal _closing = closing;

                            if (_returned.Count == 0 && _delivery.Count == 1)
                                _closing = closing = agreement;

                            else
                            {
                                try
                                {
                                    var firstDelivery = context.delivery_items.Include(d => d.delivery).AsNoTracking().Where(x => x.delivery_time != null && x.customer_id == cs.customer_id && x.item_id == itemId).FirstOrDefault();
                                    if (defaultItemId == 0 || defaultItemId == itemId)
                                    {
                                        if (firstDelivery != null)
                                        {
                                            decimal xQty = firstDelivery.qty;
                                            DateTime firstDate = firstDelivery.delivery.delivery_date;
                                            decimal excReteurn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.return_date == firstDate && x.status == 4).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                                            if (excReteurn == 0)
                                                _closing = closing - xQty;
                                            else
                                                _closing = closing;
                                        }
                                    }

                                }
                                catch { }
                            }

                            if (_agreement != null && _agreement.Count >= 0)
                            {
                                if (agreement > 0 /*&& _closing >= 0*/)
                                {
                                    listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                    {
                                        customerId = cs.customer_id,
                                        CustomerName = cs.customer_name,
                                        Route = cs.route != null ? cs.route.route_name : "",
                                        ItemId = itemId,
                                        ItemName = itemName,
                                        Opening = agreement,
                                        Agreement = agreement,
                                        Delivered = delivered,
                                        Returned = returned,
                                        Closing = _closing
                                    });
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }
        public Model.CustomerAgreementBalanceModel CustomerStockBalanceInPerformance(int customerId, int itemId, betaskdbEntities context)
        {
            Model.CustomerAgreementBalanceModel listCustomer = new Model.CustomerAgreementBalanceModel { };
            try
            {
                //    using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<customer> customers = new List<customer>();

                    {
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();
                        if (customers != null)
                        {
                            foreach (customer cs in customers)
                            {
                                decimal agreement = 0;
                                List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                                if (_agreement != null)
                                    agreement = _agreement.Sum(x => x.max_qty);

                                List<delivery_return> xReturn = new List<delivery_return>();
                                List<sales> xSales = context.sales.AsNoTracking().Where(x => x.customer_id == cs.customer_id).ToList();

                                DateTime minDate = (xSales != null && xSales.Count > 0) ? xSales.Min(x => x.sales_date) : DateTime.Now;

                                if (xSales == null)
                                {
                                    xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.status == 4).ToList();
                                    if (xReturn != null && xReturn.Count > 0)
                                        minDate = xReturn.Min(x => x.return_date);

                                }
                                else
                                {
                                    xReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.status == 4).ToList();
                                    if (xReturn != null && xReturn.Count > 0)
                                    {
                                        int dRes = DateTime.Compare(minDate, xReturn.Min(x => x.return_date));
                                        if (dRes > 0)
                                            minDate = xReturn.Min(x => x.return_date);
                                    }
                                }

                                decimal delivered = 0;
                                List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                                if (_delivery != null)
                                    delivered = _delivery.Sum(x => x.delivered_qty);




                                decimal returned = 0;
                                List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4).ToList();
                                if (_returned != null)
                                    returned = _returned.Sum(x => x.qty);

                                decimal closing = agreement + (delivered - returned);

                                if (delivered <= agreement && returned == 0)
                                    closing = agreement;

                                // List<Model.CustomerAgreementBalanceModel> listDetailed = CustomerStockBalanceDetailed(cs.customer_id, itemId, "", minDate, DateTime.Now, context);
                                List<Model.CustomerAgreementBalanceModel> listDetailed = CustomerStockBalance(0, cs.customer_id, itemId, "", context);

                                decimal _closing = 0;
                                if (listDetailed != null && listDetailed.Count > 0)
                                    _closing = listDetailed.Last().Closing;


                                listCustomer = new Model.CustomerAgreementBalanceModel
                                {
                                    customerId = cs.customer_id,
                                    CustomerName = cs.customer_name,
                                    Route = cs.route != null ? cs.route.route_name : "",
                                    ItemId = itemId,
                                    //ItemName = itemName,
                                    Opening = agreement,
                                    Agreement = agreement,
                                    Delivered = delivered,
                                    Returned = returned,
                                    Closing = _closing
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }
        public List<Model.CustomerAgreementBalanceModel> CustomerStockBalance(int routeId, int customerId, int itemId, string itemName, DateTime dateFrom, DateTime dateTo)
        {
            List<Model.CustomerAgreementBalanceModel> listCustomer = new List<Model.CustomerAgreementBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<customer> customers = new List<customer>();
                    if (customerId > 0)
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();
                    else
                    {
                        if (routeId > 0)
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.route_id == routeId && x.customer_type == 1).OrderBy(x => x.customer_name).ToList();
                        else
                            customers = context.customer.Include(r => r.route).AsNoTracking().Where(x => x.status == 1 && x.customer_type == 1).OrderBy(x => x.route_id).ThenBy(x => x.customer_name).ToList();
                    }
                    if (customers != null)
                    {
                        foreach (customer cs in customers)
                        {
                            decimal agreement = 0;
                            List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();
                            if (_agreement != null)
                                agreement = _agreement.Sum(x => x.max_qty);



                            decimal delivered = 0, xDelivered = 0;
                            List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time >= dateFrom && x.delivery_time <= dateTo).ToList();
                            if (_delivery != null)
                                delivered = _delivery.Sum(x => x.delivered_qty);

                            List<delivery_items> _xDelivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time < dateFrom).ToList();
                            if (_xDelivery != null)
                                xDelivered = _xDelivery.Sum(x => x.delivered_qty);


                            decimal returned = 0, xReturned = 0;
                            List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date >= dateFrom && x.return_date <= dateTo).ToList();
                            if (_returned != null)
                                returned = _returned.Sum(x => x.qty);

                            List<delivery_return> _xReturned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date < dateFrom).ToList();
                            if (_xReturned != null)
                                xReturned = _xReturned.Sum(x => x.qty);




                            decimal opening = agreement + (xDelivered - xReturned);

                            if (_xDelivery != null && _xDelivery.Count == 1 && xReturned == 0)
                                opening = agreement;
                            else
                            {
                                try
                                {
                                    var firstDelivery = context.delivery_items.Include(d => d.delivery).AsNoTracking().Where(x => x.delivery_time != null && x.customer_id == cs.customer_id && x.item_id == itemId).FirstOrDefault();
                                    int defaultItemId = context.system_settings.FirstOrDefault(x => x.status == 1).default_item_id;
                                    if (firstDelivery != null && itemId == defaultItemId)
                                    {
                                        decimal xQty = firstDelivery.qty;
                                        DateTime firstDate = firstDelivery.delivery.delivery_date;
                                        decimal excReteurn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.return_date == firstDate && x.status == 4).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                                        if (excReteurn == 0)
                                            opening = opening - xQty;
                                    }

                                }
                                catch { }

                            }

                            decimal closing = opening + (delivered - returned);

                            decimal _closing = 0;

                            //First Delivery
                            if (_xDelivery.Count == 0 && _delivery.Count == 1 && _returned.Count == 0)
                                _closing = closing = agreement;

                            else
                            {
                                List<Model.CustomerAgreementBalanceModel> listDetailed = CustomerStockBalanceDetailed(cs.customer_id, itemId, itemName, dateFrom, dateTo, context, opening);


                                if (listDetailed != null && listDetailed.Count > 0)
                                    _closing = listDetailed.Last().Closing;
                                else
                                    _closing = opening;
                            }
                            if (_agreement != null && _agreement.Count >= 0)
                            {
                                if (agreement > 0 /*&& _closing >= 0*/)
                                {
                                    //Checking starting date if start date greater than ToDate will not be considered
                                    if (delivered == 0 && returned == 0)
                                    {
                                        //List<EDMX.user_log> listUserlog = context.user_log.AsNoTracking().Where(x => x.reference_id == cs.customer_id && (x.module_action == "SaveItem" || x.module_action== "SaveCutomer" || x.module_action== "SaveCustomerAggrement")).ToList();
                                        // if (listUserlog != null && listUserlog.Count > 0)
                                        if (cs.added_time != null)
                                        {
                                            //  DateTime firstDate = Convert.ToDateTime(listUserlog.Min(x => x.server_time));
                                            DateTime firstDate = Convert.ToDateTime(cs.added_time);
                                            if (dateTo < firstDate)
                                                continue;
                                        }
                                    }

                                    listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                    {
                                        customerId = cs.customer_id,
                                        CustomerName = cs.customer_name,
                                        Route = cs.route != null ? cs.route.route_name : "",
                                        ItemId = itemId,
                                        ItemName = itemName,
                                        Opening = opening,
                                        Agreement = agreement,
                                        Delivered = delivered,
                                        Returned = returned,
                                        Closing = _closing
                                    });
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }

        public List<EDMX.customer> CustomerSearch(string name, int routeId, int employeeId, string phone, int id, string address, int active = 1)
        {
            List<EDMX.customer> listCustomerSearch = new List<customer>();
            try
            {


                using (betaskdbEntities context = new betaskdbEntities())
                {
                    if (id > 0)
                        listCustomerSearch = context.customer.Include(r => r.route).Where(x => x.customer_id == id).ToList();
                    else
                    {
                        listCustomerSearch = context.customer.Include(r => r.route).Where(x => x.status == active && (name.Length > 2 ? x.customer_name.ToLower().Contains(name) : x.customer_name.Length > 0)
                          && (routeId > 0 ? x.route_id == routeId : x.route_id > 0)
                          // && (employeeId > 0 ? x.employee_id == employeeId : (x.employeeId >=0))
                          && (phone.Length > 0 ? (x.phone.Contains(phone) || x.mobile.Contains(phone)) : (x.phone == null || x.phone.Length > 0))
                          && (address.Length > 1 ? (x.address1.ToLower().Contains(address) || x.address2.ToLower().Contains(address)) : (x.address1 == null || x.address1 == "" || x.address1.Length > 0))
                          ).OrderBy(x => x.customer_name).Take(200).ToList();
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomerSearch;
        }
        public List<Model.CustomerAgreementBalanceModel> CustomerStockBalanceDetailed(int customerId, int itemId, string itemName, DateTime dateFrom, DateTime dateTo)
        {
            List<Model.CustomerAgreementBalanceModel> listCustomer = new List<Model.CustomerAgreementBalanceModel> { };
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    List<customer> customers = new List<customer>();
                    if (customerId > 0)
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();

                    if (customers != null)
                    {



                        decimal nextOpening = 0;
                        foreach (customer cs in customers)
                        {
                            decimal agreement = 0;
                            List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();

                            if (_agreement != null)
                                agreement = _agreement.Sum(x => x.max_qty);



                            nextOpening = 0;


                            int totalDays = (dateTo - dateFrom).Days + 1;
                            decimal totalDelivered = 0, totalReturned = 0;
                            DateTime date = dateFrom;
                            for (int day = 1; day <= totalDays; day++)
                            {
                                DateTime dt = new DateTime(date.Year, date.Month, date.Day, 00, 00, 00);
                                DateTime dt1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                                decimal delivered = 0;
                                List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time >= dt && x.delivery_time <= dt1).ToList();

                                if (_delivery != null)
                                {
                                    delivered = _delivery.Sum(x => x.delivered_qty);
                                    totalDelivered += delivered;
                                }

                                decimal returned = 0;
                                List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date >= dt && x.return_date <= dt1).ToList();
                                if (_returned != null)
                                {
                                    returned = _returned.Sum(x => x.qty);
                                    totalReturned += returned;
                                }

                                //if (date.Day == 11 && date.Month == 12)
                                //{
                                //    string ss = "";
                                //}

                                if (delivered != 0 || returned != 0)
                                {


                                    #region working
                                    decimal closing = 0;//(nextOpening > 0 || returned <= 0) ? (nextOpening + (delivered - returned)) : (agreement + (delivered - returned));
                                    if (nextOpening > 0 || returned <= 0)
                                    {
                                        closing = (nextOpening + (delivered - returned));
                                    }
                                    else
                                    {
                                        if ((totalDelivered > 0 && totalReturned > 0) && listCustomer.Count > 1)
                                            closing = (delivered - returned);
                                        else if (listCustomer.Count == 0 && delivered > 0 && returned < delivered && returned > 0)
                                            closing = (delivered - returned);
                                        else
                                            closing = (agreement + (delivered - returned));
                                    }
                                    if (nextOpening < 0 && returned > 0)
                                        closing = nextOpening + (delivered - returned);

                                    //if (nextOpening == 0 && closing == agreement && listCustomer.Count > 0&& listCustomer[listCustomer.Count-1].Closing==0 )
                                    //  closing = nextOpening;

                                    listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                    {
                                        customerId = cs.customer_id,
                                        ItemId = itemId,
                                        Agreement = agreement,
                                        ItemName = itemName,
                                        Opening = nextOpening == 0 && returned == 0 ? agreement : nextOpening,
                                        Delivered = delivered,
                                        Returned = returned,
                                        Closing = closing,
                                        Date = date.ToString("dd/MM/yyyy")
                                    });
                                    //nextOpening = nextOpening == 0 ? agreement : closing


                                    nextOpening = closing == 0 ? agreement : closing;
                                    if ((totalDelivered > 0 && totalReturned > 0) && listCustomer.Count > 1)
                                        nextOpening = closing;
                                    //below changed on 08nov2021 as requested by prabeesh,vijay
                                    // nextOpening = closing;
                                }
                                #endregion

                                date = date.AddDays(1);
                            }



                        }
                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }

        //To get actual customer stock in CustomerStockBalance Report 28.Sep.2021
        public List<Model.CustomerAgreementBalanceModel> CustomerStockBalanceDetailed(int customerId, int itemId, string itemName, DateTime dateFrom, DateTime dateTo, betaskdbEntities context, decimal xClosing = 0)
        {
            List<Model.CustomerAgreementBalanceModel> listCustomer = new List<Model.CustomerAgreementBalanceModel> { };
            try
            {
                //  using (betaskdbEntities context = new betaskdbEntities())
                {

                    List<customer> customers = new List<customer>();
                    if (customerId > 0)
                        customers = context.customer.Include(r => r.route).Where(x => x.customer_id == customerId).ToList();

                    if (customers != null)
                    {
                        decimal nextOpening = xClosing;//0;
                        foreach (customer cs in customers)
                        {
                            decimal agreement = 0;
                            List<customer_aggrement> _agreement = context.customer_aggrement.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId).ToList();

                            if (_agreement != null)
                                agreement = _agreement.Sum(x => x.max_qty);

                            int totalDays = (dateTo - dateFrom).Days + 1;
                            DateTime date = dateFrom;

                            DateTime _dt = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 00, 00, 00);
                            DateTime _dt1 = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 59, 59);

                            List<delivery_items> listDelivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time >= _dt && x.delivery_time <= _dt1).ToList();
                            List<delivery_return> listReturn = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date >= _dt && x.return_date <= _dt1).ToList();

                            //  nextOpening = agreement;

                            if (listDelivery != null || listReturn != null)
                            {
                                if (listDelivery.Count > 0 || listReturn.Count > 0)
                                {
                                    for (int day = 1; day <= totalDays; day++)
                                    {
                                        DateTime dt = new DateTime(date.Year, date.Month, date.Day, 00, 00, 00);
                                        DateTime dt1 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                                        decimal delivered = 0;

                                        //List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time >= dt && x.delivery_time <= dt1).ToList();
                                        //if (_delivery != null)
                                        //    delivered = _delivery.Sum(x => x.delivered_qty);

                                        // delivered = (decimal)context.delivery_items.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.delivery_time >= dt && x.delivery_time <= dt1).Select(x=>x.delivered_qty).DefaultIfEmpty(0).Sum();
                                        delivered = (decimal)listDelivery.Where(x => x.delivery_time >= dt && x.delivery_time <= dt1).Select(x => x.delivered_qty).DefaultIfEmpty(0).Sum();
                                        decimal returned = 0;
                                        //List<delivery_return> _returned = context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date >= dt && x.return_date <= dt1).ToList();
                                        //if (_returned != null)
                                        //    returned = _returned.Sum(x => x.qty);

                                        //returned= (decimal)context.delivery_return.AsNoTracking().Where(x => x.customer_id == cs.customer_id && x.item_id == itemId && x.status == 4 && x.return_date >= dt && x.return_date <= dt1).Select(x=>x.qty).DefaultIfEmpty(0).Sum();
                                        returned = (decimal)listReturn.Where(x => x.return_date >= dt && x.return_date <= dt1).Select(x => x.qty).DefaultIfEmpty(0).Sum();

                                        if (delivered != 0 || returned != 0)
                                        {
                                            //decimal closing = (nextOpening > 0 || returned <= 0) ? (nextOpening + (delivered - returned)) : (agreement + (delivered - returned));
                                            decimal closing = (nextOpening + (delivered - returned));
                                            if (nextOpening < 0 && returned > 0)
                                                closing = nextOpening + (delivered - returned);
                                            if (listCustomer.Count == 0 && delivered > 0 && returned < delivered && returned > 0)
                                                closing = (delivered - returned);

                                            //if (nextOpening == 0 && closing == agreement && listCustomer.Count > 0 && listCustomer[listCustomer.Count - 1].Closing == 0)
                                            //    closing = nextOpening;

                                            listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                            {
                                                customerId = cs.customer_id,
                                                ItemId = itemId,
                                                Agreement = agreement,
                                                ItemName = itemName,
                                                //Opening = nextOpening == 0 && returned == 0 ? agreement : nextOpening,
                                                Opening = nextOpening,
                                                Delivered = delivered,
                                                Returned = returned,
                                                Closing = closing,
                                                Date = date.ToString("dd/MM/yyyy")
                                            });



                                            //nextOpening = closing == 0 ? agreement : closing;
                                            //below changed on 08nov2021 as requested by prabeesh,vijay
                                            nextOpening = closing;
                                        }

                                        date = date.AddDays(1);
                                    }
                                }

                            }
                            if (listCustomer.Count == 0)
                            {
                                listCustomer.Add(new Model.CustomerAgreementBalanceModel
                                {
                                    customerId = cs.customer_id,
                                    ItemId = itemId,
                                    Agreement = agreement,
                                    ItemName = itemName,
                                    Opening = agreement,
                                    Delivered = 0,
                                    Returned = 0,
                                    //Closing = xClosing!=0?xClosing: agreement,
                                    Closing = xClosing,
                                    Date = date.ToString("dd/MM/yyyy")
                                });
                            }

                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }
        private decimal GetCustomerStatementOpening(betaskdbEntities context, DateTime dateFrom, int ledgerId)
        {
            var dateFromParameter = new SqlParameter("@dateFrom", dateFrom);
            var ledgerIdParameter = new SqlParameter("@ledgerId", ledgerId);
            var resultParameter = new SqlParameter
            {
                ParameterName = "@result",
                SqlDbType = SqlDbType.Decimal,
                Precision = 18,
                Scale = 3,
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            context.Database.ExecuteSqlCommand(
                "EXEC SP_GetCustomerStatementOpening @dateFrom, @ledgerId, @result OUTPUT",
                dateFromParameter,
                ledgerIdParameter,
                resultParameter);

            decimal open = Convert.ToDecimal(resultParameter.Value);
            return open;
        }
        public List<Model.CustomerStatementDetailedModel> CustomerStatementDetailed(DateTime dateFrom, DateTime dateTo, int customerId)
        {
            List<Model.CustomerStatementDetailedModel> listCustomer = new List<Model.CustomerStatementDetailedModel>();
            try
            {

                using (betaskdbEntities context = new betaskdbEntities())
                {
                    int ledgerId = Convert.ToInt32(context.customer.AsNoTracking().Where(x => x.customer_id == customerId).FirstOrDefault().ledger_id);

                    decimal open = GetCustomerStatementOpening(context, dateFrom, ledgerId);


                    decimal openDebit = 0, openCredit = 0;
                    //var opDebit = context.account_transaction.AsNoTracking().Where(x => x.transaction_date < dateFrom && x.status == 1 && x.ledger_id == ledgerId).ToList();
                    //var opCredit = context.account_transaction.AsNoTracking().Where(x => x.transaction_date < dateFrom && x.status == 1 && x.ledger_id == ledgerId).ToList();
                    //openDebit = opDebit != null ? Convert.ToDecimal(opDebit.Sum(x => x.debit)) : 0;
                    //openCredit = opCredit != null ? Convert.ToDecimal(opCredit.Sum(x => x.credit)) : 0;
                    decimal _open = open;// openDebit - openCredit;
                    if (_open < 0)
                    {
                        openCredit = _open * -1;
                        openDebit = 0;
                    }
                    else
                    {
                        openCredit = 0;
                        openDebit = _open;
                    }
                    listCustomer.Add(new Model.CustomerStatementDetailedModel
                    {
                        Date = dateFrom,
                        Description = "Opening balance",
                        Debit = openDebit,
                        Credit = openCredit,
                    });

                    List<account_transaction> listccounts = context.account_transaction.AsNoTracking().Where(x => x.status == 1 && x.transaction_date >= dateFrom && x.transaction_date <= dateTo && x.ledger_id == ledgerId).ToList();


                    int lastTypeId = 0;
                    foreach (account_transaction acc in listccounts)
                    {
                        int typeId = Convert.ToInt32(acc.transaction_type_id);
                        if (lastTypeId > 0 && lastTypeId == typeId)
                            continue;
                        if (acc.transaction_type == "SALE")
                        {
                            var cd = listccounts.Where(x => x.transaction_type_id == typeId && x.credit > 0 && x.transaction_type == "SALE").FirstOrDefault();
                            string description = "";
                            decimal qty = 0;
                            List<sales_item> saleList = context.sales_item.AsNoTracking().Include(i => i.item).Where(x => x.status == 1 && x.sales_id == typeId).ToList();
                            var items = saleList.Select(x => x.item.item_name).Distinct();
                            if (items.Count() == 1)
                            {
                                description = saleList.Select(x => x.item.item_name).FirstOrDefault();
                                qty = saleList.Sum(x => x.qty);
                            }
                            else
                            {
                                foreach (sales_item sl in saleList)
                                {
                                    description += $"{ sl.item.item_name} Qty={sl.qty} @ {sl.net_amount}";
                                    description += " ,\r\n";
                                    qty += sl.qty;
                                }
                            }
                            decimal debit = listccounts.Where(x => x.transaction_type_id == typeId && x.debit > 0 && x.transaction_type == "SALE").FirstOrDefault() != null ? Math.Round(listccounts.Where(x => x.transaction_type_id == typeId && x.debit > 0 && x.transaction_type == "SALE").FirstOrDefault().debit, 3) : 0;
                            listCustomer.Add(new Model.CustomerStatementDetailedModel
                            {
                                Description = description,
                                Debit = debit,
                                Credit = Math.Round(cd != null ? cd.credit : 0, 3),
                                Qty = qty,
                                Date = acc.transaction_date,
                                TransactionType = acc.transaction_type
                            });

                        }
                        else
                        {
                            listCustomer.Add(new Model.CustomerStatementDetailedModel
                            {
                                Description = acc.narration,
                                Debit = acc.debit,
                                Credit = acc.credit,
                                Qty = 0,
                                Date = acc.transaction_date,
                                TransactionType = acc.transaction_type
                            });
                        }
                        lastTypeId = typeId;
                    }



                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return listCustomer;
        }
        //to update ledger id manualy
        public void UpdateCustomerLedgerId(customer _customer, int salesManLedgerId, int newLedgerId)
        {
            try
            {

                using (var context = new betaskdbEntities())
                {
                    var _cust = context.customer.AsNoTracking().Where(c => c.customer_id == _customer.customer_id).FirstOrDefault();
                    if (_cust != null && _cust.customer_id > 0)
                    {
                        if (newLedgerId > 0)
                            _customer.ledger_id = newLedgerId;
                        if (salesManLedgerId > 0)
                            _cust.salesman_ledger = salesManLedgerId;
                        context.Entry(_customer).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public void UpdateCustomerLedgerName(customer _customer, string ledgerName)
        {
            try
            {

                using (var context = new betaskdbEntities())
                {
                    if (!context.account_ledger.Any(x => x.ledger_name == ledgerName))
                    {
                        account_ledger ledger = context.account_ledger.FirstOrDefault(x => x.ledger_id == _customer.ledger_id);
                        ledger.ledger_name = ledgerName;
                        context.Entry(ledger).Property(x => x.ledger_name).IsModified = true;
                        context.SaveChanges();
                    }
                    else
                        throw new Exception("already exist");

                }
            }
            catch
            {
                throw;
            }
        }

        public List<SP_CustomerSummary_Result> GetCustomerSummaryByPayment()
        {
            List<SP_CustomerSummary_Result> listCustomer = new List<SP_CustomerSummary_Result>();
            try
            {

                using (var context = new betaskdbEntities())
                {
                    listCustomer = context.SP_CustomerSummary().ToList();
                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }
        public account_ledger GetCustomerSalemanLedger(int customerId)
        {
            account_ledger ledger = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer xCustomer = context.customer.FirstOrDefault(x => x.customer_id == customerId);
                    if (xCustomer.salesman_ledger != null)
                    {
                        ledger = context.account_ledger.FirstOrDefault(x => x.ledger_id == xCustomer.salesman_ledger);
                    }
                }
            }
            catch
            {
                throw;
            }
            return ledger;

        }
        public void SaveCustomerDivision(EDMX.customer_division division)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Entry(division).State = division.division_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<EDMX.customer_division> GetCustomerDivision(int customerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.customer_division.Where(x => x.customer_id == customerId).ToList();
                }
            }
            catch { throw; }
        }

        public decimal UpdateAndGetCustomerOutstanding(int customerId, DbContext context)
        {
            decimal outStanding = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateAndGetCustomerOutstanding", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
                        outStanding = Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting outstanding {ex.Message}");
            }
            return outStanding;
        }
        public void UpdateCustomerOnlineProfile(customer customer)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var xCustomer = context.customer.Find(customer.customer_id);
                    xCustomer.app_address1 = customer.app_address1;
                    xCustomer.app_address2 = customer.app_address2;
                    xCustomer.app_email = customer.app_email;
                    xCustomer.app_phone = customer.app_phone;
                    xCustomer.app_customer_name = customer.app_customer_name;
                    xCustomer.app_password = customer.app_password;

                    context.Entry(xCustomer).Property(c => c.app_address1).IsModified = true;
                    context.Entry(xCustomer).Property(c => c.app_address2).IsModified = true;
                    context.Entry(xCustomer).Property(c => c.app_email).IsModified = true;
                    context.Entry(xCustomer).Property(c => c.app_phone).IsModified = true;
                    context.Entry(xCustomer).Property(c => c.app_customer_name).IsModified = true;
                    context.Entry(xCustomer).Property(c => c.app_password).IsModified = true;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Model.GroupCustomerModel> GetGroupCustomer(betaskdbEntities context)
        {
            
                var groupCustomers = context.customer
                    .Where(x => x.is_group == true && x.status == 1)
                    .Select(x => new Model.GroupCustomerModel { GroupId = x.customer_id, GroupName = x.customer_name })
                    .ToList();
            return groupCustomers;
            
        }
        public List<Model.GroupCustomerModel> GetGroupCustomer()
        {
            using (var context = new betaskdbEntities())
            {
                var groupCustomers = context.customer
                    .Where(x => x.is_group == true && x.status == 1)
                    .Select(x => new Model.GroupCustomerModel { GroupId = x.customer_id, GroupName = x.customer_name })
                    .ToList();
                return groupCustomers;
            }

        }

       
    }

}

