using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SUDLife_Authentication.Models.Request;
using SUDLife_Authentication.Models.Response;
using SUDLife_Authentication.ServiceLayer;
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
        public AuthController(ClsAuthenticationService authenticationService, ILogger<AuthController> logger) { 
            this._authenticationService = authenticationService;
            this._logger= logger;
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
       

       
    }
}
