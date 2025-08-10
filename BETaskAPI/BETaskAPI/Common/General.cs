using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using BETaskAPI.DAL.DAL;
using BETaskAPI.DAL.EDMX;
using BETaskAPI.Models;

namespace BETaskAPI.Common
{
    public static class General
    {
        public static decimal GetRateWithTax(DAL.EDMX.customer_aggrement agreement)
        {
            decimal rate = 0;
            try
            {
                if (agreement != null)
                {
                    if (agreement.unit_price > 0)
                    {
                        decimal vatRate = Convert.ToDecimal(agreement.item.tax_setting.tax_value);
                        //decimal vatAmount = Math.Round(((agreement.unit_price * vatRate) / 100), 2);
                        decimal vatAmount = (agreement.unit_price * vatRate) / 100;
                        rate = vatRate > 0 ? agreement.unit_price + vatAmount : agreement.unit_price;
                        rate = TruncateDecimalPlaces(rate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rate;
        }
        public static decimal TruncateDecimalPlaces(decimal val, int places = 4)
        {
            decimal roundedNumber = Math.Round(val, places, MidpointRounding.AwayFromZero);
            return roundedNumber;
        }
        public static DateTime GetArabTme()
        {
            return System.TimeZoneInfo.ConvertTimeFromUtc(
DateTime.UtcNow,
TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));
        }
        public static int IsSundayDelivery()
        {
            int enableSundayDelivery = 2;
            try
            {
                enableSundayDelivery = Convert.ToInt32(ConfigurationManager.AppSettings["EnableSundayDelivery"]);
            }
            catch { }
            return enableSundayDelivery;
        }
        public static int IsEnableOnlinePayment()
        {
            int enableOnlinePayment = 2;
            try
            {
                enableOnlinePayment = Convert.ToInt32(ConfigurationManager.AppSettings["EnableOnlinePayment"]);
            }
            catch { }
            return enableOnlinePayment;
        }

        public static decimal GetRechargeTax()
        {
            decimal rechargeTax = 0;
            try
            {
                rechargeTax = Convert.ToDecimal(ConfigurationManager.AppSettings["RechargeTax"]);
            }
            catch { }
            return rechargeTax;
        }
    }
}