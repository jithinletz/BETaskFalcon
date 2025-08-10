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

    public class AccountTransactionBAL
    {

        DAL.DAL.AccountTransactionDAL accountTransactionDAL = new REP.AccountTransactionDAL();

        public int SaveAccountTransaction(List<EDMX.account_transaction> listAcountTransaction, Enum transactionType,ref string referance)
        {
            int transaction = 0;
            try
            {
                transaction = accountTransactionDAL.SaveAccountTransactionList(listAcountTransaction, transactionType,ref referance);
            }
            catch
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                decimal amount = listAcountTransaction[0].credit == 0 ? listAcountTransaction[0].debit : listAcountTransaction[0].credit;
                string summary = $" Saving {transactionType.ToString()},Amount={amount} Narration={listAcountTransaction[0].narration} ";
                if (summary.Length > 250)
                {
                    summary = summary.Substring(0, 249);
                }
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = transaction,
                    summary = summary,

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
               
            }
            return transaction;
        }
        public void SaveAccountTransactionCheque(EDMX.account_transaction_cheque cheque)
        {
            try
            {
                accountTransactionDAL.SaveAccountTransactionCheque(cheque);
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.account_transaction> SearchTransaction(DateTime dateFrom, DateTime dateTo, Enum tranType,int ledgerId=0,string search="")
        {
            try
            {
                return accountTransactionDAL.SearchTransaction(dateFrom, dateTo, tranType, ledgerId, search);
            }
            catch
            {
                throw;
            }
        }
        public EDMX.account_transaction_cheque GetCheque(int transactionNumber)
        {
            try
            {
                return accountTransactionDAL.GetCheque(transactionNumber);
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.account_transaction> GetAccountTransactionDetail(int transactionNumber, string transactionType = "")
        {
            try
            {
                return accountTransactionDAL.GetAccountTransactionDetail(transactionNumber, transactionType);
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.account_transaction> GetPaymentRecieptReport(int ledger, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return accountTransactionDAL.GetPaymentRecieptReport(ledger, dateFrom, dateTo);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetAccountTransactionStatement(DateTime dateFrom, DateTime dateTo, out decimal openingDebit, out decimal openingCredit, int ledgerId = 0,bool hideNotReconsiled=true, bool includeCostCneter = false, int primaryCostCenter = 0, int costCenter = 0,string search="",int groupId=0)
        {
            try
            {
                return accountTransactionDAL.GetAccountTransactionStatement(dateFrom, dateTo, out openingDebit, out openingCredit, ledgerId,hideNotReconsiled,includeCostCneter,primaryCostCenter,costCenter, search,groupId);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.DaybookModel> Daybook(DateTime dateFrom)
        {
            try
            {
                return accountTransactionDAL.Daybook(dateFrom);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.DaybookDetailedModel> DaybookDetailed(DateTime dateFrom)
        {
            try
            {
                return accountTransactionDAL.DaybookDetailed(dateFrom);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.DaybookModel> CustomerStatementSummary(DateTime dateTo, int ledgerId, int customerType = 1, int routeId = 0)
        {
            try
            {
                return accountTransactionDAL.CustomerStatementSummary(dateTo, ledgerId, customerType, routeId);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.DaybookModel> CustomerStatementSummaryBySalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int salesmanLedger, out decimal openingDebit, out decimal openingCredit, int customerType = 1, int routeId = 0)
        {
            try
            {
                return accountTransactionDAL.CustomerStatementSummaryBySalesman(dateFrom, dateTo, ledgerId, salesmanLedger, out openingDebit, out openingCredit, customerType, routeId);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.DaybookModel> CustomerStatementSummaryBySalesman(DateTime dateTo, int ledgerId, int salesmanLedger, int customerType = 1, int routeId = 0)
        {
            try
            {
                return accountTransactionDAL.CustomerStatementSummaryBySalesman(dateTo, ledgerId, salesmanLedger, customerType, routeId);
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.account_transaction> CustomerStatementDetailed(DateTime dateFrom, DateTime dateTo, int ledgerId, out decimal openingDebit, out decimal openingCredit, int routeId = 0)
        {
            try
            {
                return accountTransactionDAL.CustomerStatementDetailed(dateFrom, dateTo, ledgerId, out openingDebit, out openingCredit, routeId);
            }
            catch
            {
                throw;
            }
        }

        public List<EDMX.account_transaction> CustomerStatementDetailedBySalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int salesmanLedger, out decimal openingDebit, out decimal openingCredit)
        {
            try
            {
                return accountTransactionDAL.CustomerStatementDetailedBySalesman(dateFrom, dateTo, ledgerId, salesmanLedger,out openingDebit, out openingCredit);
            }
            catch
            {
                throw;
            }
        }
        public List<DAL.Model.DaybookModel> CustomerStatementSummaryBySalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int salesmanLedger, int customerType = 1, int routeId = 0)
        {
            try
            {
                return accountTransactionDAL.CustomerStatementSummaryBySalesman(dateFrom, dateTo, ledgerId, salesmanLedger, customerType, routeId);
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.account_transaction> GetOpening(int routeId = 0)
        {
            try
            {
                return accountTransactionDAL.GetOpening(routeId);
            }
            catch
            {
                throw;
            }
        }
        public void UpdateOpening(int transactionId, int ledgerId, decimal debit, decimal credit)
        {
            try
            {
                accountTransactionDAL.UpdateOpening(transactionId, ledgerId, debit, credit);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);
                decimal amount = debit > 0 ? debit : credit;
                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = transactionId,
                    summary = $" Updating Opeinng  Amount={amount} debit={debit}, credit={credit}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<DAL.Model.CashStatementRoutewiseModel> GetCashStatementRoutewise(DateTime date, int routeId)
        {
            try
            {
                return accountTransactionDAL.GetCashStatementRoutewise(date, routeId);
            }
            catch (Exception ee)
            {
                throw;
            }
        }


        public List<DAL.Model.ReconciliationModel> ReconciliationStatement(DateTime dateFrom, DateTime dateTo, int ledgerId, string mode)
        {
            try
            {
                return accountTransactionDAL.ReconciliationStatement(dateFrom, dateTo, ledgerId, mode);
            }
            catch
            { throw; }
        }
        public decimal GetLedgerOpening(int ledgerId,DateTime dateFrom,DateTime dateTo)
        {
            try
            {
                return accountTransactionDAL.GetLedgerOpening(ledgerId,dateFrom,dateTo);
            }
            catch
            { throw; }
        }
        public List<DAL.Model.ReconciliationModel> ReconciliationStatementReconciled(DateTime dateFrom, DateTime dateTo, int ledgerId)
        {
            try
            {
                return accountTransactionDAL.ReconciliationStatementReconciled(dateFrom, dateTo, ledgerId);
            }
            catch
            { throw; }
        }

        public decimal GetReconciledBalance(int ledgerId, DateTime dateFrom, DateTime dateTo,decimal _reconciledBalance)
        {
            try
            {
                return accountTransactionDAL.GetReconciledBalance(ledgerId, dateFrom, dateTo, _reconciledBalance);
            }
            catch
            { throw; }
        }

        public void SaveReconciliation(List<DAL.Model.ReconcilUpdateModel> listTransaction, string dateFrom, string datetTo, string bank)
        {
            try
            {
                accountTransactionDAL.SaveReconciliation(listTransaction);
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
                    module_action = "Reconciliation",
                    reference_id = 0,
                    summary = $" Reconciliation done for the date {dateFrom} - {datetTo} {bank}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void RemoveReconciliation(int transactionNumber)
        {
            try
            {
                accountTransactionDAL.RemoveReconciliation(transactionNumber);
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
                    module_action = "RemoveReconciliation",
                    reference_id = 0,
                    summary = $" Reconciliation removed for the transaction {transactionNumber}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<DAL.Model.RoutewiseCashbookModel> RoutewiseCashbook(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return accountTransactionDAL.RoutewiseCashbook(dateFrom, dateTo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateRouteInTransaction(DateTime date)
        {
            try
            {
                General.Error($"\n{General.batchNumber} - UpdateRouteInTransaction");

                accountTransactionDAL.UpdateRouteInTransaction(date);
                General.Error($"\n{General.batchNumber} - completed UpdateRouteInTransaction");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #region Print
        public void PrintJournalVoucher(int transactionNumebr,string tranType="JOURNAL")
        {
            List<EDMX.account_transaction> listAccount = null;
            try
            {
                listAccount = accountTransactionDAL.GetAccountTransactionDetail(transactionNumebr,tranType);
                if (listAccount != null && listAccount.Count >= 0)
                {
                    DataTable tblJournal = new DataTable();
                    BETask.Report.DSReports.JournalVoucherDataTable journalVoucherDataTable = new Report.DSReports.JournalVoucherDataTable();
                    tblJournal = journalVoucherDataTable.Clone();

                    BAL.CompanyBAL companyBAL = new CompanyBAL();

                    string companyAddress = companyBAL.GetCompanyAddress();
                    if (listAccount[0].transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.PETTY.ToString()|| listAccount[0].transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.JOURNAL.ToString() || listAccount[0].transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.OPENING.ToString())
                    {
                        foreach (EDMX.account_transaction account in listAccount)
                        {
                            if (account != null)
                            {
                                DataRow dataRow = tblJournal.NewRow();
                                dataRow["TransactionNumber"] = account.transaction_type_id;
                                dataRow["Ledger"] = account.account_ledger.ledger_name;
                                dataRow["Debit"] = account.debit;
                                dataRow["Credit"] = account.credit;
                                dataRow["Narration"] = account.narration;
                                dataRow["TransactionDate"] = General.ConvertDateAppFormat(account.transaction_date);
                                dataRow["AmountInWord"] = General.NumToWord(account.debit, false);
                                tblJournal.Rows.Add(dataRow);
                            }
                        }
                    }

                    if (tblJournal != null && tblJournal.Rows.Count > 0)
                    {
                        string header = General.companyName;
                        RPT reportForm = new RPT(RPT.EnumReportType.Journal, header,companyAddress,tranType, tblJournal);
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
                    reference_id = 0,
                    summary = $" Print Payment Voucher Report {listAccount[0].transaction_number}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintPaymentVoucher(int transactionNumebr,string tranType="Payment Voucher")
        {
            List<EDMX.account_transaction> listAccount = null;
            try
            {
                listAccount = accountTransactionDAL.GetAccountTransactionDetail(transactionNumebr);
                if (listAccount != null && listAccount.Count >= 0)
                {
                    DataTable tblPayment = new DataTable();
                    DataTable tblPaymentDetails = new DataTable();
                    BETask.Report.DSReports.PaymentVoucherDataTable paymentVoucherDataTable = new Report.DSReports.PaymentVoucherDataTable();
                    tblPayment = paymentVoucherDataTable.Clone();
                    BETask.Report.DSReports.PaymentVoucherDetailsDataTable paymentVoucherDetailsDataTable = new Report.DSReports.PaymentVoucherDetailsDataTable();
                    tblPaymentDetails = paymentVoucherDetailsDataTable.Clone();

                    BAL.CompanyBAL companyBAL = new CompanyBAL();
                    string companyAddress = companyBAL.GetCompanyAddress();
                    if (listAccount[0].transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.PAYMENT.ToString())
                    {
                        foreach (EDMX.account_transaction account in listAccount)
                        {
                            if (account != null)
                            {

                                if (account.credit > 0)
                                {
                                    DataRow dataRow = tblPayment.NewRow();
                                    dataRow["TransactionNumber"] = account.transaction_number;
                                    dataRow["PaymentNumber"] = account.transaction_type_id;
                                    dataRow["CompanyAccount"] = account.account_ledger.ledger_name;
                                    dataRow["CompanyAmount"] = account.credit;
                                    dataRow["MainNarration"] = account.narration;
                                    dataRow["VoucherDate"] = General.ConvertDateAppFormat(account.transaction_date);
                                    dataRow["AmountInWord"] = General.NumToWord(account.credit, false);
                                    tblPayment.Rows.Add(dataRow);
                                }
                                else
                                {
                                    DataRow dataRow = tblPaymentDetails.NewRow();
                                    dataRow["Ledger"] = account.account_ledger.ledger_name;
                                    dataRow["LedgerAmount"] = account.debit;
                                    dataRow["LedgerNarration"] = account.narration;
                                    dataRow["VoucherNumber"] = account.voucher_number;
                                    tblPaymentDetails.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                    else if (listAccount[0].transaction_type == DAL.DAL.AccountTransactionDAL.EnumTransactionTypes.RECIEPT.ToString())
                    {
                        foreach (EDMX.account_transaction account in listAccount)
                        {
                            if (account != null)
                            {

                                if (account.debit > 0)
                                {
                                    DataRow dataRow = tblPayment.NewRow();
                                    dataRow["TransactionNumber"] = account.transaction_number;
                                    dataRow["PaymentNumber"] = account.transaction_type_id;
                                    dataRow["CompanyAccount"] = account.account_ledger.ledger_name;
                                    dataRow["CompanyAmount"] = account.debit;
                                    dataRow["MainNarration"] = account.narration;
                                    dataRow["VoucherDate"] = General.ConvertDateAppFormat(account.transaction_date);
                                    dataRow["AmountInWord"] = General.NumToWord(account.debit, false);
                                    tblPayment.Rows.Add(dataRow);
                                }
                                else
                                {
                                    DataRow dataRow = tblPaymentDetails.NewRow();
                                    dataRow["Ledger"] = account.account_ledger.ledger_name;
                                    dataRow["LedgerAmount"] = account.credit;
                                    dataRow["LedgerNarration"] = account.narration;
                                    dataRow["VoucherNumber"] = account.voucher_number;
                                    tblPaymentDetails.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                    if (tblPayment != null && tblPayment.Rows.Count > 0)
                    {
                        string header = $"{General.companyName}";
                        RPT reportForm = new RPT(header, companyAddress,tranType ,tblPayment, tblPaymentDetails);
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
                    reference_id = 0,
                    summary = $" Print Payment Voucher Report {listAccount[0].transaction_number}  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintPaymentReport(DataTable tblData, string header)
        {
            try
            {

                RPT reportForm = new RPT(BETask.Report.ReportForm.EnumReportType.PaymentReport, header, tblData);
                reportForm.Show();
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
                    summary = $" Print Payment Reciept Report  ",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintAccountStatement(DataTable tblStatement, DateTime dateFrom, DateTime dateTo,decimal openingDebit, decimal openingCredit, string headerText = "")
        {
            string header = $"{headerText} Ledger statement between {General.ConvertDateAppFormat(dateFrom)} and {General.ConvertDateAppFormat(dateTo)}";
            CompanyBAL companyBAL = new CompanyBAL();
            CompanyModel company = companyBAL.GetCompanyDetails();
            string address = $"{company.Name} \n {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, TRN:{company.Tin}";

            try
            {
                //List<EDMX.account_transaction> listItemTransaction = GetAccountTransactionStatement(dateFrom, dateTo, out openingDebit,out openingCredit,ledgerId);
                if (tblStatement != null && tblStatement.Rows.Count > 0)
                {
                    //header += $" of {listItemTransaction[0].item.item_name}";
                    DataTable tblItemsList = new DataTable();
                    BETask.Report.DSReports.AccountStatementDataTable itemsListDataTable = new Report.DSReports.AccountStatementDataTable();
                    tblItemsList = itemsListDataTable.Clone();
                    if (openingDebit != 0 || openingCredit != 0)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["Date"] = dateFrom.AddDays(-1);
                        dataRow["Ledger"] = "Opening";
                        dataRow["Debit"] = openingDebit;
                        dataRow["Credit"] = openingCredit;
                        dataRow["Type"] = "Opening";
                        dataRow["Narration"] = "Opening";
                        tblItemsList.Rows.Add(dataRow);
                    }
                    foreach (DataRow dr in tblStatement.Rows)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["Date"] = General.ConvertDateAppFormat(Convert.ToDateTime(dr["transaction_date"]));
                        dataRow["Ledger"] = dr["ledger_name"];
                        dataRow["Debit"] = dr["debit"];
                        dataRow["Credit"] = dr["credit"];
                        dataRow["Type"] = dr["transaction_type"];
                        dataRow["Narration"] = dr["narration"];
                        tblItemsList.Rows.Add(dataRow);

                    }
                    if (tblItemsList != null && tblItemsList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.AccountStatement, header, address, tblItemsList);
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
                    summary = $" Account Statement Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintCustomerAccountStatement(DateTime dateTo, int ledgerId, int customerType, int routeId, string header)
        {
            string custType = customerType == 1 ? "Customer" : "Supplier";

            try
            {

                List<DAL.Model.DaybookModel> listAccountTransaction = CustomerStatementSummary(dateTo, ledgerId, customerType, routeId);
                if (listAccountTransaction != null && listAccountTransaction.Count > 0)
                {
                    //header += $" of {listItemTransaction[0].item.item_name}";
                    DataTable tblCustomerList = new DataTable();
                    BETask.Report.DSReports.CustomerStatementSummaryDataTable customerStatementSummaryDataTable = new Report.DSReports.CustomerStatementSummaryDataTable();
                    tblCustomerList = customerStatementSummaryDataTable.Clone();

                    decimal totalOutstanding = 0, totalAdvance = 0;
                    foreach (DAL.Model.DaybookModel tran in listAccountTransaction)
                    {

                        decimal outstanding = 0, advance = 0;
                        if (customerType == 1)
                        {
                            if ((tran.Credit - tran.Debit) < 0)
                            {
                                outstanding = tran.Debit - tran.Credit;
                                totalOutstanding += outstanding;
                            }
                            else
                            {
                                advance = tran.Credit - tran.Debit;
                                totalAdvance += advance;
                            }
                        }
                        else
                        {
                            if ((tran.Credit - tran.Debit) < 0)
                            {
                                advance = tran.Debit - tran.Credit;
                                totalAdvance += advance;
                            }
                            else
                            {
                                outstanding = tran.Credit - tran.Debit;
                                totalOutstanding += outstanding;
                            }
                        }

                        // foreach (DAL.Model.DaybookModel item in listAccountTransaction)
                        {
                            DataRow dataRow = tblCustomerList.NewRow();
                            dataRow["PartyName"] = tran.TransactionType;
                            dataRow["Debit"] = tran.Debit;
                            dataRow["Credit"] = tran.Credit;
                            dataRow["Outstanding"] = outstanding;
                            dataRow["Advance"] = advance;

                            tblCustomerList.Rows.Add(dataRow);

                        }

                    }
                    if (tblCustomerList != null && tblCustomerList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.CustomerStatementSummary, header, tblCustomerList);
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
                    summary = $"Customer Statement SUmmary Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintCustomerAccountStatementDetailed(DataTable tblData, string header)
        {
            try
            {
                if (tblData != null && tblData.Rows.Count > 0)
                {
                    RPT reportForm = new RPT(RPT.EnumReportType.CustomerStatementDetailed, header, tblData);
                    reportForm.Show();
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
                    summary = $"Customer Statement Detailed Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintCustomerAccountStatementSalesman(DateTime dateFrom, DateTime dateTo, int ledgerId, int customerType, int routeId, string header, bool hideOpening)
        {
            string custType = customerType == 1 ? "Customer" : "Supplier";

            try
            {

                decimal openingDebit = 0, openingCredit = 0;

                List<DAL.Model.DaybookModel> listAccountTransaction = new List<DAL.Model.DaybookModel>();
                if (hideOpening)
                    listAccountTransaction = CustomerStatementSummaryBySalesman(dateFrom, dateTo, 0, ledgerId, out openingDebit, out openingCredit, customerType, routeId);
                else
                {
                    listAccountTransaction = CustomerStatementSummaryBySalesman(dateTo, 0, ledgerId, customerType, routeId);
                }
                if (listAccountTransaction != null && listAccountTransaction.Count > 0)
                {
                    //header += $" of {listItemTransaction[0].item.item_name}";
                    DataTable tblCustomerList = new DataTable();
                    BETask.Report.DSReports.CustomerStatementSummaryDataTable customerStatementSummaryDataTable = new Report.DSReports.CustomerStatementSummaryDataTable();
                    tblCustomerList = customerStatementSummaryDataTable.Clone();

                    decimal totalOutstanding = 0, totalAdvance = 0;
                    foreach (DAL.Model.DaybookModel tran in listAccountTransaction)
                    {

                        decimal outstanding = 0, advance = 0;
                        if (customerType == 1)
                        {
                            if ((tran.Credit - tran.Debit) < 0)
                            {
                                outstanding = tran.Debit - tran.Credit;
                                totalOutstanding += outstanding;
                            }
                            else
                            {
                                advance = tran.Credit - tran.Debit;
                                totalAdvance += advance;
                            }
                        }
                        else
                        {
                            if ((tran.Credit - tran.Debit) < 0)
                            {
                                advance = tran.Debit - tran.Credit;
                                totalAdvance += advance;
                            }
                            else
                            {
                                outstanding = tran.Credit - tran.Debit;
                                totalOutstanding += outstanding;
                            }
                        }

                        // foreach (DAL.Model.DaybookModel item in listAccountTransaction)
                        {
                            DataRow dataRow = tblCustomerList.NewRow();
                            dataRow["PartyName"] = tran.TransactionType;
                            dataRow["Debit"] = tran.Debit;
                            dataRow["Credit"] = tran.Credit;
                            dataRow["Outstanding"] = outstanding;
                            dataRow["Advance"] = advance;

                            tblCustomerList.Rows.Add(dataRow);

                        }

                    }
                    if (tblCustomerList != null && tblCustomerList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.CustomerStatementSummary, header, tblCustomerList);
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
                    summary = $"Customer Statement SUmmary Print {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintDaybookSummary(DateTime dateFrom)
        {

            string header = $"Daybook Summary for the date  {General.ConvertDateAppFormat(dateFrom)}";
            try
            {
                List<DAL.Model.DaybookModel> listItemTransaction = Daybook(dateFrom);
                if (listItemTransaction != null && listItemTransaction.Count > 0)
                {
                    //header += $" of {listItemTransaction[0].item.item_name}";
                    DataTable tblItemsList = new DataTable();
                    BETask.Report.DSReports.DaybookSummaryDataTable daybookSummaryDataTable = new Report.DSReports.DaybookSummaryDataTable();
                    tblItemsList = daybookSummaryDataTable.Clone();
                    foreach (DAL.Model.DaybookModel item in listItemTransaction)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["TransactionType"] = item.TransactionType;
                        dataRow["Debit"] = item.Debit;
                        dataRow["Credit"] = item.Credit;
                        tblItemsList.Rows.Add(dataRow);

                    }
                    if (tblItemsList != null && tblItemsList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.DaybookSummary, header, tblItemsList);
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
                    summary = $" Print Daybook Summary {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        public void PrintDaybookDetailed(DateTime dateFrom)
        {

            string header = $"Daybook Detailed for the date  {General.ConvertDateAppFormat(dateFrom)}";
            try
            {
                List<DAL.Model.DaybookDetailedModel> listItemTransaction = DaybookDetailed(dateFrom);
                if (listItemTransaction != null && listItemTransaction.Count > 0)
                {
                    //header += $" of {listItemTransaction[0].item.item_name}";
                    DataTable tblItemsList = new DataTable();
                    BETask.Report.DSReports.DaybookDetailedDataTable daybookDetailedDataTable = new Report.DSReports.DaybookDetailedDataTable();
                    tblItemsList = daybookDetailedDataTable.Clone();
                    foreach (DAL.Model.DaybookDetailedModel item in listItemTransaction)
                    {
                        DataRow dataRow = tblItemsList.NewRow();
                        dataRow["TransactionGroup"] = item.TransactionGroup;
                        dataRow["TransactionType"] = item.TransactionType;
                        dataRow["Debit"] = item.Debit;
                        dataRow["Credit"] = item.Credit;
                        tblItemsList.Rows.Add(dataRow);

                    }
                    if (tblItemsList != null && tblItemsList.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.DaybookDetailed, header, tblItemsList);
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
                    summary = $" Print Daybook Detailed {header} ",
                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void PrintRoutewiseCashbook(List<BETask.DAL.Model.RoutewiseCashbookModel> listRoutewiseCashbookModel, string header)
        {
            try
            {
                if (listRoutewiseCashbookModel != null && listRoutewiseCashbookModel.Count > 0)
                {

                    DataTable tblRoutewiseCashbook = new DataTable();
                    BETask.Report.DSReports.RoutewiseCashbookDataTable RoutewiseCashbookDatatable = new Report.DSReports.RoutewiseCashbookDataTable();
                    tblRoutewiseCashbook = RoutewiseCashbookDatatable.Clone();
                    foreach (BETask.DAL.Model.RoutewiseCashbookModel objRoutewiseCashbook in listRoutewiseCashbookModel)
                    {
                        if (objRoutewiseCashbook.RouteId > 1)
                        {
                            DataRow dataRow = tblRoutewiseCashbook.NewRow();

                            dataRow["RouteName"] = objRoutewiseCashbook.RouteName;
                            dataRow["Debit"] = objRoutewiseCashbook.Debit;
                            dataRow["Credit"] = objRoutewiseCashbook.Credit;
                            tblRoutewiseCashbook.Rows.Add(dataRow);
                        }
                    }
                    if (tblRoutewiseCashbook != null && tblRoutewiseCashbook.Rows.Count > 0)
                    {
                        RPT reportForm = new RPT(RPT.EnumReportType.RoutewiseCashbook, header, tblRoutewiseCashbook);
                        reportForm.Show();
                    }
                }

            }
            catch
            {
                throw;
            }


            #endregion Print

        }

    }
}
