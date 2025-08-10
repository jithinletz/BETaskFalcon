using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using BETask.Common;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using EDMXApp = BETask.APP;
using BETask.APP.EDMX;

namespace BETask.BAL
{
    public class SynchronizationBAL
    {
        SynchronizationDAL sync = new SynchronizationDAL(null);
        EDMXApp.DAL.SynchronizationAppDAL syncApp = new EDMXApp.DAL.SynchronizationAppDAL();

        public int CustomerOustatingCount()
        {
            try
            {
                return sync.CustomerOutstanding().Count;
            }
            catch { throw; }
        }


        public async Task<int> CustomerOutstandingRoutewise(int routeId = 0, int customerId = 0)
        {
            int res = 0;
            try
            {
                General.Error($"\n{General.batchNumber} - CustomerOutstandingRoutewise 15 minues");

                if (routeId == 0 && customerId == 0)
                {
                    RouteBAL routeBAL = new RouteBAL();
                    List<EDMX.route> listStoute = routeBAL.GetAllRoutes();
                    listStoute = listStoute.OrderBy(x => x.route_id).ToList();
                    if (General.lastRouteOutstandingUpdated == 0)
                    {
                        General.lastRouteOutstandingUpdated = listStoute[0].route_id;
                    }
                    else
                    {
                        bool finalroute = true;
                        foreach (EDMX.route rt in listStoute)
                        {
                            if (rt.route_id > General.lastRouteOutstandingUpdated)
                            {
                                finalroute = false;
                                General.lastRouteOutstandingUpdated = rt.route_id;
                                break;
                            }

                        }
                        if (finalroute)
                            General.lastRouteOutstandingUpdated = listStoute[0].route_id;
                    }
                    routeId = General.lastRouteOutstandingUpdated;

                }

                List<EDMX.SP_SyncCustomerOutstandingRoutewise_Result> result = sync.CustomerOutstandingRoutewise(routeId, customerId);
                if (result != null && result.Count > 0)
                {
                    List<DAL.CustomerOutstandingModel> listCustomerApp = new List<DAL.CustomerOutstandingModel>();
                    if (customerId == 0)
                    {

                        foreach (EDMX.SP_SyncCustomerOutstandingRoutewise_Result rs in result)
                        {

                            listCustomerApp.Add(new DAL.CustomerOutstandingModel
                            {
                                customer_id = rs.customer_id,
                                customer_name = rs.customer_name,
                                outstanding = rs.outstanding,
                                wallet_balance = rs.wallet_balance == null ? 0 : Convert.ToDecimal(rs.wallet_balance) //Convert.ToInt32(rs.outstanding) < 0 ? Convert.ToInt32(rs.outstanding)*-1 : 0
                            });
                        }
                    }
                    else
                    {
                        var custData = result.Where(x => x.customer_id == customerId).FirstOrDefault();
                        if (custData != null)
                        {
                            listCustomerApp.Add(new DAL.CustomerOutstandingModel
                            {
                                customer_id = custData.customer_id,
                                customer_name = custData.customer_name,
                                outstanding = custData.outstanding,
                                wallet_balance = Convert.ToDecimal(custData.wallet_balance) //Convert.ToInt32(rs.outstanding) < 0 ? Convert.ToInt32(rs.outstanding)*-1 : 0
                            });
                        }
                    }

                    string err = "";
                    if (listCustomerApp.Count > 0)
                        res = sync.SynchronizeCustomerOutstanding(listCustomerApp, out err);
                    if (!String.IsNullOrEmpty(err))
                    {
                        throw new Exception(err);
                    }

                }
                General.Error($"\n{General.batchNumber} - completed CustomerOutstandingRoutewise 15 minues");

            }
            catch (Exception ee)
            {
                //HelpBAL helpBAL = new HelpBAL();
                //helpBAL.EmailError($"IN Sync CustomerOutstanding {ee.Message}", ee.ToString(), false);
                General.Error(ee.ToString());
            }
            finally
            {
                //General.Action("Sync CustomerOutstanding then Wallet");

                WalletDAL walletDAL = new WalletDAL();
                List<EDMX.customer> listCustomer = walletDAL.GenerateWalletSync(0, customerId);


            }
            return res;
        }


