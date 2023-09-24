using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.exceptions;
using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.entities;

namespace veterinaria.yara.infrastructure.data.repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(IConfiguration configuration, DataContext dataContext, ILogger<UsuarioRepository> logger, IMapper mapper)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UsuarioDTO> ObtenerRol(UsuarioLogeoDTO usuarioParam)
        {
            var usuario = new UsuarioDTO();
            try
            {

                var res = await _dataContext.Usuarios
                     .Where(u => u.Correo == usuarioParam.Correo && u.Clave == usuarioParam.Clave)
                     .Join(_dataContext.UsuarioRoles,
                         usuario => usuario.IdUsuario,
                         usuarioRol => usuarioRol.IdUsuario,
                         (usuario, usuarioRol) => new
                         {
                             usuario.Nombres,
                             usuario.Correo,
                             usuario.Apellidos,
                             Rol = _dataContext.Roles
                                 .Where(r => r.IdRol == usuarioRol.IdRol)
                                 .Select(r => r.NombreRol)
                                 .FirstOrDefault()
                         })
                     .FirstOrDefaultAsync();

                if (res != null)
                {
                    usuario.Correo = res.Correo;
                    usuario.Nombres = res.Nombres;
                    usuario.Apellidos = res.Apellidos;
                    usuario.Rol = res.Rol;
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw new VeterinariaYaraException(ex.Message);
            }
        }


        public async Task<UsuarioDTO> Login(UsuarioLogeoDTO usuarioDTOParam)
        {
            UsuarioDTO usuario;
            string jwToken;

            try
            {
                usuario = await ObtenerRol(usuarioDTOParam);

                if (usuario is null)
                {
                    throw new VeterinariaYaraException("El correo registrado no existe");
                }
                jwToken = GenerarToken(usuario);

            }
            catch (Exception ex)
            {
                throw new VeterinariaYaraException(ex.Message);
            }

            var usuarito = new UsuarioDTO
            {
                Clave = usuario.Clave,
                Correo = usuario.Correo,
                Apellidos = usuario.Apellidos,
                Nombres = usuario.Nombres,
                Token = jwToken,
                Rol = usuario.Rol
            };

            return usuarito;
        }

        private string GenerarToken(UsuarioDTO usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,usuario.Nombres),
                new Claim(ClaimTypes.Email,usuario.Correo)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var securityToken = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: creds
                    );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public async Task<CrearResponse> CrearUsuario(UsuarioDTO usuarioParam)
        {
            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    var usuario = _mapper.Map<Usuario>(usuarioParam);

                    _dataContext.Usuarios.Add(usuario);
                    await _dataContext.SaveChangesAsync();

                    if (!string.IsNullOrEmpty(usuarioParam.Rol))
                    {
                        var rolExistente = await _dataContext.Roles.FirstOrDefaultAsync(r => r.NombreRol == usuarioParam.Rol);

                        if (rolExistente == null)
                        {
                            rolExistente = new Role { IdRol = Guid.NewGuid(), NombreRol = usuarioParam.Rol };
                            _dataContext.Roles.Add(rolExistente);
                            await _dataContext.SaveChangesAsync();
                        }

                        _dataContext.UsuarioRoles.Add(new UsuarioRole { IdUsuarioRol = Guid.NewGuid(), IdUsuario = usuario.IdUsuario, IdRol = rolExistente.IdRol });
                        await _dataContext.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError("Crear usuario [" + JsonConvert.SerializeObject(usuarioParam) + "]", ex);
                    throw new VeterinariaYaraException(ex.Message);
                }
            }

            var response = new CrearResponse
            {
                Response = "El usuario fue creado con éxito"
            };
            return response;
        }

        //public async Task<CrearResponse> CrearUsuario(UsuarioDTO usuarioParam)
        //{
        //    try
        //    {
        //        var usuario = _mapper.Map<Usuario>(usuarioParam);
        //        _dataContext.Usuarios.Add(usuario);
        //        await _dataContext.SaveChangesAsync();
        //    }

        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Crear usuario [" + JsonConvert.SerializeObject(usuarioParam) + "]", ex);
        //        throw new VeterinariaYaraException(ex.Message);
        //    }

        //    var response = new CrearResponse
        //    {
        //        Response = "El usuario fue creado con éxito"
        //    };
        //    return response;
        //}
    }
}
