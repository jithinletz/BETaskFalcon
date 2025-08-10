using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BETaskAPI.DAL.DAL
{
    public class CustomerAggrementDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public  customer_aggrement  GetCustomerAggrement(int customerId,int itemId)
        {
            customer_aggrement result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    result = context.customer_aggrement.AsNoTracking().Where(c => c.customer_id == customerId && c.item_id == itemId).OrderByDescending(x=>x.max_qty).FirstOrDefault(); 
                }

            }
            catch
            {
                throw;
            }

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<customer_aggrement> GetCustomerAggrement(int customerId)
        {
           List<customer_aggrement> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    result = context.customer_aggrement.AsNoTracking().Include(c => c.item).AsNoTracking().Include(t=>t.item.tax_setting).AsNoTracking().
                        Where(c => c.customer_id == customerId && c.status==1).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public List<customer_aggrement> GetCustomerAggrementDefault(int customerId,int itemId)
        {
            List<customer_aggrement> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    result = context.customer_aggrement.AsNoTracking().Include(c => c.item).AsNoTracking().Include(t => t.item.tax_setting).AsNoTracking().
                        Where(c => c.customer_id == customerId && c.status == 1 && c.item_id==itemId).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }






    }
}
