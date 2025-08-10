using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CCA.Util;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;


namespace PaymentGatewayApi
{
    public class CCAvenueEncryption
    {
        readonly string encryptionKey=ConfigurationManager.AppSettings["CCAvenueEncryptKeyAbu"];
        public string GenerateStatusCheckHash(string referenceNo)
        {
            CCACrypto ccaCrypto = new CCACrypto();
            var jsonData = new
            {
                reference_no = referenceNo
            };
            string data = JsonConvert.SerializeObject(jsonData);
            string strEncRequest = ccaCrypto.Encrypt(data, encryptionKey);
            return strEncRequest;
        }
    }
}