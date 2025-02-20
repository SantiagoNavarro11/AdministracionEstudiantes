using Mectronics.AdministracionEstudiantes.Repositorio;
using Mectronics.AdministracionEstudiantes.Repositorio.Repositorios;
using Mectronics.AdministracionEstudiantes.Servicio;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using Microsoft.OpenApi.Models;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registrar AutoMapper con el ensamblado que contiene los perfiles
builder.Services.AddAutoMapper(typeof(AutoMapeador));

//Inyeccion de Dependencias
builder.Services.AddScoped<IConexionBaseDatos, ConexionBaseDatos>();
builder.Services.AddScoped<IMateriaServicio,MateriaServicio>();
builder.Services.AddScoped<IMateriaRepositorio, MateriaRepositorio>();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configurar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de estudiantes.",
        Version = "v1",
        Description = "API para la gestión de gestion de estudiantes.",
        Contact = new OpenApiContact
        {
            Name = "Soporte API",
            Email = "davidnavarro038@gmail.com"
        }
    });

    // Obtener la ruta del archivo XML de documentación
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Incluir comentarios XML en Swagger
    options.IncludeXmlComments(xmlPath);
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

app.Run();
