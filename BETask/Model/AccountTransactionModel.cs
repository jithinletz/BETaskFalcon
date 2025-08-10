using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
    class AccountTransactionModel
    {
    }
    public class PurchaseAccountPosting
    {
        public DateTime TransactionDate { get; set; }
        public int PurchaseLedger { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int DiscountRecievedLedger { get; set; }
        public decimal DiscountRecievedAmount { get; set; }
        public int RoundOffLedger { get; set; }
        public decimal RoundOffAmount { get; set; }
        public int VatOnPurchaseLedger { get; set; }
        public decimal VatOnPurchaseAmount { get; set; }
        public int CashPurchaseLedger { get; set; }
        public decimal CashPurchaseAmount { get; set; }
        public int CreditPurchaseLedger { get; set; }
        public decimal CreditPurchaseAmount { get; set; }
        public int BankPurchaseLedger { get; set; }
        public decimal BankPurchaseAmount { get; set; }
        public int PurchaseId { get; set; }
        public string Narration { get; set; }

    }
}
