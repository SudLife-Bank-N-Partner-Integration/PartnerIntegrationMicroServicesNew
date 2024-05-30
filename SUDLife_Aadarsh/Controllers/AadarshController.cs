using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.Models.Response;
using SUDLife_Aadarsh.ServiceLayer;
using SUDLife_CallThirdPartyAPI;

namespace SUDLife_Aadarsh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadarshController : ControllerBase
    {
        private IConfiguration config { get; set; }
        private readonly ThirdPartyAPI _thirdPartyAPI;
        private readonly ClsAadarsh _clsAadarsh;

        public AadarshController(IConfiguration configuration, ThirdPartyAPI thirdPartyAPI, ClsAadarsh clsAadarsh)
        {
            this.config = configuration;
            this._thirdPartyAPI = thirdPartyAPI;
            this._clsAadarsh = clsAadarsh;
        }

        [HttpPost("Aadarsh")]
        public async Task<IActionResult> CallThirdParty([FromBody] ClsAadarshPlainRequest request)
        {
            ClsSDEBasePremiumRequest root = await _clsAadarsh.AadarshDetails(request);

            ClsAadarshPlainResponse _AadarshResponse = new ClsAadarshPlainResponse();
            // Third party API URL
            string url = "https://siapi.sudlife.in/nsureservices.svc/generatebiapi";

            // Serialize request to JSON
            string jsonBody = System.Text.Json.JsonSerializer.Serialize(root);
            var APIRequest = jsonBody.Replace("null", "[]");

            // Calling the third-party API
            dynamic ResponseFromNsureservice = _thirdPartyAPI.ClientAPI(url, Method.Post, jsonBody);

            // Map the response to your response model
            if (!string.IsNullOrEmpty(ResponseFromNsureservice))
            {
                var jsonString2 = JsonConvert.DeserializeObject<ClsSDEBasePremiumResponse>(ResponseFromNsureservice);
                if (jsonString2.Status == "Fail" || jsonString2.BIJson == null)
                {
                    _AadarshResponse.Status = jsonString2.Status;
                    _AadarshResponse.Message = jsonString2.Message;
                    _AadarshResponse.TransactionId = jsonString2.TransactionId;

                    if (jsonString2.InputValidationStatus != null)
                    {
                        if (jsonString2.InputValidationStatus[0].IpKwMessage.Count > 0)
                        {
                            _AadarshResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].IpKwMessage[0];
                        }
                        else
                        {
                            _AadarshResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].GeneralError;
                        }
                    }

                }
                else
                {
                    var InputValidationStatus = jsonString2.InputValidationStatus;
                    _AadarshResponse.ModalPremium = Math.Round(InputValidationStatus[0].ModalPremium);
                    _AadarshResponse.Tax = Math.Round(InputValidationStatus[0].Tax);
                    _AadarshResponse.ModalPremiumwithTax = Math.Round((_AadarshResponse.ModalPremium + _AadarshResponse.Tax));

                    _AadarshResponse.Message = jsonString2.Message;
                    _AadarshResponse.Status = jsonString2.Status;
                    _AadarshResponse.TransactionId = jsonString2.TransactionId;


                }

            }
            else
            {
                //fail response
                _AadarshResponse.Status = "Failure - Service Calling";
                _AadarshResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";
            }

            return Ok(_AadarshResponse);
        }
    }
}
