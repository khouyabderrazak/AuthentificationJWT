using Authentification.JWT.Service;
using Authentification.JWT.Service.Services;
using Authentification.JWT.WebAPI.Middlwares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using Authentification.JWT.Service.Dependency;

var logger = NLog.LogManager.GetCurrentClassLogger();
logger.Debug("Initialisation de l'application");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    builder.Services.AddSingleton<NLog.ILogger>(sp => LogManager.GetLogger("GlobalLogger"));

    // Configuration de Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //  services d'authentification
    builder.Services.AddAthentificationServices(builder.Configuration);

    //  UserService
    builder.Services.AddScoped<UserService>();

    //  IJwtService
    builder.Services.AddScoped<IJwtService, JwtService>();

    builder.Services.AddSingleton<GlobalExceptionHandler>();

    //jwt configuration
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; 
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

  
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "L'application a échoué lors de l'initialisation");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

