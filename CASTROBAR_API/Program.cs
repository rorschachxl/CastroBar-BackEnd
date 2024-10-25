using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Obtener la clave secreta desde la configuraci�n
var SecretKey = builder.Configuration["Key:secretKey"];
if (string.IsNullOrEmpty(SecretKey))
{
    throw new InvalidOperationException("SecretKey no est� configurada correctamente.");
}

// Configurar la autenticaci�n JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Configuraci�n de Swagger para autenticaci�n JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CastroBar API", Version = "v1" });

    // A�adir definici�n de seguridad JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // A�adir requerimiento de seguridad
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
            new string[] {}
        }
    });
});

// Otros servicios
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("2"));
});
builder.Services.AddControllers();
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
builder.Services.AddTransient<IMetodoPagoRepository, MetodoPagoRepository>();
builder.Services.AddTransient<IMesaRepository, MesaRepository>();
builder.Services.AddTransient<ProductoService>();
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

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware de autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();