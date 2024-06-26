using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SUDLife_Akshay.Model.Request;
using SUDLife_Akshay.Model.Response;
using SUDLife_Akshay.ServiceLayer;
using SUDLife_SecruityMechanism;

namespace SUDLife_Akshay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkshayController : ControllerBase
    {
        private readonly ClsAkshay _clsAkshay;
        private readonly ClsSecurityMech _SecurityMech;
        private readonly ILogger<AkshayController> _logger;
        private readonly IConfiguration? _configuration;
        public AkshayController(ClsAkshay clsAkshay, ClsSecurityMech clsSecurityMech, ILogger<AkshayController> logger, IConfiguration configuration)
        {
            this._clsAkshay = clsAkshay;
            this._SecurityMech = clsSecurityMech;
            this._logger = logger;
            this._configuration = configuration;


        }

        [Authorize]
        [HttpPost("Akshay")]
        public async Task<IActionResult> Akshay([FromBody] ClsAkshayEncryptedRequest request)
        {
            try
            {
                _logger.LogInformation("Received request in Akshay action");
                string PlainRequestBody = string.Empty;
                string PlainResponseBody = string.Empty;
                string EncryptResponseBody = string.Empty;
                ClsAkshayEncryptedResponse objEncResponse = new ClsAkshayEncryptedResponse();
                ClsAkshayPlainResponse ObjAkshayResponse = new ClsAkshayPlainResponse();
                string SecreteKey = _configuration.GetSection("URLS:SecreteKey").Value;
                if (request.EncryptReqSign != null && request.EncryptReqSign != "")
                {
                    PlainRequestBody = _SecurityMech.Decrypt(request.EncryptReqSign, SecreteKey);
                }
                ClsAkshayPlainRequest _AkshayRequest = JsonConvert.DeserializeObject<ClsAkshayPlainRequest>(PlainRequestBody);
                ObjAkshayResponse = await _clsAkshay.AkshayDetails(_AkshayRequest);
                PlainResponseBody = JsonConvert.SerializeObject(ObjAkshayResponse);
                EncryptResponseBody = _SecurityMech.Encrypt(PlainResponseBody, SecreteKey);

                objEncResponse = new ClsAkshayEncryptedResponse((int)StatusCodes.Status200OK, request.Source, EncryptResponseBody);

                return Ok(objEncResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured in Akshay action");
                return BadRequest(ex.Message);
            }

        }
    }
}
