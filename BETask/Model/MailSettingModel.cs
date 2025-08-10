using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETask.Model
{
   public static class MailSettingModel
    {
        public static int mail_id { get; set; }
        public static string from_mail { get; set; }
        public static string password { get; set; }
        public static string cc1 { get; set; }
        public static string cc2 { get; set; }
        public static string bcc1 { get; set; }
        public static string bcc2 { get; set; }
        public static string smtp_host { get; set; }
        public static int enable_ssl { get; set; }
        public static int smtp_port { get; set; }
        public static int smtp_timeout { get; set; }
        public static int smtp_use_deafaultcredential { get; set; }
    }
}
