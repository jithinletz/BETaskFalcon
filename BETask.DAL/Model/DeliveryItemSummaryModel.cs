using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class DeliveryItemSummaryModel
    {
        public string ItemName { get; set; }
        public decimal ScheduledQty { get; set; }
        public decimal DeleveredQty { get; set; }
        public decimal Foc { get; set; }
        public decimal Amount { get; set; }

        public decimal TotalQty { get; set; }

    }
}
