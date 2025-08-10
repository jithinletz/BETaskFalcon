using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class RoutewiseSaleModel
    {
        public DateTime DeliveryDate { get; set; }
        public decimal Loading { get; set; }
        public decimal Offload { get; set; }
        public decimal Sale { get; set; }
        public decimal Empty { get; set; }
        public decimal Balance { get; set; }
        public decimal Damage { get; set; }
        public decimal Cash { get; set; }
        public decimal Collection { get; set; }
        public decimal Wallet { get; set; }
        public decimal Coupon { get; set; }
        public decimal DoSale { get; set; }
        public decimal SalesmanCredit { get; set; }
        public decimal Outstanding { get; set; }
        public decimal Total { get; set; }
        public decimal Foc { get; set; }
    }
}
