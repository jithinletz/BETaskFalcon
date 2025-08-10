using BETask.DAL.DAL;
using BETask.Model;
using EDMX = BETask.DAL.EDMX;
using System;
using System.Collections.Generic;
using System.Data;
using RPT = BETask.Report.ReportForm;
using BETask.Common;
using System.Diagnostics;
using System.Linq;
using BETask.APP.EDMX;
using AutoMapper;
using BETask.DAL.EDMX;

namespace BETask.BAL
{
   public class OfferBAL
    {
        BETask.DAL.DAL.OfferDAL offerDAL = new OfferDAL();
        BETask.APP.DAL.CouponAppDAL offerAppDAL = new APP.DAL.CouponAppDAL();
        public void SaveOffer(EDMX.offer offer)
        {
            try
            {
                offerDAL.SaveOffer(offer);
                

                var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<EDMX.offer, BETask.APP.EDMX.offer>()
               );
                var mapper = new Mapper(config);
                BETask.APP.EDMX.offer offerApp = mapper.Map<BETask.APP.EDMX.offer>(offer);
                offerAppDAL.SaveOffer(offerApp);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = offer.offer_id,
                    summary = $" Offer : {offer.offer_name}",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }

        public List<EDMX.offer> GetOffers(betaskdbEntities context = null)
        {
            try
            {
                return offerDAL.GetOffers(context);
            }
            catch { throw; }
        }

        public void RemoveOffer(int offerId)
        {
            try
            {
                offerDAL.RemoveOffer(offerId);
                offerAppDAL.RemoveOffer(offerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var st = new StackTrace();
                var sf = st.GetFrame(1);


                EDMX.user_log user_Log = new EDMX.user_log()
                {
                    username = General.userName,
                    module = this.GetType().Name,
                    module_action = sf.GetMethod().Name,
                    reference_id = offerId,
                    summary = $" Offer Deactivated",

                };
                DAL.DAL.UserLogDAL.Log(user_Log);
            }
        }
        
    }
}
