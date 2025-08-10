using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class CustomerPerformanceModel
    {
        public string CustomerName { get; set; }
        public string Route { get; set; }
        public string LastTransaction { get; set; }
        public int Transactions { get; set; }
        public string Agreement { get; set; }
        public string Mobile { get; set; }
        public decimal CustomerStock { get; set; }
        public string Paymentmode { get; set; }
        public decimal WalletBalance { get; set; }


    }
}
