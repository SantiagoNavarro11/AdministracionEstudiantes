using Mectronics.AdministracionEstudiantes.Repositorio;
using Mectronics.AdministracionEstudiantes.Repositorio.Repositorios;
using Mectronics.AdministracionEstudiantes.Servicio.Servicios;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRol;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS antes de builder.Build()
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500") // URL del frontend
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

// Agregar servicios a la contenedor
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Registrar AutoMapper con el ensamblado que contiene los perfiles
builder.Services.AddAutoMapper(typeof(AutoMapeador));

// Inyección de dependencias
builder.Services.AddScoped<IConexionBaseDatos, ConexionBaseDatos>();
builder.Services.AddScoped<IRolServicio, RolServicio>();
builder.Services.AddScoped<IRolRepositorio, RolRepositorio>();

builder.Services.AddControllers();

// Configurar Swagger para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Roles",
        Version = "v1",
        Description = "API para la gestión de roles.",
        Contact = new OpenApiContact
        {
            Name = "Soporte API",
            Email = "davidnavarro038@gmail.com"
        }
    });

    // Incluir comentarios XML en Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Habilitar CORS antes de UseAuthorization()
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
