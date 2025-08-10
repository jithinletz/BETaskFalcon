using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Company
    {  

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }       
        //public string Description { get; set; }
       // public string TIN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string POBox { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }        
        public string Web { get; set; }
       // public string Status { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
   
    }


}