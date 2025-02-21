using Mectronics.AdministracionEstudiantes.Repositorio;
using Mectronics.AdministracionEstudiantes.Repositorio.Repositorios;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IInscripcionMaterias;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using Microsoft.OpenApi.Models;
using Mectronics.AdministracionEstudiantes.Servicio.Servicios;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Agregar `IConfiguration` al contenedor de dependencias
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registrar AutoMapper con el ensamblado que contiene los perfiles
builder.Services.AddAutoMapper(typeof(AutoMapeador));

//Inyeccion de Dependencias 
builder.Services.AddScoped<IConexionBaseDatos, ConexionBaseDatos>();
builder.Services.AddScoped<IInscripcionMateriaServicio, InscripcionMateriaServicio>();
builder.Services.AddScoped<IInscripcionMateriaRepositorio, InscripcionMateriaRepositorio>();

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Tiendas",
        Version = "v1",
        Description = "API para la gestión de estudiantes.",
        Contact = new OpenApiContact
        {
            Name = "Soporte API",
            Email = "soporte@insoftin.com"
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
