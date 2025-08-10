using System;
using EDMX = BETask.DAL.EDMX;
using REP = BETask.DAL.DAL;
using BETask.Model;
using BETask.Common;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace BETask.BAL
{
   public class CommonSettingBAL
    {
        REP.CommonSettingDAL commonSettingDAL = new REP.CommonSettingDAL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tax"></param>
        public void SaveTaxSetting(TaxSettingModel tax)
        {
            try
            {
                EDMX.tax_setting tax_Setting = new EDMX.tax_setting
                {
                    tax_id = tax.Tax_id,
                    description = tax.Description,
                    tax_name = tax.Tax_name,
                    tax_value = tax.Tax_value,
                    status = tax.Status
                };
                commonSettingDAL.SaveTaxSetting(tax_Setting);
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
                    summary = $" Saving tax  { tax.Tax_name} Id ={ tax.Tax_id}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="tax_id"></param>
       /// <returns></returns>
        public List<EDMX.tax_setting> GetAllTaxes(int tax_id)
        {
            try
            {
                return commonSettingDAL.GetAllTaxes(tax_id);
            }
            catch
            {
                throw;
            }
        }
       /// <summary>
       /// 
       /// </summary>
        /// <param name="uom"></param>
        public void SaveUOMSetting(EDMX.uom_setting uom)
        {
            try
            {

                commonSettingDAL.SaveUOMSetting(uom);
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
                    summary = $" Saving UOM  { uom.uom_name} Id ={ uom.uom_id}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uom_id"></param>
        /// <returns></returns>
        public List<EDMX.uom_setting> GetAllUOM(int uom_id)
        {
            try
            {
                return commonSettingDAL.GetAllUOM(uom_id);
            }
            catch
            {
                throw;
            }
        }

    
    }
}
