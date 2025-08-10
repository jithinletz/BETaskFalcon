using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data.SqlClient;

namespace BETaskAPI.Common
{
  

   static class General
    {
        public static int companyId = 1,locationId = 1;
        public static string companyName { get; set; }
        public static string userName { get; set; } = "LETZ";
        public static int userId { get; set; } 
        public static string cloudConnection { get; set; }
   
        public static decimal ParseDecimal(string text)
        {
            decimal val = 0;
            decimal.TryParse(text, out val);
            return val;
        }
        public static int ParseInt(string text)
        {
            int val = 0;
            int.TryParse(text, out val);
            return val;
        }

        public static decimal TruncateDecimalPlaces(decimal val, int places=2)
        {
            return Math.Round(val, places);
            if (places < 0)
            {
                throw new ArgumentException("places");
            }
            return Math.Round(val - Convert.ToDecimal((0.5 / Math.Pow(10, places))), places);
        }
        public static decimal TruncateDecimalPlacesString(string val, int places=2)
        {
            decimal retValue = 0;
            decimal.TryParse(val, out retValue);
            if (places < 0)
            {
                throw new ArgumentException("places");
            }
            // return Math.Round(val - Convert.ToDecimal((0.5 / Math.Pow(10, places))), places,MidpointRounding.AwayFromZero);
            return decimal.Round(retValue, places, MidpointRounding.AwayFromZero);
        }
      
       
       
        public static DateTime ConvertDateServerFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd"));
        }
        public static DateTime ConvertDateServerFormatWithCurrentTime(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            DateTime tm = DateTime.Now;
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,tm.Hour , tm.Minute, tm.Second);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static DateTime ConvertDateServerFormatWithStartTime(DateTime dateTime)
        {
            
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 00, 00,00);

            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public static DateTime ConvertDateServerFormatWithEndTime(DateTime dateTime)
        {
          
            DateTime dt = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            
            return DateTime.Parse(dt.ToString("yyyy/MM/dd HH:mm:ss")); 
        }
        public static DateTime ConvertDateTimeServerFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return DateTime.Parse(dateTime.ToString("yyyy/MM/dd hh:mm tt"));
        }
        public static DateTime ConvertDateServerFormat_string(string dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return  DateTime.Parse(DateTime.Parse(dateTime).ToString("yyyy/MM/dd"));
        }
        public static string ConvertDateAppFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return dateTime.ToString("dd/MM/yyyy");
        }
        public static string ConvertDateTimeAppFormat(DateTime dateTime)
        {
            //return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return dateTime.ToString("dd/MM/yyyy hh:mm tt");
        }
        

    }
}
