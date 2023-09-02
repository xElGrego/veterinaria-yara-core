using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using veterinaria.yara.application.interfaces.repositories;
using veterinaria.yara.infrastructure.data.repositories;
using veterinaria.yara.infrastructure.data.repositories.chat;
using veterinaria.yara.infrastructure.extentions;
using veterinaria.yara.infrastructure.mappings;

namespace veterinaria.yara.infrastructure.ioc
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

            var builderConnection = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
            builderConnection.Password = "yara19975";

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.Internal().MethodMappingEnabled = false;
                mc.AddProfile(new MappingProfile());
            });

            services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(MappingProfile).Assembly);

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builderConnection.ConnectionString);

            }, ServiceLifetime.Transient
            );

            return services;

        }
    }
}
