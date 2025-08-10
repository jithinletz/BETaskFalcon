using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class DeliveryHistory
    {
        public List<DeliveryItem> listDeliveryItem { get; set; }
        public List<RechargeHistory> listRechargeHistory { get; set; }
    }

}