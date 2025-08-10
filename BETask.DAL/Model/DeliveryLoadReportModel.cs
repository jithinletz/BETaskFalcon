using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
    public class DeliveryLoadReportModel
    {
        //public int DeliveryId { get; set; }
        public DateTime Date { get; set; }
        // public DateTime DateTo { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string EmployeeName { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal LoadQty { get; set; }
        public decimal TotalQty { get; set; }
        public decimal SoldQty { get; set; }
        public decimal DamageQty { get; set; }
        public decimal BalanceQty { get; set; }
        public string Remarks { get; set; }
    }
}