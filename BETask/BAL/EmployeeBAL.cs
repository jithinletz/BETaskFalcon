using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using EDMXAPP = BETask.APP.EDMX;
using System;
using System.Collections.Generic;
using BETask.Common;
using System.Diagnostics;
using System.Data;
using RPT = BETask.Report.ReportForm;

namespace BETask.BAL
{
    public class EmployeeBAL
    {
        EmployeeDAL objEmployee = new EmployeeDAL();
        public void SaveEmployee(EDMX.employee employee)
        {
            try
            {
                int employeeId = objEmployee.SaveEmployee(employee);
            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = employee.employee_id,
                    summary = $" Updating Employee {employee.first_name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.employee> GetAllEmployee()
        {
            try
            {
                return objEmployee.GetAllEmployee();
            }
            catch { throw; }
        }
        public List<EDMX.employee> GetActiveEmployees()
        {
            try
            {
                return objEmployee.GetActiveEmployees();
            }
            catch { throw; }
        }
        public EDMX.employee GetAllEmployeeDetails(int employeeId)
        {
            try
            {
                return objEmployee.GetAllEmployeeDetails(employeeId);
            }
            catch { throw; }
        }
        public List<EDMX.employee> GetAllEmployee(int routeId, bool allEmployee)
        {
            try
            {
                return objEmployee.GetAllEmployee(routeId,allEmployee);
            }
            catch { throw; }
        }
        public List<EDMX.employee> GetAllEmployeeRoutewise()
        {
            try
            {
                return objEmployee.GetEmployeeRoutes();
            }
            catch { throw; }
        }
        public void PrintEmployee(int routeId, bool allEmployee)
        {
            try
            {
                List<EDMX.employee> listEmployee = GetAllEmployee(routeId, allEmployee);
                if (listEmployee != null && listEmployee.Count > 0)
                {
                    DataTable tblEmployee = new DataTable();
                    BETask.Report.DSReports.EmployeeReportDataTable employeeReportDataTable = new Report.DSReports.EmployeeReportDataTable();
                    tblEmployee = employeeReportDataTable.Clone();
                    foreach (EDMX.employee employee in listEmployee)
                    {
                        DataRow dr = tblEmployee.NewRow();
                        dr["Code"] = employee.employee_code;
                        dr["FirstName"] = employee.first_name;
                        dr["LastName"] = employee.last_name;
                        dr["Designation"] = employee.designation;
                        dr["JoinDate"] = General.ConvertDateAppFormat(employee.join_date);
                        if (employee.resign_date != null)
                            dr["ResignaDate"] = General.ConvertDateAppFormat(DateTime.Parse(employee.resign_date.ToString()));
                        else
                            dr["ResignaDate"] = "";
                        dr["Route"] = employee.route_id != null ? employee.route.route_name : "";
                        tblEmployee.Rows.Add(dr);
                    }
                    if (tblEmployee != null && tblEmployee.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.EmployeeReport, "Employee Reprot", tblEmployee);
                        reportForm.Show();
                    }
                }
            }
            catch { throw; }
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
                    summary = $" Print Employee List ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public int Login(string username, string password)
        {
            bool resp = false;
            try
            {
               var employee= objEmployee.Login(username, password);
                return employee;

            }
            catch { throw; }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = username,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id=0,
                    summary=resp?"Login Success":"Login Failed",
                   
                };
                UserLogDAL.Log(user_Log);
            }
            
        }
        public int CreateSalesmanLedger(string ledgerName,int employeeId,int xledgerId=0)
        {
            int ledgerId = xledgerId;
            try
            {
                if (ledgerName !=string.Empty)
                {
                    LedgerMappingDAL objLedgerMapDAL = new LedgerMappingDAL();
                    AccountLedgerDAL ledgerDAL = new AccountLedgerDAL();
                    EDMX.ledger_mapping mapLedger = new EDMX.ledger_mapping();
                    string custType = "SALESMANCREDIT";
                   
                        mapLedger = objLedgerMapDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.CUSTOMER);
                   

                    if (mapLedger != null && mapLedger.group_id > 0)
                    {
                        EDMX.account_ledger ledger = new EDMX.account_ledger()
                        {
                            ledger_id=ledgerId,
                            group_id = Convert.ToInt32(mapLedger.group_id),
                            ledger_name = $"{ledgerName}",
                            description = custType,
                            status = 1,
                        };
                         ledgerId = ledgerDAL.SaveAccountLedger(ledger);
                      
                    }
                 
                }
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
                    reference_id = employeeId,
                    summary =$"{ledgerName}",

                };
                UserLogDAL.Log(user_Log);
            }
            return ledgerId;

        }
        public int UpdateCustomerLedger_SalesmanCredit(int routeId, int employeeId,int ledgerId,string ledgerName, string userName)
        {
            try
            {
                return objEmployee.UpdateCustomerLedger_SalesmanCredit(routeId, ledgerId, ledgerName, userName);
                   
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
                    reference_id = employeeId,
                    summary = $"Updateing salemancredit ledger to ledger {ledgerName}",

                };
                UserLogDAL.Log(user_Log);
            }
        }

        public DataTable GetPunchReport(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            try
            {
                return objEmployee.GetPunchReport(dateFrom, dateTo, employeeId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool UpdatePunchReport(DAL.Model.PunchModel punch)
        {
            try
            {
                return objEmployee.UpdatePunchReport(punch);
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
                    reference_id = punch.EmployeeId,
                    summary = $" Updating punch",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintPunchReport(DateTime dateFrom, DateTime dateTo, DataTable tblReport)
        {
            try
            {
              //  DataTable tblReport = objEmployee.GetPunchReport(dateFrom, dateTo, employeeId);
                BETask.Report.DSReports.PunchReportDataTable punchReportDataTable = new Report.DSReports.PunchReportDataTable();
                foreach (DataRow dr in tblReport.Rows)
                {
                    DataRow row = punchReportDataTable.NewRow();
                    row["PunchDate"] = dr["PunchDate"];
                    row["EmployeeName"] = dr["Employee"];
                    row["PunchIn"] = dr["PunchIn"];
                    row["PunchOut"] =dr["PunchOut"];
                    row["HoursWorked"] = dr["HoursLogged"];
                    row["InLocation"] =dr["LocationIn"];
                    row["OutLocation"] = dr["LocationOut"];
                    row["Route"] = "";
                    row["Remarks"] = "";

                    punchReportDataTable.Rows.Add(row);
                }
                if (tblReport != null && tblReport.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.PunchReport, $"Punch Rpeort Between {General.ConvertDateAppFormat(dateFrom)} & {General.ConvertDateAppFormat(dateTo)}", punchReportDataTable);
                    reportForm.Show();
                }
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
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
                    summary = $" Updating punch",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
    }
}
