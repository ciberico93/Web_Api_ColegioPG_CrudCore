using AppColegioPG.Context;
using AppColegioPG.Models;
using AppColegioPG.Services;
using AppColegioPG.Services.IServices;
using AppColegioPG.Utilidades;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//************* creamos una variable y llamamos a la conexión que creamos  Appsettings.json

var ConnectionString = builder.Configuration.GetConnectionString("CadenaSQL");

// Registramos el contexto de la base de datos

builder.Services.AddDbContext<ColegioPGDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services.AddScoped<IMetodos<Cursos>, CursosServices>();

builder.Services.AddScoped<IMetodos<Alumnos>, AlumnosServices>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(option =>
{
    option.AddPolicy("NuevaPolitica",app =>
    {
            app.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


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

app.UseCors("NuevaPolitica");

app.Run();
