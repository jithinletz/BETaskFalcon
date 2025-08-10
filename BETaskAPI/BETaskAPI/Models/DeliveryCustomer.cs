using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class DeliveryCustomer
    {
        public int DeliveryId { get; set; } 

        public string DeliveryDate { get; set; }
        public string DeliveredDateTime { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Pobox { get; set; }
        public int DeliveryStatus { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string WalletNumber { get; set; }
        public decimal WalletBalance { get; set; }
        public string Remarks { get; set; }
        public int DistanceRadious { get; set; } = 0; // For checking customer distance while delivery. In meters
        public decimal Outstanding { get; set; }
        // public int status { get; set; }
        public string Payment_mode { get; set; }
        public string Delivery_interval { get; set; }
        public int RateIncludeTax { get; set; } = 1;
        public string Building { get; set; }
    }
}