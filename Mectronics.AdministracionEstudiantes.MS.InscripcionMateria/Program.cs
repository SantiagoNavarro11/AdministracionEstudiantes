using Mectronics.AdministracionEstudiantes.Repositorio;
using Mectronics.AdministracionEstudiantes.Repositorio.Repositorios;
using Mectronics.AdministracionEstudiantes.Servicio.Servicios;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IInscripcionMaterias;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});


// Agregar `IConfiguration` al contenedor de dependencias
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registrar AutoMapper con el ensamblado que contiene los perfiles
builder.Services.AddAutoMapper(typeof(AutoMapeador));

//Inyeccion de Dependencias 
builder.Services.AddScoped<IConexionBaseDatos, ConexionBaseDatos>();
builder.Services.AddScoped<IInscripcionMateriaServicio, InscripcionMateriaServicio>();
builder.Services.AddScoped<IInscripcionMateriaRepositorio, InscripcionMateriaRepositorio>();
builder.Services.AddScoped<IMateriaRepositorio, MateriaRepositorio>();

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger para la documentaci n de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Inscripci n Materias",
        Version = "v1",
        Description = "API de Inscripci n Materias",
        Contact = new OpenApiContact
        {
            Name = "Soporte API",
            Email = "davidnavarro038@gmail.com"
        }
    });

    // Obtener la ruta del archivo XML de documentaci n
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

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
