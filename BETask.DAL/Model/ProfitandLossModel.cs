using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class ProfitandLossModel
    {
        public string Description { get; set; }
        public decimal amount1 { get; set; }
        public decimal amount2 { get; set; }
        public decimal amount3 { get; set; }
        public string HeadType { get; set; }//Title,Value
    }

    public class ProfitandLossModelNew
    {
        public decimal OpeningStock { get; set; }
        public decimal ClosingStock { get; set; }
        public decimal Purchase { get; set; }
        public decimal PurchaseReturn { get; set; }
        public decimal Sale { get; set; }
        public decimal SaleReturn { get; set; }
        public decimal SaleTax { get; set; }
        public decimal PurchaseTax { get; set; }
        public List<ProfitandLossModel> ListDirectExp { get; set; }
        public List<ProfitandLossModel> ListINDirectExp { get; set; }
        public List<ProfitandLossModel> ListINDirectIncome { get; set; }
        public List<ProfitandLossModel> ListDirectIncome { get; set; }

    }

}
