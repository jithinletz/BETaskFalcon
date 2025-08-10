using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class DeliveryItem
    {

        public DateTime DeliveryDate { get; set; }
        public string ItemName { get; set; }
        public decimal Qty { get; set; }
        public decimal NetAmount { get; set; }
        public string DeliveredBy { get; set; }       

    }
}