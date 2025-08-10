using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using System.ComponentModel;
using System.Drawing;

namespace BETask.DAL.DAL
{
   public class CustomerAssetDAL
    {

        private readonly betaskdbEntities _context;
        public CustomerAssetDAL(betaskdbEntities context)
        {
            this._context = context;
        }
        public CustomerAssetDAL()
        {
            
        }

        public void SaveCustomerAsset(EDMX.customer_asset customer_Asset)
        {
            try
            {

                customer_Asset.employee_id = customer_Asset.employee_id == 0 ? null : customer_Asset.employee_id;
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            string agreementNo = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT.ToString());
                            if (context.customer_asset.Any(x => x.customer_id == customer_Asset.customer_id && x.status == 1))
                            {
                                agreementNo = context.customer_asset.FirstOrDefault(x => x.customer_id == customer_Asset.customer_id && x.status == 1).agreement_no.ToString();

                            }
                            else
                            {
                                DocumentSerialDAL document = new DocumentSerialDAL();
                                document.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT, context);
                            }

                            customer_Asset.agreement_no = agreementNo;
                            context.Entry(customer_Asset).State = EntityState.Added;
                            context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateDate(DateTime dateFrom, DateTime dateTo, string agreementNo)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var xAssetList = context.customer_asset.Where(a => a.agreement_no == agreementNo).ToList();
                    if (xAssetList != null)
                    {
                        foreach (var xAsset in xAssetList)
                        {
                            xAsset.agreement_from = dateFrom;
                            xAsset.agreement_to = dateTo;
                            context.customer_asset.Attach(xAsset);
                            context.Entry(xAsset).Property(x => x.agreement_from).IsModified = true;
                            context.Entry(xAsset).Property(x => x.agreement_to).IsModified = true;
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void UpdateAsseFromDelivery(delivery_items dl)
        {
            try
            {
                DocumentSerialDAL documentSerial = new DocumentSerialDAL();
                delivery _delivery = _context.delivery.FirstOrDefault(x => x.delivery_id == dl.delivery_id);
                customer_asset cs = _context.customer_asset.FirstOrDefault(x => x.customer_id == dl.customer_id && x.delivery_type.ToLower() == "delivery" && x.status == 1);
                string docNo = "";
                if (cs != null && !string.IsNullOrEmpty(cs.agreement_no))
                    docNo = cs.agreement_no;
                else
                {
                    
                    docNo = documentSerial.GetNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT.ToString(),_context);
                    DocumentSerialDAL document = new DocumentSerialDAL();
                    document.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT, _context);
                }
                customer_asset asset = new customer_asset
                {
                    agreement_no = docNo,
                    agreement_from = cs == null ? dl.delivery_time : cs.agreement_from,
                    agreement_to = cs == null ? dl.delivery_time.Value.AddYears(1) : cs.agreement_to,
                    employee_id = _delivery.employee_id,
                    amount = dl.net_amount,
                    barcode = "",
                    delivery_date = DateTime.Parse(dl.delivery_time.ToString()),
                    delivery_type = "Delivery",
                    qty = dl.qty,
                    status = 1,
                    remarks = $"delivery-{dl.delivery_id}",
                    updated_on = DateTime.Now,
                    monthly_purchase = 0,
                    customer_id = dl.customer_id,
                    item_id = dl.item_id

                };
                _context.customer_asset.Add(asset);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateAsseFromReturn(delivery_return dl)
        {
            try
            {

              //  delivery _delivery = _context.delivery.FirstOrDefault(x => x.delivery_id == dl.delivery_id);
                customer_asset cs = _context.customer_asset.FirstOrDefault(x => x.customer_id == dl.customer_id && x.delivery_type.ToLower() == "delivery" && x.status == 1);
                string docNo = "";
                if (cs != null && !string.IsNullOrEmpty(cs.agreement_no))
                    docNo = cs.agreement_no;
                else
                {
                    docNo = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT.ToString());
                    DocumentSerialDAL document = new DocumentSerialDAL();
                    document.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT, _context);
                }
                customer_asset asset = new customer_asset
                {
                    agreement_no = docNo,
                    agreement_from = cs == null ? dl.return_date : cs.agreement_from,
                    agreement_to = cs == null ? dl.return_date.AddYears(1) : cs.agreement_to,
                    employee_id = dl.employee_id,
                    amount = 0,
                    barcode = "",
                    delivery_date = DateTime.Parse(dl.return_date.ToString()),
                    delivery_type = "Return",
                    qty = dl.qty,
                    status = 1,
                    remarks = $"return-{dl.delivery_return_id}",
                    updated_on = DateTime.Now,
                    monthly_purchase = 0,
                    customer_id = dl.customer_id,
                    item_id = dl.item_id

                };
                _context.customer_asset.Add(asset);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int SynchCustomerAsset(int routeId = 0, int customerId = 0)
        {
            int cnt = 0;
            List<EDMX.customer> listCustomer = new List<customer>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    CompanyDAL companyDAL = new CompanyDAL();
                    EDMX.system_settings system_Settings = companyDAL.GetSystemSettings();

                    //listCustomer = context.customer.AsNoTracking().Include(p=>p.customer_aggrement).AsNoTracking().Where(p =>p.status == 1 &&(routeId>0?p.route_id==routeId:p.route_id>0) && (customerId>0?p.customer_id==customerId:p.customer_id>0)).ToList();
                    listCustomer = context.customer.AsNoTracking().Include(p => p.customer_aggrement).AsNoTracking().Where(p => p.status == 1 &&
                                    (routeId <= 0 || p.route_id == routeId) &&
                                    (customerId <= 0 || p.customer_id == customerId)).ToList();

                    foreach (EDMX.customer cs in listCustomer)
                    {
                        string agreementNo = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT.ToString());
                        if (context.customer_asset.Any(x => x.customer_id == cs.customer_id))
                        {
                            agreementNo = context.customer_asset.FirstOrDefault(x => x.customer_id == cs.customer_id).agreement_no.ToString();
                        }
                        else
                        {
                            DocumentSerialDAL document = new DocumentSerialDAL();
                            document.UpdateNextDocument(DocumentSerialDAL.EnumDocuments.AGREEMENT, context);
                        }

                        DateTime date = DateTime.Now;

                        if (cs.added_time != null)
                            date = DateTime.Parse(cs.added_time.ToString());

                        var existItems = cs.customer_aggrement.Select(x => x.item_id).ToList();

                        List<customer_asset> xAssets = context.customer_asset.Where(x => x.customer_id == cs.customer_id && (x.delivery_type.ToLower() == "delivery" || x.delivery_type.ToLower() == "return") && x.status == 1
                        && !existItems.Contains(x.item_id)).ToList();
                        if (xAssets != null && xAssets.Count > 0)
                            context.customer_asset.RemoveRange(xAssets);

                        foreach (customer_aggrement ag in cs.customer_aggrement)
                        {

                            int monthlyPurchase = 0;
                            if (ag.item_id == system_Settings.default_item_id)
                                monthlyPurchase = Convert.ToInt32(ag.max_qty);

                            if (!context.customer_asset.Any(x => x.customer_aggrement_id == ag.customer_aggrement_id && x.status == 1))
                            {
                                EDMX.customer_asset asset = new customer_asset
                                {
                                    barcode = ag.serail_number,
                                    amount = ag.unit_price,
                                    qty = ag.max_qty,
                                    item_id = ag.item_id,
                                    other_details = ag.remarks,
                                    status = 1,
                                    customer_id = cs.customer_id,
                                    delivery_date = date,
                                    delivery_type = "Delivery",
                                    customer_aggrement_id = ag.customer_aggrement_id,
                                    remarks = "Synched",
                                    updated_on = DateTime.Now,
                                    agreement_from = cs.added_time,
                                    agreement_to = cs.added_time.Value.AddYears(1),
                                    agreement_no = agreementNo,
                                    employee_id = null,
                                    monthly_purchase = monthlyPurchase,


                                };
                                context.customer_asset.Add(asset);
                                //context.Entry(asset).State = EntityState.Added;

                                cnt++;
                            }
                        }


                    }
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public List<EDMX.customer_asset> GetCustomerAsset(int customerId)
        {
            List<EDMX.customer_asset> listCustomerAsset = new List<customer_asset>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCustomerAsset = context.customer_asset.Include(i => i.item).Include(e=>e.employee).Where(x => x.customer_id == customerId && x.status == 1 && x.qty > 0).ToList();

                }
            }
            catch
            {
                throw;
            }
            return listCustomerAsset;
        }
        public List<EDMX.customer_asset> GetCustomerAssetTransactions(int customerId)
        {
            List<EDMX.customer_asset> listCustomerAsset = new List<customer_asset>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCustomerAsset = context.customer_asset.Include(i => i.item).Include(e => e.employee).Where(x => x.customer_id == customerId && x.status == 1 && (x.delivery_type.ToLower() != "delivery" && x.delivery_type.ToLower() != "return") && x.qty > 0).ToList();

                }
            }
            catch
            {
                throw;
            }
            return listCustomerAsset;
        }
        public List<EDMX.customer_asset> GetCustomerAssetAgreement(int customerId, List<int> selectedList = null, int status = 1)
        {

            List<EDMX.customer_asset> listCustomerAsset = new List<customer_asset>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCustomerAsset = context.customer_asset.Include(i => i.item).Include(e => e.employee).Where(x => x.customer_id == customerId && x.status == status
                     && (x.delivery_type.ToLower() == "delivery" || x.delivery_type.ToLower() == "return") && x.qty > 0 && x.item.agreement_item == 1).ToList();

                    int itemId = context.system_settings.FirstOrDefault(x => x.status == 1).default_item_id;
                    List<customer_asset> listExcluded = new List<customer_asset>();
                    foreach (customer_asset cs in listCustomerAsset)
                    {
                        if ((selectedList != null && selectedList.Count > 0) && !selectedList.Exists(x => x == cs.customer_asset_id))
                        {
                            listExcluded.Add(cs);
                        }
                        if (cs.item_id == itemId && cs.amount == 0)
                        {
                            listCustomerAsset.Remove(cs);
                            break;
                        }
                    }
                    if (listExcluded.Count > 0)
                    {
                        listCustomerAsset.RemoveAll(item => listExcluded.Contains(item));
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomerAsset;
        }
        public List<EDMX.customer_asset> GetAllCustomerAsset(int routeId,string deliveryMode,DateTime fromDate,DateTime toDate,string barcode)
        {
            List<EDMX.customer_asset> listCustomerAsset = new List<customer_asset>();
            try
            {
                using(var context = new betaskdbEntities())
                {
                    if (!string.IsNullOrEmpty(barcode))
                    {
                        listCustomerAsset = context.customer_asset.Include(i => i.customer).Include(x => x.item).Include(x => x.employee).Include(x => x.customer.route).Where(x => x.status == 1 && (routeId > 0 ? x.customer.route_id == routeId : x.customer.route_id > 0)).Where(x =>x.barcode.Contains(barcode)).OrderBy(x => x.customer_id).ToList();
                    }
                    else
                    {
                        listCustomerAsset = context.customer_asset.Include(i => i.customer).Include(x => x.item).Include(x => x.employee).Include(x => x.customer.route).Where(x => x.status == 1 && (routeId > 0 ? x.customer.route_id == routeId : x.customer.route_id > 0)).Where(x => x.delivery_date >= fromDate && x.delivery_date <= toDate).OrderBy(x => x.customer_id).ToList();
                        if (!string.IsNullOrEmpty(deliveryMode))
                        {
                            listCustomerAsset = listCustomerAsset.Where(x => x.delivery_type.ToLower() == deliveryMode.ToLower()).ToList();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCustomerAsset;
        }
        public int CloseAgreement(int customerId, string agreement)
        {
            int resp = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<customer_asset> listAsset = context.customer_asset.Where(x => x.customer_id == customerId && x.agreement_no == agreement).ToList();
                    if (listAsset.Count > 0)
                    {
                        foreach (customer_asset asset in listAsset)
                        {
                            
                            asset.status = 5;
                            //asset.remarks = $"{asset.remarks} Finished:{DateTime.Now}";
                            context.customer_asset.Attach(asset);
                            context.Entry(asset).Property(x => x.status).IsModified = true;
                            context.SaveChanges();
                            resp++;
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;
        }

        public void UpdateAssetDetails(customer_asset asset)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer_asset xAsset = context.customer_asset.FirstOrDefault(x => x.customer_asset_id == asset.customer_asset_id);
                    if (xAsset != null)
                    {
                        
                        xAsset.barcode = asset.barcode;
                        xAsset.remarks = asset.remarks;
                        xAsset.monthly_purchase = asset.monthly_purchase;
                        context.customer_asset.Attach(xAsset);
                        context.Entry(xAsset).Property(x => x.barcode).IsModified = true;
                        context.Entry(xAsset).Property(x => x.remarks).IsModified = true;
                        context.Entry(xAsset).Property(x => x.monthly_purchase).IsModified = true;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public customer_asset CloseAssetItem(int assetId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer_asset xAsset = context.customer_asset.FirstOrDefault(x => x.customer_asset_id == assetId);
                    if (xAsset != null)
                    {

                        xAsset.status = 5;
                        context.customer_asset.Attach(xAsset);
                        context.Entry(xAsset).Property(x => x.status).IsModified = true;
                        context.SaveChanges();
                    }
                    return xAsset;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public decimal GetItemRateExistingAgreement(int customerId,int itemId)
        {
            decimal rate = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    customer_asset asset = context.customer_asset.FirstOrDefault(x => x.customer_id == customerId && x.item_id == itemId && x.status == 1 && x.amount>0);

                    if (asset != null)
                        rate = asset.amount;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rate;
        }

    }
}
