using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class SalesItem
    {
        public string CustomerName { get; set; }
        public DateTime SalesDate { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string UOMName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeVAT { get; set; }
        public decimal VATAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string SalesNumber { get; set; }
        public string PaymentMode { get; set; }
    }

}
