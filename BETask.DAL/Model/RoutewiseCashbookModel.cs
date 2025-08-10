using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class RoutewiseCashbookModel
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
