using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class LogIn
    {    
       
        public string Company { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Version { get; set; } = "V.0.0.0";
    }
}