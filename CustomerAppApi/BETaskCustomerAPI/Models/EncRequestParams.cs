using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskCustomerAPI
{
    public class EncRequestParams
    {
        public string OrderId { get; set; }
        public string Tid { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
    }
}