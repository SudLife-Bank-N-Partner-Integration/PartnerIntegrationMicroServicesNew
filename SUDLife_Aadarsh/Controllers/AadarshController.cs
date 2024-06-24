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
using SUDLife_DataRepo;
using SUDLife_SecruityMechanism;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SUDLife_Aadarsh.Controllers
{
    [Route("api/Aadarsh/[action]")]
    [ApiController]
    public class AadarshController : ControllerBase
    {
        private readonly ILogger<AadarshController> _logger;
        private readonly ClsSecurityMech _SecurityMech;
        private readonly IConfiguration? _configuration;
        private readonly ClsAadarsh _clsAadarsh;
        private readonly IExectueProcdure _execproc;

        public AadarshController(
            IConfiguration configuration,
            ClsAadarsh clsAadarsh,
            ClsSecurityMech SecurityMech,
            ILogger<AadarshController> logger,IExectueProcdure exectueProcdure)
        {
            this._configuration = configuration;
            this._clsAadarsh = clsAadarsh;
            this._SecurityMech = SecurityMech;
            this._logger = logger;
            _execproc = exectueProcdure;
        }

        [Authorize]
        [HttpPost("Aadarsh")]
        public async Task<IActionResult> Aadarsh([FromBody] ClsAadarshEncryptedRequest request)
        {
            try
            {
                _logger.LogInformation("Received request in Aadarsh action");
                string PlainRequestBody = string.Empty;
                string PlainResponseBody = string.Empty;
                string EncryptResponseBody = string.Empty;
                ClsAadarshEncryptedResponse objEncResponse = new ClsAadarshEncryptedResponse();
                ClsAadarshPlainResponse ObjAadarshResponse = new ClsAadarshPlainResponse();
                string SecreteKey = _configuration.GetSection("URLS:SecreteKey").Value;
                if (request.EncryptReqSign != null && request.EncryptReqSign != "")
                {
                    PlainRequestBody = _SecurityMech.Decrypt(request.EncryptReqSign, SecreteKey);
                }
                ClsAadarshPlainRequest _aadarshRequest = JsonConvert.DeserializeObject<ClsAadarshPlainRequest>(PlainRequestBody);
                ObjAadarshResponse = await _clsAadarsh.AadarshDetails(_aadarshRequest);
                PlainResponseBody = JsonConvert.SerializeObject(ObjAadarshResponse);
                EncryptResponseBody = _SecurityMech.Encrypt(PlainResponseBody, SecreteKey);

                objEncResponse = new ClsAadarshEncryptedResponse((int)StatusCodes.Status200OK, request.Source, EncryptResponseBody);

                return Ok(objEncResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured in Aadarsh action");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public void UpdateLogs()
        {
            UpdateLogs logs = new UpdateLogs(_execproc);
            logs.ExecuteProc();
        }

    }
}
