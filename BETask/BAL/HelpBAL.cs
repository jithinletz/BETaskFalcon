using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using BETask.Common;
using System.Drawing;

namespace BETask.BAL
{
    class HelpBAL
    {

        public  void Sharescreenshot()
        {
            try
            {
                //Creating a new Bitmap object
                Rectangle resolution = Screen.PrimaryScreen.Bounds;
                Bitmap captureBitmap = new Bitmap(resolution.Width, resolution.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                //capture our Current Screen

                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                //Creating a New Graphics Object

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                //Copying Image from The Screen

                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                //Saving the Image File (I am here Saving it in My E drive).
                captureBitmap.Save(@"" + Application.StartupPath + "\\Backup\\Capture.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //Displaying the Successfull Result
                EmailScreenshot(@"" + Application.StartupPath + "\\Backup\\Capture.jpg", true);
            }
            catch
            {

            }
        }
        public static bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public void EmailScreenshot(string FilePath, bool MessageEmail)
        {
            if (!CheckNet())
            {
                if (MessageEmail)
                {
                    MessageBox.Show("No Internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            BETask.DAL.DAL.CompanyDAL comp = new BETask.DAL.DAL.CompanyDAL();
            var mailSett = comp.GetMailSettings();
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
                    mm.Subject = mailSett.mail_subject + $"BETask ERP screenshot shared by {General.userName} for {General.companyName} {DateTime.Today}";
                    //mm.Body = body + "<br /> FROM : " + HttpContext.Current.Session["cleintip"].ToString() + "<br />USER : " + HttpContext.Current.Session["username"].ToString() + "<br /><hr /><br /><br /><b>* Please do not reply to this message. Replies to this message are routed to an unmonitored mailbox. If you have questions please contact Your IT Team.</b>";
                    string html = " <br /> Please find the attached screenshort ";
                    StringBuilder sb = new StringBuilder();
                    string tab = "\t";
                    sb.AppendLine("<html>");
                    sb.AppendLine(tab + "<body>");
                    sb.Append("<h1><br /> Please find the attached screenshort  </h1> ");
                    sb.AppendLine(tab + "</body>");
                    sb.AppendLine("</html>");
                    mm.Body = sb.ToString();
                    string[] email = Email.Split(',');
                    for (int i = 0; i < email.Length; i++)
                    {
                        if (email[i] != string.Empty)
                        {

                            //if (System.Text.RegularExpressions.Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                            {
                                mm.To.Add(email[i].ToLower().ToString());

                            }

                        }
                    }



                    try
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = mailSett.smtp_host;
                        smtp.EnableSsl = mailSett.enable_ssl == 1 ? true : false;
                        NetworkCredential NetworkCred = new NetworkCredential(mailSett.from_mail, mailSett.password);
                        mm.From = new MailAddress(mailSett.from_mail);
                        mm.IsBodyHtml = true;
                        Attachment att = new Attachment(FilePath);
                        mm.Attachments.Add(att);
                        smtp.UseDefaultCredentials = mailSett.smtp_use_deafaultcredential == 1 ? true : false; ;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = mailSett.smtp_port;
                        smtp.Timeout = mailSett.smtp_timeout;
                        smtp.Send(mm);
                        mm.Dispose();
                        if (MessageEmail)
                            MessageBox.Show("Screenshot shared", "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SmtpException ee)
                    {
                        if (MessageEmail)
                            MessageBox.Show(ee.Message, "Mail configuration missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }

        public void EmailError(string message,string error, bool MessageEmail)
        {
            if (!CheckNet())
            {
                if (MessageEmail)
                {
                    MessageBox.Show("No Internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            BETask.DAL.DAL.CompanyDAL comp = new BETask.DAL.DAL.CompanyDAL();
            var mailSett = comp.GetMailSettings();
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
                    mm.Subject = mailSett.mail_subject + $"{message} BETask error shared by {General.userName} for {General.companyName} {DateTime.Today} ";
                    //mm.Body = body + "<br /> FROM : " + HttpContext.Current.Session["cleintip"].ToString() + "<br />USER : " + HttpContext.Current.Session["username"].ToString() + "<br /><hr /><br /><br /><b>* Please do not reply to this message. Replies to this message are routed to an unmonitored mailbox. If you have questions please contact Your IT Team.</b>";
                    string html = " <br /> Please check the error ";
                    StringBuilder sb = new StringBuilder();
                    string tab = "\t";
                    sb.AppendLine("<html>");
                    sb.AppendLine(tab + "<body>");
                    sb.Append("<h1><br /> "+error+"  </h1> ");
                    sb.AppendLine(tab + "</body>");
                    sb.AppendLine("</html>");
                    mm.Body = sb.ToString();
                    string[] email = Email.Split(',');
                    for (int i = 0; i < email.Length; i++)
                    {
                        if (email[i] != string.Empty)
                        {

                            //if (System.Text.RegularExpressions.Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                            {
                                mm.To.Add(email[i].ToLower().ToString());

                            }

                        }
                    }



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
                        General.Error(ee.ToString());
                    }
                }
                catch (Exception ee)
                {

                }
            }
        }
    }
}
