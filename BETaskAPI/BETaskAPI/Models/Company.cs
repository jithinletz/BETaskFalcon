using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Company
    {
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TRN { get; set; }
        public int EnableInvoiceShare { get; set; }
        public string DefaultCurrency { get; set; }
        public int ShowCoupon { get; set; }
        public int DoNewScreen { get; set; }

    }
}