using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETaskAPI.DAL
{
    public class PunchStatusModel
    {

        public long PunchId { get; set; }
        public string PunchType { get; set; }//IN or OUT
        public string PunchDate { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public string EmployeeName { get; set; }


    }
}
