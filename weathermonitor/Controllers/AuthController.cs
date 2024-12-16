using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIWeather.WebAPP.Application.Usuarios;
using APIWeather.WebAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace weathermonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuariosService _usuariosService;

        public AuthController(IConfiguration configuration, IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
            _configuration = configuration;
        }
        /// <summary>
        /// Realiza o login no sistema para realizar a autenticação nas cidades favoritas
        /// </summary>
        /// <param name="login"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginParams login, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await _usuariosService.ValidateLogin(login.UserName, login.Password, cancellationToken);

                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, login.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", loggedUser.Id.ToString())
                    };

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(4),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), Name = loggedUser.Nome });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}