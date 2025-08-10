using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class BalanceSheetModel
    {
        public string Liability { get; set; }
        public decimal LiabliltyAmount { get; set; }
        public string Asset { get; set; }
        public decimal AssetAmount { get; set; }
    }
}
