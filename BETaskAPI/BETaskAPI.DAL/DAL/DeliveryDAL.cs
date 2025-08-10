using BETaskAPI.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BETask.DAL.DAL;
using System.Data.SqlClient;
using System.Data;

namespace BETaskAPI.DAL.DAL
{
    public class DeliveryDAL
    {
        public enum EnumPaymentModes { Cash, Bank, Credit, Coupon, DO, SalesmanCredit }
        public enum EnumDocuments { SALE, ACCOUNT, PAYMENT, RECIEPT, DO, DOINV, AGREEMENT, DOSALE, PETTY, JOURNAL };//DOSALE for DO Delivery

        readonly ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
        readonly AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
        LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public List<delivery> GetAllDeliveryCustomers(DateTime selectedDate, int userId, string customerName)
        {
            List<delivery> deliveryCustomers = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    //var query = context.delivery
                    //    .Include(d => d.delivery_items)
                    //    .Include(d => d.delivery_items.Select(c => c.customer))
                    //    .Where(x => x.employee_id == userId && x.delivery_date == selectedDate);

                    //deliveryCustomers = context.delivery.Include(d => d.delivery_items.Select(c => c.customer).Where(c=>c.customer_name== customerName || customerName==string.Empty)).Where(d=>d.delivery_date==selectedDate).ToList(); 
                    deliveryCustomers = context.delivery.Include(d=>d.delivery_items).Include(d => d.delivery_items.Select(c => c.customer)).Where(x => x.employee_id == userId && x.delivery_date == selectedDate).ToList();
                    if (deliveryCustomers != null && deliveryCustomers.Count >= 1)
                    {
                        try
                        {
                            deliveryCustomers = deliveryCustomers.OrderBy(x => x.delivery_items.Select(di => di.delivery_time)).ToList();
                            //deliveryCustomers = deliveryCustomers.OrderByDescending(x => x.delivery_items.Max(di => di.delivery_item_id)).ToList();
                        }
                        catch { }
                    }
                }

            }
            catch(Exception ex)
            {
                throw;
            }

