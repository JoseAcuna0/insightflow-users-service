using users_service;
using users_service.src.Interface;
using users_service.src.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Punto de entrada principal de la aplicación Users Service.
/// Configura el host, servicios, middlewares y ejecuta la API REST.
/// </summary>

// --- CONFIGURACIÓN DEL HOST ---

/// <summary>
/// Inicializa el constructor de la aplicación web con opciones personalizadas.
/// Se configura explícitamente el ContentRootPath para evitar errores de
/// monitoreo de archivos (inotify) en entornos Linux o contenedores Docker.
/// </summary>
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

/// <summary>
/// Carga la configuración desde el archivo appsettings.json.
/// Se deshabilita reloadOnChange para evitar errores de observadores de archivos
/// en entornos de despliegue en la nube.
/// </summary>
builder.Configuration.AddJsonFile(
    "appsettings.json",
    optional: true,
    reloadOnChange: false
);


// --- 1. CONFIGURACIÓN DE SERVICIOS (Inyección de Dependencias) ---

/// <summary>
/// Registra los controladores de la API y configura el serializador JSON.
/// Se utiliza camelCase para asegurar compatibilidad con clientes frontend
/// desarrollados en JavaScript/TypeScript.
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            System.Text.Json.JsonNamingPolicy.CamelCase;
    });

/// <summary>
/// Registra los servicios necesarios para la generación de documentación
/// OpenAPI/Swagger.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Registro del servicio de negocio IUserService.
/// Se utiliza AddSingleton para mantener una única instancia en memoria
/// durante todo el ciclo de vida de la aplicación.
/// </summary>
builder.Services.AddSingleton<IUserService, UserService>();

/// <summary>
/// Configuración de política CORS.
/// Permite el acceso desde cualquier origen, método y encabezado,
/// facilitando la integración con el frontend.
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// --- 2. CONFIGURACIÓN DEL MIDDLEWARE (Pipeline de Peticiones) ---

/// <summary>
/// Construye la aplicación a partir de la configuración definida.
/// </summary>
var app = builder.Build();

/// <summary>
/// Habilita Swagger y la interfaz Swagger UI.
/// Se deja disponible tanto en desarrollo como en producción.
/// </summary>
app.UseSwagger();
app.UseSwaggerUI();

/// <summary>
/// Habilita la política CORS definida previamente.
/// Debe ejecutarse antes del mapeo de controladores.
/// </summary>
app.UseCors();

/// <summary>
/// Redirección HTTPS deshabilitada.
/// En entornos cloud la terminación SSL es gestionada por un proxy inverso.
/// </summary>
// app.UseHttpsRedirection();

/// <summary>
/// Mapea los controladores para manejar las solicitudes HTTP entrantes.
/// </summary>
app.MapControllers();

/// <summary>
/// Inicia la ejecución de la aplicación.
/// </summary>
app.Run();
