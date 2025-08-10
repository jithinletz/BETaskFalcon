using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class RechargeHistory
    {

        public DateTime DeliveryTime { get; set; }
        public string PaymentMode { get; set; }
        public decimal Amount { get; set; }      
        public string CollectedBy { get; set; }       

    }
}