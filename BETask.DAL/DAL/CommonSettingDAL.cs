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
   public class CommonSettingDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledger"></param>
        public void SaveTaxSetting(tax_setting tax)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    context.Entry(tax).State = tax.tax_id == 0 ? EntityState.Added : EntityState.Modified;
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
        /// <param name="group_id"></param>
        /// <returns></returns>
        public List<tax_setting> GetAllTaxes(int tax_id)
        {
            List<tax_setting> listTax = new List<tax_setting>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listTax = context.tax_setting.Where(x => x.status == 1 && (tax_id >= 0 ? (x.tax_id == tax_id) : x.tax_id >= 0)).OrderBy(x => x.tax_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listTax;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ledger"></param>
        public void SaveUOMSetting(uom_setting uom)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    context.Entry(uom).State = uom.uom_id == 0 ? EntityState.Added : EntityState.Modified;
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
        /// <param name="group_id"></param>
        /// <returns></returns>
        public List<uom_setting> GetAllUOM(int uom_id)
        {
            List<uom_setting> listUom = new List<uom_setting>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listUom = context.uom_setting.Where(x => x.status == 1 && (uom_id >= 0 ? (x.uom_id == uom_id) : x.uom_id >= 0)).OrderBy(x => x.uom_id).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listUom;
        }
    }
}
