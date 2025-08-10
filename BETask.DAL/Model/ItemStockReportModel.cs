using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.DAL.Model
{
   public class ItemStockReportModel
    {
        public Int32 item_id { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string Item_name { get; set; }
        public decimal Opening_Stock { get; set; }
        public decimal Closing_Stock { get; set; }
        public decimal Purchase { get; set; }
        public decimal Sale { get; set; }
        public decimal ProductionIn { get; set; }
        public decimal ProductionOut { get; set; }
        public decimal Return { get; set; }
        public decimal Damage { get; set; }       
        public string TransferType { get; set; }
        public decimal TransferIn { get; set; }
        public decimal TransferOut { get; set; }
    }
}
