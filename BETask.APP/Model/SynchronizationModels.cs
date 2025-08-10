using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.APP.Model
{
    class SynchronizationModels
    {
    }
    public class CustomerOutstandingModel
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public Nullable<decimal> outstanding { get; set; }
        public decimal wallet_balance { get; set; }
    }
}
