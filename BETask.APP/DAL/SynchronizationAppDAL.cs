using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.APP.Model;
using System.Data.SqlClient;

namespace BETask.APP.DAL
{
    public class SynchronizationAppDAL
    {
        public int SynchronizeCustomerOutstanding(List<CustomerOutstandingModel> listCustomerOutstanding, out string error)
        {
            error = string.Empty;
            int result = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    //  List<customer> listCust = context.customer.Where(x => x.status == 1).ToList();
                    foreach (CustomerOutstandingModel cOutstanding in listCustomerOutstanding)
                    {
                        var cust = context.customer.Where(x => x.status == 1 && x.customer_id == cOutstanding.customer_id).FirstOrDefault();// listCust.Where(x => x.customer_id == cOutstanding.customer_id).FirstOrDefault();
                        if (cust != null)
                        {
                            result++;
                            cust.outstanding_amount = cOutstanding.outstanding;
                            cust.wallet_balance = (!String.IsNullOrEmpty(cust.wallet_number) && cust.status == 1) ? (cOutstanding.wallet_balance != 0 ? cOutstanding.wallet_balance : cust.wallet_balance) : cust.wallet_balance;
                            context.Entry(cust).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                    }

                }
            }
            catch (Exception ee)
            {

                error = ee.ToString();
                return result;
            }
            return result;
        }

