using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class CustomerOutstandingvsWallet
    {
        public int customerId { get; set; }
        public string CustomerName { get; set; }
        public string Route { get; set; }
        public string WalletNumber { get; set; }
        public decimal Outstanding { get; set; }
        public decimal WalletBalance { get; set; }
    }
}
