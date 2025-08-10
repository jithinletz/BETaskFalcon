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
    public class EmployeeDAL
    {
        public int SaveEmployee(employee _employee)
        {
            int employeeId = 0;
            try
            {

                using (var context = new betaskdbEntities())
                {
                    if (_employee.salesman_credit_ledger == 0)
                        _employee.salesman_credit_ledger = null;
                    _employee.route_id = _employee.route_id == 0 ? null : _employee.route_id;
                    _employee.resign_date = _employee.status == 1 ? null : _employee.resign_date;
                    context.Entry(_employee).State = _employee.employee_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                    employeeId = _employee.employee_id;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return employeeId;
        }
        public List<employee> GetAllEmployee()
        {
            List<employee> listEmployee = new List<employee>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listEmployee = context.employee.Include(r => r.route).Include(l => l.account_ledger).Where(X => X.status <= 2).OrderBy(x => x.status).ThenBy(x => x.first_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listEmployee;
        }
        public List<employee> GetActiveEmployees()
        {
            List<employee> listEmployee = new List<employee>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listEmployee = context.employee.Include(r => r.route).Include(l => l.account_ledger).Where(X => X.status ==1).OrderBy(x => x.first_name).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listEmployee;
        }
        public List<employee> GetAllEmployeeOtherthanRouteuser(betaskdbEntities _context = null)
        {
            List<employee> listEmployee = new List<employee>();
            try
            {
                if (_context != null)
                    listEmployee = _context.employee.Include(r => r.route).Include(l => l.account_ledger).Where(X => X.status <= 2 && X.department != "RouteUser").OrderBy(x => x.status).ThenBy(x => x.first_name).ToList();
                else
                {
                    using (var context = new betaskdbEntities())
                    {
                        listEmployee = context.employee.Include(r => r.route).Include(l => l.account_ledger).Where(X => X.status <= 2 && X.department != "RouteUser").OrderBy(x => x.status).ThenBy(x => x.first_name).ToList();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listEmployee;
        }
        public List<employee> GetAllEmployee(int routeId, bool allEmployee)
        {
            List<employee> listEmployee = new List<employee>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listEmployee = context.employee.Include(r => r.route).OrderBy(x => x.resign_date).ThenBy(x => x.route_id).ThenBy(x => x.first_name).ToList();
                    if (!allEmployee)
                    {
                        listEmployee = listEmployee.Where(x => x.status == 1).ToList();
                    }
                    if (routeId > 0)
                    {
                        listEmployee = listEmployee.Where(x => x.route_id == routeId).ToList();
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listEmployee;
        }
        public employee GetAllEmployeeDetails(int employeeId)
        {
            employee objEmployee = new employee();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objEmployee = context.employee.Include(r => r.route).Where(x => x.employee_id == employeeId).OrderBy(x => x.status).ThenBy(x => x.employee_id).FirstOrDefault();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return objEmployee;
        }
        public int Login(string username, string password)
        {

            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_UserLogIn", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);

                            var userId = cmd.ExecuteScalar();
                            return Convert.ToInt32(userId);
                        }
                    }

                }

            }
            catch (Exception ee)
            {
                throw;
            }

           
        }
        public List<EDMX.employee> GetEmployeeRoutes()
        {
            List<employee> listEmployee = new List<employee>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listEmployee = context.employee.AsNoTracking().Include(r => r.route).AsNoTracking().Where(x => x.status == 1 && x.route_id != null).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listEmployee;
        }
        public int UpdateCustomerLedger_SalesmanCredit(int routeId, int ledgerId, string ledgerName, string userName)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<customer> listCustomer = context.customer.Where(x => x.route_id == routeId && x.payment_mode.ToLower() == "salesmancredit" && x.status == 1 && x.customer_type == 1 && x.salesman_ledger != ledgerId).ToList();
                    foreach (customer cs in listCustomer)
                    {
                        cs.salesman_ledger = ledgerId;
                        context.SaveChanges();

                        user_log user = new user_log()
                        {
                            module = "SalesmanCredit",
                            module_action = "UpdateCustomerLedger",
                            reference_id = cs.customer_id,
                            server_time = DateTime.Now,
                            summary = $"Update of salesman credit ledger to {ledgerName}",
                            username = userName

                        };
                        context.Entry(user).State = EntityState.Added;
                        context.SaveChanges();
                        result++;
                    }



                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public account_ledger GetSalemanCreditLedgerByRoute(int routeId)
        {
            account_ledger ledger = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    ledger = context.employee.Include(a => a.account_ledger).FirstOrDefault(x => x.route_id == routeId).account_ledger;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ledger;
        }
        public employee GetEmployeeByCustomer(int customerId)
        {
            employee _employee = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int routeId = Convert.ToInt32(context.customer.FirstOrDefault(x => x.customer_id == customerId).route_id);
                    _employee = context.employee.AsNoTracking().FirstOrDefault(x => x.route_id == routeId);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return _employee;
        }

        public DataTable GetPunchReport(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            try
            {
                DataTable tblReport = new DataTable();

                using (DbContext context = new betaskdbEntities())
                {

                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetPunchDReport", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters with values
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@dateTo", dateTo);
                            cmd.Parameters.AddWithValue("@employeeId", employeeId);
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

        public bool UpdatePunchReport(Model.PunchModel punch)
        {
            try
            {

                using (DbContext context = new betaskdbEntities())
                {
                    using (SqlConnection connection = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_InsertPunch", connection))
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

                            int rowsAffected = cmd.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on getting outstanding {ex.Message}");
            }
            return false;
        }

    }
}
