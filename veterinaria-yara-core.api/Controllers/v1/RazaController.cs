using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.dtos;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Paginador;
using veterinaria_yara_core.domain.DTOs.Raza;

namespace veterinaria_yara_core.api.Controllers.v1
{
    [Tags("Raza")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class RazaController : BaseApiController
    {
        private readonly IRaza _razaRepository;

        public RazaController(IRaza razaRepository)
        {
            _razaRepository = razaRepository ?? throw new ArgumentNullException(nameof(razaRepository));
        }

        /// <summary>
        /// Genera la lista de las razas
        /// </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<RazaDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/obtener-razas")]
        public async Task<ActionResult<List<RazaDTO>>> ObtenerRazas()
        {
            var response = await _razaRepository.ObtenerRazas();
            return Ok(response);
        }


        /// <summary>
        /// Genera la lista de razas junto a su respectiva paginación
        /// </summary>
        /// <param name="buscar"> Palabra clave para buscar una raza en especifico </param>
        /// <param name="start"> Número de la página donde se requiere empezar la consulta </param>
        /// <param name="length"> Cantidad de items que se desea obtener </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginationFilterResponse<RazaDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-razas")]
        public async Task<ActionResult<PaginationFilterResponse<RazaDTO>>> ConsultaRazas(string buscar, int start, int length)
        {
            var response = await _razaRepository.ConsultarRazas(buscar, start, length);
            return Ok(response);
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RazaDTO), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-razas-id")]
        public async Task<ActionResult<MsDtoResponse<RazaDTO>>> ConsultaRazasId(Guid idRaza)
        {
            var response = await _razaRepository.ConsultarRazaId(idRaza);
            return Ok(new MsDtoResponse<RazaDTO>(response));
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-raza")]
        public async Task<ActionResult<CrearResponse>> CrearRaza([FromBody][Required] NuevaRazaDTO raza)
        {
            var response = await _razaRepository.CrearRaza(raza);
            return Ok(response);
        }


        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/editar-raza")]
        public async Task<ActionResult<CrearResponse>> EditarRaza([FromBody][Required] RazaDTO raza)
        {
            var response = await _razaRepository.EditarRaza(raza);
            return Ok(response);
        }

        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/eliminar-raza")]
        public async Task<ActionResult<CrearResponse>> EliminarRaza([FromHeader][Required] Guid idRaza)
        {
            var response = await _razaRepository.EliminarRaza(idRaza);
            return Ok(response);
        }
    }
}
