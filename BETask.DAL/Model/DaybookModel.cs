using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class DaybookModel
    {
        public string TransactionType { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
    public class DaybookDetailedModel
    {
        public string TransactionGroup { get; set; }
        public string TransactionType { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
}
