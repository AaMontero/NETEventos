using EventosServicio.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<pruebaTecnicaDBContext>(
    options=> options.UseSqlServer(builder.Configuration.GetConnectionString("CADENASQL"))
    );

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(builder.Configuration.GetConnectionString("URL_FRONT"))
                    //.SetIsOriginAllowed(origin => true) 
                    .AllowCredentials()); // allow credentials
// Configuración del pipeline de HTTP

// Mapear los controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();