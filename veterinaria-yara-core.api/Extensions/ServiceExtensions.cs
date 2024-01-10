using Microsoft.AspNetCore.Mvc;

namespace veterinaria_yara_core.api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            return services;
        }
    }
}
