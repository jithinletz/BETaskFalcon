using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BETaskAPI
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        public string From { get; set; } = ConfigurationManager.AppSettings["FromEmail"].Trim();
        public string AppPassword { get; set; } = ConfigurationManager.AppSettings["AppPassword"].Trim();
        public List<string> To { get; set; }
        public List<string> Cc { get; set; } =string.IsNullOrEmpty( ConfigurationManager.AppSettings["MailCC"].Trim())?null: ConfigurationManager.AppSettings["MailCC"].Trim().Split(';').ToList();
        public List<string> Bcc { get; set; }
        public string smtpAddress { get; set; } = ConfigurationManager.AppSettings["SmtpAddress"].Trim();
        public int smtpPort { get; set; } = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
        public List<string> AttachmentFiles { get; set; }
        bool invalid = false;




        public Email()
        {
        }
        public Email(Email mail)
        {
            To = mail.To;
            Subject = mail.Subject;
            Body = mail.Body + ConfigurationManager.AppSettings["EmailSignature"];
            ValidateEmails();
        }

        private void ValidateEmails()
        {
            if (To != null && To.Count > 0)
            {
                foreach (string email in To.ToList())
                {
                    bool isValid = IsValidEmail(email);
                    if (!isValid)
                        To.Remove(email);
                }
            }

            if (Cc != null && Cc.Count > 0)
            {
                foreach (string email in Cc.ToList())
                {
                    bool isValid = IsValidEmail(email);
                    if (!isValid)
                        Cc.Remove(email);
                }
            }
        }

        public bool IsValidEmail(string strIn)
        {
            try
            {
                if (String.IsNullOrEmpty(strIn))
                    return false;

                // Use IdnMapping class to convert Unicode domain names.
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
                if (invalid)
                    return false;

                // Return true if strIn is in valid e-mail format.
                return Regex.IsMatch(strIn,
                       @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                       RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }

        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        public void Send()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                var client = new SmtpClient(smtpAddress, smtpPort)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(From, AppPassword)
                };



                var message = new MailMessage
                {
                    //                    Sender = new MailAddress(From, From),
                    From = new MailAddress(From, ConfigurationManager.AppSettings["DisplayFrom"].Trim()),
                    Subject = Subject,
                    Body = Body,
                    IsBodyHtml = true,
                    //  Priority = MailPriority.High
                };

                if (To != null)
                    AddDestinataryToList(To, message.To);
                if (Cc != null)
                    AddDestinataryToList(Cc, message.CC);
                if (Bcc != null)
                    AddDestinataryToList(Bcc, message.Bcc);

                if (AttachmentFiles != null && AttachmentFiles.Count > 0)
                {
                    var attachments = AttachmentFiles.Select(file => new Attachment(file));
                    foreach (var attachment in attachments)
                        message.Attachments.Add(attachment);
                }
                client.Send(message);

            }
            catch (System.Exception ex)
            {

                throw;
            }

        }

        private void AddDestinataryToList(IEnumerable<string> from,
         ICollection<MailAddress> mailAddressCollection)
        {
            foreach (var destinatary in from)
                mailAddressCollection.Add(new MailAddress(destinatary, destinatary));
        }
    }
}
