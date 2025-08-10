using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Diagnostics;
using BETask.Common;

namespace BETask.BAL
{

    class CompanyBAL
    {
        CompanyDAL objCompanyDAL = null;

       public CompanyBAL() {
            objCompanyDAL = new CompanyDAL();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CompanyModel GetCompanyDetails()
        {
            CompanyModel company = null;
            try
            {
                DAL.EDMX.system_settings settings;
                EDMX.company _company = objCompanyDAL.GetCompany(out settings);
                if (_company != null)
                {
                    company = new CompanyModel()
                    {
                        Address1 = _company.address1,
                        Address2 = _company.address2,
                        City = _company.city,
                        Description = _company.description,
                        Company_id = _company.company_id,
                        Email = _company.email,
                        Mobile = _company.mobile,
                        Name = _company.company_name,
                        Phone = _company.phone,
                        POBox = _company.pobox,
                        Street = _company.street,
                        Tin = _company.tin,
                        Web = _company.web,
                        Cloud_Connection = _company.cloud_connection,
                        FinancialDateFrom=_company.financial_from_date,
                        FinancialDateTo = _company.financial_to_date,
                    };
                }
                General.cloudConnection = company.Cloud_Connection;
                General.companyId = company.Company_id;
                General.companyName = company.Name;
                DAL.DAL.CompanyDAL companyDAL = new CompanyDAL();
                General.softwareStartDate = companyDAL.GetSoftwareStartDate();
                General.viewLoadingDsr = Convert.ToBoolean(settings.view_loading_in_dsr);
            }
            catch
            {
                throw;
            }

            return company;
        }

        public string GetCompanyAddress()
        {
            string address = string.Empty;
            try
            {
                Model.CompanyModel company = GetCompanyDetails();
               
                address = String.Format("{0} {1} {2} {3} {4} {5} {6} {7} ", company.Address1 != "" ? company.Address1 : "", company.Address2!= "" ? company.Address2 : "", company.City != "" ? company.City : "", company.Street != "" ? company.Street : "", company.Email != "" ? company.Email : "", company.Phone != "" ? company.Phone : "", company.Mobile != "" ? company.Mobile : "", company.Web != "" ? company.Web : "");
            }
            catch (Exception ee)
            {
                throw;
            }
            return address;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        public void SaveCompany(CompanyModel _company) {
            try
            {
                if (_company != null) {
                    EDMX.company company = new EDMX.company()
                    {
                        address1= _company.Address1,
                        address2 = _company.Address2,
                        city = _company.City,
                        description = _company.Description,
                        company_id = _company.Company_id,
                        email = _company.Email,
                        mobile = _company.Mobile,
                        company_name = _company.Name,
                        phone = _company.Phone,
                        pobox = _company.POBox,
                        street = _company.Street,
                        tin = _company.Tin,
                        web = _company.Web,
                        financial_from_date= _company.FinancialDateFrom,
                        financial_to_date = _company.FinancialDateTo,
                        status=1
                    };
                    objCompanyDAL.SaveCompany(company);
                }
            }
            catch {
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
                    summary = $" Updating Company details { _company.Description}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }

        }

        public bool CheckFinancialDate(DateTime date)
        {
            try
            {
                CompanyDAL company = new CompanyDAL();
                if (!company.CheckFinancialDate(date))
                    return false;
                return true;
            }
            catch { throw; }
        }


        public void SetMailDefaults()
        {
            try {
                var mail = objCompanyDAL.GetMailSettings();
                Model.MailSettingModel.bcc1 = mail.bcc1;
                Model.MailSettingModel.bcc2 = mail.bcc2;
                Model.MailSettingModel.cc1 = mail.cc1;
                Model.MailSettingModel.cc2 = mail.cc2;
                Model.MailSettingModel.enable_ssl = mail.enable_ssl;
                Model.MailSettingModel.from_mail = mail.from_mail;
                Model.MailSettingModel.mail_id = mail.mail_id;
                Model.MailSettingModel.password = mail.password;
                Model.MailSettingModel.smtp_host = mail.smtp_host;
                Model.MailSettingModel.smtp_port = mail.smtp_port;
                Model.MailSettingModel.smtp_timeout = mail.smtp_timeout;
                Model.MailSettingModel.smtp_use_deafaultcredential = mail.smtp_use_deafaultcredential;
            }
            catch
            {
                throw;
            }
        }
      
    }
}
