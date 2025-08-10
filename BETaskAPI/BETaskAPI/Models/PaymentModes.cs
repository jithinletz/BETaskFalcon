using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class PaymentModes
    {
        public string[] Paymentmodes = { "Cash","Credit", "Coupon", "Bank", "DO", "SalesmanCredit" };
        public string[] Deliveryinterval = { "Sunday", "Monday", "Tuesday","Wednesday", "Thursday", "Friday", "Saturday", "Twice in Week", "Weekly Once", "Custom" };
        public List<string> Routes { get; set; }
        public List<string> Building { get; set; }

    }
}