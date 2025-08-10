using BETask.DAL.DAL;
using BETask.Model;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace BETask.BAL
{
    
   public class BalanceSheetBAL
    {
        BalanceSheetDAL balanceSheet = new BalanceSheetDAL();
        public List<DAL.Model.BalanceSheetModel> BalanceSheet(DateTime dateFrom, DateTime dateTo,decimal currentProfit=0)
        {
            try
            {
                return balanceSheet.BalanceSheet(dateFrom,dateTo, currentProfit);
            }
            catch
            {
                throw;
            }
        }
        public void Print(DateTime dateFrom, DateTime dateTo, DataGridView grid)
        {
            //List<DAL.Model.ProfitandLossModel> listPL = pandl.GenerateProfitandLoss(dateFrom, dateTo);
            try
            {
                string header = $"Balance sheet between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)}";
                CompanyBAL companyBAL = new CompanyBAL();
                CompanyModel company = companyBAL.GetCompanyDetails();
                string address = $"{company.Name} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, TRN:{company.Tin}";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.BalancesheetDataTable routewiseSaleDataTable = new Report.DSReports.BalancesheetDataTable();
                tblReport = routewiseSaleDataTable.Clone();

                foreach (DataGridViewRow gdr in grid.Rows)
                {
                    DataRow dr = tblReport.NewRow();
                    string space = ""; space = space.PadLeft(15);
                    dr["Asset"] = gdr.Cells["clmDesc1"].Value.ToString();
                    dr["AssetAmount"] = gdr.Cells["clmAmt1"].Value.ToString();
                    dr["Liability"] = gdr.Cells["clmDesc2"].Value.ToString();
                    dr["LiabilityAmount"] = gdr.Cells["clmAmt2"].Value.ToString();
                    tblReport.Rows.Add(dr);
                }
                RPT reportForm = new RPT(RPT.EnumReportType.Balancesheet, header, address, tblReport);
                reportForm.Show();
            }



            catch { throw; }
        }
    }
}
