using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;



namespace PaymentGatewayApi.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("api/CCAvenue/Test")]
        public IHttpActionResult Test()
        {
            return Ok("Success");
        }

        [HttpPost]
        [Route("api/CCAvenue/GetStatusCheckHash")]
        public IHttpActionResult GetStatusCheckHash([FromBody] string request)
        {
            try
            {

                CCAvenueEncryption cCAvenueEncryption = new CCAvenueEncryption();
                var resp = cCAvenueEncryption.GenerateStatusCheckHash(request);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return Ok("Error");
            }

        }

    }
}