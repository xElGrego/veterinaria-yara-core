using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Elasticsearch;
using Serilog;

namespace Common.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               var elasticUri = context.Configuration["ElasticConfiguration:Uri"];

               var valor = $"{context.HostingEnvironment.ApplicationName?.ToLowerInvariant().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLowerInvariant().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";
               configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithProcessId()
                    .WriteTo.Trace()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUri ?? ""))
                        {
                            IndexFormat = $"{context.HostingEnvironment.ApplicationName?.ToLowerInvariant().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLowerInvariant().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",//Nombre app + ambiente + fecha
                            AutoRegisterTemplate = true, //
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        })
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName ?? "")
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName ?? "")
                    .ReadFrom.Configuration(context.Configuration);
           };
    }
}
