using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;
var builder = WebApplication.CreateBuilder(args);

// Obtener la clave secreta desde la configuración
var SecretKey = builder.Configuration["Key:secretKey"];
if (string.IsNullOrEmpty(SecretKey))
{
    throw new InvalidOperationException("SecretKey no está configurada correctamente.");
}

// Configurar la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired-Time","true");
                }
                return Task.CompletedTask;
            }

        };
    });
builder.Services.AddControllers();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("2"));
});
builder.Services.AddDbContext<DbAadd54CastrobarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<UsuariosRepository>();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IEstadoRepository, EstadoRepository>();
builder.Services.AddTransient<IRecetaRepository, RecetaRepository>();
builder.Services.AddTransient<IProveedorRepository, ProveedorRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<ISubcategoriaRepository, SubcategoriaRepository>();
builder.Services.AddTransient<IProductoRecetaRepository, ProductoRecetaRepository>();
builder.Services.AddTransient<IProductoProveedorRepository, ProductoProveedorRepository>();
builder.Services.AddTransient<IOrdenRepository, OrdenRepository>();
builder.Services.AddTransient<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddTransient<IMesaRepository, MesaRepository>();
builder.Services.AddTransient<IProductoOrdenRepository, ProductoOrdenRepository>();
builder.Services.AddTransient<IEmailUtility, EmailUtility>();
builder.Services.AddTransient<EmailUtility>();
builder.Services.AddTransient<ProductoService>();
builder.Services.AddTransient<ProductoOrdenService>();
builder.Services.AddTransient<OrdenService>();
builder.Services.AddTransient<MetodoPagoService>();
builder.Services.AddTransient<MesaService>();
builder.Services.AddTransient<ProductoProveedorService>();
builder.Services.AddTransient<ProveedorService>();
builder.Services.AddTransient<CategoriaService>();
builder.Services.AddTransient<SubcategoriaService>();
builder.Services.AddTransient<RecetaService>();
builder.Services.AddTransient<ProductoRecetaService>();
builder.Services.AddTransient<EstadoService>();
builder.Services.AddTransient<TokenAndEncript>();
builder.Services.AddTransient<ProductoUtilities>();
builder.Services.AddTransient<EstadoUtilities>();
builder.Services.AddTransient<CategoriaUtilities>();
builder.Services.AddTransient<SubcategoriaUtilities>();

// Agregar Endpoints API Explorer
builder.Services.AddEndpointsApiExplorer();
// Configuración del CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configuración de Swagger para autenticación JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CastroBar API", Version = "v1", Description = "Esta es la API de CastroBar, esta API esta enfocada en ser una herramienta útil para el manejo de mesas en restaurantes, priorizando el servicio rapido al usuario, también hay una función que permite encasillar a las personas en Mesas de manera ordenada. Tambien hay una funcionalidad que permite hacer un seguimiento de la cuenta , asi como también hay funciones para generar los pagos." , });

    // Añadir definición de seguridad JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Añadir requerimiento de seguridad
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Aplicar la política de CORS antes de la autenticación y autorización
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

// Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();