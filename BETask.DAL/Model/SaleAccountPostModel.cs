using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class SaleAccountPostModel
    {

        public DateTime TransactionDate { get; set; }
        public int SalesLedger { get; set; }
        public decimal SalesAmount { get; set; }
        public int DiscountAllowedLedger { get; set; }
        public decimal DiscountAllowedAmount { get; set; }
        public int RoundOffLedger { get; set; }
        public decimal RoundOffAmount { get; set; }
        public int VatOnSaleLedger { get; set; }
        public decimal VatOnSaleAmount { get; set; }
        public int CashSaleLedger { get; set; }
        public decimal CashSaleAmount { get; set; }
        public int CreditSaleLedger { get; set; }
        public decimal CreditPSaleAmount { get; set; }
        public int BankSaleLedger { get; set; }
        public decimal BankSaleAmount { get; set; }
        public int SaleId { get; set; }
        public string Narration { get; set; }
        public decimal CashBalance { get; set; } //to be collected amount-collected amount
    }
}
