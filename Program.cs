
using users_service;
using users_service.src.Interface;
using users_service.src.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

// --- CRÍTICO: ARREGLO PARA CIERRE INMEDIATO (INOTIFY) ---
// Se crea el builder con opciones explícitas para evitar errores de monitoreo de archivos.
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // Asegura que no se monitoreen archivos de configuración que causan el error de 'inotify'
    ContentRootPath = Directory.GetCurrentDirectory() 
});

// Opcional: Asegurarse de que el appsettings.json no se recargue, aunque el constructor anterior ya ayuda mucho.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);


// --- 1. CONFIGURACIÓN DE SERVICIOS ---

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto soluciona el problema de mayúsculas/minúsculas (PascalCase -> camelCase)
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    }); 
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>(); 

// Configuración de CORS (Permisiva para Desarrollo/Taller)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});


// --- 2. CONFIGURACIÓN DEL MIDDLEWARE ---

var app = builder.Build();

// CRÍTICO: Habilitar Swagger en producción (Render)
app.UseSwagger();
app.UseSwaggerUI(); 

// Habilitar la política CORS
app.UseCors();

// OPCIONAL: Comentar para evitar el warning 'Failed to determine the https port' en Render
// app.UseHttpsRedirection(); 

app.MapControllers(); 

app.Run();