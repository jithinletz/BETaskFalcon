using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
    class CompanyModel
    {
        public int Company_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string POBox { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Web { get; set; }
        public string Tin { get; set; }
        public string Email { get; set; }
        public string Cloud_Connection { get; set; }
        public DateTime FinancialDateFrom { get; set; }
        public DateTime FinancialDateTo { get; set; }
    }
}
