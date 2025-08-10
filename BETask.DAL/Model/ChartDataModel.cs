using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class ChartDataModel
    {
        public List<ChartDelivery> listDelivery { get; set; }
        public List<ChartCollection> listCollection { get; set; }
    }
    public class ChartDelivery
    {
        public string Route { get; set; }
        public decimal Delivery { get; set; }
        public decimal Sales { get; set; }
    }
    public class ChartCollection
    {
        public string PaymentMode { get; set; }
        public decimal Amount { get; set; }
     
    }
}
