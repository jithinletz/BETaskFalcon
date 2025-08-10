using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.APP.Model
{
    public class DailyCollectionModel
    {
        public int daily_collection_id { get; set; }
        public Nullable<int> delivery_id { get; set; }
        public int customer_id { get; set; }
        public decimal net_amount { get; set; }
        public decimal collected_amount { get; set; }
        public string payment_mode { get; set; }
        public int employee_id { get; set; }
        public System.DateTime delivery_time { get; set; }
        public string remarks { get; set; }
        public int status { get; set; }
    }
}
