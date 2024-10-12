using CASTROBAR_API.Models;
using CASTROBAR_API.Repositories;
using CASTROBAR_API.Services;
using CASTROBAR_API.Utilities;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DbAadd54CastrobarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<UsuariosRepository>();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<ProductoService>();
builder.Services.AddTransient<TokenAndEncript>();
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
