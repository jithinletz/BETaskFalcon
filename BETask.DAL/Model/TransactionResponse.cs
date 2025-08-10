using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL
{
    public class TransactionResponse
    {
        public string ReferenceId { get; set; }
        public decimal AmountReceived { get; set; }
        public string PaymentReferenceId { get; set; }
        public string PaymentMode { get; set; }
        public string StatusText { get; set; }
        public string Response { get; set; }
        public string Version { get; set; } = "V.0.0.1";

    }
}
