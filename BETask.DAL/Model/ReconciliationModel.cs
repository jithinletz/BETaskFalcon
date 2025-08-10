using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class ReconciliationModel
    {
        public int TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string PartyAccount { get; set; }
        public string Cheque { get; set; }
        public string ChequeBank { get; set; }
        public string ChequeDate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Narration { get; set; }
        public string ReconcilDate { get; set; }

    }
}
