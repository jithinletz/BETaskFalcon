using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BETaskCustomerAPI.Models
{
    public class MailSettings
    {
       
            public int mail_id { get; set; }
            public string from_mail { get; set; }
            public string to_mail { get; set; }
            public string password { get; set; }
            public string cc1 { get; set; }
            public string cc2 { get; set; }
            public string bcc1 { get; set; }
            public string bcc2 { get; set; }
            public string smtp_host { get; set; }
            public int enable_ssl { get; set; }
            public int smtp_port { get; set; }
            public int smtp_timeout { get; set; }
            public int smtp_use_deafaultcredential { get; set; }
            public string mail_subject { get; set; }
        
    }
}