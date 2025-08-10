using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskAPI.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public int UserType { get; set; }
    }
}