using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using EDMXApp = BETask.APP.EDMX;
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

    public class ProfitandLossBAL
    {
        DAL.DAL.ProfitAndLossDAL pandl = new ProfitAndLossDAL();
        DAL.DAL.TrailBalanceDAL trial = new TrailBalanceDAL();
        public List<DAL.Model.ProfitandLossModel> GenerateProfitandLoss(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return pandl.GenerateProfitandLoss(dateFrom, dateTo);
            }
            catch
            {
                throw;
            }
        }
        public DAL.Model.ProfitandLossModelNew GenerateProfitandLossNew(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return pandl.GenerateProfitandLossNew(dateFrom, dateTo);
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
                string header = $"Profit and Loss between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)}";
                CompanyBAL companyBAL = new CompanyBAL();
                CompanyModel company = companyBAL.GetCompanyDetails();
                string address = $"{company.Name} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, TRN:{company.Tin}";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.ProfitandLossDataTable routewiseSaleDataTable = new Report.DSReports.ProfitandLossDataTable();
                tblReport = routewiseSaleDataTable.Clone();

                foreach (DataGridViewRow gdr in grid.Rows)
                {
                    DataRow dr = tblReport.NewRow();
                    string space = ""; space = space.PadLeft(15);
                    dr["Ledgername"] = gdr.Cells["clmDesc1"].Value.ToString().Contains("_____")?"_".PadLeft(30,'_'): gdr.Cells["clmDesc1"].Value;
                    dr["Amount"] = gdr.Cells["clmAmt1"].Value.ToString().Contains("_____")?"":gdr.Cells["clmAmt1"].Value;
                    dr["Ledgername1"] = gdr.Cells["clmDesc2"].Value.ToString().Contains("_____") ? "_".PadLeft(30, '_') : gdr.Cells["clmDesc2"].Value;
                    dr["Amount1"] = gdr.Cells["clmAmt2"].Value.ToString().Contains("_____") ? "" : gdr.Cells["clmAmt2"].Value;
                    tblReport.Rows.Add(dr);
                }
                RPT reportForm = new RPT(RPT.EnumReportType.ProfitandLoss, header, address, tblReport);
                reportForm.Show();
            }



            catch { throw; }
        }

        public List<DAL.Model.TrailBalanceModel> TrialBalance(DateTime dateFrom, DateTime dateTo,bool opening=false)
        {
            try
            {
                return trial.TrialBalance(dateFrom,dateTo);
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<DAL.Model.TrailBalanceDatewiseModel> TrialBalanceDatewise(DateTime dateFrom, DateTime dateTo, bool opening = false)
        {
            try
            {
                return trial.TrialBalanceDatewise(dateFrom, dateTo);
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public List<DAL.Model.TrailBalanceModel> TrialBalanceDetailed(DateTime dateFrom, DateTime dateTo, bool opening = true)
        {
            try
            {
                return trial.TrialBalanceDetailed(dateFrom, dateTo, opening);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public List<DAL.Model.TrailBalanceDatewiseModel> TrialBalanceDetailedDatewise(DateTime dateFrom, DateTime dateTo, bool opening = true,bool excludeCustomer=false)
        {
            try
            {
                return trial.TrialBalanceDetailedDatewise(dateFrom, dateTo, opening,excludeCustomer);
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        public void PostProfitAndLoss(decimal debit, decimal credit, DateTime date)
        {
            try
            {
                AccountTransactionDAL transactionDAL = new AccountTransactionDAL();
                transactionDAL.PostProfitAndLoss(debit, credit, date);
            }
            catch
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                decimal amount = debit>0?debit:credit;
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = 0,
                    summary = $" posting profit and loss debit={debit} credit={credit} Narration=for the year {date.Year} ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintTrailBalance(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                List<DAL.Model.TrailBalanceModel> listTrail = trial.TrialBalanceDetailed(dateFrom, dateTo);


                CompanyBAL companyBAL = new CompanyBAL();
                CompanyModel company = companyBAL.GetCompanyDetails();
                string header = $"{company.Name}";
                //string address = $"{company.Description} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, VAT Number:{company.Tin}";
                string header1 = $"Trial Balance as on {General.ConvertDateAppFormat(dateTo)}";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.TrialBalanceDataTable trialDataTable = new Report.DSReports.TrialBalanceDataTable();
                tblReport = trialDataTable.Clone();
                if (listTrail != null && listTrail.Count > 0)
                {
                    foreach (DAL.Model.TrailBalanceModel pl in listTrail)
                    {
                        DataRow dr = tblReport.NewRow();
                        dr["LedgerName"] = pl.Description;
                        dr["GroupName"] = pl.HeaderType;
                        dr["Debit"] = pl.Debit;
                        dr["Credit"] = pl.Credit;
                        tblReport.Rows.Add(dr);
                    }
                    RPT reportForm = new RPT(RPT.EnumReportType.TrialBalance, header, header1, tblReport);
                    reportForm.Show();
                }

            }

            catch { throw; }
        }

        public void PrintTrailBalanceDatewise(DateTime dateFrom, DateTime dateTo,bool excludeCustomer=false)
        {

            try
            {
                List<DAL.Model.TrailBalanceDatewiseModel> listTrail = trial.TrialBalanceDetailedDatewise(dateFrom, dateTo,true, excludeCustomer);


                CompanyBAL companyBAL = new CompanyBAL();
                CompanyModel company = companyBAL.GetCompanyDetails();
                string header = $"{company.Name}";
                string header1 = $"Trial Balance as on {General.ConvertDateAppFormat(dateTo)}";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.TrialBalanceDatewiseDataTable trialDataTable = new Report.DSReports.TrialBalanceDatewiseDataTable();
                tblReport = trialDataTable.Clone();
                if (listTrail != null && listTrail.Count > 0)
                {
                    foreach (DAL.Model.TrailBalanceDatewiseModel pl in listTrail)
                    {
                        DataRow dr = tblReport.NewRow();
                        dr["LedgerName"] = pl.Description;
                        dr["GroupName"] = pl.HeaderType;
                        dr["Debit"] = pl.Debit;
                        dr["Credit"] = pl.Credit;
                        dr["OpeningDebit"] = pl.OpeningDebit;
                        dr["OpeningCredit"] = pl.OpeningCredit;
                        dr["ClosingDebit"] = pl.ClosingDebit;
                        dr["ClosingCredit"] = pl.ClosingCredit;
                        tblReport.Rows.Add(dr);
                    }
                    RPT reportForm = new RPT(RPT.EnumReportType.TrialBalanceDatewise, header, header1, tblReport);
                    reportForm.Show();
                }

            }

            catch { throw; }
        }


        public void PrintTrailBalanceSummary(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                List<DAL.Model.TrailBalanceModel> listTrail = trial.TrialBalance(dateFrom, dateTo);


                CompanyBAL companyBAL = new CompanyBAL();
                CompanyModel company = companyBAL.GetCompanyDetails();
                string header = $"Trial Balance {company.Name} as on {General.ConvertDateAppFormat(dateTo)}";
                //string address = $"{company.Description} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, VAT Number:{company.Tin}";
                string header1 = $"Trial Balance as on {General.ConvertDateAppFormat(dateTo)} ";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.TrialBalanceDataTable trialDataTable = new Report.DSReports.TrialBalanceDataTable();
                tblReport = trialDataTable.Clone();
                if (listTrail != null && listTrail.Count > 0)
                {
                    foreach (DAL.Model.TrailBalanceModel pl in listTrail)
                    {
                        DataRow dr = tblReport.NewRow();
                        dr["LedgerName"] = pl.Description;
                        dr["GroupName"] = pl.Description;
                        dr["Debit"] = pl.Debit;
                        dr["Credit"] = pl.Credit;
                        tblReport.Rows.Add(dr);
                    }
                    RPT reportForm = new RPT(RPT.EnumReportType.TrialBalanceSummary, header, header1, tblReport);
                    reportForm.Show();
                }

            }

            catch { throw; }
        }
        public void PrintTrailBalanceSummaryDatewise(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                List<DAL.Model.TrailBalanceDatewiseModel> listTrail = trial.TrialBalanceDatewise(dateFrom, dateTo);


                CompanyBAL companyBAL = new CompanyBAL();
                CompanyModel company = companyBAL.GetCompanyDetails();
                string header = $"Trial Balance {company.Name} as on {General.ConvertDateAppFormat(dateTo)}";
                //string address = $"{company.Description} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, VAT Number:{company.Tin}";
                string header1 = $"Trial Balance as on {General.ConvertDateAppFormat(dateTo)} ";
                DataTable tblReport = new DataTable();
                BETask.Report.DSReports.TrialBalanceDatewiseDataTable trialDataTable = new Report.DSReports.TrialBalanceDatewiseDataTable();
                tblReport = trialDataTable.Clone();
                if (listTrail != null && listTrail.Count > 0)
                {
                    foreach (DAL.Model.TrailBalanceDatewiseModel pl in listTrail)
                    {
                        DataRow dr = tblReport.NewRow();
                        dr["LedgerName"] = pl.Description;
                        dr["GroupName"] = pl.Description;
                        dr["Debit"] = pl.Debit;
                        dr["Credit"] = pl.Credit;
                        dr["OpeningDebit"] = pl.OpeningDebit;
                        dr["OpeningCredit"] = pl.OpeningCredit;
                        dr["ClosingDebit"] = pl.ClosingDebit;
                        dr["ClosingCredit"] = pl.ClosingCredit;
                        tblReport.Rows.Add(dr);
                    }
                    RPT reportForm = new RPT(RPT.EnumReportType.TrialBalanceSummaryDatewise, header, header1, tblReport);
                    reportForm.Show();
                }

            }

            catch { throw; }
        }
    }
}
