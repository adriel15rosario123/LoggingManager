using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Primary;
using LoggingManagerCore.Ports.Secundary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoggingManagerAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            this.configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult LogIn([FromBody] Credential credential)
        {

            var response = _authService.logIn(credential);

            if (response is null || response.ErrorCode is not null)
            {
                return Unauthorized(response);
            }
            else
            {
                List<Claim> claims = new List<Claim> {
                    new Claim("role",response.Data!.User.UserType.Type)
                };

                DateTime expiresAt = DateTime.UtcNow.AddMinutes(30);
                response.Data.Token.ExpiresAt = expiresAt;
                response.Data.Token.Key = CreateToken(claims, expiresAt);

                return Ok(response);
            }

        }


        [HttpGet("token/status")]
        public IActionResult TokenStatus(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["SecretKey"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return Ok(new { isActive = true });
            }
            catch
            {
                return Ok(new { isActive = false });
            }
        }

        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            byte[] secretKey = Encoding.ASCII.GetBytes(configuration["SecretKey"] ?? string.Empty);

            //generate the jwt
            JwtSecurityToken jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expiresAt,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

}
