using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.APP.EDMX;
using System.Data.Entity;
using System.Data;
namespace BETask.APP.DAL
{
    public class CustomerAppDAL
    {
        public List<EDMX.customer_upload> GetAllCustomerDocuments(int CustomerId)
        {
            List<EDMX.customer_upload> listAllCustomerDocuments;
            try
            {
               using(var context = new betaskdbEntitiesAPP())
               {
                    listAllCustomerDocuments = context.customer_upload.Where(x => x.customer_id == CustomerId && x.status == 1).ToList();
               }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return listAllCustomerDocuments;
        }
        public List<EDMX.customer_temp> GetPendingCustomers()
        {
            List<EDMX.customer_temp> listPendingCustomer;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    listPendingCustomer = context.customer_temp.Where(x => x.status == 3).OrderBy(x => x.route).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listPendingCustomer;
        }
        public List<EDMX.customer_aggrement_temp> GetCustomerAgreementTemp(int _customerId)
        {
            List<EDMX.customer_aggrement_temp> listAgreement;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    int customerId =Convert.ToInt32( context.customer.AsNoTracking().Where(x => x.customer_id == _customerId).FirstOrDefault().customer_temp_id.GetValueOrDefault(0));
                    listAgreement = context.customer_aggrement_temp.Where(x => x.customer_temp_id== customerId && x.status == 1 ).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listAgreement;
        }
        /// <summary>
        /// Status 3=Pending , 4=Saved , 2= De avtivated or removed
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<EDMX.customer_temp> GetCustomersTemp(int status)
        {
            List<EDMX.customer_temp> listPendingCustomer;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    listPendingCustomer = context.customer_temp.Where(x => x.status == status).OrderBy(x => x.route).ThenBy(x => x.customer_name).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listPendingCustomer;
        }

        public EDMX.customer GetCustomerApp(int customerId)
        {
            EDMX.customer customer;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    customer = context.customer.FirstOrDefault(x => x.customer_id ==customerId);
                }
            }
            catch
            {
                throw;
            }
            return customer;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_customer"></param>
        public void SaveCustomer(customer _customer, int selectedPendingCusId)
        {
            try
            {
                if (_customer.building_id ==0)
                    _customer.building_id = null;
                if (_customer.employee_id == 0)
                    _customer.employee_id = null;
                if (_customer.offer_id == 0)
                    _customer.offer_id = null;
                using (var context = new betaskdbEntitiesAPP())
                {
                    var _cust = context.customer.AsNoTracking().Where(c => c.customer_id == _customer.customer_id).FirstOrDefault();
                    if (_cust != null)
                    {
                        _customer.customer_temp_id = _cust.customer_temp_id;
                        _customer.app_address1 = _cust.app_address1;
                        _customer.app_address2= _cust.app_address1;
                        _customer.app_customer_name = _cust.app_address1;
                        _customer.app_email = _cust.app_email;
                        _customer.app_password = _cust.app_password;
                        _customer.app_phone = _cust.app_phone;
                    }

                    context.Entry(_customer).State = _cust == null ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();
                    if (selectedPendingCusId > 0)
                    {
                        var _pendcust = context.customer_temp.Where(c => c.customer_id == selectedPendingCusId).FirstOrDefault();
                        _pendcust.status = 4;
                        context.Entry(_pendcust).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public async void UpdateCustomerWallet(int customerId,decimal walletBalance)
        {
            
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    customer xCust = context.customer.FirstOrDefault(x => x.customer_id == customerId);
                    if (xCust != null)
                    {
                        xCust.wallet_balance = walletBalance;
                        context.customer.Attach(xCust);
                        context.Entry(xCust).Property(x => x.wallet_balance).IsModified = true;
                        context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }

        public async Task<int> UpdateCustomerWallet(List<EDMX.customer> listCustomer)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    foreach (customer cs in listCustomer)
                    {
                        customer xCust = context.customer.FirstOrDefault(x => x.customer_id == cs.customer_id);
                        if (xCust != null)
                        {
                            xCust.wallet_balance = cs.wallet_balance??0;
                            context.customer.Attach(xCust);
                            context.Entry(xCust).Property(x => x.wallet_balance).IsModified = true;
                            result++;
                        }
                        context.SaveChanges();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;

        }

        public void SaveCustomerAggrement(List<EDMX.customer_aggrement> items, int customerId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    List<EDMX.customer_aggrement> aggrements = context.customer_aggrement.Where(p => p.customer_id == customerId).ToList();
                    if (aggrements != null && aggrements.Count > 0)
                    {
                        context.customer_aggrement.RemoveRange(aggrements);
                        context.SaveChanges();
                    }
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

        public void UpdateCustomerAggrementFromAsset(EDMX.customer_aggrement item)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
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
                using (var context = new betaskdbEntitiesAPP())
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

        public void RemoveAgreement(int itemId,decimal qty, int customerId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    customer_aggrement aggrement = context.customer_aggrement.FirstOrDefault(x => x.customer_id == customerId && x.item_id == itemId && x.max_qty==qty && x.status==1);
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
        public void UpdateCustomerStatus(int custId, int status, string customerName)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    var _pendcust = context.customer_temp.Where(c => c.customer_id == custId).FirstOrDefault();
                    _pendcust.status = status;
                    _pendcust.customer_name = customerName;
                    context.Entry(_pendcust).State = EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch
            {
                throw;
            }
        }
        public List<customer> SelectFilterCustomer(List<int> customerIds)
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {

                    listCustomer = context.customer.AsNoTracking().Where(x => customerIds.Contains(x.customer_id)).ToList();

                }
            }
            catch
            {
                throw;
            }
            return listCustomer;
        }
        public void SaveCustomerDivision(EDMX.customer_division division)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    customer_division xDiv = context.customer_division.FirstOrDefault(x => x.division_id == division.division_id);
                    if (xDiv != null)
                    {
                        xDiv.division_name = division.division_name;
                        context.Entry(xDiv).State = EntityState.Modified;
                    }
                    else
                        context.Entry(division).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
    }
}
