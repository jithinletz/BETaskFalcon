using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Linq;


namespace BETask.BAL
{
    public class DeliveryBAL
    {

        DeliveryDAL deliveryDAL = new DeliveryDAL();
        SynchronizationBAL synchronizationBAL = new SynchronizationBAL();
        public void GenerateDeliveryId(DateTime dateFrom, DateTime dateTo, List<EDMX.employee> listEmployee)
        {
            try
            {
                List<int> listDeliveryIds = deliveryDAL.GenerateDeliveryId(General.ConvertDateServerFormat(dateFrom), General.ConvertDateServerFormat(dateTo), listEmployee);

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
                    reference_id = 0,
                    summary = $" Generate delivery ids date between {dateFrom} and {dateTo}   ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public DataTable GetLocalDeliveryReport(int deliveryId)
        {
            try
            {
                return deliveryDAL.GetLocalDeliveryReport(deliveryId);
            }
            catch { throw; }
        }


        public void GenerateDeliveryId(DateTime date)
        {
            try
            {
                List<int> listDeliveryIds = deliveryDAL.GenerateDeliveryId(date);

            }
            catch (Exception ee)
            {
                throw;
            }
            finally
            {
                DAL.DAL.SynchronizationDAL sync = new SynchronizationDAL(null);
                sync.CustomerOutstanding();

                var st = new StackTrace();
                var sf = st.GetFrame(1);
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" Generate delivery ids date between {date}   ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public int SaveDelivery(EDMX.delivery delivery, List<EDMX.delivery_items> deliveryItems, List<EDMX.delivery_item_summary> delivery_item_summary, bool afterDeliveryUpdate = false)
        {
            int deliveryId = 0;
            try
            {
                deliveryId = deliveryDAL.SaveDelivery(delivery, deliveryItems, delivery_item_summary, afterDeliveryUpdate);
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
                    reference_id = deliveryId,
                    summary = $" Saving Delivery Employee {delivery.employee_id}, Vehicle={delivery.vehicle_no}, Route={delivery.delivery_route},Customers={delivery.customer_count}, Net Amount={delivery.net_amount} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return deliveryId;
        }


        public int SaveDeliveryItemSummary(List<EDMX.delivery_item_summary> delivery_item_summary, int _deliveryId)
        {
            int deliveryItemSummaryId = 0;
            try
            {
                deliveryItemSummaryId = deliveryDAL.SaveDeliveryItemSummaryFromDelivery(delivery_item_summary, _deliveryId);
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
                    reference_id = _deliveryId,
                    summary = $"Saving Delivery item summary ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return deliveryItemSummaryId;
        }

        public List<EDMX.delivery> SearchDeliveryIdGenerated(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return deliveryDAL.SearchDeliveryIdGenerated(dateFrom, dateTo);
            }
            catch { throw; }
        }

        public List<EDMX.delivery> SearchDelivery(DateTime dateFrom, DateTime dateTo, int empId)
        {
            try
            {
                return deliveryDAL.SearchDelivery(dateFrom, dateTo, empId);
            }
            catch { throw; }
        }
        public int GetDeliveryId(DateTime _date, int _empId)
        {
            try
            {
                return deliveryDAL.GetDeliveryId(_date, _empId);
            }
            catch { throw; }
        }
        public decimal GetPreviousDayBalance(DateTime _date, int _empId, int _ItemId)
        {
            try
            {
                return deliveryDAL.GetPreviousDayBalance(_date, _empId, _ItemId);
            }
            catch { throw; }
        }
        public decimal GetPreviousDayBalanceFromDelivery(DateTime _date, int _empId, int _ItemId)
        {
            try
            {
                return deliveryDAL.GetPreviousDayBalanceFromDelivery(_date, _empId, _ItemId);
            }
            catch { throw; }
        }
        public decimal GetPreviousDayBalanceandLoading(DateTime _date, int deliveryId, int _empId, int _itemId, ref decimal totalLoading, ref decimal totalEmpty, ref decimal totalShort, ref decimal totalExtra, ref bool firstLoad)
        {
            try
            {
                return deliveryDAL.GetPreviousDayBalanceandLoading(_date, deliveryId, _empId, _itemId, ref totalLoading, ref totalEmpty, ref totalShort, ref totalExtra, ref firstLoad);
            }
            catch { throw; }
        }
        public decimal GetSoldQuantity(int _deliveryId, int _ItemId)
        {
            try
            {
                return deliveryDAL.GetSoldQuantity(_deliveryId, _ItemId);
            }
            catch { throw; }
        }

        public EDMX.delivery GetDeliveryDetails(int deliveryId)
        {
            try
            {
                return deliveryDAL.GetDeliveryDetails(deliveryId);
            }
            catch { throw; }
        }
        public DataSet GetDelivery(int deliveryId)
        {
            try
            {
                return deliveryDAL.GetDelivery(deliveryId);
            }
            catch { throw; }
        }
        public List<BETask.DAL.Model.DeliveryLoadReportModel> GetDeliveryLoadDetails(DateTime _dateFrom, DateTime _dateTo, int _empId, int _itemId)
        {
            try
            {
                return deliveryDAL.GetDeliveryLoadDetails(_dateFrom, _dateTo, _empId, _itemId);
            }
            catch (Exception ex) { throw; }
        }
        public List<EDMX.delivery_item_summary> GetDeliveryItemSummaryById(int deliveryId)
        {
            try
            {
                return deliveryDAL.GetDeliveryItemSummaryById(deliveryId);
            }
            catch { throw; }
        }

        public EDMX.delivery GetDeliveryDetails(int employeeId, int routeId, DateTime date, bool getDliveryIdOnly = false)
        {
            try
            {
                return deliveryDAL.GetDeliveryDetails(employeeId, routeId, date, getDliveryIdOnly);
            }
            catch { throw; }
        }
        public void DistinctRout_Vehicle(out List<string> route, out List<string> vehicle)
        {
            try
            {
                deliveryDAL.DistinctRout_Vehicle(out route, out vehicle);
            }
            catch { throw; }
        }
        public List<EDMX.delivery> GetDeliveryDataByDate(DateTime deliveryDate)
        {
            try
            {
                return deliveryDAL.GetDeliveryDataByDate(deliveryDate);
            }
            catch { throw; }
        }

        public void UpdateDeliveredItems(List<EDMX.delivery_items> listDeliveryItems, List<EDMX.daily_collection> listDailyCollection = null, int deposit = 2)
        {
            try
            {
                if (deposit == 2)
                    deliveryDAL.UpdateDeliveredItems(listDeliveryItems, listDailyCollection);
                else
                    deliveryDAL.UpdateDeliveredItemsBottleDeposit(listDeliveryItems, listDailyCollection);

            }
            catch { throw; }
        }
        public void UpdateDeliveredItemsRecheck(List<EDMX.delivery_items> listDeliveryItems, List<EDMX.daily_collection> listDailyCollection = null)
        {
            try
            {
                deliveryDAL.UpdateDeliveredItemsRecheck(listDeliveryItems, listDailyCollection);

            }
            catch { throw; }
        }
        public void EnableSpotDelivery(int deliveryId)
        {
            try
            {

                deliveryDAL.EnableSpotDelivery(deliveryId);
            }
            catch
            {
                throw;
            }
        }

        public void DisableSpotDelivery(int deliveryId)
        {
            try
            {
                deliveryDAL.DisableSpotDelivery(deliveryId);
            }
            catch
            {
                throw;
            }
        }



        public void PrintDelivery(int deliveryId)
        {
            try
            {
                EDMX.delivery delivery = deliveryDAL.GetDeliveryDetails(deliveryId);
                if (delivery != null)
                {
                    DataTable tblDeliveryHead = new DataTable();
                    DataTable tblDeliveryItems = new DataTable();
                    DataTable tblDeliveryItemSummary = new DataTable();
                    BETask.Report.DSReports.DeliveryHeadDataTable deliveryHeadDataTable = new Report.DSReports.DeliveryHeadDataTable();
                    tblDeliveryHead = deliveryHeadDataTable.Clone();
                    BETask.Report.DSReports.DeliveryItemsDataTable deliveryItemDataTable = new Report.DSReports.DeliveryItemsDataTable();
                    tblDeliveryItems = deliveryItemDataTable.Clone();
                    BETask.Report.DSReports.DeliveryItemSummaryDataTable deliveryItemSummaryDataTable = new Report.DSReports.DeliveryItemSummaryDataTable();
                    tblDeliveryItemSummary = deliveryItemSummaryDataTable.Clone();

                    DataRow rowHead = tblDeliveryHead.NewRow();
                    rowHead["DelieryId"] = delivery.delivery_id;
                    rowHead["DeliveryDate"] = General.ConvertDateAppFormat(delivery.delivery_date);
                    rowHead["Employee"] = String.Format("{0} {1}", delivery.employee.first_name, delivery.employee.last_name);
                    rowHead["VehicelNo"] = delivery.vehicle_no;
                    rowHead["Route"] = delivery.delivery_route;
                    rowHead["CustomerCount"] = delivery.customer_count;
                    rowHead["NetAmount"] = delivery.net_amount;
                    rowHead["Remarks"] = delivery.remarks;
                    tblDeliveryHead.Rows.Add(rowHead);

                    foreach (EDMX.delivery_items item in delivery.delivery_items)
                    {
                        DataRow rowItem = tblDeliveryItems.NewRow();
                        rowItem["ItemName"] = item.item.item_name;
                        rowItem["Packing"] = item.item.uom_setting.uom_name;
                        rowItem["Qty"] = item.qty;
                        rowItem["CustomerName"] = item.customer.customer_name;
                        rowItem["NetAmount"] = item.net_amount;
                        tblDeliveryItems.Rows.Add(rowItem);
                    }

                    foreach (EDMX.delivery_item_summary summary in delivery.delivery_item_summary)
                    {
                        DataRow rowSummary = tblDeliveryItemSummary.NewRow();
                        rowSummary["ItemName"] = summary.item.item_name;
                        rowSummary["Packing"] = summary.item.uom_setting.uom_name;
                        rowSummary["Qty"] = summary.qty;
                        rowSummary["AddQty"] = GetDeliveryItemSummaryAdditionalItemQty(deliveryId, summary.item_id);
                        tblDeliveryItemSummary.Rows.Add(rowSummary);
                    }

                    if (tblDeliveryHead != null && tblDeliveryHead.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.DeliveryInvoice, "", tblDeliveryHead, tblDeliveryItems, tblDeliveryItemSummary);
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
                    reference_id = deliveryId,
                    summary = $" Print Delivery  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintCustomerDeliveryDaybook(string header, int deliveryId, DataTable tblHeader, DataTable tblDetail)
        {
            try
            {
                if (tblHeader != null && tblDetail.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.CustomerDeliveryDaybook, header, tblHeader, tblDetail);
                    reportForm.Show();
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
                    reference_id = deliveryId,
                    summary = $" Print Customer Delivery Daybook ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintCustomerDeliveryDaybook(string header, int deliveryId, DataTable tblHeader, DataTable tblDetail, DataTable tblPaymentmodes, DataTable tblLoading)
        {
            try
            {
                if (tblHeader != null && tblDetail.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.CustomerDeliveryDaybook, header, tblHeader, tblDetail, tblPaymentmodes, tblLoading);
                    reportForm.Show();
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
                    reference_id = deliveryId,
                    summary = $" Print Customer Delivery Daybook ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public int SaveDeliveryReturn(EDMX.delivery_return delivery)
        {
            int deliveryId = 0;
            try
            {
                deliveryDAL.SaveDeliveryReturn(delivery);
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
                    reference_id = delivery.delivery_return_id,
                    summary = $" Saving Delivery Return Employee {delivery.employee_id} , Customer {delivery.customer_id} ,Item {delivery.item_id} , Qty {delivery.qty} , Date {delivery.return_date} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return deliveryId;
        }
        public int ApproveDeliveryReturn(List<EDMX.delivery_return> delivery)
        {
            int deliveryId = 0;
            try
            {
                deliveryDAL.ApproveDeliveryReturn(delivery);
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
                    reference_id = deliveryId,
                    summary = $" Approving Delivery Return for the date {delivery[0].return_date} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return deliveryId;
        }

        public void UpdateDailyCollection(EDMX.daily_collection coll)
        {

            try
            {
                deliveryDAL.UpdateDailyCollection(coll);
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
                    reference_id = coll.delivery_id == null ? Convert.ToInt32(coll.daily_collection_id) : coll.delivery_id,
                    summary = $" updating collection={coll.daily_collection_id}, and delivery={coll.delivery_id} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);

                CustomerBAL customerBAL = new CustomerBAL();

            }
        }
        public List<EDMX.delivery_return> GetDelliveryReturn(DateTime returnDate, int status = 1, int empId = 0)
        {
            try
            {
                return deliveryDAL.GetDeliveryReturn(returnDate, status, empId);
            }
            catch { throw; }
        }
        public DataSet GetDeliverySalesReprot(int employeeId, DateTime dateFrom, DateTime dateTo, int itemId, int deliveryId = 0)
        {
            try
            {
                return deliveryDAL.GetDeliverySalesReprot(employeeId, dateFrom, dateTo, itemId, deliveryId);
            }
            catch { throw; }
        }
        public DataSet GetDeliverySalesReprotRouteWise(int routeId, DateTime dateFrom, DateTime dateTo, int itemId, int deliveryId = 0)
        {
            try
            {
                return deliveryDAL.GetDeliverySalesReprotRouteWise(routeId, dateFrom, dateTo, itemId, deliveryId);
            }
            catch { throw; }
        }

        public string GetPaymentModeInExceptionCase(int customerId, int deliveryId, int itemId, string _paymentMode)
        {
            try
            {
                return deliveryDAL.GetPaymentModeInExceptionCase(customerId, deliveryId, itemId, _paymentMode);
            }
            catch { throw; }
        }
        public List<EDMX.delivery_items> GetDeliveryItemsByIdandCustomer(int deliveryId, int customerId)
        {
            try
            {
                return deliveryDAL.GetDeliveryItemsByIdandCustomer(deliveryId, customerId);
            }
            catch { throw; }
        }
        public bool DeleteNotApprovedDeliveryReturn(int deliveryReturnId)
        {
            try
            {
                EDMX.delivery_return deliveryReturn = deliveryDAL.DeleteNotApprovedDeliveryReturn(deliveryReturnId);

                return true;
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
                    reference_id = deliveryReturnId,
                    summary = $" Deleteing Delivery Return ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.daily_collection> GetDailyColelction(DateTime dailyDate, int routeId, int employeeId, int customerId, string paymentMode)
        {
            try
            {
                DateTime d1 = dailyDate;
                TimeSpan ts = new TimeSpan(00, 00, 01);
                d1 = d1.Date + ts;

                DateTime d2 = dailyDate;
                TimeSpan ts1 = new TimeSpan(23, 59, 59);
                d2 = d2.Date + ts1;
                return deliveryDAL.GetDailyColelction(General.ConvertDateTimeServerFormat(d1), General.ConvertDateTimeServerFormat(d2), routeId, employeeId, customerId, paymentMode);
            }
            catch { throw; }
        }

        public DataTable GetDepositCollection(DateTime fromDate, DateTime toDate, int customerId, int itemId, int routeId)
        {
            try
            {
                DateTime d1 = fromDate;
                TimeSpan ts = new TimeSpan(00, 00, 01);
                d1 = d1.Date + ts;

                DateTime d2 = toDate;
                TimeSpan ts1 = new TimeSpan(23, 59, 59);
                d2 = d2.Date + ts1;
                return deliveryDAL.GetDepositCollection(d1, d2, customerId, itemId, routeId);
            }
            catch { throw; }
        }

        public List<EDMX.daily_collection> GetCollectionReport(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int routeId, int employeeId, int customerId, string paymentMode)
        {
            return deliveryDAL.GetCollectionReport(dailyDateTimeFrom, dailyDateTimeTo, routeId, employeeId, customerId, paymentMode);

        }

        public List<EDMX.daily_collection> PrintCollectionOnly(DateTime dateFrom, DateTime dateTo, int routeId, int employeeId, string employeeName)
        {
            try
            {

                List<EDMX.daily_collection> listCollection = deliveryDAL.GetCollectionOnly(dateFrom, dateTo, routeId, employeeId);
                if (listCollection != null && listCollection.Count > 0)
                {
                    BETask.Report.DSReports.DSRCollectionDataTable dSRCollectionDataTable = new Report.DSReports.DSRCollectionDataTable();
                    DataTable tblCollection = dSRCollectionDataTable.Clone();
                    foreach (EDMX.daily_collection coll in listCollection)
                    {
                        DataRow dr = tblCollection.NewRow();
                        dr["Customer"] = coll.customer.customer_name;
                        dr["Amount"] = coll.collected_amount;
                        dr["Mode"] = coll.payment_mode;
                        tblCollection.Rows.Add(dr);
                    }
                    RPT reportForm = new RPT(RPT.EnumReportType.DSRCollection, General.ConvertDateAppFormat(dateFrom), employeeName, tblCollection);
                    reportForm.Show();
                }
                return listCollection;
            }
            catch { throw; }
        }

        public void PrintCollectionReport(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int routeId, int employeeId, int customerId, string paymentMode)
        {
            try
            {
                string heading = $"Date between {General.ConvertDateAppFormat(dailyDateTimeFrom) } and {General.ConvertDateAppFormat(dailyDateTimeTo)  }";
                var collections = deliveryDAL.GetCollectionReport(dailyDateTimeFrom, dailyDateTimeTo, routeId, employeeId, customerId, paymentMode);
                if (collections != null && collections.Count > 0)
                {
                    BETask.Report.DSReports.CollectionReportDataTable dSRCollectionDataTable = new Report.DSReports.CollectionReportDataTable();
                    DataTable tblCollection = dSRCollectionDataTable.Clone();
                    foreach (EDMX.daily_collection coll in collections)
                    {
                        DataRow dr = tblCollection.NewRow();
                        dr["Date"] = General.ConvertDateTimeAppFormat(coll.delivery_time);
                        dr["Employee"] = $"{coll.employee.first_name} {coll.employee.last_name}";
                        dr["Customer"] = coll.customer.customer_name;
                        dr["Route"] = coll.customer.route.route_name;
                        dr["CollectionAmount"] = coll.collected_amount;
                        dr["Paymentmode"] = coll.payment_mode;
                        tblCollection.Rows.Add(dr);
                    }
                    RPT reportForm = new RPT(RPT.EnumReportType.CollectionReport, heading, tblCollection);
                    reportForm.Show();
                }
            }
            catch { throw; }
        }
        public int RemoveDuplicteCollection(int collId, int custoemrId, DateTime date)
        {
            try
            {
                return deliveryDAL.RemoveDuplicteCollection(collId, custoemrId, date);
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
                    reference_id = collId,
                    summary = $"Remove Duplicate Caollection {custoemrId}",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public int ReRunDSR(int deliveryId)
        {
            try
            {
                return deliveryDAL.ReRunDSR(deliveryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetDeliveryCount(int deliveryId)
        {
            return deliveryDAL.GetDeliveryCount(deliveryId);
        }



        public void PrintDailyCollection(DateTime dailyDate, int routeId, int employeeId, int customerId, string paymentMode, bool collectionOnly)
        {
            string header = $"Daily Colelction for the Date {General.ConvertDateAppFormat(dailyDate)}";
            try
            {
                List<EDMX.daily_collection> listDailyCollection = GetDailyColelction(dailyDate, routeId, employeeId, customerId, paymentMode);
                if (collectionOnly)
                    listDailyCollection = listDailyCollection.Where(x => x.delivery_id == null).ToList();

                if (listDailyCollection != null && listDailyCollection.Count > 0)
                {

                    DataTable tblData = new DataTable();
                    BETask.Report.DSReports.DailyCollectionDataTable dailyCollectionDataTable = new Report.DSReports.DailyCollectionDataTable();
                    tblData = dailyCollectionDataTable.Clone();


                    foreach (EDMX.daily_collection coll in listDailyCollection)
                    {
                        DataRow dr = tblData.NewRow();
                        dr["Employee"] = coll.employee.first_name;
                        dr["Customer"] = coll.customer.customer_name;
                        dr["Route"] = coll.customer.route.route_name;
                        dr["PaymentMode"] = coll.payment_mode;
                        dr["Netamount"] = coll.net_amount;
                        dr["CollectedAmount"] = coll.collected_amount;
                        tblData.Rows.Add(dr);
                    }
                    if (tblData != null && tblData.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.DailyCollection, header, tblData);
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
                    summary = $"Daily Collection Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void UpdateDailyCollectionStatus(int deliveryId, int customerId, int status)
        {
            try
            {
                deliveryDAL.UpdateDailyCollectionStatus(deliveryId, customerId, status);
            }
            catch { throw; }
        }
        public void DSRUpdateCollectionRecieved(int deliveryId, string employee)
        {
            try
            {
                deliveryDAL.DSRUpdateCollectionRecieved(deliveryId);
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
                    reference_id = deliveryId,
                    summary = $"DSR Collection Recieved Update  {deliveryId}",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void MapCollectionIdToDelieveryItem(int dailyCollectionId, int deliveryItemId)
        {
            try
            {
                deliveryDAL.MapCollectionIdToDelieveryItem(dailyCollectionId, deliveryItemId);
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
                    reference_id = deliveryItemId,
                    summary = $"mapped to collection  {dailyCollectionId} for {deliveryItemId}",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        #region reports
        public List<EDMX.delivery> EmployeeDeliveryReport(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            try
            {
                return deliveryDAL.EmployeeDeliveryReport(dateFrom, dateTo, employeeId);
            }
            catch { throw; }
        }
        public DataTable EmployeeDeliveryReportSummary(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            try
            {
                return deliveryDAL.EmployeeDeliveryReportSumamry(dateFrom, dateTo, employeeId);
            }
            catch { throw; }
        }
        public List<DAL.Model.DeliveryItemSummaryModel> GetDeliveryItemSumamry(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            try
            {
                return deliveryDAL.GetDeliveryItemSumamry(dateFrom, dateTo, employeeId);
            }
            catch { throw; }
        }
        public decimal GetDeliveryItemSummaryAdditionalItemQty(int deliveryId, int itemId)
        {
            try
            {
                return deliveryDAL.GetDeliveryItemSummaryAdditionalItemQty(deliveryId, itemId);
            }
            catch { throw; }
        }
        public void UpdateScehduledDeliveryQty(int deliveryId, int deliveryItemId, decimal oldQty, decimal qty, decimal gross, decimal vat, decimal net)
        {
            try
            {
                deliveryDAL.UpdateScehduledDeliveryQty(deliveryId, deliveryItemId, qty, gross, vat, net);
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
                    reference_id = deliveryId,
                    summary = $"{deliveryItemId} Updating scheduled qty {oldQty} to {qty}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void UpdateAdditionalDeliveryQty(decimal addQty, int deliveryId, int itemId)
        {
            try
            {
                deliveryDAL.UpdateAdditionalDeliveryQty(addQty, deliveryId, itemId);
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
                    reference_id = deliveryId,
                    summary = $"Update delivery additional qty={addQty}",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public int GetDeliveryIdByDate(DateTime deliveryDate, int deliveryId)
        {
            return deliveryDAL.GetDeliveryIdByDate(deliveryDate, deliveryId);
        }

        public void ChangeDeliveryId(long saleId, int deliveryId,int newDeliveryId,string message)
        {
            try
            {
                deliveryDAL.ChangeDeliveryId(saleId, deliveryId, newDeliveryId);

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
                    reference_id = newDeliveryId,
                    summary = $"{message} sale {saleId} , old {deliveryId} , new {newDeliveryId}",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
            public void PrintEmployeeDeliveryReport(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            try
            {
                List<EDMX.delivery> listDelivery = deliveryDAL.EmployeeDeliveryReport(dateFrom, dateTo, employeeId);
                DataTable tblDelivery = new DataTable();
                BETask.Report.DSReports.DeliveryStaffReportDataTable deliveryStaffReportDataTable = new Report.DSReports.DeliveryStaffReportDataTable();
                tblDelivery = deliveryStaffReportDataTable.Clone();
                foreach (EDMX.delivery delivery in listDelivery)
                {
                    string employee = String.Format("{0} {1}", delivery.employee.first_name, delivery.employee.last_name);
                    var deliveredCustomerCount = delivery.delivery_items.Where(x => x.delivery_time != null).GroupBy(t => new { t.customer_id }).Count();
                    var saleSum = delivery.sales.Sum(n => n.net_amount);
                    DataRow dr = tblDelivery.NewRow();
                    dr["DeliveryID"] = delivery.delivery_id;
                    dr["StaffName"] = employee;
                    dr["Vehicle"] = delivery.vehicle_no;
                    dr["Route"] = delivery.delivery_route;
                    dr["DeliveryCustomerCount"] = delivery.customer_count;
                    dr["ReachedCustomerCount"] = deliveredCustomerCount;
                    dr["DeliveryAmount"] = delivery.net_amount;
                    dr["SalesAmount"] = saleSum;
                    dr["DeliveryDate"] = General.ConvertDateAppFormat(delivery.delivery_date);
                    tblDelivery.Rows.Add(dr);
                }
                if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.DeliveryReportStaffWise, "", tblDelivery);
                    reportForm.Show();
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
                    summary = $" Print Delivry Staffwise Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintDeliverySummary(DateTime dateFrom, DateTime dateTo, int employeeId, string header)
        {
            try
            {
                List<DAL.Model.DeliveryItemSummaryModel> listDelivery = deliveryDAL.GetDeliveryItemSumamry(dateFrom, dateTo, employeeId);
                DataTable tblDelivery = new DataTable();
                BETask.Report.DSReports.DeliverySummaryDataTable deliverySummaryDataTable = new Report.DSReports.DeliverySummaryDataTable();
                tblDelivery = deliverySummaryDataTable.Clone();
                foreach (DAL.Model.DeliveryItemSummaryModel delivery in listDelivery)
                {

                    DataRow dr = tblDelivery.NewRow();
                    dr["ItemName"] = delivery.ItemName;
                    dr["Qty"] = delivery.DeleveredQty;
                    dr["Foc"] = delivery.Foc;
                    dr["Total"] = delivery.TotalQty;
                    dr["Amount"] = delivery.Amount;
                    tblDelivery.Rows.Add(dr);
                }
                if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.DeliverySummary, header, tblDelivery);
                    reportForm.Show();
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
                    summary = $" Print Delivry Staffwise Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public DataTable ItemDeliveryReport(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, bool deliveredOnly, int routeId = 0, decimal rangeFrom = 0, decimal rangeTo = 0, string paymentMode = "")
        {
            try
            {
                return deliveryDAL.ItemDeliveryReport(dateFrom, dateTo, customerId, itemId, deliveredOnly, routeId,rangeFrom,rangeTo,paymentMode);
            }
            catch { throw; }
        }
        public void PrintDeliveryLoadReport(DateTime dateFrom, DateTime dateTo, int employeeId, int itemId)
        {
            string employee = string.Empty;
            string item = string.Empty;
            try
            {
                List<BETask.DAL.Model.DeliveryLoadReportModel> listDeliveryItemSummary = deliveryDAL.GetDeliveryLoadDetails(dateFrom, dateTo, employeeId, itemId);
                DataTable tblDelivery = new DataTable();

                BETask.Report.DSReports.DeliveryLoadReportDataTable deliveryItemSummaryDataTable = new Report.DSReports.DeliveryLoadReportDataTable();
                tblDelivery = deliveryItemSummaryDataTable.Clone();
                foreach (BETask.DAL.Model.DeliveryLoadReportModel DeliverySummary in listDeliveryItemSummary)
                {
                    DataRow dr = tblDelivery.NewRow();
                    dr["Date"] = General.ConvertDateAppFormat(dateFrom);
                    dr["ItemId"] = DeliverySummary.ItemId;
                    dr["ItemName"] = DeliverySummary.ItemName;
                    dr["EmployeeName"] = DeliverySummary.EmployeeName;
                    dr["PreviousBalance"] = DeliverySummary.PreviousBalance;
                    dr["LoadQty"] = DeliverySummary.LoadQty;
                    dr["TotalQty"] = DeliverySummary.TotalQty;
                    dr["SoldQty"] = DeliverySummary.SoldQty;
                    dr["DamageQty"] = DeliverySummary.DamageQty;
                    dr["BalanceQty"] = DeliverySummary.BalanceQty;
                    dr["Remarks"] = DeliverySummary.Remarks;
                    employee = DeliverySummary.EmployeeName;
                    item = DeliverySummary.ItemName;
                    tblDelivery.Rows.Add(dr);
                }
                if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                {
                    string head = $"Item Load Report {employee} {item} between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)}";

                    RPT reportForm = new RPT(RPT.EnumReportType.DeliveryLoadReport, head, tblDelivery);
                    reportForm.Show();


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
                    summary = $" Print Delivry Load Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintItemDeliveryReport(string route, DataTable tblReport)
        {
            try
            {

                DataTable tblDelivery = new DataTable();
                BETask.Report.DSReports.DeliveryItemReportDataTable deliveryItemReportDataTable = new Report.DSReports.DeliveryItemReportDataTable();
                tblDelivery = deliveryItemReportDataTable.Clone();
                foreach (DataRow row in tblReport.Rows)
                {
                    string employee = Convert.ToString(row["employee"]);
                    DataRow dr = tblDelivery.NewRow();
                    dr["DeliveryID"] = row["delivery_id"];
                    dr["CustomerName"] = row["customer_name"];
                    dr["ItemName"] = row["item_name"];
                    dr["Packing"] = row["uom_name"];
                    dr["DeliveredQty"] = row["delivered_qty"];
                    dr["Qty"] = row["qty"];
                    dr["Rate"] = row["rate"];
                    dr["DeliveryTime"] = row["delivery_time"];
                    tblDelivery.Rows.Add(dr);
                }
                if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                {
                    string header = "";
                    if (!String.IsNullOrEmpty(route))
                        header = $"Customer wise Item Delivery report for the route {route}";
                    else
                        header = "Customer wise Item Delivery report";
                    RPT reportForm = new RPT(RPT.EnumReportType.DeliveryReportItemWise, header, tblDelivery);
                    reportForm.Show();
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
                    summary = $" Print Delivry Staffwise Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintDepositCollection(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, int routeId, string itemName)
        {
            try
            {
                DataTable collections = GetDepositCollection(General.ConvertDateServerFormat(dateFrom), General.ConvertDateServerFormat(dateTo), customerId, itemId, routeId);
                DataTable tblCollection = new DataTable();
                BETask.Report.DSReports.DepositCollectionDataTable depositCollectionDataTable = new Report.DSReports.DepositCollectionDataTable();
                tblCollection = depositCollectionDataTable.Clone();
                foreach (DataRow row in collections.Rows)
                {
                    DataRow dr = tblCollection.NewRow();
                    dr["Customer"] = row["CustomerName"];
                    dr["Route"] = row["RouteName"];
                    dr["Date"] = row["DeliveryTime"];
                    dr["Qty"] = row["Qty"];
                    dr["Amount"] = row["CollectionAmount"];
                    dr["IsRefund"] = row["IsRefund"];
                    dr["ItemName"] = row["ItemName"];
                    tblCollection.Rows.Add(dr);
                }
                if (tblCollection != null && tblCollection.Rows.Count > 0)
                {
                    string header = $"Date between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)} {itemName}";

                    RPT reportForm = new RPT(RPT.EnumReportType.DepositCollection, header, tblCollection);
                    reportForm.Show();
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
                    summary = $" Print Delivry Staffwise Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        //public List<EDMX.delivery_return> GetDeliveryReturnReport(DateTime dateFrom, DateTime dateTo, int routeId, int customerId, int employeeId, int itemId, int returnType)
        //{
        //    try
        //    {
        //        return deliveryDAL.GetDeliveryReturnReport(dateFrom, dateTo, routeId, customerId, employeeId, itemId, returnType);
        //    }
        //    catch { throw; }
        //}

        public DataTable GetDeliveryReturnReport(DateTime dateFrom, DateTime dateTo, int routeId, int customerId, int employeeId, int itemId, int returnType)
        {
            try
            {
                return deliveryDAL.GetDeliveryReturnReport(dateFrom, dateTo, routeId, customerId, employeeId, itemId, returnType);
            }
            catch { throw; }
        }
        public void PrintDeliveryReturn(DateTime dateFrom, DateTime dateTo, int routeId, int customerId, int employeeId, int itemId, int returnType,DataTable tblReturn)
        {
            try
            {
                string header = $"Delivery Return Report for the date between { General.ConvertDateAppFormat(dateFrom)} and { General.ConvertDateAppFormat(dateTo)}";
                if (tblReturn == null)
                    tblReturn = GetDeliveryReturnReport(dateFrom, dateTo, routeId, customerId, employeeId, itemId, returnType);
                DataTable tblDelivery = new DataTable();
                BETask.Report.DSReports.DeliveryReturnDataTable deliveryReportDataTable = new Report.DSReports.DeliveryReturnDataTable();
                tblDelivery = deliveryReportDataTable.Clone();
                foreach (DataRow row in tblReturn.Rows)
                {
                   // string employee = String.Format("{0} {1}", delivery.employee.first_name, delivery.employee.last_name);

                    DataRow dr = tblDelivery.NewRow();
                    dr["ReturnDate"] = General.ConvertDateAppFormat(DateTime.Parse(row["return_date"].ToString()));
                    dr["Employee"] = row["Employee"];
                    dr["Customer"] = row["customer_name"];
                    dr["Route"] = row["route_name"];
                    dr["ItemName"] = row["item_name"];
                    dr["Qty"] = row["qty"];

                    tblDelivery.Rows.Add(dr);
                }
                if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.DeliveryReturnReport, header, tblDelivery);
                    reportForm.Show();
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
                    summary = $" Print Delivery Return Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public BETask.DAL.Model.ProductionDeliveryReportModel GetProductionDeliveryReport(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            try
            {
                return deliveryDAL.GetProductionDeliveryReport(dateFrom, dateTo, itemId);
            }
            catch { throw; }
        }
        public void PrintProductionDeliveryReport(DateTime dateFrom, DateTime dateTo, int itemId, string header)
        {
            try
            {
                BETask.DAL.Model.ProductionDeliveryReportModel itemProduction = deliveryDAL.GetProductionDeliveryReport(dateFrom, dateTo, itemId);
                List<BETask.DAL.Model.ProductionDeliveryDetail> listDetail = itemProduction.listDeliveryDetail != null ? itemProduction.listDeliveryDetail : null;
                if (listDetail != null && listDetail.Count > 0)
                {
                    DataTable tblDelivery = new DataTable();
                    BETask.Report.DSReports.ItemDeliveryProductionDataTable deliveryReportDataTable = new Report.DSReports.ItemDeliveryProductionDataTable();
                    tblDelivery = deliveryReportDataTable.Clone();
                    foreach (BETask.DAL.Model.ProductionDeliveryDetail dl in listDetail)
                    {
                        DataRow dr = tblDelivery.NewRow();
                        dr["Employee"] = dl.EmployeeName;
                        dr["Scheduled"] = dl.Scheduled;
                        dr["Delivered"] = dl.Delivered;
                        dr["Balance"] = dl.Balance;
                        dr["Returned"] = dl.Returned;
                        dr["Production"] = itemProduction.Production;
                        dr["Stock"] = itemProduction.Stock;

                        tblDelivery.Rows.Add(dr);
                    }
                    if (tblDelivery != null && tblDelivery.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.ItemDeliveryProduction, header, tblDelivery);
                        reportForm.Show();
                    }
                }

            }
            catch { throw; }
        }
        #endregion reports
    }
}
