using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class DSRDeliveryItemsModel
    {
        public string ItemName { get; set; }
        public string CustomerName { get; set; }
        public decimal Rate   { get; set; }
        public decimal Sale { get; set; }
        public decimal Empty { get; set; }
        public decimal CashSale { get; set; }
        public decimal CreditSale { get; set; }
        public decimal CouponSale { get; set; }
        public decimal DoSale { get; set; }
        public decimal SalesmanCredit { get; set; }
    }
}
