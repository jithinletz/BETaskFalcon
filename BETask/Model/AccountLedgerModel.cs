using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
   public class AccountLedgerModel
    {
        public int Ledger_id { get; set; }
        public string Ledger_name { get; set; }
        public string Description { get; set; }
        public int Group_id { get; set; }
        public int Status { get; set; }
        public string GroupName{ get; set; }
        public int EnableCostCnetr { get; set; }
    }
}
