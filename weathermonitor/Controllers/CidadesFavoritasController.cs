using APIWeather.WebAPP.Application.CidadesFavoritas;
using APIWeather.WebAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace weathermonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadesFavoritasController : ControllerBase
    {

        private readonly ICidadesFavoritasService _cidadesFavoritasService;

        public CidadesFavoritasController(ICidadesFavoritasService cidadesFavoritasService)
        {
            _cidadesFavoritasService = cidadesFavoritasService;
        }
        /// <summary>
        /// M�todo utilizado para adicionar uma cidade favorita
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("create-favorite-city")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AddCidadeFavoritaAsync([FromBody] CreateCidadesFavoritasParams city, CancellationToken cancellationToken)
        {
            if (User.Identity?.IsAuthenticated == null || !User.Identity.IsAuthenticated) return Unauthorized(new { Message = "Usu�rio n�o autorizado." });

            if (string.IsNullOrWhiteSpace(city.Name) || string.IsNullOrWhiteSpace(city.Latitude) ||
                string.IsNullOrWhiteSpace(city.Longitude))
            {
                return BadRequest();
            }

            try
            {
                await _cidadesFavoritasService.CreateCidadeFavoritaAsync(city.Name, city.Latitude,
                    city.Longitude, cancellationToken);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }

        }
        /// <summary>
        /// M�todo utilizado para remover uma cidade favorita
        /// </summary>
        /// <param name="cityId">Id da cidade que est� a ser removida das favoritas</param>
        /// <returns></returns>
        [HttpDelete("{cityId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RemoveCidadeFavoritaAsync([FromRoute] Guid cityId, CancellationToken cancellationToken)
        {
            if (User.Identity?.IsAuthenticated == null || !User.Identity.IsAuthenticated) return Unauthorized(new { Message = "Usu�rio n�o autorizado." });
            if (cityId == Guid.Empty) return BadRequest();
            try
            {
                await _cidadesFavoritasService.RemoveCidadeFavoritaAsync(cityId, cancellationToken);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Usu�rio n�o autorizado.");
            }
        }

        /// <summary>
        /// M�todo utilizado para buscar todas as cidades favoritas para o usu�rio logado
        /// </summary>
        /// <returns></returns>
        [HttpGet("favorites-cities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<CidadesFavoritasViewModel>>> GetCidadesFavoritasAsync(CancellationToken cancellationToken)
        {
            if (User.Identity?.IsAuthenticated == null || !User.Identity.IsAuthenticated) return Unauthorized(new { Message = "Usu�rio n�o autorizado." });
            try
            {
                var cities = await _cidadesFavoritasService.GetCidadesFavoritasAsync(cancellationToken);
                return Ok(cities);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}