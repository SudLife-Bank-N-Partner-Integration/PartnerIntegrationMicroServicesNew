using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SUDLife_Authentication.Models.Request;
using SUDLife_Authentication.Models.Response;
using SUDLife_Authentication.ServiceLayer;
using SUDLife_DataRepo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SUDLife_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        
    {
        private readonly ClsAuthenticationService _authenticationService;
        private readonly ILogger<AuthController> _logger;
        private readonly IExectueProcdure _exectueProcdure;

        public AuthController(ClsAuthenticationService authenticationService, ILogger<AuthController> logger, IExectueProcdure exectueProcdure) { 
            this._authenticationService = authenticationService;
            this._logger= logger;
            this._exectueProcdure = exectueProcdure;

        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] ClsTokenRequest tokenRequest)
        {
            try
            {
                _logger.LogInformation("Token generation started");
                bool isValidUser = _authenticationService.ValidateUser(tokenRequest.username, tokenRequest.password);

                if (!isValidUser)
                {
                    return Unauthorized();
                }

                var accessToken = _authenticationService.GenerateAccessToken(tokenRequest.username);

                var response = new ClsTokenResponse
                {
                    access_token = accessToken,
                    token_type = "bearer",
                    expires_in = DateTime.Now.AddMinutes(30).ToString("g"),
                };

                var jsonResponse = JsonConvert.SerializeObject(response);

                return Ok(jsonResponse);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw;
            }
            }

        [HttpGet("{partner}")]
        public IActionResult GetOrGenerateSecretKey(string partner)
        {
            DataSet ds = new DataSet();
            try
            {
                var partnerParam = new SqlParameter
                {
                    ParameterName = "@Partner",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = partner
                };

                var parameters = new[] { partnerParam};

                // Execute the stored procedure
                ds =  _exectueProcdure.ExecuteProcedure("GetOrGenerateSecretKey", parameters);

                var secretKey = ds.Tables[0].Rows[0]["SecretKey"];

                // Return a 404 if the secret key is not found
                if (secretKey == null)
                {
                    return NotFound("Secret key not found.");
                }

                return Ok(new { SecretKey = secretKey });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting or generating the secret key.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
