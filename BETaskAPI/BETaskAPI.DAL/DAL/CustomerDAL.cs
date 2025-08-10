using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BETaskAPI.DAL.DAL
{
    public class CustomerDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public customer GetCustomerDetails(int customerId, bool couponValidate = false)
        {
            customer result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    result = context.customer.Find(customerId);
                    if (couponValidate && result.group_id > 0)
                    {
                        result = context.customer.Find(result.group_id);
                    }
                }

            }
            catch
            {
                throw;
            }

            return result;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cus"></param>
        public void SaveCustomer(customer cus)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (cus.customer_id > 0)
                    {
                        var xCust = context.customer.Where(x => x.customer_id == cus.customer_id).FirstOrDefault();
                        if (xCust != null)
                        {
                            cus.route_id = xCust.route_id;
                            cus.address2 = xCust.address2;
                            cus.contact_person = xCust.contact_person;
                            cus.wallet_number = xCust.wallet_number;
                            cus.wallet_balance = xCust.wallet_balance;
                            cus.ledger_id = xCust.ledger_id;
                            cus.payment_mode = xCust.payment_mode;
                            cus.delivery_interval = xCust.delivery_interval;
                        }
                    }
                    context.Entry(cus).State = cus.customer_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }


        public void UpdateCustomerLocation(customer cus)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (cus.customer_id > 0)
                    {
                        var xCust = context.customer.Where(x => x.customer_id == cus.customer_id).FirstOrDefault();
                        if (xCust != null)
                        {
                            xCust.lat = cus.lat;
                            xCust.lng = cus.lng;
                        }
                        context.Entry(xCust).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        public void SaveCustomer(customer customer, List<customer_aggrement> aggrements)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (customer.employee_id == 0)
                        customer.employee_id = null;
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var employee = context.employee.FirstOrDefault(x => x.employee_id == customer.employee_id);
                            if (employee != null && employee.designation != null && employee.designation.ToLower() == "executive")
                            {
                                if (context.customer.Any(x => x.customer_name.ToLower() == customer.customer_name))
                                    throw new Exception("Customer name already exist, please change name");

                                customer.ledger_id = SaveCustomerLedger(customer.customer_name, context);

                                context.customer.Add(customer);
                                context.SaveChanges();

                                UpdateWalletNumber(context, customer);

                                foreach (var obj in aggrements)
                                {
                                    obj.show_app = 1;
                                    obj.customer_id = customer.customer_id;
                                    context.Entry(obj).State = EntityState.Added;
                                    context.SaveChanges();
                                }
                            }
                            else
                            {
                                throw new Exception("You don't have the privelege to create customers");
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                        transaction.Commit();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private void UpdateWalletNumber(betaskdbEntities context, customer customer)
        {
            try
            {
                var settings = context.system_settings.AsNoTracking().FirstOrDefault();
                int pading = settings.min_wallet_length - (customer.customer_id.ToString().Length + settings.wallet_prefix.Length);
                pading = pading < 0 ? 0 : pading;
                string padstring = "".PadRight(pading, '0');
                string walletNumber = $"{settings.wallet_prefix}{padstring}{customer.customer_id}";
                customer.wallet_number = walletNumber;
                context.customer.Attach(customer);
                context.Entry(customer).Property(x => x.wallet_number).IsModified = true;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"on UpdateWalletNumber {ex.Message}");
            }
        }

        private int SaveCustomerLedger(string ledgerName, betaskdbEntities context)
        {
            try
            {
                int groupId = Convert.ToInt32(context.ledger_mapping.Where(x => x.status == 1 && x.ledger_type == "CUSTOMER" && x.status == 1).FirstOrDefault().group_id);
                EDMX.account_ledger account_Ledger = new account_ledger()
                {
                    ledger_name = ledgerName,
                    description = "CUSTOMER",
                    enable_cost_center = 2,
                    group_id = groupId,
                    ledger_id = 0,
                    status = 1
                };

                context.account_ledger.Add(account_Ledger);
                context.SaveChanges();
                return account_Ledger.ledger_id;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /*
        public List<customer> GetCustomerList(int employeeId)
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    int routeId = 0;
                    var oEmployee = context.employee.Where(x => x.employee_id == employeeId).FirstOrDefault();
                    routeId = Convert.ToInt32(oEmployee.route_id);
                    List<route_group> listGroupRoute = context.route_group.AsNoTracking().Where(x => x.route_id == routeId && x.Status==1).ToList();
                    if (listGroupRoute == null || listGroupRoute.Count==0)
                    {
                        listCustomer = context.customer.AsNoTracking().Include(r => r.route).Include(b => b.building).Where(x => x.route_id == routeId && x.status == 1).OrderBy(o => o.customer_name).ToList();

                        //for admin users
                        try
                        {
                            if (oEmployee.other_details != null && oEmployee.other_details.ToString().ToLower() == "adm")
                            {
                                listCustomer = context.customer.Include(r => r.route).Include(b => b.building).Where(x => x.status == 1).OrderBy(o => o.customer_name).ToList();
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        foreach(route_group rt in listGroupRoute)
                        listCustomer.AddRange( context.customer.Include(r => r.route).Include(b => b.building).Where(x => x.route_id==rt.sub_route_id && x.status == 1).OrderBy(o => o.customer_name).ToList());

                    }
                    //if (routeId != 0)
                    //{
                    //  //  listCustomer = context.customer.Include(r => r.route).Where(x => x.route_id == routeId && x.status == 1).OrderBy(ro => ro.route_id).ThenBy(o => o.customer_name).ToList();
                    //}
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return listCustomer;
        }*/

        public async Task<List<SP_GetCustomerListByRoute_Result>> GetCustomerListAsync(int employeeId)
        {
            List<SP_GetCustomerListByRoute_Result> listCustomer = new List<SP_GetCustomerListByRoute_Result>();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    int routeId = 0;
                    //var oEmployee = context.employee.AsNoTracking().Where(x => x.employee_id == employeeId).FirstOrDefault();
                    //routeId = Convert.ToInt32(oEmployee.route_id);
                    string otherDetail = string.Empty;

                    using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                    {
                        conn.Open();
                        GetEmployeeDetails(employeeId, ref routeId, ref otherDetail, conn);
                        List<route_group> listGroupRoute = GetSubroutes(routeId, conn);

                        //List<route_group> listGroupRoute = context.route_group.AsNoTracking().Where(x => x.route_id == routeId && x.Status == 1).ToList();

                        if (listGroupRoute == null || listGroupRoute.Count == 0)
                        {
                            try
                            {
                                //for admin users
                                if (!string.IsNullOrEmpty(otherDetail) && otherDetail.ToLower() == "adm")
                                {
                                    listCustomer = listCustomer = context.SP_GetCustomerListByRoute(0).ToList();
                                }
                                else
                                {
                                    listCustomer = await ExecuteStoredProcedureAndGetDataTable(conn, routeId);
                                    //listCustomer = context.SP_GetCustomerListByRoute(routeId).ToList();
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            foreach (route_group rt in listGroupRoute)
                            {
                                // listCustomer.AddRange(context.SP_GetCustomerListByRoute(rt.sub_route_id).ToList());
                                listCustomer.AddRange(await ExecuteStoredProcedureAndGetDataTable(conn, rt.sub_route_id));
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listCustomer;
        }

        private static void GetEmployeeDetails(int employeeId, ref int routeId, ref string otherDetail, SqlConnection conn)
        {
            using (SqlCommand command = new SqlCommand("SP_GetEmployee", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empId", employeeId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        routeId = reader["route_id"] != DBNull.Value ? Convert.ToInt32(reader["route_id"]) : 0;
                        otherDetail = reader["other_details"] != DBNull.Value ? reader["other_details"].ToString() : string.Empty;

                    }
                }
            }
        }

        private static List<route_group> GetSubroutes(int routeId, SqlConnection conn)
        {
            var listGroupRoute = new List<route_group>();
            using (SqlCommand command = new SqlCommand("SP_GetRouteGroupsByRouteId", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@routeId", routeId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["sub_route_id"] != DBNull.Value)
                        {
                            listGroupRoute.Add(new route_group
                            {
                                sub_route_id = Convert.ToInt32(reader["sub_route_id"])
                            });
                        }
                    }
                }
            }

            return listGroupRoute;
        }

        private async Task<List<SP_GetCustomerListByRoute_Result>> ExecuteStoredProcedureAndGetDataTable(SqlConnection conn, int routeId)
        {
            try
            {
                DataTable tblCustomerList = new DataTable();

                using (SqlCommand command = new SqlCommand("SP_GetCustomerListByRoute", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@routeId", routeId);
                    if (conn.State != ConnectionState.Open)
                    {
                        await conn.OpenAsync();
                    }

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        tblCustomerList.Load(reader);
                    }
                }

                var convertedList = ConvertDataTableToCustomerList(tblCustomerList);
                return convertedList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<SP_GetCustomerListByRoute_Result> ConvertDataTableToCustomerList(DataTable dt)
        {
            List<SP_GetCustomerListByRoute_Result> customerList = new List<SP_GetCustomerListByRoute_Result>();

            foreach (DataRow row in dt.Rows)
            {
                SP_GetCustomerListByRoute_Result customer = new SP_GetCustomerListByRoute_Result
                {
                    customer_id = Convert.ToInt32(row["customer_id"]),
                    customer_name = Convert.ToString(row["customer_name"]),
                    remarks = Convert.ToString(row["remarks"]),
                    distance = row.IsNull("distance") ? (int?)null : Convert.ToInt32(row["distance"]),
                    address1 = Convert.ToString(row["address1"]),
                    address2 = Convert.ToString(row["address2"]),
                    city = Convert.ToString(row["city"]),
                    street = Convert.ToString(row["street"]),
                    pobox = Convert.ToString(row["pobox"]),
                    email = Convert.ToString(row["email"]),
                    phone = Convert.ToString(row["phone"]),
                    mobile = Convert.ToString(row["mobile"]),
                    trn = Convert.ToString(row["trn"]),
                    lat = Convert.ToString(row["lat"]),
                    lng = Convert.ToString(row["lng"]),
                    wallet_balance = Convert.ToDecimal(row.IsNull("wallet_balance") ? (decimal?)null : Convert.ToDecimal(row["wallet_balance"])),
                    wallet_number = Convert.ToString(row["wallet_number"]),
                    delivery_interval = Convert.ToString(row["delivery_interval"]),
                    payment_mode = Convert.ToString(row["payment_mode"]),
                    outstanding_amount = row.IsNull("outstanding_amount") ? (decimal?)null : Convert.ToDecimal(row["outstanding_amount"]),
                    route_name = Convert.ToString(row["route_name"]),
                    building_name = Convert.ToString(row["building_name"])
                };

                customerList.Add(customer);
            }

            return customerList;
        }
        public List<SP_GetCustomerNearest_Result> GetCustomerListNearestLocation(int employeeId, decimal lat, decimal lng)
        {
            List<SP_GetCustomerNearest_Result> listCustomer = new List<SP_GetCustomerNearest_Result>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int routeId = 0;
                    var oEmployee = context.employee.Where(x => x.employee_id == employeeId).FirstOrDefault();
                    routeId = Convert.ToInt32(oEmployee.route_id);
                    listCustomer = context.SP_GetCustomerNearest(lat, lng, employeeId).ToList();

                    //for admin users
                    try
                    {
                        if (oEmployee.other_details != null && oEmployee.other_details.ToString().ToLower() != "adm")
                        {
                            listCustomer = listCustomer.Where(x => x.route_id == oEmployee.route_id).ToList();
                        }
                    }
                    catch { }
                    //if (routeId != 0)
                    //{
                    //  //  listCustomer = context.customer.Include(r => r.route).Where(x => x.route_id == routeId && x.status == 1).OrderBy(ro => ro.route_id).ThenBy(o => o.customer_name).ToList();
                    //}
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listCustomer;
        }

        public decimal CustomerOutstandingTotal(int employeeId)
        {
            decimal customerOutstanding = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int routeId = 0;
                    var oEmployee = context.employee.Where(x => x.employee_id == employeeId).FirstOrDefault();
                    routeId = Convert.ToInt32(oEmployee.route_id);
                    if (routeId != 0)
                        customerOutstanding = Convert.ToDecimal(context.customer.Where(x => x.outstanding_amount > 0 && x.route_id == routeId && x.status == 1).Sum(x => x.outstanding_amount));
                    else
                        customerOutstanding = Convert.ToDecimal(context.customer.Where(x => x.outstanding_amount > 0 && x.status == 1).Sum(x => x.outstanding_amount));
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return customerOutstanding;
        }
        public decimal CheckCouponValidity(string couponNumbers, int customerId)
        {
            decimal couponTotal = 0;
            try
            {
                string[] coupons = couponNumbers.Split(',');
                if (coupons.Length > 0)
                {
                    using (var context = new betaskdbEntities())
                    {
                        foreach (string cp in coupons)
                        {
                            int leafNumber = Convert.ToInt32(cp.Replace(",", ""));
                            //coupon listCoupon = context.coupon.Include(l => l.coupon_leaf.Where(n=>n.leaf_number == Convert.ToInt32(cp) && n.status==1)).Where(x => x.customer_id == customerId && x.status == 1 ).FirstOrDefault();
                            coupon_leaf leaf = context.coupon_leaf.AsNoTracking().Include(c => c.coupon).Where(x => x.coupon.customer_id == customerId && x.coupon.status == 1 && x.leaf_number == leafNumber && x.status == 1).FirstOrDefault();
                            if (leaf != null)
                                couponTotal += Convert.ToDecimal(leaf.leaf_rate);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return couponTotal;
        }
        public List<string> GetRoutes()
        {
            List<string> Routes = new List<string>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    Routes = context.route.Where(x => x.status == 1).Select(x => x.route_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return Routes;
        }
        public int GetRouteIdByName(string routeName)
        {
            List<string> Routes = new List<string>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.route route = context.route.Where(x => x.route_name.ToLower() == routeName.ToLower()).FirstOrDefault();
                    return route?.route_id ?? 0;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public int GeBuildingIdByName(string buildingName)
        {
            List<string> Routes = new List<string>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.building building = context.building.Where(x => x.building_name.ToLower() == buildingName.ToLower()).FirstOrDefault();
                    return building?.building_id ?? 0;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public string GetEmployeeRoute(int employeeId)
        {
            string route = string.Empty;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    employee empl = context.employee.Include(r => r.route).Where(x => x.employee_id == employeeId).FirstOrDefault();
                    if (empl != null)
                        route = empl.route.route_name;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return route;
        }

        public List<string> GetBuilding(int employeeId = 0)
        {
            List<string> _building = new List<string>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int routeId = Convert.ToInt32(context.employee.Where(x => x.employee_id == employeeId).FirstOrDefault().route_id);
                    route _route = context.route.Where(x => x.route_id == routeId).FirstOrDefault();
                    List<building> listBuilding = context.building.Where(x => x.status == 1 && x.route.ToLower().Contains(_route.route_name.ToLower())).OrderBy(x => x.building_name).ToList();
                    List<route_group> listGroupRoute = context.route_group.Include(x => x.route).AsNoTracking().Where(x => x.route_id == routeId && x.Status == 1).ToList();
                    if (listGroupRoute != null && listGroupRoute.Count > 0)
                    {
                        foreach (route_group rt in listGroupRoute)
                        {
                            string rtName = context.route.AsNoTracking().Where(x => x.route_id == rt.sub_route_id).FirstOrDefault().route_name;
                            listBuilding.AddRange(context.building.Where(x => x.status == 1 && x.route.ToLower().Contains(rtName)).OrderBy(x => x.building_name).ToList());
                        }
                    }
                    if (listBuilding == null && listBuilding.Count > 0)
                        _building = context.building.Where(x => x.status == 1).OrderBy(x => x.building_name).Select(x => x.building_name).Take(10).ToList();
                    else
                        _building = listBuilding.Select(x => x.building_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return _building;
        }
        public List<EDMX.customer_division> GetCustomerDivision(int customerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.customer_division.Where(x => x.customer_id == customerId && x.status == 1).ToList();
                }
            }
            catch { throw; }
        }

        public PunchStatusModel GetEmployeePunchStatus(int employeeId)
        {
            PunchStatusModel punchStatusModel = new PunchStatusModel { };
            string punchType = "IN";
            string date = DateTime.Now.ToString("dd/MMM/yyyy");
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetEmployeePunchStatus", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@employeeId", employeeId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                punchType = "IN";
                                punchStatusModel.PunchType = punchType;

                            }
                            else
                            {
                                if (reader.Read())
                                {
                                    Object employeeName = reader["employee_name"];
                                    object punchOutObj = reader["punch_out"];
                                    object punchDateObj = reader["punch_date"];
                                    object punchInObj = reader["punch_in"];
                                    object punchId = reader["punch_id"];


                                    if (punchOutObj != DBNull.Value)
                                    {
                                        punchType = "IN";
                                        punchId = 0;
                                    }
                                    else
                                    {
                                        punchType = "OUT";
                                        date = punchInObj.ToString();
                                    }

                                    punchStatusModel.EmployeeName = employeeName.ToString();
                                    punchStatusModel.PunchType = punchType;
                                    punchStatusModel.PunchDate = punchDateObj.ToString();
                                    punchStatusModel.PunchIn = punchInObj.ToString();
                                    punchStatusModel.PunchOut = punchOutObj.ToString();
                                    punchStatusModel.PunchId = Convert.ToInt64(punchId);


                                }
                                else
                                    punchStatusModel.PunchType = punchType;
                            }
                        }
                    }
                }
                }
            catch (Exception ex)
            {

                throw;
            }
            return punchStatusModel;
        }

        public PunchModel GetEmployeePunchDetailByPunchId(long pId, SqlConnection conn)
        {
            PunchModel punchModel = new PunchModel { };
            
            try
            {
                if (pId == 0) return null;
               
                {
                    using (SqlCommand command = new SqlCommand("SP_GetEmployeePunchDetailsById", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PunchId", pId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                punchModel= null;

                            }
                            else if (reader.Read())
                            {
                               
                                
                                    object punchOutObj = reader["punch_out"];
                                    object punchDateObj = reader["punch_date"];
                                    object punchInObj = reader["punch_in"];
                                    object punchId = reader["punch_id"];

                                    punchModel.PunchDate =Convert.ToDateTime( punchDateObj.ToString());
                                    punchModel.PunchIn = Convert.ToDateTime(punchInObj.ToString());
                                    punchModel.PunchOut = null;
                                    punchModel.PunchId = Convert.ToInt64(punchId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return punchModel;
        }

        public  bool InsertPunchAsync(PunchModel punch)
        {
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();

                    if (punch.PunchId > 0)
                    {
                        var xPunch = GetEmployeePunchDetailByPunchId(punch.PunchId, conn);
                        if (xPunch != null)
                        {
                            punch.PunchIn = xPunch.PunchIn;
                            punch.Remarks = xPunch.Remarks;
                            punch.PunchOut = DateTime.Now;
                            punch.PunchDate = xPunch.PunchDate;
                        }
                    }
                    else
                    {
                        punch.PunchDate =DateTime.Now;

                    }


                    using (SqlCommand cmd = new SqlCommand("SP_InsertPunch", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Map model parameters
                        cmd.Parameters.AddWithValue("@punchId", punch.PunchId);
                        cmd.Parameters.AddWithValue("@employeeId", punch.EmployeeId);
                        cmd.Parameters.AddWithValue("@lat", punch.Lat);
                        cmd.Parameters.AddWithValue("@lng", punch.Lng);
                        cmd.Parameters.AddWithValue("@locationDetails", punch.LocationDetails);
                        cmd.Parameters.AddWithValue("@punchDate", punch.PunchDate);
                        cmd.Parameters.AddWithValue("@punchIn", punch.PunchIn);
                        cmd.Parameters.AddWithValue("@punchOut", punch.PunchOut ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@remarks", string.IsNullOrWhiteSpace(punch.Remarks) ? (object)DBNull.Value : punch.Remarks);
                        cmd.Parameters.AddWithValue("@status", punch.Status);
                        cmd.Parameters.AddWithValue("@appDate", punch.AppDate);
                        cmd.Parameters.AddWithValue("@appVersion", punch.AppVersion);

                        int rowsAffected =  cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw new ApplicationException("Error inserting punch record", ex);
            }
        }


    }
}
