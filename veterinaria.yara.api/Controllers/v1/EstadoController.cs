using Microsoft.AspNetCore.Mvc;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.domain.DTOs.Estados;
using veterinaria_yara_core.application.models.dtos;

namespace veterinaria_yara_core.api.Controllers.v1
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
