using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL
{
   public class CustomerMonthlyOutstandingModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal OB { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal CB { get; set; }
    }
    public class CustomerOutstandingModel
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public Nullable<decimal> outstanding { get; set; }
        public decimal wallet_balance { get; set; }
    }
}
