using Mectronics.AdministracionEstudiantes.Repositorio;
using Mectronics.AdministracionEstudiantes.Repositorio.Repositorios;
using Mectronics.AdministracionEstudiantes.Servicio;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de CORS ANTES de builder.Build()
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

// Add services to the container.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapeador));

// Inyección de dependencias
builder.Services.AddScoped<IConexionBaseDatos, ConexionBaseDatos>();
builder.Services.AddScoped<IMateriaServicio, MateriaServicio>();
builder.Services.AddScoped<IMateriaRepositorio, MateriaRepositorio>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Materias.",
        Version = "v1",
        Description = "API para la gestión de materias.",
        Contact = new OpenApiContact
        {
            Name = "Soporte API",
            Email = "davidnavarro038@gmail.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configurar el middleware en el orden correcto
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar CORS ANTES de `UseAuthorization()`
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
