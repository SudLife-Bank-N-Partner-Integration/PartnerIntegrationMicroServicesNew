using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SUDLife_GetApplicationNumber.Models.Request;
using SUDLife_GetApplicationNumber.Models.Response;
using SUDLife_CallThirdPartyAPI;


namespace SUDLife_GetApplicationNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAppNumberController : ControllerBase
    {
        private IConfiguration config { get; set; }
        private readonly ThirdPartyAPI thirdPartyAPI;

        public GetAppNumberController(IConfiguration configuration)
        {
            this.config = configuration;
        }

        [HttpPost("GetAppNumber")]
        public async Task<IActionResult> GetAppNumber([FromBody] ClsGetAppNumberPlainRequest request)
        {
            ClsGetAppNumberResponse appNumberResponse = new ClsGetAppNumberResponse();


            string? SourceName, ProductName, ProductType, RequestFromBnk, EncryptedReq, _TransactionId;
            SourceName = request.Source;
            ProductName = request.AppNoReq.ProductName;
            ProductType = request.AppNoReq.ProductType;
            RequestFromBnk = request.ToString();
            EncryptedReq = request.ToString();
            _TransactionId = "123";
            int SourceId = Convert.ToInt32(EnumSource.OneSilverBullet);
            appNumberResponse = SaveAppNoDataString(ProductName, ProductType, RequestFromBnk, EncryptedReq, _TransactionId, SourceId);

            // Map the response to your response model
            var apiResponse = appNumberResponse;
            return Ok(apiResponse);
        }

        public ClsGetAppNumberResponse SaveAppNoDataString(string? productName, string? producttype, string? requestFromBank, string? encryptReq, string? transactionId, int sourceId)
        {
            // code - Save request in database and return new appNo

            Random random = new Random();
            int number = random.Next(10000000, 99999999);
            ClsGetAppNumberResponse clsGetAppNumberResponse = new ClsGetAppNumberResponse();
            clsGetAppNumberResponse.ApplicationNo = number.ToString();
            return clsGetAppNumberResponse;
        }


    }
    public enum EnumSource
    {
        OneSilverBullet = 1,
        SUD = 2,
        Bimaplan = 3,
        BOI = 4,
        PolicyBazaar = 5,
        Scoreme = 6
    }

}
