using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BETaskCustomerAPI
{
    public class EncResponseParams
    {
        public string Status { get; set; }
        public string Message { get; set; } 
        public string OrderId { get; set; }
        public string TId { get; set; }
        public string EncRequest { get; set; }
        public string TrackingId { get; set; }
        public string RequestHash { get; set; }
        public string RedirectUrl { get; set; } = ConfigurationManager.AppSettings["RedirectUrl"];
        public string CancelUrl { get; set; } = ConfigurationManager.AppSettings["CancelUrl"];
        public string RsaUrl { get; set; } = ConfigurationManager.AppSettings["RsaUrl"];
        public int RequiredRelogin { get; set; } = 2;
    }

   
    
}