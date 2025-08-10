using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
    class CommonSettingModel
    {
    }
    public class TaxSettingModel
    {
        public int Tax_id { get; set; }
        public string Tax_name { get; set; }
        public string Description { get; set; }
        public decimal Tax_value { get; set; }
        public int Status { get; set; }

    }

    public class UOMSettingModel
    {
        public int UOM_id { get; set; }
        public string UOM_name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

    }
}
