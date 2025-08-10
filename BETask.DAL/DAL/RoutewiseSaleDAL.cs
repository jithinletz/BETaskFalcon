using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Model;

namespace BETask.DAL.DAL
{
    public class RoutewiseSaleDAL
    {
        DeliveryDAL deliveryDAL = new DeliveryDAL();
        public List<Model.RoutewiseSaleModel> GetRoutewiseSale(int employeeId, int itemId, DateTime dateFrom, DateTime dateTo)
        {
            List<Model.RoutewiseSaleModel> listData = new List<RoutewiseSaleModel>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    int totalDays = (dateTo - dateFrom).Days + 1;
                    DateTime date = dateFrom;
                    for (int day = 1; day <= totalDays; day++)
                    {

                        //Loading , Offload , Sale
                        decimal loading = 0, offload = 0, sale = 0;
                        loading = Loading_Offload(context, date, employeeId, itemId, out offload, out sale);

                        //Empty Bottles
                        decimal empty = 0;
                        empty = Empty(context, date, employeeId, itemId);

                        //Damage
                        decimal damage = 0;
                        damage = Damage(context, date, employeeId, itemId);

                        //Short
                        decimal _short = sale - empty;
                        //_short = _short < 0 ? _short * -1:_short;

                        //Cash Sale
                        decimal cashSale = 0;
                        //cashSale = CashSale(context, date, employeeId, itemId);
                        cashSale = CashDelivery(context, date, employeeId, itemId);

                        //Outstanding
                        decimal creditSale = 0;
                        //creditSale = CreditSale(context, date, employeeId, itemId);
                        creditSale = CreditDelivery(context, date, employeeId, itemId);

                        //WalletSale
                        decimal walletSale = 0;
                        // walletSale = WalletSale(context, date, employeeId, itemId);
                        walletSale = WalletDelivery(context, date, employeeId, itemId);

                        //Collection otherthan delivery such as outstanding,wallet recharge
                        decimal collection = 0;
                        collection = Collections(context, date, employeeId);

                        //Do
                        decimal doSale = 0;
                        doSale = DoDelivery(context, date, employeeId, itemId);

                        //Salesman credit
                        decimal salemanCredit = 0;
                        salemanCredit = SalesmanCreditDelivery(context, date, employeeId, itemId);

                        //Total
                        decimal total = 0;
                        total = cashSale + walletSale + creditSale + doSale + salemanCredit;
                        //total = cashSale + walletSale + creditSale + collection;

                        //FOC
                        decimal foc = 0;
                        foc = FOC(context, date, employeeId, itemId);



                        listData.Add(new RoutewiseSaleModel
                        {
                            DeliveryDate = date,
                            Loading = loading,
                            Offload = offload,
                            Sale = sale,
                            Empty = empty,
                            Balance = _short,
                            Damage = damage,
                            Cash = cashSale,
                            Wallet = walletSale,
                            Coupon = 0,
                            Outstanding = creditSale,
                            Total = total,
                            Collection = collection,
                            Foc = foc,
                            DoSale = doSale,
                            SalesmanCredit = salemanCredit

                        });

                        date = date.AddDays(1);
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listData;
        }
        private decimal Loading_Offload(betaskdbEntities context, DateTime date, int employeeId, int itemId, out decimal offload, out decimal sale)
        {
            decimal loading = 0;
            offload = 0; sale = 0;
            try
            {
                //With route                                                                                                                                                                                 //without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();

                //Without route

                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    decimal _loading = 0, _offload = 0, _sale = 0;
                    int _delId = deliveryIds[i];
                    _loading = context.delivery_item_summary.AsNoTracking().Where(x => x.delivery_id == _delId && x.item_id == itemId && x.status == 1).Select(x => x.qty).DefaultIfEmpty(0).Sum();
                    _offload = context.delivery_item_summary.AsNoTracking().Where(x => x.delivery_id == _delId && x.item_id == itemId && x.status == 1).Select(x => x.used_qty).DefaultIfEmpty(0).Sum();
                    _offload = _loading - _offload;
                    loading += _loading;
                    offload += _offload;
                    _sale = context.delivery_items.AsNoTracking().Where(x => x.delivery_id == _delId && x.item_id == itemId && x.status == 4).Select(x => x.delivered_qty).DefaultIfEmpty(0).Sum();
                    sale += _sale;
                }
                //updated on 14.jul.2021 due to offload data issue
                offload = loading - sale;
            }
            catch (Exception ee)
            {
                throw;
            }
            return loading;
        }
        private decimal Empty(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal empty = 0;
            try
            {


                decimal _empty = 0;
                //with route
                if (employeeId > 0)
                    _empty = context.delivery_return.AsNoTracking().Where(x => x.employee_id == employeeId && x.item_id == itemId && x.return_date == date && x.status == 4).Sum(x => x.qty);

                //without route
                else
                    _empty = context.delivery_return.AsNoTracking().Where(x => x.item_id == itemId && x.return_date == date && x.status == 4).Sum(x => x.qty);

                empty = _empty;
            }
            catch
            {

            }
            return empty;
        }

