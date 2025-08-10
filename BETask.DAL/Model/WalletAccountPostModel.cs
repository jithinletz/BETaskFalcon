using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class WalletAccountPostModel
    {
        public int BankLedger { get; set; }
        public decimal BankLedgerAmount { get; set; }
        public int CashLedger { get; set; }
        public decimal CashAmount { get; set; }
        public int CustomerLedger { get; set; }
        public decimal CustomerAmount { get; set; }
        public string Narration { get; set; }
    }
}
