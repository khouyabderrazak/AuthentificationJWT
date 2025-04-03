using Authentification.JWT.DAL.Data;
using Authentification.JWT.Service;
using Authentification.JWT.Service.Dependency;
using Authentification.JWT.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAthentificationServices();

builder.Services.AddScoped<UserService>();  // Ajout de IUserService

builder.Services.AddScoped<IJwtService,JwtService>();

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

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
