using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SUDLife_Authentication.Models.Request;
using SUDLife_Authentication.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SUDLife_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult GetToken([FromBody] ClsTokenRequest tokenRequest)
        {
            bool isValidUser = ValidateUser(tokenRequest.username, tokenRequest.password);

            if (!isValidUser)
            {
                return Unauthorized();
            }

            var accessToken = GenerateAccessToken(tokenRequest.username);

            var response = new ClsTokenResponse
            {
                access_token = accessToken,
                token_type = "bearer",
                expires_in = DateTime.Now.AddMinutes(30).ToString("g"),
            };

            var jsonResponse = JsonConvert.SerializeObject(response);

            return Ok(jsonResponse);
        }

        public bool ValidateUser(string username, string password)
        {
            return username == "Sud_Partner" && password == "abcd@1234";
        }

        public string GenerateAccessToken(string username)
        {
            string issuer = "CCDD18D8-49FF-43E5-8E6C-D0924C2BBE0C";
            string audience = "B502C4CA-9895-419A-AF6C-65B5801CBDEA";
            string symmetricSecurityKey = "463C24EE-EE45-4957-A682-3704AD7F91C7";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
