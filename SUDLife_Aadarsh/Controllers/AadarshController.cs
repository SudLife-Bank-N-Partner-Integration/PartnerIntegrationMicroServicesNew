using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SUDLife_Aadarsh.Datalayer;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.Models.Response;
using SUDLife_Aadarsh.ServiceLayer;
using SUDLife_CallThirdPartyAPI;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SUDLife_Aadarsh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadarshController : ControllerBase
    {
        private readonly ILogger<AadarshController> _logger;
        private IConfiguration config { get; set; }
        private readonly ThirdPartyAPI _thirdPartyAPI;
        private readonly ClsAadarsh _clsAadarsh;

        public AadarshController(
            IConfiguration configuration,
            ThirdPartyAPI thirdPartyAPI,
            ClsAadarsh clsAadarsh,
            ILogger<AadarshController> logger)
        {
            this.config = configuration;
            this._thirdPartyAPI = thirdPartyAPI;
            this._clsAadarsh = clsAadarsh;
            this._logger = logger;
        }

        [HttpPost("Aadarsh")]
        public async Task<IActionResult> CallThirdParty([FromBody] ClsAadarshPlainRequest request)
        {
            _logger.LogInformation("Received request in CallThirdParty action");

            try
            {
                //UpdateLog();

                ClsSDEBasePremiumRequest root = await _clsAadarsh.AadarshDetails(request);
                ClsAadarshPlainResponse _AadarshResponse = new ClsAadarshPlainResponse();

                string url = "https://siapi.sudlife.in/nsureservices.svc/generatebiapi";

                string jsonBody = System.Text.Json.JsonSerializer.Serialize(root);
                var APIRequest = jsonBody.Replace("null", "[]");

                dynamic ResponseFromNsureservice = _thirdPartyAPI.ClientAPI(url, Method.Post, jsonBody);

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
                    _AadarshResponse.Status = "Failure - Service Calling";
                    _AadarshResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";
                }

                return Ok(_AadarshResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CallThirdParty action");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        public void UpdateLog()
        {
            UpdateLogs logs = new UpdateLogs();
            logs.ExecuteProc();
            _logger.LogInformation("Updated logs successfully");
        }
    }
}
