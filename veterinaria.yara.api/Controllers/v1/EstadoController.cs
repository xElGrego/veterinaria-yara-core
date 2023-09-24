using Microsoft.AspNetCore.Mvc;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.domain.DTOs.Estados;

namespace veterinaria.yara.api.Controllers.v1
{
    [Tags("Estado")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class EstadoController : BaseApiController
    {

        private readonly IEstados _estados;
        public EstadoController(IEstados estados)
        {
            _estados = estados ?? throw new ArgumentNullException(nameof(estados));
        }

        /// <summary>
        /// Método para obtener los estados disponibles de razas y usurarios
        /// </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<EstadosDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/obtener-estados")]
        public async Task<ActionResult<List<EstadosDTO>>> ObtenerEstados()
        {
            var response = await _estados.ObtenerEstados();
            return Ok(new List<EstadosDTO>(response));
        }
    }
}
