using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETaskAPI.DAL
{
   public class PunchModel
    {
       
            public long PunchId { get; set; }
            public int EmployeeId { get; set; }
            public string Lat { get; set; } = string.Empty;
            public string Lng { get; set; } = string.Empty;
            public string LocationDetails { get; set; } = string.Empty;
            public DateTime PunchDate { get; set; }
            public DateTime PunchIn { get; set; }
            public DateTime? PunchOut { get; set; } // Nullable
            public string Remarks { get; set; } // Nullable
            public int Status { get; set; }
            public DateTime AppDate { get; set; }
            public string AppVersion { get; set; } = string.Empty;
        
    }
}
