using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SUDLife_AssuredIncome.Model.Request;
using SUDLife_AssuredIncome.Model.Response;
using SUDLife_AssuredIncome.ServiceLayer;
using SUDLife_SecruityMechanism;

namespace SUDLife_AssuredIncome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuredIncomeController : ControllerBase
    {
            private readonly ClsAssuredIncome _clsAssuredIncome;
            private readonly ClsSecurityMech _SecurityMech;
            private readonly ILogger<AssuredIncomeController> _logger;
            private readonly IConfiguration? _configuration;
            public AssuredIncomeController(ClsAssuredIncome clsAssuredIncome, ClsSecurityMech clsSecurityMech, ILogger<AssuredIncomeController> logger, IConfiguration configuration)
            {
                this._clsAssuredIncome = clsAssuredIncome;
                this._SecurityMech = clsSecurityMech;
                this._logger = logger;
                this._configuration = configuration;


            }

            [Authorize]
            [HttpPost("AssuredIncome")]
            public async Task<IActionResult> AssuredIncome([FromBody] ClsAssuredIncomeEncryptedRequest request)
            {
                try
                {
                    _logger.LogInformation("Received request in AssuredIncome action");
                    string PlainRequestBody = string.Empty;
                    string PlainResponseBody = string.Empty;
                    string EncryptResponseBody = string.Empty;
                    ClsAssuredIncomeEncryptedResponse objEncResponse = new ClsAssuredIncomeEncryptedResponse();
                    ClsAssuredIncomePlainResponse ObjAssuredIncomeResponse = new ClsAssuredIncomePlainResponse();
                    string SecreteKey = _configuration.GetSection("URLS:SecreteKey").Value;
                    if (request.EncryptReqSign != null && request.EncryptReqSign != "")
                    {
                        PlainRequestBody = _SecurityMech.Decrypt(request.EncryptReqSign, SecreteKey);
                    }
                    ClsAssuredIncomePlainRequest _AssuredIncomeRequest = JsonConvert.DeserializeObject<ClsAssuredIncomePlainRequest>(PlainRequestBody);
                    ObjAssuredIncomeResponse = await _clsAssuredIncome.AssuredIncomeDetails(_AssuredIncomeRequest);
                    PlainResponseBody = JsonConvert.SerializeObject(ObjAssuredIncomeResponse);
                    EncryptResponseBody = _SecurityMech.Encrypt(PlainResponseBody, SecreteKey);

                    objEncResponse = new ClsAssuredIncomeEncryptedResponse((int)StatusCodes.Status200OK, request.Source, EncryptResponseBody);

                    return Ok(objEncResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occured in AssuredIncome action");
                    return BadRequest(ex.Message);
                }

            }
        }
    }
