using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.dtos;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Estados.Mascota;
using veterinaria_yara_core.domain.DTOs.Paginador;
using veterinaria_yara_core.domain.DTOs.Usuario;

namespace veterinaria_yara_core.api.Controllers.v1
{
    [Tags("Usuario")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class UsuarioController : BaseApiController
    {
        private readonly IUsuario _usuariorRepository;
        public UsuarioController(IUsuario usuariorRepository)
        {
            _usuariorRepository = usuariorRepository ?? throw new ArgumentNullException(nameof(usuariorRepository));
        }

        /// <summary>
        /// Método donde el usuario realiza el login
        /// </summary>
        ///<param name="usuario"> Objeto que se debe enviar para logearse </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(NuevoUsuarioDTO), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/login")]
        public async Task<ActionResult<UsuarioDTO>> Login([FromBody][Required] UsuarioLogeoDTO usuario)

        {
            var response = await _usuariorRepository.Login(usuario);
            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Policy = "SuperAdministrador")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-usuario")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> CrearUsuario([FromBody][Required] AgregarUsuarioDTO usuario)
        {
            var response = await _usuariorRepository.CrearUsuario(usuario);
            return Ok(response);
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
        [ProducesResponseType(typeof(MsDtoResponse<PaginationFilterResponse<UsuarioDTO>>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/consulta-usuarios")]
        public async Task<ActionResult<PaginationFilterResponse<MascotaDTO>>> ConsultarMascotasUsuario(int start, Int16 length, Guid idUsuario,
        CancellationToken cancellationToken)
        {
            var response = await _usuariorRepository.ConsultarUsuarios(start, length, cancellationToken);
            return Ok(response);
        }
    }
}
