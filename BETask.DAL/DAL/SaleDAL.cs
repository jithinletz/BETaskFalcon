using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace BETask.DAL.DAL
{
    public class SaleDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// 
        public enum EnumPaymentModes { CASH, BANK, CREDIT, COUPON, DO, SALEMENCREDIT }

        public long SaveSale(EDMX.sales _sale, List<EDMX.sales_item> items, SaleAccountPostModel saleAccountPostModel)
        {

            long _saleId = 0;
            try
            {
                if (_sale.division_id == 0)
                    _sale.division_id = null;
                if (_sale.route_id == 0)
                    _sale.route_id = null;

                string saleNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.SALE.ToString());
                if (_sale.payment_mode.ToString() == EnumPaymentModes.DO.ToString())
                {
                    saleNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DOSALE.ToString());
                }
                string doNumber = string.Empty;
                CouponDAL coupon = new CouponDAL();
                using (var context = new betaskdbEntities())
                {

                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            var _days = (_sale.sales_date - DateTime.Now).Days;
                            int allowedDate = context.system_settings.AsNoTracking().FirstOrDefault(x => x.status == 1).allowed_backdate * -1;
                            if (_days < allowedDate)
                            {
                                PrivilegeDAL privileges = new PrivilegeDAL();
                                if (!privileges.IsPriviligeProvided(Constants.UserId, PrivilegeDAL.Privileges.AllowBackDate, context))
                                    throw new Exception("Backdate entry update is not allowed");
                            }

                            if (_sale.sales_id == 0)
                            {
                                if (_sale.payment_mode.ToLower() == "do")
                                {
                                    doNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DO.ToString());
                                    _sale.do_number = doNumber;
                                    coupon.RedeemDeliveryLeafBySale(_sale, context);
                                }

                                if (_sale.sales_order == 0)
                                    _sale.sales_order = null;
                                _sale.sales_item = items;
                                _sale.sales_number = saleNumber;
                                context.Entry(_sale).State = EntityState.Added;
                                context.SaveChanges();
                                _saleId = _sale.sales_id;

                                //Update Document Next Serial
                                if (_saleId > 0)
                                {
                                    try
                                    {
                                        document_serial doc = context.document_serial.Where(docType => docType.document_type == "SALE").FirstOrDefault();
                                        if (_sale.payment_mode == EnumPaymentModes.DO.ToString())
                                            doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DOSALE.ToString()).FirstOrDefault();

                                        doc.next_number = doc.next_number + 1;
                                        context.Entry(doc).State = EntityState.Modified;
                                        context.SaveChanges();

                                        if (_sale.payment_mode.ToLower() == "do")
                                        {
                                            doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DO.ToString()).FirstOrDefault();
                                            doc.next_number = doc.next_number + 1;
                                            context.Entry(doc).State = EntityState.Modified;
                                            context.SaveChanges();
                                            coupon.RedeemDeliveryLeafBySale(_sale, context);

                                        }

                                    }
                                    catch (Exception ee)
                                    {
                                        //checking Unique number
                                    }
                                }

                                int deliveryId = 0;
                                int.TryParse(Convert.ToString(_sale.sales_order), out deliveryId);

                                //Updating Wallet
                                if (_sale.payment_mode.ToLower() == "coupon")
                                {
                                    if (_sale.old_leaf_count <= 0)
                                    {
                                        var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                        decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                        cust.wallet_balance = walletBalance - _sale.net_amount;
                                        context.Entry(cust).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                }

                                //Updating Delivery
                                if (deliveryId > 0)
                                {

                                    var deliveryItems = context.delivery_items.AsNoTracking().Where(delivery => delivery.delivery_id == deliveryId && delivery.customer_id == _sale.customer_id).ToList();

                                    foreach (delivery_items it in deliveryItems)
                                    {
                                        it.sales_id = _saleId;
                                        context.Entry(it).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }

                                    //Updating Daily Collection
                                    decimal netDeliveryAmount = deliveryItems.Sum(x => x.net_amount);
                                    int employeeId = context.delivery.AsNoTracking().Where(x => x.delivery_id == deliveryId).FirstOrDefault().employee_id;
                                    daily_collection _Collection = new daily_collection
                                    {
                                        delivery_id = deliveryId,
                                        customer_id = _sale.customer_id,
                                        net_amount = netDeliveryAmount,
                                        collected_amount = _sale.net_amount,
                                        payment_mode = _sale.payment_mode,
                                        employee_id = employeeId,
                                        delivery_time = (DateTime)deliveryItems[0].delivery_time,
                                        remarks = "Saved through sales by importing delivery",
                                        status = 4
                                    };
                                    context.Entry(_Collection).State = EntityState.Added;
                                    context.SaveChanges();
                                }
                            }
                            else
                            {
                                _saleId = _sale.sales_id;
                                //Updating Wallet
                                var existSale = context.sales.AsNoTracking().Where(x => x.sales_id == _saleId).FirstOrDefault();
                                /*
                                if (existSale.payment_mode.ToLower() == "coupon" && existSale.old_leaf_count<=0)
                                {
                                    var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                    decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                    cust.wallet_balance = walletBalance + existSale.net_amount;
                                    context.Entry(cust).State = EntityState.Modified;
                                    context.SaveChanges();
                                }*/

                                if (_sale.payment_mode.ToLower() == "coupon" && _sale.old_leaf_count <= 0)
                                {
                                    var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                    decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);

                                    if (_sale.payment_mode.ToLower() == "coupon" && existSale.old_leaf_count <= 0)
                                        cust.wallet_balance = walletBalance + existSale.net_amount;

                                    cust.wallet_balance = walletBalance - _sale.net_amount;
                                    context.Entry(cust).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                else if (_sale.payment_mode.ToLower() == "coupon" && _sale.old_leaf_count > 0)
                                {

                                    //Deposit ledger credit here
                                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                                    ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.COUPONBOOKLIABILITY);
                                    if (ledgerDeposit != null)
                                        saleAccountPostModel.CreditSaleLedger = Convert.ToInt32(ledgerDeposit.ledger_id);

                                }

                                if (existSale.payment_mode.ToLower() == "do")
                                {
                                    if (_sale.payment_mode.ToLower() == "do")
                                    {
                                        _sale.do_number = existSale.do_number;
                                        _sale.do_id = existSale.do_id;
                                    }
                                }


                                _sale.sales_order = _sale.sales_order == 0 ? null : _sale.sales_order;
                                _sale.collection_id = existSale.collection_id;
                                _sale.do_number = existSale.do_number;
                                _sale.do_id = existSale.do_id;
                                context.Entry(_sale).State = EntityState.Modified;
                                context.SaveChanges();


                                // Remove all the purchase items
                                List<EDMX.sales_item> sitems = context.sales_item.Where(p => p.sales_id == _sale.sales_id).ToList();
                                if (sitems != null && sitems.Count > 0)
                                {
                                    context.sales_item.RemoveRange(sitems);
                                    context.SaveChanges();
                                }
                                foreach (EDMX.sales_item item in items)
                                {
                                    context.Entry(item).State = EntityState.Added;
                                }
                                context.SaveChanges();

                                //Daily Collection Update
                                int collId = Convert.ToInt32(context.sales.Find(_sale.sales_id).collection_id);

                                daily_collection collection = context.daily_collection.FirstOrDefault(x => x.daily_collection_id == _sale.collection_id && x.delivery_id == _sale.sales_order);

                                if (collection != null)
                                {
                                    collection.payment_mode = _sale.payment_mode;
                                    collection.collected_amount = _sale.net_amount;
                                    collection.old_leaf_count = _sale.old_leaf_count;
                                    collection.delivery_leaf = _sale.delivery_leaf;

                                    context.Entry(collection).State = EntityState.Modified;
                                    context.SaveChanges();

                                }

                            }

                            SynchronizationDAL synchronizationDAL = new SynchronizationDAL(context);
                            decimal walletBal = 0;
                            synchronizationDAL.UpdateWalletAsOutstanding(_sale.customer_id, ref walletBal);

                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            itemTransactionDAL.SaveItemTransaction_Sales(items, context);
                            AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();

                            List<account_transaction> listMultpleSaleAccount = GetSaleLedgers_AccountPosting(items, context);
                            if (_sale.payment_mode.ToLower() == "cash" || _sale.payment_mode.ToLower() == "bank")
                                accountTransactionDAL.PostCashBankSale(saleAccountPostModel, _sale, context, listMultpleSaleAccount);
                            else
                                accountTransactionDAL.PostSale(saleAccountPostModel, _sale, context, listMultpleSaleAccount);

                            if (_sale.payment_mode.ToLower() == EnumPaymentModes.COUPON.ToString().ToLower())
                            {
                                DAL.SynchronizationDAL sync = new SynchronizationDAL(context);
                                decimal newBal = 0;
                                sync.UpdateWalletAsOutstanding(_sale.customer_id, ref newBal);
                            }

                            transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            if (transaction != null)
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
            return _saleId;
        }

        /// <summary>
        /// To update from delivery
        /// </summary>
        /// <param name="_sale"></param>
        /// <param name="items"></param>
        /// <param name="saleAccountPostModel"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public long SaveSale(EDMX.sales _sale, EDMX.sales_item item, SaleAccountPostModel saleAccountPostModel, betaskdbEntities context)
        {

            long _saleId = 0;
            try
            {
                if (_sale.division_id == 0)
                    _sale.division_id = null;
                if (_sale.route_id == 0)
                    _sale.route_id = null;

                //   string saleNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.SALE.ToString());
                //  string doNumber = string.Empty;
                CouponDAL coupon = new CouponDAL();
                //using (var context = new betaskdbEntities())
                {

                    // using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            _saleId = _sale.sales_id;
                            //Updating Wallet
                            var existSale = context.sales.AsNoTracking().Where(x => x.sales_id == _saleId).FirstOrDefault();
                            if (existSale.payment_mode.ToLower() == "coupon")
                            {
                                var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                cust.wallet_balance = walletBalance + existSale.net_amount;
                                context.Entry(cust).State = EntityState.Modified;
                                context.SaveChanges();
                            }

                            if (_sale.payment_mode.ToLower() == "coupon")
                            {
                                var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                cust.wallet_balance = walletBalance - _sale.net_amount;
                                context.Entry(cust).State = EntityState.Modified;
                                context.SaveChanges();
                            }

                            if (existSale.payment_mode.ToLower() == "do")
                            {
                                if (_sale.payment_mode.ToLower() == "do")
                                {
                                    _sale.do_number = existSale.do_number;
                                    _sale.do_id = existSale.do_id;
                                }
                            }


                            _sale.sales_order = _sale.sales_order == 0 ? null : _sale.sales_order;

                            context.Entry(_sale).State = EntityState.Modified;
                            context.SaveChanges();


                            // Remove all the purchase items

                            if (item != null)
                            {
                                context.Entry(item).State = EntityState.Modified;
                                context.SaveChanges();
                            }

                            daily_collection collection = context.daily_collection.FirstOrDefault(x => x.daily_collection_id == _sale.collection_id);
                            if (collection != null)
                            {
                                collection.collected_amount = _sale.net_amount;
                                context.Entry(collection).State = EntityState.Modified;
                                context.SaveChanges();
                            }




                            ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                            List<sales_item> items = context.sales_item.Where(x => x.sales_id == _sale.sales_id).ToList();
                            itemTransactionDAL.SaveItemTransaction_Sales(items, context);
                            AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();



                            List<account_transaction> listMultpleSaleAccount = GetSaleLedgers_AccountPosting(items, context);
                            if (_sale.payment_mode.ToLower() == "cash" || _sale.payment_mode.ToLower() == "bank")
                                accountTransactionDAL.PostCashBankSale(saleAccountPostModel, _sale, context, listMultpleSaleAccount);
                            else
                                accountTransactionDAL.PostSale(saleAccountPostModel, _sale, context, listMultpleSaleAccount);
                            //transaction.Commit();
                        }
                        catch (Exception ee)
                        {
                            throw;
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
            return _saleId;
        }
        /// <summary>
        /// Saving DO Sale
        /// </summary>
        /// <param name="sale"></param>
        /// <param name="context"></param>
        public long SaveSale(EDMX.sales sale, betaskdbEntities context)
        {
            long saleId = 0;
            try
            {
                string doNumber = string.Empty;
                // string saleNumber = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DOINV.ToString());
                // sale.sales_number = saleNumber;
                if (sale.payment_mode == SaleDAL.EnumPaymentModes.DO.ToString())
                {
                    DocumentSerialDAL documentSerialDAL = new DocumentSerialDAL();
                    doNumber = documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DO.ToString(), context);
                    sale.do_number = doNumber;
                }
                int customerLedgerId = Convert.ToInt32(context.customer.FirstOrDefault(x => x.customer_id == sale.customer_id).ledger_id);
                if (context.customer.Any(x => x.customer_id == sale.customer_id && x.group_id > 0))
                {
                    int parent = context.customer.Find(sale.customer_id).group_id;
                    if (parent > 0)
                    {
                        int groupLedgerId =Convert.ToInt32( context.customer.Find(parent).ledger_id);
                        customerLedgerId = groupLedgerId;
                    }
                }
                BETask.DAL.Model.SaleAccountPostModel saleAccount = new BETask.DAL.Model.SaleAccountPostModel
                {
                    //SalesAmount = listDailyCollection.Sum(x => x.net_amount),
                    SalesAmount = sale.gross_amount,
                    DiscountAllowedAmount = 0,
                    RoundOffAmount = 0,
                    VatOnSaleAmount = sale.total_vat,
                    CreditPSaleAmount = sale.net_amount,
                    CreditSaleLedger = customerLedgerId,
                    CashSaleAmount = 0,
                    BankSaleAmount = 0,
                    BankSaleLedger = 0,

                };

                context.Entry(sale).State = EntityState.Added;
                context.SaveChanges();
                saleId = sale.sales_id;

                string _docType = sale.payment_mode == SaleDAL.EnumPaymentModes.DO.ToString() ? DocumentSerialDAL.EnumDocuments.DOSALE.ToString() : DocumentSerialDAL.EnumDocuments.SALE.ToString();

                UpdateSaleNextDocument(sale, context, saleId, _docType);


                ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                List<sales_item> items = context.sales_item.Where(x => x.sales_id == sale.sales_id).ToList();
                itemTransactionDAL.SaveItemTransaction_Sales(items, context);
                AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();

                List<account_transaction> listMultpleSaleAccount = GetSaleLedgers_AccountPosting(items, context);
                accountTransactionDAL.PostSale(saleAccount, sale, context, listMultpleSaleAccount);

            }
            catch (DbEntityValidationException ex)
            {

                string err = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        err = ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                throw;
            }
            return saleId;
        }

        public long SaveSaleAuto(EDMX.sales _sale, List<EDMX.sales_item> items, SaleAccountPostModel saleAccountPostModel, betaskdbEntities context)
        {

            long _saleId = 0;
            try
            {
                DocumentSerialDAL documentSerialDAL = new DocumentSerialDAL();
                string _docType = _sale.payment_mode == SaleDAL.EnumPaymentModes.DO.ToString() ? DocumentSerialDAL.EnumDocuments.DOSALE.ToString() : DocumentSerialDAL.EnumDocuments.SALE.ToString();

                string saleNumber = documentSerialDAL.GetNextDocument(_docType, context);
                string doNumber = string.Empty;
                // using (var context = new betaskdbEntities())
                {
                    //   using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            if (_sale.sales_id == 0)
                            {
                                if (_sale.sales_order == 0)
                                    _sale.sales_order = null;

                                //Do invoicenumber generation and unique check
                                if (_sale.payment_mode == SaleDAL.EnumPaymentModes.DO.ToString())
                                {
                                    docheck:
                                    doNumber = documentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DO.ToString(), context);
                                    _sale.do_number = doNumber;


                                    DateTime dt = DateTime.Now.AddDays(-2);

                                    if (context.sales.AsNoTracking().Any(x => x.do_number == doNumber && x.sales_date >= dt))
                                    {
                                        document_serial _doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DO.ToString()).FirstOrDefault();

                                        _doc.next_number = _doc.next_number + 1;
                                        context.Entry(_doc).State = EntityState.Modified;
                                        context.SaveChanges();
                                        goto docheck;
                                    }
                                }

                                //
                                //Checking duplication but multiple items with same delivery id tobe checked
                                //
                                try
                                {
                                    if (_sale.sales_order != null)
                                    {
                                        DateTime dt = _sale.sales_date.AddDays(-1);
                                        sales xSale = context.sales.Where(x => x.sales_order == _sale.sales_order && x.customer_id == _sale.customer_id && x.status == 1 && x.sales_date >= dt).FirstOrDefault();

                                        if (context.delivery_items.Where(x => x.delivery_id == _sale.sales_order && x.customer_id == _sale.customer_id && x.status == 4 && x.delivery_time != _sale.sales_date).ToList().Count == 0)
                                        {
                                            if (xSale != null && (xSale.sales_date.Minute == _sale.sales_date.Minute && xSale.sales_date.Second == _sale.sales_date.Second) && xSale.payment_mode == _sale.payment_mode && xSale.collection_id == _sale.collection_id && _sale.net_amount == xSale.net_amount)
                                                return 0;
                                        }
                                    }
                                }
                                catch (Exception ex) { }
                                //-------------------------------------------------------------------------------
                                //-------------------------------------------------------------------------------

                                _sale.sales_item = items;
                                _sale.sales_number = saleNumber;
                                context.Entry(_sale).State = EntityState.Added;
                                context.SaveChanges();
                                _saleId = _sale.sales_id;

                                //Update Document Next Serial
                                UpdateSaleNextDocument(_sale, context, _saleId, _docType);

                                int deliveryId = 0;
                                int.TryParse(Convert.ToString(_sale.sales_order), out deliveryId);

                                //Updating Wallet
                                if (_sale.payment_mode.ToLower() == "coupon" || _sale.payment_mode.ToLower() == "credit")
                                {
                                    if (_sale.old_leaf_count <= 0)
                                    {
                                        var cust = context.customer.Where(customer => customer.customer_id == _sale.customer_id).FirstOrDefault();
                                        if (_sale.payment_mode.ToLower() == "coupon")
                                        {
                                            decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                            cust.wallet_balance = walletBalance - _sale.net_amount;
                                            context.Entry(cust).State = EntityState.Modified;
                                            context.SaveChanges();
                                        }
                                        //coupon balance will be - for credit sale of wallet saved customers
                                        else
                                        {
                                            //if (cust.wallet_number != null && cust.wallet_number.Length > 0)
                                            if (!string.IsNullOrEmpty(cust.wallet_number) && cust.wallet_number.Length > 0)
                                            {
                                                decimal walletBalance = Convert.ToDecimal(cust.wallet_balance);
                                                cust.wallet_balance = walletBalance - _sale.net_amount;
                                                context.Entry(cust).State = EntityState.Modified;
                                                context.SaveChanges();
                                            }
                                        }
                                    }
                                    if (_sale.payment_mode.ToLower() == "coupon" && _sale.old_leaf_count > 0)
                                    {

                                        //Deposit ledger credit here
                                        LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                                        ledger_mapping ledgerDeposit = ledgerMappingDAL.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.COUPONBOOKLIABILITY);
                                        if (ledgerDeposit != null)
                                            saleAccountPostModel.CreditSaleLedger = Convert.ToInt32(ledgerDeposit.ledger_id);

                                    }
                                }

                                //Updating Delivery salesdate and id
                                if (deliveryId > 0)
                                {
                                   

                                    var deliveryItems = context.delivery_items.Where(delivery => delivery.delivery_id == deliveryId && delivery.customer_id == _sale.customer_id).ToList();

                                    foreach (delivery_items it in deliveryItems)
                                    {
                                        it.daily_collection_id = _sale.collection_id;
                                        it.sales_id = _saleId;
                                        context.Entry(it).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }


                                }
                                ItemTransactionDAL itemTransactionDAL = new ItemTransactionDAL();
                                itemTransactionDAL.SaveItemTransaction_Sales(items, context);

                                if (_sale.payment_mode.ToUpper() != EnumPaymentModes.DO.ToString())
                                {
                                    AccountTransactionDAL accountTransactionDAL = new AccountTransactionDAL();
                                    List<account_transaction> listMultpleSaleAccount = GetSaleLedgers_AccountPosting(items, context);
                                    if (_sale.payment_mode.ToLower() == "cash" || _sale.payment_mode.ToLower() == "bank")
                                        accountTransactionDAL.PostCashBankSale(saleAccountPostModel, _sale, context, listMultpleSaleAccount);
                                    else
                                        accountTransactionDAL.PostSale(saleAccountPostModel, _sale, context, listMultpleSaleAccount);
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
            catch
            {
                throw;
            }
            return _saleId;
        }

        private static void UpdateSaleNextDocument(sales _sale, betaskdbEntities context, long _saleId, string _docType)
        {
            if (_saleId > 0)
            {
                try
                {
                    document_serial doc = context.document_serial.Where(docType => docType.document_type == _docType).FirstOrDefault();
                    doc.next_number = doc.next_number + 1;
                    context.Entry(doc).State = EntityState.Modified;
                    context.SaveChanges();

                    if (_sale.payment_mode.ToLower() == "do")
                    {
                        doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DO.ToString()).FirstOrDefault();
                        doc.next_number = doc.next_number + 1;
                        context.Entry(doc).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                }
                catch (Exception ee)
                {
                    //checking Unique number
                }
            }
        }

        public int SaveSaleDelivery(EDMX.delivery delivery, EDMX.delivery_items dl, int oldLeafCount, betaskdbEntities context)
        {
            int resp = 0;
            try
            {
                // List<int> customerIds = dl.customer_id;
                //foreach (int cid in customerIds)
                {
                    EDMX.customer _customer = context.customer.AsNoTracking().FirstOrDefault(x => x.customer_id == dl.customer_id);
                    decimal cashPaid = 0, netAmount = 0, grossAmount = 0, totalBeforVat = 0, discount = 0, totalVat = 0;
                    netAmount = dl.net_amount;//items.Where(x => x.customer_id == _customer.customer_id).Sum(x => x.net_amount);
                    grossAmount = dl.gross_amount;//items.Where(x => x.customer_id == _customer.customer_id).Sum(x => x.gross_amount);
                    totalBeforVat = dl.total_beforvat;//items.Where(x => x.customer_id == _customer.customer_id).Sum(x => x.total_beforvat);
                    discount = dl.discount;//items.Where(x => x.customer_id == _customer.customer_id).Sum(x => x.discount);
                    totalVat = dl.vat_amount;// items.Where(x => x.customer_id == _customer.customer_id).Sum(x => x.discount);

                    string paymentMode = _customer.payment_mode.ToUpper();
                    if (paymentMode == EnumPaymentModes.COUPON.ToString() || paymentMode == EnumPaymentModes.CASH.ToString())
                        cashPaid = dl.net_amount;//items.Where(x => x.customer_id == _customer.customer_id).Sum(x => x.net_amount);
                    else
                        cashPaid = 0;



                    //Updating Daily Collection
                    //decimal netDeliveryAmount = deliveryItems.Sum(x => x.net_amount);
                    //int employeeId = context.delivery.AsNoTracking().Where(x => x.delivery_id == deliveryId).FirstOrDefault().employee_id;
                    daily_collection _Collection = new daily_collection
                    {
                        delivery_id = dl.delivery_id,
                        customer_id = dl.customer_id,
                        net_amount = dl.net_amount,
                        collected_amount = dl.net_amount,
                        payment_mode = _customer.payment_mode,
                        employee_id = delivery.employee_id,
                        delivery_time = delivery.delivery_date.AddHours(6),
                        route_id = delivery.route_id,
                        remarks = "Saved through from delivery",
                        status = 4,
                        delivery_leaf = dl.delivery_leaf,
                        old_leaf_count = oldLeafCount

                    };
                    context.Entry(_Collection).State = EntityState.Added;
                    context.SaveChanges();



                    EDMX.sales sale = new sales
                    {
                        bank_id = 0,
                        cash_paid = cashPaid,
                        cheque_no = string.Empty,
                        customer_id = _customer.customer_id,
                        balance_amount = netAmount - cashPaid,
                        net_amount = netAmount,
                        gross_amount = grossAmount,
                        sales_date = delivery.delivery_date,
                        //sales_number=sales
                        payment_mode = _customer.payment_mode,
                        roundup = 0,
                        status = 1,
                        total_beforevat = totalBeforVat,
                        total_discount = discount,
                        total_vat = totalVat,
                        sales_order = delivery.delivery_id,
                        delivery_leaf = dl.delivery_leaf,
                        old_leaf_count = oldLeafCount,
                        route_id = delivery.route_id,
                        remarks = "From manual delivery",
                        collection_id = _Collection.daily_collection_id,

                    };
                    List<EDMX.sales_item> listItems = new List<sales_item> { };
                    // foreach (EDMX.delivery_items dl in items)
                    {
                        listItems.Add(new sales_item
                        {
                            discount = dl.discount,
                            gross_amount = dl.gross_amount,
                            item_id = dl.item_id,
                            net_amount = dl.net_amount,
                            qty = dl.qty,
                            rate = dl.rate,
                            status = 1,
                            total_beforvat = dl.total_beforvat,
                            vat_amount = dl.vat_amount,
                            delivery_item_id = dl.delivery_item_id


                        });
                    }
                    LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                    CustomerDAL customer = new CustomerDAL();
                    Model.SaleAccountPostModel saleAccount = new Model.SaleAccountPostModel
                    {
                        SalesAmount = sale.gross_amount,
                        DiscountAllowedAmount = sale.total_discount,
                        RoundOffAmount = sale.roundup,
                        VatOnSaleAmount = sale.total_vat,
                        CreditPSaleAmount = sale.net_amount,
                        CreditSaleLedger = Convert.ToInt32(customer.GetCustomerDetails(_customer.customer_id).ledger_id),
                        CashSaleAmount = _customer.payment_mode.ToLower().Equals("cash") ? sale.net_amount : 0,
                        BankSaleAmount = _customer.payment_mode.ToLower().Equals("bank") ? sale.net_amount : 0,
                        BankSaleLedger = Convert.ToInt32(_customer.payment_mode.ToLower().Equals("bank") ? sale.bank_id : 0),

                    };

                

                    SaveSaleAuto(sale, listItems, saleAccount, context);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;
        }

      

        /// <summary>
        /// Updating sale when after delivery updates from delivery
        /// </summary>
        /// <returns></returns>
        public long UpdateSaleDelivery(EDMX.delivery_items dl, betaskdbEntities context)
        {
            long resp = 0;
            try
            {
                sales _sale = context.sales.Where(x => x.sales_id == dl.sales_id).FirstOrDefault();
                if (_sale != null)
                {
                    sales_item item = context.sales_item.Where(x => x.sales_id == dl.sales_id && x.delivery_item_id == dl.delivery_item_id).FirstOrDefault();
                    if (item != null)
                    {
                        EDMX.customer _customer = context.customer.AsNoTracking().FirstOrDefault(x => x.customer_id == dl.customer_id);

                        _sale.total_beforevat -= item.total_beforvat;
                        _sale.gross_amount -= item.gross_amount;
                        _sale.net_amount -= item.net_amount;
                        _sale.total_vat -= item.vat_amount;
                        _sale.total_discount -= item.discount;
                        if (_sale.payment_mode.ToUpper() == EnumPaymentModes.COUPON.ToString() || _sale.payment_mode.ToUpper() == EnumPaymentModes.CASH.ToString())
                            _sale.cash_paid -= item.net_amount;

                        item.qty = dl.qty;
                        item.rate = dl.rate;
                        item.gross_amount = dl.gross_amount;
                        item.net_amount = dl.net_amount;
                        item.total_beforvat = dl.total_beforvat;
                        item.discount = dl.discount;
                        item.vat_amount = dl.vat_amount;

                        _sale.total_beforevat += dl.total_beforvat;
                        _sale.gross_amount += dl.gross_amount;
                        _sale.net_amount += dl.net_amount;
                        _sale.total_vat += dl.vat_amount;
                        _sale.total_discount += dl.discount;
                        if (_sale.payment_mode.ToUpper() == EnumPaymentModes.COUPON.ToString() || _sale.payment_mode.ToUpper() == EnumPaymentModes.CASH.ToString() || _sale.payment_mode.ToUpper() == EnumPaymentModes.BANK.ToString())
                        {
                            _sale.cash_paid += item.net_amount;
                            _sale.balance_amount = _sale.net_amount - _sale.cash_paid;
                        }
                        else
                        {
                            _sale.cash_paid -= item.net_amount;
                            _sale.balance_amount = _sale.net_amount - _sale.cash_paid;
                        }

                        LedgerMappingDAL ledgerMappingDAL = new LedgerMappingDAL();
                        CustomerDAL customer = new CustomerDAL();
                        Model.SaleAccountPostModel saleAccount = new Model.SaleAccountPostModel
                        {
                            SalesAmount = _sale.gross_amount,
                            DiscountAllowedAmount = _sale.total_discount,
                            RoundOffAmount = _sale.roundup,
                            VatOnSaleAmount = _sale.total_vat,
                            CreditPSaleAmount = _sale.net_amount,
                            CreditSaleLedger = Convert.ToInt32(customer.GetCustomerDetails(_customer.customer_id).ledger_id),
                            CashSaleAmount = _customer.payment_mode.ToLower().Equals("cash") ? _sale.net_amount : 0,
                            BankSaleAmount = _customer.payment_mode.ToLower().Equals("bank") ? _sale.net_amount : 0,
                            BankSaleLedger = Convert.ToInt32(_customer.payment_mode.ToLower().Equals("bank") ? _sale.bank_id : 0),

                        };
                        resp = SaveSale(_sale, item, saleAccount, context);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        public EDMX.sales GetSaleDetails(long saleId)
        {
            EDMX.sales sale = new EDMX.sales();

            try
            {
                using (var context = new betaskdbEntities())
                {

                    sale = context.sales.Include(p => p.sales_item.Select(i => i.item).Select(t => t.tax_setting))
                        .Include(p => p.sales_item.Select(i => i.item).Select(u => u.uom_setting)).Include(p => p.customer)
                        .Include(d => d.customer_division)
                        .Where(p => p.sales_id == saleId)
                        .FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return sale;

        }

        /// <summary>
        /// Getting items sale account and post amounts to corresponding sale accounts
        /// </summary>
        /// <param name="listItems"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private List<account_transaction> GetSaleLedgers_AccountPosting(List<sales_item> listItems, betaskdbEntities context)
        {
            List<account_ledger> listSalesLedger = new List<account_ledger>();
            List<account_transaction> listTransaction = new List<account_transaction>();
            try
            {
                LedgerMappingDAL ledgerMapping = new LedgerMappingDAL();
                ledger_mapping defaultSaleLedger = ledgerMapping.GetLegerMapping(LedgerMappingDAL.EnumLedgerMap.SALE);
                int dafaultSale = 0;
                if (defaultSaleLedger != null)
                {
                    dafaultSale = Convert.ToInt32(defaultSaleLedger.ledger_id);
                    account_ledger ledger = context.account_ledger.Where(x => x.ledger_id == dafaultSale && x.status == 1).FirstOrDefault();
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

        public long GetSaleIdByInvoice(string invoice)
        {
            long sale = 0;

            try
            {
                using (var context = new betaskdbEntities())
                {

                    var _sale = context.sales.Where(p => p.sales_number == invoice)
                          .FirstOrDefault();
                    if (_sale != null)
                        sale = _sale.sales_id;
                }
            }
            catch
            {
                throw;
            }
            return sale;

        }

        public List<sales> SearchSales(DateTime dateFrom, DateTime dateTo, int vendorId)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listSale = context.sales.Include(x => x.customer).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1 && (vendorId > 0 ? x.customer_id == vendorId : x.customer_id > 0)).Take(100).ToList();
                    if (vendorId > 0)
                    {
                        listSale = listSale.Where(x => x.customer_id == vendorId).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listSale;
        }

        public List<sales_item> SearchSaleNoTax(DateTime dateFrom, DateTime dateTo)
        {
            List<sales_item> listSale = new List<sales_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listSale = context.sales_item.Include(x => x.sales.customer).Include(x => x.item).Where(x => x.sales.sales_date >= dateFrom && x.sales.sales_date <= dateTo && x.status == 1 && x.vat_amount == 0 && x.sales.payment_mode == "Coupon").Take(100).ToList();
                }
            }
            catch
            {
                throw;
            }
            return listSale;
        }

        public List<sales> CustomerSalesReport(DateTime dateFrom, DateTime dateTo, int vendorId, string paymentmode, int routeId = 0)
        {
            List<sales> listSale = new List<sales>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listSale = context.sales.Include(x => x.customer).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1 && (routeId > 0 ? x.customer.route_id == routeId : x.customer.route_id > 0) && (vendorId > 0 ? x.customer_id == vendorId : x.customer_id > 0)).ToList();

                    if (paymentmode.ToLower() != "all")
                    {
                        listSale = listSale.Where(x => x.payment_mode.ToLower() == paymentmode.ToLower()).OrderBy(x => x.sales_date).ToList();
                    }

                }
            }
            catch
            {
                throw;
            }
            return listSale;
        }
        public List<sales_item> ItemSalesReport(DateTime dateFrom, DateTime dateTo, int vendorId, int itemId, int routeId = 0, decimal rangeFrom = 0, decimal rangeTo = 0, string paymentMode = "")
        {
            List<sales_item> listSaleItem = new List<sales_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.Database.CommandTimeout = 1500;
                    var commandText = "update sales set route_id=(select route_id from customer where customer_id=sales.customer_id) where route_id is null " +
                                     $" and cast(sales_date as date)>= '{dateFrom.ToString("yyyy-MM-dd")}' and cast(sales_date as date)<= '{dateTo.ToString("yyyy-MM-dd")}'";
                    // var newDateTime = new SqlParameter("@NewDateTime", myDateValue);
                    // var myId = new SqlParameter("@MyId", myIdValue);
                    context.Database.ExecuteSqlCommand(commandText);

                    // listSale = context.sales.Include(x => x.customer).Include(x=>x.sales_item.Select(i => i.item).Select(u => u.uom_setting)).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1).ToList();
                    listSaleItem = context.sales_item.Include(s => s.sales).Include(c => c.sales.customer).Include(i => i.item).Include(p => p.item.uom_setting).Where(s => s.sales.sales_date >= dateFrom && s.sales.sales_date <= dateTo
                     && (itemId > 0 ? s.item_id == itemId : s.item_id > 0 && s.status == 1) &&
                     (routeId > 0 ? s.sales.route_id == routeId : (s.sales.route_id > 0 || s.sales.route_id == null))
                     && (vendorId > 0 ? s.sales.customer_id == vendorId : s.sales.customer_id > 0)
                     && s.status == 1 && s.sales.status == 1 && (paymentMode != "" ? s.sales.payment_mode.ToLower() == paymentMode.ToLower() : s.sales.payment_mode.Length > 0)).OrderBy(x => x.sales.sales_date).ThenBy(x => x.item_id).ToList();

                    if (rangeTo > 0)
                    {
                        listSaleItem = listSaleItem.Where(x => x.rate >= rangeFrom && x.rate <= rangeTo).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listSaleItem;
        }
        public List<SalesItem> ItemSalesReportNew(DateTime dateFrom, DateTime dateTo, int vendorId, int itemId, int routeId = 0, decimal rangeFrom = 0, decimal rangeTo = 0, string paymentMode = "")
        {
            List<SalesItem> salesItems = new List<SalesItem>();

            try
            {

                // Define the SQL connection and command
                using (var context = new betaskdbEntities())
                {

                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_GetSalesItemWiseReport", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the stored procedure
                            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", dateTo);
                            cmd.Parameters.AddWithValue("@VendorId", vendorId);
                            cmd.Parameters.AddWithValue("@ItemId", itemId);
                            cmd.Parameters.AddWithValue("@RouteId", routeId);
                            cmd.Parameters.AddWithValue("@RangeFrom", rangeFrom);
                            cmd.Parameters.AddWithValue("@RangeTo", rangeTo);
                            cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);

                            // Open the connection
                            conn.Open();

                            // Execute the command and read the results
                            DataTable dataTable = new DataTable();

                            // Load the data into the DataTable
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                dataTable.Load(reader);
                            }

                            // Loop through the DataTable rows and add them to the salesItems list
                            foreach (DataRow row in dataTable.Rows)
                            {
                                SalesItem sale = new SalesItem
                                {
                                    CustomerName = row["customer_name"].ToString(),
                                    SalesDate = Convert.ToDateTime(row["sales_date"]),
                                    ItemId = Convert.ToInt32(row["item_id"]),
                                    ItemName = row["item_name"].ToString(),
                                    UOMName = row["uom_name"].ToString(),
                                    Quantity = Convert.ToDecimal(row["qty"]),
                                    Rate = Convert.ToDecimal(row["rate"]),
                                    GrossAmount = Convert.ToDecimal(row["gross_amount"]),
                                    Discount = Convert.ToDecimal(row["discount"]),
                                    TotalBeforeVAT = Convert.ToDecimal(row["total_before_vat"]),
                                    VATAmount = Convert.ToDecimal(row["vat_amount"]),
                                    NetAmount = Convert.ToDecimal(row["net_amount"]),
                                    SalesNumber = row["sales_number"].ToString(),
                                    PaymentMode = row["payment_mode"].ToString()
                                };

                                // Add the sales item to the list
                                salesItems.Add(sale);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return salesItems;
        }

        public List<SP_SalesMonthlyAnalysis_Result> GetSaleMonthlyAnaysis(int year)
        {
            List<SP_SalesMonthlyAnalysis_Result> listMonthlyAnalyse = new List<SP_SalesMonthlyAnalysis_Result>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listMonthlyAnalyse = context.SP_SalesMonthlyAnalysis(year).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listMonthlyAnalyse;
        }

        public List<SP_SalesMonthlyItemAnalysis_Result> GetSaleMonthlyItemAnaysis(int year, int itemId)
        {
            List<SP_SalesMonthlyItemAnalysis_Result> listMonthlyAnalyse = new List<SP_SalesMonthlyItemAnalysis_Result>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listMonthlyAnalyse = context.SP_SalesMonthlyItemAnalysis(year, itemId).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listMonthlyAnalyse;
        }

        public List<sales_item> SearchItemFocReport(DateTime dateFrom, DateTime dateTo, int customerId, int routeId, int itemId)
        {
            List<sales_item> listSales = new List<sales_item>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listSales = context.sales_item.AsNoTracking().Include(i => i.item).Include(s => s.sales).Include(c => c.sales.customer).Include(r => r.sales.customer.route)
                        .Where(x => x.status == 1 && x.sales.sales_date >= dateFrom && x.sales.sales_date <= dateTo && x.rate <= 0 && x.qty > 0).OrderBy(x => x.sales.sales_date).ThenBy(x => x.sales.customer_id).ToList();
                    if (itemId > 0)
                        listSales = listSales.Where(x => x.item_id == itemId).ToList();
                    if (customerId > 0)
                        listSales = listSales.Where(x => x.sales.customer_id == customerId).ToList();
                    if (routeId > 0)
                        listSales = listSales.Where(x => x.sales.customer.route_id == routeId).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listSales;
        }
        public Model.ChartDataModel GetChartData(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            Model.ChartDataModel chartData = new ChartDataModel();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var listSale = context.sales_item.AsNoTracking().Include(s => s.sales).Include(c => c.sales.customer).AsNoTracking().Include(r => r.sales.customer.route).Where(x => x.item_id == itemId && x.sales.sales_date >= dateFrom && x.sales.sales_date <= dateTo).Select(x => new { x.sales.customer.route.route_name, x.qty, x.net_amount, x.sales.payment_mode }).ToList();
                    var sale = listSale.GroupBy(x => x.route_name).Select(g => new { Delivery = g.Sum(x => x.qty), SaleAmount = g.Sum(x => x.net_amount), RouteName = g.Max(x => x.route_name) }).OrderByDescending(x => x.Delivery);
                    var collection = listSale.GroupBy(x => x.payment_mode).Select(g => new { Collection = g.Sum(x => x.net_amount), PaymentMode = g.Max(x => x.payment_mode) }).OrderByDescending(x => x.Collection);
                    List<Model.ChartDelivery> saleList = new List<ChartDelivery> { };
                    foreach (var prop in sale)
                    {
                        saleList.Add(new ChartDelivery
                        {
                            Route = prop.RouteName,
                            Delivery = Convert.ToDecimal(prop.Delivery),
                            Sales = Convert.ToDecimal(prop.SaleAmount),

                        });
                    }

                    List<Model.ChartCollection> collectionList = new List<ChartCollection> { };
                    foreach (var prop in collection)
                    {
                        collectionList.Add(new ChartCollection
                        {
                            PaymentMode = prop.PaymentMode,
                            Amount = prop.Collection

                        });
                    }

                    chartData.listDelivery = saleList;
                    chartData.listCollection = collectionList;

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return chartData;
        }
        public List<Model.SaleDeliveryDiffModel> GetSalesDeliveryDifference(DateTime dateFrom, DateTime dateTo, int itemId)
        {
            List<Model.SaleDeliveryDiffModel> listDiff = new List<SaleDeliveryDiffModel>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var custId = context.delivery_items.Where(x => x.delivery_time >= dateFrom && x.delivery_time <= dateTo && x.item_id == itemId).Select(x => x.customer_id).Distinct().ToList();
                    if (custId.Count > 0)
                    {
                        foreach (int cs in custId)
                        {
                            decimal delivery = 0, sales = 0;
                            List<delivery_items> deliveryItems = context.delivery_items.Where(x => x.delivery_time >= dateFrom && x.delivery_time <= dateTo && x.item_id == itemId && x.customer_id == cs && x.status == 4).ToList();
                            delivery = deliveryItems.Sum(x => x.qty);
                            List<sales> salesList = context.sales.Include(s => s.sales_item).Include(c => c.customer).Include(r => r.customer.route).Where(x => x.sales_date >= dateFrom && x.sales_date <= dateTo && x.status == 1 && x.customer_id == cs).ToList();
                            sales = salesList.Sum(x => x.sales_item.Sum(q => q.qty));
                            if (sales != delivery)
                            {
                                listDiff.Add(new SaleDeliveryDiffModel
                                {
                                    Route = salesList.Select(r => r.customer.route.route_name).FirstOrDefault(),
                                    customerId = salesList.Select(c => c.customer.customer_id).FirstOrDefault(),
                                    CustomerName = salesList.Select(c => c.customer.customer_name).FirstOrDefault(),
                                    Delivery = delivery,
                                    Sales = sales,


                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listDiff;
        }
        public string GetSaleumberByCollection(int collectionId, int deliveryId)
        {
            string saleNumber = string.Empty;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    sales sl = context.sales.AsNoTracking().Where(x => x.sales_order == deliveryId && x.collection_id == collectionId).FirstOrDefault();
                    if (sl != null)
                    {
                        saleNumber = sl.sales_number;
                    }

                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return saleNumber;
        }

        public List<EDMX.customer_division> GetCustomerDivisionFromSale(int customerId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    //  return context.customer_division.Include(s=>s.sales).Where(x => x.customer_id == customerId &&).ToList();
                    var divisions = context.sales.AsNoTracking().Where(x => x.customer_id == customerId && x.sales_date >= dateFrom && x.sales_date <= dateTo && x.division_id != null).Select(x => x.division_id).Distinct().ToList();
                    return context.customer_division.Where(x => divisions.Contains(x.division_id)).ToList();
                }
            }
            catch { throw; }
        }
        public List<EDMX.delivery_items> SaleNotGeneratedDeliveries(DateTime date,int itemId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    var deliveries = context.delivery_items.Include(x => x.delivery).Include(c=>c.customer).Include(e=>e.delivery.employee).Include(i=>i.item).Where(x=>x.delivery.delivery_date==date && x.status==4 && x.delivery_time!=null && x.sales_id==null && (itemId>0?x.item_id==itemId:x.item_id>0) && x.is_deposit!=1).ToList();

                    return deliveries;
                }
            }
            catch { throw; }
        }

        public long  GenerateSaleFromDelivery(int deliveryId, int deliveryItemId, int oldLeaf)
        {
            long saleId = 0;

            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            delivery_items dl = context.delivery_items.FirstOrDefault(x => x.delivery_id == deliveryId && x.delivery_item_id == deliveryItemId);
                            if (dl != null)
                            {
                                if (dl.payment_mode == null)
                                    dl.payment_mode = dl.customer.payment_mode;

                                string docNo = "";
                                if (dl.payment_mode == EnumPaymentModes.DO.ToString())
                                    docNo = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.DOSALE.ToString());

                                else
                                    docNo = DocumentSerialDAL.GetNextDocument(DocumentSerialDAL.EnumDocuments.SALE.ToString());


                                List<sales_item> sales_Item = new List<sales_item>();
                                sales_Item.Add(new sales_item
                                {
                                    delivery_item_id = dl.delivery_item_id,
                                    discount = 0,
                                    gross_amount = dl.gross_amount,
                                    item_id = dl.item_id,
                                    net_amount = dl.net_amount,
                                    qty = dl.qty,
                                    rate = dl.rate,
                                    status = 1,
                                    total_beforvat = dl.total_beforvat,
                                    vat_amount = dl.vat_amount,

                                });



                                sales sale = new sales
                                {
                                    sales_number = docNo,
                                    customer_id = dl.customer_id,
                                    gross_amount = dl.gross_amount,
                                    total_beforevat = dl.total_beforvat,
                                    net_amount = dl.net_amount,
                                    total_discount = dl.discount,
                                    total_vat = dl.vat_amount,
                                    balance_amount = dl.payment_mode == "Cash" || dl.payment_mode == "Coupon" ? 0 : dl.net_amount,
                                    cash_paid = dl.payment_mode == "Cash" || dl.payment_mode == "Coupon" ? dl.net_amount : 0,
                                    old_leaf_count = oldLeaf,
                                    sales_date = Convert.ToDateTime(dl.delivery_time),
                                    payment_mode = dl.payment_mode,
                                    roundup = 0,
                                    status = 1,
                                    sales_item = sales_Item,
                                    remarks = "Pending Generated",
                                    delivery_leaf = dl.delivery_leaf,
                                    route_id = dl.delivery.route_id,
                                    
                                };
                                saleId = SaveSale(sale, context);
                                dl.sales_id = saleId;
                                context.Entry(dl).State = EntityState.Modified;

                                document_serial doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.SALE.ToString()).FirstOrDefault();
                                if (dl.payment_mode == EnumPaymentModes.DO.ToString())
                                    doc = context.document_serial.Where(docType => docType.document_type == DocumentSerialDAL.EnumDocuments.DOSALE.ToString()).FirstOrDefault();
                                doc.next_number = doc.next_number + 1;
                                context.Entry(doc).State = EntityState.Modified;
                                context.SaveChanges();

                                context.SaveChanges();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return saleId;
        }
    }
}
