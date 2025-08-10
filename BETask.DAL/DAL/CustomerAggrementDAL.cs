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
    public class CustomerAggrementDAL
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<customer_aggrement> GetCustomerAggrements(int customerId,int status=1)
        {
            List<customer_aggrement> lstCustomerAggrements = new List<customer_aggrement>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (status == 0)
                    {
                        lstCustomerAggrements = lstCustomerAggrements = context.customer_aggrement.AsNoTracking().Include(x => x.item).Include(u => u.item.uom_setting).Include(v => v.item.tax_setting)
                       .Where(x => x.customer_id == customerId).ToList();
                    }
                    else
                    {
                        lstCustomerAggrements = context.customer_aggrement.AsNoTracking().Include(x => x.item).Include(u => u.item.uom_setting).Include(v => v.item.tax_setting)
                            .Where(x => x.customer_id == customerId && x.status == status).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return lstCustomerAggrements;
        }
        public customer_aggrement GetSingleAgreementById(int agreementId)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.customer_aggrement.FirstOrDefault(x => x.customer_aggrement_id == agreementId);
                }
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="customerId"></param>
        public void SaveCustomerAggrement(List<EDMX.customer_aggrement> items, int customerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    List<EDMX.customer_aggrement> aggrements = context.customer_aggrement.Where(p => p.customer_id == customerId).ToList();
                    if (aggrements != null && aggrements.Count > 0)
                    {
                        context.customer_aggrement.RemoveRange(aggrements);
                        context.SaveChanges();
                    }
                    context.customer_aggrement.AddRange(items);
                    foreach (EDMX.customer_aggrement item in items)
                    {
                        context.Entry(item).State = EntityState.Added;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                string err = ee.ToString();
                throw;
            }

        }
        public void RemoveAgreement(int agreementId, int customerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer_aggrement aggrement = context.customer_aggrement.FirstOrDefault(x => x.customer_id == customerId && x.customer_aggrement_id == agreementId);
                    if (aggrement != null)
                    {
                        aggrement.status = 2;
                        aggrement.remarks = $"{aggrement.remarks} Removed on {DateTime.Now.ToString("dd/MM/yyyy")}";
                        context.Entry(aggrement).Property(x => x.status).IsModified = true;
                        context.Entry(aggrement).Property(x => x.remarks).IsModified = true;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateCustomerAggrementFromAsset(EDMX.customer_aggrement item)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    customer_aggrement aggrement = context.customer_aggrement.FirstOrDefault(x => x.customer_id == item.customer_id && x.item_id == item.item_id && x.status == 1 && x.max_qty > 0 && x.unit_price > 0);
                    if (aggrement != null)
                    {
                        aggrement.max_qty += item.max_qty;
                        aggrement.unit_price = item.unit_price;
                        context.Entry(aggrement).State = EntityState.Modified;
                    }
                    else
                        context.Entry(item).State = EntityState.Added;

                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                string err = ee.ToString();
                throw;
            }

        }
        public void CloseAgreement(int customerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<customer_aggrement> listAgree = context.customer_aggrement.Where(x => x.customer_id == customerId && x.status == 1).ToList();
                    foreach (customer_aggrement ag in listAgree)
                    {
                        ag.status = 2;
                        context.customer_aggrement.Attach(ag);
                        context.Entry(ag).Property(x => x.status).IsModified = true;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                string err = ee.ToString();
                throw;
            }
        }
        public List<customer_aggrement> CloseAgreementItem(customer_asset asset)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer_aggrement agree = context.customer_aggrement.FirstOrDefault(x => x.customer_id == asset.customer_id && x.status == 1 && x.item_id == asset.item_id && x.max_qty == asset.qty);

                    if (agree != null)
                    {
                        agree.status = 2;
                        context.customer_aggrement.Attach(agree);
                        context.Entry(agree).Property(x => x.status).IsModified = true;
                        context.SaveChanges();
                    }
                    else
                    {
                        customer_aggrement aggrement = context.customer_aggrement.FirstOrDefault(x => x.status == 1 && x.max_qty > asset.qty && x.item_id == asset.item_id && x.customer_id == asset.customer_id);
                        if (aggrement != null)
                        {
                            aggrement.max_qty = aggrement.max_qty - asset.qty;
                            context.customer_aggrement.Attach(aggrement);
                            context.Entry(aggrement).Property(x => x.max_qty).IsModified = true;
                            context.SaveChanges();
                        }
                    }
                    return context.customer_aggrement.Where(x => x.customer_id == asset.customer_id && x.status == 1).ToList();
                }
            }
            catch (Exception ee)
            {
                string err = ee.ToString();
                throw;
            }
        }
    }
}
