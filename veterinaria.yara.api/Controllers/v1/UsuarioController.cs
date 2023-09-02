using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.dtos;
using veterinaria.yara.domain.DTOs;

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
        /// Genera la lista de las mascotas
        /// </summary>
        ///<param name="prueba"> Prueba </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<UsuarioDTO>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/login")]
        public async Task<ActionResult<MsDtoResponse<UsuarioDTO>>> Login([FromBody][Required] UsuarioLogeoDTO usuario)

        {
            var response = await _usuariorRepository.Login(usuario);
            return Ok(new MsDtoResponse<UsuarioDTO>(response));
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<CrearResponse>), 200)]
        [ProducesResponseType(typeof(MsDtoResponseError), 400)]
        [ProducesResponseType(typeof(MsDtoResponseError), 500)]
        [Route("/v1/veterinaria-yara/crear-usuario")]
        public async Task<ActionResult<MsDtoResponse<CrearResponse>>> CrearUsuario([FromBody][Required] UsuarioDTO usuario)

        {
            var response = await _usuariorRepository.CrearUsuario(usuario);
            return Ok(new MsDtoResponse<CrearResponse>(response));
        }
    }
}
