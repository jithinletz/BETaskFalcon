using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BETaskAPI.DAL.DAL
{
  public  class SyncDAL
    {
        public List<customer> GetCustomerList(int status=3)
        {
            List<customer> result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    result = context.customer.Where(c => c.status == status).ToList();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public customer GetCustomerTemDetails(int customerId)
        {
            customer result = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    result = context.customer.Include(a=>a.customer_aggrement).Where(c => c.customer_id == customerId).FirstOrDefault();
                }

            }
            catch
            {
                throw;
            }

            return result;

        }
        public void UpdateCustomerTempStatus(int customerTempId,int status=4)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer customer_Temp = context.customer.Where(x => x.customer_id == customerTempId).FirstOrDefault();
                    if (customer_Temp != null)
                    {
                        customer_Temp.status = status;
                        context.Entry(customer_Temp).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

            }
            catch
            {
                throw;
            }

        }

        public void SaveCustomerFromLocal(customer _customer)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var xCustomer = context.customer.AsNoTracking().Where(x => x.customer_id == _customer.customer_id).FirstOrDefault();
                    if (xCustomer == null)
                    {
                        context.Entry(_customer).State = EntityState.Added;
                        context.SaveChanges();
                    }
                   


                }
            }
            catch
            {
                throw;
            }
        }

    }
}
