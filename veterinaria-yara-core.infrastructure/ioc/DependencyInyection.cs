using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using StackExchange.Redis;
using veterinaria_yara_core.application.interfaces.repositories;
using veterinaria_yara_core.infrastructure.data.repositories;
using veterinaria_yara_core.infrastructure.data.repositories.chat;
using veterinaria_yara_core.infrastructure.data.repositories.estados;
using veterinaria_yara_core.infrastructure.mappings;

namespace veterinaria_yara_core.infrastructure.ioc
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loginBuilder => loginBuilder.AddSerilog(dispose: true));

            services.AddScoped<IRaza, RazaRestRepository>();
            services.AddScoped<IMascota, MascotaRestRepository>();
            services.AddScoped<IUsuario, UsuarioRepository>();
            services.AddScoped<IChat, Chat>();
            services.AddScoped<IEstados, EstadosRepository>();
            services.AddScoped<ICita, CitaRepository>();

            var builderConnection = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
            builderConnection.Password = "yara19975";

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.Internal().MethodMappingEnabled = false;
                mc.AddProfile(new MappingProfile());
            });

            services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(MappingProfile).Assembly);


            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builderConnection.ConnectionString);

            }, ServiceLifetime.Transient
            );

            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { configuration.GetConnectionString("RedisUrl") ?? "" },
                Password = configuration.GetConnectionString("RedisClave"),
                AbortOnConnectFail = false
            };

            services
                    .AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configurationOptions))
                    .BuildServiceProvider();

            services.AddHttpContextAccessor();

            return services;

        }
    }
}
