using LoggingManagerCore.Entities;
using LoggingManagerCore.Ports.Secundary;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserRepository userRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult LogIn([FromBody] Credential credential)
        {

            var response = userRepository.Login(credential);

            if(response!.ErrorCode != 0)
            {
                return BadRequest(new
                {
                    response = response
                });
            }
            else
            {
                List<Claim> claims = new List<Claim> {
                    new Claim("role",response.ResponseData!.UserType.Type)
                };
                
                DateTime expiresAt = DateTime.UtcNow.AddSeconds(30);

                return Ok(new
                {
                    accessToken = CreateToken(claims, expiresAt),
                    expires = expiresAt,
                    response = response
                });
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

                return Ok(new {active = true});
            }
            catch
            {
                return Ok(new {active = false });
            }
        }

        //[HttpPost]
        //[Authorize(Roles = "admin,client")]
        //public IActionResult LogOut()
        //{

        //}

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser(string username)
        {

            var response = userRepository.GetUserByUsername(username);

            return Ok(response);
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
