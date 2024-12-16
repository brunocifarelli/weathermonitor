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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }
        /// <summary>
        /// Método para criar o usuário
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("register-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUsuariosAsync([FromBody] CreateUsuariosParams user, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Nome) || string.IsNullOrWhiteSpace(user.Password)) return BadRequest();

            try
            {
                await _usuariosService.CreateUsuarioAsync(user.UserName, user.Nome, user.Password, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Remove o usuário do sistema
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveUsuarioAsync([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty) return BadRequest("Usuário não encontrado.");

            try
            {
                await _usuariosService.RemoveUsuarioAsync(userId, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Remove o usuário do sistema
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UsuariosViewModel>>> GetUsuariosAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _usuariosService.GetUsuariosAsync(cancellationToken);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}