using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Mascota;
using veterinaria.yara.domain.DTOs.Paginador;

namespace veterinaria.yara.api.Controllers.v1
{
    [Tags("Mascota")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class MascotaController : BaseApiController
    {
        private readonly IMascota _mascotaRepository;
        public MascotaController(IMascota mascotaRepository)
        {
            _mascotaRepository = mascotaRepository ?? throw new ArgumentNullException(nameof(mascotaRepository));
        }


        /// <summary>
        /// Genera la lista de razas paginadas
        /// </summary>
        /// <param name="skip"> Número de páginan dodne se requiere empezar la consulta </param>
        /// <param name="take"> Cantidad de items que se requiere obtener </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<PaginationFilterResponse<MascotaDTO>>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-mascotas")]
        public async Task<ActionResult<PaginationFilterResponse<MascotaDTO>>> ConsultarMascotas(int start, Int16 lenght, Guid idUsuario, CancellationToken cancellationToken)
        {
            var response = await _mascotaRepository.ConsultarMascotas(start, lenght, idUsuario, cancellationToken);
            return Ok(new MsDtoResponse<PaginationFilterResponse<MascotaDTO>>(response));
        }

        /// <summary>
        /// Método para crea la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para crear una mascota </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-mascota")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> CrearMascota([FromBody][Required] NuevaMascotaDto mascota)
        {
            var response = await _mascotaRepository.CrearMascota(mascota);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }


        /// <summary>
        /// Método para edita la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para editar una mascota </param>
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/editar-mascota")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> EditarMascota([FromBody][Required] MascotaDTO mascota)
        {
            var response = await _mascotaRepository.EditarMascota(mascota);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }


        /// <summary>
        /// Método para eliminar a una mascota a tráves de su guid
        /// </summary>
        /// <param name="IdMascota"> Id de la mascota a eliminar </param>
        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/eliminar-mascota")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> EliminarRaza([FromHeader][Required] Guid IdMascota)
        {
            var response = await _mascotaRepository.EliminarMascota(IdMascota);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }

    }
}
