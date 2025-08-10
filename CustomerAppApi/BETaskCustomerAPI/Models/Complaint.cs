using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public string Mobile { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string ComplaintType { get; set; }
        public int Status { get; set; }
      
    }
}