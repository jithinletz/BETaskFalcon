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
    public class CouponDAL
    {
        public void SaveCoupon(coupon _coupon, List<coupon_leaf> listCouponLeaf)
        {

            try
            {
                using (var context = new betaskdbEntities())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var xCoupon = context.coupon.Where(x => x.book_number == _coupon.book_number).FirstOrDefault();
                            if (xCoupon == null || _coupon.coupon_id > 0)
                            {
                                _coupon.coupon_leaf = listCouponLeaf;
                                if (_coupon.coupon_id == 0)
                                {
                                    context.Entry(_coupon).State = _coupon.coupon_id == 0 ? EntityState.Added : EntityState.Modified;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    xCoupon = context.coupon.Where(x => x.coupon_id == _coupon.coupon_id).FirstOrDefault();
                                    xCoupon.book_number = _coupon.book_number;
                                    xCoupon.book_rate = _coupon.book_rate;
                                    xCoupon.leaf_from = _coupon.leaf_from;
                                    xCoupon.leaf_end = _coupon.leaf_end;
                                    xCoupon.remarks = _coupon.remarks;
                                    xCoupon.leaf_rate = _coupon.leaf_rate;
                                    xCoupon.issue_date = _coupon.issue_date;
                                    xCoupon.status = _coupon.status;
                                    context.Entry(xCoupon).State = EntityState.Modified;
                                    context.SaveChanges();

                                    List<coupon_leaf> listLeafs = context.coupon_leaf.Where(x => x.coupon_id == _coupon.coupon_id && x.status == 1).ToList();
                                    context.coupon_leaf.RemoveRange(listLeafs);
                                    context.SaveChanges();

                                    foreach (EDMX.coupon_leaf leaf in listCouponLeaf)
                                    {
                                        if (leaf.status == 4)
                                        {
                                            leaf.redeem_date = DateTime.Now;
                                            leaf.remarks = $"Manual redeem on {DateTime.Now}";
                                        }
                                        context.Entry(leaf).State = EntityState.Added;
                                    }
                                    context.SaveChanges();
                                }
                                transaction.Commit();
                            }
                            else
                            {
                                throw new Exception("Coupon book already assigned to another customer ");
                            }
                        }
                        catch
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

        public List<coupon> SearchCoupon(string bookno, int customerId)
        {
            List<coupon> listCoupon = new List<coupon>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (bookno == string.Empty)
                    {
                        listCoupon = context.coupon.Include(c => c.customer).AsNoTracking().Where(x => x.status == 1).OrderByDescending(x => x.coupon_id).ToList();
                        if (customerId > 0)
                            listCoupon = listCoupon.Where(c => c.customer_id == customerId).ToList();
                        listCoupon = listCoupon.Count > 100 ? listCoupon.Take(100).ToList() : listCoupon;
                    }
                    else
                    {
                        listCoupon = context.coupon.Include(c => c.customer).AsNoTracking().Where(x => x.status == 1 && x.book_number == bookno).OrderByDescending(x => x.coupon_id).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listCoupon;
        }

        public coupon GetCoupon(int couponId)
        {
            coupon _coupon = new coupon();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _coupon = context.coupon.Include(l => l.coupon_leaf).AsNoTracking().Include(c => c.customer).AsNoTracking().Where(x => x.coupon_id == couponId).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
            return _coupon;
        }
        public List<EDMX.coupon_leaf> GetCouponLeaf(int couponId)
        {
            List<EDMX.coupon_leaf> _coupon = new List<EDMX.coupon_leaf>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    _coupon = context.coupon_leaf.AsNoTracking().Where(x => x.coupon_id == couponId).ToList();
                }
            }
            catch
            {
                throw;
            }
            return _coupon;
        }
        public List<EDMX.coupon> GetCouponBooks(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            List<EDMX.coupon> listCoupon = new List<coupon>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCoupon = context.coupon.AsNoTracking().Include(c => c.customer).AsNoTracking().Include(l => l.coupon_leaf).Where(x => x.issue_date >= dateFrom && x.issue_date <= dateTo && x.status == 1 && (routeId == 0 ? x.customer.route_id > 0 : x.customer.route_id == routeId)).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCoupon;
        }
        public List<EDMX.coupon_leaf> GetRedeemedLeafs(DateTime dateFrom, DateTime dateTo, int routeId)
        {
            List<EDMX.coupon_leaf> listCoupon = new List<coupon_leaf>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listCoupon = context.coupon_leaf.AsNoTracking().Include(b => b.coupon).AsNoTracking().Include(c => c.coupon.customer).Where(x => x.redeem_date != null && x.redeem_date >= dateFrom && x.redeem_date <= dateTo && (routeId == 0 ? x.coupon.customer.route_id > 0 : x.coupon.customer.route_id == routeId)).ToList();
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return listCoupon;
        }
        public string LeafExistCheck(Int64 leafFrom, Int64 leafTo, string bookNo)
        {
            string result = "";
            try
            {
                using (var context = new betaskdbEntities())
                {
                    coupon book = context.coupon.AsNoTracking().Where(x => x.book_number == bookNo).FirstOrDefault();
                    if (book != null)
                    {
                        string _customer = context.customer.AsNoTracking().Where(c => c.customer_id == book.customer_id).FirstOrDefault().customer_name;
                        result = $"This coupon book already assigined to customer {_customer}";
                    }
                    else
                    {
                        List<coupon_leaf> listLeafs = context.coupon_leaf.AsNoTracking().Where(x => x.leaf_number >= leafFrom && x.leaf_number <= leafTo).ToList();
                        if (listLeafs != null)
                        {
                            foreach (coupon_leaf lf in listLeafs)
                            {
                                coupon _coupon = context.coupon.AsNoTracking().Where(x => x.coupon_id == lf.coupon_id).FirstOrDefault();
                                if (_coupon != null)
                                {
                                    result = $"Leaf {lf.leaf_number} assigned to another coupon book {_coupon.book_number}";
                                    break;
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
            return result;
        }

        public bool DeleteCoupon(int couponId)
        {
            bool result = false;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    if (!context.coupon_leaf.Any(x => x.coupon_id==couponId &&( x.status == 4 || x.status == 5)))
                    {
                        var coupon = context.coupon.FirstOrDefault(x => x.coupon_id == couponId);
                        var couponLeaf = context.coupon_leaf.Where(x => x.coupon_id == couponId).ToList();
                        context.coupon_leaf.RemoveRange(couponLeaf);
                        context.coupon.Remove(coupon);
                        context.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
            return result;
        }

        #region DeliveryBook
        public int SaveDeliveryBook(List<EDMX.delivery_book> listDeivery)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    context.delivery_book.AddRange(listDeivery);
                    result = context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public List<EDMX.delivery_book> GetDeliveryBook(int employeeId)
        {
            List<EDMX.delivery_book> listDeliveryBook = new List<delivery_book>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDeliveryBook = context.delivery_book.Include(c=>c.customer).Where(x => x.employee_id == employeeId).ToList();

                }
            }
            catch (Exception ex) { throw; }
            return listDeliveryBook;
        }
        public List<EDMX.delivery_book> GetDeliveryBookByRoute(int routeId)
        {
            List<EDMX.delivery_book> listDeliveryBook = new List<delivery_book>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    listDeliveryBook = context.delivery_book.Include(c => c.customer).Where(x => x.route_id == routeId).ToList();
                    if (listDeliveryBook == null || listDeliveryBook.Count==0)
                    {
                        var subroutes = context.route_group.Where(x => x.route_id == routeId && x.Status == 1).Select(x=>x.sub_route_id).ToList();
                        foreach (int r in subroutes)
                        {
                            listDeliveryBook.AddRange(context.delivery_book.Include(c => c.customer).Where(x => x.route_id == r).ToList());
                        }
                        // List<int> subIds=subroutes.Select(x=>x.)
                        // listDeliveryBook = context.delivery_book.Include(c => c.customer).Include(r=>r.route).Where(x =>  subroutes.Select(r=>r.).Contains(x.route.route_id)).ToList();
                    }

                }
            }
            catch (Exception ex) { throw; }
            return listDeliveryBook;
        }
        public void DeactivateDeliveryLeaf(int employeeId, int bookId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.delivery_book book = context.delivery_book.Where(x => x.delivery_book_id == bookId && x.employee_id == employeeId).FirstOrDefault();
                    if (book != null)
                    {
                        book.status = 2;
                        book.redeemed_date = DateTime.Now;
                        context.Entry(book).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex) { throw; }
        }
       
        public void DeactivateDeliveryLeafReverse(int customerId, int bookId)
        {
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.delivery_book book = context.delivery_book.Where(x => x.delivery_book_id == bookId && x.customer_id == customerId).FirstOrDefault();
                    if (book != null)
                    {
                        book.status = 1;
                        book.redeemed_date = null;
                        context.Entry(book).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex) { throw; }
        }
        public bool IsDeliveryBookExist(string bookNo)
        {
            bool resp = false;
            try
            {
                using (var context = new betaskdbEntities())
                {

                    if (context.delivery_book.Any(x => x.book_number.ToLower() == bookNo))
                        resp = true;
                }
            }
            catch (Exception ex) { throw; }
            return resp;
        }
        public List<EDMX.delivery_book> GetBookbyLeaf(string leafNo)
        {
            List<EDMX.delivery_book> listBook = new List<delivery_book>();
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.delivery_book book = context.delivery_book.FirstOrDefault(x => x.leaf_no == leafNo);
                    if (book != null)
                    {
                        listBook = context.delivery_book.Include(c => c.customer).Where(x => x.book_number == book.book_number).ToList();
                    }
                }
            }
            catch (Exception) { throw; }
            return listBook;
        }
        public void RedeemDeliveryLeaf(delivery_items di,betaskdbEntities context)
        {
            try
            {
                delivery_book book = context.delivery_book.FirstOrDefault(x => x.leaf_no == di.delivery_leaf && x.status==1);
                if (book != null)
                {
                    book.delivery_id = di.delivery_id;
                    book.redeemed_date = di.delivery_time;
                    book.customer_id = di.customer_id;
                    book.status = 4;
                    context.Entry(book).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void RedeemDeliveryLeafBySale(sales _sale, betaskdbEntities context)
        {
            try
            {
                if (_sale.delivery_leaf != null && _sale.payment_mode.ToLower()=="do")
                {
                    delivery_book leaf = context.delivery_book.FirstOrDefault(x => x.leaf_no == _sale.delivery_leaf && x.status == 1);
                    if (leaf != null)
                    {
                        leaf.status = 4;
                        leaf.delivery_id = _sale.sales_order;
                        leaf.customer_id = _sale.customer_id;
                        leaf.redeemed_date = _sale.sales_date;
                        context.Entry(leaf).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ValidateDeliveryLeaf(string leafNo, int employeeId)
        {
            int resp = 2;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    EDMX.delivery_book leaf = context.delivery_book.AsNoTracking().FirstOrDefault(x => x.leaf_no == leafNo && x.employee_id == employeeId);
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
        public int TransferDeliveryBook(string bookNo,int routeId)
        {
            int resp = 0;
            try
            {
                using (var context = new betaskdbEntities())
                {
                    List<delivery_book> listLeafs = context.delivery_book.Where(x => x.book_number == bookNo).ToList();
                    if (listLeafs != null && listLeafs.Count > 0)
                    {
                        listLeafs.ForEach(z => z.route_id = routeId);
                        context.SaveChanges();
                        resp = listLeafs.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;
        }
        
        #endregion

    }
}
