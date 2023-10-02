using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Paginador;
using veterinaria.yara.domain.DTOs.Raza;

namespace veterinaria.yara.api.Controllers.v1
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
        [ProducesResponseType(typeof(MsDtoResponse<List<RazaDTO>>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/obtener-razas")]
        public async Task<ActionResult<MsDtoResponse<List<RazaDTO>>>> ObtenerRazas()
        {
            var response = await _razaRepository.ObtenerRazas();
            return Ok(new MsDtoResponse<List<RazaDTO>>(response));
        }


        /// <summary>
        /// Genera la lista de razas junto a su respectiva paginación
        /// </summary>
        /// <param name="buscar"> Palabra clave para buscar una raza en especifico </param>
        /// <param name="start"> Número de la página donde se requiere empezar la consulta </param>
        /// <param name="lenght"> Cantidad de items que se desea obtener </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<PaginationFilterResponse<RazaDTO>>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-razas")]
        public async Task<ActionResult<MsDtoResponse<PaginationFilterResponse<RazaDTO>>>> ConsultaRazas(string buscar, int start, int lenght)
        {
            var response = await _razaRepository.ConsultarRazas(buscar, start, lenght);
            return Ok(new MsDtoResponse<PaginationFilterResponse<RazaDTO>>(response));
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<RazaDTO>), 200)]
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
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-raza")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> CrearRaza([FromBody][Required] NuevaRazaDTO raza)
        {
            var response = await _razaRepository.CrearRaza(raza);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }


        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/editar-raza")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> EditarRaza([FromBody][Required] RazaDTO raza)
        {
            var response = await _razaRepository.EditarRaza(raza);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }

        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/eliminar-raza")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> EliminarRaza([FromHeader][Required] Guid idRaza)
        {
            var response = await _razaRepository.EliminarRaza(idRaza);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }
    }
}
