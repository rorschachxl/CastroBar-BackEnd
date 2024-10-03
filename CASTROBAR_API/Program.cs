using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", polity =>
    {
        polity.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//registro de dependencias
builder.Services.AddScoped<UsuarioRepositorio>(provider =>
    { 
        var configuration = provider.GetService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        return new UsuarioRepositorio(connectionString);
    });

builder.Services.AddScoped<UsuarioService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
