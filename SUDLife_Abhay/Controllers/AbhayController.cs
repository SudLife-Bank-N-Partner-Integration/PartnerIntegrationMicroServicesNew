using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SUDLife_Abhay.Models.Request;
using SUDLife_Abhay.Models.Response;
using SUDLife_Abhay.ServiceLayer;
using SUDLife_SecruityMechanism;
using Microsoft.AspNetCore.Authorization;

namespace SUDLife_Abhay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbhayController : ControllerBase
    {
        private readonly ClsAbhay _clsAbhay;
        private readonly ClsSecurityMech _SecurityMech;
        private readonly ILogger<AbhayController> _logger;
        private readonly IConfiguration? _configuration;
        public AbhayController(ClsAbhay clsAbhay, ClsSecurityMech clsSecurityMech, ILogger<AbhayController> logger, IConfiguration configuration)
        {
            this._clsAbhay = clsAbhay;
            this._SecurityMech = clsSecurityMech;
            this._logger = logger;
            this._configuration = configuration;
        }
        [Authorize]
        [HttpPost("Abhay")]
        public async Task<IActionResult> CallThirdParty([FromBody] ClsAbhayEncryptedRequest request)
        {
            try
            {
                _logger.LogInformation("Received request in Abhay action");
                string PlainRequestBody = string.Empty;
                string PlainResponseBody = string.Empty;
                string EncryptResponseBody = string.Empty;
                ClsAbhayEncryptedResponse objEncResponse=new ClsAbhayEncryptedResponse();
                ClsAbhayPlainResponse ObjAbhayResponse=new ClsAbhayPlainResponse(); 
                string SecreteKey = _configuration.GetSection("URLS:SecreteKey").Value;
                if (request.EncryptReqSign != null && request.EncryptReqSign != "")
                {
                    PlainRequestBody = _SecurityMech.Decrypt(request.EncryptReqSign, SecreteKey);
                }
                ClsAbhayPlainRequest _abhayRequest = JsonConvert.DeserializeObject<ClsAbhayPlainRequest>(PlainRequestBody);
                ObjAbhayResponse = await _clsAbhay.AbhayDetails(_abhayRequest);
                PlainResponseBody = JsonConvert.SerializeObject(ObjAbhayResponse);
                EncryptResponseBody = _SecurityMech.Encrypt(PlainResponseBody, SecreteKey);

                objEncResponse = new ClsAbhayEncryptedResponse((int)StatusCodes.Status200OK, request.Source, EncryptResponseBody);

                return Ok(objEncResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured in Aashirwaad action");
                return BadRequest(ex.Message);
            }
        }  
    }
}
