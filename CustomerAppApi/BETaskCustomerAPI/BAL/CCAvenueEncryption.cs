using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CCA.Util;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using BETaskCustomerAPI.Models;
using BETaskAPI.Common;
using System.Collections.Generic;

namespace BETaskCustomerAPI.BAL
{
    public class CCAvenueEncryption
    {

        private readonly string workingKey = "";
        private readonly string strAccessCode = "";
        private readonly string ccAvenueBaseUrl = "";
        private readonly string merchantId = "";
        private readonly string currency = "";
        private readonly string encryptionKeyAbu = "";
        private readonly string accesCodeAu = "";
        private readonly string version = "1.1";
        private readonly string orderStatusCommand = "orderStatusTracker";
        private readonly string orderStatusType = "JSON";



        public CCAvenueEncryption()
        {
            ccAvenueBaseUrl = ConfigurationManager.AppSettings["CCAvenueBaseUrl"];
            strAccessCode = ConfigurationManager.AppSettings["CCAvenueAccessCode"];
            workingKey = ConfigurationManager.AppSettings["CCAvenueWorkingKey"];
            merchantId = ConfigurationManager.AppSettings["CCAvenueMerchentId"];
            currency = ConfigurationManager.AppSettings["Currency"];
            encryptionKeyAbu = ConfigurationManager.AppSettings["CCAvenueEncryptKeyAbu"];
            accesCodeAu = ConfigurationManager.AppSettings["CCAvenueAccessCodeAbu"];

        }


        public async Task<EncResponseParams> GenerateEncRequest(decimal amount)
        {
            EncResponseParams encResponseParams = new EncResponseParams();
            try
            {
                encResponseParams.OrderId = GenerateUniqueString(20);
                encResponseParams.TId = Guid.NewGuid().ToString();
                string requestCode = GenerateEncRequestHash(encResponseParams.OrderId, encResponseParams.TId, currency, amount);

                using (HttpClient client = new HttpClient())
                {
                    // Define the data you want to send in the POST request
                    var data = new
                    {
                        access_code = strAccessCode,
                        encRequest = requestCode
                    };

                    // Convert the data to JSON
                    string json = JsonConvert.SerializeObject(data);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(ccAvenueBaseUrl + "appV1.do?command=generateTrackingId", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    GetTrackingIdFromResponse(responseBody, encResponseParams, amount);
                    encResponseParams.EncRequest = requestCode;


                }
                return encResponseParams;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to process {ex.Message}");
            }
        }
        private void GetTrackingIdFromResponse(string response, EncResponseParams encResponseParams, decimal amount)
        {

            using (JsonDocument doc = JsonDocument.Parse(response.ToString()))
            {
                JsonElement root = doc.RootElement;
                string status = root.GetProperty("status").GetString();
                string message = root.GetProperty("message").GetString();
                JsonElement data = root.GetProperty("data");
                string encResp = data.GetProperty("encResp").GetString();

                encResponseParams.Message = message;
                encResponseParams.Status = status;

                //Converting encrypted response to numeric tracking id
                var order = InitiateOrder(encResp);
                encResponseParams.TrackingId = order.TrackingId.ToString();
                encResponseParams.RequestHash = GenerateSHA512Hash(order.TrackingId.ToString(), order.Currency, order.Amount.ToString(), workingKey);
            }
        }



        private string GenerateEncRequestHash(string orderId, string tid, string currency, decimal amount)
        {
            CCACrypto ccaCrypto = new CCACrypto();
            var requestData = new
            {
                merchant_id = merchantId.ToString(),
                order_id = orderId,
                tid,
                currency,
                amount
            };

            string data = JsonConvert.SerializeObject(requestData);
            string strEncRequest = ccaCrypto.Encrypt(data, workingKey);
            return strEncRequest;
        }

        private Order InitiateOrder(string encryptedTrackingId)
        {
            CCACrypto ccaCrypto = new CCACrypto();
            var decryptedResponse = ccaCrypto.Decrypt(encryptedTrackingId, workingKey);
            Order order = System.Text.Json.JsonSerializer.Deserialize<Order>(decryptedResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return order;
        }

        public static string GenerateSHA512Hash(string trackingId, string currency, string amount, string workingKey)
        {
            // Concatenate the input strings
            string input = trackingId + currency + amount + workingKey;

            // Convert the input string to a byte array
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Compute the SHA-512 hash
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                // Convert the hash byte array to a hexadecimal string
                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }

                Logger.Info($"Tracking id is {trackingId} for working key {workingKey}");
                Logger.Info($"Hashkey input is {input}");
                Logger.Info($"Hashkey generated is {hashStringBuilder}\n------------------\n");

                return hashStringBuilder.ToString();
            }
        }

