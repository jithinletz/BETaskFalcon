using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;

namespace BETask.DAL.DAL
{
    public class CompanyDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public void SaveCompany(company _company)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var xCompany = context.company.AsNoTracking().Where(x => x.company_id == _company.company_id).FirstOrDefault();
                    _company.cloud_connection = xCompany.cloud_connection;
                    context.Entry(_company).State = _company.company_id == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public company GetCompany(out system_settings settings)
        {
            company objCompany = new company();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objCompany = context.company.AsNoTracking().FirstOrDefault();
                    settings=context.system_settings.AsNoTracking().FirstOrDefault();
                }
            }
            catch  
            {
                throw;
            }
            return objCompany;
        }
        public mail_settings GetMailSettings()
        {
            mail_settings _mail = new mail_settings();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _mail = context.mail_settings.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return _mail;
           

        }
        public bool CheckFinancialDate(DateTime docDate)
        {
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    company comp = context.company.AsNoTracking().Where(x => x.status == 1).FirstOrDefault();
                    if (comp != null)
                    {
                        if (comp.financial_from_date.Year >= docDate.Year && comp.financial_to_date.Year <= docDate.Year)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get financial years");
            }
            return false;
        }
        public DateTime GetSoftwareStartDate()
        {
            DateTime date = DateTime.Today;
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    date = context.system_settings.FirstOrDefault(x=>x.status==1).software_startdate;
                    /*
                    var opening = context.account_transaction.AsNoTracking().Where(x => x.transaction_type == "OPENING").ToList();
                    if(opening!=null && opening.Count>0)
                        date= opening.Min(X => X.transaction_date);
                    else
                    {
                        var firstEntry = context.account_transaction.Where(x => x.transaction_type != "OPENING").DefaultIfEmpty(null).Min(x => x.transaction_date);
                        if(firstEntry!=null)
                        firstEntry = firstEntry.AddDays(-1);
                    }
                    */

                }
            }
            catch (Exception ex)
            {
                
             //   throw new Exception("Unable to get start date");
            }
            return date;
        }
        public EDMX.system_settings GetSystemSettings()
        {
           
            using (betaskdbEntities context = new betaskdbEntities())
            {
                return context.system_settings.FirstOrDefault(x => x.status == 1);
            }

            }



    }
}
