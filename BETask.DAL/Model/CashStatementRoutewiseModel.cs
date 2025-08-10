using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
  public  class CashStatementRoutewiseModel
    {
        public DateTime TranDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RouteName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Narration { get; set; }
        public string TranType { get; set; }
    }
}
