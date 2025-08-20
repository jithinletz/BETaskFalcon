using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Item
    {
        public int ItemId { get; set; }
       // public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        
        public string ImagePath { get; set; }
        public string Description { get;  set; }
    }
}