using System;
using System.Collections.Generic;
using System.Linq;
using BETask.DAL.EDMX;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BETask.DAL.DAL
{
    public class MiscellaneousReportDAL
    {
        public DataTable WalletDifferenceReport()
        {
            DataTable tblData = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string sql = "[SP_MiscWalletDefference]";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tblData;
        }
        public DataTable DeliveySaleCompare(DateTime dateFrom, DateTime dateTo)
        {
            DataTable tblData = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        //string sql = "select * from ( select r.*,sum(e.collected_amount) as collectionAmount from(" +
                        //            " select b.delivery_date,b.delivery_id, a.customer_id,d.customer_name,e.route_name, sum(a.net_amount) as deliveryAmount,sum(c.net_amount) as saleAmount from delivery_items a" +
                        //            " inner join delivery b on b.delivery_id=a.delivery_id and b.delivery_date>='"+dateFrom.ToString("yyyy/MM/dd")+ "' and b.delivery_date<='" + dateTo.ToString("yyyy/MM/dd") + "'" +
                        //            " inner join sales c on c.customer_id=a.customer_id and CAST(c.sales_date as date)=b.delivery_date and c.status=1 and c.net_amount>0" +
                        //            " inner join customer d on d.customer_id=a.customer_id" +
                        //            " inner join route e on e.route_id=d.route_id" +
                        //            " where a.status=4 and a.net_amount>0  " +
                        //            " group by b.delivery_date,b.delivery_id,a.customer_id,d.customer_name,e.route_name )r " +
                        //            " inner join daily_collection e on e.delivery_id=r.delivery_id and e.customer_id=r.customer_id and e.collected_amount>0 and e.status=4" +
                        //            " group by  delivery_date,r.delivery_id,r.customer_id,customer_name,route_name,r.deliveryAmount,r.saleAmount" +
                        //            " )r1 where r1.collectionAmount<>r1.saleAmount order by delivery_date,route_name,customer_name"; 
                        string sql = "SP_CreateCompareDeliverySaleCollection";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom.ToString("yyyy/MM/dd"));
                            cmd.Parameters.AddWithValue("@dateTo", dateTo.ToString("yyyy/MM/dd"));
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tblData;
        }
        public DataSet DeliveySaleCompare_Details(DateTime dateFrom, int customerId, int deliveryId)
        {
            DataSet tblData = new DataSet();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string sql = "select a.sales_number,payment_mode,sales_order as deliveryNumber,a.net_amount from sales a where CAST(sales_date as date) ='" + dateFrom.ToString("yyyy/MM/dd") + "' and customer_id=" + customerId + " and a.status=1; " +
                                     " select a.delivery_time,payment_mode,a.collected_amount AS collectedAmount from daily_collection a where delivery_id=" + deliveryId + " and customer_id=" + customerId + " and a.status=4; " +
                                     " select a.delivery_time,b.item_name,a.net_amount from delivery_items a inner join item b on b.item_id=a.item_id where delivery_id=" + deliveryId + " and customer_id=" + customerId + " and a.status=4;";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {

                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tblData;
        }
        public DataSet DSRMismatchReport(DateTime dateFrom, DateTime dateTo, int deliveryId, int employeeId, int routeId, int itemId)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        StringBuilder sQuery=new StringBuilder("select * from ( select a.customer_id,c.customer_name,c.payment_mode,max(round((a.net_amount/delivered_qty),2)) as Rate,sum(a.delivered_qty) as Soled ,b.Vehicle_no FROM delivery_items a ");
                        sQuery.Append($"inner join delivery b on b.delivery_id=a.delivery_id and b.employee_id={employeeId}");
                        sQuery.Append($"inner join customer c on c.customer_id=a.customer_id");
                        sQuery.Append($" where a.item_id={itemId} and CAST(a.delivery_time as date)>='{dateFrom}' and CAST(a.delivery_time as date)<='{dateTo}' and a.delivered_qty>0");
                        sQuery.Append($"  group by a.customer_id,c.customer_name,c.payment_mode,b.Vehicle_no) rep   order by payment_mode,customer_name;");

                        sQuery.Append($"  select e.customer_name, c.payment_mode as PaymentMode, qty as Qty,a.net_amount as NetAmount,d.item_name as ItemName  from delivery_items a ");
                        sQuery.Append($"  inner join delivery b on b.delivery_id =a.delivery_id  ");
                        sQuery.Append($"  inner join sales c on c.sales_id=a.sales_id  ");
                        sQuery.Append($"  inner join item d on d.item_id=a.item_id  ");
                        sQuery.Append($" inner join customer e on e.customer_id = a.customer_id");
                        sQuery.Append($" where a.delivery_id = b.delivery_id and a.delivery_time is not null and a.delivery_id = {deliveryId} and a.status = 4   order by c.payment_mode,e.customer_name--group by c.payment_mode,d.item_name");





                        using (SqlCommand cmd = new SqlCommand(sQuery.ToString(),conn))
                        {

                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(ds);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }
        public DataTable MiscSaleTotalDifference(DateTime dateFrom, DateTime dateTo)
        {
            DataTable tblData = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string sql = "select *,(sale-item) as diff from ( " +
                            " select a.sales_number, a.net_amount as sale ,sum(b.net_amount) as item,sales_date  from sales a " +
                            " inner join sales_item b  " +
                            " on b.sales_id=a.sales_id " +
                            " group by a.sales_number,sales_date , a.net_amount) " +
                            " rep where (sale-item)>1 and cast(sales_date as date)>='" + dateFrom.ToString("yyyy-MM-dd") + "' and cast(sales_date as date)<='" + dateTo.ToString("yyyy-MM-dd") + "' " +
                            " union all  " +
                            " select *,(sale-item) as diff from ( " +
                            " select a.sales_number, a.net_amount as sale ,sum(b.net_amount) as item,sales_date  from sales a " +
                            " inner join sales_item b  " +
                            " on b.sales_id=a.sales_id " +
                            " group by a.sales_number,sales_date , a.net_amount) " +
                            " rep where (item-sale)>1 and cast(sales_date as date)>='" + dateFrom.ToString("yyyy-MM-dd") + "' and cast(sales_date as date)<='" + dateTo.ToString("yyyy-MM-dd") + "'  order by sales_date desc ";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {

                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return tblData;
        }

    }
}