        private decimal Damage(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal damage = 0;
            try
            {
                int exceptionId = 0;
                if (context.item_return_exception.Any(x => x.item_id == itemId))
                {
                    var itException = context.item_return_exception.AsNoTracking().Where(x => x.item_id == itemId && x.status == 1).FirstOrDefault();
                    if (itException != null)
                        exceptionId = itException.return_item_id;
                }
                itemId = exceptionId > 0 ? exceptionId : itemId;
                decimal _damage = 0;

                //with route
                if (employeeId > 0)
                    _damage = context.item_damage.AsNoTracking().Where(x => x.employee_id == employeeId && x.item_id == itemId && x.damage_date == date && x.status == 1).Sum(x => x.qty);

                //without route
                else
                    _damage = context.item_damage.AsNoTracking().Where(x => x.item_id == itemId && x.damage_date == date && x.status == 1).Sum(x => x.qty);

                damage = _damage;
            }
            catch
            {

            }
            return damage;
        }
        private decimal CashSale(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal cashSales = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    var _saleList = context.sales.AsNoTracking().Where(x => x.sales_order == delId && x.status == 1 && (x.payment_mode.ToLower() == "cash" || x.payment_mode.ToLower() == "bank")).ToList();

                    foreach (var _sale in _saleList)
                    {
                        if (_sale != null && _sale.sales_id > 0)
                        {
                            decimal _cashSale = 0;
                            _cashSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId && x.status == 1).Sum(x => x.net_amount);
                            cashSales += _cashSale;
                        }
                    }
                }
            }
            catch
            {
                // throw;
            }
            return cashSales;
        }
        private decimal CashDelivery(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal cashSales = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    //  var _saleList = context.daily_collection.AsNoTracking().Where(x => x.delivery_id == delId && x.status ==4 && (x.payment_mode.ToLower() == "cash" || x.payment_mode.ToLower() == "bank")).ToList();

                    //  foreach (var _sale in _saleList)
                    {
                        //    if (_sale != null && _sale.daily_collection_id > 0)
                        {
                            decimal _cashSale = 0;
                            if (context.sales.Any(x => x.sales_order == delId && (x.payment_mode.ToLower() == "cash" || x.payment_mode.ToLower() == "bank") && x.status == 1))
                            {
                                //  _cashSale = context.sales.AsNoTracking().Where(x => x.sales_order == delId && (x.payment_mode.ToLower() == "cash" || x.payment_mode.ToLower() == "bank") && x.status == 1).Sum(x => x.net_amount);
                                _cashSale = context.sales_item.Include(s => s.sales).Where(x => x.sales.sales_order == delId && (x.sales.payment_mode.ToLower() == "cash" || x.sales.payment_mode.ToLower() == "bank") && x.sales.status == 1 && (itemId > 0 ? x.item_id == itemId : x.item_id > 0)).Sum(x => x.net_amount);
                            }
                            /*Chceking more items in one delivery of a customer 
                                If more items should use _tColl below
                                */
                            // int customerId = Convert.ToInt32(dr["customer_id"].ToString());

                            // List<EDMX.delivery_items> listItems = deliveryDAL.GetDeliveryItemsByIdandCustomer(delId, _sale.customer_id);
                            //if (_sale.customer_id == 85)
                            //{
                            //    string _debug = "";
                            //}
                            decimal _tColl = 0;
                            /*if (listItems.Select(x => x.item_id).Distinct().Count() > 1 && itemId>0)
                            {
                                _tColl = listItems.Where(x => x.item_id == itemId).Sum(x => x.net_amount);
                                if (_saleList.Where(x => x.customer_id == _sale.customer_id && x.collected_amount == _tColl).Count() == 0)
                                    _tColl = 0;
                            }
                            /* E N D  Chceking more items in one delivery of a customer*/
                            //_cashSale =  _sale.collected_amount;

                            // decimal _cashSale = _tColl== 0?_sale.collected_amount:_tColl;
                            // _cashSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId && x.status == 1).Sum(x => x.net_amount);
                            cashSales += _cashSale;
                        }
                    }
                }
            }
            catch
            {
                // throw;
            }
            return cashSales;
        }
        private decimal FOC(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal foc = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status != 2).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status != 2).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    var _saleList = context.sales.AsNoTracking().Where(x => x.sales_order == delId && x.status == 1).ToList();
                    foreach (var _sale in _saleList)
                    {
                        if (_sale != null && _sale.sales_id > 0)
                        {
                            decimal _cashSale = 0;
                            if (context.sales_item.Any(x => x.sales_id == _sale.sales_id && x.item_id == itemId && x.status == 1 && x.rate == 0))
                                _cashSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId && x.status == 1 && x.rate == 0).Sum(x => x.qty);
                            foc += _cashSale;
                        }
                    }
                }
            }
            catch
            {
                // throw;
            }
            return foc;
        }
        private decimal WalletSale(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal walletSale = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    var _saleList = context.sales.AsNoTracking().Where(x => x.sales_order == delId && x.status == 1 & x.payment_mode.ToLower() == "coupon").ToList();
                    foreach (var _sale in _saleList)
                    {
                        if (_sale != null && _sale.sales_id > 0)
                        {
                            decimal _walletSale = 0;
                            _walletSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId).Sum(x => x.net_amount);
                            walletSale += _walletSale;
                        }
                    }
                }
            }
            catch
            {
                // throw;
            }
            return walletSale;
        }
        private decimal WalletDelivery(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal cashSales = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    // var _saleList = context.daily_collection.AsNoTracking().Where(x => x.delivery_id == delId && x.status == 4 && (x.payment_mode.ToLower() == "coupon")).ToList();

                    // foreach (var _sale in _saleList)
                    {
                        //   if (_sale != null && _sale.daily_collection_id > 0)
                        {
                            decimal _cashSale = 0;
                            if (context.sales.Any(x => x.sales_order == delId && (x.payment_mode.ToLower() == "coupon") && x.status == 1))
                                // _cashSale = context.sales.AsNoTracking().Where(x => x.sales_order == delId && (x.payment_mode.ToLower() == "coupon") && x.status == 1).Sum(x => x.net_amount);
                                _cashSale = context.sales_item.Include(s => s.sales).Where(x => x.sales.sales_order == delId && (x.sales.payment_mode.ToLower() == "coupon") && x.sales.status == 1 && (itemId > 0 ? x.item_id == itemId : x.item_id > 0)).Sum(x => x.net_amount);

                            /*Chceking more items in one delivery of a customer 
                                    If more items should use _tColl below
                                    */
                            // int customerId = Convert.ToInt32(dr["customer_id"].ToString());

                            // List<EDMX.delivery_items> listItems = deliveryDAL.GetDeliveryItemsByIdandCustomer(delId, _sale.customer_id);
                            // decimal _tColl = 0;
                            // if (listItems.Select(x => x.item_id).Distinct().Count() > 1 && itemId > 0)
                            // {
                            //    _tColl = listItems.Where(x => x.item_id == itemId).Sum(x => x.net_amount);
                            //               }
                            /* E N D  Chceking more items in one delivery of a customer*/

                            // decimal _cashSale = _tColl == 0 ? _sale.collected_amount : _tColl;
                            // _cashSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId && x.status == 1).Sum(x => x.net_amount);
                            cashSales += _cashSale;
                        }
                    }
                }
            }
            catch
            {
                // throw;
            }
            return cashSales;
        }

        private decimal CreditSale(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal creditSale = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    var _saleList = context.sales.AsNoTracking().Where(x => x.sales_order == delId && x.status == 1 & x.payment_mode.ToLower() == "credit").ToList();
                    foreach (var _sale in _saleList)
                    {
                        if (_sale != null && _sale.sales_id > 0)
                        {
                            decimal _creditSale = 0;
                            _creditSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId).Sum(x => x.net_amount);
                            creditSale += _creditSale;
                        }
                    }
                }
            }
            catch
            {
                //throw;
            }
            return creditSale;
        }
        private decimal CreditDelivery(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal cashSales = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    //var _saleList = context.daily_collection.AsNoTracking().Where(x => x.delivery_id == delId && x.status == 4 && (x.payment_mode.ToLower() == "credit")).ToList();

                    //foreach (var _sale in _saleList)
                    {
                        //  if (_sale != null && _sale.daily_collection_id > 0)
                        {
                            decimal _cashSale = 0;
                            if (context.sales.Any(x => x.sales_order == delId && (x.payment_mode.ToLower() == "credit") && x.status == 1))
                                // _cashSale = context.sales.AsNoTracking().Where(x => x.sales_order == delId && (x.payment_mode.ToLower() == "credit") && x.status == 1).Sum(x => x.net_amount);
                                _cashSale = context.sales_item.Include(s => s.sales).Where(x => x.sales.sales_order == delId && (x.sales.payment_mode.ToLower() == "credit") && x.sales.status == 1 && (itemId > 0 ? x.item_id == itemId : x.item_id > 0)).Sum(x => x.net_amount);


                            /*Chceking more items in one delivery of a customer 
                                    If more items should use _tColl below
                                    */
                            // int customerId = Convert.ToInt32(dr["customer_id"].ToString());

                            //List<EDMX.delivery_items> listItems = deliveryDAL.GetDeliveryItemsByIdandCustomer(delId, _sale.customer_id);
                            //decimal _tColl = 0;
                            //if (listItems.Select(x => x.item_id).Distinct().Count() > 1 && itemId > 0)
                            //{
                            //    _tColl = listItems.Where(x => x.item_id == itemId).Sum(x => x.net_amount);
                            //}
                            /* E N D  Chceking more items in one delivery of a customer*/

                            // decimal _cashSale = _tColl == 0 ? _sale.collected_amount : _tColl;
                            // _cashSale += context.sales_item.AsNoTracking().Where(x => x.sales_id == _sale.sales_id && x.item_id == itemId && x.status == 1).Sum(x => x.net_amount);
                            cashSales += _cashSale;
                        }
                    }
                }
            }
            catch
            {
                // throw;
            }
            return cashSales;
        }
        //Other collections  other than delivery collection such as outstanding,wallet recharge etc...
        private decimal Collections(betaskdbEntities context, DateTime date, int employeeId)
        {
            decimal collection = 0;
            try
            {

                decimal _collection = 0;
                //With route
                if (employeeId > 0)
                    _collection = context.daily_collection.AsNoTracking().Where(x => x.employee_id == employeeId && x.status == 4 && DbFunctions.TruncateTime(x.delivery_time) == date && (x.delivery_id == null || x.delivery_id == 0)).Sum(x => x.collected_amount);

                //Without route
                else
                    _collection = context.daily_collection.AsNoTracking().Where(x => x.status == 2 && DbFunctions.TruncateTime(x.delivery_time) == date && (x.delivery_id == null || x.delivery_id == 0)).Sum(x => x.collected_amount);
                // _collection = context.daily_collection.AsNoTracking().Where(x => x.employee_id == employeeId  ).Sum(x => x.collected_amount);
                collection += _collection;


            }
            catch (Exception ee)
            {

            }
            return collection;
        }
        private decimal DoDelivery(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal cashSales = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    {
                        decimal _cashSale = 0;
                        if (context.sales.Any(x => x.sales_order == delId && (x.payment_mode.ToLower() == "do") && x.status == 1))
                            //_cashSale = context.sales.AsNoTracking().Where(x => x.sales_order == delId && (x.payment_mode.ToLower() == "do") && x.status == 1).Sum(x => x.net_amount);
                            _cashSale = context.sales_item.Include(s => s.sales).Where(x => x.sales.sales_order == delId && (x.sales.payment_mode.ToLower() == "do") && x.sales.status == 1 && (itemId > 0 ? x.item_id == itemId : x.item_id > 0)).Sum(x => x.net_amount);

                        decimal _tColl = 0;
                        cashSales += _cashSale;
                    }
                }
            }
            catch
            {
                // throw;
            }
            return cashSales;
        }
        private decimal SalesmanCreditDelivery(betaskdbEntities context, DateTime date, int employeeId, int itemId)
        {
            decimal cashSales = 0;
            try
            {
                //with route --------------------------------------------------------------------------------------------------------------------------------------------------------------------------without route
                int[] deliveryIds = employeeId > 0 ? context.delivery.AsNoTracking().Where(x => x.employee_id == employeeId && x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray() : context.delivery.AsNoTracking().Where(x => x.delivery_date == date && x.status == 1).Select(x => x.delivery_id).ToArray();
                for (int i = 0; i < deliveryIds.Length; i++)
                {
                    int delId = deliveryIds[i];
                    {
                        decimal _cashSale = 0;
                        if (context.sales.Any(x => x.sales_order == delId && (x.payment_mode.ToLower() == "salesmancredit") && x.status == 1))
                            //  _cashSale = context.sales.AsNoTracking().Where(x => x.sales_order == delId && (x.payment_mode.ToLower() == "salesmancredit") && x.status == 1).Sum(x => x.net_amount);
                            _cashSale = context.sales_item.Include(s => s.sales).Where(x => x.sales.sales_order == delId && (x.sales.payment_mode.ToLower() == "salesmancredit") && x.sales.status == 1 && (itemId > 0 ? x.item_id == itemId : x.item_id > 0)).Sum(x => x.net_amount);

                        decimal _tColl = 0;
                        cashSales += _cashSale;
                    }
                }
            }
            catch
            {
                // throw;
            }
            return cashSales;
        }
    }
}
