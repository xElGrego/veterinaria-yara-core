using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.application.models.exceptions;
using veterinaria_yara_core.domain.DTOs;
using veterinaria_yara_core.domain.DTOs.Paginador;
using veterinaria_yara_core.domain.DTOs.Raza;
using veterinaria_yara_core.domain.entities;


namespace veterinaria_yara_core.infrastructure.data.repositories
{
    public class RazaRestRepository : IRaza
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly ILogger<RazaRestRepository> _logger;

        public RazaRestRepository(IConfiguration configuration, IMapper mapper, DataContext dataContext, ILogger<RazaRestRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginationFilterResponse<RazaDTO>> ConsultarRazas(string buscar, int start, int lenght)
        {
            PaginationFilterResponse<RazaDTO> razas = new();
            try
            {
                var totalRegistros = await _dataContext.Razas.AsNoTracking().CountAsync();

                var razasQuery = _dataContext.Razas
                 .OrderBy(x => x.FechaIngreso)
                 .Select(x => new RazaDTO
                 {
                     Descripcion = x.Descripcion,
                     IdRaza = x.IdRaza,
                     Nombre = x.Nombre,
                 })
                 .Skip(start)
                 .Take(lenght);

                razas = await razasQuery.PaginationAsync(start, lenght, totalRegistros, _mapper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar razas", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
            return razas;
        }

        public async Task<RazaDTO> ConsultarRazaId(Guid idRaza)
        {
            RazaDTO result = new();

            try
            {
                var searchData = await _dataContext.Razas.Where(x => x.IdRaza == idRaza).FirstOrDefaultAsync();
                if (searchData == null)
                {
                    throw new VeterinariaYaraException("La raza que estás buscando no existe");
                }

                result.Nombre = searchData.Nombre;
                result.Descripcion = searchData.Descripcion;
                result.IdRaza = searchData.IdRaza;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar raza id [" + JsonConvert.SerializeObject(idRaza) + "]", ex.Message);
                throw new VeterinariaYaraException(ex.Message);
            }
            return result;
        }


        public async Task<CrearResponse> CrearRaza(NuevaRazaDTO razaParam)
        {
            try
            {
                var raza = _mapper.Map<Raza>(razaParam);
                _dataContext.Razas.Add(raza);
                await _dataContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError("Crear raza [" + JsonConvert.SerializeObject(razaParam) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "La raza fue creada con éxito"
            };
            return response;
        }

        public async Task<CrearResponse> EditarRaza(RazaDTO razaParam)
        {
            try
            {
                var raza = await _dataContext.Razas.FindAsync(razaParam.IdRaza);

                if (raza == null)
                {
                    throw new VeterinariaYaraException("La raza que estás buscando no existe");
                }

                _mapper.Map(razaParam, raza);
                raza.FechaModificacion = DateTime.Now;

                _dataContext.Razas.Update(raza);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Editar raza [" + JsonConvert.SerializeObject(razaParam) + "]", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "La raza fue editada con éxito"
            };

            return response;
        }

        public async Task<CrearResponse> EliminarRaza(Guid idRaza)
        {
            try
            {
                var entidadAEliminar = _dataContext.Razas.Find(idRaza);

                if (entidadAEliminar != null)
                {
                    _dataContext.Razas.Remove(entidadAEliminar);
                    _dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Eliminar raza [" + JsonConvert.SerializeObject(idRaza) + "]", ex);
                throw new VeterinariaYaraException("Error, existen mascotas asociadas a esta raza.", ex.Message);
            }

            var response = new CrearResponse
            {
                Response = "La raza fue eliminada con éxito"
            };
            return response;
        }

        public async Task<List<RazaDTO>> ObtenerRazas()
        {
            List<RazaDTO> result = new();

            try
            {
                var searchData = await _dataContext.Razas.ToListAsync();
                if (searchData == null)
                {
                    throw new VeterinariaYaraException("No existen estados en la tabla");
                }
                result = _mapper.Map<List<RazaDTO>>(searchData);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error obtener razas", ex);
                throw new VeterinariaYaraException(ex.Message);
            }

            return result;
        }



        //public async Task<CrearResponse> EliminarRaza(int idRaza)
        //{

        //    try
        //    {

        //        var opciones = new TransactionOptions();
        //        opciones.IsolationLevel = (System.Transactions.IsolationLevel)IsolationLevel.Serializable;
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            PruebaAsync();
        //            Prueba2Async();
        //            scope.Complete();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("aqui error " , ex);
        //        throw new Exception("Error la raza ya existe", ex);
        //    }


        //    var response = new CrearResponse
        //    {
        //         Response = "La raza fue eliminada con éxito"
        //    };

        //    return response;
        //}


        //async Task PruebaAsync()
        //{
        //    try
        //    {                    


        //        var request = new Raza
        //        {
        //            Nombre = "Data Correcta",
        //            Descripcion = "Data Correcta",
        //            Estado = true,
        //        };

        //        var razaExistente = await _dataContext.Razas.Where(X => X.Nombre == request.Nombre).FirstOrDefaultAsync();

        //        if (razaExistente != null)
        //        {
        //            Console.WriteLine("La raza ya existe. Se hará un rollback de la transacción.");
        //            throw new Exception("Se produjo un error.");
        //        }


        //        _dataContext.Razas.Add(request);
        //        _dataContext.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new Exception("Se produjo un error en Prueba.", ex);
        //    }
        //}

        //public async Task Prueba2Async()
        //{
        //    List<Raza> list;
        //    string estado = "T";

        //    try
        //    {

        //        var razas = _dataContext.Razas.FromSqlRaw("EXEC ObtenerRazas @estado={0}", estado).ToList();



        //        //var result = await _dataContext.Database.ExecuteSqlRawAsync($"ObtenerRazas {estado}");
        //        Console.WriteLine("Resultado", razas);
        //    }


        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //    //string sql = "EXEC ObtenerRazas @estado";

        //    //List<SqlParameter> parms = new List<SqlParameter>
        //    //{ 
        //    //    // Create parameters    
        //    //    new SqlParameter { ParameterName = "@estado", Value = 'T' },
        //    // };

        //    //list = _dataContext.Razas.FromSqlRaw<Raza>(sql, parms.ToArray()).ToList();


        //    //var estadoParam = new SqlParameter("@estado", estado);
        //    //_dataContext.Database.ExecuteSqlRaw("ObtenerRazas @estado", parameters: new[] { "T" });
        //    //list = _dataContext.Razas.FromSqlRaw<Raza>("ObtenerRazas @estado", estadoParam).ToList();

        //    var clientIdParameter = new SqlParameter("@estado", "T");





        //    //var result = _dataContext.Database.ExecuteSqlRawAsync("ObtenerRazas @estado", clientIdParameter);



        //}
    }
}
