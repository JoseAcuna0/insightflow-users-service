
using users_service.src.Controllers;
using users_service.src.Interface;
using users_service.src.Services;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>(); 


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



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(); 


app.UseHttpsRedirection();

app.UseCors();

app.MapControllers(); 

app.Run();