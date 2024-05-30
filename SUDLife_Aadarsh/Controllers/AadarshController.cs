using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.Models.Response;
using SUDLife_CallThirdPartyAPI;
using SUDLife_SecruityMechanism;

namespace SUDLife_Aadarsh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadarshController : ControllerBase
    {
        private IConfiguration config { get; set; }
        private readonly ThirdPartyAPI thirdPartyAPI;
        private readonly ClsCryptography clsCryptography;
        
        public AadarshController(IConfiguration configuration)
        {
            this.config = configuration;
        }

        [HttpPost("Aadarsh")]
        public async Task<IActionResult> CallThirdParty([FromBody] ClsAadarshPlainRequest request)
        {
            // Third party API URL
            string url = "https://siapi.sudlife.in/nsureservices.svc/generatebiapi";

            // Serialize request to JSON
            string jsonBody = System.Text.Json.JsonSerializer.Serialize(request);
            
            // Calling the third-party API
            var response = thirdPartyAPI.ClientAPI(url, Method.Post, jsonBody);

            // Map the response to your response model
            var apiResponse = new ClsAadarshPlainResponse
            {
                
            };

            return Ok(apiResponse);
        }
    }
}
