using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
  public  class AccountGroupModel
    {
        public int Company_id { get; set; }
        public int Location_id { get; set; }
        public int Group_id { get; set; }
        public string Group_name { get; set; }
        public string Description { get; set; }
        public int Parent_id { get; set; }
        public int Status { get; set; }

    }
}
