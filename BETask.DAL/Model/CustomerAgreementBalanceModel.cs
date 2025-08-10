using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class CustomerAgreementBalanceModel
    {
        public string CustomerName { get; set; }
        public int customerId { get; set; }
        public string Route { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Opening { get; set; }
        public decimal Agreement { get; set; }//Detailed view inly
        public decimal Rate { get; set; }
        public decimal Delivered { get; set; }
        public decimal Returned { get; set; }
        public decimal Closing { get; set; }
        public string Date { get; set; }
    }
}
