using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.application.models.exceptions;
using veterinaria.yara.domain.DTOs;
using veterinaria.yara.domain.DTOs.Mascota;
using veterinaria.yara.domain.DTOs.Paginador;
using veterinaria.yara.domain.entities;

namespace veterinaria.yara.infrastructure.data.repositories
{
    public class MascotaRestRepository : IMascota
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private ILogger<MascotaRestRepository> _logger;

        public MascotaRestRepository(ILogger<MascotaRestRepository> logger, DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotas(int start, int length, Guid? idUsuarioParam, CancellationToken cancellationToken)
        {
            PaginationFilterResponse<MascotaDTO> mascotas = new();

            try
            {
                var mascotasQuery = _dataContext.Mascotas
                 .Where(m => m.Estado == true)
                 .Join(_dataContext.UsuarioMascotas,
                     mascota => mascota.IdMascota,
                     usuarioMascota => usuarioMascota.IdMascota,
                     (mascota, usuarioMascota) => new MascotaDTO
                     {
                         IdMascota = mascota.IdMascota,
                         IdUsuario = usuarioMascota.IdUsuario,
                         Nombre = mascota.Nombre,
                         Mote = mascota.Mote,
                         Edad = mascota.Edad,
                         Peso = mascota.Peso,
                         IdRaza = mascota.IdRaza,
                         FechaIngreso = mascota.FechaIngreso,
                         FechaModificacion = mascota.FechaModificacion,
                         Estado = mascota.Estado
                     });

                if (idUsuarioParam != Guid.Empty)
                {
                    mascotasQuery = mascotasQuery.Where(mascota => mascota.IdUsuario == idUsuarioParam);
                }

                mascotas = await mascotasQuery.PaginationAsync(start, length, _mapper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar mascotas", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
            return mascotas;
        }

        public async Task<MascotaDTO> ConsultarMascotaId(Guid idMascota)
        {
            MascotaDTO result = new();

            try
            {
                var searchData = await _dataContext.Mascotas.Where(x => x.IdMascota == idMascota).FirstOrDefaultAsync();
                if (searchData == null)
                {
                    throw new VeterinariaYaraException("La mascota que estás buscando no existe");
                }

                result.Nombre = searchData.Nombre;
                result.Edad = searchData.Edad;
            }
            catch (Exception ex)
            {
                throw new VeterinariaYaraException(ex.Message);
            }

            return result;
        }

        public async Task<CrearResponse> CrearMascota(NuevaMascotaDto mascotaParam)
        {
            using (var transaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    var mascota = _mapper.Map<Mascota>(mascotaParam);
                    _dataContext.Mascotas.Add(mascota);
                    await _dataContext.SaveChangesAsync();

                    _dataContext.UsuarioMascotas.Add(new UsuarioMascota { IdUsuarioMascota = Guid.NewGuid(), IdUsuario = mascotaParam.IdUsuario, IdMascota = mascota.IdMascota });
                    await _dataContext.SaveChangesAsync();


                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError("Crear mascota [" + JsonConvert.SerializeObject(mascotaParam) + "]", ex);
                    throw new VeterinariaYaraException(ex.Message);
                }
            }
            var response = new CrearResponse
            {
                Response = "La mascota fue creada con éxito"
            };

            return response;
        }

        public async Task<CrearResponse> EditarMascota(MascotaDTO mascotaParam)
        {
            try
            {
                var mascota = await _dataContext.Mascotas.FindAsync(mascotaParam.IdMascota);

                if (mascota == null)
                {
                    throw new VeterinariaYaraException("La mascota que estás buscando no existe");
                }

                _mapper.Map(mascotaParam, mascota);
                mascota.FechaModificacion = DateTime.Now;

                _dataContext.Mascotas.Update(mascota);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Editar mascota [" + JsonConvert.SerializeObject(mascotaParam) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "La mascota fue editada con éxito"
            };

            return response;
        }

        public async Task<CrearResponse> EliminarMascota(Guid idMascota)
        {
            try
            {
                var entidadAEliminar = await _dataContext.Mascotas.Include(m => m.UsuarioMascota)
                    .FirstOrDefaultAsync(m => m.IdMascota == idMascota);

                if (entidadAEliminar != null)
                {
                    _dataContext.Mascotas.Remove(entidadAEliminar);
                    await _dataContext.SaveChangesAsync();
                }
                else
                {
                    throw new VeterinariaYaraException("La mascota no existe");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Eliminar Mascota [" + JsonConvert.SerializeObject(idMascota) + "]", ex);
                throw new VeterinariaYaraException("Error al intentar borrar la mascota", ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "La mascota fue eliminada con éxito"
            };

            return response;
        }

        public async Task<MascotaDTO> UltimaMascota(Guid idUsuarioParam)
        {
            MascotaDTO result = new();

            try
            {
                var idUsuario = _dataContext.Usuarios
                    .Where(x => x.Estado == true)
                    .Select(x => x.IdUsuario)
                    .FirstOrDefault();


                if (idUsuario == null)
                {
                    throw new VeterinariaYaraException("El usuario no existe");
                }

                var ultimaMascota = await _dataContext.Mascotas
                    .Where(m => m.Estado == true)
                    .Join(_dataContext.UsuarioMascotas,
                        mascota => mascota.IdMascota,
                        usuarioMascota => usuarioMascota.IdMascota,
                        (mascota, usuarioMascota) => new MascotaDTO
                        {
                            IdMascota = mascota.IdMascota,
                            IdUsuario = usuarioMascota.IdUsuario,
                            Nombre = mascota.Nombre,
                            Mote = mascota.Mote,
                            Edad = mascota.Edad,
                            Peso = mascota.Peso,
                            IdRaza = mascota.IdRaza,
                            FechaIngreso = mascota.FechaIngreso,
                            FechaModificacion = mascota.FechaModificacion,
                            Estado = mascota.Estado
                        })
                    .Where(mascota => mascota.IdUsuario == idUsuario)
                    .OrderByDescending(mascota => mascota.IdMascota)
                    .ThenByDescending(m => m.IdMascota)
                    .FirstOrDefaultAsync();

                if (ultimaMascota != null)
                {
                    result = ultimaMascota;
                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Ultima mascota [" + JsonConvert.SerializeObject(idUsuarioParam) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }
            return result;
        }
    }
}
