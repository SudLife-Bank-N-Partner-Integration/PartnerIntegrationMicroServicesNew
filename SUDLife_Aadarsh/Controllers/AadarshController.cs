﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.Models.Response;
using SUDLife_Aadarsh.ServiceLayer;
using SUDLife_SecruityMechanism;
using System;
using System.Threading.Tasks;

namespace SUDLife_Aadarsh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadarshController : ControllerBase
    {
        private readonly ILogger<AadarshController> _logger;
        private readonly ClsSecurityMech _securityMech;
        private readonly IConfiguration _configuration;
        private readonly ClsAadarsh _clsAadarsh;

        public AadarshController(
            IConfiguration configuration,
            ClsAadarsh clsAadarsh,
            ClsSecurityMech securityMech,
            ILogger<AadarshController> logger)
        {
            _configuration = configuration;
            _clsAadarsh = clsAadarsh;
            _securityMech = securityMech;
            _logger = logger;
        }

        [HttpPost("Aadarsh")]
        public async Task<IActionResult> Aadarsh([FromBody] ClsAadarshEncryptedRequest request)
        {
            string encryptedRequestBody = string.Empty;
            string plainRequestBody = string.Empty;
            string plainResponseBody = string.Empty;
            string encryptedResponseBody = string.Empty;

            try
            {
                _logger.LogInformation("Received encrypted request");

                // Serialize and log the encrypted request
                encryptedRequestBody = JsonConvert.SerializeObject(request);

                if (!string.IsNullOrEmpty(request.EncryptReqSign))
                {
                    _logger.LogInformation("Decrypting request body");
                    string secretKey = _configuration.GetSection("URLS:SecreteKey").Value;
                    plainRequestBody = _securityMech.Decrypt(request.EncryptReqSign, secretKey);
                }

                // Deserialize the plain request
                var aadarshRequest = JsonConvert.DeserializeObject<ClsAadarshPlainRequest>(plainRequestBody);

                _logger.LogInformation("Processing request");
                var aadarshResponse = await _clsAadarsh.AadarshDetails(aadarshRequest);

                // Serialize the plain response
                plainResponseBody = JsonConvert.SerializeObject(aadarshResponse);

                _logger.LogInformation("Encrypting response body");
                if (!string.IsNullOrEmpty(plainResponseBody))
                {
                    string secretKey = _configuration.GetSection("URLS:SecreteKey").Value;
                    encryptedResponseBody = _securityMech.Encrypt(plainResponseBody, secretKey);
                }

                var encryptedResponse = new ClsAadarshEncryptedResponse((int)StatusCodes.Status200OK, request.Source, encryptedResponseBody);

                // Log enriched context without affecting the main message
                Log.ForContext("EncryptedRequest", encryptedRequestBody)
                   .ForContext("PlainRequest", plainRequestBody)
                   .ForContext("PlainResponse", plainResponseBody)
                   .ForContext("EncryptedResponse", encryptedResponseBody)
                   .Information("Processed Aadarsh request successfully");

                return Ok(encryptedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");

                // Log enriched context for error without affecting the main message
                Log.ForContext("EncryptedRequest", encryptedRequestBody)
                   .ForContext("PlainRequest", plainRequestBody)
                   .ForContext("PlainResponse", plainResponseBody)
                   .ForContext("EncryptedResponse", encryptedResponseBody)
                   .Error(ex, "Failed to process Aadarsh request");

                return BadRequest(ex.Message);
            }
        }
    }
}