        public int Route()
        {
            int res = 0;
            try
            {
                List<EDMX.route> listRoute = sync.Route();
                if (listRoute != null)
                {
                    List<EDMXApp.EDMX.route> listRouteApp = new List<EDMXApp.EDMX.route>();
                    foreach (EDMX.route route in listRoute)
                    {
                        listRouteApp.Add(new EDMXApp.EDMX.route
                        {
                            route_id = route.route_id,
                            status = route.status,
                            route_name = route.route_name
                        });
                    }
                    res = syncApp.SynchronizeRoute(listRouteApp);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
            }
            finally
            {
                General.Action("Sync Route");
            }
            return res;
        }

        internal void SaveDeliveryRequest(app_delivery_request objDeliveryRequest)
        {
            try
            {
                syncApp.SaveDeliveryRequest(objDeliveryRequest);
            }
            catch (Exception ex)
            {
                throw (ex);
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
                    reference_id = objDeliveryRequest.request_id,
                    summary = $" updating delivery request status requset id {objDeliveryRequest.request_id} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public int Building(int buildingId = 0)
        {
            int res = 0;
            try
            {
                List<EDMX.building> listBuilding = sync.Building(buildingId);
                if (listBuilding != null)
                {
                    List<EDMXApp.EDMX.building> listBuildingApp = new List<EDMXApp.EDMX.building>();
                    foreach (EDMX.building building in listBuilding)
                    {
                        listBuildingApp.Add(new EDMXApp.EDMX.building
                        {
                            building_id = building.building_id,
                            status = building.status,
                            building_name = building.building_name,
                            route = building.route,
                            area = building.area
                        });
                    }
                    res = syncApp.SynchronizeBuilding(listBuildingApp);
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
            }
            finally
            {
                General.Action("Sync Building");
            }
            return res;
        }
        public int OutstandingDailyCollection()
        {
            int result = 0;
            try
            {
                General.Error($"\n{General.batchNumber} - OutstandingDailyCollection");

                List<EDMXApp.EDMX.daily_collection> listDailyCollectionApp = syncApp.GetOutstandingDailyCollection_Pending();
                General.Error($"{listDailyCollectionApp.Count.ToString()} to be updated");
                if (listDailyCollectionApp != null && listDailyCollectionApp.Count > 0)
                {
                    List<int> collIds = new List<int>();
                    List<EDMX.daily_collection> listDailyCollection = new List<EDMX.daily_collection>();
                    foreach (EDMXApp.EDMX.daily_collection coll in listDailyCollectionApp)
                    {
                        collIds.Add(coll.daily_collection_id);
                        listDailyCollection.Add(new EDMX.daily_collection
                        {
                            collected_amount = coll.collected_amount,
                            customer_id = coll.customer_id,
                            delivery_time = coll.delivery_time,
                            employee_id = coll.employee_id,
                            net_amount = coll.net_amount,
                            payment_mode = coll.payment_mode,
                            remarks = coll.remarks == null ? coll.daily_collection_id.ToString() : $"{coll.remarks}-{coll.daily_collection_id.ToString()}",
                            status = 4,
                            daily_collection_id = coll.daily_collection_id,
                            division_id = coll.division_id,
                            is_deposit = coll.is_deposit,
                            is_refund = coll.is_refund,
                            old_leaf_count = coll.old_leaf_count
                        });
                    }

                    if (syncApp.UpdateOutstandingDailyCollection_Pending(collIds))
                    {
                        try
                        {
                            List<EDMX.customer> listUpdatedCustomer = new List<EDMX.customer>();
                            result = sync.DailyCollectionOutstanding(listDailyCollection, ref listUpdatedCustomer);

                            //updating cloud balance on spot
                            // SyncLatestWallet(listUpdatedCustomer);

                        }
                        catch (Exception ee)
                        {
                            General.Error($"Status restate to 1 {ee.ToString()}");

                            syncApp.UpdateOutstandingDailyCollection_Pending(collIds, 1);
                        }
                    }
                }
                General.Error($"\n{General.batchNumber} - Completed OutstandingDailyCollection {listDailyCollectionApp.Count}");

            }
            catch (Exception ee)
            {
                General.Error(ee.ToString());
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync OutstandingDailyCollection {ee.Message}", ee.ToString(), false);
                throw;
            }
            return result;
        }
        public async void SyncLatestWallet(List<EDMX.customer> listCustomer)
        {
            try
            {
                List<APP.EDMX.customer> listAppCustomer = new List<customer>();
                foreach (EDMX.customer cust in listCustomer)
                {
                    listAppCustomer.Add(new customer
                    {
                        customer_id = cust.customer_id,
                        wallet_balance = cust.wallet_balance ?? 0
                    });
                    General.Error($"Recharge synced :{cust.customer_id} - {cust.customer_name} - {cust.wallet_balance}");
                }
                if (listAppCustomer.Count > 0)
                {
                    APP.DAL.WalletAppDAL walletAppDAL = new EDMXApp.DAL.WalletAppDAL();
                    await walletAppDAL.SyncWallet(listAppCustomer);
                }
            }
            catch { }
        }


        public int CustomerDateUpdation()
        {

            try
            {
                return sync.CustomerDateUpdation();

            }
            catch (Exception ee)
            {
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync customer Date Updation {ee.Message}", ee.ToString(), false);
                throw;
            }

        }
        public int DeliveryReturnItems()
        {
            int result = 0;
            try
            {
                General.Error($"\n{General.batchNumber} - Started DeliveryReturnItems ");

                //sync.DeliveryReturnItems();
                List<APP.EDMX.delivery_return> listDeliveryRerutnApp = syncApp.GetDeliveryReturPenidng();
                if (listDeliveryRerutnApp != null && listDeliveryRerutnApp.Count > 0)
                {
                    List<int> collIds = new List<int>();
                    List<EDMX.delivery_return> listDeliveryReturn = new List<EDMX.delivery_return>();
                    foreach (EDMXApp.EDMX.delivery_return app in listDeliveryRerutnApp)
                    {
                        collIds.Add(app.delivery_return_id);
                        listDeliveryReturn.Add(new EDMX.delivery_return
                        {
                            customer_id = app.customer_id,
                            return_date = app.return_date,
                            item_id = app.item_id,
                            qty = app.qty,
                            employee_id = app.employee_id,
                            status = app.status,
                            return_type = app.return_type,
                            server_time = app.server_time,
                            remarks = app.remarks ?? app.delivery_return_id.ToString(),//If null then cloud return id as remarks
                            route_id = app.route_id

                        });
                    }
                    if (syncApp.UpdateDeliveryReturnStatus_Pending(collIds))
                    {
                        try
                        {
                            result = sync.SyncDeliveryReturnIytems(listDeliveryReturn);
                        }
                        catch (Exception ee)
                        {
                            syncApp.UpdateDeliveryReturnStatus_Pending(collIds, 1);
                        }
                    }
                }
                General.Error($"\n{General.batchNumber} - Started DeliveryReturnItems Completed. count {listDeliveryRerutnApp.Count}");

            }
            catch (Exception ee)
            {
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync DeliveryReturnItems {ee.Message}", ee.ToString(), false);
                throw;
            }
            return result;
        }
        public void Delivery()
        {
            try
            {
                General.Error($"\n{General.batchNumber} - Started Delivery ");

                DeliveryBAL deliveryBAL = new DeliveryBAL();


                List<EDMX.delivery> listDelivery = deliveryBAL.GetDeliveryDataByDate(General.ConvertDateServerFormat(DateTime.Today.AddDays(-1)));
                ExecuteSync(listDelivery);

                listDelivery = deliveryBAL.GetDeliveryDataByDate(General.ConvertDateServerFormat(DateTime.Today));
                ExecuteSync(listDelivery);

                General.Error($"\n{General.batchNumber} - Completed Delivery count {listDelivery.Count} ");

            }
            catch
            {
                throw;
            }
        }

        private void ExecuteSync(List<EDMX.delivery> listDelivery)
        {
            DeliveryBAL deliveryBAL = new DeliveryBAL();

            foreach (EDMX.delivery delivery in listDelivery)
            {

                int deliveryId = delivery.delivery_id;


            }
        }

        public void Delivery(DateTime date)
        {
            try
            {
                DeliveryBAL deliveryBAL = new DeliveryBAL();
                List<EDMX.delivery> listDelivery = deliveryBAL.GetDeliveryDataByDate(General.ConvertDateServerFormat(date));
                foreach (EDMX.delivery delivery in listDelivery)
                {
                    int deliveryId = delivery.delivery_id;

                }
            }
            catch (Exception ee)
            {
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync Delivery({date}) - {ee.Message}", ee.ToString(), false);
                throw;
            }
        }
        public void DeliveryFromApp()
        {
            try
            {
                General.Error($"\n{General.batchNumber} - Started DeliveryFromApp ");
                List<EDMX.delivery_items> listDeliveryItems = new List<EDMX.delivery_items>();

                List<EDMXApp.EDMX.delivery_items> listDeliveryApp = syncApp.GetPendingDeliveryFromApp(General.ConvertDateServerFormatWithStartTime(DateTime.Today.AddDays(-1)), General.ConvertDateServerFormatWithEndTime(DateTime.Today.AddDays(1)));
                if (listDeliveryApp != null && listDeliveryApp.Count > 0)
                {
                    foreach (EDMXApp.EDMX.delivery_items di in listDeliveryApp)
                    {
                        listDeliveryItems.Add(new EDMX.delivery_items
                        {
                            delivery_id = di.delivery_id,
                            customer_id = di.customer_id,
                            item_id = di.item_id,
                            qty = di.qty,
                            rate = di.rate,
                            gross_amount = di.gross_amount,
                            discount = di.discount,
                            total_beforvat = di.total_beforvat,
                            vat_amount = di.vat_amount,
                            net_amount = di.net_amount,
                            status = 3,
                            delivery_time = di.delivery_time,
                            delivered_qty = di.delivered_qty,
                            delivery_leaf = di.delivery_leaf,
                            division_id = di.division_id,
                            is_deposit = di.is_deposit,
                            payment_mode = di.payment_mode

                        });
                    }
                    DeliveryDAL deliveryDAL = new DeliveryDAL();
                    List<string> listUpdated = deliveryDAL.UpdateDeliveredItemsFromAp(listDeliveryItems);
                    syncApp.UpdateDeliveryImportedToLocalStatus(listUpdated);
                }
                General.Error($"\n{General.batchNumber} - Delievery APP  Sync completed synced={listDeliveryApp.Count}");

            }
            catch (Exception ee)
            {
                General.Error(ee.Message);
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync DeliverFromApp", ee.ToString(), false);
                throw;
            }
        }

        //If anything not updated in previous date
        public void DeliveryFromApp(int deliveryId)
        {
            try
            {
                List<EDMX.delivery_items> listDeliveryItems = new List<EDMX.delivery_items>();

                List<EDMXApp.EDMX.delivery_items> listDeliveryApp = syncApp.GetPendingDeliveryFromApp(deliveryId);
                if (listDeliveryApp != null && listDeliveryApp.Count > 0)
                {
                    foreach (EDMXApp.EDMX.delivery_items di in listDeliveryApp)
                    {
                        listDeliveryItems.Add(new EDMX.delivery_items
                        {
                            delivery_id = di.delivery_id,
                            customer_id = di.customer_id,
                            item_id = di.item_id,
                            qty = di.qty,
                            rate = di.rate,
                            gross_amount = di.gross_amount,
                            discount = di.discount,
                            total_beforvat = di.total_beforvat,
                            vat_amount = di.vat_amount,
                            net_amount = di.net_amount,
                            status = 3,
                            delivery_time = di.delivery_time,
                            delivered_qty = di.delivered_qty,
                            delivery_leaf = di.delivery_leaf,
                            division_id = di.division_id

                        });
                    }
                    DeliveryDAL deliveryDAL = new DeliveryDAL();
                    General.Error("Delievery APP Sync Started");
                    List<string> listUpdated = deliveryDAL.UpdateDeliveredItemsFromAp(listDeliveryItems);
                    syncApp.UpdateDeliveryImportedToLocalStatus(listUpdated);
                    General.Error("Delievery APP  Sync Success");
                }
            }
            catch (Exception ee)
            {
                General.Error(ee.Message);
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync DeliverFromApp", ee.ToString(), false);
                throw;
            }
        }
        public List<EDMXApp.EDMX.RPT_CollectionStatus_Result> RPTCollectionStatus(DateTime toDate)
        {

            return syncApp.RPTCollectionStatus(toDate);

        }
        public List<EDMXApp.EDMX.RPT_DeliveryReturnStatus_Result> RPTDeliveryReturnStatus(DateTime toDate)
        {

            return syncApp.RPTDeliveryReturnStatus(toDate);

        }
        public List<EDMXApp.EDMX.RPT_DeliveryStatus_Result> RPTDeliveryStatus(DateTime toDate)
        {

            return syncApp.RPTDeliveryStatus(toDate);

        }
        public void RedeemedCoupon()
        {

            try
            {
                General.Error($"\n{General.batchNumber} - Started RedeemedCoupon ");

                List<EDMXApp.EDMX.coupon_leaf> listLeafAPP = syncApp.GetRedeemedCoupons();

                List<EDMX.coupon_leaf> listLeaf = new List<EDMX.coupon_leaf>();
                int[] lfIds = new int[listLeafAPP.Count];
                if (listLeafAPP != null && listLeafAPP.Count > 0)
                {

                    foreach (EDMXApp.EDMX.coupon_leaf lfapp in listLeafAPP)
                    {
                        listLeaf.Add(new EDMX.coupon_leaf
                        {
                            leaf_id = lfapp.leaf_id,
                            remarks = lfapp.remarks,
                            status = lfapp.status,
                            redeem_date = lfapp.redeem_date
                        });

                    }
                    List<int> updatesLeafs = sync.ReddemedCoupon(listLeaf);
                    syncApp.UpdateRedeemedCouponStatus(updatesLeafs.ToArray());

                }
                General.Error($"\n{General.batchNumber} - Completed RedeemedCoupon Count {listLeafAPP.Count}");

            }
            catch (Exception ee)
            {
                General.Error(ee.Message);
                HelpBAL helpBAL = new HelpBAL();
                helpBAL.EmailError($"IN Sync RedeemCoupon", ee.ToString(), false);
                throw;
            }
        }

        public void WalletGeneration()
        {

            try
            {
                General.Error($"\n{General.batchNumber} - WalletGeneration");

                WalletDAL walletDAL = new WalletDAL();
                List<EDMX.customer> listCustomer = walletDAL.GenerateWalletNumber();

                General.Error($"\n{General.batchNumber} - completed WalletGeneration");

            }
            catch (Exception ee)
            {
                throw;
            }

        }
        public int WalletGenerationAPP(List<EDMX.customer> listCustomer)
        {
            int result = 0;
            try
            {

                if (listCustomer != null && listCustomer.Count > 0)
                {
                    List<BETask.APP.EDMX.customer> listCustomerApp = new List<EDMXApp.EDMX.customer>();
                    foreach (EDMX.customer cs in listCustomer)
                    {
                        listCustomerApp.Add(new EDMXApp.EDMX.customer { customer_id = cs.customer_id, wallet_number = cs.wallet_number });
                    }
                    APP.DAL.WalletAppDAL walletAppDAL = new EDMXApp.DAL.WalletAppDAL();
                    result = walletAppDAL.GenerateWalletNumber(listCustomerApp);
                }
                else
                {
                    throw new Exception("Nothing to update");
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return result;

        }

        public int ReRunDeliveryReturn(string returnDate)
        {
            //Deleting duplicate records
            try
            {
                return sync.ReRunDeliveryReturn(returnDate);
            }
            catch { throw; }
        }

        public bool CloudConnectionStatus(string cloudConnection)
        {
            return syncApp.CloudConnectionStatus(cloudConnection);
        }

        public async Task<int> UpdateLocation(int routeId, string range, bool isEnable)
        {
            return await sync.UpdateRouteLocation(routeId, range, isEnable);
        }

        public async Task<int> UpdateCreditLimit(int routeId, decimal creditLimit)
        {
            return await sync.UpdateRouteCreditLimit(routeId, creditLimit);

        }

        public async Task PrintOnlineReportAsync(DateTime dateFrom, DateTime dateTo, string status, int customerId)
        {
            try
            {
                string header = $"Date between {dateFrom.ToString("dd/MM/yyyy")} and {dateTo.ToString("dd/MM/yyyy")} ";
                DataTable tblData = await sync.GetOnlineTransactionReport(General.ConvertDateServerFormat(dateFrom), General.ConvertDateServerFormat(dateTo), status, 0);
                if (tblData != null && tblData.Rows.Count > 0)
                {

                    BETask.Report.DSReports.OnlinRechargeDataTable onlinRecharges = new Report.DSReports.OnlinRechargeDataTable();
                    DataTable tblRecharges = onlinRecharges.Clone();
                    foreach (DataRow row in tblData.Rows)
                    {
                        DataRow dr = tblRecharges.NewRow();
                        dr["Date"] =Convert.ToDateTime(row["start_date"]).ToString("dd/MM/yyyy hh:mm tt");
                        dr["CustomerName"] = row["customer_name"];
                        dr["ProfileName"] = row["profileName"];
                        dr["Route"] = row["route_name"];
                        dr["Employee"] = row["employee"];
                        dr["Amount"] =Convert.ToDecimal(Convert.ToString( row["amount"]));
                        dr["NetAmount"] = Convert.ToDecimal(Convert.ToString(row["amount_received"]));

                        tblRecharges.Rows.Add(dr);
                    }
                    Report.ReportForm reportForm = new Report.ReportForm(Report.ReportForm.EnumReportType.OnlineReport, header, tblRecharges);
                    reportForm.Show();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region App Delivery Request
        public List<app_delivery_request> GetAppDeliveryRequest(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                List<app_delivery_request> listAppDeliveryRequest = new List<app_delivery_request>();
                listAppDeliveryRequest = syncApp.GetAppDeliveryRequest(dateFrom, dateTo);
                return listAppDeliveryRequest;
            }
            catch { throw; }
        }
        public app_delivery_request GetAppDeliveryRequestByRequestId(int requestId = 0)
        {
            try
            {
                app_delivery_request objAppDeliveryRequest = new app_delivery_request();
                objAppDeliveryRequest = syncApp.GetAppDeliveryRequestByRequestId(requestId);
                return objAppDeliveryRequest;
            }
            catch { throw; }
        }

        public DataTable GetAppDeliveryItemByRequestId(int requestId = 0)
        {
            try
            {
                return syncApp.GetAppDeliveryItemByRequestId(requestId);
            }
            catch { throw; }
        }
        #endregion

    }
}
