using System.Reflection;
using System.Text;
using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using veterinaria.yara.api.Extensions;
using veterinaria.yara.api.SignalR;
using veterinaria.yara.infrastructure.extentions;
using veterinaria.yara.infrastructure.ioc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdministrador", policy =>
    policy.RequireRole("SuperAdministrador"));
});

builder.Services.AddSwaggerGen(options =>
{
    var title = builder.Configuration["OpenApi:info:title"];
    var version = builder.Configuration["OpenApi:info:version"];
    var description = builder.Configuration["OpenApi:info:description"];


    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = title,
        Description = description,
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = version,
        Title = title,
        Description = description,
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authenntication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "JWT Authorization  header using the Bearer scheme  . ",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

});

//builder.Services.AddHostedService<RabbitMQNotificationService>();
builder.Services.AddHostedService<RabbitMQBackgroundService>();



builder.Services.RegisterDependencies();
builder.Services.AddInfraestructure(builder.Configuration);

//Se agregar el servicio de autenticación
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });


builder.Services.AddSignalR();



var corsOrigin = "_AllowPolicy";

IConfigurationSection myArraySection = builder.Configuration.GetSection("AuthorizeSite:SiteUrl");

string[] folders = myArraySection.Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigin, policy =>
    {
        policy.WithOrigins(folders)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


builder.Host.UseSerilog(SeriLogger.Configure);
builder.Services.AddSingleton<RabbitMQConsumer>(); // Agrega el consumidor de RabbitMQ


var app = builder.Build();

app.MapHub<SignalHubGroup>("/signalRGroup");
app.MapHub<SignalHub>("/signalR");
app.MapHub<ChatHub>("/chatHubR");



app.UseSerilogRequestLogging();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
    }
   );
}


//Configurando HTTP request

app.ConfigureMetricServer();
app.ConfigureExceptionHandler();

//app.UseCors(x => x
//.AllowAnyMethod()
//.AllowAnyHeader()
//.SetIsOriginAllowed(origin => true)
//.AllowCredentials()
//);

//app.UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseCors(corsOrigin);
app.Run();
