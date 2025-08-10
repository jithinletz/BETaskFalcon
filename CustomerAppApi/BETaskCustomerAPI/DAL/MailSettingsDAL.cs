using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using BETaskCustomerAPI.Models;
using System.Data.SqlClient;
using System.Data;
using BETaskAPI.Common;

namespace BETaskAPI.DAL
{

    public class MailSettingsDAL
    {
        SqlServerDBConnectionLogic SqlServerDBConnectionLogic = new SqlServerDBConnectionLogic();
        public MailSettings GetMailSettings(string company, string location)
        {
            MailSettings objMailSettings = new MailSettings();
            SqlServerDBConnectionLogic.SqlServerDBConnection(company, location);
            try
            {
                SqlCommand cmd = new SqlCommand("APP_GetMailSetting", SqlServerDBConnectionLogic.Maincn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            objMailSettings.mail_id = Convert.ToInt32(reader["mail_id"]);
                            objMailSettings.from_mail = Convert.ToString(reader["from_mail"]);
                            objMailSettings.password = Convert.ToString(reader["password"]);
                            objMailSettings.to_mail = Convert.ToString(reader["to_mail"]);
                            objMailSettings.cc1 = Convert.ToString(reader["cc1"]);
                            objMailSettings.cc2 = Convert.ToString(reader["cc2"]);
                            objMailSettings.bcc1 = Convert.ToString(reader["bcc1"]);
                            objMailSettings.bcc2 = Convert.ToString(reader["bcc2"]);
                            objMailSettings.mail_subject = Convert.ToString(reader["mail_subject"]);
                            objMailSettings.smtp_host = Convert.ToString(reader["smtp_host"]);
                            objMailSettings.smtp_port = Convert.ToInt32(reader["smtp_port"]);
                            objMailSettings.smtp_timeout = Convert.ToInt32(reader["smtp_timeout"]);
                            objMailSettings.smtp_use_deafaultcredential = Convert.ToInt32(reader["smtp_use_deafaultcredential"]);
                            objMailSettings.enable_ssl = Convert.ToInt32(reader["enable_ssl"]);

                        }
                    }
                }


            }
            catch(Exception ex)
            {
                Logger.Error($"Mail configuration GetMailSettings  {ex.Message}");
            }
            return objMailSettings;
        }

        public static bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public void SendEmail(StringBuilder EmailMessageContent, MailSettings mailSett, string EmailHead = "", string Email = "", string FilePath = "")
        {
            if (CheckNet())
            {
                using (MailMessage mm = new MailMessage())
                {
                    // var mailSett = objMailSettings;
                    mm.Subject = $" {mailSett.mail_subject}   { EmailHead} ";
                    if (FilePath.Length > 0)//for future use
                    {
                        string html = " <br /> Please find the attached document (" + EmailHead + ") ";
                        StringBuilder sb = new StringBuilder();
                        string tab = "\t";
                        sb.AppendLine("<html>");
                        sb.AppendLine(tab + "<body>");
                        sb.Append("<h1><br /> Please find the attached  (" + EmailHead + ") </h1> ");
                        sb.AppendLine(tab + "</body>");
                        sb.AppendLine("</html>");
                        mm.Body = sb.ToString();
                    }
                    else
                    {
                        mm.Body = EmailMessageContent.ToString();
                    }
                    string[] email;
                    if (Email.Length > 0)
                    {
                        email = Email.Split(',');
                    }
                    else
                    {
                        email = mailSett.to_mail.Split(',');
                    }

                    for (int i = 0; i < email.Length; i++)
                    {
                        if (email[i] != string.Empty)
                        {
                            mm.To.Add(email[i].ToLower().ToString());
                        }
                    }

                    if (!String.IsNullOrEmpty(mailSett.cc1))
                    {
                        mm.CC.Add(mailSett.cc1);
                    }
                    if (!String.IsNullOrEmpty(mailSett.cc2))
                    {
                        mm.CC.Add(mailSett.cc1);
                    }
                    if (!String.IsNullOrEmpty(mailSett.bcc1))
                    {
                        mm.Bcc.Add(mailSett.bcc1);
                    }
                    if (!String.IsNullOrEmpty(mailSett.bcc2))
                    {
                        mm.Bcc.Add(mailSett.bcc2);
                    }

                    try
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = mailSett.smtp_host;
                        smtp.EnableSsl = mailSett.enable_ssl == 1 ? true : false;
                        //smtp.TargetName = "STARTTLS/smtp.gmail.com";
                        NetworkCredential NetworkCred = new NetworkCredential(mailSett.from_mail, mailSett.password);
                        mm.From = new MailAddress(mailSett.from_mail);
                        mm.IsBodyHtml = true;
                        //Attachment att = new Attachment(FilePath);
                        // mm.Attachments.Add(att);
                        smtp.UseDefaultCredentials = mailSett.smtp_use_deafaultcredential == 1 ? true : false; ;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = mailSett.smtp_port;
                        smtp.Timeout = mailSett.smtp_timeout;
                        smtp.Send(mm);
                        mm.Dispose();
                        //email send

                    }
                    catch (SmtpException ee)
                    {
                        // "Mail configuration missing";}
                        Logger.Error($"Mail configuration missing {ee.ToString()}");
                    }
                }
            }

        }
        public void EmailError(string message, string error, bool MessageEmail,MailSettings mailSett,string location)
        {
            
            using (MailMessage mm = new MailMessage())
            {

                string Email = "";
                if (!String.IsNullOrEmpty(mailSett.bcc2))
                {
                    Email = mailSett.bcc2;
                }
                else
                {
                    Email = "mail@letzservices.com";
                }
                try
                {
                    mm.Subject = mailSett.mail_subject + $"{message} BETask customer app error from {location} - {DateTime.Today} ";
                    //mm.Body = body + "<br /> FROM : " + HttpContext.Current.Session["cleintip"].ToString() + "<br />USER : " + HttpContext.Current.Session["username"].ToString() + "<br /><hr /><br /><br /><b>* Please do not reply to this message. Replies to this message are routed to an unmonitored mailbox. If you have questions please contact Your IT Team.</b>";
                    string html = " <br /> Please check the error ";
                    StringBuilder sb = new StringBuilder();
                    string tab = "\t";
                    sb.AppendLine("<html>");
                    sb.AppendLine(tab + "<body>");
                    sb.Append("<h1><br /> " + error + "  </h1> ");
                    sb.AppendLine(tab + "</body>");
                    sb.AppendLine("</html>");
                    mm.Body = sb.ToString();
                    mm.To.Add("jithin@letzservices.com");

                    try
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = mailSett.smtp_host;
                        smtp.EnableSsl = mailSett.enable_ssl == 1 ? true : false;
                        NetworkCredential NetworkCred = new NetworkCredential(mailSett.from_mail, mailSett.password);
                        mm.From = new MailAddress(mailSett.from_mail);
                        mm.IsBodyHtml = true;
                        // Attachment att = new Attachment(FilePath);
                        //mm.Attachments.Add(att);
                        smtp.UseDefaultCredentials = mailSett.smtp_use_deafaultcredential == 1 ? true : false; ;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = mailSett.smtp_port;
                        smtp.Timeout = mailSett.smtp_timeout;
                        smtp.Send(mm);
                        mm.Dispose();

                    }
                    catch (SmtpException ee)
                    {
                        Logger.Error(ee.Message);
                    }
                }
                catch (Exception ee)
                {
                    Logger.Error(ee.Message);
                }
            }
        }

    }
}