using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETaskSync.Common;
using BETaskAPI.DAL.DAL;
using BETaskSync.EDMX;
using edmx= BETaskAPI.DAL.EDMX;
using edmxLocal = BETaskSync.EDMX;
using System.Data.Entity;

namespace BETaskSync.Repository
{

    public class CustomerSyncRep
    {
        SyncDAL customerDAL = new SyncDAL();
        public List<edmx.customer_temp> GetCustomerList(int status = 3)
        {
           
            List<edmx.customer_temp> listCustomer = new List<edmx.customer_temp>();
            try
            {
                listCustomer = customerDAL.GetCustomerList(status);
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
            }
            return listCustomer;
        }
        public edmx.customer_temp GetCustomerTempDetails(int tempId)
        {
            SyncDAL customerDAL = new SyncDAL();
            edmx.customer_temp customer = new edmx.customer_temp();
            try
            {
                customer = customerDAL.GetCustomerTemDetails(tempId);
            }
            catch (Exception ex)
            {
                General.Error(ex.ToString());
            }
            return customer;
        }

        public int SaveCustomer(customer _customer)
        {
            int customerId = _customer.customer_id;
            if (_customer.building_id == 0)
                _customer.building_id = null;
            try
            {
                using (betaskdbEntitiesLocal context = new betaskdbEntitiesLocal())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            customer xCustomer = context.customer.Where(x => x.customer_temp_id == _customer.customer_temp_id).FirstOrDefault();
                            if (xCustomer != null)
                                return 0;

                            if (_customer.building_id == 0)
                                _customer.building = null;

                            _customer.route_id = _customer.route_id == 0 ? null : _customer.route_id;
                            _customer.ledger_id = _customer.customer_id == 0 ? null : _customer.ledger_id;

                            List<customer_aggrement> listAgreement = _customer.customer_aggrement.ToList();
                            _customer.customer_aggrement = null;

                            context.Entry(_customer).State = _customer.customer_id == 0 ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();


                            //Saving agreement
                            if (listAgreement != null && listAgreement.Count > 0)
                            {
                                foreach (customer_aggrement ag in listAgreement)
                                {
                                    ag.customer_id = _customer.customer_id;
                                    ag.status = 1;
                                    context.Entry(ag).State = EntityState.Added;

                                }
                                context.SaveChanges();
                            }

                            if (customerId == 0)
                            {
                                LedgerMappingDAL objLedgerMapDAL = new LedgerMappingDAL();
                                AccountLedgerDAL ledgerDAL = new AccountLedgerDAL();
                                EDMX.ledger_mapping mapLedger = new EDMX.ledger_mapping();
                                string custType = _customer.customer_type == 1 ? "CUSTOMER" : "SUPPLIER";
                                if (_customer.customer_type == 1)
                                {
                                    mapLedger = objLedgerMapDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.CUSTOMER);
                                }
                                else
                                {
                                    mapLedger = objLedgerMapDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMapGroupTypes.SUPPLIER);
                                }

                                if (mapLedger != null && mapLedger.group_id > 0)
                                {
                                    string ledgerName = _customer.customer_name;
                                    try
                                    {
                                        ledgerName = _customer.customer_name.Length <= 50 ? _customer.customer_name : _customer.customer_name.Substring(0, 49);
                                    }
                                    catch { }
                                    EDMX.account_ledger ledger = new EDMX.account_ledger()
                                    {
                                        group_id = Convert.ToInt32(mapLedger.group_id),
                                        ledger_name = $"{ ledgerName}",
                                        description = custType,
                                        status = 1,
                                    };
                                    int ledgerId = ledgerDAL.SaveAccountLedger(ledger);
                                    if (ledgerId > 0)
                                    {
                                        _customer.ledger_id = ledgerId;
                                        context.Entry(_customer).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                }
                                customerId = _customer.customer_id;
                            }
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            if (transaction != null)
                                transaction.Rollback();
                            string e = ee.ToString();
                            throw;

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return customerId;
        }

        public int GetRouteId(string routename)
        {
            int routeId = 0;
            try
            {
                using (betaskdbEntitiesLocal context = new betaskdbEntitiesLocal())
                {
                    EDMX.route route = context.route.Where(x => x.route_name.ToLower() == routename.ToLower()).FirstOrDefault();
                    if (route != null)
                        routeId = route.route_id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return routeId;
        }
        public int GetBuilding(string buildingName)
        {
            int buidingId = 0;
            try
            {
                using (betaskdbEntitiesLocal context = new betaskdbEntitiesLocal())
                {
                    EDMX.building building = context.building.Where(x => x.building_name.ToLower() == buildingName.ToLower()).FirstOrDefault();
                    if (building != null)
                        buidingId = building.building_id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return buidingId;
        }
        public EDMX.item GetItem(int itemId)
        {
            EDMX.item item = new EDMX.item();
            try
            {
                using (betaskdbEntitiesLocal context = new betaskdbEntitiesLocal())
                {
                    item = context.item.Include(t=>t.tax_setting).Where(x => x.item_id==itemId).FirstOrDefault();
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return item;
        }

        public void UpdateCustomerTempStatus(int customerTempId,int status=4)
        {
            try
            {
                SyncDAL customerDAL = new SyncDAL();
                customerDAL.UpdateCustomerTempStatus(customerTempId,status);

            }
            catch(Exception ex)
            {
                General.Error(ex.ToString()); ;
            }

        }

        public EDMX.customer GetCustomerLocal(int customerId)
        {
            EDMX.customer customer = new customer();
            try
            {
                using (betaskdbEntitiesLocal context = new betaskdbEntitiesLocal())
                {
                    customer = context.customer.Include(a=>a.customer_aggrement).Where(x => x.customer_id == customerId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return customer;
        }
        public List<EDMX.customer> GetCustomerLocalManual(List<int> customerIds)
        {
           List< EDMX.customer> customer = new List<EDMX.customer>();
            try
            {
                using (betaskdbEntitiesLocal context = new betaskdbEntitiesLocal())
                {
                    customer = context.customer.Include(a => a.customer_aggrement).Where(x => (customerIds.Contains(x.customer_id))).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return customer;
        }

        public void SaveCustomerToApp(edmx.customer _customer)
        {
            try
            {
               // _customer.customer_aggrement = null;
                customerDAL.SaveCustomerFromLocal(_customer);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
