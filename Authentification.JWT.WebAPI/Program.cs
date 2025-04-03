using Authentification.JWT.DAL.Data;
using Authentification.JWT.Service;
using Authentification.JWT.Service.Dependency;
using Authentification.JWT.Service.Services;
using Authentification.JWT.WebAPI.Middlwares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();

    // Register NLog
    builder.Services.AddSingleton<NLog.ILogger>(sp => LogManager.GetLogger("GlobalLogger"));

    // Swagger Configuration
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAthentificationServices();

    builder.Services.AddScoped<UserService>();  // Ajout de IUserService

    builder.Services.AddScoped<IJwtService, JwtService>();


    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    builder.Services.AddAuthentication(

        x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        ).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsjabddsjsjdsdjkdsjkdsjdsj")),
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseExceptionHandler();

    app.UseAuthentication();

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}

catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
