using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BETask.DAL.EDMX;
using System.Data.Entity;
using System.Data;
using BETask.DAL.Interface;

namespace BETask.DAL.DAL
{
    public class OfferDAL : IOffer
    {
     

        public void SaveOffer(EDMX.offer offer)
        {
            try
            {
                using(var context = new betaskdbEntities())
                {                    
                    context.Entry(offer).State = EntityState.Added;
                    context.SaveChanges();                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void RemoveOffer(int offerId)
        {
            try
            {
                using (var context = new betaskdbEntities())
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

        public List<EDMX.offer> GetOffers(betaskdbEntities _context=null)
        {
            List<EDMX.offer> listOffers = new List<offer>();
            try
            {
                if (_context != null)
                    listOffers = _context.offer.OrderBy(x => x.status).ThenBy(x => x.offer_id).ToList();
                else
                {
                    using (var context = new betaskdbEntities())
                    {
                        listOffers = context.offer.OrderBy(x => x.status).ThenBy(x => x.offer_id).ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
            return listOffers;
        }
    }
}
