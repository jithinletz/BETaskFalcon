using System;
using EDMX = BETask.DAL.EDMX;
using BETask.DAL.DAL;
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
    
    public class LoadingBAL
    {
        EmployeeDAL objEmployee = new EmployeeDAL();

        REP.LoadDAL loadDAL = new REP.LoadDAL();
        public void SaveLoad(EDMX.loading load)
        {
            try
            {
                loadDAL.SaveLoad(load);

                //List<APP.EDMX.delivery_item_summary> listAppsummary = new List<APP.EDMX.delivery_item_summary> { };

                //DAL.DAL.DeliveryDAL deliveryDAL = new REP.DeliveryDAL();
                //List<EDMX.delivery_item_summary> listDelivery = deliveryDAL.GetDeliveryItemSummary(load.delivery_id);
                //if (listDelivery != null)
                //{
                    
                //    foreach (EDMX.delivery_item_summary ds in listDelivery)
                //    {
                //        listAppsummary.Add(new APP.EDMX.delivery_item_summary
                //        {
                //            qty=ds.qty,
                //            balance_qty=ds.balance_qty,
                //            delivery_id=ds.delivery_id,
                //            item_id=ds.item_id,
                //            used_qty=ds.used_qty,
                //            status=1,
                //        });
                //    }
                //}
                //BETask.APP.DAL.DeliveryAppDAL deliveryAppDAL = new APP.DAL.DeliveryAppDAL();
                //deliveryAppDAL.SaveDeliveryItemSummary(listAppsummary);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to update cloud, please check internet connection");
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
                    reference_id = load.item_id,
                    summary = $" Saving of Loading",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public int DeleteLoad(int loadId, int deliveryId, int itemId)
        {
            try
            {
                return loadDAL.DeleteLoad(loadId, deliveryId, itemId);
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
                    summary = $" Delete loading",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintLoadingSlip(List<EDMX.loading> listLoading)
        {
            try
            {
                DataTable tblData = new DataTable();
                BETask.Report.DSReports.LoadingSlipDataTable loadingSlipDataTable = new Report.DSReports.LoadingSlipDataTable();
                tblData = loadingSlipDataTable.Clone();


                foreach (EDMX.loading coll in listLoading)
                {
                    DataRow dr = tblData.NewRow();
                    dr["LoadingTime"] = General.ConvertDateTimeAppFormat(coll.server_time);
                    dr["TotalLoad"] = coll.total_load;
                    dr["OldStock"] = coll.old_stock;
                    dr["Damage"] = coll.damage;
                    dr["Balance"] = coll.balance;
                    dr["NewLoad"] = coll.new_load;
                    dr["NewStock"] = coll.new_stock;
                    dr["OffLoad"] = coll.offload;
                    dr["Empty"] = coll.empty;
                    dr["LoadingId"] = coll.load_id;
                    dr["ItemName"] = coll.item.item_name;
                    tblData.Rows.Add(dr);
                }
                if (tblData != null && tblData.Rows.Count > 0)
                {
                    CompanyBAL companyBAL = new CompanyBAL();
                    CompanyModel company = companyBAL.GetCompanyDetails();
                    string address = $"{company.Description} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}";
                    RPT reportForm = new RPT(RPT.EnumReportType.LoadingSlip, address,listLoading[0].delivery_id.ToString(), tblData);
                    reportForm.Show();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Print(int item, int employee, DateTime fromDate, DateTime toDate, string header)
        {
            try
            {
                List<BETask.DAL.EDMX.loading> listLoading = loadDAL.GetAllLoad(item, employee, fromDate, toDate);
                if (listLoading != null && listLoading.Count > 0)
                {
                    BETask.Report.DSReports.LoadingReportDataTable loadingReportDataTable = new BETask.Report.DSReports.LoadingReportDataTable();
                    DataTable tblData = loadingReportDataTable.Clone();
                    foreach (BETask.DAL.EDMX.loading load in listLoading)
                    {
                        
                        DataRow dr = tblData.NewRow();
                        dr["Date"] = General.ConvertDateAppFormat(load.load_date);
                        dr["Employee"] = load.employee != null ? $"{load.employee.first_name} {load.employee.last_name}" : "";
                        dr["Helper"] = load.helper;
                        dr["Item"] = load.item.item_name;
                        dr["OldStock"] = load.old_stock;
                        dr["Empty"] = load.empty;
                        dr["Damage"] = load.damage;
                        dr["Balance"] = load.balance;
                        dr["NewLoad"] = load.new_load;
                        dr["TotalLoad"] = load.total_load;
                        dr["Short"] = load.@short;
                        dr["Extra"] = load.extra;
                        dr["NewStock"] = load.new_stock;
                        dr["Offload"] = load.offload;
                        dr["ActStock"] = load.act_stock;
                        dr["Sold"] = load.remarks;
                        tblData.Rows.Add(dr);
                    }
                    if (tblData != null && tblData.Rows.Count > 0)
                    {
                        BETask.Report.ReportForm reportForm = new BETask.Report.ReportForm(BETask.Report.ReportForm.EnumReportType.Loading, $" Loading Report Between {header}", tblData);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EDMX.loading> GetAllLoad(int item, int employee, DateTime fromDate, DateTime toDate)
        {
            try
            {
                return loadDAL.GetAllLoad(item, employee, fromDate, toDate);
            }
            catch
            {
                throw;
            }
        }
    }
}