        static string GenerateUniqueString(int length)
        {
            // Ensure the length is positive and reasonable
            if (length <= 0) throw new ArgumentException("Length must be a positive number.");

            // Generate enough random bytes to ensure we get a long enough Base64 string
            int byteCount = (int)Math.Ceiling(length * 8.0 / 6.0); // 6 bits per Base64 character
            byte[] randomBytes = new byte[byteCount];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert the random bytes to a Base64 string
            string base64String = Convert.ToBase64String(randomBytes);

            // Remove any padding characters ('=', '+', '/') and convert to lowercase
            StringBuilder sb = new StringBuilder();
            foreach (char c in base64String)
            {
                if (c != '=' && c != '+' && c != '/')
                {
                    sb.Append(c);
                }
            }

            // Convert to lowercase and ensure the final string is exactly the desired length
            string cleanedString = sb.ToString().ToLower();

            // Check if the cleaned string is long enough
            if (cleanedString.Length < length)
            {
                // Generate additional random bytes if necessary
                while (cleanedString.Length < length)
                {
                    byte[] additionalBytes = new byte[byteCount];
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(additionalBytes);
                    }
                    string additionalString = Convert.ToBase64String(additionalBytes);
                    foreach (char c in additionalString)
                    {
                        if (c != '=' && c != '+' && c != '/')
                        {
                            sb.Append(c);
                        }
                    }
                    cleanedString = sb.ToString().ToLower();
                }
            }

            return cleanedString.Substring(0, length);
        }


        public OrderStatusModel DecryptStatus(string encResponse)
        {
            CCACrypto ccaCrypto = new CCACrypto();
            string strEncRequest = ccaCrypto.Decrypt(encResponse, encryptionKeyAbu);

            // Deserialize the decrypted JSON string into the OrderStatus class
            Logger.Info(strEncRequest);
            OrderStatusModel orderStatus = JsonConvert.DeserializeObject<OrderStatusModel>(strEncRequest);
            Logger.Info(orderStatus.OrderStatusValue);
            return orderStatus;
        }


        public async Task<OrderStatusModel> GenerateStatusCheckHashAsync(string referenceNo)
        {
            CCACrypto ccaCrypto = new CCACrypto();
            var jsonData = new
            {
                reference_no = referenceNo
            };
            string data = JsonConvert.SerializeObject(jsonData);
            string strEncRequest = ccaCrypto.Encrypt(data, encryptionKeyAbu);
            string encrptedResponse = await GetStatusResponseEncrypted(strEncRequest, accesCodeAu, orderStatusType, orderStatusCommand, version);
            
            return DecryptStatus(encrptedResponse) ;
        }
        private async Task<string> GetStatusResponseEncrypted(string encRequest, string accessCode, string requestType, string command, string version)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = "https://login.ccavenue.ae/apis/servlet/DoWebTrans";

                // Prepare the request payload
                var payload = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("enc_request", encRequest),
                    new KeyValuePair<string, string>("access_code", accessCode),
                    new KeyValuePair<string, string>("request_type", requestType),
                    new KeyValuePair<string, string>("command", command),
                    new KeyValuePair<string, string>("version", version)
                });

                Logger.Info($"Payment status : \n {encRequest} \n {accessCode} \n {requestType} \n {command} \n {version}");
                Logger.Info(payload.ReadAsStringAsync().ToString());
                // Send the POST request
                var response = await client.PostAsync(apiUrl, payload);

                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseBody = await response.Content.ReadAsStringAsync();

                // Extract enc_response parameter
                var responseParams = System.Web.HttpUtility.ParseQueryString(responseBody);
                var encResponse = responseParams["enc_response"];
                encResponse = encResponse?.Trim();
                return encResponse;
            }
        }


    }

}



