using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using RPT = BETask.Report.ReportForm;

namespace BETask.BAL
{
    public class CouponBAL
    {
        REP.CouponDAL couponDAL = new REP.CouponDAL();
        public void SaveCoupon(EDMX.coupon coupon, List<EDMX.coupon_leaf> listCoupon)
        {
            try
            {
                couponDAL.SaveCoupon(coupon, listCoupon);
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
                    summary = $" Save coupon {coupon.book_number}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        
        public List<EDMX.coupon> SearchCoupon(string bookno, int customerId)
        {
            try
            {
                return couponDAL.SearchCoupon(bookno, customerId);
            }
            catch (Exception ee)
            {
                throw;

            }
        }


        public EDMX.coupon GetCoupon(int couponId)
        {
            try
            {
                return couponDAL.GetCoupon(couponId);
            }
            catch (Exception ee)
            {
                throw;

            }
        }
        public List<EDMX.coupon> GetCouponBooks(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            try
            {
                return couponDAL.GetCouponBooks(dateFrom, dateTo, routeId);
            }
            catch (Exception ee)
            {
                throw;

            }
        }
        public List<EDMX.coupon_leaf> GetRedeemedLeafs(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            try
            {
                return couponDAL.GetRedeemedLeafs(dateFrom, dateTo, routeId);
            }
            catch (Exception ee)
            {
                throw;

            }
        }
        public string LeafExistCheck(Int64 leafFrom, Int64 leafTo, string bookNo)
        {
            try
            {
                return couponDAL.LeafExistCheck(leafFrom, leafTo, bookNo);
            }
            catch (Exception ee)
            {
                throw;

            }
        }
        public void PrintCouponReport(DateTime dateFrom, DateTime dateTo, int routeId, string header, string searchType)
        {
            try
            {
                List<EDMX.coupon> listCoupon = couponDAL.GetCouponBooks(dateFrom, dateTo, routeId).OrderBy(x => x.book_number).ToList();
                if (listCoupon != null && listCoupon.Count > 0)
                {
                    DataTable tblDReport = new DataTable();
                    BETask.Report.DSReports.CouponBookDataTable couponBookDataTable = new Report.DSReports.CouponBookDataTable();
                    tblDReport = couponBookDataTable.Clone();
                    foreach (EDMX.coupon cp in listCoupon)
                    {
                        var leafs = cp.coupon_leaf.ToList();
                        int available = leafs.Where(x => x.status == 1).Count();
                        if (searchType == "all")
                        {
                            DataRow dr = tblDReport.NewRow();
                            dr["BookNumber"] = cp.book_number;
                            dr["IssueDate"] = General.ConvertDateAppFormat(cp.issue_date);
                            dr["Customer"] = cp.customer.customer_name;
                            dr["Amount"] = cp.book_rate;
                            dr["Leafs"] = cp.leaf_count;
                            dr["Available"] = available;
                            tblDReport.Rows.Add(dr);
                        }
                        else if (searchType == "active")
                        {
                            if (available > 0)
                            {
                                DataRow dr = tblDReport.NewRow();
                                dr["BookNumber"] = cp.book_number;
                                dr["IssueDate"] = General.ConvertDateAppFormat(cp.issue_date);
                                dr["Customer"] = cp.customer.customer_name;
                                dr["Amount"] = cp.book_rate;
                                dr["Leafs"] = cp.leaf_count;
                                dr["Available"] = available;
                                tblDReport.Rows.Add(dr);
                            }
                        }
                        else if (searchType == "used")
                        {
                            if (available <= 0)
                            {
                                DataRow dr = tblDReport.NewRow();
                                dr["BookNumber"] = cp.book_number;
                                dr["IssueDate"] = General.ConvertDateAppFormat(cp.issue_date);
                                dr["Customer"] = cp.customer.customer_name;
                                dr["Amount"] = cp.book_rate;
                                dr["Leafs"] = cp.leaf_count;
                                dr["Available"] = available;
                                tblDReport.Rows.Add(dr);
                            }
                        }
                    }

                    RPT reportForm = new RPT(RPT.EnumReportType.CouponBookReport, header, tblDReport);
                    reportForm.Show();

                }
            }
            catch (Exception ee)
            {
                throw;

            }
        }
        public void PrintCouponRedeemedReport(DateTime dateFrom, DateTime dateTo, int routeId, string header)
        {
            try
            {
                List<EDMX.coupon_leaf> listCoupon = couponDAL.GetRedeemedLeafs(dateFrom, dateTo, routeId).OrderBy(x => x.redeem_date).ToList();
                if (listCoupon != null && listCoupon.Count > 0)
                {
                    DataTable tblDReport = new DataTable();
                    BETask.Report.DSReports.CouponRedeemedDataTable couponBookDataTable = new Report.DSReports.CouponRedeemedDataTable();
                    tblDReport = couponBookDataTable.Clone();
                    foreach (EDMX.coupon_leaf cp in listCoupon)
                    {

                        DataRow dr = tblDReport.NewRow();
                        dr["BookNumber"] = cp.coupon.book_number;
                        dr["Customer"] = cp.coupon.customer.customer_name;
                        dr["LeafNumber"] = cp.leaf_number;
                        dr["LeafRate"] = cp.leaf_rate;
                        dr["DeliveryTime"] = cp.redeem_date;
                        dr["Remarks"] = cp.remarks;
                        tblDReport.Rows.Add(dr);
                    }

                    RPT reportForm = new RPT(RPT.EnumReportType.CouponRedeemedReport, header, tblDReport);
                    reportForm.Show();

                }
            }
            catch (Exception ee)
            {
                throw;

            }
        }

        public bool DeleteCoupon(int couponId,string bookNumber)
        {
            bool result = false;
            try
            {
                result = couponDAL.DeleteCoupon(couponId);
              
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
                    summary = $" Delete coupon book {bookNumber}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return result;
        }

        #region DeliveryBook

        public int SaveDeliveryBook(List<EDMX.delivery_book> listDelivery, out int resultApp)
        {
            int result = 0;
            resultApp = 0;
            try
            {
               result=  couponDAL.SaveDeliveryBook(listDelivery);
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
                    summary = $" Save coupon {listDelivery[0].book_number}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return result;
            
        }
       
        public void DeactivateDeliveryLeaf(int customerId,string bookNo, int bookId,string leaf)
        {
            try
            {
                couponDAL.DeactivateDeliveryLeaf(customerId, bookId);
               
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
                    summary = $" update delivery book {bookNo} {leaf}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public int TransferDeliveryBook(string bookNo, int routeId)
        {
            int resp = 0;
            try
            {
              resp=  couponDAL.TransferDeliveryBook(bookNo, routeId);
               
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
                    reference_id = routeId,
                    summary = $" Transfer delivery book {bookNo} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
            return resp;
        }

        #endregion

    }
}
