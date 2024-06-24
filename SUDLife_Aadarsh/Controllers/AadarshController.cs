using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SUDLife_Aadarsh.Datalayer;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.Models.Response;
using SUDLife_CallThirdPartyAPI;
using SUDLife_DataRepo;

namespace SUDLife_Aadarsh.Controllers
{
    [Route("api/Aadarsh/[action]")]
    [ApiController]
    public class AadarshController : ControllerBase
    {
        private readonly IExectueProcdure _exectueProcdure;

        private readonly ThirdPartyAPI thirdPartyAPI;
        
        public AadarshController(IExectueProcdure proc)
        {
            _exectueProcdure = proc;
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

        [HttpGet]

        public void DB()
        {
            //DatabaseConnect databaseConnect = new DatabaseConnect();
            //var abc = databaseConnect.abc;

            UpdateLogs updateLogs = new UpdateLogs(_exectueProcdure);
            updateLogs.ExecuteProc();
        }
    }
}
