using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using RPT = BETask.Report.ReportForm;

namespace BETask.BAL
{
   public class RoutewiseSaleBAL
    {
        REP.RoutewiseSaleDAL routewiseSaleDAL = new REP.RoutewiseSaleDAL();
        public List<BETask.DAL.Model.RoutewiseSaleModel> GetRoutewiseSale(DateTime dateFrom, DateTime dateTo, int employeeId, int itemId)
        {
            try
            {
                return routewiseSaleDAL.GetRoutewiseSale(employeeId, itemId, dateFrom, dateTo);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void Print(DateTime dateFrom, DateTime dateTo, int employeeId, int itemId,string header1,string header2)
        {
            try
            {
                List<BETask.DAL.Model.RoutewiseSaleModel> listData= routewiseSaleDAL.GetRoutewiseSale(employeeId, itemId, dateFrom, dateTo);
                if (listData != null && listData.Count>0)
                {

                    DataTable tblReport = new DataTable();
                    BETask.Report.DSReports.RoutewiseSaleDataTable routewiseSaleDataTable = new Report.DSReports.RoutewiseSaleDataTable();
                    tblReport = routewiseSaleDataTable.Clone();
                    foreach (BETask.DAL.Model.RoutewiseSaleModel rs in listData)
                    {
                        DataRow dr =  tblReport.NewRow();
                        dr["Docdate"] = General.ConvertDateAppFormat(rs.DeliveryDate);
                        dr["Loading"] = General.TruncateDecimalPlaces(rs.Loading,0);
                        dr["Offload"] = General.TruncateDecimalPlaces(rs.Offload, 0);
                        dr["Sale"] = General.TruncateDecimalPlaces(rs.Sale, 0);
                        dr["Empty"] = General.TruncateDecimalPlaces(rs.Empty, 0);
                        dr["Short"] = General.TruncateDecimalPlaces(rs.Balance, 0);
                        dr["Damage"] = General.TruncateDecimalPlaces(rs.Damage, 0);
                        dr["Cash"] = General.TruncateDecimalPlaces(rs.Cash, 2);
                        dr["Collection"] = General.TruncateDecimalPlaces(rs.Collection, 2);
                        dr["Coupon"] = General.TruncateDecimalPlaces(rs.Wallet, 2);
                        dr["DoSale"] = General.TruncateDecimalPlaces(rs.DoSale, 2);
                        dr["SalesmanCredit"] = General.TruncateDecimalPlaces(rs.SalesmanCredit, 2);
                        dr["Outstanding"] = General.TruncateDecimalPlaces(rs.Outstanding, 2);
                        dr["Total"] = General.TruncateDecimalPlaces(rs.Total, 2);
                        dr["Foc"] = General.TruncateDecimalPlaces(rs.Foc, 2);
                        tblReport.Rows.Add(dr);
                    }
                    if (tblReport != null && tblReport.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.RouteSaleReport, header1,header2, tblReport);
                        reportForm.Show();
                    }
                }
            }
            catch
            { }

        }
    }
}
