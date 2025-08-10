using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace BETask.DAL.DAL
{
    public class DeliveryDAL
    {
        public List<int> GenerateDeliveryId(DateTime dateFrom, DateTime dateTo, List<EDMX.employee> listEmployee)
        {
            List<int> deliveryIds = new List<int>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            int totalDays = (dateTo - dateFrom).Days + 1;
                            DateTime date = dateFrom;
                            for (int day = 1; day <= totalDays; day++)
                            {
                                foreach (employee emp in listEmployee)
                                {
                                    string routeName = context.route.AsNoTracking().Where(x => x.route_id == emp.route_id).FirstOrDefault().route_name;
                                    EDMX.delivery xDelivery = context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.employee_id == emp.employee_id && x.route_id == emp.route_id).FirstOrDefault();
                                    if (xDelivery == null)
                                    {
                                        delivery _delivery = new delivery
                                        {
                                            delivery_date = date,
                                            employee_id = emp.employee_id,
                                            route_id = emp.route_id,
                                            delivery_route = routeName,
                                            customer_count = 0,
                                            gross_amount = 0,
                                            total_discount = 0,
                                            total_beforevat = 0,
                                            total_vat = 0,
                                            net_amount = 0,
                                            remarks = string.Empty,
                                            status = 1
                                        };
                                        context.Entry(_delivery).State = EntityState.Added;
                                        context.SaveChanges();
                                        deliveryIds.Add(_delivery.delivery_id);
                                    }
                                    else
                                    {
                                        deliveryIds.Add(xDelivery.delivery_id);
                                    }
                                }
                                date = date.AddDays(1);
                            }
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
            return deliveryIds;
        }

        public DataTable GetLocalDeliveryReport(int deliveryId)
        {
            DataSet ds = new DataSet();
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetDeliveryItemsByDeliveryId", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {

                                adr.Fill(ds);

                            }
                        }
                    }

                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return ds.Tables[0];
        }
        public List<int> GenerateDeliveryId(DateTime date)
        {
            List<int> deliveryIds = new List<int>();
            try
            {

                using (var context = new betaskdbEntities())
                {
                    List<EDMX.employee> listEmployee = context.employee.AsNoTracking().Where(x => x.status == 1).ToList();
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            foreach (employee emp in listEmployee)
                            {
                                if (emp.route == null)
                                    continue;
                                string routeName = context.route.AsNoTracking().Where(x => x.route_id == emp.route_id).FirstOrDefault().route_name;
                                EDMX.delivery xDelivery = context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.employee_id == emp.employee_id && x.route_id == emp.route_id).FirstOrDefault();
                                if (xDelivery == null)
                                {
                                    delivery _delivery = new delivery
                                    {
                                        delivery_date = date,
                                        employee_id = emp.employee_id,
                                        route_id = emp.route_id,
                                        delivery_route = routeName,
                                        customer_count = 0,
                                        gross_amount = 0,
                                        total_discount = 0,
                                        total_beforevat = 0,
                                        total_vat = 0,
                                        net_amount = 0,
                                        remarks = string.Empty,
                                        status = 1
                                    };
                                    context.Entry(_delivery).State = EntityState.Added;
                                    context.SaveChanges();
                                    deliveryIds.Add(_delivery.delivery_id);
                                }
                                else
                                {
                                    deliveryIds.Add(xDelivery.delivery_id);
                                }
                            }

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
            return deliveryIds;
        }
        public int SaveDelivery(EDMX.delivery delivery, List<EDMX.delivery_items> deliveryItems, List<EDMX.delivery_item_summary> delivery_item_summary, bool afterDeliveryUpdate = false)
        {
            int _deliveryId = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery.route_id = delivery.route_id == 0 ? null : delivery.route_id;
                    if (delivery.delivery_id == 0)
                    {
                        delivery.delivery_items = deliveryItems;
                        delivery.delivery_item_summary = delivery_item_summary;
                        context.Entry(delivery).State = EntityState.Added;
                        context.SaveChanges();
                        _deliveryId = delivery.delivery_id;
                    }
                    else
                    {

                        _deliveryId = delivery.delivery_id;
                        var xDelivery = context.delivery.AsNoTracking().Where(x => x.delivery_id == _deliveryId).FirstOrDefault();
                        delivery.delivery_date = xDelivery.delivery_date;
                        var _days = (xDelivery.delivery_date - DateTime.Now).Days;

                        int allowedDate = context.system_settings.FirstOrDefault(x => x.status == 1).allowed_backdate * -1;
                        if (_days < allowedDate)
                        {
                            PrivilegeDAL privileges = new PrivilegeDAL();
                            if (!privileges.IsPriviligeProvided(Model.Constants.UserId, PrivilegeDAL.Privileges.AllowBackDate, context))
                                throw new Exception("Backdate entry update is not allowed");
                        }

                        context.Entry(delivery).State = EntityState.Modified;
                        context.SaveChanges();

                        if (!afterDeliveryUpdate)
                        {
                            // Remove all the delevery items and save new
                            List<EDMX.delivery_items> _deliveryItems = context.delivery_items.Where(p => p.delivery_id == _deliveryId && p.delivery_time == null).ToList();
                            _deliveryItems.AddRange(context.delivery_items.Where(p => p.delivery_id == _deliveryId && p.delivery_time == null && p.qty == 0).ToList());

                            if (_deliveryItems != null && _deliveryItems.Count > 0)
                            {
                                context.delivery_items.RemoveRange(_deliveryItems);
                                context.SaveChanges();
                            }
                            foreach (delivery_items dl in deliveryItems)
                            {
                                if (dl.qty == 0 && dl.delivery_item_id > 0)
                                {
                                    dl.status = 2;
                                    dl.delivery_time = null;
                                    dl.delivered_qty = 0;
                                    context.Entry(dl).State = EntityState.Modified;
                                }

                            }

                            deliveryItems.RemoveAll((x => x.qty == 0 && x.delivery_item_id > 0));
                            if (deliveryItems.Count > 0)
                            {
                                context.delivery_items.AddRange(deliveryItems);

                            }
                            context.SaveChanges();
                        }
                        else
                        {

                            foreach (delivery_items dl in deliveryItems)
                            {
                                if (dl.delivery_item_id > 0)
                                {
                                    var xDelc = context.delivery_items.Where(x => x.delivery_item_id == dl.delivery_item_id).FirstOrDefault();
                                    if (xDelc != null)
                                    {
                                        decimal oldQty = xDelc.qty;
                                        if (xDelc.qty != dl.qty || xDelc.rate != dl.rate || xDelc.sales_id != dl.sales_id)
                                        {
                                            xDelc.qty = dl.qty;
                                            xDelc.delivered_qty = dl.qty;
                                            xDelc.gross_amount = dl.gross_amount;
                                            xDelc.discount = dl.discount;
                                            xDelc.total_beforvat = dl.total_beforvat;
                                            xDelc.vat_amount = dl.vat_amount;
                                            xDelc.net_amount = dl.net_amount;
                                            xDelc.rate = dl.rate;

                                            var saleItem = context.sales_item.AsNoTracking().FirstOrDefault(x => x.delivery_item_id == dl.delivery_item_id);
                                            if (saleItem != null)
                                            {
                                                dl.sales_id = saleItem.sales_id;
                                                //Updating Sales
                                                SaleDAL saleDAL = new SaleDAL();
                                                saleDAL.UpdateSaleDelivery(dl, context);
                                            }
                                            if (dl.sales_id != null)
                                            {

                                                if (context.sales.Any(x => x.sales_id == dl.sales_id))
                                                    xDelc.sales_id = dl.sales_id;
                                            }
                                            context.Entry(xDelc).State = EntityState.Modified;

                                            //ItemSummary
                                            delivery_item_summary dls = context.delivery_item_summary.FirstOrDefault(x => x.delivery_id == dl.delivery_id && x.item_id == dl.item_id);
                                            if (dls != null)
                                            {
                                                dls.used_qty = (dls.used_qty - oldQty) + dl.qty;
                                                if (dls.qty == dls.used_qty)
                                                    dls.qty = dl.qty;
                                                dls.balance_qty = dls.qty - dls.used_qty;
                                                context.Entry(dls).State = EntityState.Modified;

                                            }
                                            else
                                            {
                                                dl.delivery = context.delivery.Single(x => x.delivery_id == dl.delivery_id);
                                                decimal prvBalance = GetPreviousDayBalance(dl.delivery.delivery_date, dl.delivery.employee_id, dl.item_id);
                                                dls = new EDMX.delivery_item_summary
                                                {
                                                    delivery_id = dl.delivery_id,
                                                    item_id = dl.item_id,
                                                    used_qty = dl.qty,
                                                    qty = prvBalance,
                                                    balance_qty = prvBalance - dl.qty,
                                                    status = 1
                                                };
                                                context.delivery_item_summary.Add(dls);
                                            }
                                            context.SaveChanges();
                                        }
                                    }

                                }
                                else
                                {
                                    dl.delivered_qty = dl.qty;
                                    dl.delivery_time = delivery.delivery_date.AddHours(6);
                                    dl.status = 4;

                                    int oldleaf = 0;
                                    if (dl.daily_collection != null)
                                        oldleaf = dl.daily_collection.old_leaf_count;
                                    dl.daily_collection = null;
                                    context.Entry(dl).State = EntityState.Added;
                                    context.SaveChanges();

                                    SaleDAL saleDAL = new SaleDAL();
                                    saleDAL.SaveSaleDelivery(delivery, dl, oldleaf, context);
                                }
                            }
                        }

                        //Deleting zero qty items
                        List<EDMX.delivery_items> _deliveryItemsZero = context.delivery_items.Where(p => p.delivery_id == _deliveryId && p.qty <= 0).ToList();
                        if (_deliveryItemsZero != null && _deliveryItemsZero.Count > 0)
                        {
                            context.delivery_items.RemoveRange(_deliveryItemsZero);
                            context.SaveChanges();
                        }


                        //Comparing new data with new saved date and updating qty and adding new items
                        foreach (EDMX.delivery_item_summary summ in delivery_item_summary)
                        {
                            EDMX.delivery_item_summary xDelItem = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId && p.item_id == summ.item_id).FirstOrDefault();
                            if (xDelItem != null)
                            {
                                // summ.qty = xDelItem.qty;
                                //summ.balance_qty = xDelItem.balance_qty;
                                summ.used_qty = GetSoldQuantity(_deliveryId, summ.item_id);
                                summ.balance_qty = summ.qty = summ.used_qty;

                                context.Entry(xDelItem).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            ////Removed 08.Jun.2022
                            //else
                            //{

                            //    context.Entry(summ).State = EntityState.Added;
                            //    context.SaveChanges();
                            //}

                        }

                        /*Removed 08.Jun.2022
                        //Comparing Saved data with new data and removing excluded items 
                        List<EDMX.delivery_item_summary> _deliveryItemSummary = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId).ToList();
                        foreach (EDMX.delivery_item_summary xItem in _deliveryItemSummary)
                        {
                            EDMX.delivery_item_summary nItem = delivery_item_summary.Where(x => x.item_id == xItem.item_id).FirstOrDefault();
                            if (nItem == null)
                            {
                                context.delivery_item_summary.Remove(xItem);
                                context.SaveChanges();
                            }
                        }
                        */

                        // List<EDMX.delivery_item_summary> _deliveryItemSummary = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId).ToList();
                        // if (_deliveryItemSummary != null && _deliveryItemSummary.Count > 0)
                        // {
                        //     context.delivery_item_summary.RemoveRange(_deliveryItemSummary);
                        //     context.SaveChanges();
                        // }
                        // context.delivery_item_summary.AddRange(delivery_item_summary);
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return _deliveryId;
        }


        public int SaveDeliveryItemSummary(List<EDMX.delivery_item_summary> delivery_item_summary, betaskdbEntities context, int loadingId)
        {
            int _deliverySummaryId = 0, _deliveryId = delivery_item_summary[0].delivery_id;
            delivery xDelivery = context.delivery.AsNoTracking().FirstOrDefault(x => x.delivery_id == _deliveryId);
            try
            {
                {

                    foreach (EDMX.delivery_item_summary summ in delivery_item_summary)
                    {
                        EDMX.delivery_item_summary xDelItem = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId && p.item_id == summ.item_id && p.status == 1).FirstOrDefault();
                        if (xDelItem != null)
                        {

                            if (context.loading.Count(x => x.delivery_id == _deliveryId && x.item_id == summ.item_id && x.status == 1) > 1)
                            {
                                if (xDelItem.qty <= 0)
                                {
                                    decimal prvsBalace = GetPreviousDayBalance(xDelivery.delivery_date, xDelivery.employee_id, summ.item_id);
                                    prvsBalace = prvsBalace < 0 ? 0 : prvsBalace;
                                    xDelItem.qty = prvsBalace + summ.qty;
                                }
                                else
                                    xDelItem.qty += summ.qty;

                                xDelItem.damage_qty += summ.damage_qty;
                                xDelItem.used_qty = summ.used_qty;
                                xDelItem.balance_qty = (xDelItem.qty) - (xDelItem.used_qty);
                                context.Entry(xDelItem).Property(x => x.used_qty).IsModified = true;
                                context.Entry(xDelItem).Property(x => x.qty).IsModified = true;
                                context.Entry(xDelItem).Property(x => x.damage_qty).IsModified = true;
                                context.Entry(xDelItem).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                decimal prvsBalace = GetPreviousDayBalance(xDelivery.delivery_date, xDelivery.employee_id, summ.item_id, loadingId, context);
                                prvsBalace = prvsBalace < 0 ? 0 : prvsBalace;
                                xDelItem.qty = prvsBalace + summ.qty;
                                xDelItem.damage_qty = summ.damage_qty;
                                xDelItem.balance_qty = summ.qty - (xDelItem.used_qty);
                                context.Entry(xDelItem).Property(x => x.used_qty).IsModified = true;
                                context.Entry(xDelItem).Property(x => x.qty).IsModified = true;
                                context.Entry(xDelItem).Property(x => x.damage_qty).IsModified = true;
                                context.Entry(xDelItem).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                        else
                        {

                            context.Entry(summ).State = EntityState.Added;
                            context.SaveChanges();
                        }
                        _deliverySummaryId = xDelItem == null ? summ.delivery_summary_id : xDelItem.delivery_summary_id;
                    }

                    //Comparing Saved data with new data and removing excluded items
                    /*
                    List<EDMX.delivery_item_summary> _deliveryItemSummary = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId).ToList();
                    foreach (EDMX.delivery_item_summary xItem in _deliveryItemSummary)
                    {
                        EDMX.delivery_item_summary nItem = delivery_item_summary.Where(x => x.item_id == xItem.item_id).FirstOrDefault();
                        if (nItem == null)
                        {
                            context.delivery_item_summary.Remove(xItem);
                            context.SaveChanges();
                        }
                    }*/
                }
            }
            catch (Exception ee)
            {

                throw;
            }
            return _deliverySummaryId;
        }

        public int SaveDeliveryItemSummaryFromDelivery(List<EDMX.delivery_item_summary> delivery_item_summary, int deliveryId)
        {
            int _deliverySummaryId = 0, _deliveryId = delivery_item_summary[0].delivery_id;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    //Comparing new data with new saved date and updating qty and adding new items
                    foreach (EDMX.delivery_item_summary summ in delivery_item_summary)
                    {
                        EDMX.delivery_item_summary xDelItem = context.delivery_item_summary.Where(p => p.delivery_id == _deliveryId && p.item_id == summ.item_id).FirstOrDefault();
                        if (xDelItem != null)
                        {
                            xDelItem.qty = summ.qty;
                            xDelItem.used_qty = summ.used_qty;
                            xDelItem.balance_qty = summ.balance_qty;
                            xDelItem.remarks = summ.remarks;
                            xDelItem.damage_qty = summ.damage_qty;
                            context.Entry(xDelItem).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.Entry(summ).State = EntityState.Added;
                            context.SaveChanges();
                        }
                        _deliverySummaryId = xDelItem == null ? summ.delivery_summary_id : xDelItem.delivery_summary_id;
                    }
                }
            }
            catch (Exception ee)
            {

                throw;
            }
            return _deliverySummaryId;
        }


        public List<EDMX.delivery_items> SyncRecheck(int deliveryId)
        {
            List<EDMX.delivery_items> listItems = new List<delivery_items>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listItems = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == deliveryId && x.delivery_time == null).ToList();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return listItems;
        }
        public void UpdateDeliveredItems(List<EDMX.delivery_items> listDeliveryItems, List<EDMX.daily_collection> listDailyCollection = null)
        {
            string err = "";
            try
            {
                BETask.DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new BETask.DAL.DAL.LedgerMappingDAL();
                BETask.DAL.DAL.AccountLedgerDAL accountLedgerDAL = new BETask.DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(BETask.DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                int bankId = accountLedgerDAL.GetAllAccountLedger(groupId)[0].ledger_id;
                CouponDAL _coupon = new CouponDAL();
                using (var context = new betaskdbEntities())
                {

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var sysDefault = context.system_settings.Single();
                            foreach (delivery_items items in listDeliveryItems)
                            {

                                delivery_items _Items = context.delivery_items.Where(x => x.delivery_item_id == items.delivery_item_id).FirstOrDefault();
                                decimal existQty = Convert.ToDecimal(_Items.delivered_qty);
                                existQty = 0; 
                                _Items.status = 4; 
                                _Items.division_id = items.division_id;
                                context.Entry(_Items).State = EntityState.Modified;
                                context.SaveChanges();

                                //Update CustomerAsset
                                if (sysDefault.default_item_id != _Items.item_id)
                                    CustomerAsseUpdate(_Items, context);


                                //Upating delivery leaf
                                if (items.delivery_leaf != null)
                                    _coupon.RedeemDeliveryLeaf(_Items, context);


                                //delivery_item_summary
                                delivery_item_summary _Item_Summary = context.delivery_item_summary.Where(x => x.delivery_id == _Items.delivery_id && x.item_id == _Items.item_id).FirstOrDefault();
                                if (_Item_Summary != null)
                                {
                                    _Item_Summary.used_qty = _Item_Summary.used_qty > 0 ? (_Item_Summary.used_qty - existQty) + items.delivered_qty : items.delivered_qty;
                                    _Item_Summary.balance_qty = _Item_Summary.balance_qty > 0 ? (_Item_Summary.balance_qty - _Item_Summary.used_qty) : 0;
                                    //  _Item_Summary.qty = _Item_Summary.qty + items.delivered_qty;
                                    context.Entry(_Item_Summary).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                //New from mobile app created
                                else
                                {
                                    _Item_Summary = new delivery_item_summary
                                    {
                                        item_id = _Items.item_id,
                                        delivery_id = _Items.delivery_id,
                                        // qty = _Items.delivered_qty,
                                        used_qty = _Items.delivered_qty,
                                        balance_qty = 0,
                                        status = 1,
                                    };
                                    context.Entry(_Item_Summary).State = EntityState.Added;
                                    context.SaveChanges();
                                }
                            }

                            //Daily Collection
                            err = UpdateDeliveryItemsCollectionAndSale(listDailyCollection, err, ledgerMappingDAL, bankId, context);

                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            string ss = err;
                            transaction.Rollback();
                            throw new Exception($"{ee.ToString()} - {err}");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        public void UpdateDeliveredItemsBottleDeposit(List<EDMX.delivery_items> listDeliveryItems, List<EDMX.daily_collection> listDailyCollection = null)
        {
            string err = "";
            try
            {
                BETask.DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new BETask.DAL.DAL.LedgerMappingDAL();
                BETask.DAL.DAL.AccountLedgerDAL accountLedgerDAL = new BETask.DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(BETask.DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                int bankId = accountLedgerDAL.GetAllAccountLedger(groupId)[0].ledger_id;
                CouponDAL _coupon = new CouponDAL();
                using (var context = new betaskdbEntities())
                {

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var sysDefault = context.system_settings.Single();
                            foreach (delivery_items items in listDeliveryItems)
                            {

                                delivery_items _Items = context.delivery_items.Where(x => x.delivery_item_id == items.delivery_item_id).FirstOrDefault();
                                decimal existQty = Convert.ToDecimal(_Items.delivered_qty);
                                existQty = 0;
                               
                                _Items.status = 4; 
                                _Items.division_id = items.division_id;
                                context.Entry(_Items).State = EntityState.Modified;
                                context.SaveChanges();

                                //Update CustomerAsset
                                if (sysDefault.default_item_id != _Items.item_id)
                                    CustomerAsseUpdate(_Items, context);



                                delivery_item_summary _Item_Summary = new delivery_item_summary
                                {
                                    item_id = _Items.item_id,
                                    delivery_id = _Items.delivery_id,
                                    // qty = _Items.delivered_qty,
                                    used_qty = _Items.delivered_qty,
                                    balance_qty = 0,
                                    status = 1,
                                };
                                context.Entry(_Item_Summary).State = EntityState.Added;
                                context.SaveChanges();
                            }


                            //Daily Collection
                            err = UpdateDeliveryItemsCollectionBottleDeposit(listDailyCollection, err, ledgerMappingDAL, bankId, context);

                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            string ss = err;
                            transaction.Rollback();
                            throw new Exception($"{ee.ToString()} - {err}");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private string UpdateDeliveryItemsCollectionAndSale(List<daily_collection> listDailyCollection, string err, LedgerMappingDAL ledgerMappingDAL, int bankId, betaskdbEntities context)
        {
            var routes = context.route.AsNoTracking().ToList();
            if (listDailyCollection != null && listDailyCollection.Count > 0)
            {
                foreach (EDMX.daily_collection coll in listDailyCollection)
                {
                    err = $"customer:{coll.customer_id} , collection id :{coll.daily_collection_id}";

                    System.Threading.Thread.Sleep(100);
                    //for checking daily collection already added or not
                    daily_collection xColl = context.daily_collection.Where(x => x.customer_id == coll.customer_id && x.delivery_time == coll.delivery_time && x.payment_mode == coll.payment_mode && x.net_amount == coll.net_amount && x.collected_amount == coll.collected_amount).FirstOrDefault();
                    if (xColl == null)
                    {
                        employee emp = context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == coll.employee_id);
                        coll.route_id = emp.route_id;
                        context.Entry(coll).State = EntityState.Added;
                        context.SaveChanges();


                        if (coll.remarks != null && coll.remarks.Contains("cash_balance_to_credit"))
                            continue;
                        if (coll.is_deposit == 1)
                        {
                            Interface.APosting.OutstandingCollectionPost(coll, context);
                            UpdateCollectionIdInDeliveryItems(context, coll);
                            continue;
                        }

                        if (coll.route_id != null && coll.route_id > 0)
                        {
                            string routeName = routes.FirstOrDefault(x => x.route_id == coll.route_id).route_name;
                            coll.remarks = $"{routeName} - {coll.remarks}";
                            coll.remarks = coll.remarks.Substring(0, Math.Min(150, coll.remarks.Length));

                        }

                        //Sales
                        string paymentMode = coll.payment_mode.ToLower();

                        CustomerDAL customer = new CustomerDAL();

                        var customerLedgerId = customer.GetCustomerDetails(coll.customer_id, context).ledger_id;
                        int _customerLedgerId = 0;
                        if (customerLedgerId != null)
                            _customerLedgerId = Convert.ToInt32(customerLedgerId);

                        //Is Deposit posting to BottleDeposit ledger instead of customer ledger
                        if (coll.is_deposit == 1)
                        {
                            //Deposit ledger credit here
                            ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.DEPOSITCOLLECTION);
                            if (ledgerDeposit != null)
                                _customerLedgerId = Convert.ToInt32(ledgerDeposit.ledger_id);
                        }

                        else
                        {
                            //Old leaf cound different posting
                            if (coll.payment_mode.ToLower() == "coupon" && coll.old_leaf_count > 0)
                            {

                                if (coll.old_leaf_count > 0)
                                {
                                    //Deposit ledger credit here
                                    ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.COUPONBOOKLIABILITY);
                                    if (ledgerDeposit != null)
                                        _customerLedgerId = Convert.ToInt32(ledgerDeposit.ledger_id);

                                }
                            }

                        }

                        List<delivery_items> listCustomerDeliveryItems = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == coll.delivery_id && x.customer_id == coll.customer_id && x.is_deposit == 2 && x.status != 2).ToList();

                        /*Some time same customer has one or more deliveries with same delivery id & different paymentCollection, 
                        that time wee need to check the time of dailycollection & deivery item of the customer or else for one collection delivery items would be return more items
                        and it will not be synced
                         */
                        if (listCustomerDeliveryItems.Count > 0)
                        {
                            listCustomerDeliveryItems = listCustomerDeliveryItems.Where(x => x.delivery_time == coll.delivery_time).ToList();
                            // listCustomerDeliveryItems = listCustomerDeliveryItems.Where(x => x.daily_collection_id == coll.daily_collection_id).ToList();
                        }
                        if (listCustomerDeliveryItems.Count == 0)
                            continue;


                        /*If posting amount not tally*/
                        bool isDifference = false;
                        decimal postGrossAmount = listCustomerDeliveryItems.Sum(x => x.gross_amount);
                        decimal postVatInSaleAmount = listCustomerDeliveryItems.Where(x => x.rate > 0).Sum(x => x.vat_amount);
                        decimal postNetAmount = coll.collected_amount;
                        if ((postGrossAmount + postVatInSaleAmount) != postNetAmount)
                        {
                            //blocked on 2023-09-26 due to vat minus
                            //decimal difference = postNetAmount - (postGrossAmount + postVatInSaleAmount);
                            //decimal _newVatAmount = postVatInSaleAmount + difference;
                            //postVatInSaleAmount = _newVatAmount;
                            postNetAmount = (postGrossAmount + postVatInSaleAmount);
                            isDifference = true;


                        }
                        /**************************/

                        BETask.DAL.Model.SaleAccountPostModel saleAccount = new BETask.DAL.Model.SaleAccountPostModel
                        {
                            //SalesAmount = listDailyCollection.Sum(x => x.net_amount),
                            SalesAmount = listCustomerDeliveryItems.Sum(x => x.gross_amount),
                            DiscountAllowedAmount = 0,
                            RoundOffAmount = 0,
                            VatOnSaleAmount = postVatInSaleAmount,
                            CreditPSaleAmount = !isDifference ? coll.collected_amount : postNetAmount,
                            CreditSaleLedger = _customerLedgerId,
                            CashSaleAmount = paymentMode == "cash" ? (!isDifference ? coll.net_amount : postNetAmount) : 0,
                            BankSaleAmount = paymentMode == "bank" ? (!isDifference ? coll.net_amount : postNetAmount) : 0,
                            BankSaleLedger = Convert.ToInt32(paymentMode == "bank" ? bankId : 0),

                        };


                        EDMX.sales sales = new sales
                        {
                            sales_id = 0,
                            sales_date = coll.delivery_time,
                            payment_mode = coll.payment_mode,
                            customer_id = coll.customer_id,
                            net_amount = !isDifference ? coll.collected_amount : postNetAmount,
                            balance_amount = (coll.net_amount - coll.collected_amount),
                            gross_amount = listCustomerDeliveryItems.Sum(x => x.gross_amount),
                            remarks = $"Auto synched - {coll.remarks}",
                            roundup = 0,
                            sales_order = listDailyCollection[0].delivery_id,
                            cash_paid = paymentMode == "cash" ? listDailyCollection.Sum(x => x.collected_amount) : 0,
                            total_beforevat = listCustomerDeliveryItems.Sum(x => x.total_beforvat),
                            total_vat = postVatInSaleAmount,
                            total_discount = 0,
                            status = 1,
                            bank_id = paymentMode == "cash" ? 0 : bankId,
                            cheque_no = string.Empty,
                            sales_number = "",
                            collection_id = coll.daily_collection_id,
                            delivery_leaf = coll.delivery_leaf,
                            division_id = coll.division_id,
                            old_leaf_count = coll.old_leaf_count,
                            route_id = emp.route_id

                        };

                        isDifference = false;

                        List<EDMX.sales_item> listSaleItem = new List<sales_item>();

                        foreach (delivery_items items in listCustomerDeliveryItems)
                        {
                            // delivery_items items = context.delivery_items.AsNoTracking().Where(x => x.delivery_item_id == item.delivery_item_id).FirstOrDefault();

                            listSaleItem.Add(new sales_item
                            {
                                item_id = items.item_id,
                                discount = items.discount,
                                gross_amount = items.gross_amount,
                                net_amount = items.net_amount,
                                qty = items.delivered_qty,
                                rate = items.rate,
                                total_beforvat = items.total_beforvat,
                                vat_amount = items.total_beforvat > 0 ? items.vat_amount : 0,
                                status = 1,
                                delivery_item_id = items.delivery_item_id


                            });
                        }
                        if (coll.is_deposit == 2)
                        {
                            SaleDAL saleDAL = new SaleDAL();
                            saleDAL.SaveSaleAuto(sales, listSaleItem, saleAccount, context);
                        }
                    }

                    //Updating daily_collection_id in delivery items 
                    UpdateCollectionIdInDeliveryItems(context, coll);
                    if (coll.payment_mode.ToLower() == "do")
                        UpdateDoLeaf(context, coll);
                }
            }

            return err;
        }

        private string UpdateDeliveryItemsCollectionBottleDeposit(List<daily_collection> listDailyCollection, string err, LedgerMappingDAL ledgerMappingDAL, int bankId, betaskdbEntities context)
        {
            if (listDailyCollection != null && listDailyCollection.Count > 0)
            {
                foreach (EDMX.daily_collection coll in listDailyCollection)
                {
                    err = $"customer:{coll.customer_id} , collection id :{coll.daily_collection_id}";

                    System.Threading.Thread.Sleep(100);
                    //for checking daily collection already added or not
                    daily_collection xColl = context.daily_collection.Where(x => x.customer_id == coll.customer_id && x.delivery_time == coll.delivery_time && x.payment_mode == coll.payment_mode && x.net_amount == coll.net_amount && x.collected_amount == coll.collected_amount).FirstOrDefault();
                    if (xColl == null)
                    {
                        employee emp = context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == coll.employee_id);
                        coll.route_id = emp.route_id;
                        context.Entry(coll).State = EntityState.Added;
                        context.SaveChanges();


                        if (coll.remarks != null && coll.remarks.Contains("cash_balance_to_credit"))
                            continue;
                        if (coll.is_deposit == 1)
                        {
                            string remarks = "";
                            try
                            {

                                if (coll.route_id != null && coll.route_id > 0)
                                {
                                    var route = context.route.AsNoTracking().FirstOrDefault(x => x.route_id == coll.route_id);
                                    remarks = route.route_name;

                                }
                                var employee = context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == coll.employee_id);

                                var customer = context.customer.AsNoTracking().FirstOrDefault(x =>x.customer_id == coll.customer_id);
                                 remarks +=  $" Bottle deposit by {employee.first_name} {employee.last_name} for customer {customer.customer_name}  , app remarks : {coll.remarks}";
                            }
                            catch { }
                            remarks = remarks.Substring(0, Math.Min(150, remarks.Length));
                            coll.remarks = remarks;
                            Interface.APosting.OutstandingCollectionPost(coll, context);
                            UpdateCollectionIdInDeliveryItems(context, coll);
                            continue;
                        }

                    }

                    //Updating daily_collection_id in delivery items 
                    UpdateCollectionIdInDeliveryItems(context, coll);
                }
            }

            return err;
        }

        private void UpdateCollectionIdInDeliveryItems(betaskdbEntities context, daily_collection coll)
        {
            if (coll.delivery_id != null)
            {
                int deliveryId = Convert.ToInt32(coll.delivery_id);
                List<delivery_items> listItems = context.delivery_items.Where(x => x.delivery_id == deliveryId && x.daily_collection_id == coll.daily_collection_id).ToList();
                if (listItems != null && listItems.Count > 0)
                {
                    foreach (delivery_items dl in listItems)
                    {
                        dl.daily_collection_id = coll.daily_collection_id;
                        context.delivery_items.Attach(dl);
                        context.Entry(dl).Property(x => x.daily_collection_id).IsModified = true;
                        context.SaveChanges();
                    }
                }
            }
        }

        private void UpdateDoLeaf(betaskdbEntities context, daily_collection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(collection.delivery_leaf))
                {
                    delivery_book leaf = context.delivery_book.FirstOrDefault(x => x.employee_id == collection.employee_id && x.leaf_no == collection.delivery_leaf);
                    if (leaf != null)
                    {
                        leaf.status = 4;
                        leaf.redeemed_date = collection.delivery_time;
                        leaf.customer_id = collection.customer_id;
                        leaf.delivery_id = collection.delivery_id;
                        context.Entry(leaf).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void CustomerAsseUpdate(delivery_items dl, betaskdbEntities context)
        {
            try
            {

                CustomerAssetDAL customerAssetDAL = new CustomerAssetDAL(context);
                customerAssetDAL.UpdateAsseFromDelivery(dl);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Only if sale not updated
        /// </summary>
        /// <param name="listDeliveryItems"></param>
        /// <param name="listDailyCollection"></param>
        public void UpdateDeliveredItemsRecheck(List<EDMX.delivery_items> listDeliveryItems, List<EDMX.daily_collection> listDailyCollection = null)
        {
            string err = "";
            try
            {
                BETask.DAL.DAL.LedgerMappingDAL ledgerMappingDAL = new BETask.DAL.DAL.LedgerMappingDAL();
                BETask.DAL.DAL.AccountLedgerDAL accountLedgerDAL = new BETask.DAL.DAL.AccountLedgerDAL();
                int groupId = Convert.ToInt32(ledgerMappingDAL.GetLegerMapping(BETask.DAL.DAL.LedgerMappingDAL.EnumLedgerMapGroupTypes.BANKACCOUNTS).group_id);
                int bankId = accountLedgerDAL.GetAllAccountLedger(groupId)[0].ledger_id;
                using (var context = new betaskdbEntities())
                {

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {




                            //Daily Collection
                            if (listDailyCollection != null && listDailyCollection.Count > 0)
                            {
                                foreach (EDMX.daily_collection coll in listDailyCollection)
                                {
                                    err = $"customer:{coll.customer_id} , collection id :{coll.daily_collection_id}";


                                    System.Threading.Thread.Sleep(100);
                                    //for checking daily collection already added or not
                                    daily_collection xColl = context.daily_collection.Where(x => x.customer_id == coll.customer_id && x.delivery_time == coll.delivery_time && x.payment_mode == coll.payment_mode && x.net_amount == coll.net_amount && x.collected_amount == coll.collected_amount).FirstOrDefault();
                                    if (xColl == null)
                                        xColl = context.daily_collection.Where(x => x.daily_collection_id == coll.daily_collection_id).FirstOrDefault();
                                    sales xSale = null;
                                    employee emp = context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == coll.employee_id);
                                    coll.route_id = emp.route_id;
                                    if (xColl != null)
                                    {
                                        coll.daily_collection_id = xColl.daily_collection_id;
                                        xSale = context.sales.AsNoTracking().Where(x => x.collection_id == xColl.daily_collection_id && x.customer_id == xColl.customer_id).FirstOrDefault();
                                    }
                                    if (xColl == null || (xColl != null && xSale == null))
                                    {
                                        if (xColl == null)
                                        {
                                            context.Entry(coll).State = EntityState.Added;
                                            context.SaveChanges();
                                        }


                                        if (coll.remarks != null && coll.remarks.Contains("cash_balance_to_credit"))
                                            continue;


                                        //Sales
                                        string paymentMode = coll.payment_mode.ToLower();

                                        CustomerDAL customer = new CustomerDAL();

                                        var customerLedgerId = customer.GetCustomerDetails(coll.customer_id, context).ledger_id;
                                        int _customerLedgerId = 0;
                                        if (customerLedgerId != null)
                                            _customerLedgerId = Convert.ToInt32(customerLedgerId);

                                        //Old leaf cound different posting
                                        if (coll.payment_mode.ToLower() == "coupon" && coll.old_leaf_count > 0)
                                        {

                                            if (coll.is_deposit == 1)
                                            {
                                                //Deposit ledger credit here
                                                ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.COUPONBOOKLIABILITY);
                                                if (ledgerDeposit != null)
                                                    _customerLedgerId = Convert.ToInt32(ledgerDeposit.ledger_id);

                                            }
                                            _customerLedgerId = 0;
                                        }

                                        List<delivery_items> listCustomerDeliveryItems = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == coll.delivery_id && x.customer_id == coll.customer_id).ToList();

                                        /*Some time same customer has one or more deliveries with same delivery id & different paymentCollection, 
                                        that time wee need to check the time of dailycollection & deivery item of the customer or else for one collection delivery items would be return more items
                                        and it will not be synced
                                         */

                                        if (listCustomerDeliveryItems.Count == 0)
                                            continue;


                                        /*If posting amount not tally*/
                                        decimal postGrossAmount = listCustomerDeliveryItems.Sum(x => x.gross_amount);
                                        decimal postVatInSaleAmount = listCustomerDeliveryItems.Where(x => x.rate > 0).Sum(x => x.vat_amount);
                                        decimal postNetAmount = coll.collected_amount;
                                        if ((postGrossAmount + postVatInSaleAmount) != postNetAmount)
                                        {
                                            decimal difference = postNetAmount - (postGrossAmount + postVatInSaleAmount);
                                            decimal _newVatAmount = postVatInSaleAmount + difference;
                                            postVatInSaleAmount = _newVatAmount;
                                        }
                                        /**************************/

                                        BETask.DAL.Model.SaleAccountPostModel saleAccount = new BETask.DAL.Model.SaleAccountPostModel
                                        {
                                            //SalesAmount = listDailyCollection.Sum(x => x.net_amount),
                                            SalesAmount = listCustomerDeliveryItems.Sum(x => x.gross_amount),
                                            DiscountAllowedAmount = 0,
                                            RoundOffAmount = 0,
                                            VatOnSaleAmount = postVatInSaleAmount,
                                            CreditPSaleAmount = coll.collected_amount,
                                            CreditSaleLedger = _customerLedgerId,
                                            CashSaleAmount = paymentMode == "cash" ? coll.net_amount : 0,
                                            BankSaleAmount = paymentMode == "bank" ? coll.net_amount : 0,
                                            BankSaleLedger = Convert.ToInt32(paymentMode == "bank" ? bankId : 0),

                                        };

                                        //removed on 07/12/2022 because net amount is not considering
                                        /*
                                        if ((coll.net_amount - coll.collected_amount) > 0)
                                        {
                                            saleAccount.CashBalance = (coll.net_amount - coll.collected_amount);
                                        }
                                        */

                                        //int _delId = Convert.ToInt32(listDailyCollection[0].delivery_id);
                                        //int _custId = Convert.ToInt32(listDailyCollection[0].customer_id);
                                        //var xCustDelivery = context.delivery_items.Where(x => x.delivery_id == _delId && x.customer_id == _custId).ToList();

                                        EDMX.sales sales = new sales
                                        {
                                            sales_id = 0,
                                            sales_date = coll.delivery_time,
                                            payment_mode = coll.payment_mode,
                                            customer_id = coll.customer_id,
                                            net_amount = coll.collected_amount,
                                            balance_amount = (coll.net_amount - coll.collected_amount),
                                            gross_amount = listCustomerDeliveryItems.Sum(x => x.gross_amount),
                                            remarks = $"Auto synched - {coll.remarks}",
                                            roundup = 0,
                                            sales_order = listDailyCollection[0].delivery_id,
                                            cash_paid = paymentMode == "cash" ? listDailyCollection.Sum(x => x.collected_amount) : 0,
                                            total_beforevat = listCustomerDeliveryItems.Sum(x => x.total_beforvat),
                                            total_vat = postVatInSaleAmount,
                                            total_discount = 0,
                                            status = 1,
                                            bank_id = paymentMode == "cash" ? 0 : bankId,
                                            cheque_no = string.Empty,
                                            sales_number = "",
                                            collection_id = coll.daily_collection_id,
                                            division_id = coll.division_id,
                                            old_leaf_count = coll.old_leaf_count,
                                            route_id = emp.route_id

                                        };

                                        List<EDMX.sales_item> listSaleItem = new List<sales_item>();

                                        foreach (delivery_items items in listCustomerDeliveryItems)
                                        {
                                            // delivery_items items = context.delivery_items.AsNoTracking().Where(x => x.delivery_item_id == item.delivery_item_id).FirstOrDefault();

                                            listSaleItem.Add(new sales_item
                                            {
                                                item_id = items.item_id,
                                                discount = items.discount,
                                                gross_amount = items.gross_amount,
                                                net_amount = items.net_amount,
                                                qty = items.delivered_qty,
                                                rate = items.rate,
                                                total_beforvat = items.total_beforvat,
                                                vat_amount = items.total_beforvat > 0 ? items.vat_amount : 0,
                                                status = 1,

                                            });
                                        }
                                        SaleDAL saleDAL = new SaleDAL();
                                        saleDAL.SaveSaleAuto(sales, listSaleItem, saleAccount, context);
                                    }

                                    //Updating daily_collection_id in delivery items 
                                    if (coll.delivery_id != null)
                                    {
                                        int deliveryId = Convert.ToInt32(coll.delivery_id);
                                        List<delivery_items> listItems = context.delivery_items.Where(x => x.delivery_id == deliveryId && x.daily_collection_id == coll.daily_collection_id).ToList();
                                        if (listItems != null && listItems.Count > 0)
                                        {
                                            foreach (delivery_items dl in listItems)
                                            {
                                                dl.daily_collection_id = coll.daily_collection_id;
                                                context.delivery_items.Attach(dl);
                                                context.Entry(dl).Property(x => x.daily_collection_id).IsModified = true;
                                                context.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            string ss = err;
                            transaction.Rollback();
                            throw new Exception($"{ee.Message} - {err}");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public List<delivery> SearchDelivery(DateTime dateFrom, DateTime dateTo, int empId)
        {
            List<delivery> listDelivery = new List<delivery>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDelivery = context.delivery.Include(x => x.employee).Where(x => x.delivery_date >= dateFrom && x.delivery_date <= dateTo && x.status == 1).ToList();
                    if (empId > 0)
                    {
                        listDelivery = listDelivery.Where(x => x.employee_id == empId).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listDelivery;
        }
        public int GetDeliveryId(DateTime _date, int _empId)
        {
            int deliveryId = 0;
            delivery objDelivery = new delivery();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    objDelivery = context.delivery.Include(x => x.employee).Where(x => x.employee_id == _empId && x.delivery_date == _date && x.status == 1).FirstOrDefault();

                    if (objDelivery != null && Convert.ToInt32(objDelivery.delivery_id) > 0)
                        deliveryId = objDelivery.delivery_id;

                }
            }
            catch
            {
                throw;
            }
            return deliveryId;
        }

        public decimal GetSoldQuantity(int _deliveryId, int _ItemId)
        {
            decimal soldItem = 0;
            List<delivery_items> listDeliveryItems = new List<delivery_items>();

            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (context.delivery_items.Any(x => x.delivery_id == _deliveryId && x.item_id == _ItemId))
                    {
                        soldItem = context.delivery_items.Where(x => x.delivery_id == _deliveryId && x.item_id == _ItemId && x.status == 4).Select(x => x.delivered_qty).DefaultIfEmpty(0).Sum();
                    }



                    /*
                    listDeliveryItems = context.delivery_items.Where(x => x.delivery_id == _deliveryId && x.item_id == _ItemId).ToList();
                    //var sumDeliverdQty= listDeliveryItems.Select(g => new { SumQty = g.delivered_qty });
                    foreach (delivery_items DeliveryData in listDeliveryItems)
                    {
                        soldItem += Convert.ToDecimal(DeliveryData.delivered_qty);
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return soldItem;
        }

        public decimal GetPreviousDayBalance(DateTime _date, int _empId, int _itemId, int loadingId = 0, betaskdbEntities _context = null)
        {
            decimal PrvBalance = 0;
            delivery objDelivery = new delivery();
            List<delivery> listDelivery = new List<delivery>();
            List<delivery_item_summary> listDeliveryItemSummary = new List<delivery_item_summary>();
            try
            {
                betaskdbEntities context = null;

                context = _context == null ? new betaskdbEntities() : _context;

                //using (context)
                {
                    if (!context.loading.Any(x => x.employee_id == _empId && x.item_id == _itemId && x.status == 1))
                    {
                        PrvBalance = 0;
                        return 0;
                    }

                    DateTime firstLoading = DateTime.MinValue;
                    if (!context.loading.Any(x => x.employee_id == _empId && x.item_id == _itemId && x.status == 1 && x.load_id != loadingId))
                    {
                        PrvBalance = 0;
                    }
                    else
                    {
                        DateTime date = new DateTime(_date.Year, _date.Month, _date.Day, 23, 59, 59);
                        var loading = context.loading.AsNoTracking().Where(x => x.employee_id == _empId && x.load_date <= date && x.item_id == _itemId).FirstOrDefault();
                        firstLoading = new DateTime(loading.load_date.Year, loading.load_date.Month, loading.load_date.Day);
                        //  objDelivery = context.delivery.Include(x => x.delivery_item_summary).Where(p => p.employee_id == _empId && p.delivery_date < _date ).OrderByDescending(i => i.delivery_date).FirstOrDefault();
                        var _objDelivery = context.delivery_item_summary.Include(x => x.delivery).Where(p => p.delivery.employee_id == _empId && (p.delivery.delivery_date < _date && p.delivery.delivery_date >= firstLoading) && p.item_id == _itemId && p.qty > 0).OrderByDescending(i => i.delivery.delivery_date).FirstOrDefault();
                        if (_objDelivery != null)
                            objDelivery = _objDelivery.delivery;
                        if (objDelivery != null && objDelivery.delivery_item_summary != null)
                        {
                            foreach (delivery_item_summary data in objDelivery.delivery_item_summary.Where(x => x.item_id == _itemId))
                            {
                                if (data.qty == 0)
                                {
                                    data.qty = context.loading.Where(x => x.employee_id == _empId && x.item_id == _itemId).Select(x => x.new_stock).DefaultIfEmpty(0).First();
                                }
                                PrvBalance += Convert.ToDecimal(data.qty - (data.used_qty + data.damage_qty));
                            }

                            /*
                             * if 20th has 300 loading and 100 balance
                             *  21st has no loading but 50 delivery and balance is 50 
                             *  22nd has loading but 40 delivery . So 22nd prv balance should be 50 and balance will 10
                             *  23rd previous balance will be 0
                             *  Consider only 20th has loading and other days only delivery . ie no qty in delivery_item summary
                             *  Then first take last qty row then find the balance
                             *  Then take all the record of next days and the used qty will be reduced from first balance qty'
                             *  1: balance qty will be 100 so previous balance is 100 
                             *  then 100-50=50 so previous balance is 50
                             *  then 50-10 =40 so previous balance is 40
                             */
                            if (objDelivery.delivery_date != DateTime.MinValue)
                            {
                                List<delivery_item_summary> listNextDeliveries = context.delivery_item_summary.Include(s => s.delivery).Where(x => x.delivery.delivery_date > objDelivery.delivery_date && x.delivery.delivery_date < _date && x.item_id == _itemId && x.used_qty > 0 && x.delivery.employee_id == _empId).ToList();
                                foreach (delivery_item_summary dl in listNextDeliveries)
                                {
                                    if (dl.used_qty > 0)
                                        PrvBalance -= (dl.used_qty + dl.damage_qty);
                                }
                            }
                            //Checking offload
                            int defaultItemId = context.system_settings.Where(x => x.status == 1).FirstOrDefault().default_item_id;
                            if (defaultItemId != _itemId)
                            {
                                loading lastOffload = context.loading.AsNoTracking().Where(x => EntityFunctions.TruncateTime(x.load_date) <= _date && x.offload > 0 && x.status == 1 && x.item_id == _itemId).OrderByDescending(x => x.load_date).FirstOrDefault();
                                if (lastOffload != null)
                                {

                                    decimal offload = lastOffload.offload;
                                    PrvBalance -= offload;

                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PrvBalance;
        }

        public decimal GetPreviousDayBalanceFromDelivery(DateTime _date, int _empId, int _itemId)
        {
            decimal PrvBalance = 0;
            delivery objDelivery = new delivery();
            List<delivery> listDelivery = new List<delivery>();
            List<delivery_item_summary> listDeliveryItemSummary = new List<delivery_item_summary>();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    objDelivery = context.delivery.Include(x => x.delivery_item_summary).Where(p => p.employee_id == _empId && p.delivery_date < _date && p.net_amount > 0).OrderByDescending(i => i.delivery_date).FirstOrDefault();
                    if (objDelivery != null)
                    {
                        foreach (delivery_item_summary data in objDelivery.delivery_item_summary.Where(x => x.item_id == _itemId))
                        {
                            PrvBalance += data.qty - data.used_qty;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PrvBalance;
        }

        public decimal GetPreviousDayBalanceandLoading(DateTime _date, int deliveryId, int _empId, int _itemId, ref decimal totalLoading, ref decimal totalEmpty, ref decimal totalShort, ref decimal totalExtra, ref bool firstLoad)
        {
            decimal PrvBalance = 0;
            delivery objDelivery = new delivery();
            List<delivery> listDelivery = new List<delivery>();
            List<delivery_item_summary> listDeliveryItemSummary = new List<delivery_item_summary>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (!context.loading.Any(x => x.employee_id == _empId && DbFunctions.TruncateTime(x.load_date) <= _date && x.status == 1 && x.item_id == _itemId))
                    {
                        firstLoad = true;
                        return 0;
                    }
                    else
                    {
                        if (context.loading.Count(x => x.employee_id == _empId && DbFunctions.TruncateTime(x.load_date) == _date && x.status == 1 && x.item_id == _itemId) == 1)
                        {
                            firstLoad = true;
                        }
                    }

                    if (context.delivery_item_summary.Any(x => x.delivery_id == deliveryId && x.item_id == _itemId & x.qty > 0 && x.status == 1))
                    {
                        delivery_item_summary xDel = context.delivery_item_summary.FirstOrDefault(x => x.delivery_id == deliveryId && x.item_id == _itemId && x.status == 1);
                        PrvBalance = xDel.qty - (xDel.used_qty + xDel.damage_qty);
                        totalLoading = xDel.qty;

                        //Getting actual stock as previous balance
                        loading load = context.loading.Where(x => x.delivery_id == deliveryId && x.item_id == _itemId && x.status == 1).OrderByDescending(x => x.load_id).FirstOrDefault();
                        if (load != null)
                        {
                            PrvBalance = load.act_stock;

                        }
                        else
                        {
                            totalLoading = 0;
                            load = context.loading.Where(x => x.delivery_id < deliveryId && x.employee_id == _empId && x.item_id == _itemId && x.status == 1).ToList().OrderByDescending(x => x.load_id).FirstOrDefault();
                            if (load != null)
                                PrvBalance = load.act_stock;
                        }

                        totalEmpty = context.loading.Where(x => x.delivery_id == deliveryId && x.status == 1).Select(x => x.empty).DefaultIfEmpty(0).Sum();
                        totalShort = context.loading.Where(x => x.delivery_id == deliveryId && x.status == 1).Select(x => x.@short).DefaultIfEmpty(0).Sum();
                        totalExtra = context.loading.Where(x => x.delivery_id == deliveryId && x.status == 1).Select(x => x.extra).DefaultIfEmpty(0).Sum();
                    }
                    //if first loading
                    else
                    {
                       DateTime loadStartDate =Convert.ToDateTime( context.system_settings.FirstOrDefault().loading_start_date);
                        //DateTime loadStartDate = new DateTime(2024,12,08); 
                        objDelivery = context.delivery.Include(x => x.delivery_item_summary).Where(p => p.employee_id == _empId && (p.delivery_date> loadStartDate && p.delivery_date < _date) ).OrderByDescending(i => i.delivery_date).FirstOrDefault();
                        if (objDelivery != null && objDelivery.delivery_item_summary != null)
                        {
                            foreach (delivery_item_summary data in objDelivery.delivery_item_summary.Where(x => x.item_id == _itemId))
                            {
                                //PrvBalance += Convert.ToDecimal(data.qty-data.used_qty);
                                PrvBalance += Convert.ToDecimal(data.qty);
                                totalLoading = 0;
                            }
                            loading load = context.loading.Where(x => x.delivery_id == deliveryId && x.item_id == _itemId && x.status == 1).OrderByDescending(x => x.load_id).FirstOrDefault();
                            if (load != null)
                            {
                                PrvBalance = load.act_stock;
                                totalLoading = context.loading.Where(x => x.delivery_id == deliveryId && x.item_id == _itemId && x.status == 1).Select(x => x.new_load).DefaultIfEmpty(0).Sum();
                            }
                            else
                            {
                                load = context.loading.Where(x => x.delivery_id < deliveryId && x.employee_id == _empId && x.item_id == _itemId && x.status == 1).ToList().OrderByDescending(x => x.load_id).FirstOrDefault();
                                if (load != null)
                                    PrvBalance = load.act_stock;
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PrvBalance;
        }

        public List<delivery> SearchDeliveryIdGenerated(DateTime dateFrom, DateTime dateTo)
        {
            List<delivery> listDelivery = new List<delivery>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDelivery = context.delivery.AsNoTracking().Include(x => x.employee).Include(r => r.route).Where(x => x.status == 1 && x.delivery_date >= dateFrom && x.delivery_date <= dateTo).OrderByDescending(x => x.delivery_id).Take(50).ToList();

                }
            }
            catch
            {
                throw;
            }
            return listDelivery;
        }

        public List<EDMX.delivery_item_summary> GetDeliveryItemSummaryById(int deliveryId)
        {
            List<EDMX.delivery_item_summary> listDeliveryItemSummary = new List<EDMX.delivery_item_summary>();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    listDeliveryItemSummary = context.delivery_item_summary.Include(p => p.item)
                      .Where(x => x.delivery_id == deliveryId).ToList();

                    foreach (delivery_item_summary ds in listDeliveryItemSummary)
                    {
                        decimal sold = GetSoldQuantity(deliveryId, ds.item_id);

                        if (sold >= 0 && (sold > ds.used_qty || sold <= ds.used_qty))
                        {
                            ds.used_qty = sold;
                            ds.balance_qty = ds.qty - sold;
                            if (ds.qty <= 0 && ds.balance_qty < 0)
                            {
                                ds.balance_qty = 0;
                            }
                            context.Entry(ds).State = EntityState.Modified;
                            context.SaveChanges();

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return listDeliveryItemSummary;

        }
        public int GetDeliveryRouteId(int empId, DateTime date)
        {
            int routeId = 0;
            using (var context = new betaskdbEntities())
            {
                var delivery = context.delivery.Where(x => x.delivery_date == date && x.employee_id == empId).FirstOrDefault();
                if (delivery != null)
                    routeId = delivery.employee_id;
            }
            return routeId;
        }
        public EDMX.delivery GetDeliveryDetails(int deliveryId)
        {
            EDMX.delivery delivery = new EDMX.delivery();

            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery = context.delivery.Include(x => x.employee).Include(r => r.route).Include(x => x.delivery_items).Include(c => c.delivery_items.Select(cu => cu.customer)).Include(x => x.delivery_items.Select(i => i.item).Select(u => u.uom_setting).Select(t => t.item.Select(p => p.tax_setting)))
                         .Include(x => x.delivery_item_summary).Include(x => x.delivery_item_summary.Select(i => i.item).Select(u => u.uom_setting).Select(t => t.item.Select(p => p.tax_setting)))
                         .Where(x => x.delivery_id == deliveryId).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return delivery;

        }

        public int GetDeliveryCount(int deliveryId)
        {
            using (var context = new betaskdbEntities())
            {
                return context.delivery_items.Where(x => x.delivery_id == deliveryId && x.status == 4).Count();
            }
        }

        public EDMX.delivery GetGeneratedDelivery(int deliveryId)
        {
            EDMX.delivery delivery = new EDMX.delivery();

            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery = context.delivery.Where(x => x.delivery_id == deliveryId).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return delivery;

        }

        public EDMX.delivery GetDeliveryDetails(int employeeId, int routeId, DateTime date, bool getDliveryIdOnly = false)
        {
            EDMX.delivery delivery = new EDMX.delivery();

            try
            {
                using (var context = new betaskdbEntities())
                {
                    if(getDliveryIdOnly)
                    return context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.employee_id == employeeId && (routeId > 0 ? x.route_id == routeId : x.route_id > 0)).FirstOrDefault();

                    delivery = context.delivery
                        .Include(x => x.employee)
                        .Include(x => x.delivery_items)
                        .Include(c => c.delivery_items.Select(cu => cu.customer))
                        .Include(x => x.delivery_items.Select(i => i.item).Select(u => u.uom_setting).Select(t => t.item.Select(p => p.tax_setting)))
                        .Include(x => x.delivery_item_summary).Include(x => x.delivery_item_summary.Select(i => i.item).Select(u => u.uom_setting).Select(t => t.item.Select(p => p.tax_setting)))
                        .Where(x => x.delivery_date == date && x.employee_id == employeeId && (routeId > 0 ? x.route_id == routeId : x.route_id > 0)).FirstOrDefault();


                    //if employee route changed
                    if (delivery == null || (delivery.delivery_items == null || delivery.delivery_items.Count == 0))
                    {
                        //int currentRoute = Convert.ToInt32(context.employee.FirstOrDefault(x => x.employee_id == employeeId).route_id);
                        // if (currentRoute != routeId)
                        {
                            delivery = context.delivery.Include(x => x.employee).Include(x => x.delivery_items).Include(c => c.delivery_items.Select(cu => cu.customer)).Include(x => x.delivery_items.Select(i => i.item).Select(u => u.uom_setting).Select(t => t.item.Select(p => p.tax_setting)))
                        .Include(x => x.delivery_item_summary).Include(x => x.delivery_item_summary.Select(i => i.item).Select(u => u.uom_setting).Select(t => t.item.Select(p => p.tax_setting)))
                        .Where(x => x.delivery_date == date && x.employee_id == employeeId).FirstOrDefault();
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return delivery;

        }

        public void DistinctRout_Vehicle(out List<string> route, out List<string> vehicle)
        {
            try
            {
                route = new List<string>();
                vehicle = new List<string>();
                using (var context = new betaskdbEntities())
                {
                    route = context.delivery.Select(x => x.delivery_route).Distinct().ToList();
                    vehicle = context.delivery.Select(x => x.vehicle_no).Distinct().ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<delivery> GetDeliveryDataByDate(DateTime deliveryDate)
        {
            List<EDMX.delivery> delivery = new List<EDMX.delivery>();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    delivery = context.delivery.Include(x => x.employee).Where(x => x.delivery_date == deliveryDate).ToList();
                }
            }
            catch
            {
                throw;
            }
            return delivery;
        }
        public List<BETask.DAL.Model.DeliveryLoadReportModel> GetDeliveryLoadDetails(DateTime dateFrom, DateTime dateTo, int employeeId, int itemId)
        {
            List<BETask.DAL.Model.DeliveryLoadReportModel> listDeliveryItemSummary = new List<BETask.DAL.Model.DeliveryLoadReportModel>();
            List<EDMX.delivery> listDelivery = new List<EDMX.delivery>();
            // EDMX.delivery_item_summary DeliveryItemSummary = new EDMX.delivery_item_summary();
            try
            {

                using (var context = new betaskdbEntities())
                {
                    decimal prvbal = 0;
                    decimal balance = 0;
                    decimal loadQty = 0;
                    decimal soldQty = 0;
                    listDelivery = context.delivery.Include(x => x.delivery_items).Include(x => x.delivery_item_summary).Where(x => x.delivery_date >= dateFrom && x.delivery_date <= dateTo && (employeeId > 0 ? x.employee_id == employeeId : x.employee_id > 0) && x.status == 1).OrderBy(x => x.delivery_date).ToList();
                    foreach (delivery Delivery in listDelivery)
                    {
                        List<EDMX.delivery_item_summary> listItemSummary = new List<EDMX.delivery_item_summary>();
                        listItemSummary = context.delivery_item_summary.Include(p => p.item).Where(x => x.delivery_id == Delivery.delivery_id && (itemId > 0 ? x.item_id == itemId : x.item_id > 0)).ToList();

                        foreach (delivery_item_summary data in listItemSummary)
                        {
                            prvbal = GetPreviousDayBalance(data.delivery.delivery_date, employeeId, Convert.ToInt32(data.item_id));
                            soldQty = data.used_qty;//GetSoldQuantity(data.delivery_id, Convert.ToInt32(data.item_id)) + data.used_qty;
                            loadQty = data.qty - prvbal;
                            balance = data.balance_qty;
                            data.used_qty = soldQty;
                            //data.balance_qty = balance;
                            BETask.DAL.Model.DeliveryLoadReportModel DeliveryItemSummary = new BETask.DAL.Model.DeliveryLoadReportModel();
                            DeliveryItemSummary.Date = Convert.ToDateTime(Delivery.delivery_date);
                            DeliveryItemSummary.ItemId = Convert.ToInt32(data.item_id);
                            DeliveryItemSummary.EmployeeName = Convert.ToString(Delivery.employee.first_name);
                            DeliveryItemSummary.ItemName = Convert.ToString(data.item.item_name);
                            DeliveryItemSummary.LoadQty = Convert.ToDecimal(loadQty);
                            DeliveryItemSummary.TotalQty = Convert.ToDecimal(data.qty);
                            DeliveryItemSummary.BalanceQty = Convert.ToDecimal(data.balance_qty);
                            DeliveryItemSummary.SoldQty = Convert.ToDecimal(data.used_qty);
                            DeliveryItemSummary.PreviousBalance = Convert.ToDecimal(prvbal);
                            DeliveryItemSummary.DamageQty = data.damage_qty;
                            DeliveryItemSummary.Remarks = Convert.ToString(data.remarks);
                            listDeliveryItemSummary.Add(DeliveryItemSummary);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listDeliveryItemSummary;
        }
        public EDMX.delivery_items GetDeliveryItem(int deliveryItemId)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.delivery_items.FirstOrDefault(x => x.delivery_item_id == deliveryItemId);
                }
            }
            catch
            {
                throw;
            }

        }
        public List<delivery> EmployeeDeliveryReport(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            List<EDMX.delivery> delivery = new List<EDMX.delivery>();

            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery = context.delivery.Include(emp => emp.employee).Include(d => d.delivery_items).Include(s => s.sales).Where(x => x.delivery_date >= dateFrom && x.delivery_date <= dateTo && (employeeId > 0 ? x.employee_id == employeeId : x.employee_id > 0) && (x.status == 1 ||  x.status==4)).OrderBy(x => x.delivery_date).ToList();
                }
            }
            catch
            {
                throw;
            }
            return delivery;
        }

        public DataTable EmployeeDeliveryReportSumamry(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            DataTable tblDSR = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetEmployeeDSRSummary", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@dateTo", dateTo);
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {

                                adr.Fill(tblDSR);

                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return tblDSR;
        }
        public List<delivery_items> ItemDeliveryReportOld(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, bool deliveredOnly, int routeId)
        {
            List<EDMX.delivery_items> deliveryItems = new List<EDMX.delivery_items>();

            try
            {
                using (var context = new betaskdbEntities())
                {
                    deliveryItems = context.delivery_items.Include(d => d.delivery).Include(c => c.customer).Include(e => e.delivery.employee).Include(i => i.item).Include(u => u.item.uom_setting).Where(d => d.delivery.delivery_date >= dateFrom && d.delivery.delivery_date <= dateTo && (d.status == 1 || d.status == 4)).ToList();
                    if (deliveredOnly)
                    {
                        deliveryItems = deliveryItems.Where(t => t.delivery_time != null).ToList();
                    }
                    if (customerId > 0)
                    {
                        deliveryItems = deliveryItems.Where(c => c.customer_id == customerId).ToList();
                    }
                    if (itemId > 0)
                    {
                        deliveryItems = deliveryItems.Where(i => i.item_id == itemId).ToList();
                    }
                    if (routeId > 0)
                    {
                        deliveryItems = deliveryItems.Where(i => i.delivery.route_id == routeId).ToList();
                    }
                    deliveryItems = deliveryItems.OrderBy(d => d.delivery.delivery_date).ToList();
                }
            }
            catch
            {
                throw;
            }
            return deliveryItems;
        }
        public DataTable ItemDeliveryReport(DateTime dateFrom, DateTime dateTo, int customerId, int itemId, bool deliveredOnly, int routeId, decimal rangeFrom = 0, decimal rangeTo = 0, string paymentMode = "")
        {
            DataTable tblReport = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SP_ItemDeliveryReport", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@DateFrom", dateFrom);
                            command.Parameters.AddWithValue("@DateTo", dateTo);
                            command.Parameters.AddWithValue("@DeliveredOnly", deliveredOnly);
                            command.Parameters.AddWithValue("@CustomerId", customerId);
                            command.Parameters.AddWithValue("@ItemId", itemId);
                            command.Parameters.AddWithValue("@RouteId", routeId);
                            command.Parameters.AddWithValue("@RangeFrom", rangeFrom);
                            command.Parameters.AddWithValue("@RangeTo", rangeTo);
                            command.Parameters.AddWithValue("@PaymentMode", paymentMode);
                            using (SqlDataAdapter adr = new SqlDataAdapter(command))
                            {

                                adr.Fill(tblReport);

                            }
                        }
                    }
                }
            }
            catch(Exception ee)
            {
                throw new Exception(ee.ToString());
            }
            return tblReport;
        }

        public List<EDMX.delivery_items> GetDeliveryDetails_Pending(int deliveryId)
        {
            EDMX.delivery delivery = new EDMX.delivery();
            List<EDMX.delivery_items> deliveryItems = new List<EDMX.delivery_items>();
            try
            {
                using (var context = new betaskdbEntities())
                {


                    deliveryItems = context.delivery_items.Include(d => d.delivery).Where(x => (x.status == 3 || x.status == 1)).ToList();
                    if (deliveryId > 0)
                    {
                        deliveryItems = deliveryItems.Where(x => x.delivery_id == deliveryId).ToList();
                    }

                }
            }
            catch
            {
                throw;
            }
            return deliveryItems;

        }
        public List<EDMX.delivery_items> GetDeliveryDetails_Pending_Recheck(int deliveryId)
        {
            EDMX.delivery delivery = new EDMX.delivery();
            List<EDMX.delivery_items> deliveryItems = new List<EDMX.delivery_items>();
            try
            {
                using (var context = new betaskdbEntities())
                {


                    deliveryItems = context.delivery_items.Include(d => d.delivery).Where(x => x.status == 4 && x.sales_id == null).ToList();
                    if (deliveryId > 0)
                    {
                        deliveryItems = deliveryItems.Where(x => x.delivery_id == deliveryId).ToList();
                    }

                }
            }
            catch
            {
                throw;
            }
            return deliveryItems;

        }
        public void SaveDeliveryReturn(EDMX.delivery_return delivery_Return)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            delivery_Return.route_id = context.employee.FirstOrDefault(x => x.employee_id == delivery_Return.employee_id).route_id;
                            context.Entry(delivery_Return).State = EntityState.Added;
                            context.SaveChanges();
                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_DeliveryReturn(delivery_Return, context);
                            if (delivery_Return.return_type == 2)
                            {
                                CustomerAssetDAL customerAssetDAL = new CustomerAssetDAL(context);
                                customerAssetDAL.UpdateAsseFromReturn(delivery_Return);
                            }
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
        public void ApproveDeliveryReturn(List<EDMX.delivery_return> listDelivery_Return)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (EDMX.delivery_return delivery_Return in listDelivery_Return)
                            {
                                if (delivery_Return.qty > 0)
                                {
                                    var xDel = context.delivery_return.Single(x => x.delivery_return_id == delivery_Return.delivery_return_id);
                                    xDel.status = 4;
                                    context.Entry(xDel).State = EntityState.Modified;
                                    context.SaveChanges();
                                    ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                                    itemTransactionDAL.SaveItemTransaction_DeliveryReturn(delivery_Return, context);
                                }

                            }
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
        public List<EDMX.delivery_return> GetDeliveryReturn(DateTime returnDate, int status = 1, int empId = 0)
        {
            List<EDMX.delivery_return> listDeliveryReturn = new List<delivery_return>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDeliveryReturn = context.delivery_return.Include(i => i.item).Include(c => c.customer).Include(r => r.customer.route).Include(e => e.employee).Where(x => x.return_date == returnDate && x.status == status /*&& x.qty > 0*/).OrderBy(o => o.employee_id).ToList();
                    if (empId > 0)
                        listDeliveryReturn = listDeliveryReturn.Where(x => x.employee_id == empId).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listDeliveryReturn;
        }
        public decimal GetDeliveryItemSummaryAdditionalItemQty(int deliveryId, int itemId)
        {
            decimal addQty = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    decimal dQty = context.delivery_items.Where(x => x.delivery_id == deliveryId && x.item_id == itemId).ToList().Sum(x => x.delivered_qty);
                    decimal sQty = context.delivery_item_summary.Where(x => x.delivery_id == deliveryId && x.item_id == itemId).ToList().Sum(x => x.qty);
                    addQty = sQty - dQty;
                }
            }

            catch
            {
                addQty = 0;
                //throw;
            }
            return addQty;
        }
        public void UpdateAdditionalDeliveryQty(decimal addQty, int deliveryId, int itemId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var itemSummary = context.delivery_item_summary.Where(x => x.delivery_id == deliveryId && x.item_id == itemId && x.status == 1).FirstOrDefault();
                    if (itemSummary != null)
                    {
                        itemSummary.qty = itemSummary.qty + addQty;
                        itemSummary.balance_qty = itemSummary.balance_qty + addQty;
                        context.Entry(itemSummary).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                }
            }

            catch
            {
                throw;
            }
        }

        public EDMX.delivery_return DeleteNotApprovedDeliveryReturn(int deliveryReturnId)
        {
            EDMX.delivery_return delivery_Return = null;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var xDeliveryReturn = context.delivery_return.Single(x => x.delivery_return_id == deliveryReturnId);
                    if (xDeliveryReturn != null)
                    {
                        delivery_Return = new delivery_return
                        {
                            customer_id = xDeliveryReturn.customer_id,
                            employee_id = xDeliveryReturn.employee_id,
                            item_id = xDeliveryReturn.item_id,
                            qty = xDeliveryReturn.qty,
                            return_date = xDeliveryReturn.return_date
                        };
                        xDeliveryReturn.status = 2;
                        context.Entry(xDeliveryReturn).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return delivery_Return;
        }
        //public List<EDMX.delivery_return> GetDeliveryReturnReport(DateTime dateFrom, DateTime dateTo, int routeId, int customerId, int employeeId, int itemId, int returnType)
        //{
        //    List<EDMX.delivery_return> listDeliveryReturn = new List<delivery_return>();
        //    try
        //    {
        //        using (var context = new betaskdbEntities())
        //        {
        //            listDeliveryReturn = context.delivery_return.Include(i => i.item).Include(c => c.customer).Include(r => r.customer.route).Include(e => e.employee).Where(x => (x.return_date >= dateFrom && x.return_date <= dateTo) && x.status == 4).OrderBy(o => o.item.item_name).ToList();
        //            if (routeId > 0)
        //                listDeliveryReturn = listDeliveryReturn.Where(x => x.customer.route_id == routeId).ToList();
        //            if (customerId > 0)
        //                listDeliveryReturn = listDeliveryReturn.Where(x => x.customer_id == customerId).ToList();
        //            if (employeeId > 0)
        //                listDeliveryReturn = listDeliveryReturn.Where(x => x.employee_id == employeeId).ToList();
        //            if (itemId > 0)
        //                listDeliveryReturn = listDeliveryReturn.Where(x => x.item_id == itemId).ToList();
        //            if (returnType == 2)
        //                listDeliveryReturn = listDeliveryReturn.Where(x => x.return_type == returnType).ToList();
        //            else
        //                listDeliveryReturn = listDeliveryReturn.Where(x => x.return_type != 2).ToList();
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return listDeliveryReturn;
        //}

        public DataTable GetDeliveryReturnReport(DateTime dateFrom, DateTime dateTo, int routeId, int customerId, int employeeId, int itemId, int returnType)
        {
            DataTable tblDeliveryReturn = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SP_GetDeliveryReturnReport", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.DateTime)).Value = dateFrom;
                            command.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime)).Value = dateTo;
                            command.Parameters.Add(new SqlParameter("@RouteId", SqlDbType.Int)).Value = routeId;
                            command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int)).Value = customerId;
                            command.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)).Value = employeeId;
                            command.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.Int)).Value = itemId;
                            command.Parameters.Add(new SqlParameter("@ReturnType", SqlDbType.Int)).Value = returnType;

                            SqlDataAdapter adapter = new SqlDataAdapter(command);

                            adapter.Fill(tblDeliveryReturn);
                            conn.Close();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return tblDeliveryReturn;
        }

        public List<Model.DeliveryItemSummaryModel> GetDeliveryItemSumamry(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            // List<EDMX.delivery_item_summary> listDeliveryItemSummary = new List<delivery_item_summary>();
            List<Model.DeliveryItemSummaryModel> listDeliveryItemSummary = new List<Model.DeliveryItemSummaryModel>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var _listDeliveryItemSummary = context.delivery_items.Include(d => d.delivery).Include(e => e.delivery.employee).Include(i => i.item).Where(x => x.delivery.delivery_date >= dateFrom && x.delivery.delivery_date <= dateTo && x.status == 4 && x.delivered_qty > 0 && x.rate > 0 && x.delivery.status == 1 && (employeeId > 0 ? x.delivery.employee_id == employeeId : x.delivery.employee_id > 0));
                    //var itemSummary = _listDeliveryItemSummary.GroupBy(x => x.item.item_name).Select(g => new { ItemName = g.Max(x => x.item.item_name),  DeleveredQty = g.Sum(x => x.delivered_qty), Amount = g.Sum(x => x.net_amount) }).OrderBy(x=>x.ItemName);
                    var itemSummary = _listDeliveryItemSummary.GroupBy(x => new { x.item.item_name, x.rate }).Select(g => new { ItemName = g.Key.item_name, g.Key.rate, DeleveredQty = g.Sum(x => x.delivered_qty), Amount = g.Sum(x => x.net_amount) }).OrderBy(x => x.ItemName);

                    var _listDeliveryItemSummaryFoc = context.delivery_items.Include(d => d.delivery).Include(e => e.delivery.employee).Include(i => i.item).Where(x => x.delivery.delivery_date >= dateFrom && x.delivery.delivery_date <= dateTo && x.status == 4 && x.delivered_qty > 0 && x.rate == 0 && x.delivery.status == 1 && (employeeId > 0 ? x.delivery.employee_id == employeeId : x.delivery.employee_id > 0));
                    var itemSummaryFoc = _listDeliveryItemSummaryFoc.GroupBy(x => x.item.item_name).Select(g => new { ItemName = g.Max(x => x.item.item_name), DeleveredQty = g.Sum(x => x.delivered_qty) }).OrderBy(x => x.ItemName);

                    foreach (var prop in itemSummary)
                    {
                        decimal foc = itemSummaryFoc.Where(x => x.ItemName == prop.ItemName).Select(x => x.DeleveredQty).DefaultIfEmpty(0).Sum();
                        listDeliveryItemSummary.Add(new Model.DeliveryItemSummaryModel
                        {

                            ItemName = $"{prop.ItemName} @ {prop.rate}",
                            ScheduledQty = 0,
                            DeleveredQty = prop.DeleveredQty,
                            Foc = foc,
                            TotalQty = foc + prop.DeleveredQty,
                            Amount = prop.Amount
                        });
                    }

                }
            }
            catch
            {
                throw;
            }
            return listDeliveryItemSummary;
        }
        public Model.ProductionDeliveryReportModel GetProductionDeliveryReport(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            Model.ProductionDeliveryReportModel prodItemReport = new Model.ProductionDeliveryReportModel { };
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<delivery_items> listItems = context.delivery_items.Include(d => d.delivery).Include(e => e.delivery.employee).Where(x => (x.delivery.delivery_date >= dateFrom && x.delivery.delivery_date <= dateTo && x.item_id == itemId) && x.delivery.status == 1).ToList();


                    var itemSummary = listItems.GroupBy(x => x.delivery.employee_id).Select(g => new { Scheduled = g.Sum(x => x.qty), Delivered = g.Sum(x => x.delivered_qty), EmployeeId = g.Max(x => x.delivery.employee_id), EmployeeName = g.Max(x => $"{x.delivery.employee.first_name} {x.delivery.employee.last_name}") });

                    prodItemReport.Stock = Convert.ToDecimal(context.item.Where(x => x.item_id == itemId).FirstOrDefault().Stock);
                    List<production> prod = (context.production.AsNoTracking().Where(x => x.item_id == itemId && x.production_date >= dateFrom && x.production_date <= dateTo && x.status == 1).ToList());
                    prodItemReport.Production = 0;
                    if (prod != null)
                    {
                        prodItemReport.Production = prod.Sum(x => x.qty);
                    }

                    List<Model.ProductionDeliveryDetail> listDetail = new List<Model.ProductionDeliveryDetail>();
                    foreach (var prop in itemSummary)
                    {
                        int _employeeId = Convert.ToInt32(prop.EmployeeId);
                        string _employeeName = prop.EmployeeName;
                        decimal _scheduled = Convert.ToDecimal(prop.Scheduled);
                        decimal _delivered = Convert.ToDecimal(prop.Delivered);
                        decimal _balance = prop.Scheduled - prop.Delivered;
                        decimal _return = 0;
                        List<delivery_return> listRerurn = context.delivery_return.AsNoTracking().Where(x => x.item_id == itemId && x.employee_id == prop.EmployeeId && (x.return_date >= dateFrom && x.return_date <= dateTo)).ToList();
                        if (listRerurn != null)
                            _return = Convert.ToDecimal(listRerurn.Sum(x => x.qty));
                        listDetail.Add(new Model.ProductionDeliveryDetail
                        {
                            EmployeeId = _employeeId,
                            EmployeeName = _employeeName,
                            Scheduled = _scheduled,
                            Delivered = _delivered,
                            Balance = _balance,
                            Returned = _return
                        });
                    }
                    prodItemReport.listDeliveryDetail = listDetail;
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return prodItemReport;
        }
        public List<EDMX.daily_collection> GetDailyColelction(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int routeId, int employeeId, int customerId, string paymentMode)
        {
            List<EDMX.daily_collection> listDailyCollection = new List<daily_collection>();
            try
            {


                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listDailyCollection = context.daily_collection.Include(x => x.employee).Include(c => c.customer).Include(r => r.customer.route).Where(x => (x.status != 2 && x.status != 5) && (x.delivery_time >= dailyDateTimeFrom && x.delivery_time <= dailyDateTimeTo)).OrderBy(o => o.customer.route_id).ToList();
                    if (routeId > 0)
                        listDailyCollection = listDailyCollection.Where(x => x.customer.route_id == routeId).ToList();
                    if (customerId > 0)
                        listDailyCollection = listDailyCollection.Where(x => x.customer_id == customerId).ToList();
                    if (employeeId > 0)
                        listDailyCollection = listDailyCollection.Where(x => x.employee_id == employeeId).ToList();
                    if (paymentMode != "All")
                        listDailyCollection = listDailyCollection.Where(x => x.payment_mode == paymentMode).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listDailyCollection;
        }

        public IEnumerable<daily_collection> GetDailyCollectionByDelivery(int deliveryId)
        {
            try
            {


                using (betaskdbEntities context = new betaskdbEntities())
                {
                    return context.daily_collection.Where(x => x.delivery_id == deliveryId).AsEnumerable();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public DataTable GetDepositCollection(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int vendorId, int itemId, int routeId)
        {
            DataTable tblData = new DataTable();
            try
            {
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SP_GetDepositCollection", conn))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add(new SqlParameter("@dailyDateTimeFrom", SqlDbType.DateTime)).Value = dailyDateTimeFrom;
                            command.Parameters.Add(new SqlParameter("@dailyDateTimeTo", SqlDbType.DateTime)).Value = dailyDateTimeTo;
                            command.Parameters.Add(new SqlParameter("@routeId", SqlDbType.Int)).Value = routeId;
                            command.Parameters.Add(new SqlParameter("@vendorId", SqlDbType.Int)).Value = vendorId;
                            command.Parameters.Add(new SqlParameter("@itemId", SqlDbType.Int)).Value = itemId;

                            SqlDataAdapter adapter = new SqlDataAdapter(command);

                            adapter.Fill(tblData);
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return tblData;
        }
        public List<EDMX.delivery_items> GetDepositCollectionOld(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int vendorId, int itemId, int routeId)
        {
            List<EDMX.delivery_items> listDepositCollection = new List<delivery_items>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    var query = context.delivery_items.Include(c=>c.customer).Include(r=>r.customer.route).Include(r=>r.daily_collection).Include(i=>i.item)
                        .Where(x => x.is_deposit == 1 &&
                                    x.delivery_time >= dailyDateTimeFrom &&
                                    x.delivery_time <= dailyDateTimeTo &&
                                    x.status == 4);

                    if (routeId > 0)
                    {
                        query = query.Where(x => x.daily_collection.route_id == routeId);
                    }

                    if (vendorId > 0)
                    {
                        query = query.Where(x => x.customer_id == vendorId);
                    }

                    if (itemId > 0)
                    {
                        query = query.Where(x => x.item_id == itemId);
                    }
                   
                    listDepositCollection = query.OrderBy(o => o.daily_collection.delivery_time).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listDepositCollection;
        }
        public List<EDMX.daily_collection> GetCollectionOnly(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int routeId, int employeeId)
        {
            List<EDMX.daily_collection> listDailyCollection = new List<daily_collection>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listDailyCollection = context.daily_collection.Include(x => x.employee).Include(c => c.customer).Include(r => r.customer.route).Where(x => x.status != 2 && x.status != 5 && (x.delivery_time >= dailyDateTimeFrom && x.delivery_time <= dailyDateTimeTo && x.employee_id == employeeId && x.delivery_id == null)).OrderBy(o => o.customer.route_id).OrderBy(x => x.payment_mode).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listDailyCollection;
        }
        public List<EDMX.daily_collection> GetCollectionReport(DateTime dailyDateTimeFrom, DateTime dailyDateTimeTo, int routeId, int employeeId, int customerId, string paymentMode)
        {
            List<EDMX.daily_collection> listDailyCollection = new List<daily_collection>();
            try
            {
                using (betaskdbEntities context = new betaskdbEntities())
                {
                    listDailyCollection = context.daily_collection.AsNoTracking().Include(x => x.employee).Include(c => c.customer).Include(r => r.customer.route).Where(x => x.status != 2 && x.status != 5 && (x.delivery_time >= dailyDateTimeFrom && x.delivery_time <= dailyDateTimeTo && x.collected_amount > 0 && (routeId > 0 ? x.route_id == routeId : x.route_id > 0) && (employeeId > 0 ? x.employee_id == employeeId : x.employee_id > 0) && (customerId > 0 ? x.customer.customer_id == customerId : x.customer.customer_id > 0) && (paymentMode.ToLower() != "all" ? x.payment_mode == paymentMode : x.payment_mode != null) && x.delivery_id == null)).OrderBy(o => o.customer.route_id).OrderBy(x => x.delivery_time).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listDailyCollection;
        }


        public void UpdateDailyCollectionStatus(int deliveryId, int customerId, int status)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var dailyColl = context.daily_collection.Single(x => x.delivery_id == deliveryId && x.customer_id == customerId);
                    dailyColl.status = status;
                    context.Entry(dailyColl).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetDeliverySalesReprot(int employeeId, DateTime dateFrom, DateTime dateTo, int itemId, int deliveryId = 0)
        {
            DataSet ds = new DataSet();
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_DeliverySalesReport", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@employeeId", employeeId);
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@dateTo", dateTo);
                            cmd.Parameters.AddWithValue("@itemId", itemId);
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {

                                adr.Fill(ds);

                            }
                        }


                        //Getting FOC of customer and add with customer Name
                        if (ds != null && ds.Tables[0] != null && deliveryId > 0)
                        {
                            DataTable tblFoc = new DataTable();
                            using (SqlCommand cmd = new SqlCommand("SP_GetDsrFocByDeliveryId", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
                                cmd.Parameters.AddWithValue("@itemId", itemId);
                                using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                                {

                                    adr.Fill(tblFoc);

                                }
                            }

                            //foreach (DataRow dr in ds.Tables[0].Rows)
                            //{
                            //    int customerId = Convert.ToInt32(dr["customer_id"]);
                            //    decimal foc = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == deliveryId && x.customer_id == customerId && x.item_id == itemId && x.status == 4 && x.rate == 0 && x.qty > 0).Select(x => x.delivered_qty).DefaultIfEmpty(0).Sum();
                            //    if (foc > 0)
                            //    {
                            //        dr["customer_name"] = $"{dr["customer_name"]} #Foc:{foc}";
                            //    }
                            //}

                            if (tblFoc != null && tblFoc.Rows.Count > 0)
                            {
                                foreach (DataRow dr in tblFoc.Rows)
                                {
                                    int customerId = Convert.ToInt32(dr["customer_id"]);
                                    decimal foc = Convert.ToInt32(dr["delivered_qty"]);
                                    DataRow[] rows = ds.Tables[0].Select($"customer_id={customerId}");
                                    foreach (DataRow row in rows)
                                    {
                                        row["customer_name"] = $"{row["customer_name"]} #Foc:{foc}";
                                    }
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return ds;
        }


        public DataSet GetDelivery(int deliveryId)
        {
            DataSet ds = new DataSet();
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_GetDeliveryDetails", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);

                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(ds);

                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetDeliverySalesReprotRouteWise(int routeId, DateTime dateFrom, DateTime dateTo, int itemId, int deliveryId = 0)
        {
            DataSet ds = new DataSet();
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SP_DeliverySalesReportRoutewise", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@routeId", routeId);
                            cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@dateTo", dateTo);
                            cmd.Parameters.AddWithValue("@itemId", itemId);
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {

                                adr.Fill(ds);

                            }
                        }
                    }

                    //Getting FOC of customer and add with customer Name
                    if (ds != null && ds.Tables[0] != null && deliveryId > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            int customerId = Convert.ToInt32(dr["customer_id"]);
                            decimal foc = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == deliveryId && x.customer_id == customerId && x.item_id == itemId && x.status == 4 && x.rate == 0 && x.qty > 0).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                            if (foc > 0)
                            {
                                dr["customer_name"] = $"{dr["customer_name"]} #Foc:{foc}";
                            }
                        }
                    }

                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return ds;
        }
        public List<Model.DSRDeliveryItemsModel> GetOtherDeliveryItems(int excludedItem, int deliveryId)
        {
            List<delivery_items> listDeliveryItems = new List<delivery_items>();
            List<Model.DSRDeliveryItemsModel> listDSRItems = new List<Model.DSRDeliveryItemsModel>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int hideDo = context.system_settings.FirstOrDefault(x => x.status == 1).hide_do;
                    listDeliveryItems = context.delivery_items.AsNoTracking().Include(d => d.delivery).Include(c => c.customer).Include(i => i.item).Where(x => x.delivery_id == deliveryId && x.item_id != excludedItem && x.delivery_time != null && x.status == 4).OrderBy(x => x.item_id).ThenBy(x => x.customer_id).ToList();
                    if (listDeliveryItems != null && listDeliveryItems.Count > 0)
                    {

                        foreach (delivery_items it in listDeliveryItems)
                        {

                            decimal empty = context.delivery_return.Where(x => x.customer_id == it.customer_id && x.item_id == it.item_id && x.employee_id == it.delivery.employee_id && x.status == 1).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                            string paymentMode = "";

                            var coll = context.daily_collection.AsNoTracking().Where(x => x.daily_collection_id == it.daily_collection_id && x.delivery_id == it.delivery_id).FirstOrDefault();
                            if (coll != null)
                                paymentMode = coll.payment_mode;
                            else
                            {
                                coll = context.daily_collection.AsNoTracking().Where(x => x.daily_collection_id == it.daily_collection_id && x.delivery_id == it.delivery_id).FirstOrDefault();
                                if (context.sales.Any(x => x.sales_order == it.delivery_id && x.customer_id == it.customer_id && x.status == 1 && x.collection_id == coll.daily_collection_id))
                                    paymentMode = context.sales.Include(s => s.sales_item).AsNoTracking().Where(x => x.sales_order == it.delivery_id && x.customer_id == it.customer_id && x.status == 1).FirstOrDefault().payment_mode.ToLower();
                                else
                                    paymentMode = coll == null ? "Cash" : coll.payment_mode;
                            }


                            decimal cashSale = 0, creditSale = 0, couponSale = 0, doSale = 0, salesmanCredit = 0;
                            if (paymentMode.ToLower() == "cash") cashSale = it.net_amount;
                            else if (paymentMode.ToLower() == "credit") creditSale = it.net_amount;
                            else if (paymentMode.ToLower() == "coupon") couponSale = it.net_amount;
                            else if (paymentMode.ToLower() == "do")
                            {
                                if (hideDo == 1)
                                {
                                    it.net_amount = 0;
                                    doSale = 0;
                                }

                            }
                            else if (paymentMode.ToLower() == "salesmancredit") salesmanCredit = it.net_amount;


                            listDSRItems.Add(new Model.DSRDeliveryItemsModel
                            {
                                ItemName = it.item.item_name,
                                CustomerName = it.customer.customer_name,
                                Rate =Math.Round( it.net_amount > 0 ? it.net_amount / it.qty : 0,3),
                                Empty = empty,
                                Sale = it.delivered_qty,
                                CashSale = cashSale,
                                CreditSale = creditSale,
                                CouponSale = couponSale,
                                DoSale = doSale,
                                SalesmanCredit = salesmanCredit


                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listDSRItems;
        }
        public string GetPaymentModeInExceptionCase(int customerId, int deliveryId, int itemId, string _paymentMode)
        {
            string paymentMode = _paymentMode;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<delivery_items> listDelivery = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == deliveryId && x.customer_id == customerId && x.item_id == itemId && x.status == 4).ToList();
                    if (listDelivery != null && listDelivery.Count > 0)
                    {
                        foreach (delivery_items dl in listDelivery)
                        {
                            if (dl != null)
                            {
                                daily_collection coll = context.daily_collection.AsNoTracking().Where(x => x.delivery_id == deliveryId && x.customer_id == customerId && x.collected_amount == dl.net_amount).FirstOrDefault();
                                if (coll != null)
                                    paymentMode = coll.payment_mode;
                            }
                        }
                    }
                }
            }
            catch { }
            return paymentMode.ToLower();
        }

        public List<delivery_items> GetDeliveryItemsByIdandCustomer(int deliveryId, int customerId)
        {
            List<delivery_items> listDelivery = new List<delivery_items>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDelivery = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == deliveryId && (customerId==0?x.customer_id>0: x.customer_id == customerId) && x.status == 4).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listDelivery;
        }

        /// <summary>
        /// Updating auto delivery item to table
        /// </summary>
        /// <param name="listItems"></param>
        public List<string> UpdateDeliveredItemsFromAp(List<EDMX.delivery_items> listItems)
        {
            CouponDAL coupon = new CouponDAL();
            List<string> listUpdated = new List<string>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    foreach (delivery_items di in listItems)
                    {
                        var xItem = context.delivery_items.AsNoTracking().Where(x => x.delivery_item_id == di.delivery_item_id).FirstOrDefault();
                        if (xItem == null)
                        {
                            context.Entry(di).State = EntityState.Added;
                            context.SaveChanges();
                            listUpdated.Add($"{di.delivery_item_id}-{di.delivery_item_id}");
                            delivery xDelivery = context.delivery.Where(x => x.delivery_id == di.delivery_id).FirstOrDefault();
                            if (xDelivery != null)
                            {
                                int customerCount = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == di.delivery_id).ToList().Select(x => x.customer_id).Distinct().Count();
                                xDelivery.gross_amount += di.gross_amount;
                                xDelivery.total_discount += di.discount;
                                xDelivery.total_beforevat += di.total_beforvat;
                                xDelivery.total_vat += di.vat_amount;
                                xDelivery.net_amount += di.net_amount;
                                xDelivery.customer_count = customerCount;
                                context.Entry(xDelivery).State = EntityState.Modified;
                                context.SaveChanges();
                                if (di.delivery_leaf != null && di.delivery_time != null)
                                {

                                    coupon.RedeemDeliveryLeaf(di, context);
                                }
                            }
                        }
                        else
                        {
                            listUpdated.Add($"{di.delivery_item_id}-{xItem.delivery_item_id}");
                        }
                    }
                }
            }
            catch (Exception ee) { throw; }
            return listUpdated;
        }



        /// <summary>
        /// Only update collection mode amount in dailycolelction . Other account related posting should be do manually
        /// </summary>
        /// <param name="coll"></param>
        public void UpdateDailyCollection(EDMX.daily_collection coll)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var xDelivery = context.daily_collection.Where(x => x.daily_collection_id == coll.daily_collection_id).FirstOrDefault();

                            if (xDelivery != null)
                            {
                                decimal oldAmount = xDelivery.collected_amount;
                                xDelivery.payment_mode = coll.payment_mode;
                                xDelivery.collected_amount = coll.collected_amount;
                                xDelivery.net_amount = coll.net_amount;
                                context.Entry(xDelivery).State = EntityState.Modified;
                                context.SaveChanges();

                                customer customer = context.customer.Find(xDelivery.customer_id);
                                if (customer != null)
                                {
                                    if (customer.payment_mode.ToLower() == SaleDAL.EnumPaymentModes.COUPON.ToString().ToLower())
                                    {
                                        customer.wallet_balance = (customer.wallet_balance - oldAmount) + (xDelivery.collected_amount);
                                        context.customer.Attach(customer);
                                        context.Entry(customer).Property(x => x.wallet_balance).IsModified = true;
                                        context.SaveChanges();
                                    }
                                }

                                //Account update
                                if (xDelivery.delivery_id == null)
                                {
                                    DateTime _date = DateTime.Parse(xDelivery.delivery_time.Date.ToString("yyyy/MM/dd"));
                                    List<account_transaction> listTransaction = context.account_transaction.Where(x => x.voucher_number == "Collection" && x.transaction_type_id== xDelivery.daily_collection_id && x.status == 1 && x.transaction_date == _date && x.transaction_type == "RECIEPT").ToList();
                                    if (listTransaction != null && listTransaction.Count > 0)
                                    {
                                        foreach (account_transaction ac in listTransaction)
                                        {
                                            if (ac.ledger_id == customer.ledger_id)
                                            {
                                                ac.credit = xDelivery.collected_amount;
                                                context.account_transaction.Attach(ac);
                                                context.Entry(ac).Property(x => x.credit).IsModified = true;

                                            }
                                            else
                                            {
                                                ac.debit = xDelivery.collected_amount;
                                                context.account_transaction.Attach(ac);
                                                context.Entry(ac).Property(x => x.debit).IsModified = true;
                                            }

                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }


                            //Manual daily collection Entry, Back date and No Sale
                            else if (xDelivery == null && coll.daily_collection_id == 0)
                            {
                                //Updating Daily Collection
                                List<delivery_items> deliveryItems = context.delivery_items.Include(d => d.delivery).Where(x => x.delivery_id == coll.delivery_id && x.customer_id == coll.customer_id && x.status == 4).ToList();

                                daily_collection _Collection = new daily_collection
                                {
                                    delivery_id = coll.delivery_id,
                                    customer_id = coll.customer_id,
                                    net_amount = coll.net_amount,
                                    collected_amount = deliveryItems.Sum(x => x.net_amount),
                                    payment_mode = coll.payment_mode,
                                    employee_id = deliveryItems[0].delivery.employee_id,
                                    delivery_time = Convert.ToDateTime(deliveryItems[0].delivery_time),
                                    remarks = "Saved through manual delivery cretaion",
                                    status = 4
                                };
                                context.Entry(_Collection).State = EntityState.Added;
                                context.SaveChanges();
                            }

                            SynchronizationDAL sync = new SynchronizationDAL(context);
                            decimal walletBal = 0;
                            sync.UpdateWalletAsOutstanding(xDelivery.customer_id, ref walletBal);

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                if (ee.ToString().ToString().Contains("Violation of UNIQUE KEY"))
                {
                    using (var context = new betaskdbEntities())
                    {
                        var xDelivery = context.daily_collection.Where(x => x.daily_collection_id == coll.daily_collection_id).FirstOrDefault();
                        if (xDelivery != null)
                        {
                            xDelivery.status = 2;
                            context.Entry(xDelivery).Property(x => x.status).IsModified = true;
                            context.Entry(xDelivery).State = EntityState.Modified;
                            context.SaveChanges();
                            return;
                        }
                    }
                }
                throw;
            }
        }

        public void UpdateScehduledDeliveryQty(int deliveryId, int deliveryItemId, decimal qty, decimal gross, decimal vat, decimal net)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var xDelivery = context.delivery.Where(x => x.delivery_id == deliveryId).FirstOrDefault();
                    if (xDelivery != null)
                    {
                        decimal xGross = Convert.ToDecimal(xDelivery.gross_amount);
                        decimal xTotalBeforeVat = Convert.ToDecimal(xDelivery.total_beforevat);
                        decimal xNet = Convert.ToDecimal(xDelivery.net_amount);
                        decimal xVat = Convert.ToDecimal(xDelivery.total_vat);

                        delivery_items xItems = context.delivery_items.Where(x => x.delivery_id == deliveryId && x.delivery_item_id == deliveryItemId && x.status == 1).FirstOrDefault();
                        if (xItems != null && xItems.delivery_item_id != 0)
                        {
                            xDelivery.gross_amount = (xGross - xItems.gross_amount) + gross;
                            xDelivery.total_beforevat = (xGross - xItems.gross_amount) + gross;
                            xDelivery.total_vat = (xVat - xItems.vat_amount) + vat;
                            xDelivery.net_amount = (xNet - xItems.net_amount) + net;

                            xItems.qty = qty;
                            xItems.gross_amount = gross;
                            xItems.total_beforvat = gross;
                            xItems.vat_amount = vat;
                            xItems.net_amount = net;
                        }
                        context.Entry(xDelivery).State = EntityState.Modified;
                        context.Entry(xItems).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                }
            }
            catch (Exception ee) { throw; }
        }

        public List<customer> GetDeliveredCustomersId_WalletUpdate(int deliveryId, DateTime deliveryTime)
        {
            List<customer> listCustomer = new List<customer>();
            try
            {
                List<int> listCustomerId = new List<int>();
                using (var context = new betaskdbEntities())
                {
                    //List<delivery_items> _delivery = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == deliveryId && (x.delivery_time != null)).ToList();
                    //listCustomerId = _delivery.Where(x=> x.delivery_time>deliveryTime).Select(x => x.customer_id).Distinct().ToList();

                    listCustomerId = context.sales.AsNoTracking().Include(c => c.customer).Where(x => x.sales_order == deliveryId && x.payment_mode.ToLower() == "coupon").ToList().Select(x => x.customer_id).Distinct().ToList();
                    listCustomer = context.customer.AsNoTracking().Where(x => listCustomerId.Contains(x.customer_id) && (x.wallet_number != null && x.wallet_number != "")).ToList();

                }
            }
            catch (Exception ee) { throw; }
            return listCustomer;

        }

        public DataTable GetDuplicatedCollections(DateTime date)
        {
            DataTable tblDuplications = new DataTable();
            {
                string sql = "SELECT c.route_name,b.customer_name,min(daily_collection_id) as collid,a.customer_id,collected_amount,a.payment_mode, COUNT(a.customer_id) as duplicated,a.delivery_time " +
                             " FROM daily_collection a " +
                             " inner join customer b on b.customer_id = a.customer_id " +
                             " inner join route c on c.route_id = b.route_id " +
                             $" where delivery_id is null and cast(delivery_time as date )= '{date.ToString("yyyy-MM-dd")}' and a.status!=5 and a.status!=2 " +
                             " GROUP BY delivery_id,a.customer_id,collected_amount,a.payment_mode,b.customer_name,c.route_name,a.delivery_time" +
                             "  HAVING COUNT(a.customer_id) > 1 ";
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            using (SqlDataAdapter adr = new SqlDataAdapter(cmd))
                            {
                                adr.Fill(tblDuplications);
                            }
                        }
                    }
                }
            }
            return tblDuplications;
        }
        public int RemoveDuplicteCollection(int collId, int custoemrId, DateTime date)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        daily_collection _Collection = context.daily_collection.Where(x => x.daily_collection_id == collId && x.customer_id == custoemrId).FirstOrDefault();
                        if (_Collection != null)
                        {


                            int ledgerId = Convert.ToInt32(context.customer.AsNoTracking().Where(x => x.customer_id == custoemrId).FirstOrDefault().ledger_id);
                            var tranNumbers = context.account_transaction.AsNoTracking().Where(x => x.ledger_id == ledgerId && x.credit == _Collection.collected_amount && x.transaction_date == date && x.voucher_number == collId.ToString() && x.transaction_type == "RECIEPT").Select(x => x.transaction_number).Distinct().ToList();
                            if (tranNumbers != null && tranNumbers.Count >= 2)
                            {
                                int traNumber = tranNumbers[0];
                                List<account_transaction> listAccount = context.account_transaction.Where(x => x.transaction_number == traNumber && x.transaction_date == date).ToList();
                                foreach (account_transaction ac in listAccount)
                                {
                                    ac.status = 2;
                                    ac.narration = $"{ac.narration} , Duplication remove {DateTime.Now}";
                                    context.Entry(ac).State = EntityState.Modified;

                                }

                                context.SaveChanges();

                            }


                            _Collection.collected_amount = 0;
                            _Collection.net_amount = 0;
                            _Collection.remarks = $"Manually repmoved duplicated Collection {_Collection.collected_amount} {DateTime.Now}";
                            _Collection.status = 5;
                            context.Entry(_Collection).State = EntityState.Modified;
                            result = context.SaveChanges();
                            transaction.Commit();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<SP_DeliverySalePaymentMode_Result> GetDeliveryDaybookPaymentMode(int deliveryId)
        {
            List<SP_DeliverySalePaymentMode_Result> listDelivery = new List<SP_DeliverySalePaymentMode_Result>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDelivery = context.SP_DeliverySalePaymentMode(deliveryId).ToList();
                }
            }
            catch
            {
                throw;

            }
            return listDelivery;
        }

        public List<EDMX.SP_FOCbyPaymentmode_Result> GetFOCSale(int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {

                    return context.SP_FOCbyPaymentmode(deliveryId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<delivery_item_summary> GetDeliveryItemSummary(int deliveryId)
        {
            List<delivery_item_summary> listDelivery = new List<delivery_item_summary>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDelivery = context.delivery_item_summary.Include(i => i.item).Where(x => x.delivery_id == deliveryId && x.status == 1).ToList();
                }
            }
            catch
            {
                throw;

            }
            return listDelivery;
        }

        public int ReRunDSR(int deliveryId)
        {
            int result = 0;
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string connectionString = $"update daily_collection set status=2 where daily_collection_id in  (" +
                                                  " SELECT min(daily_collection_id) as collid " +
                                                   " FROM daily_collection where delivery_id = " + deliveryId + " " +
                                                  " GROUP BY delivery_id, customer_id, collected_amount, payment_mode,delivery_time " +
                                                  " HAVING COUNT(customer_id) > 1)";
                        using (SqlCommand cmd = new SqlCommand(connectionString, conn))
                        {
                            result = cmd.ExecuteNonQuery();
                        }
                    }


                }

            }
            catch (Exception ee)
            {
                throw;
            }
            return result;
        }
        public decimal GetActuaItemRateByDelivery(int deliveryItemId)
        {
            decimal itemRate = 0;
            try
            {

                using (var context = new betaskdbEntities())
                {
                    delivery_items delivery = context.delivery_items.FirstOrDefault(x => x.delivery_item_id == deliveryItemId);
                    if (delivery != null)
                    {
                        customer_aggrement aggrement = context.customer_aggrement.FirstOrDefault(x => x.customer_id == delivery.customer_id && x.item_id == delivery.item_id && x.status == 1 && x.unit_price > 0);
                        if (aggrement != null)
                            itemRate = aggrement.unit_price;
                    }
                }
            }


            catch (Exception ee)
            {
                throw;
            }
            return itemRate;
        }
        public void DSRUpdateCollectionRecieved(int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery del = context.delivery.FirstOrDefault(x => x.delivery_id == deliveryId);
                    if (del != null)
                    {
                        //DisableSpotDelivery(deliveryId, context);
                        del.status = 4;
                        del.remarks = "cash.collected";
                        context.Entry(del).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EnableSpotDelivery(int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery _delivery = context.delivery.Single(x => x.delivery_id == deliveryId);
                    _delivery.status = 3;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        public void DisableSpotDelivery(int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery _delivery = context.delivery.Single(x => x.delivery_id == deliveryId);
                    _delivery.status = 4;
                    _delivery.remarks = $"Disabled at {DateTime.Now}";
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        public void DisableSpotDelivery(int deliveryId,betaskdbEntities context)
        {
            try
            {
                {
                    delivery _delivery = context.delivery.Single(x => x.delivery_id == deliveryId);
                    _delivery.status = 4;
                    //_delivery.remarks = $"Disabled at {DateTime.Now}";
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        public List<EDMX.delivery_return> GetPermanantReturn(int customerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    return context.delivery_return.AsNoTracking().Include(i => i.item).Include(p => p.item.uom_setting).Include(e => e.employee).Where(x => x.customer_id == customerId && x.return_type == 2 && x.status == 4).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<delivery_item_summary> GetDeliveryItemBalance(int deliveryId, int employeeId)
        {
            List<delivery_item_summary> listItemSummary = new List<delivery_item_summary>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<item> listItems = context.item.AsNoTracking().Where(x => x.sellable == 1 && x.status == 1).ToList();
                    foreach (item it in listItems)
                    {
                        if (!context.delivery_item_summary.Include(d => d.delivery).Any(x => x.delivery_id == deliveryId && x.item_id == it.item_id && x.delivery.employee_id == employeeId))
                        {
                            var delivery = context.delivery_item_summary.Include(i => i.item).Include(d => d.delivery).Where(x => x.delivery_id < deliveryId && x.item_id == it.item_id && x.delivery.employee_id == employeeId).OrderByDescending(x => x.delivery_id).FirstOrDefault();

                            if (delivery != null && (delivery.qty - delivery.used_qty) != 0)
                            {
                                decimal prvBal = GetPreviousDayBalance(delivery.delivery.delivery_date, employeeId, delivery.item_id);
                                delivery.qty = (delivery.qty - delivery.used_qty) + prvBal;
                                delivery.remarks = "#";
                                listItemSummary.Add(delivery);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { throw; }
            return listItemSummary;
        }

        public void MapCollectionIdToDelieveryItem(int dailyCollectionId, int deliveryItemId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    delivery_items deliveryItem = context.delivery_items.FirstOrDefault(x => x.delivery_item_id == deliveryItemId);
                    deliveryItem.daily_collection_id = dailyCollectionId;
                    context.delivery_items.Attach(deliveryItem);
                    context.Entry(deliveryItem).Property(x => x.daily_collection_id).IsModified = true;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetDeliveryIdByDate(DateTime deliveryDate,int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var delivery = context.delivery.Find(deliveryId);
                    int empId = delivery.employee_id;
                    var xDelivery = context.delivery.FirstOrDefault(x=>x.delivery_date==deliveryDate && x.employee_id== empId);
                    return xDelivery==null?0: xDelivery.delivery_id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ChangeDeliveryId(long saleId,int deliveryId,int newDeliveryId)
        {
            try
            {

                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        conn.Open();
                        string connectionString = "SP_ChangeDeliveryId";
                        using (SqlCommand cmd = new SqlCommand(connectionString, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@saleId",saleId);
                            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
                            cmd.Parameters.AddWithValue("@newDeliveryId", newDeliveryId);
                           var result = cmd.ExecuteNonQuery();
                        }
                    }


                }

            }
            catch (Exception ee)
            {
                throw;
            }
        }

    }

}
