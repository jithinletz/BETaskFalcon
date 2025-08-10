using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class CustomerDelivery
    {

      public decimal TotalAmount { get; set; }
      public List<DeliveryItems> DeliveryItems { get; set; }
       
       
    }
}