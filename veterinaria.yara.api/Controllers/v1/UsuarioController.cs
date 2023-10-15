using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Mascota;
using veterinaria.yara.domain.DTOs.Paginador;
using veterinaria.yara.domain.DTOs.Usuario;

namespace veterinaria.yara.api.Controllers.v1
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
        [ProducesResponseType(typeof(MsDtoResponse<NuevoUsuarioDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/login")]
        public async Task<ActionResult<MsDtoResponse<UsuarioDTO>>> Login([FromBody][Required] UsuarioLogeoDTO usuario)

        {
            var response = await _usuariorRepository.Login(usuario);
            return Ok(new MsDtoResponse<NuevoUsuarioDTO>(response));
        }

        [HttpPost]
        [Authorize(Policy = "SuperAdministrador")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-usuario")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> CrearUsuario([FromBody][Required] NuevoUsuarioDTO usuario)
        {
            var response = await _usuariorRepository.CrearUsuario(usuario);
            return Ok(new MsDtoResponse<CrearResponse>(response));
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
        public async Task<ActionResult<PaginationFilterResponse<UsuarioDTO>>> ConsultarUsuarios(int start, Int16 lenght,
        CancellationToken cancellationToken)
        {
            var response = await _usuariorRepository.ConsultarUsuarios(start, lenght, cancellationToken);
            return Ok(new MsDtoResponse<PaginationFilterResponse<UsuarioDTO>>(response));
        }
    }
}
