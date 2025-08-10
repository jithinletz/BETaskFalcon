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
    public class CouponAppDAL
    {
        public void SaveCoupon(coupon _coupon)
        {

            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            List<coupon_leaf> listCouponLeafs = _coupon.coupon_leaf.ToList();
                            coupon xCoupon = context.coupon.AsNoTracking().Where(x => x.coupon_id == _coupon.coupon_id).FirstOrDefault();

                            context.Entry(_coupon).State = xCoupon == null ? EntityState.Added : EntityState.Modified;
                            context.SaveChanges();

                            List<coupon_leaf> listLeafs = context.coupon_leaf.Where(x => x.coupon_id == _coupon.coupon_id).ToList();
                            context.coupon_leaf.RemoveRange(listLeafs);
                            context.SaveChanges();

                            //Save new leafs
                            context.coupon_leaf.AddRange(listCouponLeafs);
                            context.SaveChanges();

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        #region Deliverybook
        public int SaveDeliveryBook(List<EDMX.delivery_book> listDeivery)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    string bookNo = listDeivery[0].book_number;
                    if (context.delivery_book.Any(x => x.book_number == bookNo))
                        DeleteDeliveryBook(listDeivery);

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
        public void DeleteDeliveryBook(List<EDMX.delivery_book> listDeivery)
        {
            int result = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    foreach (EDMX.delivery_book dl in listDeivery)
                    {
                        EDMX.delivery_book xDelivery = context.delivery_book.FirstOrDefault(x => x.delivery_book_id == dl.delivery_book_id);
                        if (xDelivery != null)
                        {
                            context.delivery_book.Remove(xDelivery);
                            context.SaveChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
          
        }
        public void DeactivateDeliveryLeaf(int customerId, int bookId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    EDMX.delivery_book book = context.delivery_book.Where(x => x.delivery_book_id == bookId && x.customer_id == customerId).FirstOrDefault();
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

        public void ManualRedeemDeliveryLeaf(int customerId, string leaf, int deliveryId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    EDMX.delivery_book book = context.delivery_book.Where(x => x.leaf_no == leaf).FirstOrDefault();
                    if (book != null)
                    {
                        book.status = 4;
                        book.customer_id = customerId;
                        book.delivery_id = deliveryId;
                        book.redeemed_date = DateTime.Now;
                        context.Entry(book).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex) { throw; }
        }
        public bool CompareDeliveryBook(string bookNo, int leafCount)
        {
            bool resp = false;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    List<EDMX.delivery_book> listBooks = context.delivery_book.AsNoTracking().Where(x => x.book_number == bookNo).ToList();
                    if (listBooks == null || listBooks.Count != leafCount)
                        resp = false;
                    else
                        resp = true;
                }
            }
            catch (Exception ex)
            { throw; }
            return resp;
        }
        public int TransferDeliveryBook(string bookNo, int routeId)
        {
            int resp = 0;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
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

        public bool DeleteCoupon(int couponId)
        {
            bool result = false;
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    if (!context.coupon_leaf.Any(x => x.coupon_id == couponId && (x.status == 4 || x.status == 5)))
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

        #endregion

        #region Offer
        public void SaveOffer(EDMX.offer offer)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    context.Entry(offer).State = EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RemoveOffer(int offerId)
        {
            try
            {
                using (var context = new betaskdbEntitiesAPP())
                {
                    EDMX.offer offer = context.offer.Find(offerId);
                    if (offer != null)
                        offer.status = offer.status == 2 ? 1 : 2;
                    {
                        context.Entry(offer).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion offer

    }

}
