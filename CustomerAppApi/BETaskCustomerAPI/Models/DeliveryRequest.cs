using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class DeliveryRequest
    {
        public int RequestId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public decimal ItemsCount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime RequestTime { get; set; }
        public string OtherDetails { get; set; }
        public int Status { get; set; }
        public List<DeliveryRequestItem> lstDeliveryRequestItem { get; set; }

    }
    public class DeliveryRequestItem
    {
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal NetAmount { get; set; }
        public int Status { get; set; }

    }
}