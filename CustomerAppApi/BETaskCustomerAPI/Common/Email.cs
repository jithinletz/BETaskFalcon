using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BETaskCustomerAPI.Common
{
    public class Email
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailToName"></param>
        /// <param name="emailToAddress"></param>
        public static void  SendPasswordResetEmail(string emailToName, string emailToAddress, string OtpCode) {
            try
            {
               
                const string Subject = "Falcon : One-Time Password (OTP)";
                string emailBody = $@"
                    <html>
                    <body>
                        <p>Dear Customer,</p>
                        <p>Your One-Time Password (OTP) is: {OtpCode}</p>
                        <p>Please use this OTP to proceed with your authentication or verification process.</p>
                        <p>If you did not request this OTP or have any concerns, please disregard this email.</p>
                        <br>
                        <p>Best regards,</p>
                        <p>Falcon</p>
                    </body>
                    </html>"; 
                 SendEmail(emailToName, emailToAddress, Subject, emailBody);

            }
            catch (Exception ex){
                throw new Exception(ex.ToString());
            }
         
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailToName"></param>
        /// <param name="emailToAddress"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailContent"></param>
        /// <returns></returns>
        public static async Task SendEmail(string emailToName ,string emailToAddress,string emailSubject,string emailContent)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = ConfigurationManager.AppSettings["EmailAPIURL"]; ; // Replace with the actual API endpoint URL
                    string authToken = ConfigurationManager.AppSettings["EmailAPiKey"]; ; // Replace with your authentication token

                    // Set the authorization header
                    client.DefaultRequestHeaders.Add("api-key", authToken);

                    // Create the email payload
                    var payload = new
                    {
                        sender = new
                        {
                            name = "Falcon",
                            email = "support@falconbottledwater.com"
                        },
                        to = new[]
                        {
                    new
                    {
                        email = emailToAddress,
                        name = emailToName
                    }
                },
                        subject = emailSubject,
                        htmlContent = emailContent
                    };

                    // Convert the payload to JSON
                    string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    // Create the HTTP request content
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Email sent successfully");
                    }
                    else
                    {
                        Console.WriteLine("Email sending failed with status code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
          
        }
    }
}