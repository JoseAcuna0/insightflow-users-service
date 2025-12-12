
using users_service;
using users_service.src.Interface;
using users_service.src.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS ---

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>(); 

// AÑADIDO: Configuración de CORS
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

// CRÍTICO: Mover Swagger y SwaggerUI FUERA del condicional de desarrollo
// Esto fuerza la habilitación en Render (Producción)
app.UseSwagger();
app.UseSwaggerUI(); 

// AÑADIDO: Habilitar la política CORS
app.UseCors();

app.UseHttpsRedirection(); 

app.MapControllers(); 

app.Run();