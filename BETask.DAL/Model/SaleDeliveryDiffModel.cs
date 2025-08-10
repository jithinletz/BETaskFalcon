using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class SaleDeliveryDiffModel
    {
        public string Route { get; set; }
        public int customerId { get; set; }
        public string CustomerName { get; set; }
        public decimal Delivery{ get; set; }
        public decimal Sales { get; set; }
       

    }
}
