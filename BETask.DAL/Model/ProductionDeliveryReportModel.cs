using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class ProductionDeliveryReportModel
    {
        public decimal Stock { get; set; }
        public decimal Production { get; set; }
        public List<ProductionDeliveryDetail> listDeliveryDetail { get; set; }
    }
    public class ProductionDeliveryDetail
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Scheduled   { get; set; }
        public decimal Delivered { get; set; }
        public decimal Returned { get; set; }
        public decimal Balance { get; set; }
    }
}
