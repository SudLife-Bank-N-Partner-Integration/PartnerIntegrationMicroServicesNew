using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SUDLife_Aashirwaad.Model.Request;
using SUDLife_Aashirwaad.Model.Response;
using SUDLife_Aashirwaad.ServiceLayer;
using SUDLife_SecruityMechanism;
using System.Transactions;

namespace SUDLife_Aashirwaad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AashirwaadController : ControllerBase
    {
        private readonly ClsAashirwaad _clsAashirwaad;
        private readonly ClsSecurityMech _SecurityMech;
        private readonly ILogger<AashirwaadController> _logger;
        private readonly IConfiguration? _configuration;
        public AashirwaadController(ClsAashirwaad clsAashirwaad, ClsSecurityMech clsSecurityMech, ILogger<AashirwaadController> logger, IConfiguration configuration) {
            this._clsAashirwaad=clsAashirwaad;
            this._SecurityMech=clsSecurityMech;
            this._logger=logger;
            this._configuration=configuration;


        }

        [Authorize]
        [HttpPost("Aashirwaad")]
        public async Task<IActionResult> Aashirwaad([FromBody] ClsAashirwaadEncryptedRequest request)
        {
            try
            {
                _logger.LogInformation("Received request in Aashirwaad action");
                string PlainRequestBody = string.Empty;
                string PlainResponseBody = string.Empty;
                string EncryptResponseBody = string.Empty;
                ClsAashirwaadEncryptedResponse objEncResponse=new ClsAashirwaadEncryptedResponse();
                ClsAashirwaadPlainResponse ObjAashirwaadResponse = new ClsAashirwaadPlainResponse();
                string SecreteKey = _configuration.GetSection("URLS:SecreteKey").Value;
                if (request.EncryptReqSign != null && request.EncryptReqSign != "")
                {
                    PlainRequestBody = _SecurityMech.Decrypt(request.EncryptReqSign, SecreteKey);
                }
                ClsAashirwaadPlainRequest _aashirwaadRequest = JsonConvert.DeserializeObject<ClsAashirwaadPlainRequest>(PlainRequestBody);
                ObjAashirwaadResponse=await _clsAashirwaad.AashirwaadDetails(_aashirwaadRequest);
                PlainResponseBody = JsonConvert.SerializeObject(ObjAashirwaadResponse);
                EncryptResponseBody = _SecurityMech.Encrypt(PlainResponseBody, SecreteKey);

                objEncResponse = new ClsAashirwaadEncryptedResponse((int)StatusCodes.Status200OK, request.Source, EncryptResponseBody);

                return Ok(objEncResponse);
            }
            catch (Exception ex) {
                _logger.LogError("An error occured in Aashirwaad action");
                return BadRequest(ex.Message);
            }
            
        }
    }   
}
