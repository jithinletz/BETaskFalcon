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
    public class PDCBAL
    {
        DAL.DAL.PDCDAL pdcdal = new REP.PDCDAL();
        public void SavePDC(EDMX.pdc pdc)
        {
            int pdcId = 0;
            try
            {
                pdcId= pdcdal.SavePDC(pdc);
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
                    reference_id = pdcId,
                    summary = $" Saving PDC,Amount={pdc.amount} Narration={pdc.remarks}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public List<EDMX.pdc> SearchPDC(string cheuqeStatus, DateTime chequeDate, DateTime chequeDateTo)
        {
            try
            {
                return pdcdal.SearchPDC(cheuqeStatus, chequeDate, chequeDateTo);
            }
            catch
            {
                throw;
            }
        }
        public EDMX.pdc SearchPDCbyId(int pdcId)
        {
            try
            {
                return pdcdal.SearchPDCbyId(pdcId);
            }
            catch
            {
                throw;
            }
        }
        public void DeletePDC(int pdcId)
        {
            try
            {
                pdcdal.DeletePDC(pdcId);
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
                    reference_id = pdcId,
                    summary = $" DeletePDC PDC ={pdcId}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void UpdatePDC(int pdcId, string status,DateTime date)
        {
            try
            {
                pdcdal.UpdatePDC(pdcId, status,date);
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
                    reference_id = pdcId,
                    summary = $" Update PDC ={pdcId} {status}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public string CheckPDCMissingConfiguration()
        {
            try
            {
                BETask.DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new BETask.DAL.DAL.LedgerMappingDAL();

                return ledgerMappingDAL.CheckPDCMissingConfiguration();
            }
            catch { throw; }
        }
        public void Print(int printId,string header)
        {
            try
            {
                EDMX.pdc pdc = SearchPDCbyId(printId);
                if (pdc != null)
                {
                    DataTable tblPDC = new DataTable();
                    BETask.Report.DSReports.PDCInvoiceDataTable pdcInvoiceDataTable = new Report.DSReports.PDCInvoiceDataTable();
                    tblPDC = pdcInvoiceDataTable.Clone();
                    DataRow dataRow = tblPDC.NewRow();
                    dataRow["PDCId"] = pdc.pdc_id;
                    dataRow["PDCMode"] = pdc.pdc_mode;
                    dataRow["CollectedDate"] =General.ConvertDateAppFormat(pdc.doc_date);
                    dataRow["ChequeDate"] = General.ConvertDateAppFormat(pdc.cheque_date);
                    dataRow["ChequeNumber"] = pdc.cheque_number;
                    dataRow["Amount"] = pdc.amount;
                    dataRow["PartyAccount"] = pdc.account_ledger1.ledger_name;
                    dataRow["Remarks"] = pdc.remarks;
                    dataRow["LastUpdate"] = General.ConvertDateAppFormat(pdc.updated_on);
                    dataRow["ChequeStatus"] = pdc.cheque_status;
                    dataRow["BankAccount"] = pdc.account_ledger.ledger_name;
                    tblPDC.Rows.Add(dataRow);
                    if (tblPDC.Rows.Count > 0)
                    {
                        BAL.CompanyBAL companyBAL = new CompanyBAL();
                        string companyAddress = companyBAL.GetCompanyAddress();
                        RPT reportForm = new RPT(RPT.EnumReportType.PDCInvoice,header, companyAddress, tblPDC);
                        reportForm.ShowDialog();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

       
        public void PrintAll(string cheuqeStatus, DateTime chequeDate, DateTime chequeDateTo)
        {
            try
            {
                List<EDMX.pdc> listPDC= pdcdal.SearchPDC(cheuqeStatus, chequeDate, chequeDateTo);
                if (listPDC != null && listPDC.Count > 0)
                {
                    DataTable tblPDC = new DataTable();
                    BETask.Report.DSReports.PDCInvoiceDataTable pdcInvoiceDataTable = new Report.DSReports.PDCInvoiceDataTable();
                    tblPDC = pdcInvoiceDataTable.Clone();
                    foreach (EDMX.pdc pdc in listPDC)
                    {
                        DataRow dataRow = tblPDC.NewRow();
                        dataRow["PDCId"] = pdc.pdc_id;
                        dataRow["PDCMode"] = pdc.pdc_mode;
                        dataRow["CollectedDate"] = General.ConvertDateAppFormat(pdc.doc_date);
                        dataRow["ChequeDate"] = General.ConvertDateAppFormat(pdc.cheque_date);
                        dataRow["ChequeNumber"] = pdc.cheque_number;
                        dataRow["Amount"] = pdc.amount;
                        dataRow["PartyAccount"] = pdc.account_ledger1.ledger_name;
                        dataRow["Remarks"] = pdc.remarks;
                        dataRow["LastUpdate"] = General.ConvertDateAppFormat(pdc.updated_on);
                        dataRow["ChequeStatus"] = pdc.cheque_status;
                        dataRow["BankAccount"] = pdc.account_ledger.ledger_name;
                        tblPDC.Rows.Add(dataRow);
                    }
                    if (tblPDC.Rows.Count > 0)
                    {
                        BAL.CompanyBAL companyBAL = new CompanyBAL();
                        
                        string header = $"PDC Report {cheuqeStatus}";
                        RPT reportForm = new RPT(RPT.EnumReportType.PDCReport, header, tblPDC);
                        reportForm.ShowDialog();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public int GetPDCTransactionNumber(EDMX.pdc pdc)
        {
            try
            {
                return pdcdal.GetPDCTransactionNumber(pdc);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
