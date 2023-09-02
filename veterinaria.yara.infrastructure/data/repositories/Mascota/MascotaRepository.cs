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
using veterinaria.yara.infrastructure.repositories;

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

        public async Task<PaginationFilterResponse<MascotaDTO>> ConsultarMascotas(int start, int lenght, Guid? idUsuario, CancellationToken cancellationToken)
        {
            PaginationFilterResponse<MascotaDTO> mascotas = new();

            try
            {
                IQueryable<Mascota> mascotasQuery = _dataContext.Mascotas.AsNoTracking().OrderBy(r => r.FechaIngreso);

                if (idUsuario != Guid.Empty)
                {


                    mascotasQuery = mascotasQuery
               .Join(
                   _dataContext.UsuarioMascotas,
                   mascota => mascota.IdMascota,
                   usuarioMascota => usuarioMascota.IdMascota,
                   (mascota, usuarioMascota) => new { Mascota = mascota, UsuarioMascota = usuarioMascota })
               .Where(joinResult => joinResult.UsuarioMascota.IdUsuario == idUsuario)
               .Select(joinResult => joinResult.Mascota);


                    //mascotasQuery = mascotasQuery.Where(m => m.UsuarioMascota.Any(um => um.IdUsuario == idUsuario));


                }

                mascotas = await mascotasQuery.PaginationAsyncX<Mascota, MascotaDTO>(start, lenght, _mapper);
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
        public async Task<CrearResponse> EditarMascota(NuevaMascotaDto mascotaParam)
        {
            try
            {
                var searchData = await _dataContext.Mascotas.Where(x => x.IdMascota == mascotaParam.IdMascota).FirstOrDefaultAsync();
                if (searchData == null)
                {
                    throw new VeterinariaYaraException("La raza que estás buscando no existe");
                }

                var mascota = _mapper.Map<Mascota>(mascotaParam);
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

        //public async Task<CrearResponse> EliminarMascota(Guid idMascota)
        //{
        //    try
        //    {
        //        var entidadAEliminar = _dataContext.Mascotas.Find(idMascota);

        //        if (entidadAEliminar != null)
        //        {
        //            _dataContext.Mascotas.Remove(entidadAEliminar);
        //            _dataContext.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Eliminar Mascota [" + JsonConvert.SerializeObject(idMascota) + "]", ex);
        //        throw new VeterinariaYaraException("Error no se logró borrar", ex.Message);
        //    }

        //    var response = new CrearResponse
        //    {
        //        Response = "La mascota fue eliminada con éxito"
        //    };

        //    return response;
        //}
    }
}