            return deliveryCustomers;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<delivery_items> GetCustomerDeliveryItems(int deliveryId, int customerId)
        {
            List<delivery_items> deliveryItems = null;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    deliveryItems = context.delivery_items.Include(i => i.item).Where(i => i.customer_id == customerId && i.delivery_id == deliveryId && i.delivered_qty>0).ToList();
                }

            }
            catch
            {
                throw;
            }

            return deliveryItems;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public string SaveDailyCollections(daily_collection obj, string couponCode = "")
        {
            string resp = "",postInfo="";
            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            int? routeId = GetRouteByEmployee(obj.employee_id, context);
                            obj.route_id = routeId;

                            obj.division_id = obj.division_id == 0 ? null : obj.division_id;
                            //Saving to daily collection

                            if (obj.delivery_id != null || obj.delivery_id == 0)
                            {
                                List<daily_collection> xCollection = context.daily_collection.Where(x => x.delivery_id == obj.delivery_id && x.customer_id == obj.customer_id).ToList();
                                if (xCollection == null || xCollection.Count == 0)
                                {
                                    obj.status = 4;
                                    context.Entry(obj).State = EntityState.Added;
                                    context.SaveChanges();

                                    postInfo= accountTransactionDAL.PostCollection(context, obj);

                                    if (obj.payment_mode.ToLower() == "coupon" || obj.payment_mode.ToLower() == "credit" || obj.payment_mode.ToLower() == "do")
                                    {

                                        var cust = context.customer.Where(x => x.customer_id == obj.customer_id).FirstOrDefault();

                                        if (obj.delivery_id == 0)
                                        {

                                            if (obj.payment_mode.ToLower() == "coupon")
                                            {

                                                if (cust.wallet_balance == null)
                                                    cust.wallet_balance = 0;

                                                cust.wallet_balance += obj.collected_amount;
                                                context.Entry(cust).State = EntityState.Modified;
                                                context.SaveChanges();

                                            }
                                        }

                                        //Updating wallet balance for all type of credit payment mode if the customer is an wallet customer
                                        if (obj.payment_mode.ToLower() == "credit")
                                        {
                                            if (cust.wallet_number != null && cust.wallet_number.Length > 0)
                                            {
                                                decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                                cust.wallet_balance = walletBalance - obj.collected_amount;
                                                context.Entry(cust).State = EntityState.Modified;
                                                context.SaveChanges();
                                            }


                                        }
                                        if (obj.payment_mode.ToLower() == "do")
                                            UpdateDoLeaf(context, obj);


                                        if (obj.payment_mode.ToLower() == "coupon" && obj.old_leaf_count <= 0 && obj.is_deposit != 1)
                                        {
                                            UpdateCoupon(context, obj, couponCode);
                                        }


                                    }
                                }
                            }
                            //Payment Collection
                            else
                            {
                                postInfo = "From Payment Collection Else part\n";
                                context.Entry(obj).State = EntityState.Added;
                                context.SaveChanges();
                              postInfo+=  accountTransactionDAL.PostCollection(context, obj);

                                var cust = context.customer.Where(x => x.customer_id == obj.customer_id).FirstOrDefault();

                                if (obj.payment_mode.ToLower() == "coupon" && obj.is_deposit != 1)
                                {
                                    if (obj.delivery_id == 0 || obj.delivery_id == null)
                                    {

                                        if (cust.wallet_balance == null)
                                            cust.wallet_balance = 0;

                                        resp = $" Old Balance {cust.wallet_balance}";
                                        cust.wallet_balance = cust.wallet_balance + obj.collected_amount;
                                        context.Entry(cust).State = EntityState.Modified;
                                        context.SaveChanges();
                                        resp = $" ,New Balance {cust.wallet_balance}";
                                    }
                                }

                                //Updating wallet balance for all type of payment modes if the customer is an wallet customer
                                else
                                {
                                    if (!string.IsNullOrEmpty(cust.wallet_number))
                                    {
                                        cust.wallet_balance = cust.wallet_balance + obj.collected_amount;
                                        context.Entry(cust).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                }
                            }

                            List<delivery_items> items = context.delivery_items.Where(d => d.delivery_id == obj.delivery_id & d.customer_id == obj.customer_id).ToList();
                            foreach (delivery_items item in items)
                            {
                                if (item.status != 4)
                                {
                                    item.delivered_qty = item.qty;
                                    item.delivery_time = obj.delivery_time;
                                    item.status = 4;
                                    item.daily_collection_id = obj.daily_collection_id;
                                    item.delivery_leaf = obj.delivery_leaf;
                                    item.division_id = obj.division_id;
                                    context.Entry(item).State = EntityState.Modified;
                                    context.SaveChanges();

                                    delivery_item_summary _Item_Summary = context.delivery_item_summary.Where(x => x.delivery_id == obj.delivery_id && x.item_id == item.item_id).FirstOrDefault();
                                    if (_Item_Summary != null)
                                    {
                                        _Item_Summary.used_qty = _Item_Summary.used_qty > 0 ? (_Item_Summary.used_qty - item.qty) + item.qty : item.qty;
                                        _Item_Summary.balance_qty = _Item_Summary.qty - _Item_Summary.used_qty;
                                        context.Entry(_Item_Summary).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                }
                            }
                            context.SaveChanges();
                            transaction.Commit();
                            UpdateCustomerOutstanding(context, obj.customer_id);
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
            return resp+postInfo;
        }

        public void SaveDailyCollections(daily_collection collection, List<customer_aggrement> customerAgreement, string couponCode = "", decimal rechargetax = 0, decimal qtyNonFoc = 0)
        {
            try
            {

                string postInfo = "";

                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            int? routeId = GetRouteByEmployee(collection.employee_id, context);
                            collection.route_id = routeId;
                            collection.division_id = collection.division_id == 0 ? null : collection.division_id;
                            //Saving to daily collection
                            if (collection.delivery_id != null || collection.delivery_id == 0)
                            {
                                collection.status = 4;
                                context.Entry(collection).State = EntityState.Added;
                                context.SaveChanges();
                                var deliveryItems = AddDelivery(context, collection, customerAgreement, rechargetax);
                                RecheckCollectionAmountWithDelivery(context, collection, deliveryItems.Sum(x => x.net_amount));
                                if (collection.is_deposit != 1)
                                {
                                    UpdateWalletAndCoupons(collection, couponCode, context, qtyNonFoc);
                                    var sale = AddSales(context, deliveryItems, collection);
                                    UpdateSaleIdOnDeliveryItems(context, deliveryItems, sale.sales_id);
                                    AccountPosting(context, sale, collection);
                                    List<sales_item> items = context.sales_item.AsNoTracking().Where(x => x.sales_id == sale.sales_id).ToList();
                                    itemTransactionDAL.SaveItemTransaction_Sales(context, items);
                                }
                                else
                                {
                                     postInfo = accountTransactionDAL.PostCollection(context, collection);
                                }

                            }
                            context.SaveChanges();
                            transaction.Commit();

                            var customer = context.customer.Find(collection.customer_id);
                            int customerLedgerId = Convert.ToInt32(customer.ledger_id);
                            if (customer.group_id > 0)
                            {
                                UpdateCustomerOutstanding(context, customer.group_id);

                            }
                            else
                                UpdateCustomerOutstanding(context, collection.customer_id);
                        }
                        catch (Exception ee)
                        {
                            transaction.Rollback();
                            throw new Exception($"Error on SaveCollection {collection.delivery_id} -{postInfo} - {ee.ToString()}");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private async Task UpdateCustomerOutstanding(betaskdbEntities context, int custmoerId)
        {
            try
            {
               
                using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdateAndGetCustomerOutstanding", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters
                        command.Parameters.AddWithValue("@CustomerId", custmoerId);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch { }
        }

        private void AccountPosting(betaskdbEntities context, sales sale,daily_collection collection)
        {
            try
            {
                if (sale.payment_mode.ToUpper() != EnumPaymentModes.DO.ToString() && collection.is_deposit!=1)
                {
                    var customer = context.customer.Find(sale.customer_id);
                    int customerLedgerId =Convert.ToInt32(customer.ledger_id);
                    if (customer.group_id > 0)
                    {
                        var parentCustomer = context.customer.Find(customer.group_id);
                        customerLedgerId= Convert.ToInt32(parentCustomer.ledger_id);
                    }
                    int bankId = 0;
                    if (sale.payment_mode == EnumPaymentModes.Bank.ToString())
                    {
                        bankId = Convert.ToInt32(context.ledger_mapping.FirstOrDefault(x => x.ledger_type == "BANKSALE").ledger_id);
                    }

                    if (sale.net_amount != (sale.gross_amount + sale.total_vat))
                    {
                        decimal diff = (sale.net_amount - (sale.gross_amount + sale.total_vat));
                        sale.gross_amount += diff;
                    }

                    if (sale.payment_mode.ToLower() == "coupon" && sale.old_leaf_count > 0)
                    {
                            //Deposit ledger credit here
                            ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.COUPONBOOKLIABILITY);
                            if (ledgerDeposit != null)
                                customerLedgerId = Convert.ToInt32(ledgerDeposit.ledger_id);
                    }

                    Model.SaleAccountPostModel saleAccount = new Model.SaleAccountPostModel
                    {
                        SalesAmount = sale.gross_amount,
                        DiscountAllowedAmount = 0,
                        RoundOffAmount = 0,
                        VatOnSaleAmount = sale.total_vat,
                        CreditPSaleAmount = sale.net_amount,
                        CreditSaleLedger = customerLedgerId,
                        CashSaleAmount = sale.payment_mode == EnumPaymentModes.Cash.ToString() ? sale.net_amount : 0,
                        BankSaleAmount = sale.payment_mode == EnumPaymentModes.Bank.ToString() ? sale.net_amount : 0,
                        BankSaleLedger = sale.payment_mode == EnumPaymentModes.Bank.ToString() ? bankId : 0,

                    };


                    if (sale.payment_mode.ToUpper() != EnumPaymentModes.DO.ToString())
                    {
                        int defaultItemId = context.system_settings.FirstOrDefault().default_item_id;

                        List<account_transaction> listMultpleSaleAccount = GetSaleLedgers_AccountPosting(sale.sales_item.ToList(), context);
                        if (sale.payment_mode == EnumPaymentModes.Cash.ToString() || sale.payment_mode == EnumPaymentModes.Bank.ToString())
                            accountTransactionDAL.PostCashBankSale(context, saleAccount, sale, listMultpleSaleAccount);
                        else if (sale.sales_item.Count() == 1 && sale.sales_item.FirstOrDefault().item_id != defaultItemId && sale.payment_mode== EnumPaymentModes.Coupon.ToString())
                            accountTransactionDAL.PostCashBankSale(context, saleAccount, sale, listMultpleSaleAccount);
                        else
                            accountTransactionDAL.PostSale(context, saleAccount, sale, listMultpleSaleAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<account_transaction> GetSaleLedgers_AccountPosting(List<sales_item> listItems, betaskdbEntities context)
        {
            List<account_ledger> listSalesLedger = new List<account_ledger>();
            List<account_transaction> listTransaction = new List<account_transaction>();
            try
            {

                int dafaultSale = Convert.ToInt32(context.ledger_mapping.AsNoTracking().FirstOrDefault(x => x.status == 1 && x.ledger_type == "SALE" && x.status == 1).ledger_id);
                if (dafaultSale >0)
                {
                    account_ledger ledger = context.account_ledger.AsNoTracking().Where(x => x.ledger_id == dafaultSale && x.status == 1).FirstOrDefault();
                    if (ledger != null)
                    {
                        listSalesLedger.Add(ledger);

                        listTransaction.Add(new account_transaction
                        {
                            ledger_id = ledger.ledger_id,
                            debit = 0,
                            credit = 0,
                        });
                    }
                }

                List<int> _listItemIds = listItems.Select(x => x.item_id).Distinct().ToList();
                var _lisSalesLedgerIds = context.item.AsNoTracking().Where(x => _listItemIds.Contains(x.item_id) && x.sale_ledger != null).Select(x => x.sale_ledger).Distinct().ToList();
                if (_lisSalesLedgerIds != null && _lisSalesLedgerIds.Count > 0)
                {
                    bool multipleSale = false;
                    foreach (int _id in _lisSalesLedgerIds)
                    {
                        if (_id != dafaultSale)
                        {
                            account_ledger ledger = context.account_ledger.AsNoTracking().Where(x => x.ledger_id == _id).FirstOrDefault();
                            if (ledger != null)
                            {
                                multipleSale = true;
                                listSalesLedger.Add(ledger);//Sale account

                                listTransaction.Add(new account_transaction
                                {
                                    ledger_id = ledger.ledger_id,
                                    debit = 0,
                                    credit = 0,
                                });
                            }
                        }
                    }
                    if (!multipleSale)
                    {
                        return null;
                    }
                }
                else
                    return null;



                //Setting Transaction
                foreach (sales_item sl in listItems)
                {
                    decimal amount = sl.gross_amount;
                    item it = context.item.AsNoTracking().Where(x => x.item_id == sl.item_id).FirstOrDefault();

                    //Default sale account
                    if (it.sale_ledger == null)
                    {
                        listTransaction[0].credit += amount;
                    }
                    //setting accounts other than default sale account
                    else
                    {
                        foreach (account_transaction at in listTransaction)
                        {
                            if (it.sale_ledger != null && at.ledger_id == it.sale_ledger)
                            {
                                at.credit += amount;
                            }
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            return listTransaction;
        }


        private void UpdateSaleIdOnDeliveryItems(betaskdbEntities context, List<delivery_items> deliveryItems, long saleId)
        {
            try
            {
                deliveryItems.ForEach(dl =>
                {
                    dl.sales_id = saleId;
                    context.Entry(dl).State = EntityState.Modified;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on updating UpdateSaleIdOnDeliveryItems deliveryId:{deliveryItems.FirstOrDefault().delivery_id} , saleId:{saleId} ");
            }
        }

        private void UpdateWalletAndCoupons(daily_collection obj, string couponCode, betaskdbEntities context, decimal qtyNonFoc = 0)
        {
            if (obj.payment_mode.ToLower() == "coupon" || obj.payment_mode.ToLower() == "credit" || obj.payment_mode.ToLower() == "do")
            {
                var cust = context.customer.Find(obj.customer_id);
                if (obj.delivery_id != 0 && obj.is_deposit != 1)
                {
                    if (obj.payment_mode.ToLower() == "coupon" && obj.old_leaf_count <= 0)
                    {
                        if (cust.group_id > 0)
                        {
                            var parentCust = context.customer.Find(cust.group_id);
                            UpdateWalletBalance(obj, context, parentCust);
                            UpdateCoupon_DummyLeaf(context, cust.group_id, Convert.ToInt32(obj.delivery_id), qtyNonFoc, obj.delivery_time);
                        }
                        else
                        {
                            UpdateWalletBalance(obj, context, cust);
                            UpdateCoupon_DummyLeaf(context, obj.customer_id, Convert.ToInt32(obj.delivery_id), qtyNonFoc, obj.delivery_time);
                        }
                        UpdateCoupon(context, obj, couponCode);

                    }

                    else if (obj.payment_mode.ToLower() == "credit" && obj.old_leaf_count <= 0)
                    {
                        if (!string.IsNullOrEmpty(cust.wallet_number) && cust.wallet_number.Length > 0)
                        {
                            UpdateWalletBalance(obj, context, cust);
                        }
                    }
                    else if (obj.payment_mode.ToLower() == "do")
                        UpdateDoLeaf(context, obj);
                }
            }
        }

        private static void RecheckCollectionAmountWithDelivery(betaskdbEntities context,daily_collection collection,  decimal totalDeliveryAmount)
        {
            if (collection.collected_amount != totalDeliveryAmount)
            {
                collection.collected_amount = totalDeliveryAmount;
                context.daily_collection.Attach(collection);
                context.Entry(collection).Property(x => x.collected_amount).IsModified = true;
            }
        }

        private static void UpdateWalletBalance(daily_collection obj, betaskdbEntities context, customer cust)
        {
            cust.wallet_balance = cust.wallet_balance - obj.collected_amount;
            cust.outstanding_amount = Convert.ToDecimal(cust.wallet_balance);
            context.Entry(cust).State = EntityState.Modified;
            context.SaveChanges();
        }

        private List<delivery_items> AddDelivery(betaskdbEntities context,daily_collection collection, List<customer_aggrement> customerAgreement, decimal rechargetax)
        {

            if (customerAgreement != null && customerAgreement.Count > 0)
            {
                var deliveryItems = AddDeliveryItems(context, collection, rechargetax, customerAgreement);

                AddDeliveryItemSummary(context, Convert.ToInt32(collection.delivery_id), customerAgreement);

                UpdateDelivery(context, collection, deliveryItems);


                return deliveryItems;
            }
            else
            {
                throw new Exception("No items");
            }
        }


        private static void UpdateDelivery(betaskdbEntities context, daily_collection obj, List<delivery_items> delivery_Items)
        {
            decimal grossAnount = delivery_Items.Sum(x => x.gross_amount);
            delivery _delivery = context.delivery.FirstOrDefault(x => x.delivery_id == obj.delivery_id);
            int customerCount = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == obj.delivery_id).ToList().Select(x => x.customer_id).Distinct().Count();
            _delivery.customer_count = customerCount;
            _delivery.gross_amount += grossAnount;
            _delivery.total_discount = _delivery.total_discount + 0;
            _delivery.total_beforevat = _delivery.total_beforevat + grossAnount;
            _delivery.total_vat = _delivery.total_vat + delivery_Items.Sum(x => x.vat_amount); ;
            _delivery.net_amount = _delivery.net_amount + Math.Round(grossAnount + delivery_Items.Sum(x => x.vat_amount), Constants.DECIMAL_PLACES);
            _delivery.remarks = $"Last updated on {obj.delivery_time}";
            context.Entry(_delivery).State = EntityState.Modified;
        }

        private static void AddDeliveryItemSummary(betaskdbEntities context, int deliveryId,  List<customer_aggrement> customerAgreement)
        {
            List<delivery_item_summary> listItemSummary = new List<delivery_item_summary>();
            foreach (customer_aggrement ci in customerAgreement)
            {
                delivery_item_summary _Item_Summary = context.delivery_item_summary.Where(x => x.delivery_id == deliveryId && x.item_id == ci.item_id).FirstOrDefault();

                //Update
                if (_Item_Summary != null)
                {
                    _Item_Summary.used_qty = (_Item_Summary.used_qty + ci.max_qty);
                    _Item_Summary.qty = (_Item_Summary.qty + ci.max_qty);
                    _Item_Summary.balance_qty = _Item_Summary.qty - _Item_Summary.used_qty;
                    context.Entry(_Item_Summary).State = EntityState.Modified;
                }

                //Insert
                else
                {
                    listItemSummary.Add( new delivery_item_summary
                    {
                        item_id = ci.item_id,
                        delivery_id = deliveryId,
                        qty = ci.max_qty,
                        used_qty = ci.max_qty,
                        balance_qty = 0,
                        status = 1
                    });
                }
            }
            context.delivery_item_summary.AddRange(listItemSummary);

        }

        private static List<delivery_items> AddDeliveryItems(betaskdbEntities context,daily_collection obj, decimal rechargetax, List<customer_aggrement> customerAgreement)
        {
            
            List<delivery_items> deliveryItems = new List<delivery_items>();
            try
            {
                foreach (customer_aggrement ci in customerAgreement)
                {
                    item it = context.item.AsNoTracking().Include(t => t.tax_setting).Single(x => x.item_id == ci.item_id);
                    customer_aggrement ca = context.customer_aggrement.AsNoTracking().Where(x => x.item_id == ci.item_id && x.customer_id == obj.customer_id && x.status == 1).FirstOrDefault();

                    decimal vatAmount = 0;
                    decimal vatRate = Convert.ToDecimal(it.tax_setting.tax_value);

                    if ((rechargetax > 0 && obj.payment_mode.ToLower() == "coupon") || obj.is_deposit == 1)
                        vatRate = 0;

                    if (vatRate > 0)
                    {
                        vatAmount = Math.Round((ca.unit_price * vatRate / 100) * (ci.max_qty), Constants.DECIMAL_PLACES); // ci.unit_price>0? Math.Round(((ca.unit_price * ci.max_qty) * vatRate) / 100,2):0;
                        vatAmount = Math.Round(vatAmount, Constants.DECIMAL_PLACES);
                    }

                    decimal grossAmount = 0;

                    //If not deposit
                    if (obj.is_deposit != 1)
                        grossAmount = ci.unit_price > 0 ? ci.max_qty * ca.unit_price : 0;
                    //If bottle deposit
                    else
                    {
                        grossAmount = ci.unit_price * ci.max_qty;
                        vatAmount = 0;
                    }

                    decimal netAmount = ci.unit_price == 0 ? 0 : Math.Round(grossAmount + vatAmount, Constants.DECIMAL_PLACES);
                     

                    deliveryItems.Add(new delivery_items
                    {
                        item_id = ci.item_id,
                        delivery_id = Convert.ToInt32(obj.delivery_id),
                        customer_id = obj.customer_id,
                        qty = ci.max_qty,
                        rate = ci.unit_price == 0 ? 0 : ca.unit_price,
                        gross_amount = ci.unit_price == 0 ? 0 : grossAmount,
                        discount = 0,
                        total_beforvat = ci.unit_price == 0 ? 0 : grossAmount,
                        vat_amount = ci.unit_price == 0 ? 0 : vatAmount,
                        net_amount = netAmount,
                        status = 4,
                        delivery_time = obj.delivery_time,
                        delivered_qty = ci.max_qty,
                        daily_collection_id = obj.daily_collection_id,
                        delivery_leaf = obj.delivery_leaf,
                        division_id = obj.division_id,
                        is_deposit = obj.is_deposit,
                        payment_mode = obj.payment_mode,

                    });
                }
                context.delivery_items.AddRange(deliveryItems);
                context.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception($"Error on adding delivery {obj.delivery_id} , customer {obj.customer_id} \n{ex.Message}");
            
            }
            return deliveryItems;
        }

        private sales AddSales(betaskdbEntities context, List<delivery_items> deliveryItems,daily_collection collection)
        {
            try
            {
                DeliveryMapper mapper = new DeliveryMapper();
                string docType = collection.payment_mode == EnumPaymentModes.DO.ToString() ? EnumDocuments.DOSALE.ToString() : EnumDocuments.SALE.ToString();

                string saleNumber = GetNextDocument(context, docType);
                var sale = GetSale(deliveryItems, collection, saleNumber);
                sale.do_number = collection.payment_mode == EnumPaymentModes.DO.ToString() ? GetNextDocument(context, EnumPaymentModes.DO.ToString()) : null;
                List<sales_item> salesItems = mapper.MapToSalesItemList(deliveryItems);
                salesItems.ForEach(s => s.status = 1);
                sale.sales_item = salesItems;
                context.sales.Add(sale);
                context.SaveChanges();
                return sale;
                 
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on adding delivery {deliveryItems.FirstOrDefault().delivery_id} , customer {deliveryItems.FirstOrDefault().customer_id} \n{ex.Message}");
            }
        }

        private static sales GetSale(List<delivery_items> deliveryItems, daily_collection collection,string saleNumber)
        {
           

            return new sales
            {
                sales_id = 0,
                sales_date = collection.delivery_time,
                payment_mode = collection.payment_mode,
                customer_id = collection.customer_id,
                net_amount = collection.collected_amount,
                balance_amount = collection.payment_mode == EnumPaymentModes.Cash.ToString() || collection.payment_mode == EnumPaymentModes.Bank.ToString() || collection.payment_mode == EnumPaymentModes.Coupon.ToString() ? 0 : collection.collected_amount,
                gross_amount = deliveryItems.Sum(x => x.gross_amount),
                total_beforevat = deliveryItems.Sum(x => x.gross_amount),
                total_vat = deliveryItems.Sum(x => x.vat_amount),
                total_discount = 0,
                remarks = $"{collection.remarks}",
                sales_order = collection.delivery_id,
                cash_paid = collection.payment_mode == EnumPaymentModes.Cash.ToString() || collection.payment_mode == EnumPaymentModes.Bank.ToString() || collection.payment_mode == EnumPaymentModes.Coupon.ToString() ? collection.collected_amount : 0,
                roundup = 0,
                status = 1,
                bank_id = 0,
                sales_number = saleNumber,
                collection_id = long.Parse(collection.daily_collection_id.ToString()),
                delivery_leaf = collection.delivery_leaf,
                division_id = collection.division_id,
                old_leaf_count = collection.old_leaf_count,
                route_id = collection.route_id,
                do_number = collection.payment_mode == EnumPaymentModes.DO.ToString() ? saleNumber : null
            };
        }

        public  string GetNextDocument(betaskdbEntities context,string docType )
        {
            int nextSerial = 0;
            string nextSerialWithPrefix = string.Empty;
            try
            {
                // using (var context = new betaskdbEntities())
                {
                    var doc = context.document_serial.Where(x => x.document_type == docType).FirstOrDefault();
                    if (doc != null)
                    {
                        nextSerial = doc.next_number;

                        if (doc.prefix != null)
                            nextSerialWithPrefix = String.Format("{0}{1}", doc.prefix, doc.next_number);

                        else
                            nextSerialWithPrefix = doc.next_number.ToString();

                        doc.next_number++;

                        context.document_serial.Attach(doc);
                        context.Entry(doc).Property(x => x.next_number).IsModified = true;
                    }
                    else
                        throw new Exception("No document serial found");
                    
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return nextSerialWithPrefix;
        }

        private void UpdateCoupon(betaskdbEntities context, daily_collection collection, string couponCode)
        {
            try
            {
                if (couponCode != null && couponCode != "")
                {
                    customer xCustomer = context.customer.AsNoTracking().Where(x => x.customer_id == collection.customer_id).FirstOrDefault();
                    if (xCustomer.wallet_number == couponCode)
                    {
                        return;
                    }
                    string[] leafs = couponCode.Split(',');
                    if (leafs.Length > 0)
                    {
                        foreach (string lf in leafs)
                        {
                            int leaf = 0;
                            leaf = Convert.ToInt32(lf.Replace(",", ""));
                            coupon_leaf xLeaf = context.coupon_leaf.Include(c => c.coupon).Where(x => x.coupon.customer_id == collection.customer_id && x.coupon.status == 1 && x.leaf_number == leaf && x.coupon_id == x.coupon.coupon_id).FirstOrDefault();
                            if (xLeaf != null && xLeaf.coupon != null)
                            {
                                string employee = context.employee.AsNoTracking().Where(x => x.employee_id == collection.employee_id).FirstOrDefault().first_name;
                                xLeaf.status = 4;
                                xLeaf.redeem_date = collection.delivery_time;
                                xLeaf.remarks = $"redeemed for delivery {collection.delivery_id} by {employee}";
                                context.Entry(xLeaf).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        private void UpdateDoLeaf(betaskdbEntities context, daily_collection collection)
        {
            try
            {
                if (!string.IsNullOrEmpty(collection.delivery_leaf))
                {
                    delivery_book leaf = context.delivery_book.FirstOrDefault(x => x.leaf_no == collection.delivery_leaf);
                    if (leaf != null)
                    {
                        leaf.status = 4;
                        leaf.redeemed_date = collection.delivery_time;
                        leaf.customer_id = collection.customer_id;
                        leaf.delivery_id = collection.delivery_id;
                        context.Entry(leaf).State = EntityState.Modified;
                        int result = context.SaveChanges();
                    }
                }
            }
            catch (Exception ee)
            {
                throw new Exception($"Error on  UpdateDoLeaf delivery:{collection.delivery_id} customer:{collection.customer_id}");
            }
        }

        public void UpdateCoupon_DummyLeaf(betaskdbEntities context, int customerId, int deliveryId, decimal qty, DateTime date)
        {
            try
            {
                int takeQty = Convert.ToInt32(qty);
                List<coupon_leaf> xLeaf = context.coupon_leaf.Include(c => c.coupon).Where(x => x.coupon.customer_id == customerId && x.coupon.status == 1 && x.status == 1).Take(takeQty).ToList();
                if (xLeaf.Any())
                {
                    xLeaf.ForEach(lf =>
                    {
                        lf.redeem_date = date;
                        lf.status = 4;
                        lf.remarks = $"Dummy redemption {deliveryId}";
                        context.Entry(lf).State = EntityState.Modified;
                    });
                }
            }
            catch (Exception ee)
            {
                throw new Exception($"Error on  UpdateCoupon_DummyLeaf delivery:{deliveryId} customer:{customerId}");

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnItems"></param>
        public void SaveDeliveryReturn(List<delivery_return> returnItems)
        {
            try
            {
                if (returnItems.Count > 0)
                {
                    using (var context = new betaskdbEntities())
                    {
                        using (DbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                int? routeId = GetRouteByEmployee(returnItems[0].employee_id, context);
                                returnItems.ForEach(returnItem => returnItem.route_id = routeId);
                                context.delivery_return.AddRange(returnItems);
                                itemTransactionDAL.SaveItemTransaction_DeliveryReturn(context, returnItems);
                                UpdateAsseFromReturnList(context, returnItems);
                                context.SaveChanges();
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                }
            }
            catch
            {

                throw;
            }

        }

        private static int? GetRouteByEmployee(int employeeId, betaskdbEntities context)
        {
           
            var routeId = context.employee.FirstOrDefault(x => x.employee_id == employeeId).route_id;
            return routeId;
        }

        public void UpdateAsseFromReturnList(betaskdbEntities context, List<delivery_return> dls)
        {
            try
            {

                foreach (var dl in dls)
                {
                    if (dl.return_type == 2)
                    {
                        //  delivery _delivery = _context.delivery.FirstOrDefault(x => x.delivery_id == dl.delivery_id);
                        customer_asset cs = context.customer_asset.AsNoTracking().FirstOrDefault(x => x.customer_id == dl.customer_id && x.delivery_type.ToLower() == "delivery" && x.status == 1);
                        string docNo = "";
                        if (cs != null && !string.IsNullOrEmpty(cs.agreement_no))
                            docNo = cs.agreement_no;
                        else
                        {
                            docNo = GetNextDocument(context, EnumDocuments.AGREEMENT.ToString());
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
                        context.customer_asset.Add(asset);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GetTodayDeliveryId(int employeeId, DateTime deliveryDate)
        {
            int deliveryId = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    delivery delivery = context.delivery.AsNoTracking().FirstOrDefault(x => x.employee_id == employeeId && x.delivery_date == deliveryDate && (x.status==1 || x.status == 3));
                    if (delivery != null)
                        deliveryId = delivery.delivery_id;

                }
            }
            catch
            {

                throw;
            }
            return deliveryId;

        }

        public decimal GetLastDeliveryQty(int itemId, int customerId)
        {
            decimal qty = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    DateTime today = DateTime.Now.AddHours(-4);
                    today = DateTime.Parse(today.ToString("yyyy/MM/dd"));

                    int lastDeliveryId = context.delivery_items.Where(x => x.customer_id == customerId && x.item_id == itemId && x.status == 4 && x.delivery_time > today).Max(x => x.delivery_id);

                    if (lastDeliveryId > 0)
                    {
                        //qty = context.delivery_items.Where(x => x.customer_id == customerId && x.item_id == itemId && x.status == 4 && x.delivery_id == lastDeliveryId).OrderByDescending(x=>x.delivery_item_id).First().delivered_qty;
                        qty = context.delivery_items.Where(x => x.customer_id == customerId && x.item_id == itemId && x.status == 4 && x.delivery_id == lastDeliveryId).OrderByDescending(x => x.delivery_item_id).Sum(x => x.delivered_qty);
                        decimal rQty = context.delivery_return.AsNoTracking().Where(x => x.return_date == today && x.item_id == itemId && x.customer_id == customerId).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                        qty -= rQty;
                    }
                }

            }
            catch (Exception ex)
            {
                qty = 0;
                //throw;
            }
            return qty;
        }
        public int ValidateDeliveryLeaf(string leafNo, int employeeId)
        {
            int resp = 2;
            try
            {

                using (var context = new betaskdbEntities())
                {
                    int routeId = Convert.ToInt32(context.employee.AsNoTracking().FirstOrDefault(x => x.employee_id == employeeId).route_id);
                    //  EDMX.delivery_book leaf = context.delivery_book.AsNoTracking().FirstOrDefault(x => x.leaf_no == leafNo &&x.employee_id==employeeId);
                    EDMX.delivery_book leaf = context.delivery_book.AsNoTracking().FirstOrDefault(x => x.leaf_no == leafNo && x.route_id == routeId);
                    if (leaf != null)
                    {
                        resp = leaf.status;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;
        }

        public bool IsDeliveryEnabled(int employeeId,DateTime date)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var delivery = context.delivery.FirstOrDefault(x => x.employee_id == employeeId && x.delivery_date == date);
                    if (delivery == null)
                        return false;
                    else if (delivery.status != 3 && delivery.status != 1)
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

    }
}
