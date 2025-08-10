using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
   public class ItemModel
    {
        public int Item_id { get; set; }
        public string Item_name { get; set; }
        public string Barcode { get; set; }
        public string Brand { get; set; }
        public int UOM { get; set; }
        public decimal Tax { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Purchase_rate { get; set; }
        public decimal Sale_rate { get; set; }
        public decimal Opening_stock { get; set; }
        public int Rawmeterial { get; set; }// 1 true 2 is false
        public int Sellable { get; set; }// 1 true 2 is false
        public int Stockable { get; set; }// 1 true 2 is false means keepstock
        public int Status { get; set; }// 1 true 2 is false

    }
}
