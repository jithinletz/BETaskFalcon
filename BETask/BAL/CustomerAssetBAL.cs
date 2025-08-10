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
using BETask.DAL.DAL;

namespace BETask.BAL
{
    public class CustomerAssetBAL
    {
        BETask.DAL.DAL.CustomerAssetDAL customerDAL = new REP.CustomerAssetDAL();
        BETask.DAL.DAL.CustomerAssetDAL customerAssetDAL = new REP.CustomerAssetDAL();

        RouteDAL routeDAL = new RouteDAL();
        public int SaveCustomerAsset(EDMX.customer_asset customer)
        {
            int customerId = 0;
            try
            {
                customerDAL.SaveCustomerAsset(customer);
                if (customer.delivery_type.ToLower() == "delivery" || customer.delivery_type.ToLower() == "return")
                {
                    CustomerAggrementBAL customerAggrementBAL = new CustomerAggrementBAL();
                    customerAggrementBAL.UpdateCustomerAgreementFromAsset(customer);
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
                    reference_id = customer.customer_asset_id,
                    summary = $" Saving Customer Asset  Customer {customer.customer_id} ,Employee {customer.employee_id} ,Item {customer.item_id} , Qty {customer.qty},Amount {customer.amount} , Barcode {customer.barcode},ReturnType{customer.delivery_type},AgreementFrom{customer.agreement_from},AgreementTo{customer.agreement_to},AssetDetails{customer.other_details},Date {customer.delivery_date},AgreementNO{customer.agreement_no}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }

            return customerId;

        }

        public void UpdateDate(DateTime dateFrom, DateTime dateTo, string agreementNo)
        {
            try
            {
                customerDAL.UpdateDate(dateFrom, dateTo, agreementNo);
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
                    reference_id = Convert.ToInt32(agreementNo),
                    summary = $" Updating customer agreement",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }

        }

        public List<EDMX.route> GetAllRoutes()
        {

            try
            {

                return routeDAL.GetAllRoutes();

            }
            catch (Exception ee)
            {
                throw;
            }

        }
        public List<EDMX.customer_asset> GetCustomerAsset(int customerId)
        {
            try
            {
                return customerDAL.GetCustomerAsset(customerId);
            }
            catch { throw; }
        }
        public List<EDMX.customer_asset> GetCustomerAssetTransactions(int customerId)
        {
            try
            {
                return customerDAL.GetCustomerAssetTransactions(customerId);
            }
            catch { throw; }
        }

        public int SynchCustomerAsset(int routeId, int customerId = 0)
        {
            try
            {
                return customerAssetDAL.SynchCustomerAsset(routeId, customerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDMX.customer_asset> GetAllCustomerAsset(int routeId, string deliveryMode, DateTime fromDate, DateTime toDate, string barcode)
        {
            try
            {
                return customerDAL.GetAllCustomerAsset(routeId, deliveryMode, fromDate, toDate, barcode);
            }
            catch { throw; }
        }
        public List<EDMX.customer_asset> GetCustomerAssetAgreement(int customerId, List<int> selectedList = null, int status = 1)
        {
            try
            {
                return customerDAL.GetCustomerAssetAgreement(customerId, selectedList, status);
            }
            catch { throw; }
        }
        public int CloseAgreement(int customerId, string agreement)
        {
            try
            {
                int res = customerDAL.CloseAgreement(customerId, agreement);
                CustomerAggrementBAL aggrementBAL = new CustomerAggrementBAL();
                aggrementBAL.CloseCustomerAgreement(customerId);
                return res; ;
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
                    reference_id = customerId,
                    summary = $" Closing of agreement {agreement}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void UpdateAssetDetails(EDMX.customer_asset asset)
        {
            try
            {
                customerDAL.UpdateAssetDetails(asset);
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
                    reference_id = asset.customer_id,
                    summary = $"Update Customer asset {asset.barcode} , {asset.monthly_purchase}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public void CloseAssetItem(int assetId)
        {
            try
            {
                var asset = customerDAL.CloseAssetItem(assetId);
                CustomerAggrementBAL aggrementBAL = new CustomerAggrementBAL();
                aggrementBAL.CloseCustomerAgreementItem(asset);
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
                    reference_id = assetId,
                    summary = $"Close Customer asset",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }


        public void AgreementPrint(int customerId, List<int> selectedList)
        {
            try
            {
                List<BETask.DAL.EDMX.customer_asset> listCustomerAsset = customerDAL.GetCustomerAssetAgreement(customerId, selectedList);
                if (listCustomerAsset != null && listCustomerAsset.Count > 0)
                {
                    CompanyBAL companyBAL = new CompanyBAL();
                    CustomerBAL customerBAL = new CustomerBAL();
                    CustomerModel customer = customerBAL.GetCustomerDetail(customerId);
                    CompanyModel company = companyBAL.GetCompanyDetails();
                    string FirstParty = $" {company.Address1} {company.Address2} {company.City} Phone:{company.Phone}, Email:{company.Email}, TRN:{company.Tin}";
                    string SecondParty = $"{customer.Customer_Name} \n {customer.Address1} {customer.Address2} {customer.City} Phone:{customer.Phone}, Mobile:{customer.Mobile}, Email:{customer.Email},ContactPerson:{customer.ContactPerson}";
                    BETask.Report.DSReports.CustomerAssetAgreementReportDataTable customerAssetAgreementDataTable = new BETask.Report.DSReports.CustomerAssetAgreementReportDataTable();
                    DataTable tblData = customerAssetAgreementDataTable.Clone();
                    string rateQty = "";
                    DAL.DAL.CompanyDAL companyDAL = new DAL.DAL.CompanyDAL();
                    EDMX.system_settings system_Settings = companyDAL.GetSystemSettings();
                    BETask.DAL.EDMX.customer_asset defaultAsset = listCustomerAsset.Where(x => x.item_id == system_Settings.default_item_id && x.status == 1 && x.delivery_type.ToLower() == "delivery").OrderByDescending(x => x.customer_asset_id).FirstOrDefault();


                    ItemDAL itemDAL = new ItemDAL();
                    foreach (BETask.DAL.EDMX.customer_asset asset in listCustomerAsset)
                    {


                        decimal tax = 0, amount = 0;
                        EDMX.item item = itemDAL.GetItemDteials(asset.item_id);
                        if (item != null)
                        {
                            tax = Convert.ToDecimal(item.tax_setting.tax_value);
                        }
                        if (asset.amount > 0)
                        {
                            decimal taxValue = General.TruncateDecimalPlaces((asset.amount * tax) / 100, 2);
                            amount = asset.amount + taxValue;

                        }
                        if (defaultAsset != null && defaultAsset.item_id == asset.item_id)
                        {
                            rateQty = $"{amount}-{defaultAsset.monthly_purchase}";
                        }

                        if (asset.delivery_type.ToLower() == "delivery")
                        {
                            DataRow dr = tblData.NewRow();
                            dr["DeliveryDate"] = General.ConvertDateAppFormat(asset.delivery_date);
                            dr["AssetName"] = asset.item.item_name;
                            dr["Barcode"] = asset.barcode;
                            dr["Qty"] = asset.qty;
                            dr["DeliveryType"] = asset.delivery_type;
                            dr["Amount"] = amount;
                            dr["AgreementNo"] = asset.agreement_no;
                            dr["AgreementTo"] = asset.agreement_to.Value.ToShortDateString();
                            dr["AgreementFrom"] = asset.agreement_from.Value.ToShortDateString();
                            dr["Remarks"] = asset.remarks;
                            //dr["CustomerName"] = asset.customer;
                            tblData.Rows.Add(dr);
                        }
                        //While return -qty
                        else
                        {
                            foreach (DataRow dr in tblData.Rows)
                            {
                                if (dr["AssetName"].ToString() == asset.item.item_name)
                                {
                                    decimal qty = Convert.ToDecimal(dr["Qty"]);
                                    qty = qty - asset.qty;
                                    dr["Qty"] = qty;
                                }
                            }
                        }
                    }
                    if (tblData != null && tblData.Rows.Count > 0)
                    {

                        BETask.Report.ReportForm reportForm = new BETask.Report.ReportForm(BETask.Report.ReportForm.EnumReportType.CustomerAssetAgreement, FirstParty, SecondParty, rateQty, tblData);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Print(int customerId, string header, int reportType, List<int> selectedList)
        {
            try
            {
                BAL.CompanyBAL companyBAL = new CompanyBAL();
                string companyAddress = companyBAL.GetCompanyAddress();

                List<BETask.DAL.EDMX.customer_asset> listCustomerAsset = new List<EDMX.customer_asset>();

                if (reportType == 0)
                    listCustomerAsset = GetCustomerAssetAgreement(customerId, selectedList);
                else if (reportType == 1)
                    listCustomerAsset = GetCustomerAsset(customerId);
                else if (reportType == 2)
                    listCustomerAsset = GetCustomerAssetTransactions(customerId);
                if (reportType == 3)
                    listCustomerAsset = GetCustomerAssetAgreement(customerId, selectedList, 5);
                if (listCustomerAsset != null && listCustomerAsset.Count > 0)
                {
                    BETask.Report.DSReports.CustomerAssetDataTable customerAssetDataTable = new BETask.Report.DSReports.CustomerAssetDataTable();
                    DataTable tblData = customerAssetDataTable.Clone();

                    int retQty = 0;
                    foreach (BETask.DAL.EDMX.customer_asset asset in listCustomerAsset)
                    {

                        DataRow dr = tblData.NewRow();
                        dr["DeliveryDate"] = General.ConvertDateAppFormat(asset.delivery_date);
                        dr["AssetName"] = asset.item.item_name;
                        dr["Barcode"] = asset.barcode;
                        dr["Qty"] = asset.qty;
                        dr["Employee"] = asset.employee != null ? $"{asset.employee.first_name} {asset.employee.last_name}" : "";
                        dr["ReturnType"] = asset.delivery_type;
                        dr["AssetDetails"] = asset.other_details;
                        tblData.Rows.Add(dr);


                    }
                    if (tblData != null && tblData.Rows.Count > 0)
                    {
                        BETask.Report.ReportForm reportForm = new BETask.Report.ReportForm(BETask.Report.ReportForm.EnumReportType.CustomerAsset, $"Customer - {header}", General.companyName, companyAddress, tblData);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void PrintAll(int routeId, string header, string deliveryMode, DateTime fromDate, DateTime toDate, string barcode)
        {
            try
            {
                List<BETask.DAL.EDMX.customer_asset> listCustomerAsset = customerDAL.GetAllCustomerAsset(routeId, deliveryMode, fromDate, toDate, barcode);
                if (listCustomerAsset != null && listCustomerAsset.Count > 0)
                {
                    BETask.Report.DSReports.CustomerAssetReportDataTable customerAssetReportDataTable = new BETask.Report.DSReports.CustomerAssetReportDataTable();
                    DataTable tblData = customerAssetReportDataTable.Clone();
                    foreach (BETask.DAL.EDMX.customer_asset asset in listCustomerAsset)
                    {
                        DataRow dr = tblData.NewRow();
                        dr["DeliveryDate"] = General.ConvertDateAppFormat(asset.delivery_date);
                        dr["AssetName"] = asset.item.item_name;
                        dr["Qty"] = asset.qty;
                        dr["Barcode"] = asset.barcode;
                        dr["Employee"] = asset.employee != null ? $"{asset.employee.first_name} {asset.employee.last_name}" : "";
                        dr["ReturnType"] = asset.delivery_type;
                        dr["AssetDetails"] = asset.other_details;
                        dr["RouteName"] = asset.customer.route.route_name;
                        dr["CustomerName"] = asset.customer.customer_name;
                        tblData.Rows.Add(dr);
                    }
                    if (tblData != null && tblData.Rows.Count > 0)
                    {
                        BETask.Report.ReportForm reportForm = new BETask.Report.ReportForm(BETask.Report.ReportForm.EnumReportType.CustomerAssetAll, $"Customer Asset Report between {General.ConvertDateAppFormat(fromDate)} and {General.ConvertDateAppFormat(toDate)} - {header}", tblData);
                        reportForm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
