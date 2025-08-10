using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class CustomerAgreement_withClosingStockModel
    {

        public int CustomerAggrement_id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Packing { get; set; }
        public string RouteName { get; set; }
        public string CustomerName { get; set; }
        public decimal MaxQty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ClosingStock { get; set; }


    }
}
