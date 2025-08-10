using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskCustomerAPI.Models
{
    public abstract class AbstractCompanyBase
    {
        public string Company { get; set; }
        public string Location { get; set; }
    }
}