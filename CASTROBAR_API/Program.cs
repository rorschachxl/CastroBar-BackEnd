using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DbAadd54CastrobarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<UsuariosRepository>();
builder.Services.AddTransient<TokenAndEncript>();
builder.Services.AddTransient<IEmailUtility,EmailUtility>();
builder.Services.AddTransient<EmailUtility>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>{
        options.SwaggerDoc("v1", new OpenApiInfo{
            Title = "CastroBar API",
            Version = "v1",
            Description = "Esta es la API de CastroBar, esta API esta enfocada en ser una herramienta útil para el manejo de mesas en restaurantes, priorizando el servicio rapido al usuario, también hay una función que permite encasillar a las personas en Mesas de manera ordenada. Tambien hay una funcionalidad que permite hacer un seguimiento de la cuenta , asi como también hay funciones para generar los pagos."
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme(){
            Description = "JWT Token usar Bearer {token}",
            Name = "Authorization", 
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    },
                    Scheme= "OAuth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string> ()
            }
        });
    }
);
var SecretKey = builder.Configuration.GetSection("Settings").GetSection("secretKey").ToString();
#pragma warning disable CS8604 // Possible null reference argument.
var Byteskey = Encoding.UTF8.GetBytes(SecretKey);
#pragma warning restore CS8604 // Possible null reference argument.
builder.Services.AddAuthentication(confg =>
{
    confg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    confg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Byteskey),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(options=>{
    options.AddPolicy ("NuevaPolitica", app=>{
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
