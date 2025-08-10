using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BETaskAPI.DAL.DAL
{
    public class ReportDAL
    {

        public List<RPT_ItemDeliverySummary_Result> RPTItemDeliverySummary(int employeeId, DateTime fromDate, DateTime toDate)
        {
            List<RPT_ItemDeliverySummary_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_ItemDeliverySummary(employeeId, fromDate, toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public List<RPT_ItemDeliveryReturnSummary_Result> RPTItemDeliveryReturnSummary(int employeeId, DateTime fromDate, DateTime toDate)
        {
            List<RPT_ItemDeliveryReturnSummary_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_ItemDeliveryReturnSummary(employeeId, fromDate, toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }


        public List<RPT_DailyCollectionSummary_Result> RPTDailyCollectionSummary(int employeeId, DateTime fromDate, DateTime toDate)
        {
            List<RPT_DailyCollectionSummary_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_DailyCollectionSummary(employeeId, fromDate, toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }


        public List<RPT_DailyCollectionDetails_Result> RPTDailyCollectionDetails(int employeeId, DateTime date)
        {
            List<RPT_DailyCollectionDetails_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_DailyCollectionDetails(employeeId, date).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }

        public List<RPT_DailyCollectionSummaryAllPayments_Result> RPTDailyCollectionSummaryAllPayments(int employeeId, DateTime fromDate, DateTime toDate)
        {
            List<RPT_DailyCollectionSummaryAllPayments_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_DailyCollectionSummaryAllPayments(employeeId, fromDate, toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public List<RPT_DailyCollectionAllPaymentDetails_Result> RPTDailyCollectionAllPaymentDetails(int employeeId, DateTime date)
        {
            List<RPT_DailyCollectionAllPaymentDetails_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_DailyCollectionAllPaymentDetails(employeeId, date).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public List<RPT_GetRouteItemsSummary_Result> RPTRouteItemSummary(int employeeId)
        {
            List<RPT_GetRouteItemsSummary_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    result = context.RPT_GetRouteItemsSummary(employeeId).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }

        public List<RPT_GetAllRouteItemsSummary_Result> RPTRouteItemSummaryAll(int employeeId, out bool noPrivilege)
        {
            noPrivilege = false;
            List<RPT_GetAllRouteItemsSummary_Result> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var emp = context.employee.Where(x => x.employee_id == employeeId).FirstOrDefault();
                    if (emp.other_details == "adm")
                        result = context.RPT_GetAllRouteItemsSummary().ToList();
                    else
                    {
                        noPrivilege = true;
                    }
                    //throw new Exception("You have no privilege to view this report");
                }

            }
            catch (Exception ee)
            {
                throw;
            }

            return result;

        }

        //Customer outstanding report
        public List<customer> CustomerOutstandingReport(int employeeId)
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int routeId = 0;
                    employee xEmployee = context.employee.Where(x => x.employee_id == employeeId).FirstOrDefault();
                    routeId = Convert.ToInt32(xEmployee.route_id);

                    if (xEmployee.other_details!=null && xEmployee.other_details.ToString().ToLower() == "adm")
                        listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => x.status == 1).OrderBy(o => o.customer_name).OrderBy(ro => ro.route_id).ThenBy(o => o.customer_name).ToList();
                    else
                        listCustomer = context.customer.AsNoTracking().Include(r => r.route).Where(x => x.route_id == routeId && x.status == 1).OrderBy(o => o.customer_name).OrderBy(ro => ro.route_id).ThenBy(o => o.customer_name).ToList();
                    listCustomer = listCustomer.OrderByDescending(x => x.outstanding_amount).ToList();
              
                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }
        public List<delivery_items> DeliveryReportCustomerwise(DateTime dateFrom, DateTime dateTo, int employeeId,int customerId = 0)
        {
            List<delivery_items> listItems = new List<delivery_items>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    employee _employee = context.employee.AsNoTracking().Where(x => x.employee_id == employeeId).FirstOrDefault();
                    if (_employee.other_details == "adm")
                    {
                        listItems = context.delivery_items.AsNoTracking().Include(i => i.item).AsNoTracking().Include(c => c.customer).AsNoTracking().
                            Where(x => x.status == 4 && x.delivery_time >= dateFrom && x.delivery_time <= dateTo && (customerId == 0 ? x.customer_id > 0 : x.customer_id == customerId)).OrderBy(x => x.customer_id).ThenBy(x => x.delivery_time).ToList();
                    }
                    else
                    {
                        listItems = context.delivery_items.AsNoTracking().Include(i => i.item).AsNoTracking().Include(c => c.customer).AsNoTracking().Include(d => d.delivery).AsNoTracking().
                            Where(x => x.status == 4 && x.delivery.employee_id==employeeId && x.delivery_time >= dateFrom && x.delivery_time <= dateTo && (customerId == 0 ? x.customer_id > 0 : x.customer_id == customerId)).OrderBy(x => x.customer_id).ThenBy(x => x.delivery_time).ToList();
                    }
                    
                }

            }
            catch
            {
                throw;
            }
            return listItems;
        }

        public List<delivery_return> DeliveryReturnReportCustomerwise(DateTime dateFrom, DateTime dateTo, int employeeId,int customerId = 0)
        {
            List<delivery_return> listItems = new List<delivery_return>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    employee _employee = context.employee.AsNoTracking().Where(x => x.employee_id == employeeId).FirstOrDefault();
                    if (_employee.other_details == "adm")
                    {
                        listItems = context.delivery_return.AsNoTracking().Include(i => i.item).AsNoTracking().Include(c => c.customer).AsNoTracking().
                        Where(x => x.return_date >= dateFrom && x.return_date <= dateTo && (customerId == 0 ? x.customer_id > 0 : x.customer_id == customerId)).OrderBy(x => x.customer_id).ThenBy(x => x.return_date).ToList();
                    }
                    else
                    {
                        listItems = context.delivery_return.AsNoTracking().Include(i => i.item).AsNoTracking().Include(c => c.customer).AsNoTracking().
                        Where(x => x.employee_id == employeeId &&  x.return_date >= dateFrom && x.return_date <= dateTo && (customerId == 0 ? x.customer_id > 0 : x.customer_id == customerId)).OrderBy(x => x.customer_id).ThenBy(x => x.return_date).ToList();
                    }
                }

            }
            catch
            {
                throw;
            }
            return listItems;
        }

    }
}