        public int SynchronizeRoute(List<route> listRoute)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    List<route> listApproute = context.route.ToList();
                    foreach (route _route in listRoute)
                    {
                        var aRt = listApproute.Where(x => x.route_id == _route.route_id).FirstOrDefault();
                        if (aRt != null)
                        {
                            result++;
                            aRt.route_name = _route.route_name;
                            aRt.status = _route.status;
                            context.Entry(aRt).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            result++;
                            context.Entry(_route).State = EntityState.Added;
                            context.SaveChanges();
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        public int SynchronizeRouteGroup(List<route_group> listRoute)
        {
            int result = 0;
            try
            {
                int routeId = listRoute[0].route_id;
                using (var context = new betaskdbEntitiesAPP())
                {
                    context.route_group.RemoveRange(context.route_group.Where(x => x.route_id == routeId).ToList());
                    context.SaveChanges();
                    context.route_group.AddRange(listRoute);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        public int SynchronizeBuilding(List<building> listBuilding)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    List<building> listbuilding = context.building.ToList();
                    foreach (building _building in listBuilding)
                    {
                        var aRt = listbuilding.Where(x => x.building_id == _building.building_id).FirstOrDefault();
                        if (aRt != null)
                        {
                            result++;
                            aRt.building_name = _building.building_name;
                            aRt.status = _building.status;
                            aRt.route = _building.route;
                            aRt.area = _building.area;
                            context.Entry(aRt).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            result++;
                            context.Entry(_building).State = EntityState.Added;
                            context.SaveChanges();
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        public List<EDMX.daily_collection> GetOutstandingDailyCollection_Pending()
        {
            List<EDMX.daily_collection> listCollection = null;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    listCollection = context.daily_collection.Where(x => x.status == 1 && x.delivery_id == null).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listCollection;

        }
        public bool UpdateOutstandingDailyCollection_Pending(List<int> collIds, int status = 4)
        {

            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (int ids in collIds)
                            {
                                var xCollIds = context.daily_collection.Single(x => x.daily_collection_id == ids);
                                if (xCollIds != null)
                                {
                                    xCollIds.status = status;
                                }
                            }
                            context.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return false;

        }
        public List<delivery_return> GetDeliveryReturPenidng()
        {
            List<delivery_return> listDeliveryReturn = new List<delivery_return>();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    listDeliveryReturn = context.delivery_return.Where(x => x.status == 1).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listDeliveryReturn;
        }
        public bool UpdateDeliveryReturnStatus_Pending(List<int> collIds, int status = 4)
        {

            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (int ids in collIds)
                            {
                                var xCollIds = context.delivery_return.Single(x => x.delivery_return_id == ids);
                                if (xCollIds != null)
                                {
                                    xCollIds.status = status;
                                }
                            }
                            context.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return false;

        }

        public void SaveDeliveryRequest(app_delivery_request objDeliveryRequest)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    context.Entry(objDeliveryRequest).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<EDMX.coupon_leaf> GetRedeemedCoupons()
        {
            List<EDMX.coupon_leaf> listLeaf = new List<coupon_leaf>();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    listLeaf = context.coupon_leaf.Where(x => x.status == 4).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listLeaf;
        }
        public void UpdateRedeemedCouponStatus(int[] leafIds)
        {
            try
            {
                //  int[] collIds = listDailyCollection.Select(x => x.daily_collection_id).ToArray();
                using (var context = new betaskdbEntitiesAPP())
                {
                    var listLeaf = context.coupon_leaf.Where(
            i => leafIds.Contains(i.leaf_id)
            ).ToList();

                    foreach (coupon_leaf lf in listLeaf)
                    {
                        lf.status = 5;
                    }
                    context.SaveChanges();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        /// <summary>
        /// Not updated to local db items
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<delivery_items> GetPendingDeliveryFromApp(DateTime dateFrom, DateTime dateTo)
        {
            List<delivery_items> delivery = new List<delivery_items>();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    //delivery = context.delivery.AsNoTracking().Include(i => i.delivery_items.Where(d => d.delivery_item_id_local == -1)).AsNoTracking().
                    //    Where(x => x.delivery_date >= dateFrom && x.delivery_date <= dateTo && x.customer_count > 0 && x.status != 2).ToList();
                    delivery = context.delivery_items.AsNoTracking().Where(d => d.delivery_item_id_local == -1 && d.delivery_time >= dateFrom && d.delivery_time <= dateTo).ToList();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return delivery;
        }
        public List<delivery_items> GetPendingDeliveryFromApp(int deliveryId)
        {
            List<delivery_items> delivery = new List<delivery_items>();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    
                    delivery = context.delivery_items.AsNoTracking().Where(d => d.delivery_item_id_local == -1 && d.delivery_id==deliveryId ).ToList();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return delivery;
        }
        public void UpdateDeliveryImportedToLocalStatus(List<string> listUpdated)
        {
            try
            {
                //  int[] collIds = listDailyCollection.Select(x => x.daily_collection_id).ToArray();
                using (var context = new betaskdbEntitiesAPP())
                {

                    foreach (string li in listUpdated)
                    {
                        int deliveryIdApp = Convert.ToInt32(li.Split('-')[0]);
                        int deliveryId = Convert.ToInt32(li.Split('-')[1]);
                        var xDelivery = context.delivery_items.Where(x => x.delivery_item_id == deliveryIdApp).FirstOrDefault();
                        xDelivery.delivery_item_id_local = deliveryId;
                    }
                    context.SaveChanges();

                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void UpdateDeliveredCustomerWalletBalance(List<customer> listCustomer)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    foreach (customer cs in listCustomer)
                    {
                        customer xCustomer = context.customer.Where(x => x.customer_id == cs.customer_id).FirstOrDefault();
                        context.customer.Attach(xCustomer);
                        xCustomer.wallet_balance = cs.wallet_balance??0;
                        context.Entry(xCustomer).Property(x => x.wallet_balance).IsModified = true;
                    }
                    context.SaveChanges();
                }
            }
            catch { }
        }

        public List<RPT_DeliveryStatus_Result> RPTDeliveryStatus(DateTime toDate)
        {
            List<RPT_DeliveryStatus_Result> result = null;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    result = context.RPT_DeliveryStatus(toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public List<RPT_DeliveryReturnStatus_Result> RPTDeliveryReturnStatus(DateTime toDate)
        {
            List<RPT_DeliveryReturnStatus_Result> result = null;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    result = context.RPT_DeliveryReturnStatus(toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public List<RPT_CollectionStatus_Result> RPTCollectionStatus(DateTime toDate)
        {
            List<RPT_CollectionStatus_Result> result = null;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    result = context.RPT_CollectionStatus(toDate).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }


        public bool CloudConnectionStatus(string cloudConnection)
        {

            bool resp = false;

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(cloudConnection))
            {
                try
                {
                    conn.Open();
                    resp = true;
                }
                catch (Exception ee)
                {
                    resp = false;
                }
            }
            return resp;
        }

        public List<app_delivery_request> GetAppDeliveryRequest(DateTime dateFrom, DateTime dateTo)
        {
            List<app_delivery_request> listAppDeliveryRequest = new List<app_delivery_request>();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    listAppDeliveryRequest = context.app_delivery_request.Where(x => x.request_time >= dateFrom && x.request_time <= dateTo && x.status ==1).ToList();
                }

            }
            catch
            {
                throw;
            }

            return listAppDeliveryRequest;
        }

        public app_delivery_request GetAppDeliveryRequestByRequestId(int requestId)
        {
            app_delivery_request objDeliveryRequest = new app_delivery_request();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    objDeliveryRequest = context.app_delivery_request.Where(x => x.request_id == requestId).FirstOrDefault();
                }

            }
            catch
            {
                throw;
            }

            return objDeliveryRequest;
        }

        public DataTable GetAppDeliveryItemByRequestId(int requestId)
        {
            try
            {

                DataTable tblDuplications = new DataTable();
                {
                    string sql = $"select d.request_id as request_id, d.item_id as ItemId, i.item_name as ItemName,d.qty as Qty,d.rate as Rate,d.net_amount as Net FROM app_delivery_request_items d left join item i on i.item_id=d.item_id WHERE request_id={requestId}";
                    using (var context = new betaskdbEntitiesAPP())
                    {

                        using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                                {
                                    adr.Fill(tblDuplications);
                                }
                            }
                        }
                    }
                }
                return tblDuplications;
            }
            catch
            {
                throw;
            }


        }
    }
}