using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.dtos;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Estados.Mascota;
using veterinaria_yara_core.domain.DTOs.Paginador;

namespace veterinaria_yara_core.api.Controllers.v1
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
        /// <param name="start"> Número de páginan dodne se requiere empezar la consulta </param>
        /// <param name="lenght"> Cantidad de items que se requiere obtener </param>
        /// <param name="nombre"> Nombre de la mascota a buscar </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginationFilterResponse<MascotaDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-mascotas")]
        public async Task<ActionResult<PaginationFilterResponse<MascotaDTO>>> ConsultarMascotas(int start, Int16 lenght, string? nombre, int estado, DateTime fechaInicio, DateTime fechaFin, Guid idUsuario,
        CancellationToken cancellationToken)
        {
            var response = await _mascotaRepository.ConsultarMascotas(start, lenght, nombre, estado, fechaInicio, fechaFin, idUsuario, cancellationToken);
            return Ok(response);
        }


        /// <summary>
        /// Genera la lista de razas paginadas
        /// </summary>
        /// <param name="start"> Número de páginan dodne se requiere empezar la consulta </param>
        /// <param name="length"> Cantidad de items que se requiere obtener </param>
        /// <param name="idUsuario"> Id usuario </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginationFilterResponse<MascotaDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-mascotas-usuarios")]
        public async Task<ActionResult<PaginationFilterResponse<MascotaDTO>>> ConsultarMascotasUsuario(int start, Int16 length, Guid idUsuario,
        CancellationToken cancellationToken)
        {
            var response = await _mascotaRepository.ConsultarMascotasUsuario(start, length, idUsuario, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Método para crea la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para crear una mascota </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-mascota")]
        public async Task<ActionResult<CrearResponse>> CrearMascota([FromBody][Required] NuevaMascotaDto mascota)
        {
            var response = await _mascotaRepository.CrearMascota(mascota);
            return Ok(response);
        }


        /// <summary>
        /// Método para edita la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para editar una mascota </param>
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/editar-mascota")]
        public async Task<ActionResult<CrearResponse>> EditarMascota([FromBody][Required] MascotaDTO mascota)
        {
            var response = await _mascotaRepository.EditarMascota(mascota);
            return Ok(response);
        }


        /// <summary>
        /// Método para eliminar a una mascota a tráves de su guid
        /// </summary>
        /// <param name="idMascota"> Id de la mascota a eliminar </param>
        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/eliminar-mascota")]
        public async Task<ActionResult<CrearResponse>> EliminarMascota([FromHeader][Required] Guid idMascota)
        {
            var response = await _mascotaRepository.EliminarMascota(idMascota);
            return Ok(response);
        }


        /// <summary>
        /// Método para activar a una mascota a tráves de su guid
        /// </summary>
        /// <param name="IdMascota"> Id de la mascota a eliminar </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/activar-mascota")]
        public async Task<ActionResult<CrearResponse>> ActivarMascota([FromHeader][Required] Guid idMascota)
        {
            var response = await _mascotaRepository.ActivarMascota(idMascota);
            return Ok(response);
        }


        /// <summary>
        /// Método para obtener la ultima mascota registrada
        /// </summary>
        /// <param name="IdUsuario"> Id del usuario a buscar </param>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MascotaDTO), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/ultima-mascota")]
        public async Task<ActionResult<MascotaDTO>> ObtenerUltimaMascota([FromHeader][Required] Guid IdUsuario)
        {
            var response = await _mascotaRepository.UltimaMascota(IdUsuario);
            return Ok(response);
        }

        /// <summary>
        /// Método para obtener las mascotas
        /// </summary>
        /// <param name="IdUsuario"> Id del usuario a buscar </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/ordenar-mascota")]
        public async Task<ActionResult<CrearResponse>> ReordenarMascota([FromBody][Required] List<ReordenarMascotaDTO> mascotas)
        {
            var response = await _mascotaRepository.ReordenarMascota(mascotas);
            return Ok(response);
        }
    }
}
